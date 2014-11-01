using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
namespace eClock5.Clases.Interfaces
{
    class OracleBS : Interfaz
    {
        public class BalanceType
        {
            public int BALANCE_TYPE_ID { get; set; }
            public string BALANCE_NAME { get; set; }
            public string ATTRIBUTE1 { get; set; }
            public int BASE_BALANCE_TYPE_ID { get; set; }
            public bool Agregado { get; set; }
        }

        public List<BalanceType> ObtenBalanceTypes()
        {
            CeC_BD BD = new CeC_BD(this.Parametros.CadenaConexion);
            System.Data.DataSet DS = BD.lEjecutaDataSet("select BALANCE_TYPE_ID,BALANCE_NAME,ATTRIBUTE1,BASE_BALANCE_TYPE_ID from pay_balance_types");
            if (DS == null || DS.Tables.Count < 1 || DS.Tables[0].Rows.Count < 1)
                return null;
            return (List<BalanceType>)CeC_BD.ConvertTo<BalanceType>(DS.Tables[0]);

            string Datos = CeC_BD.DataSet2JsonList(DS);
            if (Datos == null || Datos == "")
                return null;
            return eClockBase.Controladores.CeC_ZLib.Json2Object<List<BalanceType>>(Datos);
        }

        public class BalanceData
        {
            public string EMPLOYEE_NUMBER { get; set; }
            public int BALANCE_TYPE_ID { get; set; }
            public decimal BALANCE_VALUE { get; set; }
            public string BALANCE_STATUS { get; set; }
            public string BALANCE_NAME { get; set; }
            public string ATTRIBUTE1 { get; set; }
            public int BASE_BALANCE_TYPE_ID { get; set; }
        }
        public List<BalanceData> ObtenRecMovNominas(int PAYROLL_ID, int Year, int PERIOD_NUM)
        {
            string Qry = "" +
            "SELECT  EMPLOYEE_NUMBER          \n" +
"         ,PBT.BALANCE_TYPE_ID \n" +
"         , CASE \n" +
"              WHEN (PBV.RUN_BALANCE_STATUS = 'V' \n" +
"                    AND PBV.BALANCE_LOAD_DATE IS NULL) THEN \n" +
"                 PRB.BALANCE_VALUE \n" +
"              WHEN PBV.RUN_BALANCE_STATUS = 'I' THEN \n" +
"                 NULL \n" +
"              WHEN PBV.RUN_BALANCE_STATUS IS NULL THEN \n" +
"                 NULL \n" +
"              WHEN (    PBV.RUN_BALANCE_STATUS = 'V' \n" +
"                    AND PBV.BALANCE_LOAD_DATE IS NOT NULL \n" +
"                    AND PPA.EFFECTIVE_DATE < PBV.BALANCE_LOAD_DATE) THEN \n" +
"                 NULL \n" +
"              ELSE \n" +
"                 PRB.BALANCE_VALUE \n" +
"           END \n" +
"              BALANCE_VALUE \n" +
"         , DECODE (PBV.RUN_BALANCE_STATUS \n" +
"                 , NULL \n" +
"                 , 'INVALID' \n" +
"                 , 'I' \n" +
"                 , 'INVALID' \n" +
"                 , 'V' \n" +
"                 , 'VALID' \n" +
"                  ) \n" +
"              BALANCE_STATUS \n" +
",PBT.BALANCE_NAME,PBT.ATTRIBUTE1,PBT.BASE_BALANCE_TYPE_ID \n" +
"          \n" +
"    FROM   PAY_BALANCE_TYPES PBT \n" +
"          \n" +
"         , PAY_DEFINED_BALANCES PDB \n" +
"         , PAY_BALANCE_DIMENSIONS PBD \n" +
"          \n" +
"         , PAY_PAYROLL_ACTIONS PPA \n" +
"         , PAY_ASSIGNMENT_ACTIONS PAA \n" +
"         , PAY_RUN_BALANCES PRB \n" +
"         , PAY_BALANCE_VALIDATION PBV \n" +
"         , PER_PEOPLE_F PPF \n" +
"         , PER_ASSIGNMENTS_F PAF" +
"   WHERE       \n" +
"            PBT.BALANCE_TYPE_ID = PDB.BALANCE_TYPE_ID \n" +
"           AND PDB.BALANCE_DIMENSION_ID = PBD.BALANCE_DIMENSION_ID \n" +
"           AND PBD.DIMENSION_LEVEL = 'ASG' \n" +
"           AND PDB.SAVE_RUN_BALANCE = 'Y' \n" +
"           AND PPA.ACTION_TYPE IN ('R', 'Q', 'B', 'I') \n" +
"           AND PAA.PAYROLL_ACTION_ID = PPA.PAYROLL_ACTION_ID \n" +
"           AND PAA.ACTION_STATUS = 'C' \n" +
"           AND PRB.ASSIGNMENT_ACTION_ID = PAA.ASSIGNMENT_ACTION_ID \n" +
"           AND PRB.ASSIGNMENT_ID = PAA.ASSIGNMENT_ID \n" +
"           AND PRB.DEFINED_BALANCE_ID = PDB.DEFINED_BALANCE_ID \n" +
"           AND PBV.DEFINED_BALANCE_ID(+) = PDB.DEFINED_BALANCE_ID \n" +
"           AND NVL (PBV.BUSINESS_GROUP_ID, PPA.BUSINESS_GROUP_ID) = PPA.BUSINESS_GROUP_ID \n" +
"           AND PPF.PERSON_ID = PAF.PERSON_ID \n" +
"           AND PAA.ASSIGNMENT_ID = PAF.ASSIGNMENT_ID \n" +
"           AND PPA.EFFECTIVE_DATE BETWEEN PPF.EFFECTIVE_START_DATE AND PPF.EFFECTIVE_END_DATE \n" +
"           AND PPA.EFFECTIVE_DATE BETWEEN PAF.EFFECTIVE_START_DATE AND PAF.EFFECTIVE_END_DATE \n" +
"           AND DATABASE_ITEM_SUFFIX = '_ASG_GRE_RUN' \n" +
"           AND PPA.TIME_PERIOD_ID IN (SELECT TIME_PERIOD_ID FROM PER_TIME_PERIODS WHERE PAYROLL_ID = " + PAYROLL_ID + "  AND PERIOD_NUM = " + PERIOD_NUM + " AND to_char(start_date, 'YYYY') = '" + Year + "') \n" +
"           AND (PBT.BALANCE_TYPE_ID IN (" + BALANCE_TYPE_ID + ") OR PBT.ATTRIBUTE1 IN('Percepciones','Deducciones','Tiempo') )\n" +
"  ORDER BY EMPLOYEE_NUMBER  \n";


            CeC_BD BD = new CeC_BD(this.Parametros.CadenaConexion);
            System.Data.DataSet DS = BD.lEjecutaDataSet(Qry);
            if (DS == null || DS.Tables.Count < 1 || DS.Tables[0].Rows.Count < 1)
                return null;
            return (List<BalanceData>)CeC_BD.ConvertTo<BalanceData>(DS.Tables[0]);
            /* string Datos = CeC_BD.DataSet2JsonList(DS);
             if (Datos == null || Datos == "")
                 return null;
             return eClockBase.Controladores.CeC_ZLib.Json2Object<List<BalanceData>>(Datos);*/
        }


        public BalanceType ObtenBalanceType(List<BalanceType> BalanceTypes, int BALANCE_TYPE_ID)
        {
            foreach (BalanceType BalanceType in BalanceTypes)
            {
                if (BalanceType.BALANCE_TYPE_ID == BALANCE_TYPE_ID)
                    return BalanceType;
            }
            return null;
        }

        public BalanceType ObtenBalanceType(List<BalanceType> BalanceTypes, string MovCve)
        {
            foreach (BalanceType BalanceType in BalanceTypes)
            {
                if (BalanceType.ATTRIBUTE1 != "Percepciones" && BalanceType.ATTRIBUTE1 != "Deducciones")
                    continue;
                if (ObtenMovCve(BalanceType.BALANCE_NAME) == MovCve)
                    return BalanceType;
            }
            return null;
        }

        public string ObtenMovCve(string Nombre)
        {
            if (Nombre.Length < 5)
                return Nombre;
            return Nombre.Substring(0, 5);
        }

        public decimal ObtenValor(string EMPLOYEE_NUMBER, List<BalanceData> BalanceDataMovs, int BALANCE_TYPE_ID)
        {
            foreach (BalanceData BData in BalanceDataMovs)
            {
                if (BData.EMPLOYEE_NUMBER == EMPLOYEE_NUMBER && BData.BALANCE_TYPE_ID == BALANCE_TYPE_ID)
                    return BData.BALANCE_VALUE;
            }
            return 0;

        }
        /// <summary>
        /// 44063 I0983_NETO
        /// 17061 I0810_VALES DESPENSA
        /// 24069 P0120_TIEMPO_EXTRA_DOBLE Hours
        /// 1062 P0001_SUELDO Days
        /// 2062 P0002_SUELDO Days
        /// 45062 P0140_DESCANSO_TRABAJADO Days
        /// </summary>
        string BALANCE_TYPE_ID = "44063,17061,24069,1062,45062,2062";
        eClockBase.Modelos.Nomina.Model_RecNominasImportar RecNominasImp;

        bool LlenaFechas(int PAYROLL_ID, int Year, int PERIOD_NUM)
        {
            string Qry = "SELECT START_DATE AS FechaInicio, END_DATE AS FechaFin FROM PER_TIME_PERIODS WHERE PAYROLL_ID = " + PAYROLL_ID + "  AND PERIOD_NUM = " + PERIOD_NUM + " AND to_char(start_date, 'YYYY') = '" + Year + "'";
            RecNominasImp.Generación = DateTime.Now;
            CeC_BD BD = new CeC_BD(this.Parametros.CadenaConexion);
            System.Data.DataSet DS = BD.lEjecutaDataSet(Qry);
            if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                System.Data.DataRow DR = DS.Tables[0].Rows[0];
                RecNominasImp.FechaInicio = eClockBase.CeC.Convierte2DateTime(DR["FechaInicio"]);
                RecNominasImp.FechaFin = eClockBase.CeC.Convierte2DateTime(DR["FechaFin"]);

                return true;
            }
            return false;
        }
        bool AgregaConcepto(string REC_NOMI_MOV_CVE, List<BalanceType> BalanceTypes)
        {
            foreach (eClockBase.Modelos.Nomina.Model_Conceptos Concepto in RecNominasImp.Conceptos)
            {
                if (Concepto.REC_NOMI_MOV_CVE == REC_NOMI_MOV_CVE)
                    return true;
            }
            var results = (from c in BalanceTypes
                           where ObtenMovCve(c.BALANCE_NAME) == REC_NOMI_MOV_CVE
                           select c).OrderBy(c => c.BALANCE_NAME);

            foreach (var result in results)
            {
                if (result.BALANCE_NAME.Length > 6)
                {
                    RecNominasImp.Conceptos.Add(new eClockBase.Modelos.Nomina.Model_Conceptos(REC_NOMI_MOV_CVE, result.BALANCE_NAME.Substring(6)));
                    return true;
                }
            }
            if (results.Count() > 0)
            {
                RecNominasImp.Conceptos.Add(new eClockBase.Modelos.Nomina.Model_Conceptos(REC_NOMI_MOV_CVE, results.First().BALANCE_NAME));
                return true;
            }
            return false;
        }
        protected bool Agregar(List<BalanceType> BalanceTypes, string REC_NOMI_MOV_CVE)
        {
            BalanceType BType = ObtenBalanceType(BalanceTypes, REC_NOMI_MOV_CVE);
            if (!BType.Agregado)
            {
                RecNominasImp.Conceptos.Add(new eClockBase.Modelos.Nomina.Model_Conceptos(REC_NOMI_MOV_CVE, BType.BALANCE_NAME.Substring(6)));
                return BType.Agregado = true;
            }
            return false;

        }
        protected override eClockBase.Modelos.Nomina.Model_RecNominasImportar ObtenRecibosNominaR(string NominaExID, int Ano, int PeriodoNO)
        {
            RecNominasImp = new eClockBase.Modelos.Nomina.Model_RecNominasImportar();
            List<BalanceType> BalanceTypes = ObtenBalanceTypes();
            int NominaID = eClockBase.CeC.Convierte2Int(NominaExID);
            List<BalanceData> BalanceDataMovs = ObtenRecMovNominas(NominaID, Ano, PeriodoNO);
            RecNominasImp.Ano = Ano;
            RecNominasImp.NominaExID = NominaExID;
            RecNominasImp.PeriodoNo = PeriodoNO;

            LlenaFechas(NominaID, Ano, PeriodoNO);
            eClockBase.Modelos.Nomina.Model_RecNominas ReciboNomina = new eClockBase.Modelos.Nomina.Model_RecNominas();
            if (BalanceTypes == null)
            {
                eClockBase.CeC_Log.AgregaError("No se obtuvieron tipos de balance desde Oracle");
                return null;
            }

            if (BalanceDataMovs == null)
            {
                eClockBase.CeC_Log.AgregaError("No se obtuvieron movimientos desde Oracle");
                return null;
            }
            foreach (BalanceData BData in BalanceDataMovs)
            {

                BalanceType BTypePadre = null;
                string REC_NOMI_MOV_CVE = ObtenMovCve(BData.BALANCE_NAME);
                int CLASIFIC_MOV_ID = 0;
                if (BData.ATTRIBUTE1 == "Percepciones")
                    CLASIFIC_MOV_ID = 1;
                if (BData.ATTRIBUTE1 == "Deducciones")
                    CLASIFIC_MOV_ID = 2;
                if (CLASIFIC_MOV_ID == 0)
                {
                    BTypePadre = ObtenBalanceType(BalanceTypes, REC_NOMI_MOV_CVE);
                    if (BTypePadre != null)
                    {
                        if (BTypePadre.ATTRIBUTE1 == "Percepciones")
                            CLASIFIC_MOV_ID = 1;
                        if (BTypePadre.ATTRIBUTE1 == "Deducciones")
                            CLASIFIC_MOV_ID = 2;
                    }
                }


                if (CLASIFIC_MOV_ID == 0)
                    continue;
                if (CLASIFIC_MOV_ID == 1 && REC_NOMI_MOV_CVE[0] != 'P')
                    continue;
                if (CLASIFIC_MOV_ID == 2 && REC_NOMI_MOV_CVE[0] != 'D')
                    continue;

                int PersonaLinkID = eClockBase.CeC.Convierte2Int(BData.EMPLOYEE_NUMBER);
                if (PersonaLinkID != ReciboNomina.PERSONA_LINK_ID)
                {
                    ReciboNomina = new eClockBase.Modelos.Nomina.Model_RecNominas();
                    ReciboNomina.PERSONA_LINK_ID = PersonaLinkID;
                    ReciboNomina.REC_NOMINA_VALES = ObtenValor(BData.EMPLOYEE_NUMBER, BalanceDataMovs, 17061);
                    ReciboNomina.REC_NOMINA_HEXTRAS = ObtenValor(BData.EMPLOYEE_NUMBER, BalanceDataMovs, 17061);
                    ReciboNomina.REC_NOMINA_N_PAGAR = ObtenValor(BData.EMPLOYEE_NUMBER, BalanceDataMovs, 44063);
                    ReciboNomina.REC_NOMINA_DIASPAG = ObtenValor(BData.EMPLOYEE_NUMBER, BalanceDataMovs, 1062);
                    if (ReciboNomina.REC_NOMINA_DIASPAG <= 0)
                        ReciboNomina.REC_NOMINA_DIASPAG = ObtenValor(BData.EMPLOYEE_NUMBER, BalanceDataMovs, 2062);
                    ReciboNomina.REC_NOMINA_DESCTRAB = ObtenValor(BData.EMPLOYEE_NUMBER, BalanceDataMovs, 45062);
                    RecNominasImp.RecibosNominas.Add(ReciboNomina);
                }
                eClockBase.Modelos.Nomina.Model_RecNomiMov Movimiento = null;
                foreach (eClockBase.Modelos.Nomina.Model_RecNomiMov sMovimiento in ReciboNomina.Movimientos)
                {
                    if (sMovimiento.REC_NOMI_MOV_CVE == REC_NOMI_MOV_CVE)
                    {
                        Movimiento = sMovimiento;
                        break;
                    }
                }
                if (Movimiento == null)
                {
                    Movimiento = new eClockBase.Modelos.Nomina.Model_RecNomiMov();
                    ReciboNomina.Movimientos.Add(Movimiento);
                    Movimiento.REC_NOMI_MOV_CVE = REC_NOMI_MOV_CVE;
                    Movimiento.CLASIFIC_MOV_ID = CLASIFIC_MOV_ID;
                    Agregar(BalanceTypes, REC_NOMI_MOV_CVE);
                }
                if (BData.ATTRIBUTE1 == "Percepciones" || BData.ATTRIBUTE1 == "Deducciones")
                    Movimiento.REC_NOMI_MOV_IMPORTE = BData.BALANCE_VALUE;
                if (BData.ATTRIBUTE1 == "Tiempo" || BData.ATTRIBUTE1 == "Tiempo")
                    Movimiento.REC_NOMI_MOV_UNIDAD = BData.BALANCE_VALUE;
            }

            return RecNominasImp;
        }


    }
}
