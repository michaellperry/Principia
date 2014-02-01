using Principia.Courses.Models;
using Principia.Model;
using Principia.ViewModels;
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
        private readonly NavigationService _navigationService;
        private readonly Sharing.Models.ShareModel _shareModel;
        private readonly Func<Module, ModuleHeaderViewModel> _newModuleHeaderViewModel;
        
        public CourseOutlineViewModel(
            Course course,
            ClipSelectionModel clipSelection,
            NavigationService navigationService,
            Sharing.Models.ShareModel shareModel,
            Func<Module, ModuleHeaderViewModel> newModuleHeaderViewModel)
        {
            _course = course;
            _clipSelection = clipSelection;
            _navigationService = navigationService;
            _shareModel = shareModel;
            _newModuleHeaderViewModel = newModuleHeaderViewModel;
        }

        public string Title
        {
            get { return _course.Title; }
        }

        public bool IsSelected
        {
            get
            {
                return
                    _clipSelection.SelectedModule == null &&
                    _clipSelection.SelectedClip == null;
            }
        }

        public void Select()
        {
            _clipSelection.SelectedModule = null;
            _clipSelection.SelectedClip = null;
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

        public ICommand SendInvitationCommand
        {
            get
            {
                return MakeCommand
                    .Do(delegate
                    {
                        _course.Community.Perform(async delegate
                        {
                            _shareModel.Course = _course;
                            await _shareModel.CreateNewTokenAsync();
                            _navigationService.GoToSendPage();
                        });
                    });
            }
        }
    }
}
