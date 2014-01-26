using Principia.Courses.Models;
using Principia.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using UpdateControls.XAML;

namespace Principia.Courses.ViewModels
{
    public class CourseOutlineViewModel
    {
        private readonly Course _course;
        private readonly ClipSelectionModel _clipSelection;
        private readonly Func<Module, ModuleHeaderViewModel> _newModuleHeaderViewModel;
        
        public CourseOutlineViewModel(
            Course course,
            ClipSelectionModel clipSelection,
            Func<Module, ModuleHeaderViewModel> newModuleHeaderViewModel)
        {
            _course = course;
            _clipSelection = clipSelection;
            _newModuleHeaderViewModel = newModuleHeaderViewModel;
        }

        public string Title
        {
            get { return _course.Title; }
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

        public ICommand NewModuleCommand
        {
            get
            {
                return MakeCommand
                    .Do(delegate
                    {
                        _course.Community.Perform(async delegate
                        {
                            var module = await _course.NewModule();
                            module.Ordinal = await _course.Modules.NextOrdinal(m => m.Ordinal);
                            _clipSelection.SelectedModule = module;
                            _clipSelection.SelectedClip = null;
                        });
                    });
            }
        }

        public ICommand NewClipCommand
        {
            get
            {
                return MakeCommand
                    .When(() => _clipSelection.SelectedModule != null)
                    .Do(delegate
                    {
                        var module = _clipSelection.SelectedModule;
                        module.Community.Perform(async delegate
                        {
                            var clip = await module.NewClip();
                            clip.Ordinal = await module.Clips.NextOrdinal(c => c.Ordinal);
                            _clipSelection.SelectedModule = module;
                            _clipSelection.SelectedClip = clip;
                        });
                    });
            }
        }
    }
}
