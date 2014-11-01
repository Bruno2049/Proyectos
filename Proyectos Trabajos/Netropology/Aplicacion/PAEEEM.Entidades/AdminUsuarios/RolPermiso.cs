using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entidades.AdminUsuarios
{
    [Serializable]
    public class RolPermiso
    {
        public int IdNavegacion { get; set; }
        public string NombreNavegacion { get; set; }
        public string UrlNavegacion { get; set; }
        public string CodigoPadres { get; set; }
        public string RutaPadres { get; set; }
        public int NivelNavegacion { get; set; }
        public string Secuencia { get; set; }
        public int IdRol { get; set; }
        public string NombreRol { get; set; }
        public string RelacionRol { get; set; }
        public int IdPermiso { get; set; }
        public string TipoPermiso { get; set; }
    }
}
