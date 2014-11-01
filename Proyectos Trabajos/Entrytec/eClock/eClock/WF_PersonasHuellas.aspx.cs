using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class WF_PersonasHuellas : System.Web.UI.Page
{
    CeC_Sesion Sesion = null;
    CeC_TablaBD Tabla = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        if (Sesion.SESION_ID <= 0)
            return;
        DataSet DS_A_Vec = (DataSet)CeC_BD.EjecutaDataSet("SELECT ALMACEN_VEC_ID, ALMACEN_VEC_NOMBRE FROM EC_ALMACEN_VEC WHERE ALMACEN_VEC_BORRADO = 0");
        if (DS_A_Vec != null && DS_A_Vec.Tables.Count > 0 && DS_A_Vec.Tables[0].Rows.Count > 0)
        {
            string Campos = "";
            foreach (DataRow DR_AVec in DS_A_Vec.Tables[0].Rows)
            {
                string Concatena = " + ";
                string PersonaID = "CONVERT(varchar,PERSONA_ID)";
                if (CeC_BD.EsOracle)
                    Concatena = " || ";

                Campos = CeC.AgregaSeparador(Campos, "'WF_Personas_ImaS.aspx?P='" + Concatena + PersonaID + Concatena + "'&AV="+ DR_AVec[0].ToString() + "'", ", ");
            }
            DataSet DS = (DataSet)CeC_BD.EjecutaDataSet("SELECT PERSONA_ID, PERSONA_LINK_ID, PERSONA_NOMBRE, AGRUPACION_NOMBRE, " + Campos +
                " FROM EC_PERSONAS WHERE PERSONA_BORRADO = 0 AND " + CeC_Autonumerico.ValidaSuscripcion("EC_PERSONAS", "PERSONA_ID", Sesion.SUSCRIPCION_ID) + " ORDER BY PERSONA_LINK_ID");

            if (DS == null || DS.Tables.Count < 1)
                return;
            if (IsPostBack)
                Grid.AutoGenerateColumns = true;
            Grid.DataSource = DS.Tables[0];
            Grid.DataMember = DS.Tables[0].TableName;
            Grid.DataKeyFields = "PERSONA_ID";
            if (!IsPostBack)
            {
                CeC_Grid.AplicaFormato(Grid);
                Grid.DataBind();
            }
            if (IsPostBack)
                Grid.AutoGenerateColumns = false;
        }


    }
    protected void Grid_InitializeRow(object sender, Infragistics.Web.UI.GridControls.RowEventArgs e)
    {

        for (int Cont = 0; Cont < e.Row.Items.Count; Cont++)
        {
            e.Row.Items[Cont].CssClass = "red";
        }
    }
}