using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;

namespace eClockBase.Modelos
{
    public class Model_DIAS_TRABAJO
    {
        //Fecha de todos los dias desde 1-01-2004 hasta 01-01-2014
        [Campo_FechaHora(true, true, "2008-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime CONFIG_USUARIO_ID { get; set; }

    }
}
