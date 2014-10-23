using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebFormsCRUD.LDN.Modelo
{
    public class Producto
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string DescripcionCorta { get; set; }
        public int Precio { get; set; }
        public int Existencias { get; set; }
        public string Logo { get; set; }
        public DateTime UltimaActualizacion { get; set; }
    }
}