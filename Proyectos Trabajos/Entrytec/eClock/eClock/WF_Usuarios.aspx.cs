
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


namespace eClock
{
    /// <summary>
    /// Descripción breve de WF_Usuarios.
    /// </summary>
    public partial class WF_Usuarios : System.Web.UI.Page
    {
        DS_Usuarios2TableAdapters.EC_USUARIOSTableAdapter DA_Usuarios;
        protected DS_Usuarios2 dS_Usuarios2;
        //declarar siempre en las clases WF del sistema
        CeC_Sesion Sesion;


        private void ControlVisible()
        {
            Grid.Visible = false;
            UsuariosCheckBox1.Visible = false;
            WIBtn_Nuevo.Visible = false;
            WIBtn_Borrar.Visible = false;
        }

        private void Habilitarcontroles()
        {
            if (!Sesion.TienePermiso(CEC_RESTRICCIONES.S0Usuarios0Usuarios_Sistema0Nuevo) && !Sesion.TienePermiso(CEC_RESTRICCIONES.S0Usuarios0Usuarios_Sistema0Editar) && !Sesion.TienePermiso(CEC_RESTRICCIONES.S0Usuarios0Usuarios_Sistema0Borrar))
            {
                Grid.Visible = false;
                UsuariosCheckBox1.Visible = false;
            }

            if (!Sesion.TienePermiso(CEC_RESTRICCIONES.S0Usuarios0Usuarios_Sistema0Nuevo))
                WIBtn_Nuevo.Visible = false;

            if (!Sesion.TienePermiso(CEC_RESTRICCIONES.S0Usuarios0Usuarios_Sistema0Editar))
                WIBtn_Editar.Visible = false;

            if (!Sesion.TienePermiso(CEC_RESTRICCIONES.S0Usuarios0Usuarios_Sistema0Borrar))
                WIBtn_Borrar.Visible = false;
        }

        protected void Page_Load(object sender, System.EventArgs e)			//ejecutar siempre para cargar variables de sesión
        {
            //Sesion = CeC_Sesion.Nuevo(this);
            //Sesion.Redirige("WF_Tabla.aspx?Parametros=EC_USUARIOS");
            Grid.DisplayLayout.CellClickActionDefault = Infragistics.WebUI.UltraWebGrid.CellClickAction.RowSelect;
            Sesion = CeC_Sesion.Nuevo(this);
            if (!Sesion.Configura.UsaUsuarios)
                Sesion.Redirige("WF_Info.aspx");
            Sesion.TituloPagina = "Usuarios";
            Sesion.DescripcionPagina = "Seleccione un usuario para editarlo o borrarlo; o cree un nuevo usuario";

            if (!Sesion.TienePermisoOHijos(CEC_RESTRICCIONES.S0Usuarios0Usuarios_Sistema, true))
            {
                Habilitarcontroles();
                //Agregar ModuloLog***
                Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Usuarios", 0, "Cosulta Todos los Usuarios", Sesion.SESION_ID);
                //*****
                return;
            }
            //**************************************************

            if (!IsPostBack)
            {
                ActualizarMail();
                Grid.DataBind();
            }
        }

        private void ActualizarMail()
        {
            for (int i = 0; i < dS_Usuarios2.EC_USUARIOS.Rows.Count; i++)
            {
                /*	DS_Usuarios.EC_USUARIOSRow FilaUsuario = (DS_Usuarios.EC_USUARIOSRow)dS_Usuarios1.EC_USUARIOS.Rows[i];
                    string Query= "Select Persona_Email from EC_PERSONAS  where Persona_link_id  = "+FilaUsuario.USUARIO_USUARIO+"";
                    string Email = CeC_BD.EjecutaEscalarString(Query);
                    CeC_BD.EjecutaComando("Update EC_USUARIOS set USUARIO_DEScrpcion = "+Email+" , USUARIO_Email = "+Email+" where USUARIO_USUARIO  = "+FilaUsuario.USUARIO_USUARIO);
                    FilaUsuario.USUARIO_DESCRIPCION = Email; 
                */
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

            this.DA_Usuarios = new DS_Usuarios2TableAdapters.EC_USUARIOSTableAdapter();
            this.dS_Usuarios2 = new DS_Usuarios2();

            ((System.ComponentModel.ISupportInitialize)(this.dS_Usuarios2)).BeginInit();
            this.WIBtn_Borrar.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.BBorrarUsuarios_Click);
            //this.WIBtn_Nuevo.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.wib);
            //this.WIBtn_Editar.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.WIBtn_Editar_Click);
            ((System.ComponentModel.ISupportInitialize)(this.dS_Usuarios2)).EndInit();

        }
        #endregion

        public void WIBtn_Editar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            int Numero_Reg = Convert.ToInt32(Grid.Rows.Count);

            for (int i = 0; i < Numero_Reg; i++)
            {
                if (Grid.Rows[i].Selected)
                {
                    //Sesion.USUARIO_ID = Convert.ToInt32(UltraWebGrid1.Rows[i].Cells[0].Value);
                    int Usua_ID = Convert.ToInt32(Grid.Rows[i].Cells[0].Value);
                    Sesion.WF_Usuarios_USUARIO_ID = Usua_ID;
                    Sesion.Redirige("WF_UsuariosEd.aspx");
                    //CeC_Sesion.GuardaIntSesion(this,"USUARIO_ID",Usua_ID);
                    //CeC_Sesion.Redirige(this,"WFEUsuarios.aspx");
                    return;
                }
            }
        }

        public void BBorrarUsuarios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            Lbl_Correcto.Text = "";
            Lbl_Error.Text = "";
            int Numero_Reg = Convert.ToInt32(Grid.Rows.Count);
            int Modificados = 0;

            for (int i = 0; i < Numero_Reg; i++)
            {
                if (Grid.Rows[i].Selected)
                {
                    try
                    {
                        int Usua_ID = Convert.ToInt32(Grid.Rows[i].Cells[0].Value);
                        string strUsua = Convert.ToString(Grid.Rows[i].Cells[3].Value);
                        //						Borrar_Usuario.Parameters[0].Value = Usua_ID;
                        int Modificados2 = DA_Usuarios.BorraUsuario(1, Usua_ID);

                        if (Modificados2 > 0)
                            Modificados++;

                        //Sesion.Redirige("WF_Usuarios.aspx");

                        //Agregar ModuloLog***
                        Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.BORRADO, "Usuarios", Usua_ID, strUsua, Sesion.SESION_ID);
                        //*****		

                        dS_Usuarios2.EC_USUARIOS.Clear();
                        DA_Usuarios.Fill(dS_Usuarios2.EC_USUARIOS, Convert.ToInt32(UsuariosCheckBox1.Checked));

                        Grid.DataBind();
                        Lbl_Correcto.Text = Modificados.ToString() + " registros modificados";
                        return;

                    }
                    catch (Exception ex)
                    {
                        Lbl_Error.Text = "Error " + ex.Message;
                        return;
                    }
                }
            }
            Lbl_Error.Text = "Debes de seleccionar una fila";
        }

        protected void UsuariosCheckBox1_CheckedChanged(object sender, System.EventArgs e)
        {
            Grid.DataBind();
        }

        protected void UltraWebGrid1_InitializeDataSource(object sender, Infragistics.WebUI.UltraWebGrid.UltraGridEventArgs e)
        {
            try
            {
                Sesion = CeC_Sesion.Nuevo(this);
                //DA_Usuarios.FillBySuscripcionID(dS_Usuarios2.EC_USUARIOS, Convert.ToInt32(UsuariosCheckBox1.Checked), Sesion.SuscripcionParametro);
                DataSet DS_Usuarios = CeC_Usuarios.ObtenUsuariosDS(Sesion.SuscripcionParametro, CeC.Convierte2Int(UsuariosCheckBox1.Checked));
                Grid.DataSource = DS_Usuarios.Tables[0];
                Grid.DataMember = DS_Usuarios.Tables[0].TableName;
                Grid.DataKeyField = "USUARIO_ID";
                //Sesion = CeC_Sesion.Nuevo(this);
                //DA_Usuarios.FillBySuscripcionID(dS_Usuarios2.EC_USUARIOS, Convert.ToInt32(UsuariosCheckBox1.Checked), Sesion.SuscripcionParametro);
                //Grid.DataSource = dS_Usuarios2.EC_USUARIOS;
            }
            catch (Exception ex) 
            {
                CIsLog2.AgregaError("WF_Usuarios.UltraWebGrid1_InitializeDataSource", ex);
            }
        }

        protected void UltraWebGrid1_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
        {
            CeC_Grid.AplicaFormato(Grid, false, false, false, false);
        }
        protected void btImprimir_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            GridExporter.Format = Infragistics.Documents.Reports.Report.FileFormat.PDF;
            GridExporter.TargetPaperOrientation = Infragistics.Documents.Reports.Report.PageOrientation.Landscape;
            GridExporter.DownloadName = "ExportacionUsuarios.pdf";
            GridExporter.Export(Grid);
        }
        protected void GridExporter_BeginExport(object sender, Infragistics.WebUI.UltraWebGrid.DocumentExport.DocumentExportEventArgs e)
        {
            CeC_Reportes.AplicaFormatoReporte(e, "Reporte de Usuarios", "./imagenes/usuarios64.png", Sesion);
        }
        protected void WIBtn_Nuevo_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            int Usua_ID = -99999;
            Sesion.WF_Usuarios_USUARIO_ID = Usua_ID;
            Sesion.Redirige("WF_UsuariosEd.aspx?SuscripcionID=" + Sesion.SuscripcionParametro);
        }
}
}