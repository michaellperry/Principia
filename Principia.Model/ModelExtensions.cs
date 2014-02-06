using System.Linq;
using System;
using System.Threading.Tasks;
using UpdateControls.Correspondence;

namespace Principia.Model
{
    public static class ModelExtensions
    {
        public static async Task<Course> NewCourse(this Individual individual)
        {
            var course = await individual.Community.AddFactAsync(new Course());
            var token = await individual.Community.AddFactAsync(new Token(Guid.NewGuid().ToString()));
            var profile = await individual.Community.AddFactAsync(new Profile(individual));
            var request = await individual.Community.AddFactAsync(new Request(profile, token));
            var grant = await individual.Community.AddFactAsync(new Grant(request, course));
            var accept = await individual.Community.AddFactAsync(new Accept(grant));
            return course;
        }

        public static async Task<Module> NewModule(this Course course)
        {
            var courseContent = await course.Community.AddFactAsync(new CourseContent(course));
            var module = await course.Community.AddFactAsync(new Module(courseContent));
            return module;
        }

        public static async Task<Clip> NewClip(this Module module)
        {
            var courseContent = await module.CourseContent.EnsureAsync();
            var clip = await module.Community.AddFactAsync(new Clip(courseContent));
            await module.Community.AddFactAsync(new ClipModule(
                clip, module, Enumerable.Empty<ClipModule>()));
            return clip;
        }

        public static async Task<int> NextOrdinal<T, R>(this Result<T> items, Func<T, TransientDisputable<R, int>> ordinalProperty)
            where T : CorrespondenceFact
            where R : CorrespondenceFact
        {
            var loadedItems = await items.EnsureAsync();
            if (!loadedItems.Any())
                return 1;

            var ordinals = await Task.WhenAll(loadedItems.Select(item =>
                ordinalProperty(item).EnsureAsync()));
            return ordinals.Max(o => o.Value) + 1;
        }

        public static async Task<Profile> NewProfile(this Individual individual)
        {
            return await individual.Community.AddFactAsync(new Profile(individual));
        }

        public static async Task<Request> NewRequest(this Token token, Individual individual)
        {
            var profile = await individual.NewProfile();
            return await token.Community.AddFactAsync(new Request(profile, token));
        }

        public static async Task<Grant> NewGrant(this Request request, Course course)
        {
            return await request.Community.AddFactAsync(new Grant(request, course));
        }

        public static async Task<Accept> NewAccept(this Grant grant)
        {
            return await grant.Community.AddFactAsync(new Accept(grant));
        }
    }
}
