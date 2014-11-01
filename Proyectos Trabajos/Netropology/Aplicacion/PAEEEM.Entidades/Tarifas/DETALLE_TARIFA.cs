namespace PAEEEM.Entidades.Tarifas
{
    public class DetalleTarifa
    {
        public int IdRegion { get; set; }
        public string Descripcion { get; set; }
        public string Usuario { get; set; }
        public int Año { get; set; }
        public string Mes { get; set; }
        public int Dia { get; set; }
        public int Hora { get; set; }
        public int Minutos { get; set; }
        //TARIFA OM
        public int IdTarifaOm { get; set; }
        public double? MtCargoKwhConsumo { get; set; }
        public double? MtCargoKwDemanda { get; set; }
        //TARIFA HM
        public int IdTarifaHm { get; set; }
        public double MtCargoDemanda { get; set; }
        public double MtCargoPunta { get; set; }
        public double MtCargoIntermedia { get; set; }
        public double MtCargoBase { get; set; }
        public double Promedio { get; set; }
        //tarifa 02
        public int IdTarifa02 { get; set; }
        public double MtCostoKwhFijo { get; set; }
        public double MtCostoKwhBasico { get; set; }
        public double MtCostoKwhIntermedio { get; set; }
        public double MtCostoKwhExcedente { get; set; }

        //tarifa 03
        public int IdTarifa03 { get; set; }
        public double MtCargoDemandaMax { get; set; }
        public double MtCargoAdicional { get; set; }
    }
}
