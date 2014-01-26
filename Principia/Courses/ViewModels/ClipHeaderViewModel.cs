using Principia.Courses.Models;
using Principia.Model;

namespace Principia.Courses.ViewModels
{
    public class ClipHeaderViewModel
    {
        private readonly Module _module;
        private readonly Clip _clip;
        private readonly ClipSelectionModel _clipSelection;
        
        public ClipHeaderViewModel(
            Module module,
            Clip clip,
            ClipSelectionModel clipSelection)
        {
            _module = module;
            _clip = clip;
            _clipSelection = clipSelection;
        }

        public string Title
        {
            get { return _clip.Title.Value ?? "<<New clip>>"; }
        }

        public bool IsSelected
        {
            get { return _clipSelection.SelectedClip == _clip; }
        }

        public void Select()
        {
            _clipSelection.SelectedModule = _module;
            _clipSelection.SelectedClip = _clip;
        }
    }
}
