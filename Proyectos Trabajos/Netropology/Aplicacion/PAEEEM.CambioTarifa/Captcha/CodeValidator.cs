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
        public const int PROGRAM = 1;
        public string EstadoName { get; set; }

        public ParseoTrama ParseoTrama { get; set; }
                          

        //NETROPOLOGY - DPF
        public Boolean ValidateServiceCodeEdit(string servicecode, out string ErrorCode, ref string StatusFlag)
        {
            Boolean ServiceCodeValidate = false;
            // assume it should be a new code unless StatusFlag starts with E of "Exists"
            Boolean isNew = !StatusFlag.StartsWith("E");

            try
            {
                if (ValidacionTrama.IsServiceCodeLongEnough(servicecode))
                {
                    var trama = OpTrama.GetTrama(servicecode, out ErrorCode);

                    /*VALIDACION DE TARIFAS*/
                    //if (ValidacionTrama.Tarifas.Contains(ValidacionTrama.GetTarifa(trama)))
                    //{
                    var parseoTrama = new ParseoTrama(trama);
                    ServiceCodeValidate = ProcesoValidacion(out StatusFlag, out ErrorCode, parseoTrama.ComplexParseo);
                    ParseoTrama = parseoTrama;
                    //}
                    //else
                    //{
                    //    ErrorCode = "La tarifa del usuario no aplica para el programa";
                    //}
                }
                else
                {
                    ErrorCode = "El número de servicio es incorrecto";
                }
            }
            catch (Exception ex)
            {
                ServiceCodeValidate = false;
                ErrorCode = ex.Message;
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
                //tarifa = tarifa.Substring(0, 1) == "7" ? "HM" : tarifa;
                //tarifa = tarifa.Substring(0, 1) == "6" ? "OM" : tarifa;
                //var noAdeudos = trama.InformacionGeneral.Conceptos.FirstOrDefault(c => c.Id == 7).ValorEntero;
                //var consumoPromedio = trama.InfoConsumo.Detalle != null ? trama.InfoConsumo.Detalle.Promedio : 0.0M;

                if (!ValidacionTrama.IsComStatusValid(comnSicom))//VALIDACION DE ESTATUS DE COMUNICACION SICOM ESTATUS VALIDO = A4
                {
                    errorCode = "Estatus de Comunicación SICOM Inválido: " + comnSicom; //(string)HttpContext.GetGlobalResourceObject("DefaultResource"
                    //    , "SICOM_Communication_Status_" + comnSicom);
                    estatusFlag = "W";
                }
                else if (ValidacionTrama.IsUserStatusValid(estusUsuario))//SOLO LOS USUARIOS QUE TENGAN ESTATUS '0' Y '01'                
                {
                    if (ValidacionTrama.ValidateUserStatus(estusUsuario, PROGRAM.ToString(CultureInfo.InvariantCulture)))
                    {
                        ServiceCodeValidate = true;
                    }
                    else
                    {
                        errorCode = "Usuario Inactivo";
                        estatusFlag = "B";
                    }

                    //if (ValidacionTrama.ValidateRate(tarifa, PROGRAM.ToString(CultureInfo.InvariantCulture)))//
                    //{
                    //    if (ValidacionTrama.ValidateUserStatus(estusUsuario, PROGRAM.ToString(CultureInfo.InvariantCulture)))
                    //    {
                    //        ServiceCodeValidate = true;
                    //    }
                    //    else
                    //    {
                    //        errorCode = "Usuario Inactivo";
                    //        estatusFlag = "B";
                    //    }
                    //}
                    //else
                    //{
                    //    errorCode = "La tarifa del usuario no aplica para el programa";
                    //    estatusFlag = "A";
                    //}
                }
                else
                {
                    errorCode = "El estatus del usuario es inválido" + estusUsuario;//"The user status is invalid.";
                }
            }
            catch (Exception ex)
            {
                errorCode = string.Format("Ocurrio un error en la aplicacion: {0}", ex.Message);
            }

            return ServiceCodeValidate;
        }

        
        

    }
}
