using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace eClock5.Clases
{
    class CeC_AppConfigW
    {
        public static bool ActualizaAppSettings(string Archivo, string KeyName, string KeyValue)
        {
            //            KeyValue = KeyValue.Replace("\"", "&quot;");
            if (KeyValue == null)
                KeyValue = "";
            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.Load(Archivo);
            foreach (XmlElement xElement in XmlDoc.DocumentElement)
            {
                if (xElement.Name == "applicationSettings")
                {
                    foreach (XmlNode xNode in xElement.ChildNodes)
                    {
                        foreach (XmlNode xNode2 in xNode.ChildNodes)
                        {
                            if (xNode2.Attributes[0].Value == KeyName)
                                if (KeyValue.Length > 0)
                                    xNode2.InnerXml = "<value>" + KeyValue + "</value>";
                                else
                                    xNode2.InnerXml = "<value />";
                        }
                    }
                }
            }
            XmlDoc.Save(Archivo);
            return false;
        }
    }
}
