using Principia.Courses.ViewModels;
using Principia.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Principia.Courses
{
    static class Factory
    {
        public static CourseListViewModel CourseListViewModel(Individual individual)
        {
            Func<Share, CourseViewModel> createCourseViewModel = share =>
                new CourseViewModel(share);

            return new CourseListViewModel(individual, createCourseViewModel);
        }
    }
}
