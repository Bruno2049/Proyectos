using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiagramaSP.Modelo;
using Newtonsoft.Json;

namespace DiagramaSP.Clases
{
    public class ControlDeArchivos
    {
        private const string Fichero = @"Prueba.txt";
        private StreamReader _lector;
        private StreamWriter Escritor;
        private List<string> ContenidoArchivo = null;

        public bool InsetarRegistro(Nodo nodo)
        {
            try
            {
                //var texto = JsonConvert.SerializeObject(nodo);
                //var sw = new System.IO.StreamWriter(Fichero);
                //sw.WriteLine(texto);
                //sw.Close();
                //return true;
                ContenidoArchivo = new List<string>();
                _lector = new StreamReader(Fichero);
                if (_lector != null)
                {
                    while (!_lector.EndOfStream)
                    {
                        var linea = _lector.ReadLine();
                        ContenidoArchivo.Add(linea);
                    }
                    _lector.Close();
                }
                var Json = ContenidoArchivo.Aggregate(String.Empty, (current, item) => current + item);

                var jList = JsonConvert.DeserializeObject<List<Nodo>>(Json);


            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Nodo> CargaNodos()
        {
            var rnk = new StreamReader(Fichero);
            var text = rnk.ReadToEnd();
            rnk.Close();
            var listaNodos = JsonConvert.DeserializeObject<List<Nodo>>(text);
            return listaNodos;
        }
    }
}
