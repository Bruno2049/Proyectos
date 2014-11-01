using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Descripción breve de CeC_PreNomina
/// </summary>
public class CeC_PreNomina
{
	public CeC_PreNomina()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}

    public static List<eClockBase.Modelos.PreNomina.Reporte_PreNomina> ObtenSimple(int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, CeC_Sesion Sesion)
    {

        string QryInasistencias = "";

        string Qry = "SELECT	EC_V_ASISTENCIAS_V5.PERSONA_ID, EC_V_ASISTENCIAS_V5.PERSONA_LINK_ID, EC_V_ASISTENCIAS_V5.PERSONA_NOMBRE,  \n" + 
" 	EC_V_ASISTENCIAS_V5.AGRUPACION_NOMBRE, EC_V_ASISTENCIAS_V5.PERSONA_DIARIO_FECHA, EC_V_PERSONAS_DIARIO_EX.TIPO_INCIDENCIAS_EX_TXT, \n" + 
" EC_V_ASISTENCIAS_V5.PERSONA_D_HE_DOBLE, EC_V_ASISTENCIAS_V5.PERSONA_D_HE_TRIPLE \n" +
" FROM	EC_V_ASISTENCIAS_V5 LEFT OUTER JOIN \n" + 
" 	EC_V_PERSONAS_DIARIO_EX ON EC_V_ASISTENCIAS_V5.PERSONA_DIARIO_ID = EC_V_PERSONAS_DIARIO_EX.PERSONA_DIARIO_ID \n" + 
" WHERE        (PERSONA_DIARIO_FECHA >= @FECHA_INICIAL@) AND  " +
                        " (PERSONA_DIARIO_FECHA < @FECHA_FINAL@) " +
                       CeC_Asistencias.ValidaAgrupacion(Persona_ID, Sesion.USUARIO_ID, Agrupacion, false) +
" \n ORDER BY EC_V_ASISTENCIAS_V5.PERSONA_ID, TIPO_INCIDENCIAS_EX_TXT";
        

        Qry = CeC_BD.AsignaParametro(Qry, "USUARIO_ID", Sesion.USUARIO_ID);
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_INICIAL", FechaInicial);
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_FINAL", FechaFinal);
        Qry = CeC_BD.AsignaParametro(Qry, "AGRUPACION_NOMBRE", Agrupacion + "%");

        string TIEXDoble = Cec_Incidencias.ObtenTipoIncidenciaExTXT(Sesion.ConfiguraSuscripcion.TipoIncidenciaExHoraExtraDoble);
        string TIEXTriple = Cec_Incidencias.ObtenTipoIncidenciaExTXT(Sesion.ConfiguraSuscripcion.TipoIncidenciaExHoraExtraTriple);

        

        DataSet DS = (DataSet)CeC_BD.EjecutaDataSet(Qry);

        List<eClockBase.Modelos.PreNomina.Reporte_PreNomina> ModelReportes = new List<eClockBase.Modelos.PreNomina.Reporte_PreNomina>();
        if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
        {
            int AntPersonaID = -1;
            string AntExTxt = "";
            int NoIncidencia = 0;

            string Guardado = "";

            decimal AcuHE_Doble = 0;
            decimal AcuHE_Triple = 0;
            string GuardadoHE = "";
            eClockBase.Modelos.PreNomina.Reporte_PreNomina ModelReporte = null;
            foreach (DataRow DR in DS.Tables[0].Rows)
            {
                try
                {
                    int PERSONA_ID = CeC.Convierte2Int(DR["PERSONA_ID"]);
                    int PERSONA_LINK_ID = CeC.Convierte2Int(DR["PERSONA_LINK_ID"]);
                    string PERSONA_NOMBRE = CeC.Convierte2String(DR["PERSONA_NOMBRE"]);
                    string AGRUPACION_NOMBRE = CeC.Convierte2String(DR["AGRUPACION_NOMBRE"]);
                    DateTime PERSONA_DIARIO_FECHA = CeC.Convierte2DateTime(DR["PERSONA_DIARIO_FECHA"]);
                    string TIPO_INCIDENCIAS_EX_TXT = CeC.Convierte2String(DR["TIPO_INCIDENCIAS_EX_TXT"]);
                    decimal PERSONA_D_HE_DOBLE = 0;// CeC.Convierte2Decimal(DR["PERSONA_D_HE_DOBLE"]);
                    decimal PERSONA_D_HE_TRIPLE = 0;// CeC.Convierte2Decimal(DR["PERSONA_D_HE_TRIPLE"]);

                    if (!DR.IsNull("PERSONA_D_HE_DOBLE"))
                        PERSONA_D_HE_DOBLE = CeC.Convierte2Decimal(DR["PERSONA_D_HE_DOBLE"]);
                    if (!DR.IsNull("PERSONA_D_HE_TRIPLE"))
                        PERSONA_D_HE_TRIPLE = CeC.Convierte2Decimal(DR["PERSONA_D_HE_TRIPLE"]);
                    if (AntPersonaID != PERSONA_ID)
                    {
                        AntPersonaID = PERSONA_ID;
                        AntExTxt = "";
                        NoIncidencia = 0;
                        ModelReporte = new eClockBase.Modelos.PreNomina.Reporte_PreNomina();
                        ModelReportes.Add(ModelReporte);
                        ModelReporte.PERSONA_ID = PERSONA_ID;
                        ModelReporte.PERSONA_LINK_ID = PERSONA_LINK_ID;
                        ModelReporte.PERSONA_NOMBRE = PERSONA_NOMBRE;
                        ModelReporte.AGRUPACION_NOMBRE = AGRUPACION_NOMBRE;
                        ModelReporte.PRE_NOMINA = "";

                        AcuHE_Doble = 0;
                        AcuHE_Triple = 0;
                        GuardadoHE = "";
                    }

                    if (!DR.IsNull("TIPO_INCIDENCIAS_EX_TXT"))
                    {
                        if (TIPO_INCIDENCIAS_EX_TXT != AntExTxt)
                        {
                            AntExTxt = TIPO_INCIDENCIAS_EX_TXT;
                            NoIncidencia = 1;
                        }
                        else
                        {
                            NoIncidencia++;
                            ModelReporte.PRE_NOMINA = ModelReporte.PRE_NOMINA.Replace(Guardado, "");
                        }

                        Guardado = NoIncidencia.ToString() + " " + AntExTxt;
                        if (ModelReporte.PRE_NOMINA.Length > 0)
                            Guardado = ", " + Guardado;
                        ModelReporte.PRE_NOMINA += Guardado;
                    }

                    AcuHE_Doble += PERSONA_D_HE_DOBLE;
                    AcuHE_Triple += PERSONA_D_HE_TRIPLE;
                    if (AcuHE_Doble > 0 || AcuHE_Triple > 0)
                    {
                        if(GuardadoHE != "")
                            ModelReporte.PRE_NOMINA = ModelReporte.PRE_NOMINA.Replace(GuardadoHE, "");
                        if (ModelReporte.PRE_NOMINA.Length > 0)
                            GuardadoHE = ", ";
                        else
                            GuardadoHE = "";

                        if (AcuHE_Doble > 0)
                            GuardadoHE += AcuHE_Doble.ToString() + " " + TIEXDoble;
                        if (AcuHE_Triple > 0)
                            GuardadoHE += ", " + AcuHE_Triple.ToString() + " " + TIEXTriple;
                        ModelReporte.PRE_NOMINA += GuardadoHE;
                    }
                }
                catch { }
            }
        }
        return ModelReportes;
    }
}