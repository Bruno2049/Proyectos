using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

/// <summary>
/// Descripción breve de CeC_Campos
/// </summary>
public class CeC_Campos
{
    /// <summary>
    /// Tipos de datos definidos en la tabla EC_TIPO_DATOS
    /// </summary>
    public enum Tipo_Datos
    {
        Desconocido = 0,
        Texto,
        Entero,
        Decimal,
        Imagen,
        Fecha,
        Fecha_y_Hora,
        Hora,
        Boleano
    };
    private DS_CamposTableAdapters.TA_EC_CAMPOS TA_Campos = new DS_CamposTableAdapters.TA_EC_CAMPOS();
    private DS_CamposTableAdapters.TA_EC_CAMPOS_TE TA_Campos_TE = new DS_CamposTableAdapters.TA_EC_CAMPOS_TE();
    private DS_CamposTableAdapters.TA_EC_MASCARAS TA_Mascaras = new DS_CamposTableAdapters.TA_EC_MASCARAS();
    private DS_CamposTableAdapters.TA_EC_CATALOGOS TA_Catalogos = new DS_CamposTableAdapters.TA_EC_CATALOGOS();
    private DS_CamposTableAdapters.TA_EC_TIPO_DATOS TA_Tipo_Datos = new DS_CamposTableAdapters.TA_EC_TIPO_DATOS();
    public DS_Campos m_ds_Campos = null;
    private static DS_Campos s_ds_Campos = null;
    /// <summary>
    /// Obtiene el DS_Campos
    /// </summary>
    public static DS_Campos ds_Campos
    {
        get
        {
            Inicializa();
            return s_ds_Campos;
        }
    }

    public static bool RecargaCampos()
    {
        s_ds_Campos = null;
        return true;
    }
    /// <summary>
    /// Verifica que el Dataset contenga datos y los inicializa
    /// </summary>
    /// <returns></returns>
    public static bool Inicializa()
    {
        if (s_ds_Campos != null)
            return false;
        ChecaCampos_TE();
        CeC_Campos Clase = new CeC_Campos();
        Clase.InicializaCampos();
        s_ds_Campos = Clase.m_ds_Campos;
        return true;
    }
    public static string ObtenCampoNombre(string Campo)
    {
        int Punto = Campo.IndexOf('.');
        if (Punto > 0)
            Campo = Campo.Substring(Punto + 1);
        return Campo;
    }
    //public CISObjetoDibujo this [int index]
    /// <summary>
    /// Regresa los campos por Campo_Nombre
    /// </summary>
    /// <param name="Campo_Nombre"></param>
    /// <returns></returns>
    public static DS_Campos.EC_CAMPOSRow Obten_Campo(string Campo_Nombre)
    {
        //        ds_Campos.EC_CAMPOS.
        Campo_Nombre = ObtenCampoNombre(Campo_Nombre);
        DS_Campos.EC_CAMPOSRow Campo = ds_Campos.EC_CAMPOS.FindByCAMPO_NOMBRE(Campo_Nombre);

        if (Campo == null && CeC.ObtenString(Campo_Nombre, 0, 12) != "ASISTENCIA_D" && CeC.ObtenString(Campo_Nombre, 0, 3) != "DIA" && CeC.ObtenString(Campo_Nombre, 0, 7) != "TURNO_D")
            CIsLog2.AgregaLog("Campo no existe - " + Campo_Nombre);
        return Campo;
    }
    /// <summary>
    /// Regresa para cada Fila de EC_CAMPOS los campos
    /// </summary>
    /// <param name="Campo_Nombre"></param>
    /// <returns></returns>
    public static DS_Campos.EC_CAMPOS_TERow Obten_CampoTE(string Campo_Nombre)
    {

        foreach (DS_Campos.EC_CAMPOS_TERow Campo in ds_Campos.EC_CAMPOS_TE)
            if (Campo.CAMPO_NOMBRE == Campo_Nombre)
                return Campo;
        return null;
        //            return ds_Campos.EC_CAMPOS_TE.FindByCAMPO_NOMBRE(Campo_Nombre);
    }
    /// <summary>
    /// Regresa la Mascara
    /// </summary>
    /// <param name="Mascara_ID"></param>
    /// <returns></returns>
    public static DS_Campos.EC_MASCARASRow Obten_Mascara(decimal Mascara_ID)
    {
        return ds_Campos.EC_MASCARAS.FindByMASCARA_ID(Convert.ToDecimal(Mascara_ID));
    }
    public CeC_Campos()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    /// <summary>
    /// Verifica si existen registros en la tabla Campos_TE lo que
    /// indicaria que se encuentran ya cargados los campos 
    /// de la tabla de empleados
    /// </summary>
    /// <returns>Regresa verdadero si hay registros</returns>
    public static bool ExistenCampos_TE()
    {
        try
        {
            DS_CamposTableAdapters.TA_EC_CAMPOS_TE TA = new DS_CamposTableAdapters.TA_EC_CAMPOS_TE();
            System.Nullable<decimal> No = TA.NoCamposEmpleados();
            if (No == null)
                return false;
            if (No < 1)
                return false;
            return true;
        }
        catch
        {

        }
        return false;
    }
    /// <summary>
    /// Intenta regresar el Tipo_Dato_ID
    /// </summary>
    /// <param name="TIPO_DATO_DS"></param>
    /// <returns></returns>
    public static int Obten_Tipo_Dato_ID(string TIPO_DATO_DS)
    {
        try
        {
            DS_CamposTableAdapters.TA_EC_TIPO_DATOS TA_TDatos = new DS_CamposTableAdapters.TA_EC_TIPO_DATOS();
            int IDTDato = Convert.ToInt32(TA_TDatos.Tipo_Dato_ID(TIPO_DATO_DS));
            return IDTDato;
        }
        catch
        {

        }
        return 0;
    }
    /// <summary>
    /// Intenta crear los campos en EC_CAMPOS_TE
    /// </summary>
    /// <returns></returns>
    public static bool CreaCampos_TE()
    {

        try
        {
            object Obj = CeC_BD.EjecutaDataSet_Schema("SELECT * FROM EC_PERSONAS_DATOS");
            if (Obj == null)
            {
                return false;
            }
            DataSet Ds = (DataSet)Obj;
            if (Ds.Tables.Count < 1)
                return false;
            DS_CamposTableAdapters.TA_EC_CAMPOS_TE TA_Campos_TE = new DS_CamposTableAdapters.TA_EC_CAMPOS_TE();
            DS_CamposTableAdapters.TA_EC_CAMPOS TA_Campos = new DS_CamposTableAdapters.TA_EC_CAMPOS();
            int EsLlave = 1;
            foreach (DataColumn Columna in Ds.Tables[0].Columns)
            {
                int ID_TDato = Obten_Tipo_Dato_ID(Columna.DataType.ToString());
                if (TA_Campos.ExisteCampo(Columna.ColumnName) == null)
                {
                    TA_Campos.Insert(Columna.ColumnName, Columna.ColumnName, 0, ID_TDato, Columna.MaxLength, 0, 0, 0, 0, 1, 0);
                }
                if (TA_Campos_TE.ExisteCampo(Columna.ColumnName) == null)
                {
                    TA_Campos_TE.Insert(Columna.ColumnName, Columna.Ordinal, EsLlave, Columna.AutoIncrement ? 1 : 0, 0, 0, 1, 0);
                }
                if (EsLlave == 1)
                    EsLlave = 0;

            }
            return true;
        }
        catch
        {


        }
        return false;

    }
    /// <summary>
    /// Verifica que existam los Campos_TE y los crea
    /// </summary>
    /// <returns></returns>
    public static bool ChecaCampos_TE()
    {
        if (!ExistenCampos_TE())
        {
            CreaCampos_TE();
        }
        return true;
    }
    /// <summary>
    /// Regresa true para checar campos
    /// </summary>
    /// <returns></returns>
    public static bool ChecaCampos()
    {
        return true;
    }
    /// <summary>
    /// Edita los campos
    /// </summary>
    /// <param name="nombre"></param>
    /// <param name="NuevaEtiqueta"></param>
    /// <returns></returns>
    public bool EditaCampo(string nombre, string NuevaEtiqueta)
    {
        try
        {
            int Indice = ds_Campos.EC_CAMPOS.Rows.IndexOf(Obten_Campo(nombre));
            ds_Campos.EC_CAMPOS.Rows[Indice]["CAMPO_ETIQUETA"] = NuevaEtiqueta;
            TA_Campos.Update(ds_Campos.EC_CAMPOS);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    /// <summary>
    /// Edita Etiqueta y Edita si es Autocatalogo
    /// </summary>
    /// <param name="nombre"></param>
    /// <param name="NuevaEtiqueta"></param>
    /// <param name="Autocatalogo"></param>
    /// <returns></returns>
    public bool EditaCampo(string nombre, string NuevaEtiqueta, bool Autocatalogo)
    {
        try
        {
            int Indice = ds_Campos.EC_CAMPOS.Rows.IndexOf(Obten_Campo(nombre));
            ds_Campos.EC_CAMPOS.Rows[Indice]["CAMPO_ETIQUETA"] = NuevaEtiqueta;
            ds_Campos.EC_CAMPOS.Rows[Indice]["CATALOGO_ID"] = Convert.ToDecimal(Autocatalogo);
            TA_Campos.Update(ds_Campos.EC_CAMPOS);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    /// <summary>
    /// Agrega un campo
    /// </summary>
    /// <param name="Nombre"></param>
    /// <param name="Etiqueta"></param>
    /// <param name="TipoDato"></param>
    /// <returns></returns>
    public bool AgregaCampo(string Nombre, string Etiqueta, Tipo_Datos TipoDato)
    {
        int AnchoGrid = 0;
        try
        {
            int TDato = Convert.ToInt32(TipoDato);
            //Se asigna la mascara 0 por default
            DS_Campos.EC_MASCARASRow Fila = m_ds_Campos.EC_MASCARAS.FindByMASCARA_ID(0);
            AnchoGrid = Convert.ToInt32(Fila.MASCARA_ANCHO);
        }
        catch (System.Exception e)
        {

        }

        return AgregaCampo(Nombre, Etiqueta, TipoDato, AnchoGrid);
    }
    /// <summary>
    /// Agrega un campo
    /// </summary>
    /// <param name="Nombre"></param>
    /// <param name="Etiqueta"></param>
    /// <param name="TipoDato"></param>
    /// <param name="AnchoPix"></param>
    /// <returns></returns>
    public bool AgregaCampo(string Nombre, string Etiqueta, Tipo_Datos TipoDato, int AnchoPix)
    {
        Nombre = Nombre.ToUpper();
        decimal CATALOGO_ID = 0;
        try
        {
            DataRow[] DRs = m_ds_Campos.EC_CATALOGOS.Select("CATALOGO_C_LLAVE = '" + Nombre + "'");
            if (DRs != null)
            {
                CATALOGO_ID = ((DS_Campos.EC_CATALOGOSRow)DRs[0]).CATALOGO_ID;
            }
            else
            {
                //Verifica si es un alias
                DRs = m_ds_Campos.EC_CATALOGOS.Select("CATALOGO_C_LLAVE = '" + Nombre.Substring(0, Nombre.Length - 2) + "'");
                if (DRs != null)
                {
                    CATALOGO_ID = ((DS_Campos.EC_CATALOGOSRow)DRs[0]).CATALOGO_ID;
                }
            }
        }
        catch (System.Exception e)
        {

        }

        try
        {

            DS_Campos.EC_CAMPOSRow Row = m_ds_Campos.EC_CAMPOS.FindByCAMPO_NOMBRE(Nombre);
            if (Row.CAMPO_NOMBRE == Nombre)
            {
                Row.CAMPO_ETIQUETA = Etiqueta;
                Row.TIPO_DATO_ID = Convert.ToDecimal(TipoDato);
                Row.CATALOGO_ID = CATALOGO_ID;
                return false;
            }
        }
        catch (System.Exception e)
        {
        }

        m_ds_Campos.EC_CAMPOS.AddEC_CAMPOSRow(Nombre, Etiqueta, 0, Convert.ToDecimal(TipoDato), 0, 0, 0, 0, 0, 0, CATALOGO_ID);
        return true;
    }

    public static bool CreaDefinicionCampo(string Nombre, bool EsTablaEmpleados)
    {
        int TEEmp = 0;
        if (EsTablaEmpleados)
            TEEmp = 1;
        if (CeC_BD.EjecutaComando("INSERT INTO EC_CAMPOS (CAMPO_NOMBRE, CAMPO_ETIQUETA, CATALOGO_ID, MASCARA_ID, TIPO_DATO_ID, CAMPO_ES_TEMPLEADOS) VALUES(" +
            "'" + Nombre + "', '" + Nombre + "',0,0,1," + TEEmp + ")") > 0)
            return true;
        return false;
    }

    /// <summary>
    /// Obtiene una lista de los campos que que serán suceptibles a filtro, separados por coma ,
    /// </summary>
    /// <returns>Lista de campos</returns>
    public static string ObtenListaCamposTEFiltro()
    {
        string CamposTE = "";
        try
        {
            for (int Cont = 0; Cont < ds_Campos.EC_CAMPOS_TE.Count; Cont++)
            {
                DS_Campos.EC_CAMPOS_TERow CTE = ds_Campos.EC_CAMPOS_TE[Cont];
                if (CTE.CAMPO_TE_INVISIBLE == 0 && CTE.CAMPO_TE_FILTRO == 1)
                {
                    if (CamposTE.Length > 0)
                        CamposTE += ", ";
                    CamposTE += CTE.CAMPO_NOMBRE;
                }
            }
        }
        catch (System.Exception e)
        {

        }
        return CamposTE;
    }

    /// <summary>
    /// Obtiene una lista de los campos que serán visibles en un dataset separados por coma
    /// </summary>
    /// <returns>Lista de campos</returns>
    public static string ObtenListaCamposTE()
    {
        string CamposTE = "";
        try
        {
            for (int Cont = 0; Cont < ds_Campos.EC_CAMPOS_TE.Count; Cont++)
            {
                DS_Campos.EC_CAMPOS_TERow CTE = ds_Campos.EC_CAMPOS_TE[Cont];
                if (CTE.CAMPO_TE_INVISIBLE == 0)
                {
                    /*  DS_Campos.EC_CAMPOSRow Campo = ds_Campos.EC_CAMPOS.FindByCAMPO_NOMBRE(CTE.CAMPO_NOMBRE);
                    
                      Campo.*/
                    if (CamposTE.Length > 0)
                        CamposTE += ", ";
                    CamposTE += "EC_PERSONAS_DATOS." + CTE.CAMPO_NOMBRE;
                }
            }
        }
        catch (System.Exception e)
        {

        }
        return CamposTE;
    }

    public static DataSet ObtenListaCamposTEDataSet()
    {
        string Campos = ObtenListaCamposTE().Replace("EC_PERSONAS_DATOS.", "");
        DataSet DS = new DataSet();
        DataTable DT = DS.Tables.Add("EC_CAMPOS");
        string[] sCampos = Campos.Split(new char[] { ',' });
        DataColumn column = new DataColumn();
        column.DataType = Type.GetType("System.String");
        column.ColumnName = "CAMPO_NOMBRE";
        DT.Columns.Add(column);
        column = new DataColumn();
        column.DataType = Type.GetType("System.String");
        column.ColumnName = "CAMPO_ETIQUETA";
        DT.Columns.Add(column);
        foreach (string Campo in sCampos)
        {
            DataRow Fila = DT.NewRow();
            Fila["CAMPO_NOMBRE"] = Campo.Trim();
            Fila["CAMPO_ETIQUETA"] = ObtenEtiqueta(Campo.Trim());
            DT.Rows.Add(Fila);
            //
        }
        return DS;
    }

    /// <summary>
    /// Obtiene una lista de los campos que serán visibles en un dataset para un Grid separados por coma 
    /// </summary>
    /// <returns>Lista de campos</returns>
    public static string ObtenListaCamposTEGrid()
    {
        string CamposTE = "";
        try
        {
            for (int Cont = 0; Cont < ds_Campos.EC_CAMPOS_TE.Count; Cont++)
            {
                DS_Campos.EC_CAMPOS_TERow CTE = ds_Campos.EC_CAMPOS_TE[Cont];
                if (CTE.CAMPO_TE_INVISIBLE == 0 && CTE.CAMPO_TE_INVISIBLEG == 0)
                {
                    /*  DS_Campos.EC_CAMPOSRow Campo = ds_Campos.EC_CAMPOS.FindByCAMPO_NOMBRE(CTE.CAMPO_NOMBRE);
                    
                      Campo.*/
                    if (CamposTE.Length > 0)
                        CamposTE += ", ";
                    CamposTE += CTE.CAMPO_NOMBRE;
                }
            }
        }
        catch (System.Exception e)
        {

        }
        return CamposTE;
    }
    /// <summary>
    /// Obtiene Si el Campo es Invisible a partir del nombre del campo de la base de datos
    /// </summary>
    /// <param name="CampoNombre"></param>
    /// <returns></returns>
    public static bool ObtenCampoInvisible(string CampoNombre)
    {
        DS_Campos.EC_CAMPOS_TERow Campo = Obten_CampoTE(CampoNombre);
        if (Campo == null)
            return true;
        else
            return Convert.ToBoolean(Campo.CAMPO_TE_INVISIBLE);
    }

    /// <summary>
    /// Obtiene la etiqueta a mostrar a partir de un nombre de un campo en la base de datos
    /// </summary>
    /// <param name="CampoNombre">Nombre del campo a buscar</param>
    /// <returns>Regresa el nombre de la etiqueta y si no es encontrada regresa "[" + CampoNombre + "]"</returns>
    public static string ObtenEtiqueta(string CampoNombre)
    {
        return ObtenEtiqueta(CampoNombre, "[" + CampoNombre + "]");
    }
    public static string ObtenEtiqueta(string CampoNombre, string Predeterminado)
    {
        string Ret = Predeterminado;
        DS_Campos.EC_CAMPOSRow Campo = Obten_Campo(CampoNombre);
        if (Campo == null)
            return Ret;
        return Campo.CAMPO_ETIQUETA;
    }
    /// <summary>
    /// Obtiene el campo llave de la tabla EC_PERSONAS_DATOS
    /// </summary>
    /// <returns>El campo llave de lo contrario ""</returns>
    public static string ObtenCampoTELlave()
    {
        string CamposTE = "";
        try
        {
            for (int Cont = 0; Cont < ds_Campos.EC_CAMPOS_TE.Count; Cont++)
            {
                DS_Campos.EC_CAMPOS_TERow CTE = ds_Campos.EC_CAMPOS_TE[Cont];
                if (CTE.CAMPO_TE_ES_LLAVE != 0)
                {
                    return CTE.CAMPO_NOMBRE;
                }
            }
        }
        catch (System.Exception e)
        {

        }
        return CamposTE;
    }

    /// <summary>
    /// Obtiene el tipo de dato de un campo en Campos y regresa el tipo en un string
    /// </summary>
    /// <param name="CampoNombre"></param>
    /// <returns></returns>
    public static string ObtenTipoDatoCampo(string CampoNombre)
    {
        DS_Campos.EC_CAMPOSRow Campo = Obten_Campo(CampoNombre);
        if (Campo == null)
            return "";
        else
            switch (Convert.ToInt32(Campo.TIPO_DATO_ID))
            {
                case 0:
                    return "";
                    break;
                case 1:
                    return "Texto";
                    break;
                case 2:
                    return "Entero";
                    break;
                case 3:
                    return "Decimal";
                    break;
                case 4:
                    return "Imagen";
                    break;
                case 5:
                    return "Fecha";
                    break;
                case 6:
                    return "Fecha_y_Hora";
                    break;
                case 7:
                    return "Hora";
                    break;
                case 8:
                    return "Boleano";
                    break;

            }
        return "";
    }
    /// <summary>
    /// Obtiene el CampoTE_Llava
    /// </summary>
    private static string m_CampoTE_Llave = "PERSONA_LINK_ID";
    public static string CampoTE_Llave
    {
        get
        {
            return m_CampoTE_Llave = "PERSONA_LINK_ID";

            if (m_CampoTE_Llave == "")
                m_CampoTE_Llave = ObtenCampoTELlave();
            return m_CampoTE_Llave;
        }
    }
    public static void gAsignaEtiquetasPredeterminadas()
    {
        ChecaCampos_TE();
        CeC_Campos Clase = new CeC_Campos();
        Clase.InicializaCampos();
        Clase.AsignaEtiquetasPredeterminadas();
        s_ds_Campos = Clase.m_ds_Campos;
    }

    public void AsignaEtiquetasPredeterminadas()
    {
        TA_Campos_TE.ClearBeforeFill = true;
        TA_Campos_TE.Fill(m_ds_Campos.EC_CAMPOS_TE);
        /*
                 * Agregar aquí el código para inicializar los campos
                 * */
        AgregaCampo("NOMBRE_COMPLETO", "Nombre Completo", Tipo_Datos.Texto);
        AgregaCampo("NOMBRE", "Nombre", Tipo_Datos.Texto);
        AgregaCampo("APATERNO", "Apellido Paterno", Tipo_Datos.Texto);
        AgregaCampo("AMATERNO", "A.Materno", Tipo_Datos.Texto);
        AgregaCampo("FECHA_NAC", "Fecha de nacimiento", Tipo_Datos.Fecha);
        AgregaCampo("RFC", "RFC", Tipo_Datos.Texto);
        AgregaCampo("CURP", "CURP", Tipo_Datos.Texto);
        AgregaCampo("IMSS", "IMSS", Tipo_Datos.Texto);
        AgregaCampo("ESTUDIOS", "Estudios", Tipo_Datos.Texto);
        AgregaCampo("SEXO", "Sexo", Tipo_Datos.Texto);
        AgregaCampo("NACIONALIDAD", "Nacionalidad", Tipo_Datos.Texto);
        AgregaCampo("FECHA_INGRESO", "Fecha de ingreso", Tipo_Datos.Fecha);
        AgregaCampo("FECHA_BAJA", "Fecha de baja", Tipo_Datos.Fecha);
        AgregaCampo("DP_CALLE_NO", "DP Calle Y No.", Tipo_Datos.Texto);
        AgregaCampo("DP_COLONIA", "DP Colonia", Tipo_Datos.Texto);
        AgregaCampo("DP_DELEGACION", "DP Delegacion", Tipo_Datos.Texto);
        AgregaCampo("DP_CIUDAD", "DP Ciudad", Tipo_Datos.Texto);
        AgregaCampo("DP_ESTADO", "DP Estado", Tipo_Datos.Texto);
        AgregaCampo("DP_PAIS", "DP Pais", Tipo_Datos.Texto);
        AgregaCampo("DP_CP", "DP CP", Tipo_Datos.Texto);
        AgregaCampo("DP_TELEFONO1", "DP Telefono1", Tipo_Datos.Texto);
        AgregaCampo("DP_TELEFONO2", "DP Telefono2", Tipo_Datos.Texto);
        AgregaCampo("DP_CELULAR1", "DP Celular1", Tipo_Datos.Texto);
        AgregaCampo("DP_CELULAR2", "DP Celular2", Tipo_Datos.Texto);
        AgregaCampo("DT_CALLE_NO", "DT Calle y No.", Tipo_Datos.Texto);
        AgregaCampo("DT_COLONIA", "DT Colonia", Tipo_Datos.Texto);
        AgregaCampo("DT_DELEGACION", "DT Delegacion", Tipo_Datos.Texto);
        AgregaCampo("DT_CIUDAD", "DT Ciudad", Tipo_Datos.Texto);
        AgregaCampo("DT_ESTADO", "DT Estado", Tipo_Datos.Texto);
        AgregaCampo("DT_PAIS", "DT Pais", Tipo_Datos.Texto);
        AgregaCampo("DT_CP", "DT CP", Tipo_Datos.Texto);
        AgregaCampo("DT_TELEFONO1", "DT Telefono1", Tipo_Datos.Texto);
        AgregaCampo("DT_TELEFONO2", "DT Telefono2", Tipo_Datos.Texto);
        AgregaCampo("DT_CELULAR1", "DT Celular1", Tipo_Datos.Texto);
        AgregaCampo("DT_CELULAR2", "DT Celular2", Tipo_Datos.Texto);
        AgregaCampo("CENTRO_DE_COSTOS", "Centro de costos", Tipo_Datos.Texto);
        AgregaCampo("AREA", "Area", Tipo_Datos.Texto);
        AgregaCampo("DEPARTAMENTO", "Departamento", Tipo_Datos.Texto);
        AgregaCampo("PUESTO", "Puesto", Tipo_Datos.Texto);
        AgregaCampo("GRUPO", "Grupo", Tipo_Datos.Texto);
        AgregaCampo("NO_CREDENCIAL", "No. Credencial", Tipo_Datos.Texto);
        AgregaCampo("LINEA_PRODUCCION", "Linea de produccion", Tipo_Datos.Texto);
        AgregaCampo("CLAVE_EMPL", "Clave de empleado", Tipo_Datos.Texto);
        AgregaCampo("COMPANIA", "Compania", Tipo_Datos.Texto);
        AgregaCampo("EMPRESA", "Empresa", Tipo_Datos.Texto);
        AgregaCampo("DIVISION", "Division", Tipo_Datos.Texto);
        AgregaCampo("REGION", "Region", Tipo_Datos.Texto);
        AgregaCampo("TIPO_NOMINA", "Tipo de nomina", Tipo_Datos.Texto);
        AgregaCampo("ZONA", "Zona", Tipo_Datos.Texto);
        AgregaCampo("CAMPO1", "Campo1", Tipo_Datos.Texto);
        AgregaCampo("CAMPO2", "Campo2", Tipo_Datos.Texto);
        AgregaCampo("CAMPO3", "Campo3", Tipo_Datos.Texto);
        AgregaCampo("CAMPO4", "Campo4", Tipo_Datos.Texto);
        AgregaCampo("CAMPO5", "Campo5", Tipo_Datos.Texto);
        AgregaCampo("REGLA_VACA_ID", "Regla de Vacaciones", Tipo_Datos.Entero);
        AgregaCampo("SUELDO_DIA", "Sueldo Dia", Tipo_Datos.Decimal);


        AgregaCampo("TERMINALES_DEXTRAS_ID", "ID. ", Tipo_Datos.Entero);
        AgregaCampo("PERSONA_NOMBRE", "Nombre Completo", Tipo_Datos.Texto);
        AgregaCampo("PERSONA_EMAIL", "Correo Electrónico", Tipo_Datos.Texto);
        AgregaCampo("PERSONA_BORRADO", "Inactivo", Tipo_Datos.Boleano);
        AgregaCampo("PERSONA_LINK_ID", "No. Empleado", Tipo_Datos.Entero);
        AgregaCampo("AGRUPACION_NOMBRE", "Agrupación", Tipo_Datos.Texto);
        //AgregaCampo("AGRUPACION_NOMBRE", "Agrupación", Tipo_Datos.Texto);

        AgregaCampo("Campo_Nombre", "Nombre del Campo", Tipo_Datos.Texto);
        AgregaCampo("Campo_Etiqueta", "Etiqueta del Campo", Tipo_Datos.Texto);
        AgregaCampo("CAMPO_LONGITUD", "Longitud del Campo", Tipo_Datos.Entero);
        AgregaCampo("CAMPO_ANCHO_GRID", "Ancho en Grid", Tipo_Datos.Entero);
        AgregaCampo("CAMPO_TE_ORDEN", "Orden de aparición", Tipo_Datos.Entero);
        AgregaCampo("CAMPO_TE_ES_LLAVE", "Es Llave", Tipo_Datos.Boleano);
        AgregaCampo("CAMPO_TE_ES_AUTONUM", "Es autonúmerico", Tipo_Datos.Boleano);
        AgregaCampo("CAMPO_TE_FILTRO", "Se aplica en Filtro", Tipo_Datos.Boleano);
        AgregaCampo("CAMPO_TE_INVISIBLE", "Invisible", Tipo_Datos.Boleano);
        AgregaCampo("CATALOGO_ID", "ID Catalogo", Tipo_Datos.Entero);
        AgregaCampo("MASCARA_ID", "ID Mascara", Tipo_Datos.Entero);
        AgregaCampo("TIPO_DATO_ID", "ID Tipo de Dato", Tipo_Datos.Entero);

        AgregaCampo("TIPO_DATO_ID", "ID Tipo de Dato", Tipo_Datos.Entero);
        AgregaCampo("TURNO_ID", "Turno", Tipo_Datos.Entero);
        AgregaCampo("TURNO_NOMBRE", "Nombre del turno", Tipo_Datos.Texto);
        AgregaCampo("TURNO_DIA_HERETARDO", "Hora de Retardo", Tipo_Datos.Hora);
        AgregaCampo("TURNO_ASISTENCIA", "Asistencia", Tipo_Datos.Boleano);
        AgregaCampo("TURNO_PHEXTRAS", "Permite horas extras", Tipo_Datos.Boleano);
        AgregaCampo("TURNO", "Turno", Tipo_Datos.Texto);
        AgregaCampo("ENTRADASALIDA", "Checadas", Tipo_Datos.Texto);


        AgregaCampo("SUSCRIPCION_ID", "Suscripcion ID", Tipo_Datos.Entero);

        AgregaCampo("SUSCRIPCION_NOMBRE", "Suscripcion", Tipo_Datos.Texto);

        AgregaCampo("TIPO_INC_C_SIS_NOMBRE", "Tipo Comida", Tipo_Datos.Entero);

        AgregaCampo("TIPO_INCIDENCIAS_EX_ID", "Tipo Incidencia Ex", Tipo_Datos.Entero);
        AgregaCampo("TIPO_INCIDENCIAS_EX_TXT", "Texto Incidencia Ex ", Tipo_Datos.Texto);
        AgregaCampo("TIPO_INCIDENCIAS_EX_NOMBRE", "Nombre Incidencia Ex ", Tipo_Datos.Texto);
        AgregaCampo("TIPO_INCIDENCIAS_EX_DESC", "Descripción Incidencia Ex", Tipo_Datos.Texto);
        AgregaCampo("TIPO_INCIDENCIAS_EX_PARAM", "Parametro Incidencia Ex", Tipo_Datos.Texto);
        AgregaCampo("TIPO_FALTA_EX_ID", "Tipo de Falta", Tipo_Datos.Entero);
        AgregaCampo("TIPO_INCIDENCIAS_EX_BORRADO", "Incidencia Ex Borrada", Tipo_Datos.Boleano);
        AgregaCampo("TIPO_INC_SIS_EX", "Incidencia del Sistema Ex", Tipo_Datos.Entero);

        AgregaCampo("TIPO_INCIDENCIA_ID", "Tipo Incidencia", Tipo_Datos.Entero);
        AgregaCampo("TIPO_INCIDENCIA_NOMBRE", "Nombre de Incidencia", Tipo_Datos.Texto);
        AgregaCampo("TIPO_INCIDENCIA_ABR", "Abreviatura", Tipo_Datos.Texto);
        AgregaCampo("TIPO_INCIDENCIA_BORRADO", "Borrado", Tipo_Datos.Boleano);

        AgregaCampo("TIPO_INC_SIS_ID", "Inc. del Sistema", Tipo_Datos.Entero);
        AgregaCampo("TIPO_INC_SIS_NOMBRE", "Nombre de Incidencia", Tipo_Datos.Texto);
        AgregaCampo("TIPO_INC_SIS_ABR", "Abreviatura", Tipo_Datos.Texto);

        AgregaCampo("PERFIL_ID", "Perfil", Tipo_Datos.Entero);
        AgregaCampo("PERFIL_NOMBRE", "Nombre", Tipo_Datos.Texto);
        AgregaCampo("PERFIL_BORRADO", "Borrado", Tipo_Datos.Boleano);

        AgregaCampo("TIPO_TURNO_NOMBRE", "Tipo de Turno", Tipo_Datos.Texto);
        AgregaCampo("TIPO_TURNO_ID", "Tipo de Turno", Tipo_Datos.Entero);
        AgregaCampo("TURNO_DIA_HE", "Hora de Entrada", Tipo_Datos.Hora);
        AgregaCampo("TURNO_DIA_HS", "Hora de Salida", Tipo_Datos.Hora);

        AgregaCampo("TERMINAL_ID", "Terminal", Tipo_Datos.Entero);
        AgregaCampo("TERMINAL_NOMBRE", "Nombre de Terminal", Tipo_Datos.Texto);
        AgregaCampo("TERMINAL_CAMPO_LLAVE", "Campo Llave", Tipo_Datos.Texto);
        AgregaCampo("TERMINAL_CAMPO_ADICIONAL", "Campo Llave Adicional", Tipo_Datos.Texto);

        AgregaCampo("ENV_REPORTE_ID", "Reporte ID", Tipo_Datos.Entero);
        AgregaCampo("ENV_REPORTE_EMAIL", "E-Mail", Tipo_Datos.Texto);
        AgregaCampo("REPORTE_ID", "Tipo de Reporte", Tipo_Datos.Entero);
        AgregaCampo("ENV_REPORTE_DESCRIPCION", "Descripción", Tipo_Datos.Texto);
        AgregaCampo("ENV_REPORTE_FECHAHORA", "Última Ejecución", Tipo_Datos.Fecha_y_Hora);
        AgregaCampo("ENV_REPORTE_FECHAHORAC", "Fecha de Creación", Tipo_Datos.Fecha);
        AgregaCampo("ENV_REPORTE_FECHAHORAE", "Siguiente Ejecución", Tipo_Datos.Fecha_y_Hora);
        AgregaCampo("ENV_REPORTE_C_DIAS", "Periodo de Envio", Tipo_Datos.Entero);
        AgregaCampo("ENV_REPORTE_DIAS_INI", "Últimos Días a Enviar", Tipo_Datos.Entero);
        AgregaCampo("ENV_REPORTE_DIAS_FIN", "Últimos Días a No Enviar", Tipo_Datos.Entero);
        AgregaCampo("ENV_REPORTE_EUVEZ", "Enviar una Vez", Tipo_Datos.Boleano);

        AgregaCampo("ALMACEN_VEC_ID", "Almacén de Vectores", Tipo_Datos.Entero);
        AgregaCampo("ALMACEN_VEC_NOMBRE", "Nombre de Almacén", Tipo_Datos.Texto);
        AgregaCampo("ALMACEN_VEC_BORRADO", "Borrado", Tipo_Datos.Boleano);

        AgregaCampo("MODELO_TERMINAL_ID", "Modelo de Terminal", Tipo_Datos.Entero);
        AgregaCampo("TIPO_TECNOLOGIA_ID", "Tecnología", Tipo_Datos.Texto);
        AgregaCampo("TIPO_TECNOLOGIA_ADD_ID", "Tecnología Adicional", Tipo_Datos.Texto);
        AgregaCampo("TERMINAL_ENROLA", "Puede Enrolar", Tipo_Datos.Boleano);
        AgregaCampo("TERMINAL_ASISTENCIA", "Genera Asistencia", Tipo_Datos.Boleano);
        AgregaCampo("TERMINAL_COMIDA", "Cobra Comidas", Tipo_Datos.Boleano);

        AgregaCampo("SITIO_ID", "Sitio", Tipo_Datos.Entero);
        AgregaCampo("SITIO_NOMBRE", "Sitio Nombre", Tipo_Datos.Texto);
        AgregaCampo("SITIO_CONSULTA", "Consulta", Tipo_Datos.Texto);
        AgregaCampo("SITIO_BORRADO", "Borrado", Tipo_Datos.Boleano);

        AgregaCampo("PERSONA_D_HE_ID", "Tiempo Extra ID", Tipo_Datos.Entero);
        AgregaCampo("PERSONA_ID", "ID", Tipo_Datos.Entero);
        AgregaCampo("PERSONA_D_HE_SIS", "HE Reales", Tipo_Datos.Hora);
        AgregaCampo("PERSONA_D_HE_CAL", "HE Calculadas", Tipo_Datos.Hora);
        AgregaCampo("PERSONA_D_HE_APL", "HE a Aplicar", Tipo_Datos.Hora);
        AgregaCampo("PERSONA_D_HE_SIS_A", "HE Reales", Tipo_Datos.Hora);
        AgregaCampo("PERSONA_D_HE_SIS_D", "HE Reales", Tipo_Datos.Hora);
        AgregaCampo("PERSONA_D_HE_FH", "Confirmacion HE", Tipo_Datos.Fecha_y_Hora);
        AgregaCampo("PERSONA_D_HE_CAL", "HE Calculadas", Tipo_Datos.Hora);
        AgregaCampo("PERSONA_D_HE_APL", "HE a Aplicar", Tipo_Datos.Hora);
        AgregaCampo("PERSONA_D_HE_SIS_A", "HE Reales", Tipo_Datos.Hora);
        AgregaCampo("PERSONA_D_HE_SIS_D", "HE Reales", Tipo_Datos.Hora);
        AgregaCampo("PERSONA_D_HE_SIMPLE", "HE Simples", Tipo_Datos.Decimal);
        AgregaCampo("PERSONA_D_HE_DOBLE", "HE Dobles", Tipo_Datos.Decimal);
        AgregaCampo("PERSONA_D_HE_TRIPLE", "HE Triples", Tipo_Datos.Decimal);
        AgregaCampo("PERSONA_D_HE_COMEN", "Comentarios", Tipo_Datos.Texto);
        AgregaCampo("TIPO_INCIDENCIA_IDHE", "Tipo Incidencia HE", Tipo_Datos.Entero);



        AgregaCampo("RECOMPENSA_ID", "Recompensa", Tipo_Datos.Entero);
        AgregaCampo("RECOMPENSA_NOMBRE", "Nombre", Tipo_Datos.Texto);
        AgregaCampo("RECOMPENSA_POR", "Porcentaje", Tipo_Datos.Entero);
        AgregaCampo("RECOMPENSA_PT", "Puntos", Tipo_Datos.Entero);
        AgregaCampo("RECOMPENSA_COMEN", "Comentario", Tipo_Datos.Texto);
        AgregaCampo("RECOMPENSA_BORRADO", "Borrado", Tipo_Datos.Boleano);

        AgregaCampo("PROMOCION_ID", "Promocion", Tipo_Datos.Entero);
        AgregaCampo("PROMOCION_NO_MOV", "Numero De Movimiento", Tipo_Datos.Entero);
        AgregaCampo("PROMOCION_NOMBRE", "Nombre", Tipo_Datos.Texto);
        AgregaCampo("PROMOCION_COMENTARIO", "Comentario", Tipo_Datos.Texto);
        AgregaCampo("PROMOCION_DESDE", "Fecha Inicial", Tipo_Datos.Fecha_y_Hora);
        AgregaCampo("PROMOCION_HASTA", "Fecha Final", Tipo_Datos.Fecha_y_Hora);
        AgregaCampo("PROMOCION_BORRADO", "Borrado", Tipo_Datos.Boleano);

        AgregaCampo("DIA_FESTIVO_FECHA", "Fecha", Tipo_Datos.Fecha);

        AgregaCampo("PRODUCTO_ID", "Producto", Tipo_Datos.Entero);
        AgregaCampo("SESION_ID", "ID Sesion", Tipo_Datos.Entero);
        AgregaCampo("PRODUCTO_NO", "Número de Producto", Tipo_Datos.Entero);
        AgregaCampo("PRODUCTO", "Producto", Tipo_Datos.Texto);
        AgregaCampo("PRODUCTO_COSTO", "Costo", Tipo_Datos.Entero);
        AgregaCampo("PRODUCTO_BORRADO", "Borrado", Tipo_Datos.Boleano);

        AgregaCampo("SUSCRIPCION_ID", "Suscripcion", Tipo_Datos.Entero);
        AgregaCampo("SUSCRIPCION_NOMBRE", "Suscripcion Nombre", Tipo_Datos.Texto);
        AgregaCampo("SUSCRIPCION_BORRADO", "Inactivo", Tipo_Datos.Boleano);

        AgregaCampo("USUARIO_ID", "Usuario", Tipo_Datos.Entero);
        AgregaCampo("USUARIO_CLAVE", "Password", Tipo_Datos.Texto);
        AgregaCampo("USUARIO_USUARIO", "Usuario", Tipo_Datos.Texto);
        AgregaCampo("USUARIO_NOMBRE", "Nombre", Tipo_Datos.Texto);
        AgregaCampo("USUARIO_DESCRIPCION", "Descripción", Tipo_Datos.Texto);
        AgregaCampo("USUARIO_EMAIL", "Email Usuario", Tipo_Datos.Texto);
        AgregaCampo("USUARIO_ENVMAILA", "Enviar EMail", Tipo_Datos.Boleano);
        AgregaCampo("USUARIO_BORRADO", "Inactivo", Tipo_Datos.Boleano);

        AgregaCampo("FORMATO_REP_ID", "Formato de Reporte", Tipo_Datos.Entero);

        //////////////////////////////////////////////////////////////////////////
        // Campos Virtuales
        AgregaCampo("FIL_DIAS_N_LAB", "Ver días no laborables", Tipo_Datos.Boleano);
        AgregaCampo("FIL_FECHA", "Fecha", Tipo_Datos.Fecha);

        AgregaCampo("SITIO_HHASTA_SVEC", "Fin de Sincronizacion", Tipo_Datos.Hora);
        AgregaCampo("SITIO_HDESDE_SVEC", "Inicio de Sincronizacion", Tipo_Datos.Hora);
        AgregaCampo("SITIO_SEGUNDOS_SYNC", "Segundos entre Sincronizacion", Tipo_Datos.Entero);

        AgregaCampo("TERMINAL_MINUTOS_DIF", "Diferencia de minutos con el servidor", Tipo_Datos.Entero);
        AgregaCampo("TERMINAL_SINCRONIZACION", "Segs entre sincronizacion", Tipo_Datos.Entero);
        AgregaCampo("TERMINAL_VALIDAHORARIOE", "Valida Horario de Entrada", Tipo_Datos.Boleano);
        AgregaCampo("TERMINAL_VALIDAHORARIOS", "Valida Horario de Salida", Tipo_Datos.Boleano);
        AgregaCampo("TERMINAL_BORRADO", "Inactivo", Tipo_Datos.Boleano);
        AgregaCampo("TERMINAL_DIR", "Direccion", Tipo_Datos.Texto);
        AgregaCampo("TIPO_TERMINAL_ACCESO_ID", "Tipo de terminal de acceso", Tipo_Datos.Texto);
        AgregaCampo("TIPO_PERSONA_ID", "Tipo de Persona", Tipo_Datos.Entero);
        AgregaCampo("TIENE_ACCESO", "¿Tiene Acceso?", Tipo_Datos.Boleano);

        AgregaCampo("PERSONA_CLAVE", "Clave", Tipo_Datos.Texto);
        AgregaCampo("PERSONA_DIARIO_FECHA", "Fecha", Tipo_Datos.Fecha);
        AgregaCampo("PERSONA_DIARIO_TT", "Tiempo Trabajado", Tipo_Datos.Hora);
        AgregaCampo("PERSONA_DIARIO_TDE", "Tiempo de Retardo", Tipo_Datos.Hora);
        AgregaCampo("INCIDENCIA_COMENTARIO", "Comentario", Tipo_Datos.Texto);

        AgregaCampo("REGLA_VACA_ID", "Regla ID", Tipo_Datos.Entero);
        AgregaCampo("REGLA_VACA_NOMBRE", "Nombre de la Regla", Tipo_Datos.Texto);
        AgregaCampo("REGLA_VACA_COMENTARIO", "Comentario", Tipo_Datos.Texto);
        AgregaCampo("REGLA_VACA_P_AD", "Permite Vacaciones Adelantadas", Tipo_Datos.Boleano);
        AgregaCampo("REGLA_VACA_P_PROP", "Permite Vacaciones Proporcionales", Tipo_Datos.Boleano);
        AgregaCampo("REGLA_VACA_BORRADO", "Borrado", Tipo_Datos.Boleano);

        AgregaCampo("REGLAS_VACA_DET_ANO", "Antigüedad", Tipo_Datos.Entero);
        AgregaCampo("REGLAS_VACA_DET_DIAS", "Días de Vacaciones", Tipo_Datos.Entero);

        AgregaCampo("SUELDO_DIA", "Sueldo por Día", Tipo_Datos.Decimal);

        AgregaCampo("TIPO_VACA_DISFRUTA_NOMBRE", "Tipo de Vacaciones", Tipo_Datos.Texto);
        AgregaCampo("VACA_DISFRUTA_FECHA", "Fecha de Disfrute", Tipo_Datos.Fecha);
        AgregaCampo("ANIO_ANTIGUEDAD", "Año de Antigüedad", Tipo_Datos.Entero);

        AgregaCampo("CONFIG_USUARIO_VARIABLE", "Variable", Tipo_Datos.Texto);
        AgregaCampo("CONFIG_USUARIO_VALOR", "Valor", Tipo_Datos.Texto);



        AgregaCampo("ACCESO_FECHAHORA", "Fecha y Hora", Tipo_Datos.Fecha_y_Hora);
        AgregaCampo("TIPO_ACCESO_NOMBRE", "Tipo de Acceso", Tipo_Datos.Texto);
        AgregaCampo("ACCESO_CALCULADO", "Calculado", Tipo_Datos.Boleano);
        AgregaCampo("ACCESO_ID", "Identificador", Tipo_Datos.Entero);
        AgregaCampo("ACCESO_E", "Entrada", Tipo_Datos.Hora);
        AgregaCampo("ACCESO_S", "Salida", Tipo_Datos.Hora);
        AgregaCampo("ACCESO_CS", "Salida Comida", Tipo_Datos.Hora);
        AgregaCampo("ACCESO_CR", "Regreso Comida", Tipo_Datos.Hora);

        AgregaCampo("Salida", "Salida", Tipo_Datos.Hora);
        AgregaCampo("Entrada", "Entrada", Tipo_Datos.Hora);

        AgregaCampo("SALIDA", "Salida", Tipo_Datos.Hora);
        AgregaCampo("ENTRADA", "Entrada", Tipo_Datos.Hora);

        AgregaCampo("SALIDA_COMER", "Salida Comida", Tipo_Datos.Hora);
        AgregaCampo("REGRESO_COMER", "Regreso Comida", Tipo_Datos.Hora);

        AgregaCampo("PERSONA_ES_ID", "Id Dia Asist", Tipo_Datos.Entero);
        AgregaCampo("PERSONA_ES_ORD", "Fecha", Tipo_Datos.Fecha);
        AgregaCampo("ACCESO_E_ES", "Entrada", Tipo_Datos.Hora);
        AgregaCampo("ACCESO_S_ES", "Salida", Tipo_Datos.Hora);
        AgregaCampo("PERSONA_ES_TE", "Tiempo de Estancia", Tipo_Datos.Hora);

        AgregaCampo("PERSONA_DIARIO_ID", "Id Dia Asist", Tipo_Datos.Entero);
        AgregaCampo("PERSONA_DIARIO_FECHA", "Fecha", Tipo_Datos.Fecha);
        AgregaCampo("HORA_ENTRADA", "Hora de Entrada", Tipo_Datos.Hora);
        AgregaCampo("HORA_SALIDA", "Hora de Salida", Tipo_Datos.Hora);
        AgregaCampo("HORA_SALIDA_COMIDA", "Hora de Salida Comida", Tipo_Datos.Hora);
        AgregaCampo("HORA_REGRESO_COMIDA", "Hora de Regreso Comida", Tipo_Datos.Hora);
        AgregaCampo("TURNO_DIA_HCS", "Salida Comida", Tipo_Datos.Hora);
        AgregaCampo("TURNO_DIA_HCR", "Regreso Comida", Tipo_Datos.Hora);
        AgregaCampo("PERSONA_DIARIO_TE", "Tiempo de Estancia", Tipo_Datos.Hora);
        AgregaCampo("PERSONA_DIARIO_TC", "Tiempo de Comida", Tipo_Datos.Hora);
        AgregaCampo("PERSONA_DIARIO_TES", "Horas X Trabajar", Tipo_Datos.Hora);

        AgregaCampo("INCIDENCIA_NOMBRE", "Tipo de Incidencia", Tipo_Datos.Texto);
        AgregaCampo("INCIDENCIA_ID", "Identificador", Tipo_Datos.Texto);
        AgregaCampo("INCIDENCIA_ABR", "Abreviatura", Tipo_Datos.Texto);

        AgregaCampo("PAsistencias", "% Asistencias", Tipo_Datos.Texto);
        AgregaCampo("PRetardos", "% Retardos", Tipo_Datos.Texto);
        AgregaCampo("PFaltas", "% Faltas", Tipo_Datos.Texto);
        AgregaCampo("PIncidencias", "% Incidencias", Tipo_Datos.Texto);
        AgregaCampo("PDiasFestivos", "% Dias Festivos", Tipo_Datos.Texto);
        AgregaCampo("PDiasDescanso", "% Dias Descanso", Tipo_Datos.Texto);

        AgregaCampo("INCIDENCIA_ABR", "Abreviatura", Tipo_Datos.Texto);

        AgregaCampo("TIPO_PERMISO_ID", "Permisos", Tipo_Datos.Entero);
        AgregaCampo("TIPO_PERMISO_NOMBRE", "Permisos", Tipo_Datos.Texto);


        AgregaCampo("USUARIO_PERMISO_ID", "Permiso ID", Tipo_Datos.Entero);
        AgregaCampo("USUARIO_PERMISO", "Permiso Sobre", Tipo_Datos.Texto);


        AgregaCampo("DIA_FESTIVO_ID", "Dia Festivo ID", Tipo_Datos.Entero);
        AgregaCampo("DIA_FESTIVO_NOMBRE", "Nombre Festivo", Tipo_Datos.Texto);
        AgregaCampo("DIA_FESTIVO_FECHA", "Fecha Festivo", Tipo_Datos.Fecha);
        AgregaCampo("DIA_FESTIVO_BORRADO", "Inactivo", Tipo_Datos.Boleano);

        AgregaCampo("DIA_FESTIVO_ID", "Dia Festivo ID", Tipo_Datos.Entero);
        AgregaCampo("DIA_FESTIVO_NOMBRE", "Nombre Festivo", Tipo_Datos.Texto);
        AgregaCampo("DIA_FESTIVO_FECHA", "Fecha Festivo", Tipo_Datos.Fecha);
        AgregaCampo("DIA_FESTIVO_BORRADO", "Inactivo", Tipo_Datos.Boleano);

        AgregaCampo("TERMINALES_DEXTRAS_ID", "ID D.Extras", Tipo_Datos.Entero);
        AgregaCampo("TIPO_TERM_DEXTRAS_ID", "Proveniente", Tipo_Datos.Entero);
        AgregaCampo("TERMINALES_DEXTRAS_TEXTO1", "No.Empleado", Tipo_Datos.Texto);
        AgregaCampo("TERMINALES_DEXTRAS_TEXTO2", "Otros", Tipo_Datos.Texto);

        AgregaCampo("LOG_AUDITORIA_ID", "ID Log Auditoria", Tipo_Datos.Entero);
        AgregaCampo("LOG_AUDITORIA_FECHAHORA", "Fecha", Tipo_Datos.Fecha_y_Hora);
        AgregaCampo("TIPO_AUDITORIA_NOMBRE", "Tipo", Tipo_Datos.Texto);
        AgregaCampo("LOG_AUDITORIA_DESCRIPCION", "Descripcion", Tipo_Datos.Texto);

        AgregaCampo("TIPO_NOMINA_ID", "ID TNomina", Tipo_Datos.Entero);
        AgregaCampo("TIPO_NOMINA_NOMBRE", "Tipo de Nomina", Tipo_Datos.Texto);
        AgregaCampo("TIPO_NOMINA_IDEX", "Id Externo", Tipo_Datos.Texto);
        AgregaCampo("TIPO_NOMINA_BORRADO", "Inactivo", Tipo_Datos.Boleano);

        AgregaCampo("PERIODO_ID", "ID Periodo", Tipo_Datos.Entero);
        AgregaCampo("PERIODO_NOM_INICIO", "Nom Inicio", Tipo_Datos.Fecha);
        AgregaCampo("PERIODO_NOM_FIN", "Nom Fin", Tipo_Datos.Fecha);
        AgregaCampo("PERIODO_ASIS_INICIO", "Asis Inicio", Tipo_Datos.Fecha);
        AgregaCampo("PERIODO_ASIS_FIN", "Asis Fin", Tipo_Datos.Fecha);

        AgregaCampo("EDO_PERIODO_ID", "ID Edo.Periodo", Tipo_Datos.Entero);
        AgregaCampo("EDO_PERIODO_NOMBRE", "Edo.Perido", Tipo_Datos.Texto);

        AgregaCampo("ALMACEN_INC_ID", "ID", Tipo_Datos.Entero);
        AgregaCampo("TIPO_ALMACEN_INC_ID", "ID", Tipo_Datos.Entero);
        AgregaCampo("TIPO_ALMACEN_INC_NOMBRE", "Tipo Mov", Tipo_Datos.Texto);
        AgregaCampo("ALMACEN_INC_FECHA", "Fecha", Tipo_Datos.Fecha);
        AgregaCampo("ALMACEN_INC_NO", "Movimiento", Tipo_Datos.Decimal);
        AgregaCampo("ALMACEN_INC_SALDO", "Saldo", Tipo_Datos.Decimal);
        AgregaCampo("ALMACEN_INC_SIGUIENTE", "Siguiente P", Tipo_Datos.Fecha);
        AgregaCampo("ALMACEN_INC_AUTOM", "Autogenerado", Tipo_Datos.Boleano);
        AgregaCampo("ALMACEN_INC_COMEN", "Comentarios", Tipo_Datos.Texto);
        AgregaCampo("ALMACEN_INC_EXTRAS", "Extras", Tipo_Datos.Texto);
        AgregaCampo("PERSONA_TERMINAL_UPDATE", "Fecha Modificacion", Tipo_Datos.Fecha_y_Hora);

        // Reporte Comidas Totales
        AgregaCampo("TOTAL_PRIMERA_COMIDA_COSTO", "Costo Primeras Comidas", Tipo_Datos.Decimal);
        AgregaCampo("TOTAL_PRIMERA_COMIDA", "No. Primeras Comidas", Tipo_Datos.Decimal);
        AgregaCampo("TOTAL_SEGUNDA_COMIDA_COSTO", "Costo Segundas Comidas", Tipo_Datos.Decimal);
        AgregaCampo("TOTAL_SEGUNDA_COMIDA", "No. Segundas Comidas", Tipo_Datos.Decimal);
        AgregaCampo("TOTAL_TIPO_COMIDA", "No. Comidas", Tipo_Datos.Entero);
        AgregaCampo("TOTAL_COMIDA_COSTO", "Total Costo Comida", Tipo_Datos.Decimal);

        // Reporte Comidas
        AgregaCampo("PERSONA_COMIDA_FECHA", "Fecha", Tipo_Datos.Fecha);
        AgregaCampo("TIPO_COBRO", "Tipo de Cobro", Tipo_Datos.Texto);
        AgregaCampo("TIPO_COMIDA_COSTOA", "Tipo Comida Costo", Tipo_Datos.Decimal);
        AgregaCampo("EMPRESA", "Empresa", Tipo_Datos.Texto);
        AgregaCampo("NUMERO_COMIDAS", "No. Comidas", Tipo_Datos.Entero);
        AgregaCampo("PRECIO", "Precio", Tipo_Datos.Decimal);
        //Reporte Monedero
        AgregaCampo("MONEDERO_ID", "Monedero", Tipo_Datos.Entero);
        AgregaCampo("MONEDERO_FECHA", "Fecha", Tipo_Datos.Fecha);
        AgregaCampo("MONEDERO_SALDO", "Saldo", Tipo_Datos.Decimal);
        AgregaCampo("MONEDERO_CONSUMO", "Consumo", Tipo_Datos.Decimal);
        AgregaCampo("ABONO", "Abono", Tipo_Datos.Decimal);
        //AgregaCampo("DIVISION", "Empresa", Tipo_Datos.Texto);
        AgregaCampo("SUBTOTAL_CONSUMO_EMPRESA", "Sub-Total Consumo", Tipo_Datos.Entero);
        AgregaCampo("SUBTOTAL_ABONO_EMPRESA", "Sub-Total Abono", Tipo_Datos.Entero);
        AgregaCampo("TOTAL_CONSUMO", "Total Consumo", Tipo_Datos.Entero);
        AgregaCampo("TOTAL_ABONO", "Total Abono", Tipo_Datos.Entero);

        //Reporte Accesos Por Terminal
        AgregaCampo("FECHAHORA_ENTRADA", "Fecha y Hora Entrada", Tipo_Datos.Fecha_y_Hora);
        AgregaCampo("TERMINAL_NOMBRE_ENTRADA", "Terminal Entrada", Tipo_Datos.Texto);
        AgregaCampo("FECHAHORA_SALIDA", "Fecha y Hora Salida", Tipo_Datos.Fecha_y_Hora);
        AgregaCampo("TERMINAL_NOMBRE_SALIDA", "Terminal Salida", Tipo_Datos.Texto);

        TA_Campos.Update(m_ds_Campos.EC_CAMPOS);
    }
    #region DataSet Table Empleados (EC_PERSONAS_DATOS)
    /// <summary>
    /// Obtiene un dataset que contiene el listado de empleados (EC_PERSONAS_DATOS)
    /// </summary>
    /// <returns></returns>
    public static DataSet ObtenDataSetTE()
    {
        return ObtenDataSetTE(false);
    }

    /// <summary>
    /// Obtiene un dataset que contiene el listado de empleados (EC_PERSONAS_DATOS)
    /// </summary>
    /// <param name="MostrarBorrados"></param>
    /// <returns></returns>
    public static DataSet ObtenDataSetTE(bool MostrarBorrados)
    {
        return ObtenDataSetTE(MostrarBorrados, "");
    }

    /// <summary>
    /// Obtiene un dataset que contiene el listado de empleados (EC_PERSONAS_DATOS)
    /// </summary>
    /// <param name="MostrarBorrados"></param>
    /// <param name="QryAdicional">complemento de comparación (despues del where)</param>
    /// <returns></returns>
    public static DataSet ObtenDataSetTE(bool MostrarBorrados, string QryAdicional)
    {
        if (QryAdicional.Length > 0)
        {
            if (QryAdicional.IndexOf("and", StringComparison.InvariantCultureIgnoreCase) < 0)
                QryAdicional = "and " + QryAdicional;
        }

        if (MostrarBorrados)
            return (DataSet)CeC_BD.EjecutaDataSet("SELECT  " + ObtenListaCamposTE() + ",PERSONA_EMAIL, PERSONA_BORRADO FROM EC_PERSONAS_DATOS, EC_PERSONAS WHERE EC_PERSONAS_DATOS.PERSONA_ID = EC_PERSONAS.PERSONA_ID " + QryAdicional);
        else
            return (DataSet)CeC_BD.EjecutaDataSet("SELECT  " + ObtenListaCamposTE() + ",PERSONA_EMAIL, PERSONA_BORRADO FROM EC_PERSONAS_DATOS, EC_PERSONAS WHERE EC_PERSONAS_DATOS.PERSONA_ID = EC_PERSONAS.PERSONA_ID AND PERSONA_BORRADO = 0 " + " " + QryAdicional);
        /*
         return (DataSet)CeC_BD.EjecutaDataSet("SELECT EC_PERSONAS.PERSONA_ID, PERSONA_NOMBRE, " + ObtenListaCamposTE() + ",PERSONA_EMAIL, PERSONA_BORRADO FROM EC_PERSONAS_DATOS, EC_PERSONAS WHERE EC_PERSONAS_DATOS.PERSONA_ID = EC_PERSONAS.PERSONA_ID " + QryAdicional);
        else
            return (DataSet)CeC_BD.EjecutaDataSet("SELECT EC_PERSONAS.PERSONA_ID, PERSONA_NOMBRE, " + ObtenListaCamposTE() + ",PERSONA_EMAIL, PERSONA_BORRADO FROM EC_PERSONAS_DATOS, EC_PERSONAS WHERE EC_PERSONAS_DATOS.PERSONA_ID = EC_PERSONAS.PERSONA_ID AND PERSONA_BORRADO = 0 " + " " + QryAdicional);
*/

    }

    /// <summary>
    /// Obtiene un dataset que contiene el listado de empleados (EC_PERSONAS_DATOS) para un Grid
    /// </summary>
    /// <returns></returns>
    public static DataSet ObtenDataSetTEGrid()
    {
        return ObtenDataSetTEGrid(false);
    }

    /// <summary>
    /// Obtiene un dataset que contiene el listado de empleados (EC_PERSONAS_DATOS) para un Grid
    /// </summary>
    /// <param name="MostrarBorrados"></param>
    /// <returns></returns>
    public static DataSet ObtenDataSetTEGrid(bool MostrarBorrados)
    {
        return ObtenDataSetTEGrid(MostrarBorrados, "");
    }

    /// <summary>
    /// Obtiene un dataset que contiene el listado de empleados (EC_PERSONAS_DATOS)para un Grid
    /// </summary>
    /// <param name="MostrarBorrados"></param>
    /// <param name="QryAdicional">complemento de comparación (despues del where)</param>
    /// <returns></returns>
    public static DataSet ObtenDataSetTEGrid(bool MostrarBorrados, string QryAdicional)
    {
        if (QryAdicional.Length > 0)
        {
            if (QryAdicional.IndexOf("and", StringComparison.InvariantCultureIgnoreCase) < 0)
                QryAdicional = "and " + QryAdicional;
        }
        if (MostrarBorrados)
            return (DataSet)CeC_BD.EjecutaDataSet("SELECT PERSONA_NOMBRE, " + ObtenListaCamposTEGrid() + ",PERSONA_EMAIL, SUSCRIPCION_ID, TURNO_ID, PERSONA_BORRADO FROM EC_PERSONAS_DATOS, EC_PERSONAS WHERE PERSONA_LINK_ID = " + CampoTE_Llave + " " + QryAdicional);
        else
            return (DataSet)CeC_BD.EjecutaDataSet("SELECT PERSONA_NOMBRE, " + ObtenListaCamposTEGrid() + ",PERSONA_EMAIL, SUSCRIPCION_ID, TURNO_ID, PERSONA_BORRADO FROM EC_PERSONAS_DATOS, EC_PERSONAS WHERE PERSONA_LINK_ID = " + CampoTE_Llave + " AND PERSONA_BORRADO = 0 " + " " + QryAdicional);
    }

    #endregion
    /// <summary>
    /// Inicializa los campos
    /// </summary>
    /// <returns></returns>
    public bool InicializaCampos()
    {
        m_ds_Campos = new DS_Campos();
        TA_Campos.ClearBeforeFill = true;
        TA_Mascaras.ClearBeforeFill = true;
        TA_Campos.Fill(m_ds_Campos.EC_CAMPOS);
        TA_Mascaras.Fill(m_ds_Campos.EC_MASCARAS);
        TA_Catalogos.ClearBeforeFill = true;
        TA_Catalogos.Fill(m_ds_Campos.EC_CATALOGOS);
        TA_Tipo_Datos.ClearBeforeFill = true;
        TA_Tipo_Datos.Fill(m_ds_Campos.EC_TIPO_DATOS);
        TA_Campos_TE.ClearBeforeFill = true;
        TA_Campos_TE.Fill(m_ds_Campos.EC_CAMPOS_TE);



        /*GuardaConfigCamposW("TERMINALES_DEXTRAS_ID", "ID. ", Tipo_Datos.Entero);
        GuardaConfigCamposW("NOMBRE_COMPLETO", "Nombre Completo", Tipo_Datos.Texto);
        GuardaConfigCamposW("NOMBRE", "Nombre", Tipo_Datos.Texto);
        GuardaConfigCamposW("APATERNO", "Apellido Paterno", Tipo_Datos.Texto);
        GuardaConfigCamposW("AMATERNO", "Apellido Materno", Tipo_Datos.Texto);
        GuardaConfigCamposW("FECHA_NAC", "Fecha de nacimiento", Tipo_Datos.Texto);
        GuardaConfigCamposW("RFC", "RFC", Tipo_Datos.Texto);
        GuardaConfigCamposW("CURP", "CURP", Tipo_Datos.Texto);
        GuardaConfigCamposW("IMSS", "IMSS", Tipo_Datos.Texto);
        GuardaConfigCamposW("ESTUDIOS", "Estudios", Tipo_Datos.Texto);
        GuardaConfigCamposW("SEXO", "Sexo", Tipo_Datos.Texto);
        GuardaConfigCamposW("NACIONALIDAD", "Nacionalidad", Tipo_Datos.Texto);
        GuardaConfigCamposW("FECHA_INGRESO", "Fecha de ingreso", Tipo_Datos.Texto);
        GuardaConfigCamposW("FECHA_BAJA", "Fecha de baja", Tipo_Datos.Texto);
        GuardaConfigCamposW("DP_CALLE_NO", "DP Calle Y No.", Tipo_Datos.Texto);
        GuardaConfigCamposW("DP_COLONIA", "DP Colonia", Tipo_Datos.Texto);
        GuardaConfigCamposW("DP_DELEGACION", "DP Delegacion", Tipo_Datos.Texto);
        GuardaConfigCamposW("DP_CIUDAD", "DP Ciudad", Tipo_Datos.Texto);
        GuardaConfigCamposW("DP_ESTADO", "DP Estado", Tipo_Datos.Texto);
        GuardaConfigCamposW("DP_PAIS", "DP Pais", Tipo_Datos.Texto);
        GuardaConfigCamposW("DP_CP", "DP CP", Tipo_Datos.Texto);
        GuardaConfigCamposW("DP_TELEFONO1", "DP Telefono1", Tipo_Datos.Texto);
        GuardaConfigCamposW("DP_TELEFONO2", "DP Telefono2", Tipo_Datos.Texto);
        GuardaConfigCamposW("DP_CELULAR1", "DP Celular1", Tipo_Datos.Texto);
        GuardaConfigCamposW("DP_CELULAR2", "DP Celular2", Tipo_Datos.Texto);
        GuardaConfigCamposW("DT_CALLE_NO", "DT Calle y No.", Tipo_Datos.Texto);
        GuardaConfigCamposW("DT_COLONIA", "DT Colonia", Tipo_Datos.Texto);
        GuardaConfigCamposW("DT_DELEGACION", "DT Delegacion", Tipo_Datos.Texto);
        GuardaConfigCamposW("DT_CIUDAD", "DT Ciudad", Tipo_Datos.Texto);
        GuardaConfigCamposW("DT_ESTADO", "DT Estado", Tipo_Datos.Texto);
        GuardaConfigCamposW("DT_PAIS", "DT Pais", Tipo_Datos.Texto);
        GuardaConfigCamposW("DT_CP", "DT CP", Tipo_Datos.Texto);
        GuardaConfigCamposW("DT_TELEFONO1", "DT Telefono1", Tipo_Datos.Texto);
        GuardaConfigCamposW("DT_TELEFONO2", "DT Telefono2", Tipo_Datos.Texto);
        GuardaConfigCamposW("DT_CELULAR1", "DT Celular1", Tipo_Datos.Texto);
        GuardaConfigCamposW("DT_CELULAR2", "DT Celular2", Tipo_Datos.Texto);
        GuardaConfigCamposW("CENTRO_DE_COSTOS", "Centro de costos", Tipo_Datos.Texto);
        GuardaConfigCamposW("AREA", "Area", Tipo_Datos.Texto);
        GuardaConfigCamposW("DEPARTAMENTO", "Departamento", Tipo_Datos.Texto);
        GuardaConfigCamposW("PUESTO", "Puesto", Tipo_Datos.Texto);
        GuardaConfigCamposW("GRUPO", "Grupo", Tipo_Datos.Texto);
        GuardaConfigCamposW("NO_CREDENCIAL", "No. Credencial", Tipo_Datos.Texto);
        GuardaConfigCamposW("LINEA_PRODUCCION", "Linea de produccion", Tipo_Datos.Texto);
        GuardaConfigCamposW("CLAVE_EMPL", "Clave de empleado", Tipo_Datos.Texto);
        GuardaConfigCamposW("COMPANIA", "Compania", Tipo_Datos.Texto);
        GuardaConfigCamposW("DIVISION", "Division", Tipo_Datos.Texto);
        GuardaConfigCamposW("REGION", "Region", Tipo_Datos.Texto);
        GuardaConfigCamposW("TIPO_NOMINA", "Tipo de nomina", Tipo_Datos.Texto);
        GuardaConfigCamposW("ZONA", "Zona", Tipo_Datos.Texto);
        GuardaConfigCamposW("CAMPO1", "Campo1", Tipo_Datos.Texto);
        GuardaConfigCamposW("CAMPO2", "Campo2", Tipo_Datos.Texto);
        GuardaConfigCamposW("CAMPO3", "Campo3", Tipo_Datos.Texto);
        GuardaConfigCamposW("CAMPO4", "Campo4", Tipo_Datos.Texto);
        GuardaConfigCamposW("CAMPO5", "Campo5", Tipo_Datos.Texto);
        GuardaConfigCamposW("REGLA_VACA_ID", "Regla de Vacaciones", Tipo_Datos.Entero);
        GuardaConfigCamposW("SUELDO_DIA", "Sueldo Dia", Tipo_Datos.Decimal);
        */
        /*
         * Se guardan los cambios hechos en la tabla EC_Campos
         * */


        return true;

    }

    /// <summary>
    /// Limpia los datos guardados sobre configuración de campos
    /// y los restablece.
    /// </summary>
    /// <returns>Regresa verdadero si el proceso se realizó satisfactoriamente</returns>
    public bool ReinicializaCampos()
    {
        TA_Campos.BorraNoTE();
        return InicializaCampos();
    }

    /// <summary>
    /// Limpia los datos guardados sobre configuración de campos
    /// y los restablece.
    /// </summary>
    /// <returns>Regresa verdadero si el proceso se realizó satisfactoriamente</returns>
    public static bool ReiniciaCampos()
    {
        CeC_Campos Campos = new CeC_Campos();
        return Campos.ReinicializaCampos();

    }


    #region consulta y/o edición de un campo

    /// <summary>
    /// Obtiene el valor almacenado en la base de datos de un determinado campo
    /// </summary>
    /// <param name="Campo">Campo en la base de datos</param>
    /// <param name="Persona_ID">Identificador de persona</param>
    /// <returns>regresa el valor si es que existe, de lo contrario regresa nulo</returns>
    public static object ObtenValor(DS_Campos.EC_CAMPOSRow Campo, int Persona_ID)
    {
        object Valor = null;
        if (Campo.CAMPO_ES_TEMPLEADOS != 0)
            Valor = CeC_BD.EjecutaEscalar("SELECT EC_PERSONAS_DATOS." + Campo.CAMPO_NOMBRE + " FROM EC_PERSONAS_DATOS WHERE PERSONA_ID = " + Persona_ID.ToString());
        else
            Valor = CeC_BD.EjecutaEscalar("SELECT " + Campo.CAMPO_NOMBRE + " FROM EC_PERSONAS WHERE PERSONA_ID = " + Persona_ID.ToString());
        if (Valor == System.DBNull.Value)
            return null;
        return Valor;
    }

    /// <summary>
    /// Guarda un valor en un campo de la tabla de empleados o personas
    /// </summary>
    /// <param name="CampoNombre">Nombre que tiene el campo a guardar</param>
    /// <param name="Persona_ID"></param>
    /// <param name="Valor">Identificador de persona</param>
    /// <returns>regresa verdadero si hiso el cambio de lo contrario puede mandar error</returns>
    public static bool GuardaValor(string CampoNombre, int Persona_ID, object Valor, int SesionID)
    {
        DS_Campos.EC_CAMPOSRow Campo = CeC_Campos.Obten_Campo(CampoNombre.ToString());
        return GuardaValor(Campo, Persona_ID, Valor, SesionID);
    }

    /// <summary>
    /// Guarda un valor en un campo de la tabla de empleados o personas
    /// </summary>
    /// <param name="Campo">Campo en la base de datos</param>
    /// <param name="Persona_ID">Identificador de persona</param>
    /// <param name="Valor"></param>
    /// <returns>regresa verdadero si hiso el cambio de lo contrario puede mandar error</returns>
    private static bool GuardaValor(DS_Campos.EC_CAMPOSRow Campo, int Persona_ID, object Valor, int SesionID)
    {
        if (Campo.CAMPO_ES_TEMPLEADOS == 0)
        {
            if (!CeC.ExisteEnSeparador("PERSONA_ID, SUSCRIPCION_ID, PERSONA_LINK_ID, TIPO_PERSONA_ID, PERSONA_NOMBRE, TURNO_ID, CALENDARIO_DF_ID, PERSONA_EMAIL, DISENO_ID, AGRUPACION_NOMBRE, PERSONA_BORRADO",
                Campo.CAMPO_NOMBRE, ","))
                return true;
        }
        string ValorStr = "";
        //Se agregará el código para convertir del tipo de datos origen al tipo de datos destino
        if (Valor != null && Valor != DBNull.Value)
        {
            if (Campo.CAMPO_NOMBRE == "TURNO_ID")
            {

                ValorStr = Valor.ToString();// CeC_BD.ObtenTurnoID(Convert.ToString(Valor).Trim()).ToString();
            }
            else
            {
                switch ((Tipo_Datos)Campo.TIPO_DATO_ID)
                {
                    case Tipo_Datos.Texto:
                        ValorStr = "'" + Convert.ToString(Valor).Replace("'", "''") + "'";
                        break;
                    case Tipo_Datos.Fecha:
                        if (Valor != "")
                            ValorStr = CeC_BD.SqlFecha(Convert.ToDateTime(Valor));
                        else
                            ValorStr = "null";
                        break;
                    case Tipo_Datos.Fecha_y_Hora:
                        if (Valor != "")
                            ValorStr = CeC_BD.SqlFechaHora(Convert.ToDateTime(Valor));
                        else
                            ValorStr = "null";
                        break;
                    case Tipo_Datos.Hora:
                        ValorStr = CeC_BD.SqlFechaHora(CeC_BD.Hora2DateTime(Valor.ToString()));
                        break;
                    case Tipo_Datos.Boleano:
                        ValorStr = CeC.Convierte2Int(CeC.Convierte2Bool(Valor)).ToString();
                        break;
                    case Tipo_Datos.Entero:
                        try
                        {
                            ValorStr = Convert.ToInt32(Valor).ToString();
                        }
                        catch { ValorStr = "0"; }

                        break;
                    case Tipo_Datos.Decimal:
                        try
                        {
                            ValorStr = Convert.ToDecimal(Valor).ToString();
                        }
                        catch { ValorStr = "0"; }
                        break;
                }
            }
        }
        else
            ValorStr = "null";

        int Registros = 0;
        string Qry = "UPDATE EC_PERSONAS_DATOS SET " + Campo.CAMPO_NOMBRE + " = " + ValorStr + " WHERE PERSONA_ID = " + Persona_ID.ToString();
        if (Campo.CAMPO_ES_TEMPLEADOS != 0)
            Qry = "UPDATE EC_PERSONAS_DATOS SET " + Campo.CAMPO_NOMBRE + " = " + ValorStr + " WHERE PERSONA_ID = " + Persona_ID.ToString();
        else
            Qry = "UPDATE EC_PERSONAS SET " + Campo.CAMPO_NOMBRE + " = " + ValorStr + " WHERE PERSONA_ID = " + Persona_ID.ToString();

        Registros = CeC_BD.EjecutaComando(Qry);
        if (Registros < 1)
            return false;
        CeC_Sesion.SAgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.EDICION, "Empleados", Persona_ID, Campo.CAMPO_ETIQUETA + " = " + ValorStr, SesionID);

        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="CampoNombre"></param>
    /// <param name="CamposIDS">Campos separados por coma que el sistema nunca creará como combos</param>
    /// <returns></returns>
    public static object CreaCampo(string CampoNombre, string CamposIDS)
    {
        DS_Campos.EC_CAMPOSRow Campo = CeC_Campos.Obten_Campo(CampoNombre);
        object Objeto = null;
        if (Campo == null)
            return null;
        return null;
    }

    /// <summary>
    /// Crea un control para consulta y/o edición de un campo de la tabla
    /// de Empleados y Personas
    /// </summary>
    /// <param name="CampoNombre">Nombre del Campo</param>
    /// <param name="Persona_ID">Identificador de Persona, 
    /// si es -1 significa que es una persona nueva, si es -9998 
    /// significa que es un filtro</param>
    /// <returns>Nulo si no existe ese campo en las tablas o el control si es que existe</returns>
    public static object CreaCampo(string CampoNombre, int Persona_ID)
    {

        //        LCampo.Text = CampoNombre + "|" + Persona_ID.ToString();
        DS_Campos.EC_CAMPOSRow Campo = CeC_Campos.Obten_Campo(CampoNombre);
        //        DS_Campos.EC_CAMPOS_TERow CampoTE = CeC_Campos.Obten_CampoTE(CampoNombre);
        object Objeto = null;
        if (Campo == null)
            return null;
        return null;
    }
    /// <summary>
    /// Obtiene el valor convertido a una sentencia SQL, ej si es 
    /// fecha se convierte al formato indicado en SQL
    /// </summary>
    /// <param name="Valor">Objeto valor a convertir</param>
    /// <returns>Sentencia SQL</returns>
    public static string ObtenSqlValor(Object Valor)
    {
        if (Valor == null)
            return "''";
        switch (Valor.GetType().ToString())
        {
            case "System.DateTime":
                return CeC_BD.SqlFecha(Convert.ToDateTime(Valor));
                break;
            case "System.Boolean":
                return Convert.ToInt32(Valor).ToString();
                break;

        }
        return "'" + Valor.ToString() + "'";
    }

    public static string ObtenValorCampo(int PersonaID, string Campo_Nombre)
    {
        /*if (Campo_Nombre.ToUpper() == "TURNO_ID")
        {
            return CeC_BD.EjecutaEscalarString("SELECT TURNO_NOMBRE FROM EC_TURNOS WHERE TURNO_ID IN (SELECT TURNO_ID FROM EC_PERSONAS WHERE PERSONA_ID= " + PersonaID + ")");
        }*/
        try
        {
            string Valor = "";
            int PosPunto = Campo_Nombre.IndexOf(".");
            Campo_Nombre = Campo_Nombre.Substring(PosPunto + 1);
            return CeC_BD.EjecutaEscalarString("SELECT PERSONA_DATO FROM EC_PERSONAS_DATO WHERE PERSONA_ID = " + PersonaID + " AND CAMPO_NOMBRE = '" + Campo_Nombre + "'");

            if (Obten_CampoTE(Campo_Nombre) != null)
                return CeC_BD.EjecutaEscalarString("SELECT " + Campo_Nombre + " FROM EC_PERSONAS_DATOS WHERE  PERSONA_ID= " + PersonaID);
            return CeC_BD.EjecutaEscalarString("SELECT " + Campo_Nombre + " FROM EC_PERSONAS WHERE  PERSONA_ID= " + PersonaID);
        }
        catch { }
        return "";
    }

    public static string GuardaCampoEstatico(string Nombre, object ValorCampo, int Persona_ID, int SesionID)
    {
        DS_Campos.EC_CAMPOSRow Campo = CeC_Campos.Obten_Campo(Nombre);
        if (Campo == null)
            return "No existe el campo " + Nombre;

        object Valor = ValorCampo;
        //        if (Valor == null) return "El Campo " + Campo.CAMPO_ETIQUETA + " no se ha guardado correctamente, los demas campos se han agregado correctamente";
        if (Valor != null)
            if (Valor.ToString() == "NO DEFINIDO")
                return "El tipo " + ValorCampo + " no se ha definido para obtener el valor para " + Campo.CAMPO_ETIQUETA;
        bool Res = false;
        if (Campo.CAMPO_NOMBRE == CeC_Campos.m_CampoTE_Llave)
        {
            if (Valor == null) return "El Campo " + Campo.CAMPO_ETIQUETA + " no se ha guardado correctamente, los demas campos se han agregado correctamente";
            /* if (Convert.ToInt32(Valor) < 1)
                 return "El" + Campo.CAMPO_ETIQUETA + " no puede ser menor a 1";*/
            object ValorAnt = ObtenValor(Campo, Persona_ID);
            if (ValorAnt == null || Convert.ToInt32(ValorAnt) <= 0)
            {
                if (Convert.ToInt32(Valor) <= 0)
                {
                    Valor = CeC_Autonumerico.GeneraAutonumerico("EC_PERSONAS_DATOS", CeC_Campos.m_CampoTE_Llave);

                }
                else
                {
                    int PersonaIDDuplicado = CeC_Personas.ObtenPersonaID(Convert.ToInt32(Valor));

                    if (PersonaIDDuplicado > 0)
                    {
                        return "No se puede dar de alta un registro con el campo " + Campo.CAMPO_ETIQUETA + " igual a " +
                        Valor.ToString() + " debido a que este empleado ya existe (" + CeC_BD.ObtenPersonaNombre(PersonaIDDuplicado) + ")";

                    }
                }
                CeC_BD.EjecutaComando("INSERT INTO EC_PERSONAS (PERSONA_ID, PERSONA_LINK_ID,PERSONA_NOMBRE) VALUES(" + Persona_ID.ToString() + "," + Valor.ToString() + ",'Ninguna persona')");

                CeC_BD.EjecutaComando("INSERT INTO EC_PERSONAS_DATOS (PERSONA_ID," + Campo.CAMPO_NOMBRE + ") VALUES (" + Persona_ID.ToString() + ",'" + Valor.ToString() + "')");
                Res = true;
            }
            else
            {

                if (Convert.ToInt32(Valor) != Convert.ToInt32(ValorAnt))
                {
                    int PersonaIDDuplicado = CeC_Personas.ObtenPersonaID(Convert.ToInt32(Valor));

                    if (PersonaIDDuplicado > 0)
                    {
                        if (ValorAnt == null)
                            return "No se puede cambiar el campo " + Campo.CAMPO_ETIQUETA + " a " +
                                Valor.ToString() + " debido a que este empleado ya existe (" + CeC_BD.ObtenPersonaNombre(PersonaIDDuplicado) + ")";

                        else
                            return "No se puede cambiar el campo " + Campo.CAMPO_ETIQUETA + " de " + ValorAnt.ToString() + " a " +
                            Valor.ToString() + " debido a que este empleado ya existe (" + CeC_BD.ObtenPersonaNombre(PersonaIDDuplicado) + ")";

                    }
                }

                if (GuardaValor(Campo, Persona_ID, Valor, SesionID))
                {
                    CeC_BD.EjecutaComando(" UPDATE EC_PERSONAS SET PERSONA_LINK_ID = " + Valor.ToString() + " WHERE PERSONA_ID = " + Persona_ID);
                    Res = true;
                }
                else
                    Res = false;
            }


        }
        else
            Res = GuardaValor(Campo, Persona_ID, Valor, SesionID);

        if (Res == false)
            return "No se pudo guardar el registro " + Campo.CAMPO_ETIQUETA;
        return "";
    }
    #endregion

    /// <summary>
    /// Obtiene un data set a partir de un catalogo
    /// </summary>
    /// <param name="CatalogoID">ID del catalogo</param>
    /// <param name="Sesion">Contiene la sesion actual, para validar permisos en los filtros</param>
    /// <returns></returns>
    public static DataSet CatalogoDT(int CatalogoID, CeC_Sesion Sesion)
    {

        return CatalogoDT(ds_Campos.EC_CATALOGOS.FindByCATALOGO_ID(CatalogoID), Sesion, false);
    }

    /// <summary>
    /// Obtiene un data set a partir de un catalogo
    /// </summary>
    /// <param name="Catalogo">Fila de la tabla de catalogos</param>
    /// <param name="Sesion">Contiene la sesion actual, para validar permisos en los filtros</param>
    /// <returns></returns>
    public static DataSet CatalogoDT(DS_Campos.EC_CATALOGOSRow Catalogo, CeC_Sesion Sesion)
    {
        return CatalogoDT(Catalogo, Sesion, false);
    }

    /// <summary>
    /// Obtiene un data set a partir de un catalogo
    /// </summary>
    /// <param name="Catalogo">Fila de la tabla de catalogos</param>
    /// <param name="Sesion">Contiene la sesion actual, para validar permisos en los filtros</param>
    /// <returns></returns>
    public static DataSet CatalogoDT(DS_Campos.EC_CATALOGOSRow Catalogo, CeC_Sesion Sesion, bool SinFiltro)
    {

        if (Catalogo == null)
            return null;

        string Qry = "";
        if (Catalogo.CATALOGO_ID == 1)
            return null;
        string QryAdd = "";
        if (Sesion != null)
            if (Catalogo.CATALOGO_TABLA.IndexOf("EC_GRUPOS_") > 0)
            {
                string Numero = Catalogo.CATALOGO_TABLA.Substring(11, 1);
                QryAdd = " AND " + Catalogo.CATALOGO_TABLA + ".GRUPO_" + Numero + "_ID IN (SELECT GRUPO_" + Numero + "_ID FROM EC_USUARIOS_GRUPOS_" + Numero + " WHERE USUARIO_ID = " + Sesion.USUARIO_ID + ") ";

            }
        if (Catalogo.CATALOGO_C_LLAVE != Catalogo.CATALOGO_C_DESC)
            Qry = "SELECT " + Catalogo.CATALOGO_C_LLAVE + ", " + Catalogo.CATALOGO_C_DESC;
        else
            Qry = "SELECT " + Catalogo.CATALOGO_C_LLAVE;

        if (!Catalogo.IsCATALOGO_QA_SQLNull() && Catalogo.CATALOGO_QA_SQL.Length > 0 && !SinFiltro)
            Qry += " FROM " + Catalogo.CATALOGO_TABLA + QryAdd + " " + Catalogo.CATALOGO_QA_SQL + " \n ORDER BY " + Catalogo.CATALOGO_C_DESC;
        else
            Qry += " FROM " + Catalogo.CATALOGO_TABLA + QryAdd + " \n ORDER BY " + Catalogo.CATALOGO_C_DESC;


        object obj = CeC_BD.EjecutaDataSet(Qry);
        if (obj == null)
            return null;
        DataSet DTS = (DataSet)obj;
        AplicaFormatoDataset(DTS);
        return DTS;
    }

    /// <summary>
    /// Obtiene un data set a partir de un AutoCatalogo
    /// </summary>
    /// <param name="EC_EmpleadoCampo">Campo de la tabla de empleados que es autocatalogo</param>
    /// <param name="Sesion">Contiene la sesion actual, para validar permisos en los filtros</param>
    /// <returns></returns>
    public static DataSet CatalogoDT(string EC_EmpleadoCampo, CeC_Sesion Sesion)
    {

        if (EC_EmpleadoCampo.Length <= 0)
            return null;
        int Punto = EC_EmpleadoCampo.IndexOf('.');
        if (Punto > 0)
            EC_EmpleadoCampo = EC_EmpleadoCampo.Substring(Punto + 1);



        string Qry = "";
        string QryAdd = "";
        if (Sesion != null)
        {
            string Qry0 = "";
            if (CeC_BD.EsOracle)
                Qry0 = " EC_PERSONAS.PERSONA_ID IN(SELECT PERSONA_ID FROM EC_PERSONAS,EC_USUARIOS_PERMISOS WHERE EC_USUARIOS_PERMISOS.SUSCRIPCION_ID=EC_PERSONAS.SUSCRIPCION_ID " +
                    " AND AGRUPACION_NOMBRE LIKE USUARIO_PERMISO || '%' AND USUARIO_ID = " + Sesion.USUARIO_ID + " AND EC_PERSONAS.SUSCRIPCION_ID = " + Sesion.SUSCRIPCION_ID + " )";
            else
                Qry0 = " EC_PERSONAS.PERSONA_ID IN(SELECT PERSONA_ID FROM EC_PERSONAS,EC_USUARIOS_PERMISOS WHERE EC_USUARIOS_PERMISOS.SUSCRIPCION_ID=EC_PERSONAS.SUSCRIPCION_ID " +
                " AND AGRUPACION_NOMBRE LIKE USUARIO_PERMISO + '%' AND USUARIO_ID = " + Sesion.USUARIO_ID + " AND EC_PERSONAS.SUSCRIPCION_ID = " + Sesion.SUSCRIPCION_ID + " )";
            QryAdd = " AND " + Qry0;

        }

        Qry = "SELECT EC_PERSONAS_DATOS." + EC_EmpleadoCampo + " FROM EC_PERSONAS_DATOS, EC_PERSONAS WHERE EC_PERSONAS.PERSONA_ID = EC_PERSONAS_DATOS.PERSONA_ID " + QryAdd + " GROUP BY EC_PERSONAS_DATOS." + EC_EmpleadoCampo + " \n ORDER BY EC_PERSONAS_DATOS." + EC_EmpleadoCampo;


        object obj = CeC_BD.EjecutaDataSet(Qry);
        DataSet DTS = (DataSet)obj;
        AplicaFormatoDataset(DTS);
        return DTS;

    }
    /// <summary>
    /// Aplica un type al tipo de dato
    /// </summary>
    /// <param name="Columna"></param>
    /// <returns></returns>
    public static bool Aplica_Type(DataColumn Columna)
    {
        try
        {
            DS_Campos.EC_CAMPOSRow Campo = ds_Campos.EC_CAMPOS.FindByCAMPO_NOMBRE(Columna.ColumnName);
            if (Campo != null)
            {
                DS_Campos.EC_TIPO_DATOSRow Tipo_Dato = ds_Campos.EC_TIPO_DATOS.FindByTIPO_DATO_ID(Campo.TIPO_DATO_ID);
                if (Tipo_Dato != null)
                {
                    Type Tipo = Type.GetType(Tipo_Dato.TIPO_DATO_DS);

                    //                    if (Tipo != null)
                    //                        Columna.DataType = Tipo;                    
                }
            }
        }
        catch (Exception)
        {
        }
        return false;
    }
    /// <summary>
    /// Aplica formato a un Dataset
    /// </summary>
    /// <param name="DS"></param>
    /// <returns></returns>
    public static bool AplicaFormatoDataset(DataSet DS)
    {

        try
        {
            if (DS == null)
                return false;
            for (int Cont = 0; Cont < DS.Tables.Count; Cont++)
            {
                AplicaFormatoDataset(DS, DS.Tables[Cont].TableName);
            }
            return true;
        }
        catch (Exception)
        {

            throw;
        }
        return false;
    }
    /// <summary>
    /// Aplica formato a un Dataset
    /// </summary>
    /// <param name="DS"></param>
    /// <param name="Tabla"></param>
    /// <returns></returns>
    public static bool AplicaFormatoDataset(DataSet DS, string Tabla)
    {

        try
        {
            if (DS == null)
                return false;

            foreach (DataColumn Columna in DS.Tables[Tabla].Columns)
            {
                Aplica_Type(Columna);
            }


            return true;
        }
        catch (Exception)
        {

            throw;
        }
        return false;
    }
    /// <summary>
    /// Verifica que exista una mascara valida
    /// </summary>
    /// <param name="Tipo_Dato_ID"></param>
    /// <param name="Mascara_ID"></param>
    /// <returns></returns>
    public static bool EsMascaraValidaTD(decimal Tipo_Dato_ID, decimal Mascara_ID)
    {
        DS_CamposTableAdapters.TA_EC_TIPO_DATOS_MASCARAS TA = new DS_CamposTableAdapters.TA_EC_TIPO_DATOS_MASCARAS();
        if (Convert.ToInt32(TA.ExisteMascaraTDato(Tipo_Dato_ID, Mascara_ID)) < 1)
            return false;
        return true;

    }
    /// <summary>
    /// Guarda Etiqueta y establece si Es Invisible e invisible en el Grid,y si es autocatalogo  Es Usada en WF_Wizardb
    /// </summary>
    /// <param name="NombreCampo">Nombre del Campo a Modificar</param> 
    /// <param name="Etiqueta">Nombre de la Nueva Etiqueta</param> 
    /// <param name="Invisible">Invisible</param> 
    /// <param name="InbisibleG">Invisible en el Grid</param> 
    /// <returns>Bool true si realizo cambios false si no se realizaron</returns> 
    public bool GuardaConfigCamposW(string NombreCampo, string Etiqueta, bool Invisible, bool InvisibleG, bool Autocatalogo)
    {
        EditaCampo(NombreCampo, Etiqueta, Autocatalogo);
        DS_Campos.EC_CAMPOS_TERow Campo;
        DS_CamposTableAdapters.TA_EC_CAMPOS_TE TACampos = new DS_CamposTableAdapters.TA_EC_CAMPOS_TE();
        Campo = Obten_CampoTE(NombreCampo);
        if (Campo != null)
        {
            Campo.CAMPO_TE_INVISIBLE = Convert.ToDecimal(Invisible);
            Campo.CAMPO_TE_INVISIBLEG = Convert.ToDecimal(InvisibleG);
            TACampos.Update(Campo);

            return true;
        }
        return false;
    }


    

}
