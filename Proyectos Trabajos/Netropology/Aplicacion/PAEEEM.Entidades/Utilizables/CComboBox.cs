using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace PAEEEM.Entidades.Utilizables
{
    [DataContract]
    public class CComboBox
    {

        public CComboBox(int idElemento, string elemento)
        {
            IdElemento = idElemento;
            Elemento = elemento;
        }

        [DataMember]
        public int IdElemento { get; set; }
        [DataMember]
        public string Elemento { get; set; }

    }
}
