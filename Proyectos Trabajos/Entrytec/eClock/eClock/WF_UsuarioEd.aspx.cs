using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WF_UsuarioEd : System.Web.UI.Page
{
    CeC_Sesion Sesion;
    public void Cargar()
    {
        Tbx_Usuario.Text = Sesion.USUARIO_USUARIO;
        Tbx_Nombre.Text = Sesion.USUARIO_NOMBRE;
        Tbx_Correo.Text = CeC_Usuarios.ObtenUsuarioeMail(Sesion.USUARIO_ID);
        CeC_Config Config = new CeC_Config(Sesion.USUARIO_ID);

        Cbx_Faltas.Checked = Config.EnviarMailFaltas;
        Cbx_SolicitudPermisos.Checked = Config.EnviarMailSolicitudIncidencia;
        Cbx_Retardos.Checked = Config.EnviarMailRetardos;
        Cbx_TerminalNoResponde.Checked = Config.EnviarMailTerminalNoResponde;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        if (Sesion.SESION_ID > 0)
        {
            if (!IsPostBack)
            {
                Cargar();
            }
        }
    }
    protected void BGuardarCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        LError.Text = "";
        LCorrecto.Text = "";
        if (Sesion.SESION_ID > 0)
        {
            if (!CeC_Usuarios.CambiaUsuarioeMail(Sesion.USUARIO_ID, Tbx_Correo.Text))
            {
                LError.Text = "No se pudo guardar, intentelo nuevamente";
                return;
            }
            CeC_Config Config = new CeC_Config(Sesion.USUARIO_ID);
            Config.EnviarMailFaltas = Cbx_Faltas.Checked;
            Config.EnviarMailSolicitudIncidencia = Cbx_SolicitudPermisos.Checked;
            Config.EnviarMailRetardos = Cbx_Retardos.Checked;
            Config.EnviarMailTerminalNoResponde = Cbx_TerminalNoResponde.Checked;
            if (Tbx_ClaveAct.Text.Length > 0)
            {
                if(!CeC_Usuarios.CambiaPassword(Sesion.USUARIO_ID, Tbx_ClaveAct.Text,Tbx_ClaveNueva.Text))
                {
                    LError.Text = "No se pudo cambiar la clave, posiblemente su clave actual no coincide";
                    return;
                }
            }
            LCorrecto.Text = "Datos guardados satisfactoriamente";
        }
    }

    protected void BDeshacerCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        Cargar();
    }
}
