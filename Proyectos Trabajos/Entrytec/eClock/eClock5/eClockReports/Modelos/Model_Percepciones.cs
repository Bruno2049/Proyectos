using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;


namespace eClockReports.Modelos
{
    public class Model_Percepciones
    {
        public int PersonaID { get; set; }
        public string ID { get; set; }
        public string Nombre { get; set; }
        public decimal Unidad { get; set; }
        public decimal Importe { get; set; }
        public Model_Percepciones(string sID, string sNombre, decimal sUnidad, decimal sImporte)
        {
            ID = sID;
            Nombre = sNombre;
            Unidad = sUnidad;
            Importe = sImporte;

        }
        public Model_Percepciones()
        { }
    }
}