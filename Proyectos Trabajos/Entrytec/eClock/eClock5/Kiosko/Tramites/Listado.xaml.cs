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

using Newtonsoft.Json;
using eClock5.BaseModificada;
using eClock5;
using eClockBase;

namespace Kiosko.Tramites
{
    /// <summary>
    /// Lógica de interacción para Listado.xaml
    /// </summary>
    public partial class Listado : UserControl
    {
        bool Cerrar = false;
        public Listado()
        {
            InitializeComponent();
            Lst_Tramites.SelectionChanged += Lst_Tramites_SelectionChanged;
        }

        void Lst_Tramites_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Lst_Tramites.Seleccionado != null)
            {
                Tramites.Detalles Dlg = new Tramites.Detalles();
                Dlg.Llave = CeC.Convierte2Int(Lst_Tramites.Seleccionado.Llave);
                Kiosko.Generales.Main.MuestraComoDialogo(this, Dlg, this.Background);
            }
            else
            {
                Controles.UC_MessageBoxFullScreen Msg = new Controles.UC_MessageBoxFullScreen();
                Msg.Mensaje = "Seleccione un tipo de documento";
                Msg.Cerrado += Msg_Cerrado;
                Msg.Mostrar(this);
            }
        }

        private void UC_ToolBar_OnEventClickToolBar(eClock5.UC_ToolBar_Control Control)
        {
            switch (Control.Nombre)
            {
                case "Btn_Regresar":
                    this.Visibility = System.Windows.Visibility.Hidden;
                    break;
            }
        }
        void Msg_Cerrado()
        {
            if (Cerrar)
            {
                this.Visibility = System.Windows.Visibility.Hidden;
            }
        }

       
    }
}
