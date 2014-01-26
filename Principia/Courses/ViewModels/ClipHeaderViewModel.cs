using Principia.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Principia.Courses.ViewModels
{
    public class ClipHeaderViewModel
    {
        private readonly Clip _clip;

        public ClipHeaderViewModel(Clip clip)
        {
            _clip = clip;
        }

        public string Title
        {
            get { return _clip.Title ?? "<<New clip>>"; }
        }
    }
}
