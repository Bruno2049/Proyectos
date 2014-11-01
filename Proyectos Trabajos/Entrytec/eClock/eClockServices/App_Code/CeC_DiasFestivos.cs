using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de CeC_DiasFestivos
/// </summary>
public class CeC_DiasFestivos
{
	public CeC_DiasFestivos()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}

    /// <summary>
    /// Devuelve los dias festivos.
    /// </summary>
    /// <param name="Dia"></param>
    /// <returns></returns>
    public static int EsDiaFestivo(DateTime Dia, int PersonaID)
    {
        string Qry = "SELECT        EC_DIAS_FESTIVOS.DIA_FESTIVO_ID " +
                        " FROM            EC_PERSONAS INNER JOIN " +
                        " EC_DIAS_FESTIVOS ON EC_PERSONAS.CALENDARIO_DF_ID = EC_DIAS_FESTIVOS.CALENDARIO_DF_ID INNER JOIN " +
                        " EC_AUTONUM ON EC_PERSONAS.SUSCRIPCION_ID = EC_AUTONUM.SUSCRIPCION_ID AND  " +
                        " EC_DIAS_FESTIVOS.DIA_FESTIVO_ID = EC_AUTONUM.AUTONUM_TABLA_ID " +
                        " WHERE        (EC_AUTONUM.AUTONUM_TABLA = 'EC_DIAS_FESTIVOS') AND (EC_DIAS_FESTIVOS.DIA_FESTIVO_BORRADO = 0) " +
                        " AND EC_PERSONAS.PERSONA_ID = " + PersonaID +
                        " AND DIA_FESTIVO_FECHA = " + CeC_BD.SqlFecha(Dia);
        return CeC_BD.EjecutaEscalarInt(Qry);            
    }

    public static System.Data.DataSet ObtenDias(int PersonaID)
    {
        string Qry = "SELECT        EC_DIAS_FESTIVOS.DIA_FESTIVO_FECHA, DIA_FESTIVO_NOMBRE " +
                        " FROM            EC_PERSONAS INNER JOIN " +
                        " EC_DIAS_FESTIVOS ON EC_PERSONAS.CALENDARIO_DF_ID = EC_DIAS_FESTIVOS.CALENDARIO_DF_ID INNER JOIN " +
                        " EC_AUTONUM ON EC_PERSONAS.SUSCRIPCION_ID = EC_AUTONUM.SUSCRIPCION_ID AND  " +
                        " EC_DIAS_FESTIVOS.DIA_FESTIVO_ID = EC_AUTONUM.AUTONUM_TABLA_ID " +
                        " WHERE        (EC_AUTONUM.AUTONUM_TABLA = 'EC_DIAS_FESTIVOS') AND (EC_DIAS_FESTIVOS.DIA_FESTIVO_BORRADO = 0) " +
                        " AND EC_PERSONAS.PERSONA_ID = " + PersonaID;
        return CeC_BD.EjecutaDataSet(Qry);      
    }
}