using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.BussinessLayer;
using PAEEEM.DataAccessLayer;
using PAEEEM.Entities;
using PAEEEM.Helpers;

namespace PAEEEM.DisposalModule
{
    public partial class MaterialRecoveryRegistryMonitor : System.Web.UI.Page
    {
        private int MaterialAmountOfTechnology // added by tina 2012-03-13
        {
            get { return ViewState["MaterialAmountOfTechnology"] == null ? 6 : Convert.ToInt32(ViewState["MaterialAmountOfTechnology"]); }
            set { ViewState["MaterialAmountOfTechnology"] = value; }
        }
        #region Initialize Components
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //check if session is null, if true return to login screen
                if (null == Session["UserInfo"])
                {
                    Response.Redirect("../Login/Login.aspx");
                    return;
                }
                //setup current date to date control
                this.literalFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");                
                //load top section filter conditions
                InitializeFilterConditions();
                //add by coco 2012-04-13
                this.txtRegistryToDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                if (!string.IsNullOrEmpty(Request.QueryString["Flag"]) && Request.QueryString["Flag"] == "0")
                {
                    btnSearch_Click(new object(), null);
                }
                else
                {
                    InitializeFromDate();                    
                }                
                //end add
            }
        }

        /// <summary>
        /// Init drop down list data 
        /// </summary>
        private void InitializeFilterConditions()
        {
            InitializePrograms();
            this.drpProgram.SelectedIndex = Session["CurrentProgramMaterialMonitor"] != null ? (int)Session["CurrentProgramMaterialMonitor"] : 0;
            InitializeTechnologies();
            this.drpTechnology.SelectedIndex = Session["CurrentTechnologyMaterialMonitor"] != null ? (int)Session["CurrentTechnologyMaterialMonitor"] : 0;
            InitializeMaterialTypes();
            this.drpMaterialType.SelectedIndex = Session["CurrentMaterialMaterialMonitor"] != null ? (int)Session["CurrentMaterialMaterialMonitor"] : 0;
            this.txtRegistryDate.Text = Session["CurrentRegistryDateMaterialMonitor"] != null ? (string)Session["CurrentRegistryDateMaterialMonitor"] :"";// updated by tina 2012/04/12
            AspNetPager.Visible = false;
            // added by tina 2012/04/12
            this.txtRegistryToDate.Text = Session["CurrentRegistryToDateMaterialMonitor"] != null ? (string)Session["CurrentRegistryToDateMaterialMonitor"] : "";
            // end
        }

        private void InitializePrograms()
        {
            DataTable programs = null;
            programs = CAT_PROGRAMADal.ClassInstance.GetPrograms();

            if (programs != null)
            {
                drpProgram.DataSource = programs;
                drpProgram.DataTextField = "Dx_Nombre_Programa";
                drpProgram.DataValueField = "ID_Prog_Proy";
                drpProgram.DataBind();
                drpProgram.Items.Insert(0, new ListItem(""));
            }
        }

        private void InitializeTechnologies()
        {
            DataTable technologies = null;
            US_USUARIOModel UserModel = Session["UserInfo"] as US_USUARIOModel;

            if (UserModel != null)
            {
                //get filter condition program
                string program = (drpProgram.SelectedIndex == -1 || drpProgram.SelectedIndex == 0) ? "" : drpProgram.SelectedValue;
                if (program != "")
                {
                    technologies = CAT_TECNOLOGIADAL.ClassInstance.GetTechnologyWithProgramandDisposalCenter(program, UserModel.Id_Departamento, UserModel.Tipo_Usuario == GlobalVar.DISPOSAL_CENTER ? "M" : "B");
                }
                else
                {
                    technologies = CAT_TECNOLOGIADAL.ClassInstance.GetDisposalCenterRelatedTechnology(UserModel.Id_Departamento, UserModel.Tipo_Usuario == GlobalVar.DISPOSAL_CENTER ? "M" : "B");
                }

                //bind technologies
                if (technologies != null)
                {
                    drpTechnology.DataSource = technologies;
                    drpTechnology.DataTextField = "Dx_Nombre_General";
                    drpTechnology.DataValueField = "Cve_Tecnologia";
                    drpTechnology.DataBind();
                    drpTechnology.Items.Insert(0, new ListItem(""));
                }
            }
        }

        private void InitializeMaterialTypes()
        {
            DataTable materialOptions = null;
            string technology = (drpTechnology.SelectedIndex == -1) ? "" : drpTechnology.SelectedValue;

            //retrieve materials related to specific technology
            materialOptions = CAT_RESIDUO_MATERIALDal.ClassInstance.GetMaterialTypeByTechnology(technology);

            if (materialOptions != null)
            {
                drpMaterialType.DataSource = materialOptions;
                drpMaterialType.DataTextField = "Dx_Residuo_Material_Gral";
                drpMaterialType.DataValueField = "Cve_Residuo_Material";
                drpMaterialType.DataBind();
                drpMaterialType.Items.Insert(0, new ListItem(""));
            }
        }

        // added by tina 2012/04/12
        private void InitializeFromDate()
        {
            US_USUARIOModel UserModel = Session["UserInfo"] as US_USUARIOModel;

            if (UserModel != null)
            {
                string program = (this.drpProgram.SelectedIndex == 0 || this.drpProgram.SelectedIndex == -1) ? "" : this.drpProgram.SelectedValue;
                string technology = this.drpTechnology.SelectedIndex == -1 ? "" : this.drpTechnology.SelectedValue;
                int disposalCenterId = UserModel.Id_Departamento;
                string disposalCenterType = UserModel.Tipo_Usuario == GlobalVar.DISPOSAL_CENTER ? "M" : "B";
                string material = (drpMaterialType.SelectedIndex == -1 || drpMaterialType.SelectedIndex == 0) ? "" : drpMaterialType.SelectedValue;

                txtRegistryDate.Text = K_RECUPERACIONDal.ClassInstance.GetLastMaterialRecoveryDateByTechnologyAndMaterial(program, technology, material, disposalCenterId, disposalCenterType).ToString("yyyy-MM-dd");
            }
        }
        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //check if program and technology was selected, if no, do nothing
            if (this.drpProgram.SelectedIndex > 0 && this.drpTechnology.SelectedIndex > 0) // updated by tina 2012/04/12
            {
                //Cache filter conditions for using when grid view page changes
                CacheFilterConditions();

                //refresh grid view data
                LoadGridViewData();

                this.AspNetPager.GoToPage(1);

                // added by tina 2012-03-13
                // get material max order by technology
                //updated by tina 2012-08-10
                MaterialAmountOfTechnology = K_TECNOLOGIA_MATERIALDAL.ClassInstance.GetMaterialMaxOrderByTechnology(Convert.ToInt32(this.drpTechnology.SelectedValue));
            }
            else
            {
                if (!Page.ClientScript.IsClientScriptBlockRegistered("FilterConditionMissing"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "FilterConditionMissing", "alert('¡ Es obligatoria la selección de los Filtros: Programa, Tecnología y Fecha de Recuperación !');", true);
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("../default.aspx");
        }

        private void CacheFilterConditions()
        {
            Session["CurrentProgramMaterialMonitor"] = drpProgram.SelectedIndex;
            Session["CurrentTechnologyMaterialMonitor"] = drpTechnology.SelectedIndex;
            Session["CurrentMaterialMaterialMonitor"] = drpMaterialType.SelectedIndex;
            Session["CurrentRegistryDateMaterialMonitor"] = txtRegistryDate.Text.Trim();
        }

        private void LoadGridViewData()
        {
            US_USUARIOModel UserModel = Session["UserInfo"] as US_USUARIOModel;

            if (UserModel != null)
            {
                int PageCount = 0;
                DataTable materials = null;

                //get filter conditions
                string program = (this.drpProgram.SelectedIndex == 0 || this.drpProgram.SelectedIndex == -1) ? "" : this.drpProgram.SelectedValue;
                string technology = this.drpTechnology.SelectedIndex == -1 ? "" : this.drpTechnology.SelectedValue;
                string registryDate = this.txtRegistryDate.Text.Trim();
                int disposalCenterId = UserModel.Id_Departamento;
                string disposalCenterType = UserModel.Tipo_Usuario == GlobalVar.DISPOSAL_CENTER ? "M" : "B";
                string material = (drpMaterialType.SelectedIndex == -1 || drpMaterialType.SelectedIndex == 0) ? "" : drpMaterialType.SelectedValue;
                string registryToDate = this.txtRegistryToDate.Text.Trim(); // added by tina 2012/04/12

                // updated by tina 2012/04/12
                //retrieve materials
                materials = K_RECUPERACIONDal.ClassInstance.GetRecoveryMaterials(program, technology, registryDate, registryToDate, material, disposalCenterId, disposalCenterType, ""/*sort string*/, this.AspNetPager.CurrentPageIndex, this.AspNetPager.PageSize, out PageCount);

                //bind materials to grid view control
                if (materials != null)
                {
                    if (materials.Rows.Count == 0)
                    {
                        ViewState["emptyRow"] = true;
                        materials.Rows.Add(materials.NewRow());
                    }

                    //binding
                    this.AspNetPager.RecordCount = PageCount;
                    this.grdRecoveryMaterial.DataSource = materials;
                    this.grdRecoveryMaterial.DataBind();
                }
                //visible grid view section
                AspNetPager.Visible = true;
            }
        }

        protected void grdRecoveryMaterial_DataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow Row in grdRecoveryMaterial.Rows)
            {
                LinkButton btnEdit = (LinkButton)Row.FindControl("btnEdit");
                LinkButton btnDelete = (LinkButton)Row.FindControl("btnDelete");

                //check if is empty row
                if (ViewState["emptyRow"] != null)
                {
                    if ((bool)ViewState["emptyRow"])
                    {
                        btnEdit.Visible = false;
                        btnDelete.Visible = false;

                        //reset empty row value
                        ViewState["emptyRow"] = false;
                    }
                    else // updated by tina 2012/04/13
                    {
                        btnEdit.Visible = true;
                        if (int.Parse(this.grdRecoveryMaterial.DataKeys[Row.RowIndex][2].ToString()) < 3)
                        {
                            btnDelete.Visible = true;
                        }
                        else
                        {
                            btnDelete.Visible = false;
                        }
                        //reset empty row value
                        ViewState["emptyRow"] = true;
                    }
                }
            }
        }

        protected void AspNetPager_PageChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                LoadGridViewData();
            }
        }

        protected void AspNetPager_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            if (IsPostBack)
            {
                this.drpProgram.SelectedIndex = Session["CurrentProgramMaterialMonitor"] != null ? (int)Session["CurrentProgramMaterialMonitor"] : 0;
                this.drpTechnology.SelectedIndex = Session["CurrentTechnologyMaterialMonitor"] != null ? (int)Session["CurrentTechnologyMaterialMonitor"] : 0;
                this.drpMaterialType.SelectedIndex = Session["CurrentMaterialMaterialMonitor"] != null ? (int)Session["CurrentMaterialMaterialMonitor"] : 0;
                this.txtRegistryDate.Text = Session["CurrentRegistryDateMaterialMonitor"] != null ? (string)Session["CurrentRegistryDateMaterialMonitor"] : DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        protected void drpProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeTechnologies();
            InitializeMaterialTypes();
            InitializeFromDate(); // added by tina 2012/04/13
        }

        protected void drpTechnology_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeMaterialTypes();
            InitializeFromDate(); // added by tina 2012/04/13
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            GridViewRow Row = (GridViewRow)((LinkButton)sender).NamingContainer;
            if (Row.Cells[7].Text.Replace("&nbsp;", "").Trim() == "")
            {
                if (this.grdRecoveryMaterial.DataKeys[Row.RowIndex][1].ToString() == "" || this.grdRecoveryMaterial.DataKeys[Row.RowIndex][1].ToString() == "0")
                {
                    string Id_Recuperacion = this.grdRecoveryMaterial.DataKeys[Row.RowIndex][0].ToString();
                    Response.Redirect("MaterialRecoveryRegistryEdit.aspx?Id_Recuperacion=" + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(Id_Recuperacion)).Replace("+", "%2B"));
                }
                else
                {
                    if (!Page.ClientScript.IsClientScriptBlockRegistered("EditForbidden"))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "EditForbidden", "alert('¡ No puede ser Editar éste Lote porque cuenta con Registro de Inspección !');", true);
                    }
                }
            }
            else
            {
                if (!Page.ClientScript.IsClientScriptBlockRegistered("EditForbidden"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "EditForbidden", "alert('¡ No puede ser Editar éste Lote porque cuenta con Acta Circunstanciada !');", true);
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            GridViewRow Row = (GridViewRow)((LinkButton)sender).NamingContainer;            

            if (Row.Cells[7].Text.Replace("&nbsp;", "").Trim() == "")
            {
                if (this.grdRecoveryMaterial.DataKeys[Row.RowIndex][1].ToString() == "" || this.grdRecoveryMaterial.DataKeys[Row.RowIndex][1].ToString() == "0")
                {
                    if (!IsRecoveryAllMaterial(this.grdRecoveryMaterial.DataKeys[Row.RowIndex][0].ToString()))
                    {
                        // Updated By Tina 2012-03-27
                        string Id_Recuperacion = this.grdRecoveryMaterial.DataKeys[Row.RowIndex][0].ToString();
                        int result = K_RECUPERACION_PRODUCTOBLL.ClassInstance.DeleteDataById_Recuperacion(Id_Recuperacion);

                        if (result > 0)
                        {
                            LoadGridViewData();
                            this.AspNetPager.GoToPage(1);
                        }
                        // End
                    }
                    else
                    {
                        if (!Page.ClientScript.IsClientScriptBlockRegistered("DeleteForbidden"))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "DeleteForbidden", "alert('¡ No puede ser Borrado éste Lote porque cuenta con Registro de Recuperación Completo !');", true);
                        }
                    }
                }
                else
                {
                    if (!Page.ClientScript.IsClientScriptBlockRegistered("DeleteForbidden"))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "DeleteForbidden", "alert('¡ No puede ser Borrado éste Lote porque cuenta con Registro de Inspección !');", true);
                    }
                }
            }
            else
            {
                if (!Page.ClientScript.IsClientScriptBlockRegistered("DeleteForbidden"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "DeleteForbidden", "alert('¡ No puede ser Borrado éste Lote porque cuenta con Acta Circunstanciada !');", true);
                }
            }
        }

        private bool IsRecoveryAllMaterial(string Id_Recuperacion)
        {
            bool result = false;

            int maxMaterialAmount = K_RECUPERACIONDal.ClassInstance.GetRecuperacionProductMaxRecoveryMaterialAmount(Id_Recuperacion);
            if (maxMaterialAmount == MaterialAmountOfTechnology)
            {
                result = true;
            }

            return result;
        }

        // added by tina 2012/04/13
        protected void drpMaterialType_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeFromDate();
        }
    }
}
