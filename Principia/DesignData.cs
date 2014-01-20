using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Principia.Model;
using System.Threading.Tasks;

namespace Principia
{
    public class DesignData
    {
        public static async Task Create(
            Individual individual,
            Courses.Models.CourseSelectionModel courseSelection)
        {
            for (int i = 0; i < 20; ++i)
            {
                await CreateCourse(individual,
                    "More",
                    "More stuff to see how this scrolls.");
            }
            await CreateCourse(individual, 
                "Provable Code", 
                "Techniques for mathematically proving the correctness of software.");
            await CreateCourse(individual,
                "Patterns for Building Distributed Systems for the Enterprise",
                "Model driven architectures, CQRS, Event Sourcing, and Domain Driven Design for the rest of us.");
            var xamlPatterns = await CreateCourse(individual,
                "XAML Patterns",
                "In the spirit of Design Patterns by the Gang of Four, XAML Patterns defines a pattern language for rich client applications.");
            await CreateCourse(individual,
                "The Parse Mobile Backend with Windows 8",
                "Building Windows Store applications using the Parse mobile backend as a service.");

            xamlPatterns.Description =
                "Build applications at a higher level of abstraction. This set of interrelated patterns solves common UI application design problems in a way that keeps both developers and designers happy. Build applications faster, and make them more maintainable, on any XAML stack.";
            courseSelection.SelectedCourse = xamlPatterns;
        }

        private static async Task<Course> CreateCourse(Individual individual, string title, string shortDescription)
        {
            var course = await individual.Community.AddFactAsync(new Course());
            course.Title = title;
            course.ShortDescription = shortDescription;
            await individual.Community.AddFactAsync(new Share(individual, course));
            return course;
        }
    }
}
