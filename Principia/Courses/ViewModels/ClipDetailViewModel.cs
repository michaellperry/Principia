using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Principia.Model;

namespace Principia.Courses.ViewModels
{
    public class ClipDetailViewModel
    {
        private readonly Clip _clip;

        public ClipDetailViewModel(Clip clip)
        {
            _clip = clip;
        }

        public string Title
        {
            get { return _clip.Title; }
            set { _clip.Title = value; }
        }
    }
}
