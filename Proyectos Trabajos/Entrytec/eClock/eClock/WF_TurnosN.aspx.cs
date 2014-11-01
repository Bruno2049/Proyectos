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
using Reports = Infragistics.Documents.Reports;
using Infragistics.Documents.Reports.Report;
using Infragistics.WebUI.UltraWebGrid.ExcelExport;
using ReportText = Infragistics.Documents.Reports.Report.Text;


public partial class WF_TurnosN : System.Web.UI.Page
{
    CeC_Sesion Sesion;
    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        Sesion.TituloPagina = "Turnos";
        Sesion.DescripcionPagina = "Seleccione un turno para editarlo o borrarlo; o cree un nuevo turno";
        if (!IsPostBack)
        {
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Turnos", 0, "Consulta de Turnos", Sesion.SESION_ID);
            ActualizaDatos();
        }
        
    }
    void ActualizaDatos()
    {
        try
        {
            DataSet DS = CeC_Turnos.ObtenTurnosDS(Sesion.SUSCRIPCION_ID, TurnosCheckBox1.Checked);
            Grid.DataSource = DS;
            Grid.DataMember = DS.Tables[0].TableName;
            Grid.DataKeyField = "TURNO_ID";
            Grid.DataBind();
        }
        catch { }
    }
    protected void BAgregarTurno_Click1(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        Sesion.WF_Turnos_TURNO_ID = -1;
        Sesion.Redirige("WF_TurnosEdicion.aspx");
    }
    int ObtenTurnoID()
    {
        int Numero_registros = Grid.Rows.Count;

        for (int i = 0; i < Numero_registros; i++)
        {
            if (Grid.Rows[i].Selected)
            {
                try
                {
                    int ID_Borrado = Convert.ToInt32(Grid.Rows[i].Cells[0].Value);
                    return ID_Borrado;
                }
                catch (Exception ex)
                {
                    LError.Text = "Error : " + ex.Message;
                    return -2;
                }
            }
        }
        LError.Text = "Debes de seleccionar una fila";
        return -1;
    }
    protected void BBorrarTurno_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        LCorrecto.Text = "";
        LError.Text = "";
        int TurnoID = ObtenTurnoID();
        if (TurnoID > 0)
        {
            try
            {
                if (!CeC_Turnos.BorrarTurno(TurnoID, false))
                {
                    LError.Text = "No se pueden borrar el turno posiblemente se encuentra ligado a personas";
                    return;
                }
                //Agregar ModuloLog***
                Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.BORRADO, "Turnos", TurnoID, "", Sesion.SESION_ID);
                //*****		
                LCorrecto.Text = "Turno Borrado";
                ActualizaDatos();
                return;
            }
            catch (Exception ex)
            {
                LError.Text = "Error : " + ex.Message;
                return;
            }
        }
    }
    protected void BEditarTurno_Click1(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        int TurnoID = ObtenTurnoID();
        if (TurnoID > 0)
        {
            Sesion.WF_Turnos_TURNO_ID = TurnoID;
            //Sesion.Redirige("WF_TurnosE.aspx");
            Sesion.Redirige("WF_TurnosEdicion.aspx");

            return;
        }
    }
    protected void BtnDuplicar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        LCorrecto.Text = "";
        LError.Text = "";
        int TurnoID = ObtenTurnoID();
        if (TurnoID > 0)
        {
            int IDTurno = -1;
            DS_Turnos2.EC_TURNOS_EDICIONRow Fila;
            DS_Turnos2.EC_TURNOS_EDICIONRow FilaD;
            DS_Turnos2TableAdapters.EC_TURNOS_EDICIONTableAdapter TADSTurnos2 = new DS_Turnos2TableAdapters.EC_TURNOS_EDICIONTableAdapter();
            DS_Turnos2 dS_Turnos2 = new DS_Turnos2();
            int Numero_registros = Grid.Rows.Count;
            decimal Copia_ID = -1;
            try
            {
                IDTurno = TurnoID;
                TADSTurnos2.Fill(dS_Turnos2.EC_TURNOS_EDICION, IDTurno);
                if (dS_Turnos2.EC_TURNOS_EDICION.Rows.Count > 0)
                {
                    Fila = (DS_Turnos2.EC_TURNOS_EDICIONRow)dS_Turnos2.EC_TURNOS_EDICION.Rows[0];
                    FilaD = dS_Turnos2.EC_TURNOS_EDICION.NewEC_TURNOS_EDICIONRow();
                    //FilaD.TURNO_NOMBRE = dS_Turnos2.EC_TURNOS_EDICION[0].TURNO_NOMBRE;
                    FilaD.TURNO_PHEXTRAS = dS_Turnos2.EC_TURNOS_EDICION[0].TURNO_PHEXTRAS;
                    FilaD.TURNO_BORRADO = dS_Turnos2.EC_TURNOS_EDICION[0].TURNO_BORRADO;
                    FilaD.TIPO_TURNO_ID = dS_Turnos2.EC_TURNOS_EDICION[0].TIPO_TURNO_ID;
                    FilaD.TURNO_ASISTENCIA = dS_Turnos2.EC_TURNOS_EDICION[0].TURNO_ASISTENCIA;
                    Copia_ID = FilaD.TURNO_ID = Convert.ToInt32(CeC_Autonumerico.GeneraAutonumerico("EC_TURNOS", "TURNO_ID"));
                    FilaD.TURNO_NOMBRE = Convert.ToString("Copia de " + dS_Turnos2.EC_TURNOS_EDICION[0].TURNO_NOMBRE);
                    dS_Turnos2.EC_TURNOS_EDICION.AddEC_TURNOS_EDICIONRow(FilaD);
                }
                TADSTurnos2.Update(dS_Turnos2.EC_TURNOS_EDICION);
            }
            catch (Exception ex)
            {
                LError.Text = "Error : " + ex.Message;
                return;
            }
            try
            {
                DS_CopiaTurnosSemanalDia DSTSD = new DS_CopiaTurnosSemanalDia();
                DS_CopiaTurnosSemanalDiaTableAdapters.EC_TURNOS_SEMANAL_DIATableAdapter TSDTA = new DS_CopiaTurnosSemanalDiaTableAdapters.EC_TURNOS_SEMANAL_DIATableAdapter();
                TSDTA.Fill(DSTSD.EC_TURNOS_SEMANAL_DIA, IDTurno);
                int cont = DSTSD.EC_TURNOS_SEMANAL_DIA.Rows.Count;

                for (int a = 0; a < cont; a++)
                {
                    DS_CopiaTurnosSemanalDia.EC_TURNOS_SEMANAL_DIARow FilaTSD;
                    DS_CopiaTurnosSemanalDia.EC_TURNOS_SEMANAL_DIARow FilaTSD2;
                    FilaTSD = (DS_CopiaTurnosSemanalDia.EC_TURNOS_SEMANAL_DIARow)DSTSD.EC_TURNOS_SEMANAL_DIA.Rows[a];
                    FilaTSD2 = DSTSD.EC_TURNOS_SEMANAL_DIA.NewEC_TURNOS_SEMANAL_DIARow();
                    FilaTSD2.DIA_SEMANA_ID = FilaTSD.DIA_SEMANA_ID;
                    FilaTSD2.TURNO_DIA_ID = FilaTSD.TURNO_DIA_ID;
                    FilaTSD2.TURNO_SEMANAL_DIA_ID = CeC_Autonumerico.GeneraAutonumerico("EC_TURNOS_SEMANAL_DIA", "TURNO_SEMANAL_DIA_ID");
                    FilaTSD2.TURNO_ID = Copia_ID;

                    DSTSD.EC_TURNOS_SEMANAL_DIA.AddEC_TURNOS_SEMANAL_DIARow(FilaTSD2);
                }
                TSDTA.Update(DSTSD.EC_TURNOS_SEMANAL_DIA);
                ActualizaDatos();
                return;
            }
            catch (Exception ex)
            {
                LError.Text = "Error : " + ex.Message;
                return;
            }

        }
        
    }
    protected void Grid_InitializeDataSource(object sender, Infragistics.WebUI.UltraWebGrid.UltraGridEventArgs e)
    {
        ActualizaDatos();
    }
    protected void Grid_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        CeC_Grid.AplicaFormato(Grid, false, false, false, false);
    }
    protected void TurnosCheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        ActualizaDatos();
    }
    protected void GridExporter_BeginExport(object sender, Infragistics.WebUI.UltraWebGrid.DocumentExport.DocumentExportEventArgs e)
    {
        CeC_Reportes.AplicaFormatoReporte(e, "Reporte de Turnos", "./imagenes/turnos64.png", Sesion);    
    }
    protected void btImprimir_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        GridExporter.Format = Reports.Report.FileFormat.PDF;
        GridExporter.TargetPaperOrientation = Reports.Report.PageOrientation.Landscape;
        GridExporter.DownloadName = "ExportacionTurnos.pdf";
        GridExporter.Export(Grid);
    }
}
