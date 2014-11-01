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

public partial class WF_PersonasSemanaAct : System.Web.UI.Page
{
    protected CeC_Sesion Sesion;
    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        string Parm = Sesion.Parametros;
        if (Parm != null && Parm.Length > 0)
        {
            try
            {
                string[] Elementos = Parm.Split(new char[] { '@' });
                int Turno_ID = Convert.ToInt32(Elementos[0]);
                for (int Cont = 1; Cont < Elementos.Length; Cont++)
                {
                    string[] Valores = Elementos[Cont].Split(new char[] { '-' });
                    int Persona_Link_ID = Convert.ToInt32(Valores[0]);
                    string CampoNombre = Valores[1];
                    WS_PersonasSemana PS = new WS_PersonasSemana();
                    PS.lAsignaHorario(Persona_Link_ID, CampoNombre, Turno_ID, Sesion);
                }
            }
            catch (Exception ex)
            {
                CIsLog2.AgregaError(ex);
            }
        }
    }
}