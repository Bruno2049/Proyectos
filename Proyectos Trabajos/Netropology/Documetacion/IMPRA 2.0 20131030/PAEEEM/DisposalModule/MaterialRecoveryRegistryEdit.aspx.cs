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

namespace PAEEEM.DisposalModule
{
    public partial class MaterialRecoveryRegistryEdit : System.Web.UI.Page
    {
        #region Global Variables
        private string Id_Recuperacion
        {
            get
            {
                return ViewState["Id_Recuperacion"] == null ? "" : ViewState["Id_Recuperacion"].ToString();
            }
            set
            {
                ViewState["Id_Recuperacion"] = value;
            }
        }
        #endregion
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
                if (Request.QueryString["Id_Recuperacion"] != null && Request.QueryString["Id_Recuperacion"].ToString() != "")
                {
                    Id_Recuperacion = System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["Id_Recuperacion"].ToString().Replace("%2B", "+")));
                    InitDefaultData();
                }
            }
        }

        private void InitDefaultData()
        {
            DataTable dtMaterial = K_RECUPERACIONDal.ClassInstance.GetRecoveryMaterialByRecoveryID(Id_Recuperacion);
            if (dtMaterial != null && dtMaterial.Rows.Count > 0)
            {
                txtProgram.Text = dtMaterial.Rows[0]["Dx_Nombre_Programa"].ToString();
                txtTechnology.Text = dtMaterial.Rows[0]["Dx_Nombre_General"].ToString();
                txtRecoveryDate.Text = Convert.ToDateTime(dtMaterial.Rows[0]["Dt_Fecha_Recuperacion"].ToString()).ToString("yyyy-MM-dd");
                txtWeight.Text = dtMaterial.Rows[0]["No_Material"].ToString();
                lblMaterialName.Text = literalMaterial.Text = dtMaterial.Rows[0]["Dx_Residuo_Material_Gral"].ToString();
                lblUnit.Text = dtMaterial.Rows[0]["Dx_Unidades"].ToString();
            }
        }

        protected void btnRecovery_Click(object sender, EventArgs e)
        {
            int result = 0;
            string weightMaterial = txtWeight.Text.Trim() == "" ? "0" : txtWeight.Text.Trim();
            double weight = Convert.ToDouble(String.Format("{0:0.00}", Convert.ToDouble(weightMaterial)));
            result = K_RECUPERACIONDal.ClassInstance.UpdateMaterialWeight(Id_Recuperacion, weight);
            if (result > 0)
            {
                Response.Redirect("MaterialRecoveryRegistryMonitor.aspx");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("MaterialRecoveryRegistryMonitor.aspx?Flag=0"); // updated by tina 2012/04/13
        }
    }
}
