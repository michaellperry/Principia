using Principia.Courses.ViewModels;
using UpdateControls.Fields;
using UpdateControls.XAML;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using XamlVisibility = Windows.UI.Xaml.Visibility;

namespace Principia.Courses.Views
{
    public sealed partial class ModuleHeaderView : UserControl
    {
        private ModuleHeaderViewModel _viewModel;
        private Binding _isOpen;
        private Binding _isSelected;

        public ModuleHeaderView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModel = ForView.Unwrap<ModuleHeaderViewModel>(DataContext);
            _isOpen = Binding.Bind(() => _viewModel != null && _viewModel.IsOpen,
                delegate(bool open)
                {
                    if (open)
                        ShowClips();
                    else
                        HideClips();
                });
            _isSelected = Binding.Bind(() => _viewModel != null && _viewModel.IsSelected,
                delegate(bool selected)
                {
                    if (selected)
                        ShowSelection();
                    else
                        HideSelection();
                });
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            _isOpen.Dispose();
            _isSelected.Dispose();
        }

        private void ShowClips()
        {
            Clips.Visibility = XamlVisibility.Visible;
        }

        private void HideClips()
        {
            Clips.Visibility = XamlVisibility.Collapsed;
        }

        private void ShowSelection()
        {
            SelectedBorder.Visibility = XamlVisibility.Visible;
        }

        private void HideSelection()
        {
            SelectedBorder.Visibility = XamlVisibility.Collapsed;
        }

        private void ModuleHeader_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (_viewModel != null)
                _viewModel.Select();
            e.Handled = true;
        }
    }
}
