using Principia.Common;
using Principia.Sharing.ViewModels;
using System;
using System.Linq;
using UpdateControls.XAML;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;
using System.Collections.Generic;
using UpdateControls.Fields;

namespace Principia.Sharing.Views
{
    public sealed partial class SendPage : Page
    {
        private NavigationHelper _navigationHelper;
        private Independent<List<RequestViewModel>> _selectedRequests =
            new Independent<List<RequestViewModel>>(new List<RequestViewModel>());

        private Binding _anyRequestsSelected;

        public NavigationHelper NavigationHelper
        {
            get { return _navigationHelper; }
        }

        public SendPage()
        {
            InitializeComponent();
            _navigationHelper = new NavigationHelper(this);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _navigationHelper.OnNavigatedTo(e);

            DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += ShareTextHandler;

            _anyRequestsSelected = Binding.Bind(() => _selectedRequests.Value.Any(),
                delegate(bool anyRequestsSelected)
                {
                    BottomAppBar.IsOpen = anyRequestsSelected;
                    GrantAccessButton.IsEnabled = anyRequestsSelected;
                });
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _anyRequestsSelected.Dispose();

            DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested -= ShareTextHandler;

            _navigationHelper.OnNavigatedFrom(e);
        }

        private void ShareTextHandler(DataTransferManager sender, DataRequestedEventArgs e)
        {
            var viewModel = ForView.Unwrap<SendViewModel>(DataContext);

            if (viewModel != null)
            {
                DataRequest request = e.Request;

                string body =
                    "<p>Please collaborate with me on a course. Click the link below to open Principia, " +
                    "and I will grant you access.</p><br>" +
                    String.Format("<p><a href=\"{0}\">{0}</a></p>", viewModel.Uri.AbsoluteUri);
                string htmlFormat = HtmlFormatHelper.CreateHtmlFormat(body);

                request.Data.Properties.Title = "Invitation to collaborate in Principia";
                request.Data.Properties.Description =
                    "Send a link to friends, so that they can collaborate on this course.";
                request.Data.SetHtmlFormat(htmlFormat);
                request.Data.SetApplicationLink(viewModel.Uri);
            }
        }

        private void GrantAccess_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = ForView.Unwrap<SendViewModel>(DataContext);
            if (viewModel != null)
            {
                var requests =
                    from item in RequestGridView.SelectedItems
                    let request = ForView.Unwrap<RequestViewModel>(item)
                    where request != null
                    select request;
                viewModel.GrantAccess(requests);
            }
        }

        private void RequestGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var requests =
                from item in RequestGridView.SelectedItems
                let request = ForView.Unwrap<RequestViewModel>(item)
                where request != null
                select request;
            _selectedRequests.Value = requests.ToList();
        }
    }
}
