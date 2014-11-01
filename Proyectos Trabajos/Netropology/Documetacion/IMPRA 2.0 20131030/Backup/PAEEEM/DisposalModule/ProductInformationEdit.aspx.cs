using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using PAEEEM.DataAccessLayer;
using PAEEEM.Entities;
using PAEEEM.Helpers;

namespace PAEEEM.DisposalModule
{
    public partial class ProductInformationEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (null == Session["UserInfo"])
                {
                    Response.Redirect("../Login/Login.aspx");
                    return;
                }
                txtFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
                //Load technology and Tipo Product
                if (!string.IsNullOrEmpty(Request.QueryString["NoCredit"]) && !string.IsNullOrEmpty(Request.QueryString["Technology"]))
                {
                    string NoCredit =System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["NoCredit"].ToString().Replace("%2B", "+")));
                    int Technology = Convert.ToInt32(System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["Technology"].ToString().Replace("%2B", "+"))));
                    DataTable dtProductImformation = K_CREDITO_SUSTITUCIONDAL.ClassInstance.Get_All_K_CREDITO_SUSTITUCIONByCreditoAndTechnology(NoCredit, Technology);
                    if (dtProductImformation != null && dtProductImformation.Rows.Count > 0)
                    {
                        txtTechnology.Text = dtProductImformation.Rows[0]["Dx_Nombre_General"].ToString();
                        txtTipoProduct.Text = dtProductImformation.Rows[0]["Dx_Tipo_Producto"].ToString();
                        txtModelo.Text = dtProductImformation.Rows[0]["Dx_Modelo_Producto"].ToString();
                        txtMarca.Text = dtProductImformation.Rows[0]["Dx_Marca"].ToString();
                        txtNoSerie.Text = dtProductImformation.Rows[0]["No_Serial"].ToString();
                        txtColor.Text = dtProductImformation.Rows[0]["Dx_Color"].ToString(); ;
                        txtPeso.Text = dtProductImformation.Rows[0]["Dx_Peso"].ToString();
                        txtAntiguedad.Text = dtProductImformation.Rows[0]["Dx_Antiguedad"].ToString();
                        txtCapacidad.Text = dtProductImformation.Rows[0]["Ft_Capacidad"].ToString();
                    }
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            DataTable dtBarCode = null;
            string BarCode = "";
            dtBarCode = K_CREDITODal.ClassInstance.GetCreditsBarCodeByNoCredit(System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["NoCredit"].ToString().Replace("%2B", "+"))));
            if (dtBarCode != null && dtBarCode.Rows.Count > 0)
            {
                BarCode = dtBarCode.Rows[0]["Barcode_Solicitud"].ToString();
            }
            Response.Redirect("ProductListForDisposal.aspx?BarCode=" + Convert.ToBase64String(Encoding.Default.GetBytes(BarCode)).Replace("+", "%2B"));
        }
        /// <summary>
        /// Update Product Information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {            
            K_CREDITO_SUSTITUCIONModel CreditSustitucion = new K_CREDITO_SUSTITUCIONModel();
            if (!string.IsNullOrEmpty(Request.QueryString["NoCredit"]) && !string.IsNullOrEmpty(Request.QueryString["Technology"]))
            {
                CreditSustitucion.No_Credito = System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["NoCredit"].ToString().Replace("%2B", "+")));
                CreditSustitucion.Cve_Tecnologia = Convert.ToInt32(System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["Technology"].ToString().Replace("%2B", "+"))));
            }
            else
            {
                CreditSustitucion.No_Credito = "0";
                CreditSustitucion.Cve_Tecnologia = 0;
            }
            CreditSustitucion.Dx_Modelo_Producto = txtModelo.Text;
            CreditSustitucion.Dx_Marca = txtMarca.Text;
            CreditSustitucion.Dx_Color = txtColor.Text;
            CreditSustitucion.No_Serial = txtNoSerie.Text;
            if (!txtPeso.Text.Equals(""))
            {
                CreditSustitucion.No_Peso = decimal.Parse(txtPeso.Text);
            }
            else
            {
                CreditSustitucion.No_Peso = 0;
            }
            if (!txtCapacidad.Text.Equals(""))
            {
                //CreditSustitucion.Ft_Capacidad = float.Parse(txtCapacidad.Text);
            }
            else
            {
                //CreditSustitucion.Ft_Capacidad = 0;
            }
            CreditSustitucion.Dx_Antiguedad = txtAntiguedad.Text;
            int Result = 0;
            //Result = K_CREDITO_SUSTITUCIONDAL.ClassInstance.Update_K_CREDITO_SUSTITUCIONProductInformation(CreditSustitucion);
            if (Result < 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, typeof(Page), "SaveError", "alert('tienda cambiado error de información! información, póngase en contacto con administrador de sistemas o leer el archivo de registro.');", true);
            }
            else
            {
                Response.Redirect("ProductListForDisposal.aspx?Barcode="+Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(CreditSustitucion.No_Credito)).Replace("+","%2B"));
            }
        }
    }
}
