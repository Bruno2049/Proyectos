using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Text.RegularExpressions;
/// <summary>
/// Descripción breve de CeC_Importacion
/// </summary>
public class CeC_Importacion
{
    public string m_Errores = "";
    public DataSet m_DataSetDestino = null;
    public CeC_Importacion()
    {
    }
    public CeC_Importacion(DataSet DS)
    {
        m_DataSetDestino = DS;
    }
    public int ImportaCSVTabulador(string[] LineasCSV, DataTable Destino)
    {
        return Importa(LineasCSV, Destino, "\t");
    }
    public int ImportaCSVTabulador(string[] LineasCSV)
    {
        return ImportaCSVTabulador(LineasCSV, m_DataSetDestino.Tables[0]);
    }

    public int ImportaCSV(string[] LineasCSV)
    {
        return ImportaCSV(LineasCSV, m_DataSetDestino.Tables[0]);
    }
    public int ImportaCSV(string[] LineasCSV, DataTable Destino)
    {
        return Importa(LineasCSV, Destino, ",");
    }
    private void AgregaError(string Error, int Linea)
    {
        m_Errores = "Error en la Linea:" + Linea + " " + CeC.AgregaSeparador(m_Errores, Error, "<br>");
    }
    public int Importa(string[] Lineas, DataTable Destino, string Separador)
    {
        int Importados = 0;
        m_Errores = "";
        int NoLinea = 0;
        int NoCampos = Destino.Columns.Count;

        foreach (string Linea in Lineas)
        {
            NoLinea++;
            string NLinea = Linea.Trim();
            if (NLinea == "")
                continue;
            string[] Campos = CeC.ObtenArregoSeparador(NLinea, Separador);
            if (NoCampos != Campos.Length)
            {
                AgregaError("No coincide la catidad de campos encontrados", NoLinea);
            }
            else
            {
                try
                {
                    DataRow DR = Destino.NewRow();
                    for (int NoCampo = 0; NoCampo < NoCampos; NoCampo++)
                    {
                        DR[NoCampo] = Campos[NoCampo];
                    }
                    Destino.Rows.Add(DR);
                    Importados++;
                }
                catch (Exception ex)
                {
                    CIsLog2.AgregaError(ex);
                    AgregaError("Datos posiblemente incompatibles", NoLinea);
                }
            }

        }
        return Importados;
    }
    public static string[] String2Strings(string Cadena)
    {
        return Regex.Split(Cadena, "\r\n");
    }
}
