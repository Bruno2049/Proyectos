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
using System.Text;

public partial class WF_EdicionExportacionNOI : System.Web.UI.Page
{
    DS_ExportacionNOITableAdapters.EC_TIPO_INC_SISTableAdapter TASis;
    DS_ExportacionNOI DSSis;
    DS_ExportacionNOITableAdapters.EC_TIPO_INCIDENCIASTableAdapter TAPer;
    DS_ExportacionNOI DSPer;
    CeC_Sesion Sesion;
    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        CeC_Grid.AsignaCatalogo(WC_Campos,  "CAMPO_NOMBRE");
        Sesion.TituloPagina = "Configuración del Archivo de Exportación a NOI";
        Sesion.DescripcionPagina = "Para cada Incidencia en eClock se debe tener un equivalente en NOI, debe de asignar a cada incidencia el "+
            "código equivalente en NOI. También debe seleccionar el campo con el que identificará al empleado dentro de NOI, éste debe coincidir con el "+
            "campo en eClock para que los datos concuerden. Al hacer cualquier movimiento, se guardarán los cambios.";
        CeC_Grid.AplicaFormato(WC_Campos);
        CeC_Grid.SeleccionaID(WC_Campos, CeC_Config.CampoLlaveNOI);

        // Permisos****************************************
        if (!Sesion.TienePermisoOHijos(eClock.CEC_RESTRICCIONES.S0Configuracion, true))
        {
            GridPer.Visible = GridSis.Visible = false;
            BtnAsignarPer.Visible = BtnAsignarSis.Visible = false;
            txtAsignarPer.Visible = txtAsignarSis.Visible = false;
            WC_Campos.Visible = false;
            return;
        }
        //**************************************************

        if(!IsPostBack) 
        {   
            //Agregar Módulo Log
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Carga de Desayunos Express", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
        }
    }

    protected void GridSis_InitializeDataSource(object sender, Infragistics.WebUI.UltraWebGrid.UltraGridEventArgs e)
    {
            TASis = new DS_ExportacionNOITableAdapters.EC_TIPO_INC_SISTableAdapter();
            DSSis = new DS_ExportacionNOI();
            TASis.Fill(DSSis.EC_TIPO_INC_SIS);
            for (int i = 0; i < DSSis.EC_TIPO_INC_SIS.Rows.Count; i++)
            {
                try
                {
                    DSSis.EC_TIPO_INC_SIS[i][2] = CeC_Config.ObtenConfig(0, "NCISID_" + DSSis.EC_TIPO_INC_SIS[i][3].ToString() + "_NOI", "");
                }
                catch (Exception ex)
                {
                }
            }
        GridSis.DataSource = DSSis.EC_TIPO_INC_SIS;          
    }

    protected void GridSis_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        CeC_Grid.AplicaFormato(GridSis, false, false, true, true);
        GridSis.Columns[3].Hidden = true;        
    }

    protected void GridPer_InitializeDataSource(object sender, Infragistics.WebUI.UltraWebGrid.UltraGridEventArgs e)
    {
        TAPer = new DS_ExportacionNOITableAdapters.EC_TIPO_INCIDENCIASTableAdapter();
        DSPer = new DS_ExportacionNOI();
        TAPer.Fill(DSPer.EC_TIPO_INCIDENCIAS);
        for (int i = 0; i < DSPer.EC_TIPO_INCIDENCIAS.Rows.Count; i++)
        {
            try
            {
                DSPer.EC_TIPO_INCIDENCIAS[i][2] = CeC_Config.ObtenConfig(0, "NCIPID_" + DSPer.EC_TIPO_INCIDENCIAS[i][3].ToString() + "_NOI", "");
            }
            catch (Exception ex)
            {
            }
        }
        GridPer.DataSource = DSPer.EC_TIPO_INCIDENCIAS;     
    }

    protected void GridPer_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        CeC_Grid.AplicaFormato(GridPer, false, false, true, true);
        GridPer.Columns[3].Hidden = true;        
    }

    protected void BDeshacerCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        Sesion.Redirige("WF_ExportarIncidenciasNOI.aspx");
    }

    protected void BtnAsignarSis_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        LErrorSis.Text = "";
        if (txtAsignarSis.Text.Length == 5)
        {
            StringBuilder Cadena = new StringBuilder(txtAsignarSis.Text);
            if (!char.IsLetter(Cadena[0]))
            {
                LErrorSis.Text = "El Codigo de entrada no es valido";
                return;
            }
            if (!char.IsNumber(Cadena[1]) || !char.IsNumber(Cadena[2]) || !char.IsNumber(Cadena[3]))
            {
                LErrorSis.Text = "El Codigo de entrada no es valido";
                return;
            }
            if (!char.IsLetterOrDigit(Cadena[4]))
            {
                LErrorSis.Text = "El Codigo de entrada no es valido";
                return;
            }
        }
        else
        {
            LErrorSis.Text = "El Codigo de entrada no es valido";
            return;
        }
        for (int i = 0; i < GridSis.Rows.Count; i++)
        {
            if (GridSis.Rows[i].Selected)
            {
                CeC_Config.GuardaConfig(0, "NCISID_" + GridSis.Rows[i].Cells[3].Value.ToString() + "_NOI", txtAsignarSis.Text);
            }
        }
        for (int i = 0; i < DSSis.EC_TIPO_INC_SIS.Rows.Count; i++)
        {
            try
            {
                DSSis.EC_TIPO_INC_SIS[i][2] = CeC_Config.ObtenConfig(0, "NCISID_" + DSSis.EC_TIPO_INC_SIS[i][3].ToString() + "_NOI", "");
            }
            catch (Exception ex)
            {
            }
        }
        GridSis.DataSource = DSSis.EC_TIPO_INC_SIS;
        GridSis.DataBind();
    }

    protected void BtnAsignarPer_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        LErrorPer.Text = "";
        if (txtAsignarPer.Text.Length == 5)
        {
            StringBuilder Cadena = new StringBuilder(txtAsignarPer.Text);
            if (!char.IsLetter(Cadena[0]))
            {
                LErrorPer.Text = "El Codigo de entrada no es valido";
                return;
            }
            if (!char.IsNumber(Cadena[1]) || !char.IsNumber(Cadena[2]) || !char.IsNumber(Cadena[3]))
            {
                LErrorPer.Text = "El Codigo de entrada no es valido";
                return;
            }
            if (!char.IsLetterOrDigit(Cadena[4]))
            {
                LErrorPer.Text = "El Codigo de entrada no es valido";
                return;
            }
        }
        else
        {
            LErrorPer.Text = "El Codigo de entrada no es valido";
            return;
        }
        for (int i = 0; i < GridPer.Rows.Count; i++)
        {
            if (GridPer.Rows[i].Selected)
            {                
                CeC_Config.GuardaConfig(0, "NCIPID_" + GridPer.Rows[i].Cells[3].Value.ToString() + "_NOI", txtAsignarPer.Text);
            }
        }
        for (int i = 0; i < DSPer.EC_TIPO_INCIDENCIAS.Rows.Count; i++)
        {
            try
            {
                DSPer.EC_TIPO_INCIDENCIAS[i][2] = CeC_Config.ObtenConfig(0, "NCIPID_" + DSPer.EC_TIPO_INCIDENCIAS[i][3].ToString() + "_NOI", "");
            }
            catch (Exception ex)
            {

            }
        }
        GridPer.DataSource = DSPer.EC_TIPO_INCIDENCIAS;
        GridPer.DataBind();
    }

    protected void WC_Campos_SelectedRowChanged(object sender, Infragistics.WebUI.WebCombo.SelectedRowChangedEventArgs e)
    {
        CeC_Config.CampoLlaveNOI = WC_Campos.DataValue.ToString();
    }
}