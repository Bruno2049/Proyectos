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

public partial class WF_ModulosOpcionalesEd : System.Web.UI.Page
{
    CeC_Sesion Sesion;

    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        CMd_Base Clase = CMd_Base.CreaClase(Sesion.WF_ModuloOpcional);
        Sesion.TituloPagina = "Configuración del Módulo Opcional " + Clase.LeeNombre(); ;
        Sesion.DescripcionPagina = "Seleccione las Opciones a Editar del Módulo Opcional ";

        // Permisos****************************************
        if (!Sesion.TienePermisoOHijos(eClock.CEC_RESTRICCIONES.S0Configuracion0ModulosOpc0Edicion, true))
        {
            //Habilitarcontroles();
            return;
        }
        //**************************************************

        if (!IsPostBack)
        {
            /*****************
             Agregar ModuloLog
             *****************/
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Edición Módulo Opcional", 0, "Edición de Módulo Opcional", Sesion.SESION_ID);
        }
    }

    protected void Grid_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        CeC_Grid.AplicaFormato(Grid, true, false, false, true);
    }

    protected void Grid_InitializeDataSource(object sender, Infragistics.WebUI.UltraWebGrid.UltraGridEventArgs e)
    {
        ActualizaDatos();
    }

    /// <summary>
    /// Actualiza los datos en el Grid
    /// </summary>
    protected void ActualizaDatos()
    {
        Sesion = CeC_Sesion.Nuevo(this);
        string mod = Sesion.WF_ModuloOpcional;
        DataSet DS = (DataSet)CeC_BD.EjecutaDataSet("SELECT CONFIG_USUARIO_VARIABLE, CONFIG_USUARIO_VALOR " +
                                   "FROM EC_CONFIG_USUARIO WHERE (CONFIG_USUARIO_VARIABLE LIKE '" + mod + "%')");
        foreach (DataRow DR in DS.Tables[0].Rows)
        {
            string Parametro = DR[0].ToString();
            DR[0] = Parametro.Substring(Parametro.IndexOf(".")+1);
        }
        Grid.DataSource = DS;
    }

    protected void btn_Regresar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        Sesion.Redirige("WF_ModulosOpcionales.aspx");
    }

    protected void btn_Guardar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        int reg = 0;
        for (int i = 0; i < Grid.Rows.Count; i++)
        {
            reg = CeC_BD.EjecutaComando("UPDATE EC_CONFIG_USUARIO SET CONFIG_USUARIO_VALOR = '" + Grid.Rows[i].Cells[1].Value.ToString() + "'" +
                                   " WHERE CONFIG_USUARIO_VARIABLE = '" + Sesion.WF_ModuloOpcional + "." + Grid.Rows[i].Cells[0].Value.ToString() + "'");
        }
        if (reg == 0)
            LError.Text = "Los cambios no pudieron guardarse";
        else
            LCorrecto.Text = "Se han guardado los cambios correctamente";
    }
}
