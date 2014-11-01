using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entidades.Alta_Solicitud
{
    [Serializable]
    public class DatosPyme
    {
        public int CveTipoIndustria { get; set; }
        public int CveSectorEconomico { get; set; }
        public string DxNombreComercial { get; set; }
        public int NoEmpleados { get; set; }
        public decimal TotGastosMensuales { get; set; }
        public decimal PromVtasMensuales { get; set; }
        public int CveCp { get; set; }
        public string CodigoPostal { get; set; }
        public int CveDelegMunicipio { get; set; }
        public int CveEstado { get; set; }
        public int CveMunicipioNafin { get; set; }
        public string RFC { get; set; }
    }
}
