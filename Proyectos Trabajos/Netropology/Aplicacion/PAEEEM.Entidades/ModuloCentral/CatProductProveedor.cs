using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entidades.ModuloCentral
{
    public class CatProductProveedor
    {
        public CatProductProveedor() { }

        public int cveProduc { get; set; }
        public int idProve { get; set; }
        public int status { get; set; }
        public decimal precio { get; set; }
        public string correo { get; set; }
        //
        public string marca { get; set; }
        public string modelo { get; set; }
        public string representante { get; set; }
        public int StatusProveedor { get; set; }
    }
}
