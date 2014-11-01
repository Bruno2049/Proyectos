using System;


namespace PAEEEM.Entidades.AltaBajaEquipos
{
    [Serializable]
    public class GridEquiposBaja
    {
        public int? Rownum { get; set; }
        public int Id_Credito_Sustitucion { get; set; }
        public string No_Credito { get; set; }
        public int idConsecutivo { get; set; }
        public int Cve_Tecnologia { get; set; }
        public string Tecnologia { get; set; }
        public string Producto { get; set; }
        public string Grupo { get; set; }
        public string Capacidad { get; set; }
        public string Unidades { get; set; }
        public int Cantidad { get; set; }
    }
}
