using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using System.IO;

public partial class WF_ExportarIncidenciasOracleBS : System.Web.UI.Page
{
    CeC_Sesion Sesion;
    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        CMd_Base.gActualizaTiposNomina(Sesion.SUSCRIPCION_ID);
    }

    /// <summary>
    /// Actualiza los datos del Grid
    /// </summary>
    protected void ActualizaDatos()
    {
        try
        {
            Sesion = CeC_Sesion.Nuevo(this);
            DataSet DS = CeC_Periodos_N.ObtenPeriodosDetalle(DateTime.Now.AddMonths(-2), DateTime.Now, 0, Sesion.SUSCRIPCION_ID);
            Grid.DataSource = DS;
            Grid.DataMember = DS.Tables[0].TableName;
            Grid.DataKeyField = "PERIODO_ID";
        }
        catch { }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Grid_InitializeDataSource(object sender, Infragistics.WebUI.UltraWebGrid.UltraGridEventArgs e)
    {
        ActualizaDatos();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Grid_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        CeC_Grid.AplicaFormato(Grid, false, false, false, false);
    }

    /// <summary>
    /// Descarga el archivo de exportación "CMd_OracleBS.tmp" generado en el envio de incidencias
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void LnkBtn_Descarga_Click(object sender, EventArgs e)
    {
        Sesion.Redirige("WF_Descarga.aspx?Parametros=CMd_OracleBS.tmp");
    }
    /// <summary>
    /// Redirige a la pantalla de Edición de Periodos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void WIBtn_Editar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        Sesion.Redirige("WF_Tabla.aspx?Parametros=EC_PERIODOS");// + Sesion.SuscripcionParametro);
        //Sesion.Redirige("WF_Tabla.aspx?Parametros=" + Sesion.SuscripcionParametro);
    }
    /// <summary>
    /// Envia las incidencias del periodo seleccionado y cierra el periodo
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void WIBtn_Guardar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        Lbl_Correcto.Text = "";
        Lbl_Error.Text = "";
        LnkBtn_Descarga.Visible = false;
        int Numero_registros = Grid.Rows.Count;

        for (int i = 0; i < Numero_registros; i++)
        {
            if (Grid.Rows[i].Selected)
            {
                try
                {
                    int Periodo_ID = Convert.ToInt32(Grid.Rows[i].DataKey);
                    DateTime Inicio = Convert.ToDateTime(Grid.Rows[i].Cells.FromKey("PERIODO_ASIS_INICIO").Value);
                    DateTime Fin = Convert.ToDateTime(Grid.Rows[i].Cells.FromKey("PERIODO_ASIS_FIN").Value);
                    string TipoNomina = CeC.Convierte2String(Grid.Rows[i].Cells.FromKey("TIPO_NOMINA_NOMBRE").Value);
                    string FiltroEmpleados = "SELECT EC_PERSONAS.PERSONA_ID FROM EC_PERSONAS_DATOS, EC_PERSONAS WHERE EC_PERSONAS_DATOS.PERSONA_ID = EC_PERSONAS.PERSONA_ID AND PERSONA_BORRADO = 0 AND TIPO_NOMINA = '" + TipoNomina + "' AND SUSCRIPCION_ID = " + Sesion.SUSCRIPCION_ID;
                    if (CeC_Periodos.ObtenEstado(Periodo_ID) == CeC_Periodos.EDO_PERIODO.Cerrado)
                    {
                        Lbl_Correcto.Text = "Periodo Procesado";
                        return;
                    }
                    if (CMd_Base.gEnviaIncidencias(Inicio, Fin, Periodo_ID, FiltroEmpleados))
                    {
                        LnkBtn_Descarga.Visible = true;
                        Lbl_Correcto.Text = "Se han enviado las incidencias con exito";
                        return;
                    }
                    else
                    {
                        Lbl_Error.Text = "No se pudieron enviar las incidencias";
                    }
                    return;
                }
                catch (Exception ex)
                {
                    Lbl_Error.Text = "Error : " + ex.Message;
                    return;
                }
            }
        }
        Lbl_Error.Text = "Debes de seleccionar una fila";
    }

    /// <summary>
    /// Deshace los cambios realizados
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void WIBtn_Deshacer_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {

    }
}
