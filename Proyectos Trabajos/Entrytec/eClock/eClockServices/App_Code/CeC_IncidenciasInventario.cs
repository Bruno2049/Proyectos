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

using System.Linq;
using System.Collections.Generic;

/// <summary>
/// Descripción breve de CeC_IncidenciasInventario
/// </summary>
public class CeC_IncidenciasInventario
{
    public enum TipoAlmacenIncidencias
    {
        Automatico = -1,
        Inicial,
        Normal,
        Proporcional,
        Adelantada,
        Limpiado,
        Descontado,
        Correccion
    }
    public CeC_IncidenciasInventario()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public int ALMACEN_INC_ID = 0;
    public int TIPO_INCIDENCIA_R_ID = 0;
    public int TIPO_ALMACEN_INC_ID = 0;
    public int PERSONA_ID = 0;
    public DateTime ALMACEN_INC_FECHA = CeC_BD.FechaNula;
    public decimal ALMACEN_INC_NO = 0;
    public decimal ALMACEN_INC_SALDO = 0;
    public DateTime ALMACEN_INC_SIGUIENTE = CeC_BD.FechaNula;
    public bool ALMACEN_INC_AUTOM = false;
    public string ALMACEN_INC_COMEN = "";
    public string ALMACEN_INC_EXTRAS = "";


    public CeC_IncidenciasInventario(int AlmacenInc_Id)
    {
        string Qry = "SELECT " + Campos + " FROM EC_ALMACEN_INC WHERE ALMACEN_INC_ID = " + AlmacenInc_Id.ToString();
        DataSet DS = (DataSet)CeC_BD.EjecutaDataSet(Qry);
        if (DS == null || DS.Tables.Count < 1 || DS.Tables[0].Rows.Count < 1)
            return;
        Carga(DS.Tables[0].Rows[0]);
    }
    public CeC_IncidenciasInventario(int PersonaID, int TipoIncidenciaRID, DateTime Fecha, TipoAlmacenIncidencias Tipo)
    {
        string Qry = "SELECT " + Campos + " FROM EC_ALMACEN_INC WHERE TIPO_INCIDENCIA_R_ID = " + TipoIncidenciaRID + " AND TIPO_ALMACEN_INC_ID = " + CeC.Convierte2Int(Tipo)
            + " AND ALMACEN_INC_FECHA = " + CeC_BD.SqlFecha(Fecha) + " AND PERSONA_ID = " + PersonaID;
        DataSet DS = (DataSet)CeC_BD.EjecutaDataSet(Qry);
        if (DS == null || DS.Tables.Count < 1 || DS.Tables[0].Rows.Count < 1)
            return;
        Carga(DS.Tables[0].Rows[0]);
    }
    public bool Carga(DataRow Fila)
    {
        try
        {
            DataRow DR = Fila;
            ALMACEN_INC_ID = CeC.Convierte2Int(DR["ALMACEN_INC_ID"]);
            TIPO_INCIDENCIA_R_ID = CeC.Convierte2Int(DR["TIPO_INCIDENCIA_R_ID"]);
            TIPO_ALMACEN_INC_ID = CeC.Convierte2Int(DR["TIPO_ALMACEN_INC_ID"]);
            PERSONA_ID = CeC.Convierte2Int(DR["PERSONA_ID"]);
            ALMACEN_INC_FECHA = CeC.Convierte2DateTime(DR["ALMACEN_INC_FECHA"]);
            ALMACEN_INC_NO = CeC.Convierte2Decimal(DR["ALMACEN_INC_NO"]);
            ALMACEN_INC_SALDO = CeC.Convierte2Decimal(DR["ALMACEN_INC_SALDO"]);
            ALMACEN_INC_SIGUIENTE = CeC.Convierte2DateTime(DR["ALMACEN_INC_SIGUIENTE"]);
            ALMACEN_INC_AUTOM = CeC.Convierte2Bool(DR["ALMACEN_INC_AUTOM"]);
            ALMACEN_INC_COMEN = CeC.Convierte2String(DR["ALMACEN_INC_COMEN"]);
            ALMACEN_INC_EXTRAS = CeC.Convierte2String(DR["ALMACEN_INC_EXTRAS"]);


            return true;
        }
        catch { }
        return false;
    }
    public static string Campos
    {
        get { return " ALMACEN_INC_ID, TIPO_INCIDENCIA_R_ID, TIPO_ALMACEN_INC_ID, PERSONA_ID, ALMACEN_INC_FECHA, ALMACEN_INC_NO, ALMACEN_INC_SALDO, ALMACEN_INC_SIGUIENTE, ALMACEN_INC_AUTOM, ALMACEN_INC_COMEN, ALMACEN_INC_EXTRAS "; }
    }

    public static decimal ObtenSaldo(string TipoIncidenciaNombre, int Persona_ID, int Usuario_ID, string Agrupacion)
    {
        string Filtro = CeC_Asistencias.ValidaAgrupacion(Persona_ID, Usuario_ID, Agrupacion, true);
        if (Filtro.Length > 0)
            Filtro = " AND " + Filtro;
        string Qry = "SELECT MAX(ALMACEN_INC_ID) AS ALMACEN_INC_ID FROM EC_ALMACEN_INC WHERE TIPO_INCIDENCIA_R_ID in (SELECT TIPO_INCIDENCIA_R_ID FROM EC_TIPO_INCIDENCIAS,EC_TIPO_INCIDENCIAS_R " +
            "WHERE EC_TIPO_INCIDENCIAS.TIPO_INCIDENCIA_ID=EC_TIPO_INCIDENCIAS_R.TIPO_INCIDENCIA_ID AND TIPO_INCIDENCIA_R_BORRADO = 0 AND TIPO_INCIDENCIA_NOMBRE = '" + TipoIncidenciaNombre + "') " + Filtro + " GROUP BY PERSONA_ID";
        decimal Saldo = CeC_BD.EjecutaEscalarDecimal("SELECT SUM(ALMACEN_INC_SALDO) AS ALMACEN_INC_SALDO FROM EC_ALMACEN_INC WHERE ALMACEN_INC_ID IN (" + Qry + ")");

        if (Saldo <= -9999)
            Saldo = 0;
        return Saldo;

    }
    public static decimal ObtenSaldo(int PersonaID, int TipoIncidenciaRID)
    {
        string Qry = "SELECT ALMACEN_INC_SALDO FROM EC_ALMACEN_INC WHERE TIPO_INCIDENCIA_R_ID = " + TipoIncidenciaRID + " AND PERSONA_ID = " + PersonaID + " \n ORDER BY  ALMACEN_INC_ID DESC";
        decimal Saldo = CeC_BD.EjecutaEscalarDecimal(Qry);

        if (Saldo <= -9999)
            Saldo = 0;
        return Saldo;
    }

    public static decimal ObtenSaldoParche(int PersonaID, int TipoIncidenciaID)
    {
        string Qry = "SELECT	EC_V_ALMACEN_INC_UMOV.ALMACEN_INC_SALDO, EC_TIPO_INCIDENCIAS_R.TIPO_INCIDENCIA_ID, EC_V_ALMACEN_INC_UMOV.PERSONA_ID \n" +
" FROM	EC_V_ALMACEN_INC_UMOV INNER JOIN \n" +
" 	EC_TIPO_INCIDENCIAS_R ON EC_V_ALMACEN_INC_UMOV.TIPO_INCIDENCIA_R_ID = EC_TIPO_INCIDENCIAS_R.TIPO_INCIDENCIA_R_ID \n" +
" WHERE TIPO_INCIDENCIA_ID = " + TipoIncidenciaID + " AND PERSONA_ID = " + PersonaID + " " +
" \n ORDER BY EC_V_ALMACEN_INC_UMOV.ALMACEN_INC_ID DESC";
        decimal Saldo = CeC_BD.EjecutaEscalarDecimal(Qry);

        if (Saldo <= -9999)
            Saldo = 0;
        return Saldo;
    }

    public static DateTime ObtenFechaSiguiente(int PersonaID, int TipoIncidenciaRID)
    {
        //        string Qry = "SELECT ALMACEN_INC_SIGUIENTE FROM EC_ALMACEN_INC WHERE (TIPO_ALMACEN_INC_ID = -1) AND TIPO_INCIDENCIA_R_ID = " + TipoIncidenciaRID + " AND PERSONA_ID = " + PersonaID + " \n ORDER BY  ALMACEN_INC_ID DESC";
        string Qry = "SELECT ALMACEN_INC_SIGUIENTE FROM EC_ALMACEN_INC WHERE (TIPO_ALMACEN_INC_ID = -1 OR TIPO_ALMACEN_INC_ID = 0) AND TIPO_INCIDENCIA_R_ID = " + TipoIncidenciaRID + " AND PERSONA_ID = " + PersonaID + " \n ORDER BY  ALMACEN_INC_ID DESC";
        return CeC_BD.EjecutaEscalarDateTime(Qry, new DateTime(0));
    }
    public static int AgregaMovimiento(int PersonaDiarioID, int TipoIncidenciaRID, TipoAlmacenIncidencias Tipo, string Comentario, string Extras, CeC_Sesion Sesion, int SuscripcionID, decimal Movimiento = -1)
    {
        CeC_Asistencias PersonaDiario = new CeC_Asistencias(PersonaDiarioID, Sesion);
        if (PersonaDiario.PERSONA_ID <= 0
            || Tipo == TipoAlmacenIncidencias.Inicial || Tipo == TipoAlmacenIncidencias.Automatico)
            return -9999;

        return AgregaMovimiento(PersonaDiario.PERSONA_ID, TipoIncidenciaRID, Tipo, PersonaDiario.PERSONA_DIARIO_FECHA, Movimiento, CeC_BD.FechaNula, false, Comentario, Extras, Sesion.SESION_ID, SuscripcionID);
    }
    public static int AgregaMovimiento(int PersonaID, int TipoIncidenciaRID, TipoAlmacenIncidencias Tipo, DateTime Fecha, decimal Movimiento, DateTime Siguiente, bool Automatico, string Comentario, string Extras, int SesionID, int SuscripcionID)
    {
        decimal Saldo = ObtenSaldo(PersonaID, TipoIncidenciaRID) + Movimiento;
        int ALMACEN_INC_ID = CeC_Autonumerico.GeneraAutonumerico("EC_ALMACEN_INC", "ALMACEN_INC_ID", SesionID, SuscripcionID);
        string Qry = "INSERT INTO EC_ALMACEN_INC (" +
            "ALMACEN_INC_ID, TIPO_INCIDENCIA_R_ID, TIPO_ALMACEN_INC_ID, " +
            "PERSONA_ID, ALMACEN_INC_FECHA, ALMACEN_INC_NO, ALMACEN_INC_SALDO, " +
            "ALMACEN_INC_SIGUIENTE, ALMACEN_INC_AUTOM, ALMACEN_INC_COMEN, " +
            "ALMACEN_INC_EXTRAS) VALUES(" +
            ALMACEN_INC_ID + ", " + TipoIncidenciaRID + ", " + CeC.Convierte2Int(Tipo) + ", " +
            PersonaID + ", " + CeC_BD.SqlFecha(Fecha) + ", " + Movimiento + ", " + Saldo + ", " +
            CeC_BD.SqlFechaHora(Siguiente) + ", " + CeC.Convierte2Int(Automatico) + ", '" + CeC_BD.ObtenParametroCadena(Comentario) + "', '" +
            CeC_BD.ObtenParametroCadena(Extras) + "')";
        if (CeC_BD.EjecutaComando(Qry) > 0)
            return ALMACEN_INC_ID;
        return -9999;
    }
    public static int ObtenIncremento(int PersonaID, DateTime Fecha)
    {
        return CeC_BD.EjecutaEscalarInt("SELECT TIPO_INCIDENCIA_R_ID FROM EC_ALMACEN_INC WHERE TIPO_ALMACEN_INC_ID = " + CeC.Convierte2Int(TipoAlmacenIncidencias.Normal)
            + " AND ALMACEN_INC_FECHA = " + CeC_BD.SqlFecha(Fecha) + " AND PERSONA_ID = " + PersonaID);
    }
    public static int ObtenTipoIncidenciaRID(int PersonaID, DateTime Fecha)
    {
        return CeC_BD.EjecutaEscalarInt("SELECT ALMACEN_INC_NO FROM EC_ALMACEN_INC WHERE TIPO_ALMACEN_INC_ID = " + CeC.Convierte2Int(TipoAlmacenIncidencias.Normal)
            + " AND ALMACEN_INC_FECHA = " + CeC_BD.SqlFecha(Fecha) + " AND PERSONA_ID = " + PersonaID);
    }
    public static bool CambiaTipoAlmacenInc(int AlmacenInc_ID, TipoAlmacenIncidencias Tipo)
    {
        if (CeC_BD.EjecutaEscalarInt("UPDATE EC_ALMACEN_INC SET TIPO_ALMACEN_INC_ID = " + CeC.Convierte2Int(Tipo) + " WHERE ALMACEN_INC_ID = " + AlmacenInc_ID) > 0)
            return true;
        return false;
    }

    public static int CorrigeMovimientos(CeC_Sesion Sesion, int PersonaID, DateTime FechaInicio, DateTime FechaFin)
    {
        string PersonasDiarioIDs = eClockBase.CeC.PersonaID2PersonasDiarioIDs(PersonaID, FechaInicio, FechaFin);
        string[] sPersonasDiarioIDs = CeC.ObtenArregoSeparador(PersonasDiarioIDs, ",");
        int RCorrectos = 0;
        foreach (string sPersonaDiarioID in sPersonasDiarioIDs)
        {
            if (Sesion != null)
            {
                if (CorrigeMovimiento(Sesion, CeC.Convierte2Int(sPersonaDiarioID), Sesion.SUSCRIPCION_ID) > 0)
                    RCorrectos++;
            }
            else
                if (CorrigeMovimiento(Sesion, CeC.Convierte2Int(sPersonaDiarioID), 0) > 0)
                    RCorrectos++;
        }
        return RCorrectos;
    }

    public static int CorrigeMovimiento(CeC_Sesion Sesion, int PersonaDiarioID, int SuscripcionID)
    {
        CeC_Asistencias Asis = new CeC_Asistencias(PersonaDiarioID, Sesion);
        /*        int TipoIncidenciaID = Cec_Incidencias.ObtenTipoIncidenciaID_PersonaDiarioID(PersonaDiarioID);
                CeC_TiempoXTiempos TxT = new CeC_TiempoXTiempos(TipoIncidenciaID, null);
                if (TxT.AutonumericoNegativo)
                    TipoIncidenciaID = TxT.Tiempoxtiempo_Sumar;*/
        return CorrigeMovimiento(Asis.PERSONA_ID, CeC_IncidenciasRegla.Persona_Diario_ID2Tipo_Incidencia_R_ID_INV(PersonaDiarioID),
            Asis.PERSONA_DIARIO_FECHA, Sesion != null ? Sesion.SESION_ID : 0, SuscripcionID);
    }

    public static int CorrigeMovimiento(int PersonaID, int TipoIncidenciaRID, DateTime Fecha, int SesionID, int SuscripcionID)
    {
        CeC_IncidenciasInventario IncInv = new CeC_IncidenciasInventario(PersonaID, TipoIncidenciaRID, Fecha, TipoAlmacenIncidencias.Normal);
        return CorrigeMovimientoAlmacenIncID(IncInv.ALMACEN_INC_ID, SesionID, SuscripcionID);

    }

    public static string ObtenAlmacenIncIDs(string AlmacenIncExtras, int TipoIncidenciaRID, TipoAlmacenIncidencias TipoAlmacenInc)
    {
        string Qry = "SELECT ALMACEN_INC_ID FROM EC_ALMACEN_INC WHERE TIPO_ALMACEN_INC_ID = " + CeC.Convierte2Int(TipoAlmacenIncidencias.Normal)
            + " AND ALMACEN_INC_EXTRAS = '" + AlmacenIncExtras + "'";
        DataSet DS = (DataSet)CeC_BD.EjecutaDataSet(Qry);
        if (DS == null || DS.Tables.Count < 1 || DS.Tables[0].Rows.Count < 1)
            return "";
        string Ids = "";
        foreach (DataRow DR in DS.Tables[0].Rows)
        {
            Ids = CeC.AgregaSeparador(Ids, CeC.Convierte2String(DR["ALMACEN_INC_ID"]), ",");
        }
        return Ids;
    }
    public static int CorrigeMovimiento(string AlmacenIncExtras, int TipoIncidenciaRID, int SesionID, int SuscripcionID)
    {
        string Ids = ObtenAlmacenIncIDs(AlmacenIncExtras, TipoIncidenciaRID, TipoAlmacenIncidencias.Normal);
        CorrigeMovimientos(Ids, SesionID, SuscripcionID);
        return 0;
    }
    public static int CorrigeMovimientoAlmacenIncID(int Almacen_IncID, int SesionID, int SuscripcionID)
    {
        try
        {
            CeC_IncidenciasInventario IncInv = new CeC_IncidenciasInventario(Almacen_IncID);

            if (IncInv.ALMACEN_INC_ID > 0)
            {
                try
                {
                    CeC_Interprete Interprete = new CeC_Interprete(IncInv.ALMACEN_INC_EXTRAS.Replace("|", "&"));
                    if (Interprete.ObtenParametroPos("INCIDENCIA_ID") >= 0)
                    {
                        Cec_Incidencias.QuitaIncidencia(CeC.Convierte2Int(Interprete.ObtenValor("INCIDENCIA_ID")));

                    }
                    ///Si contiene justificacion de accesos, entonces los deberá borrar
                    if (Interprete.ObtenParametroPos("ACCESO_JUS_ID") >= 0)
                    {
                        CeC_Accesos_Jus.BorrarAccesosJust(Interprete.ObtenValor("ACCESO_JUS_ID"));
                    }

                }
                catch { }

                CambiaTipoAlmacenInc(IncInv.ALMACEN_INC_ID, TipoAlmacenIncidencias.Correccion);

                return AgregaMovimiento(IncInv.PERSONA_ID, IncInv.TIPO_INCIDENCIA_R_ID, TipoAlmacenIncidencias.Correccion, DateTime.Now, -IncInv.ALMACEN_INC_NO, CeC_BD.FechaNula, false, "Corregido el " + DateTime.Now.ToString(), "", SesionID, SuscripcionID);
            }
        }
        catch { }
        return 0;
    }
    public static int ObtenSiguienteCiclo(int PersonaID, int TipoIncidenciaRID)
    {
        string Qry = "SELECT ALMACEN_INC_EXTRAS FROM EC_ALMACEN_INC WHERE (TIPO_ALMACEN_INC_ID = -1 OR TIPO_ALMACEN_INC_ID = 0)  AND TIPO_INCIDENCIA_R_ID = " + TipoIncidenciaRID + " AND PERSONA_ID = " + PersonaID + " \n ORDER BY  ALMACEN_INC_ID DESC";
        int Ret = CeC_BD.EjecutaEscalarInt(Qry);

        if (Ret <= -9999)
            Ret = 0;
        return Ret;
    }
    public static decimal ObtenIncremento(int TipoIncidenciaRID, int CicloNo)
    {
        decimal Incremento = CeC_BD.EjecutaEscalarInt("SELECT INC_REGLA_CICLO_INCR FROM EC_INC_REGLA_CICLOS WHERE TIPO_INCIDENCIA_R_ID = " + TipoIncidenciaRID + " AND INC_REGLA_CICLO_NO = " + CicloNo);
        if (Incremento <= -9999)
            return 0;
        return Incremento;
    }

    public static decimal ObtenIncrementoPersonaID(int TipoIncidenciaRID, int PersonaID)
    {
        int Ciclo = ObtenSiguienteCiclo(PersonaID, TipoIncidenciaRID);
        return ObtenIncremento(TipoIncidenciaRID, Ciclo);
    }

    public static bool CreaSaldosPendientes(int PersonaID, CeC_IncidenciasRegla Regla)
    {
        try
        {
            if (PersonaID == 881)
                PersonaID = 881;
            DateTime UltimaFecha = ObtenFechaSiguiente(PersonaID, Regla.TIPO_INCIDENCIA_R_ID);
            DateTime SiguienteFecha;
            TipoAlmacenIncidencias Tipo = TipoAlmacenIncidencias.Inicial;
            decimal Movimiento = 0;
            int Ciclo = 0;
            string Ext = "";

            if (UltimaFecha == new DateTime(0))
            {
                if (Regla.TIPO_INCIDENCIA_R_FIQRY == "")
                    return false;
                DateTime FechaInicial = CeC_IncidenciasRegla.sObtenFechaInicia(Regla.TIPO_INCIDENCIA_R_FIQRY, PersonaID);
                if (FechaInicial == CeC_BD.FechaNula)
                    return false; ;
                UltimaFecha = FechaInicial;
            }
            else
            {
                if (UltimaFecha > DateTime.Now)
                    return false;
                Ciclo = ObtenSiguienteCiclo(PersonaID, Regla.TIPO_INCIDENCIA_R_ID);

                if (Regla.TIPO_INCIDENCIA_R_LIMPIAR)
                {

                    AgregaMovimiento(PersonaID, Regla.TIPO_INCIDENCIA_R_ID, TipoAlmacenIncidencias.Limpiado, DateTime.Now, -ObtenSaldo(PersonaID, Regla.TIPO_INCIDENCIA_R_ID),
                        CeC_BD.FechaNula, true, "", "", 0, 0);

                }
                Movimiento = ObtenIncremento(Regla.TIPO_INCIDENCIA_R_ID, Ciclo);

                Tipo = TipoAlmacenIncidencias.Automatico;
            }
            if (Tipo == TipoAlmacenIncidencias.Inicial)
            {
                Movimiento = ObtenIncremento(Regla.TIPO_INCIDENCIA_R_ID, Ciclo);
            }
            SiguienteFecha = Regla.TS.ObtenNuevaFecha(UltimaFecha, PersonaID);
            Ciclo += 1;
            Ext = Ciclo.ToString();
            AgregaMovimiento(PersonaID, Regla.TIPO_INCIDENCIA_R_ID, Tipo, DateTime.Now, Movimiento, SiguienteFecha, true, "", Ext, 0, 0);
            if (SiguienteFecha < DateTime.Now && UltimaFecha < SiguienteFecha)
                return CreaSaldosPendientes(PersonaID, Regla);
            return true;
        }
        catch { }
        return false;
    }
    public static bool sCargando = false;
    public static bool CreaSaldosPendientes()
    {
        if (sCargando)
            return false;
        sCargando = true;
        DataSet DS = CeC_IncidenciasRegla.ObtenEC_TIPO_INCIDENCIAS_R(true, true);
        if (DS == null || DS.Tables.Count < 1 || DS.Tables[0].Rows.Count < 1)
        {
            sCargando = false;
            return false;
        }
        foreach (DataRow DR in DS.Tables[0].Rows)
        {
            try
            {
                CeC_IncidenciasRegla Regla = new CeC_IncidenciasRegla();
                Regla.Carga(DR);
                if (!Regla.TIPO_INCIDENCIA_R_INV)
                {
                    sCargando = false;
                    return false;
                }
                DataSet DS_Personas = (DataSet)CeC_BD.EjecutaDataSet(Regla.TIPO_INCIDENCIA_R_PER);
                foreach (DataRow DRPersona in DS_Personas.Tables[0].Rows)
                {
                    int PersonaID = CeC.Convierte2Int(DRPersona["PERSONA_ID"]);
                    CreaSaldosPendientes(PersonaID, Regla);
                }
            }
            catch { }
        }
        sCargando = false;
        return true;
    }

    public static string ObtenSaldosPersonasLinkID(int TIPO_INCIDENCIA_R_ID, string PersonasIDS, decimal Descuento)
    {
        string Ret = "";
        try
        {

            string[] sPersonas = CeC.ObtenArregoSeparador(PersonasIDS, ",");
            foreach (string Persona in sPersonas)
            {
                int PersonaID = CeC.Convierte2Int(Persona);
                int PersonaLinkID = CeC_Empleados.ObtenPersona_Link_ID(PersonaID);
                decimal Saldo = ObtenSaldo(PersonaID, TIPO_INCIDENCIA_R_ID);
                decimal SaldoPosterior = Saldo - Descuento;
                Ret += "El saldo de " + PersonaLinkID.ToString() + " es " + Saldo.ToString("#,##0.00") + "-" + Descuento.ToString("#,##0.00") + "=" + SaldoPosterior.ToString("#,##0.00") + " <BR>";
            }
        }
        catch { }
        return Ret;
    }
    public static string CorrigeMovimientos(string AlmacenIncIDs, int SesionID, int SuscripcionID)
    {
        string Ret = "";
        int Correctos = 0;
        int Erroneos = 0;
        try
        {

            string[] sAlmacenIncIDs = CeC.ObtenArregoSeparador(AlmacenIncIDs, ",");
            foreach (string sAlmacenIncID in sAlmacenIncIDs)
            {
                int AlmacenIncID = CeC.Convierte2Int(sAlmacenIncID);
                if (CorrigeMovimientoAlmacenIncID(AlmacenIncID, SesionID, SuscripcionID) > 0)
                    Correctos++;
                else
                    Erroneos++;
            }
        }
        catch { }
        if (Correctos > 0)
            Ret += "Se corrigieron " + Correctos + " elementos";
        if (Erroneos > 0)
            Ret += ", no se pudieron corregir " + Erroneos + " elementos";

        return Ret;
    }

    public static DataSet ObtenSaldos(int PersonaID, string Agrupacion, string TiposIncidenciasIDS, string Campos, CeC_Sesion Sesion)
    {
        string Qry = "SELECT EC_V_ALMACEN_INC_UMOV.PERSONA_ID, EC_V_ALMACEN_INC_UMOV.ALMACEN_INC_FECHA,  \n" +
        " 	EC_V_ALMACEN_INC_UMOV.ALMACEN_INC_SALDO, EC_V_ALMACEN_INC_UMOV.ALMACEN_INC_SIGUIENTE,  \n" +
        " 	EC_TIPO_INCIDENCIAS.TIPO_INCIDENCIA_NOMBRE, EC_TIPO_INCIDENCIAS_R.TIPO_INCIDENCIA_R_DESC, \n" +
        "   EC_TIPO_INCIDENCIAS_R.TIPO_INCIDENCIA_ID, EC_TIPO_INCIDENCIAS_R.TIPO_INCIDENCIA_R_ID \n" +
        " FROM	EC_TIPO_INCIDENCIAS_R INNER JOIN \n" +
        " 	EC_TIPO_INCIDENCIAS ON EC_TIPO_INCIDENCIAS_R.TIPO_INCIDENCIA_ID = EC_TIPO_INCIDENCIAS.TIPO_INCIDENCIA_ID INNER JOIN \n" +
        " 	EC_V_ALMACEN_INC_UMOV ON  \n" +
        " 	EC_TIPO_INCIDENCIAS_R.TIPO_INCIDENCIA_R_ID = EC_V_ALMACEN_INC_UMOV.TIPO_INCIDENCIA_R_ID \n" +
        " WHERE " + CeC_Asistencias.ValidaAgrupacion(PersonaID, Sesion.USUARIO_ID, Agrupacion, true);
        if (TiposIncidenciasIDS != null && TiposIncidenciasIDS != "")
            Qry += " AND EC_TIPO_INCIDENCIAS_R.TIPO_INCIDENCIA_ID IN (" + TiposIncidenciasIDS + ") ";
        Qry += " \n ORDER BY EC_V_ALMACEN_INC_UMOV.PERSONA_ID, EC_TIPO_INCIDENCIAS.TIPO_INCIDENCIA_NOMBRE";
        return CeC_BD.EjecutaDataSet(Qry);
    }

    public static List<eClockBase.Modelos.Incidencias.Model_SaldosTiposIncidenciasR> ObtenSaldos(int PersonaID, string Agrupacion, string TiposIncidenciasIDS, CeC_Sesion Sesion)
    {

        DataSet DS = ObtenSaldos(PersonaID, Agrupacion, TiposIncidenciasIDS, "PERSONA_ID,ALMACEN_INC_FECHA,ALMACEN_INC_SALDO,ALMACEN_INC_SIGUIENTE,TIPO_INCIDENCIA_NOMBRE,TIPO_INCIDENCIA_R_DESC,TIPO_INCIDENCIA_ID,TIPO_INCIDENCIA_R_ID", Sesion);
        if (DS == null || DS.Tables.Count < 1)
            return null;
        return (List<eClockBase.Modelos.Incidencias.Model_SaldosTiposIncidenciasR>)CeC_BD.ConvertTo<eClockBase.Modelos.Incidencias.Model_SaldosTiposIncidenciasR>(DS.Tables[0]);
    }
}
