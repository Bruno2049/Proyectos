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

namespace eClock5.Vista.Terminales
{
    /// <summary>
    /// Lógica de interacción para EdicionTerminalesSuscripcion.xaml
    /// </summary>
    public partial class EdicionTerminalesSuscripcion : UserControl
    {
        eClockBase.Modelos.Model_TERMINALES Terminal = null;

        public int Llave;
        public EdicionTerminalesSuscripcion()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            Carga();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_IsVisibleChanged_1(object sender, DependencyPropertyChangedEventArgs e)
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
            //Se inicializan los valores del modelo en predeterminados
            Terminal = new eClockBase.Modelos.Model_TERMINALES();

            Terminal.TERMINAL_ID = -1;
            Terminal.TERMINAL_AGRUPACION = "";

            EsNuevo = true;
            Tbx_TERMINAL_ID.Text = "-1";
            Cbx_TIPO_TERMINAL_ACCESO_ID.SeleccionadoInt = 0;
            Tbx_TERMINAL_NOMBRE.Text = "";
            Cbx_SITIO_ID.SeleccionadoInt = 0;
            Cbx_MODELO_TERMINAL_ID.SeleccionadoInt = 0;
            Cbx_TIPO_TECNOLOGIA_ID.SeleccionadoInt = 0;
            Cbx_TIPO_TECNOLOGIA_ADD_ID.SeleccionadoInt = 0;
            Cbx_TERMINAL_CAMPO_LLAVE.SeleccionadoInt = 0;
            Cbx_TERMINAL_CAMPO_ADICIONAL.SeleccionadoInt = 0;
            Chb_TERMINAL_ENROLA.IsChecked = false;
            UC_TERMINAL_DIR.Valor = "";
            Chb_TERMINAL_ASISTENCIA.IsChecked = false;
            Tbx_TERMINAL_MINUTOS_DIF.Text = "";
            Tbx_TERMINAL_DESCRIPCION.Text = "";
            Img_TERMINAL_BIN.ImagenBytes = null;
            Tbx_TERMINAL_MODELO.Text = "";
            Tbx_TERMINAL_AGRUPACION.Text = "";
            Chb_USUARIO_BORRADO.IsChecked = false;

            //ActualizaPersona();
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
                eClockBase.Modelos.Model_TERMINALES TerminaleSeleccionada = eClockBase.Controladores.CeC_ZLib.Json2Object<eClockBase.Modelos.Model_TERMINALES>(Param.Parametro.ToString());
                eClockBase.Controladores.Sesion Se = new eClockBase.Controladores.Sesion(Sesion);
                Se.ObtenDatosEvent += Se_ObtenDatosEvent;
                Se.ObtenDatos("EC_TERMINALES", "TERMINAL_ID", TerminaleSeleccionada);

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
                    Terminal = eClockBase.Controladores.CeC_ZLib.Json2Object<eClockBase.Modelos.Model_TERMINALES>(Datos);
                    Tbx_TERMINAL_ID.Text = eClockBase.CeC.Convierte2String(Terminal.TERMINAL_ID);
                    Cbx_TIPO_TERMINAL_ACCESO_ID.SeleccionadoInt = Terminal.TIPO_TERMINAL_ACCESO_ID;
                    Tbx_TERMINAL_NOMBRE.Text = Terminal.TERMINAL_NOMBRE;
                    Cbx_SITIO_ID.SeleccionadoInt = Terminal.SITIO_ID;
                    Cbx_MODELO_TERMINAL_ID.SeleccionadoInt = Terminal.MODELO_TERMINAL_ID;
                    Cbx_TIPO_TECNOLOGIA_ID.SeleccionadoInt = Terminal.TIPO_TECNOLOGIA_ID;
                    Cbx_TIPO_TECNOLOGIA_ADD_ID.SeleccionadoInt = Terminal.TIPO_TECNOLOGIA_ADD_ID;
                    Cbx_TERMINAL_CAMPO_LLAVE.SeleccionadoString = Terminal.TERMINAL_CAMPO_LLAVE;
                    Cbx_TERMINAL_CAMPO_ADICIONAL.SeleccionadoString = Terminal.TERMINAL_CAMPO_ADICIONAL;
                    Chb_TERMINAL_ENROLA.IsChecked = Terminal.TERMINAL_ENROLA;
                    UC_TERMINAL_DIR.Valor = Terminal.TERMINAL_DIR;
                    Chb_TERMINAL_ASISTENCIA.IsChecked = Terminal.TERMINAL_ASISTENCIA;
                    Tbx_TERMINAL_MINUTOS_DIF.Text = eClockBase.CeC.Convierte2String(Terminal.TERMINAL_MINUTOS_DIF);
                    Tbx_TERMINAL_DESCRIPCION.Text = Terminal.TERMINAL_DESCRIPCION;
                    Img_TERMINAL_BIN.ImagenBytes = Terminal.TERMINAL_BIN;
                    Tbx_TERMINAL_MODELO.Text = Terminal.TERMINAL_MODELO;
                    Tbx_TERMINAL_AGRUPACION.Text = Terminal.TERMINAL_AGRUPACION;
                    Chb_USUARIO_BORRADO.IsChecked = Terminal.TERMINAL_BORRADO;
                }
                //ActualizaPersona();

            }
            catch (Exception ex)
            {
                eClockBase.CeC_Log.AgregaError(ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void Guardar()
        {
            Terminal.TIPO_TERMINAL_ACCESO_ID = Cbx_TIPO_TERMINAL_ACCESO_ID.SeleccionadoInt;
            Terminal.TERMINAL_NOMBRE = Tbx_TERMINAL_NOMBRE.Text;
            Terminal.SITIO_ID = Cbx_SITIO_ID.SeleccionadoInt;
            Terminal.MODELO_TERMINAL_ID = Cbx_MODELO_TERMINAL_ID.SeleccionadoInt;
            Terminal.TIPO_TECNOLOGIA_ID = Cbx_TIPO_TECNOLOGIA_ID.SeleccionadoInt;
            Terminal.TIPO_TECNOLOGIA_ADD_ID = Cbx_TIPO_TECNOLOGIA_ADD_ID.SeleccionadoInt;
            Terminal.TERMINAL_CAMPO_LLAVE = Cbx_TERMINAL_CAMPO_LLAVE.SeleccionadoString;
            Terminal.TERMINAL_CAMPO_ADICIONAL = Cbx_TERMINAL_CAMPO_ADICIONAL.SeleccionadoString;
            Terminal.TERMINAL_ENROLA = eClockBase.CeC.Convierte2Bool(Chb_TERMINAL_ENROLA.IsChecked);
            Terminal.TERMINAL_DIR = UC_TERMINAL_DIR.Valor;
            Terminal.TERMINAL_ASISTENCIA = eClockBase.CeC.Convierte2Bool(Chb_TERMINAL_ASISTENCIA.IsChecked);
            Terminal.TERMINAL_MINUTOS_DIF = eClockBase.CeC.Convierte2Int(Tbx_TERMINAL_MINUTOS_DIF.Text);
            Terminal.TERMINAL_DESCRIPCION = Tbx_TERMINAL_DESCRIPCION.Text;
            Terminal.TERMINAL_BIN = Img_TERMINAL_BIN.ImagenBytes;
            Terminal.TERMINAL_MODELO = Tbx_TERMINAL_MODELO.Text;
            Terminal.TERMINAL_AGRUPACION = Tbx_TERMINAL_AGRUPACION.Text;
            Terminal.TERMINAL_BORRADO = eClockBase.CeC.Convierte2Bool(Chb_USUARIO_BORRADO.IsChecked);

            eClockBase.Controladores.Sesion Se = new eClockBase.Controladores.Sesion(Sesion);
            Se.GuardaDatosEvent += Se_GuardaDatosEvent;
            Se.GuardaDatos("EC_TERMINALES", "TERMINAL_ID", Terminal, EsNuevo);
        }

        void Se_GuardaDatosEvent(int Guardados)
        {
            if (Guardados > 0)
                Cerrar();
            else
            {
                Sesion.MuestraMensaje("No se pudo guardar", 5);
            }

        }
        /// <summary>
        /// 
        /// </summary>
        void Cerrar()
        {
            Visibility = System.Windows.Visibility.Hidden;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Control"></param>
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

    }
}
