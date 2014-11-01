using System;
using System.Web;
using System.Web.Mail;
using System.Net.Mail;
using eClock;
using System.Threading;
using EntryTec;
using System.Data.OleDb;
using System.Data;
using System.Drawing;

/// <summary>
/// Descripción breve de CeC_Monedero
/// </summary>
public class CeC_Monedero
{
    static string CamposSaldoMonedero = " EC_PERSONAS.PERSONA_NOMBRE, EC_PERSONAS.AGRUPACION_NOMBRE, " +
                                        " CASE WHEN EC_V_MONEDERO_SALDO.MONEDERO_SALDO > 0 THEN EC_V_MONEDERO_SALDO.MONEDERO_SALDO ELSE 0 END AS MONEDERO_SALDO ";
    static string CamposConsumoEmpleado =
                    "EC_PERSONAS_DATOS.DIVISION AS EMPRESA, " +
        //" (CASE WHEN EC_PERSONAS_DATOS.DIVISION = 'MERCK' THEN EC_PERSONAS_DATOS.DIVISION " +
        //" WHEN EC_PERSONAS_DATOS.DIVISION = 'INTER-CON' THEN EC_PERSONAS_DATOS.DIVISION " +
        //" WHEN EC_PERSONAS_DATOS.DIVISION  = 'TARJETA DE AREA' THEN EC_PERSONAS_DATOS.DIVISION " +
        //" ELSE 'OTROS' END) AS EMPRESA, " +
                    " EC_PERSONAS.PERSONA_NOMBRE, EC_PERSONAS.AGRUPACION_NOMBRE, " +
                    " SUM(CASE WHEN EC_MONEDERO.MONEDERO_CONSUMO > 0 THEN EC_MONEDERO.MONEDERO_CONSUMO ELSE 0 END) AS MONEDERO_CONSUMO, " +
                    " SUM(CASE WHEN EC_MONEDERO.MONEDERO_CONSUMO < 0 THEN EC_MONEDERO.MONEDERO_CONSUMO*-1 ELSE 0 END) AS ABONO ";
    static string CamposMovimientoMonedero = "EC_PERSONAS.PERSONA_NOMBRE, EC_PERSONAS.AGRUPACION_NOMBRE, EC_PERSONAS_DATOS.TIPO_NOMINA, " +
                    " EC_MONEDERO.MONEDERO_ID, EC_MONEDERO.MONEDERO_FECHA, EC_MONEDERO.MONEDERO_CONSUMO, " +
                    " EC_MONEDERO.MONEDERO_SALDO, EC_TIPO_COBRO.TIPO_COBRO ";
    static string MonederoProducto = "EC_PERSONAS.PERSONA_NOMBRE, EC_PERSONAS.AGRUPACION_NOMBRE, " +
                    " EC_MONEDERO_PROD.MONEDERO_ID, EC_MONEDERO.MONEDERO_FECHA, EC_MONEDERO.MONEDERO_CONSUMO, " +
                    " EC_MONEDERO.MONEDERO_SALDO, EC_TIPO_COBRO.TIPO_COBRO, " +
                    " EC_PRODUCTOS.PRODUCTO, EC_PRODUCTOS.PRODUCTO_COSTO ";
    public CeC_Monedero()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    public static DataSet ReporteSaldoMonedero(int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, int Usuario_ID)
    {
        string Campos = " EC_V_MONEDERO_SALDO.MONEDERO_ID, EC_PERSONAS.PERSONA_LINK_ID, " + CamposSaldoMonedero;
        string OrdenarPor = "EC_PERSONAS.PERSONA_NOMBRE";
        if (Persona_ID > 0)
        {
            Campos = CamposSaldoMonedero;
            OrdenarPor = "EC_PERSONAS.PERSONA_NOMBRE";
        }
        else
        {
            Campos = " EC_V_MONEDERO_SALDO.MONEDERO_ID, EC_PERSONAS.PERSONA_LINK_ID, " +
                        CamposSaldoMonedero;
            OrdenarPor = "EC_PERSONAS.PERSONA_NOMBRE";
        }
        string Qry = " SELECT    " + Campos + " " +
            "FROM         EC_PERSONAS INNER JOIN EC_V_MONEDERO_SALDO ON EC_PERSONAS.PERSONA_ID = EC_V_MONEDERO_SALDO.PERSONA_ID " +
            //"INNER JOIN EC_MONEDERO ON EC_PERSONAS.PERSONA_ID = EC_MONEDERO.PERSONA_ID " +
            "WHERE  EC_PERSONAS." +
        ValidaAgrupacion(Persona_ID, Usuario_ID, Agrupacion, true) +
            //" GROUP BY " + Campos + 
        " \n ORDER BY " + OrdenarPor;
        return (DataSet)CeC_BD.EjecutaDataSet(Qry);
    }
    public static DataSet ReporteConsumoEmpleado(int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, int Usuario_ID)
    {
        string Campos = "EC_MONEDERO.PERSONA_ID, EC_PERSONAS.PERSONA_LINK_ID, " + CamposConsumoEmpleado;
        string OrdenarPor = "EC_PERSONAS.PERSONA_LINK_ID";
        if (Persona_ID > 0)
        {
            Campos = CamposConsumoEmpleado;
            OrdenarPor = "EC_PERSONAS.PERSONA_LINK_ID";
        }
        else
        {
            Campos = "EC_MONEDERO.PERSONA_ID, EC_PERSONAS.PERSONA_LINK_ID, " +
                    CamposConsumoEmpleado;
            OrdenarPor = "EC_PERSONAS.PERSONA_LINK_ID";
        }
        string Qry = " SELECT    " + Campos + " " +
            "FROM   EC_PERSONAS INNER JOIN EC_MONEDERO ON EC_PERSONAS.PERSONA_ID = EC_MONEDERO.PERSONA_ID " +
                    " INNER JOIN  EC_PERSONAS_DATOS ON EC_PERSONAS_DATOS.PERSONA_ID = EC_MONEDERO.PERSONA_ID " +
            " WHERE MONEDERO_FECHA   >= @FECHA_INICIAL@ AND MONEDERO_FECHA < @FECHA_FINAL@ AND EC_PERSONAS." +
        ValidaAgrupacion(Persona_ID, Usuario_ID, Agrupacion, true) +
        " GROUP BY EC_MONEDERO.PERSONA_ID, EC_PERSONAS.PERSONA_LINK_ID, EC_PERSONAS.PERSONA_NOMBRE, EC_PERSONAS_DATOS.DIVISION, EC_PERSONAS.AGRUPACION_NOMBRE, EC_PERSONAS_DATOS.CLAVE_EMPL, EC_PERSONAS_DATOS.CURP " +
        " \n ORDER BY " + OrdenarPor;
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_INICIAL", FechaInicial);
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_FINAL", FechaFinal.AddDays(1));
        return (DataSet)CeC_BD.EjecutaDataSet(Qry);
    }

    public static DataSet ReportePersonalizado(int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, int Usuario_ID, CeC_Sesion Sesion)
    {
        string Select = " SELECT ";
        string Campos = "";
        string CampoPersonalizado = Sesion.ConfiguraSuscripcion.Monedero_Campo_Agrupacion;
        string OrdenarPor = " \n ORDER BY ";
        string AgruparPor = " GROUP BY ";

        if (Sesion.ConfiguraSuscripcion.Monedero_Campo_Agrupacion != "")
        {
            CampoPersonalizado += ", EC_PERSONAS_DATOS.AREA, ";
            OrdenarPor += " EC_PERSONAS_DATOS.AREA, " + Sesion.ConfiguraSuscripcion.Monedero_Campo_Agrupacion;
            AgruparPor += " EC_PERSONAS_DATOS.AREA, " + Sesion.ConfiguraSuscripcion.Monedero_Campo_Agrupacion;
        }
        else
        {
            CampoPersonalizado += "EC_PERSONAS_DATOS.AREA, ";
            OrdenarPor += " EC_PERSONAS_DATOS.AREA ";
            AgruparPor += " EC_PERSONAS_DATOS.AREA ";
        }

        Campos = CampoPersonalizado +
                    " SUM(CASE WHEN EC_MONEDERO.MONEDERO_CONSUMO > 0 THEN EC_MONEDERO.MONEDERO_CONSUMO ELSE 0 END) AS MONEDERO_CONSUMO, " +
                    " SUM(CASE WHEN EC_MONEDERO.MONEDERO_CONSUMO < 0 THEN EC_MONEDERO.MONEDERO_CONSUMO*-1 ELSE 0 END) AS ABONO ";

        string Qry = Select + Campos + " " +
        " FROM   EC_PERSONAS INNER JOIN EC_MONEDERO ON EC_PERSONAS.PERSONA_ID = EC_MONEDERO.PERSONA_ID " +
                           " INNER JOIN EC_PERSONAS_DATOS ON EC_PERSONAS_DATOS.PERSONA_ID = EC_PERSONAS.PERSONA_ID " +
        " WHERE MONEDERO_FECHA   >= @FECHA_INICIAL@ AND MONEDERO_FECHA < @FECHA_FINAL@ AND EC_PERSONAS." +
        ValidaAgrupacion(Persona_ID, Usuario_ID, Agrupacion, true) +
        AgruparPor +
        OrdenarPor;
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_INICIAL", FechaInicial);
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_FINAL", FechaFinal.AddDays(1));
        return (DataSet)CeC_BD.EjecutaDataSet(Qry);
    }
    public static DataSet ReporteMovimientoMonedero(int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, int Usuario_ID)
    {
        string Campos = " EC_PERSONAS.PERSONA_LINK_ID, " + CamposMovimientoMonedero;
        string OrdenarPor = " EC_PERSONAS.PERSONA_LINK_ID ";
        if (Persona_ID > 0)
        {
            Campos = CamposMovimientoMonedero;
            OrdenarPor = " EC_PERSONAS.PERSONA_LINK_ID ";
        }
        else
        {
            Campos = " EC_PERSONAS.PERSONA_LINK_ID, " +
                      CamposMovimientoMonedero;
            OrdenarPor = " EC_PERSONAS.PERSONA_LINK_ID ";
        }
        string Qry = " SELECT    " + Campos + " " +
        "FROM         EC_PERSONAS INNER JOIN " +
                      " EC_MONEDERO ON EC_PERSONAS.PERSONA_ID = EC_MONEDERO.PERSONA_ID INNER JOIN " +
                      " EC_TIPO_COBRO ON EC_MONEDERO.TIPO_COBRO_ID = EC_TIPO_COBRO.TIPO_COBRO_ID INNER JOIN" +
                      " EC_PERSONAS_DATOS ON EC_PERSONAS_DATOS.PERSONA_ID  = EC_PERSONAS.PERSONA_ID " +
        "WHERE MONEDERO_FECHA   >= @FECHA_INICIAL@ AND MONEDERO_FECHA < @FECHA_FINAL@ AND EC_PERSONAS." +
        ValidaAgrupacion(Persona_ID, Usuario_ID, Agrupacion, true) +
        " \n ORDER BY " + OrdenarPor;
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_INICIAL", FechaInicial);
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_FINAL", FechaFinal.AddDays(1));
        return (DataSet)CeC_BD.EjecutaDataSet(Qry);
    }
    public static DataSet ReporteMovMonederoProducto(int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, int Usuario_ID)
    {
        string Campos = " EC_PERSONAS.PERSONA_LINK_ID, " + MonederoProducto;
        string OrdenarPor = " EC_PERSONAS.PERSONA_LINK_ID ";
        if (Persona_ID > 0)
        {
            Campos = MonederoProducto;
            OrdenarPor = " EC_PERSONAS.PERSONA_LINK_ID ";
        }
        else
        {
            Campos = " EC_PERSONAS.PERSONA_LINK_ID, " +
                      MonederoProducto;
            OrdenarPor = " EC_PERSONAS.PERSONA_LINK_ID ";
        }
        string Qry = " SELECT    " + Campos + " " +
        "FROM         EC_PERSONAS INNER JOIN " +
                      "EC_MONEDERO ON EC_PERSONAS.PERSONA_ID = EC_MONEDERO.PERSONA_ID INNER JOIN " +
                      "EC_MONEDERO_PROD ON EC_MONEDERO.MONEDERO_ID = EC_MONEDERO_PROD.MONEDERO_ID INNER JOIN " +
                      "EC_PRODUCTOS ON EC_MONEDERO_PROD.PRODUCTO_ID = EC_PRODUCTOS.PRODUCTO_ID INNER JOIN " +
                      "EC_TIPO_COBRO ON EC_MONEDERO.TIPO_COBRO_ID = EC_TIPO_COBRO.TIPO_COBRO_ID " +
        "WHERE MONEDERO_FECHA   >= @FECHA_INICIAL@ AND MONEDERO_FECHA < @FECHA_FINAL@ AND EC_PERSONAS." +
        ValidaAgrupacion(Persona_ID, Usuario_ID, Agrupacion, true) +
        " GROUP BY " + Campos + " " +
            //" GROUP BY EC_PERSONAS.PERSONA_LINK_ID, EC_PERSONAS.PERSONA_NOMBRE, EC_PERSONAS.AGRUPACION_NOMBRE, " +
            //              "EC_V_MONEDERO_SALDO.MONEDERO_ID, EC_V_MONEDERO_SALDO.MONEDERO_CONSUMO, EC_V_MONEDERO_SALDO.MONEDERO_SALDO, EC_TIPO_COBRO.TIPO_COBRO " +
        " \n ORDER BY " + OrdenarPor;
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_INICIAL", FechaInicial);
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_FINAL", FechaFinal.AddDays(1));
        return (DataSet)CeC_BD.EjecutaDataSet(Qry);
    }

    public static DataSet SubtotalPorEmpresa_ConsumoEmpleado(int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, int Usuario_ID)
    {
        string SubTotal = "";
        SubTotal =
                " distinct(EC_PERSONAS_DATOS.DIVISION), " +
                " sum(case when EC_MONEDERO.MONEDERO_CONSUMO > 0 then EC_MONEDERO.MONEDERO_CONSUMO else 0 end ) AS SUBTOTAL_CONSUMO_EMPRESA, " +
                " SUM(CASE WHEN EC_MONEDERO.MONEDERO_CONSUMO < 0 THEN EC_MONEDERO.MONEDERO_CONSUMO*-1 ELSE 0 END) AS SUBTOTAL_ABONO_EMPRESA ";
        string Qry = " SELECT    " + SubTotal + " " +
            " FROM  EC_PERSONAS_DATOS INNER JOIN EC_MONEDERO ON EC_PERSONAS_DATOS.PERSONA_ID = EC_MONEDERO.PERSONA_ID " +
                                    " INNER JOIN  EC_PERSONAS ON EC_PERSONAS.PERSONA_ID = EC_MONEDERO.PERSONA_ID " +
            " WHERE MONEDERO_FECHA   >= @FECHA_INICIAL@ AND MONEDERO_FECHA < @FECHA_FINAL@ AND EC_PERSONAS." +
        ValidaAgrupacion(Persona_ID, Usuario_ID, Agrupacion, true) +
        " GROUP BY EC_PERSONAS_DATOS.division ";
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_INICIAL", FechaInicial);
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_FINAL", FechaFinal.AddDays(1));
        return (DataSet)CeC_BD.EjecutaDataSet(Qry);
    }

    public static DataSet TotalPorEmpresa_ConsumoEmpleado(int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, int Usuario_ID)
    {
        string Total = "";
        Total =
                " SUM(CASE WHEN EC_MONEDERO.MONEDERO_CONSUMO > 0 THEN EC_MONEDERO.MONEDERO_CONSUMO ELSE 0 END) AS TOTAL_CONSUMO, " +
                " SUM(CASE WHEN EC_MONEDERO.MONEDERO_CONSUMO < 0 THEN EC_MONEDERO.MONEDERO_CONSUMO*-1 ELSE 0 END) AS TOTAL_ABONO ";
        string Qry = " SELECT    " + Total + " " +
            "FROM   EC_PERSONAS INNER JOIN EC_MONEDERO ON EC_PERSONAS.PERSONA_ID = EC_MONEDERO.PERSONA_ID " +
                              " INNER JOIN  EC_PERSONAS_DATOS ON EC_PERSONAS_DATOS.PERSONA_ID = EC_MONEDERO.PERSONA_ID " +
            " WHERE MONEDERO_FECHA   >= @FECHA_INICIAL@ AND MONEDERO_FECHA < @FECHA_FINAL@ AND EC_PERSONAS." +
        ValidaAgrupacion(Persona_ID, Usuario_ID, Agrupacion, true);
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_INICIAL", FechaInicial);
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_FINAL", FechaFinal.AddDays(1));
        return (DataSet)CeC_BD.EjecutaDataSet(Qry);
    }

    #region ValidaAgrupacion
    public static string ValidaAgrupacion(int Persona_ID, int Usuario_ID, string Agrupacion, bool IncluirPersonaID_IN)
    {
        return ValidaAgrupacion(Persona_ID.ToString(), Usuario_ID, Agrupacion, IncluirPersonaID_IN);
    }

    public static string ValidaAgrupacion(string PersonasIDs, int Usuario_ID, string Agrupacion, bool IncluirPersonaID_IN)
    {
        string QryAgrupacion = "";
        string Qry = "";
        if (PersonasIDs != "" && PersonasIDs != "-1")
        {
            if (IncluirPersonaID_IN)
                Qry = "PERSONA_ID IN (" + PersonasIDs + ") ";
            else
                Qry = " AND PERSONA_ID IN (" + PersonasIDs + ") ";
        }
        else
        {
            if (Agrupacion.Trim() == "|")
                Agrupacion = "";
            ///Ahora se verifica en Usuarios Permisos
            //QryAgrupacion = " SUSCRIPCION_ID IN(SELECT SUSCRIPCION_ID FROM EC_PERMISOS_SUSCRIP WHERE USUARIO_ID = @USUARIO_ID@) ";
            if (Usuario_ID > 0)
                QryAgrupacion = " PERSONA_ID IN (" + CeC_Agrupaciones.ObtenSQLPersonaIDsPermisos(Usuario_ID) + " ) ";
            else
                QryAgrupacion = " 1=1 AND PERSONA_ID IN (SELECT PERSONA_ID FROM EC_PERSONAS WHERE PERSONA_BORRADO = 0)";
            if (Agrupacion.Length > 0)
            {
                string AgrupacionMayus = Agrupacion.ToUpper();
                if (AgrupacionMayus.IndexOf("=") > 0 || AgrupacionMayus.IndexOf("LIKE") > 0)
                {
                    Agrupacion = Agrupacion.Substring(1, Agrupacion.Length - 2);
                    string SQL = "";
                    if (!CeC_BD.EsOracle && AgrupacionMayus.IndexOf("LIKE") > 0)
                    {
                        SQL = " COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI ";
                    }
                    QryAgrupacion += " AND PERSONA_ID IN (SELECT EC_PERSONAS.PERSONA_ID FROM EC_PERSONAS, EC_PERSONAS_DATOS, EC_TURNOS WHERE EC_PERSONAS.PERSONA_ID = EC_PERSONAS_DATOS.PERSONA_ID AND " +
                        "EC_TURNOS.TURNO_ID = EC_PERSONAS.TURNO_ID AND " +
                        Agrupacion + SQL +
                        " ) ";
                }
                else
                //                    QryAgrupacion += " AND AGRUPACION_NOMBRE LIKE  @AGRUPACION_NOMBRE@";
                {
                    QryAgrupacion += " AND AGRUPACION_NOMBRE LIKE  @AGRUPACION_NOMBRE@ ";

                }
            }
            if (IncluirPersonaID_IN)
            {
                Qry = "PERSONA_ID IN (SELECT PERSONA_ID FROM EC_PERSONAS WHERE " + QryAgrupacion + " )";
            }
            else
                Qry = " AND " + QryAgrupacion + " ";
        }
        //        Qry = CeC_BD.AsignaParametro(Qry, "PERSONA_ID", Persona_ID);
        Qry = CeC_BD.AsignaParametro(Qry, "USUARIO_ID", Usuario_ID);
        Qry = CeC_BD.AsignaParametro(Qry, "AGRUPACION_NOMBRE", Agrupacion + "%");
        return Qry;
    }
    #endregion
}