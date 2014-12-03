using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Universidad.Entidades.ControlUsuario
{
    public class Sesion
    {
        public string Conexion { get; set; }
        public bool RecordarSesion { get; set; }
        public string Usuarion { get; set; }
        public string Contrasena { get; set; }
    }
}
