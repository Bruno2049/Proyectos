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
/// <summary>
/// Descripción breve de CeC_Turnos_Dia
/// </summary>
public class CeC_Turnos_Dia : CeC_Tabla
{
    int m_Turno_Dia_Id = 0;
    [Description("Identificador del turno semanal")]
    [DisplayNameAttribute("Turno_Dia_Id")]
    public int Turno_Dia_Id { get { return m_Turno_Dia_Id; } set { m_Turno_Dia_Id = value; } }
    DateTime m_Turno_Dia_Hemin = CeC_BD.FechaNula;
    [Description("Hora mínima permitida de entrada, en Horario abierto se guardara la hora de inicio de dia")]
    [DisplayNameAttribute("Turno_Dia_Hemin")]
    public DateTime Turno_Dia_Hemin { get { return m_Turno_Dia_Hemin; } set { m_Turno_Dia_Hemin = value; } }
    DateTime m_Turno_Dia_He = CeC_BD.FechaNula;
    [Description("Hora normal de entrada")]
    [DisplayNameAttribute("Turno_Dia_He")]
    public DateTime Turno_Dia_He { get { return m_Turno_Dia_He; } set { m_Turno_Dia_He = value; } }
    DateTime m_Turno_Dia_Hemax = CeC_BD.FechaNula;
    [Description("Hora máxima permitida de entrada")]
    [DisplayNameAttribute("Turno_Dia_Hemax")]
    public DateTime Turno_Dia_Hemax { get { return m_Turno_Dia_Hemax; } set { m_Turno_Dia_Hemax = value; } }
    DateTime m_Turno_Dia_Heretardo = CeC_BD.FechaNula;
    [Description("Hora desde (>=) que será contemplado como retardo")]
    [DisplayNameAttribute("Turno_Dia_Heretardo")]
    public DateTime Turno_Dia_Heretardo { get { return m_Turno_Dia_Heretardo; } set { m_Turno_Dia_Heretardo = value; } }
    DateTime m_Turno_Dia_Hsmin = CeC_BD.FechaNula;
    [Description("Hora mínima permitida de salida")]
    [DisplayNameAttribute("Turno_Dia_Hsmin")]
    public DateTime Turno_Dia_Hsmin { get { return m_Turno_Dia_Hsmin; } set { m_Turno_Dia_Hsmin = value; } }
    DateTime m_Turno_Dia_Hs = CeC_BD.FechaNula;
    [Description("Hora normal de salida")]
    [DisplayNameAttribute("Turno_Dia_Hs")]
    public DateTime Turno_Dia_Hs { get { return m_Turno_Dia_Hs; } set { m_Turno_Dia_Hs = value; } }
    DateTime m_Turno_Dia_Hsmax = CeC_BD.FechaNula;
    [Description("Hora máxima de salida")]
    [DisplayNameAttribute("Turno_Dia_Hsmax")]
    public DateTime Turno_Dia_Hsmax { get { return m_Turno_Dia_Hsmax; } set { m_Turno_Dia_Hsmax = value; } }
    DateTime m_Turno_Dia_Hbloque = CeC_BD.FechaNula;
    [Description("Indica el boque de acceso (ej. entradas cada media hora)")]
    [DisplayNameAttribute("Turno_Dia_Hbloque")]
    public DateTime Turno_Dia_Hbloque { get { return m_Turno_Dia_Hbloque; } set { m_Turno_Dia_Hbloque = value; } }
    DateTime m_Turno_Dia_Hbloquet = CeC_BD.FechaNula;
    [Description("Indica la tolerancia que existira para cada bloque")]
    [DisplayNameAttribute("Turno_Dia_Hbloquet")]
    public DateTime Turno_Dia_Hbloquet { get { return m_Turno_Dia_Hbloquet; } set { m_Turno_Dia_Hbloquet = value; } }
    DateTime m_Turno_Dia_Htiempo = CeC_BD.FechaNula;
    [Description("Tiempo que tiene que laborar el empleado para cumplir su jornada de trabajo si es cero se no se tomara en cuenta esta opción, en caso contrario si no cuenta con este tiempo tendra una alerta por salida antes de tiempo")]
    [DisplayNameAttribute("Turno_Dia_Htiempo")]
    public DateTime Turno_Dia_Htiempo { get { return m_Turno_Dia_Htiempo; } set { m_Turno_Dia_Htiempo = value; } }
    bool m_Turno_Dia_Haycomida = true;
    [Description("Indica si este horario diario tiene comida")]
    [DisplayNameAttribute("Turno_Dia_Haycomida")]
    public bool Turno_Dia_Haycomida { get { return m_Turno_Dia_Haycomida; } set { m_Turno_Dia_Haycomida = value; } }
    DateTime m_Turno_Dia_Hcs = CeC_BD.FechaNula;
    [Description("Hora de salida a comer")]
    [DisplayNameAttribute("Turno_Dia_Hcs")]
    public DateTime Turno_Dia_Hcs { get { return m_Turno_Dia_Hcs; } set { m_Turno_Dia_Hcs = value; } }
    DateTime m_Turno_Dia_Hcr = CeC_BD.FechaNula;
    [Description("Hora de regreso de comer")]
    [DisplayNameAttribute("Turno_Dia_Hcr")]
    public DateTime Turno_Dia_Hcr { get { return m_Turno_Dia_Hcr; } set { m_Turno_Dia_Hcr = value; } }
    DateTime m_Turno_Dia_Hctiempo = CeC_BD.FechaNula;
    [Description("Tendra 0 si se trata de que tiene un horario fijo de comida, y cualquier valor en caso de que tenga un tiempo de comida ej 60 minutos")]
    [DisplayNameAttribute("Turno_Dia_Hctiempo")]
    public DateTime Turno_Dia_Hctiempo { get { return m_Turno_Dia_Hctiempo; } set { m_Turno_Dia_Hctiempo = value; } }
    DateTime m_Turno_Dia_Hctolera = CeC_BD.FechaNula;
    [Description("Minutos de tolerancia para comer")]
    [DisplayNameAttribute("Turno_Dia_Hctolera")]
    public DateTime Turno_Dia_Hctolera { get { return m_Turno_Dia_Hctolera; } set { m_Turno_Dia_Hctolera = value; } }
    bool m_Turno_Dia_Phex = true;
    [Description("Indica si la terminal no permitirá entrar si esta fuera de horario de entrada  ")]
    [DisplayNameAttribute("Turno_Dia_Phex")]
    public bool Turno_Dia_Phex { get { return m_Turno_Dia_Phex; } set { m_Turno_Dia_Phex = value; } }
    bool m_Turno_Dia_No_Asis = false;
    [Description("Indica que este turno día no generará asistencia")]
    [DisplayNameAttribute("Turno_Dia_No_Asis")]
    public bool Turno_Dia_No_Asis { get { return m_Turno_Dia_No_Asis; } set { m_Turno_Dia_No_Asis = value; } }
    DateTime m_Turno_Dia_Heretardo_B = CeC_BD.FechaNula;
    [Description("Hora desde (>=) que será contemplado como retardo minimo")]
    [DisplayNameAttribute("Turno_Dia_Heretardo_B")]
    public DateTime Turno_Dia_Heretardo_B { get { return m_Turno_Dia_Heretardo_B; } set { m_Turno_Dia_Heretardo_B = value; } }
    DateTime m_Turno_Dia_Heretardo_C = CeC_BD.FechaNula;
    [Description("Hora desde (>=) que será contemplado como retardo maximo")]
    [DisplayNameAttribute("Turno_Dia_Heretardo_C")]
    public DateTime Turno_Dia_Heretardo_C { get { return m_Turno_Dia_Heretardo_C; } set { m_Turno_Dia_Heretardo_C = value; } }
    DateTime m_Turno_Dia_Heretardo_D = CeC_BD.FechaNula;
    [Description("Hora desde (>=) que será contemplado como retardo superior")]
    [DisplayNameAttribute("Turno_Dia_Heretardo_D")]
    public DateTime Turno_Dia_Heretardo_D { get { return m_Turno_Dia_Heretardo_D; } set { m_Turno_Dia_Heretardo_D = value; } }

    //public CeC_Turnos_Dia()
    //{
    //    //
    //    // TODO: Agregar aquí la lógica del constructor
    //    //
    //}

    public CeC_Turnos_Dia(CeC_Sesion Sesion)
        : base("EC_TURNOS_DIA", "TURNO_DIA_ID", Sesion)
    { }

    public CeC_Turnos_Dia(int TurnoDiaId, CeC_Sesion Sesion)
        : base("EC_TURNOS_DIA", "TURNO_DIA_ID", Sesion)
    {
        Carga(TurnoDiaId.ToString(), Sesion);
    }
    /// <summary>
    /// Carga, edita o crea nuevas configuracion del Turno Dia
    /// </summary>
    /// <param name="TurnoDiaId">Identificador del turno semanal</param>
    /// <param name="TurnoDiaHemin">Hora mínima permitida de entrada, en Horario abierto se guardara la hora de inicio de dia</param>
    /// <param name="TurnoDiaHe">Hora normal de entrada</param>
    /// <param name="TurnoDiaHemax">Hora máxima permitida de entrada</param>
    /// <param name="TurnoDiaHeretardo">Hora desde (>=) que será contemplado como retardo</param>
    /// <param name="TurnoDiaHsmin">Hora mínima permitida de salida</param>
    /// <param name="TurnoDiaHs">Hora normal de salida</param>
    /// <param name="TurnoDiaHsmax">Hora máxima de salida</param>
    /// <param name="TurnoDiaHbloque">Indica el boque de acceso (ej. entradas cada media hora)</param>
    /// <param name="TurnoDiaHbloquet">Indica la tolerancia que existira para cada bloque</param>
    /// <param name="TurnoDiaHtiempo">Tiempo que tiene que laborar el empleado para cumplir su jornada de trabajo si es cero se no se tomara en cuenta esta opción, en caso contrario si no cuenta con este tiempo tendra una alerta por salida antes de tiempo</param>
    /// <param name="TurnoDiaHaycomida">Indica si este horario diario tiene comida</param>
    /// <param name="TurnoDiaHcs">Hora de salida a comer</param>
    /// <param name="TurnoDiaHcr">Hora de regreso de comer</param>
    /// <param name="TurnoDiaHctiempo">Tendra 0 si se trata de que tiene un horario fijo de comida, y cualquier valor en caso de que tenga un tiempo de comida ej 60 minutos</param>
    /// <param name="TurnoDiaHctolera">Minutos de tolerancia para comer</param>
    /// <param name="TurnoDiaPhex">Indica si la terminal no permitirá entrar si esta fuera de horario de entrada  </param>
    /// <param name="TurnoDiaNoAsis">Indica que este turno día no generará asistencia</param>
    /// <param name="TurnoDiaHeretardoB">Hora desde (>=) que será contemplado como retardo minimo</param>
    /// <param name="TurnoDiaHeretardoC">Hora desde (>=) que será contemplado como retardo maximo</param>
    /// <param name="TurnoDiaHeretardoD">Hora desde (>=) que será contemplado como retardo superior</param>
    /// <param name="Sesion">Variable de Sesion</param>
    /// <returns>True si se realizaron los cambios correctamente. Falso en otro caso</returns>
    public bool Actualiza(int TurnoDiaId, DateTime TurnoDiaHemin, DateTime TurnoDiaHe, DateTime TurnoDiaHemax, DateTime TurnoDiaHeretardo, DateTime TurnoDiaHsmin, DateTime TurnoDiaHs, DateTime TurnoDiaHsmax, DateTime TurnoDiaHbloque, DateTime TurnoDiaHbloquet, DateTime TurnoDiaHtiempo, bool TurnoDiaHaycomida, DateTime TurnoDiaHcs, DateTime TurnoDiaHcr, DateTime TurnoDiaHctiempo, DateTime TurnoDiaHctolera, bool TurnoDiaPhex, bool TurnoDiaNoAsis, DateTime TurnoDiaHeretardoB, DateTime TurnoDiaHeretardoC, DateTime TurnoDiaHeretardoD,
        CeC_Sesion Sesion)
    {
        try
        {
            bool Nuevo = false;
            if (!Carga(TurnoDiaId.ToString(), Sesion))
                Nuevo = true;
            m_EsNuevo = Nuevo;
            Turno_Dia_Id = TurnoDiaId;
            Turno_Dia_Hemin = TurnoDiaHemin;
            Turno_Dia_He = TurnoDiaHe;
            Turno_Dia_Hemax = TurnoDiaHemax;
            Turno_Dia_Heretardo = TurnoDiaHeretardo;
            Turno_Dia_Hsmin = TurnoDiaHsmin;
            Turno_Dia_Hs = TurnoDiaHs;
            Turno_Dia_Hsmax = TurnoDiaHsmax;
            Turno_Dia_Hbloque = TurnoDiaHbloque;
            Turno_Dia_Hbloquet = TurnoDiaHbloquet;
            Turno_Dia_Htiempo = TurnoDiaHtiempo;
            Turno_Dia_Haycomida = TurnoDiaHaycomida;
            Turno_Dia_Hcs = TurnoDiaHcs;
            Turno_Dia_Hcr = TurnoDiaHcr;
            Turno_Dia_Hctiempo = TurnoDiaHctiempo;
            Turno_Dia_Hctolera = TurnoDiaHctolera;
            Turno_Dia_Phex = TurnoDiaPhex;
            Turno_Dia_No_Asis = TurnoDiaNoAsis;
            Turno_Dia_Heretardo_B = TurnoDiaHeretardoB;
            Turno_Dia_Heretardo_C = TurnoDiaHeretardoC;
            Turno_Dia_Heretardo_D = TurnoDiaHeretardoD;
            if (Guarda(Sesion))
            {
                return true;
            }
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError("CeC_Turnos_Dia.Actualiza", ex);
        }
        return false;
    }
    public static DateTime ObtenHS(int PersonaID)
    {
        string Qry = "SELECT     EC_TURNOS_DIA.TURNO_DIA_HS " +
"FROM         EC_PERSONAS INNER JOIN " +
"EC_TURNOS ON EC_PERSONAS.TURNO_ID = EC_TURNOS.TURNO_ID INNER JOIN " +
"EC_TURNOS_SEMANAL_DIA ON EC_TURNOS.TURNO_ID = EC_TURNOS_SEMANAL_DIA.TURNO_ID INNER JOIN " +
"EC_TURNOS_DIA ON EC_TURNOS_SEMANAL_DIA.TURNO_DIA_ID = EC_TURNOS_DIA.TURNO_DIA_ID " +
"WHERE     (EC_PERSONAS.PERSONA_ID = " + PersonaID + ") AND (EC_TURNOS_SEMANAL_DIA.DIA_SEMANA_ID = 2) ";
        return CeC_BD.EjecutaEscalarDateTime(Qry);
    }
}