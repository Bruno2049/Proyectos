using System;

namespace PAEEEM.Entidades.ModuloCentral
{
    [Serializable]
    public class Tarifa_Tecnologia
    {
        public Tarifa_Tecnologia()
        { }

        public int CveTecnologia { get; set; }
        public int CveTarifa { get; set; }
        public string DxTarifa { get; set; }
        public int Estatus { get; set; }
        public DateTime? FechaAdicion { get; set; }
        public string AdicionadoPor { get; set; }
        public int EstatusInt { get; set; }
    }
}
