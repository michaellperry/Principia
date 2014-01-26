﻿using Principia.Courses.Models;
using Principia.Courses.ViewModels;
using Principia.Model;
using Principia.ViewModels;
using System;

namespace Principia.Courses
{
    static class Factory
    {
        public static CourseListViewModel CourseListViewModel(
            Individual individual,
            CourseSelectionModel courseSelection,
            NavigationService navigationService)
        {
            Func<Course, CourseViewModel> createCourseViewModel = course =>
                new CourseViewModel(course, courseSelection);

            return new CourseListViewModel(
                individual,
                courseSelection,
                createCourseViewModel,
                navigationService);
        }

        public static CourseOutlineViewModel CourseOutlineViewModel(Course course, ClipSelectionModel clipSelection)
        {
            return new CourseOutlineViewModel(course, clipSelection,
                module => new ModuleHeaderViewModel(module, clipSelection,
                    clip => new ClipHeaderViewModel(clip)));
        }

        public static CourseDetailViewModel CourseDetailViewModel(Course course)
        {
            return new CourseDetailViewModel(course);
        }
    }
}
