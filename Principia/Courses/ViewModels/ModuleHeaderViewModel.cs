using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Principia.Courses.ViewModels
{
    public class ModuleHeaderViewModel
    {
        public string Title { get; set; }
        public bool IsSelected { get; set; }
        public IEnumerable<ClipHeaderViewModel> Clips { get; set; }
    }
}
