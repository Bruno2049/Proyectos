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
using Infragistics.WebUI.UltraWebGrid;

public partial class WF_TiempoExtra : System.Web.UI.Page
{
    DS_TiempoExtra dS_TiempoExt;
    CeC_Sesion Sesion;
    DS_TiempoExtraTableAdapters.TiempoExtraTableAdapter TA_TiempoExt = new DS_TiempoExtraTableAdapters.TiempoExtraTableAdapter();

    private void Habilitarcontroles()
    {
        Grid.Visible = false;
        btn_Asignar.Visible = false;
        btn_Filtro.Visible = false;
        btn_Guardar.Visible = false;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);

        Sesion.TituloPagina = "Asignar Horas Extra";
        Sesion.DescripcionPagina = "Asigne las Horas Extras a pagar a cada Empleado";

        // Permisos****************************************
        if (!Sesion.TienePermiso(eClock.CEC_RESTRICCIONES.S0TiempoExtra))
        {
            if (!Sesion.TienePermisoOHijos(eClock.CEC_RESTRICCIONES.S0TiempoExtra0Consulta, true))
            {
                Habilitarcontroles();
                return;
            }
            else
            {
                btn_Asignar.Visible = false;
                btn_Filtro.Visible = false;
                btn_Guardar.Visible = false;
            }
        }
        //**************************************************

        if (!IsPostBack)
        {
            
            if (Sesion.WF_EmpleadosFil_Qry.Length > 0)
            {
                Sesion.WF_TiempoExtraQry = Sesion.WF_EmpleadosFil_Qry;
                Sesion.WF_EmpleadosFil_Qry = "";
            }
            if (Sesion.WF_TiempoExtraQry.Length <= 0)
            {
                btn_Filtro_Click(null, null);
                return;
            }
            Sesion.DescripcionPagina = "Asigne las Horas Extras a pagar a cada Empleado (" + Sesion.WF_EmpleadosFil_QryInformacion + ")";
            //ActualizaDatos();

            /*****************
             Agregar ModuloLog
             *****************/
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Envio Reportes", 0, "Reglas de Envio de Reportes", Sesion.SESION_ID);

        }
    }

    protected void Grid_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        CeC_Grid.AplicaFormato(Grid, true, false, false, true);
        if (!IsPostBack)
        {
            //Introduce la máscara de Hora en la columna 7 (Hrs. Extra a Aplicar)
            Grid.Columns[7].AllowUpdate = AllowUpdate.Yes;
            Grid.Columns[7].Type = ColumnType.Custom;
            Grid.Columns[7].HTMLEncodeContent = true;
            Grid.Columns[7].DataType = "System.DateTime";
            Grid.Columns[7].EditorControlID = WebDateTimeEdit1.ID;
            /*UltraGridColumn GridColumn = new UltraGridColumn();
            GridColumn.BaseColumnName = "Status";
            GridColumn.Header.Caption = "Status";
            GridColumn.Header.RowLayoutColumnInfo.OriginY = 0;
            GridColumn.Key = "Status";
            Grid.Bands[0].Columns.Add(GridColumn);*/
        }
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
        Sesion = CeC_Sesion.Nuevo(this);
        if (Sesion.WF_TiempoExtraQry.Length <= 0)
            return;
        dS_TiempoExt = new DS_TiempoExtra();
        

        //Carga los datos del filtro en el Grid
        TA_TiempoExt.ActualizaIn(Sesion.WF_TiempoExtraQry);
        TA_TiempoExt.FillBy(dS_TiempoExt.TiempoExtra, Sesion.WF_EmpleadosFil_FechaI, Sesion.WF_EmpleadosFil_FechaF);
        Grid.DataSource = dS_TiempoExt.TiempoExtra;
        Grid.DataMember = dS_TiempoExt.TiempoExtra.TableName;
        Grid.DataKeyField = "PERSONA_D_HE_ID";
    }

    protected void btn_Asignar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        dS_TiempoExt = new DS_TiempoExtra();
        DateTime HrExtApl;
        for (int i = 0; i < Grid.Rows.Count; i++)
        {
            if (Grid.Rows[i].Selected)
            {
                HrExtApl = (Grid.Rows[i].Cells[7].Value == null) ? Convert.ToDateTime("00:00:00") : Convert.ToDateTime(Grid.Rows[i].Cells[7].Value);
                Grid.Rows[i].Cells[7].Value = Grid.Rows[i].Cells[6].Value;
                TA_TiempoExt.AsignarHrsExtra(Convert.ToDateTime(Grid.Rows[i].Cells[6].Value), DateTime.Today, Convert.ToDecimal(Grid.Rows[i].Cells[0].Value));
                //Grid.Rows[i].Cells[8].Value = "Actualizado";
            }
        }
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        
    }

    protected void btn_Guardar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        dS_TiempoExt = new DS_TiempoExtra();
        for (int i = 0; i < Grid.Rows.Count; i++)
        {
            if(Grid.Rows[i].Cells[7].Value!=null)
                TA_TiempoExt.AsignarHrsExtra(Convert.ToDateTime(Grid.Rows[i].Cells[7].Value), DateTime.Today, Convert.ToDecimal(Grid.Rows[i].Cells[0].Value));
            else
            {
                TA_TiempoExt.AsignarHrsExtra(null, null, Convert.ToDecimal(Grid.Rows[i].Cells[0].Value));
            }
        }
        ActualizaDatos();
    }

    protected void btn_Filtro_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        Sesion.WF_EmpleadosFil(false, true, false, "Ver Horas Extras", "Filtro de Horas Extras", "WF_TiempoExtra.aspx", "Permite filtrar las un rango de horas extras", false, true, false);
    }

}


