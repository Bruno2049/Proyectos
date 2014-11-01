using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eClockReports.BaseModificada
{
    public class CeC_StreamFileLogs : CeC_StreamFile
    {
        public static string Path = "Logs";
        public static bool PathValidado = false;
        public override string ObtenPath(string ArchivoNombre)
        {
            if(!PathValidado)
            {
                try
                {
                    Path = System.IO.Path.Combine(HttpRuntime.AppDomainAppPath, Path);
                    if (!System.IO.Directory.Exists(Path))
                    {
                        System.IO.Directory.CreateDirectory(Path);
                    }
                }
                catch { }
                PathValidado = true;
            }
            return System.IO.Path.Combine(Path, ArchivoNombre);
        }
       
    }
}
