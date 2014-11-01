using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace eClockMobile.Models
{
    public class SuscripcionesModels
    {

    }

    public class EdicionSuscripcionesModels
    {
        //Identificador de la suscripcion
        public int SUSCRIPCION_ID { get; set; }
        //Identificador único del precio
        public int SUSCRIP_PRECIO_ID { get; set; }
        //Nombre de la suscripcion regularmente no contrato
        public string SUSCRIPCION_NOMBRE { get; set; }
        //Indica que la suscripción se ha borrado, por lo tanto nadie de dicha suscripción deberá tener acceso al sistema
        public bool SUSCRIPCION_BORRADO { get; set; }
        //Contiene la razon social del propietario de la suscripcion
        public string SUSCRIPCION_RAZON { get; set; }
        //RFC
        public string SUSCRIPCION_RFC { get; set; }
        //Campo uno de Dirección
        public string SUSCRIPCION_DIRECCION1 { get; set; }
        //Campo dos de Dirección
        public string SUSCRIPCION_DIRECCION2 { get; set; }
        //Indica que la suscripción se ha borrado, por lo tanto nadie de dicha suscripción deberá tener acceso al sistema
        public string SUSCRIPCION_CIUDAD { get; set; }
        //
        public string SUSCRIPCION_ESTADO { get; set; }
        //
        public string SUSCRIPCION_PAIS { get; set; }
        //Indica si el cliente requiere Factura
        public bool SUSCRIPCION_FACTURAR { get; set; }
        //Contiene la fecha de contratación
        [DataType(DataType.Date)]
        public DateTime SUSCRIPCION_CONTRATACION { get; set; }
        //Contiene la cantidad de Empleados que se permitiran en esta suscripcion
        public int SUSCRIPCION_EMPLEADOS { get; set; }
        //Contiene la cantidad de Terminales que se permitiran en esta suscripcion
        public int SUSCRIPCION_TERMINALES { get; set; }
        //Contiene la cantidad de Usuarios que se permitiran en esta suscripcion
        public int SUSCRIPCION_USUARIOS { get; set; }
        //Contiene la cantidad de Alumnos que se permitiran en esta suscripcion
        public int SUSCRIPCION_ALUMNOS { get; set; }
        //Contiene la cantidad de Visitantes que se permitiran en esta suscripcion
        public int SUSCRIPCION_VISITANTES { get; set; }
        //Indica si permitirá empleados, terminales, etc adicionales a los autorizados
        public bool SUSCRIPCION_ADICIONALES { get; set; }
        //Contiene datos adicionales a validar
        public string SUSCRIPCION_OTROS { get; set; }
        //Mensualidad de pago por la suscripcion
        public int SUSCRIPCION_MENSUAL { get; set; }
        //Fecha en la que finalizará el contrato
        [DataType(DataType.Date)]
        public DateTime SUSCRIPCION_FINAL { get; set; }
        //Indica que la suscripción se ha borrado, por lo tanto nadie de dicha suscripción deberá tener acceso al sistema
        public int EDO_SUSCRIPCION_ID { get; set; }
    }
}