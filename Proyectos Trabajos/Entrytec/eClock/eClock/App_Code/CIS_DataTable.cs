using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;
using System.Reflection;



/// <summary>
/// Descripción breve de CeT_DataTable
/// </summary>
public class CeT_DataTable : System.ComponentModel.Component
{
    public PropertyInfo ObtenPropiedad(object Objeto, string Nombre)
    {
        MemberInfo[] MInfo = Objeto.GetType().GetMember(Nombre, BindingFlags.NonPublic | BindingFlags.Instance);
        PropertyInfo PInfo = Objeto.GetType().GetProperty(Nombre, BindingFlags.NonPublic | BindingFlags.Instance);
        /*   if (PInfo != null)
           {
               return PInfo.GetValue(Objeto, null);
           }*/
        return PInfo;
    }
    public bool RemplazaConcatenacion()
    {
        if (!CeC_BD.EsOracle)
            return false;
        PropertyInfo PInfo = ObtenPropiedad(this, "CommandCollection");

        DbCommand[] DB = (DbCommand[])PInfo.GetValue(this, null);
        for (int Cont = 0; Cont < DB.Length; Cont++)
        {
            DB[Cont].CommandText = RemplazaMasX2Pipe(DB[Cont].CommandText);
        }
        return true;
    }
    public string RemplazaMasX2Pipe(string Texto)
    {
        string Res = "";
        int UPos = 0;
        for (int Cont = 0; Cont < Texto.Length; Cont++)
        {
            if (Cont > 0 && Texto[Cont] == '+')
            {
                string Antes = Texto.Substring(0, Cont).Trim();
                string Despues = Texto.Substring(Cont + 1).Trim();
                if ((Antes.Length > 0 && Antes[Antes.Length - 1] == '\'') || (Despues.Length > 0 && Despues[0] == '\''))
                {
                    Res += "||";
                }
                else
                    Res += Texto.Substring(Cont, 1);
            }
            else
                Res += Texto.Substring(Cont, 1);
        }
        //Res += Texto.Substring(Res);
        return Res;
    }
    public bool RemplazaAs()
    {
        if (!CeC_BD.EsOracle)
            return false;
        PropertyInfo PInfo = ObtenPropiedad(this, "CommandCollection");

        DbCommand[] DB = (DbCommand[])PInfo.GetValue(this, null);
        for (int Cont = 0; Cont < DB.Length; Cont++)
        {
            DB[Cont].CommandText = DB[Cont].CommandText.Replace(" AS eC_", " eC_");
        }
        return true;

    }
    public bool ActualizaIn(string Sust999999)
    {
        RemplazaConcatenacion();
        RemplazaAs();
        PropertyInfo PInfo = ObtenPropiedad(this, "CommandCollection");

        DbCommand[] DB = (DbCommand[])PInfo.GetValue(this, null);
        for (int Cont = 0; Cont < DB.Length; Cont++)
        {
            DB[Cont].CommandText = DB[Cont].CommandText.Replace("S999999", Sust999999);
            DB[Cont].CommandText = DB[Cont].CommandText.Replace("999999", Sust999999);
        }
        return true;
    }
    public bool ActualizaIn8(string Sust888888)
    {

        PropertyInfo PInfo = ObtenPropiedad(this, "CommandCollection");

        DbCommand[] DB = (DbCommand[])PInfo.GetValue(this, null);
        for (int Cont = 0; Cont < DB.Length; Cont++)
        {
            DB[Cont].CommandText = DB[Cont].CommandText.Replace("S888888", Sust888888);
            DB[Cont].CommandText = DB[Cont].CommandText.Replace("888888", Sust888888);
        }
        return true;
    }

    public bool ActualizaIn7(string Sust777777)
    {

        PropertyInfo PInfo = ObtenPropiedad(this, "CommandCollection");

        DbCommand[] DB = (DbCommand[])PInfo.GetValue(this, null);
        for (int Cont = 0; Cont < DB.Length; Cont++)
        {
            DB[Cont].CommandText = DB[Cont].CommandText.Replace("S777777", Sust777777);
            DB[Cont].CommandText = DB[Cont].CommandText.Replace("777777", Sust777777);
        }
        return true;
    }

    public bool Remplaza(string TextoSustituir)
    {
        PropertyInfo PInfo = ObtenPropiedad(this, "CommandCollection");

        DbCommand[] DB = (DbCommand[])PInfo.GetValue(this, null);

        DB[0].CommandText = TextoSustituir;
        return true;
    }
}
