//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Persistencia
{
    using System;
    using System.Collections.Generic;
    
    public partial class CabeceraFactura
    {
        public CabeceraFactura()
        {
            this.DetalleFactura = new HashSet<DetalleFactura>();
        }
    
        public int Id { get; set; }
        public string NombreCliente { get; set; }
        public Nullable<System.DateTime> FechaEmision { get; set; }
        public Nullable<decimal> Total { get; set; }
    
        public virtual ICollection<DetalleFactura> DetalleFactura { get; set; }
    }
}