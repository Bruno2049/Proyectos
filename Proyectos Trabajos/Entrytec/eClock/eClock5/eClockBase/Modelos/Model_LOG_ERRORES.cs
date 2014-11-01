using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;

namespace eClockBase.Modelos
{
    public class Model_LOG_ERRORES
    {
        //Identificador unico de cada registro de error
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int LOG_ERROR_ID { get; set; }
        //Reservado para uso futuro, indicara de que tipo de error se trata por ahora siempre ira en cero
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int TIPO_ERROR_ID { get; set; }
        //Descripción del error
        [Campo_String(true, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string LOG_ERROR_DESCRIPCION { get; set; }
        //Fecha y hora en la que ocurrio el incidente
        [Campo_FechaHora(true, false, "2008-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.TextBox)]
        public DateTime LOR_ERROR_FECHAHORA { get; set; }
        //Identificador de la sesion
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int SESION_ID { get; set; }

    }
}
