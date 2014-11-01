using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;

namespace eClockBase.Modelos
{
    public class Model_PERIODOS
    {
        //Identificador del periodo
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int PERIODO_ID { get; set; }
        //
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.ComboBox)]
        public int PERIODO_N_ID { get; set; }
        //
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.ComboBox)]
        public int EDO_PERIODO_ID { get; set; }
        //Fecha de inicio de periodo de nomina
        [Campo_FechaHora(false, false, "2008-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime PERIODO_NOM_INICIO { get; set; }
        //Fecha de Fin de periodo de nomina
        [Campo_FechaHora(false, false, "2008-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime PERIODO_NOM_FIN { get; set; }
        //Fecha de inicio de Asistencia en el periodo
        [Campo_FechaHora(false, false, "2008-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime PERIODO_ASIS_INICIO { get; set; }
        //Fecha de fin de asistencia en el periodo
        [Campo_FechaHora(false, false, "2008-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime PERIODO_ASIS_FIN { get; set; }
        //Año del periodo ej 2011
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int PERIODO_ANO { get; set; }
        //Numero de periodo
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int PERIODO_NO { get; set; }

    }
}
