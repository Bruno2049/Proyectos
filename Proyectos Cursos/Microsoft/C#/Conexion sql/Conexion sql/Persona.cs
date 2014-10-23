using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conexion_sql
{
    public class Persona
    {
        public int Id { get; set; }
        public String Nombre {get; set;}
        public String Apellido {get;set;}
        public Int64 Edad { get; set; }

        public Persona()
        { 
        }

        public Persona(int Id, String nombre, String apellido, Int64 edad)
        {
            this.Id = Id;
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Edad = edad;
        }
    }
}
