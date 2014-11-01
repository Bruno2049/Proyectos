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
/// Descripción breve de CMd_BahiaPrinc
/// </summary>
public class CMd_BahiaPrinc : CMd_Base
{
    public CMd_BahiaPrinc()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    /// <summary>
    /// Obtiene el nombre del módulo
    /// </summary>
    /// <returns></returns>
    public override string LeeNombre()
    {
        return "Programación especifica de bahía principe";
    }


    public bool CalculaDescansos()
    {
        //Al convertir el dia de semana en entero el valor 0 será para domingo
        int DiaUC = Convert.ToInt32(UltimoCalculo.DayOfWeek) + 1;
        DateTime SigDia = UltimoCalculo;
        if (DiaUC != Dia_Semana_ID_Inicio)
        {
            SigDia = UltimoCalculo.Date.AddDays(7 - DiaUC + Dia_Semana_ID_Inicio + 7);
        }
        else
            SigDia = UltimoCalculo.Date.AddDays(7);

        if (DateTime.Now >= SigDia)
        {
            string Comando = "UPDATE EC_PERSONAS_DIARIO SET TIPO_INC_SIS_ID = 10 where PERSONA_DIARIO_ID in( select min(PERSONA_DIARIO_ID) as PERSONA_DIARIO_ID " +
                            "from ( " +
                            "SELECT     PERSONA_ID, PERSONA_DIARIO_ID AS PERSONA_DIARIO_ID,   " +
                            " PERSONA_DIARIO_FECHA AS PERSONA_DIARIO_FECHA " +
                            "FROM         EC_PERSONAS_DIARIO " +
                            "WHERE     (INCIDENCIA_ID = 0) AND (TIPO_INC_SIS_ID = 12) AND (PERSONA_DIARIO_FECHA >= " + CeC_BD.SqlFecha(UltimoCalculo) + ") AND (PERSONA_DIARIO_FECHA < " + CeC_BD.SqlFecha(SigDia) + ") ) t group by PERSONA_ID) ";

            if (CeC_BD.EjecutaComando(Comando) >= 0)
            {
                CIsLog2.AgregaLog("Se generaron automaticamente los dias de descanso " + UltimoCalculo.ToShortDateString() + " - " + SigDia.ToShortDateString());

                UltimoCalculo = SigDia;
                CalculaDescansos();
                return true;
            }
            else
                CIsLog2.AgregaError("No se pudo calcular los dias de descanso " + UltimoCalculo.ToShortDateString() + " - " + SigDia.ToShortDateString());
        }

        return false;
    }

    /// <summary>
    /// esta función será ejecutada en la clase de asistencias una instante
    /// despues de generar las faltas, y una vez al día
    /// </summary>
    /// <returns></returns>
    public override bool EjecutarUnaVezAlDia()
    {
        try
        {
            CalculaDescansos();
            GuardaParametros();
            //EnviarChecadas();
            return true;
        }
        catch
        {
        }
        return false;
    }

    /*
     Domingo = 1
     Sabado = 7
     */

    int m_Dia_Semana_ID_Inicio = 2;
    [Description("Contiene el identificador del día que se inicia la semana para calcular descansos")]
    [DisplayNameAttribute("Dia_Semana_ID_Inicio")]
    public int Dia_Semana_ID_Inicio
    {
        get { return m_Dia_Semana_ID_Inicio; }
        set { m_Dia_Semana_ID_Inicio = value; }
    }

    DateTime m_UltimoCalculo = DateTime.Now;
    //    DateTime m_UltimoCalculo = new DateTime(2002, 09, 24);
    [Description("Fecha y hora de la ultima vez que se calculo los descansos")]
    [DisplayNameAttribute("UltimoCalculo")]
    public DateTime UltimoCalculo
    {
        get { return m_UltimoCalculo; }
        set { m_UltimoCalculo = value; }
    }

}
