using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entidades.AltaBajaEquipos
{
    public class DetallePresupuesto
    {
        public decimal? CostoEquipos { get; set; }
        public decimal? GastosInstalacion { get; set; }
        public decimal? IVA { get; set; }
        public decimal? Incentivo { get; set; }
        public decimal? CostoAcopioDesc { get; set; }
        public decimal? Descuento { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? TOTAL { get; set; }
    }

    public class DetalleBalenceMensual
    {
        public decimal? GastosMensuales { get; set; }
        public decimal? Ventas_mes { get; set; }
    }

    public class Fotos
    {
        public int idtipoFoto { get; set; }
        public string No_Credito { get; set; }
        public int idConsecutivoFoto { get; set; }
        public int? idCreditoSustitucion { get; set; }
        public int? idCreditoProducto { get; set; }
    }

    public class UnidadesPresupuesto
    {
        public decimal? AcopioDes { get; set; }
        public int Tecnologia { get; set; }
        public int Unidades { get; set; }
        public int Modelo { get; set; }
        public int TipoProducto { get; set; }
        public int Marca { get; set; }
        public int? Cantidad { get; set; }
        public decimal? ImporteSinIVa { get; set; }
        public decimal? SubTotal { get; set; }
        public int? Capacidad { get; set; }
        public decimal? GastosInst { get; set; }
        public decimal? Incentivo { get; set; }
        public decimal? Descuento { get; set; }
    }

    public class DetalleEquipoAltaEficiencia
    {
        public int ID { get; set; }
        public int ID_Baja { get; set; }
        public int FtTipoProducto { get; set; }
        public int? Cve_Grupo { get; set; }
        public string Dx_Grupo { get; set; }
        public int Cve_Tecnologia { get; set; }
        public string Dx_Tecnologia { get; set; }
        public int Cve_Marca { get; set; }
        public string Dx_Marca { get; set; }
        public int Cve_Modelo { get; set; }
        public string Dx_Modelo { get; set; }
        public string Dx_Sistema { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio_Distribuidor { get; set; }
        public decimal Precio_Unitario { get; set; }
        public decimal Importe_Total_Sin_IVA { get; set; }
        public decimal Importe_Total { get; set; }
        public decimal KwhAhorro { get; set; }
        public decimal KwAhorro { get; set; }
        public decimal No_Capacidad { get; set; }
        public double CapacidadAquipo { get; set; }

        public decimal MontoChatarrizacion { get; set; }
        public decimal MontoIncentivo { get; set; }
        public decimal Gasto_Instalacion { get; set; }
    }
}
