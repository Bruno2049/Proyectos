using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6._Access_Modifiers
{
    public class Bicycle
    {
        
        public string Color { get; set; }
        private struct Estructura
        {
            public bool Nueva {get;set;}
            public struct Llantas
            {
                public int Rin;
                public string TipoLlanta {get; set;}
            }
        }

        public void Pedal()
        {
            Tipo tipo = new Tipo();
            tipo.modelo = "Montaña";
            Console.WriteLine("Acabas de Activar el pedal y solo puedes ingresar a una estrutura o clase anidada \n El tipo de la bisicleta es {0}",tipo.modelo);
        }

        public class Tipo
        {
            public string modelo {get; set;}
        }
    }
}
