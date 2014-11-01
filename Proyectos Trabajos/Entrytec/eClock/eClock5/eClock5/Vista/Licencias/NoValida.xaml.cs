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
using System.Windows.Shapes;

namespace eClock5.Vista.Licencias
{
    /// <summary>
    /// Lógica de interacción para NoValida.xaml
    /// </summary>
    public partial class NoValida : Window
    {
        public NoValida()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BaseModificada.Localizaciones.sLocaliza(this);
        }

        private void Btn_Wizard_Click(object sender, RoutedEventArgs e)
        {
            Vista.Wizard.Instalacion Dlg = new Vista.Wizard.Instalacion();
            if (Dlg.ShowDialog() != true)
            {
                this.Close();
            }
        }
    }
}
