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

public partial class WF_AgrupacionPermisos : System.Web.UI.Page
{
    CeC_Sesion Sesion = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        LError.Text = "";
        LCorrecto.Text = "";
        Sesion = CeC_Sesion.Nuevo(this);
        if (!IsPostBack)
        {
            //Catalogo de permisos


            Cmb_Permiso.DataValueField = "TIPO_PERMISO_ID";
            Cmb_Permiso.DataTextField = "TIPO_PERMISO_NOMBRE";
            Cmb_Permiso.DataSource = CeC_Campos.CatalogoDT(40, Sesion); ;
            Cmb_Permiso.DataBind();
            ActualizaDatos();
            Cmb_Permiso.SelectedIndex = 0;
        }

    }
    protected void BtnAgregar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        int UsuarioID = CeC_Usuarios.ObtenUsuarioID(Tbx_Usuario.Text);
        if (UsuarioID <= 0)
        {
            LError.Text = "El usuario no se ha encontrado";
            return;
        }
        int PermisoID = 0;
        try
        {
            PermisoID = Convert.ToInt32( CeC_Grid.RegresaValor(Cmb_Permiso));
        }
        catch {
            LError.Text = "Permiso no Valido";
            return;
        }

        if (!CeC_Agrupaciones.AsignaPermiso(Sesion.SESION_ID, Sesion.SUSCRIPCION_ID, UsuarioID, Sesion.eClock_Agrupacion, PermisoID))
            LError.Text = "No se pudo cargar el permiso";
        else
        {
            ActualizaDatos();
            LCorrecto.Text = "Permiso agregado";
        }
    }

    protected void ActualizaDatos()
    {
        if (Sesion == null)
            Sesion = CeC_Sesion.Nuevo(this);
        DataSet ds = CeC_Agrupaciones.ObtenPermisosUsuarios(Sesion.SUSCRIPCION_ID, Sesion.eClock_Agrupacion);
        if (ds != null)
        {
            Grid.DataSource = ds;
            Grid.DataMember = ds.Tables[0].TableName;
            Grid.DataKeyField = "USUARIO_PERMISO_ID";
            Grid.DataBind();
        }
    }

    protected void Grid_InitializeDataSource(object sender, Infragistics.WebUI.UltraWebGrid.UltraGridEventArgs e)
    {
        ActualizaDatos();
    }
    protected void Grid_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        CeC_Grid.AplicaFormato(Grid, false, false, false, false);
    }
    protected void BtnQuitar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        LCorrecto.Text = "";
        LError.Text = "";
        int Numero_Reg = Convert.ToInt32(Grid.Rows.Count);
        int Modificados = 0;

        for (int i = 0; i < Numero_Reg; i++)
        {
            if (Grid.Rows[i].Selected)
            {
                try
                {
                    int UsuarioPermisoID = Convert.ToInt32(Grid.Rows[i].Cells[0].Value);
                    if (CeC_Agrupaciones.QuitaPermisoUsuario(UsuarioPermisoID))
                    {
                        LCorrecto.Text = "Permiso eliminado";
                        Grid.Clear();
                        ActualizaDatos();
                    }
                    else
                        LError.Text = "No se pudo eliminar el permiso";
                    return;

                }
                catch (Exception ex)
                {
                    LError.Text = "Error " + ex.Message;
                    return;
                }
            }
        }
        LError.Text = "Debes de seleccionar una fila";
    }
}
