<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Código que se ejecuta al iniciarse la aplicación
        System.Web.ApplicationServices.AuthenticationService.Authenticating +=
        new EventHandler<System.Web.ApplicationServices.AuthenticatingEventArgs>(AuthenticationService_Authenticating);
        CeC_BD.IniciaAplicacion();
    }

    void AuthenticationService_Authenticating(object sender, System.Web.ApplicationServices.AuthenticatingEventArgs e)
    {
        try
        {
            
            int SesionID = CeC_Sesion.ValidarUsuario(e.UserName, e.Password);
            if (SesionID > 0)
                e.Authenticated = true;
            else
                e.Authenticated = false;  
                              
        }
        catch (ArgumentNullException ex)
        {
            e.Authenticated = false;
        }

        e.AuthenticationIsComplete = true;
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Código que se ejecuta al cerrarse la aplicación
        CeC_Asistencias.ParaGeneración();
    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Código que se ejecuta cuando se produce un error sin procesar
        Exception ex = this.Server.GetLastError().GetBaseException();
        CIsLog2.AgregaError("Application_Error Descripción: " + ex.Message + " \nOrigen:" + ex.Source + "\n Pila:" + ex.StackTrace);

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Código que se ejecuta al iniciarse una nueva sesión
        //CIsLog2.AgregaLog("Session_Start");
    }

    void Session_End(object sender, EventArgs e) 
    {
        // Código que se ejecuta cuando finaliza una sesión. 
        // Nota: el evento Session_End se produce solamente con el modo sessionstate
        // se establece como InProc en el archivo Web.config. Si el modo de sesión se establece como StateServer
        // o SQLServer, el evento no se produce.
        //CeC_Sesion.CierraSesion(Session);
        //CIsLog2.AgregaLog("Session_End");
    }
       
</script>
