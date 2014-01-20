using Principia.Model;
using UpdateControls.Fields;

namespace Principia.Courses.Models
{
    public class CourseSelectionModel
    {
        private Independent<Course> _selectedCourse = new Independent<Course>();

        public Course SelectedCourse
        {
            get { return _selectedCourse; }
            set { _selectedCourse.Value = value; }
        }
    }
}
