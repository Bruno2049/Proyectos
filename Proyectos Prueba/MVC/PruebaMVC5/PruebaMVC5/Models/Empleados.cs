using System;

namespace PruebaMVC5.Models
{
    public class Empleados
    {
        public int IdEmpleado { get; set; }
        public int IdDepartamento { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Genero { get; set; }
        public string Ciudad { get; set; }
    }
}