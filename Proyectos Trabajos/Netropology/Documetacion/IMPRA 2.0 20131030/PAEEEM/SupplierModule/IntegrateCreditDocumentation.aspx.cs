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
using PAEEEM.BussinessLayer;
using PAEEEM.Entities;
using PAEEEM.DataAccessLayer;
using PAEEEM.Helpers;

namespace PAEEEM.SupplierModule
{
    public partial class IntegrateCreditDocumentation : System.Web.UI.Page
    {
        public string CreditoPersonName
        {
            get
            {
                return ViewState["CreditoPersonName"] == null ? "" : ViewState["CreditoPersonName"].ToString();
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
                }
                InitDefaultData();
            }
        }
        private void InitDefaultData()
        {
            txtFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtCreditoNum.Text = CreditoNumber;
            txtRazonSocial.Text = CreditoPersonName;

            DataTable dtEstado = CAT_ESTADOBLL.ClassInstance.Get_All_CAT_ESTADO();
            DataTable dtOfficeorMunicipality = CAT_DELEG_MUNICIPIOBLL.ClassInstance.Get_All_CAT_DELEG_MUNICIPIO();

            if (dtEstado.Rows.Count > 0)
            {
                drpRepresentative_Estado.DataSource = dtEstado;
                drpRepresentative_Estado.DataTextField = "Dx_Nombre_Estado";
                drpRepresentative_Estado.DataValueField = "Cve_Estado";
                drpRepresentative_Estado.DataBind();
                drpRepresentative_Estado.Items.Insert(0, "");
                drpRepresentative_Estado.SelectedIndex = 0;

                drpApplicant_Estado.DataSource = dtEstado;
                drpApplicant_Estado.DataTextField = "Dx_Nombre_Estado";
                drpApplicant_Estado.DataValueField = "Cve_Estado";
                drpApplicant_Estado.DataBind();
                drpApplicant_Estado.Items.Insert(0, "");
                drpApplicant_Estado.SelectedIndex = 0;

                drpGuarantee_Estado.DataSource = dtEstado;
                drpGuarantee_Estado.DataTextField = "Dx_Nombre_Estado";
                drpGuarantee_Estado.DataValueField = "Cve_Estado";
                drpGuarantee_Estado.DataBind();
                drpGuarantee_Estado.Items.Insert(0, "");
                drpGuarantee_Estado.SelectedIndex = 0;
            }

            if (dtOfficeorMunicipality.Rows.Count > 0)
            {
                drpRepresentative_OfficeorMunicipality.DataSource = dtOfficeorMunicipality;
                drpRepresentative_OfficeorMunicipality.DataTextField = "Dx_Deleg_Municipio";
                drpRepresentative_OfficeorMunicipality.DataValueField = "Cve_Deleg_Municipio";
                drpRepresentative_OfficeorMunicipality.DataBind();
                drpRepresentative_OfficeorMunicipality.Items.Insert(0, "");
                drpRepresentative_OfficeorMunicipality.SelectedIndex = 0;

                drpApplicant_OfficeOrMunicipality.DataSource = dtOfficeorMunicipality;
                drpApplicant_OfficeOrMunicipality.DataTextField = "Dx_Deleg_Municipio";
                drpApplicant_OfficeOrMunicipality.DataValueField = "Cve_Deleg_Municipio";
                drpApplicant_OfficeOrMunicipality.DataBind();
                drpApplicant_OfficeOrMunicipality.Items.Insert(0, "");
                drpApplicant_OfficeOrMunicipality.SelectedIndex = 0;

                drpGuarantee_OfficeOrMunicipality.DataSource = dtOfficeorMunicipality;
                drpGuarantee_OfficeOrMunicipality.DataTextField = "Dx_Deleg_Municipio";
                drpGuarantee_OfficeOrMunicipality.DataValueField = "Cve_Deleg_Municipio";
                drpGuarantee_OfficeOrMunicipality.DataBind();
                drpGuarantee_OfficeOrMunicipality.Items.Insert(0, "");
                drpGuarantee_OfficeOrMunicipality.SelectedIndex = 0;
            }
        }

        protected void btnSiguiente_Click(object sender, EventArgs e)
        {
            CreditEntity creditomodel = new CreditEntity();
            creditomodel.No_Credito = int.Parse(CreditoNumber);
            creditomodel.Dx_No_Escritura_Poder = txtRepresentative_LegalDocumentNumber.Text.Trim();
            if (txtRepresentative_LegalDocumentFecha.Text.Trim() != "")
            {
                creditomodel.Dt_Fecha_Poder = Convert.ToDateTime(txtRepresentative_LegalDocumentFecha.Text.Trim()); 
            }
            creditomodel.Dx_Nombre_Notario_Poder = txtRepresentative_NotariesProfessionName.Text.Trim();
            creditomodel.Dx_No_Notario_Poder = txtRepresentative_NotariesProfessionNumber.Text.Trim();
            creditomodel.Cve_Estado_Poder = int.Parse(drpRepresentative_Estado.SelectedValue.ToString());
            creditomodel.Cve_Deleg_Municipio_Poder = int.Parse(drpRepresentative_OfficeorMunicipality.SelectedValue.ToString());
            creditomodel.Dx_No_Escritura_Acta = txtApplicant_LegalDocumentNumber.Text.Trim();
            if (txtApplicant_LegalDocumentFecha.Text.Trim() != "")
            {
                creditomodel.Dt_Fecha_Acta = Convert.ToDateTime(txtApplicant_LegalDocumentFecha.Text.Trim());
            }
            creditomodel.Dx_Nombre_Notario_Acta = txtApplicant_NotariesProfessionName.Text.Trim();
            creditomodel.Dx_No_Notario_Acta = txtApplicant_NotariesProfessionNumber.Text.Trim();
            creditomodel.Cve_Estado_Acta = int.Parse(drpApplicant_Estado.SelectedValue.ToString());
            creditomodel.Cve_Deleg_Municipio_Acta = int.Parse(drpApplicant_OfficeOrMunicipality.SelectedValue.ToString());
            creditomodel.Dx_No_Escritura_Aval = txtGuarantee_LegalDocumentNumber.Text.Trim();
            if (txtGuarantee_LegalDocumentFecha.Text.Trim() != "")
            {
                creditomodel.Dt_Fecha_Escritura_Aval = Convert.ToDateTime(txtGuarantee_LegalDocumentFecha.Text.Trim());
            }
            creditomodel.Dx_Nombre_Notario_Escritura_Aval = txtGuarantee_NotariesProfessionName.Text.Trim();
            creditomodel.Dx_No_Notario_Escritura_Aval = txtGuarantee_NotariesProfessionName.Text.Trim();
            creditomodel.Cve_Estado_Escritura_Aval = int.Parse(drpGuarantee_Estado.SelectedValue.ToString());
            creditomodel.Cve_Deleg_Municipio_Escritura_Aval = int.Parse(drpGuarantee_OfficeOrMunicipality.SelectedValue.ToString());
            if (txtPropertyEncumbrancesFecha.Text.Trim() != "")
            {
                creditomodel.Dt_Fecha_Gravamen = Convert.ToDateTime(txtPropertyEncumbrancesFecha.Text.Trim());
            }
            creditomodel.Dx_Emite_Gravamen = txtPropertyEncumbrancesName.Text.Trim();
            creditomodel.Dx_Num_Acta_Matrimonio_Aval = txtMarriageNumber.Text.Trim();
            creditomodel.Dx_Registro_Civil_Mat_Aval = txtCitizenRegisterOffice.Text.Trim();
            creditomodel.Fg_Adquisicion_Sust = 0;

            int iResult = CreditDal.ClassInstance.UpdateCredit(creditomodel);
        }
    }
}
