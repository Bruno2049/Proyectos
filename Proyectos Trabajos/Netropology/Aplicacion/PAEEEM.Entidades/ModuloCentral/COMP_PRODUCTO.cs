using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entidades.ModuloCentral
{
    public class CompProducto
    {
        public CompProducto()
        {}

        public CAT_PRODUCTO DatosProducto { get; set; }
        public CAT_MODULOS_SE DatosModulosSe { get; set; }
    }
}
