using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;


/// <summary>
/// Descripción breve de WS_PersonasSemana
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class WS_PersonasSemana : System.Web.Services.WebService
{

    public WS_PersonasSemana()
    {

        //Eliminar la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hola a todos";
    }
    public int ObtenNoDiasDif(string NombreCampo)
    {
        if (NombreCampo.Length != 8 && NombreCampo.Length != 13)
            return -1;
        int DiaSuma = 0;
        if (NombreCampo.Substring(0, 7) == "TURNO_D")
            DiaSuma = Convert.ToInt32(NombreCampo.Substring(7));
        else
            if (NombreCampo.Substring(0, 12) == "ASISTENCIA_D")
                DiaSuma = Convert.ToInt32(NombreCampo.Substring(12));
            else
                return -1;
        return DiaSuma;
    }
    public bool lAsignaHorario(int Persona_Link_ID, string Campo, int Turno_ID, CeC_Sesion Sesion)
    {

        try
        {
            CIsLog2.AgregaLog("AsignaHorario(int Persona_Link_ID, string Campo, int Turno_ID) values (" + Persona_Link_ID + "," + Campo + "," + Turno_ID + ")");

            if (Sesion == null)
                return false;
            DS_Personas_Semana.CambiosHorarioDataTable DT = Sesion.WF_PersonasSemanaCambiosHorarioDataTable;
            if (DT == null)
            {
                DT = new DS_Personas_Semana.CambiosHorarioDataTable();
            }

            if (Campo == "TURNO_NOMBRE")
            {
                if (Turno_ID > 0)
                {
                    DT.AddCambiosHorarioRow(Persona_Link_ID,
                        CeC_BD.FechaNula, Turno_ID);
                    Sesion.WF_PersonasSemanaCambiosHorarioDataTable = DT;


                }
                return false;
            }
            if (Campo.Length != 8 && Campo.Length != 13)
                return false;
            int DiaSuma = ObtenNoDiasDif(Campo);
            if (DiaSuma < 0)
                return false;
            DT.AddCambiosHorarioRow(Persona_Link_ID,
            Sesion.WF_EmpleadosFil_FechaI.AddDays(DiaSuma), Turno_ID);
            Sesion.WF_PersonasSemanaCambiosHorarioDataTable = DT;
            return true;
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return false;
    }
    [WebMethod(Description = "AsignaHorario", EnableSession = true)]
    public bool AsignaHorario(int Persona_Link_ID, string Campo, int Turno_ID)
    {
        return lAsignaHorario(Persona_Link_ID, Campo, Turno_ID, CeC_SesionWS.Nuevo(this));

    }
}

