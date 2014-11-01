using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;

/// <summary>
/// Descripción breve de CMd_Mails
/// </summary>
public class CMd_Mails : CMd_Base
{
    public CMd_Mails()
    {
    }
    public override string LeeNombre()
    {
        return "Envio Automatico Reportes";
    }
    /// <summary>
    /// Obtiene los usuarios que tienen pendiente el envio de mails de faltas o retardos 
    /// valida que no se hayan procesado hoy
    /// </summary>
    /// <param name="Parametro">EnviarMailFaltas o EnviarMailRetardos</param>
    /// <returns></returns>
    public DataSet ObtenUsuariosFoR(string Parametro)
    {
        string Qry = " " +
                "SELECT     EC_USUARIOS.USUARIO_ID, EC_USUARIOS.USUARIO_NOMBRE, EC_USUARIOS.USUARIO_EMAIL, EC_USUARIOS.SUSCRIPCION_ID " +
                "FROM         EC_ACCESOS INNER JOIN " +
                "EC_PERSONAS ON EC_ACCESOS.PERSONA_ID = EC_PERSONAS.PERSONA_ID INNER JOIN " +
                "EC_USUARIOS ON EC_PERSONAS.SUSCRIPCION_ID = EC_USUARIOS.SUSCRIPCION_ID INNER JOIN " +
                "EC_CONFIG_USUARIO ON EC_USUARIOS.USUARIO_ID = EC_CONFIG_USUARIO.USUARIO_ID " +
                "WHERE     (EC_CONFIG_USUARIO.CONFIG_USUARIO_VARIABLE = '" + Parametro + "') AND (EC_CONFIG_USUARIO.CONFIG_USUARIO_VALOR = 'True') AND  " +
                "(EC_ACCESOS.ACCESO_CALCULADO = 1) AND EC_ACCESOS.TERMINAL_ID > 0 AND EC_ACCESOS.ACCESO_FECHAHORA >= " + CeC_BD.SqlFecha(DateTime.Today) + " AND " +
                "EC_USUARIOS.USUARIO_ID NOT IN (SELECT USUARIO_ID FROM EC_CONFIG_USUARIO WHERE CONFIG_USUARIO_VARIABLE = '" + Parametro + "UltimaFecha' AND ";
        Qry += CeC_BD.SqlFecha("CONFIG_USUARIO_VALOR") + " = " + CeC_BD.SqlFecha(DateTime.Today) + " ";
        /*
        if (!CeC_BD.EsOracle)
            Qry += " Convert(datetime,CONFIG_USUARIO_VALOR,103) =" + CeC_BD.SqlFecha(DateTime.Today) + " ";
        else
            Qry += " TO_DATE(CONFIG_USUARIO_VALOR,'DD/MM/YYYY') =" + CeC_BD.SqlFecha(DateTime.Today) + " ";
            CIsLog2.AgregaError("FALTA definir ObtenUsuarios en Oracle");*/
        Qry += " ) GROUP BY EC_USUARIOS.USUARIO_ID, EC_USUARIOS.USUARIO_NOMBRE, EC_USUARIOS.USUARIO_EMAIL, EC_USUARIOS.SUSCRIPCION_ID ";
        return (DataSet)CeC_BD.EjecutaDataSet(Qry);
    }

    public DataSet ObtenUsuarios(string Parametro)
    {
        string Qry = " " +
                "SELECT     EC_USUARIOS.USUARIO_ID, EC_USUARIOS.USUARIO_NOMBRE, EC_USUARIOS.USUARIO_EMAIL, EC_USUARIOS.SUSCRIPCION_ID " +
                "FROM         EC_USUARIOS INNER JOIN " +
                "EC_CONFIG_USUARIO ON EC_USUARIOS.USUARIO_ID = EC_CONFIG_USUARIO.USUARIO_ID " +
                "WHERE     (EC_CONFIG_USUARIO.CONFIG_USUARIO_VARIABLE = '" + Parametro + "') AND (EC_CONFIG_USUARIO.CONFIG_USUARIO_VALOR = 'True')  ";
        Qry += " GROUP BY EC_USUARIOS.USUARIO_ID, EC_USUARIOS.USUARIO_NOMBRE, EC_USUARIOS.USUARIO_EMAIL, EC_USUARIOS.SUSCRIPCION_ID ";
        return (DataSet)CeC_BD.EjecutaDataSet(Qry);
    }


    /// <summary>
    /// Envia un email a un listado de usuarios
    /// </summary>
    /// <param name="DSUsuarios">Listado de usuarios</param>
    /// <param name="Titulo"></param>
    /// <param name="Cuerpo">reemplaza <USUARIO_ID> por el UsuarioID así sucesivamente</Usuario></param>
    /// <returns></returns>
    public int EnviaMails(DataSet DSUsuarios, string Titulo, string Cuerpo,byte[] Adjunto)
    {
        int R = 0;
        if (DSUsuarios == null || DSUsuarios.Tables.Count < 1 || DSUsuarios.Tables[0].Rows.Count < 1)
            return R;


        foreach (DataRow DR in DSUsuarios.Tables[0].Rows)
        {
            try
            {
                int UsuarioID = Convert.ToInt32(DR["USUARIO_ID"]);
                string UsuarioNombre = Convert.ToString(DR["USUARIO_NOMBRE"]);
                string eMail = Convert.ToString(DR["USUARIO_EMAIL"]);
                if (eMail.Length <= 0)
                    continue;
                Cuerpo = Cuerpo.Replace("<USUARIO_NOMBRE>", UsuarioNombre);
                Cuerpo = Cuerpo.Replace("<USUARIO_EMAIL>", eMail);
                Cuerpo = Cuerpo.Replace("<USUARIO_ID>", UsuarioID.ToString());
                if (CeC_BD.EnviarMail(eMail, "", Titulo, Cuerpo, Adjunto))
                    R++;
            }
            catch (Exception ex) { CIsLog2.AgregaError(ex); }
        }
        return R;
    }

    public DataSet ObtenUsuariosTerminalNoResponde(int Suscripcion)
    {
        string Propiedad = "EnviarMailTerminalNoResponde";
        string Qry = " " +
        "SELECT     USUARIO_ID, USUARIO_NOMBRE, USUARIO_EMAIL, SUSCRIPCION_ID " +
        "FROM         EC_USUARIOS WHERE (SUSCRIPCION_ID = " + Suscripcion + " OR PERFIL_ID = 1) AND USUARIO_ID IN (" +
        "SELECT USUARIO_ID FROM  EC_CONFIG_USUARIO WHERE CONFIG_USUARIO_VARIABLE = '" + Propiedad + "' AND CONFIG_USUARIO_VALOR = 'True')  ";
        Qry += " GROUP BY EC_USUARIOS.USUARIO_ID, EC_USUARIOS.USUARIO_NOMBRE, EC_USUARIOS.USUARIO_EMAIL, EC_USUARIOS.SUSCRIPCION_ID ";
        return (DataSet)CeC_BD.EjecutaDataSet(Qry);
    }
    public DataSet ObtenUsuariosFaltas()
    {
        return ObtenUsuariosFoR("EnviarMailFaltas");
    }

    public DataSet ObtenUsuariosRetardos()
    {
        return ObtenUsuariosFoR("EnviarMailRetardos");
    }
    public string AsignaValores(string Html_Registro, string AGRUPACION_NOMBRE, string PERSONA_LINK_ID,
        string PERSONA_NOMBRE, string PERSONA_DIARIO_FECHA, string TURNO, string ENTRADASALIDA)
    {
        Html_Registro = CeC_Html.AsignaParametro(Html_Registro, "AGRUPACION_NOMBRE", AGRUPACION_NOMBRE);
        Html_Registro = CeC_Html.AsignaParametro(Html_Registro, "PERSONA_LINK_ID", PERSONA_LINK_ID);
        Html_Registro = CeC_Html.AsignaParametro(Html_Registro, "PERSONA_NOMBRE", PERSONA_NOMBRE);
        Html_Registro = CeC_Html.AsignaParametro(Html_Registro, "PERSONA_DIARIO_FECHA", PERSONA_DIARIO_FECHA);
        Html_Registro = CeC_Html.AsignaParametro(Html_Registro, "TURNO", TURNO);
        Html_Registro = CeC_Html.AsignaParametro(Html_Registro, "ENTRADASALIDA", ENTRADASALIDA);
        return Html_Registro;
    }

    public void EnviaFaltasORetardos(bool Faltas)
    {
        DataSet DS = null;
        string Formato = "";
        string Titulo = "";
        string Server = CeC_Config.LinkURL;
        string ParametroT = "";

        if (Faltas)
        {
            DS = ObtenUsuariosFaltas();
            Formato = System.IO.File.ReadAllText(HttpRuntime.AppDomainAppPath + "PL_MailFaltas.htm");
            Titulo = "Faltas del ";
            ParametroT = "F";
        }
        else
        {
            DS = ObtenUsuariosRetardos();
            Formato = System.IO.File.ReadAllText(HttpRuntime.AppDomainAppPath + "PL_MailRetardos.htm");
            Titulo = "Retardos del ";
            ParametroT = "R";
        }

        string Html_Cabecera = CeC_Html.ObtenTabla(Formato, "Cabecera");
        string Html_Agrupacion = CeC_Html.ObtenTabla(Formato, "Agrupacion");
        string Html_Registros = CeC_Html.ObtenTabla(Formato, "Registros");
        string Html_Registro = CeC_Html.ObtenElemento(Formato, "tr", "Registro");
        string Html_Pie = CeC_Html.ObtenTabla(Formato, "Pie");

        string Html_RegistrosIni = Html_Registros.Substring(0, CeC_Html.ObtenElementoInicio(Html_Registros, "tr", "Registro"));
        string Html_RegistrosFin = Html_Registros.Substring(CeC_Html.ObtenElementoFin(Html_Registros, "tr", "Registro"));
        string Html_RegistroTitulo = AsignaValores(Html_Registro,
            CeC_Campos.ObtenEtiqueta("AGRUPACION_NOMBRE"),
            CeC_Campos.ObtenEtiqueta("PERSONA_LINK_ID"),
            CeC_Campos.ObtenEtiqueta("PERSONA_NOMBRE"),
            CeC_Campos.ObtenEtiqueta("PERSONA_DIARIO_FECHA"),
            CeC_Campos.ObtenEtiqueta("TURNO"),
            CeC_Campos.ObtenEtiqueta("ENTRADASALIDA")
            );

        if (DS == null || DS.Tables.Count < 1 || DS.Tables[0].Rows.Count < 1)
            return;


        foreach (DataRow DR in DS.Tables[0].Rows)
        {
            try
            {
                int UsuarioID = Convert.ToInt32(DR["USUARIO_ID"]);
                string UsuarioNombre = Convert.ToString(DR["USUARIO_NOMBRE"]);
                string eMail = Convert.ToString(DR["USUARIO_EMAIL"]);
                if (eMail.Length <= 0)
                    continue;
                CeC_Config Config = new CeC_Config(UsuarioID);
                DateTime UltimaFecha = DateTime.Now;
                if (Faltas)
                    UltimaFecha = Config.EnviarMailFaltasUltimaFecha;
                else
                    UltimaFecha = Config.EnviarMailRetardosUltimaFecha;

                string Periodo = "";
                DateTime Hasta = DateTime.Today;
                if (Hasta.AddDays(-1) == UltimaFecha.Date)
                    Periodo = UltimaFecha.ToString("dd/MM/yyyy");
                else
                    Periodo = UltimaFecha.ToString("dd/MM/yyyy") + " a " + Hasta.AddDays(-1).ToString("dd/MM/yyyy");
                string Campos = "AGRUPACION_NOMBRE, PERSONA_LINK_ID, PERSONA_NOMBRE, PERSONA_DIARIO_FECHA, TURNO, ENTRADASALIDA";
                string OrdenarPor = "AGRUPACION_NOMBRE, PERSONA_LINK_ID,PERSONA_DIARIO_FECHA";
                DataSet DS_Asistencias = CeC_Asistencias.ObtenAsistenciaLineal(0, "", UltimaFecha, Hasta,  Campos, OrdenarPor, Faltas, !Faltas, null);
                if (DS_Asistencias != null && DS_Asistencias.Tables.Count > 0 && DS_Asistencias.Tables[0].Rows.Count > 0)
                {


                    string NRegistro = "";
                    string Cuerpo = "";
                    string AgrupacionAnt = "-444";
                    bool AgregarFin = false;
                    string Html_CabeceraEditado = Html_Cabecera;
                    Html_CabeceraEditado = CeC_Html.AsignaParametro(Html_CabeceraEditado, "PERIODO", Periodo);
                    Html_CabeceraEditado = CeC_Html.AsignaParametro(Html_CabeceraEditado, "USUARIO_NOMBRE", UsuarioNombre);
                    Html_CabeceraEditado = CeC_Html.AsignaParametro(Html_CabeceraEditado, "FECHA_ENVIO", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));
                    Cuerpo = Html_CabeceraEditado;


                    foreach (DataRow Fila in DS_Asistencias.Tables[0].Rows)
                    {
                        string Agrupacion = Fila["AGRUPACION_NOMBRE"].ToString();
                        if (Agrupacion != AgrupacionAnt)
                        {
                            if (AgregarFin)
                                Cuerpo += Html_RegistrosFin;
                            AgrupacionAnt = Agrupacion;
                            Cuerpo += CeC_Html.AsignaParametro(Html_Agrupacion, "AGRUPACION_NOMBRE", Agrupacion);
                            Cuerpo += Html_RegistrosIni;
                            Cuerpo += Html_RegistroTitulo;
                        }
                        Cuerpo += AsignaValores(Html_Registro, Agrupacion, Fila["PERSONA_LINK_ID"].ToString(),
                            Fila["PERSONA_NOMBRE"].ToString(), Convert.ToDateTime(Fila["PERSONA_DIARIO_FECHA"]).ToString("dd/MM/yyyy"),
                            Fila["TURNO"].ToString(), Fila["ENTRADASALIDA"].ToString());
                    }
                    Cuerpo += Html_RegistrosFin;

                    string Html_PieEditado = Html_Pie;
                    string Link = Server;
                    if (Faltas)
                        Link += "WF_EM_Faltas.aspx";
                    else
                        Link += "WF_EM_Retardos.aspx";
                    Html_PieEditado = CeC_Html.AsignaParametro(Html_PieEditado, "LINK_LINK", Link + "?Parametros=" + ParametroT + UltimaFecha.ToString("yyyyMMdd") + Hasta.ToString("yyyyMMdd"));
                    Html_PieEditado = CeC_Html.AsignaParametro(Html_PieEditado, "LINK_TEXTO", Server);
                    Cuerpo += Html_PieEditado;
                    Cuerpo = Cuerpo.Replace("  ", " ");
                    Cuerpo = Cuerpo.Replace("  ", " ");
                    if (CeC_BD.EnviarMail(eMail, Titulo + Periodo, Cuerpo))
                        if (Faltas)
                            Config.EnviarMailFaltasUltimaFecha = DateTime.Today;
                        else
                            Config.EnviarMailRetardosUltimaFecha = DateTime.Today;
                }
            }
            catch (Exception ex) { CIsLog2.AgregaError(ex); }
        }

    }
    public void EnviaFaltas()
    {
        EnviaFaltasORetardos(true);

    }
    public void EnviaRetardos()
    {
        EnviaFaltasORetardos(false);
    }

    /// <summary>
    /// Se ejecuta esta función una vez cada hora despues de procesar faltas
    /// </summary>
    /// <returns></returns>
    public override bool EjecutarUnaVezCadaHora()
    {
        try
        {
            CIsLog2.AgregaLog("EjecutarUnaVezCadaHora Mails ");
            //Intenta enviar cada hora las faltas y retardos pendientes por envio
            EnviaFaltas();
            EnviaRetardos();

        }
        catch { }
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
            //Se omite el envio de mails desde aqui porque puede que no existan checadas del día anterior puesto que no poleara.
            CIsLog2.AgregaLog("EjecutarUnaVezAlDia Mails ");
            /*
            EnviaFaltas();
            EnviaRetardos();*/
            //  ListaEmpledos();
            if (Valida())
                EnviarMails();
            return true;
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return false;
    }

    public bool Valida()
    {
        return (DateTime.Now.Date >= CeC_Config.ENV_AUT_MAILS_DESDE.AddDays(CeC_Config.ENV_AUT_MAILS_DIAS).Date);


    }


    /// <summary>
    /// Envia los mails
    /// </summary>
    public void EnviarMails()
    {
        try
        {
            DS_EnviaReporte dS_EnviaReporte2 = new DS_EnviaReporte();
            DS_EnviaReporteTableAdapters.EC_USUARIOSTableAdapter TA = new DS_EnviaReporteTableAdapters.EC_USUARIOSTableAdapter();
            if (CeC_BD.EsOracle)
                TA.Remplaza("SELECT     USUARIO_ID, PERFIL_ID, USUARIO_USUARIO, USUARIO_NOMBRE, USUARIO_DESCRIPCION, USUARIO_EMAIL," +
                            " (SELECT     min (PERSONA_EMAIL)" +
                            " FROM          EC_PERSONAS" +
                            " WHERE      ( EC_USUARIOS.USUARIO_USUARIO =TO_CHAR(PERSONA_LINK_ID))) AS PERSONA_EMAIL, USUARIO_ENVMAILA" +
                            " FROM         EC_USUARIOS WHERE     (USUARIO_EMAIL IS NOT NULL)  AND (USUARIO_BORRADO = 0)");
            else
                TA.ActualizaIn("CONVERT(\"varchar\", PERSONA_LINK_ID)");

            TA.Fill(dS_EnviaReporte2.EC_USUARIOS);
            int ReportesEnviados = 0;
            foreach (DS_EnviaReporte.EC_USUARIOSRow Fila in dS_EnviaReporte2.EC_USUARIOS)
            {
                int Usuario_ID = Convert.ToInt32(Fila["USUARIO_ID"].ToString());
                string Email1 = Fila["USUARIO_EMAIL"].ToString();
                string Email2 = Fila["PERSONA_EMAIL"].ToString();
                if (Email2 != null && Email2.Length > 0)
                    Email1 = Email2;

                if (EnviaReporte(0, CeC_Config.ENV_AUT_MAILS_DESDE, CeC_Config.ENV_AUT_MAILS_DESDE.AddDays(CeC_Config.ENV_AUT_MAILS_DIAS), false, Email1, "Reporte Automatico de Asistencia", "Asistencia de Empleados Asignados", Usuario_ID, true))
                    ReportesEnviados++;

            }

            CeC_Config.ENV_AUT_MAILS_DESDE = DateTime.Now;

        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
    }


    private bool EnviaReporte(int Reporte, DateTime FechaI, DateTime FechaF, bool VerDiasNLab, string Mail, string Titulo, string Mensage, int Usuario_ID, bool EsAutomatico)
    {
        try
        {
            CeC_Sesion Sesion = new CeC_Sesion();

            int Perfil_ID = CeC_BD.EjecutaEscalarInt("SELECT PERFIL_ID FROM EC_USUARIOS WHERE USUARIO_ID = " + Usuario_ID);
            string Filtro = "";
            if (Perfil_ID == 1)
                Filtro = Sesion.WF_EmpleadosFil_Qry_Temp = "SELECT PERSONA_ID FROM EC_PERSONAS WHERE PERSONA_BORRADO = 0";
            else
                Filtro = Sesion.WF_EmpleadosFil_Qry_Temp = "SELECT PERSONA_ID FROM EC_PERSONAS WHERE PERSONA_BORRADO = 0 and (EC_PERSONAS.SUSCRIPCION_ID in (Select EC_PERMISOS_SUSCRIP.SUSCRIPCION_ID from EC_PERMISOS_SUSCRIP where EC_PERMISOS_SUSCRIP.usuario_id = " + Usuario_ID + "))";

            /*
            ReportClass ReporteCR = null;
            ReporteCR = CeC_Reportes.GeneraReporte(CeC_Reportes.REPORTE.AsistenciasCC, FechaI, FechaF, Filtro, VerDiasNLab, false, null, EsAutomatico);
            if (ReporteCR != null)
            {
                string Ret = Sesion.GuardaReportePDF(ReporteCR, true);
                if (Ret.Length > 0)
                {
                    return CeC_BD.EnviarMail(Mail, "", Titulo, Mensage, Ret, EsAutomatico);
                }
            }*/
        }
        catch (Exception ex) { }
        return false;

    }
    public static int CuentaMails(string Titulo, DateTime Dia)
    {
        string Qry = "SELECT COUNT(*) FROM EC_MAILS WHERE MAIL_TITULO = '" + CeC_BD.ObtenParametroCadena(Titulo) + "' ";
        if(!CeC_BD.EsOracle)
            Qry += " AND CONVERT(CHAR, MAIL_FECHAHORA, 105) = CONVERT(CHAR, "+CeC_BD.SqlFecha(Dia)+", 105)";
        return CeC_BD.EjecutaEscalarInt(Qry);
    }
    //
    // TODO: Agregar aquí la lógica del constructor
    //
}

