using Principia.Courses.Models;
using Principia.Model;
using Principia.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using UpdateControls.Correspondence;
using UpdateControls.XAML;

namespace Principia.Courses.ViewModels
{
    class CourseListViewModel
    {
        private readonly ICommunity _community;
        private readonly Individual _individual;
        private readonly CourseSelectionModel _courseSelection;
        private readonly Func<Course, CourseViewModel> _createCourseViewModel;
        private readonly NavigationService _navigationService;
        
        public CourseListViewModel(
            ICommunity community,
            Individual individual,
            CourseSelectionModel courseSelection,
            Func<Course, CourseViewModel> createCourseViewModel,
            NavigationService navigationService)
        {
            _community = community;
            _individual = individual;
            _courseSelection = courseSelection;
            _createCourseViewModel = createCourseViewModel;
            _navigationService = navigationService;
        }

        public string LastException
        {
            get
            {
                if (_community.LastException == null)
                    return null;

                return _community.LastException.Message;
            }
        }

        public bool Synchronizing
        {
            get { return _community.Synchronizing; }
        }

        public IEnumerable<CourseViewModel> Courses
        {
            get
            {
                return
                    from accept in _individual.CoursesAccepted
                    select _createCourseViewModel(accept.Grant.Course);
            }
        }

        public ICommand NewCourse
        {
            get
            {
                return MakeCommand
                    .Do(delegate
                    {
                        _individual.Community.Perform(async delegate
                        {
                            var course = await _individual.NewCourse();
                            _courseSelection.SelectedCourse = course;
                            _navigationService.GoToCoursePage();
                        });
                    });
            }
        }
    }
}
