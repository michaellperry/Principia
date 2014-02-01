using System.Linq;
using Principia.Model;
using System.Collections.Generic;

namespace Principia.Courses.ViewModels
{
    public class CourseDetailViewModel
    {
        private readonly Course _course;

        public CourseDetailViewModel(Course course)
        {
            _course = course;
        }

        public string Title
        {
            get { return _course.Title; }
            set { _course.Title = value; }
        }

        public string ShortDescription
        {
            get { return _course.ShortDescription; }
            set { _course.ShortDescription = value; }
        }

        public string Description
        {
            get { return _course.Contents.Select(cc => cc.Description.Value).FirstOrDefault(); }
            set
            {
                _course.Community.Perform(async delegate
                {
                    var courseContent = await _course.Community.AddFactAsync(
                        new CourseContent(_course));
                    courseContent.Description = value;
                });
            }
        }

        public IEnumerable<TagViewModel> Tags { get; set; }
    }
}
