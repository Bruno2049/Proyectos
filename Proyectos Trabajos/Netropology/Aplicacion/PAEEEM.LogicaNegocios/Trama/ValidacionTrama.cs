using System;
using System.Collections.Generic;
using System.Data;
using PAEEEM.AccesoDatos.Catalogos;
using PAEEEM.AccesoDatos.SolicitudCredito;
using PAEEEM.BussinessLayer;
using PAEEEM.DataAccessLayer;

namespace PAEEEM.LogicaNegocios.Trama
{
    public static class ValidacionTrama
    {
        public static string[] Tarifas = {"02","03","OM","HM"}; //NO SE CONTO LA 02 DEBIDO AQUE ESA TARIFA LA HIZO IMPRA SOLO  SE INCLUYO LAS NUEVAS QUE HIZO NETROPOLOGY

        // RSA 20110316 communication status
        /// <summary>
        /// Check communication status
        /// </summary>
        /// <param name="status">status</param>
        /// <returns>bool</returns>
        public static bool IsComStatusValid(string estatus)
        {
            if (string.IsNullOrEmpty(estatus))
                return false;

            bool bResult = estatus.StartsWith("A");

            return bResult;
        }

        /// <summary>
        /// Check user status
        /// </summary>
        /// <param name="estatus">status</param>
        /// <returns>bool</returns>
        public static bool IsUserStatusValid(string estatus)
        {
            if (string.IsNullOrEmpty(estatus))
                return false;

            bool bResult = estatus == "01";

            return bResult;
        }

        /// <summary>
        /// Validate Rate
        /// </summary>
        /// <param name="rate"></param>
        /// <returns></returns>
        public static Boolean ValidateRate(string rate, string idPrograma)
        {
            Boolean Flag = false;
            DataTable dtRate = CAT_PROGRAMADal.ClassInstance.get_Cat_Tarifa(idPrograma, rate);//Change by Jerry 2011/08/08
            if (dtRate != null && dtRate.Rows.Count > 0)
            {
                Flag = true;
            }
            return Flag;
        }


        /// <summary>
        /// RSA
        /// Is it long enough
        /// </summary>
        /// <param name="currentdebitstatus"></param>
        /// <param name="duedate"></param>
        /// <param name="rpu"></param>
        /// <returns></returns>
        public static bool IsServiceCodeLongEnough(string rpu)
        {
            bool bResult = true;
            if (rpu.Length != 12)
            {
                bResult = false;
            }
            return bResult;
        }


        /// <summary>
        /// Validate User Status
        /// </summary>
        /// <param name="UserStatus"></param>
        /// <returns></returns>
        public static Boolean ValidateUserStatus(string UserStatus, string idPrograma)
        {

            bool flag = false;
            //DataTable programDt = CAT_PROGRAMABLL.ClassInstance.Get_CAT_PROGRAMAbyPk(idPrograma);//Changed by Jerry 2011/08/08
            var programa = new CreCredito().ObtenPrograma(int.Parse(idPrograma));
            
            if (!string.IsNullOrEmpty(UserStatus))
            {
                //if (int.Parse(UserStatus) == int.Parse(programDt.Rows[0]["No_Estatus_CFE"].ToString()))
                if (int.Parse(UserStatus) == programa.No_Estatus_CFE)
                {
                    flag = true;
                }
            }
            return flag;
        }
        /// <summary>
        /// validate CurrentDebit
        /// </summary>
        /// <param name="currentBillStatus"></param>
        /// <param name="DueDate"></param>
        /// <returns></returns>
        public static Boolean ValidateNoDebit(string currentBillStatus, string DueDate)
        {
            Boolean Flag = true;
            DateTime dueDate;
            string[] dateFormat = { "yyyyMMdd", "yyyy/MM/dd", "yyyy-MM-dd", "yyyyMM" };
            try
            {
                dueDate = DateTime.ParseExact(DueDate, dateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None);
                if (currentBillStatus == "0" && dueDate < DateTime.Now.Date)
                {
                    Flag = false;
                }
                return Flag;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// Is in debts
        /// </summary>
        /// <param name="dics">Dictionary parameters</param>
        /// <returns>bool</returns>
        public static bool IsInDebts(Dictionary<string, string> dics)
        {
            bool bResult = false;

            // if empty or with a value bigger than 00 then is in debts
            if (string.IsNullOrEmpty(dics["DebitNumber"]) || dics["DebitNumber"].CompareTo("00") != 0)
                bResult = true;

            return bResult;
        }

        public static Boolean ValidateMinConsumptionDate(string temp)
        {
            //Boolean Flag = false;
            //string[] dateFormat = {"yyyyMMdd","yyyy/MM/dd","yyyy-MM-dd","yyyyMM"};
            //DateTime tempDate;
            //try
            //{
            //    tempDate = DateTime.ParseExact(temp, dateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None);
            //    if (DateTime.Now.Date.Year - tempDate.Year >= 1)
            //    {
            //        Flag = true;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception("La fecha de consumo mínimo regresada por CFE es nula", ex);
            //}
            //return Flag;
            return true;
        }
        
        public static string GetTarifa(string trama)
        {
            string tarifa = "";
            var info = new InformacionParseo().ObtienePorCondicion(p => p.ID_INFO == 4);// 4 PERTENECE A TARIFA

            if (info != null)           
                tarifa = trama.Substring(info.INICIAL -1 , info.LONGITUD);

            tarifa = tarifa.Substring(0, 1) == "7" ? "HM" : tarifa;
            tarifa = tarifa.Substring(0, 1) == "6" ? "OM" : tarifa;

            return tarifa;
        }

    }
}
