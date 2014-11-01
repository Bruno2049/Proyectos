using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class WF_Solicitudes : System.Web.UI.Page
{
    CeC_Sesion Sesion;
    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
    }
    protected void BGuardarCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        LCorrecto.Text = "";
        LError.Text = "";
        int Numero_registros = Grid.Rows.Count;

        for (int i = 0; i < Numero_registros; i++)
        {
            if (Grid.Rows[i].Selected)
            {
                try
                {
                    int Solicitud_ID = Convert.ToInt32(Grid.Rows[i].DataKey);
                    if (CeC_Solicitudes.CambiaEstado(Solicitud_ID, CeC_Solicitudes.EstadoSolicitud.Autorizado,Sesion))
                    {
                        LCorrecto.Text = "Se han autorizado con exito";
                        return;
                    }
                    LError.Text = "No se pudo autorizadar";
                    return;
                }
                catch (Exception ex)
                {
                    LError.Text = "Error : " + ex.Message;
                    return;
                }
            }
        }
        LError.Text = "Debes de seleccionar una fila";

    }
    protected void BDeshacerCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        LCorrecto.Text = "";
        LError.Text = "";
        int Numero_registros = Grid.Rows.Count;

        for (int i = 0; i < Numero_registros; i++)
        {
            if (Grid.Rows[i].Selected)
            {
                try
                {
                    int Solicitud_ID = Convert.ToInt32(Grid.Rows[i].DataKey);
                    if (CeC_Solicitudes.CambiaEstado(Solicitud_ID, CeC_Solicitudes.EstadoSolicitud.Denegado, Sesion))
                    {
                        LCorrecto.Text = "Se han denegado satisfactoriamente";
                        return;
                    }
                    LError.Text = "No se pudo cancelar";
                    return;
                }
                catch (Exception ex)
                {
                    LError.Text = "Error : " + ex.Message;
                    return;
                }
            }
        }
        LError.Text = "Debes de seleccionar una fila";


    }

    protected void ActualizaDatos()
    {
        try
        {
            Sesion = CeC_Sesion.Nuevo(this);
            DataSet DS = CeC_Solicitudes.ObtenDetalles(Sesion.USUARIO_ID);
            Grid.DataSource = DS;
            Grid.DataMember = DS.Tables[0].TableName;
            Grid.DataKeyField = "SOLICITUD_ID";
        }
        catch { }
    }
    protected void Grid_InitializeDataSource(object sender, Infragistics.WebUI.UltraWebGrid.UltraGridEventArgs e)
    {
        ActualizaDatos();
    }
    protected void Grid_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        CeC_Grid.AplicaFormato(Grid, false, false, false, false);
    }
}

