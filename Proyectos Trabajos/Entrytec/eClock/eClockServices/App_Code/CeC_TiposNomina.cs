using System;
using System.Data;
using System.Configuration;
using System.Web;

using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.UI.HtmlControls;
using System.Web.SessionState;
using System.Data.OleDb;

using System.IO;
using System.EnterpriseServices;

/// <summary>
/// Descripción breve de CeC_TiposNomina
/// </summary>
public class CeC_TiposNomina : CeC_Tablas
{
    public CeC_TiposNomina(CeC_Sesion Sesion)
        : base("EC_TIPO_NOMINA", "TIPO_NOMINA_ID", Sesion)
    {

    }
    public static int ObtenTipoNominaIDDeTipoNominaIDEx(string TIPO_NOMINA_IDEX, int SuscripcionID)
    {
        return CeC_BD.EjecutaEscalarInt("SELECT TIPO_NOMINA_ID FROM EC_TIPO_NOMINA WHERE TIPO_NOMINA_BORRADO = 0 AND TIPO_NOMINA_IDEX = '" + TIPO_NOMINA_IDEX + "' AND " +
            CeC_Autonumerico.ValidaSuscripcion("EC_TIPO_NOMINA", "TIPO_NOMINA_ID", SuscripcionID)
            );
    }

    public static int Agraga(int PERIODO_N_ID, string TIPO_NOMINA_NOMBRE, string TIPO_NOMINA_IDEX, int SesionID, int SuscripcionID)
    {
        int TIPO_NOMINA_ID = ObtenTipoNominaIDDeTipoNominaIDEx(TIPO_NOMINA_IDEX, SuscripcionID);
        if (TIPO_NOMINA_ID > 0)
            return TIPO_NOMINA_ID;
        TIPO_NOMINA_ID = CeC_Autonumerico.GeneraAutonumerico("EC_TIPO_NOMINA", "TIPO_NOMINA_ID", SesionID, SuscripcionID);

        string Qry = "INSERT INTO EC_TIPO_NOMINA (TIPO_NOMINA_ID, CALENDARIO_DF_ID,PERIODO_N_ID, " +
            "TIPO_NOMINA_NOMBRE, TIPO_NOMINA_IDEX, TIPO_NOMINA_BORRADO) VALUES(" +
            TIPO_NOMINA_ID + ",0," + PERIODO_N_ID + ",'" + CeC_BD.ObtenParametroCadena(TIPO_NOMINA_NOMBRE) + "','" +
            TIPO_NOMINA_IDEX + "',0)";
        if (CeC_BD.EjecutaComando(Qry) > 0)
            return TIPO_NOMINA_ID;
        return -9999;

    }
}
