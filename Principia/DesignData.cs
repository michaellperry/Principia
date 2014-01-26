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
                await CourseBy(individual,
                    "More",
                    "More stuff to see how this scrolls.");
            }
            await CourseBy(individual,
                "Provable Code",
                "Techniques for mathematically proving the correctness of software.");
            await CourseBy(individual,
                "Patterns for Building Distributed Systems for the Enterprise",
                "Model driven architectures, CQRS, Event Sourcing, and Domain Driven Design for the rest of us.");
            var xamlPatterns = await CourseBy(individual,
                "XAML Patterns",
                "In the spirit of Design Patterns by the Gang of Four, XAML Patterns defines a pattern language for rich client applications.");
            await CourseBy(individual,
                "The Parse Mobile Backend with Windows 8",
                "Building Windows Store applications using the Parse mobile backend as a service.");

            xamlPatterns.Description =
                "Build applications at a higher level of abstraction. This set of interrelated patterns solves common UI application design problems in a way that keeps both developers and designers happy. Build applications faster, and make them more maintainable, on any XAML stack.";

            courseSelection.SelectedCourse = xamlPatterns;

            await CreateCourseOutline(xamlPatterns, courseSelection.ClipSelection);
        }

        private static async Task<Course> CourseBy(Individual individual, string title, string shortDescription)
        {
            var course = await individual.Community.AddFactAsync(new Course());
            course.Title = title;
            course.ShortDescription = shortDescription;
            await individual.Community.AddFactAsync(new Share(individual, course));
            return course;
        }

        private static async Task CreateCourseOutline(Course course, Principia.Courses.Models.ClipSelectionModel clipSelection)
        {
            await ModuleIn(course, "XAML Basics");
            await ModuleIn(course, "Blend Techniques");
            var composition = await ModuleIn(course, "Composition Patterns");
            await ModuleIn(course, "View Model Patterns");
            await ModuleIn(course, "Design Time Data Patterns");
            await ModuleIn(course, "Behavioral Patterns");
            await ModuleIn(course, "Animation Patterns");

            await CreateModuleClips(composition, clipSelection);
        }

        private static async Task<Module> ModuleIn(Course course, string title)
        {
            var module = await course.NewModule();
            module.Title = title;
            module.Ordinal = course.Modules.Count();
            return module;
        }

        private static async Task CreateModuleClips(Module module, Courses.Models.ClipSelectionModel clipSelection)
        {
            await ClipIn(module, "Introduction");
            await ClipIn(module, "Grid Layout");
            await ClipIn(module, "Balanced Whitespace");
            var overflow = await ClipIn(module, "Overflow");
            await ClipIn(module, "Justified ListBox");
            await ClipIn(module, "Extension Grid");
            await ClipIn(module, "Assets");
            await ClipIn(module, "Control Templates");
            await ClipIn(module, "Control Extension");
            await ClipIn(module, "Implicit Data Templates");
            await ClipIn(module, "Conclusion");

            clipSelection.SelectedClip = overflow;
        }

        private static async Task<Clip> ClipIn(Module module, string title)
        {
            var clip = await module.NewClip();
            clip.Title = title;
            clip.Ordinal = module.Clips.Count();
            return clip;
        }
    }
}
