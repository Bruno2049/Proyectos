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

public partial class WF_VariablesConfiguracion : System.Web.UI.Page
{
    CeC_Sesion Sesion;
    DS_Config_Usuario DS = new DS_Config_Usuario();
    DS_Config_UsuarioTableAdapters.EC_CONFIG_USUARIO1TableAdapter TA = new DS_Config_UsuarioTableAdapters.EC_CONFIG_USUARIO1TableAdapter();
    

    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        Sesion.TituloPagina = "Variables de Configuración";
        Sesion.DescripcionPagina = "Cambia el valor a la variable de configuración necesaria";

        LError.Text = LCorrecto.Text = "";
        // Permisos****************************************
        if (!Sesion.TienePermisoOHijos(eClock.CEC_RESTRICCIONES.S0Configuracion0Variables, true))
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
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Variables de Configuración", 0, "Cambia valores a las variables de configuración", Sesion.SESION_ID);
        }
    }

    protected void Grid_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        CeC_Grid.AplicaFormato(Grid, true, false, false, true);
        Grid.Columns[0].Hidden = true;
    }

    protected void Grid_InitializeDataSource(object sender, Infragistics.WebUI.UltraWebGrid.UltraGridEventArgs e)
    {
        TA.Fill(DS.EC_CONFIG_USUARIO1);
        Grid.DataSource = DS;
    }

    protected void btn_Regresar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        Sesion.Redirige("WF_Main.aspx");
    }

    protected void btn_Guardar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        LCorrecto.Text = LError.Text = "";
        int variables = Grid.Rows.Count;
        string valor;
        try
        {
            for (int i = 0; i < variables; i++)
            {
                valor = (Grid.Rows[i].Cells[3].Value == null)? "" : Grid.Rows[i].Cells[3].Value.ToString();
                TA.Update(Convert.ToDecimal(Grid.Rows[i].Cells[0].Value), Convert.ToDecimal(Grid.Rows[i].Cells[1].Value), Grid.Rows[i].Cells[2].Value.ToString(), valor, Convert.ToDecimal(Grid.Rows[i].Cells[0].Value));
            }
            LCorrecto.Text = "Los cambios se han guardado correctamente";
        }
        catch
        {
            LError.Text = "No se han podido guardar los cambios";
        }
    }
}
