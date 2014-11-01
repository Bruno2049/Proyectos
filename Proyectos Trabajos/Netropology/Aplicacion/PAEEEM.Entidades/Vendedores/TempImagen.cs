using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entidades.Vendedores
{
    [Serializable]
    public class TempImagen
    {
        public string CURP { get; set; }

        public byte[] Imagen { get; set; }
    }
}
