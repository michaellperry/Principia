using Principia.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Principia.Courses.ViewModels
{
    class CourseListViewModel
    {
        private readonly Individual _individual;
        private readonly Func<Share, CourseViewModel> _createCourseViewModel;

        public CourseListViewModel(Individual individual, Func<Share, CourseViewModel> createCourseViewModel)
        {
            _individual = individual;
            _createCourseViewModel = createCourseViewModel;
        }

        public IEnumerable<CourseViewModel> Courses
        {
            get
            {
                return
                    from share in _individual.SharedCourses
                    select _createCourseViewModel(share);
            }
        }
    }
}
