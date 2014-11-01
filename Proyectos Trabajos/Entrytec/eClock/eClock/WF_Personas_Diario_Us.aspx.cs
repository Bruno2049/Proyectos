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

public partial class WF_Personas_Diario_Us : System.Web.UI.Page
{
    CeC_Sesion Sesion;

    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        Sesion.TituloPagina = "Asistencias completas";
        Sesion.DescripcionPagina = "Asistencias, Retardos y Justificaciones por mes";
        if(!IsPostBack)
        {
            string QueryGrupo = "";

            if (Sesion.TienePermiso(eClock.CEC_RESTRICCIONES.S0Empleados0Listado))
                QueryGrupo = "";
            else
                QueryGrupo = " and (EC_PERSONAS.SUSCRIPCION_ID in (Select EC_PERMISOS_SUSCRIP.SUSCRIPCION_ID from EC_PERMISOS_SUSCRIP where EC_PERMISOS_SUSCRIP.usuario_id = " + Sesion.USUARIO_ID + "))";
            string Qry = "SELECT PERSONA_ID FROM EC_PERSONAS WHERE PERSONA_BORRADO = 0 AND PERSONA_ID > 0 AND PERSONA_ID in (SELECT PERSONA_ID FROM EC_PERSONAS_DIARIO)" + QueryGrupo;
            DataSet DS = (DataSet) CeC_BD.EjecutaDataSet(Qry);
            foreach (System.Data.DataRow DR in DS.Tables[0].Rows)
            {
                TableRow Fila = new TableRow();
                TableCell Celda = new TableCell();
                WC_Mes Mes = new WC_Mes();
                
//                
                Celda.Controls.Add(Mes.LoadControl("WC_Mes.ascx"));
                Fila.Cells.Add(Celda);
                Tabla.Rows.Add(Fila);
                 Mes = (WC_Mes)Celda.Controls[0];
                Mes.Inicializa(Convert.ToInt32(DR[0]), DateTime.Now.AddDays(-15), false);
            }
            //Agregar Módulo Log
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Asistencias Completas", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
        }
    }

    protected void Grid_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        CeC_Grid.AplicaFormato(Grid);
    }

    protected void Grid2_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        CeC_Grid.AplicaFormato(Grid2);
    }

    protected void Grid_Load(object sender, EventArgs e)
    {
        CeC_Grid.AplicaFormato(Grid, false, false, false, false);
        Grid.Columns[0].Width = Unit.Pixel(300);
    }

    protected void Grid2_Load(object sender, EventArgs e)
    {
        CeC_Grid.AplicaFormato(Grid2, false, false, false, false);
    }

    protected void LBAnterior_Click(object sender, EventArgs e)
    {
    }

    protected void LBSiguiente_Click(object sender, EventArgs e)
    {
    }

    protected void LBActual_Click(object sender, EventArgs e)
    {
    }
}
