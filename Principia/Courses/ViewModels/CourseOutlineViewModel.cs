using Principia.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Principia.Courses.ViewModels
{
    public class CourseOutlineViewModel
    {
        private readonly Course _course;
        private readonly Func<Module, ModuleHeaderViewModel> _newModuleHeaderViewModel;
        
        public CourseOutlineViewModel(
            Course course,
            Func<Module, ModuleHeaderViewModel> newModuleHeaderViewModel)
        {
            _course = course;
            _newModuleHeaderViewModel = newModuleHeaderViewModel;
        }

        public IEnumerable<ModuleHeaderViewModel> Modules
        {
            get
            {
                return
                    from module in _course.Modules
                    orderby module.Ordinal.Value
                    select _newModuleHeaderViewModel(module);
            }
        }
    }
}
