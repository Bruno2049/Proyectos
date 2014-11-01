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
    /// Lógica de interacción para Registro.xaml
    /// </summary>
    public partial class Registro : UserControl
    {
        public Registro()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Teclado.FocoPrincipal = Txt_Numempleado;
            Teclado.ControlesEnter.Add(TXt_ConfirmarCorreoElectronico);
            TieneErrores();
        }

        private void UC_ToolBar_OnEventClickToolBar(eClock5.UC_ToolBar_Control Control)
        {
            switch (Control.Nombre)
            {
                case "Btn_Regresar":
                    this.Visibility = System.Windows.Visibility.Hidden;
                    break;
                case "Btn_Siguiente":
                    Registrar();
                    break;
            }
        }
        
        public void Registrar()
        {
            if (TieneErrores())
                return;
            eClockBase.CeC_SesionBase Sesion = CeC_Sesion.ObtenSesion(this);
            eClockBase.Controladores.Usuarios US = new eClockBase.Controladores.Usuarios(Sesion);
            US.CreadoUsuarioEmpleado += US_CreadoUsuarioEmpleado;
            US.CreaUsuarioEmpleado(Txt_Numempleado.Text, Txt_Contrasena.Password, Txt_CorreoElectronico.Text);
           // US.CreaUsuarioEmpleado(TXT
        }

        bool Cerrar = false;
        void US_CreadoUsuarioEmpleado(string Resultado)
        {
            Controles.UC_MessageBoxFullScreen Msg = new Controles.UC_MessageBoxFullScreen();
            if (Resultado == "OK")
            {
                Msg.Mensaje = "Creado Satisfactoriamente";
                Cerrar = true;
            }
            else
            {
                Msg.Mensaje = "No se pudo crear";
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
            if (Txt_Numempleado.Text != "")
                Lbl_NoEmpleado.Visibility = System.Windows.Visibility.Collapsed;
            else
            {
                Lbl_NoEmpleado.Visibility = System.Windows.Visibility.Visible;
                Errores = true;
            }
            if (Txt_Contrasena.Password != "")
                Lbl_NoClave.Visibility = System.Windows.Visibility.Collapsed;
            else
            {
                Lbl_NoClave.Visibility = System.Windows.Visibility.Visible;
                Errores = true;
            }
            if (Txt_Contrasena.Password != Txt_ConfirmarContrasena.Password)
            {
                Lbl_CveNoCoincide.Visibility = System.Windows.Visibility.Visible;
                Errores = true;
            }
            else
                Lbl_CveNoCoincide.Visibility = System.Windows.Visibility.Collapsed;

            if (Txt_CorreoElectronico.Text != TXt_ConfirmarCorreoElectronico.Text)
            {
                Lbl_eMailNoCoincide.Visibility = System.Windows.Visibility.Visible;
                Errores = true;
            }
            else
                Lbl_eMailNoCoincide.Visibility = System.Windows.Visibility.Collapsed;
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

        private void ToolBar_BotonesCreados(object sender)
        {
            TieneErrores();
            Teclado.BotonEnter = ToolBar.Btn_Default;
        }



    }
}
