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


/// <summary>
/// Descripción breve de WF_EnviarReporteUsuarios.
/// </summary>
public partial class WF_EnviarReporteUsuarios : System.Web.UI.Page
{
    protected System.Data.OleDb.OleDbCommand oleDbSelectCommand1;
    protected System.Data.OleDb.OleDbCommand oleDbInsertCommand1;
    protected System.Data.OleDb.OleDbCommand oleDbUpdateCommand1;
    protected System.Data.OleDb.OleDbCommand oleDbDeleteCommand1;
    protected System.Data.OleDb.OleDbDataAdapter DA_Usuarios;
    protected System.Data.OleDb.OleDbConnection Conexion;
    protected System.Data.OleDb.OleDbCommand oleDbCommand1;

    protected DS_EnviaReporte dS_EnviaReporte2;
    protected eClock.DS_RACCESOS dS_RACCESOS1;

    protected eClock.DS_EmpleadosStatus dS_EmpleadosStatus1;
    private int Total_A;
    private int Total_R;
    private int Total_F;
    private int Total_J;
    private string Nombre_Reporte = "";
    CeC_Sesion Sesion;
    private string Nombre_usuario = "";
    private string Nombre_Email = "";

    enum Tipo_Reporte
    {
        Asistencia_mensual,
        Asistencia_mensual_porCC
    }

    private void Escribir_Incidencias_Leyenda(Tipo_Reporte Dato)
    {
        string Qry = "SELECT TIPO_INC_SIS_ABR, TIPO_INC_SIS_NOMBRE FROM EC_TIPO_INC_SIS WHERE TIPO_INC_SIS_ID  > 0";
        string Qry2 = "SELECT TIPO_INCIDENCIA_ABR, TIPO_INCIDENCIA_NOMBRE FROM EC_TIPO_INCIDENCIAS WHERE TIPO_INCIDENCIA_ID > 0";

        string Leyenda = "";

        if (Conexion.State != System.Data.ConnectionState.Open)
            Conexion.Open();

        OleDbCommand commando = new OleDbCommand(Qry, Conexion);
        OleDbDataReader lector;
        lector = commando.ExecuteReader();

        //IncidenciasCompletas += Sesion.AddSpacios(Abreviatura.ToUpper(),3) + "  " + Sesion.AddSpacios(NombreAbr.ToUpper(),35)+ "  "  + Sesion.AddSpacios(" ",35) ;

        while (lector.Read())
        {
            try
            {
                Leyenda += "| " + Sesion.AddSpacios(lector.GetValue(0).ToString(), 3) + " |   " + Sesion.AddSpacios(lector.GetValue(1).ToString(), 35) + "  " + Sesion.AddSpacios(" ", 35);
            }
            catch
            {
            }
        }

        lector.Close();

        OleDbCommand commando2 = new OleDbCommand(Qry2, Conexion);
        OleDbDataReader lector2;
        lector2 = commando2.ExecuteReader();

        while (lector2.Read())
        {
            try
            {
                Leyenda += "| " + Sesion.AddSpacios(lector2.GetValue(0).ToString(), 3) + " |   " + Sesion.AddSpacios(lector2.GetValue(1).ToString(), 35) + "  " + Sesion.AddSpacios(" ", 35);
            }
            catch
            {
            }
        }

        Conexion.Close();

    }

    private void Habilitarcontroles()
    {
        Panel1.Visible = false;
        Enviar_Reporte.Visible = false;
    }

    protected void Page_Load(object sender, System.EventArgs e)
    {
        // Introducir aquí el código de usuario para inicializar la página
        Sesion = CeC_Sesion.Nuevo(this);

        Sesion.TituloPagina = "Enviar Reporte";
        Sesion.DescripcionPagina = "Seleccione los usuarios a los que desea enviar un mail de asistencia";
        Sesion.Error_Mail = "";

        // Permisos****************************************
        if (!Sesion.TienePermisoOHijos(eClock.CEC_RESTRICCIONES.S0Reportes0Reportes_Enviar_Reportes_Usuarios0Grupo, true))
        {
            Habilitarcontroles();
            return;
        }
        //**************************************************

        Sesion.TituloPagina = "Enviar Reportes a Usuarios";
        Sesion.DescripcionPagina = "Seleccione un rango de fechas , uno o varios usuarios para enviar el Reporte";

        //DA_Usuarios.Fill(dS_EnviaReporte2.EC_USUARIOS);

        if (!IsPostBack)
        {
            ReporteDropDownList1.Items.Add(new ListItem("Asistencia Centro de Costos", "0"));
            ReporteDropDownList1.Items.Add(new ListItem("Asistencia Diaria", "1"));
            ReporteDropDownList1.Items.Add(new ListItem("Asistencia Mensual", "2"));
            ReporteDropDownList1.Items.Add(new ListItem("Asistencia Mensual por Centro de Costos", "3"));
            ReporteDropDownList1.Items.Add(new ListItem("Graficas de Asistencia por Agrupacion 1", "5"));
            ReporteDropDownList1.Items.Add(new ListItem("Graficas de Asistencia por Agrupacion 2", "6"));
            ReporteDropDownList1.Items.Add(new ListItem("Graficas de Asistencia por Agrupacion 3", "7"));
            ReporteDropDownList1.Items.Add(new ListItem("Graficas de Asistencia por Empleado", "8"));
            CMd_Sicoss Sic = new CMd_Sicoss();
            if (Sic.EstaHabilitado())
            {
                ReporteDropDownList1.Items.Add(new ListItem("Asistencia Sicoss", "4"));
                ReporteDropDownList1.SelectedIndex = 8;
            }
            /*
            for (int i = 0; i <dS_EnviaReporte2.EC_USUARIOS.Rows.Count; i++)
            {
                DS_EnviaReporte.EC_USUARIOSRow FilaEnviar = (DS_EnviaReporte.EC_USUARIOSRow)dS_EnviaReporte2.EC_USUARIOS.Rows[i];

                string nombre_usuario  = FilaEnviar.USUARIO_NOMBRE.ToString();
                string Mail = CeC_BD.EjecutaEscalarString("Select Persona_Email from EC_PERSONAS where EC_PERSONAS.persona_nombre like '%"+nombre_usuario +"%'");
                FilaEnviar.USUARIO_DESCRIPCION = Mail;
            }*/

            FechaIA.Value = DateTime.Today.AddDays(-7);
            FechaFA.Value = DateTime.Today;
            //Agregar Módulo Log
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Envio de Reportes a Usuarios", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
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
        this.oleDbSelectCommand1 = new System.Data.OleDb.OleDbCommand();
        this.oleDbInsertCommand1 = new System.Data.OleDb.OleDbCommand();
        this.oleDbUpdateCommand1 = new System.Data.OleDb.OleDbCommand();
        this.oleDbDeleteCommand1 = new System.Data.OleDb.OleDbCommand();
        this.DA_Usuarios = new System.Data.OleDb.OleDbDataAdapter();
        this.oleDbCommand1 = new System.Data.OleDb.OleDbCommand();
        this.Conexion = new System.Data.OleDb.OleDbConnection();
        this.dS_EnviaReporte2 = new DS_EnviaReporte();
        this.dS_RACCESOS1 = new eClock.DS_RACCESOS();
        this.dS_EmpleadosStatus1 = new eClock.DS_EmpleadosStatus();
        ((System.ComponentModel.ISupportInitialize)(this.dS_EnviaReporte2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dS_RACCESOS1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dS_EmpleadosStatus1)).BeginInit();
        this.Enviar_Reporte.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.Enviar_Reporte_Click);
        // 
        // DA_Usuarios
        // 
        this.DA_Usuarios.SelectCommand = this.oleDbCommand1;
        this.DA_Usuarios.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																								  new System.Data.Common.DataTableMapping("Table", "EC_USUARIOS", new System.Data.Common.DataColumnMapping[] {
																																																				  new System.Data.Common.DataColumnMapping("USUARIO_ID", "USUARIO_ID"),
																																																				  new System.Data.Common.DataColumnMapping("PERFIL_ID", "PERFIL_ID"),
																																																				  new System.Data.Common.DataColumnMapping("USUARIO_USUARIO", "USUARIO_USUARIO"),
																																																				  new System.Data.Common.DataColumnMapping("USUARIO_NOMBRE", "USUARIO_NOMBRE"),
																																																				  new System.Data.Common.DataColumnMapping("USUARIO_DESCRIPCION", "USUARIO_DESCRIPCION"),
																																																				  new System.Data.Common.DataColumnMapping("USUARIO_CLAVE", "USUARIO_CLAVE"),
																																																				  new System.Data.Common.DataColumnMapping("USUARIO_BORRADO", "USUARIO_BORRADO")})});
        // 
        // oleDbCommand1
        // 
        this.oleDbCommand1.CommandText = "SELECT USUARIO_ID, PERFIL_ID, USUARIO_USUARIO, USUARIO_NOMBRE, USUARIO_DESCRIPCIO" +
            "N, USUARIO_CLAVE, USUARIO_BORRADO FROM EC_USUARIOS WHERE (USUARIO_BORRADO = 0)";
        this.oleDbCommand1.Connection = this.Conexion;
        // 
        // Conexion
        // 
        this.Conexion.ConnectionString = ((string)(configurationAppSettings.GetValue("gBDatos.ConnectionString", typeof(string))));
        // 
        // dS_EnviaReporte2
        // 
        this.dS_EnviaReporte2.DataSetName = "DS_EnviaReporte";
        this.dS_EnviaReporte2.Locale = new System.Globalization.CultureInfo("es-MX");
          // 
        // dS_RACCESOS1
        // 
        this.dS_RACCESOS1.DataSetName = "DS_RACCESOS";
        this.dS_RACCESOS1.Locale = new System.Globalization.CultureInfo("es-MX");
        // 
        // dS_EmpleadosStatus1
        // 
        this.dS_EmpleadosStatus1.DataSetName = "DS_EmpleadosStatus";
        this.dS_EmpleadosStatus1.Locale = new System.Globalization.CultureInfo("es-MX");
        ((System.ComponentModel.ISupportInitialize)(this.dS_EnviaReporte2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dS_RACCESOS1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dS_EmpleadosStatus1)).EndInit();

    }
    #endregion

    private void Mandar_Mail(string Nombre_us, string NNombreRepPDF)
    {
        //funcion para enviar correo electronico
        string Nombre_Reporte = Sesion.ObtenNombreReportesPDF(NNombreRepPDF);
        //string Email_usuario = CeC_BD.EjecutaEscalarString("SELECT EC_PERSONAS.PERSONA_EMAIL FROM EC_PERSONAS WHERE EC_PERSONAS.PERSONA_NOMBRE = '"+Nombre_us+"'");

        char[] Caracteres = new char[1];
        Caracteres[0] = Convert.ToChar("\\");
        string[] NombreReporteCompleto = Nombre_Reporte.Split(Caracteres);

        string NombreReporteCompletoSimple = NombreReporteCompleto[(int)NombreReporteCompleto.Length - 1];

        string DireccionArchivoPDF = Sesion.ObtenerSoloDirectorioPDF() + NombreReporteCompletoSimple;

        if (Nombre_Email.Length > 0)
        {
            Sesion.EnviaMail(Nombre_Email, TxbMensage.Text, DireccionArchivoPDF, TxbAsunto.Text, ReporteDropDownList1.SelectedItem.Text, Nombre_usuario);
            return;
        }
        else
        {
            LError.Text = "Error: Probablemente el usuario con el Nombre " + Nombre_us + " no dispone de una direccion de correo ";
            return;
        }
    }

    private bool EnviaReporte(int Reporte, DateTime FechaI, DateTime FechaF, bool VerDiasNLab, string Mail,string Titulo, string Mensage, int Usuario_ID)    {
        try
        {
            int Perfil_ID = CeC_BD.EjecutaEscalarInt("SELECT PERFIL_ID FROM EC_USUARIOS WHERE USUARIO_ID = " + Usuario_ID);
            string Filtro = "";
            if (Perfil_ID == 1)
                 Filtro =Sesion.WF_EmpleadosFil_Qry_Temp= "SELECT PERSONA_ID FROM EC_PERSONAS WHERE PERSONA_BORRADO = 0";
            else
                 Filtro = Sesion.WF_EmpleadosFil_Qry_Temp = "SELECT PERSONA_ID FROM EC_PERSONAS WHERE PERSONA_BORRADO = 0 and (EC_PERSONAS.SUSCRIPCION_ID in (Select EC_PERMISOS_SUSCRIP.SUSCRIPCION_ID from EC_PERMISOS_SUSCRIP where EC_PERMISOS_SUSCRIP.usuario_id = " + Usuario_ID + "))";
            
/*            ReportClass ReporteCR = null;
            CeC_Reportes.REPORTE TipoReporte = new CeC_Reportes.REPORTE();
            switch (Reporte)
            {
                case 0:
                    TipoReporte = CeC_Reportes.REPORTE.AsistenciasCC;
                    //ReporteCR = CeC_Reportes.GeneraReporte(CeC_Reportes.REPORTE.AsistenciasCC, FechaI, FechaF, Filtro, VerDiasNLab, false, Sesion);
                    break;
                case 1:
                    TipoReporte = CeC_Reportes.REPORTE.AsistenciaDiaria;
                    //ReporteCR = CeC_Reportes.GeneraReporteAsistenciasD(Sesion);
                    break;
                case 2:
                    TipoReporte = CeC_Reportes.REPORTE.AsistenciaMens;
                    //ReporteCR = CeC_Reportes.GeneraReporteMensual(Sesion);
                    break;
                case 3:
                    TipoReporte = CeC_Reportes.REPORTE.AsistenciaMensCC;
                    //ReporteCR = CeC_Reportes.GeneraReporteMensualCC(Sesion);
                    break;
                case 4:
                    TipoReporte = CeC_Reportes.REPORTE.AsistenciaSicoss;
                    //ReporteCR = CeC_Reportes.GeneraReporte(CeC_Reportes.REPORTE.AsistenciasCC, FechaI, FechaF, Filtro, VerDiasNLab, false, Sesion);                    
                    break;
                case 5:
                    TipoReporte = CeC_Reportes.REPORTE.GraficasGrupo1;
                    //ReporteCR = CeC_Reportes.GeneraReporteAsistenciasGraficas(Sesion, 1);                    
                    break;
                case 6:
                    TipoReporte = CeC_Reportes.REPORTE.GraficasGrupo2;
                    //ReporteCR = CeC_Reportes.GeneraReporteAsistenciasGraficas(Sesion, 2);
                    break;
                case 7:
                    TipoReporte = CeC_Reportes.REPORTE.GraficasGrupo3;
                    //ReporteCR = CeC_Reportes.GeneraReporteAsistenciasGraficas(Sesion, 3);
                    break;
                case 8:
                    TipoReporte = CeC_Reportes.REPORTE.GraficosPersona;
                    //ReporteCR = CeC_Reportes.GeneraReporteAsistenciasGraficasPersona(Sesion);
                    break;
            }
            return false;
            /*
            ReporteCR = CeC_Reportes.GeneraReporte(TipoReporte, FechaI, FechaF, Filtro, VerDiasNLab, false, Sesion);
            if (ReporteCR != null)
            {
                string Ret = Sesion.GuardaReportePDF(ReporteCR);
                if (Ret.Length > 0)
                {
                    return CeC_BD.EnviarMail(Mail, "", Titulo, Mensage, Ret);
                }
            }*/
        }
        catch{ }
        return false;
    }

    protected void Enviar_Reporte_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        string Qry_Temp = "";
        if (!Sesion.WF_EnviarReporteUsuariosGuardado)
        {
            DateTime FechaI = Sesion.WF_EmpleadosFil_FechaI = Convert.ToDateTime(FechaIA.Value);
            DateTime FechaF = Sesion.WF_EmpleadosFil_FechaF = Convert.ToDateTime(FechaFA.Value);
            int Reporte = Convert.ToInt16(ReporteDropDownList1.SelectedValue);
            int ReportesEnviados = 0;
            Infragistics.WebUI.UltraWebGrid.UltraGridColumn CSel = Grid.Bands[0].Columns.FromKey("USUARIO_ENVMAILA");
            Infragistics.WebUI.UltraWebGrid.UltraGridColumn CEMail1 = Grid.Bands[0].Columns.FromKey("USUARIO_EMAIL");
            Infragistics.WebUI.UltraWebGrid.UltraGridColumn CEMail2 = Grid.Bands[0].Columns.FromKey("PERSONA_EMAIL");
            foreach (Infragistics.WebUI.UltraWebGrid.UltraGridRow Fila in Grid.Rows)
            {
                int Usuario_ID = Convert.ToInt32(Fila.Cells.FromKey("USUARIO_ID").Value);
                bool Seleccionado = Convert.ToBoolean(Fila.GetCellValue(CSel));
                string Email1 = Fila.GetCellText(CEMail1);
                string Email2 = Fila.GetCellText(CEMail2);
                if (Email2 != null && Email2.Length > 0)
                    Email1 = Email2;
                if (Seleccionado)
                {
                    if (EnviaReporte(Reporte, FechaI, FechaF, DiasNoLaborablesCheckBox1.Checked, Email1, TxbAsunto.Text, TxbMensage.Text, Usuario_ID))
                        ReportesEnviados++;
                }
            }
            if (ReportesEnviados <= 0)
            {
                LError.Text = "Debe seleccionar una fila";
                LCorrecto.Text = "";
            }
            else
                if (Sesion.Error_Mail.Length > 0)
                {
                    LError.Text = Sesion.Error_Mail;
                    LCorrecto.Text = "";
                }
                else
                {
                    Sesion.WF_EnviarReporteUsuariosGuardado = true;
                    LCorrecto.Text = "Reportes enviados satisfactoriamente";
                    LError.Text = "";
                }
        }
        else
            Sesion.WF_EnviarReporteUsuariosGuardado = false;
    }

    protected void UltraWebGrid1_InitializeDataSource(object sender, Infragistics.WebUI.UltraWebGrid.UltraGridEventArgs e)
    {
    }

    protected void Enviar_Reporte_Click1(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
    }

    protected void Grid_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        CeC_Grid.AplicaFormato(Grid);
    }

    protected void Grid_InitializeDataSource(object sender, Infragistics.WebUI.UltraWebGrid.UltraGridEventArgs e)
    {
        try
        {
            DS_EnviaReporteTableAdapters.EC_USUARIOSTableAdapter TA = new DS_EnviaReporteTableAdapters.EC_USUARIOSTableAdapter();
            if (CeC_BD.EsOracle)
                TA.Remplaza("SELECT     USUARIO_ID, PERFIL_ID, USUARIO_USUARIO, USUARIO_NOMBRE, USUARIO_DESCRIPCION, USUARIO_EMAIL,"+
                            " (SELECT     min (PERSONA_EMAIL)"+
                            " FROM          EC_PERSONAS"+
                            " WHERE      ( EC_USUARIOS.USUARIO_USUARIO =TO_CHAR(PERSONA_LINK_ID))) AS PERSONA_EMAIL, USUARIO_ENVMAILA" +
                            " FROM         EC_USUARIOS WHERE     (USUARIO_EMAIL IS NOT NULL)  AND (USUARIO_BORRADO = 0)");            
            else
            TA.ActualizaIn("CONVERT(\"varchar\", PERSONA_LINK_ID)");
            TA.Fill(dS_EnviaReporte2.EC_USUARIOS);
            Grid.DataSource = dS_EnviaReporte2.EC_USUARIOS;
        }
        catch (Exception ex)
        { }
    }
}
