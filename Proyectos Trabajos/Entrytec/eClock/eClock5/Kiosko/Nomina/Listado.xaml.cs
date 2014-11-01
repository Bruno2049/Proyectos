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

namespace Kiosko.Nomina
{
    /// <summary>
    /// Lógica de interacción para Listado.xaml
    /// </summary>
    public partial class Listado : UserControl
    {
        public Listado()
        {
            InitializeComponent();
        }
        private void ToolBar_OnEventClickToolBar(eClock5.UC_ToolBar_Control Control)
        {
            switch (Control.Nombre)
            {
                case "Btn_Regresar":
                    this.Visibility = System.Windows.Visibility.Hidden;
                    break;

            }
        }

        private void Lst_Nominas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Detalles Dlg = new Detalles();

                Dlg.Tag = new eClock5.Clases.Parametros(true, Lst_Nominas.Seleccionado.Llave, Lst_Nominas);
                Generales.Main.MuestraComoDialogo(this, Dlg, this.Background);
            }
            catch { }
            
        }
    }
}
