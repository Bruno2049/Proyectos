using System;
using System.Data;
using System.Data.SqlClient;
using PAEEEM.AccesoDatos.Operacion_Datos;
using PAEEEM.Entidades;
using PAEEEM.Entidades.Alta_Solicitud;

namespace PAEEEM.AccesoDatos.SolicitudCredito
{
    public class ResponseData
    {
        private PAEEEM_DESAEntidades contextModel;

        public string insertaResponseData(CompResponseData responseData)
        {
            string mensaje = "";
            contextModel = new PAEEEM_DESAEntidades();
            SqlConnection sqlConn = new SqlConnection(contextModel.Database.Connection.ConnectionString);
            


            try
            {
                

                using (var cmd = new SqlCommand
                    {
                        CommandType = CommandType.StoredProcedure,
                        CommandText = "INSERTA_RESPONSE_DATA",
                        Connection = sqlConn
                    })
                {
                    //ESTA CONTRUCCION SE REALIZO DE LA SIGUIENTE MANER A DEBIDO A QUE LA TABLA HA IDO CRECIENDO EN SUS COLUMNAS
                    ConfiguracionComando.AgregaParametro(cmd, "@CN", SqlDbType.NVarChar, responseData.Cn,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@ServiceCode", SqlDbType.NVarChar,responseData.ServiceCode, ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Rate", SqlDbType.NVarChar, responseData.Rate,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Periodof", SqlDbType.NVarChar, responseData.Periodof,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Name", SqlDbType.NVarChar, responseData.Name,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Address", SqlDbType.NVarChar, responseData.Address,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Dircomp1", SqlDbType.NVarChar, responseData.Dircomp1,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Dircomp2", SqlDbType.NVarChar, responseData.Dircomp2,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Colonia", SqlDbType.NVarChar, responseData.Colonia,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Poblacion", SqlDbType.NVarChar, responseData.Poblacion,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@StateName", SqlDbType.NVarChar, responseData.StateName,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Threads", SqlDbType.NVarChar, responseData.Threads,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Tel", SqlDbType.NVarChar, responseData.Tel,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Email", SqlDbType.NVarChar, responseData.Email,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Meter", SqlDbType.NVarChar, responseData.Meter,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@DueDate", SqlDbType.NVarChar, responseData.DueDate,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@PeriodStartDate", SqlDbType.NVarChar,responseData.PeriodStartDate, ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@PeriodEndDate", SqlDbType.NVarChar,responseData.PeriodEndDate, ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@HighConsumption", SqlDbType.NVarChar,responseData.HighConsumption, ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@VAT", SqlDbType.NVarChar, responseData.Vat,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Zone", SqlDbType.NVarChar, responseData.Zone,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Billing", SqlDbType.NVarChar, responseData.Biling,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@RGN_CFE", SqlDbType.NVarChar, responseData.RgnCfe,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@MinConsumptionDate", SqlDbType.NVarChar,responseData.MinConsumptionDate, ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@ANEXO_1", SqlDbType.NVarChar, responseData.Anexo1,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@HighConsumptionSummer", SqlDbType.NVarChar,responseData.HighConsumptionSummer, ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@HighConsumptionWinter", SqlDbType.NVarChar,responseData.HighConsumptionWinter, ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@AvgConsumptionSummer", SqlDbType.NVarChar,responseData.AvgConsumptionSummer, ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@AvgConsumptionWinter", SqlDbType.NVarChar,responseData.AvgConsumptionWinter, ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@HighAnnualConsumption", SqlDbType.NVarChar,responseData.HighAnnualConsumption, ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@HighAvgConsumption", SqlDbType.NVarChar,responseData.HighAvgConsumption, ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@TotalPeriods", SqlDbType.NVarChar,responseData.TotalPeriods, ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@WinterConsumptionHistory", SqlDbType.NVarChar,responseData.WinterConsumptionHistory, ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@SummerConsumptionHistory", SqlDbType.NVarChar,responseData.SummerConsumptionHistory, ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@PaymentHistory", SqlDbType.NVarChar,responseData.PaymentHistory, ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@UserStatus", SqlDbType.NVarChar, responseData.UserStatus,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Source", SqlDbType.NVarChar, responseData.Source,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@ComStatus", SqlDbType.NVarChar, responseData.ComStatus,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@DAC", SqlDbType.NVarChar, responseData.Dac,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@StateCode", SqlDbType.NVarChar, responseData.StateCode,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@CityCode", SqlDbType.NVarChar, responseData.CityCode,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@ZoneType", SqlDbType.NVarChar, responseData.ZoneType,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@PreviousBalance", SqlDbType.NVarChar,responseData.PreviousBalance, ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Payment", SqlDbType.NVarChar, responseData.Payment,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@PreviousDebit", SqlDbType.NVarChar,responseData.PreviousDebit, ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@FullPreviousDebit", SqlDbType.NVarChar,responseData.FullPreviousDebit, ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@CurrentBillingStatus", SqlDbType.NVarChar,responseData.CurrentBillingStatus, ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@CurrentBillingDueDate", SqlDbType.NVarChar,responseData.CurrentBillingDueDate, ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@CurrentDebit", SqlDbType.NVarChar,responseData.CurrentDebit, ParameterDirection.Input);

                    ConfiguracionComando.AgregaParametro(cmd, "@Fecha01", SqlDbType.NVarChar, responseData.Fecha01,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Cosumo01", SqlDbType.NVarChar, responseData.Consumo01,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@DateDue01", SqlDbType.NVarChar, responseData.DateDue01,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@DatePayment01", SqlDbType.NVarChar,responseData.DatePayment01, ParameterDirection.Input);

                    ConfiguracionComando.AgregaParametro(cmd, "@Fecha02", SqlDbType.NVarChar, responseData.Fecha02,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Cosumo02", SqlDbType.NVarChar, responseData.Consumo02,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@DateDue02", SqlDbType.NVarChar, responseData.DateDue02,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@DatePayment02", SqlDbType.NVarChar,responseData.DatePayment02, ParameterDirection.Input);

                    ConfiguracionComando.AgregaParametro(cmd, "@Fecha03", SqlDbType.NVarChar, responseData.Fecha03,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Cosumo03", SqlDbType.NVarChar, responseData.Consumo03,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@DateDue03", SqlDbType.NVarChar, responseData.DateDue03,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@DatePayment03", SqlDbType.NVarChar,responseData.DatePayment03, ParameterDirection.Input);

                    ConfiguracionComando.AgregaParametro(cmd, "@Fecha04", SqlDbType.NVarChar, responseData.Fecha04,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Cosumo04", SqlDbType.NVarChar, responseData.Consumo04,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@DateDue04", SqlDbType.NVarChar, responseData.DateDue04,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@DatePayment04", SqlDbType.NVarChar,responseData.DatePayment04, ParameterDirection.Input);

                    ConfiguracionComando.AgregaParametro(cmd, "@Fecha05", SqlDbType.NVarChar, responseData.Fecha05,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Cosumo05", SqlDbType.NVarChar, responseData.Consumo05,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@DateDue05", SqlDbType.NVarChar, responseData.DateDue05,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@DatePayment05", SqlDbType.NVarChar,responseData.DatePayment05, ParameterDirection.Input);

                    ConfiguracionComando.AgregaParametro(cmd, "@Fecha06", SqlDbType.NVarChar, responseData.Fecha06,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Cosumo06", SqlDbType.NVarChar, responseData.Consumo06,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@DateDue06", SqlDbType.NVarChar, responseData.DateDue06,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@DatePayment06", SqlDbType.NVarChar,responseData.DatePayment06, ParameterDirection.Input);

                    ConfiguracionComando.AgregaParametro(cmd, "@Fecha07", SqlDbType.NVarChar, responseData.Fecha07,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Cosumo07", SqlDbType.NVarChar, responseData.Consumo07,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@DateDue07", SqlDbType.NVarChar, responseData.DateDue07,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@DatePayment07", SqlDbType.NVarChar,responseData.DatePayment07, ParameterDirection.Input);

                    ConfiguracionComando.AgregaParametro(cmd, "@Fecha08", SqlDbType.NVarChar, responseData.Fecha08,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Cosumo08", SqlDbType.NVarChar, responseData.Consumo08,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@DateDue08", SqlDbType.NVarChar, responseData.DateDue08,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@DatePayment08", SqlDbType.NVarChar,responseData.DatePayment08, ParameterDirection.Input);

                    ConfiguracionComando.AgregaParametro(cmd, "@Fecha09", SqlDbType.NVarChar, responseData.Fecha09,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Cosumo09", SqlDbType.NVarChar, responseData.Consumo09,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@DateDue09", SqlDbType.NVarChar, responseData.DateDue09,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@DatePayment09", SqlDbType.NVarChar,responseData.DatePayment09, ParameterDirection.Input);

                    ConfiguracionComando.AgregaParametro(cmd, "@Fecha10", SqlDbType.NVarChar, responseData.Fecha10,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Cosumo10", SqlDbType.NVarChar, responseData.Consumo10,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@DateDue10", SqlDbType.NVarChar, responseData.DateDue10,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@DatePayment10", SqlDbType.NVarChar,responseData.DatePayment10, ParameterDirection.Input);

                    ConfiguracionComando.AgregaParametro(cmd, "@Fecha11", SqlDbType.NVarChar, responseData.Fecha11,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Cosumo11", SqlDbType.NVarChar, responseData.Consumo11,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@DateDue11", SqlDbType.NVarChar, responseData.DateDue11,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@DatePayment11", SqlDbType.NVarChar,responseData.DatePayment11, ParameterDirection.Input);

                    ConfiguracionComando.AgregaParametro(cmd, "@Fecha12", SqlDbType.NVarChar, responseData.Fecha12,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Cosumo12", SqlDbType.NVarChar, responseData.Consumo12,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@DateDue12", SqlDbType.NVarChar, responseData.DateDue12,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@DatePayment12", SqlDbType.NVarChar,responseData.DatePayment12, ParameterDirection.Input);

                    ConfiguracionComando.AgregaParametro(cmd, "@Fecha13", SqlDbType.NVarChar, responseData.Fecha13,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Cosumo13", SqlDbType.NVarChar, responseData.Consumo13,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@DateDue13", SqlDbType.NVarChar, responseData.DateDue13,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@DatePayment13", SqlDbType.NVarChar,responseData.DatePayment13, ParameterDirection.Input);

                    ConfiguracionComando.AgregaParametro(cmd, "@Fecha14", SqlDbType.NVarChar, responseData.Fecha14,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Cosumo14", SqlDbType.NVarChar, responseData.Consumo14,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@DateDue14", SqlDbType.NVarChar, responseData.DateDue14,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@DatePayment14", SqlDbType.NVarChar,responseData.DatePayment14, ParameterDirection.Input);

                    ConfiguracionComando.AgregaParametro(cmd, "@Fecha15", SqlDbType.NVarChar, responseData.Fecha15,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Cosumo15", SqlDbType.NVarChar, responseData.Consumo15,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@DateDue15", SqlDbType.NVarChar, responseData.DateDue15,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@DatePayment15", SqlDbType.NVarChar,responseData.DatePayment15, ParameterDirection.Input);

                    ConfiguracionComando.AgregaParametro(cmd, "@Fecha16", SqlDbType.NVarChar, responseData.Fecha16,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Cosumo16", SqlDbType.NVarChar, responseData.Consumo16,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@DateDue16", SqlDbType.NVarChar, responseData.DateDue16,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@DatePayment16", SqlDbType.NVarChar,responseData.DatePayment16, ParameterDirection.Input);

                    ConfiguracionComando.AgregaParametro(cmd, "@Fecha17", SqlDbType.NVarChar, responseData.Fecha17,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Cosumo17", SqlDbType.NVarChar, responseData.Consumo17,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@DateDue17", SqlDbType.NVarChar, responseData.DateDue17,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@DatePayment17", SqlDbType.NVarChar,responseData.DatePayment17, ParameterDirection.Input);

                    ConfiguracionComando.AgregaParametro(cmd, "@Fecha18", SqlDbType.NVarChar, responseData.Fecha18,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Cosumo18", SqlDbType.NVarChar, responseData.Consumo18,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@DateDue18", SqlDbType.NVarChar, responseData.DateDue18,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@DatePayment18", SqlDbType.NVarChar,responseData.DatePayment18, ParameterDirection.Input);

                    ConfiguracionComando.AgregaParametro(cmd, "@Fecha19", SqlDbType.NVarChar, responseData.Fecha19,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Cosumo19", SqlDbType.NVarChar, responseData.Consumo19,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@DateDue19", SqlDbType.NVarChar, responseData.DateDue19,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@DatePayment19", SqlDbType.NVarChar,responseData.DatePayment19, ParameterDirection.Input);

                    ConfiguracionComando.AgregaParametro(cmd, "@Fecha20", SqlDbType.NVarChar, responseData.Fecha20,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Cosumo20", SqlDbType.NVarChar, responseData.Consumo20,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@DateDue20", SqlDbType.NVarChar, responseData.DateDue20,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@DatePayment20", SqlDbType.NVarChar,responseData.DatePayment20, ParameterDirection.Input);

                    ConfiguracionComando.AgregaParametro(cmd, "@Fecha21", SqlDbType.NVarChar, responseData.Fecha21,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Cosumo21", SqlDbType.NVarChar, responseData.Consumo21,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@DateDue21", SqlDbType.NVarChar, responseData.DateDue21,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@DatePayment21", SqlDbType.NVarChar,responseData.DatePayment21, ParameterDirection.Input);

                    ConfiguracionComando.AgregaParametro(cmd, "@Fecha22", SqlDbType.NVarChar, responseData.Fecha22,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Cosumo22", SqlDbType.NVarChar, responseData.Consumo22,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@DateDue22", SqlDbType.NVarChar, responseData.DateDue22,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@DatePayment22", SqlDbType.NVarChar,responseData.DatePayment22, ParameterDirection.Input);

                    ConfiguracionComando.AgregaParametro(cmd, "@Fecha23", SqlDbType.NVarChar, responseData.Fecha23,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Cosumo23", SqlDbType.NVarChar, responseData.Consumo23,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@DateDue23", SqlDbType.NVarChar, responseData.DateDue23,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@DatePayment23", SqlDbType.NVarChar,responseData.DatePayment23, ParameterDirection.Input);

                    ConfiguracionComando.AgregaParametro(cmd, "@Fecha24", SqlDbType.NVarChar, responseData.Fecha24,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Cosumo24", SqlDbType.NVarChar, responseData.Consumo24,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@DateDue24", SqlDbType.NVarChar, responseData.DateDue24,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@DatePayment24", SqlDbType.NVarChar,responseData.DatePayment24, ParameterDirection.Input);

                    ConfiguracionComando.AgregaParametro(cmd, "@Region", SqlDbType.NVarChar, responseData.Region,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Zona", SqlDbType.NVarChar, responseData.Zona,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Estado", SqlDbType.NVarChar, responseData.Estado,ParameterDirection.Input);

                    ConfiguracionComando.AgregaParametro(cmd, "@Demandar_01", SqlDbType.NVarChar,responseData.Demandar01, ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Demandar_02", SqlDbType.NVarChar,responseData.Demandar02, ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Demandar_03", SqlDbType.NVarChar,responseData.Demandar03, ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Demandar_04", SqlDbType.NVarChar,responseData.Demandar04, ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Demandar_05", SqlDbType.NVarChar,responseData.Demandar05, ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Demandar_06", SqlDbType.NVarChar,responseData.Demandar06, ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Demandar_07", SqlDbType.NVarChar,responseData.Demandar07, ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Demandar_08", SqlDbType.NVarChar,responseData.Demandar08, ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Demandar_09", SqlDbType.NVarChar,responseData.Demandar09, ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Demandar_10", SqlDbType.NVarChar,responseData.Demandar10, ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Demandar_11", SqlDbType.NVarChar,responseData.Demandar11, ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Demandar_12", SqlDbType.NVarChar,responseData.Demandar12, ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Demandar_13", SqlDbType.NVarChar,responseData.Demandar13, ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Demandar_14", SqlDbType.NVarChar,responseData.Demandar14, ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Demandar_15", SqlDbType.NVarChar,responseData.Demandar15, ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Demandar_16", SqlDbType.NVarChar,responseData.Demandar16, ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Demandar_AVG", SqlDbType.NVarChar, responseData.DemandarMax, ParameterDirection.Input);


                    //ConfiguracionComando.AgregaParametro(cmd, "@FechaAlta", SqlDbType.DateTime, responseData.FechaAlta,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@FechaConsulta", SqlDbType.DateTime,responseData.FechaConsulta, ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@ClaveIVA", SqlDbType.NVarChar, responseData.ClaveIva,ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@DebitNumber", SqlDbType.NVarChar,responseData.DebitNumber, ParameterDirection.Input);


                    ConfiguracionComando.AgregaParametro(cmd, "@FactorPotencia01", SqlDbType.Decimal,responseData.FactorPotencia01, ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@FactorPotencia02", SqlDbType.Decimal,responseData.FactorPotencia02, ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@FactorPotencia03", SqlDbType.Decimal,responseData.FactorPotencia03, ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@FactorPotencia04", SqlDbType.Decimal,responseData.FactorPotencia04, ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@FactorPotencia05", SqlDbType.Decimal,responseData.FactorPotencia05, ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@FactorPotencia06", SqlDbType.Decimal,responseData.FactorPotencia06, ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@FactorPotencia07", SqlDbType.Decimal,responseData.FactorPotencia07, ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@FactorPotencia08", SqlDbType.Decimal,responseData.FactorPotencia08, ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@FactorPotencia09", SqlDbType.Decimal,responseData.FactorPotencia09, ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@FactorPotencia10", SqlDbType.Decimal,responseData.FactorPotencia10, ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@FactorPotencia11", SqlDbType.Decimal,responseData.FactorPotencia11, ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@FactorPotencia12", SqlDbType.Decimal,responseData.FactorPotencia12, ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@FactorPotencia13", SqlDbType.Decimal,responseData.FactorPotencia13, ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@FactorPotencia14", SqlDbType.Decimal,responseData.FactorPotencia14, ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@FactorPotencia15", SqlDbType.Decimal,responseData.FactorPotencia15, ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@FactorPotencia16", SqlDbType.Decimal,responseData.FactorPotencia16, ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@Trama", SqlDbType.NVarChar, responseData.Trama, ParameterDirection.Input);

                    ConfiguracionComando.AgregaParametro(cmd, "@MENSAJE", SqlDbType.VarChar, mensaje, 1000,
                                                         ParameterDirection.Output);

                    sqlConn.Open();

                    cmd.ExecuteNonQuery();

                    mensaje = cmd.Parameters["@MENSAJE"].Value.ToString();

                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if(sqlConn.State == ConnectionState.Open) sqlConn.Close();
                sqlConn.Dispose();               
            }

            return mensaje;
        }
    }
}
