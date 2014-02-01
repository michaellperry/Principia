using Principia.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Principia.Sharing.ViewModels
{
    public class CourseViewModel
    {
        private readonly Course _course;

        public CourseViewModel(Course course)
        {
            _course = course;
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
