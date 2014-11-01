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
using Infragistics.UltraGauge.Resources;
using Infragistics.UltraChart.Shared.Styles;
using Infragistics.UltraChart.Resources.Appearance;
using Infragistics.UltraChart.Core.Layers;
using Infragistics.UltraChart.Data.Series;
using System.Drawing;
using Infragistics.Web.UI.DisplayControls;
using Infragistics.Documents.Reports.Report;
using Infragistics.WebUI.UltraWebGrid.ExcelExport;
using Infragistics.Documents.Reports.Report.Table;
using Infragistics.WebUI.UltraWebGrid;
using Infragistics.Documents.Reports.Report.Text;
using Report = Infragistics.Documents.Reports.Report;
public partial class WF_HorasExtras : System.Web.UI.Page
{
    int Persona_Link_ID = -1;
    int Persona_ID = -1;
    string Agrupacion = "";
    string HorasExtrasAAplicar = "";
    CeC_Sesion Sesion = null;



    string[] VariablesSesion = { "ASHE_ENTRADA_SALIDA", "ASHE_COMIDA",
                                   "ASHE_TOTALES", "ASHE_INCIDENCIA", "ASHE_TURNO",
                                  "ASHE_AGRUPACION", "ASHE_EMPLEADO", "ASHE_CEROS" };

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
        Sesion.Configura.AsistenciaHEFavoritos += Consulta + "|";
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
    public void CargaConsulta(string Tag)
    {
        sCargaConsulta(this, Tag);

        CargaSesion();
        ActualizaDatos(true);
    }
    public void CargaFavoritos()
    {
        Infragistics.WebUI.UltraWebNavigator.Item MenuFavoritos = MenuElmento("FAVORITOS");
        string Favoritos = Sesion.Configura.AsistenciaHEFavoritos;
        string[] sFavoritos = Favoritos.Split(new char[] { '|' });
        foreach (string Favorito in sFavoritos)
        {
            if (MenuElmento(Favorito) == null)
            {
                string[] Elementos = Favorito.Split(new char[] { '@' });
                if (Favorito.Length > 0)
                    MenuFavoritos.Items.Add(Elementos[0], Favorito);
            }
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
    protected void GuardaAs()
    {
        Sesion.ASHE_ENTRADA_SALIDA = MenuElmento("ASHE_ENTRADA_SALIDA").Checked;
        Sesion.ASHE_COMIDA = MenuElmento("ASHE_COMIDA").Checked;
        Sesion.ASHE_TOTALES = MenuElmento("ASHE_TOTALES").Checked;
        Sesion.ASHE_INCIDENCIA = MenuElmento("ASHE_INCIDENCIA").Checked;
        Sesion.ASHE_TURNO = MenuElmento("ASHE_TURNO").Checked;
        Sesion.ASHE_AGRUPACION = MenuElmento("ASHE_AGRUPACION").Checked;
        Sesion.ASHE_EMPLEADO = MenuElmento("ASHE_EMPLEADO").Checked;
        Sesion.ASHE_CEROS = MenuElmento("ASHE_CEROS").Checked;

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
            }

            DS = CeC_AsistenciasHE.ObtenHorasExtras(
                    EstaChecado("ASHE_ENTRADA_SALIDA"),
                    EstaChecado("ASHE_COMIDA"), EstaChecado("ASHE_TOTALES"),
                    EstaChecado("ASHE_INCIDENCIA"), EstaChecado("ASHE_TURNO"),
                    EstaChecado("ASHE_AGRUPACION"), EstaChecado("ASHE_EMPLEADO"),EstaChecado("ASHE_CEROS"),
                    Persona_ID, Agrupacion, DTFechaInicial, DTFechaFinal.AddDays(1), Sesion.USUARIO_ID);

            Grid.DataSource = DS.Tables[0];
            Grid.DataMember = DS.Tables[0].TableName;
            Grid.DataKeyField = "PERSONA_D_HE_ID";

            Grid.DataBind();

            LblHXTrabajar.Text = CeC_Asistencias.ObtenTotalHoras("PERSONA_DIARIO_TES", Persona_ID, Agrupacion, DTFechaInicial, DTFechaFinal.AddDays(1), Sesion.USUARIO_ID);
            LblHTrabajadas.Text = CeC_Asistencias.ObtenTotalHoras("PERSONA_DIARIO_TT", Persona_ID, Agrupacion, DTFechaInicial, DTFechaFinal.AddDays(1), Sesion.USUARIO_ID);
            LblExtra.Text = CeC_AsistenciasHE.ObtenTotalHorasHE("PERSONA_D_HE_SIS", Persona_ID, Agrupacion, DTFechaInicial, DTFechaFinal.AddDays(1), Sesion.USUARIO_ID);
            LblExtraAplicado.Text = CeC_AsistenciasHE.ObtenTotalHorasHE("PERSONA_D_HE_APL", Persona_ID, Agrupacion, DTFechaInicial, DTFechaFinal.AddDays(1), Sesion.USUARIO_ID);
            LblExtraCalculado.Text = CeC_AsistenciasHE.ObtenTotalHorasHE("PERSONA_D_HE_CAL", Persona_ID, Agrupacion, DTFechaInicial, DTFechaFinal.AddDays(1), Sesion.USUARIO_ID);
            LblExtraSimple.Text = CeC_AsistenciasHE.ObtenTotalHorasHE("PERSONA_D_HE_SIMPLE", Persona_ID, Agrupacion, DTFechaInicial, DTFechaFinal.AddDays(1), Sesion.USUARIO_ID);
            LblExtraDoble.Text = CeC_AsistenciasHE.ObtenTotalHorasHE("PERSONA_D_HE_DOBLE", Persona_ID, Agrupacion, DTFechaInicial, DTFechaFinal.AddDays(1), Sesion.USUARIO_ID);
            LblExtraTriple.Text = CeC_AsistenciasHE.ObtenTotalHorasHE("PERSONA_D_HE_TRIPLE", Persona_ID, Agrupacion, DTFechaInicial, DTFechaFinal.AddDays(1), Sesion.USUARIO_ID);

        }
        catch (Exception ex) { CIsLog2.AgregaError(ex); }

    }
    /// <summary>
    /// Carga los valores del menu de acuerdo a las variables de sesion
    /// </summary>
    private void CargaSesion()
    {

        MenuElmento("ASHE_ENTRADA_SALIDA").Checked = Sesion.ASHE_ENTRADA_SALIDA;
        MenuElmento("ASHE_COMIDA").Checked = Sesion.ASHE_COMIDA;
        MenuElmento("ASHE_TOTALES").Checked = Sesion.ASHE_TOTALES;
        MenuElmento("ASHE_INCIDENCIA").Checked = Sesion.ASHE_INCIDENCIA;
        MenuElmento("ASHE_TURNO").Checked = Sesion.ASHE_TURNO;
        MenuElmento("ASHE_AGRUPACION").Checked = Sesion.ASHE_AGRUPACION;
        MenuElmento("ASHE_EMPLEADO").Checked = Sesion.ASHE_EMPLEADO;
        MenuElmento("ASHE_CEROS").Checked = Sesion.ASHE_CEROS;
    }

    protected void FechaFinal_ValueChanged(object sender, Infragistics.WebUI.WebSchedule.WebDateChooser.WebDateChooserEventArgs e)
    {
        ActualizaDatos(true);
    }
    protected void Grid_InitializeDataSource(object sender, Infragistics.WebUI.UltraWebGrid.UltraGridEventArgs e)
    {
        if (IsPostBack)
            Page_Load(null, null);
        ActualizaDatos();
    }
    protected void FechaInicial_ValueChanged(object sender, Infragistics.WebUI.WebSchedule.WebDateChooser.WebDateChooserEventArgs e)
    {
        DateTime DTFechaInicial = Convert.ToDateTime(FechaInicial.Value).Date;
        DateTime DTFechaFinal = Convert.ToDateTime(FechaFinal.Value).Date;

        int Dias = Convert.ToInt32(((TimeSpan)(DTFechaFinal - Sesion.AsistenciaFechaInicio)).TotalDays);
        FechaFinal.Value = DTFechaInicial.AddDays(Dias);
        ActualizaDatos(true);
    }
    protected void Grid_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        try { Grid.Columns.FromKey("PERSONA_DIARIO_ID").Hidden = true; }
        catch { }
        try { Grid.Columns.FromKey("PERSONA_ID").Hidden = true; }
        catch { }
        try { Grid.Columns.FromKey("PERSONA_D_HE_ID").Hidden = true; }
        catch { }
        try { Grid.Columns.FromKey("INCIDENCIA_ABR").Hidden = true; }
        catch { }
        CeC_Grid.AplicaFormato(Grid, true, false, true, true);
        //CeC_Grid.AplicaFormato(Grid);
        try
        {
            Infragistics.WebUI.UltraWebGrid.UltraGridColumn Columna = Grid.Columns.FromKey("PERSONA_D_HE_APL");
            Columna.Format = "###,###,###.#";
        }
        catch { }
    }
    protected void Grid_InitializeRow(object sender, Infragistics.WebUI.UltraWebGrid.RowEventArgs e)
    {
        try
        {
            string Abr = e.Row.Cells.FromKey("INCIDENCIA_ABR").Value.ToString();
            Color Color = CeC_Asistencias.ObtenColor(Abr);
            if (Color != Color.Empty)
            {
                //e.Row.Style.BackgroundImage = "~/Imagenes/PB_Rojo.png";
                //e.Row.Style.
                //                    Grid.DisplayLayout.SelectedRowStyleDefault.BackgroundImage = "Office2003SelRow.gif";
                e.Row.Style.BackColor = Color;
            }
        }
        catch { }
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
                if (TagPadre == "FAVORITOS")
                {
                    if (Tag.Substring(0, 2) == "F_")
                    {
                        if (Tag == "F_AGREGAR")
                            DlgAgregarFav.WindowState = Infragistics.Web.UI.LayoutControls.DialogWindowState.Normal;
                        CeC_Config Config = new CeC_Config(Sesion.USUARIO_ID);
                        if (Tag == "F_SiMPLE")
                            if (Sesion.Parametros == "AGRUPACION")
                                CargaConsulta("@" + Config.AsistenciaHESimpleGrupo);
                            else
                                CargaConsulta("@" + Config.AsistenciaHESimple);
                        if (Tag == "F_DETALLADO")
                            if (Sesion.Parametros == "AGRUPACION")
                                CargaConsulta("@" + Config.AsistenciaHEDetalleGrupo);
                            else
                                CargaConsulta("@" + Config.AsistenciaHEDetalle);
                    }
                    else
                    {
                        CargaConsulta(Tag);
                    }
                    return;
                }
                Muestra(TagPadre);

                if (TagPadre == "EXPORTAR" || TagPadre == "PDF" || TagPadre == "XPS" || TagPadre == "TEXTO")
                {
                    if (Tag == "EXCEL")
                    {
                        Grid.DisplayLayout.Pager.AllowPaging = false;
                        Grid.DisplayLayout.LoadOnDemand = Infragistics.WebUI.UltraWebGrid.LoadOnDemand.NotSet;
                        Grid.DataBind();
                        CeC_Grid.HorasSuma(Grid);
                        GridExcel.WorksheetName = "eClock";
                        GridExcel.DownloadName = "Exportacion.xls";
                        GridExcel.Export(Grid);
                        CIsLog2.AgregaLog("GridExcel.WorksheetName = eClock");
                    }
                    if (Tag == "XPS" || Tag == "XPS_VERTICAL")
                    {
                        GridExporter.Format = Report.FileFormat.XPS;
                        GridExporter.TargetPaperOrientation = Report.PageOrientation.Portrait;
                        GridExporter.DownloadName = "Exportacion.xps";
                    }
                    if (Tag == "XPS_HORIZONTAL")
                    {
                        GridExporter.Format = Report.FileFormat.XPS;
                        GridExporter.TargetPaperOrientation = Report.PageOrientation.Landscape;
                        GridExporter.DownloadName = "Exportacion.xps";
                    }

                    if (Tag == "PDF" || Tag == "PDF_VERTICAL")
                    {
                        GridExporter.Format = Report.FileFormat.PDF;
                        GridExporter.TargetPaperOrientation = Report.PageOrientation.Portrait;
                        GridExporter.DownloadName = "Exportacion.pdf";
                    }
                    if (Tag == "PDF_HORIZONTAL")
                    {
                        GridExporter.Format = Report.FileFormat.PDF;
                        GridExporter.TargetPaperOrientation = Report.PageOrientation.Landscape;
                        GridExporter.DownloadName = "Exportacion.pdf";
                    }

                    if (Tag == "PDF" || TagPadre == "PDF" || Tag == "XPS" || TagPadre == "XPS")
                    {
                        GridExporter.Export(Grid);
                    }
                    if (Tag == "TEXTO" || TagPadre == "TEXTO")
                    {

                        GridExporter.Format = Report.FileFormat.PlainText;
                        if (Tag == "CSV")
                            GridExporter.DownloadName = "Exportacion.txt";
                        else
                            GridExporter.DownloadName = "Exportacion.txt";
                        GridExporter.Export(Grid);
                    }
                    return;
                }
                if (TagPadre == "UsarComo")
                {
                    AplicaHorasExtrasManuales(CeC.Convierte2Int(Tag));
                }

            }
            if (Tag == "UsarMisHoras")
            {
                AplicaHorasExtrasManuales();
            }
            if (Tag == "AceptarHoras")
            {
                AsignaHorasCalculadas();
            }
            if (Tag == "QuitarHoras")
            {
                QuitarHorasAplicadas();
            }
            Muestra(Tag);

            ActualizaDatos(true);
            GuardaAs();
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
    }
    void AplicaHorasExtrasManuales()
    {
        AplicaHorasExtrasManuales(0);
    }

    void AplicaHorasExtrasManuales(int TIPO_INCIDENCIA_ID)
    {
        bool PermitirMasHorasExtras = Sesion.ConfiguraSuscripcion.PermitirMasHorasExtras;
        if (HorasExtrasAAplicar == "")
        {
            LError.Text = "No existen horas extras capturadas";
            return;
        }
        CeC_Asistencias.LimpiaUltimoReporteQryHash(Sesion);
        int TipoIncidenciaHorasExtras = Sesion.ConfiguraSuscripcion.TipoIncidenciaHorasExtras;
        if (TIPO_INCIDENCIA_ID > 0)
            TipoIncidenciaHorasExtras = TIPO_INCIDENCIA_ID;
        string[] Registros = HorasExtrasAAplicar.Split(new char[] { '|' });
        foreach (string Registro in Registros)
        {
            string[] Elementos = Registro.Split(new char[] { '@' });
            if (Elementos.Length == 3)
            {
                int PERSONA_D_HE_ID = Convert.ToInt32(Elementos[0]);

                if (!CeC_TiempoXTiempos.EsHorasExtras(TipoIncidenciaHorasExtras))
                {
                    if (!CeC_IncidenciasRegla.sTieneSaldoDisponible(CeC_AsistenciasHE.ObtenPersonaID(PERSONA_D_HE_ID), TipoIncidenciaHorasExtras, 1))
                    {
                        LError.Text = "No se pudo completar la operación, debido a que no se permitió agregar mas elementos, verifique reglas de incidencias";
                        return;
                    }
                }

                double HorasExtrasAplicadas = Convert.ToDouble(Elementos[1]);
                if (!PermitirMasHorasExtras)
                {
                    double HorasExtrasCalculadas = CeC_BD.DateTime2TimeSpan(CeC_AsistenciasHE.ObtenHorasExtrasCalculadas(PERSONA_D_HE_ID)).TotalHours;
                    if (HorasExtrasAplicadas > HorasExtrasCalculadas)
                        HorasExtrasAplicadas = HorasExtrasCalculadas;

                }
                string Comentarios = Elementos[2].ToString();
                if (Sesion.ConfiguraSuscripcion.TipoIncidenciaHorasExtras == TIPO_INCIDENCIA_ID)
                    TIPO_INCIDENCIA_ID = 0;
                CeC_AsistenciasHE.AsignaHorasExtrasApl(Sesion, PERSONA_D_HE_ID, HorasExtrasAplicadas, TIPO_INCIDENCIA_ID, Comentarios);
            }
        }
        LCorrecto.Text = "Se han asignado horas extras correctamente";
    }
    void Muestra(string Tag)
    {

    }
    protected void Menu_MenuItemChecked(object sender, Infragistics.WebUI.UltraWebNavigator.WebMenuItemCheckedEventArgs e)
    {
        Infragistics.WebUI.UltraWebNavigator.Item Padre = e.Item.Parent;
        if (Padre != null)
            Muestra(Padre.Tag.ToString());
        string Tag = e.Item.Tag.ToString();
        Muestra(Tag);
        ActualizaDatos(true);
        GuardaAs();
    }

    void AsignaHorasCalculadas()
    {
        string Comentario = "";
        if (Sesion.SESION_ID <= 0)
            return;
        CeC_Asistencias.LimpiaUltimoReporteQryHash(Sesion);
        int Numero_Resgistos = Grid.Rows.Count;
        int TipoIncidenciaHorasExtras = Sesion.ConfiguraSuscripcion.TipoIncidenciaHorasExtras;

        for (int i = 0; i < Numero_Resgistos; i++)
        {
            if (Grid.Rows[i].Selected)
            {
                int PERSONA_D_HE_ID = Convert.ToInt32(Grid.Rows[i].DataKey);
                //if (TipoIncidenciaHorasExtras > 0)
                {
                    if (!CeC_IncidenciasRegla.sTieneSaldoDisponible(CeC_AsistenciasHE.ObtenPersonaID(PERSONA_D_HE_ID), TipoIncidenciaHorasExtras, 1))
                    {
                        LError.Text = "No se pudo completar la operación, debido a que no se permitió agregar mas elementos, verifique reglas de incidencias";
                        return;
                    }
                }
                Comentario = ObtenComentario(i);
                CeC_AsistenciasHE.AsignaHorasExtrasApl(Sesion, PERSONA_D_HE_ID, Comentario);
            }
        }
        LCorrecto.Text = "Se han asignado las horas extras calculadas por el sistema";
    }

    void QuitarHorasAplicadas()
    {
        if (Sesion.SESION_ID <= 0)
            return;
        CeC_Asistencias.LimpiaUltimoReporteQryHash(Sesion);
        int Numero_Resgistos = Grid.Rows.Count;
        int TipoIncidenciaHorasExtras = Sesion.ConfiguraSuscripcion.TipoIncidenciaHorasExtras;
        for (int i = 0; i < Numero_Resgistos; i++)
        {
            if (Grid.Rows[i].Selected)
            {
                int PERSONA_D_HE_ID = Convert.ToInt32(Grid.Rows[i].DataKey);
                if (TipoIncidenciaHorasExtras > 0)
                {
                    if (!CeC_IncidenciasRegla.sTieneSaldoDisponible(CeC_AsistenciasHE.ObtenPersonaID(PERSONA_D_HE_ID), TipoIncidenciaHorasExtras, 1))
                    {
                        LError.Text = "No se pudo completar la operación, debido a que no se permitió agregar mas elementos, verifique reglas de incidencias";
                        return;
                    }
                }
                string Comentario = ObtenComentario(i);
                CeC_AsistenciasHE.AsignaHorasExtrasApl(Sesion, PERSONA_D_HE_ID, 0, 0, Comentario);
            }
        }
        LCorrecto.Text = "Se han asignado cero horas extras a los registros seleccionados";
    }
    /// <summary>
    /// Asigna el comentario sobre horas extras
    /// </summary>
    /// <param name="Banda">Fila seleccionada</param>
    /// <returns></returns>
    private string ObtenComentario(int Fila)
    {
        //int Columna;
        string Comentario = "";
        for (int Columna = 0; Columna < Grid.Bands[0].Columns.Count; Columna++)
        {
            if (Grid.Bands[0].Columns[Columna].Header.Caption == "PERSONA_D_HE_COMEN" || Grid.Bands[0].Columns[Columna].Header.Caption == "Comentarios")
            {
                Comentario = Grid.Rows[Fila].Cells[Columna].Text;
            }
        }
        return Comentario;
    }

    protected void btn_Asignar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        AsignaHorasCalculadas();
    }
    protected void btn_Guardar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {

    }
    void CargaTiposIncidencias()
    {
        DataSet DS = Cec_Incidencias.ObtenTiposIncidenciasMenu(Sesion.SUSCRIPCION_ID);
        if (DS == null)
            return;
        Infragistics.WebUI.UltraWebNavigator.Item MenuItem = MenuElmento("UsarComo");
        string TiposIncidencias = CeC.AgregaSeparador(Sesion.ConfiguraSuscripcion.TiposIncidenciasHorasExtras,CeC_TiempoXTiempos.ObtenTiemposXTiempos(Sesion,true,false),",");
        foreach (DataRow DR in DS.Tables[0].Rows)
        {
            string TipoIncidenciaID = CeC.Convierte2String(DR[0]);
            if (CeC.ExisteEnSeparador(TiposIncidencias, TipoIncidenciaID, ","))
                MenuItem.Items.Add(CeC.Convierte2String(DR[1]), TipoIncidenciaID);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
      /*  LCorrecto.Text = "";
        LError.Text = "";*/

        Sesion = CeC_Sesion.Nuevo(this);
        if (Sesion.SESION_ID <= 0)
            return;
        if (Sesion.Parametros == "AGRUPACION")
            Agrupacion = Sesion.eClock_Agrupacion;
        else
        {

            Persona_ID = Sesion.eClock_Persona_ID;
            Persona_Link_ID = CeC_BD.ObtenPersonaLinkID(Persona_ID);
        }
        if (!IsPostBack)
        {


            CargaFavoritos();
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
                LblTurno.Text = CeC_Campos.ObtenValorCampo(Persona_ID, "TURNO_ID");
                if (LblTurno.Text == "" || LblTurno.Text == "No Asignado")
                {
                    LblTurno.ForeColor = Color.Red;
                    LblTurno.Text = "No se ha asignado Turno";
                }
                ImgEmpleado.ImageUrl = "WF_Personas_ImaS.aspx?P=" + Persona_ID;
            }
            FechaInicial.Value = Sesion.AsistenciaFechaInicio;
            FechaFinal.Value = Sesion.AsistenciaFechaFin;
            CargaTiposIncidencias();
        }
        /*        LinearGauge gauge = this.GauAsis.Gauges[0] as LinearGauge;
                gauge.Scales[0].Markers[0].Value = 10;*/
        if (IsPostBack)
        {
            //            return;
        }

    }
    protected void BtnAceptar0_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        GuardaConsulta(Tbx_NombreFav.Text);
        CargaFavoritos();
        DlgAgregarFav.WindowState = Infragistics.Web.UI.LayoutControls.DialogWindowState.Hidden;
    }
    protected void BtnCancelar0_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        DlgAgregarFav.WindowState = Infragistics.Web.UI.LayoutControls.DialogWindowState.Hidden;
    }
    protected void GridExporter_BeginExport(object sender, Infragistics.WebUI.UltraWebGrid.DocumentExport.DocumentExportEventArgs e)
    {
        CeC_Reportes.AplicaFormatoReporte(e, "Asistencias", "imagenes/Asistencias.png", Sesion);
        //   e.Section.
    }
    protected void Grid_UpdateRowBatch(object sender, Infragistics.WebUI.UltraWebGrid.RowEventArgs e)
    {
        try
        {
            Infragistics.WebUI.UltraWebGrid.UltraGridRow Fila = e.Row;
            string HorasExtrasApl = CeC.Convierte2String(Fila.Cells.FromKey("PERSONA_D_HE_APL").Value);
            string Comentario = CeC.Convierte2String(Fila.Cells.FromKey("PERSONA_D_HE_COMEN").Value);
            int ID = Convert.ToInt32(Fila.DataKey);
            HorasExtrasAAplicar += ID + "@" + HorasExtrasApl + "@" + Comentario + "|";
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
    }
    protected void GridExporter_InitializeRow(object sender, Infragistics.WebUI.UltraWebGrid.DocumentExport.DocumentExportInitializeRowEventArgs e)
    {


        // GridExporter.
        //e.Row.
    }
    protected void GridExporter_RowExported(object sender, Infragistics.WebUI.UltraWebGrid.DocumentExport.RowExportedEventArgs e)
    {
        e.ContainingTable.AddRow().AddCell().AddText().Caption = "PRueba";
        //e.ContainingTable.
    }

    protected void GridExporter_RowExporting(object sender, Infragistics.WebUI.UltraWebGrid.DocumentExport.RowExportingEventArgs e)
    {

    }
    protected void GridExporter_FooterRowExported(object sender, Infragistics.WebUI.UltraWebGrid.DocumentExport.MarginRowExportedEventArgs e)
    {

    }
    protected void GridExporter_HeaderRowExported(object sender, Infragistics.WebUI.UltraWebGrid.DocumentExport.MarginRowExportedEventArgs e)
    {

    }
    bool PuedeModificar()
    {
        int Numero_Resgistos = Grid.Rows.Count;
        string PersonasDiarioID = "";

        for (int i = 0; i < Numero_Resgistos; i++)
        {
            if (Grid.Rows[i].Selected)
            {
                int Persona_D_HE_ID = Convert.ToInt32(Grid.Rows[i].DataKey);
                PersonasDiarioID = CeC.AgregaSeparador(PersonasDiarioID, CeC_AsistenciasHE.ObtenPersonaID(Persona_D_HE_ID).ToString(), ",");
            }
        }
        try
        {
            bool Ret = CeC_Periodos.PuedeModificar(PersonasDiarioID);
            if (!Ret)
                LError.Text = "Se ha bloqueado el periodo de algunos elementos, no podran ser modificados";
        }
        catch { }
        return false;
    }
    protected void GridExporter_EndExport(object sender, Infragistics.WebUI.UltraWebGrid.DocumentExport.EndExportEventArgs e)
    {
        try
        {
            Infragistics.Documents.Reports.Report.Section.ISection Seccion = e.Section;
            //
            // Add the band to the section. 
            //
            Infragistics.Documents.Reports.Report.Band.IBand band = Seccion.AddBand();
            //
            // Add a header to the band.
            //
            // Retrieve a reference to the band's header
            // and assign it to the bandHeader object.
            Infragistics.Documents.Reports.Report.Band.IBandHeader bandHeader = band.Header;
            // Cause the header to repeat on every page.
            bandHeader.Repeat = true;
            // The height of the header will be 5% of
            // the page's height. 
            bandHeader.Height = new Infragistics.Documents.Reports.Report.FixedHeight(15);
            // The header's background color will be light blue.
            bandHeader.Background =
              new Infragistics.Documents.Reports.Report.Background
              (Infragistics.Documents.Reports.Graphics.Colors.White);
            // Set the horizontal and vertical alignment of the header.
            bandHeader.Alignment =
              new Infragistics.Documents.Reports.Report.ContentAlignment
              (
                Infragistics.Documents.Reports.Report.Alignment.Left,
                Infragistics.Documents.Reports.Report.Alignment.Bottom
              );
            // The bottom border of the band will be a 
            // solid, dark blue line.
            bandHeader.Borders.Bottom =
              new Infragistics.Documents.Reports.Report.Border
              (Infragistics.Documents.Reports.Graphics.Pens.LightGray);
            // Add 5 pixels of padding around the left and right edges.
            bandHeader.Paddings.Horizontal = 5;
            // Add textual content to the header.
            Infragistics.Documents.Reports.Report.Text.IText bandHeaderText =
              bandHeader.AddText();
            bandHeaderText.Width = new Infragistics.Documents.Reports.Report.FixedWidth(20);
            //bandHeaderText.AddContent("Totales: \n");
            //
            // Add content to the band.     

            Infragistics.Documents.Reports.Report.Text.IText bandText;
            bandText = band.AddText();

            for (int banda = 0; banda < Grid.Bands.Count; banda++)
            {
                for (int columna = 0; columna < Grid.Bands[banda].Columns.Count; columna++)
                {
                    switch (Grid.Bands[banda].Columns[columna].Header.Caption)
                    {
                        case "PERSONA_DIARIO_TDE":
                        case "Tiempo de Retardo":
                        case "PERSONA_DIARIO_TE":
                        case "Tiempo de Estancia":
                        case "PERSONA_DIARIO_TC":
                        case "Tiempo de Comida":
                        case "PERSONA_DIARIO_TES":
                        case "Horas X Trabajar":
                        case "PERSONA_DIARIO_TT":
                        case "Tiempo Trabajado":
                        case "PERSONA_D_HE_SIS":
                        case "HE Reales":
                        case "PERSONA_D_HE_CAL":
                        case "HE Calculadas":
                        case "PERSONA_D_HE_APL":
                        case "HE a Aplicar":
                        case "PERSONA_D_HE_SIS_A":
                        case "PERSONA_D_HE_SIS_D":
                        case "PERSONA_D_HE_FH":
                        case "Confirmacion HE":
                        case "PERSONA_D_HE_SIMPLE":
                        case "HE Simples":
                        case "PERSONA_D_HE_DOBLE":
                        case "HE Dobles":
                        case "PERSONA_D_HE_TRIPLE":
                        case "HE Triples":
                            try
                            {
                                DS_Campos.EC_CAMPOSRow Campo = CeC_Campos.Obten_Campo(Grid.Bands[banda].Columns[columna].Key);
                                if (Campo != null)
                                {
                                    string cabeceraColumna = Grid.Bands[banda].Columns[columna].Header.Caption.ToString();
                                    Grid.Bands[banda].Columns[columna].Header.Caption = Campo.CAMPO_ETIQUETA;
                                    CeC_Grid.HorasSuma(Grid);
                                    bandText.AddContent("Total " + cabeceraColumna + ": " + Grid.Columns[columna].FooterText + "\n");
                                }
                            }
                            catch (Exception ex)
                            {
                                CIsLog2.AgregaError(ex);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

            bandText.Paddings.All = 5;
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError("WF_HorasExtras.GridExporter_EndExport", ex);
        }
    }
}