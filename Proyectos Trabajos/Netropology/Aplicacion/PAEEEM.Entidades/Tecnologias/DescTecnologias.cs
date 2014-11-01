namespace PAEEEM.Entidades.Tecnologias
{
   public class DescTecnologias
    {
        public int IdTecnologia { get; set; }
        public string Nombre { get; set; }
        public string Clave { get; set; }
        public string TipoTecnologia{ get; set; }
        public string Estatus { get; set; }
        public string Tipo { get; set; }
        public string Chatarrizacion { get; set; }
        public decimal? MontoChatarrizacion { get; set; }
        public string EquipoBaja { get; set; }
        public string EquiposAlta { get; set; }
        public string FactorSustitucion { get; set; }
        public string Incentivo { get; set; }
        public decimal? MontoIncentivo { get; set; }
        public string Plantilla { get; set; }
        public string ImprimirLeyendaDescriptiva { get; set; }
        public string CombinacionTecnologia { get; set; }


    }
}
