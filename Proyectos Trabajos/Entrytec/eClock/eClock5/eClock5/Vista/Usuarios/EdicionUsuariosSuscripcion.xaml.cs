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

namespace eClock5.Vista.Usuarios
{
    /// <summary>
    /// Lógica de interacción para EdicionUsuariosSuscripcion.xaml
    /// </summary>
    public partial class EdicionUsuariosSuscripcion : UserControl
    {
        eClockBase.Modelos.Model_USUARIOS Usuario = null;

        public int Llave;
        public EdicionUsuariosSuscripcion()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            Carga();

        }
        
        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue.Equals(true) && Sesion != null)
            {
                Carga();
            }
        }

        CeC_SesionBase Sesion;
        bool EsNuevo = true;

        void LimpiaVista()
        {
            try
            {
                ///Se inicializan los valores del modelo en predeterminados
                Usuario = new eClockBase.Modelos.Model_USUARIOS();
                Usuario.USUARIO_ID = -1;
                Usuario.SUSCRIPCION_ID = Sesion.SUSCRIPCION_ID_SELECCIONADA;
                Usuario.USUARIO_AGRUPACION = "";

                EsNuevo = true;
                Tbx_USUARIO_ID.Text = "-1";
                Tbx_USUARIO_USUARIO.Text = "";
                Tbx_USUARIO_NOMBRE.Text = "";
                Cbx_PERFIL_ID.SeleccionadoInt = 5;
                Tbx_USUARIO_DESCRIPCION.Text = "";
                Tbx_USUARIO_CLAVE.Password = "";
                Tbx_USUARIO_CLAVE_Cnf.Password = "";
                Tbx_USUARIO_EMAIL.Text = "";
                Chb_USUARIO_ESSUP.IsChecked = false;
                Chb_USUARIO_ESEMP.IsChecked = false;
                Cbx_PERSONA_ID.SeleccionadoInt = 0;
                Chb_USUARIO_BORRADO.IsChecked = false;
                ActualizaPersona();
            }
            catch { }
        }

        /// <summary>
        /// Carga los datos del Turno
        /// </summary>
        private void Carga()
        {
            try
            {
                Sesion = CeC_Sesion.ObtenSesion(this);
                LimpiaVista();
               

                Clases.Parametros Param = Clases.Parametros.Tag2Parametros(this.Tag);
                // Se llenar los campos con valores predeterminados
                eClockBase.Modelos.Model_USUARIOS UsuarioSeleccionado = eClockBase.Controladores.CeC_ZLib.Json2Object<eClockBase.Modelos.Model_USUARIOS>(Param.Parametro.ToString());
                eClockBase.Controladores.Sesion Se = new eClockBase.Controladores.Sesion(Sesion);
                Se.ObtenDatosEvent += Se_ObtenDatosEvent;
                Se.ObtenDatos("EC_USUARIOS", "USUARIO_ID", UsuarioSeleccionado);

            }
            catch (Exception ex)
            {
                CeC_Log.AgregaError(ex);
            }

        }

        void Se_ObtenDatosEvent(int Resultado, string Datos)
        {

            try
            {
                if (Resultado > 0)
                {
                    EsNuevo = false;
                    Usuario = eClockBase.Controladores.CeC_ZLib.Json2Object<eClockBase.Modelos.Model_USUARIOS>(Datos);
                    Tbx_USUARIO_ID.Text = eClockBase.CeC.Convierte2String(Usuario.USUARIO_ID);
                    //Cbx_PERFIL_ID.
                    Tbx_USUARIO_USUARIO.Text = Usuario.USUARIO_USUARIO;
                    Tbx_USUARIO_NOMBRE.Text = Usuario.USUARIO_NOMBRE;
                    Cbx_PERFIL_ID.SeleccionadoInt = Usuario.PERFIL_ID;
                    Tbx_USUARIO_DESCRIPCION.Text = Usuario.USUARIO_DESCRIPCION;
                    Tbx_USUARIO_CLAVE.Password = Usuario.USUARIO_CLAVE;
                    Tbx_USUARIO_CLAVE_Cnf.Password = Usuario.USUARIO_CLAVE;
                    Tbx_USUARIO_EMAIL.Text = Usuario.USUARIO_EMAIL;
                    Chb_USUARIO_ESSUP.IsChecked = Usuario.USUARIO_ESSUP;
                    Chb_USUARIO_ESEMP.IsChecked = Usuario.USUARIO_ESEMP;
                    Cbx_PERSONA_ID.SeleccionadoInt = Usuario.PERSONA_ID;
                    Chb_USUARIO_BORRADO.IsChecked = Usuario.USUARIO_BORRADO;
                    ActualizaPersona();
                }
            }
            catch (Exception ex)
            {
                eClockBase.CeC_Log.AgregaError(ex);
            }
        }        

        private void Guardar()
        {
            Usuario.USUARIO_USUARIO = Tbx_USUARIO_USUARIO.Text;
            Usuario.USUARIO_NOMBRE = Tbx_USUARIO_NOMBRE.Text;
            Usuario.PERFIL_ID = Cbx_PERFIL_ID.SeleccionadoInt;
            Usuario.USUARIO_DESCRIPCION = Tbx_USUARIO_DESCRIPCION.Text;
            Usuario.USUARIO_CLAVE = Tbx_USUARIO_CLAVE.Password;

            Usuario.USUARIO_EMAIL = Tbx_USUARIO_EMAIL.Text;
            Usuario.USUARIO_ESSUP = eClockBase.CeC.Convierte2Bool(Chb_USUARIO_ESSUP.IsChecked);
            Usuario.USUARIO_ESEMP = eClockBase.CeC.Convierte2Bool(Chb_USUARIO_ESEMP.IsChecked);
            Usuario.PERSONA_ID = Cbx_PERSONA_ID.SeleccionadoInt;
            Usuario.USUARIO_BORRADO = eClockBase.CeC.Convierte2Bool(Chb_USUARIO_BORRADO.IsChecked);
            eClockBase.Controladores.Sesion Se = new eClockBase.Controladores.Sesion(Sesion);
            Se.GuardaDatosEvent += Se_GuardaDatosEvent;
            Se.GuardaDatos("EC_USUARIOS", "USUARIO_ID", Usuario, EsNuevo);
        }

        void Se_GuardaDatosEvent(int Guardados)
        {
            try{
                if (Guardados > 0)
                    Cerrar();
                else
                {
                    Sesion.MuestraMensaje("No se pudo guardar", 5);
                }
            }
            catch{}

        }
        void Cerrar()
        {
            Visibility = System.Windows.Visibility.Hidden;
        }
        private void UC_ToolBar_OnEventClickToolBar(UC_ToolBar_Control Control)
        {
            switch (Control.Nombre)
            {
                case "Btn_Guardar":
                    Guardar();
                    break;
                case "Btn_Regresar":
                    Cerrar();
                    break;
            }
        }

        void ActualizaPersona()
        {
            if (eClockBase.CeC.Convierte2Bool(Chb_USUARIO_ESEMP.IsChecked))
                Cbx_PERSONA_ID.IsEnabled = true;
            else
                Cbx_PERSONA_ID.IsEnabled = false;
        }
        private void Checado(object sender, RoutedEventArgs e)
        {
            ActualizaPersona();
        }

        private void PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (Tbx_USUARIO_CLAVE.Password != Tbx_USUARIO_CLAVE_Cnf.Password)
                Lbl_USUARIO_CLAVE_NoCoincide.Visibility = System.Windows.Visibility.Visible;
            else
                Lbl_USUARIO_CLAVE_NoCoincide.Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}
