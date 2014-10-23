using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebFormsCRUD.LDN.Modelo
{
    public class Pintura
    {
        public int ID { get; set; }
        public string Titulo { get; set; }
        public string Localizacion { get; set; }
        public string LocalizacionImagen { get; set; }
        public bool TieneLogo { get; set; }
        public string Descripcion { get; set; }
        public string DescripcionCorta { get; set; }
        public DateTime Creacion { get; set; }
        public string Fondo { get; set; }
    }
}