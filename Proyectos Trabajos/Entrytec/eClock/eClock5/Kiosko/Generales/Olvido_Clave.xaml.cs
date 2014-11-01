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
    /// Lógica de interacción para Olvido_Clave.xaml
    /// </summary>
    public partial class Olvido_Clave : UserControl
    {
        CeC_SesionBase m_SesionBase = null;
        public Olvido_Clave()
        {
            InitializeComponent();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Teclado.FocoPrincipal = Txt_NumEmpleado;
            Teclado.ControlesEnter.Add(Txt_NumEmpleado);
            
        }

        private void ToolBar_BotonesCreados(object sender)
        {
            Teclado.BotonEnter = ToolBar.Btn_Default;
        }

        private void UC_ToolBar_OnEventClickToolBar(eClock5.UC_ToolBar_Control Control)
        {
            switch (Control.Nombre)
            {
                case "Btn_Regresar":
                    this.Visibility = System.Windows.Visibility.Hidden;
                    break;
                case "Btn_Siguiente":
                    RecordarContrasena();
                    break;
            }   


        }


        public void RecordarContrasena()
        {
            m_SesionBase = CeC_Sesion.ObtenSesion(this);
            eClockBase.Controladores.Usuarios US = new eClockBase.Controladores.Usuarios(m_SesionBase);
            US.OlvidoClaveEmpleadoEvent += US_OlvidoClaveEmpleadoEvent;
            //OmarSuscripcion
            US.OlvidoClaveEmpleado(Txt_NumEmpleado.Text, m_SesionBase.Suscripcion);
        }

        bool Cerrar = false;
        void US_OlvidoClaveEmpleadoEvent(string Resultado)
        {
            Controles.UC_MessageBoxFullScreen Msg = new Controles.UC_MessageBoxFullScreen();
            if (Resultado == "OK")
            {
                Msg.Mensaje = "Contraseña Enviada a su E-mail si no recibe pasar a Capital Humano";
                Cerrar = true;
            }
            else
            {
                Msg.Mensaje = "Contraseña no Enviada--Favor de Pasar a Capital Humano";
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

    }
}
