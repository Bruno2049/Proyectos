using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Descripción breve de Global
/// </summary>
public class Global:Page
{
	public Global()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}

    void Application_Start(object sender, EventArgs e)
    {
        // Código que se ejecuta al iniciarse la aplicación
        CeC_BD.IniciaAplicacion();
          
    }

    void Application_End(object sender, EventArgs e)
    {
        //  Código que se ejecuta cuando se cierra la aplicación
        CeC_Asistencias.ParaGeneración();
    }

    void Application_Error(object sender, EventArgs e)
    {
        // Código que se ejecuta al producirse un error no controlado
        Exception ex = this.Server.GetLastError().GetBaseException();
        CIsLog2.AgregaError("Application_Error Descripción: " + ex.Message + " \nOrigen:" + ex.Source + "\n Pila:" + ex.StackTrace);

    }

    void Session_Start(object sender, EventArgs e)
    {
        // Código que se ejecuta cuando se inicia una nueva sesión
        CIsLog2.AgregaLog("Session_Start");
    }

    void Session_End(object sender, EventArgs e)
    {
        // Código que se ejecuta cuando finaliza una sesión. 
        // Nota: El evento Session_End se desencadena sólo con el modo sessionstate
        // se establece como InProc en el archivo Web.config. Si el modo de sesión se establece como StateServer 
        // o SQLServer, el evento no se genera.
        CeC_Sesion.CierraSesion(Session);
        CIsLog2.AgregaLog("Session_End");
    }
}
