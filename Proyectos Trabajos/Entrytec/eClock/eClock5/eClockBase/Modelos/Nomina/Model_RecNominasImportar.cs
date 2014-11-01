using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eClockBase.Modelos.Nomina
{
    public class Model_RecNominasImportar
    {
        //Fecha de Generación del recibo 
        public DateTime Generación { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }

        public string NominaExID { get; set; }
        public int Ano { get; set; }
        public int PeriodoNo { get; set; }
        public List<Model_Conceptos> Conceptos = new List<Model_Conceptos>();
        public List<Model_RecNominas> RecibosNominas = new List<Model_RecNominas>();
    }
}
