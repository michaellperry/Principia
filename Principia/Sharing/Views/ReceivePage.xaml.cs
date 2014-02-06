using Principia.Common;
using Principia.Sharing.ViewModels;
using System;
using System.Linq;
using System.Collections.Generic;
using UpdateControls.Fields;
using UpdateControls.XAML;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;

namespace Principia.Sharing.Views
{
    public sealed partial class ReceivePage : Page
    {
        private NavigationHelper navigationHelper;
        private Independent<List<CourseViewModel>> _selectedCourses =
            new Independent<List<CourseViewModel>>(new List<CourseViewModel>());

        private Binding _anyCoursesSelected;

        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        public ReceivePage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);

            var uri = e.Parameter as Uri;
            var viewModel = ForView.Unwrap<ReceiveViewModel>(DataContext);
            if (viewModel != null && uri != null)
            {
                viewModel.SetUri(uri);
            }

            _anyCoursesSelected = Binding.Bind(
                () => _selectedCourses.Value.Any(
                    c => c.CanJoin),
                delegate(bool anyRequestsSelected)
                {
                    BottomAppBar.IsOpen = anyRequestsSelected;
                    BottomAppBar.IsSticky = anyRequestsSelected;
                    JoinButton.IsEnabled = anyRequestsSelected;
                });
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _anyCoursesSelected.Dispose();

            navigationHelper.OnNavigatedFrom(e);
        }

        private void Join_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = ForView.Unwrap<ReceiveViewModel>(DataContext);
            if (viewModel != null)
            {
                viewModel.Join(_selectedCourses.Value);
            }
        }

        private void RequestGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var courses =
                from item in CourseGridView.SelectedItems
                let course = ForView.Unwrap<CourseViewModel>(item)
                where course != null
                select course;
            _selectedCourses.Value = courses.ToList();
        }
    }
}
