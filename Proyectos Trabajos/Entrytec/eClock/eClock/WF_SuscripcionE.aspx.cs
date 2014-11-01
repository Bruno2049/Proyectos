using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WF_SuscripcionE : System.Web.UI.Page
{
    CeC_Sesion Sesion;

    /// <summary>
    /// Método que carga la página.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        if (Valida())
        {
            if (!IsPostBack)
            {
                CargaDatos();
            }
        }
    }

    protected void WIBtn_Guardar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        Lbl_Correcto.Text = "";
        Lbl_Error.Text = "";
        if (Valida())
        {
            bool Guardado = false;
            int SuscripcionID = Convert.ToInt32(Wtx_SUSCRIPCION_ID.Text);
            if (SuscripcionID < 0)
            {
                SuscripcionID = CeC_Suscripcion.CreaSuscripcion(Sesion, Wtx_SUSCRIPCION_NOMBRE.Text, Wtx_SUSCRIP_DATOS_RAZON.Text, Wtx_SUSCRIP_DATOS_EMAIL.Text);
                if (SuscripcionID > 0)
                {
                    Wtx_SUSCRIPCION_ID.Text = SuscripcionID.ToString();
                    Guardado = true;
                }
            }
            else
                Guardado = CeC_Suscripcion.GuardaSuscripcion(SuscripcionID, Wtx_SUSCRIPCION_NOMBRE.Text, Wtx_SUSCRIP_DATOS_RAZON.Text, Wtx_SUSCRIP_DATOS_EMAIL.Text, Chb_SUSCRIPCION_BORRADO.Checked);
            if (Guardado)
                Lbl_Correcto.Text = "Datos guardados satisfactoriamente";
            else
                Lbl_Error.Text = "No se pudo guardar";
        }
    }

    protected void WIBtn_Editar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        Sesion.Redirige("WF_SuscripcionDatosE.aspx?Parametros=" + Wtx_SUSCRIPCION_ID.Text);
    }

    protected void WIBtn_AgregarUsuario_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        int Perfil = 0;
        Lbl_Error.Text = "";
        Lbl_Correcto.Text = "";
        try{
        Perfil = Convert.ToInt32( CeC_Grid.RegresaValor(Wco_PERFIL_ID));
        }
        catch{}
        int SuscripcionID = Convert.ToInt32(Wtx_SUSCRIPCION_ID.Text);
        int UsuarioID = CeC_Usuarios.AgregaUsuario(Wtx_USUARIO_USUARIO.Text, Perfil, Wtx_USUARIO_NOMBRE.Text, Wtx_USUARIO_DESCRIPCION.Text, Wtx_USUARIO_EMAIL.Text, SuscripcionID, Sesion.SESION_ID);
        if (UsuarioID < 0)
        {
            Lbl_Error.Text = " No se pudo crear el usuario";
        }
        if (UsuarioID == 0)
            Lbl_Error.Text = " Nombre de usuario/eMail ya existente, elija otro";
        if (UsuarioID > 0)
            Lbl_Correcto.Text = "Usuario " + Wtx_USUARIO_EMAIL.Text + " creado con Exito";

    }
    
    protected void WIBtn_Usuarios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        Sesion.Redirige("WF_Usuarios.aspx?SuscripcionID=" + Wtx_SUSCRIPCION_ID.Text);
    }
    
    protected void WIBtn_Sitios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        Sesion.Redirige("WF_Tabla.aspx?Parametros=EC_SITIOS");
    }
    
    protected void WIBtn_Terminales_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        Sesion.Redirige("WF_Terminales.aspx?SuscripcionID="+ Wtx_SUSCRIPCION_ID.Text);
    }
    
    protected void CmbPerfil_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        CeC_Grid.AplicaFormato(Wco_PERFIL_ID);
    }
    
    protected void WIBtn_Regresar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        Sesion.Redirige("WF_Suscripciones.aspx");
    }

    bool Valida()
    {
        if (Sesion.SUSCRIPCION_ID != 1)
        {
            Sesion.MsgNoTienePermiso();
            return false;
        }
        return true;
    }

    /// <summary>
    /// Carga los datos con la información de la Suscripción
    /// </summary>
    void CargaDatos()
    {
        try
        {
            // Se usa el parametro de Sesión que se manda desde la página de Suscripción cono ID de la Suscripción
            int l_SuscripcionID = Convert.ToInt32(Sesion.Parametros);
            // Se muestra en la caja de texto el ID de la suscripción
            Wtx_SUSCRIPCION_ID.Text = l_SuscripcionID.ToString();
            // Validamos que sea una Suscripción válida y llenamos los controles
            if (l_SuscripcionID > 0)
            {
                Wpn_EC_USUARIOS_.Visible = true;
                WIBtn_AgregarUsuario.Visible = true;
                WIBtn_Sitios.Visible = true;
                WIBtn_Usuarios.Visible = true;
                WIBtn_Terminales.Visible = true;

                System.Data.DataRow DR = CeC_Suscripcion.ObtenSuscripcionDatos(l_SuscripcionID);
                if (DR != null)
                {
                    Wtx_SUSCRIPCION_NOMBRE.Text = Convert.ToString(DR["SUSCRIPCION_NOMBRE"]);
                    Wtx_SUSCRIP_DATOS_RAZON.Text = Convert.ToString(DR["USUARIO_NOMBRE"]);
                    WtxC_SUSCRIP_DATOS_EMAIL.Text = Wtx_SUSCRIP_DATOS_EMAIL.Text = Convert.ToString(DR["USUARIO_EMAIL"]);
                    if (Convert.ToBoolean(DR["SUSCRIPCION_BORRADO"]))
                        Chb_SUSCRIPCION_BORRADO.Checked = true;
                }
                CeC_Grid.AsignaCatalogo(Wco_PERFIL_ID, 42);
            }
        }
        catch { }
    }
}
