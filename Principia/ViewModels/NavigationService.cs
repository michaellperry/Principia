using Principia.Courses.Views;
using System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Principia.ViewModels
{
    public class NavigationService
    {
        private readonly INavigate _navigate;
        private readonly CoreDispatcher _coreDispatcher;

        public NavigationService(INavigate navigate)
        {
            _navigate = navigate;
            _coreDispatcher = Window.Current.Dispatcher;
        }

        public void GoToCoursePage()
        {
            NavigateTo<CoursePage>();
        }

        private async void NavigateTo<Page>()
        {
            await _coreDispatcher.RunAsync(CoreDispatcherPriority.Normal,
                delegate
                {
                    _navigate.Navigate(typeof(Page));
                });
        }
    }
}
