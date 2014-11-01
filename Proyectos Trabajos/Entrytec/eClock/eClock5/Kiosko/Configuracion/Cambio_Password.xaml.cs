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

using eClock5.BaseModificada;
using eClock5;
using eClockBase;

namespace Kiosko.Configuracion
{
    /// <summary>
    /// Lógica de interacción para Cambio_Password.xaml
    /// </summary>
    public partial class Cambio_Password : UserControl
    {
        public Cambio_Password()
        {
            InitializeComponent();
        }


        private void UC_ToolBar_OnEventClickToolBar(eClock5.UC_ToolBar_Control Control)
        {
            switch (Control.Nombre)
            {
                case "Btn_Regresar":
                    this.Visibility = System.Windows.Visibility.Hidden;
                    break;
                case "Btn_Siguiente":
                    eClockBase.CeC_SesionBase Sesion = CeC_Sesion.ObtenSesion(this);
                    eClockBase.Controladores.Usuarios Us = new eClockBase.Controladores.Usuarios(Sesion);
                    Us.CambioPasswordEvent += Us_CambioPasswordEvent;
                    Us.CambioPassword(Tbx_ContrasenaAnt.Password, Tbx_ContrasenaNueva.Password);
                    break;
            }
        }
        bool Cerrar = false;
        void Us_CambioPasswordEvent(bool Resultado)
        {
            Controles.UC_MessageBoxFullScreen Msg = new Controles.UC_MessageBoxFullScreen();
            if (Resultado == true)
            {
                Msg.Mensaje = "Cambio de contraseña Satisfactorio";

                Cerrar = true;
            }
            else
            {
                Msg.Mensaje = "Error al cambiar contraseña";
                Msg.Imagen = null;
                Cerrar = false;
            }
            Msg.Cerrado += Msg_Cerrado;
            Msg.Mostrar(this);
        }
        void Msg_Cerrado()
        {
            if (Cerrar)
            {
                this.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private bool TieneErrores()
        {
            bool Errores = false;
            if (Tbx_ContrasenaAnt.Password != "")
                Lbl_ContrasenaAntError.Visibility = System.Windows.Visibility.Collapsed;
            else
            {
                Lbl_ContrasenaAntError.Visibility = System.Windows.Visibility.Visible;
                Errores = true;
            }


            if (Tbx_ContrasenaNueva.Password != "")
                Lbl_ContrasenaNuevaError.Visibility = System.Windows.Visibility.Collapsed;
            else
            {
                Lbl_ContrasenaNuevaError.Visibility = System.Windows.Visibility.Visible;
                Errores = true;
            }


            if (Tbx_ContrasenaNueva.Password != Tbx_ConfirmarContrasena.Password)
            {
                Lbl_ConfirmarContrasenaError.Visibility = System.Windows.Visibility.Visible;
                Errores = true;
            }
            else
                Lbl_ConfirmarContrasenaError.Visibility = System.Windows.Visibility.Collapsed;


            if (ToolBar.Btn_Default != null)
                if (Errores)
                    ToolBar.Btn_Default.Visibility = System.Windows.Visibility.Collapsed;
                else
                    ToolBar.Btn_Default.Visibility = System.Windows.Visibility.Visible;
            return Errores;
        }

        private void TextoCambio(object sender, RoutedEventArgs e)
        {
            TieneErrores();
        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            Teclado.FocoPrincipal = Tbx_ContrasenaAnt;
            Teclado.ControlesEnter.Add(Tbx_ConfirmarContrasena);
            TieneErrores();
        }
        private void ToolBar_BotonesCreados(object sender)
        {
            TieneErrores();
            Teclado.BotonEnter = ToolBar.Btn_Default;
        }

    }
}
