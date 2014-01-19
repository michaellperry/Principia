using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Principia.Courses.ViewModels
{
    public class CourseOutlineViewModel
    {
        public IEnumerable<ModuleHeaderViewModel> Modules { get; set; }
    }
}
