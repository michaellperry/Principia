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
        public static async Task Create(Individual individual)
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
            await CreateCourse(individual,
                "XAML Patterns",
                "Design patterns for Microsoft client applications using XAML.");
            await CreateCourse(individual,
                "The Parse Mobile Backend with Windows 8",
                "Building Windows Store applications using the Parse mobile backend as a service.");
        }

        private static async Task CreateCourse(Individual individual, string title, string description)
        {
            var course = await individual.Community.AddFactAsync(new Course());
            course.Title = title;
            course.Description = description;
            await individual.Community.AddFactAsync(new Share(individual, course));
        }
    }
}
