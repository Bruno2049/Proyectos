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

namespace eClock5.Vista.Wizard
{
    /// <summary>
    /// Lógica de interacción para Sucursal.xaml
    /// </summary>
    public partial class Sucursal : UserControl
    {
        public bool SucursalElegir = false;
        public Sucursal()
        {
            InitializeComponent();
        }

        private void Rbn_SucursalElegir_Unchecked(object sender, RoutedEventArgs e)
        {
            Grd_SucursalElegir.Visibility = System.Windows.Visibility.Hidden;
            SucursalElegir = false;
        }

        private void Rbn_SucursalElegir_Checked(object sender, RoutedEventArgs e)
        {
            Grd_SucursalElegir.Visibility = System.Windows.Visibility.Visible;
            SucursalElegir = true;
        }
        void ActualizaBotones()
        {
            if (eClockBase.CeC.Convierte2Bool(Chb_Sincronizar.IsChecked))
                Controles.UC_Wizard.sMostrarBotones(true, true, true, false);
            else
                Controles.UC_Wizard.sMostrarBotones(true, false, true, true);
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ActualizaBotones();
        }
        public void GuardaDatos()
        {
            if (!eClockBase.CeC.Convierte2Bool(Chb_Sincronizar.IsChecked))
            {
                if (GuardaDatosEvent != null)
                    GuardaDatosEvent(-1);
                return;
            }
            if (eClockBase.CeC.Convierte2Bool(Rbn_SucursalNueva.IsChecked))
            {
                eClockBase.CeC_SesionBase SesionBase = CeC_Sesion.ObtenSesion(this);
                eClockBase.Controladores.Sesion Sesion = new eClockBase.Controladores.Sesion(SesionBase);
                Sesion.GuardaDatosEvent += Sesion_GuardaDatosEvent;
                eClockBase.Modelos.Model_SITIOS Sitio = new eClockBase.Modelos.Model_SITIOS();
                Sitio.SITIO_NOMBRE = Tbx_Nombre.Text;
                Sitio.SITIO_RESPONSABLE = Tbx_Responsable.Text;
                Sitio.SITIO_EMAIL = Tbx_EMail.Text;
                Sitio.SITIO_TELEFONOS = Tbx_Telefonos.Text;
                Sitio.SITIO_DIRECCION_1 = Tbx_Direccion_1.Text;
                Sitio.SITIO_CP = Tbx_CP.Text;
                Sitio.SITIO_CIUDAD = Tbx_Ciudad.Text;
                Sitio.SITIO_ESTADO = Tbx_Estado.Text;
                Sitio.SITIO_PAIS = Tbx_Pais.Text;
                Sesion.GuardaDatos("EC_SITIOS", "SITIO_ID", Sitio, true);
            }
            else
                if (GuardaDatosEvent != null)
                    GuardaDatosEvent(Cmb_Sitios.SeleccionadoInt);
        }

        void Sesion_GuardaDatosEvent(int Guardados)
        {
            bool bGuardados = false;
            if (Guardados > 0)
                bGuardados = true;
            if (GuardaDatosEvent != null)
                GuardaDatosEvent(Guardados);
        }
        public delegate void GuardaDatosArgs(int SitioID);
        public event GuardaDatosArgs GuardaDatosEvent;


        private void Sincronizar(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!eClockBase.CeC.Convierte2Bool(Chb_Sincronizar.IsChecked))
                {
                    Grd_Sucursal.IsEnabled = false;
                }
                else
                    Grd_Sucursal.IsEnabled = true;
                ActualizaBotones();
            }
            catch { }

        }

        private void UC_Combo_DatosActualizados()
        {
            if (Cmb_Sitios.m_Listado.Count > 0)
            {
                Cmb_Sitios.SelectedItem = Cmb_Sitios.m_Listado[0];
                Rbn_SucursalElegir.IsChecked = true;
            }
            else
                Rbn_SucursalNueva.IsChecked = true;
        }
    }
}
