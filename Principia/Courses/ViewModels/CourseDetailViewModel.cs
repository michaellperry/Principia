using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Principia.Courses.ViewModels
{
    public class CourseDetailViewModel
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public IEnumerable<TagViewModel> Tags { get; set; }
    }
}
