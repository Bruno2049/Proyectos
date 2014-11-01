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

namespace Kiosko.Nomina
{
    /// <summary>
    /// Lógica de interacción para Detalles.xaml
    /// </summary>
    public partial class Detalles : UserControl
    {
        public decimal SumImporte_Percep = 0;
        public decimal SumImporte_Deducc = 0;
        public Detalles()
        {
            InitializeComponent();
        }

        private void ToolBar_OnEventClickToolBar(eClock5.UC_ToolBar_Control Control)
        {
            switch (Control.Nombre)
            {
                case "Btn_Regresar":
                    this.Visibility = System.Windows.Visibility.Hidden;
                    break;
                case "Btn_EnviarMail":
                    Sesion = eClock5.CeC_Sesion.ObtenSesion(this);
                    eClockBase.Controladores.Reportes Rpt = new eClockBase.Controladores.Reportes(Sesion);
                    Rpt.ObtenReporteeMailEvent += Rpt_ObtenReporteeMailEvent;

                    int ReporteID = Kiosko.MainWindow.KioscoParametros.Parametros.REPORTE_ID_Nomina;
                    int FormatoID = Kiosko.MainWindow.KioscoParametros.Parametros.FORMATO_REP_ID_Nomina;
                    eClockBase.Modelos.Nomina.Model_Parametros MdlParam = new eClockBase.Modelos.Nomina.Model_Parametros();

                    MdlParam.REC_NOMINA_ID = RecNominaID;

                    Rpt.ObtenReporteeMail("Nomina", "Adjunto Nomina en PDF", ReporteID, MdlParam, FormatoID);
 
                    break;
                case "Btn_Imprimr":
                    if (RecNominaImp == 1)
                    {
                        Controles.UC_MessageBoxFullScreen Msg = new Controles.UC_MessageBoxFullScreen();
                        Msg.Mensaje = "Recibo de nómina ya fue Impreso (Impresión solo una vez por periodo)";
                        Msg.Mostrar(this);
                    }
                    else
                    {
                        ObtenReporte();
                    }
                    break;
            }
        }

        public void ObtenReporte()
        {
            Sesion = eClock5.CeC_Sesion.ObtenSesion(this);
                    eClockBase.Controladores.Reportes Rpt = new eClockBase.Controladores.Reportes(Sesion);                    
                    Rpt.ObtenReporteEvent +=Rpt_ObtenReporteEvent;

                    int ReporteID = Kiosko.MainWindow.KioscoParametros.Parametros.REPORTE_ID_RecNomina;

                    eClockBase.Modelos.Nomina.Model_Parametros MdlParam = new eClockBase.Modelos.Nomina.Model_Parametros();
                    MdlParam.REC_NOMINA_ID = RecNominaID;
                    string Param = JsonConvert.SerializeObject(MdlParam);
                    Rpt.ObtenReporte(ReporteID, Param, 0);
        }

        bool Cerrar = false;
        void Rpt_ObtenReporteEvent(byte[] ArchivoReporte, string PathArchivo)
        {
 	        Controles.UC_MessageBoxFullScreen Msg = new Controles.UC_MessageBoxFullScreen();
            if (ArchivoReporte != null)
            {
                Msg.Mensaje = "Reporte enviado a la impresora";
                System.Diagnostics.Process oProc = new System.Diagnostics.Process();
                oProc.StartInfo.FileName = MainWindow.Config.TicketAppPDF;
                string Parametros = MainWindow.Config.TicketParametros;
                Parametros = eClockBase.CeC.AsignaParametro(Parametros,"ARCHIVO",eClockBase.CeC_Stream.sObtenPath(PathArchivo));
                Parametros = eClockBase.CeC.AsignaParametro(Parametros, "IMPRESORA", "Microsoft XPS Document Writer");
                oProc.StartInfo.Arguments = Parametros;
                eClockBase.CeC_Log.AgregaLog("Ejecutando \"" + oProc.StartInfo.FileName +"\" con parametros \""+  oProc.StartInfo.Arguments  + "\"");
                oProc.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                oProc.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Minimized;
                oProc.Start();
                Cerrar = true;
                RecNominaImp = 1;


                Sesion = eClock5.CeC_Sesion.ObtenSesion(this);
                
                eClockBase.Controladores.Nominas Nom = new eClockBase.Controladores.Nominas(Sesion);
                Nom.ConfirmaReciboNominaImpresoEvent += Nom_ConfirmaReciboNominaImpresoEvent;
                Nom.ConfirmaReciboNominaImpreso(RecNominaID, RecNominaImp);

            }
            else
            {
                Msg.Mensaje = "Error al enviar Nomina";
                Msg.Imagen = null;
                Cerrar = false;
            }
            Msg.Cerrado += Msg_Cerrado;
            Msg.Mostrar(this);
        }

        void Nom_ConfirmaReciboNominaImpresoEvent(bool Resultado)
        {
            throw new NotImplementedException();
        }



        void Rpt_ObtenReporteeMailEvent(bool Estado)
        {
            Controles.UC_MessageBoxFullScreen Msg = new Controles.UC_MessageBoxFullScreen();
            if (Estado == true)
            {
                Msg.Mensaje = "Reporte enviado a su E-Mail";

                Cerrar = true;
            }
            else
            {
                Msg.Mensaje = "Error al enviar Nomina";
                Msg.Imagen = null;
                Cerrar = false;
            }
            Msg.Cerrado += Msg_Cerrado;
            Msg.Mostrar(this);
        }
        void Msg_Cerrado()
        {
            if (Cerrar)
            {
                this.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        

        eClockBase.CeC_SesionBase Sesion;
        int RecNominaID = 0;
        int RecNominaImp = 0;
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
           RecNominaID = eClockBase.CeC.Convierte2Int(eClock5.Clases.Parametros.Tag2Parametros(this.Tag).Parametro);
           RecNominaImp = eClockBase.CeC.Convierte2Int(eClock5.Clases.Parametros.Tag2Parametros(this.Tag).Listado.Seleccionado.Descripcion);
           Sesion = eClock5.CeC_Sesion.ObtenSesion(this);
           eClockBase.Controladores.Nominas cNomina = new eClockBase.Controladores.Nominas(Sesion);
           cNomina.ObtenDatosReciboEvent += cNomina_ObtenDatosReciboEvent;
           cNomina.ObtenDatosRecibo(RecNominaID);

        }

        void cNomina_ObtenDatosReciboEvent(eClockBase.Modelos.Nomina.Reporte_RecNomina RecNominas)
        {
            if (RecNominas != null)
            {
                MovPercepciones.ActualizaVista(RecNominas.Percepciones, 1, false);
                for (int i = 0; i < RecNominas.Percepciones.Count(); i++)
                {
                    SumImporte_Percep = RecNominas.Percepciones[i].REC_NOMI_MOV_IMPORTE + SumImporte_Percep;
                }


                MovDeducciones.ActualizaVista(RecNominas.Deducciones, 2, true);
                for (int i = 0; i < RecNominas.Deducciones.Count(); i++)
                {
                    SumImporte_Deducc = RecNominas.Deducciones[i].REC_NOMI_MOV_IMPORTE + SumImporte_Deducc;
                }

                eClockBase.Modelos.Nomina.Model_Rec_Nominas_Ampliados RecNominaAmpliado = RecNominas.Recibo_Nomina[0];


                Lbl_Empresa_cont.Text = " " + RecNominaAmpliado.COMPANIA;
                Lbl_NombreEmpleado_cont.Text = " " + RecNominaAmpliado.PERSONA_NOMBRE;
                Lbl_Area_cont.Text = " " + RecNominaAmpliado.AREA;
                Lbl_Imss_cont.Text = RecNominaAmpliado.IMSS;
                Lbl_Puesto_cont.Text = RecNominaAmpliado.PUESTO;
                Lbl_Curp_cont.Text = RecNominaAmpliado.CURP;
                Lbl_RFC_cont.Text = RecNominaAmpliado.RFC;
                Lbl_Vales.Text = RecNominaAmpliado.REC_NOMINA_VALES.ToString("C");
                Lbl_DíasPagados.Text = eClockBase.CeC.Convierte2String(RecNominaAmpliado.REC_NOMINA_DIASPAG);
                Lbl_DescTrabajados.Text = eClockBase.CeC.Convierte2String(RecNominaAmpliado.REC_NOMINA_DESCTRAB);
                Lbl_HrExtra.Text = RecNominaAmpliado.REC_NOMINA_HEXTRAS.ToString("N2");
                Lbl_NetPag.Text = " " + RecNominaAmpliado.REC_NOMINA_N_PAGAR.ToString("C");
                Lbl_FechaInicio.Text = RecNominaAmpliado.REC_NOMINA_FINICIO.ToShortDateString();
                Lbl_FechaFinal.Text = RecNominaAmpliado.REC_NOMINA_FFIN.ToShortDateString();
                Lbl_FechaGenera.Text = RecNominaAmpliado.REC_NOMINA_FECHA_GEN.ToShortDateString();
                Lbl_Comentarios_cont.Text = RecNominaAmpliado.REC_NOMINA_COMENTARIOS;

                Lbl_SumImporte_Perc.Text = " " + SumImporte_Percep.ToString("C");
                Lbl_SumImporte_Deducc.Text = " " + SumImporte_Deducc.ToString("C");

            }
            
        }


        
    }
}
