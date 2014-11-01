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
    /// Descripción breve de WF_Personas_Diario.
    /// </summary>
    public partial class WF_Personas_Diario : System.Web.UI.Page
    {

        //declarar siempre en las clases WF del sistema
        CeC_Sesion Sesion;

        private void Habilitarcontroles()
        {
            WC_Mes1.DiasVisibles(false);
            WC_Mes2.DiasVisibles(false);
            WC_Mes3.DiasVisibles(false);
            WC_Mes4.DiasVisibles(false);
            WC_Mes5.DiasVisibles(false);
            WC_Mes6.DiasVisibles(false);
            WC_Mes7.DiasVisibles(false);
            WC_Mes8.DiasVisibles(false);
            WC_Mes9.DiasVisibles(false);
            WC_Mes10.DiasVisibles(false);
            WC_Mes11.DiasVisibles(false);
            WC_Mes12.DiasVisibles(false);
            Webpanel1.Visible = WebPanel2.Visible = false;
            LBActual.Visible = LBAnterior.Visible = LBSiguiente.Visible = LBTemp.Visible = false;
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            //ejecutar siempre para cargar variables de sesión
            Sesion = CeC_Sesion.Nuevo(this);
            Sesion.TituloPagina = "Asistencias";
            Sesion.DescripcionPagina = "Asistencias, Retardos y Justificaciones por Año";

            // Permisos****************************************
            if (!Sesion.TienePermisoOHijos(CEC_RESTRICCIONES.S0Empleados0Consultar_Asistencia0Editar0Sin_Justificar, true))
            {
                Habilitarcontroles();
                return;
            }
            //**************************************************

            if (!IsPostBack)
            {

                Sesion.TituloPagina = "Vista anual de asistencia de personal";
                Sesion.DescripcionPagina = "De click sobre el día que desee editar";

                //Agregar ModuloLog***
                Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Persona Diario", Sesion.WF_Empleados_PERSONA_ID, "Consulta de Calendario", Sesion.SESION_ID);
                //*****				
                //Sesion.WF_Empleados_PERSONA_ID = -1;

                
                InicializaAno(Sesion.WF_Personas_Diario_AnoTemp);
            }
        }

        protected void InicializaAno(DateTime Ano)
        {
            try
            {
                WC_Mes13.MuestraTDias();



                int Persona_ID = Sesion.eClock_Persona_ID;
                LNombrePersona.Text = CeC_BD.ObtenPersonaNombre(Persona_ID);
                CeC_Asistencias.GeneraPrevioPersonaDiario(Persona_ID);
                //DateTime Ano = DateTime.Now;
                WC_Mes1.Inicializa(Persona_ID, new DateTime(Ano.Year, 1, 1));
                WC_Mes2.Inicializa(Persona_ID, new DateTime(Ano.Year, 2, 1));
                WC_Mes3.Inicializa(Persona_ID, new DateTime(Ano.Year, 3, 1));
                WC_Mes4.Inicializa(Persona_ID, new DateTime(Ano.Year, 4, 1));
                WC_Mes5.Inicializa(Persona_ID, new DateTime(Ano.Year, 5, 1));
                WC_Mes6.Inicializa(Persona_ID, new DateTime(Ano.Year, 6, 1));
                WC_Mes7.Inicializa(Persona_ID, new DateTime(Ano.Year, 7, 1));
                WC_Mes8.Inicializa(Persona_ID, new DateTime(Ano.Year, 8, 1));
                WC_Mes9.Inicializa(Persona_ID, new DateTime(Ano.Year, 9, 1));
                WC_Mes10.Inicializa(Persona_ID, new DateTime(Ano.Year, 10, 1));
                WC_Mes11.Inicializa(Persona_ID, new DateTime(Ano.Year, 11, 1));
                WC_Mes12.Inicializa(Persona_ID, new DateTime(Ano.Year, 12, 1));
                LBTemp.Text = Sesion.WF_Personas_Diario_AnoTemp.Year.ToString();
            }
            catch (Exception ex)
            {
                CIsLog2.AgregaError("WF_Personas_Diario.InicializaAno", ex);
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

        }
        #endregion


        protected void Grid_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
        {
            CeC_Grid.AplicaFormato(Grid);
        }

        protected void Grid2_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
        {
            CeC_Grid.AplicaFormato(Grid2);
        }

        protected void Grid_Load(object sender, EventArgs e)
        {
            CeC_Grid.AplicaFormato(Grid, false, false, false, false);
            Grid.Columns[0].Width = Unit.Pixel(300);
        }

        protected void Grid2_Load(object sender, EventArgs e)
        {
            CeC_Grid.AplicaFormato(Grid2, false, false, false, false);
        }

        protected void LBAnterior_Click(object sender, EventArgs e)
        {
            Sesion.WF_Personas_Diario_AnoTemp = Sesion.WF_Personas_Diario_AnoTemp.AddYears(-1);
            InicializaAno(Sesion.WF_Personas_Diario_AnoTemp);
        }
        protected void LBSiguiente_Click(object sender, EventArgs e)
        {
            Sesion.WF_Personas_Diario_AnoTemp = Sesion.WF_Personas_Diario_AnoTemp.AddYears(1);
            InicializaAno(Sesion.WF_Personas_Diario_AnoTemp);
        }

        protected void LBActual_Click(object sender, EventArgs e)
        {
            Sesion.WF_Personas_Diario_AnoTemp = DateTime.Now;
            InicializaAno(Sesion.WF_Personas_Diario_AnoTemp);
        }
    }
}
