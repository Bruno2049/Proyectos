using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class WF_TerminalE2 : System.Web.UI.Page
{
    CeC_Sesion Sesion;


    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
    }

    public string ObtenClave(string NS)
    {
        System.Security.Cryptography.SHA1CryptoServiceProvider Sha1 = new System.Security.Cryptography.SHA1CryptoServiceProvider();

        string HashSR = BitConverter.ToString(Sha1.ComputeHash(new System.IO.MemoryStream(
            System.Text.ASCIIEncoding.Default.GetBytes("NO SERIE LI: " + NS))));
        HashSR = HashSR.Replace("-", "");
        return HashSR.Substring(0, 6);
    }

    protected void BGuardarCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        int TerminalID = Sesion.WF_Terminales_TERMINALES_ID;
        LError.Text = "";
        LCorrecto.Text = "";

        string NS = ObtenClave(Tbx_Serie.Text);
        if (NS != Tbx_Clave.Text)
        {
            LError.Text = NS + "No coincide la clave con la terminal";
            return;
        }
        if (TerminalID <= 0)
        {

            int SitioID = CeC_Sitios.ObtenSitioPredeterminado(Sesion.USUARIO_ID,Sesion.SESION_ID, Sesion.SUSCRIPCION_ID);

            int ID = CeC_Sitios.TerminalesInserta(Sesion.SESION_ID, Sesion.USUARIO_ID, Tbx_Nombre.Text, 0, 5, SitioID, 22, 5, 3, 0, "PERSONA_LINK_ID", "NO_CREDENCIAL",
                true, "Red:127.0.0.1:" + Tbx_Serie.Text, true, false, 0, false, false, false);
            LCorrecto.Text = "Se han guardado los datos con exito.";
            BGuardarCambios.Visible = false;
        }
        else
        {

        }
    }
}
