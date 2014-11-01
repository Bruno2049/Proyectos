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
    /// Descripción breve de WF_PersonasAltasBajas.
    /// </summary>
    public partial class WF_PersonasAltasBajas : System.Web.UI.Page
    {
        protected System.Data.OleDb.OleDbCommand oleDbSelectCommand1;
        protected System.Data.OleDb.OleDbCommand oleDbInsertCommand1;
        protected System.Data.OleDb.OleDbCommand oleDbUpdateCommand1;
        protected System.Data.OleDb.OleDbCommand oleDbDeleteCommand1;
        protected System.Data.OleDb.OleDbDataAdapter DA_EmpleadoAltasBajas;
        protected System.Data.OleDb.OleDbConnection Conexion;
        protected DS_EmpleadoAltaBaja dS_EmpleadoAltaBaja1;
        CeC_Sesion Sesion;
        OleDbParameter Parametro = new OleDbParameter();

        private void Habilitarcontroles()
        {
            //QueryGrupo = ""
            UltraWebGrid1.Visible = false;
            AltaPersonas.Visible = false;
            BajaPersonas.Visible = false;
            Button2.Visible = false;
            PersonasCheckBox1.Visible = false;
            CBListado.Visible = false;
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            // Introducir aquí el código de usuario para inicializar la página
            //			UltraWebGrid1.DisplayLayout.CellClickActionDefault=Infragistics.WebUI.UltraWebGrid.CellClickAction.RowSelect;
            Sesion = CeC_Sesion.Nuevo(this);

            UltraWebGrid1.Rows.Band.RowSelectorStyle.Width = 14;
            Sesion.TituloPagina = "Altas y Bajas de Empleados";
            //Sesion.DescripcionPagina = "Seleccione una terminal para editarla, borrarla, o crear una nueva terminal";

            // Permisos****************************************
            if (!Sesion.TienePermisoOHijos(CEC_RESTRICCIONES.S0Empleados0Editar_Estatus, true))
            {
                Habilitarcontroles();
                return;
            }
            //**************************************************

            if (!IsPostBack)
            {

                if (Sesion.WF_EmpleadosFil_Qry.Length < 1)
                {
                    Sesion.WF_EmpleadosFil(false, false, false, "Muestra Resultados", "Filtro-Empleados Altas y Bajas", "WF_PersonasAltasBajas.aspx", "Altas y Bajas Empleados", false, true, false);
                    return;
                }
                else
                {
                    Sesion.WF_EmpleadosFil_Qry_Temp = Sesion.WF_EmpleadosFil_Qry;
                    Sesion.WF_EmpleadosFil_Qry = "";
                }
            }
            Sesion.WF_EmpleadosFil_BotonMensaje = "";

            //Sesion.DA_ModQuery(this.DA_EmpleadoAltasBajas,"AND (PERSONA_BORRADO = 0)" ,PersonasCheckBox1);	
            //Sesion.DA_ModQueryAddColumnaUltraGridPerzonalizada(this.DA_EmpleadoAltasBajas,"(PERSONA_BORRADO = 0) AND",PersonasCheckBox1," PERSONA_BORRADO ","EC_PERSONAS.PERSONA_BORRADO");

            //Agregar ModuloLog***
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Personas Altas Bajas", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE, Sesion.SESION_ID);
            //*****				
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
            this.oleDbSelectCommand1 = new System.Data.OleDb.OleDbCommand();
            this.Conexion = new System.Data.OleDb.OleDbConnection();
            this.oleDbInsertCommand1 = new System.Data.OleDb.OleDbCommand();
            this.oleDbUpdateCommand1 = new System.Data.OleDb.OleDbCommand();
            this.oleDbDeleteCommand1 = new System.Data.OleDb.OleDbCommand();
            this.DA_EmpleadoAltasBajas = new System.Data.OleDb.OleDbDataAdapter();
            this.dS_EmpleadoAltaBaja1 = new DS_EmpleadoAltaBaja();
            ((System.ComponentModel.ISupportInitialize)(this.dS_EmpleadoAltaBaja1)).BeginInit();
            this.AltaPersonas.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.AltaPersonas_Click);
            this.Button2.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.Button2_Click);
            this.BajaPersonas.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.BajaPersonas_Click);
            // 
            // oleDbSelectCommand1
            // 
            this.oleDbSelectCommand1.CommandText = "SELECT PERSONA_ID, PERSONA_LINK_ID, SUSCRIPCION_ID, TIPO_PERSONA_ID, GRUPO_2_ID, GRUP" +
                "O_3_ID, PERSONA_NOMBRE, TURNO_ID, PERSONA_EMAIL, PERSONA_BORRADO, 123456789 AS S" +
                "TATUS FROM EC_PERSONAS WHERE (PERSONA_ID > 0) AND (PERSONA_BORRADO = ?) OR (PERSONA_BORRADO = 0)";
            this.oleDbSelectCommand1.Parameters.Add(Parametro);
            this.oleDbSelectCommand1.Connection = this.Conexion;
            // 
            // Conexion
            // 
            this.Conexion.ConnectionString = ((string)(configurationAppSettings.GetValue("gBDatos.ConnectionString", typeof(string))));
            // 
            // DA_EmpleadoAltasBajas
            // 
            this.DA_EmpleadoAltasBajas.DeleteCommand = this.oleDbDeleteCommand1;
            this.DA_EmpleadoAltasBajas.InsertCommand = this.oleDbInsertCommand1;
            this.DA_EmpleadoAltasBajas.SelectCommand = this.oleDbSelectCommand1;
            this.DA_EmpleadoAltasBajas.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																											new System.Data.Common.DataTableMapping("Table", "EC_PERSONAS", new System.Data.Common.DataColumnMapping[] {
																																																							new System.Data.Common.DataColumnMapping("PERSONA_ID", "PERSONA_ID"),
																																																							new System.Data.Common.DataColumnMapping("PERSONA_LINK_ID", "PERSONA_LINK_ID"),
																																																							new System.Data.Common.DataColumnMapping("SUSCRIPCION_ID", "SUSCRIPCION_ID"),
																																																							new System.Data.Common.DataColumnMapping("TIPO_PERSONA_ID", "TIPO_PERSONA_ID"),
																																																							new System.Data.Common.DataColumnMapping("GRUPO_2_ID", "GRUPO_2_ID"),
																																																							new System.Data.Common.DataColumnMapping("GRUPO_3_ID", "GRUPO_3_ID"),
																																																							new System.Data.Common.DataColumnMapping("PERSONA_NOMBRE", "PERSONA_NOMBRE"),
																																																							new System.Data.Common.DataColumnMapping("TURNO_ID", "TURNO_ID"),
																																																							new System.Data.Common.DataColumnMapping("PERSONA_EMAIL", "PERSONA_EMAIL"),
																																																							new System.Data.Common.DataColumnMapping("PERSONA_BORRADO", "PERSONA_BORRADO")})});
            this.DA_EmpleadoAltasBajas.UpdateCommand = this.oleDbUpdateCommand1;
            // 
            // dS_EmpleadoAltaBaja1
            // 
            this.dS_EmpleadoAltaBaja1.DataSetName = "DS_EmpleadoAltaBaja";
            this.dS_EmpleadoAltaBaja1.Locale = new System.Globalization.CultureInfo("es-MX");
            ((System.ComponentModel.ISupportInitialize)(this.dS_EmpleadoAltaBaja1)).EndInit();

        }
        #endregion

        private string AltaPersona_S(bool Alta)
        {
            int PersonasSeleccionadas = 0;

            try
            {
                if (UltraWebGrid1.Rows.Count > 0)
                {
                    for (int i = 0; i < UltraWebGrid1.Rows.Count; i++)
                    {

                        int Persona_ID = Convert.ToInt32(UltraWebGrid1.Rows[i].Cells[UltraWebGrid1.Columns.FromKey(CeC_Campos.CampoTE_Llave).Index].Value);
                        Persona_ID = CeC_Personas.ObtenPersonaID(Persona_ID);
                        string strNombrePersona = Convert.ToString(UltraWebGrid1.Rows[i].Cells[UltraWebGrid1.Columns.FromKey("PERSONA_NOMBRE").Index].Value);

                        if (Alta)
                        {
                            if (!CBListado.Checked)
                            {
                                if (UltraWebGrid1.Rows[i].Selected)
                                {

                                    CeC_BD.EjecutaComando("UPDATE EC_PERSONAS SET PERSONA_BORRADO = 0 WHERE PERSONA_ID  = " + Persona_ID);
                                    //Agregar ModuloLog***
                                    Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.EDICION, "Personas", Persona_ID, strNombrePersona, Sesion.SESION_ID);
                                    //*****				
                                    PersonasSeleccionadas++;
                                }
                            }
                            else
                            {
                                CeC_BD.EjecutaComando("UPDATE EC_PERSONAS SET PERSONA_BORRADO = 0 WHERE PERSONA_ID  = " + Persona_ID);
                                //Agregar ModuloLog***
                                Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.EDICION, "Personas", Persona_ID, strNombrePersona, Sesion.SESION_ID);
                                //*****
                                PersonasSeleccionadas++;
                            }

                        }
                        else
                        {
                            if (!CBListado.Checked)
                            {
                                if (UltraWebGrid1.Rows[i].Selected)
                                {

                                    CeC_BD.EjecutaComando("UPDATE EC_PERSONAS SET PERSONA_BORRADO = 1 WHERE PERSONA_ID  = " + Persona_ID);
                                    //Agregar ModuloLog***
                                    Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.BORRADO, "Personas", Persona_ID, strNombrePersona, Sesion.SESION_ID);
                                    //*****
                                    PersonasSeleccionadas++;
                                }
                            }
                            else
                            {

                                CeC_BD.EjecutaComando("UPDATE EC_PERSONAS SET PERSONA_BORRADO = 1 WHERE PERSONA_ID  = " + Persona_ID);
                                //Agregar ModuloLog***
                                Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.BORRADO, "Personas", Persona_ID, strNombrePersona, Sesion.SESION_ID);
                                //*****
                                PersonasSeleccionadas++;
                            }
                        }
                    }

                    UltraWebGrid1_InitializeDataSource(null, null);
                    UltraWebGrid1.DataBind();


                    return PersonasSeleccionadas.ToString(); ;

                    //Sesion.Redirige("WF_EmpleadosFil.aspx");
                }
            }
            catch (Exception ex)
            {

                return "Error : " + ex.Message;

            }
            if (PersonasSeleccionadas <= 0)
                return "Error : Debes de Seleccionar una Fila";
            else
            {

                return PersonasSeleccionadas.ToString();
            }
        }

        private void BajaPersonas_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            string ret = AltaPersona_S(false);

            if (ret.Contains("Error"))
                LError.Text = ret;
            else
                LCorrecto.Text = ret + " Modificaciones Realizadas Satisfactoriamente";
            //		Sesion.WF_EmpleadosFil_Qry = "";
            //		Sesion.Redirige("WF_EmpleadosFil.aspx");
            UltraWebGrid1.DataBind();
        }

        private void Button2_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            Sesion.WF_EmpleadosFil_Qry = "";
            Sesion.Redirige("WF_EmpleadosFil.aspx");
        }

        private void AltaPersonas_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            string ret = AltaPersona_S(true);
            if (ret.Contains("Error"))
                LError.Text = ret;
            else
                LCorrecto.Text = ret + " Modificaciones Realizadas Satisfactoriamente";

            //Sesion.WF_EmpleadosFil_Qry = "";
            //Sesion.Redirige("WF_EmpleadosFil.aspx");
        }

        protected void CheckBox1_CheckedChanged(object sender, System.EventArgs e)
        {
            UltraWebGrid1.DataBind();
        }
        protected void UltraWebGrid1_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
        {
            CeC_Grid.AplicaFormato(UltraWebGrid1, true, false, true, false);
        }
        protected void UltraWebGrid1_InitializeDataSource(object sender, Infragistics.WebUI.UltraWebGrid.UltraGridEventArgs e)
        {
            Sesion = CeC_Sesion.Nuevo(this);

            if (Sesion.WF_EmpleadosFil_Qry_Temp.Length > 0)
            {
                DataSet DS = CeC_Campos.ObtenDataSetTEGrid(PersonasCheckBox1.Checked, " AND PERSONA_ID IN (" + Sesion.WF_EmpleadosFil_Qry_Temp + ")");
                UltraWebGrid1.DataSource = DS;

            }


        }
    }
}
