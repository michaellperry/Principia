using Principia.Model;
using UpdateControls.Fields;

namespace Principia.Courses.Models
{
    public class ClipSelectionModel
    {
        private Independent<Module> _selectedModule = new Independent<Module>();
        private Independent<Clip> _selectedClip = new Independent<Clip>();

        public Module SelectedModule
        {
            get { return _selectedModule; }
            set { _selectedModule.Value = value; }
        }

        public Clip SelectedClip
        {
            get { return _selectedClip; }
            set { _selectedClip.Value = value; }
        }
    }
}
