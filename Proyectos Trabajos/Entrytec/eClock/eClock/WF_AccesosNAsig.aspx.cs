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
    /// Descripción breve de WF_AccesosNAsig.
    /// </summary>
    public partial class WF_AccesosNAsig : System.Web.UI.Page
    {
        protected System.Data.OleDb.OleDbConnection Conexion;
        protected System.Data.OleDb.OleDbDataAdapter DA_AccesosNAsig;
        protected System.Data.OleDb.OleDbCommand oleDbSelectCommand1;
        protected DS_AccesosNAsig dS_AccesosNAsig1;

        CeC_Sesion Sesion;
        private void ActualizaGrid()
        {
            dS_AccesosNAsig1.Clear();
            DA_AccesosNAsig.Fill(dS_AccesosNAsig1);
            Grid.DataBind();
        }

        private void LimpiaMsg()
        {
            LError.Text = "";
            LCorrecto.Text = "";
        }

        private void Habilitarcontroles()
        {
                Grid.Visible = false;
                Webimagebutton2.Visible = false;
                Webimagebutton3.Visible = false;
                BDeshacerCambios.Visible = false;
        }

        private void Page_Load(object sender, System.EventArgs e)
        {
            Sesion = CeC_Sesion.Nuevo(this);
            Sesion.TituloPagina = "Accesos no asignados";
            Sesion.DescripcionPagina = "Seleccione los accesos que no fueron asignado a un empleado y presione el 'Borrar Seleccionados' para borrar los accesos";

            // Permisos****************************************
            if (!Sesion.TienePermisoOHijos(CEC_RESTRICCIONES.S0AccesosNoAsignados, true))
            {
                Habilitarcontroles();
                return;
            }
            //**************************************************

            if (!IsPostBack)
            {
                ActualizaGrid();

                //Agrega Módulo Log
                Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Accesos No Asignados", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
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
            this.Conexion = new System.Data.OleDb.OleDbConnection();
            this.DA_AccesosNAsig = new System.Data.OleDb.OleDbDataAdapter();
            this.oleDbSelectCommand1 = new System.Data.OleDb.OleDbCommand();
            this.dS_AccesosNAsig1 = new DS_AccesosNAsig();
            ((System.ComponentModel.ISupportInitialize)(this.dS_AccesosNAsig1)).BeginInit();
            this.Webimagebutton2.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.Webimagebutton2_Click);
            this.Webimagebutton3.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.Webimagebutton3_Click);
            this.BDeshacerCambios.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.BDeshacerCambios_Click);
            // 
            // Conexion
            // 
            this.Conexion.ConnectionString = ((string)(configurationAppSettings.GetValue("gBDatos.ConnectionString", typeof(string))));
            // 
            // DA_AccesosNAsig
            // 
            this.DA_AccesosNAsig.SelectCommand = this.oleDbSelectCommand1;
            this.DA_AccesosNAsig.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																									  new System.Data.Common.DataTableMapping("Table", "EC_PERSONAS", new System.Data.Common.DataColumnMapping[] {
																																																					  new System.Data.Common.DataColumnMapping("QUITAR", "QUITAR"),
																																																					  new System.Data.Common.DataColumnMapping("PERSONA_ID", "PERSONA_ID"),
																																																					  new System.Data.Common.DataColumnMapping("PERSONA_LINK_ID", "PERSONA_LINK_ID"),
																																																					  new System.Data.Common.DataColumnMapping("PERSONA_NOMBRE", "PERSONA_NOMBRE"),
																																																					  new System.Data.Common.DataColumnMapping("PERSONA_S_HUELLA_FECHA", "PERSONA_S_HUELLA_FECHA"),
																																																					  new System.Data.Common.DataColumnMapping("PERSONA_EMAIL", "PERSONA_EMAIL")})});
            // 
            // oleDbSelectCommand1
            // 
            this.oleDbSelectCommand1.CommandText = @"SELECT 0 AS QUITAR, EC_TERMINALES_DEXTRAS.TERMINALES_DEXTRAS_ID, EC_TERMINALES_DEXTRAS.TERMINALES_DEXTRAS_TEXTO1, EC_TERMINALES_DEXTRAS.TERMINALES_DEXTRAS_TEXTO2, EC_TERMINALES.TERMINAL_NOMBRE FROM EC_TERMINALES_DEXTRAS, EC_TERMINALES WHERE EC_TERMINALES_DEXTRAS.TERMINAL_ID = EC_TERMINALES.TERMINAL_ID AND (eC_TERMINALES_DEXTRAS.TIPO_TERM_DEXTRAS_ID = 1) ORDER BY EC_TERMINALES_DEXTRAS.TERMINALES_DEXTRAS_TEXTO1, EC_TERMINALES_DEXTRAS.TERMINALES_DEXTRAS_TEXTO2";
            this.oleDbSelectCommand1.Connection = this.Conexion;
            // 
            // dS_AccesosNAsig1
            // 
            this.dS_AccesosNAsig1.DataSetName = "DS_AccesosNAsig";
            this.dS_AccesosNAsig1.Locale = new System.Globalization.CultureInfo("es-MX");
            this.Load += new System.EventHandler(this.Page_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dS_AccesosNAsig1)).EndInit();

        }
        #endregion

        void CheckTodos(bool Check)
        {
            for (int Cont = 0; Cont < Grid.Rows.Count; Cont++)
            {
                if (Check)
                    Grid.Rows[Cont].Cells[0].Value = 1;
                else
                    Grid.Rows[Cont].Cells[0].Value = 0;
            }
        }
        private void Webimagebutton2_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            CheckTodos(false);
        }

        private void Webimagebutton3_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            CheckTodos(true);
        }

        private void BDeshacerCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            int Borrados = 0;
            int NBorrados = 0;
            string TBorrados = "";
            string TNBorrados = "";
            LimpiaMsg();

            for (int Cont = 0; Cont < Grid.Rows.Count; Cont++)
            {
                if (Convert.ToInt32(Grid.Rows[Cont].Cells[0].Value) != 0)
                {
                    string ID = Convert.ToString(Grid.Rows[Cont].Cells[1].Value);
                    string Completo = "No. Empleado = " + Convert.ToString(Grid.Rows[Cont].Cells[2].Value) + " Detalles = " + Convert.ToString(Grid.Rows[Cont].Cells[3].Value);
                    if (CeC_BD.EjecutaComando("DELETE EC_TERMINALES_DEXTRAS where TERMINALES_DEXTRAS_ID = " + ID + "") <= 0)
                    {
                        TNBorrados += Completo + "\n";
                        NBorrados++;
                    }
                    else
                    {
                        TBorrados += Completo + "\n";
                        Borrados++;
                    }
                }
            }
            if (NBorrados > 0)
            {
                LError.Text = "No se pudieron quitar los accesos empleados de la lista: \n" + TNBorrados;
            }
            if (Borrados > 0)
            {
                LCorrecto.Text = "\nSe quitaron los siguientes accesos de la lista \n" + TBorrados;
            }
            ActualizaGrid();

        }

        protected void Grid_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
        {
            CeC_Grid.AplicaFormato(Grid);
        }
        protected void Btn_Recalcular_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            LCorrecto.Text = "Se calcularon " + CeC_Accesos.ProcesaAccesosViejos() + " Accesos";
        }
}
}