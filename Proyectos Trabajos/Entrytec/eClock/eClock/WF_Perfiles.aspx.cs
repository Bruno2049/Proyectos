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
    /// Descripción breve de WF_Perfiles.
    /// </summary>
    public partial class WF_Perfiles : System.Web.UI.Page
    {
        protected System.Data.OleDb.OleDbDataAdapter DA_Perfiles;
        protected System.Data.OleDb.OleDbCommand oleDbSelectCommand1;
        protected System.Data.OleDb.OleDbCommand oleDbInsertCommand1;
        protected System.Data.OleDb.OleDbCommand oleDbUpdateCommand1;
        protected System.Data.OleDb.OleDbCommand oleDbDeleteCommand1;
        protected System.Data.OleDb.OleDbConnection Conexion;
        protected DS_Perfiles dS_Perfiles1;

        CeC_Sesion Sesion;

        private void Habilitarcontroles()
        {
            Grid.Visible = false;
            BBorrarTerminal.Visible = false;
            BAgregarTerminal.Visible = false;
            BEditarTerminal.Visible = false;
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            // Introducir aquí el código de usuario para inicializar la página
            Sesion = CeC_Sesion.Nuevo(this);
            Sesion.TituloPagina = "Perfiles";
            Sesion.DescripcionPagina = "Perfiles de usuarios existentes en el sistema";
            Grid.DisplayLayout.CellClickActionDefault = Infragistics.WebUI.UltraWebGrid.CellClickAction.RowSelect;

            DA_Perfiles.Fill(dS_Perfiles1.EC_PERFILES);
            if (!IsPostBack)
                Grid.DataBind();

            //Agregar ModuloLog***
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Perfiles", 0, "Consulta de Perfiles", Sesion.SESION_ID);
            //*****						

            
            
            {
                // Permisos****************************************
                if (!Sesion.TienePermisoOHijos(CEC_RESTRICCIONES.S0Perfiles, true))
                {
                    Habilitarcontroles();
                    return;
                }
                //**************************************************
            }
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
            this.DA_Perfiles = new System.Data.OleDb.OleDbDataAdapter();
            this.oleDbDeleteCommand1 = new System.Data.OleDb.OleDbCommand();
            this.Conexion = new System.Data.OleDb.OleDbConnection();
            this.oleDbInsertCommand1 = new System.Data.OleDb.OleDbCommand();
            this.oleDbSelectCommand1 = new System.Data.OleDb.OleDbCommand();
            this.oleDbUpdateCommand1 = new System.Data.OleDb.OleDbCommand();
            this.dS_Perfiles1 = new DS_Perfiles();

            ((System.ComponentModel.ISupportInitialize)(this.dS_Perfiles1)).BeginInit();
            // 
            // DA_Perfiles
            // 
            this.DA_Perfiles.DeleteCommand = this.oleDbDeleteCommand1;
            this.DA_Perfiles.InsertCommand = this.oleDbInsertCommand1;
            this.DA_Perfiles.SelectCommand = this.oleDbSelectCommand1;
            this.DA_Perfiles.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																								  new System.Data.Common.DataTableMapping("Table", "EC_PERFILES", new System.Data.Common.DataColumnMapping[] {
																																																				  new System.Data.Common.DataColumnMapping("PERFIL_NOMBRE", "PERFIL_NOMBRE"),
																																																				  new System.Data.Common.DataColumnMapping("PERFIL_ID", "PERFIL_ID"),
																																																				  new System.Data.Common.DataColumnMapping("PERFIL_BORRADO", "PERFIL_BORRADO")})});
            this.DA_Perfiles.UpdateCommand = this.oleDbUpdateCommand1;
            // 
            // oleDbDeleteCommand1
            // 
            this.oleDbDeleteCommand1.CommandText = "DELETE FROM EC_PERFILES WHERE (PERFIL_ID = ?) AND (PERFIL_BORRADO = ? OR ? IS NU" +
                "LL AND PERFIL_BORRADO IS NULL) AND (PERFIL_NOMBRE = ?)";
            this.oleDbDeleteCommand1.Connection = this.Conexion;
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PERFIL_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "PERFIL_ID", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PERFIL_BORRADO", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(1)), ((System.Byte)(0)), "PERFIL_BORRADO", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PERFIL_BORRADO1", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(1)), ((System.Byte)(0)), "PERFIL_BORRADO", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PERFIL_NOMBRE", System.Data.OleDb.OleDbType.VarChar, 45, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "PERFIL_NOMBRE", System.Data.DataRowVersion.Original, null));
            // 
            // Conexion
            // 
            this.Conexion.ConnectionString = ((string)(configurationAppSettings.GetValue("gBDatos.ConnectionString", typeof(string))));
            // 
            // oleDbInsertCommand1
            // 
            this.oleDbInsertCommand1.CommandText = "INSERT INTO EC_PERFILES(PERFIL_NOMBRE, PERFIL_ID, PERFIL_BORRADO) VALUES (?, ?, " +
                "?)";
            this.oleDbInsertCommand1.Connection = this.Conexion;
            this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERFIL_NOMBRE", System.Data.OleDb.OleDbType.VarChar, 45, "PERFIL_NOMBRE"));
            this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERFIL_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "PERFIL_ID", System.Data.DataRowVersion.Current, null));
            this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERFIL_BORRADO", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(1)), ((System.Byte)(0)), "PERFIL_BORRADO", System.Data.DataRowVersion.Current, null));
            // 
            // oleDbSelectCommand1
            // 
            this.oleDbSelectCommand1.CommandText = "SELECT PERFIL_NOMBRE, PERFIL_ID, PERFIL_BORRADO FROM EC_PERFILES WHERE (PERFIL_B" +
                "ORRADO = 0) ORDER BY PERFIL_NOMBRE";
            this.oleDbSelectCommand1.Connection = this.Conexion;
            // 
            // oleDbUpdateCommand1
            // 
            this.oleDbUpdateCommand1.CommandText = "UPDATE EC_PERFILES SET PERFIL_NOMBRE = ?, PERFIL_ID = ?, PERFIL_BORRADO = ? WHER" +
                "E (PERFIL_ID = ?) AND (PERFIL_BORRADO = ? OR ? IS NULL AND PERFIL_BORRADO IS NUL" +
                "L) AND (PERFIL_NOMBRE = ?)";
            this.oleDbUpdateCommand1.Connection = this.Conexion;
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERFIL_NOMBRE", System.Data.OleDb.OleDbType.VarChar, 45, "PERFIL_NOMBRE"));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERFIL_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "PERFIL_ID", System.Data.DataRowVersion.Current, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERFIL_BORRADO", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(1)), ((System.Byte)(0)), "PERFIL_BORRADO", System.Data.DataRowVersion.Current, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PERFIL_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "PERFIL_ID", System.Data.DataRowVersion.Original, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PERFIL_BORRADO", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(1)), ((System.Byte)(0)), "PERFIL_BORRADO", System.Data.DataRowVersion.Original, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PERFIL_BORRADO1", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(1)), ((System.Byte)(0)), "PERFIL_BORRADO", System.Data.DataRowVersion.Original, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PERFIL_NOMBRE", System.Data.OleDb.OleDbType.VarChar, 45, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "PERFIL_NOMBRE", System.Data.DataRowVersion.Original, null));
            // 
            // dS_Perfiles1
            // 
            this.dS_Perfiles1.DataSetName = "DS_Perfiles";
            this.dS_Perfiles1.Locale = new System.Globalization.CultureInfo("es-MX");
            ((System.ComponentModel.ISupportInitialize)(this.dS_Perfiles1)).EndInit();

        }
        #endregion

        protected void Grid_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
        {
            CeC_Grid.AplicaFormato(Grid, false, false, false, false);
        }

        protected void BBorrarPerfil_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            int Numero_resgistros = Grid.Rows.Count;
            for (int i = 0; i < Numero_resgistros; i++)
            {
                if (Grid.Rows[i].Selected)
                {
                    int Perfil_ID = Convert.ToInt32(Grid.Rows[i].Cells[1].Value);
                    if (Perfil_ID > 3)
                    {
                        CeC_BD.EjecutaComando("UPDATE EC_PERFILES SET PERFIL_BORRADO = 1 WHERE PERFIL_ID = " + Perfil_ID);
                        LCorrecto.Text = "El perfil " + CeC_BD.EjecutaEscalarString("SELECT PERFIL_NOMBRE FROM EC_PERFILES WHERE PERFIL_ID = " + Perfil_ID) + " ha sido desactivado exitosamente";
                    }
                    else
                    {
                        LError.Text = "Este perfil esta restringido.";
                    }
                }
            }
        }

        protected void BAgregarPerfil_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            Sesion.WF_PerfilesE_Perfil_ID = -1;
            Sesion.Redirige("WF_PerfilesPermisos.aspx");
        }

        protected void BEditarPerfil_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            int Numero_resgistros = Grid.Rows.Count;
            for (int i = 0; i < Numero_resgistros; i++)
            {
                if (Grid.Rows[i].Selected)
                {
                    if (Convert.ToInt32(Grid.Rows[i].Cells[1].Value) != 1)
                    {
                        Sesion.WF_PerfilesE_Perfil_ID = Convert.ToInt32(Grid.Rows[i].Cells[1].Value);
                        Sesion.Redirige("WF_PerfilesPermisos.aspx");
                        break;
                    }
                    else
                    {
                        LError.Text = "Perfil Restringido";
                        break;
                    }
                }
            }
        }
    }
}