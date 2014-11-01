using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;

namespace eClockBase.Modelos.Asistencias
{
    public class Reporte_Asistencias
    {
        //Identificador unico de registro
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int PERSONA_DIARIO_ID { get; set; }

        //
        [Campo_FechaHora(false, false, "2006-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime PERSONA_DIARIO_FECHA { get; set; }

        //Fecha y hora en la que ocurrio el evento
        [Campo_FechaHora(false, false, "2006-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime ACCESO_E { get; set; }

        //Fecha y hora en la que ocurrio el evento
        [Campo_FechaHora(false, false, "2006-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime ACCESO_S { get; set; }

        //
        [Campo_FechaHora(false, false, "2006-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime ACCESO_CS { get; set; }

        //
        [Campo_FechaHora(false, false, "2006-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime ACCESO_CR { get; set; }

        //
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBoxMemo)]
        public string INCIDENCIA_NOMBRE { get; set; }

        //
        [Campo_FechaHora(false, false, "2006-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public string TURNO { get; set; }

        //
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int INCIDENCIA_ID { get; set; }

        //Contiene el tiempo total de trabajo del empleado en el dia
        [Campo_FechaHora(false, false, "2006-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime PERSONA_DIARIO_TT { get; set; }

        //Contiene el tiempo que permanecio dentro el empleado en el dia
        [Campo_FechaHora(false, false, "2006-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime PERSONA_DIARIO_TE { get; set; }

        //Contiene el tiempo de comida del empleado en el dia
        [Campo_FechaHora(false, false, "2006-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime PERSONA_DIARIO_TC { get; set; }

        //Contiene el tiempo en deuda del empleado en el dia (Tiempo de retardo)
        [Campo_FechaHora(false, false, "2006-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime PERSONA_DIARIO_TDE { get; set; }

        //Contiene el tiempo que deberá trabajar el empleado 
        [Campo_FechaHora(false, false, "2006-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime PERSONA_DIARIO_TES { get; set; }

        //
        [Campo_FechaHora(false, false, "2006-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime PERSONA_D_HE_SIS { get; set; }

        //
        [Campo_FechaHora(false, false, "2006-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime PERSONA_D_HE_CAL { get; set; }

        //
        [Campo_FechaHora(false, false, "2006-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime PERSONA_D_HE_APL { get; set; }

        //
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int TURNO_ID { get; set; }

        //
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int PERSONA_ID { get; set; }

        //
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int TIPO_INC_SIS_ID { get; set; }

        //
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int TIPO_INC_C_SIS_ID { get; set; }

        //
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int TURNO_DIA_ID { get; set; }

        //
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int PERSONA_D_HE_ID { get; set; }

        //
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBoxMemo)]
        public string ENTRADASALIDA { get; set; }

        //
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int SUSCRIPCION_ID { get; set; }

        //
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int PERSONA_LINK_ID { get; set; }

        //
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBoxMemo)]
        public string PERSONA_NOMBRE { get; set; }

        //
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBoxMemo)]
        public string AGRUPACION_NOMBRE { get; set; }

        //
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBoxMemo)]
        public string INCIDENCIA_ABR { get; set; }

        //
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBoxMemo)]
        public string TIPO_INC_C_SIS_NOMBRE { get; set; }

        //
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public decimal PERSONA_D_HE_SIMPLE { get; set; }

        //
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public decimal PERSONA_D_HE_DOBLE { get; set; }

        //
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public decimal PERSONA_D_HE_TRIPLE { get; set; }

        //
        [Campo_FechaHora(false, false, "2006-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public string PERSONA_D_HE_COMEN { get; set; }

        //
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int TIPO_INCIDENCIA_IDHE { get; set; }

        //
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int TIPO_INCIDENCIA_ID { get; set; }

        //
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBoxMemo)]
        public string INCIDENCIA_COMENTARIO { get; set; }

        //
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int TURNO_COLOR { get; set; }

        //
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int INCIDENCIA_COLOR { get; set; }

        //
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool TURNO_DIA_PHEX { get; set; }

        //
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool TURNO_DIA_HAYCOMIDA { get; set; }

    }
}

