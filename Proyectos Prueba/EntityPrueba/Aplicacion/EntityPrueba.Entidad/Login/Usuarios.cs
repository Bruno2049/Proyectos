using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityPrueba.Entidad.Login
{
    public class Usuarios
    {
        public string Usuario { get; set; }
        public string Contrasena { get; set; }
        public string Nombre_Completo { get; set; }
        public string Correo_Electronico { get; set; }

        public int Tipo_Usuario { get; set; }
        public int Nivel_Usuario { get; set; }
        public int Historial { get; set; }
        public int Log_Registro { get; set; }
    }
}
