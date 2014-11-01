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
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        public Login()
        {
            InitializeComponent();
            Loaded += Login_Loaded;
            // Rbn_Login_Unchecked(null, null);
        }

        void Login_Loaded(object sender, RoutedEventArgs e)
        {
            Rbn_Login_Unchecked(null, null);
        }

        private void Rbn_Login_Checked(object sender, RoutedEventArgs e)
        {
            Tbx_Nombre_TextChanged(sender, null);
            Tbx_Contraseña_TextChanged(sender, null);
            Lbl_Nombre.Visibility = Visibility.Hidden;
            Tbx_Nombre.Visibility = Visibility.Hidden;
            Lbl_ConfirmarContraseña.Visibility = Visibility.Hidden;
            Tbx_ConfirmarContraseña.Visibility = Visibility.Hidden;
            Lbl_CorreoRegistrado.Visibility = System.Windows.Visibility.Hidden;
            Chb_SinContraseña.Visibility = System.Windows.Visibility.Visible;
            Chb_SinContraseña_Unchecked(sender, e);
            ActualizaSiguiente();
        }

        private void Rbn_Login_Unchecked(object sender, RoutedEventArgs e)
        {
            Tbx_Nombre_TextChanged(sender, null);
            Tbx_Contraseña_TextChanged(sender, null);
            Lbl_ClaveErronea.Visibility = System.Windows.Visibility.Hidden;
            Lbl_Nombre.Visibility = Visibility.Visible;
            Tbx_Nombre.Visibility = Visibility.Visible;
            Lbl_ConfirmarContraseña.Visibility = Visibility.Visible;
            Tbx_ConfirmarContraseña.Visibility = Visibility.Visible;

            Chb_SinContraseña.Visibility = System.Windows.Visibility.Hidden;
            Chb_SinContraseña_Unchecked(sender, e);
            ActualizaSiguiente();
        }
        public bool ValidaUsuario()
        {
            if (Grd_Url.IsVisible)
            {
                eClockBase.CeC_SesionBase Sesion = CeC_Sesion.ObtenSesion(this);
                Sesion.RutaServicios = Tbx_URL.Text;
                Sesion.GuardaDatos();
            }

            if (Rbn_Registro.IsChecked.GetValueOrDefault(false) == true)
            {
                if (Tbx_Contraseña.Password != Tbx_ConfirmarContraseña.Password)
                    return false;
                //eClockBase.Controladores.us
            }
            else
            {
            }
            return true;
        }

        private void Tbx_Contraseña_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Tbx_Contraseña.Password == "")
                Lbl_RequiereClave.Visibility = Visibility.Visible;
            else
                Lbl_RequiereClave.Visibility = Visibility.Hidden;

            if (Rbn_Registro.IsChecked.GetValueOrDefault(false) && Tbx_Contraseña.Password != Tbx_ConfirmarContraseña.Password)
                Lbl_ClaveNoCoincide.Visibility = Visibility.Visible;
            else
                Lbl_ClaveNoCoincide.Visibility = Visibility.Hidden;
            ActualizaSiguiente();
        }

        private void Tbx_ConfirmarContraseña_TextChanged(object sender, TextChangedEventArgs e)
        {
            Tbx_Contraseña_TextChanged(sender, e);
        }

        private void Chb_SinContraseña_Checked(object sender, RoutedEventArgs e)
        {
            Btn_RecuperarClave.Visibility = System.Windows.Visibility.Visible;
            Tbx_Contraseña.Visibility = System.Windows.Visibility.Hidden;
            Lbl_Contraseña.Visibility = System.Windows.Visibility.Hidden;
            Controles.UC_Wizard.sMostrarBotones(false, false, true);
        }

        private void Chb_SinContraseña_Unchecked(object sender, RoutedEventArgs e)
        {
            Btn_RecuperarClave.Visibility = System.Windows.Visibility.Hidden;
            Tbx_Contraseña.Visibility = System.Windows.Visibility.Visible;
            Lbl_Contraseña.Visibility = System.Windows.Visibility.Visible;
            //Controles.UC_Wizard.sMostrarBotones(true, true, true);
            ActualizaSiguiente();
        }

        private void Btn_RecuperarClave_Click(object sender, RoutedEventArgs e)
        {

        }

        public void ActualizaSiguiente()
        {
            if (Lbl_RequiereEMail.Visibility == System.Windows.Visibility.Visible
                || Lbl_ClaveNoCoincide.Visibility == Visibility.Visible
                || Lbl_RequiereClave.Visibility == Visibility.Visible
                )
                Controles.UC_Wizard.sMostrarBotones(false, false, true);
            else
                Controles.UC_Wizard.sMostrarBotones(false, true, true);
        }
        private void Tbx_Email_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Tbx_Email.Text == "")
                Lbl_RequiereEMail.Visibility = Visibility.Visible;
            else
                Lbl_RequiereEMail.Visibility = Visibility.Hidden;
            ActualizaSiguiente();
        }

        private void XGrid_NAutentificado_TextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void Tbx_Contraseña_TextInput(object sender, TextCompositionEventArgs e)
        {
            Tbx_Contraseña_TextChanged(sender, null);
        }

        private void Tbx_ConfirmarContraseña_TextInput(object sender, TextCompositionEventArgs e)
        {
            Tbx_Contraseña_TextChanged(sender, null);
        }

        private void Tbx_ConfirmarContraseña_KeyUp(object sender, KeyEventArgs e)
        {
            Tbx_Contraseña_TextChanged(sender, null);
        }

        private void Tbx_Contraseña_KeyUp(object sender, KeyEventArgs e)
        {
            Tbx_Contraseña_TextChanged(sender, null);
        }

        private void Tbx_Nombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Rbn_Registro.IsChecked.GetValueOrDefault(false) == true && Tbx_Nombre.Text == "")
                Lbl_RequiereNombre.Visibility = Visibility.Visible;
            else
                Lbl_RequiereNombre.Visibility = Visibility.Hidden;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Grd_Url.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void UC_WizardTitulo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Grd_Url.Visibility = System.Windows.Visibility.Visible;
            eClockBase.CeC_SesionBase Sesion= CeC_Sesion.ObtenSesion(this);
            Tbx_URL.Text = Sesion.RutaServicios;
        }


    }
}
