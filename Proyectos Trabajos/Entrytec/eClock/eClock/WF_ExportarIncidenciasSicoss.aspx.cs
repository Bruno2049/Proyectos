using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;

public partial class WF_ExportarIncidenciasSicoss: System.Web.UI.Page
{
    CeC_Sesion Sesion;
    public static int terminalID;
    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        if (!Sesion.Configura.UsaSicoss)
            Sesion.Redirige("WF_Info.aspx");
    }

    protected void btnImportar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
      
    }

   
}
