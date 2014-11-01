using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Data;
/// <summary>
/// Descripción breve de CeC_RecNominas
/// </summary>
public class CeC_RecNominas
{
    public CeC_RecNominas()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public static string ObtenDatosGuardados(int RecNominaID)
    {
        return CeC_BD.EjecutaEscalarString("SELECT REC_NOMINA_DATOS FROM EC_REC_NOMINAS WHERE REC_NOMINA_ID = " + RecNominaID);
    }
    public static bool GuardaDatos(int RecNominaID, string Datos)
    {
        if (CeC_BD.EjecutaComando("UPDATE EC_REC_NOMINAS SET REC_NOMINA_DATOS = '" + CeC_BD.ObtenParametroCadena(Datos) + "', REC_NOMINA_FCONSULTA = " + CeC_BD.QueryFechaHora + " WHERE REC_NOMINA_ID = " + RecNominaID) > 0)
            return true;
        return false;
    }

    public static bool ConfirmaReciboNominaImpreso(int RecNominaID, int RecNominaImp)
    {
        if (CeC_BD.EjecutaComando("UPDATE EC_REC_NOMINAS SET REC_NOMINA_IMPRESO = " + RecNominaImp + " WHERE REC_NOMINA_ID = " + RecNominaID + "") > 0)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public static eClockBase.Modelos.Nomina.Reporte_RecNomina ObtenRecibosNomina(eClockBase.Modelos.Nomina.Model_Parametros Param, string Lang, CeC_Sesion Sesion)
    {
        bool AutoGuardarRecibosNomina = Sesion.ConfiguraSuscripcion.AutoGuardarRecibosNomina;
        eClockBase.Modelos.Nomina.Reporte_RecNomina Ret = new eClockBase.Modelos.Nomina.Reporte_RecNomina();
        try
        {
            string QryRecNomina = "";
            string Filtro = "";
            if (Param != null)
            {
                if (Param.REC_NOMINA_ID > 0)
                {
                    Filtro = "REC_NOMINA_ID = " + Param.REC_NOMINA_ID;
                }
                else
                {
                    if (Param.TIPO_NOMINA_ID > 0)
                        Filtro = CeC.AgregaSeparador(Filtro, "TIPO_NOMINA_ID = " + Param.TIPO_NOMINA_ID, " AND ");
                    Filtro = CeC.AgregaSeparador(Filtro, "REC_NOMINA_ANO = " + Param.REC_NOMINA_ANO + "  AND REC_NOMINA_NO = " + Param.REC_NOMINA_NO + " " + CeC_Asistencias.ValidaAgrupacion(Param.PERSONA_ID, Sesion.USUARIO_ID, Param.AGRUPACION, false), " AND ");
                }
            }
            else
                Filtro = "";
            eClockBase.Modelos.Model_REC_NOMINAS ModeloRecNomina = new eClockBase.Modelos.Model_REC_NOMINAS();
            string Listado = CeC_Tabla.ObtenDatosLista("EC_REC_NOMINAS", "", ModeloRecNomina, Sesion, "", Filtro);
            List<eClockBase.Modelos.Nomina.Model_Rec_Nominas_Ampliados> RecNominas = JsonConvert.DeserializeObject<List<eClockBase.Modelos.Nomina.Model_Rec_Nominas_Ampliados>>(Listado);
            foreach (eClockBase.Modelos.Nomina.Model_Rec_Nominas_Ampliados RecNomina in RecNominas)
            {
                try
                {
                    if (RecNomina.REC_NOMINA_DATOS != null && RecNomina.REC_NOMINA_DATOS != "")
                    {
                        eClockBase.Modelos.Nomina.Model_RecNominaGuardado DatosGuardados = JsonConvert.DeserializeObject<eClockBase.Modelos.Nomina.Model_RecNominaGuardado>(RecNomina.REC_NOMINA_DATOS);
                        if (DatosGuardados.RecNomina != null)
                        {
                            Ret.Recibo_Nomina.Add(DatosGuardados.RecNomina);
                        }
                        if (DatosGuardados.Percepciones != null)
                        {
                            Ret.Percepciones.AddRange(DatosGuardados.Percepciones);
                        }
                        if (DatosGuardados.Deducciones != null)
                        {
                            Ret.Deducciones.AddRange(DatosGuardados.Deducciones);
                        }
                    }
                    else
                    {
                        System.Data.DataSet DS = CeC_CamposDatos.ObtenCampos(RecNomina.PERSONA_ID, "PERSONA_LINK_ID,PERSONA_NOMBRE,AGRUPACION_NOMBRE,AREA,TURNO,CURP,RFC,IMSS,COMPANIA,PUESTO", Sesion);
                        if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
                        {
                            try
                            {
                                eClockBase.Modelos.Nomina.Model_Rec_Nominas_Ampliados DatosAdd = null;
                                string Json = CeC_BD.DataSet2JsonV2(DS, true);
                                DatosAdd = JsonConvert.DeserializeObject<eClockBase.Modelos.Nomina.Model_Rec_Nominas_Ampliados>(Json);
                                RecNomina.PERSONA_LINK_ID = DatosAdd.PERSONA_LINK_ID;
                                RecNomina.PERSONA_NOMBRE = DatosAdd.PERSONA_NOMBRE;
                                RecNomina.AGRUPACION_NOMBRE = DatosAdd.AGRUPACION_NOMBRE;
                                RecNomina.AREA = DatosAdd.AREA;
                                RecNomina.TURNO = DatosAdd.TURNO;
                                RecNomina.CURP = DatosAdd.CURP;
                                RecNomina.RFC = DatosAdd.RFC;
                                RecNomina.IMSS = DatosAdd.IMSS;
                                RecNomina.COMPANIA = DatosAdd.COMPANIA;
                                RecNomina.PUESTO = DatosAdd.PUESTO;

                            }
                            catch { }
                        }
                        Ret.Recibo_Nomina.Add(RecNomina);
                        eClockBase.Modelos.Model_REC_NOMI_MOV ModeloRecNomiMov = new eClockBase.Modelos.Model_REC_NOMI_MOV();
                        List<eClockBase.Modelos.Model_REC_NOMI_MOV> LstDeducciones = null;
                        List<eClockBase.Modelos.Model_REC_NOMI_MOV> LstPercepciones = null;
                        Filtro = "REC_NOMINA_ID = " + RecNomina.REC_NOMINA_ID + " AND CLASIFIC_MOV_ID = 1";
                        try
                        {
                            Listado = CeC_Tabla.ObtenDatosLista("EC_REC_NOMI_MOV", "", ModeloRecNomiMov, Sesion, "", Filtro);
                            LstPercepciones = JsonConvert.DeserializeObject<List<eClockBase.Modelos.Model_REC_NOMI_MOV>>(Listado);
                            Ret.Percepciones.AddRange(LstPercepciones);
                        }
                        catch { }
                        try
                        {
                            Filtro = "REC_NOMINA_ID = " + RecNomina.REC_NOMINA_ID + " AND CLASIFIC_MOV_ID = 2";
                            Listado = CeC_Tabla.ObtenDatosLista("EC_REC_NOMI_MOV", "", ModeloRecNomiMov, Sesion, "", Filtro);
                            LstDeducciones = JsonConvert.DeserializeObject<List<eClockBase.Modelos.Model_REC_NOMI_MOV>>(Listado);
                            Ret.Deducciones.AddRange(LstDeducciones);
                        }
                        catch { }
                        if (AutoGuardarRecibosNomina)
                        {
                            eClockBase.Modelos.Nomina.Model_RecNominaGuardado DatosGuardados = new eClockBase.Modelos.Nomina.Model_RecNominaGuardado();
                            DatosGuardados.RecNomina = RecNomina;
                            DatosGuardados.Deducciones = LstDeducciones;
                            DatosGuardados.Percepciones = LstPercepciones;
                            GuardaDatos(RecNomina.REC_NOMINA_ID, JsonConvert.SerializeObject(DatosGuardados));
                        }
                    }
                }
                catch (Exception ex)
                {
                    CIsLog2.AgregaError(ex);
                }
            }
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return Ret;
    }


    public static int BorrarRecNominas(int TipoNominaID, int Ano, int PeriodoNo, CeC_Sesion Sesion)
    {

        string Qry = "DELETE EC_REC_NOMI_MOV WHERE REC_NOMINA_ID IN (SELECT REC_NOMINA_ID FROM EC_REC_NOMINAS WHERE TIPO_NOMINA_ID = " + TipoNominaID + " AND REC_NOMINA_ANO = " + Ano + " AND REC_NOMINA_NO = " + PeriodoNo + ")";
        CeC_BD.EjecutaComando(Qry);
        Qry = "DELETE EC_REC_NOMINAS WHERE TIPO_NOMINA_ID = " + TipoNominaID + " AND REC_NOMINA_ANO = " + Ano + " AND REC_NOMINA_NO = " + PeriodoNo;
        return CeC_BD.EjecutaComando(Qry);
    }

    public static eClockBase.Modelos.Importar.Model_Resultado Importar(eClockBase.Modelos.Nomina.Model_RecNominasImportar ImpDatos, CeC_Sesion Sesion, int SuscripcionID)
    {
        eClockBase.Modelos.Importar.Model_Resultado Res = new eClockBase.Modelos.Importar.Model_Resultado();
        Res.CorrectosPoss = "";
        Res.ErroneosPoss = "";
        if (ImpDatos == null || ImpDatos.RecibosNominas == null || ImpDatos.RecibosNominas.Count < 1)
        {
            Res.Error = "Sin Datos";
            return Res;
        }
        int TipoNominaID = CeC_TiposNomina.ObtenTipoNominaIDDeTipoNominaIDEx(ImpDatos.NominaExID, SuscripcionID);
        if (TipoNominaID <= 0)
        {
            Res.Error = "Tipo de nómina no existe";
            return Res;
        }
        BorrarRecNominas(TipoNominaID, ImpDatos.Ano, ImpDatos.PeriodoNo, Sesion);
        int PosRecNomina = -1;
        foreach (eClockBase.Modelos.Nomina.Model_RecNominas RecNomina in ImpDatos.RecibosNominas)
        {
            PosRecNomina++;
            eClockBase.Modelos.Model_REC_NOMINAS NuevoRecNomina = new eClockBase.Modelos.Model_REC_NOMINAS();
            NuevoRecNomina.TIPO_NOMINA_ID = TipoNominaID;
            NuevoRecNomina.REC_NOMINA_ANO = ImpDatos.Ano;
            NuevoRecNomina.REC_NOMINA_NO = ImpDatos.PeriodoNo;
            NuevoRecNomina.PERSONA_ID = CeC_Personas.ObtenPersonaIDBySuscripcion(RecNomina.PERSONA_LINK_ID, Sesion.SUSCRIPCION_ID);
            NuevoRecNomina.REC_NOMINA_FINICIO = ImpDatos.FechaInicio;
            NuevoRecNomina.REC_NOMINA_FFIN = ImpDatos.FechaFin;
            NuevoRecNomina.REC_NOMINA_COMENTARIOS = RecNomina.REC_NOMINA_COMENTARIOS;
            NuevoRecNomina.REC_NOMINA_DESCTRAB = RecNomina.REC_NOMINA_DESCTRAB;
            NuevoRecNomina.REC_NOMINA_DIASPAG = RecNomina.REC_NOMINA_DIASPAG;
            NuevoRecNomina.REC_NOMINA_FECHA_GEN = ImpDatos.Generación;
            NuevoRecNomina.REC_NOMINA_N_PAGAR = RecNomina.REC_NOMINA_N_PAGAR;
            NuevoRecNomina.REC_NOMINA_VALES = RecNomina.REC_NOMINA_VALES;
            NuevoRecNomina.REC_NOMINA_HEXTRAS = RecNomina.REC_NOMINA_HEXTRAS;
            NuevoRecNomina.REC_NOMINA_ID = CeC_Tabla.GuardaDatos("EC_REC_NOMINAS", "REC_NOMINA_ID", NuevoRecNomina, true, Sesion, SuscripcionID);
            if (NuevoRecNomina.REC_NOMINA_ID > 0)
            {
                foreach (eClockBase.Modelos.Nomina.Model_RecNomiMov RecNominaMov in RecNomina.Movimientos)
                {
                    eClockBase.Modelos.Model_REC_NOMI_MOV NuevoRecNominaMov = new eClockBase.Modelos.Model_REC_NOMI_MOV();
                    NuevoRecNominaMov.CLASIFIC_MOV_ID = RecNominaMov.CLASIFIC_MOV_ID;
                    NuevoRecNominaMov.REC_NOMI_MOV_CVE = RecNominaMov.REC_NOMI_MOV_CVE;
                    foreach (eClockBase.Modelos.Nomina.Model_Conceptos Concepto in ImpDatos.Conceptos)
                    {
                        if (NuevoRecNominaMov.REC_NOMI_MOV_CVE == Concepto.REC_NOMI_MOV_CVE)
                            NuevoRecNominaMov.REC_NOMI_MOV_CONCEPTO = Concepto.REC_NOMI_MOV_CONCEPTO;
                    }
                    NuevoRecNominaMov.REC_NOMI_MOV_SALDO = RecNominaMov.REC_NOMI_MOV_SALDO;
                    NuevoRecNominaMov.REC_NOMI_MOV_UNIDAD = RecNominaMov.REC_NOMI_MOV_UNIDAD;
                    NuevoRecNominaMov.REC_NOMI_MOV_VALOR = RecNominaMov.REC_NOMI_MOV_VALOR;
                    NuevoRecNominaMov.REC_NOMI_MOV_IMPORTE = RecNominaMov.REC_NOMI_MOV_IMPORTE;
                    NuevoRecNominaMov.REC_NOMINA_ID = NuevoRecNomina.REC_NOMINA_ID;
                    NuevoRecNominaMov.REC_NOMI_MOV_ID = CeC_Tabla.GuardaDatos("EC_REC_NOMI_MOV", "REC_NOMI_MOV_ID", NuevoRecNominaMov, true, Sesion, SuscripcionID);
                }
                Res.CorrectosPoss = CeC.AgregaSeparador(Res.CorrectosPoss, PosRecNomina.ToString(), ",");
            }
            else
            {
                Res.ErroneosPoss = CeC.AgregaSeparador(Res.ErroneosPoss, PosRecNomina.ToString(), ",");
            }
        }
        return Res;
    }

    public static DataSet ObtenNoConsultas(CeC_Sesion Sesion)
    {
        return CeC_RecNominas.ObtenNoConsultas(-1, "", -1, new DateTime(), new DateTime(), Sesion);
    }

    public static DataSet ObtenNoConsultas(int PersonaID, string Agrupacion, int TipoNominaID, DateTime FechaDesde, DateTime FechaHasta, CeC_Sesion Sesion)
    {
        string Filtro = "";
        if (TipoNominaID > 0)
            Filtro += " AND EC_TIPO_NOMINA.TIPO_NOMINA_ID = " + TipoNominaID;
        if (FechaHasta >= FechaDesde && FechaDesde != new DateTime())
            Filtro += " AND REC_NOMINA_FCONSULTA >= " + CeC_BD.SqlFecha(FechaDesde) + " AND REC_NOMINA_FCONSULTA <= " + CeC_BD.SqlFecha(FechaHasta.AddDays(1));

        string Qry = "SELECT	EC_REC_NOMINAS.REC_NOMINA_ID, EC_TIPO_NOMINA.TIPO_NOMINA_NOMBRE, EC_PERSONAS.AGRUPACION_NOMBRE,  \n" +
                " 	EC_PERSONAS.PERSONA_LINK_ID, EC_PERSONAS.PERSONA_NOMBRE, EC_REC_NOMINAS.REC_NOMINA_FCONSULTA,  \n" +
                " 	EC_REC_NOMINAS.REC_NOMINA_CONSULTADO \n" +
                " FROM	EC_TIPO_NOMINA INNER JOIN \n" +
                " 	EC_REC_NOMINAS ON EC_TIPO_NOMINA.TIPO_NOMINA_ID = EC_REC_NOMINAS.TIPO_NOMINA_ID INNER JOIN \n" +
                " 	EC_PERSONAS ON EC_REC_NOMINAS.PERSONA_ID = EC_PERSONAS.PERSONA_ID \n" +
                " WHERE EC_PERSONAS." + CeC_Asistencias.ValidaAgrupacion(PersonaID, Sesion.USUARIO_ID, Agrupacion, true) + Filtro;
        return CeC_BD.EjecutaDataSet(Qry);                
    }

    public static DataSet ObtenSinConsultas(int PersonaID, string Agrupacion, int TipoNominaID, CeC_Sesion Sesion)
    {
        string Filtro = " AND EC_REC_NOMINAS.REC_NOMINA_CONSULTADO <= 0";
        if (TipoNominaID > 0)
            Filtro += " AND EC_TIPO_NOMINA.TIPO_NOMINA_ID = " + TipoNominaID;

        string Qry = "SELECT	EC_REC_NOMINAS.REC_NOMINA_ID, EC_TIPO_NOMINA.TIPO_NOMINA_NOMBRE, EC_PERSONAS.AGRUPACION_NOMBRE,  \n" +
                " 	EC_PERSONAS.PERSONA_LINK_ID, EC_PERSONAS.PERSONA_NOMBRE \n" +
                " FROM	EC_TIPO_NOMINA INNER JOIN \n" +
                " 	EC_REC_NOMINAS ON EC_TIPO_NOMINA.TIPO_NOMINA_ID = EC_REC_NOMINAS.TIPO_NOMINA_ID INNER JOIN \n" +
                " 	EC_PERSONAS ON EC_REC_NOMINAS.PERSONA_ID = EC_PERSONAS.PERSONA_ID \n" +
                " WHERE EC_PERSONAS." + CeC_Asistencias.ValidaAgrupacion(PersonaID, Sesion.USUARIO_ID, Agrupacion, true) + Filtro;
        return CeC_BD.EjecutaDataSet(Qry);
    }

    public static DataSet ObtenSinConsultas( CeC_Sesion Sesion)
    {
        return CeC_RecNominas.ObtenSinConsultas(-1, "", -1, Sesion);
    }
}