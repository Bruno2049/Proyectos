using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;

public partial class WF_ExportacionAv: System.Web.UI.Page
{
    CeC_Sesion Sesion;
    public static int terminalID;
    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        //Titulo y Descripcion del Form
        Sesion.TituloPagina = "Importación de accesos";
        Sesion.DescripcionPagina = "Seleccione el archivo de accesos que desea importar y la terminal correspondiente y a continuación use el boton importar";

        // Permisos****************************************
        if (!Sesion.TienePermisoOHijos(eClock.CEC_RESTRICCIONES.S0ImportarAccesos, true))
        {
          //  FileUpload1.Visible = false;
            LError.Visible = false;
            LCorrecto.Visible = false;
            btnImportar.Visible = false;
            return;
        }
        //**************************************************

        if (!IsPostBack)
        {
            cmbFechaInicio.Value = cmbFechaFinal.Value = DateTime.Today;
            //Agregar Módulo Log
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Importación de Accesos", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
            DataSet ds = new DataSet();

        }
        Sesion.ControlaBoton(ref btnImportar);
    }

    protected void btnImportar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        DateTime FechaInioio = (DateTime) cmbFechaInicio.Value;
        DateTime FechaFinal = ((DateTime) cmbFechaFinal.Value).AddHours(23).AddMinutes(59).AddSeconds(59);
        DataSet DSExportacion = new DataSet(); 
       
        if (FechaFinal < FechaInioio.AddMonths(1) && FechaFinal > FechaInioio)
        {
            if (rbAccesos.Items[0].Selected)
                DSExportacion = CeC_Asistencias.ObtenAccesos(-1, "", FechaInioio, FechaFinal.AddDays(1), Sesion.USUARIO_ID, Sesion);
            else if (rbAccesos.Items[1].Selected)
                DSExportacion = CeC_Asistencias.ObtenAsistencia(true, true, true, true, true, true, "", "", true,true, -1, "", FechaInioio, FechaFinal, Sesion);
            CeC_Exportacion Exp = new CeC_Exportacion();
            Exp.GenerarArchivo(DSExportacion, txtSeparador.Value.ToString());
            Exp.ExportarArchivo(this);
        }
      //  CeC_Asistencias.ObtenAccesos(Persona_ID, Agrupacion, DTFechaInicial, DTFechaFinal.AddDays(1), Sesion.USUARIO_ID);
        /*DS = CeC_Asistencias.ObtenAsistencia(EstaChecado("AS_ENTRADA_SALIDA"),
                       EstaChecado("AS_COMIDA"), EstaChecado("AS_HORAS_EXTRAS"),
                       EstaChecado("AS_TOTALES"), EstaChecado("AS_INCIDENCIA"),
                       EstaChecado("AS_TURNO"), EstaChecado("AS_INASISTENCIAS"), EstaChecado("AS_AGRUPACION"),
                       Persona_ID, Agrupacion, DTFechaInicial, DTFechaFinal.AddDays(1), Sesion.USUARIO_ID);*/
     /*   LError.Text = "";
        LCorrecto.Text = "";
        WS_eCheck WS = new WS_eCheck();
        //if (FileUpload1.HasFile && WC_Terminales.SelectedIndex > -1)
        {
         //   if (FileUpload1.PostedFile.ContentType == "application/octet-stream" || FileUpload1
      * .PostedFile.ContentType == "text/plain")
            {
                try
                {
                    System.IO.StreamReader StreamR = new System.IO.StreamReader(FileUpload1.PostedFile.InputStream);
                    /*   StreamR.
                       System.IO.StringReader SR = new System.IO.StringReader(StreamR.ToString());*/
           /*         string Linea = "";
                    int Registros = 0;
                    do
                    {
                        Linea = StreamR.ReadLine();
                        if (Linea != null && Linea.Length == 27 &&
                            Linea.Substring(0, 11) == "1234560102@" && Linea.Substring(26, 1) == "1"
                             && Linea.Substring(17, 1) == "A")
                        {
                            try
                            {
                                string Empleado = Convert.ToInt32(Linea.Substring(11, 5)).ToString();
                                string MMDD = Linea.Substring(18, 4);
                                string HHMM = Linea.Substring(22, 4);

                                DateTime Fecha = new DateTime(DateTime.Now.Year, Convert.ToInt32(MMDD.Substring(0, 2))
                                , Convert.ToInt32(MMDD.Substring(2, 2)), Convert.ToInt32(HHMM.Substring(0, 2)),
                                Convert.ToInt32(HHMM.Substring(2, 2)), 0);

                                if (WS.AgregaChecada(Convert.ToInt32(WC_Terminales.DataValue), Empleado, Fecha, WS_eCheck.TipoAccesos.Correcto))
                                    Registros++;
                            }
                            catch (Exception Ex2) { CIsLog2.AgregaError(Ex2); }
                        }
                        else if (Linea != null && Linea.Length == 29)
                        {
                            try
                            {
                                int Empleado = Convert.ToInt32(Linea.Substring(0, 9).Trim());
                                string Fecha = Linea.Substring(10, 10);
                                string Hora = Linea.Substring(21, 8);
                                DateTime FechaHora = new DateTime(Convert.ToInt16(Fecha.Substring(0, 4)), Convert.ToInt16(Fecha.Substring(5, 2)),
                                                                  Convert.ToInt16(Fecha.Substring(8, 2)), Convert.ToInt16(Hora.Substring(0, 2)),
                                                                  Convert.ToInt16(Hora.Substring(3, 2)), Convert.ToInt16(Hora.Substring(6, 2)));
                                if (WS.RegistrarChecada(WS.ObtenPersonaID(Empleado), Convert.ToInt32(WC_Terminales.DataValue), Convert.ToInt32(WS_eCheck.TipoAccesos.Correcto), FechaHora))
                                    Registros++;
                            }
                            catch
                            { }
                        }
                    } while (Linea != null);
                    LCorrecto.Text = Registros.ToString() + " Registros importados correctamente";
                }
                catch (Exception Ex) { CIsLog2.AgregaError(Ex); }
            }
            else
                LError.Text = "Formato de archivo desconocido";
        }
        else
            LError.Text = "No se ha seleccionado el archivo y/o terminal";
*/
    }



}
