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

/// <summary>
/// Descripción breve de CeC_Sitios
/// </summary>
public class CeC_Sitios : CeC_Tabla
{
    int m_Sitio_Id = 0;
    [Description("Identificador unico del registro de sitios")]
    [DisplayNameAttribute("Sitio_Id")]
    public int Sitio_Id { get { return m_Sitio_Id; } set { m_Sitio_Id = value; } }
    string m_Sitio_Nombre = "";
    [Description("Nombre del Sitio")]
    [DisplayNameAttribute("Sitio_Nombre")]
    public string Sitio_Nombre { get { return m_Sitio_Nombre; } set { m_Sitio_Nombre = value; } }
    string m_Sitio_Consulta = "";
    [Description("Sentencia SQL que indicara a los empleados que automáticamente se asignaran a este campo")]
    [DisplayNameAttribute("Sitio_Consulta")]
    public string Sitio_Consulta { get { return m_Sitio_Consulta; } set { m_Sitio_Consulta = value; } }
    DateTime m_Sitio_Hdesde_Svec = CeC_BD.FechaNula;
    [Description("Indica desde que hora se podra sincronizar el almacen de vectores")]
    [DisplayNameAttribute("Sitio_Hdesde_Svec")]
    public DateTime Sitio_Hdesde_Svec { get { return m_Sitio_Hdesde_Svec; } set { m_Sitio_Hdesde_Svec = value; } }
    DateTime m_Sitio_Hhasta_Svec = CeC_BD.FechaNula;
    [Description("Indica Hasta que hora se podra sincronizar el almacen de vectores si esta hora es menor que la anterior significa que termina al dia siguiete")]
    [DisplayNameAttribute("Sitio_Hhasta_Svec")]
    public DateTime Sitio_Hhasta_Svec { get { return m_Sitio_Hhasta_Svec; } set { m_Sitio_Hhasta_Svec = value; } }
    int m_Sitio_Segundos_Sync = 0;
    [Description("Indica cuantos segundos esperará entre cada sincronización")]
    [DisplayNameAttribute("Sitio_Segundos_Sync")]
    public int Sitio_Segundos_Sync { get { return m_Sitio_Segundos_Sync; } set { m_Sitio_Segundos_Sync = value; } }
    bool m_Sitio_Borrado = false;
    [Description("Indica si el sitio se encuentra activo para ser seleccionado")]
    [DisplayNameAttribute("Sitio_Borrado")]
    public bool Sitio_Borrado { get { return m_Sitio_Borrado; } set { m_Sitio_Borrado = value; } }
    string m_Sitio_Responsables = "";
    [Description("Nombre del responsable del sitio")]
    [DisplayNameAttribute("Sitio_Responsables")]
    public string Sitio_Responsables { get { return m_Sitio_Responsables; } set { m_Sitio_Responsables = value; } }
    string m_Sitio_Telefonos = "";
    [Description("Telefonos de los responsable o del contacto")]
    [DisplayNameAttribute("Sitio_Telefonos")]
    public string Sitio_Telefonos { get { return m_Sitio_Telefonos; } set { m_Sitio_Telefonos = value; } }
    string m_Sitio_Direccion_1 = "";
    [Description("Campo de direccion")]
    [DisplayNameAttribute("Sitio_Direccion_1")]
    public string Sitio_Direccion_1 { get { return m_Sitio_Direccion_1; } set { m_Sitio_Direccion_1 = value; } }
    string m_Sitio_Direccion_2 = "";
    [Description("Campo de direccion")]
    [DisplayNameAttribute("Sitio_Direccion_2")]
    public string Sitio_Direccion_2 { get { return m_Sitio_Direccion_2; } set { m_Sitio_Direccion_2 = value; } }
    string m_Sitio_Referencias = "";
    [Description("Refrencias sobre como llegar al sitio")]
    [DisplayNameAttribute("Sitio_Referencias")]
    public string Sitio_Referencias { get { return m_Sitio_Referencias; } set { m_Sitio_Referencias = value; } }
    string m_Sitio_Comentarios = "";
    [Description("Comentarios adicionales sobre el sitio")]
    [DisplayNameAttribute("Sitio_Comentarios")]
    public string Sitio_Comentarios { get { return m_Sitio_Comentarios; } set { m_Sitio_Comentarios = value; } }

    public CeC_Sitios(CeC_Sesion Sesion)
        : base("EC_SITIOS", "SITIO_ID", Sesion)
    {

    }
    public CeC_Sitios(int SitioID, CeC_Sesion Sesion)
        : base("EC_SITIOS", "SITIO_ID", Sesion)
    {
        Carga(SitioID.ToString(), Sesion);
    }

    /// <summary>
    /// Actualiza los datos de los Sitios. Si no existe crea uno nuevo.
    /// </summary>
    /// <param name="SitioId">Identificador unico del registro de sitios</param>
    /// <param name="SitioNombre">Nombre del Sitio</param>
    /// <param name="SitioConsulta">Sentencia SQL que indicara a los empleados que automáticamente se asignaran a este campo</param>
    /// <param name="SitioHdesdeSvec">Indica desde que hora se podra sincronizar el almacen de vectores</param>
    /// <param name="SitioHhastaSvec">Indica Hasta que hora se podra sincronizar el almacen de vectores si esta hora es menor que la anterior significa que termina al dia siguiete</param>
    /// <param name="SitioSegundosSync">Indica cuantos segundos esperará entre cada sincronización</param>
    /// <param name="SitioBorrado">Indica si el sitio se encuentra activo para ser seleccionado</param>
    /// <param name="SitioResponsables">Nombre del responsable del sitio</param>
    /// <param name="SitioTelefonos">Telefonos de los responsable o del contacto</param>
    /// <param name="SitioDireccion1">Campo de direccion</param>
    /// <param name="SitioDireccion2">Campo de direccion</param>
    /// <param name="SitioReferencias">Refrencias sobre como llegar al sitio</param>
    /// <param name="SitioComentarios">Comentarios adicionales sobre el sitio</param>
    /// <param name="Sesion">Variable de Sesión</param>
    /// <returns>Vedadero si se pudo guardar correctamente los datos. Falso en caso de que ocurra algún problema o error al guardar los datos</returns>
    public bool Actualiza(int SitioId, string SitioNombre, string SitioConsulta, DateTime SitioHdesdeSvec, DateTime SitioHhastaSvec, int SitioSegundosSync, bool SitioBorrado, string SitioResponsables, string SitioTelefonos, string SitioDireccion1, string SitioDireccion2, string SitioReferencias, string SitioComentarios,
    CeC_Sesion Sesion)
    {
        try
        {
            bool Nuevo = false;
            if (!Carga(SitioId.ToString(), Sesion))
                Nuevo = true;
            m_EsNuevo = Nuevo;
            Sitio_Id = SitioId; 
            Sitio_Nombre = SitioNombre; 
            Sitio_Consulta = SitioConsulta; 
            Sitio_Hdesde_Svec = SitioHdesdeSvec; 
            Sitio_Hhasta_Svec = SitioHhastaSvec; 
            Sitio_Segundos_Sync = SitioSegundosSync; 
            Sitio_Borrado = SitioBorrado; 
            Sitio_Responsables = SitioResponsables; 
            Sitio_Telefonos = SitioTelefonos; 
            Sitio_Direccion_1 = SitioDireccion1; 
            Sitio_Direccion_2 = SitioDireccion2; 
            Sitio_Referencias = SitioReferencias; 
            Sitio_Comentarios = SitioComentarios;
            if (Guarda(Sesion))
            {
                return true;
            }
        }
        catch { }
        return false;
    }

    public static int ObtenSitioID(int Usuario_ID)
    {
        int R = CeC_BD.EjecutaEscalarInt(
            "SELECT min(EC_SITIOS.SITIO_ID) as SITIO_ID FROM EC_SITIOS WHERE SITIO_BORRADO = 0  " +
            "AND EC_SITIOS." + CeC_Autonumerico.ValidaUsuarioID("EC_SITIOS", "SITIO_ID", Usuario_ID));
        return R;

    }
    public static int ObtenSitioPredeterminado(int Usuario_ID, int SesionID, int SuscripcionID)
    {
        int R = ObtenSitioID(Usuario_ID);
        if (R > 0)
            return R;
        return InsertaPredeterminado(SesionID, Usuario_ID, SuscripcionID);
    }
    public static DataSet ObtenSitiosDSMenuSuscripcion(int SuscripcionID)
    {
        string ADD = "";
       
        if (SuscripcionID > 1)
            ADD = " AND EC_SITIOS." + CeC_Autonumerico.ValidaSuscripcion("EC_SITIOS", "SITIO_ID", SuscripcionID);
        return (DataSet)CeC_BD.EjecutaDataSet(
            "SELECT EC_SITIOS.SITIO_ID, EC_SITIOS.SITIO_NOMBRE FROM EC_SITIOS WHERE SITIO_BORRADO = 0 " + ADD);

    }
    public static DataSet ObtenSitiosDSMenu(int Usuario_ID)
    {
        return (DataSet)CeC_BD.EjecutaDataSet(
            "SELECT EC_SITIOS.SITIO_ID, EC_SITIOS.SITIO_NOMBRE FROM EC_SITIOS WHERE SITIO_BORRADO = 0 "+
            "AND EC_SITIOS." + CeC_Autonumerico.ValidaUsuarioID("EC_SITIOS", "SITIO_ID", Usuario_ID));

    }

    /// <summary>
    /// Asigna un catálogo a un combo, esta función se deberá llamar en la carga
    /// de la pagina o en la inicialización de datos del combo
    /// </summary>
    /// <param name="Combo">Combo que se ligara con el conjunto de datos</param>
    /// <param name="CATALOGO_C_LLAVE">Campo al que hace referencia este combo, se tomara
    /// dicho campo para buscarlo en el catálogo y ver cual le corresponde</param>
    /// <param name="Sesion">Sesion que contiene todos las variables requeridas para ciertos filtros, puede ser nulo</param>
    /// <returns>Verdadero si se realizo la operación satisfactoriamente</returns>
    public static bool AsignaCatalogo(Infragistics.WebUI.WebCombo.WebCombo Combo, int SuscripcionID)
    {
        DataSet DS;
        DS = ObtenSitiosDSMenuSuscripcion(SuscripcionID);
        if (DS == null)
            return false;
        CeC_Campos.AplicaFormatoDataset(DS, DS.Tables[0].TableName);
        Combo.DataSource = DS;
        //   Combo.DataMember = DS.Tables[0].TableName;

        Combo.DataTextField = DS.Tables[0].Columns[1].ColumnName;
        //Combo.DisplayValue = DS.Tables[0].Columns[0].ColumnName;
        Combo.DataValueField = DS.Tables[0].Columns[0].ColumnName;
        Combo.DataBind();
        return true;
    }

    /// <summary>
    /// Obtiene el numero de terminales
    /// </summary>
    /// <param name="Usuario_ID"></param>
    /// <returns></returns>
    public static int ObtenTerminalesNo(int Usuario_ID)
    {
        //string ADD = " AND EC_TERMINALES.TERMINAL_ID IN (";
        //ADD += " SELECT     EC_AUTONUM.AUTONUM_TABLA_ID AS TERMINAL_ID " +
        //        "FROM         EC_AUTONUM INNER JOIN " +
        //        " EC_PERMISOS_SUSCRIP ON EC_AUTONUM.SUSCRIPCION_ID = EC_PERMISOS_SUSCRIP.SUSCRIPCION_ID" +
        //        " WHERE     (EC_AUTONUM.AUTONUM_TABLA = 'EC_TERMINALES') AND (EC_PERMISOS_SUSCRIP.USUARIO_ID = " + Usuario_ID + "))";

        return CeC_BD.EjecutaEscalarInt(
            "SELECT count(EC_TERMINALES.TERMINAL_ID) " +
            "FROM EC_TERMINALES WHERE TERMINAL_BORRADO = 0 " +
            "AND EC_TERMINALES." + CeC_Autonumerico.ValidaUsuarioID("EC_TERMINALES", "TERMINAL_ID", Usuario_ID));

    }
    public static DataSet ObtenTerminalesDSMenu(int Usuario_ID)
    {
        //string ADD = " AND EC_TERMINALES.TERMINAL_ID IN (";
        //ADD += " SELECT     EC_AUTONUM.AUTONUM_TABLA_ID AS TERMINAL_ID " +
        //        "FROM         EC_AUTONUM INNER JOIN " +
        //        " EC_PERMISOS_SUSCRIP ON EC_AUTONUM.SUSCRIPCION_ID = EC_PERMISOS_SUSCRIP.SUSCRIPCION_ID" +
        //        " WHERE     (EC_AUTONUM.AUTONUM_TABLA = 'EC_TERMINALES') AND (EC_PERMISOS_SUSCRIP.USUARIO_ID = " + Usuario_ID + "))";

        return (DataSet)CeC_BD.EjecutaDataSet(
            "SELECT EC_TERMINALES.TERMINAL_ID, EC_TERMINALES.TERMINAL_NOMBRE " +
            "FROM EC_TERMINALES WHERE TERMINAL_BORRADO = 0 " +
            "AND EC_TERMINALES." + CeC_Autonumerico.ValidaUsuarioID("EC_TERMINALES", "TERMINAL_ID", Usuario_ID));
    }

    public static DataSet ObtenTerminalesDS(int Usuario_ID)
    {
        //string ADD = " AND EC_TERMINALES.TERMINAL_ID IN (";
        //ADD += " SELECT     EC_AUTONUM.AUTONUM_TABLA_ID AS TERMINAL_ID " +
        //        "FROM         EC_AUTONUM INNER JOIN " +
        //        " EC_PERMISOS_SUSCRIP ON EC_AUTONUM.SUSCRIPCION_ID = EC_PERMISOS_SUSCRIP.SUSCRIPCION_ID" +
        //        " WHERE     (EC_AUTONUM.AUTONUM_TABLA = 'EC_TERMINALES') AND (EC_PERMISOS_SUSCRIP.USUARIO_ID = " + Usuario_ID + "))";

        return (DataSet)CeC_BD.EjecutaDataSet(
            "SELECT EC_TERMINALES.TERMINAL_ID, TIPO_TERMINAL_ACCESO_ID, TERMINAL_NOMBRE, ALMACEN_VEC_ID, SITIO_ID, " +
            "MODELO_TERMINAL_ID, TIPO_TECNOLOGIA_ID, TIPO_TECNOLOGIA_ADD_ID, TERMINAL_SINCRONIZACION, " +
            "TERMINAL_CAMPO_LLAVE, TERMINAL_CAMPO_ADICIONAL, TERMINAL_ENROLA, TERMINAL_DIR, TERMINAL_ASISTENCIA, " +
            "TERMINAL_COMIDA, TERMINAL_MINUTOS_DIF, TERMINAL_VALIDAHORARIOE, TERMINAL_VALIDAHORARIOS, TERMINAL_BORRADO " +
            "FROM EC_TERMINALES WHERE TERMINAL_BORRADO = 0 " +
            "AND EC_TERMINALES." + CeC_Autonumerico.ValidaUsuarioID("EC_TERMINALES", "TERMINAL_ID", Usuario_ID));
    }

    public static DataSet ObtenTerminalesDSMenuporSitio(int SitioID, int UsuarioID)
    {
        if (SitioID < 0)
            return ObtenTerminalesDSMenu(UsuarioID);
        else
        {
            //string ADD = " AND EC_TERMINALES.TERMINAL_ID IN (";
            //ADD += " SELECT     EC_AUTONUM.AUTONUM_TABLA_ID AS TERMINAL_ID " +
            //        "FROM         EC_AUTONUM INNER JOIN " +
            //        " EC_PERMISOS_SUSCRIP ON EC_AUTONUM.SUSCRIPCION_ID = EC_PERMISOS_SUSCRIP.SUSCRIPCION_ID" +
            //        " WHERE     (EC_AUTONUM.AUTONUM_TABLA = 'EC_TERMINALES') AND (EC_PERMISOS_SUSCRIP.USUARIO_ID = " + UsuarioID
            //        + ")) AND EC_TERMINALES.SITIO_ID =" + SitioID;

            return (DataSet)CeC_BD.EjecutaDataSet(
                "SELECT EC_TERMINALES.TERMINAL_ID, EC_TERMINALES.TERMINAL_NOMBRE FROM EC_TERMINALES WHERE TERMINAL_BORRADO = 0 " + 
                " AND EC_TERMINALES." + CeC_Autonumerico.ValidaUsuarioID("EC_TERMINALES", "TERMINAL_ID", UsuarioID));
        }

    }
    public static int TerminalesInserta(int Sesion_ID, int Usuario_ID, string TERMINAL_NOMBRE,
        int TIPO_TERMINAL_ACCESO_ID,
        int ALMACEN_VEC_ID, int SITIO_ID, int MODELO_TERMINAL_ID, int TIPO_TECNOLOGIA_ID, int TIPO_TECNOLOGIA_ADD_ID,
        int TERMINAL_SINCRONIZACION, string TERMINAL_CAMPO_LLAVE, string TERMINAL_CAMPO_ADICIONAL,
        bool TERMINAL_ENROLA, string TERMINAL_DIR, bool TERMINAL_ASISTENCIA, bool TERMINAL_COMIDA,
        int TERMINAL_MINUTOS_DIF, bool TERMINAL_VALIDAHORARIOE, bool TERMINAL_VALIDAHORARIOS, bool TERMINAL_BORRADO)
    {
        int SuscripcionID = CeC_BD.EjecutaEscalarInt("SELECT SUSCRIPCION_ID FROM EC_USUARIOS WHERE USUARIO_ID = " + Usuario_ID);
        if (SuscripcionID < 0)
            return -9997;
        int Terminal_ID = CeC_Autonumerico.GeneraAutonumerico("EC_TERMINALES", "TERMINAL_ID", Sesion_ID, SuscripcionID);
        if (Terminal_ID < 0)
            return -9996;
        int iTerminal_Enrola = 0;
        if (TERMINAL_ENROLA)
            iTerminal_Enrola = 1;
        int iTERMINAL_ASISTENCIA = 0;
        if (TERMINAL_ASISTENCIA)
            iTERMINAL_ASISTENCIA = 1;
        int iTERMINAL_COMIDA = 0;
        if (TERMINAL_COMIDA)
            iTERMINAL_COMIDA = 1;
        int iTERMINAL_VALIDAHORARIOE = 0;
        if (TERMINAL_VALIDAHORARIOE)
            iTERMINAL_VALIDAHORARIOE = 1;
        int iTERMINAL_VALIDAHORARIOS = 0;
        if (TERMINAL_VALIDAHORARIOS)
            iTERMINAL_VALIDAHORARIOS = 1;
        int iTERMINAL_BORRADO = 0;
        if (TERMINAL_BORRADO)
            iTERMINAL_BORRADO = 1;
        if (CeC_BD.EjecutaComando("INSERT INTO EC_TERMINALES ( " +
               "TERMINAL_ID, TERMINAL_NOMBRE, TIPO_TERMINAL_ACCESO_ID, " +
               "ALMACEN_VEC_ID, SITIO_ID, MODELO_TERMINAL_ID, TIPO_TECNOLOGIA_ID, TIPO_TECNOLOGIA_ADD_ID, " +
               "TERMINAL_SINCRONIZACION, TERMINAL_CAMPO_LLAVE, " +
               "TERMINAL_CAMPO_ADICIONAL, TERMINAL_ENROLA, TERMINAL_DIR, TERMINAL_ASISTENCIA, TERMINAL_COMIDA, " +
               "TERMINAL_MINUTOS_DIF, TERMINAL_VALIDAHORARIOE, TERMINAL_VALIDAHORARIOS, TERMINAL_BORRADO) VALUES(" +
               Terminal_ID + ", '" + CeC_BD.ObtenParametroCadena(TERMINAL_NOMBRE) + "', " + TIPO_TERMINAL_ACCESO_ID + "," +
               ALMACEN_VEC_ID + "," + SITIO_ID + "," + MODELO_TERMINAL_ID + "," + TIPO_TECNOLOGIA_ID + "," + TIPO_TECNOLOGIA_ADD_ID + "," +
               TERMINAL_SINCRONIZACION + ",'" + TERMINAL_CAMPO_LLAVE + "', '" + TERMINAL_CAMPO_ADICIONAL + "'," +
               iTerminal_Enrola + ",'" + TERMINAL_DIR + "'," + iTERMINAL_ASISTENCIA + "," + iTERMINAL_COMIDA + "," +
               TERMINAL_MINUTOS_DIF + "," + iTERMINAL_VALIDAHORARIOE + "," + iTERMINAL_VALIDAHORARIOS + "," + iTERMINAL_BORRADO + ")") > 0)
            return Terminal_ID;
        return -1;

    }
    public static int TerminalesInserta(int Sesion_ID, int Usuario_ID, string TERMINAL_NOMBRE,
       int SITIO_ID, int MODELO_TERMINAL_ID, int TIPO_TECNOLOGIA_ID, int TIPO_TECNOLOGIA_ADD_ID, string TERMINAL_CAMPO_LLAVE,
          string TERMINAL_DIR, int TERMINAL_MINUTOS_DIF)
    {

        return TerminalesInserta(Sesion_ID, Usuario_ID, TERMINAL_NOMBRE, 0, 1, SITIO_ID, MODELO_TERMINAL_ID, TIPO_TECNOLOGIA_ID, TIPO_TECNOLOGIA_ADD_ID, 0, "EC_PERSONAS_DATOS.PERSONA_LINK_ID", "", true, TERMINAL_DIR, true, false, TERMINAL_MINUTOS_DIF, false, false, false);

    }
    public static string ObtenConsultaPredeterminada(int Usuario_ID, int SuscripcionID)
    {

        if (SuscripcionID <= 0)
            SuscripcionID = CeC_BD.EjecutaEscalarInt("SELECT SUSCRIPCION_ID FROM EC_USUARIOS WHERE USUARIO_ID = " + Usuario_ID);
        if (SuscripcionID < 0)
            return "";
        return "Select PERSONA_ID from EC_PERSONAS where SUSCRIPCION_ID =" + SuscripcionID;
    }

    /// <summary>
    /// Inserta un sitio predeterminado, si no se cuenta con el parametro suscripcion coloque este en 0 o menor
    /// </summary>
    /// <param name="Sesion_ID"></param>
    /// <param name="Usuario_ID"></param>
    /// <param name="SuscripcionID"></param>
    /// <returns></returns>
    public static int InsertaPredeterminado(int Sesion_ID, int Usuario_ID, int SuscripcionID)
    {
        string Consulta = ObtenConsultaPredeterminada(Usuario_ID, SuscripcionID);
        return Inserta(Sesion_ID, SuscripcionID, "Sitio Predeterminado", new DateTime(1980, 11, 24, 0, 0, 0), new DateTime(1980, 11, 24, 23, 59, 59), 30 * 60, Consulta);
    }
    public static int Inserta(int Sesion_ID, int SuscripcionID, string NombreSitio, DateTime SITIO_HDESDE_SVEC, DateTime SITIO_HHASTA_SVEC, int segundossync)
    {
        return Inserta(Sesion_ID,SuscripcionID,NombreSitio,SITIO_HDESDE_SVEC,SITIO_HHASTA_SVEC,segundossync,ObtenConsultaPredeterminada(0,SuscripcionID));
    }
    public static int Inserta(int Sesion_ID, int SuscripcionID, string NombreSitio, DateTime SITIO_HDESDE_SVEC, DateTime SITIO_HHASTA_SVEC, int segundossync, string ConsultaPersonaID)
    {

        int sitio_id = CeC_Autonumerico.GeneraAutonumerico("EC_SITIOS", "SITIO_ID", Sesion_ID, SuscripcionID);
        if (sitio_id < 0)
            return -9996;
        string Consulta = ConsultaPersonaID;
        if (CeC_BD.EjecutaComando(" INSERT INTO EC_SITIOS(SITIO_ID,SITIO_NOMBRE" +
           ",SITIO_CONSULTA ,SITIO_HDESDE_SVEC,SITIO_HHASTA_SVEC ,SITIO_SEGUNDOS_SYNC" +
           ",SITIO_BORRADO) VALUES (" + sitio_id + ",'" + NombreSitio + "','" + Consulta + "'," + CeC_BD.SqlFecha(SITIO_HDESDE_SVEC) + "," + CeC_BD.SqlFecha(SITIO_HHASTA_SVEC) + "," + segundossync + ",0)") > 0)
            return sitio_id;
        return -1;

    }
    public static bool SitiosActualiza(int Sesion_ID, int SuscripcionID, int Sitio_ID, string NombreSitio, int segundossync)
    {
        if (SuscripcionID < 0)
            return false;
        if (CeC_BD.EjecutaComando(" UPDATE EC_SITIOS SET SITIO_NOMBRE ='" + NombreSitio + "'," +
            "SITIO_SEGUNDOS_SYNC =" + segundossync + ",SITIO_BORRADO = 0 WHERE  SITIO_ID = " + Sitio_ID) > 0)
            return true;
        return false;

    }
    public static int TerminalesActualiza(int Sesion_ID, int Suscripcion_ID, string TERMINAL_NOMBRE,
       int SITIO_ID, int MODELO_TERMINAL_ID, int TIPO_TECNOLOGIA_ID, string TERMINAL_CAMPO_LLAVE,
          string TERMINAL_DIR, int TERMINAL_MINUTOS_DIF, int TerminalID)
    {
        //int SuscripcionID = CeC_BD.EjecutaEscalarInt("SELECT SUSCRIPCION_ID FROM EC_USUARIOS WHERE USUARIO_ID = " + Usuario_ID);
        if (Suscripcion_ID < 0)
            return -9997;
        return CeC_BD.EjecutaComando("UPDATE EC_TERMINALES  SET TERMINAL_NOMBRE ='" + TERMINAL_NOMBRE + "',SITIO_ID =" + SITIO_ID +
               ",MODELO_TERMINAL_ID =" + MODELO_TERMINAL_ID + ",TIPO_TECNOLOGIA_ID =" + TIPO_TECNOLOGIA_ID + ",TERMINAL_CAMPO_LLAVE ='" + TERMINAL_CAMPO_LLAVE +
               "',TERMINAL_DIR = '" + TERMINAL_DIR + "',TERMINAL_MINUTOS_DIF = " + TERMINAL_MINUTOS_DIF + " WHERE TERMINAL_ID =" + TerminalID);

    }
    public static int TerminalesDehabilita(int TerminalID)
    {
        return CeC_BD.EjecutaComando("UPDATE EC_TERMINALES  SET TERMINAL_BORRADO = 1 WHERE TERMINAL_ID =" + TerminalID);

    }
}



