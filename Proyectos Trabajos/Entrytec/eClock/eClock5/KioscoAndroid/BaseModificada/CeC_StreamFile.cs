using System;
using System.Collections.Generic;
using System.Text;

namespace KioscoAndroid.BaseModificada
{
    class CeC_StreamFile : eClockBase.CeC_Stream
    {
        public static string Path = "Datos";
        public static bool PathValidado = false;
        public override string ObtenPath(string ArchivoNombre)
        {
            if (!PathValidado)
            {
                if (!System.IO.Directory.Exists(Path))
                {
                    System.IO.Directory.CreateDirectory(Path);
                }
                PathValidado = true;
            }
            return System.IO.Path.Combine(Path, ArchivoNombre);
        }
        public override System.IO.StreamWriter AgregaTexto(string ArchivoNombre)
        {
            return System.IO.File.AppendText(ObtenPath(ArchivoNombre));
        }


        public override System.IO.StreamWriter NuevoTexto(string ArchivoNombre)
        {
            ArchivoNombre = ObtenPath(ArchivoNombre);
            if (System.IO.File.Exists(ArchivoNombre))
                System.IO.File.Delete(ArchivoNombre);
            return System.IO.File.CreateText(ArchivoNombre);
        }

        public override bool AgregaTexto(string ArchivoNombre, string Texto)
        {
            try
            {
                ArchivoNombre = ObtenPath(ArchivoNombre);
                System.IO.File.AppendAllText(ArchivoNombre, Texto);
                return true;
            }
            catch
            { }
            return false;
        }
        public override bool NuevoTexto(string ArchivoNombre, string Texto)
        {
            try
            {
                ArchivoNombre = ObtenPath(ArchivoNombre);
                if (System.IO.File.Exists(ArchivoNombre))
                    System.IO.File.Delete(ArchivoNombre);
                System.IO.File.WriteAllText(ArchivoNombre, Texto);
                return true;
            }
            catch
            { }
            return false;
        }
        public override string LeerString(string ArchivoNombre)
        {
            try
            {
                ArchivoNombre = ObtenPath(ArchivoNombre);
                return System.IO.File.ReadAllText(ArchivoNombre);
                //            return null;
            }
            catch
            { }
            return "";
        }

        public override System.IO.StreamReader LeerTexto(string ArchivoNombre)
        {
            try
            {
                ArchivoNombre = ObtenPath(ArchivoNombre);
                return System.IO.File.OpenText(ArchivoNombre);
                //            return null;
            }
            catch
            { }
            return null;
        }

        public override bool ExisteArchivo(string ArchivoNombre)
        {
            return System.IO.File.Exists(ObtenPath(ArchivoNombre));
        }

        public override DateTime FechaHoraModificacion(string ArchivoNombre)
        {
            return System.IO.File.GetLastWriteTime(ObtenPath(ArchivoNombre));
        }

        public override byte[] LeerBytes(string ArchivoNombre)
        {
            return System.IO.File.ReadAllBytes(ObtenPath(ArchivoNombre));
        }
        public override bool NuevoBytes(string ArchivoNombre, byte[] Datos)
        {
            try
            {
                ArchivoNombre = ObtenPath(ArchivoNombre);
                if (System.IO.File.Exists(ArchivoNombre))
                    System.IO.File.Delete(ArchivoNombre);
                System.IO.File.WriteAllBytes(ArchivoNombre, Datos);
                return true;
            }
            catch
            { }
            return false;
        }

        public override bool Ejecuta(string ArchivoNombre)
        {
            try
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = sObtenPath(ArchivoNombre);
                process.Start();
                return true;
            }
            catch { }
            return false;
        }
    }
}
