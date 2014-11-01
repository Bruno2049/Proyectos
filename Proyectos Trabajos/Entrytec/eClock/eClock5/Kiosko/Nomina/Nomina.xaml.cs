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

namespace Kiosko.Nomina
{
    /// <summary>
    /// Lógica de interacción para Nomina.xaml
    /// </summary>
    public partial class Nomina : UserControl
    {
        public Nomina()
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
                case "Btn_Siguiente":
                    Controles.UC_MessageBoxFullScreen Msg = new Controles.UC_MessageBoxFullScreen();
                    if (Tbx_Contrasena.Password == "")
                    {
                        Msg.Mensaje = "Debe ingresar su password";
                        Msg.Mostrar(this);
                        return;
                    }
                    

                    eClockBase.CeC_SesionBase Sesion = CeC_Sesion.ObtenSesion(this);
                    eClockBase.Controladores.Usuarios US = new eClockBase.Controladores.Usuarios(Sesion);
                    US.ValidaPasswordEvent += US_ValidaPasswordEvent;
                    US.ValidaPassword(Tbx_Contrasena.Password);


                    break;

            }
        }

        void US_ValidaPasswordEvent(bool Resultado)
        {
            if (Resultado == true)
            {
                Kiosko.Generales.Main.MuestraComoDialogo(this, new Kiosko.Nomina.Listado(), this.Background);
            }
            else
            {
                Controles.UC_MessageBoxFullScreen Msg = new Controles.UC_MessageBoxFullScreen();
                Msg.Mensaje = "Error de contraseña";
                Msg.Mostrar(this);
                return;
            }
        }       

        private void ToolBar_BotonesCreados(object sender)
        {
            Teclado.BotonEnter = ToolBar.Btn_Default;
        }

        private void UserControl_Loaded_2(object sender, RoutedEventArgs e)
        {
            Teclado.FocoPrincipal = Tbx_Contrasena;
            Teclado.ControlesEnter.Add(Tbx_Contrasena);
            //Teclado.BotonEnter = ToolBar.Btn_Default;
        }


    }
}
