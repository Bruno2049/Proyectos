using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using System.Reflection;

/// <summary>
/// Descripción breve de CMd_Base
/// </summary>
public class CMd_Base
{
    public CMd_Base()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    /// <summary>
    /// Agrega o crea modulos a la Base de datos
    /// </summary>
    /// <returns></returns>
    public static bool CreaModulos()
    {
        AgregaModulo(1, typeof(CMd_AdamAs400_Vac).ToString());
        AgregaModulo(3, typeof(CMd_Mails).ToString());
        AgregaModulo(4, typeof(CMd_Adam5SQL).ToString());
        AgregaModulo(5, typeof(CMd_Sicoss2).ToString());
        AgregaModulo(6, typeof(CMd_BahiaPrinc).ToString());
        AgregaModulo(7, typeof(CMd_OracleBS).ToString());
        AgregaModulo(100, typeof(CMd_Query).ToString());
        return AgregaModulo(2, typeof(CMd_Sicoss).ToString());

    }
    /// <summary>
    /// Regresa u obtiene los modulos de la base
    /// </summary>
    /// <returns></returns>
    private static CMd_Base[] ObtenModulos()
    {
        DataSet DS = (DataSet)CeC_BD.EjecutaDataSet("SELECT CONFIG_USUARIO_VALOR FROM EC_CONFIG_USUARIO WHERE USUARIO_ID = 0 AND CONFIG_USUARIO_VARIABLE LIKE '" +
             "CMd_Base.MODULO_" + "%' ORDER BY CONFIG_USUARIO_VARIABLE");
        if (DS == null)
            return null;
        if (DS.Tables.Count <= 0)
            return null;
        if (DS.Tables[0].Rows.Count <= 0)
            return null;

        CMd_Base[] Modulos = new CMd_Base[DS.Tables[0].Rows.Count];
        int Modulo = 0;
        foreach (DataRow DR in DS.Tables[0].Rows)
        {
            try
            {
                Modulos[Modulo] = EstaModuloHabilitado(DR[0].ToString());
            }
            catch (Exception ex)
            {
                CIsLog2.AgregaError(ex);
                //throw;
            }
            Modulo++;
        }
        return Modulos;
    }
    public static CMd_Base CreaClase(string NombreModulo)
    {
        try
        {
            CMd_Base Modulo = (CMd_Base)Assembly.GetExecutingAssembly().CreateInstance(NombreModulo);
            return Modulo;
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return null;
    }
    public static CMd_Base EstaModuloHabilitado(string NombreModulo)
    {
        try
        {
            CMd_Base Modulo = CreaClase(NombreModulo);
            if (!Modulo.EstaHabilitado())
                Modulo = null;
            else
            {
                Modulo.CargaParametros();
                Modulo.GuardaParametros();
            }
            return Modulo;
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
            //throw;
        }
        return null;
    }

    /// <summary>
    /// Se ejecuta esta función una vez al dia despues de procesar faltas
    /// </summary>
    /// <returns></returns>
    public static bool gEjecutarUnaVezAlDia()
    {
        CMd_Base[] Modulos = ObtenModulos();
        if (Modulos == null)
            return false;
        CIsLog2.AgregaLog("gEjecutarUnaVezAlDia ");
        for (int Cont = 0; Cont < Modulos.Length; Cont++)
            if (Modulos[Cont] != null)
            {
                try
                {
                    Modulos[Cont].EjecutarUnaVezAlDia();
                }
                catch (Exception ex)
                {
                    CIsLog2.AgregaError(ex);
                }
            }
        return true;
    }

    /// <summary>
    /// Se ejecuta esta función una vez cada hora despues de procesar faltas
    /// </summary>
    /// <returns></returns>
    public static bool gEjecutarUnaVezCadaHora()
    {
        CMd_Base[] Modulos = ObtenModulos();
        if (Modulos == null)
            return false;

        for (int Cont = 0; Cont < Modulos.Length; Cont++)
            if (Modulos[Cont] != null)
            {
                try
                {
                    Modulos[Cont].EjecutarUnaVezCadaHora();
                }
                catch (Exception ex)
                {
                    CIsLog2.AgregaError(ex);
                }
            }
        return true;
    }

    public static bool gActualizaTiposIncidencias(int SuscripcionID)
    {
        CMd_Base[] Modulos = ObtenModulos();
        if (Modulos == null)
            return false;

        for (int Cont = 0; Cont < Modulos.Length; Cont++)
            if (Modulos[Cont] != null)
            {
                try
                {
                    Modulos[Cont].ActualizaTiposIncidencias(SuscripcionID);
                }
                catch (Exception ex)
                {
                    CIsLog2.AgregaError(ex);
                }
            }
        return true;
    }

    public static bool gActualizaEmpleados(int SuscripcionID, bool Manual)
    {
        CMd_Base[] Modulos = ObtenModulos();
        if (Modulos == null)
            return false;

        for (int Cont = 0; Cont < Modulos.Length; Cont++)
            if (Modulos[Cont] != null)
            {
                try
                {
                    Modulos[Cont].ActualizaEmpleados(SuscripcionID, Manual);
                }
                catch (Exception ex)
                {
                    CIsLog2.AgregaError(ex);
                }
            }
        return true;
    }
    public static bool gActualizaTurnos(int SuscripcionID, string Nomina)
    {
        bool ret = false;
        CMd_Base[] Modulos = ObtenModulos();
        if (Modulos == null)
            return false;

        for (int Cont = 0; Cont < Modulos.Length; Cont++)
            if (Modulos[Cont] != null)
            {
                try
                {
                    if (Modulos[Cont].ActualizaTurnos(SuscripcionID, Nomina))
                        ret = true;
                    else
                        ret = false;
                }
                catch (Exception ex)
                {
                    CIsLog2.AgregaError(ex);
                }
            }
        return ret;
    }
    public static bool gActualizaTiposNomina(int SuscripcionID)
    {
        CMd_Base[] Modulos = ObtenModulos();
        if (Modulos == null)
            return false;

        for (int Cont = 0; Cont < Modulos.Length; Cont++)
            if (Modulos[Cont] != null)
            {
                try
                {
                    Modulos[Cont].ActualizaTiposNomina(SuscripcionID);
                }
                catch (Exception ex)
                {
                    CIsLog2.AgregaError(ex);
                }
            }
        return true;
    }

    public static bool gEnviaIncidencias(DateTime FechaInicial, DateTime FechaFinal, int PeriodoID, string SQLPersonas)
    {
        CMd_Base[] Modulos = ObtenModulos();
        if (Modulos == null)
            return false;

        for (int Cont = 0; Cont < Modulos.Length; Cont++)
            if (Modulos[Cont] != null)
            {
                try
                {
                    if (!Modulos[Cont].EnviaIncidencias(FechaInicial, FechaFinal, PeriodoID, SQLPersonas))
                        return false;
                }
                catch (Exception ex)
                {
                    CIsLog2.AgregaError(ex);
                }
            }
        return true;
    }

    public static bool gRecibeIncidencias(DateTime FechaInicial, DateTime FechaFinal, string SQLPersonas)
    {
        CMd_Base[] Modulos = ObtenModulos();
        if (Modulos == null)
            return false;

        for (int Cont = 0; Cont < Modulos.Length; Cont++)
            if (Modulos[Cont] != null)
            {
                try
                {
                    if (!Modulos[Cont].RecibeIncidencias(FechaInicial, FechaFinal, SQLPersonas))
                        return false;
                }
                catch (Exception ex)
                {
                    CIsLog2.AgregaError(ex);
                }
            }
        return true;
    }

    /// <summary>
    /// Agrega un módulo a la base de datos
    /// </summary>
    /// <param name="NoModulo">Orden o número de módulo</param>
    /// <param name="ClaseNombre">Nombre de la clase modulo.
    /// </param>
    /// <returns></returns>
    private static bool AgregaModulo(int NoModulo, string ClaseNombre)
    {
        return CeC_Config.GuardaConfig(0, "CMd_Base.MODULO_" + NoModulo.ToString("000"), ClaseNombre);
    }

    /// <summary>
    /// esta función será ejecutada en la clase de asistencias una instante
    /// despues de generar las faltas, y una vez al día
    /// </summary>
    /// <returns></returns>
    public virtual bool EjecutarUnaVezAlDia()
    {
        return true;
    }


    public virtual bool EnviaIncidencias(DateTime FechaInicial, DateTime FechaFinal, int PeriodoID, string SQLPersonas)
    {
        return true;
    }
    public virtual bool RecibeIncidencias(DateTime FechaInicial, DateTime FechaFinal, string SQLPersonas)
    {

        return true;
    }
    /// <summary>
    /// esta función será ejecutada en la clase de asistencias una instante
    /// despues de generar las faltas, y una vez cada hora
    /// </summary>
    /// <returns></returns>
    public virtual bool EjecutarUnaVezCadaHora()
    {
        return true;
    }

    /// <summary>
    /// Esta función se ejecutara al dar click en en boton actualizaTiposIncidencias
    /// </summary>
    /// <returns></returns>
    public virtual bool ActualizaTiposIncidencias(int SuscripcionID)
    {
        return true;
    }

    public virtual bool ActualizaTiposNomina(int SuscripcionID)
    {
        return true;
    }

    /// <summary>
    /// Esta función se ejecutara al dar click en en boton actualizaEmpleados
    /// </summary>
    /// <returns></returns>
    public virtual bool ActualizaEmpleados(int SuscripcionID, bool Manual)
    {
        return true;
    }
    /// <summary>
    /// Esta función se ejecutara al dar click en en boton ActualizaTurnos
    /// </summary>
    /// <param name="SuscripcionID"></param>
    /// <returns></returns>
    public virtual bool ActualizaTurnos(int SuscripcionID, string Nomina)
    {
        return true;
    }
    /// <summary>
    /// Habilita el valor obtenido
    /// </summary>
    /// <returns></returns>
    public bool EstaHabilitado()
    {
        return LeeValor("Habilitado", Habilitado);
    }
    /// <summary>
    /// Carga las propiedades o parametros de la clase
    /// </summary>
    /// <returns></returns>
    public virtual bool CargaParametros()
    {
        PropertyInfo[] Propiedades = this.GetType().GetProperties();

        if (Propiedades == null || Propiedades.Length < 1)
        {
            return false;
        }

        bool IniciadoElemento = false;


        for (int Cont = 0; Cont < Propiedades.Length; Cont++)
        {
            if (Propiedades[Cont].CanWrite)
            {
                object Dato = Propiedades[Cont].GetValue(this, null);
                if (LeeValor(Propiedades[Cont].Name, ref Dato))
                    Propiedades[Cont].SetValue(this, Dato, null);
            }
        }
        return true;
    }
    /// <summary>
    /// Guarda las propiedades o parametros de la clase
    /// </summary>
    /// <returns></returns>
    public virtual bool GuardaParametros()
    {
        bool IniciadoElemento = false;
        PropertyInfo[] Propiedades = this.GetType().GetProperties();

        if (Propiedades == null || Propiedades.Length < 1)
        {
            return false;
        }

        for (int Cont = 0; Cont < Propiedades.Length; Cont++)
        {
            if (Propiedades[Cont].CanWrite)
            {

                object Dato = Propiedades[Cont].GetValue(this, null);
                GuardaValor(Propiedades[Cont].Name, Dato);
            }
        }

        return true;
    }
    /// <summary>
    /// Obtiene el nombre
    /// </summary>
    /// <returns></returns>
    public virtual string LeeNombre()
    {
        return GetType().ToString();
    }

    bool m_Habilitado = false;
    [Description("Indica si estará activo el módulo")]
    [DisplayNameAttribute("Habilitado")]
    public bool Habilitado
    {
        get { return m_Habilitado; }
        set { m_Habilitado = value; }
    }

    /// <summary>
    /// Lee los valores en caso de ser de tipo string,int,bool,datetime
    /// </summary>
    /// <param name="Variable"></param>
    /// <param name="Destino"></param>
    /// <returns></returns>
    private bool LeeValor(string Variable, ref object Destino)
    {
        try
        {
            switch (Destino.GetType().ToString())
            {
                case "System.String":
                    Destino = LeeValor(Variable, Convert.ToString(Destino));
                    break;
                case "System.Int32":
                    Destino = LeeValor(Variable, Convert.ToInt32(Destino));
                    break;
                case "System.Boolean":
                    Destino = LeeValor(Variable, Convert.ToBoolean(Destino));
                    break;
                case "System.DateTime":
                    Destino = LeeValor(Variable, Convert.ToDateTime(Destino));
                    break;
            }
            return true;
        }
        catch
        {
        }
        return false;
    }
    /// <summary>
    /// Guarda los valores en caso de ser de tipo string,int,bool,datetime
    /// </summary>
    /// <param name="Variable"></param>
    /// <param name="Valor"></param>
    /// <returns></returns>
    private bool GuardaValor(string Variable, object Valor)
    {
        try
        {
            switch (Valor.GetType().ToString())
            {
                case "System.String":
                    try
                    {
                        return GuardaValor(Variable, Convert.ToString(Valor));
                    }
                    catch (Exception ex)
                    {
                        CIsLog2.AgregaError("CMd_Base.GuardaValor", ex);
                    }
                    break;
                case "System.Int32":
                    try
                    {
                        return GuardaValor(Variable, Convert.ToInt32(Valor));
                    }
                    catch (Exception ex)
                    {
                        CIsLog2.AgregaError("CMd_Base.GuardaValor", ex);
                    }
                    break;
                case "System.Boolean":
                    try
                    {
                        return GuardaValor(Variable, Convert.ToBoolean(Valor));
                    }
                    catch (Exception ex)
                    {
                        CIsLog2.AgregaError("CMd_Base.GuardaValor", ex);
                    }
                    break;
                case "System.DateTime":
                    try
                    {
                        return GuardaValor(Variable, Convert.ToDateTime(Valor));
                    }
                    catch (Exception ex)
                    {
                        CIsLog2.AgregaError("CMd_Base.GuardaValor", ex);
                    }
                    break;
            }
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError("CMd_Base.GuardaValor", ex);
        }
        return false;
    }
    /// <summary>
    /// Obtiene el valor de configuracion
    /// </summary>
    /// <param name="Variable"></param>
    /// <param name="ValorPredeterminado"></param>
    /// <returns></returns>
    private string LeeValor(string Variable, string ValorPredeterminado)
    {
        return CeC_Config.ObtenConfig(0, GetType().ToString() + "." + Variable, ValorPredeterminado);
    }
    /// <summary>
    /// Guarda el valor de configuracion
    /// </summary>
    /// <param name="Variable"></param>
    /// <param name="Valor"></param>
    /// <returns></returns>
    private bool GuardaValor(string Variable, string Valor)
    {
        return CeC_Config.GuardaConfig(0, GetType().ToString() + "." + Variable, Valor);
    }
    /// <summary>
    /// Obtiene el valor de configuracion
    /// </summary>
    /// <param name="Variable"></param>
    /// <param name="ValorPredeterminado"></param>
    /// <returns></returns>
    private int LeeValor(string Variable, int ValorPredeterminado)
    {
        return CeC_Config.ObtenConfig(0, GetType().ToString() + "." + Variable, ValorPredeterminado);
    }
    /// <summary>
    /// Guarda el valor de configuracion
    /// </summary>
    /// <param name="Variable"></param>
    /// <param name="Valor"></param>
    /// <returns></returns>
    private bool GuardaValor(string Variable, int Valor)
    {
        return CeC_Config.GuardaConfig(0, GetType().ToString() + "." + Variable, Valor);
    }
    /// <summary>
    /// Obtiene el valor de configuracion
    /// </summary>
    /// <param name="Variable"></param>
    /// <param name="ValorPredeterminado"></param>
    /// <returns></returns>
    private bool LeeValor(string Variable, bool ValorPredeterminado)
    {
        return CeC_Config.ObtenConfig(0, GetType().ToString() + "." + Variable, ValorPredeterminado);
    }
    /// <summary>
    /// Guarda el valor de configuracion
    /// </summary>
    /// <param name="Variable"></param>
    /// <param name="Valor"></param>
    /// <returns></returns>
    private bool GuardaValor(string Variable, bool Valor)
    {
        return CeC_Config.GuardaConfig(0, GetType().ToString() + "." + Variable, Valor);
    }
    /// <summary>
    /// Obtiene el valor de configuracion
    /// </summary>
    /// <param name="Variable"></param>
    /// <param name="ValorPredeterminado"></param>
    /// <returns></returns>
    private DateTime LeeValor(string Variable, DateTime ValorPredeterminado)
    {
        return CeC_Config.ObtenConfig(0, GetType().ToString() + "." + Variable, ValorPredeterminado);
    }
    /// <summary>
    /// Guarda el valor de configuracion
    /// </summary>
    /// <param name="Variable"></param>
    /// <param name="Valor"></param>
    /// <returns></returns>
    private bool GuardaValor(string Variable, DateTime Valor)
    {
        return CeC_Config.GuardaConfig(0, GetType().ToString() + "." + Variable, Valor);
    }

    public string ObtenValorCampo(string ValorCampoTexto, DataRow FilaDatos)
    {
        try
        {
            string R = "";
            if (ValorCampoTexto.IndexOf("DIASEMANA(") == 0)
            {
                DayOfWeek Dia = CeC.Convierte2DateTime(FilaDatos[ValorCampoTexto.Substring(10, ValorCampoTexto.Length - 11)]).DayOfWeek;
                /*if (Dia == DayOfWeek.Sunday)
                    return "7";*/
                return CeC.Convierte2String((int)(Dia) + 1);
            }
            if (ValorCampoTexto.IndexOf("DIVHORAS(") == 0)
            {
                //La división es una operación aritmética de descomposición que consiste en averiguar cuántas veces un número (el divisor) está contenido en otro número (el dividendo).
                string Valor = ValorCampoTexto.Substring(9, ValorCampoTexto.Length - 10);
                string[] Valores = CeC.ObtenArregoSeparador(Valor, ",");
                DateTime Dividendo = CeC.Convierte2DateTime(FilaDatos[Valores[0]]);
                DateTime Divisor = CeC.Convierte2DateTime(FilaDatos[Valores[1]]);
                return CeC.Convierte2Double(CeC_BD.DateTime2TimeSpan(Dividendo).TotalHours / CeC_BD.DateTime2TimeSpan(Divisor).TotalHours).ToString("0.000");
            }
            if (ValorCampoTexto.IndexOf("HORAS(") == 0)
            {
                DateTime Fecha = CeC.Convierte2DateTime(FilaDatos[ValorCampoTexto.Substring(6, ValorCampoTexto.Length - 7)]);
                return CeC_BD.DateTime2TimeSpan(Fecha).TotalHours.ToString("0.000");
            }
            if (ValorCampoTexto.IndexOf("MINUTOS(") == 0)
            {
                DateTime Fecha = CeC.Convierte2DateTime(FilaDatos[ValorCampoTexto.Substring(8, ValorCampoTexto.Length - 9)]);
                return CeC_BD.DateTime2TimeSpan(Fecha).TotalMinutes.ToString("0");
            }
            if (ValorCampoTexto.IndexOf("FECHA(") == 0)
            {
                DateTime Fecha = CeC.Convierte2DateTime(FilaDatos[ValorCampoTexto.Substring(6, ValorCampoTexto.Length - 7)]);
                return Fecha.ToString("dd/MM/yyyy");
            }
            if (ValorCampoTexto.IndexOf("COMEN_") == 0)
            {
                string Comentarios = CeC.Convierte2String(FilaDatos["INCIDENCIA_COMENTARIO"]);
                if (ValorCampoTexto.IndexOf("COMEN_TS2HORAS(") == 0)
                {
                    TimeSpan TSHoras = CeC.Convierte2TimeSpan(CeC.ObtenValorJson(Comentarios,ValorCampoTexto.Substring(15, ValorCampoTexto.Length - 16),""));
                    return TSHoras.TotalHours.ToString("0.000");
                    //return CeC_BD.DateTime2TimeSpan(Fecha).TotalHours.ToString("0.000");
                }
                if (ValorCampoTexto.IndexOf("COMEN_TEXTO(") == 0)
                {
                    return CeC.ObtenValorJson(Comentarios, ValorCampoTexto.Substring(12, ValorCampoTexto.Length - 13), "");
                    
                    //return CeC_BD.DateTime2TimeSpan(Fecha).TotalHours.ToString("0.000");
                }
            }
            if (FilaDatos.Table.Columns.IndexOf(ValorCampoTexto) >= 0)
                return CeC.Convierte2String(FilaDatos[ValorCampoTexto]);

        }
        catch (Exception ex) { CIsLog2.AgregaError(ex); }
        return ValorCampoTexto;
    }
    public virtual string ObtenQryIncidenciaseClock(DateTime Desde, DateTime Hasta, string QryPersonasID)
    {

        string Qry = "SELECT   EC_PERSONAS.PERSONA_ID,  EC_PERSONAS_DATOS.PERSONA_LINK_ID, EC_PERSONAS_DATOS.COMPANIA, EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA, " +
                      "EC_PERSONAS_DIARIO.PERSONA_DIARIO_TT, EC_PERSONAS_DIARIO.PERSONA_DIARIO_TE, EC_PERSONAS_DIARIO.PERSONA_DIARIO_TC,  " +
                      "EC_PERSONAS_DIARIO.PERSONA_DIARIO_TDE, EC_PERSONAS_DIARIO.PERSONA_DIARIO_TES,  " +
                      "EC_V_PERSONAS_DIARIO_EX.TIPO_INCIDENCIAS_EX_ID, EC_V_PERSONAS_DIARIO_EX.TIPO_INCIDENCIAS_EX_TXT,  " +
                      "EC_V_PERSONAS_DIARIO_EX.TIPO_FALTA_EX_ID, EC_V_PERSONAS_DIARIO_EX.TIPO_INCIDENCIAS_EX_PARAM,  " +
                      "EC_V_PERSONAS_DIARIO_EX.INCIDENCIA_COMENTARIO " +
                      "FROM         EC_PERSONAS_DATOS INNER JOIN " +
                      "EC_PERSONAS ON EC_PERSONAS_DATOS.PERSONA_ID = EC_PERSONAS.PERSONA_ID INNER JOIN " +
                      "EC_PERSONAS_DIARIO ON EC_PERSONAS.PERSONA_ID = EC_PERSONAS_DIARIO.PERSONA_ID INNER JOIN " +
                      "EC_V_PERSONAS_DIARIO_EX ON EC_PERSONAS_DIARIO.PERSONA_DIARIO_ID = EC_V_PERSONAS_DIARIO_EX.PERSONA_DIARIO_ID";
        Qry += " WHERE PERSONA_BORRADO = 0 AND EC_PERSONAS_DIARIO.PERSONA_ID IN (" + QryPersonasID +
            ") AND PERSONA_DIARIO_FECHA >= " + CeC_BD.SqlFecha(Desde) + " AND PERSONA_DIARIO_FECHA <= " +
            CeC_BD.SqlFecha(Hasta);
        return Qry;
    }

    public virtual string ObtenQryIncidenciaseClockV5(DateTime Desde, DateTime Hasta, string QryPersonasID)
    {

        string Qry = "SELECT   EC_PERSONAS.PERSONA_ID,  EC_PERSONAS.PERSONA_LINK_ID, EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA, \n" +
                      "EC_PERSONAS_DIARIO.PERSONA_DIARIO_TT, EC_PERSONAS_DIARIO.PERSONA_DIARIO_TE, EC_PERSONAS_DIARIO.PERSONA_DIARIO_TC,  \n" +
                      "EC_PERSONAS_DIARIO.PERSONA_DIARIO_TDE, EC_PERSONAS_DIARIO.PERSONA_DIARIO_TES,  \n" +
                      "EC_V_PERSONAS_DIARIO_EX.TIPO_INCIDENCIAS_EX_ID, EC_V_PERSONAS_DIARIO_EX.TIPO_INCIDENCIAS_EX_TXT,  \n" +
                      "EC_V_PERSONAS_DIARIO_EX.TIPO_FALTA_EX_ID, EC_V_PERSONAS_DIARIO_EX.TIPO_INCIDENCIAS_EX_PARAM,  \n" +
                      "EC_V_PERSONAS_DIARIO_EX.INCIDENCIA_COMENTARIO, EC_PERSONAS_DIARIO.INCIDENCIA_ID \n" +
                      "FROM         EC_PERSONAS INNER JOIN \n" +
                      "EC_PERSONAS_DIARIO ON EC_PERSONAS.PERSONA_ID = EC_PERSONAS_DIARIO.PERSONA_ID INNER JOIN \n" +
                      "EC_V_PERSONAS_DIARIO_EX ON EC_PERSONAS_DIARIO.PERSONA_DIARIO_ID = EC_V_PERSONAS_DIARIO_EX.PERSONA_DIARIO_ID";
        Qry += " WHERE PERSONA_BORRADO = 0 AND EC_PERSONAS_DIARIO.PERSONA_ID IN (" + QryPersonasID +
            ") AND PERSONA_DIARIO_FECHA >= \n" + CeC_BD.SqlFecha(Desde) + " AND PERSONA_DIARIO_FECHA <= \n" +
            CeC_BD.SqlFecha(Hasta);
        return Qry;
    }
}
