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
using eClock5;
using eClock5.BaseModificada;


namespace Kiosko.Asistencias
{
    /// <summary>
    /// Lógica de interacción para Asistencias.xaml
    /// </summary>
    public partial class Asistencias : UserControl
    {
        public Asistencias()
        {
            InitializeComponent();
        }

        private void ToolBar_OnEventClickToolBar(eClock5.UC_ToolBar_Control Control)
        {
            switch (Control.Nombre)
            {
                case "Btn_Enviar":
                    eClockBase.Controladores.Reportes cReportes = new eClockBase.Controladores.Reportes(Sesion);
                    cReportes.ObtenReporteeMailEvent += cReportes_ObtenReporteeMailEvent;
                    eClockBase.Modelos.Asistencias.Model_Parametros ParamReporte = new eClockBase.Modelos.Asistencias.Model_Parametros(Sesion.Mdl_Sesion.PERSONA_ID, "", RangoFechas.FechaInicial, RangoFechas.FechaFinal);
                    cReportes.ObtenReporteeMailEvent += cReportes_ObtenReporteeMailEvent;
                    cReportes.ObtenReporteeMail("Asistencias del " + RangoFechas.FechaInicial.ToShortDateString() + " - " + RangoFechas.FechaFinal.ToShortDateString(), "", MainWindow.KioscoParametros.Parametros.REPORTE_ID_Asistencia, ParamReporte, MainWindow.KioscoParametros.Parametros.FORMATO_REP_ID_Asistencia);
                    break;
                case "Btn_Regresar":
                    Close();
                    break;

            }
        }

        bool Cerrar = false;
        private void Close()
        {
            this.Visibility = System.Windows.Visibility.Hidden;
            RangoFechas.Btn_Main.IsChecked = false;
        }

        void Msg_Cerrado()
        {
            if (Cerrar)
            {
                RangoFechas.Btn_Main.IsChecked = false;
                this.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        void cReportes_ObtenReporteeMailEvent(bool Estado)
        {
            Controles.UC_MessageBoxFullScreen Msg = new Controles.UC_MessageBoxFullScreen();
            if (Estado == true)
            {
                Msg.Mensaje = "Reporte de Asistencias enviado a su e-Mail";

                Cerrar = true;
            }
            else
            {
                Msg.Mensaje = "Error al enviar Reporte de Asistencias";
                Msg.Imagen = null;
                Cerrar = false;
            }
            Msg.Cerrado += Msg_Cerrado;
            Msg.Mostrar(this);
        }

        private void UC_RangoFechas_Click_1(object sender, RoutedEventArgs e)
        {



        }
        //******************************************************************************************************************************
        void Asistencia_EventoObtenAsistenciaLinealNFinalizado(List<eClockBase.Modelos.Asistencias.Model_Asistencia_Lineal_N> Asistencia)
        {
            Lst_Asistencia.Modelo2Vista(Asistencia);
        }

        CeC_SesionBase Sesion = null;
        private void ActualizaAsistencia()
        {
            if(Sesion == null)
                Sesion = CeC_Sesion.ObtenSesion(this);
            eClockBase.Controladores.Asistencias Asistencia = new eClockBase.Controladores.Asistencias(Sesion);
            Asistencia.EventoObtenAsistenciaLinealNFinalizado += Asistencia_EventoObtenAsistenciaLinealNFinalizado;
            Asistencia.ObtenAsistenciaLinealN(
                 eClockBase.CeC.PersonaID2PersonaDiarioID(Sesion.Mdl_Sesion.PERSONA_ID, RangoFechas.FechaInicial),
                  eClockBase.CeC.PersonaID2PersonaDiarioID(Sesion.Mdl_Sesion.PERSONA_ID, RangoFechas.FechaFinal));
        }

        private void RangoFechas_CambioFechas(object sender, RoutedEventArgs e)
        {
            ActualizaAsistencia();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            RangoFechas.FechaInicial = DateTime.Now.AddDays(-7);
            RangoFechas.FechaFinal = DateTime.Now.AddDays(-1);
            ActualizaAsistencia();
        }

        private void Btn_SaldoIncidencias_Click(object sender, RoutedEventArgs e)
        {
            Kiosko.Asistencias.SaldosIncidencias Dlg = new SaldosIncidencias();

            Generales.Main.MuestraComoDialogo(this, Dlg, this.Background);
           
        }
        //*******************************************************************************************************************************

    }
}
