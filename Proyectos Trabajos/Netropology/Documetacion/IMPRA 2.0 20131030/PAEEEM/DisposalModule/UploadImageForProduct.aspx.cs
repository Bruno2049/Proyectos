using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.IO;
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
    public partial class UploadImageForProduct : System.Web.UI.Page
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
                literalFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }
        /// <summary>
        /// Upload three Images
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            string Folder = "";
            //Get technology and No credit
            if (!string.IsNullOrEmpty(Request.QueryString["NoCredit"]) && !string.IsNullOrEmpty(Request.QueryString["Technology"]))
            {

                Folder = System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["NoCredit"].ToString().Replace("%2B", "+"))) + "_" + System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["Technology"].ToString().Replace("%2B", "+")));
            }

            string strFilePath = "\\ProductImage\\" + Folder + "\\";
            //int iCount = 0;
            if (!Directory.Exists(Server.MapPath(strFilePath)))
            {
                Directory.CreateDirectory(Server.MapPath(strFilePath));
            }
            string strFileName;
            string strExtend;
            string Image1Path = "";
            string Image2Path = "";
            string Image3Path = "";

            try
            {
                string ResultMaxLength = VilidateImageMaxLength();
                if (!ResultMaxLength.Equals(""))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "ImageLength", "alert('Por favor, asegúrese de que cada imagen con un tamaño de menos de 100 kb');", true);
                    return;
                }
                string ImageFormat = ".jpg,.png,.bmp";
                //Upload image1
                if (fldImage1.HasFile)
                {
                    strFileName = fldImage1.FileName;
                    strExtend = fldImage1.FileName.Substring(fldImage1.FileName.LastIndexOf(".")).ToString();
                    if (ImageFormat.IndexOf(strExtend, StringComparison.OrdinalIgnoreCase) > 0)
                    {
                        fldImage1.SaveAs(Server.MapPath(strFilePath) + strFileName);
                        Image1Path = strFilePath + strFileName;
                    }
                }
                //Upload image2
                if (fldImage2.HasFile)
                {
                    strFileName = fldImage2.FileName;
                    strExtend = fldImage2.FileName.Substring(fldImage2.FileName.LastIndexOf(".")).ToString();
                    if (ImageFormat.IndexOf(strExtend, StringComparison.OrdinalIgnoreCase) > 0)
                    {
                        fldImage2.SaveAs(Server.MapPath(strFilePath) + strFileName);
                        Image2Path = strFilePath + strFileName;
                    }
                }
                //Upload image3
                if (fldImage3.HasFile)
                {
                    strFileName = fldImage3.FileName;
                    strExtend = fldImage3.FileName.Substring(fldImage3.FileName.LastIndexOf(".")).ToString();
                    if (ImageFormat.IndexOf(strExtend, StringComparison.OrdinalIgnoreCase) > 0)
                    {
                        fldImage3.SaveAs(Server.MapPath(strFilePath) + strFileName);
                        Image3Path = strFilePath + strFileName;
                    }
                } 
                //Insert into DB
                K_CREDITO_SUSTITUCIONModel Instance = new K_CREDITO_SUSTITUCIONModel();
                Instance.No_Credito = System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["NoCredit"].ToString().Replace("%2B", "+")));
                if (!string.IsNullOrEmpty(System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["Technology"].ToString().Replace("%2B", "+")))))
                {
                    Instance.Cve_Tecnologia = Convert.ToInt32(System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["Technology"].ToString().Replace("%2B", "+"))));
                }
                else
                {
                    Instance.Cve_Tecnologia = 0;
                }
                //Instance.Image1 = Image1Path;
                //Instance.Image2 = Image2Path;
                //Instance.Image3 = Image3Path;
                //int result = K_CREDITO_SUSTITUCIONDAL.ClassInstance.UpLoadProductImage(Instance);
                //if (result > 0)
                //{
                //    string message = HttpContext.GetGlobalResourceObject("DefaultResource", "UploadImageSuccessfull") as string;
                //    ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "UploadImage", "alert('" + message + "');", true);
                //}
            }

            catch (Exception ex)
            {
                string message = ex.Message;
                ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "UploadImage", "alert('" + message + "');", true);
            }
        }
        /// <summary>
        /// Validate Upload File maxlength
        /// </summary>
        /// <returns></returns>
        private string VilidateImageMaxLength()
        {
            string MaxLength = "";
            //Validate Image1
            if (this.fldImage1.HasFile)
            {
                if (this.fldImage1.PostedFile.ContentLength > 100 * 1024)
                {
                    MaxLength = lblImage1.Text;
                }
            }
            //Validate Image2
            if (this.fldImage2.HasFile)
            {
                if (this.fldImage2.PostedFile.ContentLength > 100 * 1024)
                {
                    if (MaxLength.Equals(""))
                    {
                        MaxLength = lblImage2.Text;
                    }
                    else
                    {
                        MaxLength = MaxLength + "," + lblImage2.Text;
                    }
                }
            }
            //Validate Image3
            if (this.fldImage3.HasFile)
            {
                if (this.fldImage3.PostedFile.ContentLength > 100 * 1024)
                {
                    if (MaxLength.Equals(""))
                    {
                        MaxLength = lblImage3.Text;
                    }
                    else
                    {
                        MaxLength = MaxLength + "," + lblImage3.Text;
                    }
                }
            }
            return MaxLength;
        }
        /// <summary>
        /// Cancel Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
    }
}
