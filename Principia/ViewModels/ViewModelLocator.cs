using System;
using System.Collections.Generic;
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

        public object Main
        {
            get
            {
                return ViewModel(() => new MainViewModel(
                    _synchronizationService.Community,
                    _synchronizationService.Individual));
            }
        }

        public object CourseList
        {
            get
            {
                return ViewModel(() => Courses.Factory.CourseListViewModel(
                    _synchronizationService.Individual));
            }
        }

        public Courses.ViewModels.CourseOutlineViewModel CourseOutline
        {
            get
            {
                return new Courses.ViewModels.CourseOutlineViewModel
                {
                    Modules = new List<Courses.ViewModels.ModuleHeaderViewModel>
                    {
                        NormalModule("XAML Basics"),
                        NormalModule("Blend Techniques"),
                        SelectedModule("Composition Patterns"),
                        NormalModule("View Model Patterns"),
                        NormalModule("Design-Time Data Patterns"),
                        NormalModule("Behavioral Patterns"),
                        NormalModule("Animation Patterns")
                    }
                };
            }
        }

        private static Principia.Courses.ViewModels.ModuleHeaderViewModel NormalModule(string title)
        {
            return new Courses.ViewModels.ModuleHeaderViewModel
            {
                Title = title,
                SelectionState = Courses.ViewModels.SelectionState.Normal,
                Clips = new List<Courses.ViewModels.ClipHeaderViewModel>
                {
                    Clip("You shouldn't see me")
                }
            };
        }

        private static Principia.Courses.ViewModels.ModuleHeaderViewModel SelectedModule(string title)
        {
            return new Courses.ViewModels.ModuleHeaderViewModel
            {
                Title = title,
                SelectionState = Courses.ViewModels.SelectionState.Selected,
                Clips = new List<Courses.ViewModels.ClipHeaderViewModel>
                {
                    Clip("Introduction"),
                    Clip("Balanced Whitespace"),
                    Clip("Overflow"),
                    Clip("Extension Grid"),
                    Clip("Assets"),
                    Clip("Control Templates"),
                    Clip("Implicit Data Templates"),
                    Clip("Conclusion")
                }
            };
        }

        private static Courses.ViewModels.ClipHeaderViewModel Clip(string title)
        {
            return new Courses.ViewModels.ClipHeaderViewModel
            {
                Title = title
            };
        }
    }
}
