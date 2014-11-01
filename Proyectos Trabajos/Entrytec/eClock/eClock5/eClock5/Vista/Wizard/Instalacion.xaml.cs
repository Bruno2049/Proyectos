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
    /// Lógica de interacción para Instalacion.xaml
    /// </summary>
    public partial class Instalacion : Window
    {
        eClockBase.CeC_SesionBase m_SesionBase = null;
        public Instalacion()
        {
            InitializeComponent();
            m_SesionBase = CeC_Sesion.ObtenSesion(this);
            eClockSyncModel = Modelos.eClockSync.Carga();
        }
        Login Ctr_Login;
        Licencia Ctr_Licencia;
        Suscripcion Ctr_Suscripcion;
        Sucursal Ctr_Sucursal;
        Terminales Ctr_Terminales;
        Modelos.eClockSync eClockSyncModel;
        private void UC_Wizard_OnSiguiente_1(int PosActual, string Control)
        {
            switch (Control)
            {
                case "Licencia":
                    {
                        this.IsEnabled = false;
                        Ctr_Licencia = Wizard.Pasos[PosActual] as Licencia;
                        eClockBase.Controladores.Licencias Lic = new eClockBase.Controladores.Licencias(m_SesionBase);
                        Lic.LicenciaCambio += Lic_LicenciaCambio;
                        if (Ctr_Licencia.Chb_NoTengoClave.IsChecked.GetValueOrDefault(false))
                        {
                            Lic.CreaUsaLicencia();
                        }
                        else
                        {
                            Lic.UsaLicencia(Ctr_Licencia.Tbx_Licencia.Text);
                        }
                    }
                    break;
                case "Login":
                    {
                        Ctr_Login = Wizard.Pasos[PosActual] as Login;
                        Ctr_Login.ValidaUsuario();
                        if (Ctr_Login.Rbn_Login.IsChecked.GetValueOrDefault())
                        {
                            eClockBase.Controladores.Sesion Sesion = new eClockBase.Controladores.Sesion(m_SesionBase);
                            Sesion.CreaSesionCompleted += Sesion_CreaSesionCompleted;
                            Sesion.LogeoFinalizado += Sesion_LogeoFinalizado;
                            Sesion.CreaSesion_Inicio(Ctr_Login.Tbx_Email.Text, Ctr_Login.Tbx_Contraseña.Password, false);
                        }
                        else
                        {
                            eClockBase.Controladores.Usuarios CrearUsuario = new eClockBase.Controladores.Usuarios(m_SesionBase);
                            CrearUsuario.SuscripcionCreadaEvent += CrearUsuario_SuscripcionCreadaEvent;
                            CrearUsuario.CreaUsuarioSuscripcion(Ctr_Login.Tbx_Email.Text, Ctr_Login.Tbx_Contraseña.Password, Ctr_Login.Tbx_Nombre.Text, "Autogenerado", Ctr_Login.Tbx_Email.Text);
                        }
                        this.IsEnabled = false;


                    }
                    break;
                case "Suscripcion":
                    this.IsEnabled = false;
                    Ctr_Suscripcion.GuardaDatosEvent += delegate(bool Guardados)
                            {
                                this.IsEnabled = true;
                                if (Guardados)
                                    Wizard.Siguiente();
                            };
                    Ctr_Suscripcion.GuardaDatos();
                    break;
                case "Sucursal":
                    {
                        GuardaSucursal();
                    }
                    break;
                default:
                    Wizard.Siguiente();
                    break;
            }

        }

        int Sitio_ID = -1;
        void GuardaSucursal(bool Cerrar = false)
        {
            Ctr_Terminales = (Terminales)Wizard.ObtenControl("Terminales");
            IsEnabled = false;
            Ctr_Sucursal = (Sucursal)Wizard.ObtenControl("Sucursal"); ;
            Ctr_Sucursal.GuardaDatosEvent += delegate(int SitioID)
            {
                this.IsEnabled = true;
                if (SitioID > 0)
                {
                    Ctr_Terminales.Sitio_ID = SitioID;
                    Sitio_ID = SitioID;
                    if (!Cerrar)
                        Wizard.Siguiente();
                    eClockBase.Controladores.Usuarios CrearUsuario = new eClockBase.Controladores.Usuarios(m_SesionBase);
                    CrearUsuario.ObtenerUsuarioSincronizadorEvent += delegate(eClockBase.Modelos.Model_USUARIOS UsuarioSuscripcion)
                    {
                        if (UsuarioSuscripcion != null)
                        {
                            eClockSyncModel.Ejecutar = true;
                            eClockSyncModel.Usuario = UsuarioSuscripcion.USUARIO_USUARIO;
                            eClockSyncModel.Password = Modelos.eClockSync.CalculaHash(UsuarioSuscripcion.USUARIO_CLAVE);
                            eClockSyncModel.Sitios = SitioID.ToString();
                            eClockSyncModel.Guarda();
                            if (Cerrar)
                                Close();
                        }
                    };
                    CrearUsuario.ObtenerUsuarioSincronizador();
                }
                else
                {
                    Ctr_Terminales.Sitio_ID = -1;
                    eClockSyncModel.Ejecutar = false;
                    eClockSyncModel.Usuario = "";
                    eClockSyncModel.Password = "";
                    eClockSyncModel.Sitios = "";
                    eClockSyncModel.Guarda();
                    if (Cerrar)
                        Close();
                }

            };
            Ctr_Sucursal.GuardaDatos();
        }

        void GuardaTerminales(bool Cerrar = false)
        {
            Ctr_Terminales = (Terminales)Wizard.ObtenControl("Terminales");
            IsEnabled = false;
            Ctr_Terminales.GuardarEvent += delegate(bool Guardados)
                {
                    this.IsEnabled = true;
                    if (Guardados)
                    {
                        if (!Cerrar)
                            Wizard.Siguiente();
                        else
                            Close();
                    }
                };
            Ctr_Terminales.Guardar();
        }



        void Lic_LicenciaCambio(bool Correcta)
        {
            this.IsEnabled = true;
            if (Correcta)
                Wizard.Siguiente();
            else
                Ctr_Licencia.Lbl_Incorrecta.Visibility = System.Windows.Visibility.Visible;
        }

        void Sesion_LogeoFinalizado(object sender, EventArgs e)
        {
            if (m_SesionBase.EstaLogeado())
            {
                Ctr_Suscripcion = (Suscripcion)Wizard.ObtenControl("Suscripcion");
                Ctr_Suscripcion.CargaDatos();
                Wizard.Siguiente();
            }
        }

        void Sesion_CreaSesionCompleted(object sender, eClockBase.ES_Sesion.CreaSesionCompletedEventArgs e)
        {
            this.IsEnabled = true;
            if (!m_SesionBase.EstaLogeado())
                Ctr_Login.Lbl_ClaveErronea.Visibility = System.Windows.Visibility.Visible;
        }

        void CrearUsuario_SuscripcionCreadaEvent(bool Guardado, int Resultado)
        {
            this.IsEnabled = true;
            if (Guardado)
            {
                Ctr_Login.Lbl_CorreoRegistrado.Visibility = System.Windows.Visibility.Hidden;
                Ctr_Login.XGrid_NAutentificado.Visibility = System.Windows.Visibility.Hidden;
                Ctr_Suscripcion = (Suscripcion)Wizard.ObtenControl("Suscripcion");
                Ctr_Suscripcion.CargaDatos();
                Wizard.Siguiente();

            }
            else
            {
                switch (Resultado)
                {
                    case -9:
                        Ctr_Login.Lbl_CorreoRegistrado.Visibility = System.Windows.Visibility.Visible;
                        break;
                }
            }

        }

        private void UC_Wizard_OnCancelar_1(int PosActual, string Control)
        {
            this.Close();
        }

        private void UC_Wizard_OnFinalizar_1(int PosActual, string Control)
        {
            switch (Control)
            {
                case "Sucursal":
                    GuardaSucursal(true);                    
                    break;
                case "Terminales":
                    GuardaTerminales(true);
                    break;
                default:
                    Close();
                    break;
            }
        }

        private void Wizard_Loaded(object sender, RoutedEventArgs e)
        {
            eClock5.eClock.AsignaIcono(this);
            BaseModificada.Localizaciones.sLocaliza(this);
        }


    }
}
