using System;

namespace PAEEEM.Entidades.AdminUsuarios
{
    [Serializable]
    public class PermisoUsuario
    {
        public int IdNavegacion { get; set; }
        public string NombreNavegacion { get; set; }
        public string UrlNavegacion { get; set; }
        public string CodigoPadres { get; set; }
        public string RutaPadres { get; set; }
        public string Estatus { get; set; }
        public int NivelNavegacion { get; set; }
        public string Secuencia { get; set; }
        public string TipoPermiso { get; set; }
        public int IdPermiso { get; set; }
    }
}
