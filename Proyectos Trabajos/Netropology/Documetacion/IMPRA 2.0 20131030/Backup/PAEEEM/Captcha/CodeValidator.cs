using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net.Sockets;

using PAEEEM.BussinessLayer;
using PAEEEM.Entities;
using PAEEEM.Helpers;
using PAEEEM.DataAccessLayer;

namespace PAEEEM.Captcha
{
    public class CodeValidator
    {
        public string EstadoName { get; set; }
        
        /// <summary>
        /// Validate the input service code, and check if it is valid
        /// </summary>
        /// <param name="servicecode">Service Code</param>
        /// <returns>bool</returns>

     
        
        
        public Boolean ValidateServiceCode(string servicecode, out string ErrorCode, ref string StatusFlag)
        {
            Boolean ServiceCodeValidate = false;
            // assume it should be a new code unless StatusFlag starts with E of "Exists"
            Boolean isNew = !StatusFlag.StartsWith("E");

            StatusFlag = "";
            ErrorCode = "";

            Dictionary<string, string> Dics; // = SICOMHelper.GetAttributes(servicecode);

            if (isNew && K_CREDITODal.ClassInstance.IsServiceCodeExist(servicecode))
            {
                ErrorCode = HttpContext.GetGlobalResourceObject("DefaultResource", "TheServiceCodehaveUsed") as string;
            }    
            else if (IsServiceCodeLongEnough(servicecode) /*SICOMHelper.tempRpus.Keys.Contains<string>(servicecode)*/)
            {
                Dics = SICOMHelper.GetAttributes(servicecode);
                //Added by Jerry 2011/08/12
                //this.RGN_CFE = Dics["CN"];
                //this.ZoneType = Dics["ZoneType"];

                //End
                // RSA 20110316 communication status
                if (!IsComStatusValid(Dics["ComStatus"]))
                {
                    ErrorCode = (string)HttpContext.GetGlobalResourceObject("DefaultResource"
                        , "SICOM_Communication_Status_" + Dics["ComStatus"]);
                        StatusFlag = "W";
                }
                else if (IsUserStatusValid(Dics["UserStatus"]))  //only user status "0" and "01" are valid
                {
                    if (string.IsNullOrEmpty(Dics["HighAvgConsumption"]))
                    {
                        ErrorCode = "El usuario no presenta consumo.";
                        StatusFlag = "B";
                    }
                    else if (ValidateRate(Dics["Rate"]))
                    {
                        if (ValidateUserStatus(Dics["UserStatus"]))
                        {
                            if (ValidateNoDebit(Dics["CurrentBillingStatus"], Dics["DueDate"]) && !IsInDebts(Dics))
                            {
                                if (ValidateMinConsumptionDate(Dics["MinConsumptionDate"]))
                                {
                                    ServiceCodeValidate = true;
                                   
                                }
                                else
                                {
                                    ErrorCode = "La fecha recibida del servidor de CFE es inválida";
                                    StatusFlag = "B";
                                }
                            }
                            else
                            {
                                ErrorCode = "El usuario presenta adeudo con CFE";
                                StatusFlag = "B";
                            }
                        }
                        else
                        {
                            ErrorCode = "Usuario Inactivo";
                            StatusFlag = "B";
                        }
                    }
                    else
                    {
                        ErrorCode = "La tarifa del usuario no aplica para el programa";
                        StatusFlag = "A";
                    }
                }
                else
                {
                    ErrorCode = "El estatus del usuario es inválido" + Dics["UserStatus"];//"The user status is invalid.";
                }

            }
            else
            {
                ErrorCode = "El número de servicio es incorrecto";
            }

            return ServiceCodeValidate;
        }
        /// <summary>
        /// RSA
        /// Is it long enough
        /// </summary>
        /// <param name="currentdebitstatus"></param>
        /// <param name="duedate"></param>
        /// <returns></returns>
        private bool IsServiceCodeLongEnough(string rpu)
        {
            bool bResult = true;
            if (rpu.Length != 12)
            {
                bResult = false;
            }
            return bResult;
        }
        // RSA 20110316 communication status
        /// <summary>
        /// Check communication status
        /// </summary>
        /// <param name="status">status</param>
        /// <returns>bool</returns>
        private bool IsComStatusValid(string status)
        {
            bool bResult = false;

            if (status.StartsWith("A"))
            {
                bResult = true;
            }
            return bResult;
        }
        /// <summary>
        /// Check user status
        /// </summary>
        /// <param name="status">status</param>
        /// <returns>bool</returns>
        private bool IsUserStatusValid(string status)
        {
            bool bResult = false;

            if (status == "01")
            {
                bResult = true;
            }
            return bResult;
        }

        /// <summary>
        /// Validate Rate
        /// </summary>
        /// <param name="Rate"></param>
        /// <returns></returns>
        private Boolean ValidateRate(string Rate)
        {
            Boolean Flag = false;
            DataTable dtRate = CAT_PROGRAMADal.ClassInstance.get_Cat_Tarifa(Global.PROGRAM.ToString(), Rate);//Change by Jerry 2011/08/08
            if (dtRate != null && dtRate.Rows.Count > 0)
            {
                Flag = true;
            }
            return Flag;
        }
        /// <summary>
        /// Validate User Status
        /// </summary>
        /// <param name="UserStatus"></param>
        /// <returns></returns>
        private Boolean ValidateUserStatus(string UserStatus)
        {
            
            bool Flag = false;
            DataTable ProgramDt = CAT_PROGRAMABLL.ClassInstance.Get_CAT_PROGRAMAbyPk(Global.PROGRAM.ToString());//Changed by Jerry 2011/08/08
            if (!string.IsNullOrEmpty(UserStatus))
            {
                if (int.Parse(UserStatus) == int.Parse(ProgramDt.Rows[0]["No_Estatus_CFE"].ToString()))
                {
                    Flag = true;
                }
            }
            return Flag;
        }
        /// <summary>
        /// validate CurrentDebit
        /// </summary>
        /// <param name="CurrentBillStatus"></param>
        /// <param name="DueDate"></param>
        /// <returns></returns>
        private Boolean ValidateNoDebit(string CurrentBillStatus, string DueDate)
        {
            Boolean Flag = true;
            DateTime dueDate;
            string[] dateFormat = { "yyyyMMdd", "yyyy/MM/dd", "yyyy-MM-dd", "yyyyMM" };
            try
            {
                dueDate = DateTime.ParseExact(DueDate, dateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None);
                if (CurrentBillStatus == "0" && dueDate < DateTime.Now.Date)
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
        private bool IsInDebts(Dictionary<string, string> dics)
        {
            bool bResult = false;

            // if empty or with a value bigger than 00 then is in debts
            if (string.IsNullOrEmpty(dics["DebitNumber"]) || dics["DebitNumber"].CompareTo("00") != 0)
                bResult = true;

            return bResult;
        }

        private Boolean ValidateMinConsumptionDate(string temp)
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

    }
}
