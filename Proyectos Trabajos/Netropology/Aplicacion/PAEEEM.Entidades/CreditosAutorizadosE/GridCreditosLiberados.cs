using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entidades.CreditosAutorizadosE
{
    public class GridCreditosLiberados
    {
        public string No_Credito { get; set; }//
        public DateTime? FechaIngreso { get; set; }//
        public DateTime? FechaAutorizado { get; set; }//
        public DateTime? FechaLiberado { get; set; }//
        public int RPU { get; set; }//
        public string Tarifa { get; set; }//
        public string TarifaOrigen { get; set; }//
        public string intelisis { get; set; }//
        public string RazonSocial { get; set; }//
        public string NombreComercial { get; set; }//
        public string RFC { get; set; }//
        public string GiroComercial { get; set; }//
        public string Estado { get; set; }//
        public string Municipio { get; set; }//
        public decimal? MontoFinanciado { get; set; }//
        public decimal? GastosInstalacion { get; set; }//
        public int Incentivo { get; set; }
        public DateTime? PAmortizacion { get; set; }//
        public decimal? Amortizacion { get; set; }//
        public decimal Chatarrizacion { get; set; }
        public decimal? kwhAhorro { get; set; }//
        public decimal? kwAhorro { get; set; }//
        public decimal? FactorPotencia { get; set; }//
        public decimal? kwhPromedio { get; set; }//
        public decimal? kwPromedio { get; set; }//
        public DateTime FechaPagoDist { get; set; }
        public decimal MontoPagoDist { get; set; }
        public string RazonSocialDist { get; set; }//
        public string NombreComercialDist { get; set; }//
        public string TipoSucuralDist { get; set; }//
        public string Region { get; set; }
        public string Zona { get; set; }

        public int? NO_EA_RC { get; set; }
        public int? NO_EA_AA { get; set; }
        public int? NO_EA_IL { get; set; }
        public int? NO_EA_ME { get; set; }
        public int? NO_EA_SE { get; set; }
        public int? NO_EA_ILED { get; set; }
        public int? NO_EA_BC { get; set; }
        public int? NO_EA_II { get; set; }
        public int? NO_EA_CR { get; set; }

        public int? NO_EB_RC { get; set; }
        public int? NO_EB_AA { get; set; }
        public int? NO_EB_ME { get; set; }
        public int? NO_EB_CR { get; set; }

        public int? Cve_region { get; set; }
        public int? Cve_Zona { get; set; }
        public int? Cve_Estado { get; set; }
        public int? Cve_Municipio { get; set; }
        public DateTime FechaColocacion { get; set; }
        public string TipoDistribuidor { get; set; }
    }
}
