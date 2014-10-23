using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Server_Controls
{
    public partial class Control1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Click(object sender, EventArgs e)
        {
            Label1.Text = "Precionaste el boton";
        }
    }
}