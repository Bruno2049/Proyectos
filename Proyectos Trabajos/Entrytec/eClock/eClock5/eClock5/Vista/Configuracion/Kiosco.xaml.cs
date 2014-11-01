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

using eClockBase;

namespace eClock5.Vista.Configuracion
{
    /// <summary>
    /// Lógica de interacción para Kiosco.xaml
    /// </summary>
    public partial class Kiosco : UserControl
    {
        CeC_SesionBase m_SesionBase = null;
        public Kiosco()
        {
            InitializeComponent();
        }


        private void UC_ToolBar_OnEventClickToolBar(UC_ToolBar_Control Control)
        {
            switch (Control.Nombre)
            {
                case "Btn_Regresar":
                    this.Visibility = Visibility.Hidden;
                    break;
                case "Btn_Guardar":
                    try
                    {
                        CtrlKiosco.Parametros.REPORTE_ID_Asistencia = Cmb_Reporte_Asistencia.SeleccionadoInt;
                        CtrlKiosco.Parametros.REPORTE_ID_Nomina = Cmb_Reporte_Nomina.SeleccionadoInt;
                        CtrlKiosco.Parametros.REPORTE_ID_RecNomina = Cmb_Reporte_Rec_Nomina.SeleccionadoInt;

                        CtrlKiosco.Parametros.FORMATO_REP_ID_Asistencia = Cmb_Formato_Reporte_Asistencia.SeleccionadoInt;
                        CtrlKiosco.Parametros.FORMATO_REP_ID_Nomina = Cmb_Formato_Reporte_Nomina.SeleccionadoInt;
                        CtrlKiosco.Parametros.TIPO_INCIDENCIA_ID_Vaca = Cmb_TIPO_INCIDENCIA_ID_Vaca.SeleccionadoInt;
                        CtrlKiosco.Parametros.Color = CP_Color_Reporte.sColorActual;
                        CtrlKiosco.GuardaParametros();
                    }
                    catch
                    {
                    }

                    break;

            }
        }

        void CtrlKiosco_GuardaParametrosEvent(bool Resultado)
        {
            if (Resultado == true)
            {
                m_SesionBase.MuestraMensaje("Parametros de Reporte Guardados..");
            }
            else
            {
                m_SesionBase.MuestraMensaje("Parametros No Guardados..");
            }
        }
        eClockBase.ControladoresParametros.Kiosco CtrlKiosco;
        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            eClockBase.CeC_SesionBase Sesion = CeC_Sesion.ObtenSesion(this);
            CtrlKiosco = new eClockBase.ControladoresParametros.Kiosco();
            CtrlKiosco.CargaParametrosEvent += CtrlKiosco_CargaParametrosEvent;
            CtrlKiosco.CargaParametros(Sesion);
        }

        void CtrlKiosco_CargaParametrosEvent(bool Cargados)
        {
            if (Cargados)
            {
                Cmb_Reporte_Asistencia.SeleccionadoInt = CtrlKiosco.Parametros.REPORTE_ID_Asistencia;
                Cmb_Reporte_Nomina.SeleccionadoInt = CtrlKiosco.Parametros.REPORTE_ID_Nomina;
                Cmb_Reporte_Rec_Nomina.SeleccionadoInt = CtrlKiosco.Parametros.REPORTE_ID_RecNomina;

                Cmb_Formato_Reporte_Asistencia.SeleccionadoInt = CtrlKiosco.Parametros.FORMATO_REP_ID_Asistencia;
                Cmb_Formato_Reporte_Nomina.SeleccionadoInt = CtrlKiosco.Parametros.FORMATO_REP_ID_Nomina;
                Cmb_TIPO_INCIDENCIA_ID_Vaca.SeleccionadoInt = CtrlKiosco.Parametros.TIPO_INCIDENCIA_ID_Vaca;
                CP_Color_Reporte.sColorActual = CtrlKiosco.Parametros.Color;

            }
        }



    }
}
