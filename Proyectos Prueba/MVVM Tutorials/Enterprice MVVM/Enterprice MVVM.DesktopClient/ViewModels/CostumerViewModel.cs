
namespace Enterprice_MVVM.DesktopClient.ViewModels
{
    using Windows;
    using Microsoft.Build.Framework;
    using System.ComponentModel;

    public class CostumerViewModel : ViewModel
    {
        private string _costumerName;

        [Required]
        public string CostumerName
        {
            get
            {
                return _costumerName;
            }
            set
            {
                _costumerName = value;
                NotifyPropertyChanged();
            }
        }

        protected override string OnValidate(string propertyName)
        {
            if (CostumerName != null && CostumerName.Length < 1)
                return "Customer name most bo greter than oe equal to 4 characters";
            
            return base.OnValidate(propertyName);
        }
    }
}
