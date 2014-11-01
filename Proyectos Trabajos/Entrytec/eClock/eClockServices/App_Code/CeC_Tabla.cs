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
using Newtonsoft.Json;
using System.Collections.Generic;

/// <summary>
/// Descripción breve de CeC_Tabla
/// </summary>
public class CeC_Tabla
{
    /// <summary>
    /// Si es verdadero significa que permitira autonumericos en el ID negativos
    /// y la unica forma para saber si es nuevo es que el campo sea nulo
    /// </summary>
    public bool AutonumericoNegativo = false;
    private CeC_Tablas m_Configuracion = null;
    public const int GeneraAutonumerico = -999999;
    public enum TipoRegistro
    {
        Nuevo = 0,
        Consulta,
        Edicion
    }
    public DataSet m_DSDatos = null;
    /// <summary>
    /// Incializa los campos de la tabla
    /// </summary>
    /// <param name="Tabla"></param>
    /// <param name="CampoLlave">Campo Llave de la tabla</param>
    /// <param name="Sesion"></param>
    public CeC_Tabla(string Tabla, string CampoLlave, CeC_Sesion Sesion)
    {
        m_Tabla = Tabla;
        m_CamposLlave = CampoLlave;
        m_Campos = ObtenCampos(Sesion, TipoRegistro.Consulta);
    }

    public string m_CamposLlave = "";
    public string m_Tabla = "";
    public bool m_EsNuevo = true;
    public string m_Campos = "";
    public string m_QryGuardar = "";
    public string m_UltimoError = "";
    public DataRow m_Fila = null;
    private int m_NoCamposLlave = 0;

    /// <summary>
    /// Propiedad que regresa el número de campos llave
    /// </summary>
    public int NoCamposLlave
    {
        get
        {
            if (m_NoCamposLlave > 0)
                return m_NoCamposLlave;
            string[] sCamposLlave = CeC.ObtenArregoSeparador(m_CamposLlave, ",");
            m_NoCamposLlave = sCamposLlave.Length;
            return m_NoCamposLlave;
        }
    }
    /// <summary>
    /// Obtiene el query para obtener los datos de la tabla
    /// </summary>
    /// <param name="Sesion"></param>
    /// <param name="Where">Contiene el Where de la sentencia, no debe contener la palabra Where</param>
    /// <param name="OrderBy">Idica como se debe ordenar, no debe contener la palabr Order By</param>
    /// <returns>Cadena que contiene la sentencia para obtener los datos de la tabla</returns>
    public virtual string ObtenQry(CeC_Sesion Sesion, string Where, string OrderBy)
    {
        string Qry = "SELECT " + m_Campos + " FROM " + m_Tabla;
        if (Where.Length > 0)
            Qry += " WHERE " + Where;

        if (OrderBy.Length > 0)
            Qry += " \n ORDER BY  " + OrderBy;
        return Qry;
    }


    public virtual string ObtenGridQry(CeC_Sesion Sesion, bool MuestraBorrados)
    {
        string Qry = "SELECT " + m_Campos + " FROM " + m_Tabla + " \n ORDER BY " + m_CamposLlave;
        return Qry;
    }

    public virtual string ObtenExportarQry(CeC_Sesion Sesion, bool MuestraBorrados)
    {
        string Qry = "SELECT " + m_Campos + " FROM " + m_Tabla + " \n ORDER BY " + m_CamposLlave;
        return Qry;
    }

    public virtual DataSet ObtenDS(CeC_Sesion Sesion, string Where, string OrderBy)
    {
        return (DataSet)CeC_BD.EjecutaDataSet(ObtenQry(Sesion, Where, OrderBy));
    }


    public virtual DataSet ObtenGridDS(CeC_Sesion Sesion, bool MuestraBorrados)
    {
        return (DataSet)CeC_BD.EjecutaDataSet(ObtenGridQry(Sesion, MuestraBorrados));
    }

    public virtual bool Carga(string CamposLlave, object Identificador, CeC_Sesion Sesion)
    {
        string Qry = "";

        Qry = "SELECT " + m_Campos + " FROM " + m_Tabla + " WHERE ";
        string Where = "";
        string[] sCamposLlave = CeC.ObtenArregoSeparador(CamposLlave, ",");
        string[] Identificadores = null;
        if (Identificador.GetType().FullName == "System.String")
            Identificadores = new string[] { Identificador.ToString() };
        else
            Identificadores = (string[])Identificador;
        int Pos = 0;
        foreach (string CampoLlave in sCamposLlave)
        {
            Where = CeC.AgregaSeparador(Where, CampoLlave + " = " + Valor2Sql(Identificadores[Pos]) + "", " AND ");
            Pos++;
        }
        Qry += Where;

        DataSet DS = (DataSet)CeC_BD.EjecutaDataSet(Qry);
        if (DS == null || DS.Tables.Count < 1 || DS.Tables[0].Rows.Count < 1)
            return false;
        return Carga(DS.Tables[0].Rows[0], false);
    }
    /// <summary>
    /// Funcion se encarga de cargar los datos de la sesion
    /// </summary>
    /// <param name="Identificador">Objeto idetificador</param>
    /// <param name="Sesion">Sesion actual</param>
    /// <returns></returns>
    public virtual bool Carga(object Identificador, CeC_Sesion Sesion)
    {
        return Carga(m_CamposLlave, Identificador, Sesion);

        string Qry = "";
        if (NoCamposLlave > 1)
        {
            Qry = "SELECT " + m_Campos + " FROM " + m_Tabla + " WHERE ";
            string Where = "";
            string[] sCamposLlave = CeC.ObtenArregoSeparador(m_CamposLlave, ",");
            string[] Identificadores = (string[])Identificador;
            int Pos = 0;
            foreach (string CampoLlave in sCamposLlave)
            {
                Where = CeC.AgregaSeparador(Where, CampoLlave + " = " + Valor2Sql(Identificadores[Pos]) + "", " AND ");
                Pos++;
            }
            Qry += Where;
        }
        else
            Qry = "SELECT " + m_Campos + " FROM " + m_Tabla + " WHERE " + m_CamposLlave + " = " + Valor2Sql(Identificador) + "";
        DataSet DS = (DataSet)CeC_BD.EjecutaDataSet(Qry);
        m_DSDatos = DS;
        if (DS == null || DS.Tables.Count < 1 || DS.Tables[0].Rows.Count < 1)
            return false;
        return Carga(DS.Tables[0].Rows[0]);
    }

    public virtual bool Borrar(object Identificador, CeC_Sesion Sesion)
    {
        string Qry = "";
        Qry = "DELETE FROM " + m_Tabla + " WHERE ";
        string Where = "";
        string[] sCamposLlave = CeC.ObtenArregoSeparador(m_CamposLlave, ",");
        string[] Identificadores = (string[])Identificador;
        int Pos = 0;
        foreach (string CampoLlave in sCamposLlave)
        {
            Where = CeC.AgregaSeparador(Where, CampoLlave + " = " + Valor2Sql(Identificadores[Pos]) + "", " AND ");
            Pos++;
        }
        Qry += Where;
        if (CeC_BD.EjecutaComando(Qry) > 0)
            return true;
        return false;
    }

    public virtual string ObtenCampos(CeC_Sesion Sesion, TipoRegistro Tipo)
    {
        if (m_Campos != "")
            return m_Campos;
        PropertyInfo[] Propiedades = this.GetType().GetProperties();

        if (Propiedades == null || Propiedades.Length < 1)
        {
            return "";
        }
        if (m_Tabla == "")
            return "";
        m_Campos = "";
        for (int Cont = 0; Cont < Propiedades.Length; Cont++)
        {
            if (Propiedades[Cont].CanWrite)
            {
                m_Campos = CeC.AgregaSeparador(m_Campos, Propiedades[Cont].Name, ",");
            }
        }
        return m_Campos;
    }

    /// <summary>
    /// Caga los datos almacenados en un dataRow proveniente de la base de datos
    /// </summary>
    /// <param name="Fila"></param>
    /// <returns></returns>
    public virtual bool Carga(DataRow Fila)
    {
        return Carga(Fila, true);
    }
    /// <summary>
    /// Carga los campos a consultar
    /// </summary>
    /// <returns></returns>
    public virtual bool Carga(DataRow Fila, bool EsNuevo)
    {
        try
        {
            m_Fila = Fila;
            PropertyInfo[] Propiedades = this.GetType().GetProperties();

            if (Propiedades == null || Propiedades.Length < 1)
            {
                return false;
            }

            if (!EsNuevo)
                m_EsNuevo = false;

            for (int Cont = 0; Cont < Propiedades.Length; Cont++)
            {
                if (Propiedades[Cont].CanWrite)
                {
                    object Dato = Propiedades[Cont].GetValue(this, null);
                    if (ObtenValorDR(Propiedades[Cont].Name, ref Dato))
                    {
                        Propiedades[Cont].SetValue(this, Dato, null);
                    }
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return false;
    }

    public virtual bool Guarda(CeC_Sesion Sesion)
    {
        m_UltimoError = "";
        m_QryGuardar = "";

        PropertyInfo[] Propiedades = this.GetType().GetProperties();

        if (Propiedades == null || Propiedades.Length < 1)
        {
            return false;
        }

        bool IniciadoElemento = false;

        //Solo se guardara el nombre del campo llave cuando exista la necesidad de generar el autonumerico
        string CampoLlave = "";
        string[] CamposLlave = CeC.ObtenArregoSeparador(m_CamposLlave, ",");
        if (CamposLlave.Length == 1 && m_EsNuevo)
            CampoLlave = CamposLlave[0];
        string QryPrefijoGuardar = "";
        string QryGuardar = "";
        string QryFiltroUpdate = "";
        string QrySubfijoGuardar = "";
        string QryValores = "";
        string QryValoresInsert = "";
        if (m_EsNuevo)
        {
            QryPrefijoGuardar = "INSERT INTO " + m_Tabla + " (";
            ObtenCampos(Sesion, TipoRegistro.Nuevo);
        }
        else
        {
            QryPrefijoGuardar = "UPDATE " + m_Tabla + " SET ";
            ObtenCampos(Sesion, TipoRegistro.Edicion);
        }

        int Actualizados = 0;

        for (int Cont = 0; Cont < Propiedades.Length; Cont++)
        {
            if (Propiedades[Cont].CanWrite && CeC.ExisteEnSeparador(m_Campos, Propiedades[Cont].Name, ","))
            {

                object Dato = Propiedades[Cont].GetValue(this, null);
                try
                {
                    if (m_EsNuevo && CamposLlave.Length == 1 && Propiedades[Cont].Name.ToUpper() == CamposLlave[0].ToUpper() && Dato.GetType().Name != "String")
                        if (Dato != null || (!AutonumericoNegativo && CeC.Convierte2Int(Dato, 1) < 0))
                        {
                            if (Sesion == null)
                                Dato = CeC_Autonumerico.GeneraAutonumerico(m_Tabla, CamposLlave[0].ToUpper());
                            else
                                Dato = CeC_Autonumerico.GeneraAutonumerico(m_Tabla, CamposLlave[0].ToUpper(), Sesion.SESION_ID, Sesion.SuscripcionParametro);
                            Propiedades[Cont].SetValue(this, Dato, null);
                        }
                }
                catch { }

                string ValorSql = Valor2Sql(Dato);
                if (m_EsNuevo)
                {
                    QryValoresInsert = CeC.AgregaSeparador(QryValoresInsert, Propiedades[Cont].Name, ", ");
                    QryValores = CeC.AgregaSeparador(QryValores, ValorSql, ", ");
                }
                else
                {
                    QryValores = CeC.AgregaSeparador(QryValores, Propiedades[Cont].Name + " = " + ValorSql, ", ");
                    if (CeC.ExisteEnSeparador(m_CamposLlave.ToUpper(), Propiedades[Cont].Name.ToUpper(), ","))
                        QryFiltroUpdate = CeC.AgregaSeparador(QryFiltroUpdate, Propiedades[Cont].Name + " = " + ValorSql, " AND ");
                }
                Actualizados++;
            }
        }
        if (Actualizados < 1)
        {

        }
        if (m_EsNuevo)
            QryGuardar = QryPrefijoGuardar + QryValoresInsert + ") VALUES (" + QryValores + ")";
        else
            QryGuardar = QryPrefijoGuardar + QryValores + " WHERE " + QryFiltroUpdate;
        if (CeC_BD.EjecutaComando(QryGuardar) > 0)
            return true;
        m_UltimoError = CeC_BD.UltimoErrorBD;
        return false;
    }

    #region ObtenValorDRes
    /// <summary>
    /// Lee los valores en caso de ser de tipo string,int,bool,datetime
    /// </summary>
    /// <param name="Variable"></param>
    /// <param name="Destino"></param>
    /// <returns></returns>
    private bool ObtenValorDR(string Variable, ref object Destino)
    {
        try
        {
            switch (Destino.GetType().ToString())
            {
                case "System.String":
                    Destino = ObtenValorDR(Variable, Convert.ToString(Destino));
                    break;
                case "System.Int32":
                    Destino = ObtenValorDR(Variable, Convert.ToInt32(Destino));
                    break;
                case "System.Boolean":
                    Destino = ObtenValorDR(Variable, Convert.ToBoolean(Destino));
                    break;
                case "System.DateTime":
                    Destino = ObtenValorDR(Variable, Convert.ToDateTime(Destino));
                    break;
                case "System.Decimal":
                    Destino = ObtenValorDR(Variable, Convert.ToDecimal(Destino));
                    break;
                case "System.Double":
                    Destino = ObtenValorDR(Variable, Convert.ToDouble(Destino));
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
    public static string Valor2Sql(object Valor)
    {
        string ValorStr = "";
        try
        {

            //Se agregará el código para convertir del tipo de datos origen al tipo de datos destino
            if (Valor != null && Valor != DBNull.Value)
            {
                switch (Valor.GetType().ToString())
                {
                    case "System.String":
                        ValorStr = "'" + CeC_BD.ObtenParametroCadena(CeC.Convierte2String(Valor)) + "'";
                        break;
                    case "System.Int32":
                        ValorStr = CeC.Convierte2Int(Valor, 0).ToString();
                        break;
                    case "System.Int64":
                        ValorStr = CeC.Convierte2Int(Valor, 0).ToString();
                        break;
                    case "System.Boolean":
                        ValorStr = CeC.Convierte2Int(Valor, 0).ToString();
                        break;
                    case "System.DateTime":
                        ValorStr = CeC_BD.SqlFechaHora(CeC.Convierte2DateTime(Valor));
                        break;
                    case "System.Decimal":
                        ValorStr = CeC.Convierte2Decimal(Valor, 0).ToString();
                        break;
                    case "System.Double":
                        ValorStr = CeC.Convierte2Double(Valor, 0).ToString();
                        break;
                    case "Newtonsoft.Json.Linq.JValue":
                        {
                            Newtonsoft.Json.Linq.JValue jValor = (Newtonsoft.Json.Linq.JValue)Valor;

                            switch (jValor.Type)
                            {
                                case Newtonsoft.Json.Linq.JTokenType.Date:
                                    ValorStr = CeC_BD.SqlFechaHora(jValor.ToObject<DateTime>());
                                    break;
                                case Newtonsoft.Json.Linq.JTokenType.String:
                                    ValorStr = "'" + CeC_BD.ObtenParametroCadena(jValor.ToObject<string>()) + "'";
                                    break;
                                case Newtonsoft.Json.Linq.JTokenType.Integer:
                                    ValorStr = jValor.ToObject<long>().ToString();
                                    break;
                                case Newtonsoft.Json.Linq.JTokenType.Boolean:
                                    bool bValor = jValor.ToObject<bool>();
                                    ValorStr = bValor ? "1" : "0";
                                    break;
                                case Newtonsoft.Json.Linq.JTokenType.Float:
                                    ValorStr = jValor.ToObject<decimal>().ToString();
                                    break;
                                default:
                                    ValorStr = "'" + CeC_BD.ObtenParametroCadena(jValor.ToObject<string>()) + "'";
                                    break;

                            }
                        }
                        break;
                }
            }
            else
                ValorStr = "null";


        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return ValorStr;
    }
    /// <summary>
    /// Obtiene el valor string de el data row seleccionado
    /// </summary>
    /// <param name="Variable"></param>
    /// <param name="ValorPredeterminado"></param>
    /// <returns></returns>
    private string ObtenValorDR(string Variable, string ValorPredeterminado)
    {
        return CeC.Convierte2String(m_Fila[Variable], ValorPredeterminado);
    }
    /// <summary>
    /// Obtiene el valor de configuracion
    /// </summary>
    /// <param name="Variable"></param>
    /// <param name="ValorPredeterminado"></param>
    /// <returns></returns>
    private int ObtenValorDR(string Variable, int ValorPredeterminado)
    {
        return CeC.Convierte2Int(m_Fila[Variable], ValorPredeterminado);
    }
    /// <summary>
    /// Obtiene el valor de configuracion
    /// </summary>
    /// <param name="Variable"></param>
    /// <param name="ValorPredeterminado"></param>
    /// <returns></returns>
    private bool ObtenValorDR(string Variable, bool ValorPredeterminado)
    {
        return CeC.Convierte2Bool(m_Fila[Variable], ValorPredeterminado);
    }
    /// <summary>
    /// Obtiene el valor de configuracion
    /// </summary>
    /// <param name="Variable"></param>
    /// <param name="ValorPredeterminado"></param>
    /// <returns></returns>
    private DateTime ObtenValorDR(string Variable, DateTime ValorPredeterminado)
    {
        return CeC.Convierte2DateTime(m_Fila[Variable], ValorPredeterminado);
    }
    /// <summary>
    /// Obtiene el valor de configuracion
    /// </summary>
    /// <param name="Variable"></param>
    /// <param name="ValorPredeterminado"></param>
    /// <returns></returns>
    private Decimal ObtenValorDR(string Variable, Decimal ValorPredeterminado)
    {
        return CeC.Convierte2Decimal(m_Fila[Variable], ValorPredeterminado);
    }
    /// <summary>
    /// Obtiene el valor de configuracion
    /// </summary>
    /// <param name="Variable"></param>
    /// <param name="ValorPredeterminado"></param>
    /// <returns></returns>
    private Double ObtenValorDR(string Variable, Double ValorPredeterminado)
    {
        return CeC.Convierte2Double(m_Fila[Variable], ValorPredeterminado);
    }
    #endregion

    /// <summary>
    /// Obtiene una cadena de caracteres que obiene la información de exportacion
    /// </summary>
    /// <param name="Sesion">Identificador de la sesión</param>
    /// <param name="Separador">se recomieda tabulador \t</param>
    /// <returns>Cadena de caracteres con la información de exportacion</returns>
    public string ExportarPlano(string Separador, CeC_Sesion Sesion, bool MuestraBorrados)
    {
        DataSet DS = (DataSet)CeC_BD.EjecutaDataSet(ObtenExportarQry(Sesion, MuestraBorrados));
        if (DS == null || DS.Tables.Count <= 0)
            return "";
        string Ret = "";
        foreach (DataColumn Columna in DS.Tables[0].Columns)
            Ret = CeC.AgregaSeparador(Ret, Columna.ColumnName, Separador);
        //Ret = CeC.AgregaSeparador(Ret, "", CeC.SaltoLinea);

        foreach (DataRow DR in DS.Tables[0].Rows)
        {
            string Campos = "";
            foreach (Object Objeto in DR.ItemArray)
            {
                Campos = CeC.AgregaSeparador(Campos, Objeto.ToString(), Separador);
            }
            Ret = CeC.AgregaSeparador(Ret, Campos, CeC.SaltoLinea);
        }
        return Ret;
    }

    public int ImportaPlano(string Informacion, string Separador, CeC_Sesion Sesion, ref string Errores)
    {
        try
        {
            int Importados = 0;

            string[] Lineas = CeC.ObtenArregoSeparador(Informacion, CeC.SaltoLinea);
            string[] Columnas = CeC.ObtenArregoSeparador(Lineas[0], Separador);
            DataTable DT = new DataTable(m_Tabla);
            foreach (string Columna in Columnas)
                DT.Columns.Add(Columna);
            for (int Pos = 1; Pos < Lineas.Length; Pos++)
            {
                string[] Campos = CeC.ObtenArregoSeparador(Lineas[Pos], Separador, true);
                if (Campos.Length != Columnas.Length)
                {
                    Errores = CeC.AgregaSeparador(Errores, "La linea tiene campos diferentes a los definidos, >>" + Lineas[Pos], CeC.SaltoLinea);
                }
                else
                {
                    m_Fila = DT.Rows.Add(Campos);
                    if (!Carga(m_Fila))
                        Errores = CeC.AgregaSeparador(Errores, "No se cargaron todos los campos de la fila >>" + Lineas[Pos], CeC.SaltoLinea);
                    if (!Guarda(Sesion))
                    {
                        Errores = CeC.AgregaSeparador(Errores, "No se pudo guardar la fila >>" + Lineas[Pos], CeC.SaltoLinea);
                    }
                    else
                        Importados++;
                }
            }
            return Importados;
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return -1;
    }

    public static string ObtenDatos(string Tabla, string Llaves, object Modelo, CeC_Sesion Sesion, string OrdenCampos = "", string OtroFiltro = "")
    {
        return ObtenDatos(Tabla, Llaves, JsonConvert.SerializeObject(Modelo), Sesion, OrdenCampos, OtroFiltro);
    }
    /*
    public static object ObtenDatos(string Tabla, string Llaves, object Modelo, CeC_Sesion Sesion)
    {
        try
        {
            string Qry = "";

            PropertyInfo[] Propiedades = Modelo.GetType().GetProperties();

            if (Propiedades == null || Propiedades.Length < 1)
            {
                return "ERROR_SIN_PROPIEDADES";
            }
            string Campos = "";
            for (int Cont = 0; Cont < Propiedades.Length; Cont++)
            {
                if (Propiedades[Cont].CanWrite)
                    Campos = CeC.AgregaSeparador(Campos, Propiedades[Cont].Name, ",");
            }

            Qry = "SELECT " + Campos + " FROM " + Tabla + " WHERE ";
            string Where = "";
            string[] sCamposLlave = CeC.ObtenArregoSeparador(Llaves, ",");

            int Pos = 0;
            foreach (string CampoLlave in sCamposLlave)
            {
                object Valor = Modelo.GetType().GetProperty(CampoLlave).GetValue(Modelo, null);
                Where = CeC.AgregaSeparador(Where, CampoLlave + " = " + Valor2Sql(Valor
                    ) + "", " AND ");
                Pos++;
            }
            Qry += Where;

            DataSet DS = (DataSet)CeC_BD.EjecutaDataSet(Qry);
            if (DS == null || DS.Tables.Count < 1 || DS.Tables[0].Rows.Count < 1)
                return null;
            string[] sCampos = CeC.ObtenArregoSeparador(Llaves, ",");
            DataRow Fila = DS.Tables[0].Rows[0];
            foreach (string Campo in sCampos)
            {
                object Valor = Modelo.GetType().GetProperty(Campo).GetValue(Modelo, null);
                if (CeC.Convierte2Object(Fila[Campo], Valor))
                    Modelo.GetType().GetProperty(Campo).SetValue(Modelo, Valor, null);
            }
            return Modelo;
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return "ERROR_DESCONOCIDO";
    }*/
    public static string ObtenDatosLista(string Tabla, string Llaves, object Modelo, CeC_Sesion Sesion, string OrdenCampos = "", string OtroFiltro = "")
    {
        return ObtenDatosLista(Tabla, Llaves, JsonConvert.SerializeObject(Modelo), Sesion, OrdenCampos, OtroFiltro);
    }

    public static string ObtenDatosLista(string Tabla, string Llaves, string ModeloJson, CeC_Sesion Sesion, string OrdenCampos = "", string OtroFiltro = "")
    {
        string Ret = ObtenDatos(Tabla, Llaves, ModeloJson, Sesion, OrdenCampos, OtroFiltro);
        if (Ret[0] != '[')
            Ret = "[" + Ret + "]";
        return Ret;
    }
    public static string ObtenDatos(string Tabla, string Llaves, string ModeloJson, CeC_Sesion Sesion, string OrdenCampos = "", string OtroFiltro = "")
    {
        try
        {
            string Qry = "";
            Newtonsoft.Json.Linq.JContainer Contenedor = (Newtonsoft.Json.Linq.JContainer)JsonConvert.DeserializeObject(ModeloJson);
            if (Contenedor.Count < 1)
                return "ERROR_SIN_PROPIEDADES";
            string Campos = "";
            foreach (Newtonsoft.Json.Linq.JProperty Propiedad in Contenedor)
            {
                Campos = CeC.AgregaSeparador(Campos, Propiedad.Name, ",");
            }
            Qry = "SELECT " + Campos + " FROM " + Tabla;
            string Where = "";

            if (Llaves != null && Llaves != "")
            {
                string[] sCamposLlave = CeC.ObtenArregoSeparador(Llaves, ",");
                int Pos = 0;
                foreach (string CampoLlave in sCamposLlave)
                {

                    Where = CeC.AgregaSeparador(Where, CampoLlave + " = " + Valor2Sql(((Newtonsoft.Json.Linq.JValue)(Contenedor[CampoLlave])).Value)
                         + "", " AND ");
                    Pos++;
                }
            }
            else
            {
                Where = CeC_Tablas.QryValidaciones(Tabla, Sesion, false);
            }
            if (Where != "")
                Qry += " WHERE (" + Where + ")";
            if (OtroFiltro != "")
            {
                if (Where != "")
                {
                    Qry += " AND ";
                }
                else
                {
                    Qry += " WHERE ";
                }
                Qry += OtroFiltro;
            }
            if (OrdenCampos != "")
            {
                Qry += " \n ORDER BY " + OrdenCampos;
            }
            DataSet DS = (DataSet)CeC_BD.EjecutaDataSet(Qry);
            if (DS == null || DS.Tables.Count < 1 || DS.Tables[0].Rows.Count < 1)
                return "ERROR_SIN_RESULTADOS";
            string Resultado = CeC_BD.DataSet2JsonV2(DS, true);
            return Resultado;//.Substring(1, Resultado.Length - 2); ;
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return "ERROR_DESCONOCIDO";
    }

    public static DataTable ObtenDatos(string Tabla, string Llaves, DataTable Modelo, CeC_Sesion Sesion)
    {
        try
        {
            string Qry = "";

            DataRow Fila = Modelo.Rows[0];

            if (Modelo.Columns.Count < 1)
            {
                return new DataTable("ERROR_SIN_PROPIEDADES");
            }
            string Campos = "";
            for (int Cont = 0; Cont < Modelo.Columns.Count; Cont++)
            {
                if (!Modelo.Columns[Cont].ReadOnly)
                    Campos = CeC.AgregaSeparador(Campos, Modelo.Columns[Cont].ColumnName, ",");
            }

            Qry = "SELECT " + Campos + " FROM " + Tabla + " WHERE ";
            string Where = "";
            string[] sCamposLlave = CeC.ObtenArregoSeparador(Llaves, ",");

            int Pos = 0;
            foreach (string CampoLlave in sCamposLlave)
            {
                object Valor = Fila[CampoLlave];
                Where = CeC.AgregaSeparador(Where, CampoLlave + " = " + Valor2Sql(Valor
                    ) + "", " AND ");
                Pos++;
            }
            Qry += Where;

            DataSet DS = (DataSet)CeC_BD.EjecutaDataSet(Qry);
            if (DS == null || DS.Tables.Count < 1 || DS.Tables[0].Rows.Count < 1)
                return null;
            return DS.Tables[0];
            /*
            string[] sCampos = CeC.ObtenArregoSeparador(Llaves, ",");
            DataRow Fila = DS.Tables[0].Rows[0];
            foreach (string Campo in sCampos)
            {
                object Valor = Modelo.GetType().GetProperty(Campo).GetValue(Modelo, null);
                if (CeC.Convierte2Object(Fila["Campo"], Valor))
                    Modelo.GetType().GetProperty(Campo).SetValue(Modelo, Valor, null);
            }
            return Modelo;*/
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return new DataTable("ERROR_DESCONOCIDO");
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Tabla"></param>
    /// <param name="Llaves"></param>
    /// <param name="Modelo"></param>
    /// <param name="EsNuevo"></param>
    /// <param name="Sesion"></param>
    /// <param name="SuscripcionID"></param>
    /// <returns>Regresa el ID del Identificador Creado o 1 si es edicion</returns>
    public static int GuardaDatos(string Tabla, string Llaves, DataTable Modelo, bool EsNuevo, CeC_Sesion Sesion, int SuscripcionID)
    {
        try
        {
            if (Modelo.Columns.Count < 1 || Modelo.Rows.Count < 1)
                return -1;
            int R = 1;
            bool IniciadoElemento = false;

            //Solo se guardara el nombre del campo llave cuando exista la necesidad de generar el autonumerico
            string CampoLlave = "";
            string[] CamposLlave = CeC.ObtenArregoSeparador(Llaves, ",");
            if (CamposLlave.Length == 1)
                CampoLlave = CamposLlave[0];
            string QryPrefijoGuardar = "";
            string QryGuardar = "";
            string QryFiltroUpdate = "";
            string QrySubfijoGuardar = "";
            string QryValores = "";
            string QryValoresInsert = "";
            if (EsNuevo)
            {
                QryPrefijoGuardar = "INSERT INTO " + Tabla + " (";
            }
            else
            {
                QryPrefijoGuardar = "UPDATE " + Tabla + " SET ";
            }

            int Actualizados = 0;
            DataRow Fila = Modelo.Rows[0];


            for (int Cont = 0; Cont < Modelo.Columns.Count; Cont++)
            {

                object Dato = Fila[Cont];
                string ColumnaNombre = Modelo.Columns[Cont].ColumnName.ToUpper();
                try
                {
                    if (EsNuevo && CamposLlave.Length == 1 && ColumnaNombre == CampoLlave.ToUpper())
                    {
                        Dato = R = CeC_Autonumerico.GeneraAutonumerico(Tabla, CampoLlave.ToUpper(), Sesion.SESION_ID, SuscripcionID);
                    }
                }
                catch { }

                string ValorSql = Valor2Sql(Dato);
                if (EsNuevo)
                {
                    QryValoresInsert = CeC.AgregaSeparador(QryValoresInsert, ColumnaNombre, ", ");
                    QryValores = CeC.AgregaSeparador(QryValores, ValorSql, ", ");
                }
                else
                {
                    QryValores = CeC.AgregaSeparador(QryValores, ColumnaNombre + " = " + ValorSql, ", ");
                    if (CeC.ExisteEnSeparador(Llaves.ToUpper(), ColumnaNombre, ","))
                        QryFiltroUpdate = CeC.AgregaSeparador(QryFiltroUpdate, ColumnaNombre + " = " + ValorSql, " AND ");
                }
                Actualizados++;
            }
            if (Actualizados < 1)
            {

            }
            if (EsNuevo)
                QryGuardar = QryPrefijoGuardar + QryValoresInsert + ") VALUES (" + QryValores + ")";
            else
                QryGuardar = QryPrefijoGuardar + QryValores + " WHERE " + QryFiltroUpdate;
            if (CeC_BD.EjecutaComando(QryGuardar) > 0)
                return R;
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return -2;

    }


    public static int BorrarDatos(string Tabla, string Llaves, string ModeloJson, CeC_Sesion Sesion)
    {
        try
        {
            Newtonsoft.Json.Linq.JContainer Contenedor = (Newtonsoft.Json.Linq.JContainer)JsonConvert.DeserializeObject(ModeloJson);
            if (Contenedor.Count < 1)
                return -11;
            string CampoBorrado = CeC_Tablas.Obten_TablaCBorrado(Tabla);

            int R = 1;
            bool IniciadoElemento = false;

            //Solo se guardara el nombre del campo llave cuando exista la necesidad de generar el autonumerico
            string CampoLlave = "";
            string[] CamposLlave = CeC.ObtenArregoSeparador(Llaves, ",");
            if (CamposLlave.Length == 1)
                CampoLlave = CamposLlave[0];

            string QryGuardar = "";
            string QryFiltroUpdate = "";


            foreach (Newtonsoft.Json.Linq.JProperty Propiedad in Contenedor)
            {

                object Dato = Propiedad.Value.ToString();
                string ColumnaNombre = Propiedad.Name;
                string ValorSql = "";

                if (CeC.ExisteEnSeparador(Llaves.ToUpper(), ColumnaNombre, ","))
                {
                    if (ValorSql == "")
                        ValorSql = Valor2Sql(Propiedad.Value);
                    QryFiltroUpdate = CeC.AgregaSeparador(QryFiltroUpdate, ColumnaNombre + " = " + ValorSql, " AND ");
                }

            }

            if (CampoBorrado != "")
                QryGuardar = "UPDATE " + Tabla + " SET " + CampoBorrado + " = 1 WHERE " + QryFiltroUpdate;
            else
                QryGuardar = "DELETE " + Tabla + " WHERE " + QryFiltroUpdate;
            if (CeC_BD.EjecutaComando(QryGuardar) > 0)
            {
                try
                {
                    CeC_Autonumerico.Borra(Tabla, Llaves, CeC.Convierte2Int(((Newtonsoft.Json.Linq.JValue)(Contenedor[CampoLlave])).Value), Sesion);
                }
                catch { }
                return R;
            }
            else
                return -19;
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return -12;
    }

    public static bool EsBinario(Newtonsoft.Json.Linq.JProperty Propiedad)
    {
        try
        {
            if (Propiedad.Value.Type == Newtonsoft.Json.Linq.JTokenType.String)
            {
                if (Propiedad.Value.ToString().Length > 4000)
                    return true;
            }
        }
        catch { }

        return false;
    }

    public static int GuardaDatos1aN(string Tabla, string CampoLlaveUno, string ValorLlaveUno, string CampoLlaveN, string Activos, CeC_Sesion Sesion, int SuscripcionID)
    {
        string CampoBorrado = CeC_Tablas.Obten_TablaCBorrado(Tabla);
        string QryQuitaPermiso = "DELETE " + Tabla + " WHERE " + CampoLlaveUno + " ='" + ValorLlaveUno + "' AND " + CampoLlaveN + " NOT IN (" + Activos + ")";
        int Borrados = CeC_BD.EjecutaComando(QryQuitaPermiso);
        int Actualizados = 0;
        int Nuevos = 0;
        string[] sActivos = CeC.ObtenArregoSeparador(Activos, ",");
        foreach (string sActivo in sActivos)
        {
            string QryChecaPermiso = "SELECT COUNT(*) FROM " + Tabla + " WHERE " + CampoLlaveUno + " ='" + ValorLlaveUno + "' AND " + CampoLlaveN + " = '" + sActivo + "'";
            if (CeC_BD.EjecutaEscalarInt(QryChecaPermiso) > 0)
                continue;
            string QryDaPermiso = "INSERT INTO " + Tabla + " (" + CampoLlaveUno + "," + CampoLlaveN + ") VALUES ('" + ValorLlaveUno + "', '" + sActivo + "')";
            if (CeC_BD.EjecutaComando(QryDaPermiso) > 0)
                Nuevos++;
        }
        return Borrados + Actualizados + Nuevos;
    }

    public static int GuardaDatos(string Tabla, string Llaves, object ModeloDatos, bool EsNuevo, CeC_Sesion Sesion, int SuscripcionID)
    {
        return GuardaDatos(Tabla, Llaves, JsonConvert.SerializeObject(ModeloDatos), EsNuevo, Sesion, SuscripcionID);
    }
    /// <summary>
    /// Se encarga de separar los campos e insertar o actualizar los campos a guardar
    /// obteniendo las propiedades del contenedor el cual nos permitira evaluar si generar
    /// o no un autonumerico, porsteriormente guarda el valor de la propiedad para poder 
    /// armar el query de guardar, Por ultimo guarda los datos sobre la tabla correspondiente.
    /// </summary>
    /// <param name="Tabla"></param>
    /// <param name="Llaves"></param>
    /// <param name="ModeloJson"></param>
    /// <param name="EsNuevo"></param>
    /// <param name="Sesion"></param>
    /// <param name="SuscripcionID"></param>
    /// <returns>Vacio si no fu correcto</returns>
    public static int GuardaDatos(string Tabla, string Llaves, string ModeloJson, bool EsNuevo, CeC_Sesion Sesion, int SuscripcionID)
    {
        try
        {
            ModeloJson = eClockBase.Controladores.CeC_ZLib.ZJson2Json(ModeloJson);
            object JSon = JsonConvert.DeserializeObject(ModeloJson);
            Newtonsoft.Json.Linq.JContainer Contenedor = (Newtonsoft.Json.Linq.JContainer)JSon;
            if (Contenedor.Count < 1)
                return -1;

            if (JSon.GetType().Name == "JArray")
            {
                int Modificados = 0;
                //bool ValidaSuscripcion =  CeC_Tablas.ValidarSuscripcion(Tabla);
                foreach (Newtonsoft.Json.Linq.JObject Elemento in Contenedor)
                {

                    bool lEsNuevo = EsNuevo;
                    if (!lEsNuevo)
                        lEsNuevo = !CeC_Tablas.ExisteRegistro(Tabla, Llaves,
                            CeC.Convierte2String(((Newtonsoft.Json.Linq.JValue)(((Newtonsoft.Json.Linq.JContainer)Elemento)[Llaves])).Value), Sesion, SuscripcionID);
                    if (GuardaDatos(Tabla, Llaves, Elemento.ToString(), lEsNuevo, Sesion, SuscripcionID) > 0)
                    {
                        Modificados++;
                    }

                }
                return Modificados;
            }
            int R = 1;
            bool IniciadoElemento = false;

            //Solo se guardara el nombre del campo llave cuando exista la necesidad de generar el autonumerico
            string CampoLlave = "";
            string[] CamposLlave = CeC.ObtenArregoSeparador(Llaves, ",");
            if (CamposLlave.Length == 1)
                CampoLlave = CamposLlave[0];
            string QryPrefijoGuardar = "";
            string QryGuardar = "";
            string QryFiltroUpdate = "";
            string QrySubfijoGuardar = "";
            string QryValores = "";
            string QryValoresInsert = "";
            if (EsNuevo)
            {
                QryPrefijoGuardar = "INSERT INTO " + Tabla + " (";
            }
            else
            {
                QryPrefijoGuardar = "UPDATE " + Tabla + " SET ";
            }

            int Actualizados = 0;

            List<Newtonsoft.Json.Linq.JProperty> PropiedadesBinary = new List<Newtonsoft.Json.Linq.JProperty>();

            foreach (Newtonsoft.Json.Linq.JProperty Propiedad in Contenedor)
            {

                object Dato = Propiedad.Value.ToString();
                string ColumnaNombre = Propiedad.Name;
                string ValorSql = "";
                try
                {
                    if (EsNuevo && CamposLlave.Length == 1 && ColumnaNombre == CampoLlave.ToUpper())
                    {

                        if ((CeC.Convierte2Int(Dato, 0) == 0 && Dato.ToString() == "0") || CeC.Convierte2Int(Dato, 0) != 0)
                        {
                            Dato = R = CeC_Autonumerico.GeneraAutonumerico(Tabla, CampoLlave.ToUpper(), Sesion.SESION_ID, SuscripcionID);
                            ValorSql = Valor2Sql(Dato);
                        }
                    }
                }
                catch { }
                if (EsBinario(Propiedad))
                {
                    PropiedadesBinary.Add(Propiedad);
                    continue;
                }
                if (ValorSql == "")
                    ValorSql = Valor2Sql(Propiedad.Value);

                if (EsNuevo)
                {
                    QryValoresInsert = CeC.AgregaSeparador(QryValoresInsert, ColumnaNombre, ", ");
                    QryValores = CeC.AgregaSeparador(QryValores, ValorSql, ", ");
                }
                else
                {
                    QryValores = CeC.AgregaSeparador(QryValores, ColumnaNombre + " = " + ValorSql, ", ");
                }
                if (CeC.ExisteEnSeparador(Llaves.ToUpper(), ColumnaNombre, ","))
                    QryFiltroUpdate = CeC.AgregaSeparador(QryFiltroUpdate, ColumnaNombre + " = " + ValorSql, " AND ");
                Actualizados++;
            }
            if (Actualizados < 1)
            {

            }
            if (EsNuevo)
                QryGuardar = QryPrefijoGuardar + QryValoresInsert + ") VALUES (" + QryValores + ")";
            else
                QryGuardar = QryPrefijoGuardar + QryValores + " WHERE " + QryFiltroUpdate;
            if (CeC_BD.EjecutaComando(QryGuardar) > 0)
            {
                if (!EsNuevo)
                {
                    try
                    {
                        CeC_Autonumerico.Actualiza(Tabla, Llaves, CeC.Convierte2Int(((Newtonsoft.Json.Linq.JValue)(Contenedor[CampoLlave])).Value), Sesion);
                    }
                    catch (Exception ex)
                    {
                        CIsLog2.AgregaError(ex);
                    }
                }
                foreach (Newtonsoft.Json.Linq.JProperty Propiedad in PropiedadesBinary)
                {
                    CeC_BD.AsignaBinario(Tabla, QryFiltroUpdate, Propiedad.Name, Propiedad.Value.ToObject<byte[]>());
                }
                return R;
            }
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return -2;
    }


}
