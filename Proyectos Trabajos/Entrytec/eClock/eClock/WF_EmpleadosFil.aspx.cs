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


/// <summary>
/// Descripción breve de WF_EmpleadosFil.
/// </summary>
public partial class WF_EmpleadosFil : System.Web.UI.Page
{
    public string Sentencia = "SELECT EC_PERSONAS.PERSONA_ID WHERE EC_PERSONAS.PERSONAL_LINK_ID IN(SELECT EC_PERSONAS_DATOS.TRACVE AS PERSONAL_LINK_ID FROM EC_PERSONAS_DATOS ";
    public string CondicionSentencia = "";
    public string ANDS = "";
    protected Infragistics.WebUI.WebDataInput.WebImageButton WebImageButton1;
    public CeC_Sesion Sesion;
    private string ResultadoQueryTerminales = "";
    private string PrefijoCB_CamposFiltro = "CB_CF_";
    private string CamposFiltro = "";
    private string Grupo1Valor = "";
    private string Grupo2Valor = "";
    private string Grupo3Valor = "";


    Infragistics.WebUI.WebDataInput.WebDateTimeEdit WDTE = new Infragistics.WebUI.WebDataInput.WebDateTimeEdit();
    Infragistics.WebUI.WebDataInput.WebDateTimeEdit WDTEH = new Infragistics.WebUI.WebDataInput.WebDateTimeEdit();
    System.Web.UI.WebControls.CheckBox ChkHora = new CheckBox();

    protected string GeneraFiltro()
    {
        if (Sesion.WF_EmpleadosFil_HayFecha > 0)
        {
            Sesion.WF_EmpleadosFil_FechaI = WbCal_FIL_FECHA.Layout.SelectedDate;
            Lbl_Desde.Visible = true;
            if (Sesion.WF_EmpleadosFil_HayFecha == 2)
            {
                Lbl_Hasta.Visible = true;
            }
            Sesion.WF_EmpleadosFil_FechaF = WbCal_H_FIL_FECHA.SelectedDate.Add(new TimeSpan(1, 0, 0, 0));
            if (Sesion.WF_EmpleadosFil_HayHora)
            {
                if (ChkHora.Checked)
                {
                    Sesion.WF_EmpleadosFil_FechaI = Sesion.WF_EmpleadosFil_FechaI.AddHours(WDTE.Date.Hour);
                    Sesion.WF_EmpleadosFil_FechaI = Sesion.WF_EmpleadosFil_FechaI.AddMinutes(WDTE.Date.Minute);
                    Sesion.WF_EmpleadosFil_FechaF = Sesion.WF_EmpleadosFil_FechaF.AddHours(WDTEH.Date.Hour);
                    Sesion.WF_EmpleadosFil_FechaF = Sesion.WF_EmpleadosFil_FechaF.AddMinutes(WDTEH.Date.Minute);
                    Sesion.WF_EmpleadosFil_FechaF = Sesion.WF_EmpleadosFil_FechaF.AddDays(-1);
                }
            }
        }
        string QryResultante = "";
        string Constante = "SELECT EC_PERSONAS.PERSONA_ID FROM EC_PERSONAS, EC_PERSONAS_DATOS WHERE EC_PERSONAS.PERSONA_BORRADO = 0 AND EC_PERSONAS.PERSONA_ID = EC_PERSONAS_DATOS.PERSONA_ID AND EC_PERSONAS.SUSCRIPCION_ID=" + Sesion.SUSCRIPCION_ID + " ";

        if (TablaFiltro.Visible == true)
        {
            string Filtro = "";
            string[] Campos = CamposFiltro.Split(new char[] { ',' });
            foreach (string Campo in Campos)
            {
                try
                {
                    Control Contr = TablaFiltro.FindControl(PrefijoCB_CamposFiltro + Campo);
                    System.Web.UI.WebControls.CheckBox Chk = (System.Web.UI.WebControls.CheckBox)Contr;
                    if (Chk.Checked == true)
                    {
                        string NuevoFiltro = ObtenFiltro(Chk.ID.Substring(PrefijoCB_CamposFiltro.Length));
                        if (Filtro.Length > 0 && NuevoFiltro.Length > 0)
                            Filtro += " AND ";
                        if (NuevoFiltro.Length > 0)
                            Filtro += NuevoFiltro;
                    }
                }
                catch { }
            }
            QryResultante = Constante;
            if (Filtro.Length <= 0)
                return QryResultante;
            QryResultante += " AND " + Filtro;
            return QryResultante;
        }
        else
        {
            QryResultante = Constante;
            if (RbnL_Campos.SelectedIndex == 0)
                QryResultante = Constante + " AND EC_PERSONAS.PERSONA_LINK_ID =" + Wne_NoEmpleado.Value.ToString();
            if (RbnL_Campos.SelectedIndex == 1)
                QryResultante = Constante + " AND EC_PERSONAS.PERSONA_NOMBRE LIKE '%" + Wtx_Nombre.Text.ToUpper() + "%' ";
            if (RbnL_Campos.SelectedIndex == 2)
                QryResultante = Constante + " AND AGRUPACION_NOMBRE like '" + Grupo1Valor + "'";

            //QryResultante = Constante + " AND " + CeC_Config.CampoGrupo3 + " = " + CeC_Campos.ObtenSqlValor(Grupo3Valor);
            return QryResultante;
        }
    }

    private string ObtenFiltro(string NombreCampo)
    {
        string Filtro = "";
        try
        {
            Object ControlCampo = TablaFiltro.FindControl(NombreCampo);
            switch (NombreCampo)
            {
                case "FORMATO_REP_ID":
                    {
                        try
                        {
                            Sesion.WF_EmpleadosFil_FormatoReporte = Convert.ToInt32(CeC_Campos.ObtenValorCampo(ControlCampo));
                        }
                        catch (Exception ex)
                        {
                            Sesion.WF_EmpleadosFil_FormatoReporte = 0;
                        }
                    }
                    break;
                case "TERMINAL_ID":
                    {
                        Filtro = "EC_terminales.terminal_id = " + (CeC_Campos.ObtenValorCampo(ControlCampo).ToString());
                    }
                    break;
                case "TURNO_ID":
                    {
                        Filtro = "EC_PERSONAS.turno_id = " + (CeC_Campos.ObtenValorCampo(ControlCampo).ToString());
                    }
                    break;
                case "FIL_DIAS_N_LAB":
                    {
                        try
                        {
                            bool MuestraDiasNLab = Convert.ToBoolean(CeC_Campos.ObtenValorCampo(ControlCampo));
                            if (MuestraDiasNLab)
                                Sesion.WF_EmpleadosFil_DNL = 1;
                            else
                                Sesion.WF_EmpleadosFil_DNL = 0;
                        }
                        catch (Exception ex)
                        {
                            Sesion.WF_EmpleadosFil_DNL = 0;
                        }
                    }
                    break;
                default:
                    {
                        string sTipoDato = ControlCampo.ToString();

                        if (sTipoDato == CeC_Campos.ObtenTipoControl(CeC_Campos.Tipo_Datos.Texto))
                        {
                            Filtro = "UPPER(" + NombreCampo + ") like '%" + CeC_Campos.ObtenValorCampo(ControlCampo).ToString().ToUpper() + "%'";
                        }
                        else if (sTipoDato == CeC_Campos.ObtenTipoControl(CeC_Campos.Tipo_Datos.Boleano))
                        {
                            Filtro = NombreCampo + " = " + Convert.ToInt32(CeC_Campos.ObtenValorCampo(ControlCampo)) + "";
                        }
                        else
                        {
                            object ValorCampo = CeC_Campos.ObtenValorCampo(ControlCampo);
                            Object ControlCampoHasta = TablaFiltro.FindControl("H_" + NombreCampo);
                            Object ControlCampoHastaValor = CeC_Campos.ObtenValorCampo(ControlCampoHasta);
                            if (ControlCampoHasta != null && ControlCampoHastaValor != null && ControlCampoHastaValor.ToString() != "0")
                            {
                                Filtro = NombreCampo + " >= " + CeC_Campos.ObtenSqlValor(ValorCampo);
                                Filtro += " AND " + NombreCampo + " <= " + CeC_Campos.ObtenSqlValor(ControlCampoHastaValor);
                            }
                            else
                            {
                                Filtro = NombreCampo + " = " + CeC_Campos.ObtenSqlValor(ValorCampo);
                            }
                        }
                    }
                    break;
            }
        }
        catch
        {
        }
        return Filtro;
    }

    private string NombreCB(string NombreCampo)
    {
        return PrefijoCB_CamposFiltro + NombreCampo;
    }

    protected void ponhora()
    {
        TableRow Fila = new TableRow();

        TableCell Cell1 = new TableCell();
        TableCell Cell2 = new TableCell();

        WDTE.Value = DateTime.Now;
        WDTE.EditModeFormat = "HH:mm";
        WDTE.DisplayModeFormat = "HH:mm";
        WDTE.Visible = true;

        WDTEH.Value = DateTime.Now;
        WDTEH.EditModeFormat = "HH:mm";
        WDTEH.DisplayModeFormat = "HH:mm";
        WDTEH.Visible = true;
        ChkHora.ID = PrefijoCB_CamposFiltro + "Hora";
        ChkHora.Text = "Hora (Desde - Hasta)";
        Cell1.Controls.Add(ChkHora);
        Fila.Cells.Add(Cell1);
        Cell1.HorizontalAlign = HorizontalAlign.Left;
        Cell2.Controls.Add(WDTE);
        Cell2.HorizontalAlign = HorizontalAlign.Left;

        Fila.Cells.Add(Cell2);
        TableCell Cell3 = new TableCell();
        Cell3.HorizontalAlign = HorizontalAlign.Left;
        Cell3.Controls.Add(WDTEH);
        Fila.Cells.Add(Cell3);
        TablaFiltro.Rows.Add(Fila);
    }

    protected bool AgregaCampo(string NombreCampo, bool Hasta, bool Obligatorio)
    {
        try
        {
            if (CamposFiltro.Length > 0)
                CamposFiltro += ",";
            CamposFiltro += NombreCampo;
            object Obj = CeC_Campos.CreaCampo(NombreCampo, -9998);
            if (Obj != null)
            {

                ((System.Web.UI.WebControls.WebControl)Obj).Width = Unit.Pixel(200);
                TableRow Fila = new TableRow();
                TableCell Cell1 = new TableCell();
                TableCell Cell2 = new TableCell();
                System.Web.UI.WebControls.CheckBox Chk = new CheckBox();
                Chk.Text = CeC_Campos.ObtenEtiqueta(NombreCampo);
                if (Obligatorio)
                {
                    Chk.Checked = true;
                    Chk.Enabled = false;
                }
                Chk.ID = PrefijoCB_CamposFiltro + NombreCampo;
                Cell1.Controls.Add(Chk);
                Fila.Cells.Add(Cell1);
                Cell1.HorizontalAlign = HorizontalAlign.Left;
                Cell2.HorizontalAlign = HorizontalAlign.Left;
                Cell2.Controls.Add((System.Web.UI.Control)Obj);
                Fila.Cells.Add(Cell2);
                string sTipoDato = Obj.GetType().ToString();
                if (sTipoDato != CeC_Campos.ObtenTipoControl(CeC_Campos.Tipo_Datos.Texto)
                    && sTipoDato != CeC_Campos.ObtenTipoControl(CeC_Campos.Tipo_Datos.Boleano)
                    && Hasta)
                {
                    object ObjH = CeC_Campos.CreaCampo(NombreCampo, -1);
                    ((System.Web.UI.WebControls.WebControl)ObjH).ID = "H_" + NombreCampo;
                    ((System.Web.UI.WebControls.WebControl)ObjH).Width = Unit.Pixel(200);
                    TableCell Cell3 = new TableCell();
                    Cell3.HorizontalAlign = HorizontalAlign.Left;
                    Cell3.Controls.Add((System.Web.UI.Control)ObjH);
                    Fila.Cells.Add(Cell3);
                }
                TablaFiltro.Rows.Add(Fila);

                return true;
            }
        }
        catch
        {
        }
        return false;
    }

    protected void CargaFiltros()
    {
        CeC_Campos.Inicializa();

        TableRow Fila = new TableRow();
        TableCell Cell1 = new TableCell();
        TableCell Cell2 = new TableCell();

        TableCell Cell3 = new TableCell();
        Cell1.HorizontalAlign = HorizontalAlign.Left;
        Cell2.HorizontalAlign = HorizontalAlign.Left;
        Cell3.HorizontalAlign = HorizontalAlign.Left;
        Cell1.Text = "Filtro por:";
        Cell2.Text = "Desde / Contiene";
        Cell3.Text = "Hasta";

        Fila.Cells.Add(Cell1);
        Fila.Cells.Add(Cell2);
        Fila.Cells.Add(Cell3);
        TablaFiltro.Rows.Add(Fila);

        if (Sesion.WF_EmpleadosFil_HayFecha > 0)
        {
            //  LDesde.Visible = true;
            WbCal_FIL_FECHA.Visible = true;
        }
        if (Sesion.WF_EmpleadosFil_HayFecha == 2)
        {
            //  LHasta.Visible = true;
            WbCal_H_FIL_FECHA.Visible = true;
        }

        //   AgregaCampo("FIL_FECHA", true, true);

        if (Sesion.WF_EmpleadosFil_HayEmpleados)
        {
            string[] Campos = CeC_Campos.ObtenListaCamposTEFiltro().Split(new Char[] { ',' });
            foreach (string Campo in Campos)
            {
                string NombreCampo = Campo.Trim();
                AgregaCampo(NombreCampo, true, false);
            }
        }
        else
        {
            WebGroupBox1.Visible = false;
            RbnL_Busqueda.Visible = false;
            Wpn_Busqueda_.Visible = false;
        }


        AgregaCampo("PERSONA_EMAIL", false, false);
        AgregaCampo("TURNO_ID", false, false);

        if (Sesion.WF_EmpleadosFil_HayTerminales)
            AgregaCampo("TERMINAL_ID", false, false);
        /*        if (Sesion.WF_EmpleadosFil_HayTerminales)
                    AgregaCampo("TERMINAL_ID", false, false);*/
        if (Sesion.WF_EmpleadosFil_HayHora)
            ponhora();
        if (!CeC_Config.AsistenciaNoLaborable && Sesion.WF_EmpleadosFil_HayDiasLaborables)
            AgregaCampo("FIL_DIAS_N_LAB", false, true);
        if (Sesion.WF_EmpleadosFil_HayExportacion)
            AgregaCampo("FORMATO_REP_ID", false, true);
        //            AgregaCampo("PERSONA_BORRADO", Persona_ID);
    }
    protected void CargaOpciones()
    {
        if (Sesion.WF_EmpleadosFil_Opciones.Length > 0)
        {
            //BMostrarReporte.Visible = false;
            WImgV_OptImagen.Visible = true;
            //
            foreach (string Opcion in Sesion.WF_EmpleadosFil_Opciones.Split(new Char[] { '@' }))
            {
                string[] Parametros = Opcion.Split(new Char[] { '|' });
                if (Parametros.Length > 3)
                {
                    Infragistics.Web.UI.ListControls.ImageItem II = new Infragistics.Web.UI.ListControls.ImageItem();
                    II.ImageUrl = "EmpleadosFill/" + Parametros[0];
                    II.Key = Parametros[1];
                    II.AltText = Parametros[2];
                    II.ToolTip = Parametros[3];
                    WImgV_OptImagen.Items.Add(II);
                }
            }
        }

    }

    protected void Page_Load(object sender, System.EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        //Titulo y Descripcion
        Sesion.TituloPagina = Sesion.TituloPagina;
        Sesion.DescripcionPagina = "Introduzca la Datos Conocidos de los Empleados que requiera obtener, no olvide seleccionar los campos para aplicar la busqueda apropiada";
        //Titulo y Descripcion Fin

        Sesion.ControlaBoton(ref WIBtn_MostrarReporte);
        Sesion.ControlaBoton(ref WIBtn_RestablecerValores);
        //Codigo para poner mensaje en botones *****
        CargaOpciones();
        if (!IsPostBack)
        {
            RbnL_Busqueda.SelectedIndex = 0;
            WbCal_FIL_FECHA.SelectedDate = (System.DateTime.Today);
            WbCal_H_FIL_FECHA.SelectedDate = (System.DateTime.Today);
        }
        RbnL_Campos.Items.FindByValue("Todos Los Empleados").Selected = true;


        //}

        //     CeC_Grid.AplicaFormato(WCGrupo);
        RbnL_Campos.Items[0].Text = CeC_Campos.ObtenEtiqueta(CeC_Campos.CampoTE_Llave);

        CargaFiltros();
        //CeC_Grid.AplicaFormato(WCGrupo);
        return;
    }

    #region Código generado por el Diseñador de Web Forms
    override protected void OnInit(EventArgs e)
    {
        //
        // CODEGEN: llamada requerida por el Diseñador de Web Forms ASP.NET.
        //
        InitializeComponent();
        base.OnInit(e);
    }

    /// <summary>
    /// Método necesario para admitir el Diseñador. No se puede modificar
    /// el contenido del método con el editor de código.
    /// </summary>
    private void InitializeComponent()
    {
        System.Configuration.AppSettingsReader configurationAppSettings = new System.Configuration.AppSettingsReader();
        this.WIBtn_RestablecerValores.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.WIBtn_RestablecerValores_Click);
        this.WIBtn_MostrarReporte.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.WIBtn_MostrarReporte_Click);

    }
    #endregion

    protected void Wco_Grupo_InitializeDataSource(object sender, Infragistics.WebUI.WebCombo.WebComboEventArgs e)
    {
        try { Grupo1Valor = Wco_Grupo.DisplayValue.ToString(); }
        catch { }
        try
        {
            DataSet DS = CeC_Agrupaciones.ObtenAgrupaciones(Sesion.USUARIO_ID);
            Wco_Grupo.DataSource = DS;
            //   Combo.DataMember = DS.Tables[0].TableName;

            Wco_Grupo.DataTextField = DS.Tables[0].Columns[0].ColumnName;
            //Combo.DisplayValue = DS.Tables[0].Columns[0].ColumnName;
            Wco_Grupo.DataValueField = DS.Tables[0].Columns[0].ColumnName;
            Wco_Grupo.DataBind();
        }
        catch { }
    }

    protected void WCGrupo_SelectedRowChanged(object sender, Infragistics.WebUI.WebCombo.SelectedRowChangedEventArgs e)
    {

    }
    protected void RbnL_Busqueda_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RbnL_Busqueda.SelectedIndex == 0)
        {
            TablaFiltro.Visible = false;
            WebGroupBox1.Visible = true;
        }
        else
        {
            TablaFiltro.Visible = true;
            WebGroupBox1.Visible = false;
        }
    }
    protected void WIBtn_MostrarReporte_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        if (RbnL_Busqueda.SelectedIndex == 0 && (((RbnL_Campos.SelectedIndex == 0) && (Wne_NoEmpleado.Text == "")) || ((RbnL_Campos.SelectedIndex == 2) && (Grupo1Valor.Length < 0))))
        {
            Lbl_Error.Text = "Debe especificar un criterio de busqueda";
        }
        else
        {
            string QueryGrupo = "";
            if (!Sesion.TienePermiso(eClock.CEC_RESTRICCIONES.S0Empleados0Listado))
            {
                if (Sesion.TienePermiso(eClock.CEC_RESTRICCIONES.S0Empleados0Listado0Grupo) || Sesion.TienePermiso(eClock.CEC_RESTRICCIONES.S0Reportes0Reportes_Empleados0Grupo))
                {
                    QueryGrupo = " AND (EC_PERSONAS.SUSCRIPCION_ID IN (SELECT EC_PERMISOS_SUSCRIP.SUSCRIPCION_ID FROM EC_PERMISOS_SUSCRIP WHERE EC_PERMISOS_SUSCRIP.USUARIO_ID = " + Sesion.USUARIO_ID + "))";
                }
            }
            Sesion.WF_EmpleadosFil_Qry = GeneraFiltro() + QueryGrupo;
            /*  if (Sesion.WF_EmpleadosFil_Qry == "SELECT EC_PERSONAS.PERSONA_ID FROM EC_PERSONAS, EC_PERSONAS_DATOS WHERE EC_PERSONAS.PERSONA_LINK_ID = EC_PERSONAS_DATOS." + CeC_Campos.CampoTE_Llave + QueryGrupo && RadioButtonList1.SelectedIndex == 1)
              {
                  LError.Text = "Debe especificar un criterio de busqueda";
              }
              else*/
            {
                Sesion.WF_EmpleadosFil_QryInformacion = Sesion.ExaminarQuery(Sesion.WF_EmpleadosFil_Qry);
                if (Sesion.WF_EmpleadosFil_HayFecha > 0 && !Sesion.WF_EmpleadosFil_HayHora)
                {
                    if (Sesion.WF_EmpleadosFil_HayFecha == 2)
                        Sesion.WF_EmpleadosFil_QryInformacion = Sesion.WF_EmpleadosFil_QryInformacion + "  Desde: " + Sesion.WF_EmpleadosFil_FechaI.ToString("dd/MM/yyyy") + " Hasta: " + Sesion.WF_EmpleadosFil_FechaF.AddDays(-1).ToString("dd/MM/yyyy");
                    if (Sesion.WF_EmpleadosFil_HayFecha == 1)
                        Sesion.WF_EmpleadosFil_QryInformacion = Sesion.WF_EmpleadosFil_QryInformacion + "  Desde: " + Sesion.WF_EmpleadosFil_FechaI.ToString("dd/MM/yyyy");
                }
                if (Sesion.WF_EmpleadosFil_HayHora && ChkHora.Checked)
                {
                    Sesion.WF_EmpleadosFil_QryInformacion = Sesion.ExaminarQuery(Sesion.WF_EmpleadosFil_Qry) + " Desde: " + Sesion.WF_EmpleadosFil_FechaI.ToString("dd/MM/yyyy hh:mm") + " Hasta: " + Sesion.WF_EmpleadosFil_FechaF.ToString("dd/MM/yyyy hh:mm");
                }
                Sesion.WF_EmpleadosFil_QryInformacion = Sesion.WF_EmpleadosFil_QryInformacion.Replace("donde PERSONAS.Inactivo = 0 y   Desde:", "Desde:");

                if (WImgV_OptImagen.Visible)
                {
                    Sesion.Redirige(WImgV_OptImagen.SelectedItem.Key);
                }
                else
                    Sesion.Redirige(Sesion.WF_EmpleadosFil_LINK);
            }
        }
        return;
    }
    protected void WIBtn_RestablecerValores_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {

    }
    protected void RbnL_Campos_SelectedIndexChanged(object sender, EventArgs e)
    {/*
        switch (RBCampos.SelectedIndex)
        {
            case 0:
                txtNombre.Enabled = false;
                WCGrupo.Enabled = false;
                txtNoEmpleado.Enabled= true;
                break;
            case 1:
                txtNombre.Enabled = true;
                WCGrupo.Enabled = false;
                txtNoEmpleado.Enabled = false;
                break;
            case 2:
                txtNombre.Enabled = false;
                WCGrupo.Enabled = true;
                txtNoEmpleado.Enabled = false;
                break;
            case 3:
                txtNombre.Enabled = false;
                WCGrupo.Enabled = false;
                txtNoEmpleado.Enabled = false;
                break;
        }
       */
    }
    protected void Wco_Grupo_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        CeC_Grid.AplicaFormato(Wco_Grupo);
    }
}