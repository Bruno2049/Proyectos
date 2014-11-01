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
/// Descripción breve de CeC_CRep
/// </summary>
public class CeC_CRepLogo
{
    public static string TablaNombre
    {
        get { return "LogoReportes"; }
    }
    public static DataTable Tabla
    {
        get
        {
            DataTable DT = new DataTable();
            DT.TableName = TablaNombre;
            DT.Columns.Add("Logo", System.Type.GetType("System.Byte[]"));

            DataRow DR = DT.NewRow();
            DR[0] = CeC_BD.ObtenImagen("imgreporte");
            DT.Rows.Add(DR);
            return DT;
        }
    }


}
