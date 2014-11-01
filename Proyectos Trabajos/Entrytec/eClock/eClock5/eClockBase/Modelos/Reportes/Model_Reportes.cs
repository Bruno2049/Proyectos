using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eClockBase.Modelos.Reportes
{
    public class Model_Reportes
    {
        public int REPORTE_ID { get; set; }
        public string REPORTE_TITULO { get; set; }
        public string REPORTE_DESCRIP { get; set; }
        public string REPORTE_IDIOMA { get; set; }
        public string REPORTE_MODELO { get; set; }
        public decimal REPORTE_PRECIO { get; set; }
        public string REPORTE_TAMANO { get; set; }
        public string REPORTE_PARAM { get; set; }
        public string REPORTE_FORMATOS { get; set; }
        public string REPORTE_SRV_NOMBRE { get; set; }
        public string REPORTE_SRV_EMPR { get; set; }
        public string REPORTE_SRV_DESC { get; set; }
        public DateTime REPORTE_USUARIO_SPAGO { get; set; }
        public DateTime REPORTE_USUARIO_UUSO { get; set; }
        public int REPORTE_USUARIO_EDO { get; set; }
        public DateTime REPORTE_USUARIO_INST { get; set; }
        public DateTime REPORTE_USUARIO_UMOD { get; set; }
        public int REPORTE_USUARIO_ORD { get; set; }
    }
}
