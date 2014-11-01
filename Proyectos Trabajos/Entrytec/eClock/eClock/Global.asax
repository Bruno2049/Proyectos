<%@ Application Language="C#" %>

<script runat="server">

    
    void Application_Start(object sender, EventArgs e)
    {
        CeC_BD.IniciaAplicacion();
    }

   protected void Application_End(object sender, EventArgs e)
    {
        //  Código que se ejecuta cuando se cierra la aplicación
        CeC_Asistencias.ParaGeneración();
    }

    void Application_Error(object sender, EventArgs e)
    {
        // Código que se ejecuta al producirse un error no controlado
        Exception ex = this.Server.GetLastError().GetBaseException();
        //CIsLog2.AgregaError("Application_Error Descripción: " + ex.Message + " \nOrigen:" + ex.Source + "\n Pila:" + ex.StackTrace);
        
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
    void Application_BeginRequest(Object source, EventArgs e)
    {

        HttpApplication app = (HttpApplication)source;

        HttpContext context = app.Context;

        // Attempt to peform first request initialization

        FirstRequestInitialization.Initialize(context);

    }


    class FirstRequestInitialization
    {

        private static bool s_InitializedAlready = false;

        private static Object s_lock = new Object();

        // Initialize only on the first request

        public static void Initialize(HttpContext context)
        {

            if (s_InitializedAlready)
            {

                return;

            }

            lock (s_lock)
            {

                if (s_InitializedAlready)
                {

                    return;

                }

                // Perform first-request initialization here ...

                s_InitializedAlready = true;

            }

        }

    }
</script>
