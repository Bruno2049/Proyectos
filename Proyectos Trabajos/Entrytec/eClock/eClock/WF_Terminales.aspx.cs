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
using Infragistics.WebUI.UltraWebGrid.ExcelExport;
using Report = Infragistics.Documents.Reports.Report;


/// <summary>
/// Descripción breve de WF_Terminales.
/// </summary>
public partial class WF_Terminales : System.Web.UI.Page
{
    protected DS_Terminales dS_Terminales1;
    CeC_Sesion Sesion;


    private void ControlVisible(bool Caso)
    {
        Uwg_EC_TERMINALES.Visible = Caso;
        WIBtn_Nuevo.Visible = Caso;
        WIBtn_Borrar.Visible = Caso;
        WIBtn_Editar.Visible = Caso;
        Chb_EC_TERMINALES.Visible = Caso;
    }

    private void Habilitarcontroles()
    {
        if (!Sesion.TienePermiso(eClock.CEC_RESTRICCIONES.S0Terminales0Nuevo))
            WIBtn_Nuevo.Visible = false;

        if (!Sesion.TienePermiso(eClock.CEC_RESTRICCIONES.S0Terminales0Editar))
            WIBtn_Editar.Visible = false;

        if (!Sesion.TienePermiso(eClock.CEC_RESTRICCIONES.S0Terminales0Borrar))
            WIBtn_Borrar.Visible = false;

        if (!Sesion.TienePermiso(eClock.CEC_RESTRICCIONES.S0Terminales0Borrar) && !Sesion.TienePermiso(eClock.CEC_RESTRICCIONES.S0Terminales0Nuevo) && !Sesion.TienePermiso(eClock.CEC_RESTRICCIONES.S0Terminales0Editar))
        {
            Uwg_EC_TERMINALES.Visible = false;
            Chb_EC_TERMINALES.Visible = false;
        }
    }

    protected void IniciaReporte()
    {
        // Permisos****************************************
        /*
                    string[] PermisoR = new string[10];

                    Sesion.TituloPagina = "Reporte Terminales";
                    Sesion.DescripcionPagina = "";

                    PermisoR[0] = "S";
                    PermisoR[1] = "S.Reportes";
                    PermisoR[2] = "S.Reportes.Reportes_Terminales";


                    if (!Sesion.Acceso(PermisoR, CIT_Perfiles.Acceso(Sesion.PERFIL_ID, this)))
                    {
                        CIT_Perfiles.CrearVentana(this, Sesion.MensajeVentanaJScript(), Sesion.TituloPagina, "Aceptar", "WF_Main.aspx", "", "");
                        ControlVisible(false);
                        return;
                    }
                    //**************************************************
                    eClock.CR_Terminales cR_Terminales1 = new CR_Terminales();
                    cR_Terminales1.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.DefaultPaperOrientation;
                    cR_Terminales1.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize;
                    cR_Terminales1.PrintOptions.PaperSource = CrystalDecisions.Shared.PaperSource.Upper;
                    cR_Terminales1.PrintOptions.PrinterDuplex = CrystalDecisions.Shared.PrinterDuplex.Default;

                    DA_Terminales.Fill(dS_Terminales2.EC_TERMINALES);
                    cR_Terminales1.SetDataSource(dS_Terminales2);
                    if (Sesion.EsSoloReporte == 2)
                    {
                        //Agregar ModuloLog***
                        Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Terminales", 0, "Reporte PDF de Terminales", Sesion.SESION_ID);
                        //*****				


                        if (Sesion.GuardaReportePDF(cR_Terminales1).Length > 0)
                        {
                            Sesion.Redirige("WF_PaginaReporte.aspx");
                        }
                    }
                    else
                    {
                        //Agregar ModuloLog***
                        Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Terminales", 0, "Reporte Excel de Terminales", Sesion.SESION_ID);
                        //*****				

                        if (Sesion.GuardaReporteXLS(cR_Terminales1).Length > 0)
                        {
                            Sesion.Redirige("WF_PaginaReporte.aspx");
                        }
                    }*/
    }

    protected void Page_Load(object sender, System.EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        if (!Sesion.Configura.UsaTerminales)
            if (Sesion.Parametros != "Editar")
                Sesion.Redirige("WF_PersonasSHuella.aspx");
        if (Sesion.EsSoloReporte > 0)
        {
            IniciaReporte();
            return;
        }
        // Introducir aquí el código de usuario para inicializar la página

        Sesion.TituloPagina = "Terminales";
        Sesion.DescripcionPagina = "Seleccione una terminal para editarla o borrarla; o cree una nueva Terminal";

        if (!IsPostBack)
        {
            int NOPErsonas = CeC_Terminales.ObtenNoPersonasXTransmitir(Sesion.SuscripcionParametro, Chb_EC_TERMINALES.Checked);
            if (NOPErsonas > 0)
                Lbl_Error.Text = "" + NOPErsonas + " Personas por transferir";
            // Permisos****************************************
            if (!Sesion.TienePermisoOHijos(eClock.CEC_RESTRICCIONES.S0Terminales, true))
            {
                Habilitarcontroles();
                return;
            }
            //**************************************************

            //Agregar ModuloLog***
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Terminales", 0, "Consulta de Terminales", Sesion.SESION_ID);
            //*****				
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

    protected void TerminalesCheckBox1_CheckedChanged(object sender, System.EventArgs e)
    {
        ActualizaDatos();
    }

    protected void Grid_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        CeC_Grid.AplicaFormato(Uwg_EC_TERMINALES, true, false, false, true);
    }

    protected void ActualizaDatos()
    {
        try
        {
            Sesion = CeC_Sesion.Nuevo(this);
            DataSet DS = CeC_Terminales.ObtenTerminales(Sesion.SuscripcionParametro, Chb_EC_TERMINALES.Checked);
            Uwg_EC_TERMINALES.DataSource = DS;
            Uwg_EC_TERMINALES.DataMember = DS.Tables[0].TableName;
            Uwg_EC_TERMINALES.DataKeyField = "TERMINAL_ID";
        }
        catch { }
    }

    protected void Grid_InitializeDataSource(object sender, Infragistics.WebUI.UltraWebGrid.UltraGridEventArgs e)
    {
        ActualizaDatos();
    }

    protected void btn_Buscar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        Sesion.Redirige("eClockDesc.application?SESION_ID=" + Sesion.SESION_ID + "&BUSCAR=1");
    }

    protected void btn_AccesoPersonas_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        int Numero_registros = Uwg_EC_TERMINALES.Rows.Count;
        for (int i = 0; i < Numero_registros; i++)
        {
            if (Uwg_EC_TERMINALES.Rows[i].Selected)
            {
                int Terminal_Id = Convert.ToInt32(Uwg_EC_TERMINALES.Rows[i].Cells[0].Value);
                Sesion.Redirige_WF_TerminalesPersonas(Terminal_Id);
                return;
            }
        }
        Lbl_Error.Text = "Debes de seleccionar una fila";
    }

    protected void btn_BorrarTerminal_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        DS_TerminalesTableAdapters.EC_TERMINALESTableAdapter TA_Terminales = new DS_TerminalesTableAdapters.EC_TERMINALESTableAdapter();
        int Numero_resgistros = Uwg_EC_TERMINALES.Rows.Count;

        for (int i = 0; i < Numero_resgistros; i++)
        {
            if (Uwg_EC_TERMINALES.Rows[i].Selected)
            {

                int Terminal_ID = Convert.ToInt32(Uwg_EC_TERMINALES.Rows[i].Cells[0].Value);
                CeC_Terminales_Param TP = new CeC_Terminales_Param(Terminal_ID);
                TA_Terminales.ABorrado(Terminal_ID);
                if (TP.SITIO_HIJO_ID > 0)
                    CeC_BD.EjecutaComando("UPDATE eC_SITIOS SET SITIO_BORRADO WHERE SITIO_ID =" + TP.SITIO_HIJO_ID);
            }
        }
    }

    protected void btn_AgregarTerminal_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        // Se pone como ID = -1 para especificar que es una terminal nueva y no necesita cargar datos
        Sesion.WF_Terminales_TERMINALES_ID = -1;
        Sesion.Redirige("WF_TerminalesEd.aspx?SuscripcionID=" + Sesion.SuscripcionParametro);
        //Sesion.Redirige("WF_TerminalesE.aspx?SuscripcionID=" + Sesion.SuscripcionParametro);
    }

    protected void btn_EditarTerminal_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        int Numero_registros = Uwg_EC_TERMINALES.Rows.Count;

        for (int i = 0; i < Numero_registros; i++)
        {
            if (Uwg_EC_TERMINALES.Rows[i].Selected)
            {
                int Terminal_Id = Convert.ToInt32(Uwg_EC_TERMINALES.Rows[i].Cells[0].Value);
                Sesion.WF_Terminales_TERMINALES_ID = Terminal_Id;
                Sesion.Redirige("WF_TerminalesEd.aspx?SuscripcionID=" + Sesion.SuscripcionParametro);
                //Sesion.Redirige("WF_TerminalesE.aspx?SuscripcionID=" + Sesion.SuscripcionParametro);
                return;
            }
        }
        Lbl_Error.Text = "Debes de seleccionar una fila";
    }
    protected void GridExporter_BeginExport(object sender, Infragistics.WebUI.UltraWebGrid.DocumentExport.DocumentExportEventArgs e)
    {
        CeC_Reportes.AplicaFormatoReporte(e, "Terminales", "", Sesion);

    }
    protected void btImprimir_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        GridExporter.Format = Report.FileFormat.PDF;
        GridExporter.TargetPaperOrientation = Report.PageOrientation.Landscape;
        GridExporter.DownloadName = "ExportacionTerminales.pdf";
        GridExporter.Export(Uwg_EC_TERMINALES);
    }
    protected void Btn_MostrarLog_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        int Numero_registros = Uwg_EC_TERMINALES.Rows.Count;

        for (int i = 0; i < Numero_registros; i++)
        {
            if (Uwg_EC_TERMINALES.Rows[i].Selected)
            {
                int Terminal_Id = Convert.ToInt32(Uwg_EC_TERMINALES.Rows[i].Cells[0].Value);
                Lbl_Correcto.Text = CeC_Terminales_DExtras.ObtenLogHtml(Terminal_Id);
                return;
            }
        }
        Lbl_Error.Text = "Debes de seleccionar una fila";

    }
    protected void Grid_InitializeRow(object sender, Infragistics.WebUI.UltraWebGrid.RowEventArgs e)
    {
        try
        {
            Infragistics.WebUI.UltraWebGrid.UltraGridCell Cell_NO_PERSONAS_POS = e.Row.Cells.FromKey("NO_PERSONAS_POS");
            Infragistics.WebUI.UltraWebGrid.UltraGridCell Cell_NO_PERSONAS_ENV = e.Row.Cells.FromKey("NO_PERSONAS_ENV");
            Infragistics.WebUI.UltraWebGrid.UltraGridCell Cell_Conexion_Correcta = e.Row.Cells.FromKey("Conexion_Correcta");
            Infragistics.WebUI.UltraWebGrid.UltraGridCell Cell_Error_Conexion = e.Row.Cells.FromKey("Error_Conexion");
            Infragistics.WebUI.UltraWebGrid.UltraGridCell Cell_ComunicacionCorrecta = e.Row.Cells.FromKey("ComunicacionCorrecta");
            Infragistics.WebUI.UltraWebGrid.UltraGridCell Cell_Error_Comunicacion = e.Row.Cells.FromKey("Error_Comunicacion");
            Infragistics.WebUI.UltraWebGrid.UltraGridCell Cell_FechaHora_Enviada = e.Row.Cells.FromKey("FechaHora_Enviada");
            Infragistics.WebUI.UltraWebGrid.UltraGridCell Cell_FechaHora_Error = e.Row.Cells.FromKey("FechaHora_Error");
            Infragistics.WebUI.UltraWebGrid.UltraGridCell Cell_Checadas_Descargadas = e.Row.Cells.FromKey("Checadas_Descargadas");
            Infragistics.WebUI.UltraWebGrid.UltraGridCell Cell_Checadas_Error = e.Row.Cells.FromKey("Checadas_Error");
            Infragistics.WebUI.UltraWebGrid.UltraGridCell Cell_Vectores_Descargados = e.Row.Cells.FromKey("Vectores_Descargados");
            Infragistics.WebUI.UltraWebGrid.UltraGridCell Cell_Vectores_Error_Desc = e.Row.Cells.FromKey("Vectores_Error_Desc");
            Infragistics.WebUI.UltraWebGrid.UltraGridCell Cell_Vectores_Enviados = e.Row.Cells.FromKey("Vectores_Enviados");
            Infragistics.WebUI.UltraWebGrid.UltraGridCell Cell_Vectores_Error_Env = e.Row.Cells.FromKey("Vectores_Error_Env");


            int NO_PERSONAS_POS = CeC.Convierte2Int(Cell_NO_PERSONAS_POS.Value);
            int NO_PERSONAS_ENV = CeC.Convierte2Int(Cell_NO_PERSONAS_ENV.Value);
            DateTime Conexion_Correcta = CeC.Convierte2DateTime(Cell_Conexion_Correcta.Value);
            DateTime Error_Conexion = CeC.Convierte2DateTime(Cell_Error_Conexion.Value);
            DateTime ComunicacionCorrecta = CeC.Convierte2DateTime(Cell_ComunicacionCorrecta.Value);
            DateTime Error_Comunicacion = CeC.Convierte2DateTime(Cell_Error_Comunicacion.Value);
            DateTime FechaHora_Enviada = CeC.Convierte2DateTime(Cell_FechaHora_Enviada.Value);
            DateTime FechaHora_Error = CeC.Convierte2DateTime(Cell_FechaHora_Error.Value);
            DateTime Checadas_Descargadas = CeC.Convierte2DateTime(Cell_Checadas_Descargadas.Value);
            DateTime Checadas_Error = CeC.Convierte2DateTime(Cell_Checadas_Error.Value);
            DateTime Vectores_Descargados = CeC.Convierte2DateTime(Cell_Vectores_Descargados.Value);
            DateTime Vectores_Error_Desc = CeC.Convierte2DateTime(Cell_Vectores_Error_Desc.Value);
            DateTime Vectores_Enviados = CeC.Convierte2DateTime(Cell_Vectores_Enviados.Value);
            DateTime Vectores_Error_Env = CeC.Convierte2DateTime(Cell_Vectores_Error_Env.Value);
            if (NO_PERSONAS_ENV < NO_PERSONAS_POS)
                Cell_NO_PERSONAS_ENV.Style.BackColor = Color.Red;
            else
                Cell_NO_PERSONAS_ENV.Style.BackColor = Color.Green;

            if (Error_Conexion > Conexion_Correcta)
            {
                Cell_Conexion_Correcta.Style.BackColor = Color.Red;
                Cell_Error_Conexion.Style.BackColor = Color.Red;
            }
            else
                Cell_Conexion_Correcta.Style.BackColor = Color.Green;

            if (Error_Comunicacion > ComunicacionCorrecta)
            {
                Cell_ComunicacionCorrecta.Style.BackColor = Color.Red;
                Cell_Error_Comunicacion.Style.BackColor = Color.Red;
            }
            else
                Cell_ComunicacionCorrecta.Style.BackColor = Color.Green;

            if (FechaHora_Error > FechaHora_Enviada)
            {
                Cell_FechaHora_Enviada.Style.BackColor = Color.Red;
                Cell_FechaHora_Error.Style.BackColor = Color.Red;
            }
            else
                Cell_FechaHora_Enviada.Style.BackColor = Color.Green;

            if (Checadas_Error > Checadas_Descargadas)
            {
                Cell_Checadas_Descargadas.Style.BackColor = Color.Red;
                Cell_Checadas_Error.Style.BackColor = Color.Red;
            }
            else
                Cell_Checadas_Descargadas.Style.BackColor = Color.Green;

            if (Vectores_Error_Desc > Vectores_Descargados)
            {
                Cell_Vectores_Descargados.Style.BackColor = Color.Red;
                Cell_Vectores_Error_Desc.Style.BackColor = Color.Red;
            }
            else
                Cell_Vectores_Descargados.Style.BackColor = Color.Green;

            if (Vectores_Error_Env > Vectores_Enviados)
            {
                Cell_Vectores_Enviados.Style.BackColor = Color.Red;
                Cell_Vectores_Error_Env.Style.BackColor = Color.Red;
            }
            else
                Cell_Vectores_Enviados.Style.BackColor = Color.Green;
        }
        catch { }

    }
}