using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Configuration;
/// <summary>
/// Descripción breve de CeC_TimeSpan
/// </summary>
public class CeC_TimeSpan
{

    int m_TimeSpanID = -1;

    public int TimeSpanID
    {
        get { return m_TimeSpanID; }
        set { m_TimeSpanID = value; }
    }
    int m_TIMESPAN_MESES;

    public int TIMESPAN_MESES
    {
        get { return m_TIMESPAN_MESES; }
        set { m_TIMESPAN_MESES = value; }
    }
    int m_TIMESPAN_DIAS;
    public int TIMESPAN_DIAS
    {
        get { return m_TIMESPAN_DIAS; }
        set { m_TIMESPAN_DIAS = value; }
    }
    int m_TIMESPAN_HORAS;
    public int TIMESPAN_HORAS
    {
        get { return m_TIMESPAN_HORAS; }
        set { m_TIMESPAN_HORAS = value; }
    }
    int m_TIMESPAN_MINUTOS;
    public int TIMESPAN_MINUTOS
    {
        get { return m_TIMESPAN_MINUTOS; }
        set { m_TIMESPAN_MINUTOS = value; }
    }
    int m_TIMESPAN_SEGUNDOS;
    public int TIMESPAN_SEGUNDOS
    {
        get { return m_TIMESPAN_SEGUNDOS; }
        set { m_TIMESPAN_SEGUNDOS = value; }
    }
    int m_TIMESPAN_QUINCENAS;
    public int TIMESPAN_QUINCENAS
    {
        get { return m_TIMESPAN_QUINCENAS; }
        set { m_TIMESPAN_QUINCENAS = value; }
    }
    string m_TIMESPAN_QADV;
    public string TIMESPAN_QADV
    {
        get { return m_TIMESPAN_QADV; }
        set { m_TIMESPAN_QADV = value; }
    }
    int m_TIMESPAN_HABIL;
    public int TIMESPAN_HABIL
    {
        get { return m_TIMESPAN_HABIL; }
        set { m_TIMESPAN_HABIL = value; }
    }

    public CeC_TimeSpan(int TimeSpanID)
    {
        m_TimeSpanID = TimeSpanID;
        DataSet DS = (DataSet)CeC_BD.EjecutaDataSet("SELECT TIMESPAN_ID, TIMESPAN_MESES, TIMESPAN_DIAS, TIMESPAN_HORAS, " +
            "TIMESPAN_MINUTOS, TIMESPAN_SEGUNDOS, TIMESPAN_QUINCENAS, TIMESPAN_QADV, TIMESPAN_HABIL FROM EC_TIMESPAN WHERE TIMESPAN_ID = " + TimeSpanID);
        if (DS == null || DS.Tables.Count < 1 || DS.Tables[0].Rows.Count < 1)
            return;
        DataRow DR = DS.Tables[0].Rows[0];
        m_TIMESPAN_MESES = Convert.ToInt32(DR["TIMESPAN_MESES"]);
        m_TIMESPAN_DIAS = Convert.ToInt32(DR["TIMESPAN_DIAS"]);
        m_TIMESPAN_HORAS = Convert.ToInt32(DR["TIMESPAN_HORAS"]);
        m_TIMESPAN_MINUTOS = Convert.ToInt32(DR["TIMESPAN_MINUTOS"]);
        m_TIMESPAN_SEGUNDOS = Convert.ToInt32(DR["TIMESPAN_SEGUNDOS"]);
        m_TIMESPAN_QUINCENAS = Convert.ToInt32(DR["TIMESPAN_QUINCENAS"]);
        m_TIMESPAN_QADV = Convert.ToString(DR["TIMESPAN_QADV"]);
        m_TIMESPAN_HABIL = Convert.ToInt32(DR["TIMESPAN_HABIL"]);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Fecha"></param>
    /// <param name="PersonaID">Si se tiene el personaID se ingresa, de lo contrario -1</param>
    /// <returns></returns>
    public DateTime ObtenNuevaFecha(DateTime Fecha, int PersonaID)
    {
        return ObtenNuevaFecha(Fecha, -9999, PersonaID);
    }
    public DateTime ObtenNuevaFecha(DateTime Fecha)
    {
        return ObtenNuevaFecha(Fecha, -9999, -1);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Fecha"></param>
    /// <param name="CalendarioDFID">Si se tiene el Calendario de DiasFestivos se ingresa de lo contrario -9999</param>
    /// <param name="PersonaID">Si se tiene el personaID se ingresa, de lo contrario -1</param>
    /// <returns></returns>
    public DateTime ObtenNuevaFecha(DateTime Fecha, int CalendarioDFID, int PersonaID)
    {
        TimeSpan TS = new TimeSpan(m_TIMESPAN_DIAS, m_TIMESPAN_HORAS, m_TIMESPAN_MINUTOS, m_TIMESPAN_SEGUNDOS);
        Fecha = Fecha.Add(TS);
        Fecha = Fecha.AddMonths(m_TIMESPAN_MESES);

        if (m_TIMESPAN_HABIL != 0)
        {

            if (Fecha.DayOfWeek == DayOfWeek.Saturday)
            {
                if (m_TIMESPAN_HABIL < 0)
                    Fecha = Fecha.AddDays(-1);
                else
                    Fecha = Fecha.AddDays(2);
            }
            if (Fecha.DayOfWeek == DayOfWeek.Sunday)
            {
                if (m_TIMESPAN_HABIL < 0)
                    Fecha = Fecha.AddDays(1);
                else
                    Fecha = Fecha.AddDays(-2);
            }
        }
        return Fecha;
    }
}
