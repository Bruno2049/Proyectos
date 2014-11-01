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
using System.Data.OleDb;

namespace eClock
{
    /// <summary>
    /// Descripción breve de WF_Config.
    /// </summary>
    public partial class WF_Config : System.Web.UI.Page
    {
        protected System.Data.OleDb.OleDbCommand oleDbSelectCommand1;
        protected System.Data.OleDb.OleDbCommand oleDbInsertCommand1;
        protected System.Data.OleDb.OleDbCommand oleDbUpdateCommand1;
        protected System.Data.OleDb.OleDbCommand oleDbDeleteCommand1;
        protected DS_Config dS_Config1;
        protected System.Data.OleDb.OleDbConnection oleDbConnection1;
        protected System.Data.OleDb.OleDbDataAdapter DATerminalesNombres;
        protected System.Data.OleDb.OleDbCommand oleDbSelectCommand2;
        protected System.Data.OleDb.OleDbCommand oleDbInsertCommand2;
        protected System.Data.OleDb.OleDbCommand oleDbUpdateCommand2;
        protected System.Data.OleDb.OleDbCommand oleDbDeleteCommand2;
        protected System.Data.OleDb.OleDbDataAdapter DA_EC_USUARIOS;
        CeC_Sesion Sesion;

        private void Habilitarcontroles()
        {
            BGuardarCambios.Visible = false;
            BDeshacerCambios.Visible = false;
            
        }

        protected void CTerminalMaestra_Load(object sender, EventArgs e)
        {
            //CeC_BD.EjecutaEscalarString("SELECT CONFIG_USUARIO_VALOR FROM EC_CONFIG_USUARIO WHERE CONFIG_USUARIO_VARIABLE='TERMINAL_ID_ENROLA' AND CONFIG_USUARIO_ID = 0");       
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Sesion = CeC_Sesion.Nuevo(this);
            Sesion.TituloPagina = "Configuración del sistema";
            Sesion.DescripcionPagina = "Puede configurar el sistema de acuerdo a sus necesidades";

            // Permisos****************************************
            if (Sesion.SUSCRIPCION_ID != 1 || !Sesion.TienePermisoOHijos(CEC_RESTRICCIONES.S0Configuracion0Editar, true))
            {
                Habilitarcontroles();
                CIT_Perfiles.CrearVentana(this, Sesion.MensajeVentanaJScript(), Sesion.TituloPagina, "Aceptar", "WF_Main.aspx", "", "");
                return;
            }
            //**************************************************

            DATerminalesNombres.Fill(dS_Config1);
            if (!IsPostBack)
            {
                string[] Lista1 = new string[1];
                Lista1[0] = "EC_PERSONAS_DATOS";

                //				CampoClaveMail();
                LlenarCombo(ListaCampo(Lista1));
                LlenarComboCampoLlaveIncidencias();

                /*TEInicio.Value = CeC_Config.SyncH_HoraInicio;
                TEFin.Value = CeC_Config.SyncH_HoraFin;
                */
                CargaDatos();

                CeC_Grid.AplicaFormato(WCGpo1);
                CeC_Grid.AplicaFormato(WCGpo2);
                CeC_Grid.AplicaFormato(WCGpo3);
                //prellenado
                CeC_Grid.SeleccionaID(WCGpo1, CeC_Config.CampoGrupo1);
                CeC_Grid.SeleccionaID(WCGpo2, CeC_Config.CampoGrupo2);
                CeC_Grid.SeleccionaID(WCGpo3, CeC_Config.CampoGrupo3);
                CeC_Grid.SeleccionaID(WebCombo4, CeC_Config.CampoGrupo4Combo4);

                //Agregar Módulo Log
                Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Configuración", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
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
            this.DATerminalesNombres = new System.Data.OleDb.OleDbDataAdapter();
            this.oleDbDeleteCommand1 = new System.Data.OleDb.OleDbCommand();
            this.oleDbConnection1 = new System.Data.OleDb.OleDbConnection();
            this.oleDbInsertCommand1 = new System.Data.OleDb.OleDbCommand();
            this.oleDbSelectCommand1 = new System.Data.OleDb.OleDbCommand();
            this.oleDbUpdateCommand1 = new System.Data.OleDb.OleDbCommand();
            this.dS_Config1 = new DS_Config();
            this.oleDbSelectCommand2 = new System.Data.OleDb.OleDbCommand();
            this.oleDbInsertCommand2 = new System.Data.OleDb.OleDbCommand();
            this.oleDbUpdateCommand2 = new System.Data.OleDb.OleDbCommand();
            this.oleDbDeleteCommand2 = new System.Data.OleDb.OleDbCommand();
            this.DA_EC_USUARIOS = new System.Data.OleDb.OleDbDataAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dS_Config1)).BeginInit();
            this.BDeshacerCambios.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.BDeshacerCambios_Click);
            this.BGuardarCambios.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.BGuardarCambios_Click);
            // 
            // DATerminalesNombres
            // 
            this.DATerminalesNombres.DeleteCommand = this.oleDbDeleteCommand1;
            this.DATerminalesNombres.InsertCommand = this.oleDbInsertCommand1;
            this.DATerminalesNombres.SelectCommand = this.oleDbSelectCommand1;
            this.DATerminalesNombres.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																										  new System.Data.Common.DataTableMapping("Table", "TerminalesNombres", new System.Data.Common.DataColumnMapping[] {
																																																							   new System.Data.Common.DataColumnMapping("TERMINAL_ID", "TERMINAL_ID"),
																																																							   new System.Data.Common.DataColumnMapping("TERMINAL_NOMBRE", "TERMINAL_NOMBRE")})});
            this.DATerminalesNombres.UpdateCommand = this.oleDbUpdateCommand1;
            // 
            // oleDbDeleteCommand1
            // 
            this.oleDbDeleteCommand1.CommandText = "DELETE FROM EC_TERMINALES WHERE (TERMINAL_ID = ?) AND (TERMINAL_NOMBRE = ?)";
            this.oleDbDeleteCommand1.Connection = this.oleDbConnection1;
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TERMINAL_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TERMINAL_ID", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TERMINAL_NOMBRE", System.Data.OleDb.OleDbType.VarChar, 45, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "TERMINAL_NOMBRE", System.Data.DataRowVersion.Original, null));
            // 
            // oleDbConnection1
            // 
            this.oleDbConnection1.ConnectionString = ((string)(configurationAppSettings.GetValue("gBDatos.ConnectionString", typeof(string))));
            // 
            // oleDbInsertCommand1
            // 
            this.oleDbInsertCommand1.CommandText = "INSERT INTO EC_TERMINALES(TERMINAL_ID, TERMINAL_NOMBRE) VALUES (?, ?)";
            this.oleDbInsertCommand1.Connection = this.oleDbConnection1;
            this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("TERMINAL_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TERMINAL_ID", System.Data.DataRowVersion.Current, null));
            this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("TERMINAL_NOMBRE", System.Data.OleDb.OleDbType.VarChar, 45, "TERMINAL_NOMBRE"));
            // 
            // oleDbSelectCommand1
            // 
            this.oleDbSelectCommand1.CommandText = "SELECT TERMINAL_ID, TERMINAL_NOMBRE FROM EC_TERMINALES WHERE (TERMINAL_BORRADO =" +
                " 0) ORDER BY TERMINAL_NOMBRE";
            this.oleDbSelectCommand1.Connection = this.oleDbConnection1;
            // 
            // oleDbUpdateCommand1
            // 
            this.oleDbUpdateCommand1.CommandText = "UPDATE EC_TERMINALES SET TERMINAL_ID = ?, TERMINAL_NOMBRE = ? WHERE (TERMINAL_ID" +
                " = ?) AND (TERMINAL_NOMBRE = ?)";
            this.oleDbUpdateCommand1.Connection = this.oleDbConnection1;
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("TERMINAL_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TERMINAL_ID", System.Data.DataRowVersion.Current, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("TERMINAL_NOMBRE", System.Data.OleDb.OleDbType.VarChar, 45, "TERMINAL_NOMBRE"));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TERMINAL_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TERMINAL_ID", System.Data.DataRowVersion.Original, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TERMINAL_NOMBRE", System.Data.OleDb.OleDbType.VarChar, 45, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "TERMINAL_NOMBRE", System.Data.DataRowVersion.Original, null));
            // 
            // dS_Config1
            // 
            this.dS_Config1.DataSetName = "DS_Config";
            this.dS_Config1.Locale = new System.Globalization.CultureInfo("es-MX");
            // 
            // oleDbSelectCommand2
            // 
            this.oleDbSelectCommand2.CommandText = "SELECT USUARIO_ID, PERFIL_ID, USUARIO_USUARIO, USUARIO_NOMBRE, USUARIO_DESCRIPCIO" +
                "N, USUARIO_CLAVE, USUARIO_BORRADO FROM EC_USUARIOS";
            this.oleDbSelectCommand2.Connection = this.oleDbConnection1;
            // 
            // DA_EC_USUARIOS
            // 
            this.DA_EC_USUARIOS.DeleteCommand = this.oleDbDeleteCommand2;
            this.DA_EC_USUARIOS.InsertCommand = this.oleDbInsertCommand2;
            this.DA_EC_USUARIOS.SelectCommand = this.oleDbSelectCommand2;
            this.DA_EC_USUARIOS.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																									  new System.Data.Common.DataTableMapping("Table", "EC_USUARIOS", new System.Data.Common.DataColumnMapping[] {
																																																					  new System.Data.Common.DataColumnMapping("USUARIO_ID", "USUARIO_ID"),
																																																					  new System.Data.Common.DataColumnMapping("PERFIL_ID", "PERFIL_ID"),
																																																					  new System.Data.Common.DataColumnMapping("USUARIO_USUARIO", "USUARIO_USUARIO"),
																																																					  new System.Data.Common.DataColumnMapping("USUARIO_NOMBRE", "USUARIO_NOMBRE"),
																																																					  new System.Data.Common.DataColumnMapping("USUARIO_DESCRIPCION", "USUARIO_DESCRIPCION"),
																																																					  new System.Data.Common.DataColumnMapping("USUARIO_CLAVE", "USUARIO_CLAVE"),
																																																					  new System.Data.Common.DataColumnMapping("USUARIO_BORRADO", "USUARIO_BORRADO")})});
            this.DA_EC_USUARIOS.UpdateCommand = this.oleDbUpdateCommand2;
            ((System.ComponentModel.ISupportInitialize)(this.dS_Config1)).EndInit();

        }
        #endregion

        private void CargaDatos()
        {
            try
            {
                TSincTime.Value = CeC_Config.SyncTimeOutG;

                CBRecalcular.Checked = CeC_Config.Recalcula;
                this.FInicial.Value = CeC_Config.RecalculaFInicial;
                FFinal.Value = CeC_Config.RecalculaFFinal;
                try
                {
                    TextBox1.Text = CeC_Config.NombreGrupo1;
                    TextBox2.Text = CeC_Config.NombreGrupo2;
                    TextBox3.Text = CeC_Config.NombreGrupo3;
                }
                catch
                {
                }
                if (CeC_Config.CampoGrupoSeleccionado == 1) RD1.Checked = true;
                if (CeC_Config.CampoGrupoSeleccionado == 2) RD2.Checked = true;
                if (CeC_Config.CampoGrupoSeleccionado == 3) RD3.Checked = true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
        }

        private void LlenarCombo(string[] Tablas)
        {

            //ListaCampo
            DataSet DS = new DataSet("TodosCampos");
            DataTable DT = new DataTable("Tabla1");
            DataRow DR;

            WCGpo1.Columns.Clear();
            WCGpo1.Rows.Clear();

            WCGpo2.Columns.Clear();
            WCGpo2.Rows.Clear();

            WCGpo3.Columns.Clear();
            WCGpo3.Rows.Clear();

            DT.Columns.Add("Nombrecampo", System.Type.GetType("System.String"));

            WCGpo1.DataSource = DS;
            WCGpo1.DataMember = DT.TableName.ToString();
            WCGpo1.DataTextField = "Nombrecampo";//DT.Columns[0].ColumnName.ToString();
            WCGpo1.DataValue = "Nombrecampo";
            WCGpo1.DataValueField = "Nombrecampo";

            WCGpo2.DataSource = DS;
            WCGpo2.DataMember = DT.TableName.ToString();
            WCGpo2.DataTextField = "Nombrecampo";//DT.Columns[0].ColumnName.ToString();
            WCGpo2.DataValue = "Nombrecampo";
            WCGpo2.DataValueField = "Nombrecampo";

            WCGpo3.DataSource = DS;
            WCGpo3.DataMember = DT.TableName.ToString();
            WCGpo3.DataTextField = "Nombrecampo";//DT.Columns[0].ColumnName.ToString();
            WCGpo3.DataValue = "Nombrecampo";
            WCGpo3.DataValueField = "Nombrecampo";

            for (int i = 0; i < Tablas.Length; i++)
            {
                DR = DT.NewRow();
                DR[0] = Tablas[i].ToString();
                DT.Rows.Add(DR);
            }
            DS.Tables.Add(DT);

            WCGpo1.DataBind();
            WCGpo2.DataBind();
            WCGpo3.DataBind();
        }

        private void LlenarComboCampoLlaveIncidencias()
        {

            WebCombo4.Columns.Clear();
            WebCombo4.Rows.Clear();

            string QRY = "Select TIPO_INCIDENCIA_ID,TIPO_INCIDENCIA_NOMBRE FROM EC_TIPO_INCIDENCIAS WHERE TIPO_INCIDENCIA_BORRADO = 0";

            DataSet DS = new DataSet("Tabla_Set");
            OleDbDataAdapter Adaptador = new OleDbDataAdapter(QRY, oleDbConnection1);
            Adaptador.Fill(DS);

            WebCombo4.DataSource = DS;
            WebCombo4.DataMember = DS.Tables[0].TableName.ToString();
            WebCombo4.DataTextField = DS.Tables[0].Columns[1].ColumnName.ToString();
            WebCombo4.DataValueField = DS.Tables[0].Columns[0].ColumnName.ToString();

            WebCombo4.DataBind();
        }

        private string[] ListaCampo(string[] Tablas)
        {
            string ListaCampo_String = "";

            for (int i = 0; i < Tablas.Length; i++)
            {
                if (oleDbConnection1.State != System.Data.ConnectionState.Open)
                    oleDbConnection1.Open();

                OleDbCommand Commando = new OleDbCommand("Select * From " + Tablas[i] + " where " + CeC_Campos.CampoTE_Llave.ToString() + " = -1", oleDbConnection1);
                OleDbDataReader Lector = Commando.ExecuteReader();
                try
                {
                    for (int j = 0; j < Lector.FieldCount; j++)
                    {
                        string Columna_nombre = Lector.GetName(j);
                        ListaCampo_String += Tablas[i] + "." + Columna_nombre + "|";
                    }
                }
                catch (Exception ex)
                {
                    if (oleDbConnection1.State == ConnectionState.Open)
                        oleDbConnection1.Dispose();
                    string d = ex.Message;
                }
                Lector.Close();
            }
            char[] Caracter = new char[1];
            Caracter[0] = Convert.ToChar("|");
            string[] ret = ListaCampo_String.Split(Caracter);
            return ret;
        }

        protected void BDeshacerCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            CargaDatos();
        }

        protected void BGuardarCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            try
            {
                CeC_Config.SyncTimeOutG = Convert.ToInt32(TSincTime.Value);
                CeC_Config.Recalcula = CBRecalcular.Checked;
                CeC_Config.RecalculaFInicial = Convert.ToDateTime(FInicial.Value);
                CeC_Config.RecalculaFFinal = Convert.ToDateTime(FFinal.Value);
                //				Sesion.Configura.

                if ((WCGpo1.SelectedIndex == -1 && WCGpo2.SelectedIndex == -1 && WCGpo3.SelectedIndex == -1) || WebCombo4.SelectedIndex == -1)
                {
                    LError.Text = "Debe de seleccionar alguna opcion de sus listas de opciones";
                    return;
                }
                LimpiaCatalogoGrupo();
                bool Cambio = false;
                string Campos = "";
                if (RD1.Checked || RD2.Checked || RD3.Checked)
                {
                    Campos += WCGpo1.DataValue.ToString();
                    if (CeC_Config.CampoGrupo1 != WCGpo1.DataValue.ToString())
                    {                        
                        CeC_Config.CampoGrupo1 = WCGpo1.DataValue.ToString();
                        Cambio = true;
                    }
                }
                if (RD2.Checked || RD3.Checked)
                {
                    Campos += "|" + WCGpo2.DataValue.ToString();
                    if (CeC_Config.CampoGrupo2 != WCGpo2.DataValue.ToString())
                    {                        
                        CeC_Config.CampoGrupo2 = WCGpo2.DataValue.ToString();
                        Cambio = true;
                    }
                }
                if (RD3.Checked)
                {
                    Campos += "|" + WCGpo3.DataValue.ToString();
                    if (CeC_Config.CampoGrupo3 != WCGpo3.DataValue.ToString())
                    {                        
                        CeC_Config.CampoGrupo3 = WCGpo3.DataValue.ToString();
                        Cambio = true;
                    }
                    
                }
                if (Cambio == true)
                {
                    //Temporalmente se borraran las agrupaciones anteriores para poder generar las nuevas agrupaciones
                    CeC_Agrupaciones.RegeneraAgrupaciones(Sesion.SUSCRIPCION_ID, Campos, true);
                }

                if (WebCombo4.SelectedIndex != -1)
                {
                    CeC_Config.CampoGrupo4Combo4 = Convert.ToInt32(WebCombo4.SelectedRow.Cells[0].Text);
                    CeC_Config.CampoGrupo4Combo4_Texto = WebCombo4.SelectedRow.Cells[0].Text;
                }
                else
                {
                    LError.Text = "Seleccione la incidencia de Vacaciones correspondiente";
                    return;
                }
                /*CeC_Config.SyncH_HoraInicio = Convert.ToDateTime(TEInicio.Value);
                CeC_Config.SyncH_HoraFin = Convert.ToDateTime(TEFin.Value);
                */
                CeC_Config.NombreGrupo1 = TextBox1.Text;
                CeC_Config.NombreGrupo2 = TextBox2.Text;
                CeC_Config.NombreGrupo3 = TextBox3.Text;

                int ret = 0;
                if (RD1.Checked) ret = 1;
                if (RD2.Checked) ret = 2;
                if (RD3.Checked) ret = 3;

                CeC_Config.CampoGrupoSeleccionado = ret;
                if (RD1.Checked == true)
                {
                    ChecaSeleccionado(1);
                }
                if (RD2.Checked == true)
                {
                    ChecaSeleccionado(1);
                    ChecaSeleccionado(2);
                }
                if (RD3.Checked == true)
                {
                    ChecaSeleccionado(1);
                    ChecaSeleccionado(2);
                    ChecaSeleccionado(3);
                }

                if (RD2.Checked == true && WCGpo1.SelectedCell.Value.ToString() == WCGpo2.SelectedCell.Value.ToString())
                {
                    LError.Text = "No se puede usar el mismo campo para mas de un grupo";
                }
                if (RD3.Checked == true && WCGpo1.SelectedCell.Value.ToString() == WCGpo2.SelectedCell.Value.ToString())
                {
                    LError.Text = "No se puede usar el mismo campo para mas de un grupo";
                }
                CeC_BD.CreaRelacionesEmpleados();
                CeC_Campos.RecargaCampos();
                //Sesion.Redirige("WF_Main.aspx");
            }
            catch (Exception ex)
            {
                LError.Text = "Ocurio el siguiente error: \n" + ex.Message;
            }
        }

        private bool LimpiaCatalogoGrupo()
        {
            foreach (DS_Campos.EC_CAMPOSRow Campo in CeC_Campos.ds_Campos.EC_CAMPOS)
            {
                if (Campo.CATALOGO_ID >= 2 && Campo.CATALOGO_ID <= 4)
                {
                    Campo.CATALOGO_ID = 0;
                }
            }
            //CeC_Campos.Obten_Campo().CATALOGO_ID = 2;
            DS_CamposTableAdapters.TA_EC_CAMPOS TA = new DS_CamposTableAdapters.TA_EC_CAMPOS();
            TA.Update(CeC_Campos.ds_Campos);
            return true;
        }

        private bool ChecaSeleccionado(int NoGrupo)
        {
            string Seleccionado = "";
            try
            {
                if (NoGrupo == 1)
                    Seleccionado = WCGpo1.SelectedRow.Cells[0].Text;
                if (NoGrupo == 2)
                    Seleccionado = WCGpo2.SelectedRow.Cells[0].Text;
                if (NoGrupo == 3)
                    Seleccionado = WCGpo3.SelectedRow.Cells[0].Text;
                if (Seleccionado == null)
                {
                    LError.Text = "Debe seleccionar un campo del grupo " + NoGrupo.ToString();
                    return false;
                }
                else
                {
                    string[] Seleccionados = Seleccionado.Split('.');
                    if (CeC_Config.GuardaConfigGrupos(Seleccionados[1], NoGrupo + 1))
                    {
                        return true;
                    }
                    else
                    {
                        LError.Text = "Ha ocurrido un error";
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void CampoClaveMail()
        {
            DA_EC_USUARIOS.Fill(dS_Config1.EC_USUARIOS);

            DataTable Tabla = new DataTable("TablaMail");
            DataRow Fila;
            DataSet Datos = new DataSet("Mail");

            Tabla.Columns.Add("NombreCampo", System.Type.GetType("System.String"));

            for (int i = 0; i < dS_Config1.EC_USUARIOS.Columns.Count; i++)
            {
                System.Type Tipodato = dS_Config1.EC_USUARIOS.Columns[i].DataType;
                if (Tipodato == System.Type.GetType("System.String"))
                {
                    Fila = Tabla.NewRow();
                    Fila["NombreCampo"] = dS_Config1.EC_USUARIOS.Columns[i].ColumnName;
                    Tabla.Rows.Add(Fila);
                }
            }
            Datos.Tables.Add(Tabla);
        }

        protected void CTerminalMaestra_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
        {

        }

        protected void WebCombo4_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
        {
            CeC_Grid.AplicaFormato(WebCombo4);
        }

        protected void btnConfigurarLogos_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            Sesion.Redirige("WF_ConfigLogos.aspx");
        }

        protected void btnConfigurarSMTP_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            Sesion.Redirige("WF_ConfigSMTP.aspx");
        }
        //protected void btnConfigurarVariables_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        //{
        //    Sesion.Redirige("WF_ConfigVariables.aspx");
        //}
        protected void btnConfigurarVariables_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            Sesion.Redirige("WF_ConfigUsuarioE.aspx");
        }

        protected void btnDatosEmpresa_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            Sesion.Redirige("WF_DatosEmpresa.aspx");
        }

        protected void BtnWizardb_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            Sesion.OrigenConfigCampos = "WF_Config.aspx";
            Sesion.Redirige("WF_Wizardb.aspx");
        }
    }
}