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
    /// Descripción breve de WF_DiaPersona.
    /// </summary>
    public partial class WF_DiaPersona : System.Web.UI.Page
    {
        CeC_Sesion Sesion;
        DS_DiaPersona2 DS_DiaPersona = new DS_DiaPersona2();
        DS_DiaPersona2TableAdapters.EC_DIAPERSONA_ADAPTER Adaptador = new DS_DiaPersona2TableAdapters.EC_DIAPERSONA_ADAPTER();

        private void Habilitarcontroles()
        {
            BBuscarEmpleado.Visible = false;
            TBTracve.Visible = false;
            FechaInicial.Visible = false;
            LArea.Visible = false;
            LCC.Visible = false;
            LDepto.Visible = false;
            LEntrada.Visible = false;
            LHorario.Visible = false;
            LHorarioRet.Visible = false;
            LIncidencia.Visible = false;
            LJustifica.Visible = false;
            LMotivo.Visible = false;
            LMotivo.Visible = false;
            LNombre.Visible = false;
            Foto.Visible = false;
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Sesion = CeC_Sesion.Nuevo(this);
            Sesion.TituloPagina = "Dia de un empleado";
            Sesion.DescripcionPagina = "Ingrese el No. de empleado y a continuación use el botón Mostrar para ver los detalles del empleado";

            // Permisos****************************************
            if (!Sesion.TienePermisoOHijos(CEC_RESTRICCIONES.S0Vigilancia, true))
            {
                Habilitarcontroles();
                return;
            }
            //**************************************************

            if (!IsPostBack)
            {
                FechaInicial.Value = System.DateTime.Now;
                LimpiaCampos();
                //Agregar Módulo Log
                Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Día de un Empleado", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
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
            this.BBuscarEmpleado.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.BBuscarEmpleado_Click);
            this.FechaInicial.ValueChanged += new Infragistics.WebUI.WebSchedule.WebDateChooser.ValueChangedEventHandler(this.FechaInicial_ValueChanged);
        }
        #endregion

        private void LimpiaCampos()
        {
            LTracve.Text = "-";
            LNombre.Text = "-";
            LArea.Text = "-";
            LDepto.Text = "-";
            LHorario.Text = "-";
            LCC.Text = "-";
            LHorarioRet.Text = "-";
            LEntrada.Text = "-";
            LSalida.Text = "-";
            LJustifica.Text = "-";
            LPor.Text = "-";
            LMotivo.Text = "-";
            LIncidencia.Text = "-";
        }

        private void ActualizaDatos(int PersonaLinkID, DateTime Fecha)
        {
            try
            {
                Adaptador.Fill(DS_DiaPersona.EC_DIAPERSONA_TABLE, (decimal)PersonaLinkID, Fecha);
                LTracve.Text = DS_DiaPersona.EC_DIAPERSONA_TABLE[0][DS_DiaPersona.EC_DIAPERSONA_TABLE.PERSONA_LINK_IDColumn.Caption].ToString();
                LNombre.Text = DS_DiaPersona.EC_DIAPERSONA_TABLE[0][DS_DiaPersona.EC_DIAPERSONA_TABLE.NOMBREColumn.Caption].ToString();
                LArea.Text = DS_DiaPersona.EC_DIAPERSONA_TABLE[0][DS_DiaPersona.EC_DIAPERSONA_TABLE.AREAColumn.Caption].ToString();
                LDepto.Text = DS_DiaPersona.EC_DIAPERSONA_TABLE[0][DS_DiaPersona.EC_DIAPERSONA_TABLE.DEPARTAMENTOColumn.Caption].ToString();
                LCC.Text = DS_DiaPersona.EC_DIAPERSONA_TABLE[0][DS_DiaPersona.EC_DIAPERSONA_TABLE.CENTRO_COSTOSColumn.Caption].ToString();
                LHorarioRet.Text = Convert.ToDateTime(DS_DiaPersona.EC_DIAPERSONA_TABLE[0][DS_DiaPersona.EC_DIAPERSONA_TABLE.HORA_RETARDOColumn.Caption]).ToShortTimeString();
                LEntrada.Text = Convert.ToDateTime(DS_DiaPersona.EC_DIAPERSONA_TABLE[0][DS_DiaPersona.EC_DIAPERSONA_TABLE.HORA_ENTRADAColumn.Caption]).ToShortTimeString();
                LSalida.Text = Convert.ToDateTime(DS_DiaPersona.EC_DIAPERSONA_TABLE[0][DS_DiaPersona.EC_DIAPERSONA_TABLE.HORA_SALIDAColumn.Caption]).ToShortTimeString();
                LTipoturno.Text = DS_DiaPersona.EC_DIAPERSONA_TABLE[0][DS_DiaPersona.EC_DIAPERSONA_TABLE.TURNOColumn.Caption].ToString();
                LIncidencia.Text = DS_DiaPersona.EC_DIAPERSONA_TABLE[0][DS_DiaPersona.EC_DIAPERSONA_TABLE.INCIDENCIA_SISTEMAColumn.Caption].ToString();
                LJustifica.Text = DS_DiaPersona.EC_DIAPERSONA_TABLE[0][DS_DiaPersona.EC_DIAPERSONA_TABLE.INCIDENCIA_NOMBREColumn.Caption].ToString();
                LMotivo.Text = DS_DiaPersona.EC_DIAPERSONA_TABLE[0][DS_DiaPersona.EC_DIAPERSONA_TABLE.INCIDENCIA_COMENTARIOColumn.Caption].ToString();
                Sesion.WF_Empleados_PERSONA_LINK_ID = Convert.ToInt32(DS_DiaPersona.EC_DIAPERSONA_TABLE[0][DS_DiaPersona.EC_DIAPERSONA_TABLE.PERSONA_LINK_IDColumn.Caption]);
                //Foto.ImageUrl = ("WF_Personas_ImaS.aspx");
            }
            catch (Exception ex)
            {
                LimpiaCampos();
            }
        }

        protected void BBuscarEmpleado_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            ActualizaDatos(Convert.ToInt32(TBTracve.Value), Convert.ToDateTime(FechaInicial.Value));
        }

        protected void FechaInicial_ValueChanged(object sender, Infragistics.WebUI.WebSchedule.WebDateChooser.WebDateChooserEventArgs e)
        {
            ActualizaDatos(Convert.ToInt32(TBTracve.Value), Convert.ToDateTime(FechaInicial.Value));
        }
    }
}