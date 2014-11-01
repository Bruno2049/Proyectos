using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.Entidades;
namespace PAEEEM.Entidades.AltaBajaEquipos
{
    public class DetalleEquipoAltaEfic
    {
        public string NO_CREDITO { get; set; }
        public string Dx_Grupo { get; set; }
        public string Dx_Tecnologia { get; set; }
        public string Dx_Marca { get; set; }
        public string Dx_Modelo { get; set; }
        public string Dx_Sistema { get; set; }
        public int? Cantidad { get; set; }
        public decimal? Precio_Distribuidor { get; set; }
        public decimal Precio_Unitario { get; set; }
        public decimal? Importe_Total_Sin_IVA { get; set; }
        public decimal Importe_Total { get; set; }
        public string No_Capacidad { get; set; }
        public decimal MontoChatarrizacion { get; set; }
        public decimal? MontoIncentivo { get; set; }
        public decimal? Gasto_Instalacion { get; set; }
        public string Fabricante { get; set; }
        public string CostAcopDes { get; set; }
        public string TarifaOrigen { get; set; }
        public string TarifaFutura { get; set; }

        public int? IdCreditoSustitucion { get; set; }
        public int? IdCreditoProducto { get; set; }
        public int IdConsecutivo { get; set; }
        public int idTipoFoto { get; set; }

        public string URL { get; set; }
        public byte[] Foto { get; set; }
    }
}
