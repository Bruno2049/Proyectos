using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Descripción breve de CeC_Interprete
/// </summary>
public class CeC_Interprete
{
    private string[] m_Cadenas = null;
    private string[] m_Parametros = null;
    private string[] m_Valores = null;
    public static string[] ObtenParametros(string[] Cadenas)
    {
        string[] Parametros = new string[Cadenas.Length];
        int i = 0;
        foreach (string Cadena in Cadenas)
        {
            Parametros[i++] = ObtenParametro(Cadena);
        }
        return Parametros;
    }
    public string ObtenValor(string Parametro)
    {
        int Pos = ObtenParametroPos(Parametro);
        if (Pos >= 0)
            if (ContieneValores(m_Valores[Pos]))
                return ObtenValores(m_Valores[Pos]);
            else
                return m_Valores[Pos];
        return "";
    }

    public int ObtenParametroPos(string Parametro)
    {
        for (int Cont = 0; Cont < m_Parametros.Length; Cont++)
            if (m_Parametros[Cont] == Parametro)
                return Cont;
        return -1;
    }

    public string[] ObtenParametros()
    {
        return m_Parametros;
    }

    public static string ObtenParametro(string Cadena)
    {
        return CeC.ObtenColumnaSeparador(Cadena, "=", 0).Trim();
    }
    public static string ObtenValorg(string Cadena)
    {
        return CeC.ObtenColumnaSeparador(Cadena, "=", 1).Trim();
    }
    public static bool ContieneValores(string Valor)
    {
        if (Valor[0] == '(' && Valor[Valor.Length - 1] == ')')
            return true;
        return false;
    }
    public static string ObtenValores(string Valor)
    {
        return Valor.Substring(1, Valor.Length - 2);
    }
    public CeC_Interprete(string Contenido)
    {
        m_Cadenas = CeC.ObtenArregoSeparador(Contenido, "&");
        m_Parametros = ObtenParametros(m_Cadenas);
        m_Valores = new string[m_Cadenas.Length];
        int i = 0;
        foreach (string Cadena in m_Cadenas)
        {
            m_Valores[i++] = ObtenValorg(Cadena);
        }
    }
    public bool AsignaParametro(string Parametro, string Valor)
    {
        try
        {
            int Pos = ObtenParametroPos(Parametro);
            if (Pos >= 0)
            {
                m_Valores[Pos] = Valor;
            }
            else
            {
                string[] Parametros = new string[m_Parametros.Length + 1];
                string[] Valores = new string[m_Parametros.Length + 1];
                for (int Cont = 0; Cont < m_Parametros.Length; Cont++)
                {
                    Parametros[Cont] = m_Parametros[Cont];
                    Valores[Cont] = m_Valores[Cont];
                }
                Parametros[m_Parametros.Length] = Parametro;
                Valores[m_Parametros.Length] = Valor;
                m_Valores = Valores;
                m_Parametros = Parametros;
            }
            return true;
        }
        catch (Exception ex)
        { }
        return false;
    }

    public string ObtenCadena()
    {
        string Cadena = "";
        for (int Cont = 0; Cont < m_Parametros.Length; Cont++)
        {
            Cadena = CeC.AgregaSeparador(Cadena, m_Parametros[Cont] + "=" + m_Valores[Cont], "&");
        }
        return Cadena;
    }
}