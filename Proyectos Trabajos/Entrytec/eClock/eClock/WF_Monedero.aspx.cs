using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
//using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Infragistics.UltraGauge.Resources;
using Infragistics.UltraChart.Shared.Styles;
using Infragistics.UltraChart.Resources.Appearance;
using Infragistics.UltraChart.Core.Layers;
using Infragistics.UltraChart.Data.Series;
//using System.Drawing;
using Infragistics.Web.UI.DisplayControls;
using Infragistics.WebUI.UltraWebGrid.ExcelExport;
using Reports = Infragistics.Documents.Reports;

using ReportText = Infragistics.Documents.Reports.Report.Text;
//using Reports.Graphics;
using Infragistics.Documents.Reports.Report.Table;
using Infragistics.Documents.Reports.Report.Text;
using Infragistics.Documents.Reports.Graphics;
public partial class WF_Monedero : System.Web.UI.Page
{
    int Persona_Link_ID = -1;
    int Persona_ID = -1;
    string Agrupacion = "";
    CeC_Sesion Sesion = null;
    bool Ordenar = false;
    bool ImprimirFirma = false;
    int Persona_Link_ID_ANT = 0;
    string ExportaNombre = "";
    string LeyendaFirma = "";
    int EmpleadosXHoja = 0;
    int NoEmpleado = 0;
    /// <summary>
    /// Agregar los nuevos elementos al final de la lista para conservar
    /// compatibilidad con versiones anteriores de los reportes favoritos
    /// </summary>
    string[] VariablesSesion = { "AsistenciaMostrar", "AS_PREDETERMINADO", "AS_7_DIAS", "AS_31_DIAS", "AS_ENTRADA_SALIDA",
                                  "AS_COMIDA", "AS_HORAS_EXTRAS", "AS_TOTALES", "AS_INCIDENCIA", "AS_TURNO", "AS_SOLO_FALTAS","AS_SOLO_RETARDOS",
                                  "AS_AGRUPACION", "AS_G_AGRUPACION", "AS_G_EMPLEADO", "AsistenciaGraficar", "AsistenciaMostrarAccesos",
                                  "FIRMA","AS_EMPLEADO", "AsistenciaMostrarES", "AS_T_AGRUPACION", "AS_T_EMPLEADO","AS_T_TOTALES", "AS_T_HISTORIAL", "AS_T_SALDO"};

    public bool GuardaConsulta(string Nombre)
    {

        string Consulta = Nombre + "@";
        foreach (string Parametro in VariablesSesion)
        {
            bool Activo = CeC_Sesion.LeeBoolSesion(this, Parametro, false);
            if (Activo)
                Consulta += "1";
            else
                Consulta += "0";
        }
        CeC_Asistencias.AgregaFavorito(Sesion.USUARIO_ID, Consulta);
        return true;
    }
    public bool ObtenValPos(string Cadena, int Pos)
    {
        if (Pos >= Cadena.Length)
            return false;
        if (Cadena[Pos] == '1')
            return true;
        return false;
    }
    public void sCargaConsulta(object Pagina, string Tag)
    {
        string[] Elementos = Tag.Split(new char[] { '@' });
        int Cont = 0;
        foreach (string Parametro in VariablesSesion)
        {
            CeC_Sesion.GuardaBoolSesion(Pagina, Parametro, ObtenValPos(Elementos[1], Cont));
            Cont++;
        }
    }

    protected Infragistics.WebUI.UltraWebNavigator.Item MenuElmento(string Tag)
    {
        return Menu.Find(Tag);
    }
    protected bool EstaChecado(string Tag)
    {
        Infragistics.WebUI.UltraWebNavigator.Item Elemento = Menu.Find(Tag);
        return Elemento.Checked;
    }

    protected void ActualizaDatos()
    {
        ActualizaDatos(false);
    }

    protected void ActualizaDatos(bool LimpiarGrid)
    {
        try
        {
            if (Sesion == null)
                return;
            if (Sesion.SESION_ID <= 0)
                return;
            DateTime DTFechaInicial = Convert.ToDateTime(FechaInicial.Value).Date;
            DateTime DTFechaFinal = Convert.ToDateTime(FechaFinal.Value).Date;
            Sesion.AsistenciaFechaInicio = DTFechaInicial;
            Sesion.AsistenciaFechaFin = DTFechaFinal;
            DataSet DS = null;
            if (LimpiarGrid)
            {
                Grid.Clear();
                Grid.DataMember = "";
                Ordenar = true;
            }
            if (MenuElmento("SALDO_MONEDERO").Checked)
            {
                FechaInicial.Visible = false;
                FechaFinal.Visible = false;
                Tbx_NoDias.Visible = false;
                Btn_NoDias.Visible = false;
                LblAgrupacionSL0.Visible = false;
                DS = CeC_Monedero.ReporteSaldoMonedero(Persona_ID, Agrupacion, DTFechaInicial, DTFechaFinal, Sesion.USUARIO_ID);
                Grid.DataSource = DS.Tables[0];
                Grid.DataMember = DS.Tables[0].TableName;
                Grid.DataKeyField = "PERSONA_ID";
            }
            if (MenuElmento("CONSUMO_EMPLEADO").Checked)
            {
                FechaInicial.Visible = true;
                FechaFinal.Visible = true;
                Tbx_NoDias.Visible = true;
                Btn_NoDias.Visible = true;
                LblAgrupacionSL0.Visible = true;
                DS = CeC_Monedero.ReporteConsumoEmpleado(Persona_ID, Agrupacion, DTFechaInicial, DTFechaFinal, Sesion.USUARIO_ID);
                Grid.DataSource = DS.Tables[0];
                Grid.DataMember = DS.Tables[0].TableName;
                Grid.DataKeyField = "PERSONA_ID";
            }
            if (MenuElmento("PERSONALIZADO").Checked)
            {
                FechaInicial.Visible = true;
                FechaFinal.Visible = true;
                Tbx_NoDias.Visible = true;
                Btn_NoDias.Visible = true;
                LblAgrupacionSL0.Visible = true;
                DS = CeC_Monedero.ReportePersonalizado(Persona_ID, Agrupacion, DTFechaInicial, DTFechaFinal, Sesion.USUARIO_ID, Sesion);
                Grid.DataSource = DS.Tables[0];
                Grid.DataMember = DS.Tables[0].TableName;
                Grid.DataKeyField = "PERSONA_ID";
            }
            if (MenuElmento("MOV_MONEDERO").Checked)
            {
                FechaInicial.Visible = true;
                FechaFinal.Visible = true;
                Tbx_NoDias.Visible = true;
                Btn_NoDias.Visible = true;
                LblAgrupacionSL0.Visible = true;
                DS = CeC_Monedero.ReporteMovimientoMonedero(Persona_ID, Agrupacion, DTFechaInicial, DTFechaFinal, Sesion.USUARIO_ID);
                Grid.DataSource = DS.Tables[0];
                Grid.DataMember = DS.Tables[0].TableName;
                Grid.DataKeyField = "PERSONA_ID";
            }
            if (MenuElmento("MOV_MONEDERO_PROD").Checked)
            {
                FechaInicial.Visible = true;
                FechaFinal.Visible = true;
                Tbx_NoDias.Visible = true;
                Btn_NoDias.Visible = true;
                LblAgrupacionSL0.Visible = true;
                DS = CeC_Monedero.ReporteMovMonederoProducto(Persona_ID, Agrupacion, DTFechaInicial, DTFechaFinal, Sesion.USUARIO_ID);
                Grid.DataSource = DS.Tables[0];
                Grid.DataMember = DS.Tables[0].TableName;
                Grid.DataKeyField = "PERSONA_ID";
            }
            Grid.DataBind();
        }
        catch (Exception ex) { CIsLog2.AgregaError(ex); }
    }
    /// <summary>
    /// Carga los valores del menu de acuerdo a las variables de sesión
    /// </summary>
    private void CargaSesion()
    {

    }
    void ValidaPerfilesMenu()
    {
        try
        {
            if (Sesion.PERFIL_ID == 3 || Sesion.PERFIL_ID == 6)
            {
                MenuElmento("SALDO_MONEDERO").Hidden = false;
                MenuElmento("CONSUMO_EMPLEADO").Hidden = false;
                MenuElmento("PERSONALIZADO").Hidden = false;
                MenuElmento("MOV_MONEDERO").Hidden = false;
                MenuElmento("MOV_MONEDERO_PROD").Hidden = false;
                MenuElmento("TURNOS").Hidden = true;
                MenuElmento("JUSTIFICACIONES").Hidden = true;
            }
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
    }

    protected void CargaVariables()
    {
        Sesion = CeC_Sesion.Nuevo(this);
        if (Sesion.SESION_ID <= 0)
            return;
        string Parametros = Sesion.Parametros;
        if (Parametros == "REGRESO")
        {
            if (Sesion.eClock_Agrupacion.Length > 0)
                Parametros = "AGRUPACION";
        }
        if (Parametros == "AGRUPACION")
            Agrupacion = Sesion.eClock_Agrupacion;
        else
        {
            Persona_ID = Sesion.eClock_Persona_ID;
            Persona_Link_ID = CeC_BD.ObtenPersonaLinkID(Persona_ID);
            Sesion.eClock_Agrupacion = "";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        CargaVariables();
        if (!IsPostBack)
        {

            this.Grid.Width = Unit.Percentage(100);
            this.Grid.Height = Unit.Percentage(100);
            this.Grid.DisplayLayout.FrameStyle.CustomRules = "table-layout:auto";
            // Agrega Encabezado para exportar reportes a excel.
            //this.Grid.DisplayLayout.HeaderTitleModeDefault = Infragistics.WebUI.UltraWebGrid.CellTitleMode.Always;
            // Agrega Footer para totales en los reportes.
            this.Grid.DisplayLayout.ColFootersVisibleDefault = Infragistics.WebUI.UltraWebGrid.ShowMarginInfo.Yes;
            CeC_Config Config = new CeC_Config(Sesion.USUARIO_ID);

            CargaSesion();

            if (Sesion.Parametros == "AGRUPACION")
            {
                LblNoEmpleadoSL.Visible = LblNoEmp.Visible = false;
                LblNombreSL.Visible = LblNombre.Visible = false;
                LblTurnoSL.Visible = LblTurno.Visible = false;
                ImgEmpleado.Visible = false;
                LblAgrupacion.Text = Agrupacion;
            }
            else
            {
                LblNoEmp.Text = Persona_Link_ID.ToString();
                LblNombre.Text = CeC_BD.ObtenPersonaNombre(Persona_ID);
                LblAgrupacion.Text = CeC_Campos.ObtenValorCampo(Persona_ID, "AGRUPACION_NOMBRE");
                int TurnoID = CeC.Convierte2Int(CeC_Campos.ObtenValorCampo(Persona_ID, "TURNO_ID"), 0);

                if (0 == TurnoID)
                {
                    LblTurno.ForeColor = System.Drawing.Color.Red;
                    LblTurno.Text = "No se ha asignado Turno";
                }
                else
                    LblTurno.Text = CeC_Turnos.ObtenTurnoNombre(TurnoID);
                ImgEmpleado.ImageUrl = "WF_Personas_ImaS.aspx?P=" + Persona_ID;
            }
            FechaInicial.Value = Sesion.AsistenciaFechaInicio;
            FechaFinal.Value = Sesion.AsistenciaFechaFin;

            ValidaPerfilesMenu();
        }
        /*        LinearGauge gauge = this.GauAsis.Gauges[0] as LinearGauge;
                gauge.Scales[0].Markers[0].Value = 10;*/
        if (!IsPostBack)
        {
            CeC_Asistencias.GeneraPrevioPersonaDiario(Persona_ID);
        }
        if (IsPostBack && sender != null)
        {
            //            return;
            if (Request.Params.Get("__EVENTTARGET") == "MenuMenu")
            {
                string Tag = Request.Params.Get("ValorMenuTag");
                string TagPadre = Request.Params.Get("ValorMenuTagPadre");
                if (TagPadre == "SALDO_MONEDERO")
                { }
                if (TagPadre == "CONSUMO_EMPLEADO")
                { }
                if (TagPadre == "PERSONALIZADO")
                { }
                if (TagPadre == "MOV_MONEDERO")
                { }
                if (TagPadre == "MOV_MONEDERO_PROD")
                { }
                ActualizaDatos(true);
            }
            if (Request.Params.Get("__EVENTTARGET") == "Tbx_NoDias")
            {
                FechaFinal.Value = CeC.Convierte2DateTime(FechaInicial.Value).AddDays(CeC.Convierte2Int(Request.Params.Get("__EVENTARGUMENT")) - 1);
                ActualizaDatos(true);
            }
        }
        //ActualizaDatos();
    }

    protected void Grid_InitializeDataSource(object sender, Infragistics.WebUI.UltraWebGrid.UltraGridEventArgs e)
    {
        if (IsPostBack)
        {
            //Page_Load(null, null);
            CargaVariables();
        }
        ActualizaDatos();
    }
    protected void AsignaFormatoDia(Infragistics.WebUI.UltraWebGrid.UltraGridColumn Columna, int NO)
    {
        DateTime DTFechaInicial = Convert.ToDateTime(FechaInicial.Value).Date;
        Columna.Header.Caption = DTFechaInicial.AddDays(NO).ToString("dddd dd-MM-yy");
        Columna.Header.Style.Font.Size = 7;
        Columna.CellStyle.Font.Size = 6;
        Columna.Width = System.Web.UI.WebControls.Unit.Pixel(70);
    }
    protected void AsignaFormatoInc(Infragistics.WebUI.UltraWebGrid.UltraGridColumn Columna, int NO)
    {
        DateTime DTFechaInicial = Convert.ToDateTime(FechaInicial.Value).Date;
        Columna.Header.Caption = DTFechaInicial.AddDays(NO).ToString("dd-MM-yy");
        Columna.Header.Style.Font.Size = 7;
        Columna.CellStyle.Font.Size = 6;
        Columna.Width = System.Web.UI.WebControls.Unit.Pixel(30);
    }
    string ObtenImag(object Valor, string Color)
    {
        return "PB/" + Color + Valor.ToString() + ".jpg";
    }

    string ObtenDiv()
    {
        return " background-size: 100%; background-position: fixed; text-align: center;";
    }

    protected void Grid_InitializeRow(object sender, Infragistics.WebUI.UltraWebGrid.RowEventArgs e)
    {

    }
    protected void Grid_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        try { Grid.Columns.FromKey("PERSONA_DIARIO_ID").Hidden = true; }
        catch { }
        try { Grid.Columns.FromKey("PERSONA_ID").Hidden = true; }
        catch { }
        try { Grid.Columns.FromKey("ACCESO_ID").Hidden = true; }
        catch { }
        try { Grid.Columns.FromKey("PERSONA_ES_ID").Hidden = true; }
        catch { }

        try { Grid.Columns.FromKey("INCIDENCIA_ABR").Hidden = true; }
        catch { }
        Grid.DisplayLayout.ColFootersVisibleDefault = Infragistics.WebUI.UltraWebGrid.ShowMarginInfo.Yes;
        CeC_Grid.SumaTotales(Grid);
        CargaOrden();
    }

    protected void FechaInicial_ValueChanged(object sender, Infragistics.WebUI.WebSchedule.WebDateChooser.WebDateChooserEventArgs e)
    {
        DateTime DTFechaInicial = Convert.ToDateTime(FechaInicial.Value).Date;
        DateTime DTFechaFinal = Convert.ToDateTime(FechaFinal.Value).Date;

        int Dias = Convert.ToInt32(((TimeSpan)(DTFechaFinal - Sesion.AsistenciaFechaInicio)).TotalDays);
        FechaFinal.Value = DTFechaInicial.AddDays(Dias);
        ActualizaDatos(true);
    }
    protected void FechaFinal_ValueChanged(object sender, Infragistics.WebUI.WebSchedule.WebDateChooser.WebDateChooserEventArgs e)
    {
        DateTime DTFechaInicial = Convert.ToDateTime(FechaInicial.Value).Date;
        DateTime DTFechaFinal = Convert.ToDateTime(FechaFinal.Value).Date;
        Tbx_NoDias.Value = (DTFechaFinal - DTFechaInicial).TotalDays;
        ActualizaDatos(true);
    }

    void Muestra(string Tag)
    {
        if (Tag == "SALDO_MONEDERO")
        {
            MenuElmento("CONSUMO_EMPLEADO").Checked = false;
            MenuElmento("PERSONALIZADO").Checked = false;
            MenuElmento("MOV_MONEDERO").Checked = false;
            MenuElmento("MOV_MONEDERO_PROD").Checked = false;
        }
        if (Tag == "CONSUMO_EMPLEADO")
        {
            MenuElmento("SALDO_MONEDERO").Checked = false;
            MenuElmento("PERSONALIZADO").Checked = false;
            MenuElmento("MOV_MONEDERO").Checked = false;
            MenuElmento("MOV_MONEDERO_PROD").Checked = false;
        }
        if (Tag == "PERSONALIZADO")
        {
            MenuElmento("SALDO_MONEDERO").Checked = false;
            MenuElmento("CONSUMO_EMPLEADO").Checked = false;
            MenuElmento("MOV_MONEDERO_PROD").Checked = false;
            MenuElmento("MOV_MONEDERO").Checked = false;
        }
        if (Tag == "MOV_MONEDERO")
        {
            MenuElmento("SALDO_MONEDERO").Checked = false;
            MenuElmento("CONSUMO_EMPLEADO").Checked = false;
            MenuElmento("PERSONALIZADO").Checked = false;
            MenuElmento("MOV_MONEDERO_PROD").Checked = false;
        }
        if (Tag == "MOV_MONEDERO_PROD")
        {
            MenuElmento("SALDO_MONEDERO").Checked = false;
            MenuElmento("CONSUMO_EMPLEADO").Checked = false;
            MenuElmento("PERSONALIZADO").Checked = false;
            MenuElmento("MOV_MONEDERO").Checked = false;
        }
    }

    protected void Menu_MenuItemChecked(object sender, Infragistics.WebUI.UltraWebNavigator.WebMenuItemCheckedEventArgs e)
    {
        Infragistics.WebUI.UltraWebNavigator.Item Padre = e.Item.Parent;
        if (Padre != null)
            Muestra(Padre.Tag.ToString());
        string Tag = e.Item.Tag.ToString();
        Muestra(Tag);
        ActualizaDatos(true);
        ValidaPerfilesMenu();
    }
    int ObtenNoDia(string ColumnaNombre)
    {
        if ((ColumnaNombre.Length > 12 && ColumnaNombre.Substring(0, 12) == "ASISTENCIA_D") ||
            (ColumnaNombre.Length > 7 && ColumnaNombre.Substring(0, 7) == "TURNO_D") ||
            (ColumnaNombre.Length > 3 && ColumnaNombre.Substring(0, 3) == "DIA"))
        {
            int Sumar = 0;
            if (ColumnaNombre.Length > 12)
                Sumar = Convert.ToInt32(ColumnaNombre.Substring(12));
            else
                if (ColumnaNombre.Length > 7)
                    Sumar = Convert.ToInt32(ColumnaNombre.Substring(7));
                else
                    Sumar = Convert.ToInt32(ColumnaNombre.Substring(3));
            DateTime FInicial = CeC.Convierte2DateTime(FechaInicial.Value);
            if (FInicial.AddDays(Sumar).Year != FInicial.Year)
                if (FInicial.Year % 4 != 0)
                    Sumar++;
            return Sumar;
        }
        return -1;
    }

    bool PuedeModificar()
    {
        if (CeC.ExisteEnSeparador(Sesion.ConfiguraSuscripcion.UsuariosIdsModificanFechasBloqueadas, Sesion.USUARIO_ID.ToString(), ","))
            return true;
        int Numero_Resgistos = Grid.Rows.Count;
        string PersonasDiarioID = "";

        for (int i = 0; i < Numero_Resgistos; i++)
        {
            if (Grid.Rows[i].Selected && (MenuElmento("AS_PREDETERMINADO").Checked || MenuElmento("AS_T_HISTORIAL").Checked))
            {
                int Persona_Diario_ID = Convert.ToInt32(Grid.Rows[i].DataKey);
                PersonasDiarioID = CeC.AgregaSeparador(PersonasDiarioID, Persona_Diario_ID.ToString(), ",");
            }

            for (int Cont = 0; Cont < Grid.Rows[i].Cells.Count; Cont++)
            {
                if (Grid.Rows[i].Selected || Grid.Rows[i].Cells[Cont].Selected)
                {
                    int Persona_Diario_ID = Convert.ToInt32(Grid.Rows[i].DataKey);
                    string Columna = Grid.Rows[i].Cells[Cont].Column.Key;

                    if (Columna == "TURNO_NOMBRE")
                    {
                    }
                    else
                    {
                        int DiaNo = ObtenNoDia(Columna);
                        if (DiaNo >= 0)
                            PersonasDiarioID = CeC.AgregaSeparador(PersonasDiarioID, Convert.ToString(Persona_Diario_ID + DiaNo), ",");
                    }
                }
            }
        }
        try
        {
            if (MenuElmento("AS_T_HISTORIAL").Checked)
                return CeC_Periodos.PuedeModificarAlmacenInc(PersonasDiarioID);

            return CeC_Periodos.PuedeModificar(PersonasDiarioID);
            //if(!Ret)

        }
        catch { }
        return false;
    }

    string BorrarInventario()
    {
        if (!PuedeModificar())
            return "";
        int Numero_Resgistos = Grid.Rows.Count;
        string Errores = "";


        string sAlmacenIncIDs = "";

        for (int i = 0; i < Numero_Resgistos; i++)
        {
            if (Grid.Rows[i].Selected)
            {
                int AlmacenIncID = Convert.ToInt32(Grid.Rows[i].DataKey);
                sAlmacenIncIDs = CeC.AgregaSeparador(sAlmacenIncIDs, AlmacenIncID.ToString(), ",");
            }
        }
        return CeC_IncidenciasInventario.CorrigeMovimientos(sAlmacenIncIDs, Sesion.SESION_ID, Sesion.SUSCRIPCION_ID);

    }

    protected void Menu_MenuItemClicked(object sender, Infragistics.WebUI.UltraWebNavigator.WebMenuItemEventArgs e)
    {
        try
        {
            Infragistics.WebUI.UltraWebNavigator.Item Padre = e.Item.Parent;
            string Tag = e.Item.Tag.ToString();
            if (Padre != null)
            {
                string TagPadre = Padre.Tag.ToString();
                Muestra(TagPadre);
                if (Tag == "FAVORITO_BORRAR")
                {
                    Sesion.Configura.AsistenciaFavoritos = Sesion.Configura.AsistenciaFavoritos.Replace(TagPadre, "");
                    Padre.Parent.Items.Remove(Padre);
                    return;
                }
                if (Tag == "FAVORITO_COMPARTIR")
                {
                    //DlgCompartirFav.WindowState = Infragistics.Web.UI.LayoutControls.DialogWindowState.Normal;
                    Sesion.EjecutaScript("");
                    /*  confirm("¿Desea compartir sus datos?", function () {
                  window.location.href = 'http://www.ericmmartin.com/projects/simplemodal/';
              });*/
                }
                if (TagPadre == "FAVORITOS")
                {
                    if (Tag.Substring(0, 2) == "F_")
                    {
                        if (Tag == "F_AGREGAR")
                        {
                            DlgAgregarFav.WindowState = Infragistics.Web.UI.LayoutControls.DialogWindowState.Normal;
                            //                        Sesion.EjecutaScript("");

                        }
                    }
                    else
                    {
                        CargaConsulta(Tag);
                    }
                    return;
                }
                if (TagPadre == "TURNOS")
                { }
                if (TagPadre == "TURNOSPRED")
                { }

                if (TagPadre == "EXPORTAR" || TagPadre == "PDF" || TagPadre == "XPS" || TagPadre == "TEXTO")
                {
                    if (Tag == "EXCEL")
                    {
                        Grid.DisplayLayout.Pager.AllowPaging = false;
                        Grid.DisplayLayout.LoadOnDemand = Infragistics.WebUI.UltraWebGrid.LoadOnDemand.NotSet;
                        Grid.DataBind();
                        GridExcel.WorksheetName = "eClock";
                        GridExcel.DownloadName = "Exportacion.xls";
                        GridExcel.Export(Grid);
                        CIsLog2.AgregaLog("GridExcel.WorksheetName = eClock");
                    }
                    if (Tag == "XPS" || Tag == "XPS_VERTICAL")
                    {
                        GridExporter.Format = Reports.Report.FileFormat.XPS;
                        GridExporter.TargetPaperOrientation = Reports.Report.PageOrientation.Portrait;
                        GridExporter.DownloadName = "Exportacion.xps";
                    }
                    if (Tag == "XPS_HORIZONTAL")
                    {
                        GridExporter.Format = Reports.Report.FileFormat.XPS;
                        GridExporter.TargetPaperOrientation = Reports.Report.PageOrientation.Landscape;
                        GridExporter.DownloadName = "Exportacion.xps";
                    }
                    if (Tag == "PDF" || Tag == "PDF_VERTICAL")
                    {
                        GridExporter.Format = Reports.Report.FileFormat.PDF;
                        GridExporter.TargetPaperOrientation = Reports.Report.PageOrientation.Portrait;
                        GridExporter.DownloadName = "Exportacion.pdf";
                    }
                    if (Tag == "PDF_HORIZONTAL")
                    {
                        GridExporter.Format = Reports.Report.FileFormat.PDF;
                        GridExporter.TargetPaperOrientation = Reports.Report.PageOrientation.Landscape;
                        GridExporter.DownloadName = "Exportacion.pdf";
                    }
                    if (Tag == "PDF" || TagPadre == "PDF" || Tag == "XPS" || TagPadre == "XPS")
                    {
                        GridExporter.Export(Grid);
                    }
                    if (Tag == "TEXTO" || TagPadre == "TEXTO")
                    {
                        GridExporter.Format = Reports.Report.FileFormat.PlainText;
                        if (Tag == "CSV")
                            GridExporter.DownloadName = "Exportacion.txt";
                        else
                            GridExporter.DownloadName = "Exportacion.txt";
                        GridExporter.Export(Grid);
                    }
                    return;
                }
            }
            else
            {
                if (Tag == "BORRAR_INVENTARIO")
                    BorrarInventario();
            }
            Muestra(Tag);
            ActualizaDatos(true);
            ValidaPerfilesMenu();
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
    }
    
    protected void BtnAct_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        string Tag = Request.Params.Get("ValorMenuTag");
        string TagPadre = Request.Params.Get("ValorMenuTagPadre");
        if (TagPadre == "TURNOS")
        { }
        if (TagPadre == "TURNOSPRED")
        { }
        if (TagPadre == "JUSTIFICACIONES")
        { }
        ActualizaDatos(true);
    }
    protected void GridExporter_BeginExport(object sender, Infragistics.WebUI.UltraWebGrid.DocumentExport.DocumentExportEventArgs e)
    {
        ImprimirFirma = MenuElmento("FIRMA").Checked;
        string Empresa = "";
        if (Sesion.ConfiguraSuscripcion.CompaniaNombre != "")
            Empresa = "\n" + Sesion.ConfiguraSuscripcion.CompaniaNombre;
        if (MenuElmento("SALDO_MONEDERO").Checked)
            CeC_Reportes.AplicaFormatoReporte(e, "Saldo de Monedero", "Desde: " + Sesion.AsistenciaFechaInicio.ToString("dd/MM/yyyy") + "\tHasta:  " + Sesion.AsistenciaFechaFin.ToString("dd/MM/yyyy"), "imagenes/Asistencias.png", Sesion);
        if (MenuElmento("CONSUMO_EMPLEADO").Checked)
            //CeC_Reportes.AplicaFormatoReporteConsumoEmpleado(e, "Consumo por Empleado", "Desde: " + Sesion.AsistenciaFechaInicio.ToString("dd/MM/yyyy") + "\tHasta:  " + Sesion.AsistenciaFechaFin.ToString("dd/MM/yyyy"), "imagenes/Asistencias.png", Sesion);
            CeC_Reportes.AplicaFormatoReporte(e, "Consumo por Empleado", "Desde: " + Sesion.AsistenciaFechaInicio.ToString("dd/MM/yyyy") + "\tHasta:  " + Sesion.AsistenciaFechaFin.ToString("dd/MM/yyyy"), "imagenes/Asistencias.png", Sesion);
        if (MenuElmento("PERSONALIZADO").Checked)
            CeC_Reportes.AplicaFormatoReporte(e, "Reporte Pesonalizado", "Desde: " + Sesion.AsistenciaFechaInicio.ToString("dd/MM/yyyy") + "\tHasta:  " + Sesion.AsistenciaFechaFin.ToString("dd/MM/yyyy"), "imagenes/Asistencias.png", Sesion);
        if (MenuElmento("MOV_MONEDERO").Checked)
            CeC_Reportes.AplicaFormatoReporte(e, "Movimientos por Monedero", "Desde: " + Sesion.AsistenciaFechaInicio.ToString("dd/MM/yyyy") + "\tHasta:  " + Sesion.AsistenciaFechaFin.ToString("dd/MM/yyyy"), "imagenes/Asistencias.png", Sesion);
        if (MenuElmento("MOV_MONEDERO_PROD").Checked)
            CeC_Reportes.AplicaFormatoReporte(e, "Monedero por Producto", "Desde: " + Sesion.AsistenciaFechaInicio.ToString("dd/MM/yyyy") + "\tHasta:  " + Sesion.AsistenciaFechaFin.ToString("dd/MM/yyyy"), "imagenes/Asistencias.png", Sesion);
        if (ImprimirFirma)
        {
            try { Grid.Columns.FromKey("PERSONA_NOMBRE").Hidden = true; }
            catch { }
            try { Grid.Columns.FromKey("PERSONA_LINK_ID").Hidden = true; }
            catch { }
            try { Grid.Columns.FromKey("AGRUPACION_NOMBRE").Hidden = true; }
            catch { }
        }
    }

    void AgregarPieFirma(ITable Tabla)
    {
        if (!ImprimirFirma)
            return;
        if (LeyendaFirma == "")
            LeyendaFirma = Sesion.ConfiguraSuscripcion.LEYENDA_REPORTE_ASISTENCIA;
        ITableRow row = Tabla.AddRow();
        ITableCell cell = row.AddCell();
        cell.Width = new Reports.Report.RelativeWidth(100);
        IText cellText = cell.AddText();
        Reports.Report.Text.Style fontStyle = new Reports.Report.Text.Style(Fonts.Tahoma, Brushes.Black);
        cellText.Alignment.Horizontal = Reports.Report.Alignment.Center;
        cellText.AddLineBreak();
        cellText.AddLineBreak();
        //cellText.AddContent("\n", fontStyle);
        //cellText.AddContent("\n", fontStyle);
        cellText.AddContent("________________________________________________\n", fontStyle);
        cellText.AddContent(ExportaNombre, fontStyle);
        cellText.AddLineBreak();
        cellText.AddContent(LeyendaFirma, fontStyle);
        // style the new row
        cell.Background = new Reports.Report.Background(Reports.Graphics.Brushes.AliceBlue);
        cell.Paddings.Top = 2;
        cell.Paddings.Bottom = 2;
        cell.Borders.Bottom = new Reports.Report.Border(Reports.Graphics.Pens.Gray);
        if (EmpleadosXHoja <= 0)
            EmpleadosXHoja = Sesion.ConfiguraSuscripcion.EmpleadosXHoja;

        if (NoEmpleado % EmpleadosXHoja == EmpleadosXHoja - 1)
            Tabla.AddRow().AddCell().AddPageBreak();
        NoEmpleado++;
    }

    public string ObtenPersonaNombre(Infragistics.WebUI.UltraWebGrid.UltraGridRow Fila)
    {
        string Nombre = "";
        try
        {
            Nombre = Convert.ToString(Fila.Cells.FromKey("PERSONA_NOMBRE").Value);
        }
        catch
        {
            int Persona_ID = Convert.ToInt32(Fila.Cells.FromKey("PERSONA_ID").Value);
            return CeC_BD.ObtenPersonaNombre(Persona_ID);
        }
        return Nombre;
    }
    protected void GridExporter_RowExporting(object sender, Infragistics.WebUI.UltraWebGrid.DocumentExport.RowExportingEventArgs e)
    {
        try
        {
            if (!ImprimirFirma)
                return;
            int Persona_Link_ID = Convert.ToInt32(e.GridRow.Cells.FromKey("PERSONA_LINK_ID").Value);
            if (Persona_Link_ID_ANT != Persona_Link_ID)
            {

                if (Persona_Link_ID_ANT != 0)
                {
                    AgregarPieFirma(e.ContainingTable);

                    // e.ContainingTable.AddRow().AddCell().AddQuickText
                }
                Persona_Link_ID_ANT = Persona_Link_ID;

                {
                    // Add an extra row element to the document
                    ITableRow row = e.ContainingTable.AddRow();
                    ITableCell cell = row.AddCell();
                    cell.Width = new Reports.Report.RelativeWidth(100);
                    IText cellText = cell.AddText();

                    Reports.Report.Text.Style fontStyle = new Reports.Report.Text.Style(Fonts.Tahoma, Brushes.Black);
                    ExportaNombre = ObtenPersonaNombre(e.GridRow);
                    cellText.AddContent("Empleado: " + ExportaNombre + " (" + Persona_Link_ID + ")", fontStyle);

                    // style the new row
                    cell.Background = new Reports.Report.Background(Reports.Graphics.Brushes.AliceBlue);
                    cell.Paddings.Top = 2;
                    cell.Paddings.Bottom = 2;
                    cell.Borders.Bottom = new Reports.Report.Border(Reports.Graphics.Pens.Gray);
                }
            }

        }
        catch { }
    }
    protected void GridExporter_RowExported(object sender, Infragistics.WebUI.UltraWebGrid.DocumentExport.RowExportedEventArgs e)
    {
        if (e.GridRow.ParentCollection.Count <= e.GridRow.Index + 1)
        {
            AgregarPieFirma(e.ContainingTable);
        }

    }

    protected void Grid_SortColumn(object sender, Infragistics.WebUI.UltraWebGrid.SortColumnEventArgs e)
    {
        /* Infragistics.WebUI.UltraWebGrid.UltraGridColumn Columna = Grid.Bands[0].Columns[e.ColumnNo];
         if(Columna.SortIndicator == Infragistics.WebUI.UltraWebGrid.SortIndicator.Ascending)
             Columna.SortIndicator = Infragistics.WebUI.UltraWebGrid.SortIndicator.Descending;
         else
             Columna.SortIndicator = Infragistics.WebUI.UltraWebGrid.SortIndicator.Ascending;
        
         */
        GuardaOrden();
    }

    private void GuardaOrden()
    {
        try
        {
            //            Grid.Bands[0].SortedColumns[0].SortIndicator
            //Sesion.AS_CAMPO_ORDEN = Grid.Bands[e.BandNo].Columns[e.ColumnNo].Key;
            string Orden = "";
            Infragistics.WebUI.UltraWebGrid.SortedColsCollection sortedCols = Grid.Bands[0].SortedColumns;
            for (int i = 0; i < sortedCols.Count; i++)
            {
                if (sortedCols[i].SortIndicator == Infragistics.WebUI.UltraWebGrid.SortIndicator.Ascending)
                    Orden = CeC.AgregaSeparador(Orden, "A|" + sortedCols[i].BaseColumnName, ",");
                if (sortedCols[i].SortIndicator == Infragistics.WebUI.UltraWebGrid.SortIndicator.Descending)
                    Orden = CeC.AgregaSeparador(Orden, "D|" + sortedCols[i].BaseColumnName, ",");
            }
            GridOrden = Orden;
        }
        catch { }
    }

    private void CargaOrden()
    {
        if (!Ordenar)
            return;
        CargaPropColumnas();
        string[] Campos = CeC.ObtenArregoSeparador(GridOrden, ",");
        foreach (string Campo in Campos)
        {
            try
            {
                string[] Valores = CeC.ObtenArregoSeparador(Campo, "|");
                Infragistics.WebUI.UltraWebGrid.UltraGridColumn Columna = Grid.Bands[0].Columns.FromKey(Valores[1]);
                if (Valores[0] == "A")
                    Columna.SortIndicator = Infragistics.WebUI.UltraWebGrid.SortIndicator.Ascending;
                if (Valores[0] == "D")
                    Columna.SortIndicator = Infragistics.WebUI.UltraWebGrid.SortIndicator.Descending;
                Grid.Bands[0].SortedColumns.Add(Columna);
            }
            catch
            {
            }
        }
    }

    private void CargaPropColumnas()
    {
        if (!Ordenar)
            return;
        string[] Campos = CeC.ObtenArregoSeparador(GridAgrupadas, ",");
        foreach (string Campo in Campos)
        {
            try
            {
                Infragistics.WebUI.UltraWebGrid.UltraGridColumn Columna = Grid.Bands[0].Columns.FromKey(Campo);
                Columna.IsGroupByColumn = true;
            }
            catch { }
        }
        Campos = CeC.ObtenArregoSeparador(GridStaticas, ",");
        foreach (string Campo in Campos)
        {
            try
            {
                Infragistics.WebUI.UltraWebGrid.UltraGridColumn Columna = Grid.Bands[0].Columns.FromKey(Campo);
                Columna.Header.Fixed = true;
            }
            catch { }
        }
    }
    private void GuardaPropColumnas()
    {
        string Agrupadas = "";
        string Estaticas = "";
        for (int i = 0; i < Grid.Bands[0].Columns.Count; i++)
        {
            Infragistics.WebUI.UltraWebGrid.UltraGridColumn Columna = Grid.Bands[0].Columns[i];
            if (Columna.Header.Fixed)
                Estaticas = CeC.AgregaSeparador(Estaticas, Columna.Key, ",");
            if (Columna.IsGroupByColumn)
                Agrupadas = CeC.AgregaSeparador(Agrupadas, Columna.Key, ",");

        }
        GridStaticas = Estaticas;
        GridAgrupadas = Agrupadas;
    }
    #region Variables de Sesión
    /// <summary>
    /// obtiene o establece el listado de columnas separadas por coma del orden aplicado
    /// ej. A|Campo1,D|Campo2
    /// usar A para Acendente y B para descendente
    /// </summary>
    private string GridOrden
    {
        get { return Sesion.Lee("Asistencia_GridOrden", ""); }
        set { Sesion.Guarda("Asistencia_GridOrden", value); }
    }
    /// <summary>
    /// obtiene o establece el listado de columnas estaticas separadas por coma del orden aplicado
    /// ej. Campo1,Campo2
    /// </summary>
    private string GridStaticas
    {
        get { return Sesion.Lee("Asistencia_GridStaticas", ""); }
        set { Sesion.Guarda("Asistencia_GridStaticas", value); }
    }

    /// <summary>
    /// obtiene o establece el listado de columnas Agrupadas separadas por coma del orden aplicado
    /// ej. Campo1,Campo2
    /// </summary>
    private string GridAgrupadas
    {
        get { return Sesion.Lee("Asistencia_GridAgrupadas", ""); }
        set { Sesion.Guarda("Asistencia_GridAgrupadas", value); }
    }
    #endregion


    protected void Grid_GroupColumn(object sender, Infragistics.WebUI.UltraWebGrid.ColumnEventArgs e)
    {
        GuardaPropColumnas();
    }
    protected void Grid_UnGroupColumn(object sender, Infragistics.WebUI.UltraWebGrid.ColumnEventArgs e)
    {
        GuardaPropColumnas();
    }
    protected void Grid_ColumnMove(object sender, Infragistics.WebUI.UltraWebGrid.ColumnEventArgs e)
    {
        GuardaPropColumnas();
    }

    protected void Tbx_NoDias_ValueChanged(object sender, Infragistics.Web.UI.EditorControls.TextEditorValueChangedEventArgs e)
    {
        FechaFinal.Value = CeC.Convierte2DateTime(FechaInicial.Value).AddDays(Tbx_NoDias.ValueInt);
        ActualizaDatos(true);
    }

    protected void GridExporter_EndExport(object sender, Infragistics.WebUI.UltraWebGrid.DocumentExport.EndExportEventArgs e)
    {
        if (MenuElmento("CONSUMO_EMPLEADO").Checked)
        {
            CeC_Monedero.MostrarTotales_ConsumoEmpleado(e, Sesion);
        }

        //CeC_ConfigSuscripcion Config = new CeC_ConfigSuscripcion();
        //string ArregloConfig = Config.Mostrar_Subtotales;
        //string[] Elementos = CeC.ObtenArregoSeparador(ArregloConfig, ",", true);
        //// Obtenarreglo separador ObtenUsuarioConfig
        //foreach (string Linea in Elementos)
        //{
        //    //if (MenuElmento("CONSUMO_EMPLEADO").Checked)
        //    if (MenuElmento(Linea).Checked)
        //    {
        //        CeC_Monedero.MostrarTotales_ConsumoEmpleado(e, Sesion);
        //    }
        //}
    }
    public void CargaConsulta(string Tag)
    {
        sCargaConsulta(this, Tag);

        CargaSesion();
        ActualizaDatos(true);
    }
    protected void BtnCancelar0_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        DlgAgregarFav.WindowState = Infragistics.Web.UI.LayoutControls.DialogWindowState.Hidden;
    }
}