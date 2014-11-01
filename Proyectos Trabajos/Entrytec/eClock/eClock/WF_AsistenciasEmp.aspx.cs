using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
//using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Infragistics.UltraGauge.Resources;
using Infragistics.UltraChart.Shared.Styles;
using Infragistics.UltraChart.Resources.Appearance;
using Infragistics.UltraChart.Core.Layers;
using Infragistics.UltraChart.Data.Series;
//using System.Drawing;
using Infragistics.Web.UI.DisplayControls;
using Infragistics.WebUI.UltraWebGrid.ExcelExport;
using Reports = Infragistics.Documents.Reports;

using ReportText = Infragistics.Documents.Reports.Report.Text;
//using Reports.Graphics;
using Infragistics.Documents.Reports.Report.Table;
using Infragistics.Documents.Reports.Report.Text;
using Infragistics.Documents.Reports.Graphics;
using Infragistics.WebUI.UltraWebGrid;
using System.Globalization;

public partial class WF_AsistenciasEmp : System.Web.UI.Page
{
    int Persona_Link_ID = -1;
    int Persona_ID = -1;
    string Agrupacion = "";
    CeC_Sesion Sesion = null;
    bool Ordenar = false;
    bool ImprimirFirma = false;
    int Persona_Link_ID_ANT = 0;
    string ExportaNombre = "";
    string LeyendaFirma = "";
    int EmpleadosXHoja = 0;
    int NoEmpleado = 0;
    //string AuxTag = "";
    string[] Terminales;

    /// <summary>
    /// Agregar los nuevos elementos al final de la lista para conservar
    /// compatibilidad con versiones anteriores de los reportes favoritos
    /// </summary>
    string[] VariablesSesion = { "AsistenciaMostrar", "AS_PREDETERMINADO", "AS_7_DIAS", "AS_31_DIAS", "AS_ENTRADA_SALIDA",
                                  "AS_COMIDA", "AS_HORAS_EXTRAS", "AS_TOTALES", "AS_INCIDENCIA", "AS_TURNO", "AS_SOLO_FALTAS","AS_SOLO_RETARDOS",
                                  "AS_AGRUPACION", "AS_G_AGRUPACION", "AS_G_EMPLEADO", "AsistenciaGraficar", "AsistenciaMostrarAccesos","Terminal_ES",
                                  "FIRMA","AS_EMPLEADO", "AsistenciaMostrarES", "AS_T_AGRUPACION", "AS_T_EMPLEADO","AS_T_TOTALES", "AS_T_HISTORIAL", "AS_T_SALDO", "AS_T_FALTAS"};
    protected void Page_Load(object sender, EventArgs e)
    {

        CargaVariables();
        if (!IsPostBack)
        {
            this.Grid.Width = Unit.Percentage(100);
            this.Grid.Height = Unit.Percentage(100);
            this.Grid.DisplayLayout.FrameStyle.CustomRules = "table-layout:auto";
            // Carga las variables de configuración
            CeC_Config Config = new CeC_Config(Sesion.USUARIO_ID);
            // Determina si se cargaran las Faltas
            if (Sesion.AsistenciaEspecifica == "FALTAS")
            {
                sCargaConsulta(this, "@" + Config.AsistenciaSoloFaltas);
                Sesion.AsistenciaEspecifica = "";
            }
            // Determina si se cargaran los Retardos
            if (Sesion.AsistenciaEspecifica == "RETARDOS")
            {
                sCargaConsulta(this, "@" + Config.AsistenciaSoloRetardos);
                Sesion.AsistenciaEspecifica = "";
            }
            // Se recomienda ejecutar una sola vez para mostrar todos los usuarios.
            // Si no se desactiva la variable GeneraPrevioPersonasDiario, esto generara un retraso en las pantallas de Asistencia
            if (CeC_Config.GeneraPrevioPersonasDiario)
                CagaPrevioPersonasDiario(Sesion.SUSCRIPCION_ID, Sesion);
            // Carga los Reportes guardados como Favoritos
            CargaFavoritos();
            // Carga las variables de Sesion para el Menu
            CargaSesion();
            // Carga los Turnos
            CargaTurnos();
            // Carga el Menu de Terminales
            CargaAgrupacionTerminales();
            // Carga los tipos de Incidencias Predeterminadas
            CargaTiposIncidenciasSistema();
            // Carga los tipos de Incidencias 
            CargaTiposIncidencias();
            // Determinamos si se van a mostrar los datos del Empleado o de la Agrupacion
            if (Sesion.Parametros == "AGRUPACION")
            {
                LblNoEmpleadoSL.Visible = LblNoEmp.Visible = false;
                LblNombreSL.Visible = LblNombre.Visible = false;
                LblTurnoSL.Visible = LblTurno.Visible = false;
                ImgEmpleado.Visible = false;
                LblAgrupacion.Text = Agrupacion;
            }
            else
            {
                LblNoEmp.Text = Persona_Link_ID.ToString();
                LblNombre.Text = CeC_BD.ObtenPersonaNombre(Persona_ID);
                LblAgrupacion.Text = CeC_Campos.ObtenValorCampo(Persona_ID, "AGRUPACION_NOMBRE");
                int TurnoID = CeC.Convierte2Int(CeC_Campos.ObtenValorCampo(Persona_ID, "TURNO_ID"), 0);
                // Si el empleado no tiene asigando turno se muestra un mensaje
                if (0 == TurnoID)
                {
                    LblTurno.ForeColor = System.Drawing.Color.Red;
                    LblTurno.Text = "No se ha asignado Turno";
                }
                else
                    LblTurno.Text = CeC_Turnos.ObtenTurnoNombre(TurnoID);
                ImgEmpleado.ImageUrl = "WF_Personas_ImaS.aspx?P=" + Persona_ID;
            }
            FechaInicial.Value = Sesion.AsistenciaFechaInicio;
            FechaFinal.Value = Sesion.AsistenciaFechaFin;
            ValidaPerfilesMenu();
        }
        /*        LinearGauge gauge = this.GauAsis.Gauges[0] as LinearGauge;
                gauge.Scales[0].Markers[0].Value = 10;*/
        //if (!IsPostBack)
        //{
        //    CeC_Asistencias.GeneraPrevioPersonaDiario(Persona_ID);
        //}
        if (IsPostBack && sender != null)
        {
            //            return;
            if (Request.Params.Get("__EVENTTARGET") == "MenuMenu")
            {
                string Tag = Request.Params.Get("ValorMenuTag");
                string TagPadre = Request.Params.Get("ValorMenuTagPadre");
                if (TagPadre == "AsistenciaMostrarAccesos")
                    ObtenAccesosES_Terminal(Tag);
                if (TagPadre == "TURNOS")
                    AsignaHorario(Tag);
                if (TagPadre == "TURNOSPRED")
                    AsignaHorarioPred(Tag);
                if (TagPadre == "JUSTIFICACIONES")
                    AsignaJustificacion(Tag);
                ActualizaDatos(true);
            }
            if (Request.Params.Get("__EVENTTARGET") == "Tbx_NoDias")
            {
                FechaFinal.Value = CeC.Convierte2DateTime(FechaInicial.Value).AddDays(CeC.Convierte2Int(Request.Params.Get("__EVENTARGUMENT")) - 1);
                ActualizaDatos(true);
            }

            if (Request.Params.Get("__EVENTTARGET") == "BtnNuevoFavAceptar")
                NuevoFav();
            if (Request.Params.Get("__EVENTTARGET") == "BtnCompartirFavAceptar")
            {
                //                string Tag = Request.Params.Get("ValorMenuTag");
                //                string TagPadre = Request.Params.Get("ValorMenuTagPadre");
                Comparte(Request.Params.Get("ValorMenuTagPadre"));
            }

        }
        //ActualizaDatos(true);
    }
    /// <summary>
    /// Carga datos predeterminados para que aparezcan todas las personas en los reportes de Asistencia aún cuando no tengan registros las personas.
    /// </summary>
    /// <param name="Suscripcion_ID">Suscripcion</param>
    /// <param name="Sesion">Variable de Sesion</param>
    private void CagaPrevioPersonasDiario(int Suscripcion_ID, CeC_Sesion Sesion)
    {
        DataSet DS = CeC_Personas.ObtenPersonasIDS_DS(Suscripcion_ID, Sesion);
        foreach (DataRow DR in DS.Tables[0].Rows)
        {
            int PersonaID = Convert.ToInt32(DR["PERSONA_ID"]);
            CeC_Asistencias.GeneraPrevioPersonaDiario(PersonaID);
        }
    }
    /// <summary>
    /// Guarda las consultas en Favoritos
    /// </summary>
    /// <param name="Nombre">Nombre de la consulta</param>
    /// <returns>True si se guardo la consulta. Falso en otro caso</returns>
    public bool GuardaConsulta(string Nombre)
    {
        string Consulta = Nombre + "@";
        foreach (string Parametro in VariablesSesion)
        {
            bool Activo = CeC_Sesion.LeeBoolSesion(this, Parametro, false);
            if (Activo)
                Consulta += "1";
            else
                Consulta += "0";
        }
        // Agrega la consulta a Favoritos
        CeC_Asistencias.AgregaFavorito(Sesion.USUARIO_ID, Consulta);
        return true;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Cadena"></param>
    /// <param name="Pos"></param>
    /// <returns></returns>
    public bool ObtenValPos(string Cadena, int Pos)
    {
        if (Pos >= Cadena.Length)
            return false;
        if (Cadena[Pos] == '1')
            return true;
        return false;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Pagina"></param>
    /// <param name="Tag"></param>
    public void sCargaConsulta(object Pagina, string Tag)
    {
        string[] Elementos = Tag.Split(new char[] { '@' });
        int Cont = 0;
        foreach (string Parametro in VariablesSesion)
        {
            CeC_Sesion.GuardaBoolSesion(Pagina, Parametro, ObtenValPos(Elementos[1], Cont));
            Cont++;
        }
    }
    /// <summary>
    /// Comparte el reporte Favorito
    /// </summary>
    /// <param name="Favorito">Nombre del Reporte</param>
    public void Comparte(string Favorito)
    {
        if (Favorito != "")
        {
            int ID = CeC_Usuarios.ObtenUsuarioID(Tbx_NombreUsuario.Text);
            if (ID > 0)
                CeC_Asistencias.AgregaFavorito(ID, Favorito);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Tag"></param>
    public void CargaConsulta(string Tag)
    {
        sCargaConsulta(this, Tag);
        CargaSesion();
        ActualizaDatos(true);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Tag"></param>
    /// <returns></returns>
    protected Infragistics.WebUI.UltraWebNavigator.Item MenuElmento(string Tag)
    {
        return Menu.Find(Tag);
    }
    /// <summary>
    /// Verifica si el elemento esta seleccionado
    /// </summary>
    /// <param name="Tag">Nombre de la etiqueta</param>
    /// <returns>Verdadero si esta checado. Falso en otro caso</returns>
    protected bool EstaChecado(string Tag)
    {
        Infragistics.WebUI.UltraWebNavigator.Item Elemento = Menu.Find(Tag);
        return Elemento.Checked;
    }
    /// <summary>
    /// 
    /// </summary>
    protected void GuardaAs()
    {
        Sesion.AS_ENTRADA_SALIDA = MenuElmento("AS_ENTRADA_SALIDA").Checked;
        Sesion.AS_HORAS_EXTRAS = MenuElmento("AS_HORAS_EXTRAS").Checked;
        Sesion.AS_COMIDA = MenuElmento("AS_COMIDA").Checked;
        Sesion.AS_TOTALES = MenuElmento("AS_TOTALES").Checked;
        Sesion.AS_INCIDENCIA = MenuElmento("AS_INCIDENCIA").Checked;
        Sesion.AS_TURNO = MenuElmento("AS_TURNO").Checked;
        Sesion.AS_SOLO_FALTAS = MenuElmento("AS_SOLO_FALTAS").Checked;
        Sesion.AS_SOLO_RETARDOS = MenuElmento("AS_SOLO_RETARDOS").Checked;
        Sesion.AS_AGRUPACION = MenuElmento("AS_AGRUPACION").Checked;
        Sesion.AS_EMPLEADO = MenuElmento("AS_EMPLEADO").Checked;
        Sesion.AS_G_AGRUPACION = MenuElmento("AS_G_AGRUPACION").Checked;
        Sesion.AS_G_EMPLEADO = MenuElmento("AS_G_EMPLEADO").Checked;
        Sesion.AS_FIRMA = MenuElmento("FIRMA").Checked;
        Sesion.AS_T_AGRUPACION = MenuElmento("AS_T_AGRUPACION").Checked;
        Sesion.AS_T_EMPLEADO = MenuElmento("AS_T_EMPLEADO").Checked;
        Sesion.AS_T_TOTALES = MenuElmento("AS_T_TOTALES").Checked;
        Sesion.AS_T_HISTORIAL = MenuElmento("AS_T_HISTORIAL").Checked;
        Sesion.AS_T_SALDO = MenuElmento("AS_T_SALDO").Checked;
        Sesion.AS_T_FALTAS = MenuElmento("AS_T_FALTAS").Checked;
    }
    /// <summary>
    /// Actualiza los datos en el Grid
    /// </summary>
    protected void ActualizaDatos()
    {
        ActualizaDatos(false);
    }
    /// <summary>
    /// Actualiza los datos que se mostraran en el Grid
    /// </summary>
    /// <param name="LimpiarGrid">Si es True se limpian los datos en el Grid</param>
    protected void ActualizaDatos(bool LimpiarGrid)
    {
        try
        {
            // Verificamos las variables de Sesion
            if (Sesion == null)
                return;
            if (Sesion.SESION_ID <= 0)
                return;
            // Fecha Inicial y Fecha Final del periodo seleccionado
            DateTime DTFechaInicial = Convert.ToDateTime(FechaInicial.Value).Date;
            DateTime DTFechaFinal = Convert.ToDateTime(FechaFinal.Value).Date;
            // Guardamos como variables de Sesion las Fechas
            Sesion.AsistenciaFechaInicio = DTFechaInicial;
            Sesion.AsistenciaFechaFin = DTFechaFinal;
            // DataSet que se usa para guardar los datos a mostrar en el Grid
            DataSet DS = new DataSet();
            string Tag = Request.Params.Get("ValorMenuTag");
            // Limpiamos los datos del Grid
            if (LimpiarGrid)
            {
                Grid.Clear();
                Grid.DataMember = "";
                Ordenar = true;
            }
            
            // Obtenemos un DataSet (Tabla) de los Accesos
            //if (AuxTag == "" || AuxTag == "AsistenciaMostrarAccesos")
            if (MenuElmento("AsistenciaMostrarAccesos").Checked)
            {
                DS = CeC_Asistencias.ObtenAccesosTerminal(Persona_ID, Agrupacion, DTFechaInicial, DTFechaFinal.AddDays(1), Sesion.USUARIO_ID, Sesion.AccesoTerminalFiltro, Sesion);
                Grid.DataSource = DS.Tables[0];
                Grid.DataMember = DS.Tables[0].TableName;
                Grid.DataKeyField = "ACCESO_ID";
            }
            else
            {
                // Obtenemos un DataSet (Tabla) los Accesos de ES por Terminal
                ObtenAccesosES_Terminal(Sesion.Terminal_ES);
            }
            // Obtenemos un DataSet (Tabla) de la Asistencia con Entradas y Salidas
            if (MenuElmento("AsistenciaMostrarES").Checked)
            {
                DS = CeC_Asistencias.ObtenAsistenciaES(Persona_ID, Agrupacion, DTFechaInicial, DTFechaFinal.AddDays(1),  Sesion);
                Grid.DataSource = DS.Tables[0];
                Grid.DataMember = DS.Tables[0].TableName;
                Grid.DataKeyField = "PERSONA_ES_ID";
            }
            // Obtenemos un DataSet (Tabla) de la Asistencia
            if (MenuElmento("AsistenciaMostrar").Checked)
            {
                // Obtenemos un DataSet (Tabla) de el reporte de Asistencia Predeterminado
                if (MenuElmento("AS_PREDETERMINADO").Checked)
                {
                    DS = CeC_Asistencias.ObtenAsistencia(EstaChecado("AS_ENTRADA_SALIDA"),
                        EstaChecado("AS_COMIDA"), EstaChecado("AS_HORAS_EXTRAS"),
                        EstaChecado("AS_TOTALES"), EstaChecado("AS_INCIDENCIA"),
                        EstaChecado("AS_TURNO"), ObtenTimposIncidenciasSistemaSeleccionadas(),
                        ObtenTimposIncidenciasSeleccionadas(), EstaChecado("AS_AGRUPACION"),
                        EstaChecado("AS_EMPLEADO"),
                        Persona_ID, Agrupacion, DTFechaInicial, DTFechaFinal.AddDays(1), Sesion);

                }
                // Obtenemos un DataSet (Tabla) de el reporte de Asistencia en Formato 7 Dias
                if (MenuElmento("AS_7_DIAS").Checked)
                {
                    DS = CeC_Asistencias.ObtenAsistenciaSemanal(EstaChecado("AS_ENTRADA_SALIDA"), EstaChecado("AS_TURNO"),
                        EstaChecado("AS_AGRUPACION"), EstaChecado("AS_EMPLEADO"),
                        Persona_ID, Agrupacion, DTFechaInicial,  Sesion);
                }
                // Obtenemos un DataSet (Tabla) de el reporte de Asistencia en Formato 7 Dias
                if (MenuElmento("AS_31_DIAS").Checked)
                {
                    DS = CeC_Asistencias.ObtenAsistenciaHorizontal(EstaChecado("AS_ENTRADA_SALIDA"), EstaChecado("AS_TURNO"),
                        EstaChecado("AS_INCIDENCIA"), EstaChecado("AS_AGRUPACION"), EstaChecado("AS_EMPLEADO"),
                        Persona_ID, Agrupacion, DTFechaInicial, DTFechaFinal.AddDays(1), Sesion);
                }
                // Muestra en el Grid los datos
                Grid.DataSource = DS.Tables[0];
                Grid.DataMember = DS.Tables[0].TableName;
                Grid.DataKeyField = "PERSONA_DIARIO_ID";
            }
            // Obtenemos un DataSet (Tabla) con los totales
            if (MenuElmento("AsistenciaTotales").Checked)
            {
                DS = CeC_Asistencias.ObtenAsistenciaTotales(EstaChecado("AS_T_AGRUPACION"),
                EstaChecado("AS_T_EMPLEADO"), EstaChecado("AS_T_TOTALES"),
                EstaChecado("AS_T_HISTORIAL"), EstaChecado("AS_T_SALDO"), EstaChecado("AS_T_FALTAS"), Persona_ID, Agrupacion, DTFechaInicial, DTFechaFinal.AddDays(1),  Sesion);

                Grid.DataSource = DS.Tables[0];
                Grid.DataMember = DS.Tables[0].TableName;
                if (EstaChecado("AS_T_HISTORIAL"))
                    Grid.DataKeyField = "ALMACEN_INC_ID";
            }
            // Obtenemos un DataSet (Tabla) con los datos para graficar
            if (MenuElmento("AsistenciaGraficar").Checked)
            {
                DS = CeC_Asistencias.ObtenAsistenciaGrafica(EstaChecado("AS_G_AGRUPACION"),
                        EstaChecado("AS_G_EMPLEADO"),
                        Persona_ID, Agrupacion, DTFechaInicial, DTFechaFinal.AddDays(1),  Sesion);
                Grid.DataSource = DS.Tables[0];
                Grid.DataMember = DS.Tables[0].TableName;
            }
            ((Infragistics.UltraChart.Resources.Appearance.NumericSeries)Chart.CompositeChart.Series[0]).Points[0].Value =
                CeC_Asistencias.ObtenTotalAsistencias(Persona_ID, Agrupacion, DTFechaInicial, DTFechaFinal.AddDays(1), Sesion.USUARIO_ID);
            ((Infragistics.UltraChart.Resources.Appearance.NumericSeries)Chart.CompositeChart.Series[1]).Points[0].Value =
                CeC_Asistencias.ObtenTotalRetardos(Persona_ID, Agrupacion, DTFechaInicial, DTFechaFinal.AddDays(1), Sesion.USUARIO_ID);
            ((Infragistics.UltraChart.Resources.Appearance.NumericSeries)Chart.CompositeChart.Series[2]).Points[0].Value =
                CeC_Asistencias.ObtenTotalFaltas(Persona_ID, Agrupacion, DTFechaInicial, DTFechaFinal.AddDays(1), Sesion.USUARIO_ID);
            ((Infragistics.UltraChart.Resources.Appearance.NumericSeries)Chart.CompositeChart.Series[3]).Points[0].Value =
                CeC_Asistencias.ObtenTotalJustificaciones(Persona_ID, Agrupacion, DTFechaInicial, DTFechaFinal.AddDays(1), Sesion.USUARIO_ID);
            if (!LimpiarGrid && LblHXTrabajar.Text != "")
                return;
            // Mostramos el Grid en pantalla
            Grid.DataBind();

            /*  if (MenuElmento("AsistenciaGraficar").Checked)
              {
                              WebProgressBar PB = new WebProgressBar();
              PB.Maximum = 100;
                  Grid.Bands[0].Columns[0].EditorControlID = PB
              }*/

            LblHXTrabajar.Text = CeC_Asistencias.ObtenTotalHoras("PERSONA_DIARIO_TES", Persona_ID, Agrupacion, DTFechaInicial, DTFechaFinal.AddDays(1), Sesion.USUARIO_ID);
            LblHTrabajadas.Text = CeC_Asistencias.ObtenTotalHoras("PERSONA_DIARIO_TT", Persona_ID, Agrupacion, DTFechaInicial, DTFechaFinal.AddDays(1), Sesion.USUARIO_ID);
            LblHComida.Text = CeC_Asistencias.ObtenTotalHoras("PERSONA_DIARIO_TC", Persona_ID, Agrupacion, DTFechaInicial, DTFechaFinal.AddDays(1), Sesion.USUARIO_ID);
            LblHRetardo.Text = CeC_Asistencias.ObtenTotalHoras("PERSONA_DIARIO_TDE", Persona_ID, Agrupacion, DTFechaInicial, DTFechaFinal.AddDays(1), Sesion.USUARIO_ID);
            LblExtra.Text = CeC_AsistenciasHE.ObtenTotalHorasHE("PERSONA_D_HE_SIS", Persona_ID, Agrupacion, DTFechaInicial, DTFechaFinal.AddDays(1), Sesion.USUARIO_ID);
        }
        catch (Exception ex) { CIsLog2.AgregaError(ex); }
    }
    /// <summary>
    /// Carga los valores del menu de acuerdo a las variables de sesión
    /// </summary>
    private void CargaSesion()
    {
        if (Sesion.AsistenciaMostrarAccesos)
            AsistenciaMostrarAccesos();
        if (Sesion.AsistenciaMostrarES)
            AsistenciaMostrarES();
        if (Sesion.AsistenciaGraficar)
            AsistenciaGraficar();
        if (Sesion.AsistenciaTotales)
            AsistenciaTotales();
        if (Sesion.AsistenciaMostrar)
        {
            AsistenciaMostrar();
            if (Sesion.AS_PREDETERMINADO)
                AS_PREDETERMINADO();
            if (Sesion.AS_7_DIAS)
                AS_7_DIAS();
            if (Sesion.AS_31_DIAS)
                AS_31_DIAS();
        }
        MenuElmento("AS_ENTRADA_SALIDA").Checked = Sesion.AS_ENTRADA_SALIDA;
        MenuElmento("AS_HORAS_EXTRAS").Checked = Sesion.AS_HORAS_EXTRAS;
        MenuElmento("AS_COMIDA").Checked = Sesion.AS_COMIDA;
        MenuElmento("AS_TOTALES").Checked = Sesion.AS_TOTALES;
        MenuElmento("AS_INCIDENCIA").Checked = Sesion.AS_INCIDENCIA;
        MenuElmento("AS_TURNO").Checked = Sesion.AS_TURNO;
        MenuElmento("AS_SOLO_FALTAS").Checked = Sesion.AS_SOLO_FALTAS;
        MenuElmento("AS_SOLO_RETARDOS").Checked = Sesion.AS_SOLO_RETARDOS;
        MenuElmento("AS_EMPLEADO").Checked = Sesion.AS_EMPLEADO;
        MenuElmento("AS_AGRUPACION").Checked = Sesion.AS_AGRUPACION;
        MenuElmento("AS_G_AGRUPACION").Checked = Sesion.AS_G_AGRUPACION;
        MenuElmento("AS_G_EMPLEADO").Checked = Sesion.AS_G_EMPLEADO;
        MenuElmento("FIRMA").Checked = Sesion.AS_FIRMA;
    }

    /// <summary>
    /// Valida si el Perfil del Usuario tiene los suficientes permisos para asignar Turnos, Justificaciones e Incidencias
    /// </summary>
    void ValidaPerfilesMenu()
    {
        if (Sesion.PERFIL_ID == 3 || Sesion.PERFIL_ID == 6)
        {
            MenuElmento("TURNOS").Hidden = true;
            MenuElmento("JUSTIFICACIONES").Hidden = true;
            MenuElmento("AGREGAR_INCIDENCIAS").Hidden = true;
        }
    }
    /// <summary>
    /// Carga los parámetros de Sesion
    /// </summary>
    protected void CargaVariables()
    {
        Sesion = CeC_Sesion.Nuevo(this);
        if (Sesion.SESION_ID <= 0)
            return;
        string Parametros = Sesion.Parametros;
        if (Parametros == "REGRESO")
        {
            if (Sesion.eClock_Agrupacion.Length > 0)
                Parametros = "AGRUPACION";
        }
        if (Parametros == "AGRUPACION")
            Agrupacion = Sesion.eClock_Agrupacion;
        else
        {
            Persona_ID = Sesion.eClock_Persona_ID;
            Persona_Link_ID = CeC_BD.ObtenPersonaLinkID(Persona_ID);
            Sesion.eClock_Agrupacion = "";
        }
    }
    /// <summary>
    /// Inicializa los datos del Grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Grid_InitializeDataSource(object sender, Infragistics.WebUI.UltraWebGrid.UltraGridEventArgs e)
    {
        if (IsPostBack)
        {
            //Page_Load(null, null);
            CargaVariables();
        }
        ActualizaDatos();
    }
    /// <summary>
    /// Asigna el formato dddd "dd-MM-yy" a las cabeceras de columna
    /// </summary>
    /// <param name="Columna">Columna que se aplicará el formato</param>
    /// <param name="NO">Dias que se sumara a la Fecha Inicial</param>
    protected void AsignaFormatoDia(Infragistics.WebUI.UltraWebGrid.UltraGridColumn Columna, int NO)
    {
        DateTime DTFechaInicial = Convert.ToDateTime(FechaInicial.Value).Date;
        Columna.Header.Caption = DTFechaInicial.AddDays(NO).ToString("dddd dd-MM-yy");
        Columna.Header.Style.Font.Size = 7;
        Columna.CellStyle.Font.Size = 6;
        Columna.Width = System.Web.UI.WebControls.Unit.Pixel(70);
    }
    /// <summary>
    /// Asigna el formato dddd "dd-MM-yy" a las cabeceras de columna
    /// </summary>
    /// <param name="Columna">Columna que se aplicará el formato</param>
    /// <param name="NO">Dias que se sumara a la Fecha Inicial</param>
    protected void AsignaFormatoInc(Infragistics.WebUI.UltraWebGrid.UltraGridColumn Columna, int NO)
    {
        DateTime DTFechaInicial = Convert.ToDateTime(FechaInicial.Value).Date;
        Columna.Header.Caption = DTFechaInicial.AddDays(NO).ToString("dd-MM-yy");
        Columna.Header.Style.Font.Size = 7;
        Columna.CellStyle.Font.Size = 6;
        Columna.Width = System.Web.UI.WebControls.Unit.Pixel(30);
    }
    /// <summary>
    /// Obtiene las Imagenes de la carpeta PB
    /// </summary>
    /// <param name="Valor"></param>
    /// <param name="Color"></param>
    /// <returns></returns>
    string ObtenImag(object Valor, string Color)
    {
        return "PB/" + Color + Valor.ToString() + ".jpg";
    }
    /// <summary>
    /// Obtiene el estilo para el div
    /// </summary>
    /// <returns></returns>
    string ObtenDiv()
    {
        return " background-size: 100%; background-position: fixed; text-align: center;";
    }
    /// <summary>
    /// Aplica formato a las filas
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Grid_InitializeRow(object sender, Infragistics.WebUI.UltraWebGrid.RowEventArgs e)
    {
        // Verificamos si se quiere mostrar la gráfica de asistencia
        if (MenuElmento("AsistenciaGraficar").Checked)
        {
            int TotalAsistencias = Convert.ToInt32(e.Row.Cells.FromKey("TotalAsistencias").Value);
            int TotalRetardos = Convert.ToInt32(e.Row.Cells.FromKey("TotalRetardos").Value);
            int TotalFaltas = Convert.ToInt32(e.Row.Cells.FromKey("TotalFaltas").Value);
            int TotalIncidencias = Convert.ToInt32(e.Row.Cells.FromKey("TotalIncidencias").Value);
            int TotalTotal = TotalAsistencias + TotalRetardos + TotalFaltas + TotalIncidencias;
            if (TotalTotal == 0)
                TotalTotal = 1;

            //e.Row.Cells.FromKey("PAsistencias").Style.BackgroundImage = ObtenImag(TotalAsistencias * 100 / TotalTotal, "Azul");
            //e.Row.Cells.FromKey("PAsistencias").Style.HorizontalAlign = HorizontalAlign.Center;
            //e.Row.Cells.FromKey("PAsistencias").Text = e.Row.Cells.FromKey("PAsistencias").Text + "%";
            //e.Row.Cells.FromKey("PRetardos").Style.BackgroundImage = ObtenImag(TotalRetardos * 100 / TotalTotal, "Amarillo");
            //e.Row.Cells.FromKey("PRetardos").Style.HorizontalAlign = HorizontalAlign.Center;
            //e.Row.Cells.FromKey("PRetardos").Text = e.Row.Cells.FromKey("PRetardos").Text + "%";
            //e.Row.Cells.FromKey("PFaltas").Style.BackgroundImage = ObtenImag(TotalFaltas * 100 / TotalTotal, "Rojo");
            //e.Row.Cells.FromKey("PFaltas").Style.HorizontalAlign = HorizontalAlign.Center;
            //e.Row.Cells.FromKey("PFaltas").Text = e.Row.Cells.FromKey("PFaltas").Text + "%";
            //e.Row.Cells.FromKey("PIncidencias").Style.BackgroundImage = ObtenImag(TotalIncidencias * 100 / TotalTotal, "Verde");
            //e.Row.Cells.FromKey("PIncidencias").Style.HorizontalAlign = HorizontalAlign.Center;
            //e.Row.Cells.FromKey("PIncidencias").Text = e.Row.Cells.FromKey("PIncidencias").Text + "%";
            e.Row.Cells.FromKey("PAsistencias").Style.BackgroundImage = ObtenImag(TotalAsistencias * 100 / TotalTotal, "Azul");
            e.Row.Cells.FromKey("PAsistencias").Style.CustomRules = ObtenDiv();
            e.Row.Cells.FromKey("PAsistencias").Text += "%";
            e.Row.Cells.FromKey("PRetardos").Style.BackgroundImage = ObtenImag(TotalRetardos * 100 / TotalTotal, "Amarillo");
            e.Row.Cells.FromKey("PRetardos").Style.CustomRules = ObtenDiv();
            e.Row.Cells.FromKey("PRetardos").Text += "%";
            e.Row.Cells.FromKey("PFaltas").Style.BackgroundImage = ObtenImag(TotalFaltas * 100 / TotalTotal, "Rojo");
            e.Row.Cells.FromKey("PFaltas").Style.CustomRules = ObtenDiv();
            e.Row.Cells.FromKey("PFaltas").Text += "%";
            e.Row.Cells.FromKey("PIncidencias").Style.BackgroundImage = ObtenImag(TotalIncidencias * 100 / TotalTotal, "Verde");
            e.Row.Cells.FromKey("PIncidencias").Style.CustomRules = ObtenDiv();
            e.Row.Cells.FromKey("PIncidencias").Text += "%";
            e.Row.Cells.FromKey("PDiasFestivos").Style.BackgroundImage = ObtenImag(TotalFaltas * 100 / TotalTotal, "Rojo");
            e.Row.Cells.FromKey("PDiasFestivos").Style.CustomRules = ObtenDiv();
            e.Row.Cells.FromKey("PDiasFestivos").Text += "%";
            e.Row.Cells.FromKey("PDiasDescanso").Style.BackgroundImage = ObtenImag(TotalFaltas * 100 / TotalTotal, "Rojo");
            e.Row.Cells.FromKey("PDiasDescanso").Style.CustomRules = ObtenDiv();
            e.Row.Cells.FromKey("PDiasDescanso").Text += "%";
        }
        // Verificamos si se mostrara la asistencia
        if (MenuElmento("AsistenciaMostrar").Checked)
        {
            for (int Cont = 0; Cont < e.Row.Cells.Count; Cont++)
            {
                string Columna = e.Row.Cells[Cont].Column.Key;
                int DiaNo = ObtenNoDia(Columna);
                if (DiaNo >= 0)
                {
                    string Valor = CeC.Convierte2String(e.Row.Cells[Cont].Value);
                    if (Valor.IndexOf('/') > 0)
                    {
                        Valor = Valor.Substring(Valor.IndexOf('/') + 1).Trim();
                    }
                    System.Drawing.Color Color = CeC_Asistencias.ObtenColor(Valor);
                    if (Color != System.Drawing.Color.Empty)
                    {
                        e.Row.Cells[Cont].Style.BackColor = Color;
                    }
                }
            }
            try
            {
                string Abr = CeC.Convierte2String(e.Row.Cells.FromKey("INCIDENCIA_ABR").Value);
                System.Drawing.Color Color = CeC_Asistencias.ObtenColor(Abr);
                if (Color != System.Drawing.Color.Empty)
                {
                    //e.Row.Style.BackgroundImage = "~/Imagenes/PB_Rojo.png";
                    //e.Row.Style.
                    //                    Grid.DisplayLayout.SelectedRowStyleDefault.BackgroundImage = "Office2003SelRow.gif";
                    e.Row.Style.BackColor = Color;
                }
            }
            catch (Exception ex)
            {
                CIsLog2.AgregaError("WF_AsistenciasEmp.Grid_InitializeRow", ex);
            }
        }
    }
    /// <summary>
    /// Inicializa el formato que se aplicará a el Grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Grid_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        //CeC_Grid.AplicaFormato(Grid, true, false, true, true);
        // Muestra el Footer en el Grid
        Grid.DisplayLayout.ColFootersVisibleDefault = Infragistics.WebUI.UltraWebGrid.ShowMarginInfo.Yes;
        CeC_Grid.HorasSuma(Grid);
        //Grid.Columns.FromKey("PERSONA_DIARIO_ID")
        /* e.
         Grid.DisplayLayout.Bands[0].Columns[1].*/

        try { Grid.Columns.FromKey("PERSONA_DIARIO_ID").Hidden = true; }
        catch { }
        try { Grid.Columns.FromKey("PERSONA_ID").Hidden = true; }
        catch { }
        try { Grid.Columns.FromKey("ACCESO_ID").Hidden = true; }
        catch { }
        try { Grid.Columns.FromKey("PERSONA_ES_ID").Hidden = true; }
        catch { }

        try { Grid.Columns.FromKey("INCIDENCIA_ABR").Hidden = true; }
        catch { }
        CeC_Grid.AplicaFormato(Grid, true, false, true, true);

        //        Grid.DisplayLayout.FixedCellStyleDefault.Font.Size = 8;
        //        .FrameStyle.Font.Size = 8;
        Grid.DisplayLayout.FixedHeaderIndicatorDefault = Infragistics.WebUI.UltraWebGrid.FixedHeaderIndicator.Button;
        //        e.Layout.UseFixedHeaders = true;
        /*        try { Grid.Columns.FromKey("PERSONA_NOMBRE").Header.Fixed = true; }
                catch { }*/
        if (MenuElmento("AsistenciaMostrar").Checked && MenuElmento("AS_7_DIAS").Checked)
        {
            try { Grid.Columns.FromKey("PERSONA_NOMBRE").Header.Style.Font.Size = 8; }
            catch { }
            for (int Cont = 0; Cont < 7; Cont++)
            {
                try { AsignaFormatoDia(Grid.Columns.FromKey("ASISTENCIA_D" + Cont), Cont); }
                catch { }
                try { AsignaFormatoDia(Grid.Columns.FromKey("TURNO_D" + Cont), Cont); }
                catch { }
            }
            Grid.DisplayLayout.SelectTypeCellDefault = Infragistics.WebUI.UltraWebGrid.SelectType.Extended;
            Grid.DisplayLayout.SelectTypeColDefault = Infragistics.WebUI.UltraWebGrid.SelectType.Extended;
            Grid.DisplayLayout.CellClickActionDefault = Infragistics.WebUI.UltraWebGrid.CellClickAction.CellSelect;
        }
        if (MenuElmento("AsistenciaMostrar").Checked && MenuElmento("AS_31_DIAS").Checked)
        {
            for (int Cont = 0; Cont < Grid.Columns.Count; Cont++)
            {
                string Columna = Grid.Columns[Cont].Key;
                if ((Columna.Length > 12 && Columna.Substring(0, 12) == "ASISTENCIA_D") ||
                    (Columna.Length > 7 && Columna.Substring(0, 7) == "TURNO_D"))
                {
                    int Sumar = 0;
                    if (Columna.Length > 12)
                        Sumar = Convert.ToInt32(Columna.Substring(12));
                    else
                        Sumar = Convert.ToInt32(Columna.Substring(7));
                    AsignaFormatoDia(Grid.Columns.FromKey(Columna), Sumar);
                }
                if (Columna.Length > 3 && Columna.Substring(0, 3) == "DIA")
                {
                    int Sumar = 0;
                    Sumar = Convert.ToInt32(Columna.Substring(3));
                    AsignaFormatoInc(Grid.Columns.FromKey(Columna), Sumar);
                }
            }
            Grid.DisplayLayout.SelectTypeCellDefault = Infragistics.WebUI.UltraWebGrid.SelectType.Extended;
            Grid.DisplayLayout.SelectTypeColDefault = Infragistics.WebUI.UltraWebGrid.SelectType.Extended;
            Grid.DisplayLayout.CellClickActionDefault = Infragistics.WebUI.UltraWebGrid.CellClickAction.CellSelect;
        }
        if (MenuElmento("AsistenciaGraficar").Checked)
        {
            e.Layout.Bands[0].Columns.FromKey("PAsistencias").AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No;
            e.Layout.Bands[0].Columns.FromKey("PRetardos").AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No;
            e.Layout.Bands[0].Columns.FromKey("PFaltas").AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No;
            e.Layout.Bands[0].Columns.FromKey("PIncidencias").AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No;
            e.Layout.Bands[0].Columns.FromKey("PDiasFestivos").AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No;
            e.Layout.Bands[0].Columns.FromKey("PDiasDescanso").AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No;
        }
        CargaOrden();
    }
    /// <summary>
    /// Captura el cambio de Fecha Inicial del periodo a mostrar
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void FechaInicial_ValueChanged(object sender, Infragistics.WebUI.WebSchedule.WebDateChooser.WebDateChooserEventArgs e)
    {
        DateTime DTFechaInicial = Convert.ToDateTime(FechaInicial.Value).Date;
        DateTime DTFechaFinal = Convert.ToDateTime(FechaFinal.Value).Date;

        int Dias = Convert.ToInt32(((TimeSpan)(DTFechaFinal - Sesion.AsistenciaFechaInicio)).TotalDays);
        FechaFinal.Value = DTFechaInicial.AddDays(Dias);
        ActualizaDatos(true);
    }
    /// <summary>
    /// Captura el cambio de Fecha Final del periodo a mostrar
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void FechaFinal_ValueChanged(object sender, Infragistics.WebUI.WebSchedule.WebDateChooser.WebDateChooserEventArgs e)
    {
        DateTime DTFechaInicial = Convert.ToDateTime(FechaInicial.Value).Date;
        DateTime DTFechaFinal = Convert.ToDateTime(FechaFinal.Value).Date;
        Tbx_NoDias.Value = (DTFechaFinal - DTFechaInicial).TotalDays;
        ActualizaDatos(true);
    }
    /// <summary>
    /// Carga el listado de Turnos por Suscripción
    /// </summary>
    void CargaTurnos()
    {
        try
        {
            DataSet DS = CeC_Turnos.ObtenTurnosDSMenu(Sesion.SUSCRIPCION_ID);
            if (DS == null)
                return;
            //int MenuID = MenuElmento();
            Infragistics.WebUI.UltraWebNavigator.Item MenuTurnos = MenuElmento("TURNOS");
            MenuTurnos.Items.Add("Descanso", -1);
            MenuTurnos.Items.Add("Predeterminado", 0);
            MenuTurnos.Items.Add("-", -2).Separator = true;
            foreach (DataRow DR in DS.Tables[0].Rows)
            {
                MenuTurnos.Items.Add(DR[1].ToString(), DR[0].ToString());
                MenuTurnos.Items[0].Items.Add(DR[1].ToString(), DR[0].ToString());
            }
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError("WF_AsistenciasEmp.CargaTurnos", ex);
        }
    }
    /// <summary>
    /// Carga el listado de terminales por Suscripción
    /// </summary>
    void CargaAgrupacionTerminales()
    {
        try
        {
            int i = 0;
            DataSet DS = CeC_Terminales.ObtenTerminalesAgrupacion(Sesion.SUSCRIPCION_ID);
            if (DS == null)
                return;
            Infragistics.WebUI.UltraWebNavigator.Item MenuTerminales = MenuElmento("AsistenciaMostrarAccesos");
            //MenuTerminales.Items.Add("Mostrar Todas las Terminales", 0);
            //MenuTerminales.Items.Add("-", -2).Separator = true;
            Terminales = new string[DS.Tables[0].Rows.Count];
            foreach (DataRow DR in DS.Tables[0].Rows)
            {
                MenuTerminales.Items.Add(DR[0].ToString());
                Terminales[i++] = MenuTerminales.ToString(); //.Tag.ToString();
                //MenuTerminales.Items[0].Items.Add(DR[1].ToString(), DR[0].ToString());
            }
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError("WF_AsistenciasEmp.CargaMenuTerminales", ex);
        }
    }

    /// <summary>
    /// Obtiene las Entradas y las Salidas por Agrupacion de Terminales
    /// </summary>
    /// <param name="Tag">Nombre de la Agrupación de la Terminal</param>
    void ObtenAccesosES_Terminal(string Tag)
    {
        try
        {
            if (Sesion == null)
                return;
            if (Sesion.SESION_ID <= 0)
                return;
            Ordenar = true;
            DateTime DTFechaInicial = Convert.ToDateTime(FechaInicial.Value).Date;
            DateTime DTFechaFinal = Convert.ToDateTime(FechaFinal.Value).Date;
            Sesion.AsistenciaFechaInicio = DTFechaInicial;
            Sesion.AsistenciaFechaFin = DTFechaFinal;
            String TerminalAgrupacion = Tag;
            DataSet DS = CeC_Accesos.ObtenEntradaSalidaDS(Persona_ID, TerminalAgrupacion, Agrupacion, DTFechaInicial, DTFechaFinal.AddDays(1), Sesion.USUARIO_ID, Sesion);
            if (DS == null || DS.Tables.Count <= 0)
            {
                CIsLog2.AgregaError("ObtenAccesosES_Terminal.ActualizaDatos. No hay datos para mostrar");
                return;
            }
            DataTable DTAccesos = AccesosES(DS);
            Grid.DataSource = DTAccesos;
            Grid.DataMember = DTAccesos.TableName;
            Grid.DataKeyField = "PERSONA_LINK_ID";
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError("WF_AsistenciasEmp.ObtenAccesosES_Terminal", ex);
        }
    }
    /// <summary>
    /// Obtiene los tipos de Incidencias del Sistema
    /// </summary>
    /// <returns>Cadena con los tipos de incidencias separados por comas</returns>
    string ObtenTimposIncidenciasSistemaSeleccionadas()
    {
        string TiposIncidenciasSistema = "";
        Infragistics.WebUI.UltraWebNavigator.Item MenuItem = MenuElmento("FILTRAR_POR_SISTEMA");

        foreach (Infragistics.WebUI.UltraWebNavigator.Item Item in MenuItem.Items)
        {
            if (Item.Checked)
            {
                TiposIncidenciasSistema = CeC.AgregaSeparador(TiposIncidenciasSistema, CeC.Convierte2Int(Item.Tag).ToString(), ",");
            }
        }
        if (Sesion.AS_SOLO_FALTAS)
            TiposIncidenciasSistema = CeC_Asistencias.ObtenIDFaltas(Sesion);
        if (Sesion.AS_SOLO_RETARDOS)
            TiposIncidenciasSistema = CeC_Asistencias.ObtenIDRetardos(Sesion);

        return TiposIncidenciasSistema;
    }
    /// <summary>
    /// Carga los tipos de Incidencias
    /// </summary>
    void CargaTiposIncidenciasSistema()
    {
        DataSet DS = Cec_Incidencias.ObtenTiposIncidenciasSistemaMenu();
        if (DS == null)
            return;
        Infragistics.WebUI.UltraWebNavigator.Item MenuItem = MenuElmento("FILTRAR_POR_SISTEMA");
        foreach (DataRow DR in DS.Tables[0].Rows)
        {
            MenuItem.Items.Add(DR[1].ToString(), DR[0].ToString()).CheckBox = Infragistics.WebUI.UltraWebNavigator.CheckBoxes.True;
        }
    }
    /// <summary>
    /// Obtiene los tipos de Incidencias
    /// </summary>
    /// <returns>Cadena con los tipos de incidencias separadas por coma</returns>
    string ObtenTimposIncidenciasSeleccionadas()
    {
        string TiposIncidencias = "";
        Infragistics.WebUI.UltraWebNavigator.Item MenuItem = MenuElmento("FILTRAR_POR");
        bool SoloJustificaciones = EstaChecado("AS_SOLO_JUSTIFICACIONES");
        foreach (Infragistics.WebUI.UltraWebNavigator.Item Item in MenuItem.Items)
        {
            if (SoloJustificaciones && Item.Checked)
                Item.Checked = false;
            if (SoloJustificaciones || Item.Checked)
            {
                int IDTipoIncidencia = CeC.Convierte2Int(Item.Tag);
                if (IDTipoIncidencia > 0)
                    TiposIncidencias = CeC.AgregaSeparador(TiposIncidencias, IDTipoIncidencia.ToString(), ",");
            }
        }
        return TiposIncidencias;
    }
    /// <summary>
    /// Carga los tipos de incidencias
    /// </summary>
    void CargaTiposIncidencias()
    {
        DataSet DS = Cec_Incidencias.ObtenTiposIncidenciasMenu(Sesion.SUSCRIPCION_ID);
        if (DS == null)
            return;
        Infragistics.WebUI.UltraWebNavigator.Item MenuItemFiltrarPor = MenuElmento("FILTRAR_POR");
        Infragistics.WebUI.UltraWebNavigator.Item MenuItem = MenuElmento("JUSTIFICACIONES");
        MenuItem.Items.Add("Borrar Incidencia", -1);
        //        Menu.Items[MenuID].Items.Add("Predeterminado", 0);
        MenuItem.Items.Add("-", -2).Separator = true;
        foreach (DataRow DR in DS.Tables[0].Rows)
        {
            MenuItem.Items.Add(DR[1].ToString(), DR[0].ToString());
            MenuItemFiltrarPor.Items.Add(DR[1].ToString(), DR[0].ToString()).CheckBox = Infragistics.WebUI.UltraWebNavigator.CheckBoxes.True;
        }
    }
    /// <summary>
    /// Selecciona el elemento de menu "Asistencia" y deselecciona los demás elementos
    /// </summary>
    void AsistenciaMostrar()
    {
        Ordenar = true;
        MenuElmento("BORRAR_INVENTARIO").Hidden = true;
        if (CeC_Usuarios.ObtenEsEmpleado(Sesion.USUARIO_ID))
            MenuElmento("TURNOS").Hidden = true;
        else
            MenuElmento("TURNOS").Hidden = false;
        MenuElmento("JUSTIFICACIONES").Hidden = false;
        MenuElmento("AGREGAR_INCIDENCIAS").Hidden = false;
        MenuElmento("AsistenciaMostrar").Checked = true;
        Sesion.AsistenciaMostrar = true;
        MenuElmento("AsistenciaGraficar").Checked = false;
        Sesion.AsistenciaGraficar = false;
        MenuElmento("AsistenciaTotales").Checked = false;
        Sesion.AsistenciaTotales = false;
        MenuElmento("AsistenciaMostrarAccesos").Checked = false;
        Sesion.AsistenciaMostrarAccesos = false;
    }
    /// <summary>
    /// Selecciona el elemento de menu "" y deselecciona los demás elementos
    /// </summary>
    void AsistenciaGraficar()
    {
        Ordenar = true;
        MenuElmento("BORRAR_INVENTARIO").Hidden = true;
        MenuElmento("TURNOS").Hidden = true;
        MenuElmento("JUSTIFICACIONES").Hidden = true;
        MenuElmento("AGREGAR_INCIDENCIAS").Hidden = true;
        MenuElmento("AsistenciaMostrar").Checked = false;
        Sesion.AsistenciaMostrar = false;
        MenuElmento("AsistenciaGraficar").Checked = true;
        Sesion.AsistenciaGraficar = true;
        MenuElmento("AsistenciaTotales").Checked = false;
        Sesion.AsistenciaTotales = false;
        MenuElmento("AsistenciaMostrarAccesos").Checked = false;
        Sesion.AsistenciaMostrarAccesos = false;
    }
    /// <summary>
    /// Selecciona el elemento de menu "" y deselecciona los demás elementos
    /// </summary>
    void AsistenciaTotales()
    {
        Ordenar = true;

        MenuElmento("BORRAR_INVENTARIO").Hidden = !Sesion.AS_T_HISTORIAL;
        MenuElmento("TURNOS").Hidden = true;
        MenuElmento("JUSTIFICACIONES").Hidden = true;
        MenuElmento("AGREGAR_INCIDENCIAS").Hidden = true;
        MenuElmento("AsistenciaMostrar").Checked = false;
        Sesion.AsistenciaMostrar = false;
        MenuElmento("AsistenciaTotales").Checked = true;
        Sesion.AsistenciaTotales = true;
        MenuElmento("AsistenciaGraficar").Checked = false;
        Sesion.AsistenciaGraficar = false;
        MenuElmento("AsistenciaMostrarAccesos").Checked = false;
        Sesion.AsistenciaMostrarAccesos = false;
    }
    /// <summary>
    /// Selecciona el elemento de menu "Accesos" y deselecciona los demás elementos
    /// </summary>
    void AsistenciaMostrarAccesos()
    {
        Ordenar = true;
        MenuElmento("BORRAR_INVENTARIO").Hidden = true;
        MenuElmento("TURNOS").Hidden = true;
        MenuElmento("JUSTIFICACIONES").Hidden = true;
        MenuElmento("AGREGAR_INCIDENCIAS").Hidden = true;
        MenuElmento("AsistenciaMostrar").Checked = false;
        Sesion.AsistenciaMostrar = false;
        MenuElmento("AsistenciaGraficar").Checked = false;
        Sesion.AsistenciaGraficar = false;
        MenuElmento("AsistenciaTotales").Checked = false;
        Sesion.AsistenciaGraficar = false;
        MenuElmento("AsistenciaMostrarAccesos").Checked = true;
        Sesion.AsistenciaMostrarAccesos = true;
    }
    void ObtenAccesosES_Terminal()
    {
        MenuElmento("BORRAR_INVENTARIO").Hidden = true;
        MenuElmento("TURNOS").Hidden = true;
        MenuElmento("JUSTIFICACIONES").Hidden = true;
        MenuElmento("AGREGAR_INCIDENCIAS").Hidden = true;
        MenuElmento("AsistenciaMostrar").Checked = false;
        Sesion.AsistenciaMostrar = false;
        MenuElmento("AsistenciaGraficar").Checked = false;
        Sesion.AsistenciaGraficar = false;
        MenuElmento("AsistenciaTotales").Checked = false;
        Sesion.AsistenciaGraficar = false;
        MenuElmento("AsistenciaMostrarAccesos").Checked = false;
        Sesion.AsistenciaMostrarAccesos = false;
    }
    /// <summary>
    /// 
    /// </summary>
    void AsistenciaMostrarES()
    {
        MenuElmento("BORRAR_INVENTARIO").Hidden = true;
        MenuElmento("TURNOS").Hidden = true;
        MenuElmento("JUSTIFICACIONES").Hidden = true;
        MenuElmento("AGREGAR_INCIDENCIAS").Hidden = true;
        MenuElmento("AsistenciaMostrar").Checked = false;
        Sesion.AsistenciaMostrar = false;
        MenuElmento("AsistenciaGraficar").Checked = false;
        Sesion.AsistenciaGraficar = false;
        MenuElmento("AsistenciaTotales").Checked = false;
        Sesion.AsistenciaTotales = false;
        MenuElmento("AsistenciaMostrarAccesos").Checked = false;
        Sesion.AsistenciaMostrarAccesos = false;
    }
    /// <summary>
    /// 
    /// </summary>
    void AS_PREDETERMINADO()
    {
        MenuElmento("AS_PREDETERMINADO").Checked = true;
        Sesion.AS_PREDETERMINADO = true;
        MenuElmento("AS_7_DIAS").Checked = false;
        Sesion.AS_7_DIAS = false;
        MenuElmento("AS_31_DIAS").Checked = false;
        Sesion.AS_31_DIAS = false;
        MenuElmento("AS_ENTRADA_SALIDA").Hidden = false;
        MenuElmento("AS_COMIDA").Hidden = false;
        MenuElmento("AS_HORAS_EXTRAS").Hidden = false;
        MenuElmento("AS_TOTALES").Hidden = false;
        MenuElmento("AS_TURNO").Hidden = false;
        MenuElmento("AS_INCIDENCIA").Hidden = false;
        MenuElmento("AS_SOLO_FALTAS").Hidden = false;
        MenuElmento("AS_SOLO_RETARDOS").Hidden = false;
        MenuElmento("AS_AGRUPACION").Hidden = false;
    }
    /// <summary>
    /// 
    /// </summary>
    void AS_7_DIAS()
    {
        MenuElmento("AS_PREDETERMINADO").Checked = false;
        Sesion.AS_PREDETERMINADO = false;
        MenuElmento("AS_7_DIAS").Checked = true;
        Sesion.AS_7_DIAS = true;
        MenuElmento("AS_31_DIAS").Checked = false;
        Sesion.AS_31_DIAS = false;
        MenuElmento("AS_ENTRADA_SALIDA").Hidden = false;
        MenuElmento("AS_COMIDA").Hidden = true;
        MenuElmento("AS_HORAS_EXTRAS").Hidden = true;
        MenuElmento("AS_TOTALES").Hidden = true;
        MenuElmento("AS_TURNO").Hidden = false;
        MenuElmento("AS_INCIDENCIA").Hidden = true;
        MenuElmento("AS_SOLO_FALTAS").Hidden = true;
        MenuElmento("AS_SOLO_RETARDOS").Hidden = true;
        MenuElmento("AS_AGRUPACION").Hidden = false;
    }
    /// <summary>
    /// 
    /// </summary>
    void AS_31_DIAS()
    {

        MenuElmento("AS_PREDETERMINADO").Checked = false;
        Sesion.AS_PREDETERMINADO = false;
        MenuElmento("AS_7_DIAS").Checked = false;
        Sesion.AS_7_DIAS = false;
        MenuElmento("AS_31_DIAS").Checked = true;
        Sesion.AS_31_DIAS = true;
        MenuElmento("AS_ENTRADA_SALIDA").Hidden = false;
        MenuElmento("AS_COMIDA").Hidden = true;
        MenuElmento("AS_HORAS_EXTRAS").Hidden = true;
        MenuElmento("AS_TOTALES").Hidden = false;
        MenuElmento("AS_TURNO").Hidden = false;
        MenuElmento("AS_INCIDENCIA").Hidden = false;
        MenuElmento("AS_SOLO_FALTAS").Hidden = true;
        MenuElmento("AS_SOLO_RETARDOS").Hidden = true;
        MenuElmento("AS_AGRUPACION").Hidden = false;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Tag"></param>
    void Muestra(string Tag)
    {
        if (Tag == "AGREGAR_INCIDENCIAS")
            Sesion.Redirige("WF_IncidenciasCargaMasiva.aspx");
        if (Tag == "AsistenciaMostrar")
            AsistenciaMostrar();
        if (Tag == "AsistenciaGraficar")
            AsistenciaGraficar();
        if (Tag == "AsistenciaTotales")
            AsistenciaTotales();
        if (Tag == "AsistenciaMostrarAccesos")
            AsistenciaMostrarAccesos();
        if (Tag == "AsistenciaMostrarES")
            AsistenciaMostrarES();
        if (Tag == "AS_PREDETERMINADO")
            AS_PREDETERMINADO();
        if (Tag == "AS_7_DIAS")
            AS_7_DIAS();
        if (Tag == "AS_31_DIAS")
            AS_31_DIAS();
        if (Tag == "AS_T_TOTALES")
            AS_T_TOTALES();
        if (Tag == "AS_T_HISTORIAL")
            AS_T_HISTORIAL();
        if (Tag == "AS_T_SALDO")
            AS_T_SALDO();
        if (Tag == "AS_T_FALTAS")
            AS_T_FALTAS();
    }
    void AS_T_TOTALES()
    {
        MenuElmento("AS_T_TOTALES").Checked = true;
        Sesion.AS_T_TOTALES = true;
        MenuElmento("AS_T_HISTORIAL").Checked = false;
        Sesion.AS_T_HISTORIAL = false;
        MenuElmento("AS_T_SALDO").Checked = false;
        Sesion.AS_T_SALDO = false;
        MenuElmento("AS_T_FALTAS").Checked = false;
        Sesion.AS_T_FALTAS = false;
    }
    void AS_T_HISTORIAL()
    {
        MenuElmento("AS_T_TOTALES").Checked = false;
        Sesion.AS_T_TOTALES = false;
        MenuElmento("AS_T_HISTORIAL").Checked = true;
        Sesion.AS_T_HISTORIAL = true;
        MenuElmento("AS_T_SALDO").Checked = false;
        Sesion.AS_T_SALDO = false;
        MenuElmento("BORRAR_INVENTARIO").Hidden = false;
        MenuElmento("AS_T_FALTAS").Checked = false;
        Sesion.AS_T_FALTAS = false;
    }
    void AS_T_SALDO()
    {
        MenuElmento("AS_T_TOTALES").Checked = false;
        Sesion.AS_T_TOTALES = false;
        MenuElmento("AS_T_HISTORIAL").Checked = false;
        Sesion.AS_T_HISTORIAL = false;
        MenuElmento("AS_T_SALDO").Checked = true;
        Sesion.AS_T_SALDO = true;
        MenuElmento("AS_T_FALTAS").Checked = false;
        Sesion.AS_T_FALTAS = false;
    }
    void AS_T_FALTAS()
    {
        MenuElmento("AS_T_TOTALES").Checked = false;
        Sesion.AS_T_TOTALES = false;
        MenuElmento("AS_T_HISTORIAL").Checked = false;
        Sesion.AS_T_HISTORIAL = false;
        MenuElmento("AS_T_SALDO").Checked = false;
        Sesion.AS_T_SALDO = false;
        MenuElmento("AS_T_FALTAS").Checked = true;
        Sesion.AS_T_FALTAS = true;
    }
    /// <summary>
    /// Verifica que elemento esta seleccionado
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Menu_MenuItemChecked(object sender, Infragistics.WebUI.UltraWebNavigator.WebMenuItemCheckedEventArgs e)
    {
        Infragistics.WebUI.UltraWebNavigator.Item Padre = e.Item.Parent;
        if (Padre != null)
            Muestra(Padre.Tag.ToString());
        string Tag = e.Item.Tag.ToString();
        Muestra(Tag);
        ActualizaDatos(true);
        GuardaAs();
        ValidaPerfilesMenu();
    }
    int ObtenNoDia(string ColumnaNombre)
    {
        if ((ColumnaNombre.Length > 12 && ColumnaNombre.Substring(0, 12) == "ASISTENCIA_D") ||
            (ColumnaNombre.Length > 7 && ColumnaNombre.Substring(0, 7) == "TURNO_D") ||
            (ColumnaNombre.Length > 3 && ColumnaNombre.Substring(0, 3) == "DIA")
            )
        {
            int Sumar = 0;
            if (ColumnaNombre.Length > 12)
                Sumar = Convert.ToInt32(ColumnaNombre.Substring(12));
            else
                if (ColumnaNombre.Length > 7)
                    Sumar = Convert.ToInt32(ColumnaNombre.Substring(7));
                else
                    Sumar = Convert.ToInt32(ColumnaNombre.Substring(3));
            DateTime FInicial = CeC.Convierte2DateTime(FechaInicial.Value);
            if (FInicial.AddDays(Sumar).Year != FInicial.Year)
                if (FInicial.Year % 4 != 0)
                    Sumar++;
            return Sumar;
        }
        return -1;
    }

    bool PuedeModificar()
    {
        if (CeC.ExisteEnSeparador(Sesion.ConfiguraSuscripcion.UsuariosIdsModificanFechasBloqueadas, Sesion.USUARIO_ID.ToString(), ","))
            return true;
        int Numero_Resgistos = Grid.Rows.Count;
        string PersonasDiarioID = "";

        for (int i = 0; i < Numero_Resgistos; i++)
        {
            if (Grid.Rows[i].Selected && (MenuElmento("AS_PREDETERMINADO").Checked || MenuElmento("AS_T_HISTORIAL").Checked))
            {
                int Persona_Diario_ID = Convert.ToInt32(Grid.Rows[i].DataKey);
                PersonasDiarioID = CeC.AgregaSeparador(PersonasDiarioID, Persona_Diario_ID.ToString(), ",");
            }

            for (int Cont = 0; Cont < Grid.Rows[i].Cells.Count; Cont++)
            {
                if (Grid.Rows[i].Selected || Grid.Rows[i].Cells[Cont].Selected)
                {
                    int Persona_Diario_ID = Convert.ToInt32(Grid.Rows[i].DataKey);
                    string Columna = Grid.Rows[i].Cells[Cont].Column.Key;

                    if (Columna == "TURNO_NOMBRE")
                    {
                    }
                    else
                    {
                        int DiaNo = ObtenNoDia(Columna);
                        if (DiaNo >= 0)
                            PersonasDiarioID = CeC.AgregaSeparador(PersonasDiarioID, Convert.ToString(Persona_Diario_ID + DiaNo), ",");
                    }
                }
            }
        }
        try
        {
            if (MenuElmento("AS_T_HISTORIAL").Checked)
                return CeC_Periodos.PuedeModificarAlmacenInc(PersonasDiarioID);

            return CeC_Periodos.PuedeModificar(PersonasDiarioID);
            //if(!Ret)

        }
        catch { }
        return false;
    }
    /// <summary>
    /// Asigna el Horario a las filas seleccionadas
    /// </summary>
    /// <param name="Tag">Horario que se va a asignar</param>
    void AsignaHorario(string Tag)
    {
        int HorarioID = Convert.ToInt32(Tag);
        int Numero_Resgistos = Grid.Rows.Count;
        if (!PuedeModificar())
            return;
        CeC_Asistencias.LimpiaUltimoReporteQryHash(Sesion);
        string PersonasDiariosIDS = "";
        for (int i = 0; i < Numero_Resgistos; i++)
        {
            if (Grid.Rows[i].Selected)
            {

                int Persona_Diario_ID = Convert.ToInt32(Grid.Rows[i].DataKey);
                if (MenuElmento("AS_PREDETERMINADO").Checked)
                {
                    PersonasDiariosIDS = CeC.AgregaSeparador(PersonasDiariosIDS, Convert.ToString(Persona_Diario_ID), ",");
                }
                else
                {

                    foreach (Infragistics.WebUI.UltraWebGrid.UltraGridCell Cell in Grid.Rows[i].Cells)
                    {
                        string Columna = Cell.Column.Key;
                        if (Columna == "TURNO_NOMBRE")
                        {
                            CeC_Turnos.AsignaHorarioPred(CeC_Asistencias.ObtenPersonaID(Persona_Diario_ID), HorarioID, Sesion.SESION_ID);
                        }
                        else
                        {
                            int DiaNo = ObtenNoDia(Columna);
                            if (DiaNo >= 0)
                                PersonasDiariosIDS = CeC.AgregaSeparador(PersonasDiariosIDS, Convert.ToString(Persona_Diario_ID + DiaNo), ",");
                        }
                    }
                }
            }
            for (int Cont = 0; Cont < Grid.Rows[i].Cells.Count; Cont++)
            {
                if (Grid.Rows[i].Cells[Cont].Selected)
                {
                    int Persona_Diario_ID = Convert.ToInt32(Grid.Rows[i].DataKey);
                    string Columna = Grid.Rows[i].Cells[Cont].Column.Key;

                    if (Columna == "TURNO_NOMBRE")
                    {
                        CeC_Turnos.AsignaHorarioPred(CeC_Asistencias.ObtenPersonaID(Persona_Diario_ID), HorarioID, Sesion.SESION_ID);
                    }
                    else
                    {
                        int DiaNo = ObtenNoDia(Columna);
                        if (DiaNo >= 0)
                            PersonasDiariosIDS = CeC.AgregaSeparador(PersonasDiariosIDS, Convert.ToString(Persona_Diario_ID + DiaNo), ",");
                    }
                }
            }
        }
        if (PersonasDiariosIDS.Length > 0)
        {
            CeC_Turnos.AsignaHorario(PersonasDiariosIDS, HorarioID, Sesion);
        }
    }
    /// <summary>
    /// Asigna el horario predeterminado a las filas seleccionadas
    /// </summary>
    /// <param name="Tag">Horario a asignar</param>
    void AsignaHorarioPred(string Tag)
    {
        int HorarioID = Convert.ToInt32(Tag);
        CeC_Asistencias.LimpiaUltimoReporteQryHash(Sesion);
        if (Persona_ID > 0)
        {
            CeC_Turnos.AsignaHorarioPred(HorarioID, Persona_ID, Agrupacion, Sesion.USUARIO_ID, Sesion.SESION_ID);
            LblTurno.Text = CeC_Campos.ObtenValorCampo(Persona_ID, "TURNO_ID");
        }
        else
        {
            int Numero_Resgistos = Grid.Rows.Count;
            string PersonasDiarioID = "";

            for (int i = 0; i < Numero_Resgistos; i++)
            {
                if (Grid.Rows[i].Selected && MenuElmento("AS_PREDETERMINADO").Checked)
                {
                    int Persona_Diario_ID = Convert.ToInt32(Grid.Rows[i].DataKey);
                    PersonasDiarioID = CeC.AgregaSeparador(PersonasDiarioID, Persona_Diario_ID.ToString(), ",");
                    continue;
                }
                for (int Cont = 0; Cont < Grid.Rows[i].Cells.Count; Cont++)
                {
                    if (Grid.Rows[i].Selected || Grid.Rows[i].Cells[Cont].Selected)
                    {
                        int Persona_Diario_ID = Convert.ToInt32(Grid.Rows[i].DataKey);
                        string Columna = Grid.Rows[i].Cells[Cont].Column.Key;

                        if (Columna == "TURNO_NOMBRE")
                        {
                            PersonasDiarioID = CeC.AgregaSeparador(PersonasDiarioID, Persona_Diario_ID.ToString(), ",");
                            break;
                        }
                        else
                        {
                            int DiaNo = ObtenNoDia(Columna);
                            if (DiaNo >= 0)
                            {
                                PersonasDiarioID = CeC.AgregaSeparador(PersonasDiarioID, Convert.ToString(Persona_Diario_ID + DiaNo), ",");
                                break;
                            }
                        }
                    }
                }
            }
            if (PersonasDiarioID.Length > 0)
            {
                string PersonasIDS = CeC_Asistencias.ObtenPersonasIDs(PersonasDiarioID);
                CeC_Turnos.AsignaHorarioPred(HorarioID, PersonasIDS, Agrupacion, Sesion.USUARIO_ID, Sesion.SESION_ID);
            }
        }
    }
    /// <summary>
    /// Borra el Inventario de Incidencias
    /// </summary>
    /// <returns></returns>
    string BorrarInventario()
    {
        if (!PuedeModificar())
            return "";
        int Numero_Resgistos = Grid.Rows.Count;
        string Errores = "";


        string sAlmacenIncIDs = "";

        for (int i = 0; i < Numero_Resgistos; i++)
        {
            if (Grid.Rows[i].Selected)
            {
                int AlmacenIncID = Convert.ToInt32(Grid.Rows[i].DataKey);
                sAlmacenIncIDs = CeC.AgregaSeparador(sAlmacenIncIDs, AlmacenIncID.ToString(), ",");
            }
        }
        return CeC_IncidenciasInventario.CorrigeMovimientos(sAlmacenIncIDs, Sesion.SESION_ID, Sesion.SUSCRIPCION_ID);

    }
    /// <summary>
    /// Asigna la justificacion a las personas seleccionadas
    /// </summary>
    /// <param name="Tag">Justificación a asignar</param>
    /// <returns></returns>
    string AsignaJustificacion(string Tag)
    {
        if (!PuedeModificar())
            return "";
        CeC_Asistencias.LimpiaUltimoReporteQryHash(Sesion);
        int Numero_Resgistos = Grid.Rows.Count;
        string Errores = "";


        string sPersonasDiarioIDS = "";

        for (int i = 0; i < Numero_Resgistos; i++)
        {
            if (Grid.Rows[i].Selected)
            {
                int Persona_Diario_ID = Convert.ToInt32(Grid.Rows[i].DataKey);
                if (MenuElmento("AS_PREDETERMINADO").Checked)
                {
                    sPersonasDiarioIDS = CeC.AgregaSeparador(sPersonasDiarioIDS, Persona_Diario_ID.ToString(), ",");
                }
                else
                {

                    foreach (Infragistics.WebUI.UltraWebGrid.UltraGridCell Cell in Grid.Rows[i].Cells)
                    {
                        int DiaNo = ObtenNoDia(Cell.Column.Key);
                        if (DiaNo >= 0)
                            sPersonasDiarioIDS = CeC.AgregaSeparador(sPersonasDiarioIDS, Convert.ToString(Persona_Diario_ID + DiaNo), ",");
                    }
                }

            }

            for (int Cont = 0; Cont < Grid.Rows[i].Cells.Count; Cont++)
            {
                if (Grid.Rows[i].Cells[Cont].Selected)
                {
                    int Persona_Diario_ID = Convert.ToInt32(Grid.Rows[i].DataKey);
                    string Columna = Grid.Rows[i].Cells[Cont].Column.Key;
                    int DiaNo = ObtenNoDia(Columna);
                    if (DiaNo >= 0)
                    {
                        int Persona_Diario_Real = Persona_Diario_ID + DiaNo;
                        sPersonasDiarioIDS = CeC.AgregaSeparador(sPersonasDiarioIDS, Persona_Diario_Real.ToString(), ",");
                    }
                }
            }

        }

        int TipoIncidenciaID = Convert.ToInt32(Tag);

        if (CeC_Usuarios.ObtenEsEmpleado(Sesion.USUARIO_ID))
        {
            if (sPersonasDiarioIDS.Length > 0)
            {
                Sesion.Redirige("WF_SolicitudNueva.aspx", TipoIncidenciaID + "@" + sPersonasDiarioIDS);

            }
            return "";
        }
        int IncidenciaID = 0;
        string PendientesPorValidar = "";
        bool TieneIncidenciaRegla = CeC_IncidenciasRegla.TieneIncidenciaRegla(TipoIncidenciaID);
        DataSet DSEC_TIPO_INCIDENCIAS_R = null;
        if (TieneIncidenciaRegla)
            DSEC_TIPO_INCIDENCIAS_R = CeC_IncidenciasRegla.ObtenEC_TIPO_INCIDENCIAS_R(TipoIncidenciaID);

        if (TipoIncidenciaID > 0 && !TieneIncidenciaRegla)
        {
            IncidenciaID = Cec_Incidencias.CreaIncidencia(TipoIncidenciaID, "", Sesion.SESION_ID);
            if (IncidenciaID <= 0)
            {
                return "";
            }
        }
        else
            IncidenciaID = 0;

        string[] asPersonasDiarioIDS = CeC.ObtenArregoSeparador(sPersonasDiarioIDS, ",");

        if (asPersonasDiarioIDS == null)
            return "";

        foreach (string sPersonaDiarioID in asPersonasDiarioIDS)
        {
            int TipoIncidencia_R_ID = 0;
            int iPersonaDiairioID = CeC.Convierte2Int(sPersonaDiarioID);
            if (TieneIncidenciaRegla)
            {
                TipoIncidencia_R_ID = CeC_IncidenciasRegla.ObtenTipo_Incidencia_R_ID(DSEC_TIPO_INCIDENCIAS_R, iPersonaDiairioID);
            }
            if (TipoIncidencia_R_ID <= 0)
            {
                if (Cec_Incidencias.AsignaIncidencia(iPersonaDiairioID, IncidenciaID, Sesion.SESION_ID) <= 0)
                {
                    Errores += " No se pudo justificar a " + iPersonaDiairioID + " (error al asignar la incidencia)\n";
                    continue;
                }
            }
            else
            {
                PendientesPorValidar = CeC.AgregaSeparador(PendientesPorValidar, TipoIncidencia_R_ID.ToString(), ",");
                PendientesPorValidar = CeC.AgregaSeparador(PendientesPorValidar, sPersonaDiarioID, ",");
            }
        }

        if (PendientesPorValidar.Length > 0)
        {
            string PorJustificar = "";
            string TipoIncidenciasRIDs = CeC_IncidenciasRegla.ObtenIncidenciaReglasIDs(PendientesPorValidar);
            string[] sTipoIncidenciasRIDs = TipoIncidenciasRIDs.Split(new char[] { ',' });
            foreach (string TipoIncidenciaRID in sTipoIncidenciasRIDs)
            {
                string PersonasDiarioIDS = Cec_Incidencias.ObtenPersonasDiarioIDs(PendientesPorValidar, Convert.ToInt32(TipoIncidenciaRID));
                PorJustificar = CeC.AgregaSeparador(PorJustificar, TipoIncidenciaRID, "@");
                PorJustificar = CeC.AgregaSeparador(PorJustificar, PersonasDiarioIDS, "|");

            }
            Sesion.Redirige("WF_IncidenciasNueva.aspx", PorJustificar);
            return TipoIncidenciasRIDs;
        }
        if (Errores.Length > 0)
            CIsLog2.AgregaError(Errores);
        return "";
    }
    /// <summary>
    /// Captura a que elemento del Menu se le dió click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Menu_MenuItemClicked(object sender, Infragistics.WebUI.UltraWebNavigator.WebMenuItemEventArgs e)
    {
        string Tag = null;
        try
        {
            Infragistics.WebUI.UltraWebNavigator.Item Padre = e.Item.Parent;
            if (e.Item.Tag != null)
            {
                Tag = e.Item.Tag.ToString();
            }
            else
            {
                Tag = e.Item.ToString();
            }
            // Me ayuda a guardar la etiqueta que se selecciono.
            switch (Tag)
            { 
                case "EXPORTAR":
                case "PDF":
                case "XPS":
                case "TEXTO":
                case "EXCEL":
                case "XPS_VERTICAL":
                case "XPS_HORIZONTAL":
                case "PDF_VERTICAL":
                case "PDF_HORIZONTAL":
                case "DBF":
                case "CSV":
                case "TXT":
                case "FAVORITO_BORRAR":
                case "FAVORITO_COMPARTIR":
                case "FAVORITOS":
                case "F_AGREGAR":
                case "F_SOLO_RETARDOS":
                case "F_SOLO_FALTAS":
                    //AuxTag;
                    break;
                default:
                    Sesion.Terminal_ES = Tag;
                    break;
            }
            
            if (Padre != null)
            {
                string TagPadre = Padre.Tag.ToString();
                if (Tag == "FAVORITO_BORRAR")
                {
                    Sesion.Configura.AsistenciaFavoritos = Sesion.Configura.AsistenciaFavoritos.Replace(TagPadre, "");
                    Padre.Parent.Items.Remove(Padre);
                    return;
                }
                if (Tag == "FAVORITO_COMPARTIR")
                {
                    //DlgCompartirFav.WindowState = Infragistics.Web.UI.LayoutControls.DialogWindowState.Normal;
                    Sesion.EjecutaScript("");
                    /*  confirm("¿Desea compartir sus datos?", function () {
                  window.location.href = 'http://www.ericmmartin.com/projects/simplemodal/';
              });*/
                }
                if (TagPadre == "FAVORITOS")
                {
                    if (Tag.Substring(0, 2) == "F_")
                    {
                        if (Tag == "F_AGREGAR")
                        {
                            DlgAgregarFav.WindowState = Infragistics.Web.UI.LayoutControls.DialogWindowState.Normal;
                            //                        Sesion.EjecutaScript("");

                        }

                        if (Tag == "F_SOLO_RETARDOS")
                            CargaConsulta("@" + Sesion.ConfiguraSuscripcion.AsistenciaSoloRetardos);
                        if (Tag == "F_SOLO_FALTAS")
                            CargaConsulta("@" + Sesion.ConfiguraSuscripcion.AsistenciaSoloFaltas);
                    }
                    else
                    {
                        CargaConsulta(Tag);
                    }
                    return;
                }
                //Muestra(TagPadre);
                if (TagPadre == "AsistenciaMostrarAccesos" || TagPadre == "EXPORTAR")
                {
                    if (Tag != "EXCEL")
                    {
                        Grid.Clear();
                        ObtenAccesosES_Terminal(Sesion.Terminal_ES);
                        ObtenAccesosES_Terminal();
                        //Grid.DataBind();
                        //return;
                    }
                }
                if (TagPadre == "TURNOS")
                    AsignaHorario(Tag);
                if (TagPadre == "TURNOSPRED")
                    AsignaHorarioPred(Tag);
                if (TagPadre == "JUSTIFICACIONES")
                    AsignaJustificacion(Tag);

                if (TagPadre == "EXPORTAR" || TagPadre == "PDF" || TagPadre == "XPS" || TagPadre == "TEXTO")
                {
                    if (Tag == "EXCEL")
                    {
                        Grid.DisplayLayout.Pager.AllowPaging = false;
                        Grid.DisplayLayout.LoadOnDemand = Infragistics.WebUI.UltraWebGrid.LoadOnDemand.NotSet;
                        Grid.DataBind();
                        CeC_Grid.HorasSuma(Grid);
                        GridExcel.WorksheetName = "eClock";
                        GridExcel.DownloadName = "Exportacion.xls";
                        GridExcel.Export(Grid);
                        CIsLog2.AgregaLog("GridExcel.WorksheetName = eClock");
                    }
                    if (Tag == "XPS" || Tag == "XPS_VERTICAL")
                    {
                        Grid.DisplayLayout.Pager.AllowPaging = false;
                        Grid.DisplayLayout.LoadOnDemand = Infragistics.WebUI.UltraWebGrid.LoadOnDemand.NotSet;
                        Grid.DataBind();
                        GridExporter.Format = Reports.Report.FileFormat.XPS;
                        GridExporter.TargetPaperOrientation = Reports.Report.PageOrientation.Portrait;
                        GridExporter.DownloadName = "Exportacion.xps";
                    }
                    if (Tag == "XPS_HORIZONTAL")
                    {
                        Grid.DisplayLayout.Pager.AllowPaging = false;
                        Grid.DisplayLayout.LoadOnDemand = Infragistics.WebUI.UltraWebGrid.LoadOnDemand.NotSet;
                        Grid.DataBind();
                        GridExporter.Format = Reports.Report.FileFormat.XPS;
                        GridExporter.TargetPaperOrientation = Reports.Report.PageOrientation.Landscape;
                        GridExporter.DownloadName = "Exportacion.xps";
                    }

                    if (Tag == "PDF" || Tag == "PDF_VERTICAL")
                    {
                        Grid.DisplayLayout.Pager.AllowPaging = false;
                        Grid.DisplayLayout.LoadOnDemand = Infragistics.WebUI.UltraWebGrid.LoadOnDemand.NotSet;
                        Grid.DataBind();
                        GridExporter.Format = Reports.Report.FileFormat.PDF;
                        GridExporter.TargetPaperOrientation = Reports.Report.PageOrientation.Portrait;
                        GridExporter.DownloadName = "Exportacion.pdf";
                    }
                    if (Tag == "PDF_HORIZONTAL")
                    {
                        Grid.DisplayLayout.Pager.AllowPaging = false;
                        Grid.DisplayLayout.LoadOnDemand = Infragistics.WebUI.UltraWebGrid.LoadOnDemand.NotSet;
                        Grid.DataBind();
                        GridExporter.Format = Reports.Report.FileFormat.PDF;
                        GridExporter.TargetPaperOrientation = Reports.Report.PageOrientation.Landscape;
                        GridExporter.DownloadName = "Exportacion.pdf";
                    }
                    if (Tag == "DBF")
                    {
                        //Grid.DisplayLayout.Pager.AllowPaging = false;
                        //Grid.DisplayLayout.LoadOnDemand = Infragistics.WebUI.UltraWebGrid.LoadOnDemand.NotSet;
                        //Grid.DataBind();
                        //GridExcel.DownloadName = "Exportacion.dbf";
                        //GridExcel.Export(Grid);
                        //string path = @"c:\temp\MyTest.txt";
                        //System.IO.Stream stream = new 
                        Grid.DataBind();
                        GridExporter.Format = Reports.Report.FileFormat.PlainText;
                        GridExporter.DownloadName = "Exportacion.dbf";
                        GridExporter.Export(Grid);
                        //GridExporter.Export(Grid, stream, true);
                    }
                    if (Tag == "PDF" || TagPadre == "PDF" || Tag == "XPS" || TagPadre == "XPS")
                    {
                        //Grid.DataBind();
                        GridExporter.Export(Grid);
                    }
                    if (Tag == "TEXTO" || TagPadre == "TEXTO")
                    {
                        GridExporter.Format = Reports.Report.FileFormat.PlainText;
                        if (Tag == "CSV")
                        {
                            Grid.DisplayLayout.Pager.AllowPaging = false;
                            Grid.DisplayLayout.LoadOnDemand = Infragistics.WebUI.UltraWebGrid.LoadOnDemand.NotSet;
                            Grid.DataBind();
                            GridExporter.DownloadName = "Exportacion.txt";
                        }
                        if (Tag == "TXT")
                        {
                            Grid.DisplayLayout.Pager.AllowPaging = false;
                            Grid.DisplayLayout.LoadOnDemand = Infragistics.WebUI.UltraWebGrid.LoadOnDemand.NotSet;
                            Grid.DataBind();
                            GridExporter.DownloadName = "Exportacion.txt";
                        }
                        Grid.DataBind();
                        GridExporter.Export(Grid);
                    }
                    return;
                }
            }
            else
            {
                if (Tag == "BORRAR_INVENTARIO")
                    BorrarInventario();
            }
            Muestra(Tag);
            ActualizaDatos(true);
            GuardaAs();
            ValidaPerfilesMenu();
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
    }

    protected void BtnAct_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        string Tag = Request.Params.Get("ValorMenuTag");
        string TagPadre = Request.Params.Get("ValorMenuTagPadre");
        if (TagPadre == "AsistenciaMostrarAccesos")
            ObtenAccesosES_Terminal(Tag);
        if (TagPadre == "TURNOS")
            AsignaHorario(Tag);
        if (TagPadre == "TURNOSPRED")
            AsignaHorarioPred(Tag);
        if (TagPadre == "JUSTIFICACIONES")
            AsignaJustificacion(Tag);
        ActualizaDatos(true);
    }
    protected void GridExporter_BeginExport(object sender, Infragistics.WebUI.UltraWebGrid.DocumentExport.DocumentExportEventArgs e)
    {
        ImprimirFirma = MenuElmento("FIRMA").Checked;
        string Empresa = "";
        if (Sesion.ConfiguraSuscripcion.CompaniaNombre != "")
            Empresa = "\n" + Sesion.ConfiguraSuscripcion.CompaniaNombre;
        if (Sesion.AsistenciaMostrarAccesos)
            CeC_Reportes.AplicaFormatoReporte(e, "Accesos", "", "imagenes/Asistencias.png", Sesion);
        if (Sesion.AsistenciaMostrarES)
            CeC_Reportes.AplicaFormatoReporte(e, "Entradas / Salidas", "", "imagenes/Asistencias.png", Sesion);
        if (Sesion.AsistenciaGraficar)
            CeC_Reportes.AplicaFormatoReporte(e, "Asistencia Grafica", "", "imagenes/Asistencias.png", Sesion);
        if (Sesion.AsistenciaTotales)
            CeC_Reportes.AplicaFormatoReporte(e, "Totales de Asistencia", "", "imagenes/Asistencias.png", Sesion);
        if (Sesion.AsistenciaMostrar)
            CeC_Reportes.AplicaFormatoReporte(e, "Asistencias", "", "imagenes/Asistencias.png", Sesion);
        if (ImprimirFirma)
        {
            try { Grid.Columns.FromKey("PERSONA_NOMBRE").Hidden = true; }
            catch { }
            try { Grid.Columns.FromKey("PERSONA_LINK_ID").Hidden = true; }
            catch { }
            try { Grid.Columns.FromKey("AGRUPACION_NOMBRE").Hidden = true; }
            catch { }
        }
    }

    public void CargaFavorito(Infragistics.WebUI.UltraWebNavigator.Item MenuFavoritos, string Nombre, string Formato)
    {
        try
        {
            Infragistics.WebUI.UltraWebNavigator.Item Menu = MenuFavoritos.Items.Add(Nombre, Formato);
            Menu.Items.Add("Compartir", "FAVORITO_COMPARTIR");
            Menu.Items.Add("Borrar", "FAVORITO_BORRAR");
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
    }

    public void CargaFavoritos()
    {
        Infragistics.WebUI.UltraWebNavigator.Item MenuFavoritos = MenuElmento("FAVORITOS");
        CeC_Config Cfg = new CeC_Config(Sesion.USUARIO_ID);
        string Favoritos = Cfg.AsistenciaFavoritos;
        string[] sFavoritos = Favoritos.Split(new char[] { '|' });
        foreach (string Favorito in sFavoritos)
        {
            if (MenuElmento(Favorito) == null)
            {
                string[] Elementos = Favorito.Split(new char[] { '@' });
                if (Favorito.Length > 0)
                    CargaFavorito(MenuFavoritos, Elementos[0], Favorito);
            }
        }
    }

    protected void NuevoFav()
    {
        GuardaConsulta(Tbx_NombreFav.Text);
        CargaFavoritos();
    }

    protected void BtnCancelar0_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        DlgAgregarFav.WindowState = Infragistics.Web.UI.LayoutControls.DialogWindowState.Hidden;
    }

    void AgregarPieFirma(ITable Tabla)
    {
        if (!ImprimirFirma)
            return;
        if (LeyendaFirma == "")
            LeyendaFirma = Sesion.ConfiguraSuscripcion.LEYENDA_REPORTE_ASISTENCIA;
        ITableRow row = Tabla.AddRow();
        ITableCell cell = row.AddCell();
        cell.Width = new Reports.Report.RelativeWidth(100);
        IText cellText = cell.AddText();


        Reports.Report.Text.Style fontStyle = new Reports.Report.Text.Style(Fonts.Tahoma, Brushes.Black);

        cellText.Alignment.Horizontal = Reports.Report.Alignment.Center;

        cellText.AddLineBreak();
        cellText.AddLineBreak();
        //cellText.AddContent("\n", fontStyle);
        //cellText.AddContent("\n", fontStyle);
        cellText.AddContent("________________________________________________\n", fontStyle);
        cellText.AddContent(ExportaNombre, fontStyle);
        cellText.AddLineBreak();
        cellText.AddContent(LeyendaFirma, fontStyle);


        // style the new row
        cell.Background = new Reports.Report.Background(Reports.Graphics.Brushes.AliceBlue);
        cell.Paddings.Top = 2;
        cell.Paddings.Bottom = 2;
        cell.Borders.Bottom = new Reports.Report.Border(Reports.Graphics.Pens.Gray);
        if (EmpleadosXHoja <= 0)
            EmpleadosXHoja = Sesion.ConfiguraSuscripcion.EmpleadosXHoja;

        if (NoEmpleado % EmpleadosXHoja == EmpleadosXHoja - 1)
            Tabla.AddRow().AddCell().AddPageBreak();
        NoEmpleado++;

    }
    public string ObtenPersonaNombre(Infragistics.WebUI.UltraWebGrid.UltraGridRow Fila)
    {
        string Nombre = "";
        try
        {
            Nombre = Convert.ToString(Fila.Cells.FromKey("PERSONA_NOMBRE").Value);
        }
        catch
        {
            int Persona_ID = Convert.ToInt32(Fila.Cells.FromKey("PERSONA_ID").Value);
            return CeC_BD.ObtenPersonaNombre(Persona_ID);
        }
        return Nombre;
    }
    protected void GridExporter_RowExporting(object sender, Infragistics.WebUI.UltraWebGrid.DocumentExport.RowExportingEventArgs e)
    {
        try
        {
            if (!ImprimirFirma)
                return;
            int Persona_Link_ID = Convert.ToInt32(e.GridRow.Cells.FromKey("PERSONA_LINK_ID").Value);
            if (Persona_Link_ID_ANT != Persona_Link_ID)
            {

                if (Persona_Link_ID_ANT != 0)
                {
                    AgregarPieFirma(e.ContainingTable);

                    // e.ContainingTable.AddRow().AddCell().AddQuickText
                }
                Persona_Link_ID_ANT = Persona_Link_ID;

                {
                    // Add an extra row element to the document
                    ITableRow row = e.ContainingTable.AddRow();
                    ITableCell cell = row.AddCell();
                    cell.Width = new Reports.Report.RelativeWidth(100);
                    IText cellText = cell.AddText();


                    Reports.Report.Text.Style fontStyle = new Reports.Report.Text.Style(Fonts.Tahoma, Brushes.Black);
                    ExportaNombre = ObtenPersonaNombre(e.GridRow);
                    cellText.AddContent("Empleado: " + ExportaNombre + " (" + Persona_Link_ID + ")", fontStyle);

                    // style the new row
                    cell.Background = new Reports.Report.Background(Reports.Graphics.Brushes.AliceBlue);
                    cell.Paddings.Top = 2;
                    cell.Paddings.Bottom = 2;
                    cell.Borders.Bottom = new Reports.Report.Border(Reports.Graphics.Pens.Gray);
                }
            }

        }
        catch { }
    }
    protected void GridExporter_RowExported(object sender, Infragistics.WebUI.UltraWebGrid.DocumentExport.RowExportedEventArgs e)
    {
        if (e.GridRow.ParentCollection.Count <= e.GridRow.Index + 1)
        {
            AgregarPieFirma(e.ContainingTable);
        }

    }
    protected void BtnAceptar1_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        DlgCompartirFav.WindowState = Infragistics.Web.UI.LayoutControls.DialogWindowState.Hidden;
    }
    protected void Grid_SortColumn(object sender, Infragistics.WebUI.UltraWebGrid.SortColumnEventArgs e)
    {
        /* Infragistics.WebUI.UltraWebGrid.UltraGridColumn Columna = Grid.Bands[0].Columns[e.ColumnNo];
         if(Columna.SortIndicator == Infragistics.WebUI.UltraWebGrid.SortIndicator.Ascending)
             Columna.SortIndicator = Infragistics.WebUI.UltraWebGrid.SortIndicator.Descending;
         else
             Columna.SortIndicator = Infragistics.WebUI.UltraWebGrid.SortIndicator.Ascending;
        
         */
        GuardaOrden();
    }

    private void GuardaOrden()
    {
        try
        {
            //            Grid.Bands[0].SortedColumns[0].SortIndicator
            //Sesion.AS_CAMPO_ORDEN = Grid.Bands[e.BandNo].Columns[e.ColumnNo].Key;
            string Orden = "";
            Infragistics.WebUI.UltraWebGrid.SortedColsCollection sortedCols = Grid.Bands[0].SortedColumns;
            for (int i = 0; i < sortedCols.Count; i++)
            {
                if (sortedCols[i].SortIndicator == Infragistics.WebUI.UltraWebGrid.SortIndicator.Ascending)
                    Orden = CeC.AgregaSeparador(Orden, "A|" + sortedCols[i].BaseColumnName, ",");
                if (sortedCols[i].SortIndicator == Infragistics.WebUI.UltraWebGrid.SortIndicator.Descending)
                    Orden = CeC.AgregaSeparador(Orden, "D|" + sortedCols[i].BaseColumnName, ",");
            }
            GridOrden = Orden;
        }
        catch { }
    }

    private void CargaOrden()
    {
        if (!Ordenar)
            return;
        CargaPropColumnas();
        string[] Campos = CeC.ObtenArregoSeparador(GridOrden, ",");
        foreach (string Campo in Campos)
        {
            try
            {
                string[] Valores = CeC.ObtenArregoSeparador(Campo, "|");
                Infragistics.WebUI.UltraWebGrid.UltraGridColumn Columna = Grid.Bands[0].Columns.FromKey(Valores[1]);
                if (Valores[0] == "A")
                    Columna.SortIndicator = Infragistics.WebUI.UltraWebGrid.SortIndicator.Ascending;
                if (Valores[0] == "D")
                    Columna.SortIndicator = Infragistics.WebUI.UltraWebGrid.SortIndicator.Descending;
                Grid.Bands[0].SortedColumns.Add(Columna);
            }
            catch
            {
            }
        }
    }

    private void CargaPropColumnas()
    {
        if (!Ordenar)
            return;
        string[] Campos = CeC.ObtenArregoSeparador(GridAgrupadas, ",");
        foreach (string Campo in Campos)
        {
            try
            {
                Infragistics.WebUI.UltraWebGrid.UltraGridColumn Columna = Grid.Bands[0].Columns.FromKey(Campo);
                Columna.IsGroupByColumn = true;
            }
            catch { }
        }
        Campos = CeC.ObtenArregoSeparador(GridStaticas, ",");
        foreach (string Campo in Campos)
        {
            try
            {
                Infragistics.WebUI.UltraWebGrid.UltraGridColumn Columna = Grid.Bands[0].Columns.FromKey(Campo);
                Columna.Header.Fixed = true;
            }
            catch { }
        }
    }
    private void GuardaPropColumnas()
    {
        string Agrupadas = "";
        string Estaticas = "";
        for (int i = 0; i < Grid.Bands[0].Columns.Count; i++)
        {
            Infragistics.WebUI.UltraWebGrid.UltraGridColumn Columna = Grid.Bands[0].Columns[i];
            if (Columna.Header.Fixed)
                Estaticas = CeC.AgregaSeparador(Estaticas, Columna.Key, ",");
            if (Columna.IsGroupByColumn)
                Agrupadas = CeC.AgregaSeparador(Agrupadas, Columna.Key, ",");

        }
        GridStaticas = Estaticas;
        GridAgrupadas = Agrupadas;
    }

    /// <summary>
    /// Obtiene un DataTable con los datos de Entrada y Salida por Terminal
    /// </summary>
    /// <param name="DS">DataSet con los datos de todos los Accesos</param>
    /// <returns></returns>
    protected DataTable AccesosES(DataSet DS)
    {
        int PersonaLinkID_Ant = 0;
        // Se va agregar aqui codigo para solucionar problemas con los tipos de acceso. Se debe hacer un parche para que nose ejecute siempre
        //CeC_BD.EjecutaComando("");
        //CeC_BD.EjecutaComando("");
        string Terminal_AgrupacionAnt = "Ninguna";
        DS_AsistenciasEmp.AccesosESAutDataTable DTAccesos = new DS_AsistenciasEmp.AccesosESAutDataTable();
        DS_AsistenciasEmp.AccesosESAutRow DRAccesos = null;
        foreach (DataRow DR in DS.Tables[0].Rows)
        {
            int TipoAccesoID = CeC.Convierte2Int(DR["TIPO_ACCESO_ID"]);
            int PersonaLinkID = CeC.Convierte2Int(DR["PERSONA_LINK_ID"]);
            string PersonaNombre = CeC.Convierte2String(DR["PERSONA_NOMBRE"]);
            string TerminalNombre = CeC.Convierte2String(DR["TERMINAL_NOMBRE"]);
            string Terminal_Agrupacion = CeC.Convierte2String(DR["TERMINAL_AGRUPACION"]);
            DateTime FechaHora = CeC.Convierte2DateTime(DR["ACCESO_FECHAHORA"]);
            bool Nuevo = false;
            bool EsEntrada = false;

            if (Terminal_AgrupacionAnt != Terminal_Agrupacion || PersonaLinkID != PersonaLinkID_Ant)
                Nuevo = true;
            if (TipoAccesoID == 2 || TipoAccesoID == 0 || TipoAccesoID == 1)
            {
                Nuevo = true;
                EsEntrada = true;
            }
            if (!Nuevo && PersonaLinkID == PersonaLinkID_Ant)
            {
                if (!DRAccesos.IsTERMINAL_NOMBRE_SALIDANull())
                    Nuevo = true;
            }

            if (Nuevo)
            {
                DRAccesos = DTAccesos.NewAccesosESAutRow();
                DRAccesos.PERSONA_LINK_ID = CeC.Convierte2String(PersonaLinkID);
                DRAccesos.PERSONA_NOMBRE = PersonaNombre;
                DTAccesos.AddAccesosESAutRow(DRAccesos);
            }

            if (EsEntrada)
            {
                DRAccesos.TERMINAL_NOMBRE_ENTRADA = TerminalNombre;
                DRAccesos.FECHAHORA_ENTRADA = CeC.Convierte2String(FechaHora);
            }
            else
            {
                DRAccesos.TERMINAL_NOMBRE_SALIDA = TerminalNombre;
                DRAccesos.FECHAHORA_SALIDA = CeC.Convierte2String(FechaHora);
            }
            PersonaLinkID_Ant = PersonaLinkID;
            Terminal_AgrupacionAnt = Terminal_Agrupacion;
        }
        return DTAccesos;
    }
    #region Variables de Sesión
    /// <summary>
    /// obtiene o establece el listado de columnas separadas por coma del orden aplicado
    /// ej. A|Campo1,D|Campo2
    /// usar A para Acendente y B para descendente
    /// </summary>
    private string GridOrden
    {
        get { return Sesion.Lee("Asistencia_GridOrden", ""); }
        set { Sesion.Guarda("Asistencia_GridOrden", value); }
    }
    /// <summary>
    /// obtiene o establece el listado de columnas estaticas separadas por coma del orden aplicado
    /// ej. Campo1,Campo2
    /// </summary>
    private string GridStaticas
    {
        get { return Sesion.Lee("Asistencia_GridStaticas", ""); }
        set { Sesion.Guarda("Asistencia_GridStaticas", value); }
    }

    /// <summary>
    /// obtiene o establece el listado de columnas Agrupadas separadas por coma del orden aplicado
    /// ej. Campo1,Campo2
    /// </summary>
    private string GridAgrupadas
    {
        get { return Sesion.Lee("Asistencia_GridAgrupadas", ""); }
        set { Sesion.Guarda("Asistencia_GridAgrupadas", value); }
    }
    #endregion


    protected void Grid_GroupColumn(object sender, Infragistics.WebUI.UltraWebGrid.ColumnEventArgs e)
    {
        GuardaPropColumnas();
    }
    protected void Grid_UnGroupColumn(object sender, Infragistics.WebUI.UltraWebGrid.ColumnEventArgs e)
    {
        GuardaPropColumnas();
    }
    protected void Grid_ColumnMove(object sender, Infragistics.WebUI.UltraWebGrid.ColumnEventArgs e)
    {
        GuardaPropColumnas();
    }

    protected void Tbx_NoDias_ValueChanged(object sender, Infragistics.Web.UI.EditorControls.TextEditorValueChangedEventArgs e)
    {
        FechaFinal.Value = CeC.Convierte2DateTime(FechaInicial.Value).AddDays(Tbx_NoDias.ValueInt);
        ActualizaDatos(true);
    }
    /// <summary>
    /// Agrega al final del archivo los totales.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridExporter_EndExport(object sender, Infragistics.WebUI.UltraWebGrid.DocumentExport.EndExportEventArgs e)
    {
        try
        {
            Infragistics.Documents.Reports.Report.Section.ISection Seccion = e.Section;
            //
            // Add the band to the section. 
            //
            Infragistics.Documents.Reports.Report.Band.IBand band = Seccion.AddBand();
            //
            // Add a header to the band.
            //
            // Retrieve a reference to the band's header
            // and assign it to the bandHeader object.
            Infragistics.Documents.Reports.Report.Band.IBandHeader bandHeader = band.Header;
            // Cause the header to repeat on every page.
            bandHeader.Repeat = true;
            // The height of the header will be 5% of
            // the page's height. 
            bandHeader.Height = new Infragistics.Documents.Reports.Report.FixedHeight(15);
            // The header's background color will be light blue.
            bandHeader.Background =
              new Infragistics.Documents.Reports.Report.Background
              (Infragistics.Documents.Reports.Graphics.Colors.White);
            // Set the horizontal and vertical alignment of the header.
            bandHeader.Alignment =
              new Infragistics.Documents.Reports.Report.ContentAlignment
              (
                Infragistics.Documents.Reports.Report.Alignment.Left,
                Infragistics.Documents.Reports.Report.Alignment.Bottom
              );
            // The bottom border of the band will be a 
            // solid, dark blue line.
            bandHeader.Borders.Bottom =
              new Infragistics.Documents.Reports.Report.Border
              (Infragistics.Documents.Reports.Graphics.Pens.LightGray);
            // Add 5 pixels of padding around the left and right edges.
            bandHeader.Paddings.Horizontal = 5;
            // Add textual content to the header.
            Infragistics.Documents.Reports.Report.Text.IText bandHeaderText =
              bandHeader.AddText();
            bandHeaderText.Width = new Infragistics.Documents.Reports.Report.FixedWidth(20);
            //bandHeaderText.AddContent("Totales: \n");
            //
            // Add content to the band.     

            Infragistics.Documents.Reports.Report.Text.IText bandText;
            bandText = band.AddText();

            for (int banda = 0; banda < Grid.Bands.Count; banda++)
            {
                for (int columna = 0; columna < Grid.Bands[banda].Columns.Count; columna++)
                {
                    switch (Grid.Bands[banda].Columns[columna].Header.Caption)
                    {
                        case "PERSONA_DIARIO_TDE":
                        case "Tiempo de Retardo":
                        case "PERSONA_DIARIO_TE":
                        case "Tiempo de Estancia":
                        case "PERSONA_DIARIO_TC":
                        case "Tiempo de Comida":
                        case "PERSONA_DIARIO_TES":
                        case "Horas X Trabajar":
                        case "PERSONA_DIARIO_TT":
                        case "Tiempo Trabajado":
                        case "PERSONA_D_HE_SIS":
                        case "HE Reales":
                        case "PERSONA_D_HE_CAL":
                        case "HE Calculadas":
                        case "PERSONA_D_HE_APL":
                        case "HE a Aplicar":
                        case "PERSONA_D_HE_SIS_A":
                        case "PERSONA_D_HE_SIS_D":
                        case "PERSONA_D_HE_FH":
                        case "Confirmacion HE":
                        case "PERSONA_D_HE_SIMPLE":
                        case "HE Simples":
                        case "PERSONA_D_HE_DOBLE":
                        case "HE Dobles":
                        case "PERSONA_D_HE_TRIPLE":
                        case "HE Triples":
                            try
                            {
                                DS_Campos.EC_CAMPOSRow Campo = CeC_Campos.Obten_Campo(Grid.Bands[banda].Columns[columna].Key);
                                if (Campo != null)
                                {
                                    string cabeceraColumna = Grid.Bands[banda].Columns[columna].Header.Caption.ToString();
                                    Grid.Bands[banda].Columns[columna].Header.Caption = Campo.CAMPO_ETIQUETA;
                                    CeC_Grid.HorasSuma(Grid);
                                    bandText.AddContent("Total " + cabeceraColumna + ": " + Grid.Columns[columna].FooterText + "\n");
                                }
                            }
                            catch (Exception ex)
                            {
                                CIsLog2.AgregaError(ex);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

            bandText.Paddings.All = 5;
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError("WF_Asistencias.GridExporter_EndExport", ex);
        }
    }
}
