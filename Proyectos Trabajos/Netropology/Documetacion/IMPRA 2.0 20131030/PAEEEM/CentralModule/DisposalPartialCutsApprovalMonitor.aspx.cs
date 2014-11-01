using System;
using System.Collections;
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

namespace PAEEEM.CentralModule
{
    public partial class DisposalPartialCutsApprovalMonitor : System.Web.UI.Page
    {
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
            InitProgram();
            InitDisposalCenter();
            InitCreateDate();
            InitEstatus();
        }

        /// <summary>
        /// Load data of program
        /// </summary>
        private void InitProgram()
        {
            DataTable dtprogram = null;
            dtprogram = CAT_PROGRAMADal.ClassInstance.GetPrograms();
            if (dtprogram != null)
            {
                this.drpProgram.DataSource = dtprogram;
                this.drpProgram.DataTextField = "Dx_Nombre_Programa";
                this.drpProgram.DataValueField = "ID_Prog_Proy";
                this.drpProgram.DataBind();
                this.drpProgram.Items.Insert(0, new ListItem(""));
            }
        }

        /// <summary>
        /// Load Disposal Center
        /// </summary>
        private void InitDisposalCenter()
        {
            DataTable dtDisposalCenter = null;
            dtDisposalCenter = CAT_CENTRO_DISPDAL.ClassInstance.GetDisposalCenterAndBranch();
            if (dtDisposalCenter != null)
            {
                this.drpDisposal.DataSource = dtDisposalCenter;
                this.drpDisposal.DataTextField = "Dx_Razon_Social";
                this.drpDisposal.DataValueField = "Id_Centro_Disp";
                this.drpDisposal.DataBind();
                this.drpDisposal.Items.Insert(0, new ListItem(""));
            }
        }

        /// <summary>
        /// Load Create Date
        /// </summary>
        private void InitCreateDate()
        {
            DataTable dtCreateDate = null;
            dtCreateDate = K_CORTE_PARCIALDAL.ClassInstance.GetCreateDate();
            if (dtCreateDate != null)
            {
                this.drpCreateDate.DataSource = dtCreateDate;
                this.drpCreateDate.DataTextField = "Dt_Fecha_Creacion";
                this.drpCreateDate.DataBind();
                this.drpCreateDate.Items.Insert(0, new ListItem(""));
            }
        }

        /// <summary>
        /// Load disposed status
        /// </summary>
        private void InitEstatus()
        {
            this.drpEstatus.Items.Add("");
            this.drpEstatus.Items.Add("PENDIENTE");
            this.drpEstatus.Items.Add("COMPLETADO");
        }

        /// <summary>
        /// Load data of partial cut 
        /// </summary>
        private void LoadGridViewData()
        {
            US_USUARIOModel UserModel = null;
            UserModel = Session["UserInfo"] as US_USUARIOModel;

            int PageCount = 0;
            DataTable dtPartialCuts = null;

            int program = (this.drpProgram.SelectedIndex == 0 || this.drpProgram.SelectedIndex == -1) ? 0 : Convert.ToInt32(this.drpProgram.SelectedValue);
            int disposalCenter = (this.drpDisposal.SelectedIndex == 0 || this.drpDisposal.SelectedIndex == -1) ? 0 : Convert.ToInt32(this.drpDisposal.SelectedValue);
            string createDate = (this.drpCreateDate.SelectedIndex == 0 || this.drpCreateDate.SelectedIndex == -1) ? "" : this.drpCreateDate.SelectedItem.Text;
            int estatus = 0;
            if (this.drpEstatus.SelectedIndex != 0 && this.drpEstatus.SelectedIndex != -1)
            {
                switch (this.drpEstatus.Text)
                {
                    case "PENDIENTE":
                        estatus = (int)DisposalStatus.PENDIENTE;
                        break;
                    case "COMPLETADO":
                        estatus = (int)DisposalStatus.COMPLETADO;
                        break;
                    default:
                        break;
                }
            }
            string DisposalType = "";
            if (this.drpDisposal.SelectedIndex != 0 && this.drpDisposal.SelectedIndex != -1)
            {
                if (this.drpDisposal.Text.Contains("Main Center"))
                {
                    DisposalType = "M";
                }
                else
                {
                    DisposalType = "B";
                }
            }

            dtPartialCuts = K_CORTE_PARCIALDAL.ClassInstance.GetPartialCutsForApproval(program, disposalCenter, DisposalType, createDate, estatus, "", this.AspNetPager.CurrentPageIndex, this.AspNetPager.PageSize, out PageCount);
            if (dtPartialCuts != null)
            {
                if (dtPartialCuts.Rows.Count == 0)
                {
                    dtPartialCuts.Rows.Add(dtPartialCuts.NewRow());
                }
                //Bind to grid view
                this.AspNetPager.RecordCount = PageCount;
                this.grdPartialCuts.DataSource = dtPartialCuts;
                this.grdPartialCuts.DataBind();
            }
        }

        protected void grdPartialCuts_DataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow row in this.grdPartialCuts.Rows)
            {
                LinkButton linkApproval = (LinkButton)row.FindControl("linkApproval");
                if (row.RowType == DataControlRowType.DataRow && row.Cells[0].Text.Replace("&nbsp;", "") != "" && row.Cells[5].Text.Equals("PENDIENTE"))
                {
                    linkApproval.Visible = true;
                    linkApproval.CommandArgument = row.Cells[0].Text.Replace("&nbsp;", "");
                }
                else
                {
                    linkApproval.Visible = false;
                }
            }
        }

        protected void grdPartialCuts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Approval", StringComparison.OrdinalIgnoreCase))
            {
                Response.Redirect("DisposalPartialCutsApproval.aspx?PartialCode=" + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(e.CommandArgument.ToString())).Replace("+", "%2B"));
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
                this.drpProgram.SelectedIndex = Session["Program"] != null ? (int)Session["Program"] : 0;
                this.drpDisposal.SelectedIndex = Session["DisposalCenter"] != null ? (int)Session["DisposalCenter"] : 0;
                this.drpCreateDate.SelectedIndex = Session["CreateDate"] != null ? (int)Session["CreateDate"] : 0;
                this.drpEstatus.SelectedIndex = Session["drpEstatus"] != null ? (int)Session["drpEstatus"] : 0;
            }
        }

        protected void drpProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGridViewData();
            Session["Program"] = drpProgram.SelectedIndex;
        }

        protected void drpDisposal_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGridViewData();
            Session["DisposalCenter"] = drpDisposal.SelectedIndex;
        }

        protected void drpCreateDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGridViewData();
            Session["CreateDate"] = drpCreateDate.SelectedIndex;
        }

        protected void drpEstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGridViewData();
            Session["drpEstatus"] = drpEstatus.SelectedIndex;
        }
    }
}
