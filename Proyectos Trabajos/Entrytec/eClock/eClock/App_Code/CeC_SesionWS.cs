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
/// Descripción breve de CeC_SesionWS
/// </summary>
public class CeC_SesionWS : CeC_Sesion
{
	public CeC_SesionWS()
	{
        Configura = new CeC_Config();
	}


    static public CeC_SesionWS Nuevo(System.Web.Services.WebService WebService)
    {
        if (WebService == null)
        {
            return null;
        }
        try
        {
            CeC_SesionWS Sesion = new CeC_SesionWS();
            Sesion.m_ObjetoWeb = WebService;
            return Sesion;
        }
        catch (System.Exception e)
        {

        }
        return null;
    }
}
