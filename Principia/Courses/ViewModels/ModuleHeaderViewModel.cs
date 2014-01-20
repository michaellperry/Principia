using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Principia.Model;

namespace Principia.Courses.ViewModels
{
    public class ModuleHeaderViewModel
    {
        private readonly Module _module;

        public ModuleHeaderViewModel(Module module)
        {
            _module = module;
        }

        public string Title
        {
            get { return _module.Title; }
        }

        public bool IsSelected { get; set; }

        public IEnumerable<ClipHeaderViewModel> Clips { get; set; }
    }
}
