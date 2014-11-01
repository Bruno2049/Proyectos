using System.Collections.Generic;
using System.Data;
using PAEEEM.AccesoDatos;
using PAEEEM.AccesoDatos.Catalogos;
using PAEEEM.AccesoDatos.SolicitudCredito;
using PAEEEM.DataAccessLayer;
using PAEEEM.Entidades;

namespace PAEEEM.LogicaNegocios.Credito
{
    public class blCredito
    {
        public static CRE_Credito Obtener(string NoCredito)
        {
            CRE_Credito credito = beCredito.Obtener(NoCredito);

            return credito;
        }

        public static CRE_Credito Insertar(CRE_Credito credito)
        {
            CRE_Credito nuevo = null;

            var datos = beCredito.Insertar(credito);

            if (datos != null)
            {
                nuevo = datos;
            }

            return nuevo;
        }

        public static bool Actualizar(CRE_Credito credito)
        {
            bool obj;

            using (var r = new Repositorio<CRE_Credito>())
            {
                obj = r.Actualizar(credito);
            }

            return obj;
        }

        //public int Insert_K_CreditoData(EntidadCredito Credit, K_CREDITO_COSTO Cost, K_CREDITO_DESCUENTO Descuento, List<K_CREDITO_PRODUCTO> Product, H_SCHEDULE_JOBS ScheduleEntity, CAT_AUXILIAR Cat_Auxiliar, string PeriodEndDate)
        //{
        //    int Count = 0;
        //    //int returnTemp = 0;
        //    try
        //    {
        //        using (TransactionScope scope = new TransactionScope())
        //        {

        //            //int IMaxAccount = K_CREDITODal.ClassInstance.Select_Max_No_Credit();
        //            //Credit.No_Credito = IMaxAccount;
        //            Cost.No_Credito = Credit.No_Credito;
        //            Descuento.No_Credito = Credit.No_Credito;
        //            ScheduleEntity.Credit_No = Credit.No_Credito;

        //            //K_CREDITO_AMORTIZACION
        //            DataTable CreditAmortizacionDt = CalculateCreditAmortizacion(Credit.No_Credito, Credit.Mt_Monto_Solicitado, Credit.Pct_Tasa_Fija / 100, Credit.No_Plazo_Pago, Credit.Cve_Periodo_Pago, Credit.Pct_Tasa_Interes / 100, Credit.Pct_Tasa_IVA / 100, Credit.Pct_CAT / 100, PeriodEndDate);
        //            double deTemp = 0;
        //            for (int i = 0; i < CreditAmortizacionDt.Rows.Count; i++)
        //            {
        //                deTemp = deTemp + double.Parse(CreditAmortizacionDt.Rows[i]["Mt_Pago"].ToString());
        //            }
        //            Credit.Mt_Monto_Total_Pagar = deTemp;

        //            Count +=
        //                CREDITO_DAL.ClassInstance.AddCredit(Credit);

        //            if (Count > 0)
        //            {
        //                foreach (K_CREDITO_PRODUCTO ProductModel in Product)
        //                {
        //                    ProductModel.No_Credito = Credit.No_Credito;
        //                    Count += K_CREDITO_PRODUCTODal.ClassInstance.Insert_K_CREDITO_PRODUCTO(ProductModel);
        //                }
        //                Count += K_CREDITO_DESCUENTODal.ClassInstance.Insert_K_CREDITO_DESCUENTO(Descuento);
        //                Count += K_CREDITO_COSTODal.ClassInstance.Insert_K_Credito_Costo(Cost);
        //                Count += K_CREDITO_AMORTIZACIONDal.ClassInstance.Insert_K_CREDITO_AMORTIZACION(Credit.No_Credito, CreditAmortizacionDt);
        //                Count += ScheduleJobsDal.ClassInstance.AddScheduleJob(ScheduleEntity);
        //                // Update by Tina 2011/08/09
        //                //if (Cat_Auxiliar.No_Credito == 0)
        //                //{
        //                //    Cat_Auxiliar.No_Credito = IMaxAccount;
        //                //    CAT_AUXILIARDal.ClassInstance.Insert_CAT_AUXILIAR(Cat_Auxiliar);
        //                //}
        //                Cat_Auxiliar.No_Credito = Credit.No_Credito;
        //                Count += CAT_AUXILIARDal.ClassInstance.Insert_CAT_AUXILIAR(Cat_Auxiliar);
        //                // End

        //                K_CREDITO_TEMPDAL.ClassInstance.Delete_K_credito_TempByUserName(Credit.Dx_Usr_Ultmod); //add by coco 2012-07-18
        //            }
        //            scope.Complete();
        //            //returnTemp = IMaxAccount;
        //        }
        //    }
        //    catch (LsDAException ex)
        //    {
        //        throw new LsBLException(this, ex.Message, ex, true);
        //    }
        //    return Count;
        //}

        //public DataTable CalculateCreditAmortizacion(string creditNumber, double montoSolicitado, double pctTasaFija, int plazoPago, int periodo, double pctTasaInteres, double pctTasaIVA, double pctCAT, string periodEndDate)
        //{
        //    DataTable dtResult = null;
        //    const int BaseComercialDays = 360;
        //    int AmortizationYear = 1, PeriodDays = 0;
        //    double FixedPaymentNumber, FixedPaymentNumberRoundUp;
        //    DateTime PeriodEndDate, ProbableDeliveryDate, PeriodStartDate, FD, DD = DateTime.Now.Date;
        //    DateTime FirstPayDate = DateTime.Now.Date;

        //    try
        //    {
        //        //check period end date
        //        if (!string.IsNullOrEmpty(periodEndDate))
        //        {
        //            string[] format = { "yyyyMMdd", "yyyy/MM/dd", "yyyy-MM-dd" };
        //            PeriodEndDate = DateTime.ParseExact(periodEndDate, format, System.Threading.Thread.CurrentThread.CurrentCulture, System.Globalization.DateTimeStyles.None).Date;

        //            ProbableDeliveryDate = DateTime.Now.AddDays(25).Date;
        //            PeriodStartDate = PeriodEndDate.AddDays(1).Date;

        //            TimeSpan span = ProbableDeliveryDate - PeriodStartDate;
        //            //Init base parameters
        //            if (periodo == 1 /*Monthly Billing*/)
        //            {
        //                AmortizationYear = 12;
        //                PeriodDays = 30;

        //                if (span.TotalDays <= 20)
        //                {
        //                    FD = PeriodStartDate.AddDays(30).Date;
        //                }
        //                else
        //                {
        //                    FD = PeriodStartDate.AddDays(60).Date;
        //                }

        //                DD = FD.AddDays(11).Date.AddDays(15).Date;
        //            }
        //            else  /*Bimonthly Billing*/
        //            {
        //                AmortizationYear = 6;
        //                PeriodDays = 60;

        //                if (span.TotalDays <= 49)
        //                {
        //                    FD = PeriodStartDate.AddDays(60).Date;
        //                }
        //                else
        //                {
        //                    FD = PeriodStartDate.AddDays(120).Date;
        //                }

        //                DD = FD.AddDays(22).Date.AddDays(15).Date;
        //            }

        //            //calculate the first pay date
        //            if (System.Math.Abs(DD.Day - 16) <= System.Math.Abs(DD.Day - 28))
        //            {
        //                FirstPayDate = new DateTime(DD.Year, DD.Month, 16);
        //            }
        //            else
        //            {
        //                FirstPayDate = new DateTime(DD.Year, DD.Month, 28);
        //            }
        //        }
        //        //Calculate fixed payment value
        //        double p = System.Math.Pow(1 + (pctTasaFija / AmortizationYear), -plazoPago);

        //        FixedPaymentNumber = montoSolicitado / ((1 - p) / (pctTasaFija / AmortizationYear));//fixed payment value
        //        FixedPaymentNumberRoundUp = System.Math.Round(FixedPaymentNumber, 0);//fixed payment round up value
        //        if (FixedPaymentNumberRoundUp <= FixedPaymentNumber)
        //        {
        //            FixedPaymentNumberRoundUp += 1;
        //        }
        //        //Calculate the payment schedule table
        //        //prepare calculation result table schema
        //        DataTable dtTempAmortization = new DataTable();
        //        dtTempAmortization.Columns.Add("No_Credito", typeof(string));
        //        dtTempAmortization.Columns.Add("No_Pago", typeof(int));
        //        dtTempAmortization.Columns.Add("Dt_Fecha", typeof(DateTime));
        //        dtTempAmortization.Columns.Add("No_Dias_Periodo", typeof(int));
        //        dtTempAmortization.Columns.Add("Mt_Capital", typeof(double));
        //        dtTempAmortization.Columns.Add("Mt_Interes", typeof(double));
        //        dtTempAmortization.Columns.Add("Mt_IVA", typeof(double));
        //        dtTempAmortization.Columns.Add("Mt_Pago", typeof(double));
        //        dtTempAmortization.Columns.Add("Mt_Amortizacion", typeof(double));
        //        dtTempAmortization.Columns.Add("Mt_Saldo", typeof(double));
        //        dtTempAmortization.Columns.Add("Dt_Fecha_Credito_Amortización", typeof(DateTime));
        //        //calculation
        //        for (int i = 1; i <= plazoPago; i++)
        //        {
        //            List<double> Intereses = new List<double>();
        //            double TempInteres;

        //            DataRow Row = dtTempAmortization.NewRow();
        //            Row["No_Credito"] = creditNumber;
        //            Row["No_Pago"] = i;
        //            //Row["Dt_Fecha"] = DateTime.Now.Date.AddMonths(i - 1);
        //            Row["No_Dias_Periodo"] = PeriodDays;
        //            Row["Dt_Fecha_Credito_Amortización"] = DateTime.Now.Date;
        //            if (i == 1)
        //            {
        //                Row["Mt_Capital"] = string.Format("{0:0.00}", montoSolicitado);
        //                Row["Dt_Fecha"] = FirstPayDate;
        //            }
        //            else
        //            {
        //                Row["Mt_Capital"] = string.Format("{0:0.00}", dtTempAmortization.Rows[i - 2]["Mt_Saldo"]);
        //                if (periodo == 1)//monthly billing
        //                {
        //                    Row["Dt_Fecha"] = FirstPayDate.AddMonths(i - 1);
        //                }
        //                else//bimonthly billing
        //                {
        //                    Row["Dt_Fecha"] = FirstPayDate.AddMonths((i - 1) * 2);
        //                }
        //            }
        //            //calculate temp interest middle variable
        //            TempInteres = Convert.ToDouble(Row["Mt_Capital"].ToString()) * Convert.ToDouble(Row["No_Dias_Periodo"].ToString()) * pctTasaFija / BaseComercialDays;
        //            Intereses.Add(TempInteres);//maybe used later
        //            if (i != plazoPago)
        //            {
        //                Row["Mt_Amortizacion"] = string.Format("{0:0.00}", FixedPaymentNumber - TempInteres);
        //            }
        //            else
        //            {
        //                Row["Mt_Amortizacion"] = Row["Mt_Capital"];
        //            }
        //            Row["Mt_Interes"] = string.Format("{0:0.00}", TempInteres / (1 + pctTasaIVA));
        //            Row["Mt_IVA"] = string.Format("{0:0.00}", Convert.ToDouble(Row["Mt_Interes"].ToString()) * pctTasaIVA);
        //            if (i != plazoPago)
        //            {
        //                Row["Mt_Pago"] = string.Format("{0:0}", FixedPaymentNumberRoundUp);
        //            }
        //            else
        //            {
        //                Row["Mt_Pago"] = string.Format("{0:0}", FixedPaymentNumberRoundUp + (FixedPaymentNumber * plazoPago - FixedPaymentNumberRoundUp * plazoPago));
        //            }

        //            Row["Mt_Saldo"] = string.Format("{0:0.00}", Convert.ToDouble(Row["Mt_Capital"].ToString()) - Convert.ToDouble(Row["Mt_Amortizacion"].ToString()));
        //            //Fixing Interest value

        //            Row["Mt_Interes"] = (Convert.ToDouble(Row["Mt_Pago"]) - Convert.ToDouble(Row["Mt_Amortizacion"]) - Convert.ToDouble(Row["Mt_IVA"]));

        //            //Add row to table
        //            dtTempAmortization.Rows.Add(Row);
        //        }
        //        //copy data to result table
        //        dtResult = dtTempAmortization.Copy();
        //    }
        //    catch (Exception ex)
        //    {
        //        LsLog.LogToFile(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
        //        dtResult = null;
        //    }
        //    return dtResult;
        //}

    }
}
