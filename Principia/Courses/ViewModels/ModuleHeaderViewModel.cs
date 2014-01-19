using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Principia.Courses.ViewModels
{
    public class ModuleHeaderViewModel
    {
        public string Title { get; set; }
        public SelectionState SelectionState { get; set; }
        public IEnumerable<ClipHeaderViewModel> Clips { get; set; }
    }
}
