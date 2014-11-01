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

namespace eClock
{
    /// <summary>
    /// Descripción breve de WF_Tipo_Inc_Comida_Sis.
    /// </summary>
    public partial class WF_Tipo_Inc_Comida_Sis : System.Web.UI.Page
    {
        protected System.Data.OleDb.OleDbDataAdapter DA_IncidenciasSistemaComida;
        protected System.Data.OleDb.OleDbCommand oleDbSelectCommand1;
        protected System.Data.OleDb.OleDbCommand oleDbInsertCommand1;
        protected System.Data.OleDb.OleDbCommand oleDbUpdateCommand1;
        protected System.Data.OleDb.OleDbCommand oleDbDeleteCommand1;
        protected System.Data.OleDb.OleDbConnection Conexion;
        protected DS_TiposIncidencias dS_TiposIncidencias1;
        CeC_Sesion Sesion;

        private void ControlVisible()
        {
            Grid.Visible = false;
            BRegresar.Visible = false;
        }

        private void Habilitarcontroles()
        {
            Grid.Visible = false;
            BRegresar.Visible = false;
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            // Introducir aquí el código de usuario para inicializar la página
            Grid.DisplayLayout.CellClickActionDefault = Infragistics.WebUI.UltraWebGrid.CellClickAction.RowSelect;
            Sesion = CeC_Sesion.Nuevo(this);
            CeC_Grid.AplicaFormato(Grid, false, false, false, false);
            Sesion.TituloPagina = "Tipo de Incidencias del Sistema para comidas";
            Sesion.DescripcionPagina = "Incidencias del Sistema existentes";


            {
                // Permisos****************************************
                if (!Sesion.TienePermisoOHijos(CEC_RESTRICCIONES.S0Incidencias0Sistema_Comida, true))
                {
                    Habilitarcontroles();
                    return;
                }
                //**************************************************
            }
            DA_IncidenciasSistemaComida.Fill(dS_TiposIncidencias1.EC_TIPO_INC_COMIDA_SIS);

            //Agregar ModuloLog***
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Incidencia_Tipo_Comida", 0, "Consulta de Incidencias_Comida", Sesion.SESION_ID);
            //*****				

            if (!IsPostBack)
                Grid.DataBind();
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
            this.DA_IncidenciasSistemaComida = new System.Data.OleDb.OleDbDataAdapter();
            this.oleDbDeleteCommand1 = new System.Data.OleDb.OleDbCommand();
            this.Conexion = new System.Data.OleDb.OleDbConnection();
            this.oleDbInsertCommand1 = new System.Data.OleDb.OleDbCommand();
            this.oleDbSelectCommand1 = new System.Data.OleDb.OleDbCommand();
            this.oleDbUpdateCommand1 = new System.Data.OleDb.OleDbCommand();
            this.dS_TiposIncidencias1 = new DS_TiposIncidencias();
            ((System.ComponentModel.ISupportInitialize)(this.dS_TiposIncidencias1)).BeginInit();
            this.BRegresar.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.Bregresar_Click);
            // 
            // DA_IncidenciasSistemaComida
            // 
            this.DA_IncidenciasSistemaComida.DeleteCommand = this.oleDbDeleteCommand1;
            this.DA_IncidenciasSistemaComida.InsertCommand = this.oleDbInsertCommand1;
            this.DA_IncidenciasSistemaComida.SelectCommand = this.oleDbSelectCommand1;
            this.DA_IncidenciasSistemaComida.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																												  new System.Data.Common.DataTableMapping("Table", "EC_TIPO_INC_COMIDA_SIS", new System.Data.Common.DataColumnMapping[] {
																																																											 new System.Data.Common.DataColumnMapping("TIPO_INC_C_SIS_ID", "TIPO_INC_C_SIS_ID"),
																																																											 new System.Data.Common.DataColumnMapping("TIPO_INC_C_SIS_NOMBRE", "TIPO_INC_C_SIS_NOMBRE"),
																																																											 new System.Data.Common.DataColumnMapping("TIPO_INC_C_SIS_ABR", "TIPO_INC_C_SIS_ABR")})});
            this.DA_IncidenciasSistemaComida.UpdateCommand = this.oleDbUpdateCommand1;
            // 
            // oleDbDeleteCommand1
            // 
            this.oleDbDeleteCommand1.CommandText = "DELETE FROM EC_TIPO_INC_SIS WHERE (TIPO_INC_SIS_ID = ?) AND (TIPO_INC_SIS_ABR = " +
                "? OR ? IS NULL AND TIPO_INC_SIS_ABR IS NULL) AND (TIPO_INC_SIS_NOMBRE = ?)";
            this.oleDbDeleteCommand1.Connection = this.Conexion;
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TIPO_INC_SIS_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TIPO_INC_SIS_ID", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TIPO_INC_SIS_ABR", System.Data.OleDb.OleDbType.VarChar, 2, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "TIPO_INC_SIS_ABR", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TIPO_INC_SIS_ABR1", System.Data.OleDb.OleDbType.VarChar, 2, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "TIPO_INC_SIS_ABR", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TIPO_INC_SIS_NOMBRE", System.Data.OleDb.OleDbType.VarChar, 45, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "TIPO_INC_SIS_NOMBRE", System.Data.DataRowVersion.Original, null));
            // 
            // Conexion
            // 
            this.Conexion.ConnectionString = ((string)(configurationAppSettings.GetValue("gBDatos.ConnectionString", typeof(string))));
            // 
            // oleDbInsertCommand1
            // 
            this.oleDbInsertCommand1.CommandText = "INSERT INTO EC_TIPO_INC_SIS(TIPO_INC_SIS_ID, TIPO_INC_SIS_NOMBRE, TIPO_INC_SIS_A" +
                "BR) VALUES (?, ?, ?)";
            this.oleDbInsertCommand1.Connection = this.Conexion;
            this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("TIPO_INC_SIS_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TIPO_INC_SIS_ID", System.Data.DataRowVersion.Current, null));
            this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("TIPO_INC_SIS_NOMBRE", System.Data.OleDb.OleDbType.VarChar, 45, "TIPO_INC_SIS_NOMBRE"));
            this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("TIPO_INC_SIS_ABR", System.Data.OleDb.OleDbType.VarChar, 2, "TIPO_INC_SIS_ABR"));
            // 
            // oleDbSelectCommand1
            // 
            this.oleDbSelectCommand1.CommandText = "SELECT TIPO_INC_C_SIS_ID, TIPO_INC_C_SIS_NOMBRE, TIPO_INC_C_SIS_ABR FROM EC_TIPO" +
                "_INC_COMIDA_SIS ORDER BY TIPO_INC_C_SIS_NOMBRE";
            this.oleDbSelectCommand1.Connection = this.Conexion;
            // 
            // oleDbUpdateCommand1
            // 
            this.oleDbUpdateCommand1.CommandText = "UPDATE EC_TIPO_INC_SIS SET TIPO_INC_SIS_ID = ?, TIPO_INC_SIS_NOMBRE = ?, TIPO_IN" +
                "C_SIS_ABR = ? WHERE (TIPO_INC_SIS_ID = ?) AND (TIPO_INC_SIS_ABR = ? OR ? IS NULL" +
                " AND TIPO_INC_SIS_ABR IS NULL) AND (TIPO_INC_SIS_NOMBRE = ?)";
            this.oleDbUpdateCommand1.Connection = this.Conexion;
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("TIPO_INC_SIS_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TIPO_INC_SIS_ID", System.Data.DataRowVersion.Current, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("TIPO_INC_SIS_NOMBRE", System.Data.OleDb.OleDbType.VarChar, 45, "TIPO_INC_SIS_NOMBRE"));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("TIPO_INC_SIS_ABR", System.Data.OleDb.OleDbType.VarChar, 2, "TIPO_INC_SIS_ABR"));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TIPO_INC_SIS_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TIPO_INC_SIS_ID", System.Data.DataRowVersion.Original, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TIPO_INC_SIS_ABR", System.Data.OleDb.OleDbType.VarChar, 2, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "TIPO_INC_SIS_ABR", System.Data.DataRowVersion.Original, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TIPO_INC_SIS_ABR1", System.Data.OleDb.OleDbType.VarChar, 2, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "TIPO_INC_SIS_ABR", System.Data.DataRowVersion.Original, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TIPO_INC_SIS_NOMBRE", System.Data.OleDb.OleDbType.VarChar, 45, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "TIPO_INC_SIS_NOMBRE", System.Data.DataRowVersion.Original, null));
            // 
            // dS_TiposIncidencias1
            // 
            this.dS_TiposIncidencias1.DataSetName = "DS_TiposIncidencias";
            this.dS_TiposIncidencias1.Locale = new System.Globalization.CultureInfo("es-MX");
            ((System.ComponentModel.ISupportInitialize)(this.dS_TiposIncidencias1)).EndInit();

        }
        #endregion

        private void Bregresar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            Sesion.Redirige("WF_Main.aspx");
        }

    }
}
