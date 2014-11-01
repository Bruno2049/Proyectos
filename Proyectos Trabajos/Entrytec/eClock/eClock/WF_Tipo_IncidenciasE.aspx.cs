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
    /// Descripción breve de WF_Tipo_IncidenciasE.
    /// </summary>
    public partial class WF_Tipo_IncidenciasE : System.Web.UI.Page
    {
        protected System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter1;
        protected System.Data.OleDb.OleDbCommand oleDbSelectCommand1;
        protected System.Data.OleDb.OleDbCommand oleDbInsertCommand1;
        protected System.Data.OleDb.OleDbCommand oleDbUpdateCommand1;
        protected System.Data.OleDb.OleDbCommand oleDbDeleteCommand1;
        protected System.Data.OleDb.OleDbConnection Conexion;
        protected System.Data.OleDb.OleDbDataAdapter DA_TiposIncidencias_Edicion;
        protected DS_TiposIncidencias dS_TiposIncidencias1;
        CeC_Sesion Sesion;
        DS_TiposIncidencias.EC_TIPO_INCIDENCIAS_EdicionRow Fila;


        private void Habilitarcontroles(bool Caso, string Restriccion)
        {
            if (!Sesion.TienePermiso(CEC_RESTRICCIONES.S0Incidencias0Personalizadas0Nuevo) && !Sesion.TienePermiso(CEC_RESTRICCIONES.S0Incidencias0Personalizadas0Borrar) && !Sesion.TienePermiso(CEC_RESTRICCIONES.S0Incidencias0Personalizadas0Borrar))
            {
                TipoIncidenciaId.Enabled = Caso;
                TipoIncidenciaNombre.Enabled = Caso;
                CBBorrar.Visible = Caso;

                RVTipoIncidenciaId.Visible = Caso;
                RVTipoIncidenciaNombre.Visible = Caso;
                RequiredFieldValidator1.Visible = Caso;
                LBorrar.Visible = Caso;
                BGuardarCambios.Visible = Caso;
                BDeshacerCambios.Visible = Caso;
            }

            if (!Sesion.TienePermiso(CEC_RESTRICCIONES.S0Incidencias0Personalizadas0Borrar))
            {
                LBorrar.Visible = Caso;
                CBBorrar.Visible = Caso;
            }

        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            // Introducir aquí el código de usuario para inicializar la página
            Sesion = CeC_Sesion.Nuevo(this);
            Sesion.TituloPagina = "Edición de Tipo de Incidencias";
            Sesion.DescripcionPagina = "Ingrese los datos de la Incidencia";



            // Permisos****************************************

            string[] Permiso = new string[10];

            /*Permiso[0] = "S";
            Permiso[1] = "S.Incidencias";
            Permiso[3] = "S.Incidencias.Personalizadas";
            Permiso[4] = "S.Incidencias.Personalizadas.Nuevo";
            Permiso[5] = "S.Incidencias.Personalizadas.Editar";
            Permiso[6] = "S.Incidencias.Personalizadas.Borrar";*/
            Permiso[0] = CEC_RESTRICCIONES.S0Incidencias0Personalizadas0Borrar;
            Permiso[1] = CEC_RESTRICCIONES.S0Incidencias0Personalizadas0Editar;
            Permiso[2] = CEC_RESTRICCIONES.S0Incidencias0Personalizadas0Nuevo;


            if (!Sesion.Acceso(Permiso, CIT_Perfiles.Acceso(Sesion.PERFIL_ID, this)))
            {
                CIT_Perfiles.CrearVentana(this, Sesion.MensajeVentanaJScript(), Sesion.TituloPagina, "Aceptar", "FR_Main.htm", "", "");
                Habilitarcontroles(false, Sesion.Restriccion.ToString());
                return;
            }

            Habilitarcontroles(false, Sesion.Restriccion.ToString());
            //**************************************************


            DA_TiposIncidencias_Edicion.SelectCommand.Parameters[0].Value = Sesion.WF_Tipo_Incidencias_TIPO_INCIDENCIA_ID;
            DA_TiposIncidencias_Edicion.Fill(dS_TiposIncidencias1);


            if (Sesion.WF_Tipo_Incidencias_TIPO_INCIDENCIA_ID > 0)
            {
                if (dS_TiposIncidencias1.EC_TIPO_INCIDENCIAS_Edicion.Rows.Count > 0)
                {
                    Fila = (DS_TiposIncidencias.EC_TIPO_INCIDENCIAS_EdicionRow)dS_TiposIncidencias1.EC_TIPO_INCIDENCIAS_Edicion.Rows[0];

                    if (!IsPostBack)
                        AtarControles(true);
                }
                else
                {
                    if (!IsPostBack)
                        AtarControles(true);

                    Fila = dS_TiposIncidencias1.EC_TIPO_INCIDENCIAS_Edicion.NewEC_TIPO_INCIDENCIAS_EdicionRow();
                    LBorrar.Visible = false;
                    CBBorrar.Visible = false;
                }
            }
            else
            {
                Fila = dS_TiposIncidencias1.EC_TIPO_INCIDENCIAS_Edicion.NewEC_TIPO_INCIDENCIAS_EdicionRow();

                if (!IsPostBack)
                    AtarControles(true);

                LBorrar.Visible = false;
                CBBorrar.Visible = false;
            }
            //Agregar Módulo Log
            if(!IsPostBack)    
                Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Edición de Tipo de Incidencias", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
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
            this.DA_TiposIncidencias_Edicion = new System.Data.OleDb.OleDbDataAdapter();
            this.oleDbDeleteCommand1 = new System.Data.OleDb.OleDbCommand();
            this.Conexion = new System.Data.OleDb.OleDbConnection();
            this.oleDbInsertCommand1 = new System.Data.OleDb.OleDbCommand();
            this.oleDbSelectCommand1 = new System.Data.OleDb.OleDbCommand();
            this.oleDbUpdateCommand1 = new System.Data.OleDb.OleDbCommand();
            this.dS_TiposIncidencias1 = new DS_TiposIncidencias();
            ((System.ComponentModel.ISupportInitialize)(this.dS_TiposIncidencias1)).BeginInit();
            this.BDeshacerCambios.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.BDeshacerCambios_Click);
            this.BGuardarCambios.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.BGuardarCambios_Click);
            // 
            // DA_TiposIncidencias_Edicion
            // 
            this.DA_TiposIncidencias_Edicion.DeleteCommand = this.oleDbDeleteCommand1;
            this.DA_TiposIncidencias_Edicion.InsertCommand = this.oleDbInsertCommand1;
            this.DA_TiposIncidencias_Edicion.SelectCommand = this.oleDbSelectCommand1;
            this.DA_TiposIncidencias_Edicion.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																												  new System.Data.Common.DataTableMapping("Table", "EC_TIPO_INCIDENCIAS_Edicion", new System.Data.Common.DataColumnMapping[] {
																																																												  new System.Data.Common.DataColumnMapping("TIPO_INCIDENCIA_ID", "TIPO_INCIDENCIA_ID"),
																																																												  new System.Data.Common.DataColumnMapping("TIPO_INCIDENCIA_NOMBRE", "TIPO_INCIDENCIA_NOMBRE"),
																																																												  new System.Data.Common.DataColumnMapping("TIPO_INCIDENCIA_ABR", "TIPO_INCIDENCIA_ABR"),
																																																												  new System.Data.Common.DataColumnMapping("TIPO_INCIDENCIA_BORRADO", "TIPO_INCIDENCIA_BORRADO")})});
            this.DA_TiposIncidencias_Edicion.UpdateCommand = this.oleDbUpdateCommand1;
            // 
            // oleDbDeleteCommand1
            // 
            this.oleDbDeleteCommand1.CommandText = "DELETE FROM EC_TIPO_INCIDENCIAS WHERE (TIPO_INCIDENCIA_ID = ?) AND (TIPO_INCIDEN" +
                "CIA_ABR = ? OR ? IS NULL AND TIPO_INCIDENCIA_ABR IS NULL) AND (TIPO_INCIDENCIA_B" +
                "ORRADO = ? OR ? IS NULL AND TIPO_INCIDENCIA_BORRADO IS NULL) AND (TIPO_INCIDENCI" +
                "A_NOMBRE = ?)";
            this.oleDbDeleteCommand1.Connection = this.Conexion;
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TIPO_INCIDENCIA_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TIPO_INCIDENCIA_ID", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TIPO_INCIDENCIA_ABR", System.Data.OleDb.OleDbType.VarChar, 2, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "TIPO_INCIDENCIA_ABR", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TIPO_INCIDENCIA_ABR1", System.Data.OleDb.OleDbType.VarChar, 2, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "TIPO_INCIDENCIA_ABR", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TIPO_INCIDENCIA_BORRADO", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(1)), ((System.Byte)(0)), "TIPO_INCIDENCIA_BORRADO", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TIPO_INCIDENCIA_BORRADO1", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(1)), ((System.Byte)(0)), "TIPO_INCIDENCIA_BORRADO", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TIPO_INCIDENCIA_NOMBRE", System.Data.OleDb.OleDbType.VarChar, 45, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "TIPO_INCIDENCIA_NOMBRE", System.Data.DataRowVersion.Original, null));
            // 
            // Conexion
            // 
            this.Conexion.ConnectionString = ((string)(configurationAppSettings.GetValue("gBDatos.ConnectionString", typeof(string))));
            // 
            // oleDbInsertCommand1
            // 
            this.oleDbInsertCommand1.CommandText = "INSERT INTO EC_TIPO_INCIDENCIAS(TIPO_INCIDENCIA_ID, TIPO_INCIDENCIA_NOMBRE, TIPO" +
                "_INCIDENCIA_ABR, TIPO_INCIDENCIA_BORRADO) VALUES (?, ?, ?, ?)";
            this.oleDbInsertCommand1.Connection = this.Conexion;
            this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("TIPO_INCIDENCIA_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TIPO_INCIDENCIA_ID", System.Data.DataRowVersion.Current, null));
            this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("TIPO_INCIDENCIA_NOMBRE", System.Data.OleDb.OleDbType.VarChar, 45, "TIPO_INCIDENCIA_NOMBRE"));
            this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("TIPO_INCIDENCIA_ABR", System.Data.OleDb.OleDbType.VarChar, 2, "TIPO_INCIDENCIA_ABR"));
            this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("TIPO_INCIDENCIA_BORRADO", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(1)), ((System.Byte)(0)), "TIPO_INCIDENCIA_BORRADO", System.Data.DataRowVersion.Current, null));
            // 
            // oleDbSelectCommand1
            // 
            this.oleDbSelectCommand1.CommandText = "SELECT TIPO_INCIDENCIA_ID, TIPO_INCIDENCIA_NOMBRE, TIPO_INCIDENCIA_ABR, TIPO_INCI" +
                "DENCIA_BORRADO FROM EC_TIPO_INCIDENCIAS WHERE (TIPO_INCIDENCIA_ID = ?)";
            this.oleDbSelectCommand1.Connection = this.Conexion;
            this.oleDbSelectCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("TIPO_INCIDENCIA_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TIPO_INCIDENCIA_ID", System.Data.DataRowVersion.Current, null));
            // 
            // oleDbUpdateCommand1
            // 
            this.oleDbUpdateCommand1.CommandText = @"UPDATE EC_TIPO_INCIDENCIAS SET TIPO_INCIDENCIA_ID = ?, TIPO_INCIDENCIA_NOMBRE = ?, TIPO_INCIDENCIA_ABR = ?, TIPO_INCIDENCIA_BORRADO = ? WHERE (TIPO_INCIDENCIA_ID = ?) AND (TIPO_INCIDENCIA_ABR = ? OR ? IS NULL AND TIPO_INCIDENCIA_ABR IS NULL) AND (TIPO_INCIDENCIA_BORRADO = ? OR ? IS NULL AND TIPO_INCIDENCIA_BORRADO IS NULL) AND (TIPO_INCIDENCIA_NOMBRE = ?)";
            this.oleDbUpdateCommand1.Connection = this.Conexion;
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("TIPO_INCIDENCIA_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TIPO_INCIDENCIA_ID", System.Data.DataRowVersion.Current, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("TIPO_INCIDENCIA_NOMBRE", System.Data.OleDb.OleDbType.VarChar, 45, "TIPO_INCIDENCIA_NOMBRE"));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("TIPO_INCIDENCIA_ABR", System.Data.OleDb.OleDbType.VarChar, 2, "TIPO_INCIDENCIA_ABR"));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("TIPO_INCIDENCIA_BORRADO", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(1)), ((System.Byte)(0)), "TIPO_INCIDENCIA_BORRADO", System.Data.DataRowVersion.Current, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TIPO_INCIDENCIA_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TIPO_INCIDENCIA_ID", System.Data.DataRowVersion.Original, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TIPO_INCIDENCIA_ABR", System.Data.OleDb.OleDbType.VarChar, 2, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "TIPO_INCIDENCIA_ABR", System.Data.DataRowVersion.Original, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TIPO_INCIDENCIA_ABR1", System.Data.OleDb.OleDbType.VarChar, 2, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "TIPO_INCIDENCIA_ABR", System.Data.DataRowVersion.Original, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TIPO_INCIDENCIA_BORRADO", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(1)), ((System.Byte)(0)), "TIPO_INCIDENCIA_BORRADO", System.Data.DataRowVersion.Original, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TIPO_INCIDENCIA_BORRADO1", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(1)), ((System.Byte)(0)), "TIPO_INCIDENCIA_BORRADO", System.Data.DataRowVersion.Original, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TIPO_INCIDENCIA_NOMBRE", System.Data.OleDb.OleDbType.VarChar, 45, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "TIPO_INCIDENCIA_NOMBRE", System.Data.DataRowVersion.Original, null));
            // 
            // dS_TiposIncidencias1
            // 
            this.dS_TiposIncidencias1.DataSetName = "DS_TiposIncidencias";
            this.dS_TiposIncidencias1.Locale = new System.Globalization.CultureInfo("es-MX");
            ((System.ComponentModel.ISupportInitialize)(this.dS_TiposIncidencias1)).EndInit();

        }
        #endregion
        private void AtarControles(bool Caso)
        {

            if (Caso == true)
            {

                if (Sesion.WF_Tipo_Incidencias_TIPO_INCIDENCIA_ID < 0)
                {
                    TipoIncidenciaId.Value = 0;//Convert.ToInt32(CeC_Autonumerico.GeneraAutonumerico("EC_TIPO_INCIDENCIAS", "TIPO_INCIDENCIA_ID"));
                }
                else
                {
                    try
                    {
                        TipoIncidenciaId.Text = Convert.ToString(Fila.TIPO_INCIDENCIA_ID);
                    }
                    catch
                    {
                        TipoIncidenciaId.Text = "";
                    }
                }

                try
                {
                    TipoIncidenciaNombre.Text = Fila.TIPO_INCIDENCIA_NOMBRE;
                }
                catch
                {
                    TipoIncidenciaNombre.Text = "";
                }

                try
                {
                    IncidenciaAbreviatura.Text = Fila.TIPO_INCIDENCIA_ABR;
                }
                catch
                {
                    IncidenciaAbreviatura.Text = "";
                }

                try
                {
                    CBBorrar.Checked = Convert.ToBoolean(Fila.TIPO_INCIDENCIA_BORRADO);
                }
                catch
                {
                    CBBorrar.Checked = false;
                }

            }
            else
            {
                Fila.TIPO_INCIDENCIA_ID = Convert.ToInt32(TipoIncidenciaId.Value);
                Fila.TIPO_INCIDENCIA_NOMBRE = TipoIncidenciaNombre.Text;
                Fila.TIPO_INCIDENCIA_ABR = IncidenciaAbreviatura.Text;
                Fila.TIPO_INCIDENCIA_BORRADO = Convert.ToInt32(CBBorrar.Checked);
            }
        }

        private void BDeshacerCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            AtarControles(true);
        }

        private void BGuardarCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            LCorrecto.Text = "";
            LError.Text = "";

            try
            {
                AtarControles(false);


                if (Sesion.WF_Tipo_Incidencias_TIPO_INCIDENCIA_ID < 0)
                {
                    Fila.TIPO_INCIDENCIA_ID = Convert.ToInt32(CeC_Autonumerico.GeneraAutonumerico("EC_TIPO_INCIDENCIAS", "TIPO_INCIDENCIA_ID", Sesion));
                    dS_TiposIncidencias1.EC_TIPO_INCIDENCIAS_Edicion.AddEC_TIPO_INCIDENCIAS_EdicionRow(Fila);

                    int TPIncId = Convert.ToInt32(Fila.TIPO_INCIDENCIA_ID);
                    string TPIncNom = Convert.ToString(Fila.TIPO_INCIDENCIA_NOMBRE);
                    //Agregar ModuloLog***
                    Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.NUEVO, "Tipo Incidencia Edicion", TPIncId, TPIncNom, Sesion.SESION_ID);
                    //*****				

                }
                else
                {

                    string TPIncNom = Convert.ToString(Fila.TIPO_INCIDENCIA_NOMBRE);
                    //Agregar ModuloLog***
                    Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.EDICION, "Tipo Incidencia Edicion", Sesion.WF_Tipo_Incidencias_TIPO_INCIDENCIA_ID, TPIncNom, Sesion.SESION_ID);
                    //*****				
                }

                int Modificaciones = DA_TiposIncidencias_Edicion.Update(dS_TiposIncidencias1.EC_TIPO_INCIDENCIAS_Edicion);
                LCorrecto.Text = Modificaciones.ToString() + " registros modificados";

                Sesion.Redirige("WF_Tipo_Incidencias.aspx");

                return;

            }
            catch (Exception ex)
            {
                LBorrar.Text = "Error : " + ex.Message;

            }
        }
    }
}
