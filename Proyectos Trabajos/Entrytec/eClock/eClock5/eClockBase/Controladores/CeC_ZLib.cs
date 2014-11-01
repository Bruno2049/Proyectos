using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ionic.Zlib;
namespace eClockBase.Controladores
{
    public class CeC_ZLib
    {
        static string Cabecera = "CeC_ZLib=";
        public static string Json2ZJson(string Json)
        {
            if (Json == null)
                return null;
            if (Json.Length >= 8000)
            {
                string ZJson = Cabecera + Newtonsoft.Json.JsonConvert.SerializeObject(ZlibStream.CompressString(Json));
                return ZJson;
            }
            return Json;
        }
        public static string ZJson2Json(string ZJson)
        {
            if(ZJson.Length > Cabecera.Length)
                if (ZJson.Substring(0, Cabecera.Length) == Cabecera)
                {
                    byte[] Resultante = Newtonsoft.Json.JsonConvert.DeserializeObject<byte[]>(ZJson.Substring(Cabecera.Length));
                    ZJson = ZlibStream.UncompressString(Resultante);
                    return ZJson;
                }
            return ZJson;
        }

        public static string Object2Json(object Objeto)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(Objeto);
        }
        public static string Object2ZJson(object Objeto)
        {
            return Json2ZJson(Object2Json(Objeto));
        }
        public static string Normaliza(string Cadena)
        {
            Cadena = Cadena.Replace("{\"@xml:space\":\"preserve\",\"#significant-whitespace\":\"  \"}", "\"  \"");
            return Cadena;
        }
        public static T Json2Object<T>(string value)
        {
            value = Normaliza(value);
            string R = ZJson2Json(value);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(R);
        }
    }
}
