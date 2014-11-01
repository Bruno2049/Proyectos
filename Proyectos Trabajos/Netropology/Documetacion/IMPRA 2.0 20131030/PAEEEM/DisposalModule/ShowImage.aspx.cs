using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PAEEEM.DisposalModule
{
    public partial class ShowImage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["ImagePath"] != null && Request.QueryString["ImagePath"].ToString() != "")
            {
                imgProduct.ImageUrl = ".." + Request.QueryString["ImagePath"].ToString();
            }
        }
    }
}
