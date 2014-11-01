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
using Microsoft.Win32;

namespace eClock5.Vista.Importar
{
    /// <summary>
    /// Lógica de interacción para Excel.xaml
    /// </summary>
    public partial class Excel : UserControl
    {
        public Excel()
        {
            InitializeComponent();
        }

        private void Btn_Examinar_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialogo = new OpenFileDialog();

            dialogo.Filter = "Archivos  Aceptados |*.xls;*.xlsx ";
            dialogo.ShowDialog();
            Tbx_SeleccionarArchivo.Text = dialogo.FileName;
        }
    }
}
