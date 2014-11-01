using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Prueba2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CeC_Sesion Sesion = CeC_Sesion.Nuevo(this);
        WUC__IncidenciaEd1.CargaDatos(10, TextBox1.Text);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
    protected void WebDatePicker1_ValueChanged(object sender, Infragistics.Web.UI.EditorControls.TextEditorValueChangedEventArgs e)
    {

    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        string Resultado = WUC__IncidenciaEd1.ObtenValores(10, TextBox1.Text);
        TextBox1.Text = Resultado;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        WUC__IncidenciaEd1.CargaDatos(10, TextBox1.Text);
    }
}