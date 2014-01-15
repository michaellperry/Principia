using Principia.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Principia.Courses.ViewModels
{
    class CourseViewModel
    {
        private readonly Share _share;

        public CourseViewModel(Share share)
        {
            _share = share;            
        }

        public string Title
        {
            get { return _share.Course.Title; }
        }

        public string Description
        {
            get { return _share.Course.Description; }
        }
    }
}
