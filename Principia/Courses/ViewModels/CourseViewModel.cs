using Principia.Courses.Models;
using Principia.Model;

namespace Principia.Courses.ViewModels
{
    class CourseViewModel
    {
        private readonly Course _course;
        private readonly CourseSelectionModel _courseSelection;
        
        public CourseViewModel(
            Course course,
            CourseSelectionModel courseSelection)
        {
            _course = course;
            _courseSelection = courseSelection;
        }

        internal Course Course
        {
            get { return _course; }
        }

        public string Title
        {
            get { return _course.Title; }
        }

        public string ShortDescription
        {
            get { return _course.ShortDescription; }
        }

        public void Select()
        {
            _courseSelection.SelectedCourse = _course;
        }
    }
}
