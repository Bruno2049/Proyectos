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
using Infragistics.WebUI.UltraWebGrid.ExcelExport;
using Reports = Infragistics.Documents.Reports;
using Report = Infragistics.Documents.Reports.Report;
using ReportText = Infragistics.Documents.Reports.Report.Text;

namespace eClock
{
    /// <summary>
    /// Descripción breve de WF_Turnos1.
    /// </summary>
    public partial class WF_Turnos1 : System.Web.UI.Page
    {
        protected System.Data.OleDb.OleDbDataAdapter DA_Turno;
        protected System.Data.OleDb.OleDbCommand oleDbSelectCommand1;
        protected System.Data.OleDb.OleDbCommand oleDbInsertCommand1;
        protected System.Data.OleDb.OleDbCommand oleDbUpdateCommand1;
        protected System.Data.OleDb.OleDbCommand oleDbDeleteCommand1;
        protected System.Data.OleDb.OleDbConnection Conexion;
        protected DS_Turnos dS_Turnos1;
        protected System.Data.OleDb.OleDbCommand Borrar_Turno;

        CeC_Sesion Sesion;

        private void ControlVisible()
        {
            Grid.Visible = false;
            BAgregarTurno.Visible = false;
            BBorrarTurno.Visible = false;
            BEditarTurno.Visible = false;
            TurnosCheckBox1.Visible = false;
        }

        private void Habilitarcontroles()
        {
            if (!Sesion.TienePermiso(CEC_RESTRICCIONES.S0Turnos0Edicion0Detalles))
                BEditarTurno.Visible = false;

            if (!Sesion.TienePermiso(CEC_RESTRICCIONES.S0Turnos0Nuevo))
            {
                BAgregarTurno.Visible = false;
                BtnDuplicar.Visible = false;
            }

            if (!Sesion.TienePermiso(CEC_RESTRICCIONES.S0Turnos0Borrar))
                BBorrarTurno.Visible = false;
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            // Introducir aquí el código de usuario para inicializar la página
            Grid.DisplayLayout.CellClickActionDefault = Infragistics.WebUI.UltraWebGrid.CellClickAction.RowSelect;

            Sesion = CeC_Sesion.Nuevo(this);
            //Descripcion y Titulo de la Pagina
            Sesion.TituloPagina = "Turnos";
            Sesion.DescripcionPagina = "Seleccione un turno para editarlo o borrarlo; o cree un nuevo turno";
            //Descripcion y Titulo de la Pagina

            
            {
                // Permisos****************************************
                if (!Sesion.TienePermisoOHijos(CEC_RESTRICCIONES.S0Turnos, true))
                {
                    
                    ControlVisible();
                    return;
                }
                Habilitarcontroles();
                //**************************************************
            }
            Sesion.DescripcionPagina = "Cree un nuevo turno, o seleccione un turno para consultar su información o borrarlo";
            Sesion.TituloPagina = "Turnos";

            //Agregar ModuloLog***
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Turnos", 0, "Consulta de Turnos", Sesion.SESION_ID);
            //*****		


            Sesion.ControlaBoton(ref BBorrarTurno);
            Sesion.ControlaBoton(ref BEditarTurno);
            Sesion.ControlaBoton(ref BAgregarTurno);
            Sesion.ControlaBoton(ref BtnDuplicar);
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
            this.DA_Turno = new System.Data.OleDb.OleDbDataAdapter();
            this.oleDbDeleteCommand1 = new System.Data.OleDb.OleDbCommand();
            this.Conexion = new System.Data.OleDb.OleDbConnection();
            this.oleDbInsertCommand1 = new System.Data.OleDb.OleDbCommand();
            this.oleDbSelectCommand1 = new System.Data.OleDb.OleDbCommand();
            this.oleDbUpdateCommand1 = new System.Data.OleDb.OleDbCommand();
            this.dS_Turnos1 = new DS_Turnos();
            this.Borrar_Turno = new System.Data.OleDb.OleDbCommand();

            ((System.ComponentModel.ISupportInitialize)(this.dS_Turnos1)).BeginInit();
            this.BBorrarTurno.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.BBorrarTurno_Click);
            this.BAgregarTurno.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.BAgregarTurno_Click);
            this.BEditarTurno.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.BEditarTurno_Click);
            // 
            // DA_Turno
            // 
            this.DA_Turno.DeleteCommand = this.oleDbDeleteCommand1;
            this.DA_Turno.InsertCommand = this.oleDbInsertCommand1;
            this.DA_Turno.SelectCommand = this.oleDbSelectCommand1;
            this.DA_Turno.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																							   new System.Data.Common.DataTableMapping("Table", "EC_TURNOS", new System.Data.Common.DataColumnMapping[] {
																																																			 new System.Data.Common.DataColumnMapping("TURNO_ID", "TURNO_ID"),
																																																			 new System.Data.Common.DataColumnMapping("TIPO_TURNO_ID", "TIPO_TURNO_ID"),
																																																			 new System.Data.Common.DataColumnMapping("TURNO_NOMBRE", "TURNO_NOMBRE"),
																																																			 new System.Data.Common.DataColumnMapping("TURNO_ASISTENCIA", "TURNO_ASISTENCIA"),
																																																			 new System.Data.Common.DataColumnMapping("TURNO_BORRADO", "TURNO_BORRADO")})});
            this.DA_Turno.UpdateCommand = this.oleDbUpdateCommand1;
            // 
            // oleDbDeleteCommand1
            // 
            this.oleDbDeleteCommand1.CommandText = "DELETE FROM EC_TURNOS WHERE (TURNO_ID = ?) AND (TIPO_TURNO_ID = ? OR ? IS NULL A" +
                "ND TIPO_TURNO_ID IS NULL) AND (TURNO_ASISTENCIA = ?) AND (TURNO_BORRADO = ? OR ?" +
                " IS NULL AND TURNO_BORRADO IS NULL) AND (TURNO_NOMBRE = ?)";
            this.oleDbDeleteCommand1.Connection = this.Conexion;
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TURNO_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TURNO_ID", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TIPO_TURNO_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TIPO_TURNO_ID", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TIPO_TURNO_ID1", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TIPO_TURNO_ID", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TURNO_ASISTENCIA", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(1)), ((System.Byte)(0)), "TURNO_ASISTENCIA", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TURNO_BORRADO", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(1)), ((System.Byte)(0)), "TURNO_BORRADO", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TURNO_BORRADO1", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(1)), ((System.Byte)(0)), "TURNO_BORRADO", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TURNO_NOMBRE", System.Data.OleDb.OleDbType.VarChar, 45, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "TURNO_NOMBRE", System.Data.DataRowVersion.Original, null));
            // 
            // Conexion
            // 
            this.Conexion.ConnectionString = CeC_BD.CadenaConexion();
            // 
            // oleDbInsertCommand1
            // 
            this.oleDbInsertCommand1.CommandText = "INSERT INTO EC_TURNOS(TURNO_ID, TIPO_TURNO_ID, TURNO_NOMBRE, TURNO_ASISTENCIA, T" +
                "URNO_BORRADO) VALUES (?, ?, ?, ?, ?)";
            this.oleDbInsertCommand1.Connection = this.Conexion;
            this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("TURNO_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TURNO_ID", System.Data.DataRowVersion.Current, null));
            this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("TIPO_TURNO_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TIPO_TURNO_ID", System.Data.DataRowVersion.Current, null));
            this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("TURNO_NOMBRE", System.Data.OleDb.OleDbType.VarChar, 45, "TURNO_NOMBRE"));
            this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("TURNO_ASISTENCIA", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(1)), ((System.Byte)(0)), "TURNO_ASISTENCIA", System.Data.DataRowVersion.Current, null));
            this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("TURNO_BORRADO", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(1)), ((System.Byte)(0)), "TURNO_BORRADO", System.Data.DataRowVersion.Current, null));
            // 
            // oleDbSelectCommand1
            // 
            this.oleDbSelectCommand1.CommandText = @"SELECT EC_TURNOS.TURNO_ID, EC_TURNOS.TIPO_TURNO_ID, EC_TURNOS.TURNO_NOMBRE, EC_TURNOS.TURNO_PHEXTRAS,EC_TURNOS.TURNO_ASISTENCIA, EC_TURNOS.TURNO_PHEXTRAS, EC_TURNOS.TURNO_BORRADO, EC_TIPO_TURNOS.TIPO_TURNO_NOMBRE, 123456789 AS STATUS FROM EC_TURNOS, EC_TIPO_TURNOS WHERE EC_TURNOS.TIPO_TURNO_ID = EC_TIPO_TURNOS.TIPO_TURNO_ID AND (EC_TURNOS.TURNO_BORRADO = 0) ORDER BY EC_TURNOS.TURNO_NOMBRE";
            this.oleDbSelectCommand1.Connection = this.Conexion;
            // 
            // oleDbUpdateCommand1
            // 
            this.oleDbUpdateCommand1.CommandText = @"UPDATE EC_TURNOS SET TURNO_ID = ?, TIPO_TURNO_ID = ?, TURNO_NOMBRE = ?, TURNO_ASISTENCIA = ?, TURNO_BORRADO = ? WHERE (TURNO_ID = ?) AND (TIPO_TURNO_ID = ? OR ? IS NULL AND TIPO_TURNO_ID IS NULL) AND (TURNO_ASISTENCIA = ?) AND (TURNO_BORRADO = ? OR ? IS NULL AND TURNO_BORRADO IS NULL) AND (TURNO_NOMBRE = ?)";
            this.oleDbUpdateCommand1.Connection = this.Conexion;
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("TURNO_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TURNO_ID", System.Data.DataRowVersion.Current, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("TIPO_TURNO_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TIPO_TURNO_ID", System.Data.DataRowVersion.Current, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("TURNO_NOMBRE", System.Data.OleDb.OleDbType.VarChar, 45, "TURNO_NOMBRE"));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("TURNO_ASISTENCIA", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(1)), ((System.Byte)(0)), "TURNO_ASISTENCIA", System.Data.DataRowVersion.Current, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("TURNO_PHEXTRAS", System.Data.OleDb.OleDbType.Boolean, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(1)), ((System.Byte)(0)), "TURNO_PHEXTRAS", System.Data.DataRowVersion.Current, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("TURNO_BORRADO", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(1)), ((System.Byte)(0)), "TURNO_BORRADO", System.Data.DataRowVersion.Current, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TURNO_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TURNO_ID", System.Data.DataRowVersion.Original, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TIPO_TURNO_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TIPO_TURNO_ID", System.Data.DataRowVersion.Original, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TIPO_TURNO_ID1", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TIPO_TURNO_ID", System.Data.DataRowVersion.Original, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TURNO_ASISTENCIA", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(1)), ((System.Byte)(0)), "TURNO_ASISTENCIA", System.Data.DataRowVersion.Original, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TURNO_BORRADO", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(1)), ((System.Byte)(0)), "TURNO_BORRADO", System.Data.DataRowVersion.Original, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TURNO_BORRADO1", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(1)), ((System.Byte)(0)), "TURNO_BORRADO", System.Data.DataRowVersion.Original, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TURNO_NOMBRE", System.Data.OleDb.OleDbType.VarChar, 45, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "TURNO_NOMBRE", System.Data.DataRowVersion.Original, null));
            // 
            // dS_Turnos1
            // 
            this.dS_Turnos1.DataSetName = "DS_Turnos";
            this.dS_Turnos1.Locale = new System.Globalization.CultureInfo("es-MX");
            // 
            // Borrar_Turno
            // 
            this.Borrar_Turno.CommandText = "UPDATE EC_TURNOS SET TURNO_BORRADO = 1 WHERE (TURNO_ID = ?)";
            this.Borrar_Turno.Connection = this.Conexion;
            this.Borrar_Turno.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TURNO_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TURNO_ID", System.Data.DataRowVersion.Original, null));
            ((System.ComponentModel.ISupportInitialize)(this.dS_Turnos1)).EndInit();

        }
        #endregion
        private void BAgregarTurno_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            Sesion.WF_Turnos_TURNO_ID = -1;
            //Sesion.Redirige("WF_TurnosE.aspx");
            Sesion.Redirige("WF_TurnosEdicion.aspx?SuscripcionID=" + Sesion.SuscripcionParametro);
        }

        private void BEditarTurno_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
          
        }

        private void BBorrarTurno_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            LCorrecto.Text = "";
            LError.Text = "";
            int Numero_registros = Grid.Rows.Count;

            for (int i = 0; i < Numero_registros; i++)
            {
                if (Grid.Rows[i].Selected)
                {
                    try
                    {
                        int ID_Borrado = Convert.ToInt32(Grid.Rows[i].Cells[0].Value);
                        int ret = CeC_BD.EjecutaEscalarInt("SELECT * FROM EC_PERSONAS WHERE EC_PERSONAS.TURNO_ID = " + ID_Borrado);

                        if (ret > 0)
                        {
                            LError.Text = "No se pueden borrar turnos que se encuentren ligados a personas";
                            return;
                        }
                        string NombreTurno = Convert.ToString(Grid.Rows[i].Cells[2].Value);

                        //Agregar ModuloLog***
                        Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.BORRADO, "Turnos", ID_Borrado, NombreTurno, Sesion.SESION_ID);
                        //*****		

                        if (Conexion.State != System.Data.ConnectionState.Open)
                            Conexion.Open();
                        Borrar_Turno.Parameters[0].Value = ID_Borrado;
                        int Modificaciones = Borrar_Turno.ExecuteNonQuery();
                        LCorrecto.Text = Modificaciones.ToString() + " resgistros modificados";

                        dS_Turnos1.EC_TURNOS.Clear();
                        DA_Turno.Fill(dS_Turnos1.EC_TURNOS);
                        Grid.DataBind();
                        return;
                    }
                    catch (Exception ex)
                    {
                        LError.Text = "Error : " + ex.Message;
                        return;
                    }
                }
            }
            LError.Text = "Debes de seleccionar una fila";
        }

        protected void TurnosCheckBox1_CheckedChanged(object sender, System.EventArgs e)
        {
            Grid.DataBind();
        }


        protected void Grid_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
        {
            CeC_Grid.AplicaFormato(Grid, false, false, false, false);
            Grid.Rows.Band.RowSelectorStyle.Width = 14;
        }

        protected void Webimagebutton1_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            LCorrecto.Text = "";
            LError.Text = "";
            int IDTurno = -1;
            DS_Turnos2.EC_TURNOS_EDICIONRow Fila;
            DS_Turnos2.EC_TURNOS_EDICIONRow FilaD;
            DS_Turnos2TableAdapters.EC_TURNOS_EDICIONTableAdapter TADSTurnos2 = new DS_Turnos2TableAdapters.EC_TURNOS_EDICIONTableAdapter();
            DS_Turnos2 dS_Turnos2 = new DS_Turnos2();
            int Numero_registros = Grid.Rows.Count;
            decimal Copia_ID = -1;
            for (int i = 0; i < Numero_registros; i++)
            {
                if (Grid.Rows[i].Selected)
                {
                    try
                    {
                        IDTurno = Convert.ToInt32(Grid.Rows[i].Cells[0].Value);
                        TADSTurnos2.Fill(dS_Turnos2.EC_TURNOS_EDICION, IDTurno);
                        string NombreTurno = Convert.ToString(Grid.Rows[i].Cells[2].Value);
                        if (dS_Turnos2.EC_TURNOS_EDICION.Rows.Count > 0)
                        {
                            Fila = (DS_Turnos2.EC_TURNOS_EDICIONRow)dS_Turnos2.EC_TURNOS_EDICION.Rows[0];
                            FilaD = dS_Turnos2.EC_TURNOS_EDICION.NewEC_TURNOS_EDICIONRow();
                            FilaD.TURNO_PHEXTRAS = dS_Turnos2.EC_TURNOS_EDICION[0].TURNO_PHEXTRAS;
                            FilaD.TURNO_BORRADO = dS_Turnos2.EC_TURNOS_EDICION[0].TURNO_BORRADO;
                            FilaD.TIPO_TURNO_ID = dS_Turnos2.EC_TURNOS_EDICION[0].TIPO_TURNO_ID;
                            FilaD.TURNO_ASISTENCIA = dS_Turnos2.EC_TURNOS_EDICION[0].TURNO_ASISTENCIA;
                            Copia_ID = FilaD.TURNO_ID = Convert.ToInt32(CeC_Autonumerico.GeneraAutonumerico("EC_TURNOS", "TURNO_ID"));
                            FilaD.TURNO_NOMBRE = Convert.ToString("Copia de " + dS_Turnos2.EC_TURNOS_EDICION[0].TURNO_NOMBRE);
                            dS_Turnos2.EC_TURNOS_EDICION.AddEC_TURNOS_EDICIONRow(FilaD);
                        }
                        TADSTurnos2.Update(dS_Turnos2.EC_TURNOS_EDICION);
                    }
                    catch (Exception ex)
                    {
                        LError.Text = "Error : " + ex.Message;
                        return;
                    }
                    try
                    {
                        DS_CopiaTurnosSemanalDia DSTSD = new DS_CopiaTurnosSemanalDia();
                        DS_CopiaTurnosSemanalDiaTableAdapters.EC_TURNOS_SEMANAL_DIATableAdapter TSDTA = new DS_CopiaTurnosSemanalDiaTableAdapters.EC_TURNOS_SEMANAL_DIATableAdapter();
                        TSDTA.Fill(DSTSD.EC_TURNOS_SEMANAL_DIA, IDTurno);
                        int cont = DSTSD.EC_TURNOS_SEMANAL_DIA.Rows.Count;

                        for (int a = 0; a < cont; a++)
                        {
                            DS_CopiaTurnosSemanalDia.EC_TURNOS_SEMANAL_DIARow FilaTSD;
                            DS_CopiaTurnosSemanalDia.EC_TURNOS_SEMANAL_DIARow FilaTSD2;
                            FilaTSD = (DS_CopiaTurnosSemanalDia.EC_TURNOS_SEMANAL_DIARow)DSTSD.EC_TURNOS_SEMANAL_DIA.Rows[a];
                            FilaTSD2 = DSTSD.EC_TURNOS_SEMANAL_DIA.NewEC_TURNOS_SEMANAL_DIARow();
                            FilaTSD2.DIA_SEMANA_ID = FilaTSD.DIA_SEMANA_ID;
                            FilaTSD2.TURNO_DIA_ID = FilaTSD.TURNO_DIA_ID;
                            FilaTSD2.TURNO_SEMANAL_DIA_ID = CeC_Autonumerico.GeneraAutonumerico("EC_TURNOS_SEMANAL_DIA", "TURNO_SEMANAL_DIA_ID");
                            FilaTSD2.TURNO_ID = Copia_ID;

                            DSTSD.EC_TURNOS_SEMANAL_DIA.AddEC_TURNOS_SEMANAL_DIARow(FilaTSD2);
                        }
                        TSDTA.Update(DSTSD.EC_TURNOS_SEMANAL_DIA);
                        Grid.DataBind();
                        Sesion.Redirige("WF_Turnos.aspx");
                        return;
                    }
                    catch (Exception ex)
                    {
                        LError.Text = "Error : " + ex.Message;
                        return;
                    }
                }
            }
            LError.Text = "Debes de seleccionar una fila";
        }
        protected void BAgregarTurno_Click1(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {

        }
        protected void BEditarTurno_Click1(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            LCorrecto.Text = "";
            LError.Text = "";
            int Numero_registros = Grid.Rows.Count;

            for (int i = 0; i < Numero_registros; i++)
            {
                if (Grid.Rows[i].Selected)
                {
                    try
                    {
                        int ID_Sesion = Convert.ToInt32(Grid.Rows[i].Cells[0].Value);
                        string NombreTurno = Convert.ToString(Grid.Rows[i].Cells[2].Value);

                        //Agregar ModuloLog***
                        Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Turnos", ID_Sesion, NombreTurno, Sesion.SESION_ID);
                        //*****		

                        Sesion.WF_Turnos_TURNO_ID = ID_Sesion;
                        //Sesion.Redirige("WF_TurnosE.aspx");
                        Sesion.Redirige("WF_TurnosEdicion.aspx?SuscripcionID=" + Sesion.SuscripcionParametro);
                        return;
                    }
                    catch (Exception ex)
                    {
                        LError.Text = "Error : " + ex.Message;
                        return;
                    }
                }
            }
            LError.Text = "Debes de seleccionar una fila";
        }
        protected void BBorrarTurno_Click1(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {

        }
        protected void btImprimir_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            GridExporter.Format = Report.FileFormat.PDF;
            GridExporter.TargetPaperOrientation = Report.PageOrientation.Landscape;
            GridExporter.DownloadName = "ExportacionTerminales.pdf";
            GridExporter.Export(Grid);
        }
        protected void GridExporter_BeginExport(object sender, Infragistics.WebUI.UltraWebGrid.DocumentExport.DocumentExportEventArgs e)
        {
            CeC_Reportes.AplicaFormatoReporte(e, "Reporte de Turnos", "./imagenes/turnos64.png", Sesion);    
            
        }
        void CargaDatos()
        {
            DataSet DS = CeC_Turnos.ObtenTurnosDS(Sesion.SUSCRIPCION_ID, TurnosCheckBox1.Checked);
            Grid.DataSource = DS;
            Grid.DataMember = DS.Tables[0].TableName;
            Grid.DataKeyField = "TURNO_ID";
        }
        protected void Grid_InitializeDataSource(object sender, Infragistics.WebUI.UltraWebGrid.UltraGridEventArgs e)
        {
            CargaDatos();
        }
}
}