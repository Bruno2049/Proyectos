//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AplicacionFragancias.Entidades
{
    using System;
    using System.Collections.Generic;
    
    public partial class COM_PRODUCTOS
    {
        public int IDPRODUCTOS { get; set; }
        public string NOORDENCOMPRA { get; set; }
        public string NOMBREPRODUCTO { get; set; }
        public string LOTE { get; set; }
        public decimal CANTIDADPRODUCTO { get; set; }
        public System.DateTime FECHAENTREGA { get; set; }
        public decimal PRECIOUNITARIO { get; set; }
        public bool ENTREGADO { get; set; }
        public bool BORRADO { get; set; }
    
        public virtual COM_ORDENCOMPRA COM_ORDENCOMPRA { get; set; }
    }
}