using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entidades.AdminUsuarios
{
    [Serializable]
    public class AccionUsuario
    {
        public int IdAccion { get; set; }
        public string NombreAccion { get; set; }
        public bool EstatusAccion { get; set; }
    }
}
