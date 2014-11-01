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

public partial class WF_ExportarIncidenciasNomipaq : System.Web.UI.Page
{
    CeC_Sesion Sesion;
    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        if (!Sesion.ConfiguraSuscripcion.UsaNomipac)
            Sesion.Redirige("WF_Info.aspx");
        txtEjercicio.Text = DateTime.Today.Year.ToString();
        Sesion.TituloPagina = "Exportar Incidencias a Nomipaq";
        Sesion.DescripcionPagina = "Debe seleccionar un rango de fechas, un Periodo y un Ejercicio, despues de esto de click en Generar Archivo e "+
        "eClock generara un archivo en la carpeta de la aplicación dentro de una subcarpeta llamada pdf con el nombre \"Exportacion de Incidencias\" y la fecha de la "+
        "exportación. Nomipaq solicitará un archivo para importar y seleccionaremos el generado por eClock. Es importante que los Numeros de Empleado "+
        "coincidan, de lo contrario los datos serán erróneos";
        txtValor.Value = 1;

        // Permisos****************************************
        if (!Sesion.TienePermisoOHijos(eClock.CEC_RESTRICCIONES.S0Configuracion, true))
        {
            WebPanel2.Visible = false;
            return;
        }
        //**************************************************

        if (!IsPostBack)
        {
            //Agregar Módulo Log
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Exportar Incidencias a  Nomipaq", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);

            FechaI.SelectedDate = DateTime.Now.AddDays(-8);
            FechaF.SelectedDate = DateTime.Now.AddDays(-1);
        }
    }

    protected void BDeshacerCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        Sesion.Redirige("WF_Main.aspx");
    }

    protected void BGuardarCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        LCorrecto.Text = "";
        try
        {
            DS_ExportacionNomipaq DS = new DS_ExportacionNomipaq();
            string ruta = CeC_Config.RutaReportesPDF;
            DS_ExportacionNomipaqTableAdapters.INC_DIATableAdapter TA = new DS_ExportacionNomipaqTableAdapters.INC_DIATableAdapter();
            TA.FillInc_Dia(DS.INC_DIA, FechaI.SelectedDate, FechaF.SelectedDate.AddDays(1));
            System.IO.FileStream F;
            //      F = System.IO.File.Create(ruta);
            string[] Lineas = new string[DS.INC_DIA.Rows.Count];
            string linea;
            for (int i = 0; i < DS.INC_DIA.Rows.Count; i++)
            {
                if (DS.INC_DIA[i].TIPO_INC_SIS_ABR == "")
                    linea = DS.INC_DIA[i].PERSONA_DIARIO_FECHA.Date.ToString("dd/MM/yyyy") + "|" + txtValor.Text + "|" + DS.INC_DIA[i].PERSONA_LINK_ID.ToString()
                    + "|" + txtPeriodo.Value.ToString() + "|" + txtEjercicio.Value.ToString() + "|" + DS.INC_DIA[i].TIPO_INCIDENCIA_NOMBRE.ToString() + "|"
                    + DS.INC_DIA[i].TIPO_INCIDENCIA_ABR.ToString();
                else
                    linea = DS.INC_DIA[i].PERSONA_DIARIO_FECHA.Date.ToString("dd/MM/yyyy") + "|" + txtValor.Text + "|" + DS.INC_DIA[i].PERSONA_LINK_ID.ToString()
                    + "|" + txtPeriodo.Value.ToString() + "|" + txtEjercicio.Value.ToString() + "|" + DS.INC_DIA[i].TIPO_INC_SIS_NOMBRE.ToString() + "|"
                    + DS.INC_DIA[i].TIPO_INC_SIS_ABR.ToString();
                Lineas[i] = linea;
            }
            System.IO.File.WriteAllLines(HttpRuntime.AppDomainAppPath + CeC_Config.RutaReportesPDF + "Exportacion de Incidencias " + DateTime.Now.ToString("ddMMyyyy HHmm") + ".txt", Lineas);
        }
        catch { }
        LCorrecto.Text = "Se ha generado correctamente el archivo de exportación";
        CeC_Sesion.SAgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.NUEVO, "Exportacion Nomipaq", 0, "Se ha exportado correctamente el periodo "
            + FechaI.SelectedDate.ToString("dd/MM/yyyy") + " - " + FechaF.SelectedDate.ToString("dd/MM/yyyy"), Sesion.SESION_ID);
        CeC_Periodos.AgregaPeriodoBloqueado(FechaI.SelectedDate, FechaF.SelectedDate,Sesion.SESION_ID,Sesion.SUSCRIPCION_ID);

//        Sesion.Redirige("WF_Main.aspx");
    }
}