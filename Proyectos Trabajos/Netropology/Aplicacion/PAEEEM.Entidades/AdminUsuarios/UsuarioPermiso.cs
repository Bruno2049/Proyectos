using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entidades.AdminUsuarios
{
    [Serializable]
    public class UsuarioPermiso
    {
        public int IdUsuarioPermiso { get; set; }
        public int IdUsuario { get; set; }
        public int IdPermiso { get; set; }
        public bool Estatus { get; set; }
        public bool ExistePermisoRol { get; set; }
        public DateTime FechaAdicion { get; set; }
        public string AdicionadoPor { get; set; }
    }
}
