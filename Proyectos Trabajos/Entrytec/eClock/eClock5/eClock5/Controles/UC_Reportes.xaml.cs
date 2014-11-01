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

namespace eClock5.Controles
{
    /// <summary>
    /// Lógica de interacción para UC_Reportes.xaml
    /// </summary>
    public partial class UC_Reportes : UserControl
    {
        private object m_ParametrosGenerales = null;

        public object ParametrosGenerales
        {
            get { return m_ParametrosGenerales; }
            set { m_ParametrosGenerales = value; }
        }

        private string m_Modelos = "";
        public string Modelos
        {
            get { return m_Modelos; }
            set { m_Modelos = value; }
        }

        public UC_Reportes()
        {
            InitializeComponent();
        }
        eClockBase.CeC_SesionBase Sesion;
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            Sesion = CeC_Sesion.ObtenSesion(this);
            eClockBase.Controladores.Reportes Reportes = new eClockBase.Controladores.Reportes(Sesion);
            Reportes.ObtenListadoEvent += Reportes_ObtenListadoEvent;
            Reportes.ObtenListado(Modelos);
        }
        List<UC_Reporte> ReportesVista = new List<UC_Reporte>();
        void Reportes_ObtenListadoEvent(List<eClockBase.Modelos.Reportes.Model_Reportes> Reportes)
        {
            try
            {
                // Datos[0].Seleccionado = true;
                ReportesVista.Clear();
                Contenedor.Children.Clear();
                foreach (eClockBase.Modelos.Reportes.Model_Reportes Reporte in Reportes)
                {
                    if (Modelos != null && Modelos != "")
                        if (!BaseModificada.CeC.ExisteEnSeparador(Modelos, Reporte.REPORTE_MODELO, ","))
                            continue;
                    UC_Reporte ReporteVista = new UC_Reporte();
                    ReporteVista.ClickEvento += ReporteVista_ClickEvento;
                    ReporteVista.ParametrosGenerales = ParametrosGenerales;
                    ReporteVista.DataContext = Reporte;
                    ReportesVista.Add(ReporteVista);
                    ReporteVista.Margin = new Thickness(10);
                    Contenedor.Children.Add(ReporteVista);
                }


            }
            catch (Exception ex)
            {
                eClockBase.CeC_Log.AgregaError(ex);
            }
        }

        void ReporteVista_ClickEvento(eClockBase.Modelos.Reportes.Model_Reportes Reporte, string Parametros, bool Prueba)
        {
            string[] Formatos = eClockBase.CeC.ObtenArregoSeparador(Reporte.REPORTE_FORMATOS, ",");
            if ((Reporte.REPORTE_PARAM != null && Reporte.REPORTE_PARAM.Length > 2) || Formatos.Length > 1)
            {
                UC_Reporte_Param Param = new UC_Reporte_Param();
                Param.Carga(Reporte, Parametros, Prueba);
                Param.MuestraReporteEvent += delegate(eClockBase.Modelos.Reportes.Model_Reportes rReporte, string sParametros, bool bPrueba, int FormatoReporteID)
                    {
                        eClockBase.Controladores.Reportes CReporte = new eClockBase.Controladores.Reportes(Sesion);
                        CReporte.ObtenReporteEvent += CReporte_ObtenReporteEvent;
                        CReporte.ObtenReporte(rReporte.REPORTE_ID, sParametros, FormatoReporteID);
                    };
                Param.EnviaReporteEvent += delegate(string Titulo, string Cuerpo, eClockBase.Modelos.Reportes.Model_Reportes rReporte, string sParametros, bool bPrueba, int FormatoReporteID)
                {
                    eClockBase.Controladores.Reportes CReporte = new eClockBase.Controladores.Reportes(Sesion);
                    CReporte.ObtenReporteeMailEvent += CReporte_ObtenReporteeMailEvent;
                    CReporte.ObtenReporteeMail(Titulo, Cuerpo, rReporte.REPORTE_ID, sParametros, FormatoReporteID);
                };

                UC_Listado.MuestraComoDialogo(this, Param, this.Background);
            }
            else
            {
                eClockBase.Controladores.Reportes CReporte = new eClockBase.Controladores.Reportes(Sesion);
                CReporte.ObtenReporteEvent += CReporte_ObtenReporteEvent;
                CReporte.ObtenReporte(Reporte.REPORTE_ID, Parametros, 0);
            }
        }

        void CReporte_ObtenReporteeMailEvent(bool Estado)
        {
            //throw new NotImplementedException();
        }


        void CReporte_ObtenReporteEvent(byte[] ArchivoReporte, string ArchivoNombre)
        {
            eClockBase.CeC_Stream.sEjecuta(ArchivoNombre);
        }

        private void ToolBar_OnEventClickToolBar(UC_ToolBar_Control Control)
        {
            switch (Control.Nombre)
            {
                case "Btn_Regresar":
                    this.Visibility = System.Windows.Visibility.Hidden;
                    break;
            }
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (Contenedor.ActualWidth > 0)
            {
                Contenedor.ItemHeight = Contenedor.ItemWidth = (Contenedor.ActualWidth - 10 * 3) / 2;
            }
        }


    }
}
