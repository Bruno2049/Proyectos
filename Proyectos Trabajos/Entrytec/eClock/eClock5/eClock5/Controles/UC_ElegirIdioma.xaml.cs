using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace eClock5.Controles
{
    /// <summary>
    /// Lógica de interacción para UC_ElegirIdioma.xaml
    /// </summary>
    public partial class UC_ElegirIdioma : UserControl
    {
        public UC_ElegirIdioma()
        {
            InitializeComponent();
        }

        Window Ventana
        {
            get {return  Window.GetWindow(this); }
        }

        private void Btn_Español_Click(object sender, RoutedEventArgs e)
        {
            BaseModificada.Localizaciones.sLocaliza(Ventana, "es");
        }

        private void Btn_Ingles_Click(object sender, RoutedEventArgs e)
        {
            BaseModificada.Localizaciones.sLocaliza(Ventana, "en");
        }

        private void Btn_Portugues_Click(object sender, RoutedEventArgs e)
        {
            BaseModificada.Localizaciones.sLocaliza(Ventana, "pt");
        }

        private void Btn_Italiano_Click(object sender, RoutedEventArgs e)
        {
            BaseModificada.Localizaciones.sLocaliza(Ventana, "it");
        }

        private void Btn_Frances_Click(object sender, RoutedEventArgs e)
        {
            BaseModificada.Localizaciones.sLocaliza(Ventana, "fr");
        }

        private void Btn_Lbl_Click(object sender, RoutedEventArgs e)
        {
            BaseModificada.Localizaciones.sLocaliza(Ventana, "C#");
        }
    }
}
