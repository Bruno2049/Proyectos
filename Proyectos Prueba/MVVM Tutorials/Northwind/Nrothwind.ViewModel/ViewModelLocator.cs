using Northwind.Application;

namespace Northwind.ViewModel
{
    public class ViewModelLocator
    {
        private static MainWindowViewModel _mainWindowViewModel;

        public static MainWindowViewModel MainWindowViewModelStatic
        {
            get
            {
                if (_mainWindowViewModel == null)
                {
                    _mainWindowViewModel = new MainWindowViewModel(new UIDataProvider());
                }
                return _mainWindowViewModel;
            }
        }
    }
}
