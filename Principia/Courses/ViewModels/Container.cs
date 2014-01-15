using Principia.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Principia.Courses.ViewModels
{
    static class Container
    {
        public static CourseListViewModel CourseListViewModel(Individual individual)
        {
            Func<Share, CourseViewModel> createCourseViewModel = share =>
                new CourseViewModel(share);

            return new CourseListViewModel(individual, createCourseViewModel);
        }
    }
}
