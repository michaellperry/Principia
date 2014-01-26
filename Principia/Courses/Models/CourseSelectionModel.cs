using Principia.Model;
using System.Collections.Generic;
using UpdateControls.Fields;

namespace Principia.Courses.Models
{
    public class CourseSelectionModel
    {
        private Independent<Course> _selectedCourse = new Independent<Course>();
        private Dictionary<Course, ClipSelectionModel> _clipSelectionByCourse =
            new Dictionary<Course, ClipSelectionModel>();

        public Course SelectedCourse
        {
            get { return _selectedCourse; }
            set { _selectedCourse.Value = value; }
        }

        public ClipSelectionModel ClipSelection
        {
            get
            {
                Course course = SelectedCourse;
                if (course == null)
                    return null;

                ClipSelectionModel clipSelection;
                if (!_clipSelectionByCourse.TryGetValue(course, out clipSelection))
                {
                    clipSelection = new ClipSelectionModel();
                    _clipSelectionByCourse.Add(course, clipSelection);
                }
                return clipSelection;
            }
        }
    }
}
