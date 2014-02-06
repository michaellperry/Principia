using Principia.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Principia.Sharing.ViewModels
{
    public class CourseViewModel
    {
        private readonly Grant _grant;
        
        public CourseViewModel(Grant grant)
        {
            _grant = grant;
        }

        internal Grant Grant
        {
            get { return _grant; }
        }

        public string Title
        {
            get { return _grant.Course.Title; }
        }

        public string ShortDescription
        {
            get { return _grant.Course.ShortDescription; }
        }

        public bool CanJoin
        {
            get { return !_grant.Accepts.Any(); }
        }
    }
}
