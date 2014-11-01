using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using PAEEEM.DataAccessLayer;
using PAEEEM.Entities;
using PAEEEM.Helpers;

namespace PAEEEM.DisposalModule
{
    public partial class DisposedProductsMonitor : System.Web.UI.Page
    {
        #region Global Variables
        /// <summary>
        /// property
        /// </summary>
        private string All_Technologies
        {
            get
            {
                return ViewState["All_Technology"] == null ? "" : ViewState["All_Technology"].ToString();
            }
            set
            {
                ViewState["All_Technology"] = value;
            }
        }
        #endregion

        /// <summary>
        /// Init Default Data When page Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (null == Session["UserInfo"])
                {
                    Response.Redirect("../Login/Login.aspx");
                    return;
                }
                //Init date control
                this.literalFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
                //Session is not null to load default data                
                InitDropDownList();
                LoadGridViewData();
            }
        }

        /// <summary>
        /// Init drop down list data
        /// </summary>
        private void InitDropDownList()
        {
            InitReceiptDate();
            InitTechnology();
            InitEstatus();
        }

        /// <summary>
        /// Load date of receipt
        /// </summary>
        private void InitReceiptDate()
        {
            DataTable dtReceiptDate = K_CREDITO_SUSTITUCIONDAL.ClassInstance.GetReceiptDate();
            if (dtReceiptDate != null)
            {
                this.drpReceiptDate.DataSource = dtReceiptDate;
                this.drpReceiptDate.DataTextField = "Dt_Fecha_Recepcion";
                this.drpReceiptDate.DataBind();
                this.drpReceiptDate.Items.Insert(0, new ListItem(""));
            }
        }

        /// <summary>
        /// Load technology
        /// </summary>
        private void InitTechnology()
        {
            DataTable dtTechnology = null;
            US_USUARIOModel UserModel = null;
            if (Session["UserInfo"] != null)
            {
                UserModel = Session["UserInfo"] as US_USUARIOModel;
                //load related technology for disposal main center and branch separately
                dtTechnology = CAT_CENTRO_DISP_SUCURSALDAL.ClassInstance.GetTechnologyByDisposal(UserModel.Tipo_Usuario, UserModel.Id_Departamento);

                if (dtTechnology != null)
                {
                    this.drpTechnology.DataSource = dtTechnology;
                    this.drpTechnology.DataTextField = "Dx_Nombre_General";
                    this.drpTechnology.DataValueField = "Cve_Tecnologia";
                    this.drpTechnology.DataBind();
                    this.drpTechnology.Items.Insert(0, new ListItem(""));

                    // all technology
                    foreach (DataRow dr in dtTechnology.Rows)
                    {
                        All_Technologies += dr["Cve_Tecnologia"].ToString() + ",";
                    }
                    //trim the last comma
                    if (All_Technologies != string.Empty)
                    {
                        All_Technologies = All_Technologies.TrimEnd(',');
                    }
                }
            }
        }

        /// <summary>
        /// Load estatus
        /// </summary>
        private void InitEstatus()
        {
            this.drpEstatus.Items.Add("");
            this.drpEstatus.Items.Add("EN RECEPCION");
            this.drpEstatus.Items.Add("PARA INHABILITACION");
            this.drpEstatus.Items.Add("INHABILITADO");
            this.drpEstatus.Items.Add("RECUPERACION DE RESIDUOS");
        }

        /// <summary>
        /// Load default data when page load
        /// </summary>
        private void LoadGridViewData()
        {
            US_USUARIOModel UserModel = null;
            UserModel = Session["UserInfo"] as US_USUARIOModel;

            int PageCount = 0;
            DataTable dtDisposedProducts = null;

            string receiptDate = (this.drpReceiptDate.SelectedIndex == 0 || this.drpReceiptDate.SelectedIndex == -1) ? "" : this.drpReceiptDate.SelectedItem.Text;
            string technology = (this.drpTechnology.SelectedIndex == 0 || this.drpTechnology.SelectedIndex == -1) ? All_Technologies : this.drpTechnology.SelectedValue;

            int estatus = 0;
            if (this.drpEstatus.SelectedIndex != 0 && this.drpEstatus.SelectedIndex != -1)
            {
                switch (this.drpEstatus.Text)
                {
                    case "EN RECEPCION":
                        estatus = (int)DisposalStatus.ENRECEPCION;
                        break;
                    case "PARA INHABILITACION":
                        estatus = (int)DisposalStatus.PARAINHABILITACION;
                        break;
                    case "INHABILITADO":
                        estatus = (int)DisposalStatus.INHABILITADO;
                        break;
                    case "RECUPERACION DE RESIDUOS":
                        estatus = (int)DisposalStatus.RECUPERACIONDERESIDUOS;
                        break;
                    default:
                        break;
                }
            }
            //retrieve the user type
            string DisposalType = string.Empty;
            if (UserModel.Tipo_Usuario == GlobalVar.DISPOSAL_CENTER)
            {
                DisposalType = "M";
            }
            else
            {
                DisposalType = "B";
            }
            //retrieve the disposed products belong to this specific disposal center
            dtDisposedProducts = K_CREDITO_SUSTITUCIONDAL.ClassInstance.GetDisposedProducts(UserModel.Id_Departamento,DisposalType, receiptDate, technology, estatus, "", this.AspNetPager.CurrentPageIndex, this.AspNetPager.PageSize, out PageCount);
            if (dtDisposedProducts != null)
            {
                if (dtDisposedProducts.Rows.Count == 0)
                {
                    dtDisposedProducts.Rows.Add(dtDisposedProducts.NewRow());
                }
                //Bind to grid view
                this.AspNetPager.RecordCount = PageCount;
                this.grdDisposedProducts.DataSource = dtDisposedProducts;
                this.grdDisposedProducts.DataBind();
            }
        }

        /// <summary>
        /// Hide link button when status is invalid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdDisposedProducts_DataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow row in this.grdDisposedProducts.Rows)
            {
                LinkButton linkRegistro = (LinkButton)row.FindControl("linkRegistro");
                CheckBox ckbSelect = (CheckBox)row.FindControl("ckbSelect");

                //determine whether to display registro button by product status
                if (row.RowType == DataControlRowType.DataRow && row.Cells[5].Text.Replace("&nbsp;", "") == "" && row.Cells[6].Text.Replace("&nbsp;", "") == "" && row.Cells[7].Text.Replace("&nbsp;", "") == "1")
                {
                    linkRegistro.Visible = true;
                    linkRegistro.CommandArgument = row.Cells[8].Text.Replace("&nbsp;", "") + ";" + row.Cells[9].Text.Replace("&nbsp;", "");
                }
                else
                {
                    linkRegistro.Visible = false;
                }
            }
        }

        /// <summary>
        ///  Do the action when command button clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdDisposedProducts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string[] cmdArgs = e.CommandArgument.ToString().Split(';');
            string creditNo = cmdArgs[0];
            int technologyID = Convert.ToInt32(cmdArgs[1]);

            if (e.CommandName.Equals("Registry", StringComparison.OrdinalIgnoreCase))//jump to material registry screen
            {
                Response.Redirect("ProductMaterialRegister.aspx?CreditNo=" + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(creditNo)).Replace("+", "%2B") +
                                                                               "&TechnologyID=" + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(technologyID.ToString())).Replace("+", "%2B"));
            }
        }

        /// <summary>
        /// hidden some fields
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdDisposedProducts_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[7].Visible = false;
                e.Row.Cells[8].Visible = false;
                e.Row.Cells[9].Visible = false;
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
                this.drpReceiptDate.SelectedIndex = Session["CurrentDate"] != null ? (int)Session["CurrentDate"] : 0;
                this.drpTechnology.SelectedIndex = Session["CurrentTechnology"] != null ? (int)Session["CurrentTechnology"] : 0;
                this.drpEstatus.SelectedIndex = Session["CurrentEstatus"] != null ? (int)Session["CurrentEstatus"] : 0;
            }
        }

        protected void drpReceiptDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGridViewData();
            Session["CurrentDate"] = drpReceiptDate.SelectedIndex;
        }

        protected void drpTechnology_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGridViewData();
            Session["CurrentTechnology"] = drpTechnology.SelectedIndex;
        }

        protected void drpEstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGridViewData();
            Session["CurrentEstatus"] = drpEstatus.SelectedIndex;
        }

        protected void btnGeneratePartialCut_Click(object sender, EventArgs e)
        {
            Response.Redirect("GeneratePartialCuts.aspx");
        }

        protected void btnGenerateAct_Click(object sender, EventArgs e)
        {
            Response.Redirect("GeneratorFinalAct.aspx");
        }        
    }
}
