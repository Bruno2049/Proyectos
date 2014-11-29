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
    /// Lógica de interacción para Main.xaml
    /// </summary>
    public partial class Main : UserControl
    {
        public Main()
        {
            InitializeComponent();
        }
        public delegate void SesionCerradaArgs(Main Control);
        public event SesionCerradaArgs SesionCerrada;
        

        string Tablas = "PERMISOS,MENSAJES,VACACIONES,EC_TIPO_TRAMITE,EC_ACTIVIDADES,ASISTENCIAS,NOMINA";
        string Valores = "1,0,0,0,0,0,0,0,0,0,0";
        public int ObtenValor(string Tabla)
        {
            try
            {
                int Columna = CeC.PosicionEnSeparador(Tablas, Tabla, ",");
                return CeC.Convierte2Int(CeC.ObtenColumnaSeparador(Valores, ",", Columna));

            }
            catch { }
            return -9;
        }
        public void LimpiaPantalla()
        {
            Lbl_NombreEmpleado.Text = "";
            Lbl_NumEmpleado.Text = "";
            Lbl_UltimaSesion.Text = "";
            Btn_Permisos.NoAlertas = 0;
            Btn_Mensajes.NoAlertas = 0;
            Btn_Vacaciones.NoAlertas = 0;
            Btn_Tramites.NoAlertas = 0;
            Btn_CalendarioActividades.NoAlertas = 0;
            Btn_Asistencia.NoAlertas = 0;
            Btn_Nomina.NoAlertas = 0;
            Img_Foto.Source = null;
        }
        public void ActualizaNos()
        {
            eClockBase.CeC_SesionBase Sesion = CeC_Sesion.ObtenSesion(this);

            eClockBase.Controladores.Sesion SE = new eClockBase.Controladores.Sesion(Sesion);
            SE.ObtenNoCambiosEvent += SE_ObtenNoCambiosEvent;
            SE.ObtenNoCambios(Tablas);
        }

        public void GuardarConsulta(string Tabla)
        {
            eClockBase.CeC_SesionBase Sesion = CeC_Sesion.ObtenSesion(this);

            eClockBase.Controladores.Sesion SE = new eClockBase.Controladores.Sesion(Sesion);
            SE.GuardaConsulta(Tabla);
        }

        void SE_ObtenNoCambiosEvent(string Resultado)
        {
            Valores = Resultado;
            Btn_Permisos.NoAlertas = ObtenValor("PERMISOS");
            Btn_Mensajes.NoAlertas = ObtenValor("MENSAJES");
            Btn_Vacaciones.NoAlertas = ObtenValor("VACACIONES");
            Btn_Tramites.NoAlertas = ObtenValor("EC_TIPO_TRAMITE");
            Btn_CalendarioActividades.NoAlertas = ObtenValor("EC_ACTIVIDADES");
            Btn_Asistencia.NoAlertas = ObtenValor("ASISTENCIAS");
            Btn_Nomina.NoAlertas = ObtenValor("NOMINA");
        }

        private void UC_ToolBar_OnEventClickToolBar(eClock5.UC_ToolBar_Control Control)
        {
            switch (Control.Nombre)
            {
                case "Btn_Regresar":
                    CerrarSesion();

                    break;
            }
        }
        public void CerrarSesion()
        {
            eClockBase.CeC_SesionBase Sesion = CeC_Sesion.ObtenSesion(this);
            eClockBase.Controladores.Sesion SE = new eClockBase.Controladores.Sesion(Sesion);
            SE.SesionCerrada += SE_SesionCerrada;
            SE.CerrarSesion();
            if (SesionCerrada != null)
                SesionCerrada(this);
        }

        void SE_SesionCerrada(bool Cerrado)
        {

        }


        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {

        }

        public void MostrarDatos()
        {
            LimpiaPantalla();
            eClockBase.CeC_SesionBase Sesion = CeC_Sesion.ObtenSesion(this);

            eClockBase.Controladores.Sesion SE = new eClockBase.Controladores.Sesion(Sesion);
            SE.ObtenDatosPersonaEvent += SE_ObtenDatosPersonaEvent;
            SE.ObtenDatosPersona();

            
            if (Sesion.Mdl_Sesion.PERSONA_ID > 0)
            {
                eClockBase.Controladores.Personas Persona = new eClockBase.Controladores.Personas(Sesion);
                Persona.ObtenFotoThumbnailEvent += Persona_ObtenFotoThumbnailEvent;
                Persona.ObtenFotoThumbnail(Sesion.Mdl_Sesion.PERSONA_ID, eClockBase.CeC.Convierte2Int(Img_Foto.Height), eClockBase.CeC.Convierte2Int(Img_Foto.Height));
            }

            Banner.Actualiza();
            ActualizaNos();
        }

        void Persona_ObtenFotoThumbnailEvent(byte[] Foto)
        {

            System.IO.MemoryStream MS = new System.IO.MemoryStream(Foto);
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = MS;
            bi.EndInit();
            Img_Foto.Source = bi;
        }

        void SE_ObtenDatosPersonaEvent(eClockBase.Modelos.Sesion.Model_DatosPersona Datos)
        {
            if (Datos != null)
            {
                Lbl_NombreEmpleado.Text = Datos.PERSONA_NOMBRE;
                Lbl_NumEmpleado.Text = eClockBase.CeC.Convierte2String(Datos.PERSONA_LINK_ID);
                Lbl_UltimaSesion.Text = eClockBase.CeC.Convierte2String(Datos.UltimaSesion);


            }
        }

        public static void MuestraComoDialogo(ContentControl Padre, UserControl Hijo, Color ColorFondo)
        {
            MuestraComoDialogo(Padre, Hijo, new SolidColorBrush(ColorFondo));
        }
        public static void MuestraComoDialogo(ContentControl Padre, UserControl Hijo, Brush ColorFondo)
        {
            Hijo.Width = double.NaN;
            Hijo.Height = double.NaN;
            Hijo.Margin = new Thickness(0);
            Hijo.Background = ColorFondo;

            if (Padre.Content.ToString() != "System.Windows.Controls.ScrollViewer")
            {
                ((Panel)Padre.Content).Children.Add(Hijo);
                Hijo.IsVisibleChanged += Hijo_IsVisibleChanged;
            }
            else
            {
                ((Panel)Padre.FindName("Grid")).Children.Add(Hijo);
                Hijo.IsVisibleChanged += Hijo_IsVisibleChanged;
            }

        }

        static void Hijo_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            bool Visible = ((bool)e.NewValue);
            if (!Visible)
            {
                ((Panel)((UserControl)sender).Parent).Children.Remove((UserControl)sender);
            }
        }
        private void Btn_Vacaciones_Click(object sender, RoutedEventArgs e)
        {
            MuestraComoDialogo(this, new Vacaciones.Solicitud(), Btn_Vacaciones.ColorFondo);
            GuardarConsulta("VACACIONES");
        }

        private void Btn_CalendarioActividades_Click(object sender, RoutedEventArgs e)
        {
            MuestraComoDialogo(this, new Actividades.Listado(), Btn_CalendarioActividades.ColorFondo);
            GuardarConsulta("EC_ACTIVIDADES");
        }

        private void Btn_Tramites_Click(object sender, RoutedEventArgs e)
        {
            MuestraComoDialogo(this, new Tramites.Listado(), Btn_Tramites.ColorFondo);
            GuardarConsulta("EC_TIPO_TRAMITE");
        }


        private void Btn_Asistencia_Click(object sender, RoutedEventArgs e)
        {
            MuestraComoDialogo(this, new Asistencias.Asistencias(), Btn_Asistencia.ColorFondo);
            GuardarConsulta("ASISTENCIAS");
        }

        private void Btn_Nomina_Click(object sender, RoutedEventArgs e)
        {
            MuestraComoDialogo(this, new Nomina.Nomina(), Btn_Nomina.ColorFondo);
            GuardarConsulta("NOMINA");
        }

        private void Btn_Permisos_Click(object sender, RoutedEventArgs e)
        {
            MuestraComoDialogo(this, new Permisos.Permisos(), Btn_Permisos.ColorFondo);
            GuardarConsulta("PERMISOS");
        }

        private void Btn_Mensajes_Click(object sender, RoutedEventArgs e)
        {
            MuestraComoDialogo(this, new Mensajes.Mensajes(), Btn_Mensajes.ColorFondo);
            GuardarConsulta("MENSAJES");
        }

        private void Btn_Configuracion_Click(object sender, RoutedEventArgs e)
        {
            MuestraComoDialogo(this, new Configuracion.Cambio_Password(), Btn_Configuracion.ColorFondo);
        }

    }
}
