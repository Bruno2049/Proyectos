using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class WF_ConfigUsuario : System.Web.UI.Page
{
    CeC_Sesion Sesion;
    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        Sesion.Redirige("WF_Tabla.aspx?Parametros=EC_CONFIG_USUARIO");
    }
    /// <summary>
    /// Edita la configuración
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Btn_EditarPeriodo_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        Sesion.Redirige("WF_Tabla.aspx?Parametros=EC_CONFIG_USUARIO");
    }

    protected void Uwg_EC_CONFIG_USUARIO_InitializeDataSource(object sender, Infragistics.WebUI.UltraWebGrid.UltraGridEventArgs e)
    {
        ActualizaDatos();
    }
    protected void Uwg_EC_CONFIG_USUARIO_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        CeC_Grid.AplicaFormato(Uwg_EC_CONFIG_USUARIO, false, false, false, false);
    }
    protected void ActualizaDatos()
    {
        try
        {
            //Sesion = CeC_Sesion.Nuevo(this);
            DataSet DS = CeC_ConfigUsuario.ObtenConfigDetalle(Sesion.WF_Usuarios_USUARIO_ID);
            Uwg_EC_CONFIG_USUARIO.DataSource = DS;
            Uwg_EC_CONFIG_USUARIO.DataMember = DS.Tables[0].TableName;
            Uwg_EC_CONFIG_USUARIO.DataKeyField = "CONFIG_USUARIO_ID";
        }
        catch { }
    }
}