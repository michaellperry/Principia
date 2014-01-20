using Principia.Courses.ViewModels;
using System.ComponentModel;
using UpdateControls.XAML;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Principia.Courses.Views
{
    public sealed partial class ModuleHeaderView : UserControl
    {
        private INotifyPropertyChanged _inpc;

        public ModuleHeaderView()
        {
            InitializeComponent();

            DataContextChanged += ModuleHeaderView_DataContextChanged;
        }

        void ModuleHeaderView_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if (_inpc != null)
            {
                _inpc.PropertyChanged -= ViewModel_PropertyChanged;
            }

            var inpc = args.NewValue as INotifyPropertyChanged;
            if (inpc != null)
            {
                inpc.PropertyChanged += ViewModel_PropertyChanged;
                UpdateIsSelected();
            }

            _inpc = inpc;
        }

        void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsSelected")
            {
                UpdateIsSelected();
            }
        }

        private void UpdateIsSelected()
        {
            var viewModel = ForView.Unwrap<ModuleHeaderViewModel>(DataContext);
            if (viewModel != null)
            {
                if (viewModel.IsSelected)
                {
                    ShowClips();
                }
                else
                {
                    HideClips();
                }
            }
        }

        private void ShowClips()
        {
            Clips.Visibility = Windows.UI.Xaml.Visibility.Visible;
            SelectedBorder.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        private void HideClips()
        {
            Clips.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            SelectedBorder.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }
    }
}
