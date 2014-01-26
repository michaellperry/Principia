using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Principia.Model;

namespace Principia.Courses.ViewModels
{
    public class ModuleDetailViewModel
    {
        private readonly Module _module;

        public ModuleDetailViewModel(Module module)
        {
            _module = module;
        }

        public string Title
        {
            get { return _module.Title; }
            set { _module.Title = value; }
        }
    }
}
