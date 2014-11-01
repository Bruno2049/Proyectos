using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class WF_Suscripciones : System.Web.UI.Page
{
    CeC_Sesion Sesion;

    bool Valida()
    {
        if (Sesion.SUSCRIPCION_ID != 1)
        {
            Sesion.MsgNoTienePermiso();
            return false;
        }
        return true;
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        if (Valida())
        {
            string Parametro = Sesion.Parametros;
            if (!IsPostBack)
            {
                CargaDatosDS();
                Grid.DataBind();
            }
            //if (Parametro.Length > 1 && Parametro.Substring(1) != "null")
            //{
                //int SuscripcionID = Convert.ToInt32(Parametro.Substring(1));
                //if (Parametro[0] == 'E')
                //{
                //    Sesion.Redirige("WF_SuscripcionE.aspx?Parametros=" + SuscripcionID);
                //}
                //if (Parametro[0] == 'B')
                //{
                //    Borrar(SuscripcionID);
                //}
            //}
        }
    }
    /// <summary>
    /// Borra la Suscripción
    /// </summary>
    /// <param name="SuscripcionID">Identificador de la Suscripción</param>
    /// <returns>Verdadero si se borro la Sucripción. Falso en caso de que no se ha borrado la Suscripción</returns>
    bool Borrar(int SuscripcionID)
    {
        Lbl_Correcto.Text = "";
        Lbl_Error.Text = "";

        try
        {
            if (SuscripcionID <= 0)
            {
                Lbl_Error.Text = "Debe de seleccionar una fila";
                return false;
            }
            if (CeC_Suscripcion.BorraSuscripcion(SuscripcionID)) 
            {
                Lbl_Correcto.Text = " Suscripcion " + SuscripcionID + " dada de baja correctamente";
                Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.BORRADO, "Suscripcion", SuscripcionID, "", Sesion.SESION_ID);
                CargaDatosDS();
                Grid.DataBind();
                return true; ;
            }
            Lbl_Error.Text = "Error : No se pudo dar de baja la suscripcion";
        }
        catch (Exception ex)
        {
            Lbl_Error.Text = "Error :" + ex.Message;
            return false;
        }
        return false;
    }

    protected void WIBtn_Nuevo_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        if (Valida())
        {
            Sesion.Redirige("WF_SuscripcionE.aspx?Parametros=-1" );
        }
    }
    protected DataSet DS = null;
    /// <summary>
    /// Carga todas las sucripciónes del sistema.
    /// </summary>
    protected void CargaDatosDS()
    {
        
        //  CeC_Campos.ReiniciaCampos();
        DS = CeC_Suscripcion.ObtenSuscripcionesDSGrid(Chb_EmpleadosBorrados.Checked);
        if (DS != null)
        {
            Grid.DataSource = DS;
            Grid.DataMember = DS.Tables[0].TableName;
            Grid.DataKeyField = CeC_Suscripcion.CampoLlave;
            Grid.DataBind();
        }
    }
    protected void Grid_DataSource(object sender, Infragistics.WebUI.UltraWebGrid.UltraGridEventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        if (Valida())
        {
            CargaDatosDS();
            Grid.DataBind();
        }
    }
    protected void Grid_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        Grid.Attributes.Add("CellClickHandler", "if (Grid_CellClickHandler" +
        "'Grid',igtbl_getActiveCell('Grid'),0) {return false;})");
        CeC_Grid.AplicaFormato(Grid, true, true, false, false);
    }
    protected void CBEmpBorrados_CheckedChanged(object sender, EventArgs e)
    {
        CargaDatosDS();
        Grid.DataBind();
    }
    protected void WIBtn_Editar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        int Numero_Reg = Convert.ToInt32(Grid.Rows.Count);

        for (int i = 0; i < Numero_Reg; i++)
        {
            if (Grid.Rows[i].Selected)
            {
                //Sesion.USUARIO_ID = Convert.ToInt32(UltraWebGrid1.Rows[i].Cells[0].Value);
                int SuscripcionID = Convert.ToInt32(Grid.Rows[i].Cells[0].Value);
                //Sesion.SuscripcionParametro = Usua_ID;
                Sesion.Redirige("WF_SuscripcionE.aspx?Parametros=" + SuscripcionID.ToString());
                //CeC_Sesion.GuardaIntSesion(this, "USUARIO_ID", SuscripcionID);
                //CeC_Sesion.Redirige(this, "WF_SuscripcionE.aspx");
                return;
            }
        }
    }
    protected void WIBtn_Borrar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        int Numero_Reg = Convert.ToInt32(Grid.Rows.Count);

        for (int i = 0; i < Numero_Reg; i++)
        {
            if (Grid.Rows[i].Selected)
            {
                //Sesion.USUARIO_ID = Convert.ToInt32(UltraWebGrid1.Rows[i].Cells[0].Value);
                int SuscripcionID = Convert.ToInt32(Grid.Rows[i].Cells[0].Value);
                Borrar(SuscripcionID);
                CeC_Sesion.GuardaIntSesion(this, "USUARIO_ID", SuscripcionID);
                CeC_Sesion.Redirige(this, "WF_SuscripcionE.aspx");
                return;
            }
        }
    }
}
