using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eClockReports.Modelos
{
    public class Model_ReciboNomina
    {
        public List<Model_Nomina> Nomina = new List<Model_Nomina>();
        public List<Model_Deducciones> Deducciones = new List<Model_Deducciones>();
        public List<Model_Percepciones> Percepciones = new List<Model_Percepciones>();
    }
}