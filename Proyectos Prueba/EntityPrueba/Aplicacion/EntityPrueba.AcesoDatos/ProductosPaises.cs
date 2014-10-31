//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EntityPrueba.AcesoDatos
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductosPaises
    {
        public ProductosPaises()
        {
            this.ProductosClasificaciones = new HashSet<ProductosClasificaciones>();
            this.ProductosReglas = new HashSet<ProductosReglas>();
        }
    
        public int ProductoPaisId { get; set; }
        public byte PaisId { get; set; }
        public int ProductoId { get; set; }
        public Nullable<short> LineaCatalogoId { get; set; }
        public Nullable<short> FormaFarmaceuticaId { get; set; }
        public string Presentacion { get; set; }
        public string Concentracion { get; set; }
        public Nullable<System.DateTime> FechaInclusion { get; set; }
        public byte[] Imagen { get; set; }
        public bool Activo { get; set; }
        public Nullable<int> ComentarioRelacionId { get; set; }
    
        public virtual Productos Productos { get; set; }
        public virtual ICollection<ProductosClasificaciones> ProductosClasificaciones { get; set; }
        public virtual ICollection<ProductosReglas> ProductosReglas { get; set; }
    }
}
