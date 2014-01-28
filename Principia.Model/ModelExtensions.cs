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
            await individual.Community.AddFactAsync(new Share(individual, course));
            return course;
        }

        public static async Task<Module> NewModule(this Course course)
        {
            var module = await course.Community.AddFactAsync(new Module(course));
            return module;
        }

        public static async Task<Clip> NewClip(this Module module)
        {
            var course = await module.Course.EnsureAsync();
            var clip = await module.Community.AddFactAsync(new Clip(course));
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
    }
}
