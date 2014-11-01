using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.BussinessLayer;
using PAEEEM.DataAccessLayer;
using PAEEEM.Helpers;
using PAEEEM.Entities;

namespace PAEEEM.RegionalModule
{
    public partial class AssignTechnologiesToDisposal : System.Web.UI.Page
    {
        #region Global Variables
        private int DisposalCenterId
        {
            get
            {
                return ViewState["DisposalID"] == null ? 0 : Convert.ToInt32(ViewState["DisposalID"].ToString());
            }
            set
            {
                ViewState["DisposalID"] = value;
            }
        }
        private string DisposalCenterType
        {
            get
            {
                return ViewState["Type"] == null ? "" : ViewState["Type"].ToString();
            }
            set
            {
                ViewState["Type"] = value;
            }
        }
        private bool ExistAssignedTechnology
        {
            get
            {
                return ViewState["Flag"] == null ? false : Convert.ToBoolean(ViewState["Flag"]);
            }
            set
            {
                ViewState["Flag"] = value;
            }
        }
        private ArrayList AssignedTechnology
        {
            get
            {
                return ViewState["AssignedTechnology"] == null ? new ArrayList() : ViewState["AssignedTechnology"] as ArrayList;
            }
            set
            {
                ViewState["AssignedTechnology"] = value;
            }
        }
        #endregion

        #region Initialize Components
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserInfo"] == null)
                {
                    Response.Redirect("../Login/Login.aspx");
                    return;
                }

                //Setup default date to date control for displaying
                this.literalFecha.Text = DateTime.Now.ToString("dd-MM-yyyy");

                //Check if it is new added or editing
                if (Request.QueryString["DisposalID"] != null && Request.QueryString["DisposalID"].ToString() != "" && Request.QueryString["Type"] != null && Request.QueryString["Type"].ToString() != "")
                {
                    DisposalCenterId = Convert.ToInt32(System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["DisposalID"].ToString().Replace("%2B", "+"))));
                    DisposalCenterType = System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["Type"].ToString().Replace("%2B", "+")));

                    //Load current data
                    LoadData();
                }
            }
        }

        private void LoadData()
        {
            InitializeHeaderSection();
            //Load right side
            LoadRelatedTechnologyList();
            //Load left side
            LoadTechnologyList();
        }

        private void InitializeHeaderSection()
        {
            DataTable disposalCenters = null;

            if (DisposalCenterType.ToLower() == "matriz") // updated by tina 2012-02-29
            {
                disposalCenters = CAT_CENTRO_DISPDAL.ClassInstance.GetDisposalsByPk(DisposalCenterId);
            }
            else
            {
                disposalCenters = CAT_CENTRO_DISP_SUCURSALDAL.ClassInstance.GetDisposalByBranch(DisposalCenterId);
            }

            //setup header section information
            if (disposalCenters != null && disposalCenters.Rows.Count > 0)
            {
                txtSociaNamel.Text = disposalCenters.Rows[0]["Dx_Razon_Social"].ToString();
                txtBusinessName.Text = disposalCenters.Rows[0]["Dx_Nombre_Comercial"].ToString();
            }
        }

        private void LoadTechnologyList()
        {
            // RSA 20120926 solo sustitución
            // DataTable technologies = CAT_TECNOLOGIADAL.ClassInstance.Get_All_CAT_TECNOLOGIAByProgram(Global.PROGRAM.ToString());
            DataTable technologies = CAT_TECNOLOGIADAL.ClassInstance.Get_ALL_Material_Technology();
            if (technologies != null)
            {
                //Clear the assigned technology from the total technology
                DataTable temp = technologies.Clone();
                foreach (DataRow dataRow in technologies.Rows)
                {
                    if (this.listSelectedTechnology.Items.FindByText(dataRow["Dx_Nombre_General"].ToString()) == null)
                    {
                        temp.Rows.Add(new object[] { dataRow["Cve_Tecnologia"].ToString(), dataRow["Dx_Nombre_General"].ToString() });
                    }
                }

                //Bind data
                this.listTechnology.DataSource = temp;
                this.listTechnology.DataTextField = "Dx_Nombre_General";
                this.listTechnology.DataValueField = "Cve_Tecnologia";
                listTechnology.DataBind();
            }
        }

        private void LoadRelatedTechnologyList()
        {
            DataTable relatedTechnologies = null;

            if (DisposalCenterType.ToLower() == "matriz") // updated bu tina 2012-02-29
            {
                relatedTechnologies = CAT_CENTRO_DISP_SUCURSALDAL.ClassInstance.GetTechnologyByDisposal(GlobalVar.DISPOSAL_CENTER, DisposalCenterId);
            }
            else
            {
                relatedTechnologies = CAT_CENTRO_DISP_SUCURSALDAL.ClassInstance.GetTechnologyByDisposal(GlobalVar.DISPOSAL_CENTER_BRANCH, DisposalCenterId);
            }

            if (relatedTechnologies != null && relatedTechnologies.Rows.Count > 0)
            {
                ExistAssignedTechnology = true;//Has related technology

                //Cache the assigned technology to this disposal center
                AssignedTechnology = new ArrayList();

                foreach (DataRow row in relatedTechnologies.Rows)
                {
                    AssignedTechnology.Add(row["Cve_Tecnologia"].ToString());
                }

                //Bind data
                listSelectedTechnology.DataSource = relatedTechnologies;
                listSelectedTechnology.DataTextField = "Dx_Nombre_General";
                listSelectedTechnology.DataValueField = "Cve_Tecnologia";
                listSelectedTechnology.DataBind();                
            }
        }
        #endregion

        #region Private methods
        private void GetData(out List<K_CENTRO_DISP_TECNOLOGIAModel> listAssignedTechnology, out List<string[]> listDelete)
        {
            listAssignedTechnology = new List<K_CENTRO_DISP_TECNOLOGIAModel>();
            listDelete = new List<string[]>();

            if (ExistAssignedTechnology)
            {
                foreach (ListItem item in listSelectedTechnology.Items)
                {
                    if (!AssignedTechnology.Contains(item.Value))
                    {
                        K_CENTRO_DISP_TECNOLOGIAModel model = new K_CENTRO_DISP_TECNOLOGIAModel();
                        model.Id_Centro_Disp = DisposalCenterId;
                        model.Cve_Tecnologia = Convert.ToInt32(item.Value);
                        model.Cve_Estatus_CD_Tec = 1;
                        model.Dt_Fecha_CD_Tec = DateTime.Now;
                        model.Fg_Tipo_Centro_Disp = DisposalCenterType.ToLower() == "matriz" ? "M" : "B"; // updated by tina 2012-02-29

                        listAssignedTechnology.Add(model);
                    }
                    else
                    {
                        AssignedTechnology.Remove(item.Value);
                    }
                }

                for (int i = 0; i < AssignedTechnology.Count; i++)
                {
                    string[] para = new string[3];
                    para[0] = DisposalCenterId.ToString();
                    para[1] = AssignedTechnology[i].ToString();
                    para[2] = DisposalCenterType.ToLower() == "matriz" ? "M" : "B"; // updated by tina 2012-02-29

                    listDelete.Add(para);
                }
            }
            else
            {
                foreach (ListItem item in listSelectedTechnology.Items)
                {
                    K_CENTRO_DISP_TECNOLOGIAModel model = new K_CENTRO_DISP_TECNOLOGIAModel();
                    model.Id_Centro_Disp = DisposalCenterId;
                    model.Cve_Tecnologia = Convert.ToInt32(item.Value);
                    model.Cve_Estatus_CD_Tec = 0;
                    model.Dt_Fecha_CD_Tec = DateTime.Now;
                    model.Fg_Tipo_Centro_Disp = DisposalCenterType.ToLower() == "matriz" ? "M" : "B"; // updated by tina 2012-02-29

                    listAssignedTechnology.Add(model);
                }
            }
        }

        private void RefreshLeftSection(ArrayList arrNames)
        {
            foreach (string tech in arrNames)
            {
                this.listTechnology.Items.Remove(this.listTechnology.Items.FindByText(tech));
            }
        }

        private void RefreshRightSection(ArrayList arrNames)
        {
            foreach (string tech in arrNames)
            {
                this.listTechnology.Items.Add(tech);
            }
        }
        #endregion

        #region Button Events
        protected void btnSelectMultiple_Click(object sender, EventArgs e)
        {
            ArrayList technologies = new ArrayList();

            foreach (ListItem item in listTechnology.Items)
            {
                if (item.Selected)
                {
                    if (!listSelectedTechnology.Items.Contains(item))
                    {
                        listSelectedTechnology.Items.Add(item);
                        //Cache moved items
                        technologies.Add(item.Text);
                    }
                }
            }

            //Refresh
            this.RefreshLeftSection(technologies);

            listSelectedTechnology.SelectedIndex = -1;
        }

        protected void btnRemoveMultiple_Click(object sender, EventArgs e)
        {
            ArrayList technologies = new ArrayList();

            for (int i = listSelectedTechnology.Items.Count - 1; i >= 0; i--)
            {
                if (listSelectedTechnology.Items[i].Selected)
                {
                    //Cache moved items
                    technologies.Add(listSelectedTechnology.Items[i].Text);
                    //Removed
                    listSelectedTechnology.Items.RemoveAt(i);                    
                }
            }

            //Refresh
            this.RefreshRightSection(technologies);

            listTechnology.SelectedIndex = -1;
        }

        protected void btnSelectAll_Click(object sender, EventArgs e)
        {
            ArrayList technologies = new ArrayList();

            foreach (ListItem itemLeft in listTechnology.Items)
            {
                if (!listSelectedTechnology.Items.Contains(itemLeft))
                {
                    listSelectedTechnology.Items.Add(itemLeft);
                    //Cache
                    technologies.Add(itemLeft.Text);
                }
            }

            //Refresh
            this.RefreshLeftSection(technologies);

            listSelectedTechnology.SelectedIndex = -1;
        }

        protected void btnRemoveAll_Click(object sender, EventArgs e)
        {
            ArrayList technologies = new ArrayList();

            foreach (ListItem item in this.listSelectedTechnology.Items)
            {
                technologies.Add(item.Text);
            }
            //Removed
            listSelectedTechnology.Items.Clear();
            //Refresh
            this.RefreshRightSection(technologies);

            listTechnology.SelectedIndex = -1;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int result = 0;
            List<K_CENTRO_DISP_TECNOLOGIAModel> listAssignedTechnology = new List<K_CENTRO_DISP_TECNOLOGIAModel>();
            List<string[]> listDelete = new List<string[]>();

            GetData(out listAssignedTechnology, out listDelete);

            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                if (listAssignedTechnology != null && listAssignedTechnology.Count > 0)
                {
                    result = K_CENTRO_DISP_TECNOLOGIABLL.ClassInstance.Insert_K_CENTRO_DISP_TECNOLOGIA(listAssignedTechnology);
                }
                if (listDelete != null && listDelete.Count > 0)
                {
                    result += K_CENTRO_DISP_TECNOLOGIABLL.ClassInstance.Delete_K_CENTRO_DISP_TECNOLOGIA(listDelete);
                }
                scope.Complete();
            }
            if (result > 0)
            {
                //Cache the assigned technology to this disposal center
                AssignedTechnology = new ArrayList();

                if (this.listSelectedTechnology.Items.Count > 0)
                {
                    ExistAssignedTechnology = true;//Has related technology
                    foreach (ListItem item in this.listSelectedTechnology.Items)
                    {
                        AssignedTechnology.Add(item.Value.ToString());
                    }
                }
                else
                {
                    ExistAssignedTechnology = false;//Has not related technology
                }

                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "ChangesSaved", "alert('Los cambios han sido guardados.');", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "NoChanges", "alert('No hubo cambios.');", true);
            }
        }        

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("DisposalCenterMonitor.aspx");
        } 
        #endregion
    }
}
