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
using System.Text.RegularExpressions;
using PAEEEM.BussinessLayer;
using PAEEEM.Entities;
using PAEEEM.Helpers;

namespace PAEEEM.SupplierModule
{
    public partial class AcquisitionOrReplacement : System.Web.UI.Page
    {
        public int DrpNum
        {
            get
            {
                return ViewState["DrpNum"] == null ? 1 : int.Parse(ViewState["DrpNum"].ToString());
            }
            set
            {
                ViewState["DrpNum"] = value;
            }
        }
        public string CreditoPersonName
        {
            get
            {
                return ViewState["CreditoPersonName"] == null ? "" :ViewState["CreditoPersonName"].ToString();
            }
            set
            {
                ViewState["CreditoPersonName"] = value;
            }
        }
        public string CreditoNumber
        {
            get
            {
                return ViewState["CreditoNumber"] == null ? "" : ViewState["CreditoNumber"].ToString();
            }
            set
            {
                ViewState["CreditoNumber"] = value;
            }
        }
        public int AdquisicionStatus
        {
            get
            {
                return ViewState["AdquisicionStatus"] == null ? 0 : int.Parse(ViewState["AdquisicionStatus"].ToString());
            }
            set
            {
                ViewState["AdquisicionStatus"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserInfo"] == null)
                {
                    Response.Redirect("../Login/Login.aspx");
                    return;
                }
                if (Request.QueryString["No_Credito"] != null && Request.QueryString["No_Credito"].ToString() != "")
                {
                    CreditoNumber = Request.QueryString["No_Credito"].ToString();
                    CreditoPersonName = Request.QueryString["Dx_Razon_Social"].ToString();
                    AdquisicionStatus = Convert.ToInt32(Request.QueryString["FG_ADQUISICION_SUST"].ToString());
                }
                InitDefaultData();
            }
        }

        private void InitDefaultData()
        {
            DrpNum = 1;

            txtFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtCreditoNum.Text = CreditoNumber;
            txtRazonSocial.Text = CreditoPersonName;
            if (AdquisicionStatus == 0)
            {
                radAcquisition.Checked = false;
                radReplacement.Checked = true;
            }
            else
            {
                radAcquisition.Checked = true;
                radReplacement.Checked = false;
            }

            drpTechnology1.DataSource = CAT_TECNOLOGIABLL.ClassInstance.Get_All_CAT_TECNOLOGIA();
            drpTechnology1.DataTextField = "Dx_Nombre_General";
            drpTechnology1.DataValueField = "Cve_Tecnologia";
            drpTechnology1.DataBind();
            drpTechnology1.Items.Insert(0, "");
            drpTechnology1.SelectedIndex = 0;

            ////

            drpDisposalCenter1.DataSource = CAT_CENTRO_DISPBLL.ClassInstance.Get_All_CAT_CENTRO_DISP(0);
            drpDisposalCenter1.DataTextField = "Dx_Razon_Social";
            drpDisposalCenter1.DataValueField = "Id_Centro_Disp";
            drpDisposalCenter1.DataBind();
            drpDisposalCenter1.Items.Insert(0, "");
        }

        protected void btnAddRecord_Click(object sender, EventArgs e)
        {

            DrpNum = DrpNum + 1;

            TableRow tr = new TableRow();
            tr.ID = "tr" + DrpNum.ToString();

            TableCell tc1 = new TableCell();
            TableCell tc2 = new TableCell();
            TableCell tc3 = new TableCell();
            TableCell tc4 = new TableCell();

            tc1.Width = new Unit("260px");
            DropDownList drpTechnology = new DropDownList();
            drpTechnology.ID = "drpTechnology" + DrpNum.ToString();
            drpTechnology.CssClass = "DropDownList";
            tc1.Controls.Add(drpTechnology);

            tc2.Width = new Unit("300px");
            TextBox txtUnidades = new TextBox();
            txtUnidades.ID = "txtUnidades" + DrpNum.ToString();
            txtUnidades.CssClass = "TextBox";

            //RegularExpressionValidator revUnidades = new RegularExpressionValidator();
            //revUnidades.ID = "revUnidades" + DrpNum.ToString();
            //revUnidades.
            tc2.Controls.Add(txtUnidades);

            tc3.Width = new Unit("260px");
            DropDownList drpDisposalCenter = new DropDownList();
            drpDisposalCenter.ID = "drpDisposalCenter" + DrpNum.ToString();
            drpDisposalCenter.CssClass = "DropDownList";
            tc3.Controls.Add(drpDisposalCenter);

            tc4.Width = new Unit("260px");

            tr.Cells.Add(tc1);
            tr.Cells.Add(tc2);
            tr.Cells.Add(tc3);
            tr.Cells.Add(tc4);

            tbAcqOrRep.Rows.Add(tr);

            if (drpTechnology != null)
            {
                drpTechnology.DataSource = CAT_TECNOLOGIABLL.ClassInstance.Get_All_CAT_TECNOLOGIA();
                drpTechnology.DataTextField = "Dx_Nombre_General";
                drpTechnology.DataValueField = "Cve_Tecnologia";
                drpTechnology.DataBind();
                drpTechnology.Items.Insert(0, "");
                drpTechnology.SelectedIndex = 0;
            }
            ////
            if (drpDisposalCenter != null)
            {
                drpDisposalCenter.DataSource = CAT_CENTRO_DISPBLL.ClassInstance.Get_All_CAT_CENTRO_DISP(0);
                drpDisposalCenter.DataTextField = "Dx_Razon_Social";
                drpDisposalCenter.DataValueField = "Id_Centro_Disp";
                drpDisposalCenter.DataBind();
                drpDisposalCenter.Items.Insert(0, "");
                drpDisposalCenter.SelectedIndex = 0;
            }
        }

        protected void btnSiguiente_Click(object sender, EventArgs e)
        {
            List<K_CREDITO_SUSTITUCIONModel> list = new List<K_CREDITO_SUSTITUCIONModel>();
            for (int i = 1; i <= DrpNum; i++)
            {
                DropDownList drpTechnology = tbAcqOrRep.FindControl("drpTechnology" + DrpNum.ToString()) as DropDownList;
                DropDownList drpDisposalCenter = tbAcqOrRep.FindControl("drpDisposalCenter" + DrpNum.ToString()) as DropDownList;
                TextBox txtUnidades = tbAcqOrRep.FindControl("drpUnidades" + DrpNum.ToString()) as TextBox;

                if (drpTechnology.SelectedIndex != 0 && drpTechnology.SelectedIndex != -1
                    && drpDisposalCenter.SelectedIndex != 0 && drpDisposalCenter.SelectedIndex != -1)
                {
                    K_CREDITO_SUSTITUCIONModel model = new K_CREDITO_SUSTITUCIONModel();

                    model.No_Credito = int.Parse(CreditoNumber);
                    model.Cve_Tecnologia = int.Parse(drpTechnology.SelectedValue.ToString());
                    //model.No_Unidades = int.Parse(drpUnidades.SelectedValue.ToString());
                    model.Id_Centro_Disp = int.Parse(drpDisposalCenter1.SelectedValue.ToString());
                    model.Dt_Fecha_Credito_Sustitucion = DateTime.Now;

                    list.Add(model);
                }
            }
            K_CREDITO_SUSTITUCIONBLL.ClassInstance.Insert_K_CREDITO_SUSTITUCION(list);

            CleanData();
        }

        private void CleanData()
        {
            if (DrpNum > 1)
            {
                for (int i = DrpNum + 1; i <= DrpNum; i++)
                {
                    //TableRow tr = tbAcqOrRep.FindControl("tr" + DrpNum.ToString());
                    //tbAcqOrRep.Rows.Remove(tr);
                }
            }
            drpTechnology1.SelectedIndex = 0;
            drpDisposalCenter1.SelectedIndex = 0;
            txtUnidades1.Text = "";
            DrpNum = 1;
        }

        protected void radAcquisition_CheckedChanged(object sender, EventArgs e)
        {
            if (radAcquisition.Checked)
            {
                HiddenDropDownList();
            }
        }

        protected void radReplacement_CheckedChanged(object sender, EventArgs e)
        {
            if (radReplacement.Checked)
            {
                ShowDropDownList();
            }
        }

        private void ShowDropDownList()
        {
            for (int i = 1; i <= DrpNum; i++)
            {
                DropDownList drpTechnology = tbAcqOrRep.FindControl("drpTechnology" + DrpNum.ToString()) as DropDownList;
                DropDownList drpUnidades = tbAcqOrRep.FindControl("drpUnidades" + DrpNum.ToString()) as DropDownList;
                DropDownList drpDisposalCenter = tbAcqOrRep.FindControl("drpDisposalCenter" + DrpNum.ToString()) as DropDownList;
                drpTechnology.Enabled = true;
                drpUnidades.Enabled = true;
                drpDisposalCenter.Enabled = true;
            }
        }

        private void HiddenDropDownList()
        {
            for (int i = 1; i <= DrpNum; i++)
            {
                DropDownList drpTechnology = tbAcqOrRep.FindControl("drpTechnology" + DrpNum.ToString()) as DropDownList;
                DropDownList drpUnidades = tbAcqOrRep.FindControl("drpUnidades" + DrpNum.ToString()) as DropDownList;
                DropDownList drpDisposalCenter = tbAcqOrRep.FindControl("drpDisposalCenter" + DrpNum.ToString()) as DropDownList;
                drpTechnology.Enabled = false;
                drpUnidades.Enabled = false;
                drpDisposalCenter.Enabled = false;
            }
        }
    }
}
