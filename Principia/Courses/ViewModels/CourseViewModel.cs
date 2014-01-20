using Principia.Model;

namespace Principia.Courses.ViewModels
{
    class CourseViewModel
    {
        private readonly Course _course;
        
        public CourseViewModel(Course course)
        {
            _course = course;
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
    }
}
