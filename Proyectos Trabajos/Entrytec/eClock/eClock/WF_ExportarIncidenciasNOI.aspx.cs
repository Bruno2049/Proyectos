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
using System.Text;
public partial class WF_ExportarIncidenciasNOI : System.Web.UI.Page
{
    DS_ExportacionNOITableAdapters.IncidenciasPersonasTableAdapter TA;
    DS_ExportacionNOI DS;
    CeC_Sesion Sesion;
    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        if (!Sesion.Configura.UsaNOI)
            Sesion.Redirige("WF_Info.aspx");
        Sesion.TituloPagina = "Exportación de Incidencias a NOI";
        Sesion.DescripcionPagina = "Exporta Asistencias, Faltas, Retardos y Justificaciones dentro de un rango de fechas, en un archivo previamente configurado "
        + "y será guardado en la carpeta PDF con el título \"Exportacion de Incidencias NOI\" y la fecha de creación. Éste deberá de ser usado cuando NOI pida un "
        + "archivo para importar";

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
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Exportar Incidencias a  NOI", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
        }
    }

    protected void BGuardarCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        TA = new DS_ExportacionNOITableAdapters.IncidenciasPersonasTableAdapter();
        DS = new DS_ExportacionNOI();
        TA.ActualizaIn(CeC_Config.CampoLlaveNOI);
        try
        {
            TA.Fill(DS.IncidenciasPersonas, FechaI.SelectedDate, FechaF.SelectedDate);
            //string[] Lineas = new string[DS.IncidenciasPersonas.Rows.Count];
            ArrayList Lineas = new ArrayList();
            int index = 0;
            for (int i = 0; i < DS.IncidenciasPersonas.Rows.Count; i++)
            {
                string linea = "0000000000000000000000                             ";
                string ID = DS.IncidenciasPersonas[i].ID.ToString().PadLeft(5, ' ');
                linea = linea.Insert(0, ID);
                string cad = "";
                if (DS.IncidenciasPersonas[i].TIPO_INCIDENCIA_ID > 0)
                {
                    cad = CeC_Config.ObtenConfig(0, "NCIPID_" + DS.IncidenciasPersonas[i].TIPO_INCIDENCIA_ID.ToString() + "_NOI", "    ");
                }
                else
                {
                    cad = CeC_Config.ObtenConfig(0, "NCISID_" + DS.IncidenciasPersonas[i].TIPO_INC_SIS_ID.ToString() + "_NOI", "    ");
                }
                if (cad == "    ")
                    continue;
                linea = linea.Insert(5, cad.Substring(0, 4));
                linea = linea.Insert(9, "4");
                linea = linea.Insert(10, DS.IncidenciasPersonas[i].PERSONA_DIARIO_FECHA.ToString("ddMMyy") + DS.IncidenciasPersonas[i].PERSONA_DIARIO_FECHA.ToString("ddMMyy"));
                linea = linea.Insert(22, cad.Substring(4, 1));
                linea = linea.Insert(45, "1");
                Lineas.Add(linea);

            }
            string[] LineasFinal = new string[Lineas.Count];
            int j = 0;
            foreach (string n in Lineas)
            {
                LineasFinal[j] = n;
                j++;
            }
            System.IO.File.WriteAllLines(HttpRuntime.AppDomainAppPath + CeC_Config.RutaReportesPDF + "Exportacion de Incidencias NOI " + DateTime.Now.ToString("ddMMyyyy HHmm") + ".dat", LineasFinal);
            string filename = "PDF\\Exportacion de Incidencias NOI " + DateTime.Now.ToString("ddMMyyyy HHmm") + ".dat";
            if (!String.IsNullOrEmpty(filename))
            {
                String path = Server.MapPath(filename);

                System.IO.FileInfo toDownload = new System.IO.FileInfo(path);
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + toDownload.Name);
                Response.AddHeader("Content-Length", toDownload.Length.ToString());
                Response.ContentType = "application/octet-stream";
                Response.WriteFile(filename);
                Response.End();
            }
            Sesion.Redirige("WF_Main.aspx");
        }
        catch (Exception ex)
        {
            LError.Text = "Ocurrio el Siguiente error: " + ex.Message;
        }
    }

    protected void BDeshacerCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        Sesion.Redirige("WF_Main.aspx");
    }
}