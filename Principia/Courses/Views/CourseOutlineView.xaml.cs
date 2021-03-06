﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UpdateControls.XAML;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Principia.Courses.ViewModels;
using XamlVisibility = Windows.UI.Xaml.Visibility;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Principia.Courses.Views
{
    public sealed partial class CourseOutlineView : UserControl
    {
        private CourseOutlineViewModel _viewModel;
        private Binding _isSelected;

        public CourseOutlineView()
        {
            this.InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModel = ForView.Unwrap<CourseOutlineViewModel>(DataContext);
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
            _isSelected.Dispose();
        }

        private void Overview_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (_viewModel != null)
                _viewModel.Select();
        }

        private void ShowSelection()
        {
            SelectedBorder.Visibility = XamlVisibility.Visible;
        }

        private void HideSelection()
        {
            SelectedBorder.Visibility = XamlVisibility.Collapsed;
        }
    }
}
