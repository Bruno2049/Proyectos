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
            Qry += " ORDER BY  " + OrderBy;        
        return Qry;
    }


    public virtual string ObtenGridQry(CeC_Sesion Sesion, bool MuestraBorrados)
    {
        string Qry = "SELECT " + m_Campos + " FROM " + m_Tabla + " ORDER BY " + m_CamposLlave;
        return Qry;
    }

    public virtual string ObtenExportarQry(CeC_Sesion Sesion, bool MuestraBorrados)
    {
        string Qry = "SELECT " + m_Campos + " FROM " + m_Tabla + " ORDER BY " + m_CamposLlave;
        return Qry;
    }

    public virtual DataSet ObtenDS(CeC_Sesion Sesion, string Where, string OrderBy)
    {
        return (DataSet)CeC_BD.EjecutaDataSet(ObtenQry(Sesion,Where,OrderBy));
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
                    if (m_EsNuevo && CamposLlave.Length == 1 && Propiedades[Cont].Name.ToUpper() == CamposLlave[0].ToUpper() && (Dato != null || (!AutonumericoNegativo && CeC.Convierte2Int(Dato, 1) < 0)))
                    {
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
    protected string Valor2Sql(object Valor)
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

    /// <summary>
    /// Obtiene un dataset, valida que tenga tablas y lo liga usando el DataSource y DataMember
    ///         DataSet DS = Tabla.ObtenGridDS(Sesion);
    ///        if (DS == null || DS.Tables.Count  1)
    ///        return;
    ///    Grid.DataSource = DS.Tables[0];
    ///    Grid.DataMember = DS.Tables[0].TableName;
    /// </summary>
    /// <param name="Grid"></param>
    /// <param name="Sesion">Clase de sesion que contiene información que indicará si ejecutará algun filtro</param>
    /// <returns>Verdadero se se realizo la asignación satisfactoriamente</returns>
    public bool Grid_DataBinding(Infragistics.Web.UI.GridControls.WebDataGrid Grid, CeC_Sesion Sesion, bool MuestraBorrados)
    {
        try
        {
            DataSet DS = ObtenGridDS(Sesion, MuestraBorrados);
            if (DS == null || DS.Tables.Count < 1)
                return false;
            if (!Sesion.m_PaginaWeb.IsPostBack)
                Grid.AutoGenerateColumns = false;
            Grid.DataSource = DS.Tables[0];
            Grid.DataMember = DS.Tables[0].TableName;
            Grid.DataKeyFields = this.m_CamposLlave;
            if (!Sesion.m_PaginaWeb.IsPostBack)
            {
                CeC_Grid.AplicaFormato(Grid);
                Grid.DataBind();
            }

            return true;
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return false;
    }


}
