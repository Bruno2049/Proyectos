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

namespace eClock5.Vista.Configuracion
{
    /// <summary>
    /// Lógica de interacción para MiConfiguracion.xaml
    /// </summary>
    public partial class MiConfiguracion : UserControl
    {
        public MiConfiguracion()
        {
            InitializeComponent();
        }
        public delegate void SesionCerradaArgs(MiConfiguracion Control);
        public event SesionCerradaArgs SesionCerrada;
        eClockBase.CeC_SesionBase Sesion;
       private bool TieneErrores()
        {
            bool Errores = false;
            if (Tbx_ContrasenaAnt.Password != "")
                Lbl_ContrasenaAntError.Visibility = System.Windows.Visibility.Collapsed;
            else
            {
                Lbl_ContrasenaAntError.Visibility = System.Windows.Visibility.Visible;
                Errores = true;
            }


            if (Tbx_ContrasenaNueva.Password != "")
                Lbl_ContrasenaNuevaError.Visibility = System.Windows.Visibility.Collapsed;
            else
            {
                Lbl_ContrasenaNuevaError.Visibility = System.Windows.Visibility.Visible;
                Errores = true;
            }


            if (Tbx_ContrasenaNueva.Password != Tbx_ConfirmarContrasena.Password)
            {
                Lbl_ConfirmarContrasenaError.Visibility = System.Windows.Visibility.Visible;
                Errores = true;
            }
            else
                Lbl_ConfirmarContrasenaError.Visibility = System.Windows.Visibility.Collapsed;


            return Errores;
        }

        private void TextoCambio(object sender, RoutedEventArgs e)
        {
            TieneErrores();
        }

        private void Btn_Cambiarcontrasena_Click(object sender, RoutedEventArgs e)
        {
            eClockBase.CeC_SesionBase Sesion = CeC_Sesion.ObtenSesion(this);
            eClockBase.Controladores.Usuarios Us = new eClockBase.Controladores.Usuarios(Sesion);
            Us.CambioPasswordEvent += Us_CambioPasswordEvent;
            Us.CambioPassword(Tbx_ContrasenaAnt.Password, Tbx_ContrasenaNueva.Password);
            
        }

        bool Cerrar = false;
        void Us_CambioPasswordEvent(bool Resultado)
        {
            if(Resultado)
                Lbl_ContrasenaSatisfactorio.Visibility = System.Windows.Visibility.Visible;
            
        }

        private void Btn_CerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            CerrarSesion();
            
        }
        public void CerrarSesion()
        {
            
            eClockBase.Controladores.Sesion SE = new eClockBase.Controladores.Sesion(Sesion);
            SE.SesionCerrada += SE_SesionCerrada;
            SE.CerrarSesion();

        }

        void SE_SesionCerrada(bool Cerrado)
        {
            if(Cerrado)
                Application.Current.Shutdown();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Sesion = CeC_Sesion.ObtenSesion(this);
            if (Sesion.SUSCRIPCION_ID == 1)
            {
                Spn_Suscripcion.Visibility = System.Windows.Visibility.Visible;
            }
            Lbl_FechaCompilacion.Text = RetrieveLinkerTimestamp().ToString();
        }

        private void Btn_Cambiar_Click(object sender, RoutedEventArgs e)
        {
            if (Cmb_Suscripciones.SeleccionadoInt > 0)
            {
                Sesion.SUSCRIPCION_ID_SELECCIONADA = Cmb_Suscripciones.SeleccionadoInt;
                eClock5.BaseModificada.Localizaciones.sLocaliza(Window.GetWindow(this));
            }
        }

        private DateTime RetrieveLinkerTimestamp()
        {
            string filePath = System.Reflection.Assembly.GetCallingAssembly().Location;
            const int c_PeHeaderOffset = 60;
            const int c_LinkerTimestampOffset = 8;
            byte[] b = new byte[2048];
            System.IO.Stream s = null;

            try
            {
                s = new System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                s.Read(b, 0, 2048);
            }
            finally
            {
                if (s != null)
                {
                    s.Close();
                }
            }

            int i = System.BitConverter.ToInt32(b, c_PeHeaderOffset);
            int secondsSince1970 = System.BitConverter.ToInt32(b, i + c_LinkerTimestampOffset);
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0);
            dt = dt.AddSeconds(secondsSince1970);
            dt = dt.AddHours(TimeZone.CurrentTimeZone.GetUtcOffset(dt).Hours);
            return dt;
        }
    }
}
