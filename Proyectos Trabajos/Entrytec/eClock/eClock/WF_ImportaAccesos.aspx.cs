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

public partial class WF_ImportaAccesos : System.Web.UI.Page
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
            FileUpload1.Visible = false;
            LError.Visible = false;
            LCorrecto.Visible = false;
            btnImportar.Visible = false;
            return;
        }
        //**************************************************

        if (!IsPostBack)
        {
            //Agregar Módulo Log
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Importación de Accesos", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);


            WC_Terminales.DataSource = CeC_Terminales.ObtenTerminalesCatalogo(Sesion); ;
            WC_Terminales.DataValueField = "TERMINAL_ID";
            WC_Terminales.DataTextField = "TERMINAL_NOMBRE";
            WC_Terminales.DataBind();
            WC_Terminales.Columns[0].Hidden = true;
            CeC_Grid.AplicaFormato(WC_Terminales);
        }
        Sesion.ControlaBoton(ref btnImportar);
    }
    private string ImportaArchivoAnviz(byte[] Archivo, out int Importados)
    {
        string Errores = "";
        Importados = 0;
        try
        {
            WS_eCheck WS = new WS_eCheck();

            int LongitudTamBloque = 3;
            int LongitudBloque = 14;
            int Registros = Convert.ToInt32(ObtenValor(Archivo, 0, LongitudTamBloque));

            for (int i = LongitudTamBloque; i < LongitudBloque * Registros; i += LongitudBloque)
            {
                ulong Empleado = ObtenValor(Archivo, i, 5);
                ulong Fecha = ObtenValor(Archivo, i + 5, 4);
                ulong BackUpID = ObtenValor(Archivo, i + 5 + 4, 1);
                ulong TipoRegistro = ObtenValor(Archivo, i + 5 + 4 + 1, 1);
                ulong CodigoTrabajo = ObtenValor(Archivo, i + 5 + 4 + 1 + 1, 3);

                DateTime F2000 = new DateTime(2000, 01, 01);
                F2000 = F2000.AddSeconds(Fecha);
                if (CeC_Accesos.AgregaChecada(Convert.ToInt32(WC_Terminales.DataValue), Empleado.ToString(), F2000, Convert.ToInt32(WS_eCheck.TipoAccesos.Correcto), Sesion.SESION_ID, Sesion.SUSCRIPCION_ID, false))
                    Importados++;
            }
            return Errores;
        }
        catch { }
        return Errores;
    }

    private ulong ObtenValor(byte[] Arreglo, int Pos, int Longitud)
    {
        ulong Valor = 0;
        if (Longitud > 4)
        {
            Pos += Longitud - 4;
            Longitud = 4;
        }
        for (int Cont = Pos; Cont < Pos + Longitud; Cont++)
        {
            Valor = Valor * 256 + Arreglo[Cont];
        }
        return Valor;
    }


    protected void btnImportar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        LError.Text = "";
        LCorrecto.Text = "";

        if (FileUpload1.HasFile && WC_Terminales.SelectedIndex > -1)
        {
            if (FileUpload1.PostedFile.ContentType == "application/octet-stream" && System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToUpper() == ".KQ")
            {
                byte[] Datos = new byte[FileUpload1.PostedFile.InputStream.Length];
                FileUpload1.PostedFile.InputStream.Read(Datos, 0, Datos.Length);
                int Importados = 0;
                LError.Text = ImportaArchivoAnviz(Datos, out Importados);
                LCorrecto.Text = Importados.ToString() + " Registros importados correctamente";
                return;
            }
            if (FileUpload1.PostedFile.ContentType == "application/octet-stream" || FileUpload1.PostedFile.ContentType == "text/plain")
            {
                try
                {
                    WS_eCheck WS = new WS_eCheck();
                    System.IO.StreamReader StreamR = new System.IO.StreamReader(FileUpload1.PostedFile.InputStream);
                    /*   StreamR.
                       System.IO.StringReader SR = new System.IO.StringReader(StreamR.ToString());*/
                    string Linea = "";
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

                                if (CeC_Accesos.AgregaChecada(Convert.ToInt32(WC_Terminales.DataValue), Empleado, Fecha, Convert.ToInt32(WS_eCheck.TipoAccesos.Correcto), Sesion.SESION_ID, Sesion.SUSCRIPCION_ID, false))
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
                                // Si en la Linea de importación los segundos exceden de 60, esa linea causa error
                                // Ejemplo: 
                                // Error: 000000044|2012/06/25|06:04:62
                                // Correcto: 000000037|2012/06/25|06:05:25
                                DateTime FechaHora = new DateTime(Convert.ToInt16(Fecha.Substring(0, 4)), Convert.ToInt16(Fecha.Substring(5, 2)),
                                                                  Convert.ToInt16(Fecha.Substring(8, 2)), Convert.ToInt16(Hora.Substring(0, 2)),
                                                                  Convert.ToInt16(Hora.Substring(3, 2)), Convert.ToInt16(Hora.Substring(6, 2)));
                                if (WS.RegistrarChecada(WS.ObtenPersonaID(Empleado), Convert.ToInt32(WC_Terminales.DataValue), Convert.ToInt32(WS_eCheck.TipoAccesos.Correcto), FechaHora))
                                    Registros++;
                            }
                            catch(Exception ex)
                            {
                                CIsLog2.AgregaError(ex);
                            }
                        }
                    } while (Linea != null);
                    if (Registros <= 0)
                        LError.Text = " Ningún registro se ha importado";
                    else
                        LCorrecto.Text = Registros.ToString() + " Registros importados correctamente";
                }
                catch (Exception Ex) { CIsLog2.AgregaError(Ex); }
            }
            else
                LError.Text = "Formato de archivo desconocido";
        }
        else
            LError.Text = "No se ha seleccionado el archivo y/o terminal";
    }
}
