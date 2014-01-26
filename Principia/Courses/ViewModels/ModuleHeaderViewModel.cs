using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Principia.Model;
using Principia.Courses.Models;

namespace Principia.Courses.ViewModels
{
    public class ModuleHeaderViewModel
    {
        private readonly Module _module;
        private readonly ClipSelectionModel _clipSelection;
        private readonly Func<Clip, ClipHeaderViewModel> _newClipHeaderViewModel;
        
        public ModuleHeaderViewModel(
            Module module,
            ClipSelectionModel clipSelection,
            Func<Clip, ClipHeaderViewModel> newClipHeaderViewModel)
        {
            _module = module;
            _clipSelection = clipSelection;
            _newClipHeaderViewModel = newClipHeaderViewModel;
        }

        public string Title
        {
            get { return _module.Title.Value ?? "<<New module>>"; }
        }

        public bool IsSelected
        {
            get
            {
                return
                    _clipSelection.SelectedModule == _module &&
                    _clipSelection.SelectedClip == null;
            }
        }

        public IEnumerable<ClipHeaderViewModel> Clips
        {
            get
            {
                return
                    from clip in _module.Clips
                    orderby clip.Ordinal.Value
                    select _newClipHeaderViewModel(clip);
            }
        }
    }
}
