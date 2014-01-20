using Principia.Common;
using UpdateControls.XAML;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Principia.Courses.ViewModels;

namespace Principia.Courses.Views
{
    public sealed partial class CourseListPage : Page
    {
        private NavigationHelper navigationHelper;

        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }


        public CourseListPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
        }

        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        private void Course_ItemClick(object sender, ItemClickEventArgs e)
        {
            var courseViewModel = ForView.Unwrap<CourseViewModel>(e.ClickedItem);
            if (courseViewModel != null)
            {
                courseViewModel.Select();
                Frame.Navigate(typeof(CoursePage));
            }
        }
    }
}
