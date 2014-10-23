using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Demos_WebServices : BasePage
{
  protected void Page_Load(object sender, EventArgs e)
  {

  }

  [WebMethod]
  public static string HelloWorld(string name)
  {
    return string.Format("Hello {0}", name);
  }
}