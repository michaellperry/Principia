using Principia.Courses.ViewModels;
using UpdateControls.XAML;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Principia.Courses.Selectors
{
    public class DetailViewSelector : DataTemplateSelector
    {
        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (ForView.Unwrap<CourseDetailViewModel>(item) != null)
                return Application.Current.Resources["CourseDataTemplate"] as DataTemplate;
            if (ForView.Unwrap<ModuleDetailViewModel>(item) != null)
                return Application.Current.Resources["ModuleDataTemplate"] as DataTemplate;
            if (ForView.Unwrap<ClipDetailViewModel>(item) != null)
                return Application.Current.Resources["ClipDataTemplate"] as DataTemplate;

            return base.SelectTemplateCore(item, container);
        }
    }
}
