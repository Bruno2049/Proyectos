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

namespace eClock
{
    /// <summary>
    /// Descripción breve de WF_Dias_Festivos.
    /// </summary>
    public partial class WF_Dias_Festivos : System.Web.UI.Page
    {
        protected System.Data.OleDb.OleDbDataAdapter DA_DiasFestivos;
        protected System.Data.OleDb.OleDbCommand oleDbSelectCommand1;
        protected System.Data.OleDb.OleDbCommand oleDbInsertCommand1;
        protected System.Data.OleDb.OleDbCommand oleDbUpdateCommand1;
        protected System.Data.OleDb.OleDbCommand oleDbDeleteCommand1;
        protected System.Data.OleDb.OleDbConnection Conexion;
        protected DS_DiasFestivos dS_DiasFestivos1;
        protected System.Data.OleDb.OleDbCommand Borrar_Dias_Festivos;

        CeC_Sesion Sesion;

        private void ControlVisible()
        {
            Grid.Visible = false;
            DiasCheckBox1.Visible = false;
            BAgregarDiaFestivo.Visible = false;
            BBorrarDiaFestivo.Visible = false;
            BEditarDiaFestivo.Visible = false;
        }

        private void Habilitarcontroles()
        {
            if (!Sesion.TienePermiso(CEC_RESTRICCIONES.S0Dias_Festivos0Nuevo))
                BAgregarDiaFestivo.Visible = false;

            if (!Sesion.TienePermiso(CEC_RESTRICCIONES.S0Dias_Festivos0Editar))
                BEditarDiaFestivo.Visible = false;

            if (!Sesion.TienePermiso(CEC_RESTRICCIONES.S0Dias_Festivos0Borrar))
                BBorrarDiaFestivo.Visible = false;
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            // Introducir aquí el código de usuario para inicializar la página
            Sesion = CeC_Sesion.Nuevo(this);
            //Titulo y Descripcion del Form
            Sesion.TituloPagina = "Días Festivos";
            Sesion.DescripcionPagina = "Seleccione el día festivo que requiera 'Editar' o 'Borrar'; o use 'Nuevo' si desea agregar otro día festivo";
            //Titulo y Descripcion del Form

           
            {
                // Permisos****************************************
                if (!Sesion.TienePermisoOHijos(CEC_RESTRICCIONES.S0Dias_Festivos, true))
                {
                    ControlVisible();
                    Habilitarcontroles();
                    return;
                }
                //**************************************************
            }



//            Grid.DisplayLayout.CellClickActionDefault = Infragistics.WebUI.UltraWebGrid.CellClickAction.RowSelect;

            //Agregar ModuloLog***
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Dias Festivos", 0, "Consulta de Dias Festivos", Sesion.SESION_ID);
            //*****
          /*  if (!IsPostBack)
                Grid.DataBind();*/
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
            this.dS_DiasFestivos1 = new DS_DiasFestivos();
            this.DA_DiasFestivos = new System.Data.OleDb.OleDbDataAdapter();
            this.oleDbDeleteCommand1 = new System.Data.OleDb.OleDbCommand();
            this.Conexion = new System.Data.OleDb.OleDbConnection();
            this.oleDbInsertCommand1 = new System.Data.OleDb.OleDbCommand();
            this.oleDbSelectCommand1 = new System.Data.OleDb.OleDbCommand();
            this.oleDbUpdateCommand1 = new System.Data.OleDb.OleDbCommand();
            this.Borrar_Dias_Festivos = new System.Data.OleDb.OleDbCommand();

            ((System.ComponentModel.ISupportInitialize)(this.dS_DiasFestivos1)).BeginInit();
            this.BBorrarDiaFestivo.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.BBorrarDiaFestivo_Click);
            this.BAgregarDiaFestivo.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.BAgregarDiaFestivo_Click);
            this.BEditarDiaFestivo.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.BEditarDiaFestivo_Click);
            // 
            // dS_DiasFestivos1
            // 
            this.dS_DiasFestivos1.DataSetName = "DS_DiasFestivos";
            this.dS_DiasFestivos1.Locale = new System.Globalization.CultureInfo("es-MX");
            // 
            // DA_DiasFestivos
            // 
            this.DA_DiasFestivos.DeleteCommand = this.oleDbDeleteCommand1;
            this.DA_DiasFestivos.InsertCommand = this.oleDbInsertCommand1;
            this.DA_DiasFestivos.SelectCommand = this.oleDbSelectCommand1;
            this.DA_DiasFestivos.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																									  new System.Data.Common.DataTableMapping("Table", "EC_DIAS_FESTIVOS", new System.Data.Common.DataColumnMapping[] {
																																																						   new System.Data.Common.DataColumnMapping("DIA_FESTIVO_ID", "DIA_FESTIVO_ID"),
																																																						   new System.Data.Common.DataColumnMapping("DIA_FESTIVO_FECHA", "DIA_FESTIVO_FECHA"),
																																																						   new System.Data.Common.DataColumnMapping("DIA_FESTIVO_NOMBRE", "DIA_FESTIVO_NOMBRE"),
																																																						   new System.Data.Common.DataColumnMapping("DIA_FESTIVO_BORRADO", "DIA_FESTIVO_BORRADO")})});
            this.DA_DiasFestivos.UpdateCommand = this.oleDbUpdateCommand1;
            // 
            // oleDbDeleteCommand1
            // 
            this.oleDbDeleteCommand1.CommandText = "DELETE FROM EC_DIAS_FESTIVOS WHERE (DIA_FESTIVO_ID = ?) AND (DIA_FESTIVO_BORRADO" +
                " = ? OR ? IS NULL AND DIA_FESTIVO_BORRADO IS NULL) AND (DIA_FESTIVO_FECHA = ?) A" +
                "ND (DIA_FESTIVO_NOMBRE = ?)";
            this.oleDbDeleteCommand1.Connection = this.Conexion;
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_DIA_FESTIVO_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "DIA_FESTIVO_ID", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_DIA_FESTIVO_BORRADO", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(1)), ((System.Byte)(0)), "DIA_FESTIVO_BORRADO", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_DIA_FESTIVO_BORRADO1", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(1)), ((System.Byte)(0)), "DIA_FESTIVO_BORRADO", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_DIA_FESTIVO_FECHA", System.Data.OleDb.OleDbType.DBTimeStamp, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "DIA_FESTIVO_FECHA", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_DIA_FESTIVO_NOMBRE", System.Data.OleDb.OleDbType.VarChar, 45, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "DIA_FESTIVO_NOMBRE", System.Data.DataRowVersion.Original, null));
            // 
            // Conexion
            // 
            this.Conexion.ConnectionString = ((string)(configurationAppSettings.GetValue("gBDatos.ConnectionString", typeof(string))));
            // 
            // oleDbInsertCommand1
            // 
            this.oleDbInsertCommand1.CommandText = "INSERT INTO EC_DIAS_FESTIVOS(DIA_FESTIVO_ID, DIA_FESTIVO_FECHA, DIA_FESTIVO_NOMB" +
                "RE, DIA_FESTIVO_BORRADO) VALUES (?, ?, ?, ?)";
            this.oleDbInsertCommand1.Connection = this.Conexion;
            this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("DIA_FESTIVO_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "DIA_FESTIVO_ID", System.Data.DataRowVersion.Current, null));
            this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("DIA_FESTIVO_FECHA", System.Data.OleDb.OleDbType.DBTimeStamp, 0, "DIA_FESTIVO_FECHA"));
            this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("DIA_FESTIVO_NOMBRE", System.Data.OleDb.OleDbType.VarChar, 45, "DIA_FESTIVO_NOMBRE"));
            this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("DIA_FESTIVO_BORRADO", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(1)), ((System.Byte)(0)), "DIA_FESTIVO_BORRADO", System.Data.DataRowVersion.Current, null));
            // 
            // oleDbSelectCommand1
            // 
            this.oleDbSelectCommand1.CommandText = "SELECT DIA_FESTIVO_ID, DIA_FESTIVO_FECHA, DIA_FESTIVO_NOMBRE, DIA_FESTIVO_BORRADO" +
                ", 123456789 AS STATUS FROM EC_DIAS_FESTIVOS WHERE (DIA_FESTIVO_BORRADO = 0) AND" +
                " (DIA_FESTIVO_ID > 0) ORDER BY DIA_FESTIVO_FECHA";
            this.oleDbSelectCommand1.Connection = this.Conexion;
            // 
            // oleDbUpdateCommand1
            // 
            this.oleDbUpdateCommand1.CommandText = @"UPDATE EC_DIAS_FESTIVOS SET DIA_FESTIVO_ID = ?, DIA_FESTIVO_FECHA = ?, DIA_FESTIVO_NOMBRE = ?, DIA_FESTIVO_BORRADO = ? WHERE (DIA_FESTIVO_ID = ?) AND (DIA_FESTIVO_BORRADO = ? OR ? IS NULL AND DIA_FESTIVO_BORRADO IS NULL) AND (DIA_FESTIVO_FECHA = ?) AND (DIA_FESTIVO_NOMBRE = ?)";
            this.oleDbUpdateCommand1.Connection = this.Conexion;
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("DIA_FESTIVO_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "DIA_FESTIVO_ID", System.Data.DataRowVersion.Current, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("DIA_FESTIVO_FECHA", System.Data.OleDb.OleDbType.DBDate, 0, "DIA_FESTIVO_FECHA"));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("DIA_FESTIVO_NOMBRE", System.Data.OleDb.OleDbType.VarChar, 45, "DIA_FESTIVO_NOMBRE"));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("DIA_FESTIVO_BORRADO", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(1)), ((System.Byte)(0)), "DIA_FESTIVO_BORRADO", System.Data.DataRowVersion.Current, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_DIA_FESTIVO_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "DIA_FESTIVO_ID", System.Data.DataRowVersion.Original, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_DIA_FESTIVO_BORRADO", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(1)), ((System.Byte)(0)), "DIA_FESTIVO_BORRADO", System.Data.DataRowVersion.Original, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_DIA_FESTIVO_BORRADO1", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(1)), ((System.Byte)(0)), "DIA_FESTIVO_BORRADO", System.Data.DataRowVersion.Original, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_DIA_FESTIVO_FECHA", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "DIA_FESTIVO_FECHA", System.Data.DataRowVersion.Original, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_DIA_FESTIVO_NOMBRE", System.Data.OleDb.OleDbType.VarChar, 45, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "DIA_FESTIVO_NOMBRE", System.Data.DataRowVersion.Original, null));
            // 
            // Borrar_Dias_Festivos
            // 
            this.Borrar_Dias_Festivos.CommandText = "UPDATE EC_DIAS_FESTIVOS SET DIA_FESTIVO_BORRADO = 1 WHERE (DIA_FESTIVO_ID = ?)";
            this.Borrar_Dias_Festivos.Connection = this.Conexion;
            this.Borrar_Dias_Festivos.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_DIA_FESTIVO_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "DIA_FESTIVO_ID", System.Data.DataRowVersion.Original, null));
            ((System.ComponentModel.ISupportInitialize)(this.dS_DiasFestivos1)).EndInit();

        }
        #endregion

        protected void BAgregarDiaFestivo_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            Sesion.WF_Dias_Festivos_DIAFESTIVO_ID = -1;
            Sesion.Redirige("WF_Dias_FestivosE.aspx");
        }

        protected void BEditarDiaFestivo_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            LError.Text = "";
            LCorrecto.Text = "";
            int Numero_Resgistos = Grid.Rows.Count;

            for (int i = 0; i < Numero_Resgistos; i++)
            {
                if (Grid.Rows[i].Selected)
                {
                    int Id_DiaFestivo = Convert.ToInt32(Grid.Rows[i].DataKey);
                    Sesion.WF_Dias_Festivos_DIAFESTIVO_ID = Id_DiaFestivo;
                    Sesion.Redirige("WF_Dias_FestivosE.aspx");
                    return;
                }
            }
            LError.Text = "Debes de Seleccionar una fila";
        }

        protected void BBorrarDiaFestivo_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            LError.Text = "";
            LCorrecto.Text = "";

            int Numero_Resgistos = Grid.Rows.Count;
            for (int i = 0; i < Numero_Resgistos; i++)
            {
                if (Grid.Rows[i].Selected)
                {
                    try
                    {
                        if (Conexion.State != System.Data.ConnectionState.Open)
                            Conexion.Open();
                        int Id_DiaFestivo = Convert.ToInt32(Grid.Rows[i].DataKey);
                        string strDiaFestivo = Convert.ToString(Grid.Rows[i].Cells[2].Value);
                        Borrar_Dias_Festivos.Parameters[0].Value = Id_DiaFestivo;
                        int Modificaciones = Borrar_Dias_Festivos.ExecuteNonQuery();
                        LCorrecto.Text = Modificaciones.ToString() + " registros modificados";

                        //Agregar ModuloLog***
                        Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Dias Festivos", Id_DiaFestivo, strDiaFestivo, Sesion.SESION_ID);
                        //*****

                        dS_DiasFestivos1.EC_DIAS_FESTIVOS.Clear();
                        DA_DiasFestivos.Fill(dS_DiasFestivos1.EC_DIAS_FESTIVOS);
                        Grid.DataBind();
                        if (Conexion.State == ConnectionState.Open)
                            Conexion.Dispose();
                        return;
                    }
                    catch (Exception ex)
                    {
                        if (Conexion.State == ConnectionState.Open)
                            Conexion.Dispose();
                        LError.Text = "Error :" + ex.Message;
                        return;
                    }
                }
            }
            LError.Text = "Debes de Seleccionar una fila";
        }

        protected void CheckBox1_CheckedChanged(object sender, System.EventArgs e)
        {
            Grid.DataBind();
        }

        protected void Grid_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
        {
            CeC_Grid.AplicaFormato(Grid, false, false, false, false);
        }
        protected void GridExporter_BeginExport(object sender, Infragistics.WebUI.UltraWebGrid.DocumentExport.DocumentExportEventArgs e)
        {
            CeC_Reportes.AplicaFormatoReporte(e, "Días Festivos", "", Sesion);
        }
        protected void btImprimir_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            GridExporter.Format = Infragistics.Documents.Reports.Report.FileFormat.PDF;
            GridExporter.TargetPaperOrientation = Infragistics.Documents.Reports.Report.PageOrientation.Portrait;
            GridExporter.DownloadName = "ExportacionDiasFestivos.pdf";
            GridExporter.Export(Grid);
        }
        protected void Grid_InitializeDataSource(object sender, Infragistics.WebUI.UltraWebGrid.UltraGridEventArgs e)
        {
            Sesion = CeC_Sesion.Nuevo(this);
            DataSet DS = CeC_Asistencias.ObtenDiasFestivos(Sesion.USUARIO_ID, DiasCheckBox1.Checked);
            Grid.DataSource = DS.Tables[0];
            Grid.DataMember = DS.Tables[0].TableName;
            Grid.DataKeyField = "DIA_FESTIVO_ID";
        }
}
}