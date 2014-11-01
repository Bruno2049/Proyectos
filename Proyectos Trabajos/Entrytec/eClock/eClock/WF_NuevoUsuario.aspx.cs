using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WF_NuevoUsuario : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void WebImageButton1_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        LError.Text = "";
        LCorrecto.Text = "";
        int Persona_ID = CeC_Personas.ObtenPersonaID(Convert.ToInt32(Tbx_Persona_link_ID.Value));
        if (Persona_ID > 0)
        {
            if (CeC_Usuarios.ObtenUsuarioID(Tbx_eMail.Text) > 0)
            {
                LError.Text = "El correo electronico se ha usado con otro usuario<br>si usted es propietario de este correo pude usar la opcion recuperar contraseña";
                return;
            }
            string Usuario = CeC_Usuarios.ObtenUsuario(Persona_ID);
            if (Usuario.Length > 0)
            {
                LError.Text = "Ya se ha creado previamente un usuario ("+Usuario+") para este empleado<br>si usted es propietario pude usar la opcion recuperar contraseña";
                return;
            }
            if (CeC_Usuarios.CreaUsuarioDesdeEmpeado(Persona_ID, Tbx_eMail.Text) > 0)
            {
                LCorrecto.Text = "Se ha creado correctamente su usuario, se le enviara por correo electronico su clave de acceso";
            }
            else
                LError.Text = "No se pudo crear el usuario verifiquelo con su personal de soporte, posiblemente el mail no coincida";

        }
        else
        {
            LError.Text = "No se encontro al empleado";
        }
    }
}
