using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WF_Personas_A_Vec : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            CeC_Sesion Sesion;
            Sesion = CeC_Sesion.Nuevo(this);
//            int PersonaId = Sesion.WF_Personas_ImaS_Persona_ID;// CeC_BD.EjecutaEscalarInt("SELECT PERSONA_ID FROM EC_PERSONAS WHERE PERSONA_LINK_ID = " + Sesion.WF_Empleados_PERSONA_LINK_ID.ToString());
            string [] Valores = CeC.ObtenArregoSeparador(Sesion.Parametros, ",");
            byte[] Huella = CeC_BD.ObtenBinario("EC_PERSONAS_A_VEC", "PERSONA_ID", CeC.Convierte2Int(Valores[0]), "ALMACEN_VEC_ID", CeC.Convierte2Int(Valores[1]), "PERSONAS_A_VEC_" + CeC.Convierte2Int(Valores[2]));
            if (Huella != null)
            {
                Response.ContentType = "image/Bmp";
                Response.BinaryWrite(Huella);
            }
        }
        catch
        {
        }
    }
}