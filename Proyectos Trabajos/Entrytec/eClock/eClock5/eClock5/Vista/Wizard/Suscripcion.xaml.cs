using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#if !NETFX_CORE
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
#else
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
#endif
using Newtonsoft.Json;

namespace eClock5.Vista.Wizard
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
        public eClockBase.Modelos.Model_SUSCRIPCION SuscripcionActual = null;
        public void CargaDatos()
        {
            eClockBase.CeC_SesionBase SesionBase = CeC_Sesion.ObtenSesion(this);
            eClockBase.Controladores.Sesion Sesion = new eClockBase.Controladores.Sesion(SesionBase);
            Sesion.ObtenDatosEvent += Sesion_ObtenDatosEvent;
            SuscripcionActual = new eClockBase.Modelos.Model_SUSCRIPCION();
            SuscripcionActual.SUSCRIPCION_ID = SesionBase.SUSCRIPCION_ID_SELECCIONADA;
            Sesion.ObtenDatos("EC_SUSCRIPCION", "SUSCRIPCION_ID", SuscripcionActual);
        }

        void Sesion_ObtenDatosEvent(int Resultado, string Datos)
        {
            try
            {
                if (Resultado > 0)
                {
                    SuscripcionActual = eClockBase.Controladores.CeC_ZLib.Json2Object<eClockBase.Modelos.Model_SUSCRIPCION>(Datos);
                    Tbx_Nombre.Text = SuscripcionActual.SUSCRIPCION_NOMBRE;
                    Tbx_Razon.Text = SuscripcionActual.SUSCRIPCION_RAZON;
                    Tbx_RFC.Text = SuscripcionActual.SUSCRIPCION_RFC;
                    Tbx_Direccion_1.Text = SuscripcionActual.SUSCRIPCION_DIRECCION1;
                    Tbx_CP.Text = SuscripcionActual.SUSCRIPCION_CP;
                    Tbx_Ciudad.Text = SuscripcionActual.SUSCRIPCION_CIUDAD;
                    Tbx_Estado.Text = SuscripcionActual.SUSCRIPCION_ESTADO;
                    Tbx_Pais.Text = SuscripcionActual.SUSCRIPCION_PAIS;
                }
            }
            catch (Exception ex)
            {
                eClockBase.CeC_Log.AgregaError(ex);
            }
        }
        public void GuardaDatos()
        {
            SuscripcionActual.SUSCRIPCION_NOMBRE = Tbx_Nombre.Text;
            SuscripcionActual.SUSCRIPCION_RAZON = Tbx_Razon.Text;
            SuscripcionActual.SUSCRIPCION_RFC = Tbx_RFC.Text;
            SuscripcionActual.SUSCRIPCION_DIRECCION1 = Tbx_Direccion_1.Text;
            SuscripcionActual.SUSCRIPCION_CP = Tbx_CP.Text;
            SuscripcionActual.SUSCRIPCION_CIUDAD = Tbx_Ciudad.Text;
            SuscripcionActual.SUSCRIPCION_ESTADO = Tbx_Estado.Text;
            SuscripcionActual.SUSCRIPCION_PAIS = Tbx_Pais.Text;
            eClockBase.CeC_SesionBase SesionBase = CeC_Sesion.ObtenSesion(this);
            eClockBase.Controladores.Sesion Sesion = new eClockBase.Controladores.Sesion(SesionBase);
            Sesion.GuardaDatosEvent += Sesion_GuardaDatosEvent;
            Sesion.GuardaDatos("EC_SUSCRIPCION", "SUSCRIPCION_ID",SuscripcionActual, false);
        }

        void Sesion_GuardaDatosEvent(int Guardados)
        {

            bool bGuardados = false;
            if (Guardados > 0)
                bGuardados = true;

            if (GuardaDatosEvent != null)
                GuardaDatosEvent(bGuardados);

        }
        public delegate void GuardaDatosArgs(bool Guardados);
        public event GuardaDatosArgs GuardaDatosEvent;


        void ActualizaBotones()
        {
            Controles.UC_Wizard.sMostrarBotones(true, true, true);
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ActualizaBotones();
        }
    }
}
