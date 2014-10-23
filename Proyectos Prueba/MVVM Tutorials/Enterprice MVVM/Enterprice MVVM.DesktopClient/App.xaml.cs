using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Enterprice_MVVM.DesktopClient.Views;

namespace Enterprice_MVVM.DesktopClient
{
    using ViewModels;
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainWindow windows = new MainWindow
            {
                DataContext = new CostumerViewModel()
            };

            windows.Show();
        }
    }
}
