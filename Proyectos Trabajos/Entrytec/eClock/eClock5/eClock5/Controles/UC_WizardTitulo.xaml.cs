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
    /// Lógica de interacción para UC_WizardTitulo.xaml
    /// </summary>
    public partial class UC_WizardTitulo : UserControl
    {
        public string Titulo
        {
            get
            {
                return Lbl_Titulo.Text;
            }
            set
            {
                Lbl_Titulo.Text = value;
            }
        }

        public string SubTitulo
        {
            get
            {
                return Lbl_SubTitulo.Text;
            }
            set
            {
                Lbl_SubTitulo.Text = value;
            }
        }
        public UC_WizardTitulo()
        {
            InitializeComponent();
        }
    }
}
