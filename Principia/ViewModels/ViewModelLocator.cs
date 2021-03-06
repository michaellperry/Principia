using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UpdateControls.XAML;
using Windows.UI.Xaml.Controls;

namespace Principia.ViewModels
{
    public class ViewModelLocator : ViewModelLocatorBase
    {
        private readonly SynchronizationService _synchronizationService;
        private readonly Courses.Models.CourseSelectionModel _courseSelection;
        private NavigationService _navigationService;

        public ViewModelLocator()
        {
            _synchronizationService = new SynchronizationService();
            _courseSelection = new Courses.Models.CourseSelectionModel();

            if (!DesignMode)
                _synchronizationService.Initialize();
            else
                _synchronizationService.InitializeDesignMode(_courseSelection);
        }

        public void InitializeNavigationService(INavigate value)
        {
            _navigationService = new NavigationService(value);
        }

        public object CourseList
        {
            get
            {
                return ViewModel(() => Courses.Factory.CourseListViewModel(
                    _synchronizationService.Community,
                    _synchronizationService.Individual,
                    _courseSelection,
                    _navigationService));
            }
        }

        public object CourseOutline
        {
            get
            {
                return ViewModel(delegate
                {
                    if (_courseSelection.SelectedCourse == null)
                        return null;
                    else
                    {
                        return Courses.Factory.CourseOutlineViewModel(
                            _synchronizationService.Community,
                            _courseSelection.SelectedCourse,
                            _courseSelection.ClipSelection,
                            _navigationService,
                            _synchronizationService.ShareModel);
                    }
                });
            }
        }

        public object CourseDetail
        {
            get
            {
                return ViewModel(delegate
                {
                    var clipSelection = _courseSelection.ClipSelection;
                    if (clipSelection == null)
                        return null;

                    if (clipSelection.SelectedClip != null)
                        return Courses.Factory.ClipDetailViewModel(
                            clipSelection.SelectedClip);

                    if (clipSelection.SelectedModule != null)
                        return Courses.Factory.ModuleDetailViewModel(
                            clipSelection.SelectedModule);

                    return Courses.Factory.CourseDetailViewModel(
                        _courseSelection.SelectedCourse);
                });
            }
        }

        public object Receive
        {
            get
            {
                return ViewModel(() => new Sharing.ViewModels.ReceiveViewModel(
                    _synchronizationService.Community,
                    _synchronizationService.ShareModel,
                    _synchronizationService.Individual));
            }
        }

        public object Send
        {
            get
            {
                return ViewModel(() => new Sharing.ViewModels.SendViewModel(
                    _synchronizationService.Community,
                    _synchronizationService.ShareModel,
                    _synchronizationService.Individual));
            }
        }
    }
}
