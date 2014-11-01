using System;
using System.IO;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace PAEEEM.DisposalModule
{
    public partial class UploadImageReview : System.Web.UI.Page
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
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            string CreditID = "0";
          if(!string.IsNullOrEmpty(Request.QueryString["CreditID"]))
            {
                CreditID = Request.QueryString["CreditID"].ToString();
            }
          string strFilePath = "\\OldEquipmentReceiptionImage\\" + CreditID + "\\";
            if (!Directory.Exists(Server.MapPath(strFilePath)))
            {
                Directory.CreateDirectory(Server.MapPath(strFilePath));
            }
            string strFileName = "";
            string strExtend = "";
            string ImageFormat = ".jpg,.png,.bmp";
            string Image1Path = "";
            if (fldSelect.HasFile)
            {
                strFileName = fldSelect.FileName;
                strExtend = fldSelect.FileName.Substring(fldSelect.FileName.LastIndexOf(".")).ToString();
                if (ImageFormat.IndexOf(strExtend, StringComparison.OrdinalIgnoreCase) > 0)
                {
                    fldSelect.SaveAs(Server.MapPath(strFilePath) + strFileName);
                    Image1Path = strFilePath + strFileName;
                }
            }
            if (Image1Path != "")
            {
                imgReview.Visible = true;
                imgReview.ImageUrl = Image1Path;
            }
        }
        /// <summary>
        /// Leave this page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("");
        }
    }
}
