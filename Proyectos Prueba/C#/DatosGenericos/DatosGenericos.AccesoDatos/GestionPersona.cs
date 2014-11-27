using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatosGenericos.Entidades;

namespace DatosGenericos.AccesoDatos
{
    public class GestionPersona
    {
        private string _ruta;
        private FileStream _file;

        public string Ruta
        {
            get { return _ruta; }
            set { _ruta = value; }
        }
        
        public GestionPersona(string Ruta)
        {
            _ruta = Ruta;
        }

        public bool InsertaRegistro()
        {
            _file = new FileStream(_ruta,FileMode.OpenOrCreate,FileAccess.ReadWrite);

        }
    }
}
