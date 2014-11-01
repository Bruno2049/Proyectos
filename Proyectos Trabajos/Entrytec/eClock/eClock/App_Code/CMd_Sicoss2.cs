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
/// Descripción breve de CMd_Sicoss2
/// </summary>
public class CMd_Sicoss2 : CMd_Base
{
    public CMd_Sicoss2()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    /// <summary>
    /// Obtiene el nombre del módulo
    /// </summary>
    /// <returns></returns>
    public override string LeeNombre()
    {
        return "Integración Ver. 2.0 Sicoss";
    }


    /// <summary>
    /// esta función será ejecutada en la clase de asistencias una instante
    /// despues de generar las faltas, y una vez cada hora
    /// </summary>
    /// <returns></returns>
    public override bool EjecutarUnaVezCadaHora()
    {
        if (EjecutaCadaHora)
            EjecutarUnaVezAlDia();
        return true;
    }

    /// <summary>
    /// esta función será ejecutada en la clase de asistencias una instante
    /// despues de generar las faltas, y una vez al día
    /// </summary>
    /// <returns></returns>
    public override bool EjecutarUnaVezAlDia()
    {
        try
        {
            ActualizaTiposIncidencias(-1);
            ActualizaEmpleados(-1, false);

            //EnviarChecadas();
            return true;
        }
        catch
        {
        }
        return false;
    }

    string m_URL_WS_Empleados = "http://www.gruposicoss.com.mx/WSSicoss/WSEmpleado/Empleado.asmx";
    [Description("Contiene la URL del webservice de empleados")]
    [DisplayNameAttribute("URL_WS_Empleados")]
    public string URL_WS_Empleados
    {
        get { return m_URL_WS_Empleados; }
        set { m_URL_WS_Empleados = value; }
    }

    string m_URL_WS_Tabuladores = "http://www.gruposicoss.com.mx/WSSicoss/WSTabuladores/WSTabuladores.asmx";
    [Description("Contiene la URL del webservice de tabuladores")]
    [DisplayNameAttribute("URL_WS_Tabuladores")]
    public string URL_WS_Tabuladores
    {
        get { return m_URL_WS_Tabuladores; }
        set { m_URL_WS_Tabuladores = value; }
    }

    string m_URL_WS_Faltas = "";
    [Description("Contiene la URL del webservice de Faltas")]
    [DisplayNameAttribute("URL_WS_Faltas")]
    public string URL_WS_Faltas
    {
        get { return m_URL_WS_Faltas; }
        set { m_URL_WS_Faltas = value; }
    }
    string m_URL_WS_Movimientos = "";
    [Description("Contiene la URL del webservice de Movimientos")]
    [DisplayNameAttribute("URL_WS_Movimientos")]
    public string URL_WS_Movimientos
    {
        get { return m_URL_WS_Movimientos; }
        set { m_URL_WS_Movimientos = value; }
    }

    string m_URL_WS_IncapacidadesTMP = "";
    [Description("Contiene la URL del webservice de Incapacidades (Temporal por Benjaa)")]
    [DisplayNameAttribute("m_URL_WS_IncapacidadesTMP")]
    public string URL_WS_IncapacidadesTMP
    {
        get { return m_URL_WS_IncapacidadesTMP; }
        set { m_URL_WS_IncapacidadesTMP = value; }
    }
    string m_Cliente_ID = "";
    [Description("Identificador de Cliente registro en la base InternetSeguridad")]
    [DisplayNameAttribute("Cliente_ID")]
    public string Cliente_ID
    {
        get { return m_Cliente_ID; }
        set { m_Cliente_ID = value; }
    }

    bool m_EsPrimeraCarga = true;
    [Description("Indica si es la primera carga")]
    [DisplayNameAttribute("EsPrimeraCarga")]
    public bool EsPrimeraCarga
    {
        get { return m_EsPrimeraCarga; }
        set { m_EsPrimeraCarga = value; }
    }

    DateTime m_FechaPrimeraCarga = DateTime.Now;
    [Description("Indica la fecha de la primera carga")]
    [DisplayNameAttribute("FechaPrimeraCarga")]
    public DateTime FechaPrimeraCarga
    {
        get { return m_FechaPrimeraCarga; }
        set { m_FechaPrimeraCarga = value; }
    }

    string m_BD_Servidor = "";
    [Description("Nombre de servidor o servicio SQL")]
    [DisplayNameAttribute("BD_Servidor")]
    public string BD_Servidor
    {
        get { return m_BD_Servidor; }
        set { m_BD_Servidor = value; }
    }

    string m_BD_Nombre = "";
    [Description("Nombre de la base de datos")]
    [DisplayNameAttribute("BD_Nombre")]
    public string BD_Nombre
    {
        get { return m_BD_Nombre; }
        set { m_BD_Nombre = value; }
    }

    string m_BD_Usuario = "";
    [Description("Usuario de acceso a base de datos")]
    [DisplayNameAttribute("BD_Usuario")]
    public string BD_Usuario
    {
        get { return m_BD_Usuario; }
        set { m_BD_Usuario = value; }
    }

    string m_BD_Clave = "";
    [Description("Clave de acceso a la base de datos")]
    [DisplayNameAttribute("BD_Clave")]
    ///Obtiene o establece la URLWebServiceSicoss
    public string BD_Clave
    {
        get { return m_BD_Clave; }
        set { m_BD_Clave = value; }
    }

    private string ObtenString(object Valor, int MaxLen)
    {
        try
        {
            if (Valor == null)
                return "";
            if (Valor == DBNull.Value)
                return "";
            string Texto = Valor.ToString();
            if (Texto.Length <= MaxLen)
                return Texto;
            return Texto.Substring(0, MaxLen);
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return "";
    }
    static bool m_CargandoInicial = false;
    public bool CargaInicial(bool PrimeraCarga)
    {
        if (m_CargandoInicial)
            return false;
        m_CargandoInicial = true;
        try
        {
            WSEmpleado.Empleado wsEmpleado = new WSEmpleado.Empleado();
            wsEmpleado.Url = URL_WS_Empleados;
            CIsLog2.AgregaLog("ActualizaEmpleados-> Preparando para obtener empleados de sicoss " + wsEmpleado.Url);
            /* DataSet DS_Empleado = wsEmpleado.GetEmpleado(Cliente_ID, "000002");
             DS_Empleado.WriteXml(CeC_Sesion.ObtenRutaArchivoTemp("DS_Empleado"));*/

            if (!wsEmpleado.dbOpen(BD_Servidor, BD_Nombre, BD_Usuario, BD_Clave))
                return false;
            DataSet DS_EmpleadosActivos = wsEmpleado.ListaEmpleadosVigentes(Cliente_ID);
            CIsLog2.AgregaLog("ActualizaEmpleados-> Finalizado");
            if (DS_EmpleadosActivos != null && DS_EmpleadosActivos.Tables.Count > 0 && DS_EmpleadosActivos.Tables[0].Rows.Count > 0)
            {
                CeC_BD.EjecutaComando("UPDATE EC_PERSONAS SET PERSONA_BORRADO = 1 WHERE PERSONA_LINK_ID IN (SELECT " + CeC_Campos.CampoTE_Llave + " FROM EC_PERSONAS_DATOS WHERE CLAVE_EMPL IS NOT NULL AND CLAVE_EMPL <> '' )");
                foreach (DataRow DR in DS_EmpleadosActivos.Tables[0].Rows)
                {
                    try
                    {
                        string Trab_ID = DR[0].ToString();
                        int PERSONA_LINK_ID = Convert.ToInt32(Trab_ID);
                        CeC_BD.EjecutaComando("UPDATE EC_PERSONAS SET PERSONA_BORRADO = 0 WHERE PERSONA_LINK_ID = " + PERSONA_LINK_ID);
                    }
                    catch (Exception exx)
                    {
                    }
                }
                DS_CMd_BaseTableAdapters.EC_PERSONAS_DATOSTableAdapter EC_PERSONAS_DATOS_TA = new DS_CMd_BaseTableAdapters.EC_PERSONAS_DATOSTableAdapter();
                DS_CMd_Base.EC_PERSONAS_DATOSDataTable EC_PERSONAS_DATOS = EC_PERSONAS_DATOS_TA.GetData();
                DS_CMd_BaseTableAdapters.EC_PERSONASTableAdapter EC_PERSONAS_TA = new DS_CMd_BaseTableAdapters.EC_PERSONASTableAdapter();
                DS_CMd_Base.EC_PERSONASDataTable EC_PERSONAS = EC_PERSONAS_TA.GetData();

                foreach (DataRow DR in DS_EmpleadosActivos.Tables[0].Rows)
                {
                    try
                    {
                        string Trab_ID = DR[0].ToString();

                        int PERSONA_LINK_ID = Convert.ToInt32(Trab_ID);
                        DS_CMd_Base.EC_PERSONAS_DATOSRow Empleado = EC_PERSONAS_DATOS.FindByPERSONA_LINK_ID(PERSONA_LINK_ID);
                        int Persona_ID = CeC_Personas.ObtenPersonaID(PERSONA_LINK_ID);
                        DS_CMd_Base.EC_PERSONASRow Persona = null;
                        if (Persona_ID > 0)
                            Persona = EC_PERSONAS.FindByPERSONA_ID(Persona_ID);
                        if (Empleado == null)
                        {
                            Empleado = EC_PERSONAS_DATOS.NewEC_PERSONAS_DATOSRow();
                            Empleado.PERSONA_LINK_ID = PERSONA_LINK_ID;

                        }
/*                        else
                        {
                            //prueba borrar
                            Empleado.ZONA= "1";
                            EC_PERSONAS_DATOS_TA.Update(EC_PERSONAS_DATOS);
                            continue;
                        }*/
                        if (Empleado.PERSONA_LINK_ID == 110 || Empleado.PERSONA_LINK_ID == 101)
                        {
                            Empleado.PERSONA_LINK_ID = Empleado.PERSONA_LINK_ID;
                        }
                        if (Persona != null)
                        {
                            Persona.PERSONA_BORRADO = 0;
                            /*   Persona.PERSONA_NOMBRE = Adam_Trabajador.nombre;
                               if (!Adam_Trabajador.Ise_mailNull())
                                   Persona.PERSONA_EMAIL = Adam_Trabajador.e_mail;
                               else
                                   Persona.SetPERSONA_EMAILNull();*/
                        }
                        /*if (!PrimeraCarga && Empleado.RowState != DataRowState.Detached && Empleado.CLAVE_EMPL == Trab_ID)
                            continue;*/
                        Empleado.CLAVE_EMPL = Trab_ID;
                        DataSet DS_Empleado = wsEmpleado.GetEmpleado(Cliente_ID, Trab_ID);

                        DataRow DR_Trab = DS_Empleado.Tables["Trab"].Rows[0];
                        Empleado.NOMBRE = ObtenString(DR_Trab["Nombre"], EC_PERSONAS_DATOS.NOMBREColumn.MaxLength);
                        Empleado.APATERNO = ObtenString(DR_Trab["Paterno"], EC_PERSONAS_DATOS.APATERNOColumn.MaxLength);
                        Empleado.AMATERNO = ObtenString(DR_Trab["Materno"], EC_PERSONAS_DATOS.AMATERNOColumn.MaxLength);
                        Empleado.RFC = ObtenString(DR_Trab["RFC"], EC_PERSONAS_DATOS.RFCColumn.MaxLength);
                        Empleado.IMSS = ObtenString(DR_Trab["IMSS"], EC_PERSONAS_DATOS.IMSSColumn.MaxLength);
                        Empleado.CURP = ObtenString(DR_Trab["CURP"], EC_PERSONAS_DATOS.CURPColumn.MaxLength);
                        Empleado.DP_CALLE_NO = ObtenString(DR_Trab["Calle"], EC_PERSONAS_DATOS.DP_CALLE_NOColumn.MaxLength);
                        Empleado.DP_COLONIA = ObtenString(DR_Trab["Colonia"], EC_PERSONAS_DATOS.DP_COLONIAColumn.MaxLength);
                        Empleado.DP_CP = ObtenString(DR_Trab["CP"], EC_PERSONAS_DATOS.DP_CPColumn.MaxLength);
                        Empleado.DP_ESTADO = ObtenString(DR_Trab["Estado"], EC_PERSONAS_DATOS.DP_ESTADOColumn.MaxLength);
                        Empleado.DP_CIUDAD = ObtenString(DR_Trab["Ciudad"], EC_PERSONAS_DATOS.DP_CIUDADColumn.MaxLength);
                        Empleado.DP_TELEFONO1 = ObtenString(DR_Trab["Telefono"], EC_PERSONAS_DATOS.DP_TELEFONO1Column.MaxLength);
                        Empleado.AREA = ObtenString(DR_Trab["Ocupacion"], EC_PERSONAS_DATOS.AREAColumn.MaxLength);
                        Empleado.CAMPO2 = ObtenString(DR_Trab["Observacion"], EC_PERSONAS_DATOS.CAMPO2Column.MaxLength);

                        DataRow DR_Mov = DS_Empleado.Tables["Mov"].Rows[DS_Empleado.Tables["Mov"].Rows.Count - 1];
                        Empleado.CENTRO_DE_COSTOS = ObtenString(DR_Mov["Centro"], EC_PERSONAS_DATOS.CENTRO_DE_COSTOSColumn.MaxLength);
                        Empleado.DEPARTAMENTO = ObtenString(DR_Mov["Depto"], EC_PERSONAS_DATOS.DEPARTAMENTOColumn.MaxLength);
                        Empleado.PUESTO = ObtenString(DR_Mov["Puesto"], EC_PERSONAS_DATOS.PUESTOColumn.MaxLength);
                        Empleado.CAMPO1 = ObtenString(DR_Mov["Contrato"], EC_PERSONAS_DATOS.CAMPO1Column.MaxLength);
                        try
                        {
                            Empleado.FECHA_INGRESO = Convert.ToDateTime(DR_Trab["FechaIngreso"]);
                            Empleado.CAMPO5 = Empleado.FECHA_INGRESO.ToString("dd' 'MMMM' de 'yyyy");
                            foreach (DataRow FilaMovimiento in DS_Empleado.Tables["Mov"].Rows)
                            {
                                try
                                {
                                    string Mov_ID = FilaMovimiento["Mov_ID"].ToString();
                                    if (Mov_ID == "A" || Mov_ID == "R")
                                    {
                                        Empleado.CAMPO5 = Convert.ToDateTime(FilaMovimiento["FechaReingreso"]).ToString("dd' 'MMMM' de 'yyyy");
                                        if (Persona != null)
                                            Persona.PERSONA_BORRADO = 0;
                                    }
                                    if (Mov_ID == "B")
                                    {
                                        if (Persona != null)
                                            Persona.PERSONA_BORRADO = 1;
                                    }
                                }
                                catch { }
                            }

                        }
                        catch { }

                        if (Empleado.RowState == DataRowState.Detached)
                            EC_PERSONAS_DATOS.AddEC_PERSONAS_DATOSRow(Empleado);
                        EC_PERSONAS_DATOS_TA.Update(EC_PERSONAS_DATOS);
                    }
                    catch (Exception exx)
                    {
                        CIsLog2.AgregaError("ActualizaEmpleados-> No se pudo convertir a entero " + DR[0].ToString());
                        CIsLog2.AgregaError(exx);
                    }

                }
                EC_PERSONAS_TA.Update(EC_PERSONAS);
                EC_PERSONAS_DATOS_TA.Update(EC_PERSONAS_DATOS);
                CeC_BD.CreaRelacionesEmpleados();

            }
            m_CargandoInicial = false;
            return true;
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        m_CargandoInicial = false;
        return false;
    }

    public override bool ActualizaEmpleados(int SuscripcionID, bool Manual)
    {

        if (CargaInicial(EsPrimeraCarga))
        {
            if (EsPrimeraCarga)
            {
                FechaPrimeraCarga = DateTime.Now;
                EsPrimeraCarga = false;
                GuardaParametros();
            }
            return true;
        }
        return false;
    }
    public static Byte[] ObtenArregloBytes(string Cadena)
    {
        if (Cadena.Length < 1)
            return null;
        Byte[] Arreglo = new byte[Cadena.Length + 1];
        for (int Cont = 0; Cont < Cadena.Length; Cont++)
        {
            Arreglo[Cont] = Convert.ToByte(Cadena[Cont]);
        }
        Arreglo[Cadena.Length] = 0;
        return Arreglo;
    }
    public static int ObtenID(string Cadena)
    {
        byte[] Cad = ObtenArregloBytes(Cadena);
        int Ret = 0;
        for (int Cont = 0; Cont < Cad.Length; Cont++)
        {
            Ret = Ret * 256 + Cad[Cont];
        }
        return Ret;
    }
    public override bool ActualizaTiposIncidencias(int SuscripcionID)
    {
        string InExIDS = "";
        string InIDS = "";
        try
        {
            WSTabuladores.Service wsTabuladores = new WSTabuladores.Service();
            wsTabuladores.Url = URL_WS_Tabuladores;
            if (!wsTabuladores.dbOpen(BD_Servidor, BD_Nombre, BD_Usuario, BD_Clave))
                return false;
            DataSet DS = wsTabuladores.LeerTabulador(WSTabuladores.TabuladorSicoss.TAB_FALTAS);

            DS_CMd_BaseTableAdapters.EC_TIPO_INCIDENCIAS_EXTableAdapter EC_TipoInc_Ex_TA = new DS_CMd_BaseTableAdapters.EC_TIPO_INCIDENCIAS_EXTableAdapter();
            DS_CMd_Base.EC_TIPO_INCIDENCIAS_EXDataTable EC_TipoIncEx = EC_TipoInc_Ex_TA.GetData();
            DS_CMd_BaseTableAdapters.EC_TIPO_INCIDENCIASTableAdapter EC_TipoInc_TA = new DS_CMd_BaseTableAdapters.EC_TIPO_INCIDENCIASTableAdapter();
            DS_CMd_Base.EC_TIPO_INCIDENCIASDataTable EC_TipoInc = EC_TipoInc_TA.GetData();
            object [] Registro =  new object[2];
            DS.Tables[0].Rows.Add("I1", "Enfermedad general",false);
            DS.Tables[0].Rows.Add("I2", "Riesgo de trabajo", false);
            DS.Tables[0].Rows.Add("I3", "Maternidad", false);
            foreach (DataRow Fila in DS.Tables[0].Rows)
            {
                try
                {
                    string Falta_ID = Fila["Falta_ID"].ToString();
                    string Descripcion = Fila["Descripcion"].ToString();
                    bool Descuenta = Convert.ToBoolean(Fila["Descuenta"]);
                    DS_CMd_Base.EC_TIPO_INCIDENCIAS_EXRow TipoInc = null;
                    int Falta_ID_Int = ObtenID(Falta_ID);
                    TipoInc = EC_TipoIncEx.FindByTIPO_INCIDENCIAS_EX_ID(Falta_ID_Int);
                    if (TipoInc == null)
                    {
                        TipoInc = EC_TipoIncEx.NewEC_TIPO_INCIDENCIAS_EXRow();
                        TipoInc.TIPO_INCIDENCIAS_EX_ID = Falta_ID_Int;
                        TipoInc.TIPO_FALTA_EX_ID = 0;
                        TipoInc.TIPO_INCIDENCIAS_EX_TXT = Falta_ID;
                        TipoInc.TIPO_INCIDENCIAS_EX_BORRADO = 0;
                    }
                    TipoInc.TIPO_INCIDENCIAS_EX_NOMBRE = Descripcion.Trim();
                    if (Descuenta)
                        TipoInc.TIPO_INCIDENCIAS_EX_DESC = 1;
                    else
                        TipoInc.TIPO_INCIDENCIAS_EX_DESC = 0;


                    if (TipoInc.RowState == DataRowState.Detached)
                    {
                        EC_TipoIncEx.AddEC_TIPO_INCIDENCIAS_EXRow(TipoInc);
                        string Abr = TipoInc.TIPO_INCIDENCIAS_EX_TXT;
                        if (Abr.Length > 2)
                            Abr = Abr.Substring(0, 2);
                        InExIDS += TipoInc.TIPO_INCIDENCIAS_EX_ID.ToString() + "|";
                        int IDInc = CeC_Autonumerico.GeneraAutonumerico("EC_TIPO_INCIDENCIAS", "TIPO_INCIDENCIA_ID");
                        InIDS += IDInc + "|";
                        EC_TipoInc.AddEC_TIPO_INCIDENCIASRow(IDInc,
                            TipoInc.TIPO_INCIDENCIAS_EX_NOMBRE.Trim(),
                            Abr, 0);
                    }
                }
                catch (Exception exc)
                {
                    CIsLog2.AgregaError(exc);
                }
            }

            EC_TipoInc_Ex_TA.Update(EC_TipoIncEx);
            EC_TipoInc_TA.Update(EC_TipoInc);



        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        string[] InExID = InExIDS.Split(new char[] { '|' });
        string[] InID = InIDS.Split(new char[] { '|' });
        DS_CMd_BaseTableAdapters.EC_TIPO_INCIDENCIAS_EX_INCTableAdapter TA = new DS_CMd_BaseTableAdapters.EC_TIPO_INCIDENCIAS_EX_INCTableAdapter();
        for (int Cont = 0; Cont < InExID.Length - 1; Cont++)
        {
            try
            {

                TA.Insert(Convert.ToInt32(InExID[Cont]), Convert.ToInt32(InID[Cont]));
            }
            catch (Exception exd)
            {
                CIsLog2.AgregaError(exd);
            }
        }
        DS_CMd_BaseTableAdapters.EC_TIPO_INCIDENCIAS_EXTableAdapter TAINC = new DS_CMd_BaseTableAdapters.EC_TIPO_INCIDENCIAS_EXTableAdapter();
        TAINC.Actualiza_EC_TIPO_INCIDENCIAS_EX_INC();
        TAINC.Actualiza_EC_TIPO_INCIDENCIAS_EX_INC_SIS();
        return true;
        return false;
    }


    bool m_EjecutaCadaHora = false;
    [Description("Indica si se ejecutara la sincronización con sicoss cada hora")]
    [DisplayNameAttribute("EjecutaCadaHora")]
    ///Obtiene o establece el si se ejecutara una vez cada hora
    public bool EjecutaCadaHora
    {
        get { return m_EjecutaCadaHora; }
        set { m_EjecutaCadaHora = value; }
    }

    public override bool EnviaIncidencias(DateTime FechaInicial, DateTime FechaFinal, int PeriodoID, string SQLPersonas)
    {
        try
        {
            WSFaltaTrabID.WSFaltaTrabID wsFalta = new WSFaltaTrabID.WSFaltaTrabID();
            wsFalta.Url = URL_WS_Faltas;
            CIsLog2.AgregaLog("EnviaIncidencias-> Preparando para enviar incidencias a sicoss " + wsFalta.Url);


            if (!wsFalta.dbOpen(BD_Servidor, BD_Nombre, BD_Usuario, BD_Clave))
                return false;
            string Qry = "SELECT  EC_PERSONAS_DATOS.CLAVE_EMPL, EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA,  EC_TIPO_INCIDENCIAS_EX_INC_SIS.TIPO_INCIDENCIAS_EX_ID, " +
                        "EC_TIPO_INCIDENCIAS_EX_INC.TIPO_INCIDENCIAS_EX_ID AS TIPO_INCIDENCIAS_EX_ID2 " +
                        "FROM         EC_PERSONAS_DIARIO INNER JOIN " +
                        "EC_TIPO_INCIDENCIAS_EX_INC_SIS ON  " +
                        "EC_PERSONAS_DIARIO.TIPO_INC_SIS_ID = EC_TIPO_INCIDENCIAS_EX_INC_SIS.TIPO_INC_SIS_ID INNER JOIN " +
                        "EC_INCIDENCIAS ON EC_PERSONAS_DIARIO.INCIDENCIA_ID = EC_INCIDENCIAS.INCIDENCIA_ID INNER JOIN " +
                        "EC_TIPO_INCIDENCIAS_EX_INC ON EC_INCIDENCIAS.TIPO_INCIDENCIA_ID = EC_TIPO_INCIDENCIAS_EX_INC.TIPO_INCIDENCIA_ID INNER JOIN " +
                        "EC_PERSONAS ON EC_PERSONAS_DIARIO.PERSONA_ID = EC_PERSONAS.PERSONA_ID INNER JOIN " +
                        "EC_PERSONAS_DATOS ON EC_PERSONAS.PERSONA_LINK_ID = EC_PERSONAS_DATOS.PERSONA_LINK_ID " +
                        "WHERE EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA >= " + CeC_BD.SqlFecha(FechaInicial) +
                        "AND EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA <  " + CeC_BD.SqlFecha(FechaFinal.AddDays(1)) +
                        "AND EC_PERSONAS_DIARIO.PERSONA_ID IN (" + SQLPersonas + ") ";

            object Obj = CeC_BD.EjecutaDataSet(Qry);
            if (Obj != null)
            {
                DataSet DS = (DataSet)Obj;
                foreach (DataRow DR in DS.Tables[0].Rows)
                {
                    try
                    {
                        string CLAVE_EMPL = DR["CLAVE_EMPL"].ToString();
                        if (CLAVE_EMPL.Length <= 0)
                            continue;
                        DateTime PERSONA_DIARIO_FECHA = Convert.ToDateTime(DR["PERSONA_DIARIO_FECHA"]);
                        int TIPO_INCIDENCIAS_EX_ID = Convert.ToInt32(DR["TIPO_INCIDENCIAS_EX_ID"]);
                        int TIPO_INCIDENCIAS_EX_ID2 = Convert.ToInt32(DR["TIPO_INCIDENCIAS_EX_ID2"]);
                        if (TIPO_INCIDENCIAS_EX_ID2 > 0)
                        {

                            string TipoIncidenciaExTXT = Cec_Incidencias.ObtenTipoIncidenciaExTXT(TIPO_INCIDENCIAS_EX_ID2);
                            if (TipoIncidenciaExTXT == "I1" || TipoIncidenciaExTXT == "I2" || TipoIncidenciaExTXT == "I3")
                                continue;
                            wsFalta.Nueva(CLAVE_EMPL, PERSONA_DIARIO_FECHA, TipoIncidenciaExTXT, "eClock");
                        }
                        else
                            if (TIPO_INCIDENCIAS_EX_ID > 0)
                            {
                                string TipoIncidenciaExTXT = Cec_Incidencias.ObtenTipoIncidenciaExTXT(TIPO_INCIDENCIAS_EX_ID);
                                wsFalta.Nueva(CLAVE_EMPL, PERSONA_DIARIO_FECHA, TipoIncidenciaExTXT, "eClock");

                            }
                    }
                    catch (Exception ex1)
                    {
                        CIsLog2.AgregaError(ex1);
                    }
                }
            }

            wsFalta.dbClose();
            return true;
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return false;
    }

    public override bool RecibeIncidencias(DateTime FechaInicial, DateTime FechaFinal, string SQLPersonas)
    {
        try
        {
            WSFaltaTrabID.WSFaltaTrabID wsFalta = new WSFaltaTrabID.WSFaltaTrabID();
            wsFalta.Url = URL_WS_Faltas;
            CIsLog2.AgregaLog("RecibeIncidencias-> Preparando para obtener incidencias de sicoss " + wsFalta.Url);

            int Sesion_ID = CeC_Sesion.CreaSesion(0, 0);
            if (!wsFalta.dbOpen(BD_Servidor, BD_Nombre, BD_Usuario, BD_Clave))
                return false;
            DataSet DS = wsFalta.LeerTodos(FechaInicial, FechaFinal);
            if (DS != null)
            {
                
                foreach (DataRow DR in DS.Tables[0].Rows)
                {
                    try
                    {
                        int Persona_ID = CeC_Personas.ObtenPersonaID(Convert.ToInt32(DR["Trab_ID"]));
                        if(Persona_ID > 0)
                        {
                            //Verifica si hay checadas de un empleado en el intervalo de fechas específicas
                            int checadas = CeC_BD.EjecutaEscalarInt("SELECT SUM(CASE WHEN EC_ACCESOS.ACCESO_ID > 0 THEN 1 END )" +
                                                   " FROM EC_ACCESOS INNER JOIN EC_TERMINALES ON EC_ACCESOS.TERMINAL_ID = EC_TERMINALES.TERMINAL_ID" +
                                                   " WHERE (EC_ACCESOS.PERSONA_ID = " + Persona_ID + ") AND (EC_ACCESOS.ACCESO_FECHAHORA >= '" +
                                                   FechaInicial.ToString("dd/MM/yyyy") + "') AND (EC_ACCESOS.ACCESO_FECHAHORA <= '" + FechaFinal.ToString("dd/MM/yyyy") + "')" +
                                                   " AND (EC_TERMINALES.TERMINAL_ASISTENCIA = 1) AND (EC_ACCESOS.TIPO_ACCESO_ID = 1)");
                            //Si no hay checadas de entrada y de salida asigna 'Asistencia Normal' al empleado
                            //para posteriormente asignar las incidencias que se capturaron en el sistema de nómina
                            if (checadas == 0 || checadas == -9999)
                            {
                                CeC_BD.EjecutaComando("UPDATE EC_PERSONAS_DIARIO SET TIPO_INC_SIS_ID = 1 " +
                                                       "WHERE PERSONA_ID = " + Persona_ID + " AND PERSONA_DIARIO_FECHA >= '" +
                                                       FechaInicial.ToString("dd/MM/yyyy") + "' AND PERSONA_DIARIO_FECHA <= '" + 
                                                       FechaFinal.ToString("dd/MM/yyyy") + "';");
                            } 
                            DateTime Fecha = Convert.ToDateTime(DR["Fecha"]);
                            int TipoIncidenciaExID = Cec_Incidencias.ObtenTipoIncidenciaExID(Persona_ID, Fecha);
                            string ID = Cec_Incidencias.ObtenTipoIncidenciaExTXT(TipoIncidenciaExID);
                            string IncidenciaSicoss = DR["Falta_ID"].ToString();
                            if (IncidenciaSicoss != ID)
                            {
                                int IncidenciaSicossID = Cec_Incidencias.ObtenTipoIncidenciaExID(IncidenciaSicoss);
                                int TipoIncidenciaID = Cec_Incidencias.ObtenTipoIncidenciaID(IncidenciaSicossID);
                                if (TipoIncidenciaID > 0)
                                {
                                    int IncidenciaID = Cec_Incidencias.CreaIncidencia(TipoIncidenciaID, "Capturado en Sicoss", Sesion_ID);
                                    Cec_Incidencias.AsignaIncidencia(Fecha, Fecha, Persona_ID, IncidenciaID);
                                }
                            }
                        }
                    }
                    catch (Exception ex1)
                    {
                        CIsLog2.AgregaError(ex1);
                    }

                }
            }
            wsFalta.dbClose();
            //WSIncapacidad
            WSIncapacidad.WS_Incapacidad wsIncapacidad = new WSIncapacidad.WS_Incapacidad();
            wsIncapacidad.Url = URL_WS_IncapacidadesTMP;
            CIsLog2.AgregaLog("RecibeIncidencias-> Preparando para obtener incapacidades de sicoss " + wsIncapacidad.Url);
            if (!wsIncapacidad.dbOpen(BD_Servidor, BD_Nombre, BD_Usuario, BD_Clave))
                return false;
            WSIncapacidad.DSIncapacidadSicoss.IncapacidadDataTable DTInc = wsIncapacidad.IncapacidadporFecha(FechaInicial, FechaFinal);
            if (DTInc.Rows.Count > 0)
            {
                foreach (WSIncapacidad.DSIncapacidadSicoss.IncapacidadRow iDR in DTInc)
                {
                    
                    try
                    {
                        int Persona_ID = CeC_Personas.ObtenPersonaID(Convert.ToInt32(iDR.Trab_ID));
                        if (Persona_ID > 0)
                        {
                            DateTime FechaI = Convert.ToDateTime(iDR.FechaInicio);
                            DateTime FechaF = Convert.ToDateTime(iDR.FechaFinal);
                            while (FechaI <= FechaF)
                            {
                                string IncidenciaSicoss = "I" + iDR.Rama_ID.ToString();

                                int TipoIncidenciaExID = Cec_Incidencias.ObtenTipoIncidenciaExID(Persona_ID, FechaI);
                                string ID = Cec_Incidencias.ObtenTipoIncidenciaExTXT(TipoIncidenciaExID);
                                if (IncidenciaSicoss != ID)
                                {
                                    int IncidenciaSicossID = Cec_Incidencias.ObtenTipoIncidenciaExID(IncidenciaSicoss);
                                    int TipoIncidenciaID = Cec_Incidencias.ObtenTipoIncidenciaID(IncidenciaSicossID);
                                    if (TipoIncidenciaID > 0)
                                    {
                                        int IncidenciaID = Cec_Incidencias.CreaIncidencia(TipoIncidenciaID, "Capturado en Sicoss", Sesion_ID);
                                        Cec_Incidencias.AsignaIncidencia(Convert.ToDateTime(iDR.FechaInicio), Convert.ToDateTime(iDR.FechaFinal), Persona_ID, IncidenciaID);
                                        break;
                                    }
                                }
                                FechaI = FechaI.AddDays(1);
                            }
                        }
                    }
                    catch (Exception ex1)
                    {
                        CIsLog2.AgregaError(ex1);
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
}

/*
Trab

DR["Trab_ID"]
Tarjeta_ID
CURP
Banco_ID
Paterno
Materno
Nombre
IMSS
RFC
FechaIngreso
Depto_ID
Puerto_ID
Ocupacion
Actualizado
Observacion
FechaNacimiento
NacimientoLugar
Sexo_IDa
EstadoCivil_IDa
FechaCasado
CasadoLugar
Calle
Colonia
CP
Estado
Ciudad
Cartilla
Pasaporte
Permiso
Nacion_ID
Estudio_IDa
Valuacion_ID
InfonavitTipo_IDa
HorasJornada
PagaHorasExtras
Telefono

Mov

Centro
Depto
Puesto
Contrato

TrabFoto
TrabFoto
 */