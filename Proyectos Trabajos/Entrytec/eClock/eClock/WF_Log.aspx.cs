using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Infragistics.Documents.Reports.Report;
using Infragistics.WebUI.UltraWebGrid.ExcelExport;
using Report = Infragistics.Documents.Reports.Report;


public partial class WF_Log : System.Web.UI.Page
{
    CeC_Sesion Sesion;
    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        if (!IsPostBack)
        {
            FechaFinal.MaxDate = DateTime.Today;
            FechaFinal.Value = DateTime.Today;
            FechaInicial.Value = DateTime.Today.AddDays(-7);

        }
    }
    protected void btImprimir_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        GridExporter.Format = Report.FileFormat.PDF;
        GridExporter.TargetPaperOrientation = Report.PageOrientation.Portrait;
        GridExporter.DownloadName = "Log.pdf";
        GridExporter.Export(Grid);
    }
    protected void Grid_InitializeDataSource(object sender, Infragistics.WebUI.UltraWebGrid.UltraGridEventArgs e)
    {
        ActualizaDatos();

    }
    protected void Grid_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        CeC_Grid.AplicaFormato(Grid, false, true, false, false);
        Grid.Columns.FromKey("USUARIO_USUARIO").Width = System.Web.UI.WebControls.Unit.Pixel(80);
        Grid.Columns.FromKey("LOG_AUDITORIA_FECHAHORA").Width = System.Web.UI.WebControls.Unit.Pixel(90);
        Grid.Columns.FromKey("TIPO_AUDITORIA_NOMBRE").Width = System.Web.UI.WebControls.Unit.Pixel(180);
        Grid.Columns.FromKey("LOG_AUDITORIA_DESCRIPCION").Width = System.Web.UI.WebControls.Unit.Pixel(300);
    }
    protected void GridExporter_BeginExport(object sender, Infragistics.WebUI.UltraWebGrid.DocumentExport.DocumentExportEventArgs e)
    {
        Grid.Columns.FromKey("LOG_AUDITORIA_DESCRIPCION").Width = System.Web.UI.WebControls.Unit.Pixel(700);
        CeC_Reportes.AplicaFormatoReporte(e, "Log de Auditoria del periodo " + Convert.ToDateTime(FechaInicial.Value).ToString("dd/MM/yyyy") + "-" + Convert.ToDateTime(FechaFinal.Value).ToString("dd/MM/yyyy"), "", Sesion);

    }
    void ActualizaDatos()
    {
        Sesion = CeC_Sesion.Nuevo(this);
        if (Sesion.PERFIL_ID != 4 && Sesion.PERFIL_ID != 1)
        {
            return;
        }
//        Grid.Clear();
        int SuscripcionID = Sesion.SUSCRIPCION_ID;
        string Qry = "";
        if (SuscripcionID > 1)
            Qry = " SUSCRIPCION_ID = " + SuscripcionID + " AND ";

        DataSet DS = (DataSet)CeC_BD.EjecutaDataSet("" +
                "SELECT     EC_USUARIOS.USUARIO_USUARIO, EC_LOG_AUDITORIA.LOG_AUDITORIA_FECHAHORA, " +
                "EC_TIPO_AUDITORIA.TIPO_AUDITORIA_NOMBRE, EC_LOG_AUDITORIA.LOG_AUDITORIA_DESCRIPCION " +
                "FROM         EC_LOG_AUDITORIA INNER JOIN " +
                "EC_TIPO_AUDITORIA ON EC_LOG_AUDITORIA.TIPO_AUDITORIA_ID = EC_TIPO_AUDITORIA.TIPO_AUDITORIA_ID INNER JOIN " +
                "EC_SESIONES ON EC_LOG_AUDITORIA.SESION_ID = EC_SESIONES.SESION_ID INNER JOIN " +
                "EC_USUARIOS ON EC_SESIONES.USUARIO_ID = EC_USUARIOS.USUARIO_ID " +
                "WHERE " + Qry + "(EC_LOG_AUDITORIA.LOG_AUDITORIA_FECHAHORA >= " + 
                CeC_BD.SqlFecha(Convert.ToDateTime(FechaInicial.Value)) +
                " AND EC_LOG_AUDITORIA.LOG_AUDITORIA_FECHAHORA < " + CeC_BD.SqlFecha(Convert.ToDateTime(FechaFinal.Value).AddDays(1)) + ") " +
                "ORDER BY EC_LOG_AUDITORIA.LOG_AUDITORIA_FECHAHORA DESC ");
        Grid.DataSource = DS.Tables[0];
        Grid.DataMember = DS.Tables[0].TableName;
        //Grid.DataKeyField = "ACCESO_ID";
        if(!IsPostBack)
            Grid.DataBind();
    }

    protected void FechaInicial_ValueChanged(object sender, Infragistics.WebUI.WebSchedule.WebDateChooser.WebDateChooserEventArgs e)
    {
        ActualizaDatos();
    }
    protected void FechaFinal_ValueChanged(object sender, Infragistics.WebUI.WebSchedule.WebDateChooser.WebDateChooserEventArgs e)
    {
        ActualizaDatos();
    }
    protected void Grid_InitializeRow(object sender, Infragistics.WebUI.UltraWebGrid.RowEventArgs e)
    {

    }
}
