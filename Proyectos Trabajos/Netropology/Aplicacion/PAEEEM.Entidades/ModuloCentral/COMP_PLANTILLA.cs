using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entidades.ModuloCentral
{
    [Serializable]
    public class Comp_Plantilla
    {
        public Comp_Plantilla()
        { }

        public int? CvePlantilla { get; set; }
        public string DxDescripcion { get; set; }
        public int? Estatus { get; set; }
        public DateTime? FechaAdicion { get; set; }
        public int? AdicionadoPor { get; set; }
        public string DxPanel { get; set; }
        public List<Campo_Customizable_Plantilla> LstCamposCustomizables { get; set; }
        //public List<Campo_Customizable> LstCamposCustomizables { get; set; }
    }
}
