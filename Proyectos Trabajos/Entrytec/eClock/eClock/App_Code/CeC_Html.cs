using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Descripción breve de CeC_Html
/// </summary>
public class CeC_Html
{
    public CeC_Html()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    public static string ObtenTabla(string Html, string ID)
    {
        return ObtenElemento(Html,"table",ID);
    }
    public static string ObtenElemento(string Html, string Elemento, string ID)
    {
        string Ret = "";
        int Pos = Html.IndexOf("<" + Elemento + " id=\"" + ID + "\"");
        string FinElemento = "</" + Elemento + ">";
        int UPos = Html.IndexOf(FinElemento, Pos);
        Ret = Html.Substring(Pos, UPos - Pos + FinElemento.Length);
        return Ret;
    }
    public static int ObtenElementoInicio(string Html, string Elemento, string ID)
    {
        return Html.IndexOf("<" + Elemento + " id=\"" + ID + "\"");
    }
    public static int ObtenElementoFin(string Html, string Elemento, string ID)
    {
        int Pos = Html.IndexOf("<" + Elemento + " id=\"" + ID + "\"");
        string FinElemento = "</" + Elemento + ">";
        int UPos = Html.IndexOf(FinElemento, Pos);
        return UPos + FinElemento.Length;
    }
    public static string AsignaParametro(string Qry, string Parametro, string Valor)
    {
        return Qry.Replace("@" + Parametro + "@", Valor);
    }
}
