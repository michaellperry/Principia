﻿using Principia.Courses.Models;
using Principia.Model;
using Principia.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using UpdateControls.XAML;

namespace Principia.Courses.ViewModels
{
    class CourseListViewModel
    {
        private readonly Individual _individual;
        private readonly CourseSelectionModel _courseSelection;
        private readonly Func<Course, CourseViewModel> _createCourseViewModel;
        private readonly NavigationService _navigationService;
        
        public CourseListViewModel(
            Individual individual,
            CourseSelectionModel courseSelection,
            Func<Course, CourseViewModel> createCourseViewModel,
            NavigationService navigationService)
        {
            _individual = individual;
            _courseSelection = courseSelection;
            _createCourseViewModel = createCourseViewModel;
            _navigationService = navigationService;
        }

        public IEnumerable<CourseViewModel> Courses
        {
            get
            {
                return
                    from share in _individual.SharedCourses
                    select _createCourseViewModel(share.Course);
            }
        }

        public CourseViewModel SelectedCourse
        {
            get
            {
                if (_courseSelection.SelectedCourse == null)
                    return null;
                else
                    return _createCourseViewModel(_courseSelection.SelectedCourse);
            }
            set
            {
                if (value == null)
                    _courseSelection.SelectedCourse = null;
                else
                    _courseSelection.SelectedCourse = value.Course;
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
