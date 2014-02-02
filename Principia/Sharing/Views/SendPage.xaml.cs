using Principia.Common;
using Principia.Sharing.ViewModels;
using System;
using UpdateControls.XAML;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Principia.Sharing.Views
{
    public sealed partial class SendPage : Page
    {
        private NavigationHelper _navigationHelper;

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
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
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

                request.Data.Properties.Title = "Invitation to collaborate in Principia";
                request.Data.Properties.Description =
                    "Send a link to friends, so that they can collaborate on this course.";
                request.Data.SetText(
                    "<p>Please collaborate with me on a course. Click the link below to open Principia, " +
                    "and I will grant you access.</p><br/>" +
                    String.Format("<a href=\"{0}\">{0}</a>", viewModel.Uri.AbsoluteUri));
                request.Data.SetWebLink(viewModel.Uri);
            }
        }
    }
}
