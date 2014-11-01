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

public partial class eClockAct : System.Web.UI.Page
{
    protected CeC_Sesion Sesion;
    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        if (Sesion.SESION_ID <= 0)
            return;
        string Parm = Sesion.Parametros;
        if (Parm != null && Parm.Length > 0)
        {
            if (Parm.Length > 8 && Parm.Substring(0, 8) == "PERSONAS")
            {
                if (Parm[8] == '|' || Parm[8] == 'S')
                {
                    Sesion.eClock_Agrupacion = Parm.Substring(8);
                    CIsLog2.AgregaLog(Parm.Substring(8));
                }
                else
                {
                    int ID = Convert.ToInt32(Parm.Substring(8, Parm.Length - 9));
                    Sesion.eClock_Persona_ID = ID;

                }
            }
            if (Parm.Length > 5 && Parm.Substring(0, 5) == "TURNO")
            {
                Sesion.WF_Turnos_TURNO_ID = Sesion.eClock_Turno_ID = Convert.ToInt32(Parm.Substring(5));
            }
            if (Parm.Length > 5 && Parm.Substring(0, 5) == "MUEVE")
            {
                string Valor = Parm.Substring(5);
                char[] delimiterChars = { '/' };
                string[] Valores = Valor.Split(delimiterChars);
                /*  if(Valores[1][Valores[1].Length-1] != '|')
                      Valores[1] += "|";*/
                try
                {
                    if (Valores[1][Valores[1].Length - 1] != '|')
                        Valores[1] = Valores[1] + "|";
                    int Persona_ID = Convert.ToInt32(Valores[0]);
                    CeC_Agrupaciones.AgregaAgrupacion(Sesion.SUSCRIPCION_ID, Valores[1]);
                    CeC_Empleados.AsignaAgrupacion(Persona_ID, Valores[1], Sesion.SESION_ID);
                }
                catch
                {
                    int P = Valores[0].LastIndexOf('|');

                    string NTag = Valores[0].Substring(P + 1);
                    CeC_Agrupaciones.CambiaNombreAgrupacion(Sesion.USUARIO_ID, Valores[0], Valores[1] + NTag + "|");
                }
            }
            if (Parm.Length > 6 && Parm.Substring(0, 6) == "AGREGA")
            {
                string Valor = Parm.Substring(6);
                if (Valor[Valor.Length - 1] != '|')
                    Valor += "|";
                CeC_Agrupaciones.AgregaAgrupacion(Sesion.SUSCRIPCION_ID, Valor);
            }
            if (Parm.Length > 8 && Parm.Substring(0, 8) == "ELIMINAR")
            {
                string Valor = Parm.Substring(8);

                try
                {
                    int Persona_ID = Convert.ToInt32(Valor);
                    CeC_Personas.Borra(Persona_ID, Sesion.SUSCRIPCION_ID);
                }
                catch
                {
                    CeC_Agrupaciones.BorrarAgrupacion(Sesion.SUSCRIPCION_ID, Valor, false);
                }
            }
            if (Parm.Length > 8 && Parm.Substring(0, 8) == "RENOMBRA")
            {
                string Valor = Parm.Substring(8);
                char[] delimiterChars = { '@' };
                string[] Valores = Valor.Split(delimiterChars);
                try
                {
                    int Persona_ID = Convert.ToInt32(Valores[0]);
                    CeC_Empleados.AsignaNombre(Persona_ID, Valores[1], Sesion.SESION_ID);
                }
                catch
                {
                    string Tag = Valores[0];
                    int P = Tag.LastIndexOf('|');

                    string NTag = Tag.Substring(0, P + 1) + Valores[1];
                    CeC_Agrupaciones.CambiaNombreAgrupacion(Sesion.USUARIO_ID, Valores[0], NTag + "|");
                }
            }
        }
        string UrlDestino = Sesion.UrlDestino;
        string RND = Sesion.RND;
        RND = "sdf";
        if (UrlDestino != null && UrlDestino.Length > 0)            
        {
            if (UrlDestino.IndexOf(".aspx") > 0)
            {
                if (RND != null && RND.Length > 0)
                    Sesion.Redirige(UrlDestino, true);
                else
                    RND = RND;
            }
        }

    }
}
