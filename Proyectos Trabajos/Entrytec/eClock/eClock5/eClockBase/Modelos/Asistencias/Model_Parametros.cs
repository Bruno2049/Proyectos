using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eClockBase.Modelos.Asistencias
{
    public class Model_Parametros
    {
        public int PERSONA_ID { get; set; }
        public string AGRUPACION { get; set; }
        public DateTime FECHA_INICIAL { get; set; }
        public DateTime FECHA_FINAL { get; set; }
        public string TIPO_INC_SIS_IDs { get; set; }
        public string TIPO_INCIDENCIA_IDs { get; set; }
        public Model_Parametros()
        {
        }
        public Model_Parametros(int PersonaID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal)
        {
            PERSONA_ID = PersonaID;
            AGRUPACION = Agrupacion;
            FECHA_INICIAL = FechaInicial;
            FECHA_FINAL = FechaFinal;
            TIPO_INC_SIS_IDs = "";
            TIPO_INCIDENCIA_IDs = "";
        }
        public Model_Parametros(int PersonaID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, string TiposIncidenciasSistemaIDs, string TiposIncidenciasIDs)
        {
            PERSONA_ID = PersonaID;
            AGRUPACION = Agrupacion;
            FECHA_INICIAL = FechaInicial;
            FECHA_FINAL = FechaFinal;
            TIPO_INC_SIS_IDs = TiposIncidenciasSistemaIDs;
            TIPO_INCIDENCIA_IDs = TiposIncidenciasIDs;
        }
    }
}
