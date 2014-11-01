//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PAEEEM.Entidades
{
    using System;
    using System.Collections.Generic;
    
    public partial class CRE_Facturacion
    {
        public CRE_Facturacion()
        {
            this.CRE_FACTURACION_DETALLE = new HashSet<CRE_FACTURACION_DETALLE>();
        }
    
        public int IdFactura { get; set; }
        public string No_Credito { get; set; }
        public byte IdTipoFacturacion { get; set; }
        public int Cve_Tarifa { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Iva { get; set; }
        public decimal Total { get; set; }
        public decimal PagoFactBiMen { get; set; }
        public decimal MontoMaxFacturar { get; set; }
        public System.DateTime Fecha_Adicion { get; set; }
        public string AdicionadoPor { get; set; }
        public byte Estatus { get; set; }
    
        public virtual ICollection<CRE_FACTURACION_DETALLE> CRE_FACTURACION_DETALLE { get; set; }
        public virtual Tipo_Facturacion Tipo_Facturacion { get; set; }
        public virtual CAT_TARIFA CAT_TARIFA { get; set; }
        public virtual CRE_Credito CRE_Credito { get; set; }
    }
}