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

        public object CourseOutline
        {
            get
            {
                return ViewModel(() => new Courses.ViewModels.CourseOutlineViewModel
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
                });
            }
        }

        public Courses.ViewModels.CourseDetailViewModel CourseDetail
        {
            get
            {
                return new Courses.ViewModels.CourseDetailViewModel
                {
                    Title = "XAML Patterns",
                    ShortDescription = "In the spirit of Design Patterns by the Gang of Four, XAML Patterns defines a pattern language for rich client applications.",
                    Description = "Build applications at a higher level of abstraction. This set of interrelated patterns solves common UI application design problems in a way that keeps both developers and designers happy. Build applications faster, and make them more maintainable, on any XAML stack.",
                    Tags = new List<Courses.ViewModels.TagViewModel>
                    {
                        Tag("WPF"),
                        Tag("Silverlight"),
                        Tag("Windows Phone"),
                        Tag("Windows 8"),
                        Tag("WinRT"),
                        Tag("MVVM"),
                        Tag("ReactiveUI"),
                        Tag("XAML"),
                        Tag("Patterns")
                    }
                };
            }
        }

        private static Principia.Courses.ViewModels.ModuleHeaderViewModel NormalModule(string title)
        {
            return new Courses.ViewModels.ModuleHeaderViewModel
            {
                Title = title
            };
        }

        private static Principia.Courses.ViewModels.ModuleHeaderViewModel SelectedModule(string title)
        {
            return new Courses.ViewModels.ModuleHeaderViewModel
            {
                Title = title,
                IsSelected = true,
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

        private Courses.ViewModels.TagViewModel Tag(string text)
        {
            return new Courses.ViewModels.TagViewModel
            {
                Text = text
            };
        }
    }
}
