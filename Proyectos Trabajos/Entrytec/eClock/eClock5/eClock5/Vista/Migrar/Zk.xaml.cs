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

namespace eClock5.Vista.Migrar
{
    /// <summary>
    /// Lógica de interacción para Zk.xaml
    /// </summary>
    public partial class Zk : UserControl
    {
        public Zk()
        {
            InitializeComponent();
        }
        OpenFileDialog ArchivoBD = new OpenFileDialog();

        private void Btn_Examinar_Click(object sender, RoutedEventArgs e)
        {
            ArchivoBD.Filter = "Archivos  Aceptados |*.mdb;";
            ArchivoBD.ShowDialog();
            Tbx_RutaDeLaCarpeta.Text = ArchivoBD.FileName;
        }

        private void Lbl_Siguiente_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
