using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.AccesoDatos.Catalogos;
using PAEEEM.Entidades.ModuloCentral;
using PAEEEM.Helpers;
using PAEEEM.LogicaNegocios.LOG;
using PAEEEM.LogicaNegocios.ModuloCentral;
using PAEEEM.LogicaNegocios.SolicitudCredito;

namespace PAEEEM.CentralModule
{
    public partial class AdministradorPlantillasProd : Page
    {
        #region Propiedades

        public int IdPlantilla
        {
            set { ViewState["IdPlantilla"] = value; }
            
            get { return (int)ViewState["IdPlantilla"]; }
        }

        public Comp_Plantilla Plantilla
        {
            set
            {
                ViewState["Plantilla"] = value;
            }
            get
            {
                return (Comp_Plantilla)ViewState["Plantilla"];
            }
        }

        public List<Campo_Base_Plantilla> LstCamposBase
        {
            set
            {
                ViewState["LstCamposBase"] = value;
            }
            get
            {
                return (List<Campo_Base_Plantilla>)ViewState["LstCamposBase"];
            }
        }

        public int [] CamposBaseReporte
        {
            set
            {
                ViewState["CamposBaseReporte"] = value;
            }
            get
            {
                return (int[])ViewState["CamposBaseReporte"];
            }
        }
        

        public Campo_Customizable Customizable
        {
            set
            {
                ViewState["Customizable"] = value;
            }
            get
            {
                return (Campo_Customizable)ViewState["Customizable"];
            }
        }

        public string NombrePlantilla
        {
            get { return ViewState["NombrePlantilla"] == null ? "" : ViewState["NombrePlantilla"].ToString(); }
            set { ViewState["NombrePlantilla"] = value; }
        }

        public Campo_Customizable_Plantilla CustomizablePlantilla
        {
            set
            {
                ViewState["CustomizablePlantilla"] = value;
            }
            get
            {
                return (Campo_Customizable_Plantilla)ViewState["CustomizablePlantilla"];
            }
        }

        public List<Campo_Customizable> LstCustomizables
        {
            set
            {
                ViewState["LstCustomizables"] = value;
            }
            get
            {
                return (List<Campo_Customizable>)ViewState["LstCustomizables"];
            }
        }

        public List<Campo_Customizable_Plantilla> LstCustomizablesPlantilla
        {
            set
            {
                ViewState["LstCustomizablesPlantilla"] = value;
            }
            get
            {
                return (List<Campo_Customizable_Plantilla>)ViewState["LstCustomizablesPlantilla"];
            }
        }

        public bool ValidaGrid
        {
            set 
            {
                ViewState["ValidaGrid"] = value;
            }
            get
            {
                return (bool)ViewState["ValidaGrid"];
            }
        }

        #endregion

        #region Carga Inicial
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (null == Session["UserInfo"])
                    {
                        Response.Redirect("../Login/Login.aspx");
                        return;
                    }
                    //trSistemaArreglo.Visible = false;
                    LlenarCatalogoClase();
                    //LlenarCatalogoTipo();
                    LlenatCatalogoTipoProducto();
                    //AgregaNuevaPlantilla();
                    //IdPlantilla = 4;
                    //CargaDatosPlantilla();
                    CargaPlantillasXTecnologia();
                    ImgBtnHome.Visible = !containerLiteral.Visible;
                }
                catch (Exception ex)
                {
                    PanelPlantillasTecnologia.Visible =
                        PanelPlantillaComun.Visible = PanelPlantillaBC.Visible = containerSE.Visible = false;
                    ScriptManager.RegisterClientScriptBlock
                        (UpdatePanel1, typeof (Page), "Error", "alert('" + ex.Message + "')", true);
                }
            }
            else
            {
                ImgBtnHome.Visible = true;
                lblTitle.Visible = false;
            }
        }

        #endregion

        #region Eventos

        protected void BtnAgregaCampoGrid_Click(object sender, EventArgs e)
        {
            grdCamposCustomizables.DataSource = null;
            grdCamposCustomizables.DataBind();

            //PanelEdicionPlantilla.Visible = false;
            OcultaPlantillas();
            PanelCamposCustomizables.Visible = true;
        }

        protected void BtnCreaNuevo_Click(object sender, EventArgs e)
        {
            PanelCamposCustomizables.Visible = false;

            TxtCveCatalogo.Text = "";
            TxtNomCatalogo.Text = "";
            TxtDescCatalogo.Text = "";
            TxtToolTip.Text = "";
            trValores.Visible = true;
            RadioListValoresCat.Items.Clear();

            TxtClaveCampoTexto.Text = "";
            TxtNombreCampoTexto.Text = "";
            TxtDescripcionCampoTexto.Text = "";
            TxtToolTipTexto.Text = "";

            if (RBlstTipoCampo.Items[0].Selected)
            {
                
                PanelCampoTexto.Visible = true;
                BtnGuardarCampoTexto.Visible = true;
                BtnAgregarAPlantilla2.Visible = false;
            }
            if (RBlstTipoCampo.Items[1].Selected)
            {
                PanelCampoCatalogo.Visible = true;
                BtnGuardarCatalogo.Visible = true;
                BtnAgregarAPlantilla.Visible = false;
                RadioListValoresCat.Items.Clear();
            }
        }

        protected void BtnBuscarCampo_Click(object sender, EventArgs e)
        {
            PanelCamposCustomizables.Visible = false;
            PanelBusquedaCamposCustomizables.Visible = true;
        }

        protected void BtnAgregarValor_Click(object sender, EventArgs e)
        {
            if (TxtValorCatalogo.Text != "")
            {
                RadioListValoresCat.Items.Add(new ListItem(TxtValorCatalogo.Text));
                TxtValorCatalogo.Text = "";

                ImgEliminar.Visible = true;
                TxtNuevoNombreCat.Visible = true;
                BtnActualizaCat.Visible = true;
            }
        }

        protected void BtnGuardarCatalogo_Click(object sender, EventArgs e)
        {
            Page.Validate();

            if (Page.IsValid)
            {
                InsertaCatalogoCustomizable();
                ImgEliminar.Visible = false;
                TxtNuevoNombreCat.Visible = false;
                BtnActualizaCat.Visible = false;
            }
        }

        protected void BtnGuardarCampoTexto_Click(object sender, EventArgs e)
        {
            Page.Validate();

            if (Page.IsValid)
            {
                if(TxtCveCatalogo.Text == "")
                    InsertaCampoTextoCustomizable();
                else
                    ActualizaCatCustomizable();
            }
        }

        protected void BtnBusquedaCampos_Click(object sender, EventArgs e)
        {
            BuscaCustomizables();
        }

        protected void BtnSelectCustom_Click(object sender, EventArgs e)
        {
            var checkseleccionados = 0;
            var cveCampoSelec = 0;

            for (int i = 0; i < grdBuscaCamposCustomizables.Rows.Count; i++)
            {
                var ckbSelect = grdBuscaCamposCustomizables.Rows[i].FindControl("ckbSelect") as CheckBox;

                if (ckbSelect != null && ckbSelect.Checked)
                {
                    var dataKey = grdBuscaCamposCustomizables.DataKeys[i];
                    if (dataKey != null)
                        cveCampoSelec = int.Parse(dataKey[0].ToString());
                    checkseleccionados++;
                }
            }

            if (checkseleccionados == 0)
            {
                ScriptManager.RegisterClientScriptBlock
                            (UpdatePanel1, typeof(Page), "Advertencia", "alert('Debe seleccionar al menos un registro')", true);
            }

            else if (checkseleccionados > 1)
            {
                ScriptManager.RegisterClientScriptBlock
                            (UpdatePanel1, typeof(Page), "Advertencia", "alert('Debe seleccionar solo un registro')", true);
            }

            else
            {
                Customizable = null;
                Customizable = LstCustomizables.FirstOrDefault(me => me.CveCampo == cveCampoSelec);

                if (Customizable != null)
                {
                    PanelBusquedaCamposCustomizables.Visible = false;
                    grdBuscaCamposCustomizables.DataSource = null;
                    grdBuscaCamposCustomizables.DataBind();

                    if (Customizable.DxTipo == "Cat")
                    {
                        CargaDatosCatalogo();
                        BtnAgregarAPlantilla.Visible = true;
                        BtnGuardarCatalogo.Visible = true;
                        PanelCampoCatalogo.Visible = true;
                    }

                    if (Customizable.DxTipo == "Text")
                    {
                        CargaDatosCampoTexto();
                        BtnGuardarCampoTexto.Visible = true;
                        BtnAgregarAPlantilla2.Visible = true;
                        PanelCampoTexto.Visible = true;
                    }

                }
            }
        }

        protected void BtnAgregarAPlantilla_Click(object sender, EventArgs e)
        {
            AgregarCampoAPlantilla();
            TxtCveCatalogo.Text = "";
            TxtNomCatalogo.Text = "";
            TxtDescCatalogo.Text = "";
            TxtToolTip.Text = "";
            PanelCampoCatalogo.Visible = false;
            MuestraPlantilla();
            //PanelEdicionPlantilla.Visible = true;
        }

        protected void BtnAgregarAPlantilla2_Click(object sender, EventArgs e)
        {
            AgregarCampoAPlantilla();
            TxtClaveCampoTexto.Text = "";
            TxtNombreCampoTexto.Text = "";
            TxtDescripcionCampoTexto.Text = "";
            TxtToolTipTexto.Text = "";
            PanelCampoTexto.Visible = false;
            MuestraPlantilla();
            //PanelEdicionPlantilla.Visible = true;
        }

        protected void BtnImgEliminar_Click(object sender, ImageClickEventArgs e)
        {
            var gridViewRow = (GridViewRow)((ImageButton)sender).NamingContainer;
            var dataKey = grdCamposCustomizables.DataKeys[gridViewRow.RowIndex];
            if (dataKey != null)
            {
                var cveCampoSelec = (int)dataKey[0];

                var campoEliminar = Plantilla.LstCamposCustomizables.FirstOrDefault(me => me.CveCampo == cveCampoSelec);

                var eliminado =
                    LogicaNegocios.ModuloCentral.CatalogosPlanilla.ClassInstance.EliminaCampoPlantilla(campoEliminar);

                if (eliminado)
                {
                    /*INSERTAR EVENTO CAMBIOS DEL PROCESO PLANTILLAS EN LOG*/
                    if (campoEliminar != null)
                        Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                            Convert.ToInt16(Session["IdRolUserLogueado"]), Convert.ToInt16(Session["IdDepartamento"]),
                            "PLANTILLAS", "CAMBIOS",
                            Plantilla.DxDescripcion, "",
                            "motivoEliminarcampo??", "", "Eliminó de la plantilla el campo : " + campoEliminar.DxNombreCampo);

                    CargaGridSustomizablesPlantilla();
                    LblNomPlantilla.Text = Plantilla.DxDescripcion;
                }
                else
                {
                    var updatePanel1 = UpdatePanel1;
                    if (updatePanel1 != null)
                        ScriptManager.RegisterClientScriptBlock
                            (updatePanel1, typeof (Page), "Error",
                                "alert('No se pudo eliminar el campo de la Plantilla')", true);
                }
            }
        }

        protected void ckbSelect_CheckedChanged(object sender, EventArgs e)
        {
            var gridViewRow = (GridViewRow)((CheckBox)sender).NamingContainer;
            var dataKey = grdCamposCustomizables.DataKeys[gridViewRow.RowIndex];
            if (dataKey != null)
            {
                var cveCampoSelec = (int)dataKey[0];

                var campoEditar = Plantilla.LstCamposCustomizables.FirstOrDefault(me => me.CveCampo == cveCampoSelec);
                if (campoEditar != null)
                {
                    campoEditar.CveAgregarReporte = ((CheckBox)sender).Checked ? 1 : 0;

                    var editado =
                        LogicaNegocios.ModuloCentral.CatalogosPlanilla.ClassInstance.ActualizaCampoPlantilla(campoEditar);

                    if (editado)
                    {
                        CargaGridSustomizablesPlantilla();
                        LblNomPlantilla.Text = Plantilla.DxDescripcion;
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock
                            (UpdatePanel1, typeof(Page), "Error", "alert('No se pudo actualizar el campo de la Plantilla')", true);
                    }
                }
            }
        }

        protected void BtnGuardarPlantilla_Click(object sender, EventArgs e)
        {
            try
            {
                Page.Validate();

                if (Page.IsValid)
                {
                    
                    if (Plantilla.DxDescripcion != "PLANTILLA SE")
                    {
                        if (Plantilla.DxDescripcion == "PLANTILLA BC")
                        {
                            var a = 0;
                            var tipoProducto = DDXTipoProductoBC.SelectedIndex != 0
                                ? DDXTipoProductoBC.SelectedValue
                                : "27";

                            foreach (var campoBasePlantilla in LstCamposBase.FindAll(me => me.Cve_Adicional3 == tipoProducto))
                            {

                                var checkbox = (CheckBox)PanelPlantillaBC.FindControl(campoBasePlantilla.Dx_Adicional2);
                                campoBasePlantilla.Cve_Agregar_Reporte = checkbox.Checked ? 1 : 0;

                                LogicaNegocios.ModuloCentral.CatalogosPlanilla.ActualizaBasePlantilla(campoBasePlantilla);

                                if (!CamposBaseReporte[a].Equals(campoBasePlantilla.Cve_Agregar_Reporte))
                                {
                                    /*INSERTAR EVENTO CAMBIOS DEL PROCESO PLANTILLA EN LOG*/
                                    Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                                        Convert.ToInt16(Session["IdRolUserLogueado"]),
                                        Convert.ToInt16(Session["IdDepartamento"]), "PLANTILLAS", "CAMBIOS",
                                        Plantilla.DxDescripcion.ToString(CultureInfo.InvariantCulture), "MOTIVOS??", "",
                                        CamposBaseReporte[a].ToString(CultureInfo.InvariantCulture) == "0"
                                            ? "Quitó del reporte" + campoBasePlantilla.Dx_Campo_Base
                                            : "Agregó a reporte" + campoBasePlantilla.Dx_Campo_Base,
                                        campoBasePlantilla.Cve_Agregar_Reporte == 0
                                            ? "Quitó del reporte" + campoBasePlantilla.Dx_Campo_Base
                                            : "Agregó a reporte" + campoBasePlantilla.Dx_Campo_Base);
                                }
                                a++;
                            }
                        }

                        else
                        {
                            var a = 0;
                            foreach (var campoBasePlantilla in LstCamposBase)
                            {

                                var checkbox = (CheckBox)PanelPlantillaComun.FindControl(campoBasePlantilla.Dx_Adicional2);
                                campoBasePlantilla.Cve_Agregar_Reporte = checkbox.Checked ? 1 : 0;

                                LogicaNegocios.ModuloCentral.CatalogosPlanilla.ActualizaBasePlantilla(campoBasePlantilla);

                                if (!CamposBaseReporte[a].Equals(campoBasePlantilla.Cve_Agregar_Reporte))
                                {
                                    /*INSERTAR EVENTO CAMBIOS DEL PROCESO PLANTILLA EN LOG*/
                                    Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                                        Convert.ToInt16(Session["IdRolUserLogueado"]),
                                        Convert.ToInt16(Session["IdDepartamento"]), "PLANTILLAS", "CAMBIOS",
                                        Plantilla.DxDescripcion.ToString(CultureInfo.InvariantCulture), "MOTIVOS??", "",
                                        CamposBaseReporte[a].ToString(CultureInfo.InvariantCulture) == "0"
                                            ? "Quito del reporte" + campoBasePlantilla.Dx_Campo_Base
                                            : "Agrego a Reporte" + campoBasePlantilla.Dx_Campo_Base,
                                        campoBasePlantilla.Cve_Agregar_Reporte == 0
                                            ? "Quito del reporte" + campoBasePlantilla.Dx_Campo_Base
                                            : "Agrego a Reporte" + campoBasePlantilla.Dx_Campo_Base);
                                }
                                a++;
                            }
                        }
                    }

                    else
                    {
                        var a = 0;
                        var clase = DDXClaseSE.SelectedValue != "" ? int.Parse(DDXClaseSE.SelectedValue) : 1;
                        var tipo = DDXTipoSE.SelectedValue != "" ? int.Parse(DDXTipoSE.SelectedValue) : 2;

                        foreach (var campoBasePlantilla in LstCamposBase.FindAll(me => me.Cve_Adicional1 == clase && me.Cve_Adicional2 == tipo))
                        {
                            var checkbox = (CheckBox)containerSE.FindControl(campoBasePlantilla.Dx_Adicional2);
                            campoBasePlantilla.Cve_Agregar_Reporte = checkbox.Checked ? 1 : 0;

                            LogicaNegocios.ModuloCentral.CatalogosPlanilla.ActualizaBasePlantilla(campoBasePlantilla);

                            if (!CamposBaseReporte[a].Equals(campoBasePlantilla.Cve_Agregar_Reporte))
                            {
                                /*INSERTAR EVENTO CAMBIOS DEL PROCESO PLANTILLA EN LOG*/
                                Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                                    Convert.ToInt16(Session["IdRolUserLogueado"]),
                                    Convert.ToInt16(Session["IdDepartamento"]), "PLANTILLAS", "CAMBIOS",
                                    Plantilla.DxDescripcion.ToString(CultureInfo.InvariantCulture), "MOTIVOS??", "",
                                    CamposBaseReporte[a].ToString(CultureInfo.InvariantCulture) == "0"
                                        ? "Quito del reporte" + campoBasePlantilla.Dx_Campo_Base
                                        : "Agrego a Reporte" + campoBasePlantilla.Dx_Campo_Base,
                                    campoBasePlantilla.Cve_Agregar_Reporte == 0
                                        ? "Quito del reporte" + campoBasePlantilla.Dx_Campo_Base
                                        : "Agrego a Reporte" + campoBasePlantilla.Dx_Campo_Base);
                            }
                            a++;
                        }

                        DDXClaseSE.SelectedIndex = 0;
                        DDXTipoSE.SelectedIndex = 0;
                    }



                    for (var i = 0; i < grdCamposCustomizables.Rows.Count; i++)
                    {
                        var ckbSelect = grdCamposCustomizables.Rows[i].FindControl("ckbSelect") as CheckBox;
                        var nuevoValor = ckbSelect != null && ckbSelect.Checked ? 1 : 0;
                        var dataKey = grdCamposCustomizables.DataKeys[i];
                        if (dataKey != null)
                        {
                            var cveCampoSelec = int.Parse(dataKey[0].ToString());
                            var cveAgregarReporte = int.Parse(dataKey[1].ToString());

                            if (cveAgregarReporte == nuevoValor) continue;
                            var campoEditar =
                                Plantilla.LstCamposCustomizables.FirstOrDefault(me => me.CveCampo == cveCampoSelec);
                            if (campoEditar == null) continue;
                            campoEditar.CveAgregarReporte = nuevoValor;
                            var editado =
                                LogicaNegocios.ModuloCentral.CatalogosPlanilla.ClassInstance.ActualizaCampoPlantilla(
                                    campoEditar);
                            if (editado)
                            {
                                /*INSERTAR EVENTO CAMBIOS DEL PROCESO PLANTILLA EN LOG*/
                                Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                                    Convert.ToInt16(Session["IdRolUserLogueado"]),
                                    Convert.ToInt16(Session["IdDepartamento"]), "PLANTILLAS", "CAMBIOS",
                                    Plantilla.DxDescripcion.ToString(CultureInfo.InvariantCulture), "MOTIVOS??", "",
                                    "",
                                    campoEditar.CveAgregarReporte == 0
                                        ? "Quitó del reporte" + campoEditar.DxNombreCampo
                                        : "Agregó al Reporte" + campoEditar.DxNombreCampo);
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock
                                    (UpdatePanel1, typeof (Page), "Error",
                                        "alert('Se produjo un Error al actualizar los campos')", true);
                                return;
                            }
                        }
                    }

                    ValidaGrid = true;
                    CargaGridSustomizablesPlantilla();

                    ScriptManager.RegisterClientScriptBlock

                        (UpdatePanel1, typeof (Page), "Actualizar", "alert('Se guardarón los cambios con Exitó ')", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock
                    (UpdatePanel1, typeof(Page), "Error", "alert('" + ex.Message + " ')", true);
            }
        }

        protected void grdCamposCustomizables_RowCreated(object sender, GridViewRowEventArgs e)
        {

            if (ValidaGrid)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    int cveAgregaReporte = 0;

                    if (DataBinder.Eval(e.Row.DataItem, "CveAgregarReporte") != null)
                        cveAgregaReporte = (int)DataBinder.Eval(e.Row.DataItem, "CveAgregarReporte");

                    var checbox = (CheckBox)e.Row.FindControl("ckbSelect");
                    checbox.Checked = cveAgregaReporte == 1;
                }
            }
        }

        #endregion

        #region Metodos Protegidos

        protected void InsertaCatalogoCustomizable()        
        {
            var catalogo = new Campo_Customizable
            {
                DxNombreCampo = TxtNomCatalogo.Text.ToUpper().Trim().Replace(' ','_'),
                DxDescripcionCampo = TxtDescCatalogo.Text,
                DxTipo = "Cat",
                ToolTip = TxtToolTip.Text,
                Estatus = 1,
                FechaAdicion = DateTime.Now,
                AdicionadoPor = 1,
                CveObligatorio = RbLstObligatorio.Items[0].Selected ? 1 : 0
            };

            var campo = 
                LogicaNegocios.ModuloCentral.CatalogosPlanilla.ClassInstance.InsertaCampoCustomizable(catalogo);


            if (campo.Cve_Campo != 0)
            {
                Customizable = catalogo;
                Customizable.CveCampo = campo.Cve_Campo;
                var lstValoresCatalogo = new List<Valor_Campo_Catalogo>();

                for (var i = 0; i < RadioListValoresCat.Items.Count; i++)
                {
                    var valor = new Valor_Campo_Catalogo
                    {
                        CveCampo = campo.Cve_Campo,
                        DxValor = RadioListValoresCat.Items[i].Text,
                        Estatus = 1,
                        FechaAdicion = DateTime.Now,
                        AdicionadoPor = 1
                    };

                    lstValoresCatalogo.Add(valor);
                }

                catalogo.LstValoresCatalogo = lstValoresCatalogo;

                var insertoValores =
                    LogicaNegocios.ModuloCentral.CatalogosPlanilla.ClassInstance.InsertaValoresCatalogo(lstValoresCatalogo);

                if (insertoValores)
                {
                    TxtCveCatalogo.Text = campo.Cve_Campo.ToString(CultureInfo.InvariantCulture);
                    BtnAgregarAPlantilla.Visible = true;
                    ScriptManager.RegisterClientScriptBlock
                            (UpdatePanel1, typeof(Page), "Actualizar", "alert('Se guardó el Catalogo con Exitó ')", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock
                            (UpdatePanel1, typeof(Page), "Error", "alert('Ocurrio un Error al guardar los valores del catalogo ')", true);
                }
            }
            else
            {
                var updatePanel1 = UpdatePanel1;
                if (updatePanel1 != null)
                    ScriptManager.RegisterClientScriptBlock
                        (updatePanel1, typeof(Page), "Error", "alert('Ocurrio un Error al guardar el catalogo ')", true);
            }

            
        }

        protected void InsertaCampoTextoCustomizable()
        {
            var campoTexto = new Campo_Customizable
            {
                DxNombreCampo = TxtNombreCampoTexto.Text.ToUpper().Trim().Replace(' ', '_'),
                DxDescripcionCampo = TxtDescripcionCampoTexto.Text,
                DxTipo = "Text",
                ToolTip = TxtToolTipTexto.Text,
                Estatus = 1,
                FechaAdicion = DateTime.Now,
                AdicionadoPor = 1,
                CveObligatorio = RBLObligatorioTexto.Items[0].Selected ? 1 : 0
            };

            var campo =
                LogicaNegocios.ModuloCentral.CatalogosPlanilla.ClassInstance.InsertaCampoCustomizable(campoTexto);

            if (campo.Cve_Campo != 0)
            {
                /*INSERTAR EVENTO CAMBIOS DEL PROCESO PLANTILLA EN LOG*/
                Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                    Convert.ToInt16(Session["IdRolUserLogueado"]), Convert.ToInt16(Session["IdDepartamento"]),
                    "PLANTILLAS", "CAMBIOS",
                    Plantilla.DxDescripcion.ToString(CultureInfo.InvariantCulture), "MOTIVOS??",
                    "", "", campo.Dx_Descripcion_Campo);
                Customizable = campoTexto;
                Customizable.CveCampo = campo.Cve_Campo;
                TxtClaveCampoTexto.Text = campo.Cve_Campo.ToString(CultureInfo.InvariantCulture);
                BtnAgregarAPlantilla2.Visible = true;
                var updatePanel1 = UpdatePanel1;
                if (updatePanel1 != null)
                    ScriptManager.RegisterClientScriptBlock
                        (updatePanel1, typeof (Page), "Actualizar", "alert('Se guardó el campo de texto con éxito ')",
                            true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock
                    (UpdatePanel1, typeof (Page), "Error", "alert('Ocurrió un Error al guardar el campo ')", true);
            }
        }

        protected void ActualizaCatCustomizable()
        {
            var catalogo = new Campo_Customizable
            {
                CveCampo = Customizable.CveCampo,
                DxNombreCampo = TxtNomCatalogo.Text.ToUpper().Trim().Replace(' ', '_'),
                DxDescripcionCampo = TxtDescCatalogo.Text,
                DxTipo = "Cat",
                ToolTip = TxtToolTip.Text,
                Estatus = 1,
                FechaAdicion = Customizable.FechaAdicion,
                AdicionadoPor = 1,
                CveObligatorio = RbLstObligatorio.Items[0].Selected ? 1 : 0
            };

            var actualiza = CatalogosPlanilla.ActualizaCampoCustomizable(catalogo);

            if (actualiza)
            {
                var lstValoresCatalogo = new List<Valor_Campo_Catalogo>();

                for (var i = 0; i < RadioListValoresCat.Items.Count; i++)
                {
                    if (RadioListValoresCat.Items[i].Text == RadioListValoresCat.Items[i].Value)
                    {
                        RadioListValoresCat.Items[i].Enabled = false;

                        var valor = new Valor_Campo_Catalogo
                        {
                            CveCampo = (int) Customizable.CveCampo,
                            DxValor = RadioListValoresCat.Items[i].Text,
                            Estatus = 1,
                            FechaAdicion = DateTime.Now,
                            AdicionadoPor = 1
                        };

                        lstValoresCatalogo.Add(valor);
                    }
                }

                var insertoValores =
                    LogicaNegocios.ModuloCentral.CatalogosPlanilla.ClassInstance.InsertaValoresCatalogo(
                        lstValoresCatalogo);

                if (insertoValores)
                {
                    BtnAgregarAPlantilla.Visible = true;
                    BtnGuardarCatalogo.Visible = false;
                    ScriptManager.RegisterClientScriptBlock
                            (UpdatePanel1, typeof(Page), "Actualizar", "alert('Se Actualizó el Catalogo con Exitó ')", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock
                            (UpdatePanel1, typeof(Page), "Error", "alert('Ocurrio un Error al guardar los valores del catalogo ')", true);
                }
            }
        }
        
        protected void BuscaCustomizables2()
        {
            var paginaActual = AspNetPager.CurrentPageIndex;
            var tamanioPagina = AspNetPager.PageSize;


            grdBuscaCamposCustomizables.DataSource = LstCustomizables.FindAll(
                    me =>
                        me.Rownum >= (((paginaActual - 1) * tamanioPagina) + 1) &&
                        me.Rownum <= (paginaActual * tamanioPagina));

            grdBuscaCamposCustomizables.DataBind();           
        }

        protected void BuscaCustomizables()
        {

            LstCustomizables =
                LogicaNegocios.ModuloCentral.CatalogosPlanilla.ClassInstance.ObtenListaCustomizables();

            if (DDXTipoCampo.SelectedValue == "Tod")
            {
                if (TxtNombreCampo.Text != "")
                {
                    LstCustomizables = LstCustomizables.FindAll(me => me.DxNombreCampo.Contains(TxtNombreCampo.Text));
                }
                
                grdBuscaCamposCustomizables.DataSource = LstCustomizables;
                grdBuscaCamposCustomizables.DataBind();
            }
            else
            {
                LstCustomizables = LstCustomizables.FindAll
                    (me => me.DxTipo == DDXTipoCampo.SelectedValue
                        && me.DxNombreCampo.Contains(TxtNombreCampo.Text.ToUpper()));

                grdBuscaCamposCustomizables.DataSource = LstCustomizables;
                grdBuscaCamposCustomizables.DataBind();
            }

            var rownum = 1;

            foreach (var campoCustomizable in LstCustomizables)
            {
                campoCustomizable.Rownum = rownum;
                rownum++;
            }

            AspNetPager.RecordCount = LstCustomizables.Count;
        }

        protected void CargaDatosCatalogo()
        {
            trValores.Visible = true;
            TxtCveCatalogo.Text = Customizable.CveCampo.ToString();
            TxtNomCatalogo.Text = Customizable.DxNombreCampo;
            TxtDescCatalogo.Text = Customizable.DxDescripcionCampo;
            TxtToolTip.Text = Customizable.ToolTip;

            if (Customizable.CveObligatorio == 1)
                RbLstObligatorio.Items[0].Selected = true;

            if (Customizable.CveObligatorio == 0)
                RbLstObligatorio.Items[1].Selected = true;

            if (Customizable.CveCampo != null)
                Customizable.LstValoresCatalogo = 
                    LogicaNegocios.ModuloCentral.CatalogosPlanilla.ClassInstance.ObtenValoresCatalogo((int)Customizable.CveCampo);

            RadioListValoresCat.DataSource = Customizable.LstValoresCatalogo;
            RadioListValoresCat.DataValueField = "CveValor";
            RadioListValoresCat.DataTextField = "DxValor";
            RadioListValoresCat.DataBind();

            for (var i = 0; i < RadioListValoresCat.Items.Count; i++)
            {
                RadioListValoresCat.Items[i].Enabled = false;
            }
        }

        protected void CargaDatosCampoTexto()
        {
            TxtClaveCampoTexto.Text = Customizable.CveCampo.ToString();
            TxtNombreCampoTexto.Text = Customizable.DxNombreCampo;
            TxtDescripcionCampoTexto.Text = Customizable.DxDescripcionCampo;
            TxtToolTipTexto.Text = Customizable.ToolTip;

            if (Customizable.CveObligatorio == 1)
                RBLObligatorioTexto.Items[0].Selected = true;

            if (Customizable.CveObligatorio == 0)
                RBLObligatorioTexto.Items[1].Selected = true;
        }

        protected void CargaDatosPlantilla()
        {
            Plantilla = 
                LogicaNegocios.ModuloCentral.CatalogosPlanilla.ClassInstance.ObtenPlantillaSeleccionada(IdPlantilla);

           ValidaGrid = true;
            CargaGridSustomizablesPlantilla();

            //Plantilla.DxDescripcion = LblNomPlantilla.Text;
            LblNomPlantilla.Text = Plantilla.DxDescripcion;
            CargaCamposBase();
            MuestraPlantilla();
            BtnGuardarPlantilla.Visible = true;
        }

        protected void AgregarCampoAPlantilla()
        {
            try
            {
                if (Plantilla.CvePlantilla != null)
                {
                    if (Customizable.CveCampo != null)
                    {
                        var nuevoCampo = new Campo_Customizable_Plantilla
                        {
                            CvePlantilla = (int) Plantilla.CvePlantilla,
                            CveCampo = (int) Customizable.CveCampo,
                            Estatus = 1,
                            FechaAdicion = DateTime.Now
                        };

                        var resultado =
                            LogicaNegocios.ModuloCentral.CatalogosPlanilla.ClassInstance.InsertaCampoPlantilla(
                                nuevoCampo);

                        if (resultado == null) return;
                        /*INSERTAR EVENTO CAMBIOS DEL PROCESO PLANTILLA EN LOG*/
                        Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                            Convert.ToInt16(Session["IdRolUserLogueado"]), Convert.ToInt16(Session["IdDepartamento"]),
                            "PLANTILLAS", "CAMBIOS",
                            Plantilla.DxDescripcion.ToString(CultureInfo.InvariantCulture), "MOTIVOS??",
                            "", "", "Agregó el campo: " + Customizable.DxNombreCampo);
                        ValidaGrid = true;
                        CargaGridSustomizablesPlantilla();
                    }
                }
            }
            catch (Exception ex)
            {
                ValidaGrid = true;
                CargaGridSustomizablesPlantilla();
                LblNomPlantilla.Text = Plantilla.DxDescripcion;
                ScriptManager.RegisterClientScriptBlock
                    (UpdatePanel1, typeof (Page), "Error", "alert('" + ex.Message + " ')", true);
            }
        }

        protected void CargaCamposBase()
        {
            LstCamposBase = 
                LogicaNegocios.ModuloCentral.CatalogosPlanilla.ClassInstance.ObtenBasesPlantilla(Plantilla.CvePlantilla);

            CamposBaseReporte = new int[200];
            var a = 0;
            foreach (var cr in LstCamposBase)
            {
                if (cr.Cve_Agregar_Reporte != null) CamposBaseReporte[a] = (int)cr.Cve_Agregar_Reporte;
                a++;
            }

            if(Plantilla.DxDescripcion != "PLANTILLA SE")
            {
                Panel panelPlantilla;
                switch (Plantilla.DxDescripcion)
                {
                    case "PLANTILLA BC":
                        panelPlantilla = PanelPlantillaBC;
                        break;

                    default:
                        panelPlantilla = PanelPlantillaComun;
                        break;
                }



                if (Plantilla.DxDescripcion == "PLANTILLA BC")
                {
                    var tipoProducto = DDXTipoProductoBC.SelectedIndex != 0 ? DDXTipoProductoBC.SelectedValue : "27";

                    foreach (var campo in LstCamposBase.FindAll(me => me.Cve_Adicional3 == tipoProducto))
                    {
                        var checkbox = (CheckBox) panelPlantilla.FindControl(campo.Dx_Adicional2);
                        checkbox.Checked = campo.Cve_Agregar_Reporte == 1;
                    }
                }
                else
                {
                    foreach (var campo in LstCamposBase)
                    {
                        var checkbox = (CheckBox)panelPlantilla.FindControl(campo.Dx_Adicional2);
                        checkbox.Checked = campo.Cve_Agregar_Reporte == 1;
                    }
                }

                tablaCustomizablesPlantilla.Width = "100%";
            }
            else
            {
                var clase = DDXClaseSE.SelectedValue != "" ? int.Parse(DDXClaseSE.SelectedValue) : 1;
                var tipo = DDXTipoSE.SelectedValue != "" ? int.Parse(DDXTipoSE.SelectedValue) : 2;

                foreach (var campo in LstCamposBase.FindAll(me => me.Cve_Adicional1 == clase && me.Cve_Adicional2 == tipo))
                {
                    var checkbox = (CheckBox)containerSE.FindControl(campo.Dx_Adicional2);
                    checkbox.Checked = campo.Cve_Agregar_Reporte == 1;
                }

                tablaCustomizablesPlantilla.Width = "80%";
            }
        }

        protected void CargaGridSustomizablesPlantilla()
        {
            if (Plantilla.DxDescripcion != "PLANTILLA BC" || Plantilla.DxDescripcion != "PLANTILLA SE")
            {
                LstCustomizablesPlantilla =
                    LogicaNegocios.ModuloCentral.CatalogosPlanilla.ClassInstance.ObtenCustomizablesPlantilla(
                        Plantilla.CvePlantilla);

                Plantilla.LstCamposCustomizables = LstCustomizablesPlantilla;

                grdCamposCustomizables.DataSource = Plantilla.LstCamposCustomizables;
                grdCamposCustomizables.DataBind();

                AspNetPager2.RecordCount = LstCustomizablesPlantilla.Count;
                ValidaGrid = false;
            }
        }

        protected void CargaGridCustomizablesPlantilla2()
        {
            var paginaActual = AspNetPager2.CurrentPageIndex;
            var tamanioPagina = AspNetPager2.PageSize;

            grdCamposCustomizables.DataSource = Plantilla.LstCamposCustomizables.FindAll(
                    me =>
                        me.Rownum >= (((paginaActual - 1) * tamanioPagina) + 1) &&
                        me.Rownum <= (paginaActual * tamanioPagina));

            grdCamposCustomizables.DataBind();
        }

        public void MuestraPlantilla()
        {
            PanelPlantillaBC.Visible = containerSE.Visible = PanelPlantillaComun.Visible = false;

            switch (Plantilla.DxDescripcion)
            {
                case "PLANTILLA BC":
                    PanelPlantillaBC.Visible = true;
                    break;

                case "PLANTILLA SE":
                    containerSE.Visible = true;
                    break;

                default:
                    PanelPlantillaComun.Visible = PanelGridCustomizables.Visible = true;
                    CargaGridSustomizablesPlantilla();
                    break;
            }

            BtnGuardarPlantilla.Visible = true;

            //PanelGridCustomizables.Visible = true;
        }

        public void OcultaPlantillas()
        {
            PanelPlantillaBC.Visible = containerSE.Visible = PanelPlantillaComun.Visible = false;
            PanelGridCustomizables.Visible = false;
            BtnGuardarPlantilla.Visible = false;
        }

        protected void AgregaNuevaPlantilla()
        {
            Plantilla = new Comp_Plantilla {DxDescripcion = TxtNuevaPlantilla.Text.ToUpper(), Estatus = 1, FechaAdicion = DateTime.Now};
            
            var nuevaPlantilla = LogicaNegocios.ModuloCentral.CatalogosPlanilla.InsertaPlantilla(Plantilla);

            if (nuevaPlantilla != null)
            {
                /*INSERTAR EVENTO ALTA DEL PROCESO PLANTILLAS EN LOG*/
                Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                    Convert.ToInt16(Session["IdRolUserLogueado"]), Convert.ToInt16(Session["IdDepartamento"]),
                    "PLANTILLAS", "ALTA",
                    nuevaPlantilla.Dx_Descripcion.ToString(CultureInfo.InvariantCulture), "",
                    "Fecha alta: " + DateTime.Now, "", "");

                Plantilla.CvePlantilla = nuevaPlantilla.Cve_Plantilla;
                IdPlantilla = (int) Plantilla.CvePlantilla;

                LstCamposBase =LogicaNegocios.ModuloCentral.CatalogosPlanilla.ClassInstance.ObtenBasesPlantilla(1);
                var camposBase = LogicaNegocios.ModuloCentral.CatalogosPlanilla.InserCampoBasePlantillas(LstCamposBase,(int) Plantilla.CvePlantilla);
                LstCamposBase =LogicaNegocios.ModuloCentral.CatalogosPlanilla.ClassInstance.ObtenBasesPlantilla((int) Plantilla.CvePlantilla);
                CargaDatosPlantilla();
                CargaCamposBase();
            }

            TxtNuevaPlantilla.Text = "";
        }

        protected void CargaPlantillasXTecnologia()
        {
            try
            {
                var tabla = new StringBuilder();
                tabla.Append("<table style='font-size: small; width: 90%'>");
                tabla.Append("  <tr class='trh'>");

                int cont = 0;
                var lstPlantillas = LogicaNegocios.ModuloCentral.CatalogosTecnologia.OCatPlantillas();
                var posicionPlantillas = new string[lstPlantillas.Count];

                tabla.Append("<td>Tecnología</td>");

                foreach (var catPlantilla in lstPlantillas)
                {
                    tabla.Append("<td>");
                    tabla.Append(catPlantilla.Dx_Descripcion);
                    tabla.Append("</td>");
                    posicionPlantillas[cont] = catPlantilla.Cve_Plantilla.ToString(CultureInfo.InvariantCulture);
                    cont++;
                }

                tabla.Append("  </tr>");

                var lstTecnologias = LogicaNegocios.ModuloCentral.CatalogosTecnologia.OCatTecnologias();
                var cont2 = 1;

                foreach (var tecnologia in lstTecnologias)
                {
                    if ((cont2 %= 2) == 0)
                        tabla.Append("<tr class='tr2'>");
                    else
                    {
                        tabla.Append("<tr class='tr1'>");
                    }

                    tabla.Append("<td style='text-align: right'>");
                    tabla.Append(tecnologia.Dx_Nombre_General);
                    tabla.Append("</td>");

                    for (int i = 0; i < cont; i++)
                    {
                        var valor = posicionPlantillas.GetValue(i);
                        var cvePlantilla = Convert.ToInt32(valor);

                        if (tecnologia.Cve_Plantilla == cvePlantilla)
                        {
                            tabla.Append("<td><input type='checkbox' disabled='true' name='option2' value='' checked> </td>");
                        }
                        else
                        {
                            tabla.Append("<td><br></td>");
                        }

                    }

                    tabla.Append("</tr>");
                    cont2++;
                }

                tabla.Append("</table>");

                LiteralTablaPlantillas.Text = tabla.ToString();

                OcultaPlantillas();
                containerNP.Visible = false;
                contaiterEdicionPlantilla.Visible = false;
                containerLiteral.Visible = true;
                PanelPlantillasTecnologia.Visible = true;
                PanelBusquedaCamposCustomizables.Visible = false;
                PanelCampoCatalogo.Visible = false;
                PanelCampoTexto.Visible = false;
                ImgBtnHome.Visible = false;
                lblTitle.Visible = true;
            }
            catch (Exception ex)
            {
                PanelPlantillasTecnologia.Visible =
                    PanelPlantillaComun.Visible = PanelPlantillaBC.Visible = containerSE.Visible = false;
            }
        }

        #endregion                                      

        #region Eventos de navegacion

        //protected void ImgBtnRegresar1_Click(object sender, ImageClickEventArgs e)
        //{
        //    PanelCamposCustomizables.Visible = false;
        //    MuestraPlantilla();
        //    BtnGuardarPlantilla.Visible = true;
        //}

        //protected void ImgBtnRegresar2_Click(object sender, ImageClickEventArgs e)
        //{
        //    PanelBusquedaCamposCustomizables.Visible = false;
        //    MuestraPlantilla();
        //}

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            PanelCampoTexto.Visible = false;
            PanelCamposCustomizables.Visible = true;
        }

        #endregion

        #region llenarCatalogos

        protected void LlenarCatalogoClase()
        {
            try
            {

                var lstClase = LogicaNegocios.ModuloCentral.AdministraProducto.ObCatSeClases();
                DDXClaseSE.DataSource = lstClase;
                DDXClaseSE.DataValueField = "Cve_Clase";
                DDXClaseSE.DataTextField = "Dx_Clase";
                DDXClaseSE.DataBind();

                DDXClaseSE.Items.Insert(0, new ListItem("", ""));
                DDXClaseSE.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                PanelPlantillasTecnologia.Visible =
                    PanelPlantillaComun.Visible = PanelPlantillaBC.Visible = containerSE.Visible = false;

                ScriptManager.RegisterClientScriptBlock
                    (UpdatePanel1, typeof(Page), "Error", "alert('" + ex.Message + "')", true);
            }
        }

        protected void LlenarCatalogoTipo()
        {
            try
            {
                DDXTipoSE.Items.Clear();
                var lstTipo = LogicaNegocios.ModuloCentral.AdministraProducto.ObSeTipos(DDXClaseSE.SelectedValue);
                DDXTipoSE.DataSource = lstTipo;
                DDXTipoSE.DataValueField = "Cve_Tipo";
                DDXTipoSE.DataTextField = "Dx_Nombre_Tipo";
                DDXTipoSE.DataBind();

                DDXTipoSE.Items.Insert(0, new ListItem("", ""));
                DDXTipoSE.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                PanelPlantillasTecnologia.Visible =
                    PanelPlantillaComun.Visible = PanelPlantillaBC.Visible = containerSE.Visible = false;

                ScriptManager.RegisterClientScriptBlock
                    (UpdatePanel1, typeof(Page), "Error", "alert('" + ex.Message + "')", true);
            }
        }

        protected void LlenatCatalogoTipoProducto()
        {
            try
            {
                var lstTipoProd = CatalogosSolicitud.ObtenTipoProducto(7);
                DDXTipoProductoBC.DataSource = lstTipoProd;
                DDXTipoProductoBC.DataValueField = "Ft_Tipo_Producto";
                DDXTipoProductoBC.DataTextField = "Dx_Tipo_Producto";
                DDXTipoProductoBC.DataBind();

                DDXTipoProductoBC.Items.Insert(0, new ListItem("", ""));
                DDXTipoProductoBC.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

                PanelPlantillasTecnologia.Visible =
                   PanelPlantillaComun.Visible = PanelPlantillaBC.Visible = containerSE.Visible = false;

                ScriptManager.RegisterClientScriptBlock
                    (UpdatePanel1, typeof(Page), "Error", "alert('" + ex.Message + "')", true); ;
            }
        }

        #endregion

        #region Eventos 2

        protected void BtnCrearNuevaPlantilla_Click(object sender, EventArgs e)
        {
            Page.Validate();

            if (Page.IsValid)
            {
                AgregaNuevaPlantilla();
                containerNP.Visible = false;
                PanelPlantillasTecnologia.Visible = false;
            }
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            containerNP.Visible = false;
            CargaPlantillasXTecnologia();
            containerLiteral.Visible = true;
        }

        protected void BtnAgregarNuevaPlantilla_Click(object sender, EventArgs e)
        {
            containerLiteral.Visible = false;
            containerNP.Visible = true;
            TxtNuevaPlantilla.Focus();
        }

        protected void BtnEditarPlantilla_Click(object sender, EventArgs e)
        {
            var lstPlantillas = LogicaNegocios.ModuloCentral.CatalogosTecnologia.OCatPlantillas();
            RadioBtnLstPlantillas.DataSource = lstPlantillas;
            RadioBtnLstPlantillas.DataValueField = "Cve_Plantilla";
            RadioBtnLstPlantillas.DataTextField = "Dx_Descripcion";
            RadioBtnLstPlantillas.DataBind();

            containerLiteral.Visible = false;
            contaiterEdicionPlantilla.Visible = true;
        }

        protected void BtnIrEdicionPlantilla_Click(object sender, EventArgs e)
        {
            var cvePlantila = int.Parse(RadioBtnLstPlantillas.SelectedValue);
            IdPlantilla = cvePlantila;
            PanelPlantillasTecnologia.Visible = false;
            CargaDatosPlantilla();
        }

        protected void ImgBtnHome_Click(object sender, ImageClickEventArgs e)
        {
            CargaPlantillasXTecnologia();
            BtnGuardarPlantilla.Visible = false;
        }

        protected void BtnCancelarEdicion_Click(object sender, EventArgs e)
        {
            contaiterEdicionPlantilla.Visible = false;
            CargaPlantillasXTecnologia();
            containerLiteral.Visible = true;
        }

        protected void ImgEliminar_Click(object sender, ImageClickEventArgs e)
        {
            var posicion = RadioListValoresCat.SelectedIndex;

            RadioListValoresCat.Items.RemoveAt(posicion);
        }

        protected void BtnActualizaCat_Click(object sender, EventArgs e)
        {
            var posicion = RadioListValoresCat.SelectedIndex;

            RadioListValoresCat.Items.RemoveAt(posicion);
            RadioListValoresCat.Items.Insert(posicion, new ListItem(TxtNuevoNombreCat.Text));
            TxtNuevoNombreCat.Text = "";
        }

        //protected void ImgBtnRegresar3_Click(object sender, ImageClickEventArgs e)
        //{
        //    PanelCampoCatalogo.Visible = false;
        //    MuestraPlantilla();
        //    BtnGuardarPlantilla.Visible = true;
        //}

        //protected void ImgBtnRegresar4_Click(object sender, ImageClickEventArgs e)
        //{
        //    PanelCampoTexto.Visible = false;
        //    MuestraPlantilla();
        //    BtnGuardarPlantilla.Visible = true;
        //}

        protected void DDXTipoProductoBC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDXTipoProductoBC.SelectedItem.Text.Contains("BANCO FIJO"))
                PanelPlantillaBC2.Visible = false;

            if (DDXTipoProductoBC.SelectedItem.Text.Contains("BANCO AUTOMATICO"))
                PanelPlantillaBC2.Visible = true;

            CargaCamposBase();
        }

        protected void DDXClaseSE_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenarCatalogoTipo();
        }

        protected void DDXTipoSE_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargaCamposBase();

            var clase = DDXClaseSE.SelectedValue != "" ? int.Parse(DDXClaseSE.SelectedValue) : 1;
            var tipo = DDXTipoSE.SelectedValue != "" ? int.Parse(DDXTipoSE.SelectedValue) : 2;
            var muestra = true;
            
            if (clase == 2)
            {
                if (tipo == 11)
                {
                    muestra = false;

                    LblPoste.Visible = !muestra;
                    TxtPoste.Visible = !muestra;
                    ChkTxtPoste.Visible = !muestra;

                    LblPrecioConectores.Visible = TxtPrecioConectores.Visible = ChkTxtPrecioConectores.Visible = muestra;
                    LblAisladores.Visible = TxtAisladores.Visible = ChkTxtAisladores.Visible = muestra;
                    LblAisladoresSoporte.Visible = TxtAisladoresSoporte.Visible = ChkTxtAisladoresSoporte.Visible = muestra;
                    LblCableGuia.Visible = TxtCableGuia.Visible = ChkTxtCableGuia.Visible = muestra;
                    LblCableGuiaCorto.Visible = TxtCableGuiaCorto.Visible = ChkTxtCableGuiaCorto.Visible = muestra;
                    LblCableGuiaTransformador.Visible = TxtCableGuiaTransformador.Visible = ChkTxtCableGuiaTransformador.Visible = muestra;
                    LblApartarayo.Visible = TxtApartarayo.Visible = ChkTxtApartarayo.Visible = muestra;
                    LblCortaCircuito.Visible = TxtCortaCircuito.Visible = ChkTxtCortaCircuito.Visible = muestra;
                    LblFusible.Visible = TxtFusible.Visible = ChkTxtFusible.Visible = muestra;
                    LabelCableGuia.Visible = TxtCableGuiaConexion.Visible = ChkTxtCableGuiaConexion.Visible = muestra;
                    LableCableConexion.Visible = TxtCableConexionSubterraneo.Visible = ChkTxtCableConexionSubterraneo.Visible = muestra;

                    LblEmpalmes.Visible = TxtEmpalmes.Visible = ChkTxtEmpalmes.Visible = !muestra;
                    LblCableExtremos.Visible = TxtExtremos.Visible = ChkTxtExtremos.Visible = !muestra;

                    LblDDXMarcaFusibleSE.Visible = TxtPrecioFusibleSE.Visible = ChkTxtPrecioFusibleSE.Visible = !muestra;
                }
                else
                {
                    muestra = true;

                    LblPoste.Visible = muestra;
                    TxtPoste.Visible = muestra;
                    ChkTxtPoste.Visible = muestra;

                    LblPrecioConectores.Visible = TxtPrecioConectores.Visible = ChkTxtPrecioConectores.Visible = muestra;
                    LblAisladores.Visible = TxtAisladores.Visible = ChkTxtAisladores.Visible = muestra;
                    LblAisladoresSoporte.Visible = TxtAisladoresSoporte.Visible = ChkTxtAisladoresSoporte.Visible = muestra;
                    LblCableGuia.Visible = TxtCableGuia.Visible = ChkTxtCableGuia.Visible = muestra;
                    LblCableGuiaCorto.Visible = TxtCableGuiaCorto.Visible = ChkTxtCableGuiaCorto.Visible = muestra;
                    LblCableGuiaTransformador.Visible = TxtCableGuiaTransformador.Visible = ChkTxtCableGuiaTransformador.Visible = muestra;
                    LblApartarayo.Visible = TxtApartarayo.Visible = ChkTxtApartarayo.Visible = muestra;
                    LblCortaCircuito.Visible = TxtCortaCircuito.Visible = ChkTxtCortaCircuito.Visible = muestra;
                    LblFusible.Visible = TxtFusible.Visible = ChkTxtFusible.Visible = muestra;
                    LabelCableGuia.Visible = TxtCableGuiaConexion.Visible = ChkTxtCableGuiaConexion.Visible = muestra;
                    LableCableConexion.Visible = TxtCableConexionSubterraneo.Visible = ChkTxtCableConexionSubterraneo.Visible = muestra;

                    LblEmpalmes.Visible = TxtEmpalmes.Visible = ChkTxtEmpalmes.Visible = !muestra;
                    LblCableExtremos.Visible = TxtExtremos.Visible = ChkTxtExtremos.Visible = !muestra;

                    LblDDXMarcaFusibleSE.Visible = TxtPrecioFusibleSE.Visible = ChkTxtPrecioFusibleSE.Visible = muestra;
                }

                LblVerificacionUVIE.Visible = true;
            }
            else
            {
                muestra = true;

                LblPoste.Visible = muestra;
                TxtPoste.Visible = muestra;
                ChkTxtPoste.Visible = muestra;

                LblPrecioConectores.Visible = TxtPrecioConectores.Visible = ChkTxtPrecioConectores.Visible = muestra;
                LblAisladores.Visible = TxtAisladores.Visible = ChkTxtAisladores.Visible = muestra;
                LblAisladoresSoporte.Visible = TxtAisladoresSoporte.Visible = ChkTxtAisladoresSoporte.Visible = muestra;
                LblCableGuia.Visible = TxtCableGuia.Visible = ChkTxtCableGuia.Visible = muestra;
                LblCableGuiaCorto.Visible = TxtCableGuiaCorto.Visible = ChkTxtCableGuiaCorto.Visible = muestra;
                LblCableGuiaTransformador.Visible = TxtCableGuiaTransformador.Visible = ChkTxtCableGuiaTransformador.Visible = muestra;
                LblApartarayo.Visible = TxtApartarayo.Visible = ChkTxtApartarayo.Visible = muestra;
                LblCortaCircuito.Visible = TxtCortaCircuito.Visible = ChkTxtCortaCircuito.Visible = muestra;
                LblFusible.Visible = TxtFusible.Visible = ChkTxtFusible.Visible = muestra;
                LabelCableGuia.Visible = TxtCableGuiaConexion.Visible = ChkTxtCableGuiaConexion.Visible = !muestra;
                LableCableConexion.Visible = TxtCableConexionSubterraneo.Visible = ChkTxtCableConexionSubterraneo.Visible = !muestra;

                LblEmpalmes.Visible = TxtEmpalmes.Visible = ChkTxtEmpalmes.Visible = !muestra;
                LblCableExtremos.Visible = TxtExtremos.Visible = ChkTxtExtremos.Visible = !muestra;

                LblDDXMarcaFusibleSE.Visible = TxtPrecioFusibleSE.Visible = ChkTxtPrecioFusibleSE.Visible = !muestra;

                LblVerificacionUVIE.Visible = tipo != 4;
            }
        }

        protected void grdBuscaCamposCustomizables_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdBuscaCamposCustomizables.PageIndex = e.NewPageIndex;
            BuscaCustomizables();
        }

        protected void grdCamposCustomizables_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdCamposCustomizables.PageIndex = e.NewPageIndex;
            CargaGridSustomizablesPlantilla();
        }

        

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            CargaPlantillasXTecnologia();
            BtnGuardarPlantilla.Visible = false;
        }

        protected void link1_Click(object sender, EventArgs e)
        {
            CargaPlantillasXTecnologia();
            lblTitle.Visible = true;
            BtnGuardarPlantilla.Visible = false;
        }

        protected void AspNetPager_PageChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                BuscaCustomizables2();
            }
        }

        protected void AspNetPager2_PageChanged(object sender, EventArgs e)
        {
            CargaGridCustomizablesPlantilla2();
        }

        
        protected void Button1_Click(object sender, EventArgs e)
        {
            PanelCampoTexto.Visible = false;
            MuestraPlantilla();
            BtnGuardarPlantilla.Visible = true;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            PanelCampoCatalogo.Visible = false;
            MuestraPlantilla();
            BtnGuardarPlantilla.Visible = true;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            PanelBusquedaCamposCustomizables.Visible = false;
            MuestraPlantilla();
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            PanelCamposCustomizables.Visible = false;
            MuestraPlantilla();
            BtnGuardarPlantilla.Visible = true;
        }

        #endregion
    }
}