using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
namespace eClockBase.Modelos.Localizaciones
{
    public class Model_Localizaciones
    {
        public string LOCALIZACION_LLAVE { get; set; }
        public string LOCALIZACION_ETIQUETA { get; set; }
        public string LOCALIZACION_AYUDA { get; set; }
        public Model_Localizaciones()
        {
        }
        public static List<Model_Localizaciones> Nuevos(Controladores.ListadoJsonLocal JsonLocal)
        {
            if (JsonLocal == null)
                return null;
            if (JsonLocal.Listado == null)
                return null;
            List<Modelos.Localizaciones.Model_Localizaciones> Resultado = null;
            try
            {
                //Resultado = eClockBase.Controladores.CeC_ZLib.Json2Object<List<Modelos.Localizaciones.Model_Localizaciones>>(JsonLocal.Listado);
                Resultado = Controladores.CeC_ZLib.Json2Object<List<Modelos.Localizaciones.Model_Localizaciones>>(JsonLocal.Listado);
            }
            catch (Exception ex)
            {
                eClockBase.CeC_Log.AgregaError(ex);
            }
            return Resultado;
        }

    }
}
