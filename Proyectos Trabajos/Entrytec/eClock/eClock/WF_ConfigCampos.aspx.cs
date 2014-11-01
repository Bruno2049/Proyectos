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

public partial class WF_ConfigCampos : System.Web.UI.Page
{
    private System.ComponentModel.IContainer components;
    CeC_Sesion Sesion;
    //public DS_Campos ds_Campos = new DS_Campos();

    DS_Campos dS_Campos1;
    protected void Page_Load(object sender, EventArgs e)
    {   
        Sesion = CeC_Sesion.Nuevo(this);
        if (!Sesion.TienePermiso(eClock.CEC_RESTRICCIONES.S0Configuracion))
        {
            CIT_Perfiles.CrearVentana(this, Sesion.MensajeVentanaJScript(), Sesion.TituloPagina, "Aceptar", "WF_Main.aspx", "", "");
            BGuardarCambios.Visible = false;
            BDeshacerCambios.Visible = false;
        }
        if (!IsPostBack)
        {
            //Agregar Módulo Log
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Configuración de Campos", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
        }
    }





    protected void Grid_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        Grid.Columns.FromKey("CAMPO_TE_ORDEN").Hidden = true;
        Grid.Columns.FromKey("CAMPO_NOMBRE").AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No;
        Grid.Columns.FromKey("CAMPO_TE_ES_LLAVE").AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No;
        Grid.Columns.FromKey("CAMPO_TE_ES_AUTONUM").AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No;
        Grid.Columns.FromKey("CAMPO_TE_COLUMNA").Hidden = true;
        
        CeC_Grid.AplicaFormato(Grid);
    }
    protected void CargaDatosDS()
    {
      //  CeC_Campos.ReiniciaCampos();


        dS_Campos1 = new DS_Campos();
        CeC_Campos.AplicaFormatoDataset(dS_Campos1, dS_Campos1.CAMPOS_TE_GRID.TableName);
        DS_CamposTableAdapters.TA_CAMPOS_TE_GRID TA = new DS_CamposTableAdapters.TA_CAMPOS_TE_GRID();
        TA.Fill(dS_Campos1.CAMPOS_TE_GRID);
        Grid.DataSource = dS_Campos1;
        Grid.DataMember = dS_Campos1.CAMPOS_TE_GRID.TableName;
        Grid.DataKeyField = dS_Campos1.CAMPOS_TE_GRID.CAMPO_NOMBREColumn.ColumnName;

    }

    protected void Grid_DataSource(object sender, Infragistics.WebUI.UltraWebGrid.UltraGridEventArgs e)
    {
        CargaDatosDS();
//        Grid.DataBind();

    }
    protected void Grid_UpdateRow1(object sender, Infragistics.WebUI.UltraWebGrid.RowEventArgs e)
    {
//        e.Row.
    /*    bool EsValido =  CeC_Campos.EsMascaraValidaTD(Convert.ToDecimal(e.Row.Cells.FromKey("TIPO_DATO_ID").Value),Convert.ToDecimal(e.Row.Cells.FromKey("MASCARA_ID").Value));
        if(!EsValido)
        {

            foreach (DataColumn DC in dS_Campos1.CAMPOS_TE_GRID.Columns)
            {
                Infragistics.WebUI.UltraWebGrid.UltraGridCell Cell = e.Row.Cells.FromKey("TIPO_DATO_ID");
                if(Cell != null)
                {
                    Cell
                }
                Cell.Value = 
                DC.ColumnName
            }
            e.Row.ResetCells();
//            e.Cancel;
        }
        */
//        dS_Campos1.RejectChanges();
        
    }
/*    protected void Grid_UpdateGrid(object sender, Infragistics.WebUI.UltraWebGrid.UpdateEventArgs e)
    {
        
        Grid.ClientResponse.ResponseStatus = Infragistics.WebUI.UltraWebGrid.XmlHTTPResponseStatus.Fail;
        Grid.ClientResponse.Tag = "Error al guardar";
        

    }*/
    private bool ValidaMascara(Infragistics.WebUI.UltraWebGrid.UltraGridRow Fila)
    {
        bool EsValido = CeC_Campos.EsMascaraValidaTD(Convert.ToDecimal(Fila.Cells.FromKey("TIPO_DATO_ID").Value), Convert.ToDecimal(Fila.Cells.FromKey("MASCARA_ID").Value));
        return EsValido;
    }
 /*   protected void Grid_UpdateCell(object sender, Infragistics.WebUI.UltraWebGrid.CellEventArgs e)
    {
        if (e.Cell.Column.BaseColumnName == "TIPO_DATO_ID" || e.Cell.Column.BaseColumnName == "MASCARA_ID")
        {
            if(!ValidaMascara(e.Cell.Row))
            {
                DS_Campos.CAMPOS_TE_GRIDRow Fila = dS_Campos1.CAMPOS_TE_GRID.FindByCAMPO_NOMBRE(Convert.ToString(e.Cell.Row.DataKey));
                if (e.Cell.Column.BaseColumnName == "MASCARA_ID")
                    e.Cell.Value = Convert.ToInt32(Fila.MASCARA_ID);
                else
                    e.Cell.Value = Convert.ToInt32(Fila.TIPO_DATO_ID);
                e.Cell.Text = "hola";
                //Fila.CAMPO_ETIQUETA
                //if(e.Cell.Style. == Infragistics.WebUI.UltraWebGrid.GridItemStyle)
                e.Cell.Row.Cells.FromKey("CAMPO_ETIQUETA").Value = "hola";
                e.Cell.Row.Cells.FromKey("CAMPO_ETIQUETA").Reset();
                e.Cell.Reset();
                e.Cancel = true;

            }
        }


       // e.Data
    }*/
    protected void BMostrarReporte_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        LError.Text = "Texot error";
    }
    protected void BRestablecerValores_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        LCorrecto.Text = "Texot";
    }
    protected void BDeshacerCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        Grid.DataBind();
    }
    
    protected void BGuardarCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        LCorrecto.Text = "";
        LError.Text = "";
        int Errores = 0;
        int Correctos = 0;
        string SErrores = "";
        DS_CamposTableAdapters.TA_EC_CAMPOS TA_C = new DS_CamposTableAdapters.TA_EC_CAMPOS();
        DS_CamposTableAdapters.TA_EC_CAMPOS_TE TA_CTE = new DS_CamposTableAdapters.TA_EC_CAMPOS_TE();

        foreach (Infragistics.WebUI.UltraWebGrid.UltraGridRow Fila in Grid.Rows)
	    {
            try
            {
                String CAMPO_NOMBRE = Convert.ToString(Fila.DataKey);
                decimal MASCARA_ID = Convert.ToDecimal(CeC_Grid.ObtenValorCelda(Fila,"MASCARA_ID"));
                if(!ValidaMascara(Fila))
                {
                    MASCARA_ID = 0;
                }
                TA_C.ActualizaCampo(
                    Convert.ToString(CeC_Grid.ObtenValorCelda(Fila,"CAMPO_ETIQUETA")),
                    MASCARA_ID,
                    Convert.ToDecimal(CeC_Grid.ObtenValorCelda(Fila,"TIPO_DATO_ID")),
                    Convert.ToDecimal(CeC_Grid.ObtenValorCelda(Fila,"CAMPO_LONGITUD")),
                    Convert.ToDecimal(CeC_Grid.ObtenValorCelda(Fila,"CAMPO_ANCHO_GRID")),
                    Convert.ToDecimal(CeC_Grid.ObtenValorCelda(Fila,"CATALOGO_ID")),
                    CAMPO_NOMBRE
                    );

                TA_CTE.ActualizaCampo(
                    Convert.ToDecimal(CeC_Grid.ObtenValorCelda(Fila, "CAMPO_TE_COLUMNA")),
                    Convert.ToDecimal(CeC_Grid.ObtenValorCelda(Fila, "CAMPO_TE_FILTRO")),
                    Convert.ToDecimal(CeC_Grid.ObtenValorCelda(Fila, "CAMPO_TE_INVISIBLE")),
                    Convert.ToDecimal(CeC_Grid.ObtenValorCelda(Fila, "CAMPO_TE_INVISIBLEG")),
                    CAMPO_NOMBRE
                    );
                Correctos++;
            }
            catch (System.Exception ex)
            {
                Errores++;
            	SErrores += ex.Message + "\n";
            }
    	    
	    }
        if (Errores > 0)
        {
            LError.Text = "Existieron " + Errores.ToString() + " error(es)\n" + SErrores;
        }
        if (Correctos > 0)
            LCorrecto.Text = Correctos.ToString() + " Guardados";
        //Grid.Rows
        CargaDatosDS();
        Grid.DataBind();
    }
}
