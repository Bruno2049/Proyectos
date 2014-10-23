using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hotkey_Funtionality
{
    public partial class HotKey : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TextBox1.Focus();
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {

        }

        protected void CambioLista1(object sender, EventArgs e)
        {
            string[] carArray = new[] { "Ford", "Honda", "BMW", "Dodge" };
            string[] airplaneArray = new[] { "Boeing 777", "Boeing 747", "Boeing 737" };
            string[] trainArray = new[] { "Bullet Train", "Amtrack", "Tram" };
            if (DropDownList1.SelectedValue == "Car")
            {
                DropDownList2.DataSource = carArray;
            }
            else if (DropDownList1.SelectedValue == "Airplane")
            {
                DropDownList2.DataSource = airplaneArray;
            }
            else if (DropDownList1.SelectedValue == "Train")
            {
                DropDownList2.DataSource = trainArray;
            }
            DropDownList2.DataBind();
            DropDownList2.Visible = DropDownList1.SelectedValue != "Select an Item";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Write("You selected <b>" + DropDownList1.SelectedValue.ToString() + ": " + DropDownList2.SelectedValue.ToString() + "</b>");
        }
    }
}