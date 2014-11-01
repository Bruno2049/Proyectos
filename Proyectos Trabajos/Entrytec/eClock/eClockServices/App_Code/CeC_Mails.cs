using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Web.Mail;
using System.Net.Mail;

/// <summary>
/// Descripción breve de CeC_Mails
/// </summary>
public class CeC_Mails
{
    public CeC_Mails()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    private static bool m_ProcesandoMails = false;
    private static Thread m_ThreadMails = null;

    private static bool m_HayMailsPendientes = true;
    /// <summary>
    /// esta funcion se deberá llamar cada que agregen mails
    /// </summary>
    public static void  CambioMailsPendientes()
    {
        m_HayMailsPendientes = true;
    }


    public static void IniciaThread()
    {
        m_ThreadMails = new Thread(new ThreadStart(EnviaMails));
        m_ThreadMails.Start();
    }
    public static bool m_Parar = false;
    public static void ParaThread()
    {
        m_Parar = true;
        if (m_ThreadMails != null)
            m_ThreadMails.Abort();
    }
    private static void EnviaMails()
    {
        m_ProcesandoMails = true;
        m_HayMailsPendientes = true;
        while (!m_Parar)
        {
            try
            {
                if (m_HayMailsPendientes)
                {
                    m_HayMailsPendientes = false;
                    string Usuario = CeC_Config.ServidorSMTPNombreUsuario;
                    System.Net.Mail.SmtpClient Cliente = new System.Net.Mail.SmtpClient(CeC_Config.SevidorSMTP, CeC_Config.SevidorSMTPPuerto);
                    if (Usuario.Length > 0)
                    {
                        Cliente.DeliveryMethod = SmtpDeliveryMethod.Network;
                        Cliente.UseDefaultCredentials = false;
                        Cliente.Credentials = new System.Net.NetworkCredential(Usuario, CeC_Config.ServidorSMTPPass);
                    }
                    DS_MailsTableAdapters.EC_MAILSTableAdapter TAMail = new DS_MailsTableAdapters.EC_MAILSTableAdapter();
                    DS_Mails.EC_MAILSDataTable DT = null;
                    if (CeC_BD.EsOracle)
                        DT = TAMail.GetDataNoEnviadoOracle();
                    else
                        DT = TAMail.GetDataNoEnviado();

                    string Correo = CeC_Config.SevidorCorreo;

                    foreach (DS_Mails.EC_MAILSRow Fila in DT)
                    {
                        try
                        {
                            bool Enviar = true;
                            if (!Fila.IsMAIL_DESTINOSNull())
                            {
                                System.Net.Mail.MailMessage MM = new System.Net.Mail.MailMessage();

                                MM.IsBodyHtml = true;

                                MM.From = new System.Net.Mail.MailAddress(CeC_Config.SevidorCorreo, "eClock");
                                string[] Destinos = Fila.MAIL_DESTINOS.Split(new Char[] { ',', ';' });

                                foreach (string Destino in Destinos)
                                {
                                    try
                                    {
                                        CeT_EmailAddress EAdd = new CeT_EmailAddress(Destino);
                                        if (EAdd.IsValid)
                                            MM.To.Add(Destino);
                                    
                                    }
                                    catch (System.Exception exEM){}
                                }


                                if (MM.To.Count > 0)
                                {
                                    string Mensaje = "";
                                    
                                    if (!Fila.IsMAIL_COPIASNull() && Fila.MAIL_COPIAS != "")
                                        MM.CC.Add(Fila.MAIL_COPIAS);
                                    //                                    MM.Bcc.Add("emails.merck@EntryTec.com.mx");
                                    if (!Fila.IsMAIL_TITULONull() && Fila.MAIL_TITULO != "")
                                        MM.Subject = Fila.MAIL_TITULO;
                                    else
                                        MM.Subject = "[Sin título]";
                                    if (!Fila.IsMAIL_MENSAJENull() && Fila.MAIL_MENSAJE != "")
                                        MM.Body = Mensaje = Fila.MAIL_MENSAJE;
                                    else
                                        MM.Body = "";
                                    //                                    MM.Body += " \n\n" + Compania + "\n" + CompaniaDir + "\n" + CompaniaTelefono + "\n" + CompaniaUrl + "\n";
                                    if (CeC_BD.EsOracle)
                                        Fila.MAIL_ADJUNTO = CeC_BD.ObtenBinario("EC_MAILS", "MAIL_ID", Convert.ToInt32(Fila.MAIL_ID), "MAIL_ADJUNTO");
                                    if (!Fila.IsMAIL_ADJUNTONull() && Fila.MAIL_ADJUNTO != null)
                                    {
                                        if (Mensaje == "EN_ADJUNTO")
                                        {
                                            MM.Body = MM.Body.Replace("EN_ADJUNTO", ObtenString(Fila.MAIL_ADJUNTO, 0, Fila.MAIL_ADJUNTO.Length));
                                        }
                                        else
                                        {
                                            System.IO.MemoryStream MS = new System.IO.MemoryStream(Fila.MAIL_ADJUNTO);
                                            System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(MS, ObtenNombreAdjunto(Fila.MAIL_ADJUNTO));
                                            MM.Attachments.Add(attachment);
                                        }
                                    }

                                    Cliente.Send(MM);

                                }
                                else
                                    CIsLog2.AgregaError("envío de un Mail omitido ID = " + Fila.MAIL_ID + " Destino = " + Fila.MAIL_DESTINOS + " posiblemente el mail este mal escrito");

                            }
                            else
                                CIsLog2.AgregaError("envío de un Mail omitido ID = " + Fila.MAIL_ID + " no hay destinatarios ");

                            Fila.MAIL_ENVIADO = 1;
                            Fila.MAIL_FECHAHORAE = DateTime.Now;
                            TAMail.Enviado(1, DateTime.Now, Fila.MAIL_ID);
                        }
                        catch (SmtpFailedRecipientsException Smtpex)
                        {
                            for (int i = 0; i < Smtpex.InnerExceptions.Length; i++)
                            {

                                SmtpStatusCode status = Smtpex.InnerExceptions[i].StatusCode;
                                if (status == SmtpStatusCode.MailboxUnavailable)
                                {
                                    CIsLog2.AgregaError("envío de un Mail omitido ID = " + Fila.MAIL_ID + " Destino = " + Fila.MAIL_DESTINOS + " la cuenta no existe");
                                    TAMail.Enviado(1, DateTime.Now, Fila.MAIL_ID);
                                    break;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            CIsLog2.AgregaError("envío de un Mail pospuesto ID = " + Fila.MAIL_ID);
                            CIsLog2.AgregaError(ex);
                        }

                    }
                    //TAMail.Update(DT);

                }
                CeC.Sleep(1000);
            }
            catch (Exception ex)
            {
                CIsLog2.AgregaError(ex);
            }
        }
        m_ProcesandoMails = false;
    }
    public static string ObtenString(byte[] Arreglo, int Pos, int Len)
    {
        string Texto = "";
        if (Len < 0)
            Len = 0;
        if (Pos > Arreglo.Length)
            return "";
        if (Pos < 0)
            Pos = 0;

        if (Len <= 0 || Pos + Len > Arreglo.Length)
            Len = Arreglo.Length - Pos;
        byte[] TextoArr = new byte[Len];
        Array.Copy(Arreglo, Pos, TextoArr, 0, Len);
        return System.Text.ASCIIEncoding.ASCII.GetString(TextoArr);

    }
    private static string ObtenNombreAdjunto(byte[] Arreglo)
    {
        if (Arreglo.Length > 10 && Arreglo[1] == 'P' && Arreglo[2] == 'D' && Arreglo[3] == 'F')
        {
            return "Reporte.PDF";
        }
        return "Archivo.txt";

    }



    /// <summary>
    /// Agrega a la base de datos un mail el cual podra ser enviado por otro proceso
    /// </summary>
    /// <param name="Destinatarios">lista de emails separados por coma a quienes se les enviara el mail</param>
    /// <param name="DestinatariosCopia">lista de emails separados por coma a quienes se les enviara copia del mail</param>
    /// <param name="Titulo">Titulo del mail</param>
    /// <param name="Mensaje">Contenido del Mail</param>
    /// <param name="Adjunto">Archivo ajunto en caso de no tener ser'a nulo</param>
    /// <returns>regresa verdadero si se pudo agregar</returns>
    public static bool EnviarMail(string Destinatarios, string DestinatariosCopia, string Titulo, string Mensaje, byte[] Adjunto)
    {
        try
        {
            if (EnviaMensaje("", Destinatarios, DestinatariosCopia, Titulo, Mensaje, 0, Adjunto, null) > 0)
                return true;
        }
        catch (Exception ex) { CIsLog2.AgregaError(ex); }
        return false;
    }
    /// <summary>
    /// Agrega a la base de datos un mail el cual podra ser enviado por otro porceso
    /// </summary>
    /// <param name="Destinatarios">lista de emails separados por coma a quienes se les enviara el mail</param>
    /// <param name="DestinatariosCopia">lista de emails separados por coma a quienes se les enviara copia del mail</param>
    /// <param name="Titulo">Titulo del mail</param>
    /// <param name="Mensaje">Contenido del Mail</param>
    /// <param name="AdjuntoRutaRelativa">Archivo ajunto en caso de no tener ser'a ""</param>
    /// <returns></returns>
    public static bool EnviarMail(string Destinatarios, string DestinatariosCopia, string Titulo, string Mensaje, string AdjuntoRutaRelativa)
    {
        return EnviarMail(Destinatarios, DestinatariosCopia, Titulo, Mensaje, AdjuntoRutaRelativa, false);
    }
    public static bool EnviarMail(string Destinatarios, string DestinatariosCopia, string Titulo, string Mensaje, string AdjuntoRutaRelativa, bool EsAutomatico)
    {
        try
        {
            byte[] Archivo = null;
            if (AdjuntoRutaRelativa != "")
                if (EsAutomatico)
                    Archivo = System.IO.File.ReadAllBytes(/*HttpRuntime.AppDomainAppPath +*/ AdjuntoRutaRelativa);
                else
                    Archivo = System.IO.File.ReadAllBytes(HttpRuntime.AppDomainAppPath + AdjuntoRutaRelativa);
            return EnviarMail(Destinatarios, DestinatariosCopia, Titulo, Mensaje, Archivo);
        }
        catch (Exception ex) { CIsLog2.AgregaError(ex); }
        return false;

    }

    public static bool EnviarMailA(string Destinatarios, string DestinatariosCopia, string Titulo, string Mensaje, string AdjuntoRutaRelativa)
    {
        try
        {
            byte[] Archivo = null;
            if (AdjuntoRutaRelativa != "")
                Archivo = System.IO.File.ReadAllBytes(/*HttpRuntime.AppDomainAppPath +*/ AdjuntoRutaRelativa);
            return EnviarMail(Destinatarios, DestinatariosCopia, Titulo, Mensaje, Archivo);
        }
        catch (Exception ex) { CIsLog2.AgregaError(ex); }
        return false;

    }

    /// <summary>
    /// Agrega a la base de datos un mail el cual podra ser enviado por otro proceso
    /// </summary>
    /// <param name="Destinatarios">lista de emails separados por coma a quienes se les enviara el mail</param>
    /// <param name="Titulo">Titulo del mail</param>
    /// <param name="Mensaje">Contenido del Mail</param>
    /// <returns>regresa verdadero si se pudo agregar</returns>
    public static bool EnviarMail(string Destinatarios, string Titulo, string Mensaje)
    {
        byte[] EnAdjunto = null;
        if (Mensaje.Length > 8000)
        {
            EnAdjunto = CeC.ObtenArregloBytes(Mensaje);
            Mensaje = "EN_ADJUNTO";
        }
        return EnviarMail(Destinatarios, "", Titulo, Mensaje, EnAdjunto);
    }

    public static string ObtenSmssPendientes(int CantidadMaxima, CeC_Sesion Sesion)
    {
        string Qry = "SELECT TOP " + CantidadMaxima + " MAIL_ID,MAIL_DESTINOS,MAIL_COPIAS,MAIL_MENSAJE " +
                "FROM EC_MAILS where MAIL_ENVIADO = 0 AND MAIL_TIPO = 2 ";
        return CeC_BD.DataSet2JsonV2(CeC_BD.EjecutaDataSet(Qry));
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="FechaInicial"></param>
    /// <param name="FechaFinal"></param>
    /// <param name="Origenes"></param>
    /// <param name="Destinos"></param>
    /// <param name="Copias"></param>
    /// <param name="Tipo">0 = eMail; 1 = Mensaje Usuario; 2; SMS Usuario; -1 Todos</param>
    /// <returns></returns>
    public static string ObtenMensajes(string Origenes, string Destinos, string Copias, int Tipo, int MailID_Hasta, int NoElementos, CeC_Sesion Sesion)
    {
        string Filtro = "";
        if (Origenes != null && Origenes != "")
            Filtro = CeC.AgregaSeparador(Filtro, "MAIL_DESDE IN (" + CeC_BD.ObtenParametroCadena(Origenes) + ")", " AND ");
        if (Destinos != null && Destinos != "")
            Filtro = CeC.AgregaSeparador(Filtro, "MAIL_DESTINOS IN (" + CeC_BD.ObtenParametroCadena(Destinos) + ")", " AND ");
        if (Copias != null && Copias != "")
            Filtro = CeC.AgregaSeparador(Filtro, "MAIL_COPIAS IN (" + CeC_BD.ObtenParametroCadena(Copias) + ")", " AND ");
        if (MailID_Hasta > 0)
            Filtro = CeC.AgregaSeparador(Filtro, "MAIL_ID < " + MailID_Hasta + "", " AND ");
        string Qry = "SELECT MAIL_ID, MAIL_DESDE, MAIL_DESTINOS, MAIL_COPIAS, MAIL_TITULO, MAIL_MENSAJE, MAIL_ADJUNTO," +
            " MAIL_ADJUNTO_NOMBRE, MAIL_FECHAHORA, MAIL_ENVIADO, MAIL_FECHAHORAE, MAIL_TIPO FROM EC_MAILS WHERE " + Filtro;
        return CeC_BD.DataSet2JsonV2(CeC_BD.EjecutaDataSet(Qry));
    }
    public static string Homologa(string eMails, int Tipo)
    {
        eMails =  eMails.Replace("\r\n", ";");
        if (Tipo != 0)
            return eMails;
        string[] seMails = CeC.ObtenArregoSeparador(eMails, ";");
        string NuevosMails = "";
        foreach (string eMail in seMails)
        {
            int UsuarioID = CeC.Convierte2Int(eMail);
            if (eMail == UsuarioID.ToString())
            {
                string UsuarioeMail = CeC_Usuarios.ObtenUsuarioeMail(UsuarioID);
                NuevosMails = CeC.AgregaSeparador(NuevosMails, UsuarioeMail, ";");
            }
            else
                NuevosMails = CeC.AgregaSeparador(NuevosMails, eMail, ";");
        }
        return NuevosMails;
    }
    public static int EnviaMensaje(string Desde, string Destinos, string Copias, string Titulo, string Mensaje, int Tipo, byte[] Binario, CeC_Sesion Sesion)
    {
        if ((Destinos == null || Destinos == "") && (Copias == null || Copias == ""))
            return -1;
        int MailID = CeC_Autonumerico.GeneraAutonumerico("EC_MAILS", "MAIL_ID", Sesion);
        Destinos = Homologa(Destinos, Tipo);
        Desde = Homologa(Desde, Tipo);
        Copias = Homologa(Copias, Tipo);

        string Qry = "INSERT INTO EC_MAILS (MAIL_ID, MAIL_DESDE, MAIL_DESTINOS, MAIL_COPIAS, MAIL_TITULO, MAIL_MENSAJE, MAIL_FECHAHORA, MAIL_TIPO) VALUES(" +
            MailID + ", '" + CeC_BD.ObtenParametroCadena(Desde) + "', '" + CeC_BD.ObtenParametroCadena(Destinos) + "', '"
            + CeC_BD.ObtenParametroCadena(Copias) + "', '" + CeC_BD.ObtenParametroCadena(Titulo) + "', '" + CeC_BD.ObtenParametroCadena(Mensaje) +
            "'," + CeC_BD.SqlFechaHora(DateTime.Now) + ", " + Tipo + ")";
        int R = CeC_BD.EjecutaComando(Qry);
        if (R > 0)
        {
            if (Binario != null)
                CeC_BD.AsignaBinario("EC_MAILS", "MAIL_ID =" + MailID, "MAIL_ADJUNTO", Binario);
            if (Tipo == -1)
            {
                int UsuarioIDDesde = CeC.Convierte2Int(Desde);
                string DesdeNombre = CeC_Usuarios.ObtenNombre(UsuarioIDDesde);
                
                ///Envia un email
                EnviaMensaje(CeC_Usuarios.ObtenUsuarioeMail(UsuarioIDDesde),
                    CeC_Usuarios.ObtenUsuarioeMail(CeC.Convierte2Int(Destinos)),
                    CeC_Usuarios.ObtenUsuarioeMail(CeC.Convierte2Int(Copias)),
                    Titulo, Mensaje + CeC.SaltoLineaHtml + DesdeNombre
                    , 0, Binario, Sesion);
                ///Envia un SMS
                EnviaMensaje(CeC_Usuarios.ObtenUsuarioTelefono(UsuarioIDDesde),
                    CeC_Usuarios.ObtenUsuarioTelefono(CeC.Convierte2Int(Destinos)),
                    CeC_Usuarios.ObtenUsuarioTelefono(CeC.Convierte2Int(Copias)),
                    Titulo, Mensaje, 2, Binario, Sesion);

            }
            CambioMailsPendientes();
            return 1;
        }

        return 0;
    }
    public static int EnviaMensajeUsuario(int UsuarioID, bool EsCopia, string Titulo, string Mensaje, byte[] Binario, CeC_Sesion Sesion, int Tipo = -1)
    {

        int UsuarioIDDesde = 0;
        if (Sesion != null)
            UsuarioIDDesde = Sesion.USUARIO_ID;
        string Destino = "";
        string Copia = "";
        if (!EsCopia)
            Destino = UsuarioID.ToString();
        else
            Copia = UsuarioID.ToString();
        int MailID = EnviaMensaje(UsuarioIDDesde.ToString(), Destino, Copia, Titulo, Mensaje, Tipo, Binario, Sesion);

        return MailID;
    }

    public static int UltimoIDBorradoCon(int UsuarioIDCon, CeC_Sesion Sesion)
    {
        return CeC_Config.ObtenConfig(Sesion.USUARIO_ID, "CeC_MailBorradoCon" + UsuarioIDCon, 0);
    }

    public static string UltimosIDBorradosCon(CeC_Sesion Sesion)
    {
        string R = "";
        try
        {
            System.Data.DataSet DS = CeC_BD.EjecutaDataSet("SELECT CONFIG_USUARIO_VALOR FROM EC_CONFIG_USUARIO WHERE USUARIO_ID = " + Sesion.USUARIO_ID + " AND CONFIG_USUARIO_VARIABLE LIKE 'CeC_MailBorradoCon%'");
            if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
                foreach (System.Data.DataRow DR in DS.Tables[0].Rows)
                    R = CeC.AgregaSeparador(R, DR[0].ToString(), ",");

        }
        catch
        {}
        return R;
    }

    public static int BorraMensajesCon(int UsuarioID, CeC_Sesion Sesion)
    {
        int UsuarioIDDesde = 0;
        if (Sesion != null)
            UsuarioIDDesde = Sesion.USUARIO_ID;
        string UsuarioeMailDesde = CeC_Usuarios.ObtenUsuarioeMail(UsuarioIDDesde);
        string UsuarioeMailDestino = CeC_Usuarios.ObtenUsuarioeMail(UsuarioID);
        string In = "'"+UsuarioID + "','" +UsuarioIDDesde+"'";

                
        //string Qry = "UPDATE EC_MAILS SET MAIL_BORRADO_USR = " + UsuarioIDDesde + " WHERE MAIL_DESDE in (" + In + ",'" + UsuarioeMailDesde + "') AND MAIL_DESTINOS IN (" + In + ",'" + UsuarioeMailDestino + "') ";
        string Qry = "SELECT MAX(MAIL_ID) FROM EC_MAILS WHERE MAIL_DESDE in (" + In + ") AND MAIL_DESTINOS IN (" + In + ") ";
        if (UsuarioID != UsuarioIDDesde)
            Qry += " AND MAIL_DESDE<>MAIL_DESTINOS";
        int MailID  = CeC_BD.EjecutaEscalarInt(Qry);
        CeC_Config.GuardaConfig(Sesion.USUARIO_ID, "CeC_MailBorradoCon" + UsuarioID, MailID);
        return MailID;
    }

    public static int EnviaMensajeUsuario(string UsuariosIds, string UsuariosIdsCopias, string Titulo, string Mensaje, byte[] Binario, CeC_Sesion Sesion)
    {
        string[] sUsuariosIds = CeC.ObtenArregoSeparador(UsuariosIds, ",");
        string[] sUsuariosIdsCopias = CeC.ObtenArregoSeparador(UsuariosIdsCopias, ",");
        int Enviados = 0;
        int Errores = 0;
        foreach (string sUsuarioID in sUsuariosIds)
        {
            int UsuarioID = CeC.Convierte2Int(sUsuarioID);
            if (EnviaMensajeUsuario(UsuarioID, false, Titulo, Mensaje, Binario, Sesion) > 0)
                Enviados++;
            else
                Errores++;
        }

        foreach (string sUsuarioID in sUsuariosIdsCopias)
        {
            int UsuarioID = CeC.Convierte2Int(sUsuarioID);
            if (EnviaMensajeUsuario(UsuarioID, true, Titulo, Mensaje, Binario, Sesion) > 0)
                Enviados++;
            else
                Errores++;
        }
        return Enviados;

    }

    public static int ObtenNoMensajes(DateTime FechaDesde, int Tipo, CeC_Sesion Sesion)
    {
        return CeC_BD.EjecutaEscalarInt("SELECT COUNT(*) FROM EC_V_CHATS WHERE CHAT = '" + Sesion.USUARIO_ID + "' AND MAIL_TIPO=" + Tipo + " AND MAIL_FECHAHORA > " + CeC_BD.SqlFechaHora(FechaDesde));
    }
}