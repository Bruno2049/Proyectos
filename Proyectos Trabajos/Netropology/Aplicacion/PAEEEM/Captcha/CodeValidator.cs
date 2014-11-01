using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using PAEEEM.Entidades.Tarifas;
using PAEEEM.Entidades.Trama;
using PAEEEM.Helpers;
using PAEEEM.DataAccessLayer;
using PAEEEM.LogicaNegocios.Tarifas;
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
        /// <param name="StatusFlag"></param>
        /// <param name="causa"></param>
        /// <param name="ErrorCode"></param>
        /// <returns>bool</returns>                     
        public Boolean ValidateServiceCode(string servicecode, out string ErrorCode, ref string StatusFlag, out  string[] causa)
        {
            Boolean ServiceCodeValidate = false;
            // assume it should be a new code unless StatusFlag starts with E of "Exists"
            Boolean isNew = !StatusFlag.StartsWith("E");
            
            Dictionary<string, string> Dics; // = SICOMHelper.GetAttributes(servicecode);
            causa = new[] {"", ""};

            //if (isNew && K_CREDITODal.ClassInstance.IsServiceCode_K_Datos_Pyme(servicecode))
            //{
            //    ErrorCode = HttpContext.GetGlobalResourceObject("DefaultResource", "TheServiceCodeNotPyME") as string;
            //}
            if (isNew && K_CREDITODal.ClassInstance.IsServiceCodeExist(servicecode))
            {
                ErrorCode = HttpContext.GetGlobalResourceObject("DefaultResource", "TheServiceCodehaveUsed") as string;
                causa[0] = "1";
            }    
            else if (ValidacionTrama.IsServiceCodeLongEnough(servicecode)) /*SICOMHelper.tempRpus.Keys.Contains<string>(servicecode)*/
            {
                
                var trama = OpTrama.GetTrama(servicecode,out ErrorCode);

                /*VALIDACION DE TARIFAS*/
                if (ValidacionTrama.Tarifas.Contains(ValidacionTrama.GetTarifa(trama)))
                {
                    var parseoTrama = new ParseoTrama(trama);                                   
                    ServiceCodeValidate = ProcesoValidacion(out StatusFlag, out ErrorCode, parseoTrama.ComplexParseo,out causa);
                    ParseoTrama = parseoTrama;
                }               
                else
                {
                    Dics = SICOMHelper.GetAttributes(servicecode);
                    ServiceCodeValidate = ProcesoValidacionT02(out StatusFlag, out ErrorCode, Dics,out causa);
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
                string[] causa;
                if (ValidacionTrama.Tarifas.Contains(ValidacionTrama.GetTarifa(trama)))
                {
                    var parseoTrama = new ParseoTrama(trama);
                    ServiceCodeValidate = ProcesoValidacion(out StatusFlag, out ErrorCode, parseoTrama.ComplexParseo,out causa);
                    ParseoTrama = parseoTrama;
                }
                else
                {
                    Dics = SICOMHelper.GetAttributes(servicecode);
                    ServiceCodeValidate = ProcesoValidacionT02(out StatusFlag, out ErrorCode, Dics,out causa);
                }          
            }
            else
            {
                ErrorCode = "El número de servicio es incorrecto";
            }

            return ServiceCodeValidate;
        }
        
        ///NETROPOLOGY
        public bool ProcesoValidacion(out string estatusFlag, out string errorCode, CompParseo trama, out string[] causa)
        {
            var ServiceCodeValidate = false;
            estatusFlag = "";
            errorCode = "";
            causa = new[] { "", "" };

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
                    causa[0] = "2";
                    causa[1] = comnSicom;
                }
                else if (ValidacionTrama.IsUserStatusValid(estusUsuario))//SOLO LOS USUARIOS QUE TENGAN ESTATUS '0' Y '01'                
                {
                    if (consumoPromedio <= 0)//DEBE DE CONTENER UN CONSUMO
                    {
                        errorCode = "El usuario no presenta consumo.";
                        estatusFlag = "B";
                        causa[0] = "8";
                        causa[1] = "El usuario no presenta consumo.";
                    }
                    else if (ValidacionTrama.ValidateRate(tarifa, Global.PROGRAM.ToString(CultureInfo.InvariantCulture)))//
                    {
                        if (ValidacionTrama.ValidateUserStatus(estusUsuario, Global.PROGRAM.ToString(CultureInfo.InvariantCulture)))
                        {
                            if (noAdeudos == 0)//VALIDA QUE EL USUARIO NO PRESENTE ADEUDOS
                            {
                                ServiceCodeValidate = true;
                            }
                            else
                            {
                                errorCode = "El usuario presenta adeudo con CFE";
                                estatusFlag = "B";
                                causa[0] = "3";
                            }
                        }
                        else
                        {
                            errorCode = "Usuario Inactivo";
                            estatusFlag = "B";
                            causa[0] = "4";
                            causa[1] = estusUsuario;
                        }
                    }
                    else
                    {
                        errorCode = "La tarifa del usuario no aplica para el programa";
                        estatusFlag = "A";
                        causa[0] = "5";
                        causa[1] = tarifa;
                    }
                }
                else
                {
                    errorCode = "El estatus del usuario es inválido" + estusUsuario;//"The user status is invalid.";
                    causa[0] = "4";
                    causa[1] = estusUsuario;
                }
            }
            catch (Exception ex)
            {
                errorCode =  string.Format("Ocurrió un error en la aplicacion: {0}",ex.Message);
            }
            

            return ServiceCodeValidate;
        }

        /*
         * ESTA VALIDACION NO CAMBIO ESTA SE EMPLEARA SOLO PARA TARIFA 02
         * ESTO DEBIDO AQUE EL NUEVO DESARROLO REALIZADO POR NETROPOLOGY CONTEMPLO T03, THM Y TOM
         */

        public bool ProcesoValidacionT02(out string statusFlag, out string errorCode, Dictionary<string, string> dics,
            out string[] causa)
        {
            var serviceCodeValidate = false;
            statusFlag = "";
            errorCode = "";
            causa = new[] {"",""};

            if (!ValidacionTrama.IsComStatusValid(dics["ComStatus"]))
                //VALIDACION DE ESTATUS DE COMUNICACION SICOM ESTATUS VALIDO = A4
            {
                errorCode = (string) HttpContext.GetGlobalResourceObject("DefaultResource"
                    , "SICOM_Communication_Status_" + dics["ComStatus"]);
                statusFlag = "W";
                causa[0] = "4";
                causa[1] = dics["ComStatus"];
            }
            else if (ValidacionTrama.IsUserStatusValid(dics["UserStatus"]))
                //SOLO LOS USUARIOS QUE TENGAN ESTATUS '0' Y '01'                
            {
                if (string.IsNullOrEmpty(dics["HighAvgConsumption"])) //DEBE DE CONTENER UN CONSUMO
                {
                    errorCode = "El usuario no presenta consumo.";
                    statusFlag = "B";
                    causa[0] = "8";
                    causa[1] = "El usuario no presenta consumo.";
                }
                else if (ValidacionTrama.ValidateRate(dics["Rate"],
                    Global.PROGRAM.ToString(CultureInfo.InvariantCulture))) //
                {
                    if (ValidacionTrama.ValidateUserStatus(dics["UserStatus"],
                        Global.PROGRAM.ToString(CultureInfo.InvariantCulture)))
                    {
                        if (ValidacionTrama.ValidateNoDebit(dics["CurrentBillingStatus"], dics["DueDate"]) &&
                            !ValidacionTrama.IsInDebts(dics))
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
                            causa[0] = "3";
                        }
                    }
                    else
                    {
                        errorCode = "Usuario Inactivo";
                        statusFlag = "B";
                        causa[0] = "4";
                        causa[1] = dics["UserStatus"];
                    }
                }
                else
                {
                    errorCode = "La tarifa del usuario no aplica para el programa";
                    statusFlag = "A";
                    causa[0] = "5";
                    causa[1] = dics["Rate"];
                }
            }
            else
            {
                errorCode = "El estatus del usuario es inválido" + dics["UserStatus"];
                    //"The user status is invalid.";
                causa[0] = "4";
                causa[1] = dics["UserStatus"];
            }

            return serviceCodeValidate;
        }



        public bool ValidacionTarifa(CompParseo parseo, out string errorCode, out string[] causa)
        {
            CompTarifa infoTarifa;
            causa = new[] { "",""};
            var tipoTarifa = parseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 4).Dato;
            tipoTarifa = tipoTarifa.Substring(0, 1) == "7" ? "HM" : tipoTarifa;
            tipoTarifa = tipoTarifa.Substring(0, 1) == "6" ? "OM" : tipoTarifa;

            switch (tipoTarifa)
            {
                case "02":
                    var t02 = new AlgoritmoTarifa02(parseo);
                    infoTarifa = t02.T02;
                    break;
                case "03":
                    var t03 = new AlgoritmoTarifa03(parseo);
                    infoTarifa = t03.T03;
                    break;
                case "HM":
                    var tHm = new AlgoritmoTarifaHM(parseo);
                    infoTarifa = tHm.Thm;
                    break;
                case "OM":
                    var tOm = new AlgoritmoTarifaOM(parseo);
                    infoTarifa = tOm.Tom;
                    break;
                default:
                    infoTarifa = null;
                    break;
            }

            if (infoTarifa != null)
            {
                if (!infoTarifa.AnioFactValido)
                {
                    errorCode = "Usuario no cumple con año de Facturación";
                    
                    causa[0] = "6";
                    causa[1] = "";
                    return false;
                }
                if (!infoTarifa.PeriodosValidos)
                {
                    errorCode = "Usuario no cumple con el número de periodos validos";
                    causa[0] = "7";
                    causa[1] = Convert.ToString(infoTarifa.Periodos);
                    return false;
                }
                errorCode = "";
                return true;
            }
            errorCode = "Ocurrió un problema al revisar los datos de la Trama";
            return false;
        }
        

    }
}
