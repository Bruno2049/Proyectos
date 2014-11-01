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

namespace eClock5.Controles
{
    /// <summary>
    /// Lógica de interacción para UC_Reporte.xaml
    /// </summary>
    public partial class UC_Reporte : UserControl
    {
        private object m_ParametrosGenerales = null;

        public object ParametrosGenerales
        {
            get { return m_ParametrosGenerales; }
            set { m_ParametrosGenerales = value; }
        }

        public UC_Reporte()
        {
            InitializeComponent();
        }

        public string ObtenParametros()
        {
            string R = "";
            if (ParametrosGenerales != null)
            {
                R = JsonConvert.SerializeObject(m_ParametrosGenerales);
            }
            return R;
        }

        eClockBase.CeC_SesionBase Sesion = null;
        eClockBase.Modelos.Reportes.Model_Reportes Reporte = null;
        bool GenerarReporte = true;
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Reporte = (eClockBase.Modelos.Reportes.Model_Reportes) DataContext;
            
            Lbl_Titulo.Text = Reporte.REPORTE_TITULO;
            if (Reporte.REPORTE_PRECIO > 0 && Reporte.REPORTE_USUARIO_SPAGO < DateTime.Now)
            {
                GenerarReporte = false;
            }
            else
            {
                Lbl_Proveedor.Visibility = System.Windows.Visibility.Hidden;
                Stp_Comprar.Visibility = System.Windows.Visibility.Hidden;
            }
            //Lbl_Descripcion.Text = Reporte.REPORTE_DESCRIP;
            Sesion = CeC_Sesion.ObtenSesion(this);
            eClockBase.Controladores.Sesion CtSesion = new eClockBase.Controladores.Sesion(Sesion);
            CtSesion.ObtenImagenFinalizado += delegate(byte[] Imagen)
                {
                    Img_Reporte.Source = eClock5.BaseModificada.CeC.Byte2Imagen(Imagen);
                };
            CtSesion.ObtenImagen("EC_REPORTES", "REPORTE_ID", Reporte.REPORTE_ID, "REPORTE_IMAGEN");
        }

        private void Btn_Generar_Click(object sender, RoutedEventArgs e)
        {

        }

        void CReporte_ObtenReporteEvent(byte[] ArchivoReporte, string ArchivoNombre)
        {
            eClockBase.CeC_Stream.sEjecuta(ArchivoNombre);
        }

        public delegate void ClickArgs(eClockBase.Modelos.Reportes.Model_Reportes Reporte, string Parametros, bool Prueba);
        public event ClickArgs ClickEvento;


        private void Btn_Main_Click(object sender, RoutedEventArgs e)
        {
            if (GenerarReporte)
            {
                if (ClickEvento != null)
                    ClickEvento(Reporte, ObtenParametros(), false);

            }
        }

        private void Btn_Probar_Click(object sender, RoutedEventArgs e)
        {
            if (ClickEvento != null)
                ClickEvento(Reporte, ObtenParametros(), true);

        }

        private void Btn_Comprar_Click(object sender, RoutedEventArgs e)
        {

        }


        private void Btn_Excel_Click(object sender, RoutedEventArgs e)
        {
            if (ClickEvento != null)
                ClickEvento(Reporte, ObtenParametros(), false);
        }


    }
}
