using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace PAEEEM.Entidades.Vendedores
{
    [Serializable]
    public class DatosVendedoresDist
    {
        public int IdVendedor { get; set; }
        public string Nombre { get; set; }
        public string Curp { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string TipoIdentificacion { get; set; }
        public string NoIdentificacion { get; set; }
        public string Estatus { get; set; }
        public string AccesoSistema { get; set; }
        public string Incidencia { get; set; }
        public byte[] Archivo { get; set; }
    }
}
