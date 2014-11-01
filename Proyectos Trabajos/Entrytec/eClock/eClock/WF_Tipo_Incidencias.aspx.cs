using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Infragistics.Documents.Reports.Report;
using Infragistics.WebUI.UltraWebGrid.ExcelExport;
using Report = Infragistics.Documents.Reports.Report;
using ReportText = Infragistics.Documents.Reports.Report.Text;

namespace eClock
{
    /// <summary>
    /// Descripción breve de WF_Tipo_Incidencias.
    /// </summary>
    public partial class WF_Tipo_Incidencias : System.Web.UI.Page
    {
        CeC_Sesion Sesion;

        private void ControlVisible()
        {
            Grid.Visible = false;
            BBorrarTipoIncidencia.Visible = false;
            BEditarTipoIncidencia.Visible = false;
            BAgregarTipoIncidencia.Visible = false;
            IncidenciasCheckBox1.Visible = false;
        }

        private void Habilitarcontroles()
        {
            if (!Sesion.TienePermiso(CEC_RESTRICCIONES.S0Incidencias0Personalizadas0Nuevo))
                BAgregarTipoIncidencia.Visible = false;

            if (!Sesion.TienePermiso(CEC_RESTRICCIONES.S0Incidencias0Personalizadas0Editar))
                BEditarTipoIncidencia.Visible = false;

            if (!Sesion.TienePermiso(CEC_RESTRICCIONES.S0Incidencias0Personalizadas0Borrar))
                BBorrarTipoIncidencia.Visible = false;

            /*EmpleadosCheckBox1.Visible = Caso;
                WC_Select.Visible = Caso;
                Label1.Visible = Caso;*/
        }


        protected void Page_Load(object sender, System.EventArgs e)
        {
            // Introducir aquí el código de usuario para inicializar la página


            Sesion = CeC_Sesion.Nuevo(this);
            Sesion.TituloPagina = "Tipos de Incidencias Configuradas";
            Sesion.DescripcionPagina = "Seleccione una incidencia para editarla o borrarla; o cree una nueva incidencia";


            {
                // Permisos****************************************
                if (!Sesion.TienePermisoOHijos(CEC_RESTRICCIONES.S0Incidencias0Personalizadas, true))
                {
                    ControlVisible();
                    Habilitarcontroles();
                    return;
                }
                //**************************************************
            }

//            Sesion.DA_ModQueryAddColumnaUltraGridPerzonalizada(DA_TiposIncidencias, "(TIPO_INCIDENCIA_BORRADO = 0) AND", IncidenciasCheckBox1, "TIPO_INCIDENCIA_BORRADO");

            DataSet DS = Cec_Incidencias.ObtenTiposIncidencias(Sesion.SUSCRIPCION_ID, IncidenciasCheckBox1.Checked);
            Grid.DataSource = DS.Tables[0];
            Grid.DataMember = DS.Tables[0].TableName;
            Grid.DataKeyField = "TIPO_INCIDENCIA_ID";            

            //Agregar ModuloLog***
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Tipo Incidencias", 0, "Consulta de Incidencias", Sesion.SESION_ID);
            //*****				

            if (!IsPostBack)
                Grid.DataBind();
            Grid.DisplayLayout.CellClickActionDefault = Infragistics.WebUI.UltraWebGrid.CellClickAction.RowSelect;
            CeC_Grid.AplicaFormato(Grid, false, false, false, false);

            Grid.Rows.Band.RowSelectorStyle.Width = 5;
        }

        #region Código generado por el Diseñador de Web Forms
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: llamada requerida por el Diseñador de Web Forms ASP.NET.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.Configuration.AppSettingsReader configurationAppSettings = new System.Configuration.AppSettingsReader();
            
            this.BBorrarTipoIncidencia.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.BBorrarTipoIncidencia_Click);
            this.BAgregarTipoIncidencia.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.BAgregarTipoIncidencia_Click);
            this.BEditarTipoIncidencia.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.BEditarTipoIncidencia_Click);

        }
        #endregion



        private void BEditarTipoIncidencia_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            LCorrecto.Text = "";
            LError.Text = "";

            int Numero_Resgitro = Grid.Rows.Count;

            for (int i = 0; i < Numero_Resgitro; i++)
            {
                if (Grid.Rows[i].Selected)
                {
                    try
                    {
                        int TipoInc_id = Convert.ToInt32(Grid.Rows[i].DataKey);
                        Sesion.WF_Tipo_Incidencias_TIPO_INCIDENCIA_ID = TipoInc_id;
                        Sesion.Redirige("WF_Tipo_IncidenciasE.aspx");
                        return;
                    }
                    catch (Exception ex)
                    {
                        LError.Text = "Error :" + ex.Message;
                        return;
                    }

                }
            }
            LError.Text = "Debes de seleccionar una fila";
        }

        private void BBorrarTipoIncidencia_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            LCorrecto.Text = "";
            LError.Text = "";

            int Numero_Resgitro = Grid.Rows.Count;
            int Modificaciones = 0;

            for (int i = 0; i < Numero_Resgitro; i++)
            {
                if (Grid.Rows[i].Selected)
                {
                    try
                    {
                        int TipoInc_id = Convert.ToInt32(Grid.Rows[i].DataKey);

                        Cec_Incidencias.TipoIncidenciaBorra(TipoInc_id);
                        Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.BORRADO, "Tipo Incidencias", TipoInc_id, "", Sesion.SESION_ID);
                        //*****				
                        LCorrecto.Text = Modificaciones.ToString() + " registros modificados";
                        Grid.DataBind();

                        return;
                    }
                    catch (Exception ex)
                    {
                        LError.Text = "Error :" + ex.Message;
                        return;
                    }

                }
            }
            LError.Text = "Debes de seleccionar una fila";
        }

        private void BAgregarTipoIncidencia_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            Sesion.WF_Tipo_Incidencias_TIPO_INCIDENCIA_ID = -1;
            Sesion.Redirige("WF_Tipo_IncidenciasE.aspx");
        }

        protected void IncidenciasCheckBox1_CheckedChanged(object sender, System.EventArgs e)
        {
            Grid.DataBind();
        }
        protected void btn_Actualizar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            CMd_Base.gActualizaTiposIncidencias(Sesion.SUSCRIPCION_ID);
            Grid.DataBind();
        }
        protected void GridExporter_BeginExport(object sender, Infragistics.WebUI.UltraWebGrid.DocumentExport.DocumentExportEventArgs e)
        {
            CeC_Reportes.AplicaFormatoReporte(e, "Reporte de Incidencias", "./imagenes/tiposincidencias64.png",Sesion);   
        }
        protected void btImprimir_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            GridExporter.Format = Report.FileFormat.PDF;
            GridExporter.TargetPaperOrientation = Report.PageOrientation.Landscape;
            GridExporter.DownloadName = "ExportacionIncidencias.pdf";
            GridExporter.Export(Grid);
        }
        protected void BAgregarTipoIncidencia_Click1(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {

        }
}
}
