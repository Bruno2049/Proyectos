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

public partial class WF_ModulosOpcionales : System.Web.UI.Page
{
    CeC_Sesion Sesion;

    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);

        Sesion.TituloPagina = "Configuración de Módulos Opcionales";
        Sesion.DescripcionPagina = "Seleccione el Módulo Opcional a habilitar";

        // Permisos****************************************
        if (!Sesion.TienePermisoOHijos(eClock.CEC_RESTRICCIONES.S0Configuracion0ModulosOpc, true))
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
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Módulos Opcionales", 0, "Configuración de Módulos Opcionales", Sesion.SESION_ID);
        }
    }

    protected void Grid_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        CeC_Grid.AplicaFormato(Grid, true, false, false, true);
        Grid.Columns[0].Hidden = true;
        Grid.Columns[2].Type = Infragistics.WebUI.UltraWebGrid.ColumnType.CheckBox;
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
        DataSet DS = (DataSet)CeC_BD.EjecutaDataSet("SELECT CONFIG_USUARIO_VARIABLE, CONFIG_USUARIO_VALOR, '0' as 'CMd_Base_Habilitado' " +
                                   "FROM EC_CONFIG_USUARIO WHERE (CONFIG_USUARIO_VARIABLE LIKE 'CMD_Base%')");
        foreach (DataRow DR in DS.Tables[0].Rows)
        {
            DR[2] = (CMd_Base.EstaModuloHabilitado(DR[1].ToString()) != null ? 1 : 0);
        }
        Grid.DataSource = DS;
    }


    protected void btn_Guardar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        for (int i = 0; i < Grid.Rows.Count; i++)
        {
            try
            {
                CMd_Base Clase = CMd_Base.CreaClase(Grid.Rows[i].Cells[1].Value.ToString());
                Clase.LeeNombre();
                if (Grid.Rows[i].Cells[2].Value.ToString() == "1" || Grid.Rows[i].Cells[2].Value.ToString() == "true")
                {
                    Clase.CargaParametros();
                    Clase.Habilitado = true;
                    Clase.GuardaParametros();
                }
                else
                {
                    int reg = CeC_BD.EjecutaComando("UPDATE EC_CONFIG_USUARIO SET CONFIG_USUARIO_VALOR = 'False' WHERE CONFIG_USUARIO_VARIABLE = '" +
                                           Grid.Rows[i].Cells[1].Value.ToString() + ".Habilitado'");
                }
                LCorrecto.Text = "Los cambios se han guardado correctamente";
            }
            catch
            {
                LError.Text = "Los cambios no se guardaron correctamente";
                return;
            }
        } 
        
    }

    protected void btn_Editar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        for (int i = 0; i < Grid.Rows.Count; i++)
        {
            if (Grid.Rows[i].Selected)
            {
                Sesion.WF_ModuloOpcional = Grid.Rows[i].Cells[1].Value.ToString();
                Sesion.Redirige("WF_ModulosOpcionalesEd.aspx");
                break;
            }
        }
        LError.Text = "Debes de seleccionar una fila";
    }
}
