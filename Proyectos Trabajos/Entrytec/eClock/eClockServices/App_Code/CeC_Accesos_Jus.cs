using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using System.Reflection;

/// <summary>
/// Descripción breve de CeC_Accesos_Jus
/// </summary>
public class CeC_Accesos_Jus : CeC_Tabla
{
    int m_Acceso_Jus_Id = 0;
    [Description("Identificador unico de registro")]
    [DisplayNameAttribute("Acceso_Jus_Id")]
    public int Acceso_Jus_Id { get { return m_Acceso_Jus_Id; } set { m_Acceso_Jus_Id = value; } }
    int m_T_Inc_Acceso_Id = 0;
    [Description("Indica de que persona se refiere el acceso")]
    [DisplayNameAttribute("T_Inc_Acceso_Id")]
    public int T_Inc_Acceso_Id { get { return m_T_Inc_Acceso_Id; } set { m_T_Inc_Acceso_Id = value; } }
    int m_Acceso_Id = 0;
    [Description("Terminal en la que ocurrio dicho acceso, si es 0 significa que fue una justificacion")]
    [DisplayNameAttribute("Acceso_Id")]
    public int Acceso_Id { get { return m_Acceso_Id; } set { m_Acceso_Id = value; } }
    DateTime m_Acceso_Jus_Diff = CeC_BD.FechaNula;
    [Description("Fecha y hora en la que ocurrio el evento")]
    [DisplayNameAttribute("Acceso_Jus_Diff")]
    public DateTime Acceso_Jus_Diff { get { return m_Acceso_Jus_Diff; } set { m_Acceso_Jus_Diff = value; } }
    string m_Acceso_Jus_Desc = "";
    [Description("Indica el tipo de acceso")]
    [DisplayNameAttribute("Acceso_Jus_Desc")]
    public string Acceso_Jus_Desc { get { return m_Acceso_Jus_Desc; } set { m_Acceso_Jus_Desc = value; } }
    bool m_Acceso_Jus_Inter = false;
    [Description("Indica si el acceso se ha calculado para asistencia")]
    [DisplayNameAttribute("Acceso_Jus_Inter")]
    public bool Acceso_Jus_Inter { get { return m_Acceso_Jus_Inter; } set { m_Acceso_Jus_Inter = value; } }

    public CeC_Accesos_Jus(CeC_Sesion Sesion)
        : base("EC_ACCESOS_JUS", "ACCESO_JUS_ID", Sesion)
    {

    }
    public CeC_Accesos_Jus(int AccesoJustID, CeC_Sesion Sesion)
        : base("EC_ACCESOS_JUS", "ACCESO_JUS_ID", Sesion)
    {
        Carga(AccesoJustID.ToString(), Sesion);
    }
    public bool Nuevo(int TIncAccesoId, int AccesoId, DateTime AccesoJusDiff, string AccesoJusDesc, bool AccesoJusInter,
                        CeC_Sesion Sesion)
    {
        return Actualiza(-9999, TIncAccesoId, AccesoId, AccesoJusDiff, AccesoJusDesc, AccesoJusInter, Sesion);
    }
    public bool Actualiza(int AccesoJusId, int TIncAccesoId, int AccesoId, DateTime AccesoJusDiff, string AccesoJusDesc, bool AccesoJusInter,
                            CeC_Sesion Sesion)
    {
        try
        {
            bool Nuevo = false;
            if (!Carga(AccesoJusId.ToString(), Sesion))
                Nuevo = true;
            m_EsNuevo = Nuevo;
            Acceso_Jus_Id = AccesoJusId;
            T_Inc_Acceso_Id = TIncAccesoId;
            Acceso_Id = AccesoId;
            Acceso_Jus_Diff = AccesoJusDiff;
            Acceso_Jus_Desc = AccesoJusDesc;
            Acceso_Jus_Inter = AccesoJusInter;
            return Guarda(Sesion);
        }
        catch { }
        return false;
    }

    /// <summary>
    /// Crea una justificacion de accesos
    /// </summary>
    /// <param name="TIncAccesoId">Identificador del Tipo de Incidencia de Acceso</param>
    /// <param name="AccesoJusDiff"></param>
    /// <param name="AccesoJusDesc"></param>
    /// <param name="AccesoJusInter"></param>
    /// <param name="PersonaDiarioID"></param>
    /// <param name="HoraInicio"></param>
    /// <param name="HoraFin"></param>
    /// <param name="Justificacion"></param>
    /// <param name="Sesion"></param>
    /// <returns></returns>
    public static string NuevaJustificacion(int TIncAccesoId, DateTime AccesoJusDiff, string AccesoJusDesc, bool AccesoJusInter, int PersonaDiarioID, DateTime HoraInicio, DateTime HoraFin,
         CeC_Sesion Sesion)
    {

        try
        {
            int PersonaID = CeC_Asistencias.ObtenPersonaID(PersonaDiarioID);
            CeC_Accesos AccInicio = new CeC_Accesos(Sesion);
            if (AccInicio.Nuevo(PersonaID, 0, HoraInicio, 2, false, Sesion))
            {
                CeC_Accesos AccFin = new CeC_Accesos(Sesion);
                if (AccFin.Nuevo(PersonaID, 0, HoraFin, 3, false, Sesion))
                {
                    CeC_Accesos_Jus JusInicio = new CeC_Accesos_Jus(Sesion);
                    if (JusInicio.Nuevo(TIncAccesoId, AccInicio.Acceso_Id, CeC_BD.FechaNula, AccesoJusDesc, true, Sesion))
                    {
                        CeC_Accesos_Jus JusFin = new CeC_Accesos_Jus(Sesion);
                        if (JusFin.Nuevo(TIncAccesoId, AccFin.Acceso_Id, CeC_BD.TimeSpan2DateTime(HoraFin - HoraInicio), AccesoJusDesc, true, Sesion))
                        {
                            CeC_Asistencias.RecalculaAccesos(PersonaDiarioID.ToString());
                            return "ACCESO_JUS_ID=(" + JusInicio.Acceso_Jus_Id + "," + JusFin.Acceso_Jus_Id + ")";
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return "";
    }

    public static bool BorrarAccesosJust(string sAccesosJusIds)
    {
        string Qry = "SELECT ACCESO_ID FROM EC_ACCESOS_JUS WHERE ACCESO_JUS_ID IN (" + sAccesosJusIds + ") ";
        DataSet DS = (DataSet)CeC_BD.EjecutaDataSet(Qry);
        if (DS == null || DS.Tables.Count < 1 || DS.Tables[0].Rows.Count < 1)
            return true;
        string AccesosIds = "";
        foreach (DataRow DR in DS.Tables[0].Rows)
        {
            AccesosIds = CeC.AgregaSeparador(AccesosIds, CeC.Convierte2String(DR["ACCESO_ID"]), ",");
        }
        return CeC_Accesos.BorrarAccesos(AccesosIds);
    }
}