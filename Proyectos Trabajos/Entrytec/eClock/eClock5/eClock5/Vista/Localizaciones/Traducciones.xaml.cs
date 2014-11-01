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
using System.Windows.Shapes;
using Newtonsoft.Json;

using eClockBase;

namespace eClock5.Vista.Localizaciones
{
    /// <summary>
    /// Lógica de interacción para Traducciones.xaml
    /// </summary>
    public partial class Traducciones : Window
    {
        eClockBase.CeC_SesionBase Sesion;
        List<eClockBase.Modelos.Model_LOCALIZACIONES> Localizaciones;
        public string m_Llave;
        string IdiomaActual = "-";
        public Traducciones()
        {
            InitializeComponent();
        }
        public static void CargaTraduccion(string Llave)
        {
            Traducciones Dlg = new Traducciones();
            Dlg.m_Llave = Llave;
            Dlg.ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Sesion = CeC_Sesion.ObtenSesion(this);
            eClockBase.Controladores.Sesion cSesion = new eClockBase.Controladores.Sesion(Sesion);
            cSesion.ObtenDatosEvent += cSesion_ObtenDatosEvent;
            cSesion.ObtenDatos("EC_LOCALIZACIONES", "", new eClockBase.Modelos.Model_LOCALIZACIONES(), "", "LOCALIZACION_LLAVE = '" + m_Llave + "'");
        }

        void Agrega(string Idioma)
        {
            foreach (eClockBase.Modelos.Model_LOCALIZACIONES Localizacion in Localizaciones)
            {
                if (Idioma == Localizacion.LOCALIZACION_IDIOMA)
                {
                    return;
                }
            }
            eClockBase.Modelos.Model_LOCALIZACIONES LocalizacionNueva = new eClockBase.Modelos.Model_LOCALIZACIONES();
//            LocalizacionNueva.LOCALIZACION_ID = -1;
            LocalizacionNueva.LOCALIZACION_LLAVE = m_Llave;
            LocalizacionNueva.LOCALIZACION_IDIOMA = Idioma;
            Localizaciones.Add(LocalizacionNueva);
        }

        void cSesion_ObtenDatosEvent(int Resultado, string Datos)
        {
            Tbx_Llavereferencia.Text = m_Llave;
            Tbx_Llavereferencia_Ad.Text = m_Llave;
            if (Resultado > 0)
            {
                Datos = eClockBase.CeC.Json2JsonList(Datos);
                Localizaciones = eClockBase.Controladores.CeC_ZLib.Json2Object<List<eClockBase.Modelos.Model_LOCALIZACIONES>>(Datos);
                foreach (eClockBase.Modelos.Model_LOCALIZACIONES Localizacion in Localizaciones)
                    if (Localizacion.LOCALIZACION_IDIOMA == null)
                        Localizacion.LOCALIZACION_IDIOMA = "";

            }
            else
                Localizaciones = new List<eClockBase.Modelos.Model_LOCALIZACIONES>();
            Agrega("");
            Agrega("es");
            Agrega("en");
            Agrega("it");
            Agrega("fr");
            Agrega("pt");
            MuestraVista("");
            MuestraVista(Sesion.IDIOMA);
        }
        public void Vista2Modelo(string Idioma)
        {
            if(Localizaciones == null)
                return;
            foreach (eClockBase.Modelos.Model_LOCALIZACIONES Localizacion in Localizaciones)
            {
                if (Idioma == Localizacion.LOCALIZACION_IDIOMA)
                {
                    if (Idioma == "")
                    {
                        Localizacion.LOCALIZACION_ID = CeC.Convierte2Int(Tbx_LocalizacionID.Text);
                        Localizacion.LOCALIZACION_IDIOMA = Tbx_Lenguage.Text;
                        Localizacion.LOCALIZACION_LLAVE = Tbx_Llavereferencia.Text;
                        Localizacion.LOCALIZACION_ETIQUETA = Tbx_TextoPantalla.Text;
                        Localizacion.LOCALIZACION_DESCRIPCION = Tbx_Descripcion.Text;
                        Localizacion.LOCALIZACION_IMAGEN = Tbx_Imagen.Text;
                        Localizacion.LOCALIZACION_ALTMENU = Tbx_MenuAlt.Text;
                        Localizacion.LOCALIZACION_HTML = Tbx_HTML.Text;
                        Localizacion.LOCALIZACION_AYUDA = Tbx_Ayuda.Text;
                    }
                    else
                    {
                        Localizacion.LOCALIZACION_ID = CeC.Convierte2Int(Tbx_LocalizacionID_Ad.Text);
                        Localizacion.LOCALIZACION_IDIOMA = Tbx_Lenguage_Ad.Text;
                        Localizacion.LOCALIZACION_LLAVE = Tbx_Llavereferencia_Ad.Text;
                        Localizacion.LOCALIZACION_ETIQUETA = Tbx_TextoPantalla_Ad.Text;
                        Localizacion.LOCALIZACION_DESCRIPCION = Tbx_Descripcion_Ad.Text;
                        Localizacion.LOCALIZACION_IMAGEN = Tbx_Imagen_Ad.Text;
                        Localizacion.LOCALIZACION_ALTMENU = Tbx_MenuAlt_Ad.Text;
                        Localizacion.LOCALIZACION_HTML = Tbx_HTML_Ad.Text;
                        Localizacion.LOCALIZACION_AYUDA = Tbx_Ayuda_Ad.Text;
                    }
                    return;
                }
            }
        }
        public void MuestraVista(string Idioma)
        {
            if (Localizaciones == null)
                return;
            if (Idioma != "")
                IdiomaActual = Idioma;
            foreach (eClockBase.Modelos.Model_LOCALIZACIONES Localizacion in Localizaciones)
            {
                if (Idioma == Localizacion.LOCALIZACION_IDIOMA)
                {
                    if(Idioma == "")
                    {

                        Tbx_LocalizacionID.Text = CeC.Convierte2String(Localizacion.LOCALIZACION_ID);
                        Tbx_Lenguage.Text = Localizacion.LOCALIZACION_IDIOMA;
                        Tbx_Llavereferencia.Text = Localizacion.LOCALIZACION_LLAVE;
                        Tbx_TextoPantalla.Text = Localizacion.LOCALIZACION_ETIQUETA;
                        Tbx_Descripcion.Text = Localizacion.LOCALIZACION_DESCRIPCION;
                        Tbx_Imagen.Text = Localizacion.LOCALIZACION_IMAGEN;
                        Tbx_MenuAlt.Text = Localizacion.LOCALIZACION_ALTMENU;
                        Tbx_HTML.Text = Localizacion.LOCALIZACION_HTML;
                        Tbx_Ayuda.Text = Localizacion.LOCALIZACION_AYUDA;
                        
                    }
                    else
                    {
                        Tbx_LocalizacionID_Ad.Text = CeC.Convierte2String(Localizacion.LOCALIZACION_ID);
                        Tbx_Lenguage_Ad.Text = Localizacion.LOCALIZACION_IDIOMA;
                        Tbx_Llavereferencia_Ad.Text = Localizacion.LOCALIZACION_LLAVE;
                        Tbx_TextoPantalla_Ad.Text = Localizacion.LOCALIZACION_ETIQUETA;
                        Tbx_Descripcion_Ad.Text = Localizacion.LOCALIZACION_DESCRIPCION;
                        Tbx_Imagen_Ad.Text = Localizacion.LOCALIZACION_IMAGEN;
                        Tbx_MenuAlt_Ad.Text = Localizacion.LOCALIZACION_ALTMENU;
                        Tbx_HTML_Ad.Text = Localizacion.LOCALIZACION_HTML;
                        Tbx_Ayuda_Ad.Text = Localizacion.LOCALIZACION_AYUDA;
                    }
                    return;
                }
            }

        }

        private void Btn_Borrar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Rbn_Checked(object sender, RoutedEventArgs e)
        {
            Vista2Modelo(IdiomaActual);

            MuestraVista(((RadioButton)sender).Tag.ToString());
        }

        private void Btn_Aceptar_Click(object sender, RoutedEventArgs e)
        {
            Vista2Modelo("");
            Vista2Modelo(IdiomaActual);
            eClockBase.Controladores.Sesion cSesion = new eClockBase.Controladores.Sesion(Sesion);
            cSesion.GuardaDatosEvent += cSesion_GuardaDatosEvent;
            cSesion.GuardaDatos("EC_LOCALIZACIONES", "LOCALIZACION_ID", Localizaciones, false);
        }

        void cSesion_GuardaDatosEvent(int Guardados)
        {
            if (Guardados> 0)
                this.Close();
        }


    }
}
