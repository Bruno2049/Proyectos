using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Descripción breve de CeC_Exportacion
/// </summary>
public class CeC_Exportacion
{
    string m_NombreTemporal = "";
    string[] sRegistros = null;
    int sLineas = 0;
    public CeC_Exportacion()
    {

    }
    public bool GenerarArchivo(DataSet DS, string Separador)
    {
        try
        {
            int Linea = 1;
            string[] Registros = null;
            Registros = new string[DS.Tables[0].Rows.Count + 1];
            foreach (DataRow Registro in DS.Tables[0].Rows)
            {

                foreach (DataColumn Columna in DS.Tables[0].Columns)
                {
                    if (Linea <= 1)
                    {
                        Registros[0] += Columna.ColumnName + Separador;
                        Registros[Linea] += Registro[Columna] + Separador;

                    }
                    else
                        Registros[Linea] += Registro[Columna] + Separador;

                }
                Linea++;
            }
            sLineas = Linea;
            sRegistros = Registros;
            return true;
        }
        catch { return false; }
    }

    public int ExportarArchivo(Page Pagina)
    {

        Pagina.Response.ContentType = "text/html";
        Pagina.Response.Write(ObtenTexto());
        return sLineas;
    }

    public string ObtenTexto()
    {
        string Separador = "\r\n";
        string Texto = "";
        foreach (string Registro in sRegistros)
        {
            Texto = CeC.AgregaSeparador(Texto, Registro, Separador);
        }
        return Texto;
    }

    public bool GuardaTemporal(string Nombre)
    {
        try
        {
            m_NombreTemporal = Nombre;
            string NArchivo = HttpRuntime.AppDomainAppPath + CeC_Config.RutaReportesPDF + m_NombreTemporal;
            if (System.IO.File.Exists(NArchivo))
                System.IO.File.Delete(NArchivo);
            System.IO.File.WriteAllText(NArchivo, ObtenTexto());
            return true;
        }
        catch { }
        return false;
    }

    public bool GuardaTemporal()
    {
        System.Random Rnd = new Random(99999);
        int R = Rnd.Next(0, 99999);
        return GuardaTemporal("Temporal" + R + ".txt");
    }
    public string ObtenArchivo(string Nombre)
    {
        try
        {
            return System.IO.File.ReadAllText(HttpRuntime.AppDomainAppPath + CeC_Config.RutaReportesPDF + Nombre);
        }
        catch { }
        return "";
    }
    public string ObtenArchivo()
    {
        return ObtenArchivo(m_NombreTemporal);
    }
    public int ExportarArchivoGuardado(Page Pagina)
    {
        Pagina.Response.ContentType = "text/html";
        Pagina.Response.Write(ObtenArchivo());
        return sLineas;
    }
    public int ExportarArchivoGuardado(Page Pagina, string NombreArchivo)
    {
        Pagina.Response.ContentType = "text/html";
        Pagina.Response.Write(ObtenArchivo(NombreArchivo));
        return sLineas;
    }
}
