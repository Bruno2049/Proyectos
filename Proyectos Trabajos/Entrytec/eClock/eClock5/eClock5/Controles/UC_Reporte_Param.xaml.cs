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
    /// Lógica de interacción para UC_Reporte_Param.xaml
    /// </summary>
    public partial class UC_Reporte_Param : UserControl
    {
        public eClockBase.Modelos.Reportes.Model_Reportes Reporte;
        public string Parametros;
        public bool Prueba;
        public UC_Reporte_Param()
        {
            InitializeComponent();
        }
        public void Carga(eClockBase.Modelos.Reportes.Model_Reportes rReporte, string sParametros, bool bPrueba)
        {
            Reporte = rReporte;
            Parametros = sParametros;
            Prueba = bPrueba;
            //if(Reporte.REPORTE_FORMATOS != null && Reporte.REPORTE_FORMATOS != "")
            Cmb_Formato.Filtro = "FORMATO_REP_ID IN (" + Reporte.REPORTE_FORMATOS + ")";
            Campos.Asigna(Reporte.REPORTE_PARAM, sParametros);
        }

        public delegate void MuestraReporteArgs(eClockBase.Modelos.Reportes.Model_Reportes rReporte, string sParametros, bool bPrueba, int FormatoReporteID);
        public event MuestraReporteArgs MuestraReporteEvent;

        public delegate void EnviaReporteArgs(string Titulo, string Cuerpo, eClockBase.Modelos.Reportes.Model_Reportes rReporte, string sParametros, bool bPrueba, int FormatoReporteID);
        public event EnviaReporteArgs EnviaReporteEvent;

        private void ToolBar_OnEventClickToolBar(UC_ToolBar_Control Control)
        {
            switch (Control.Nombre)
            {
                case "Btn_Regresar":
                    this.Visibility = System.Windows.Visibility.Hidden;
                    break;
                case "Btn_EMail":
                    if (Cmb_Formato.SeleccionadoInt < 0)
                    {
                        break;
                    }
                    if (EnviaReporteEvent != null)
                    {
                        string CamposJSon = Campos.Vista2JSon();
                        if (CamposJSon == "{}")
                            CamposJSon = Parametros;
                        EnviaReporteEvent("", "", Reporte, CamposJSon, Prueba, Cmb_Formato.SeleccionadoInt);
                    }
                    break;

                    break;
                case "Btn_Reporte":
                    if (Cmb_Formato.SeleccionadoInt < 0)
                    {
                        break;
                    }
                    if (MuestraReporteEvent != null)
                    {
                        string CamposJSon = Campos.Vista2JSon();
                        if (CamposJSon == "{}")
                            CamposJSon = Parametros;
                        MuestraReporteEvent(Reporte, CamposJSon, Prueba, Cmb_Formato.SeleccionadoInt);
                    }
                    break;
            }
        }

        private void Cmb_Formato_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Cmb_Formato.SeleccionadoInt < 0)
                Lbl_ErrorFormatoReporte.Visibility = System.Windows.Visibility.Visible;
            else
                Lbl_ErrorFormatoReporte.Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}
