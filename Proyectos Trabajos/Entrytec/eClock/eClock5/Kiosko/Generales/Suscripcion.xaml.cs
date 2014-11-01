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

namespace Kiosko.Generales
{
    /// <summary>
    /// Lógica de interacción para Suscripcion.xaml
    /// </summary>
    public partial class Suscripcion : UserControl
    {
        public Suscripcion()
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
                    SuscripcionID();
                    break;
            }
        }
        eClockBase.CeC_SesionBase Sesion;
        public void SuscripcionID()
        {

            Sesion = CeC_Sesion.ObtenSesion(this);
            eClockBase.Controladores.Suscripciones SU = new eClockBase.Controladores.Suscripciones(Sesion);
            SU.ObtenidoSuscripcionID += SU_ObtenidoSuscripcionID;
            SU.ObtenSuscripcionID(Tbx_NumSuscripcion.Text);
            SU.ObtenSuscripcionURL(Tbx_NumSuscripcion.Text);

            //US.CreaUsuarioEmpleado(Txt_Numempleado.Text, Txt_Contrasena.Password, Txt_CorreoElectronico.Text);
            // US.CreaUsuarioEmpleado(TXT
        }
        bool Cerrar = false;
        void SU_ObtenidoSuscripcionID(int SuscripcionID)
        {
            Controles.UC_MessageBoxFullScreen Msg = new Controles.UC_MessageBoxFullScreen();
            if (SuscripcionID > 0)
            {
                Msg.Mensaje = "Existe Suscripcion";
                Cerrar = true;
            }
            else
            {
                Msg.Mensaje = "No Existe la Suscripcion";
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

        private void Teclado_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Teclado.FocoPrincipal = Tbx_NumSuscripcion;
            Teclado.ControlesEnter.Add(Tbx_NumSuscripcion);
            
        }
        private void ToolBar_BotonesCreados(object sender)
        {
            Teclado.BotonEnter = ToolBar.Btn_Default;
        }

    }
}
