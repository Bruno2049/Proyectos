using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WF_EM_Faltas : System.Web.UI.Page
{
    CeC_Sesion Sesion = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.Redirect("WF_LogIn.aspx?Parametros=WF_EM_Faltas.aspx?Parametros=" + this.Request.Params["Parametros"].ToString(), true);
        //return;
        Sesion = CeC_Sesion.Nuevo(this);
        if (Sesion.SESION_ID <= 0)
        {
            Sesion.LinkLogIn = "WF_EM_Faltas.aspx?Parametros=" + Sesion.Parametros;
//            Response.Redirect("WF_LogIn.aspx?Parametros=WF_EM_Faltas.aspx", true);
            //Response.Redirect("WF_LogIn.aspx?Parametros=WF_EM_Faltas.aspx?Parametros=" + this.Request.Params["Parametros"].ToString(), true);
            //CeC_Sesion.sRedirige(this, "WF_LogIn.aspx?Parametros=WF_EM_Faltas.aspx");//@" + ;
            return;
        }
        
        ///http://localhost:49351/eClock/WF_EM_Faltas.aspx?Parametros=F2010050120100601
        ///
        try
        {
            
            string Parametros = Sesion.Parametros;
            CeC_Config Config = new CeC_Config(Sesion.USUARIO_ID);
            
            if (Parametros[0] == 'F')                
                Sesion.AsistenciaEspecifica = "FALTAS";
            else
                Sesion.AsistenciaEspecifica = "RETARDOS";
            Sesion.AsistenciaFechaInicio = new DateTime(
                Convert.ToInt32(Parametros.Substring(1, 4)),
                Convert.ToInt32(Parametros.Substring(5, 2)),
                Convert.ToInt32(Parametros.Substring(7, 2)));
            Sesion.AsistenciaFechaFin = new DateTime(
                Convert.ToInt32(Parametros.Substring(9, 4)),
                Convert.ToInt32(Parametros.Substring(13, 2)),
                Convert.ToInt32(Parametros.Substring(15, 2)));
        }
        catch { }
    }
}
