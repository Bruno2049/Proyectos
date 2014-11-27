using System;

namespace DatosGenericos.Entidades
{
    public class ModelPersona
    {
        public int IdEmpleado { get; set; }
        public int IdUsuario { get; set; }
        public int IdPuesto { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public DateTime? FechaNacimieto { get; set; }
        public string Rfc { get; set; }
        public string Curp { get; set; }
        public int IdTelefonos { get; set; }
        public int IdDireccion { get; set; }
    }
}
