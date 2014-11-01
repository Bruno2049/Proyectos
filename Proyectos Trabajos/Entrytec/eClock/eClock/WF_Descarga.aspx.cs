using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class WF_Descarga : System.Web.UI.Page
{
    /// <summary>
    /// Descarga los reportes que se encuentran en la ruta de PDF de eClock. 
    /// Obtiene el nombre del archivo usando como parámetros la variable "Sesion.Parametros"
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        CeC_Sesion Sesion;
        Sesion = CeC_Sesion.Nuevo(this);
        CeC_Exportacion Exp = new CeC_Exportacion();
        if (Sesion.Parametros != "")
        {
            try
            {
                string Archivo = HttpRuntime.AppDomainAppPath + CeC_Config.RutaReportesPDF + Sesion.Parametros;
                FileInfo file = new FileInfo(Archivo);
                Response.Clear();
                Response.ClearHeaders();
                Response.ClearContent();
                Response.AppendHeader("Content-Disposition", "attachment; filename = " + Sesion.Parametros);
                Response.AppendHeader("Content-Length", file.Length.ToString());
                Response.ContentType = "application/download";
                Response.WriteFile(file.FullName);
                Response.Flush();
                Response.Close();
                Response.End();
            }
            catch (Exception ex)
            {
                CIsLog2.AgregaError(ex);
            }
        }
        else 
        { 
            //Pantalla de error
            // Se creara una pantalla (popUp window) que reciba de esta pantalla el mensaje de error a mostrar
        }
    }
}