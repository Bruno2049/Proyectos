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
    
    public partial class ALM_CAT_ALMECENES
    {
        public ALM_CAT_ALMECENES()
        {
            this.COM_ORDENCOMPRA = new HashSet<COM_ORDENCOMPRA>();
        }
    
        public short IDALAMACENES { get; set; }
        public string NOMBREALMACEN { get; set; }
        public string DESCRIPCION { get; set; }
        public bool BORRADO { get; set; }
    
        public virtual ICollection<COM_ORDENCOMPRA> COM_ORDENCOMPRA { get; set; }
    }
}