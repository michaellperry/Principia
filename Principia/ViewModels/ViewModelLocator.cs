using System;
using System.ComponentModel;
using System.Linq;
using UpdateControls.XAML;

namespace Principia.ViewModels
{
    public class ViewModelLocator : ViewModelLocatorBase
    {
        private readonly SynchronizationService _synchronizationService;

        public ViewModelLocator()
        {
            _synchronizationService = new SynchronizationService();
            //if (!DesignMode)
            //    _synchronizationService.Initialize();
            //else
                _synchronizationService.InitializeDesignMode();
        }

        public object CourseList
        {
            get
            {
                return ViewModel(() => Courses.ViewModels.Container.CourseListViewModel(
                    _synchronizationService.Individual));
            }
        }
    }
}