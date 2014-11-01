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
/// Descripción breve de Cec_Comida
/// </summary>
public class Cec_Comida
{
    CeC_Sesion Sesion;
    /// <summary>
    /// Campos a mostrar en los reportes de Comidas por Empleado
    /// </summary>
    static string CamposComidaEmpleado = "EC_PERSONAS.PERSONA_NOMBRE, " +
                        " EC_PERSONAS.AGRUPACION_NOMBRE, " +
                        " EC_PERSONAS_DATOS.TIPO_NOMINA, " +
                        " SUM(CASE WHEN EC_V_PERSONAS_COMIDA.PRIMERA_COMIDA_ES = 1 THEN 1 ELSE 0 END) AS TOTAL_PRIMERA_COMIDA, " +
                        " SUM(EC_V_PERSONAS_COMIDA.PRIMERA_COMIDA_COSTO) AS TOTAL_PRIMERA_COMIDA_COSTO, " +
                        " SUM(CASE WHEN EC_V_PERSONAS_COMIDA.SEGUNDA_COMIDA_ES = 2 THEN 1 ELSE 0 END) AS TOTAL_SEGUNDA_COMIDA, " +
                        " SUM(EC_V_PERSONAS_COMIDA.SEGUNDA_COMIDA_COSTO) AS TOTAL_SEGUNDA_COMIDA_COSTO, " +
                        " COUNT(EC_V_PERSONAS_COMIDA.TIPO_COMIDA_ID) AS TOTAL_TIPO_COMIDA, " +
                        " SUM(EC_V_PERSONAS_COMIDA.TIPO_COMIDA_COSTOA) AS TOTAL_COMIDA_COSTO ";
    /// <summary>
    /// 
    /// </summary>
    static string CamposCentroCostos = " EC_PERSONAS_DATOS.DIVISION, EC_PERSONAS_DATOS.CENTRO_DE_COSTOS ";

    /// <summary>
    /// Campos a mostrar en los reportes de Detallados de Comida
    /// </summary>
    static string CamposDetalleComida = "EC_PERSONAS.PERSONA_NOMBRE, EC_PERSONAS.AGRUPACION_NOMBRE, EC_V_PERSONAS_COMIDA.PERSONA_COMIDA_FECHA, " +
                                        "EC_TIPO_COBRO.TIPO_COBRO, EC_V_PERSONAS_COMIDA.TIPO_COMIDA_COSTOA ";
    static string CamposSubTotal = " SUM(CASE WHEN EC_V_PERSONAS_COMIDA.TIPO_COMIDA_ID >=1 THEN 1 ELSE 0 END) AS NUMERO_COMIDAS, " +
                                    " (SUM(CASE WHEN EC_V_PERSONAS_COMIDA.TIPO_COMIDA_ID >=1 THEN 1 ELSE 0 END) * (SELECT TIPO_COMIDA_COSTO FROM EC_TIPO_COMIDA WHERE TIPO_COMIDA_ID = '2')) AS PRECIO ";
    public Cec_Comida()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    /// <summary>
    /// Crea el reporte de Comidas por Agrupación o por Empleado. Valida la agrupación y toma el periodo de fechas que se seleccionaron.
    /// </summary>
    /// <param name="Persona_ID">ID de la Persona</param>
    /// <param name="Agrupacion">Nombre de la agrupación a validar.</param>
    /// <param name="FechaInicial">Fecha inicial del periodo</param>
    /// <param name="FechaFinal">Fecha final del periodo</param>
    /// <param name="Usuario_ID">ID del usuario de la Sesión actual.</param>
    /// <returns>Regresa un DataSet con los datos de las comidas por agrupacion o empleado</returns>
    public static DataSet ReporteComidaEmpleado(int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, int Usuario_ID)
    {
        string Campos = "";
        string OrdenarPor = "";
        Campos = "EC_PERSONAS.PERSONA_LINK_ID, " +
                        CamposComidaEmpleado;
        OrdenarPor = "EC_PERSONAS.PERSONA_LINK_ID";
        //if (Persona_ID > 0)
        //{
        //    Campos = CamposComidaEmpleado;
        //    OrdenarPor = "EC_PERSONAS.PERSONA_LINK_ID";
        //}
        //else
        //{
        //    Campos = "EC_PERSONAS.PERSONA_LINK_ID, " +
        //                CamposComidaEmpleado;
        //    OrdenarPor = "EC_PERSONAS.PERSONA_LINK_ID";
        //}

        string Qry = " SELECT    " + Campos + " " +
            "FROM EC_V_PERSONAS_COMIDA INNER JOIN " +
                " EC_PERSONAS ON EC_V_PERSONAS_COMIDA.PERSONA_ID = EC_PERSONAS.PERSONA_ID " +
                " INNER JOIN EC_PERSONAS_DATOS ON EC_PERSONAS_DATOS.PERSONA_ID = EC_PERSONAS.PERSONA_ID " +
            " WHERE PERSONA_COMIDA_FECHA   >= @FECHA_INICIAL@ AND PERSONA_COMIDA_FECHA < @FECHA_FINAL@ AND EC_PERSONAS." +
        ValidaAgrupacion(Persona_ID, Usuario_ID, Agrupacion, true) +
        " GROUP BY EC_PERSONAS.PERSONA_LINK_ID, EC_PERSONAS.PERSONA_NOMBRE, EC_PERSONAS.AGRUPACION_NOMBRE, EC_PERSONAS_DATOS.TIPO_NOMINA " +
        " \n ORDER BY " + OrdenarPor;
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_INICIAL", FechaInicial);
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_FINAL", FechaFinal.AddDays(1));
        return (DataSet)CeC_BD.EjecutaDataSet(Qry);
    }
    /// <summary>
    /// Crea el reporte detallado de Comidas por Agrupacion o Empleado.
    /// </summary>
    /// <param name="Persona_ID">ID de la Persona</param>
    /// <param name="Agrupacion">Nombre de la agrupación a validar.</param>
    /// <param name="FechaInicial">Fecha inicial del periodo</param>
    /// <param name="FechaFinal">Fecha final del periodo</param>
    /// <param name="Usuario_ID">ID del usuario de la Sesión actual.</param>
    /// <returns>Regresa un DataSet con los datos detallador de las comidas por agrupación o empleado</returns>
    public static DataSet ReporteDetalleComida(int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, int Usuario_ID)
    {
        string Campos = "";
        string OrdenarPor = "";
        Campos = "EC_PERSONAS.PERSONA_LINK_ID, " +
                    CamposDetalleComida;
        OrdenarPor = "EC_PERSONAS.PERSONA_NOMBRE";
        //if (Persona_ID > 0)
        //{
        //    Campos = CamposDetalleComida;
        //    OrdenarPor = "EC_PERSONAS.PERSONA_NOMBRE";
        //}
        //else
        //{
        //    Campos = "EC_PERSONAS.PERSONA_LINK_ID, " + 
        //            CamposDetalleComida;
        //    OrdenarPor = "EC_PERSONAS.PERSONA_NOMBRE";
        //}

        string Qry = " SELECT    " + Campos + " " +
            "FROM EC_PERSONAS INNER JOIN EC_V_PERSONAS_COMIDA ON EC_V_PERSONAS_COMIDA.PERSONA_ID = EC_PERSONAS.PERSONA_ID " +
                            " INNER JOIN EC_TIPO_COMIDA ON EC_TIPO_COMIDA.TIPO_COMIDA_ID = EC_V_PERSONAS_COMIDA.TIPO_COMIDA_ID " +
                            " INNER JOIN EC_TIPO_COBRO ON EC_V_PERSONAS_COMIDA.TIPO_COBRO_ID = EC_TIPO_COBRO.TIPO_COBRO_ID " +
            " WHERE PERSONA_COMIDA_FECHA   >= @FECHA_INICIAL@ AND PERSONA_COMIDA_FECHA < @FECHA_FINAL@ AND EC_PERSONAS." +
        ValidaAgrupacion(Persona_ID, Usuario_ID, Agrupacion, true) +
        " \n ORDER BY " + OrdenarPor;
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_INICIAL", FechaInicial);
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_FINAL", FechaFinal.AddDays(1));
        return (DataSet)CeC_BD.EjecutaDataSet(Qry);
    }
    /// <summary>
    /// Obtiene el reporte Personalizado para comidas. Los campos personalizados de configuran en la tabla EC_CONFIG_USUARIO de la forma:
    /// NOMBRE_DE_LA_TABLA.NOMBRE_DEL_CAMPO
    /// </summary>
    /// <param name="Persona_ID">ID de la Persona</param>
    /// <param name="Agrupacion">Nombre de la agrupación a validar.</param>
    /// <param name="FechaInicial">Fecha inicial del periodo</param>
    /// <param name="FechaFinal">Fecha final del periodo</param>
    /// <param name="Usuario_ID">ID del usuario de la Sesión actual.</param>
    /// <returns>Regresa un DataSet con los datos detallador de las comidas por agrupación o empleado</returns>
    public static DataSet ReporteEmpresa(int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, int Usuario_ID, CeC_Sesion Sesion)
    {
        string Select = " SELECT ";
        string Campos = "";
        string CampoPersonalizado = Sesion.ConfiguraSuscripcion.Comida_Campo_Agrupacion;
        string OrdenarPor = " \n ORDER BY ";
        string AgruparPor = " GROUP BY ";

        if (Sesion.ConfiguraSuscripcion.Comida_Campo_Agrupacion != "")
        {
            CampoPersonalizado += ", ";
            AgruparPor += Sesion.ConfiguraSuscripcion.Comida_Campo_Agrupacion;
            OrdenarPor += Sesion.ConfiguraSuscripcion.Comida_Campo_Agrupacion;
        }
        else
        {
            CampoPersonalizado = "";
            OrdenarPor = "";
            AgruparPor = "";
        }

        Campos =    //" DIVISION AS EMPRESA, " +
                    CampoPersonalizado +
                    " SUM(CASE WHEN EC_V_PERSONAS_COMIDA.PRIMERA_COMIDA_ES = 1 THEN 1 ELSE 0 END) AS TOTAL_PRIMERA_COMIDA, " +
                    " SUM(EC_V_PERSONAS_COMIDA.PRIMERA_COMIDA_COSTO) AS TOTAL_PRIMERA_COMIDA_COSTO, " +
                    " SUM(CASE WHEN EC_V_PERSONAS_COMIDA.SEGUNDA_COMIDA_ES = 2 THEN 1 ELSE 0 END) AS TOTAL_SEGUNDA_COMIDA, " +
                    " SUM(EC_V_PERSONAS_COMIDA.SEGUNDA_COMIDA_COSTO) AS TOTAL_SEGUNDA_COMIDA_COSTO, " +
                    " COUNT(EC_V_PERSONAS_COMIDA.TIPO_COMIDA_ID) AS TOTAL_TIPO_COMIDA, " +
                    " SUM(EC_V_PERSONAS_COMIDA.TIPO_COMIDA_COSTOA) AS TOTAL_COMIDA_COSTO ";

        string Qry = Select + Campos + " " +
            " FROM EC_V_PERSONAS_COMIDA INNER JOIN EC_PERSONAS ON EC_V_PERSONAS_COMIDA.PERSONA_ID = EC_PERSONAS.PERSONA_ID " +
                                      " INNER JOIN EC_PERSONAS_DATOS ON EC_PERSONAS_DATOS.PERSONA_ID = EC_PERSONAS.PERSONA_ID " +
            " WHERE PERSONA_COMIDA_FECHA   >= @FECHA_INICIAL@ AND PERSONA_COMIDA_FECHA < @FECHA_FINAL@ AND EC_PERSONAS." +
        ValidaAgrupacion(Persona_ID, Usuario_ID, Agrupacion, true) +
        AgruparPor +
        OrdenarPor;
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_INICIAL", FechaInicial);
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_FINAL", FechaFinal.AddDays(1));
        return (DataSet)CeC_BD.EjecutaDataSet(Qry);
    }
    /// <summary>
    /// Muestra el reporte por centro de costos.
    /// </summary>
    /// <param name="Persona_ID"></param>
    /// <param name="Agrupacion"></param>
    /// <param name="FechaInicial"></param>
    /// <param name="FechaFinal"></param>
    /// <param name="Usuario_ID"></param>
    /// <param name="Sesion"></param>
    /// <returns>DataSet con la tabla que contiene el reporte.</returns>
    public static DataSet ReporteCentroCostos(int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, int Usuario_ID, CeC_Sesion Sesion)
    {
        string Select = " SELECT ";
        string Campos = "";
        string OrdenarPor = " \n ORDER BY ";
        string AgruparPor = " GROUP BY ";

        Campos = CamposCentroCostos + " , " +
                " SUM(CASE WHEN EC_V_PERSONAS_COMIDA.TIPO_COMIDA_ID >=1 THEN 1 ELSE 0 END) AS NUMERO_COMIDAS, " +
                " (SUM(CASE WHEN EC_V_PERSONAS_COMIDA.TIPO_COMIDA_ID >=1 THEN 1 ELSE 0 END) * (SELECT TIPO_COMIDA_COSTO FROM EC_TIPO_COMIDA WHERE TIPO_COMIDA_ID = '2')) AS PRECIO ";
        string Qry = Select + Campos + " " +
            " FROM EC_V_PERSONAS_COMIDA INNER JOIN EC_PERSONAS_DATOS ON EC_PERSONAS_DATOS.PERSONA_ID = EC_V_PERSONAS_COMIDA.PERSONA_ID " +
                                      " INNER JOIN EC_PERSONAS ON EC_V_PERSONAS_COMIDA.PERSONA_ID = EC_PERSONAS.PERSONA_ID " +
            " WHERE PERSONA_COMIDA_FECHA   >= @FECHA_INICIAL@ AND PERSONA_COMIDA_FECHA < @FECHA_FINAL@ AND EC_PERSONAS." +
        ValidaAgrupacion(Persona_ID, Usuario_ID, Agrupacion, true) +
        AgruparPor + CamposCentroCostos +
        OrdenarPor + CamposCentroCostos;
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_INICIAL", FechaInicial);
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_FINAL", FechaFinal.AddDays(1));
        return (DataSet)CeC_BD.EjecutaDataSet(Qry);
    }
    public static DataSet ReportePersonalizado(int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, int Usuario_ID, CeC_Sesion Sesion)
    {
        string Select = " SELECT ";
        string Campos = "";
        string CampoPersonalizado = Sesion.ConfiguraSuscripcion.Comida_Campo_Agrupacion;
        string OrdenarPor = " \n ORDER BY ";
        string AgruparPor = " GROUP BY ";

        if (Sesion.ConfiguraSuscripcion.Comida_Campo_Agrupacion != "")
        {
            CampoPersonalizado += ", ";
            AgruparPor += Sesion.ConfiguraSuscripcion.Comida_Campo_Agrupacion;
            OrdenarPor += Sesion.ConfiguraSuscripcion.Comida_Campo_Agrupacion;
        }
        else
        {
            CampoPersonalizado = "";
            OrdenarPor = "";
            AgruparPor = "";
        }

        Campos =
                CampoPersonalizado +
                " SUM(CASE WHEN EC_V_PERSONAS_COMIDA.PRIMERA_COMIDA_ES = 1 THEN 1 ELSE 0 END) AS TOTAL_PRIMERA_COMIDA, " +
                " SUM(EC_V_PERSONAS_COMIDA.PRIMERA_COMIDA_COSTO) AS TOTAL_PRIMERA_COMIDA_COSTO, " +
                " SUM(CASE WHEN EC_V_PERSONAS_COMIDA.SEGUNDA_COMIDA_ES = 2 THEN 1 ELSE 0 END) AS TOTAL_SEGUNDA_COMIDA, " +
                " SUM(EC_V_PERSONAS_COMIDA.SEGUNDA_COMIDA_COSTO) AS TOTAL_SEGUNDA_COMIDA_COSTO, " +
                " COUNT(EC_V_PERSONAS_COMIDA.TIPO_COMIDA_ID) AS TOTAL_TIPO_COMIDA, " +
                " SUM(EC_V_PERSONAS_COMIDA.TIPO_COMIDA_COSTOA) AS TOTAL_COMIDA_COSTO ";

        string Qry = Select + Campos + " " +
            " FROM EC_V_PERSONAS_COMIDA INNER JOIN EC_PERSONAS ON EC_V_PERSONAS_COMIDA.PERSONA_ID = EC_PERSONAS.PERSONA_ID " +
                                      " INNER JOIN EC_PERSONAS_DATOS ON EC_PERSONAS_DATOS.PERSONA_ID = EC_PERSONAS.PERSONA_ID " +
            " WHERE PERSONA_COMIDA_FECHA   >= @FECHA_INICIAL@ AND PERSONA_COMIDA_FECHA < @FECHA_FINAL@ AND EC_PERSONAS." +
        ValidaAgrupacion(Persona_ID, Usuario_ID, Agrupacion, true) +
        AgruparPor +
        OrdenarPor;
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_INICIAL", FechaInicial);
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_FINAL", FechaFinal.AddDays(1));
        return (DataSet)CeC_BD.EjecutaDataSet(Qry);
    }
    /// <summary>
    /// Valida la agrupación que se va a mostrar
    /// </summary>
    /// <param name="Persona_ID">ID de la Persona</param>
    /// <param name="Usuario_ID">ID del usuario de la Sesión actual.</param>
    /// <param name="Agrupacion">Nombre de la agrupación a validar.</param>
    /// <param name="IncluirPersonaID_IN"></param>
    /// <returns></returns>
    public static string ValidaAgrupacion(int Persona_ID, int Usuario_ID, string Agrupacion, bool IncluirPersonaID_IN)
    {
        return ValidaAgrupacion(Persona_ID.ToString(), Usuario_ID, Agrupacion, IncluirPersonaID_IN);
    }
    /// <summary>
    /// Valida la agrupación que se va a mostrar
    /// </summary>
    /// <param name="PersonasIDs"></param>
    /// <param name="Usuario_ID">ID del usuario de la Sesión actual.</param>
    /// <param name="Agrupacion">Nombre de la agrupación a validar.</param>
    /// <param name="IncluirPersonaID_IN"></param>
    /// <returns></returns>
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
    public static DataSet SubtotalPorEmpresa(int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, int Usuario_ID)
    {
        string SubTotal = "";
        SubTotal =
                " EC_PERSONAS_DATOS.DIVISION, " + CamposSubTotal;
        //" EC_PERSONAS_DATOS.CENTRO_DE_COSTOS, " +
        //" SUM(EC_V_PERSONAS_COMIDA.TIPO_COMIDA_COSTOA) AS PRECIO ";
        //" SUM(CASE WHEN EC_V_PERSONAS_COMIDA.SEGUNDA_COMIDA_ES = 2 THEN 1 ELSE 0 END) AS TOTAL_SEGUNDA_COMIDA, " +
        //" SUM(EC_V_PERSONAS_COMIDA.SEGUNDA_COMIDA_COSTO) AS TOTAL_SEGUNDA_COMIDA_COSTO ";

        string Qry = " SELECT " + SubTotal + " " +
            " FROM   EC_V_PERSONAS_COMIDA INNER JOIN EC_PERSONAS_DATOS ON EC_V_PERSONAS_COMIDA.PERSONA_ID = EC_PERSONAS_DATOS.PERSONA_ID " +
                                        " INNER JOIN EC_PERSONAS ON EC_PERSONAS.PERSONA_ID = EC_V_PERSONAS_COMIDA.PERSONA_ID   " +
            " WHERE PERSONA_COMIDA_FECHA   >= @FECHA_INICIAL@ AND PERSONA_COMIDA_FECHA < @FECHA_FINAL@ AND EC_PERSONAS." +
        ValidaAgrupacion(Persona_ID, Usuario_ID, Agrupacion, true) +
            " GROUP BY " + " EC_PERSONAS_DATOS.DIVISION " +
            " \n ORDER BY " + " EC_PERSONAS_DATOS.DIVISION ";   //, EC_PERSONAS_DATOS.CENTRO_DE_COSTOS ";
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_INICIAL", FechaInicial);
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_FINAL", FechaFinal.AddDays(1));
        return (DataSet)CeC_BD.EjecutaDataSet(Qry);
    }

    public static DataSet TotalPorEmpresa(int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, int Usuario_ID)
    {
        string Total = "";
        decimal SegundaComidaCosto = CeC_BD.EjecutaEscalarDecimal("SELECT TIPO_COMIDA_COSTO FROM EC_TIPO_COMIDA WHERE TIPO_COMIDA_ID = '2'");
        Total = " SUM(CASE WHEN EC_V_PERSONAS_COMIDA.TIPO_COMIDA_ID >=1 THEN 1 ELSE 0 END) AS NUMERO_COMIDAS,  " +
                    "(SUM(CASE WHEN EC_V_PERSONAS_COMIDA.TIPO_COMIDA_ID >=1 THEN 1 ELSE 0 END) * '" + SegundaComidaCosto + "') AS PRECIO ";
        //" SUM(CASE WHEN EC_V_PERSONAS_COMIDA.SEGUNDA_COMIDA_ES = 2 THEN 1 ELSE 0 END) AS TOTAL_SEGUNDA_COMIDA, " +
        //" SUM(EC_V_PERSONAS_COMIDA.SEGUNDA_COMIDA_COSTO) AS TOTAL_SEGUNDA_COMIDA_COSTO ";
        string Qry = " SELECT    " + Total + " " +
            " FROM   EC_V_PERSONAS_COMIDA INNER JOIN EC_PERSONAS_DATOS ON EC_V_PERSONAS_COMIDA.PERSONA_ID = EC_PERSONAS_DATOS.PERSONA_ID " +
                                        " INNER JOIN EC_PERSONAS ON EC_PERSONAS.PERSONA_ID = EC_PERSONAS_DATOS.PERSONA_ID   " +
            " WHERE PERSONA_COMIDA_FECHA   >= @FECHA_INICIAL@ AND PERSONA_COMIDA_FECHA < @FECHA_FINAL@ AND EC_PERSONAS." +
        ValidaAgrupacion(Persona_ID, Usuario_ID, Agrupacion, true);
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_INICIAL", FechaInicial);
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_FINAL", FechaFinal.AddDays(1));
        return (DataSet)CeC_BD.EjecutaDataSet(Qry);
    }

    public static DataSet SubtotalPorCentroCostos(int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, int Usuario_ID)
    {
        string SubTotal = "";
        SubTotal =
                " EC_PERSONAS_DATOS.CENTRO_DE_COSTOS, " +
            //" EC_PERSONAS_DATOS.CENTRO_DE_COSTOS, " +
                " SUM(CASE WHEN EC_V_PERSONAS_COMIDA.TIPO_COMIDA_ID >=1 THEN 1 ELSE 0 END) AS TOTAL_COMIDA, " +
                " (SUM(CASE WHEN EC_V_PERSONAS_COMIDA.TIPO_COMIDA_ID >=1 THEN 1 ELSE 0 END) * (SELECT TIPO_COMIDA_COSTO FROM EC_TIPO_COMIDA WHERE TIPO_COMIDA_ID = '2')) AS TOTAL_COMIDA_COSTO ";
        //" SUM(EC_V_PERSONAS_COMIDA.TIPO_COMIDA_COSTOA) AS TOTAL_COMIDA_COSTO ";
        //" SUM(CASE WHEN EC_V_PERSONAS_COMIDA.SEGUNDA_COMIDA_ES = 2 THEN 1 ELSE 0 END) AS TOTAL_SEGUNDA_COMIDA, " +
        //" SUM(EC_V_PERSONAS_COMIDA.SEGUNDA_COMIDA_COSTO) AS TOTAL_SEGUNDA_COMIDA_COSTO ";

        string Qry = " SELECT " + SubTotal + " " +
            " FROM   EC_V_PERSONAS_COMIDA INNER JOIN EC_PERSONAS_DATOS ON EC_V_PERSONAS_COMIDA.PERSONA_ID = EC_PERSONAS_DATOS.PERSONA_ID " +
                                        " INNER JOIN EC_PERSONAS ON EC_PERSONAS.PERSONA_ID = EC_PERSONAS_DATOS.PERSONA_ID   " +
            " WHERE PERSONA_COMIDA_FECHA   >= @FECHA_INICIAL@ AND PERSONA_COMIDA_FECHA < @FECHA_FINAL@ AND EC_PERSONAS." +
        ValidaAgrupacion(Persona_ID, Usuario_ID, Agrupacion, true) +
            " GROUP BY " + " EC_PERSONAS_DATOS.CENTRO_DE_COSTOS ";//, EC_PERSONAS_DATOS.CENTRO_DE_COSTOS ";
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_INICIAL", FechaInicial);
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_FINAL", FechaFinal.AddDays(1));
        return (DataSet)CeC_BD.EjecutaDataSet(Qry);
    }

    public static DataSet TotalPorCentroCostos(int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, int Usuario_ID)
    {
        string Total = "";
        Total =
                " SUM(CASE WHEN EC_V_PERSONAS_COMIDA.TIPO_COMIDA_ID >=1 THEN 1 ELSE 0 END) AS TOTAL_COMIDA, " +
                " (SUM(CASE WHEN EC_V_PERSONAS_COMIDA.TIPO_COMIDA_ID >=1 THEN 1 ELSE 0 END) * (SELECT TIPO_COMIDA_COSTO FROM EC_TIPO_COMIDA WHERE TIPO_COMIDA_ID = '2')) AS TOTAL_COMIDA_COSTO ";
        //" SUM(EC_V_PERSONAS_COMIDA.TIPO_COMIDA_COSTOA) AS TOTAL_COMIDA_COSTO ";
        //" SUM(CASE WHEN EC_V_PERSONAS_COMIDA.SEGUNDA_COMIDA_ES = 2 THEN 1 ELSE 0 END) AS TOTAL_SEGUNDA_COMIDA, " +
        //" SUM(EC_V_PERSONAS_COMIDA.SEGUNDA_COMIDA_COSTO) AS TOTAL_SEGUNDA_COMIDA_COSTO ";
        string Qry = " SELECT    " + Total + " " +
            " FROM   EC_V_PERSONAS_COMIDA INNER JOIN EC_PERSONAS_DATOS ON EC_V_PERSONAS_COMIDA.PERSONA_ID = EC_PERSONAS_DATOS.PERSONA_ID " +
                                        " INNER JOIN EC_PERSONAS ON EC_PERSONAS.PERSONA_ID = EC_PERSONAS_DATOS.PERSONA_ID   " +
            " WHERE PERSONA_COMIDA_FECHA   >= @FECHA_INICIAL@ AND PERSONA_COMIDA_FECHA < @FECHA_FINAL@ AND EC_PERSONAS." +
        ValidaAgrupacion(Persona_ID, Usuario_ID, Agrupacion, true);
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_INICIAL", FechaInicial);
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_FINAL", FechaFinal.AddDays(1));
        return (DataSet)CeC_BD.EjecutaDataSet(Qry);
    }
}