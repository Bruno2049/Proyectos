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

public partial class WF_PersonasTerminales : System.Web.UI.Page
{
    DS_Terminales DSTerminales = new DS_Terminales();
    DS_TerminalesTableAdapters.DataTablePersonasTermTableAdapter TATerminal = new DS_TerminalesTableAdapters.DataTablePersonasTermTableAdapter();
    protected int Persona_ID;
    protected CeC_Sesion Sesion;
    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        Sesion.TituloPagina = "Asignación de Terminales a Empleado";
        Sesion.DescripcionPagina = "Escoja las Terminales por las que tendrá Acceso el Empleado";
        lnombre.Text = CeC_BD.ObtenPersonaNombre(Sesion.eClock_Persona_ID);

        //Agregar Módulo Log
        if (!IsPostBack)
        {
            Persona_ID = Sesion.eClock_Persona_ID;
/*<<<<<<< .mine
            //TATerminal.ActualizaIn("AND EC_AUTONUM.SUSCRIPCION_ID = " + Sesion.SUSCRIPCION_ID);
            //TATerminal.FillPersona_ID(DSTerminales.DataTablePersonasTerm, Persona_ID);
            string QRY = "";
            QRY = "SELECT TERMINAL_ID, TERMINAL_NOMBRE, SITIO_NOMBRE, " +
                    "(SELECT PERSONA_TERMINAL_UPDATE FROM EC_PERSONAS_TERMINALES WHERE TERMINAL_ID = EC_TERMINALES.TERMINAL_ID AND PERSONA_ID = " + Persona_ID + ") AS PERSONA_TERMINAL_UPDATE, " +
                    "(SELECT (CASE WHEN PERSONA_ID IS NULL THEN 0 ELSE 1 END) AS ACTIVO FROM EC_PERSONAS_TERMINALES WHERE TERMINAL_ID = EC_TERMINALES.TERMINAL_ID AND PERSONA_ID = " + Persona_ID + " AND PERSONA_TERMINAL_BORRADO = 0) AS ACTIVO " +
                    "FROM EC_TERMINALES, EC_SITIOS ";
            QRY += " WHERE TERMINAL_BORRADO = 0  AND EC_TERMINALES.SITIO_ID = EC_SITIOS.SITIO_ID " +
                " AND " + CeC_Autonumerico.ValidaSuscripcion("EC_TERMINALES", "TERMINAL_ID", Sesion.SUSCRIPCION_ID);
            QRY += " ORDER BY TERMINAL_NOMBRE";
            DataSet DS = (DataSet)CeC_BD.EjecutaDataSet(QRY);
=======*/
            DataSet DS = CeC_Terminales.ObtenPersonaTerminalDS(Persona_ID, Sesion.SUSCRIPCION_ID);
            if (DS == null)
                return;
//>>>>>>> .r8499
            UWGTerminales.DataSource = DS;
            UWGTerminales.DataMember = DS.Tables[0].TableName;
            UWGTerminales.DataKeyField = "TERMINAL_ID";
            UWGTerminales.DataBind();
        }
    }

    protected void UWGTerminales_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        CeC_Grid.AplicaFormato(UWGTerminales, true, false, false, true);
        UWGTerminales.Columns[0].Hidden = true;
        UWGTerminales.Columns[1].AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No;
        UWGTerminales.Columns[4].Type = Infragistics.WebUI.UltraWebGrid.ColumnType.CheckBox;
    }

    protected void UWGTerminales_UpdateRowBatch(object sender, Infragistics.WebUI.UltraWebGrid.RowEventArgs e)
    {
        CeC_BD.ActualizaRowBatch(UWGTerminales, e, DSTerminales, DSTerminales.DataTablePersonasTerm.TableName.ToString(), "TERMINAL_ID");
    }

    protected void btnregresar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        Sesion.Redirige("WF_EmpleadosEd.aspx");
    }

    protected void WIBtn_Guardar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        Persona_ID = Sesion.eClock_Persona_ID;
        int existe;
        for (int i = 0; i < UWGTerminales.Rows.Count; i++)
        {
            int idterminal = Convert.ToInt32(UWGTerminales.Rows[i].Cells[0].Value);
            bool DarAcceso = Convert.ToBoolean(UWGTerminales.Rows[i].Cells[4].Value);
            if (DarAcceso)
                CeC_Terminales.PermisoDa(Persona_ID, idterminal);
            else
                CeC_Terminales.PermisoQuita(Persona_ID, idterminal);

        }
    }
}
