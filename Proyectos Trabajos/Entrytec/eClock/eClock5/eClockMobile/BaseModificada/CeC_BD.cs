using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml;
using System.IO;

namespace eClockMobile.BaseModificada
{
    public class CeC_BD
    {
        public static string DataSet2Json(DataSet DS)
        {
            try
            {
                if (DS == null)
                    return null;
                StringWriter sw = new StringWriter();
                DS.WriteXml(sw, XmlWriteMode.IgnoreSchema);
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(sw.ToString());

                //return JsonConvert.SerializeXmlNode(xd.DocumentElement["Table"], Newtonsoft.Json.Formatting.None, true);
                return JsonConvert.SerializeObject(xd);
            }
            catch (Exception ex)
            {
                eClockBase.CeC_Log.AgregaError(ex);
            }
            return null;
        }

        public static string DataSet2JsonList(DataSet DS, bool ArregloOpcional = false)
        {
            string Json = DataSet2Json(DS);
            if (Json == null)
                return null;
            string Buscar = "\"" + DS.Tables[0].TableName + "\":";
            int Pos = Json.IndexOf(Buscar);
            if (Pos > 0)
            {
                string Res = Json.Substring(Pos + Buscar.Length, Json.Length - Pos - Buscar.Length - 2);

                if (!ArregloOpcional && Res[0] != '[')
                    Res = "[" + Res + "]";
                return Res;

            }
            int Cor = Json.IndexOf("[");
            if (Cor > 0)
                Json = Json.Substring(Cor);
            Cor = Json.LastIndexOf("]");
            if (Cor > 0)
                Json = Json.Substring(0, Cor + 1);
            return Json;
        }
    }
}
