using System.Threading.Tasks;

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
    }
}
