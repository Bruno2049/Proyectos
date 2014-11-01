using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;
namespace eClockBase.Modelos
{
    public class Model_ENV_REPORTES
    {
        //Identificador de registro de programación de envio de reportes
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int ENV_REPORTE_ID { get; set; }
        //Contiene el usuario al que se le enviará el reporte
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int USUARIO_ID { get; set; }
        //RFU Contiene el email de la persona a la que se le enviará el reporte
        [Campo_String(false, false, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string ENV_REPORTE_EMAIL { get; set; }
        //Contiene el Formato del archivo en el que será enviado el reporte por default 0 (PDF)
        [Campo_Int(false, false, -1, -1, 0, Campo_IntAttribute.Tipo.TextBox)]
        public int FORMATO_REP_ID { get; set; }
        //Contiene el ID del reporte que será enviado
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int REPORTE_ID { get; set; }
        //Contiene la descripción de la regla
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string ENV_REPORTE_DESCRIPCION { get; set; }
        //Ultima Fecha y hora en la que se envio el reporte, apartir de esta fecha se sumaran los días que este configurado el envio de reporte
        [Campo_FechaHora(false, false, "2008-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime ENV_REPORTE_FECHAHORA { get; set; }
        //Contiene el campo ENV_REPORTE_FECHAHORA pero no será modificado por la programación solo por el usuario
        [Campo_FechaHora(false, false, "2008-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime ENV_REPORTE_FECHAHORAC { get; set; }
        //Contiene el campo ENV_REPORTE_FECHAHORA pero no será modificado por la programación solo por el usuario
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int ENV_REPORTE_C_DIAS { get; set; }
        //Indica la cantidad de dias anterior de la fecha de ejecución del reporte (ENV_REPORTE_FECHAHORAE) que servirá como fecha inicial del filtro, de manera predeterminada se usará ENV_REPORTE_C_DIAS o 0, (-1 cada quincena, -2 cada mes)
        [Campo_Int(false, false, -1, -1, 0, Campo_IntAttribute.Tipo.TextBox)]
        public int ENV_REPORTE_DIAS_INI { get; set; }
        //Indica la cantidad de dias anterior de la fecha de ejecución del reporte que servirá como fecha Final del filtro, (-1 cada quincena, -2 cada mes)
        [Campo_Int(false, false, -1, -1, 0, Campo_IntAttribute.Tipo.TextBox)]
        public int ENV_REPORTE_DIAS_FIN { get; set; }
        //Indica que esta regla se ejecutará solo una vez y despues se borrará
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool ENV_REPORTE_EUVEZ { get; set; }
        //Fecha y hora en la que se deberá enviar el mail.
        [Campo_FechaHora(false, false, "2008-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime ENV_REPORTE_FECHAHORAE { get; set; }
        //Indica que la tarea no se ejecutará por lo pronto ya que estará inactiva.
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool ENV_REPORTE_BORRADO { get; set; }

    }
}
