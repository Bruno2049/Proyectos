using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using PAEEEM.DataAccessLayer;
using PAEEEM.Entities;
using PAEEEM.Helpers;
using System.Collections.Generic;

namespace PAEEEM.CentralModule
{
    public partial class AddAndEditProduct : System.Web.UI.Page
    {
        const string DxCveCC_SE = "SE";
        public Dictionary<string, string> DxCveCC
        {
            get
            {
                if (ViewState["DxCveCC"] == null)
                    ViewState["DxCveCC"] = new Dictionary<string, string>();
                return (Dictionary<string, string>)ViewState["DxCveCC"];
            }
            set
            {
                ViewState["DxCveCC"] = value;
            }
        }
        public int StatusID
        {
            get
            {
                return ViewState["StatusID"] == null ? 0 : int.Parse(ViewState["StatusID"].ToString());
            }
            set
            {
                ViewState["StatusID"] = value;
            }
        }
        /// <summary>
        /// Init Default Data When page Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    //Initialize Manufacture Dropdownlist
                    InitializeDrpManufacture();
                    //Initialize Technology dropdownlist
                    InitializeDrpTechnology();
                    //Initialize Tipo Of Products dropdownlist
                    InitializeDrpTipoProduct();
                    //Initialize Marca dropdownlist
                    InitializeDrpMarca();
                    //Initialize Capacidad dropdownlist
                    InitializeDrpCapacidad(); // added by Tina 2012-02-24

                    // RSA 2012-09-13 SE attributes Start
                    InitializeDrpSE_TIPO();
                    InitializeDrpSE_TRANSFORMADOR();
                    InitializeDrpSE_TRANSFORM_FASE();
                    InitializeDrpSE_TRANSFORM_MARCA();
                    InitializeDrpSE_TRANSFORM_RELACION();
                    InitializeDrpSE_APARTARRAYO();
                    InitializeDrpSE_APARTARRAYO_MARCA();
                    InitializeDrpSE_CORTACIRCUITO();
                    InitializeDrpSE_CORTACIRC_MARCA();
                    InitializeDrpSE_INTERRUPTOR();
                    InitializeDrpSE_INTERRUPTOR_MARCA();
                    InitializeDrpSE_CONDUCTOR();
                    InitializeDrpSE_CONDUCTOR_MARCA();
                    InitializeDrpSE_COND_CONEXION();
                    InitializeDrpSE_COND_CONEXION_MARCA();
                    // RSA 2012-09-13 SE attributes End

                    //Edit Product
                    if (!string.IsNullOrEmpty(Request.QueryString["ProductID"]))
                    {
                        string ProductID = Request.QueryString["ProductID"];

                        LoadEditData(ProductID);
                    }

                    this.lblFechaDate.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, typeof(Page), "LoadException", "alert('" + ex.Message + "');", true);
                }
            }
        }
        /// <summary>
        /// Load Product Information
        /// </summary>
        /// <param name="productId"></param>
        private void LoadEditData(string productId)
        {
            DataTable dtProduct = CAT_PRODUCTODal.ClassInstance.Get_CAT_PRODUCTO_ByPK(productId);
            if (dtProduct != null && dtProduct.Rows.Count > 0)
            {
                drpManufacture.SelectedValue = dtProduct.Rows[0]["Cve_Fabricante"].ToString();
                drpTechnology.SelectedValue = dtProduct.Rows[0]["Cve_Tecnologia"].ToString();
                drpTipoProduct.SelectedValue = dtProduct.Rows[0]["Ft_Tipo_Producto"].ToString();
                drpMarca.SelectedValue = dtProduct.Rows[0]["Cve_Marca"].ToString();
                txtNameProduct.Text = dtProduct.Rows[0]["Dx_Nombre_Producto"].ToString();
                txtModel.Text = dtProduct.Rows[0]["Dx_Modelo_Producto"].ToString();
                txtMaxPrice.Text = string.Format("{0:0.00}", dtProduct.Rows[0]["Mt_Precio_Max"]);
                txtEficience.Text = string.Format("{0:0.00}", dtProduct.Rows[0]["No_Eficiencia_Energia"]);
                txtConsumer.Text = string.Format("{0:0.00}", dtProduct.Rows[0]["No_Max_Consumo_24h"]);
                txtAhorroConsumo.Text = string.Format("{0:0.00}", dtProduct.Rows[0]["Ahorro_Consumo"]);
                txtAhorroDemanda.Text = string.Format("{0:0.00}", dtProduct.Rows[0]["Ahorro_Demanda"]);

                // updated by tina 2012-02-24
                if (!string.IsNullOrEmpty(dtProduct.Rows[0]["Cve_Capacidad_Sust"].ToString()))
                {
                    drpCapacidad.SelectedValue = dtProduct.Rows[0]["Cve_Capacidad_Sust"].ToString();
                }
                // end
                StatusID = int.Parse(dtProduct.Rows[0]["Cve_Estatus_Producto"].ToString());

                // RSA 2012-09-13 SE attributes Start
                drpSE_TIPO.SelectedValue = dtProduct.Rows[0]["Cve_Tipo_SE"].ToString();
                drpSE_TRANSFORMADOR.SelectedValue = dtProduct.Rows[0]["Cve_Transformador_SE"].ToString();
                drpSE_TRANSFORM_FASE.SelectedValue = dtProduct.Rows[0]["Cve_Fase_Transformador"].ToString();
                drpSE_TRANSFORM_MARCA.SelectedValue = dtProduct.Rows[0]["Cve_Marca_Transform"].ToString();
                drpSE_TRANSFORM_RELACION.SelectedValue = dtProduct.Rows[0]["Cve_Relacion_Transform"].ToString();
                drpSE_APARTARRAYO.SelectedValue = dtProduct.Rows[0]["Cve_Apartarrayos_SE"].ToString();
                drpSE_APARTARRAYO_MARCA.SelectedValue = dtProduct.Rows[0]["Cve_Marca_Apartar"].ToString();
                drpSE_CORTACIRCUITO.SelectedValue = dtProduct.Rows[0]["Cve_Cortacircuito_SE"].ToString();
                drpSE_CORTACIRC_MARCA.SelectedValue = dtProduct.Rows[0]["Cve_Marca_Cortacirc"].ToString();
                drpSE_INTERRUPTOR.SelectedValue = dtProduct.Rows[0]["Cve_Interruptor_SE"].ToString();
                drpSE_INTERRUPTOR_MARCA.SelectedValue = dtProduct.Rows[0]["Cve_Marca_Interrup"].ToString();
                drpSE_CONDUCTOR.SelectedValue = dtProduct.Rows[0]["Cve_Conductor_SE"].ToString();
                drpSE_CONDUCTOR_MARCA.SelectedValue = dtProduct.Rows[0]["Cve_Marca_Conductor"].ToString();
                drpSE_COND_CONEXION.SelectedValue = dtProduct.Rows[0]["Cve_Cond_Conex_SE"].ToString();
                drpSE_COND_CONEXION_MARCA.SelectedValue = dtProduct.Rows[0]["Cve_Cond_Conex_Mca"].ToString();

                ShowSE(IsSE(drpTechnology.SelectedValue));
                // RSA 2012-09-13 SE attributes End
            }
        }
        /// <summary>
        /// Initialize Manufacture Dropdownlist
        /// </summary>
        private void InitializeDrpManufacture()
        {
            DataTable dtManufacture = CAT_FABRICANTEDal.ClassInstance.Get_All_CAT_FABRICANTE();
            if (dtManufacture != null && dtManufacture.Rows.Count > 0)
            {
                drpManufacture.DataSource = dtManufacture;
                drpManufacture.DataValueField = "Cve_Fabricante";
                drpManufacture.DataTextField = "Dx_Nombre_Fabricante";
                drpManufacture.DataBind();
            }
            drpManufacture.Items.Insert(0, new ListItem("", ""));
            drpManufacture.SelectedIndex = 0;
        }
        /// <summary>
        /// Initialize Technology dropdownlist
        /// </summary>
        private void InitializeDrpTechnology()
        {
            DataTable dtTechnology = CAT_TECNOLOGIADAL.ClassInstance.Get_All_CAT_TECNOLOGIA();
            // RSA 2012-09-11 Create dictionary
            DxCveCC.Clear();
            for (int j = 0; j < dtTechnology.Rows.Count; j++)
            {
                DxCveCC.Add(dtTechnology.Rows[j]["Cve_Tecnologia"].ToString(), dtTechnology.Rows[j]["Dx_Cve_CC"].ToString());
            }
            if (dtTechnology != null && dtTechnology.Rows.Count > 0)
            {
                drpTechnology.DataSource = dtTechnology;
                drpTechnology.DataValueField = "Cve_Tecnologia";
                drpTechnology.DataTextField = "Dx_Nombre_General";
                drpTechnology.DataBind();
            }
            drpTechnology.Items.Insert(0, new ListItem("", ""));
            drpTechnology.SelectedIndex = 0;
        }
        /// <summary>
        /// Initialize Tipo Of Products dropdownlist
        /// </summary>
        private void InitializeDrpTipoProduct()
        {
            string Technology = (this.drpTechnology.SelectedIndex == 0 || this.drpTechnology.SelectedIndex == -1) ? "" : this.drpTechnology.SelectedValue.ToString();
            DataTable dtTipoProduct = CAT_TIPO_PRODUCTODal.ClassInstance.Get_CAT_TIPO_PRODUCTOByTechnology(Technology);
            if (dtTipoProduct != null)// added by tina 2012-02-24
            {
                drpTipoProduct.DataSource = dtTipoProduct;
                drpTipoProduct.DataTextField = "Dx_Tipo_Producto";
                drpTipoProduct.DataValueField = "Ft_Tipo_Producto";
                drpTipoProduct.DataBind();
            }
            drpTipoProduct.Items.Insert(0, new ListItem("", ""));
            drpTipoProduct.SelectedIndex = 0;
        }
        /// <summary>
        /// Initialize Marca dropdownlist
        /// </summary>
        private void InitializeDrpMarca()
        {
            DataTable dtMarca = CAT_MARCADal.ClassInstance.Get_ALL_CAT_MARCADal();
            if (dtMarca != null && dtMarca.Rows.Count > 0)
            {
                drpMarca.DataSource = dtMarca;
                drpMarca.DataValueField = "Cve_Marca";
                drpMarca.DataTextField = "Dx_Marca";
                drpMarca.DataBind();
            }
            drpMarca.Items.Insert(0, new ListItem("", ""));
            drpMarca.SelectedIndex = 0;
        }

        private void InitializeDrpCapacidad() // added by Tina 2012-02-24
        {
            int technologyId = drpTechnology.SelectedIndex != -1 && drpTechnology.SelectedIndex != 0 ? Convert.ToInt32(drpTechnology.SelectedValue.ToString()) : 0;
            DataTable dtCapacidad;
            if (technologyId != 0)
            {
                dtCapacidad = CAT_CAPACIDAD_SUSTITUCIONDal.ClassInstance.Get_CAT_CAPACIDAD_SUSTITUCIONByTechnology(technologyId);
            }
            else
            {
                dtCapacidad = CAT_CAPACIDAD_SUSTITUCIONDal.ClassInstance.Get_ALL_CAT_CAPACIDAD_SUSTITUCION();
            }
            if (dtCapacidad != null)
            {
                drpCapacidad.DataSource = dtCapacidad;
                drpCapacidad.DataTextField = "No_Capacidad";
                drpCapacidad.DataValueField = "Cve_Capacidad_Sust";
                drpCapacidad.DataBind();
            }
            drpCapacidad.Items.Insert(0, new ListItem("", ""));
            drpCapacidad.SelectedIndex = 0;
        }
        /// <summary>
        /// Save Product Information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        // RSA 2012-09-12 Initialize SE combos Start
        /// <summary>
        /// Initialize Tipo dropdownlist
        /// </summary>
        private void InitializeDrpSE_TIPO()
        {
            DataTable dtSE_TIPO = CAT_MARCADal.ClassInstance.Get_ALL_CAT_SE_TIPODal();
            if (dtSE_TIPO != null && dtSE_TIPO.Rows.Count > 0)
            {
                drpSE_TIPO.DataSource = dtSE_TIPO;
                drpSE_TIPO.DataValueField = "Cve_Tipo";
                drpSE_TIPO.DataTextField = "Dx_Nombre_Tipo";
                drpSE_TIPO.DataBind();
            }
            // drpSE_TIPO.Items.Insert(0, new ListItem("", ""));
            drpSE_TIPO.SelectedIndex = 0;
        }
        /// <summary>
        /// Initialize Transformador dropdownlist
        /// </summary>
        private void InitializeDrpSE_TRANSFORMADOR()
        {
            DataTable dtSE_TRANSFORMADOR = CAT_MARCADal.ClassInstance.Get_ALL_CAT_SE_TRANSFORMADORDal();
            if (dtSE_TRANSFORMADOR != null && dtSE_TRANSFORMADOR.Rows.Count > 0)
            {
                drpSE_TRANSFORMADOR.DataSource = dtSE_TRANSFORMADOR;
                drpSE_TRANSFORMADOR.DataValueField = "Cve_Transformador";
                drpSE_TRANSFORMADOR.DataTextField = "Dx_Dsc_Transformador";
                drpSE_TRANSFORMADOR.DataBind();
            }
            // drpSE_TRANSFORMADOR.Items.Insert(0, new ListItem("", ""));
            drpSE_TRANSFORMADOR.SelectedIndex = 0;
        }
        /// <summary>
        /// Initialize Fase del Transformador dropdownlist
        /// </summary>
        private void InitializeDrpSE_TRANSFORM_FASE()
        {
            DataTable dtSE_TRANSFORM_FASE = CAT_MARCADal.ClassInstance.Get_ALL_CAT_SE_TRANSFORM_FASEDal();
            if (dtSE_TRANSFORM_FASE != null && dtSE_TRANSFORM_FASE.Rows.Count > 0)
            {
                drpSE_TRANSFORM_FASE.DataSource = dtSE_TRANSFORM_FASE;
                drpSE_TRANSFORM_FASE.DataValueField = "Cve_Fase";
                drpSE_TRANSFORM_FASE.DataTextField = "Dx_Nombre_Fase";
                drpSE_TRANSFORM_FASE.DataBind();
            }
            // drpSE_TRANSFORM_FASE.Items.Insert(0, new ListItem("", ""));
            drpSE_TRANSFORM_FASE.SelectedIndex = 0;
        }
        /// <summary>
        /// Initialize Marca del Transformador dropdownlist
        /// </summary>
        private void InitializeDrpSE_TRANSFORM_MARCA()
        {
            DataTable dtSE_TRANSFORM_MARCA = CAT_MARCADal.ClassInstance.Get_ALL_CAT_SE_TRANSFORM_MARCADal();
            if (dtSE_TRANSFORM_MARCA != null && dtSE_TRANSFORM_MARCA.Rows.Count > 0)
            {
                drpSE_TRANSFORM_MARCA.DataSource = dtSE_TRANSFORM_MARCA;
                drpSE_TRANSFORM_MARCA.DataValueField = "Cve_Marca";
                drpSE_TRANSFORM_MARCA.DataTextField = "Dx_Nombre_Marca";
                drpSE_TRANSFORM_MARCA.DataBind();
            }
            // drpSE_TRANSFORM_MARCA.Items.Insert(0, new ListItem("", ""));
            drpSE_TRANSFORM_MARCA.SelectedIndex = 0;
        }
        /// <summary>
        /// Initialize Relación de Transformación dropdownlist
        /// </summary>
        private void InitializeDrpSE_TRANSFORM_RELACION()
        {
            DataTable dtSE_TRANSFORM_RELACION = CAT_MARCADal.ClassInstance.Get_ALL_CAT_SE_TRANSFORM_RELACIONDal();
            if (dtSE_TRANSFORM_RELACION != null && dtSE_TRANSFORM_RELACION.Rows.Count > 0)
            {
                drpSE_TRANSFORM_RELACION.DataSource = dtSE_TRANSFORM_RELACION;
                drpSE_TRANSFORM_RELACION.DataValueField = "Cve_Relacion";
                drpSE_TRANSFORM_RELACION.DataTextField = "Dx_Dsc_Relacion";
                drpSE_TRANSFORM_RELACION.DataBind();
            }
            // drpSE_TRANSFORM_RELACION.Items.Insert(0, new ListItem("", ""));
            drpSE_TRANSFORM_RELACION.SelectedIndex = 0;
        }
        /// <summary>
        /// Initialize Apartarrayo dropdownlist
        /// </summary>
        private void InitializeDrpSE_APARTARRAYO()
        {
            DataTable dtSE_APARTARRAYO = CAT_MARCADal.ClassInstance.Get_ALL_CAT_SE_APARTARRAYODal();
            if (dtSE_APARTARRAYO != null && dtSE_APARTARRAYO.Rows.Count > 0)
            {
                drpSE_APARTARRAYO.DataSource = dtSE_APARTARRAYO;
                drpSE_APARTARRAYO.DataValueField = "Cve_Apartarrayo";
                drpSE_APARTARRAYO.DataTextField = "Dx_Dsc_Apartarrayo";
                drpSE_APARTARRAYO.DataBind();
            }
            // drpSE_APARTARRAYO.Items.Insert(0, new ListItem("", ""));
            drpSE_APARTARRAYO.SelectedIndex = 0;
        }
        /// <summary>
        /// Initialize Marca del Apartarrayo dropdownlist
        /// </summary>
        private void InitializeDrpSE_APARTARRAYO_MARCA()
        {
            DataTable dtSE_APARTARRAYO_MARCA = CAT_MARCADal.ClassInstance.Get_ALL_CAT_SE_APARTARRAYO_MARCADal();
            if (dtSE_APARTARRAYO_MARCA != null && dtSE_APARTARRAYO_MARCA.Rows.Count > 0)
            {
                drpSE_APARTARRAYO_MARCA.DataSource = dtSE_APARTARRAYO_MARCA;
                drpSE_APARTARRAYO_MARCA.DataValueField = "Cve_Marca";
                drpSE_APARTARRAYO_MARCA.DataTextField = "Dx_Nombre_Marca";
                drpSE_APARTARRAYO_MARCA.DataBind();
            }
            // drpSE_APARTARRAYO_MARCA.Items.Insert(0, new ListItem("", ""));
            drpSE_APARTARRAYO_MARCA.SelectedIndex = 0;
        }
        /// <summary>
        /// Initialize Cortacircuito – Fusible dropdownlist
        /// </summary>
        private void InitializeDrpSE_CORTACIRCUITO()
        {
            DataTable dtSE_CORTACIRCUITO = CAT_MARCADal.ClassInstance.Get_ALL_CAT_SE_CORTACIRCUITODal();
            if (dtSE_CORTACIRCUITO != null && dtSE_CORTACIRCUITO.Rows.Count > 0)
            {
                drpSE_CORTACIRCUITO.DataSource = dtSE_CORTACIRCUITO;
                drpSE_CORTACIRCUITO.DataValueField = "Cve_Cortacircuito";
                drpSE_CORTACIRCUITO.DataTextField = "Dx_Dsc_Cortacircuito";
                drpSE_CORTACIRCUITO.DataBind();
            }
            // drpSE_CORTACIRCUITO.Items.Insert(0, new ListItem("", ""));
            drpSE_CORTACIRCUITO.SelectedIndex = 0;
        }
        /// <summary>
        /// Initialize Marca Cortacircuito – Fusible dropdownlist
        /// </summary>
        private void InitializeDrpSE_CORTACIRC_MARCA()
        {
            DataTable dtSE_CORTACIRC_MARCA = CAT_MARCADal.ClassInstance.Get_ALL_CAT_SE_CORTACIRC_MARCADal();
            if (dtSE_CORTACIRC_MARCA != null && dtSE_CORTACIRC_MARCA.Rows.Count > 0)
            {
                drpSE_CORTACIRC_MARCA.DataSource = dtSE_CORTACIRC_MARCA;
                drpSE_CORTACIRC_MARCA.DataValueField = "Cve_Marca";
                drpSE_CORTACIRC_MARCA.DataTextField = "Dx_Nombre_Marca";
                drpSE_CORTACIRC_MARCA.DataBind();
            }
            // drpSE_CORTACIRC_MARCA.Items.Insert(0, new ListItem("", ""));
            drpSE_CORTACIRC_MARCA.SelectedIndex = 0;
        }
        /// <summary>
        /// Initialize Interruptor Termomagnético dropdownlist
        /// </summary>
        private void InitializeDrpSE_INTERRUPTOR()
        {
            DataTable dtSE_INTERRUPTOR = CAT_MARCADal.ClassInstance.Get_ALL_CAT_SE_INTERRUPTORDal();
            if (dtSE_INTERRUPTOR != null && dtSE_INTERRUPTOR.Rows.Count > 0)
            {
                drpSE_INTERRUPTOR.DataSource = dtSE_INTERRUPTOR;
                drpSE_INTERRUPTOR.DataValueField = "Cve_Interruptor";
                drpSE_INTERRUPTOR.DataTextField = "Dx_Dsc_Interruptor";
                drpSE_INTERRUPTOR.DataBind();
            }
            // drpSE_INTERRUPTOR.Items.Insert(0, new ListItem("", ""));
            drpSE_INTERRUPTOR.SelectedIndex = 0;
        }
        /// <summary>
        /// Initialize Marca Interruptor Termomagnético dropdownlist
        /// </summary>
        private void InitializeDrpSE_INTERRUPTOR_MARCA()
        {
            DataTable dtSE_INTERRUPTOR_MARCA = CAT_MARCADal.ClassInstance.Get_ALL_CAT_SE_INTERRUPTOR_MARCADal();
            if (dtSE_INTERRUPTOR_MARCA != null && dtSE_INTERRUPTOR_MARCA.Rows.Count > 0)
            {
                drpSE_INTERRUPTOR_MARCA.DataSource = dtSE_INTERRUPTOR_MARCA;
                drpSE_INTERRUPTOR_MARCA.DataValueField = "Cve_Marca";
                drpSE_INTERRUPTOR_MARCA.DataTextField = "Dx_Nombre_Marca";
                drpSE_INTERRUPTOR_MARCA.DataBind();
            }
            // drpSE_INTERRUPTOR_MARCA.Items.Insert(0, new ListItem("", ""));
            drpSE_INTERRUPTOR_MARCA.SelectedIndex = 0;
        }
        /// <summary>
        /// Initialize Conductor de Tierra dropdownlist
        /// </summary>
        private void InitializeDrpSE_CONDUCTOR()
        {
            DataTable dtSE_CONDUCTOR = CAT_MARCADal.ClassInstance.Get_ALL_CAT_SE_CONDUCTORDal();
            if (dtSE_CONDUCTOR != null && dtSE_CONDUCTOR.Rows.Count > 0)
            {
                drpSE_CONDUCTOR.DataSource = dtSE_CONDUCTOR;
                drpSE_CONDUCTOR.DataValueField = "Cve_Conductor";
                drpSE_CONDUCTOR.DataTextField = "Dx_Dsc_Conductor";
                drpSE_CONDUCTOR.DataBind();
            }
            // drpSE_CONDUCTOR.Items.Insert(0, new ListItem("", ""));
            drpSE_CONDUCTOR.SelectedIndex = 0;
        }
        /// <summary>
        /// Initialize Marca Conductor de Tierra dropdownlist
        /// </summary>
        private void InitializeDrpSE_CONDUCTOR_MARCA()
        {
            DataTable dtSE_CONDUCTOR_MARCA = CAT_MARCADal.ClassInstance.Get_ALL_CAT_SE_CONDUCTOR_MARCADal();
            if (dtSE_CONDUCTOR_MARCA != null && dtSE_CONDUCTOR_MARCA.Rows.Count > 0)
            {
                drpSE_CONDUCTOR_MARCA.DataSource = dtSE_CONDUCTOR_MARCA;
                drpSE_CONDUCTOR_MARCA.DataValueField = "Cve_Marca";
                drpSE_CONDUCTOR_MARCA.DataTextField = "Dx_Nombre_Marca";
                drpSE_CONDUCTOR_MARCA.DataBind();
            }
            // drpSE_CONDUCTOR_MARCA.Items.Insert(0, new ListItem("", ""));
            drpSE_CONDUCTOR_MARCA.SelectedIndex = 0;
        }
        /// <summary>
        /// Initialize Conductor de Conexión a Centro de Carga dropdownlist
        /// </summary>
        private void InitializeDrpSE_COND_CONEXION()
        {
            DataTable dtSE_COND_CONEXION = CAT_MARCADal.ClassInstance.Get_ALL_CAT_SE_COND_CONEXIONDal();
            if (dtSE_COND_CONEXION != null && dtSE_COND_CONEXION.Rows.Count > 0)
            {
                drpSE_COND_CONEXION.DataSource = dtSE_COND_CONEXION;
                drpSE_COND_CONEXION.DataValueField = "Cve_Conductor_Conex";
                drpSE_COND_CONEXION.DataTextField = "Dx_Dsc_Conductor_Conex";
                drpSE_COND_CONEXION.DataBind();
            }
            // drpSE_COND_CONEXION.Items.Insert(0, new ListItem("", ""));
            drpSE_COND_CONEXION.SelectedIndex = 0;
        }
        /// <summary>
        /// Initialize Marca Conductor de Conexión dropdownlist
        /// </summary>
        private void InitializeDrpSE_COND_CONEXION_MARCA()
        {
            DataTable dtSE_COND_CONEXION_MARCA = CAT_MARCADal.ClassInstance.Get_ALL_CAT_SE_COND_CONEXION_MARCADal();
            if (dtSE_COND_CONEXION_MARCA != null && dtSE_COND_CONEXION_MARCA.Rows.Count > 0)
            {
                drpSE_COND_CONEXION_MARCA.DataSource = dtSE_COND_CONEXION_MARCA;
                drpSE_COND_CONEXION_MARCA.DataValueField = "Cve_Marca";
                drpSE_COND_CONEXION_MARCA.DataTextField = "Dx_Nombre_Marca";
                drpSE_COND_CONEXION_MARCA.DataBind();
            }
            // drpSE_COND_CONEXION_MARCA.Items.Insert(0, new ListItem("", ""));
            drpSE_COND_CONEXION_MARCA.SelectedIndex = 0;
        }
        // RSA 2012-09-12 Initialize SE combos End
        
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Page.Validate();

                if (Page.IsValid)
                {
                    int result = 0;
                    int flag = 0;//indicate new or edit, 0 indicates edit, 1 indicates new

                    CAT_PRODUCTOModel ProductInstance = new CAT_PRODUCTOModel();
                    CAT_TECNOLOGIAModel TechnologyModel = CAT_TECNOLOGIADAL.ClassInstance.Get_CAT_TECNOLOGIAByPKID(drpTechnology.SelectedValue);
                    ProductInstance.Cve_Tecnologia = (drpTechnology.SelectedIndex == 0 || drpTechnology.SelectedIndex == -1) ? 0 : int.Parse(this.drpTechnology.SelectedValue.ToString());
                    ProductInstance.Cve_Fabricante = (drpManufacture.SelectedIndex == 0 || drpManufacture.SelectedIndex == -1) ? 0 : int.Parse(drpManufacture.SelectedValue.ToString());
                    ProductInstance.Cve_Marca = (drpMarca.SelectedIndex == 0 || drpMarca.SelectedIndex == -1) ? 0 : int.Parse(drpMarca.SelectedValue.ToString());
                    ProductInstance.Ft_Tipo_Producto = (drpTipoProduct.SelectedIndex == 0 || drpTipoProduct.SelectedIndex == -1) ? 0 : int.Parse(drpTipoProduct.SelectedValue.ToString());
                    // RSA 2012-09-13 persist values, use 0 for those that doesn't apply
                    if (IsSE(drpTechnology.SelectedValue))
                    {
                        //int productId = string.IsNullOrEmpty(Request.QueryString["ProductID"]) ? 0 : int.Parse(Request.QueryString["ProductID"]);
                        //int index = 1 + CAT_PRODUCTODal.ClassInstance.GetSEProductComboMax(productId
                        //    , Convert.ToInt32(drpSE_TIPO.SelectedValue)
                        //    , Convert.ToInt32(drpSE_TRANSFORM_FASE.SelectedValue)
                        //    , Convert.ToInt32(drpSE_TRANSFORMADOR.SelectedValue));
                        ProductInstance.Dx_Nombre_Producto = "SUBESTACION ELECTRICA";
                        //ProductInstance.Dx_Modelo_Producto = drpSE_TIPO.SelectedItem.Text + " "
                        //     + drpSE_TRANSFORM_FASE.SelectedItem.Text + " "
                        //     + drpSE_TRANSFORMADOR.SelectedItem.Text
                        //     + (index == 1 ? string.Empty : " " + index.ToString());
                        ProductInstance.Dx_Modelo_Producto = txtModel.Text;

                        ProductInstance.Cve_Tipo_SE = (drpSE_TIPO.SelectedIndex == 0 || drpSE_TIPO.SelectedIndex == -1) ? 0 : int.Parse(drpSE_TIPO.SelectedValue.ToString());
                        ProductInstance.Cve_Transformador_SE = (drpSE_TRANSFORMADOR.SelectedIndex == 0 || drpSE_TRANSFORMADOR.SelectedIndex == -1) ? 0 : int.Parse(drpSE_TRANSFORMADOR.SelectedValue.ToString());
                        ProductInstance.Cve_Fase_Transformador = (drpSE_TRANSFORM_FASE.SelectedIndex == 0 || drpSE_TRANSFORM_FASE.SelectedIndex == -1) ? 0 : int.Parse(drpSE_TRANSFORM_FASE.SelectedValue.ToString());
                        ProductInstance.Cve_Marca_Transform = (drpSE_TRANSFORM_MARCA.SelectedIndex == 0 || drpSE_TRANSFORM_MARCA.SelectedIndex == -1) ? 0 : int.Parse(drpSE_TRANSFORM_MARCA.SelectedValue.ToString());
                        ProductInstance.Cve_Relacion_Transform = (drpSE_TRANSFORM_RELACION.SelectedIndex == 0 || drpSE_TRANSFORM_RELACION.SelectedIndex == -1) ? 0 : int.Parse(drpSE_TRANSFORM_RELACION.SelectedValue.ToString());
                        ProductInstance.Cve_Apartarrayos_SE = (drpSE_APARTARRAYO.SelectedIndex == 0 || drpSE_APARTARRAYO.SelectedIndex == -1) ? 0 : int.Parse(drpSE_APARTARRAYO.SelectedValue.ToString());
                        ProductInstance.Cve_Marca_Apartar = (drpSE_APARTARRAYO_MARCA.SelectedIndex == 0 || drpSE_APARTARRAYO_MARCA.SelectedIndex == -1) ? 0 : int.Parse(drpSE_APARTARRAYO_MARCA.SelectedValue.ToString());
                        ProductInstance.Cve_Cortacircuito_SE = (drpSE_CORTACIRCUITO.SelectedIndex == 0 || drpSE_CORTACIRCUITO.SelectedIndex == -1) ? 0 : int.Parse(drpSE_CORTACIRCUITO.SelectedValue.ToString());
                        ProductInstance.Cve_Marca_Cortacirc = (drpSE_CORTACIRC_MARCA.SelectedIndex == 0 || drpSE_CORTACIRC_MARCA.SelectedIndex == -1) ? 0 : int.Parse(drpSE_CORTACIRC_MARCA.SelectedValue.ToString());
                        ProductInstance.Cve_Interruptor_SE = (drpSE_INTERRUPTOR.SelectedIndex == 0 || drpSE_INTERRUPTOR.SelectedIndex == -1) ? 0 : int.Parse(drpSE_INTERRUPTOR.SelectedValue.ToString());
                        ProductInstance.Cve_Marca_Interrup = (drpSE_INTERRUPTOR_MARCA.SelectedIndex == 0 || drpSE_INTERRUPTOR_MARCA.SelectedIndex == -1) ? 0 : int.Parse(drpSE_INTERRUPTOR_MARCA.SelectedValue.ToString());
                        ProductInstance.Cve_Conductor_SE = (drpSE_CONDUCTOR.SelectedIndex == 0 || drpSE_CONDUCTOR.SelectedIndex == -1) ? 0 : int.Parse(drpSE_CONDUCTOR.SelectedValue.ToString());
                        ProductInstance.Cve_Marca_Conductor = (drpSE_CONDUCTOR_MARCA.SelectedIndex == 0 || drpSE_CONDUCTOR_MARCA.SelectedIndex == -1) ? 0 : int.Parse(drpSE_CONDUCTOR_MARCA.SelectedValue.ToString());
                        ProductInstance.Cve_Cond_Conex_SE = (drpSE_COND_CONEXION.SelectedIndex == 0 || drpSE_COND_CONEXION.SelectedIndex == -1) ? 0 : int.Parse(drpSE_COND_CONEXION.SelectedValue.ToString());
                        ProductInstance.Cve_Cond_Conex_Mca = (drpSE_COND_CONEXION_MARCA.SelectedIndex == 0 || drpSE_COND_CONEXION_MARCA.SelectedIndex == -1) ? 0 : int.Parse(drpSE_COND_CONEXION_MARCA.SelectedValue.ToString());
                    }
                    else
                    {
                        ProductInstance.Dx_Nombre_Producto = txtNameProduct.Text;
                        ProductInstance.Dx_Modelo_Producto = txtModel.Text;

                        ProductInstance.Cve_Tipo_SE = 0;
                        ProductInstance.Cve_Transformador_SE = 0;
                        ProductInstance.Cve_Fase_Transformador = 0;
                        ProductInstance.Cve_Marca_Transform = 0;
                        ProductInstance.Cve_Relacion_Transform = 0;
                        ProductInstance.Cve_Apartarrayos_SE = 0;
                        ProductInstance.Cve_Marca_Apartar = 0;
                        ProductInstance.Cve_Cortacircuito_SE = 0;
                        ProductInstance.Cve_Marca_Cortacirc = 0;
                        ProductInstance.Cve_Interruptor_SE = 0;
                        ProductInstance.Cve_Marca_Interrup = 0;
                        ProductInstance.Cve_Conductor_SE = 0;
                        ProductInstance.Cve_Marca_Conductor = 0;
                        ProductInstance.Cve_Cond_Conex_SE = 0;
                        ProductInstance.Cve_Cond_Conex_Mca = 0;
                    }
                    ProductInstance.Mt_Precio_Max = (txtMaxPrice.Text == "") ? 0 : decimal.Parse(txtMaxPrice.Text);
                    ProductInstance.No_Eficiencia_Energia = (txtEficience.Text == "") ? 0 : float.Parse(txtEficience.Text);
                    ProductInstance.No_Max_Consumo_24h = (txtConsumer.Text == "") ? 0 : float.Parse(txtConsumer.Text);
                    ProductInstance.Cve_Capacidad_Sust = (drpCapacidad.SelectedIndex == 0 || drpCapacidad.SelectedIndex == -1) ? 0 : int.Parse(drpCapacidad.SelectedValue.ToString());// updated by tina 2012-02-24
                    ProductInstance.Dt_Fecha_Producto = DateTime.Now.Date;
                    ProductInstance.Ahorro_Demanda = (txtAhorroDemanda.Text == "") ? 0 : float.Parse(txtAhorroDemanda.Text);
                    ProductInstance.Ahorro_Consumo = (txtAhorroConsumo.Text == "") ? 0 : float.Parse(txtAhorroConsumo.Text);
                    if (StatusID != 0)
                    {
                        ProductInstance.Cve_Estatus_Producto = StatusID;
                    }
                    else
                    {
                        ProductInstance.Cve_Estatus_Producto = (int)ProductStatus.ACTIVO;
                    }
                    if (!string.IsNullOrEmpty(Request.QueryString["ProductID"]))
                    {
                        ProductInstance.Cve_Producto = int.Parse(Request.QueryString["ProductID"]);
                        result = CAT_PRODUCTODal.ClassInstance.UpdateProductSInformation(ProductInstance);
                    }
                    else
                    {
                        flag = 1;

                        ProductInstance.Dx_Producto_Code = TechnologyModel.Dx_Cve_CC.ToString() + LsUtility.GetNumberSequence("ProductCod").ToString().PadLeft(6, '0');
                        result = CAT_PRODUCTODal.ClassInstance.Insert_CAT_PRODUCTO(ProductInstance);
                    }
                    if (result > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, typeof(Page), "AddEdit", "alert('Se ha guardado con éxito producto ')", true);
                        if (flag == 1)
                        {
                            ClearData();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, typeof(Page), "UpdateDatabaseError",
                                                                   "alert('" + ex.Message + "');", true);
            }
        }

        private void ClearData()
        {
            this.drpManufacture.SelectedIndex = -1;
            this.drpCapacidad.SelectedIndex = -1;
            this.drpMarca.SelectedIndex = -1;
            this.drpTechnology.SelectedIndex = -1;
            this.drpTipoProduct.SelectedIndex = -1;
            this.txtConsumer.Text = "";
            this.txtEficience.Text = "";
            this.txtMaxPrice.Text = "";
            this.txtModel.Text = "";
            this.txtNameProduct.Text = "";

            this.txtAhorroConsumo.Text = string.Empty;
            this.txtAhorroDemanda.Text = string.Empty;

            this.drpSE_TIPO.SelectedIndex = -1;
            this.drpSE_TRANSFORMADOR.SelectedIndex = -1;
            this.drpSE_TRANSFORM_FASE.SelectedIndex = -1;
            this.drpSE_TRANSFORM_MARCA.SelectedIndex = -1;
            this.drpSE_TRANSFORM_RELACION.SelectedIndex = -1;
            this.drpSE_APARTARRAYO.SelectedIndex = -1;
            this.drpSE_APARTARRAYO_MARCA.SelectedIndex = -1;
            this.drpSE_CORTACIRCUITO.SelectedIndex = -1;
            this.drpSE_CORTACIRC_MARCA.SelectedIndex = -1;
            this.drpSE_INTERRUPTOR.SelectedIndex = -1;
            this.drpSE_INTERRUPTOR_MARCA.SelectedIndex = -1;
            this.drpSE_CONDUCTOR.SelectedIndex = -1;
            this.drpSE_CONDUCTOR_MARCA.SelectedIndex = -1;
            this.drpSE_COND_CONEXION.SelectedIndex = -1;
            this.drpSE_COND_CONEXION_MARCA.SelectedIndex = -1;
        }
        /// <summary>
        /// return Product monitor page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProductMonitor.aspx");
        }
        /// <summary>
        /// Technology changed Initialize Product type
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpTechnology_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeDrpTipoProduct();
            InitializeDrpCapacidad(); // added by tina 2012-02-24

            // RSA 2012-09-11 SE fields 
            if (IsSE(drpTechnology.SelectedValue))
            {
                ShowSE(true);
                drpTipoProduct.SelectedValue = drpTipoProduct.Items.FindByText("SUBESTACION ELECTRICA").Value;
                //drpMarca.SelectedValue = drpMarca.Items.FindByText("SUBESTACION ELECTRICA").Value;
            }
            else
            {
                ShowSE(false);
                drpTipoProduct.SelectedIndex = 0;
                //drpMarca.SelectedIndex = 0;
            }

        }
        private void ShowSE(bool show)
        {
            PanelSE.Enabled = show;
            PanelSE.Visible = show;
            PanelNotSE.Visible = !show;
            PanelNotSE.Enabled = !show;
        }
        private bool IsSE(string techId)
        {
            return DxCveCC.ContainsKey(techId) && DxCveCC[techId] == DxCveCC_SE;
        }
    }
}
