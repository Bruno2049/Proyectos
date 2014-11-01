using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class WF_EnvioReportes : System.Web.UI.Page
{
    protected DS_EnvioReportes dS_EnvioReportes1;
    CeC_Sesion Sesion;

    /// <summary>
    /// Muestra los botones para realizar las acciones permitidas al usuario en turno
    /// </summary>
    /// <param name="Caso"></param>
    /// <param name="Restriccion"></param>
    private void Habilitarcontroles(bool Caso, string Restriccion)
    {
        
        string PermisoFinal = CeC_Sesion.LeeStringSesion(this, "PermisosComparacion");
        if(!Sesion.TienePermiso(eClock.CEC_RESTRICCIONES.S0Envio0Reportes))
        {
            btn_DuplicarEnvio.Visible = false;

            if (!Sesion.TienePermiso(eClock.CEC_RESTRICCIONES.S0Envio0Reportes0Nuevo))
                btn_NuevoEnvio.Visible = Caso;

            if (!Sesion.TienePermiso(eClock.CEC_RESTRICCIONES.S0Envio0Reportes0Editar))
                btn_EditarEnvio.Visible = Caso;

            if (!Sesion.TienePermiso(eClock.CEC_RESTRICCIONES.S0Envio0Reportes0Borrar))
                btn_BorrarEnvio.Visible = Caso;

            if (!Sesion.ExistePermiso("S.Envio.Reportes.Borrar", PermisoFinal) && !Sesion.ExistePermiso("S.Envio.Reportes.Nuevo", PermisoFinal) && !Sesion.ExistePermiso("S.Envio.Reportes.Editar", PermisoFinal))
            {
                Grid.Visible = Caso;
                ReportesCheckBox1.Visible = Caso;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        Sesion.TituloPagina = "Envio de Reportes";
        Sesion.DescripcionPagina = "Seleccione una Regla de Envio de Reportes para editarla o borrarla; o cree una nueva Regla o duplique una existente";

        if (!IsPostBack)
        {
            /********
             Permisos
             ********/
            string[] Permiso = new string[4];
            Permiso[0] = eClock.CEC_RESTRICCIONES.S0Envio0Reportes;
            Permiso[1] = eClock.CEC_RESTRICCIONES.S0Envio0Reportes0Nuevo;
            Permiso[2] = eClock.CEC_RESTRICCIONES.S0Envio0Reportes0Editar;
            Permiso[3] = eClock.CEC_RESTRICCIONES.S0Envio0Reportes0Borrar;

            if (Sesion.SUSCRIPCION_ID != 1 || !Sesion.Acceso(Permiso, CIT_Perfiles.Acceso(Sesion.PERFIL_ID, this)))
            {
                CIT_Perfiles.CrearVentana(this, Sesion.MensajeVentanaJScript(), Sesion.TituloPagina, "Aceptar", "WF_Main.aspx", "", "");
                Habilitarcontroles(false, Sesion.Restriccion.ToString());
                return;
            }

            Habilitarcontroles(false, Sesion.Restriccion.ToString());
            ActualizaDatos();

            /*****************
             Agregar ModuloLog
             *****************/
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Envio Reportes", 0, "Reglas de Envio de Reportes", Sesion.SESION_ID);	
        }
    }

    protected void Grid_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        CeC_Grid.AplicaFormato(Grid, true, false, false, true);
    }

    protected void Grid_InitializeDataSource(object sender, Infragistics.WebUI.UltraWebGrid.UltraGridEventArgs e)
    {
        ActualizaDatos();
    }

    protected void ReportesCheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        ActualizaDatos();
    }

    /// <summary>
    /// Actualiza los datos en el Grid
    /// </summary>
    protected void ActualizaDatos()
    {
        dS_EnvioReportes1 = new DS_EnvioReportes();
        Sesion = CeC_Sesion.Nuevo(this);
        if (Sesion.SUSCRIPCION_ID != 1)
            return;
        DS_EnvioReportesTableAdapters.EC_ENV_REPORTESTableAdapter TA_EnvioReportes = new DS_EnvioReportesTableAdapters.EC_ENV_REPORTESTableAdapter();
        int Borrados = 0;
        if (ReportesCheckBox1.Checked)
            Borrados = 1;
        TA_EnvioReportes.FillBorrado(dS_EnvioReportes1.EC_ENV_REPORTES, Borrados);
        Grid.DataSource = dS_EnvioReportes1.EC_ENV_REPORTES;
        Grid.DataMember = dS_EnvioReportes1.EC_ENV_REPORTES.TableName;
        Grid.DataKeyField = "ENV_REPORTE_ID";
    }

    //Crea una nueva regla de envio
    protected void btn_NuevoEnvio_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        Sesion.WF_ENV_REPORTE_ID = -1;
        Sesion.Redirige("WF_EnvioReportesEdicion.aspx");
    }

    //Edita la regla de envio seleccionada
    protected void btn_EditarEnvio_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        int NumeroRegistros = Grid.Rows.Count;
        for (int i = 0; i < NumeroRegistros; i++)
        {
            if (Grid.Rows[i].Selected)
            {
                Sesion.WF_ENV_REPORTE_ID = Convert.ToInt32(Grid.Rows[i].Cells[0].Value);
                Sesion.WF_ENV_REPORTE_USUARIO_ID = Convert.ToInt32(Grid.Rows[i].Cells[1].Value);
                Sesion.WF_ENV_REPORTE_REPORTE_ID = Convert.ToInt32(Grid.Rows[i].Cells[4].Value); 
                Sesion.Redirige("WF_EnvioReportesEdicion.aspx");
                return;
            }
        }
        LError.Text = "Debes de seleccionar una fila";
    }

    //Borra la regla de envio seleccionada
    protected void btn_BorrarEnvio_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        DS_EnvioReportesTableAdapters.EC_ENV_REPORTESTableAdapter TA_EnvReportes = new DS_EnvioReportesTableAdapters.EC_ENV_REPORTESTableAdapter();
        int NumeroRegistros = Grid.Rows.Count;
        for (int i = 0; i < NumeroRegistros; i++)
        {
            if (Grid.Rows[i].Selected)
            {
                int EnvReporte_ID = Convert.ToInt32(Grid.Rows[i].Cells[0].Value);
                TA_EnvReportes.BorrarEnvio(EnvReporte_ID);
            }
        }
        Page_Load(sender, e);
    }

    //Duplica la regla de envio seleccionada
    protected void btn_DuplicarEnvio_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        CeC_EnvioReportes EnvReportes = new CeC_EnvioReportes();
        int NumeroRegistros = Grid.Rows.Count;
        for (int i = 0; i < NumeroRegistros; i++)
        {
            if (Grid.Rows[i].Selected)
            {
                decimal ID = Convert.ToDecimal(Grid.Rows[i].Cells[0].Value);
                bool resp = CeC_EnvioReportes.DuplicarEnv(ID);
                ActualizaDatos();
                return;
            }
        }
        LError.Text = "Debes de seleccionar una fila";
    }
}