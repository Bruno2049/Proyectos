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

public partial class WF_EjecucionSQL : System.Web.UI.Page
{
    CeC_Sesion Sesion;
    static DataSet DS = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);

        Sesion.TituloPagina = "Ejecución de Sentencias SQL";
        Sesion.DescripcionPagina = "Escriba una sentencia SQL y ejecute para visualizar el resultado";

        // Permisos****************************************
        if (!Sesion.TienePermisoOHijos(eClock.CEC_RESTRICCIONES.S0Configuracion0EjecutarSQL, true))
        {
            //Habilitarcontroles();
            return;
        }
        //**************************************************

        if (!IsPostBack)
        {
            /*****************
             Agregar ModuloLog
             *****************/
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Ejecución SQL", 0, "Ejecución de Sentencias SQL", Sesion.SESION_ID);
        }
    }

    protected void Grid_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        CeC_Grid.AplicaFormato(Grid, false, false, false, false);
    }

    protected void btn_Ejecutar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        if (Sesion.SUSCRIPCION_ID != 1)
        {
            LCorrecto.Text = "Ejecutado";
            return;
        }
        LCorrecto.Text = "";
        LError.Text = "";
        if (txt_SQL.Text.Length > 0)
        {
            string sentencia = txt_SQL.Text.ToString().Trim();
            if (sentencia.Substring(0, 6).ToUpper() == "SELECT")
            {

                DS = (DataSet)CeC_BD.EjecutaDataSet(sentencia);
                if (DS == null && CeC_BD.UltimoErrorBD.Length > 0)
                {
                    //Grid.Visible = true;
                    LError.Text = CeC_BD.UltimoErrorBD;
                }
                else
                {

                    Grid.DataSource = null;
                    Grid.ResetColumns();

                    Grid.ResetBands();
                    Grid.ResetAllStyleSettings();
                    Grid.Bands.Clear();
                    Grid.DataSource = DS;
                    Grid.DataBind();
                    CeC_Grid.AplicaFormato(Grid, false, false, false, false);
                }

            }
            else
            {
                int registros = 0;
                int Lineas = 0;
                string Errores = "";
                string[] Sentencias = sentencia.Split(new char[] { ';' });
                foreach (string SQL in Sentencias)
                {
                    if (SQL.Trim().Length > 0)
                    {
                        registros += CeC_BD.EjecutaComando(SQL);
                        Errores += "\r\n" + CeC_BD.UltimoErrorBD;
                        Lineas++;
                    }
                }
                if (Lineas > 1)
                {
                    LCorrecto.Text = "Se han modificado " + registros + " registros correctamente";
                    LError.Text = Errores;
                }
                else
                    if(Errores.Length > 0)
                        LError.Text = Errores;
                    else
                        LCorrecto.Text = "Se han modificado " + registros + " registros correctamente";
            }
        }
        else
            LError.Text = "Escriba una sentencia SQL en el área destinada";
    }

    protected void btn_Excel_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        //CeC_Grid.ExportarExcel(DS);
        /*Infragistics.Excel.Workbook hoja = new Infragistics.Excel.Workbook();
        hoja.Worksheets.Add("ConsultaSQL");
        Grid.DisplayLayout.ColHeadersVisibleDefault = Infragistics.WebUI.UltraWebGrid.ShowMarginInfo.Yes;
        ExcelExporter.Export(this.Grid, hoja);
        hoja.ActiveWorksheet = hoja.Worksheets[0];*/
    }
}
