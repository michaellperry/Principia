using Principia.Courses.Models;
using Principia.Courses.ViewModels;
using Principia.Model;
using Principia.ViewModels;
using System;
using UpdateControls.Correspondence;

namespace Principia.Courses
{
    static class Factory
    {
        public static CourseListViewModel CourseListViewModel(
            ICommunity community,
            Individual individual,
            CourseSelectionModel courseSelection,
            NavigationService navigationService)
        {
            Func<Course, CourseViewModel> createCourseViewModel = course =>
                new CourseViewModel(course, courseSelection);

            return new CourseListViewModel(
                community,
                individual,
                courseSelection,
                createCourseViewModel,
                navigationService);
        }

        public static CourseOutlineViewModel CourseOutlineViewModel(
            ICommunity communtiy,
            Course course,
            ClipSelectionModel clipSelection,
            NavigationService navigationService,
            Sharing.Models.ShareModel shareModel)
        {
            return new CourseOutlineViewModel(communtiy, course, clipSelection, navigationService, shareModel,
                module => new ModuleHeaderViewModel(module, clipSelection,
                    clip => new ClipHeaderViewModel(module, clip, clipSelection)));
        }

        public static CourseDetailViewModel CourseDetailViewModel(Course course)
        {
            return new CourseDetailViewModel(course);
        }

        public static ModuleDetailViewModel ModuleDetailViewModel(Module module)
        {
            return new ModuleDetailViewModel(module);
        }

        public static ClipDetailViewModel ClipDetailViewModel(Clip clip)
        {
            return new ClipDetailViewModel(clip);
        }
    }
}
