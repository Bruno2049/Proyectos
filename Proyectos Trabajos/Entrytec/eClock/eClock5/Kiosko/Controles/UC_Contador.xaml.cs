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

namespace Kiosko.Controles
{
    /// <summary>
    /// Lógica de interacción para UC_Contador.xaml
    /// </summary>
    public partial class UC_Contador : UserControl
    {
        public UC_Contador()
        {
            InitializeComponent();
        }
        public string Texto
        {
            get
            {
                return Lbl_Texto.Text;
            }
            set
            {
                Lbl_Texto.Text = value;
            }
        }
    }
}
