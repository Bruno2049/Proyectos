/*
	Copyright IMPRA, Inc. 2010
	All rights are reserved. Reproduction or transmission in whole or in part,
      in any form or by any means, electronic, mechanical or otherwise, is 
prohibited without the prior written consent of the copyright owner.

	$Archive:    $
	$Revision:   $
	$Author:     $
	$Date:       $
	Log at end of file
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Transactions;
using PAEEEM.DataAccessLayer;
using PAEEEM.Entidades;
using PAEEEM.Entities;
using PAEEEM.Helpers;

namespace PAEEEM.BussinessLayer
{
    public class K_CREDITOBll
    {
        private static readonly K_CREDITOBll _classinstance = new K_CREDITOBll();

        public static K_CREDITOBll ClassInstance { get { return _classinstance; } }
        /// <summary>
        /// Calculate the amortizacion table based upon the base values
        /// </summary>
        /// <param name="creditNumber">Credit Number</param>
        /// <param name="montoSolicitado">Total Quote Money</param>
        /// <param name="pctTasaFija">Pct_Tasa_Fija</param>
        /// <param name="plazoPago">Number of plazo Pago</param>
        /// <param name="periodo">Payment Period: Monthly or Bimonthly</param>
        /// <param name="pctTasaInteres">Pct_Tasa_Interes</param>
        /// <param name="pctTasaIVA">Pct_Tasa_IVA</param>
        /// <param name="pctCAT">Pct_IVA</param>
        /// <param name="periodEndDate">Period End Date</param>
        /// <returns>Calculated Amortization Result Table</returns>
        public DataTable CalculateCreditAmortizacion(string creditNumber, double montoSolicitado, double pctTasaFija, int plazoPago, int periodo, double pctTasaInteres, double pctTasaIVA, double pctCAT, string periodEndDate)
        {
            DataTable dtResult = null;
            const int BaseComercialDays = 360;
            int AmortizationYear=1, PeriodDays=0;
            double FixedPaymentNumber, FixedPaymentNumberRoundUp;
            DateTime PeriodEndDate, ProbableDeliveryDate, PeriodStartDate, FD, DD = DateTime.Now.Date;
            DateTime FirstPayDate = DateTime.Now.Date;

            try
            {
                //check period end date
                if (!string.IsNullOrEmpty(periodEndDate))
                {
                    string[] format = {"yyyyMMdd", "yyyy/MM/dd", "yyyy-MM-dd"};
                    PeriodEndDate = DateTime.ParseExact(periodEndDate, format, System.Threading.Thread.CurrentThread.CurrentCulture, System.Globalization.DateTimeStyles.None).Date;

                    ProbableDeliveryDate = DateTime.Now.AddDays(25).Date;
                    PeriodStartDate = PeriodEndDate.AddDays(1).Date;

                    TimeSpan span = ProbableDeliveryDate - PeriodStartDate;
                    //Init base parameters
                    if (periodo == 1 /*Monthly Billing*/)
                    {
                        AmortizationYear = 12;
                        PeriodDays = 30;

                        if (span.TotalDays <= 20)
                        {
                            FD = PeriodStartDate.AddDays(30).Date;
                        }
                        else
                        {
                            FD = PeriodStartDate.AddDays(60).Date;
                        }

                        DD = FD.AddDays(11).Date.AddDays(15).Date;
                    }
                    else  /*Bimonthly Billing*/
                    {
                        AmortizationYear = 6;
                        PeriodDays = 60;

                        if (span.TotalDays <= 49)
                        {
                            FD = PeriodStartDate.AddDays(60).Date;
                        }
                        else
                        {
                            FD = PeriodStartDate.AddDays(120).Date;
                        }

                        DD = FD.AddDays(22).Date.AddDays(15).Date;
                    }

                    //calculate the first pay date
                    if (System.Math.Abs(DD.Day - 16) <= System.Math.Abs(DD.Day - 28))
                    {
                        FirstPayDate = new DateTime(DD.Year, DD.Month, 16);
                    }
                    else
                    {
                        FirstPayDate = new DateTime(DD.Year, DD.Month, 28);
                    }
                }
                //Calculate fixed payment value
                double p = System.Math.Pow(1 + (pctTasaFija / AmortizationYear), -plazoPago);

                FixedPaymentNumber = montoSolicitado / ((1 - p) / (pctTasaFija / AmortizationYear));//fixed payment value
                FixedPaymentNumberRoundUp = System.Math.Round(FixedPaymentNumber, 0);//fixed payment round up value
                if (FixedPaymentNumberRoundUp <= FixedPaymentNumber)
                {
                    FixedPaymentNumberRoundUp += 1;
                }
                //Calculate the payment schedule table
                //prepare calculation result table schema
                DataTable dtTempAmortization = new DataTable();
                dtTempAmortization.Columns.Add("No_Credito", typeof(string));
                dtTempAmortization.Columns.Add("No_Pago", typeof(int));
                dtTempAmortization.Columns.Add("Dt_Fecha", typeof(DateTime));
                dtTempAmortization.Columns.Add("No_Dias_Periodo", typeof(int));
                dtTempAmortization.Columns.Add("Mt_Capital", typeof(double));
                dtTempAmortization.Columns.Add("Mt_Interes", typeof(double));
                dtTempAmortization.Columns.Add("Mt_IVA", typeof(double));
                dtTempAmortization.Columns.Add("Mt_Pago", typeof(double));
                dtTempAmortization.Columns.Add("Mt_Amortizacion", typeof(double));
                dtTempAmortization.Columns.Add("Mt_Saldo", typeof(double));
                dtTempAmortization.Columns.Add("Dt_Fecha_Credito_Amortización", typeof(DateTime));
                //calculation
                for (int i = 1; i <= plazoPago; i++)
                {
                    List<double> Intereses = new List<double>();
                    double TempInteres;

                    DataRow Row = dtTempAmortization.NewRow();
                    Row["No_Credito"] = creditNumber;
                    Row["No_Pago"] = i;
                    //Row["Dt_Fecha"] = DateTime.Now.Date.AddMonths(i - 1);
                    Row["No_Dias_Periodo"] = PeriodDays;
                    Row["Dt_Fecha_Credito_Amortización"] = DateTime.Now.Date;
                    if (i == 1)
                    {
                        Row["Mt_Capital"] = string.Format("{0:0.00}",montoSolicitado);
                        Row["Dt_Fecha"] = FirstPayDate;
                    }
                    else
                    {
                        Row["Mt_Capital"] = string.Format("{0:0.00}", dtTempAmortization.Rows[i - 2]["Mt_Saldo"]);
                        if (periodo == 1)//monthly billing
                        {
                            Row["Dt_Fecha"] = FirstPayDate.AddMonths(i - 1);
                        }
                        else//bimonthly billing
                        {
                            Row["Dt_Fecha"] = FirstPayDate.AddMonths((i - 1)*2);
                        }
                    }
                    //calculate temp interest middle variable
                    TempInteres = Convert.ToDouble(Row["Mt_Capital"].ToString()) * Convert.ToDouble(Row["No_Dias_Periodo"].ToString()) * pctTasaFija / BaseComercialDays;
                    Intereses.Add(TempInteres);//maybe used later
                    if (i != plazoPago)
                    {
                        Row["Mt_Amortizacion"] = string.Format("{0:0.00}", FixedPaymentNumber - TempInteres);
                    }
                    else
                    {
                        Row["Mt_Amortizacion"] = Row["Mt_Capital"];
                    }
                    Row["Mt_Interes"] = string.Format("{0:0.00}", TempInteres / (1 + pctTasaIVA));
                    Row["Mt_IVA"] = string.Format("{0:0.00}", Convert.ToDouble(Row["Mt_Interes"].ToString()) * pctTasaIVA);
                    if (i != plazoPago)
                    {
                        Row["Mt_Pago"] = string.Format("{0:0}", FixedPaymentNumberRoundUp);
                    }
                    else
                    {
                        Row["Mt_Pago"] = string.Format("{0:0}", FixedPaymentNumberRoundUp + (FixedPaymentNumber * plazoPago - FixedPaymentNumberRoundUp * plazoPago));
                    }

                    Row["Mt_Saldo"] = string.Format("{0:0.00}", Convert.ToDouble(Row["Mt_Capital"].ToString()) - Convert.ToDouble(Row["Mt_Amortizacion"].ToString()));
                    //Fixing Interest value

                    Row["Mt_Interes"] = (Convert.ToDouble(Row["Mt_Pago"]) - Convert.ToDouble(Row["Mt_Amortizacion"]) - Convert.ToDouble(Row["Mt_IVA"])) ;

                    //Add row to table
                    dtTempAmortization.Rows.Add(Row);
                }
                //copy data to result table
                dtResult = dtTempAmortization.Copy();
            }
            catch (Exception ex)
            {
                LsLog.LogToFile(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                dtResult = null;
            }
            return dtResult;
        }
        
        /// Si la primera Amorizacion no cumple con la regla de 15 días, se toma la fecha de la segunda amortizacion como primer pago
        public DataTable CalculateCreditAmortizacionReactiva(string creditNumber, double montoSolicitado, double pctTasaFija, int plazoPago, int periodo, double pctTasaInteres, double pctTasaIVA, double pctCAT, DateTime startDate)
        {
            DataTable dtResult = null;
            const int BaseComercialDays = 360;
            int AmortizationYear = 1, PeriodDays = 0;
            double FixedPaymentNumber, FixedPaymentNumberRoundUp;
            DateTime PeriodEndDate, ProbableDeliveryDate, PeriodStartDate, FD, DD = DateTime.Now.Date;
            DateTime FirstPayDate = DateTime.Now.Date;

            try
            {                
                if (periodo == 1 /*Monthly Billing*/)
                {
                    AmortizationYear = 12;
                    PeriodDays = 30;
                }
                else
                {
                    AmortizationYear = 6;
                    PeriodDays = 60;
                }

                FirstPayDate = new DateTime(startDate.Year, startDate.Month, startDate.Day);
                //Calculate fixed payment value
                double p = System.Math.Pow(1 + (pctTasaFija / AmortizationYear), -plazoPago);

                FixedPaymentNumber = montoSolicitado / ((1 - p) / (pctTasaFija / AmortizationYear));//fixed payment value
                FixedPaymentNumberRoundUp = System.Math.Round(FixedPaymentNumber, 0);//fixed payment round up value
                if (FixedPaymentNumberRoundUp <= FixedPaymentNumber)
                {
                    FixedPaymentNumberRoundUp += 1;
                }
                //Calculate the payment schedule table
                //prepare calculation result table schema
                DataTable dtTempAmortization = new DataTable();
                dtTempAmortization.Columns.Add("No_Credito", typeof(string));
                dtTempAmortization.Columns.Add("No_Pago", typeof(int));
                dtTempAmortization.Columns.Add("Dt_Fecha", typeof(DateTime));
                dtTempAmortization.Columns.Add("No_Dias_Periodo", typeof(int));
                dtTempAmortization.Columns.Add("Mt_Capital", typeof(double));
                dtTempAmortization.Columns.Add("Mt_Interes", typeof(double));
                dtTempAmortization.Columns.Add("Mt_IVA", typeof(double));
                dtTempAmortization.Columns.Add("Mt_Pago", typeof(double));
                dtTempAmortization.Columns.Add("Mt_Amortizacion", typeof(double));
                dtTempAmortization.Columns.Add("Mt_Saldo", typeof(double));
                dtTempAmortization.Columns.Add("Dt_Fecha_Credito_Amortización", typeof(DateTime));
                //calculation
                for (int i = 1; i <= plazoPago; i++)
                {
                    List<double> Intereses = new List<double>();
                    double TempInteres;

                    DataRow Row = dtTempAmortization.NewRow();
                    Row["No_Credito"] = creditNumber;
                    Row["No_Pago"] = i;
                    //Row["Dt_Fecha"] = DateTime.Now.Date.AddMonths(i - 1);
                    Row["No_Dias_Periodo"] = PeriodDays;
                    Row["Dt_Fecha_Credito_Amortización"] = DateTime.Now.Date;
                    if (i == 1)
                    {
                        Row["Mt_Capital"] = string.Format("{0:0.00}", montoSolicitado);
                        Row["Dt_Fecha"] = FirstPayDate;
                    }
                    else
                    {
                        Row["Mt_Capital"] = string.Format("{0:0.00}", dtTempAmortization.Rows[i - 2]["Mt_Saldo"]);
                        if (periodo == 1)//monthly billing
                        {
                            Row["Dt_Fecha"] = FirstPayDate.AddMonths(i - 1);
                        }
                        else//bimonthly billing
                        {
                            Row["Dt_Fecha"] = FirstPayDate.AddMonths((i - 1) * 2);
                        }
                    }
                    //calculate temp interest middle variable
                    TempInteres = Convert.ToDouble(Row["Mt_Capital"].ToString()) * Convert.ToDouble(Row["No_Dias_Periodo"].ToString()) * pctTasaFija / BaseComercialDays;
                    Intereses.Add(TempInteres);//maybe used later
                    if (i != plazoPago)
                    {
                        Row["Mt_Amortizacion"] = string.Format("{0:0.00}", FixedPaymentNumber - TempInteres);
                    }
                    else
                    {
                        Row["Mt_Amortizacion"] = Row["Mt_Capital"];
                    }
                    Row["Mt_Interes"] = string.Format("{0:0.00}", TempInteres / (1 + pctTasaIVA));
                    Row["Mt_IVA"] = string.Format("{0:0.00}", Convert.ToDouble(Row["Mt_Interes"].ToString()) * pctTasaIVA);
                    if (i != plazoPago)
                    {
                        Row["Mt_Pago"] = string.Format("{0:0}", FixedPaymentNumberRoundUp);
                    }
                    else
                    {
                        Row["Mt_Pago"] = string.Format("{0:0}", FixedPaymentNumberRoundUp + (FixedPaymentNumber * plazoPago - FixedPaymentNumberRoundUp * plazoPago));
                    }

                    Row["Mt_Saldo"] = string.Format("{0:0.00}", Convert.ToDouble(Row["Mt_Capital"].ToString()) - Convert.ToDouble(Row["Mt_Amortizacion"].ToString()));
                    //Fixing Interest value

                    Row["Mt_Interes"] = (Convert.ToDouble(Row["Mt_Pago"]) - Convert.ToDouble(Row["Mt_Amortizacion"]) - Convert.ToDouble(Row["Mt_IVA"]));

                    //Add row to table
                    dtTempAmortization.Rows.Add(Row);
                }
                //copy data to result table
                dtResult = dtTempAmortization.Copy();
            }
            catch (Exception ex)
            {
                LsLog.LogToFile(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                dtResult = null;
            }
            return dtResult;
        }

        /// <summary>
        /// Calculate the payment capacity
        /// </summary>
        /// <param name="plazoPago">Plazo Pago</param>
        /// <param name="monthlyNetIncome">Monthly Net Income</param>
        /// <param name="economicConsumptionSaving">Monthly Economic Consumption Savings With New Equipment</param>
        /// <param name="sixPeriodAnnualTotalCost">Annual Total Cost For Six Periods</param>
        /// <param name="twelvePeriodAnnualTotalCost">Annual Total Cost For Twelve Periods</param>
        /// <param name="period">Payment Frequency--Monthly Billing or Bimonthly Billing</param>
        /// <returns></returns>
        public double CalculatePaymentCapacity(int plazoPago, double monthlyNetIncome, double economicConsumptionSaving, double PeriodAnnualTotalCost, int period)
        {
            double paymentCapacity = 0.0;
            //double tempCalculatedNumber = default(double);//(A+0.3U)*2 or (A+0.3U), A:Economic Consumption Saving, U: Monthly net Income
            double tempCalculatedNumber = default(double);//new changed logic: A * 2 or A, A:Economic Consumption Saving
            double AnnualCostPeriod;
            double annualTotalCost = default(double);
            try
            {
                //Init base parameters
                if (period == 1 /*Monthly Billing*/)
                {
                    AnnualCostPeriod = 12.0000;
                    annualTotalCost = PeriodAnnualTotalCost;
                    //tempCalculatedNumber = economicConsumptionSaving + 0.3 * monthlyNetIncome;
                    tempCalculatedNumber = economicConsumptionSaving;//new changed logic
                }
                else if (period == 2 /*Bimonthly Billing*/)
                {
                    AnnualCostPeriod = 6.0000;
                    annualTotalCost = PeriodAnnualTotalCost;
                    //tempCalculatedNumber = (economicConsumptionSaving + 0.3 * monthlyNetIncome) * 2;
                    tempCalculatedNumber = economicConsumptionSaving; //* 2;//new changed logic
                }
                else
                {
                    //DO NOTHING
                    AnnualCostPeriod = 1;
                }
                //Calculate payment capacity
                for (int i = 1; i <= plazoPago; i++)
                {
                    double pow = i / AnnualCostPeriod;
                    double value = 1 + annualTotalCost;

                    paymentCapacity += tempCalculatedNumber / System.Math.Pow(value, pow);
                }
            }
            catch (Exception ex)
            {
                LsLog.LogToFile(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }

            return System.Math.Round(paymentCapacity, 2);
        }
        /// <summary>
        /// Calculate economic consumption savings
        /// </summary>
        /// <param name="cevEstadoNeg">Estado Neg</param>
        /// <param name="averageConsumption">Average Consumption Per Month</param>
        /// <param name="energyConsumptionSavings">Energy Consumption Savings per Month</param>
        /// <param name="pacTasaIVA">Tax</param>
        /// <returns></returns>
        public double CalculateEconomicConsumptionSavings(int cevEstadoNeg, double averageConsumption, double energyConsumptionSavings, double pctTasaIVA, string rate, ref double outOriginalConsumption)
        {
            double economicConsumptionSavings = default(double);
            List<int> keys = new List<int>();
            List<double> values = new List<double>();
            DataTable dtTarifa, dtTarifaCost;
            double originalEconomicConsumption, savingEconomicConsumption;

            try
            {
                //dtTarifa = CAT_TARIFADAL.ClassInstance.GetTarifaWithDate(DateTime.Now, 0/*reserved for later extention use*/);
                //dtTarifa = CAT_TARIFADAL.ClassInstance.GetTarifaWithoutDate(0/*reserved for later extention use*/);
                dtTarifa = CAT_TARIFADAL.ClassInstance.GetTarifaWithName(rate);

                if (dtTarifa != null && dtTarifa.Rows.Count > 0)
                {
                    dtTarifaCost = K_TARIFA_COSTODAL.ClassInstance.GetTarifaAndCostWithDateEstado(rate, cevEstadoNeg, DateTime.Now);
                    if (dtTarifaCost != null && dtTarifaCost.Rows.Count > 0)
                    {
                        keys.Add(Convert.ToInt32(dtTarifa.Rows[0]["No_Cargo_Fijo"]));
                        keys.Add(Convert.ToInt32(dtTarifa.Rows[0]["No_Basico_Hasta"]));
                        keys.Add(Convert.ToInt32(dtTarifa.Rows[0]["No_Intermedio_Hasta"]));
                        keys.Add(Convert.ToInt32(dtTarifa.Rows[0]["No_Excedente_Mayor_que"]));

                        values.Add(Convert.ToDouble(dtTarifaCost.Rows[0]["Mt_Costo_Kw_h_Fijo"]));
                        values.Add(Convert.ToDouble(dtTarifaCost.Rows[0]["Mt_Costo_Kw_h_Basico"]));
                        values.Add(Convert.ToDouble(dtTarifaCost.Rows[0]["Mt_Costo_Kw_h_Intermedio"]));
                        values.Add(Convert.ToDouble(dtTarifaCost.Rows[0]["Mt_Costo_Kw_h_Excedente"]));
                        values.Add(Convert.ToDouble(dtTarifaCost.Rows[0]["Mt_Tarifa_Demanda"]));
                        values.Add(Convert.ToDouble(dtTarifaCost.Rows[0]["Mt_Costo_Tarifa_Consumo"]));
                        //Calculate original economic consumption
                        originalEconomicConsumption = Calculator(keys, values, averageConsumption);
                        //include the Tax
                        originalEconomicConsumption += originalEconomicConsumption * pctTasaIVA;
                        //Calculate saving economic consumption
      //                  savingEconomicConsumption = Calculator(keys, values, averageConsumption - energyConsumptionSavings);
                        savingEconomicConsumption = Calculator(keys, values, energyConsumptionSavings);

                        //include Tax
                        savingEconomicConsumption += savingEconomicConsumption * pctTasaIVA;

                        //calculate the economic consumption savings
                        economicConsumptionSavings = originalEconomicConsumption - savingEconomicConsumption;

                        outOriginalConsumption = originalEconomicConsumption;
                    }
                }
            }
            catch (Exception ex)
            {
                LsLog.LogToFile(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
            return economicConsumptionSavings;
        }
        public double CalculateEconomicConsumptionSavings3(int cevEstadoNeg, double averageConsumption, double pctTasaIVA, double demanda, string strProductID, int periodo, out double ahorroEnergetico, ref double outOriginalConsumption)
        {
            double economicConsumptionSavings = default(double);
            List<int> keys = new List<int>();
            List<double> values = new List<double>();
            DataTable dtProduct, dtTarifaCost;
            double originalEconomicConsumption, savingEconomicConsumption;
            ahorroEnergetico = default(double);

            try
            {
                //dtTarifa = CAT_TARIFADAL.ClassInstance.GetTarifaWithDate(DateTime.Now, 0/*reserved for later extention use*/);
                //dtTarifa = CAT_TARIFADAL.ClassInstance.GetTarifaWithoutDate(0/*reserved for later extention use*/);
                //dtTarifa = CAT_TARIFADAL.ClassInstance.GetTarifaWithName("03");
                dtProduct = CAT_PRODUCTODal.ClassInstance.Get_CAT_PRODUCTO_ByPK(strProductID);
                //if (dtTarifa != null && dtTarifa.Rows.Count > 0)
                if (dtProduct != null && dtProduct.Rows.Count > 0)
                {
                    double ahorroDemanda = Convert.ToDouble(dtProduct.Rows[0]["Ahorro_Demanda"]);
                    double ahorroConsumo = Convert.ToDouble(dtProduct.Rows[0]["Ahorro_Consumo"]);
                    dtTarifaCost = K_TARIFA_COSTODAL.ClassInstance.GetTarifaAndCostWithDateEstado("03", cevEstadoNeg, DateTime.Now);
                    if (dtTarifaCost != null && dtTarifaCost.Rows.Count > 0)
                    {
                        /*keys.Add(Convert.ToInt32(dtTarifa.Rows[0]["No_Cargo_Fijo"]));
                        keys.Add(Convert.ToInt32(dtTarifa.Rows[0]["No_Basico_Hasta"]));
                        keys.Add(Convert.ToInt32(dtTarifa.Rows[0]["No_Intermedio_Hasta"]));
                        keys.Add(Convert.ToInt32(dtTarifa.Rows[0]["No_Excedente_Mayor_que"]));
                        
                        values.Add(Convert.ToDouble(dtTarifaCost.Rows[0]["Mt_Costo_Kw_h_Fijo"]));
                        values.Add(Convert.ToDouble(dtTarifaCost.Rows[0]["Mt_Costo_Kw_h_Basico"]));
                        values.Add(Convert.ToDouble(dtTarifaCost.Rows[0]["Mt_Costo_Kw_h_Intermedio"]));
                        values.Add(Convert.ToDouble(dtTarifaCost.Rows[0]["Mt_Costo_Kw_h_Excedente"]));
                         */
                        values.Add(Convert.ToDouble(dtTarifaCost.Rows[0]["Mt_Tarifa_Demanda"]));
                        values.Add(Convert.ToDouble(dtTarifaCost.Rows[0]["Mt_Costo_Tarifa_Consumo"]));

                        

                        
                        //double energyConsumptionSavings 


                        //Calculate original economic consumption
                        originalEconomicConsumption = (demanda*values[0] + averageConsumption*values[1])*periodo;
                        //include the Tax
                        originalEconomicConsumption += originalEconomicConsumption * pctTasaIVA;
                        //Calculate saving economic consumption
                        //                  savingEconomicConsumption = Calculator(keys, values, averageConsumption - energyConsumptionSavings);
                        savingEconomicConsumption = ((demanda-ahorroDemanda)*values[0]+(averageConsumption-ahorroConsumo)*values[1])*periodo;
                        //    Calculator(keys, values, energyConsumptionSavings);

                        //include Tax
                        savingEconomicConsumption += savingEconomicConsumption * pctTasaIVA;

                        //calculate the economic consumption savings
                        economicConsumptionSavings = originalEconomicConsumption - savingEconomicConsumption;
                        //Calculate ahorroenergetico
                        ahorroEnergetico=(ahorroDemanda+ahorroConsumo)*periodo;

                        outOriginalConsumption = originalEconomicConsumption;
                    }
                }
            }
            catch (Exception ex)
            {
                LsLog.LogToFile(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
           
            return economicConsumptionSavings;
        }
        public double CalculateAhorroEconomicoSE(string tarifaIni, string tarifaFin, double highAvgConsumption, string strProductID, double pctTasaIVA, out double ahorroEnergetico, ref double outOriginalConsumption)
        {
            double ahorro_economico = 0;
            DataTable datosIni = CAT_TARIFADAL.ClassInstance.GetTarifaCFEWithoutDate(tarifaIni);
            DataTable datosFin = CAT_TARIFADAL.ClassInstance.GetTarifaCFEWithoutDate(tarifaFin);
            DataTable dtProduct = CAT_PRODUCTODal.ClassInstance.Get_CAT_PRODUCTO_ByPK(strProductID);
            double ahorroConsumo = 0;
            ahorroEnergetico = 0;

            if (dtProduct != null && dtProduct.Rows.Count > 0 && double.TryParse(dtProduct.Rows[0]["Ahorro_Consumo"].ToString(), out ahorroConsumo))
            {
                ahorroEnergetico = highAvgConsumption * ahorroConsumo / 100;
            }

            if (datosIni != null && datosIni.Rows.Count > 0 && datosFin != null && datosFin.Rows.Count > 0)
            {
                ahorro_economico = highAvgConsumption * (
                        Convert.ToDouble(datosIni.Rows[0]["Mt_Costo_Tarifa_Consumo"]) -
                        Convert.ToDouble(datosFin.Rows[0]["Mt_Costo_Tarifa_Consumo"])
                    ) * (1 + pctTasaIVA);
                outOriginalConsumption = highAvgConsumption * Convert.ToDouble(datosIni.Rows[0]["Mt_Costo_Tarifa_Consumo"]) * (1 + pctTasaIVA);
            }

            return ahorro_economico;
        }

        /// <summary>
        /// Calculator for consumption
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="values"></param>
        /// <param name="consumption"></param>
        /// <returns></returns>
        private double Calculator(List<int> keys, List<double> values, double consumption)
        {
            double calculatorResult = default(double);

            if (consumption <= keys[1])
            {
                calculatorResult = values[0] + consumption * values[1];
            }
            else if (consumption > keys[1] && consumption <= keys[2])
            {
                calculatorResult = values[0] + keys[1] * values[1] + (consumption - keys[1]) * values[2];
            }
            else
            {
                calculatorResult = values[0] + keys[1] * values[1] + (keys[2] - keys[1]) * values[2] + (consumption - keys[2]) * values[3];
            }

            return calculatorResult;
        }
        /// <summary>
        /// insert data
        /// </summary>
        /// <param name="Credit"></param>
        /// <param name="Cost"></param>
        /// <param name="Descuento"></param>
        /// <param name="Product"></param>
        /// <returns></returns>
        public int Insert_K_CreditoData(EntidadCredito Credit, K_CREDITO_COSTOEntity Cost, K_CREDITO_DESCUENTOEntity Descuento, List<K_CREDITO_PRODUCTOEntity> Product, ScheduleJobEntity ScheduleEntity, CAT_AUXILIAREntity Cat_Auxiliar, string PeriodEndDate)
        {
            int Count = 0;
            //int returnTemp = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {

                   //int IMaxAccount = K_CREDITODal.ClassInstance.Select_Max_No_Credit();
                    //Credit.No_Credito = IMaxAccount;
                    Cost.No_Credito = Credit.No_Credito;
                    Descuento.No_Credito = Credit.No_Credito;
                    ScheduleEntity.Credit_No = Credit.No_Credito;
                   
                    //K_CREDITO_AMORTIZACION
                    DataTable CreditAmortizacionDt = CalculateCreditAmortizacion(Credit.No_Credito, Credit.Mt_Monto_Solicitado, Credit.Pct_Tasa_Fija / 100, Credit.No_Plazo_Pago, Credit.Cve_Periodo_Pago, Credit.Pct_Tasa_Interes / 100, Credit.Pct_Tasa_IVA / 100, Credit.Pct_CAT / 100, PeriodEndDate);
                    double deTemp = 0;
                    for (int i = 0; i < CreditAmortizacionDt.Rows.Count; i++)
                    {
                        deTemp = deTemp +double.Parse(CreditAmortizacionDt.Rows[i]["Mt_Pago"].ToString());
                    }
                    Credit.Mt_Monto_Total_Pagar =deTemp;

                    Count += K_CREDITODal.ClassInstance.AddCredit(Credit);
                    if (Count > 0)
                    {     
                        foreach (K_CREDITO_PRODUCTOEntity ProductModel in Product)
                        {
                            ProductModel.No_Credito = Credit.No_Credito;
                            Count += K_CREDITO_PRODUCTODal.ClassInstance.Insert_K_CREDITO_PRODUCTO(ProductModel);
                        }
                        Count += K_CREDITO_DESCUENTODal.ClassInstance.Insert_K_CREDITO_DESCUENTO(Descuento);
                        Count += K_CREDITO_COSTODal.ClassInstance.Insert_K_Credito_Costo(Cost);
                        Count += K_CREDITO_AMORTIZACIONDal.ClassInstance.Insert_K_CREDITO_AMORTIZACION(Credit.No_Credito, CreditAmortizacionDt);
                        Count += ScheduleJobsDal.ClassInstance.AddScheduleJob(ScheduleEntity);
                        // Update by Tina 2011/08/09
                        //if (Cat_Auxiliar.No_Credito == 0)
                        //{
                        //    Cat_Auxiliar.No_Credito = IMaxAccount;
                        //    CAT_AUXILIARDal.ClassInstance.Insert_CAT_AUXILIAR(Cat_Auxiliar);
                        //}
                        Cat_Auxiliar.No_Credito = Credit.No_Credito;
                        Count += CAT_AUXILIARDal.ClassInstance.Insert_CAT_AUXILIAR(Cat_Auxiliar);
                        // End

                        K_CREDITO_TEMPDAL.ClassInstance.Delete_K_credito_TempByUserName(Credit.Dx_Usr_Ultmod); //add by coco 2012-07-18
                    }
                    scope.Complete();
                    //returnTemp = IMaxAccount;
                }
            }
            catch (LsDAException ex)
            {
                throw new LsBLException(this, ex.Message, ex, true);
            }
            return Count;
        }
        /// <summary>
        /// Update credit  only with data collected in step 1 page
        /// </summary>
        /// <param name="Credit"></param>
        /// <returns></returns>
        public int Insert_K_CreditPage1Data(EntidadCredito Credit,CAT_AUXILIAREntity Cat_Auxilira)
        {
            int Result = 0;
            try
            {
                //int IMaxAccount = K_CREDITODal.ClassInstance.Select_Max_No_Credit();
                //Credit.No_Credito = IMaxAccount;

                Result = K_CREDITODal.ClassInstance.AddCredit(Credit);
                if (Result > 0)
                {
                    if (Cat_Auxilira.No_Credito == "0")
                    {
                        Cat_Auxilira.No_Credito = Credit.No_Credito;
                        CAT_AUXILIARDal.ClassInstance.Insert_CAT_AUXILIAR(Cat_Auxilira);
                    }
                }
            }
            catch (LsDAException ex)
            {
                throw new LsBLException(this, ex.Message, ex, true);
            }
            return Result;
        }
        //edit by coco 2011-08-02
        public int Update_Cat_Auxilira(CAT_AUXILIAREntity catAuxilira)
        {
            int iUpdate = 0;
            try
            {                
                DataTable dt = CAT_AUXILIARDal.ClassInstance.Get_CAT_AUXILIARByCreditNo(catAuxilira.No_Credito);
                if (dt.Rows.Count > 0)
                {
                    iUpdate = CAT_AUXILIARDal.ClassInstance.Update_CAT_AUXILIARByCreditoReview(catAuxilira);
                }
                else
                {
                    iUpdate = CAT_AUXILIARDal.ClassInstance.Insert_CAT_AUXILIAR(catAuxilira);                 
                }
            }
            catch (LsDAException ex)
            {
                throw new LsBLException(this, ex.Message, ex, true);
            }
            return iUpdate;
        }
       
        /// <summary>
        /// Update credit when review information
        /// </summary>
        /// <param name="credit"></param>
        /// <param name="lsitCreditProduct"></param>
        /// <param name="CreditAutDt"></param>
        /// <returns></returns>
        public int Update_CreditReview(EntidadCredito credit, List<K_CREDITO_PRODUCTOEntity> lsitCreditProduct, DataTable CreditAutDt,CAT_AUXILIAREntity catAuxilira,K_CREDITO_COSTOEntity creCostEntity, K_CREDITO_DESCUENTOEntity creDestEntity)
        {
            int Iupdate = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    Iupdate = K_CREDITODal.ClassInstance.UpdateCreditReview(credit);
                    if (Iupdate > 0)
                    {
                        if (catAuxilira.No_Credito == "0")
                        {
                            CAT_AUXILIARDal.ClassInstance.Delete_CatAuxilira(credit.No_Credito);
                        }
                        else
                        {
                            K_CREDITOBll.ClassInstance.Update_Cat_Auxilira(catAuxilira);
                        }
                        K_CREDITO_AMORTIZACIONDal.ClassInstance.Dalete_K_CREDITO_AMORTIZACION(credit.No_Credito);
                        K_CREDITO_PRODUCTODal.ClassInstance.Dalete_K_CREDITO_PRODUCTO(credit.No_Credito);
                        foreach (K_CREDITO_PRODUCTOEntity ProductModel in lsitCreditProduct)
                        {                            
                            K_CREDITO_PRODUCTODal.ClassInstance.Insert_K_CREDITO_PRODUCTO(ProductModel);
                        }
                        K_CREDITO_AMORTIZACIONDal.ClassInstance.Insert_K_CREDITO_AMORTIZACION(credit.No_Credito, CreditAutDt);
                        //update  K_CREDITO_COSTO table
                        int iCost= K_CREDITO_COSTODal.ClassInstance.Update_K_Credito_Costo(creCostEntity);
                        if (iCost == 0)
                        {
                            K_CREDITO_COSTODal.ClassInstance.Insert_K_Credito_Costo(creCostEntity);
                        }
                        //update K_CREDITO_DESCUENTO table
                       int ides= K_CREDITO_DESCUENTODal.ClassInstance.Update_K_CREDITO_DESCUENTO(creDestEntity);
                       if (ides == 0)
                       {
                           K_CREDITO_DESCUENTODal.ClassInstance.Insert_K_CREDITO_DESCUENTO(creDestEntity);
                       }
                    }
                    scope.Complete();
                }
            }
            catch (LsDAException ex)
            {
                throw new LsBLException(this, ex.Message, ex, true);
            }
            return Iupdate;
        }
        //end add
        //add by coco 2011-08-02
        /// <summary>
        /// 
        /// </summary>
        /// <param name="credit"></param>
        /// <param name="catAuxilira"></param>
        /// <returns></returns>
        public int Update_CreditReviewExceptProduct(EntidadCredito credit, CAT_AUXILIAREntity catAuxilira)
        {
            int Iupdate = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    Iupdate = K_CREDITODal.ClassInstance.UpdateCreditReview(credit);
                    if (Iupdate > 0)
                    {
                        if (catAuxilira.No_Credito == "0")
                        {
                            CAT_AUXILIARDal.ClassInstance.Delete_CatAuxilira(credit.No_Credito);
                        }
                        else
                        {
                            K_CREDITOBll.ClassInstance.Update_Cat_Auxilira(catAuxilira);
                        }
                        //delete K_CREDITO_AMORTIZACION
                        K_CREDITO_AMORTIZACIONDal.ClassInstance.Dalete_K_CREDITO_AMORTIZACION(credit.No_Credito);
                        //Delete K_CREDITO_PRODUCTO
                        K_CREDITO_PRODUCTODal.ClassInstance.Dalete_K_CREDITO_PRODUCTO(credit.No_Credito);
                        // Cancel Schedule Job
                        ScheduleJobsDal.ClassInstance.CanceledScheduleJob(credit.No_Credito,ParameterHelper.strCon_DBLsWebApp);//Changed by Jerry 2012/04/13
                        //delete Credito cost
                        K_CREDITO_COSTODal.ClassInstance.Delete_K_Credito_Costo(credit.No_Credito);
                        //delete credito discount
                        K_CREDITO_DESCUENTODal.ClassInstance.Delete_K_CREDITO_DESCUENTO(credit.No_Credito);
                    }
                    scope.Complete();
                }
            }
            catch (LsDAException ex)
            {
                throw new LsBLException(this, ex.Message, ex, true);
            }
            return Iupdate;
        }
        //end add    
        //add by coco 2012-07-18
        public int Insert_K_Credito_Temp(K_CREDITO_TEMPEntity entity)
        {
            int Result = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    K_CREDITO_TEMPDAL.ClassInstance.Delete_K_credito_TempByUserName(entity.User_Name);
                    Result = K_CREDITO_TEMPDAL.ClassInstance.Insert_K_Credito_Temp(entity);
                    scope.Complete();
                }
                return Result;
            }
            catch (LsDAException ex)
            {
                throw new LsBLException(this, ex.Message, ex, true);
            }
        }
        //end add
    }
}

