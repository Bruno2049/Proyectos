using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebFormsEjemplo
{
    public partial class Formulario_web2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            WebFormsEjemplo.LDN.Conexion Conexion = new LDN.Conexion();
            bool R;
            R = Conexion.GuardaProyecto(TextBox1.Text, DropDownList1.Text);

            if (R == true)
            {
                Response.Write("<script type=text/javascript> alert('Se Inserto Correctamente'); </script>");
            }

            else
            {
                Response.Write("<script type=text/javascript> alert('No se inserto'); </script>");
            }
        }
    }
}