using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entidades.Reportes
{
    public class Distribuidor
    {
        public string Tipo { get; set; }
        public int IdMatriz { get; set; }
        public int IdSucursal { get; set; }
        public string Rfc { get; set; }
        public string RazonSocial { get; set; }
        public string NombreComercial { get; set; }
        public string IdIntelisis { get; set; }
        public double? IvaDistribuidor { get; set; }
        public string DomicilioFiscal { get; set; }
        public string CpFiscal { get; set; }
        public string EstadoFiscal { get; set; }
        public string MunicipioFiscal { get; set; }
        public string ColoniaFiscal { get; set; }
        public string CalleFiscal { get; set; }
        public string NumeroFiscal { get; set; }

        public string CpFisico { get; set; }
        public int IdEstado { get; set; }
        public string EstadoFisico { get; set; }
        public int IdMunicipio { get; set; }
        public string MunicipioFisico { get; set; }
        public string ColoniaFisico { get; set; }
        public string CalleFisico { get; set; }
        public string NumeroFisico { get; set; }

        public string RepresentanteLegal { get; set; }
        public string NombreContacto { get; set; }
        public string EmailContacto { get; set; }
        public string TelefonoContacto { get; set; }

        public int IdRegion { get; set; }
        public string Region { get; set; }
        public int IdZona { get; set; }
        public string Zona { get; set; }
        public int IdEstatus { get; set; }
        public string Estatus { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime FechaUltimoEstatus { get; set; }

        public string RefrigeracionComercial { get; set; }
        public string AireAcondicionado { get; set; }
        public string MotoresElectricos { get; set; }
        public string IluminacionLineal { get; set; }
        public string IluminacionLed { get; set; }
        public string IluminacionInduccion { get; set; }
        public string Subestaciones { get; set; }
        public string BancoCapaciores { get; set; }
        public string CamarasRefrigeracion { get; set; }
        public string CalentadoresSolares { get; set; }
    }
}
