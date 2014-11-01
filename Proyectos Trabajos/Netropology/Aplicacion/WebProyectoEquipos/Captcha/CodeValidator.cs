using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using PAEEEM.Entidades.Trama;
using PAEEEM.Helpers;
using PAEEEM.DataAccessLayer;
using PAEEEM.LogicaNegocios.Trama;

namespace PAEEEM.Captcha
{
    public class CodeValidator
    {
        public string EstadoName { get; set; }


        public ParseoTrama ParseoTrama { get; set; }
        
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
            
            Dictionary<string, string> Dics; // = SICOMHelper.GetAttributes(servicecode);

            //if (isNew && K_CREDITODal.ClassInstance.IsServiceCode_K_Datos_Pyme(servicecode))
            //{
            //    ErrorCode = HttpContext.GetGlobalResourceObject("DefaultResource", "TheServiceCodeNotPyME") as string;
            //}
            if (isNew && K_CREDITODal.ClassInstance.IsServiceCodeExist(servicecode))
            {
                ErrorCode = HttpContext.GetGlobalResourceObject("DefaultResource", "TheServiceCodehaveUsed") as string;
            }    
            else if (ValidacionTrama.IsServiceCodeLongEnough(servicecode)) /*SICOMHelper.tempRpus.Keys.Contains<string>(servicecode)*/
            {
                var trama = OpTrama.GetTrama(servicecode, out ErrorCode);

                /*VALIDACION DE TARIFAS*/
                if (ValidacionTrama.Tarifas.Contains(ValidacionTrama.GetTarifa(trama)))
                {
                    var parseoTrama = new ParseoTrama(trama);                                   
                    ServiceCodeValidate = ProcesoValidacion(out StatusFlag, out ErrorCode, parseoTrama.ComplexParseo);
                    ParseoTrama = parseoTrama;
                }               
                else
                {
                    Dics = SICOMHelper.GetAttributes(servicecode);
                    ServiceCodeValidate = ProcesoValidacionT02(out StatusFlag, out ErrorCode, Dics);
                }
                
                //Added by Jerry 2011/08/12
                //this.RGN_CFE = Dics["CN"];
                //this.ZoneType = Dics["ZoneType"];

                //End
                // RSA 20110316 communication status                
            }
            else
            {
                ErrorCode = "El número de servicio es incorrecto";
            }

            
            return ServiceCodeValidate;
        }

        //NETROPOLOGY - DPF
        public Boolean ValidateServiceCodeEdit(string servicecode, out string ErrorCode, ref string StatusFlag)
        {
            Boolean ServiceCodeValidate = false;
            // assume it should be a new code unless StatusFlag starts with E of "Exists"
            Boolean isNew = !StatusFlag.StartsWith("E");

            Dictionary<string, string> Dics; // = SICOMHelper.GetAttributes(servicecode);

            if (ValidacionTrama.IsServiceCodeLongEnough(servicecode)) /*SICOMHelper.tempRpus.Keys.Contains<string>(servicecode)*/
            {
                var trama = OpTrama.GetTrama(servicecode, out ErrorCode);

                /*VALIDACION DE TARIFAS*/
                if (ValidacionTrama.Tarifas.Contains(ValidacionTrama.GetTarifa(trama)))
                {
                    var parseoTrama = new ParseoTrama(trama);
                    ServiceCodeValidate = ProcesoValidacion(out StatusFlag, out ErrorCode, parseoTrama.ComplexParseo);
                    ParseoTrama = parseoTrama;
                }
                else
                {
                    Dics = SICOMHelper.GetAttributes(servicecode);
                    ServiceCodeValidate = ProcesoValidacionT02(out StatusFlag, out ErrorCode, Dics);
                }          
            }
            else
            {
                ErrorCode = "El número de servicio es incorrecto";
            }

            return ServiceCodeValidate;
        }
        
        ///NETROPOLOGY
        public bool ProcesoValidacion(out string estatusFlag, out string errorCode, CompParseo trama)
        {
            var ServiceCodeValidate = false;
            estatusFlag = "";
            errorCode = "";

            try
            {
                var comnSicom = trama.InformacionGeneral.Conceptos.FirstOrDefault(c => c.Id == 164).Dato;
                var estusUsuario = trama.InformacionGeneral.Conceptos.FirstOrDefault(c => c.Id == 5).Dato;
                var tarifa = trama.InformacionGeneral.Conceptos.FirstOrDefault(c => c.Id == 4).Dato;
                tarifa = tarifa.Substring(0, 1) == "7" ? "HM" : tarifa;
                tarifa = tarifa.Substring(0, 1) == "6" ? "OM" : tarifa;
                var noAdeudos = trama.InformacionGeneral.Conceptos.FirstOrDefault(c => c.Id == 7).ValorEntero;
                var consumoPromedio = trama.InfoConsumo.Detalle != null ? trama.InfoConsumo.Detalle.Promedio : 0.0M;

                if (!ValidacionTrama.IsComStatusValid(comnSicom))//VALIDACION DE ESTATUS DE COMUNICACION SICOM ESTATUS VALIDO = A4
                {
                    errorCode = (string)HttpContext.GetGlobalResourceObject("DefaultResource"
                        , "SICOM_Communication_Status_" + comnSicom);
                    estatusFlag = "W";
                }
                else if (ValidacionTrama.IsUserStatusValid(estusUsuario))//SOLO LOS USUARIOS QUE TENGAN ESTATUS '0' Y '01'                
                {
                    if (consumoPromedio <= 0)//DEBE DE CONTENER UN CONSUMO
                    {
                        errorCode = "El usuario no presenta consumo.";
                        estatusFlag = "B";
                    }
                    else if (ValidacionTrama.ValidateRate(tarifa, Global.PROGRAM.ToString(CultureInfo.InvariantCulture)))//
                    {
                        if (ValidacionTrama.ValidateUserStatus(estusUsuario, Global.PROGRAM.ToString(CultureInfo.InvariantCulture)))
                        {
                            if (noAdeudos == 0)//VALIDA QUE EL USUARIO NO PRESENTE ADEUDOS
                            {
                                ServiceCodeValidate = true;
                                //if (VALIDACION_TRAMA.ValidateMinConsumptionDate((DateTime)fechaMinConsumo))//VALIDACION DE FECHA DEL MINIMO CONSUMO
                                //{
                                //    ServiceCodeValidate = true;
                                //}
                                //else
                                //{
                                //    errorCode = "La fecha recibida del servidor de CFE es inválida";
                                //    estatusFlag = "B";
                                //}
                            }
                            else
                            {
                                errorCode = "El usuario presenta adeudo con CFE";
                                estatusFlag = "B";
                            }
                        }
                        else
                        {
                            errorCode = "Usuario Inactivo";
                            estatusFlag = "B";
                        }
                    }
                    else
                    {
                        errorCode = "La tarifa del usuario no aplica para el programa";
                        estatusFlag = "A";
                    }
                }
                else
                {
                    errorCode = "El estatus del usuario es inválido" + estusUsuario;//"The user status is invalid.";
                }
            }
            catch (Exception ex)
            {
                errorCode =  string.Format("Ocurrio un error en la aplicacion: {0}",ex.Message);
            }
            

            return ServiceCodeValidate;
        }

        /*
         * ESTA VALIDACION NO CAMBIO ESTA SE EMPLEARA SOLO PARA TARIFA 02
         * ESTO DEBIDO AQUE EL NUEVO DESARROLO REALIZADO POR NETROPOLOGY CONTEMPLO T03, THM Y TOM
         */
        public bool ProcesoValidacionT02(out string statusFlag, out string errorCode, Dictionary<string,string>dics)
        {
            var serviceCodeValidate = false;
            statusFlag = "";
            errorCode = "";            

            if (!ValidacionTrama.IsComStatusValid(dics["ComStatus"]))//VALIDACION DE ESTATUS DE COMUNICACION SICOM ESTATUS VALIDO = A4
            {
                errorCode = (string)HttpContext.GetGlobalResourceObject("DefaultResource"
                    , "SICOM_Communication_Status_" + dics["ComStatus"]);
                statusFlag = "W";
            }
            else if (ValidacionTrama.IsUserStatusValid(dics["UserStatus"]))//SOLO LOS USUARIOS QUE TENGAN ESTATUS '0' Y '01'                
            {
                if (string.IsNullOrEmpty(dics["HighAvgConsumption"]))//DEBE DE CONTENER UN CONSUMO
                {
                    errorCode = "El usuario no presenta consumo.";
                    statusFlag = "B";
                }
                else if (ValidacionTrama.ValidateRate(dics["Rate"],Global.PROGRAM.ToString(CultureInfo.InvariantCulture)))//
                {
                    if (ValidacionTrama.ValidateUserStatus(dics["UserStatus"],Global.PROGRAM.ToString(CultureInfo.InvariantCulture)))
                    {
                        if (ValidacionTrama.ValidateNoDebit(dics["CurrentBillingStatus"], dics["DueDate"]) && !ValidacionTrama.IsInDebts(dics))
                        {
                            if (ValidacionTrama.ValidateMinConsumptionDate(dics["MinConsumptionDate"]))
                            {
                                serviceCodeValidate = true;
                            }
                            else
                            {
                                errorCode = "La fecha recibida del servidor de CFE es inválida";
                                statusFlag = "B";
                            }
                        }
                        else
                        {
                            errorCode = "El usuario presenta adeudo con CFE";
                            statusFlag = "B";
                        }
                    }
                    else
                    {
                        errorCode = "Usuario Inactivo";
                        statusFlag = "B";
                    }
                }
                else
                {
                    errorCode = "La tarifa del usuario no aplica para el programa";
                    statusFlag = "A";
                }
            }
            else
            {
                errorCode = "El estatus del usuario es inválido" + dics["UserStatus"];//"The user status is invalid.";
            }
            
            return serviceCodeValidate;
        }



        

    }
}
