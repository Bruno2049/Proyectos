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
    /// Descripción breve de WFEUsuarios.
    /// </summary>
    /// 
    public partial class WFEUsuarios : System.Web.UI.Page
    {
        protected System.Data.OleDb.OleDbDataAdapter DA_EUsuarios;
        protected System.Data.OleDb.OleDbCommand oleDbSelectCommand1;
        protected System.Data.OleDb.OleDbCommand oleDbInsertCommand1;
        protected System.Data.OleDb.OleDbCommand oleDbUpdateCommand1;
        protected System.Data.OleDb.OleDbCommand oleDbDeleteCommand1;
        protected System.Data.OleDb.OleDbConnection Conexion;
        protected System.Data.OleDb.OleDbDataAdapter DA_Perfiles;
        protected System.Data.OleDb.OleDbCommand oleDbSelectCommand2;
        protected System.Data.OleDb.OleDbCommand oleDbInsertCommand2;
        protected System.Data.OleDb.OleDbCommand oleDbUpdateCommand2;
        protected System.Data.OleDb.OleDbCommand oleDbDeleteCommand2;
        protected Infragistics.WebUI.WebCombo.WebCombo WebCombo1;
        protected DS_Usuarios dS_Usuarios1;
        DS_Usuarios.EC_USUARIOS_EDICIONRow Fila;
        CeC_Sesion Sesion;
        int asignagrupos;
        private void Habilitarcontroles(bool Caso, string Restriccion)
        {
            if (!Sesion.TienePermiso(CEC_RESTRICCIONES.S0Usuarios0Usuarios_Sistema0Nuevo) && !Sesion.TienePermiso(CEC_RESTRICCIONES.S0Usuarios0Usuarios_Sistema0Editar))
            {
            }
            if (!Sesion.TienePermiso(CEC_RESTRICCIONES.S0Usuarios0Usuarios_Sistema0Borrar))
            {
                PerfilId.Enabled = Caso;
                UsuarioNombre.Enabled = Caso;
                UsuarioUsuario.Enabled = Caso;
                UsuarioDescripcion.Enabled = Caso;

                CBBorrar.Visible = Caso;
                LBorrar.Visible = Caso;
            }
        }
        protected void Page_Load(object sender, System.EventArgs e)
        {

            Sesion = CeC_Sesion.Nuevo(this);
            Sesion.TituloPagina = "Usuarios-Edición";
            Sesion.DescripcionPagina = "Ingrese la información del usuario";


            // Permisos****************************************

            string[] Permiso = new string[10];

            asignagrupos = 0;

            /*Permiso[0] = "S";
            Permiso[1] = "S.Usuarios";
            Permiso[2] = "S.Usuarios.Usuarios_Sistema";
            Permiso[3] = "S.Usuarios.Usuarios_Sistema.Nuevo";
            Permiso[4] = "S.Usuarios.Usuarios_Sistema.Editar";
            Permiso[5] = "S.Usuarios.Usuarios_Sistema.Borrar";*/
            Permiso[0] = CEC_RESTRICCIONES.S0Usuarios0Usuarios_Sistema;
            Permiso[1] = CEC_RESTRICCIONES.S0Usuarios0Usuarios_Sistema0Nuevo;
            Permiso[2] = CEC_RESTRICCIONES.S0Usuarios0Usuarios_Sistema0Editar;
            Permiso[3] = CEC_RESTRICCIONES.S0Usuarios0Usuarios_Sistema0Borrar;

            if (Sesion.SUSCRIPCION_ID != 1)
            {
                Habilitarcontroles(false, Sesion.Restriccion.ToString());
                return;
            }

            if (!Sesion.Acceso(Permiso, CIT_Perfiles.Acceso(Sesion.PERFIL_ID, this)))
            {
                //CIT_Perfiles.CrearVentana(this,Sesion.MensajeVentanaJScript(),Sesion.TituloPagina,"Aceptar","WF_Main.aspx","","");
                Habilitarcontroles(false, Sesion.Restriccion.ToString());
                return;
            }

            Habilitarcontroles(false, Sesion.Restriccion.ToString());
            //**************************************************




            DA_EUsuarios.SelectCommand.Parameters[0].Value = Sesion.WF_Usuarios_USUARIO_ID;
            DA_EUsuarios.Fill(dS_Usuarios1.EC_USUARIOS_EDICION);

            DA_Perfiles.Fill(dS_Usuarios1.EC_PERFILES);

            PerfilId.DataValueField = "PERFIL_ID";
            //            PerfilId.Columns[0].Key = "PERFIL_ID";
            PerfilId.DisplayValue = "PERFIL_NOMBRE";
            PerfilId.DataTextField = "PERFIL_NOMBRE";

            PerfilId.DataSource = dS_Usuarios1.EC_PERFILES;
            PerfilId.DataBind();

            if (Sesion.WF_Usuarios_USUARIO_ID > 0)
            {
                if (dS_Usuarios1.EC_USUARIOS_EDICION.Rows.Count > 0)
                {
                    Fila = (DS_Usuarios.EC_USUARIOS_EDICIONRow)dS_Usuarios1.EC_USUARIOS_EDICION.Rows[0];
                    if (!IsPostBack)
                        AtarControles(true);
                }
                else
                {
                    //Hay Un ID pero no regresa ningun registro
                    //Practicamente imposible
                    Fila = dS_Usuarios1.EC_USUARIOS_EDICION.NewEC_USUARIOS_EDICIONRow();
                }
            }
            else
            {
                //crea un nuevo registro a partir de un dataset
                // por lo tanto el borrado no debe de persistir
                CBBorrar.Visible = false;
                LBorrar.Visible = false;
                Fila = dS_Usuarios1.EC_USUARIOS_EDICION.NewEC_USUARIOS_EDICIONRow();
            }
            //Agregar Módulo Log
            if(!IsPostBack)
                Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Edición de Usuarios", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
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
            this.DA_EUsuarios = new System.Data.OleDb.OleDbDataAdapter();
            this.oleDbDeleteCommand1 = new System.Data.OleDb.OleDbCommand();
            this.Conexion = new System.Data.OleDb.OleDbConnection();
            this.oleDbInsertCommand1 = new System.Data.OleDb.OleDbCommand();
            this.oleDbSelectCommand1 = new System.Data.OleDb.OleDbCommand();
            this.oleDbUpdateCommand1 = new System.Data.OleDb.OleDbCommand();
            this.dS_Usuarios1 = new DS_Usuarios();
            this.DA_Perfiles = new System.Data.OleDb.OleDbDataAdapter();
            this.oleDbDeleteCommand2 = new System.Data.OleDb.OleDbCommand();
            this.oleDbInsertCommand2 = new System.Data.OleDb.OleDbCommand();
            this.oleDbSelectCommand2 = new System.Data.OleDb.OleDbCommand();
            this.oleDbUpdateCommand2 = new System.Data.OleDb.OleDbCommand();
            ((System.ComponentModel.ISupportInitialize)(this.dS_Usuarios1)).BeginInit();
            this.BDeshacerCambios.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.BDeshacerCambios_Click);
            this.BGuardarCambios.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.BGuardarCambios_Click);
            // 
            // DA_EUsuarios
            // 
            this.DA_EUsuarios.DeleteCommand = this.oleDbDeleteCommand1;
            this.DA_EUsuarios.InsertCommand = this.oleDbInsertCommand1;
            this.DA_EUsuarios.SelectCommand = this.oleDbSelectCommand1;
            this.DA_EUsuarios.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																								   new System.Data.Common.DataTableMapping("Table", "EC_USUARIOS_EDICION", new System.Data.Common.DataColumnMapping[] {
																																																						   new System.Data.Common.DataColumnMapping("USUARIO_ID", "USUARIO_ID"),
																																																						   new System.Data.Common.DataColumnMapping("PERFIL_ID", "PERFIL_ID"),
																																																						   new System.Data.Common.DataColumnMapping("USUARIO_USUARIO", "USUARIO_USUARIO"),
																																																						   new System.Data.Common.DataColumnMapping("USUARIO_NOMBRE", "USUARIO_NOMBRE"),
																																																						   new System.Data.Common.DataColumnMapping("USUARIO_DESCRIPCION", "USUARIO_DESCRIPCION"),
																																																						   new System.Data.Common.DataColumnMapping("USUARIO_CLAVE", "USUARIO_CLAVE"),
																																																						   new System.Data.Common.DataColumnMapping("USUARIO_BORRADO", "USUARIO_BORRADO")})});
            this.DA_EUsuarios.UpdateCommand = this.oleDbUpdateCommand1;
            // 
            // oleDbDeleteCommand1
            // 
            this.oleDbDeleteCommand1.CommandText = @"DELETE FROM EC_USUARIOS WHERE (USUARIO_ID = ?) AND (PERFIL_ID = ?) AND (USUARIO_BORRADO = ? OR ? IS NULL AND USUARIO_BORRADO IS NULL) AND (USUARIO_CLAVE = ?) AND (USUARIO_DESCRIPCION = ? OR ? IS NULL AND USUARIO_DESCRIPCION IS NULL) AND (USUARIO_NOMBRE = ?) AND (USUARIO_USUARIO = ?)";
            this.oleDbDeleteCommand1.Connection = this.Conexion;
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_USUARIO_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "USUARIO_ID", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PERFIL_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "PERFIL_ID", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_USUARIO_BORRADO", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(1)), ((System.Byte)(0)), "USUARIO_BORRADO", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_USUARIO_BORRADO1", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(1)), ((System.Byte)(0)), "USUARIO_BORRADO", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_USUARIO_CLAVE", System.Data.OleDb.OleDbType.VarChar, 45, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "USUARIO_CLAVE", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_USUARIO_DESCRIPCION", System.Data.OleDb.OleDbType.VarChar, 45, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "USUARIO_DESCRIPCION", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_USUARIO_DESCRIPCION1", System.Data.OleDb.OleDbType.VarChar, 45, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "USUARIO_DESCRIPCION", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_USUARIO_NOMBRE", System.Data.OleDb.OleDbType.VarChar, 45, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "USUARIO_NOMBRE", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_USUARIO_USUARIO", System.Data.OleDb.OleDbType.VarChar, 20, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "USUARIO_USUARIO", System.Data.DataRowVersion.Original, null));
            // 
            // Conexion
            // 
            this.Conexion.ConnectionString = ((string)(configurationAppSettings.GetValue("gBDatos.ConnectionString", typeof(string))));
            // 
            // oleDbInsertCommand1
            // 
            this.oleDbInsertCommand1.CommandText = "INSERT INTO EC_USUARIOS(USUARIO_ID, PERFIL_ID, USUARIO_USUARIO, USUARIO_NOMBRE, " +
                "USUARIO_DESCRIPCION, USUARIO_CLAVE,USUARIO_EMAIL, USUARIO_BORRADO) VALUES (?, ?, ?, ?, ?, ?, ?, ?" +
                ")";
            this.oleDbInsertCommand1.Connection = this.Conexion;
            this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("USUARIO_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "USUARIO_ID", System.Data.DataRowVersion.Current, null));
            this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERFIL_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "PERFIL_ID", System.Data.DataRowVersion.Current, null));
            this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("USUARIO_USUARIO", System.Data.OleDb.OleDbType.VarChar, 20, "USUARIO_USUARIO"));
            this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("USUARIO_NOMBRE", System.Data.OleDb.OleDbType.VarChar, 45, "USUARIO_NOMBRE"));
            this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("USUARIO_DESCRIPCION", System.Data.OleDb.OleDbType.VarChar, 45, "USUARIO_DESCRIPCION"));
            this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("USUARIO_CLAVE", System.Data.OleDb.OleDbType.VarChar, 45, "USUARIO_CLAVE"));
            this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("USUARIO_EMAIL", System.Data.OleDb.OleDbType.VarChar, 45, "USUARIO_EMAIL"));
            this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("USUARIO_BORRADO", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(1)), ((System.Byte)(0)), "USUARIO_BORRADO", System.Data.DataRowVersion.Current, null));

            // 
            // oleDbSelectCommand1
            // 
            this.oleDbSelectCommand1.CommandText = "SELECT USUARIO_ID, PERFIL_ID, USUARIO_USUARIO, USUARIO_NOMBRE, USUARIO_DESCRIPCIO" +
                "N, USUARIO_CLAVE, USUARIO_EMAIL, USUARIO_BORRADO FROM EC_USUARIOS WHERE (USUARIO_ID = ?)";
            this.oleDbSelectCommand1.Connection = this.Conexion;
            this.oleDbSelectCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("USUARIO_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "USUARIO_ID", System.Data.DataRowVersion.Current, null));
            // 
            // oleDbUpdateCommand1
            // 
            this.oleDbUpdateCommand1.CommandText = "UPDATE EC_USUARIOS SET PERFIL_ID = ?, USUARIO_USUARIO = ?, USUARIO_NOMBRE = ?, U" +
                "SUARIO_DESCRIPCION = ?, USUARIO_CLAVE = ?, USUARIO_EMAIL = ?, USUARIO_BORRADO = ? WHERE (USUARIO_ID" +
                " = ?)";
            this.oleDbUpdateCommand1.Connection = this.Conexion;
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERFIL_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "PERFIL_ID", System.Data.DataRowVersion.Current, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("USUARIO_USUARIO", System.Data.OleDb.OleDbType.VarChar, 20, "USUARIO_USUARIO"));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("USUARIO_NOMBRE", System.Data.OleDb.OleDbType.VarChar, 45, "USUARIO_NOMBRE"));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("USUARIO_DESCRIPCION", System.Data.OleDb.OleDbType.VarChar, 45, "USUARIO_DESCRIPCION"));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("USUARIO_CLAVE", System.Data.OleDb.OleDbType.VarChar, 45, "USUARIO_CLAVE"));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("USUARIO_EMAIL", System.Data.OleDb.OleDbType.VarChar, 45, "USUARIO_EMAIL"));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("USUARIO_BORRADO", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(1)), ((System.Byte)(0)), "USUARIO_BORRADO", System.Data.DataRowVersion.Current, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_USUARIO_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "USUARIO_ID", System.Data.DataRowVersion.Original, null));
            // 
            // dS_Usuarios1
            // 
            this.dS_Usuarios1.DataSetName = "DS_Usuarios";
            this.dS_Usuarios1.Locale = new System.Globalization.CultureInfo("es-MX");
            // 
            // DA_Perfiles
            // 
            this.DA_Perfiles.DeleteCommand = this.oleDbDeleteCommand2;
            this.DA_Perfiles.InsertCommand = this.oleDbInsertCommand2;
            this.DA_Perfiles.SelectCommand = this.oleDbSelectCommand2;
            this.DA_Perfiles.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																								  new System.Data.Common.DataTableMapping("Table", "EC_PERFILES", new System.Data.Common.DataColumnMapping[] {
																																																				  new System.Data.Common.DataColumnMapping("PERFIL_ID", "PERFIL_ID"),
																																																				  new System.Data.Common.DataColumnMapping("PERFIL_NOMBRE", "PERFIL_NOMBRE"),
																																																				  new System.Data.Common.DataColumnMapping("PERFIL_BORRADO", "PERFIL_BORRADO")})});
            this.DA_Perfiles.UpdateCommand = this.oleDbUpdateCommand2;
            // 
            // oleDbDeleteCommand2
            // 
            this.oleDbDeleteCommand2.CommandText = "DELETE FROM EC_PERFILES WHERE (PERFIL_ID = ?) AND (PERFIL_BORRADO = ? OR ? IS NU" +
                "LL AND PERFIL_BORRADO IS NULL) AND (PERFIL_NOMBRE = ?)";
            this.oleDbDeleteCommand2.Connection = this.Conexion;
            this.oleDbDeleteCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PERFIL_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "PERFIL_ID", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PERFIL_BORRADO", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(1)), ((System.Byte)(0)), "PERFIL_BORRADO", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PERFIL_BORRADO1", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(1)), ((System.Byte)(0)), "PERFIL_BORRADO", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PERFIL_NOMBRE", System.Data.OleDb.OleDbType.VarChar, 45, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "PERFIL_NOMBRE", System.Data.DataRowVersion.Original, null));
            // 
            // oleDbInsertCommand2
            // 
            this.oleDbInsertCommand2.CommandText = "INSERT INTO EC_PERFILES(PERFIL_ID, PERFIL_NOMBRE, PERFIL_BORRADO) VALUES (?, ?, " +
                "?)";
            this.oleDbInsertCommand2.Connection = this.Conexion;
            this.oleDbInsertCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERFIL_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "PERFIL_ID", System.Data.DataRowVersion.Current, null));
            this.oleDbInsertCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERFIL_NOMBRE", System.Data.OleDb.OleDbType.VarChar, 45, "PERFIL_NOMBRE"));
            this.oleDbInsertCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERFIL_BORRADO", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(1)), ((System.Byte)(0)), "PERFIL_BORRADO", System.Data.DataRowVersion.Current, null));
            // 
            // oleDbSelectCommand2
            // 
            this.oleDbSelectCommand2.CommandText = "SELECT PERFIL_ID, PERFIL_NOMBRE, PERFIL_BORRADO FROM EC_PERFILES WHERE (PERFIL_B" +
                "ORRADO = 0)";
            this.oleDbSelectCommand2.Connection = this.Conexion;
            // 
            // oleDbUpdateCommand2
            // 
            this.oleDbUpdateCommand2.CommandText = "UPDATE EC_PERFILES SET PERFIL_ID = ?, PERFIL_NOMBRE = ?, PERFIL_BORRADO = ? WHER" +
                "E (PERFIL_ID = ?) AND (PERFIL_BORRADO = ? OR ? IS NULL AND PERFIL_BORRADO IS NUL" +
                "L) AND (PERFIL_NOMBRE = ?)";
            this.oleDbUpdateCommand2.Connection = this.Conexion;
            this.oleDbUpdateCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERFIL_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "PERFIL_ID", System.Data.DataRowVersion.Current, null));
            this.oleDbUpdateCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERFIL_NOMBRE", System.Data.OleDb.OleDbType.VarChar, 45, "PERFIL_NOMBRE"));
            this.oleDbUpdateCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERFIL_BORRADO", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(1)), ((System.Byte)(0)), "PERFIL_BORRADO", System.Data.DataRowVersion.Current, null));
            this.oleDbUpdateCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PERFIL_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "PERFIL_ID", System.Data.DataRowVersion.Original, null));
            this.oleDbUpdateCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PERFIL_BORRADO", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(1)), ((System.Byte)(0)), "PERFIL_BORRADO", System.Data.DataRowVersion.Original, null));
            this.oleDbUpdateCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PERFIL_BORRADO1", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(1)), ((System.Byte)(0)), "PERFIL_BORRADO", System.Data.DataRowVersion.Original, null));
            this.oleDbUpdateCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PERFIL_NOMBRE", System.Data.OleDb.OleDbType.VarChar, 45, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "PERFIL_NOMBRE", System.Data.DataRowVersion.Original, null));
            ((System.ComponentModel.ISupportInitialize)(this.dS_Usuarios1)).EndInit();

        }
        #endregion
        private void AtarControles(bool Cargar)
        {
            //DA_EUsuarios.Fill(dS_Usuarios1.EC_USUARIOS_EDICION);
            //Fila = (DS_Usuarios.EC_USUARIOS_EDICIONRow)dS_Usuarios1.EC_USUARIOS_EDICION.Rows[0];
            if (Cargar == true)
            {
                UsuarioUsuario.Text = Fila.USUARIO_USUARIO;
                UsuarioNombre.Text = Fila.USUARIO_NOMBRE;

                if (!Fila.IsUSUARIO_DESCRIPCIONNull())
                    UsuarioDescripcion.Text = Fila.USUARIO_DESCRIPCION;
                UsuarioClave.Text = Fila.USUARIO_CLAVE;
                UsuarioClaveC.Text = Fila.USUARIO_CLAVE;
                DA_Perfiles.Fill(dS_Usuarios1.EC_PERFILES);
                PerfilId.DataValueField = "PERFIL_ID";
                //            PerfilId.Columns[0].Key = "PERFIL_ID";
                PerfilId.DisplayValue = "PERFIL_NOMBRE";
                PerfilId.DataTextField = "PERFIL_NOMBRE";
                if (!Fila.IsUSUARIO_EMAILNull())
                    txtCorreo.Text = Fila.USUARIO_EMAIL;
                PerfilId.DataSource = dS_Usuarios1.EC_PERFILES;
                PerfilId.DataBind();
                CBBorrar.Checked = Convert.ToBoolean(Fila.USUARIO_BORRADO);
                CeC_Grid.SeleccionaID(PerfilId, Fila.PERFIL_ID);


                //PerfilId.DataValue = Convert.ToInt32(Fila.PERFIL_ID);
                //				PerfilId.SelectedIndex = Convert.ToInt32(Fila.PERFIL_ID);
            }
            else
            {
                Fila.USUARIO_NOMBRE = Convert.ToString(UsuarioNombre.Text);
                Fila.PERFIL_ID = Convert.ToInt32(PerfilId.DataValue);
                Fila.USUARIO_USUARIO = Convert.ToString(UsuarioUsuario.Text);
                Fila.USUARIO_DESCRIPCION = Convert.ToString(UsuarioDescripcion.Text);
                Fila.USUARIO_CLAVE = Convert.ToString(UsuarioClave.Text);
                Fila.USUARIO_EMAIL = Convert.ToString(txtCorreo.Text);
                Fila.USUARIO_BORRADO = Convert.ToInt32(CBBorrar.Checked);

            }

        }
        private bool ExisteUsuario(string NombreUsuario)
        {
            if (CeC_BD.EjecutaEscalarInt("SELECT USUARIO_ID FROM EC_USUARIOS WHERE USUARIO_USUARIO = '" + NombreUsuario + "' AND EC_USUARIOS.USUARIO_BORRADO = 0") > 0)
                return true;
            return false;

        }

        protected void BGuardarCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            if (Sesion.SUSCRIPCION_ID != 1)
            {
                LError.Text = "No tiene privilegios para hacer esto, se reportará el uso indevido";
                return;
            }
            LCorrecto.Text = "";
            LError.Text = "";
            if (UsuarioUsuario.Text != CeC_BD.EjecutaEscalarString("SELECT USUARIO_USUARIO FROM EC_USUARIOS WHERE USUARIO_ID = " + Sesion.WF_Usuarios_USUARIO_ID) && ExisteUsuario(UsuarioUsuario.Text))
            {
                LError.Text = "Usuario " + UsuarioUsuario.Text + " ya esta en uso. Intente con otro nombre de Usuario";
                return;
            }

            try
            {
                AtarControles(false);

                if (CBBorrar.Checked)
                {
                }

                if (Sesion.WF_Usuarios_USUARIO_ID < 0) // Es nuevo
                {
                    Fila.USUARIO_ID = CeC_Autonumerico.GeneraAutonumerico("EC_USUARIOS", "USUARIO_ID", Sesion.SESION_ID, Sesion.SuscripcionParametro);
                    if (Fila.USUARIO_ID < 0)
                    {
                        LError.Text = "Ha Llegado al Limite de Usuarios Permitido Por Esta Version";
                        return;
                    }
                    Fila.USUARIO_BORRADO = 0;//nuevo no debe de ser borrado

                    //Agrega el registro al dataset

                    //Agregar ModuloLog***
                    int UsuaID = Convert.ToInt32(Fila.USUARIO_ID);
                    string UsuaNombre = Convert.ToString(Fila.USUARIO_NOMBRE);
                    Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.BORRADO, "Usuarios", UsuaID, UsuaNombre, Sesion.SESION_ID);
                    //*****		
                    dS_Usuarios1.EC_USUARIOS_EDICION.AddEC_USUARIOS_EDICIONRow(Fila);
                    Sesion.WF_Usuarios_USUARIO_ID = UsuaID;
                }
                else
                {
                    //Agregar ModuloLog***
                    string UsuaNombre = Convert.ToString(Fila.USUARIO_NOMBRE);
                    Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.BORRADO, "Usuarios", Sesion.WF_Usuarios_USUARIO_ID, UsuaNombre, Sesion.SESION_ID);
                    //*****		
                }

                int Modificaciones = DA_EUsuarios.Update(dS_Usuarios1.EC_USUARIOS_EDICION);
                LCorrecto.Text = Modificaciones.ToString() + " registros Modificados";
                if (asignagrupos != 1 && PerfilId.Enabled)
                    Sesion.Redirige("WF_Usuarios.aspx?SuscripcionID=" + Sesion.SuscripcionParametro);
                else
                {
                    if (asignagrupos != 1)
                    {
                        Sesion.Redirige("WF_Main.aspx");
                    }
                }
            }
            catch (Exception ex)
            {
                LError.Text = "Error: " + ex.Message;
            }
        }

        protected void BDeshacerCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            AtarControles(true);
        }
        protected void PerfilId_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
        {

            CeC_Grid.AplicaFormato(PerfilId);
        }
        protected void btnAsignarGrupos_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {

        }
        protected void BGuardarCambios_Click1(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {

        }
}
}
