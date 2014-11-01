using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using Newtonsoft.Json;

[ServiceContract(Namespace = "")]
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
public class S_Mails
{
    // Para usar HTTP GET, agregue el atributo [WebGet]. (El valor predeterminado de ResponseFormat es WebMessageFormat.Json)
    // Para crear una operación que devuelva XML,
    //     agregue [WebGet(ResponseFormat=WebMessageFormat.Xml)]
    //     e incluya la siguiente línea en el cuerpo de la operación:
    //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
    [OperationContract]
    public void DoWork()
    {
        // Agregue aquí la implementación de la operación
        return;
    }


    [OperationContract]
    public string ObtenChats(string SesionSeguridad, string Hash)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return null;
        string Borrados = CeC_Mails.UltimosIDBorradosCon(Sesion);
        if (Borrados == "")
            Borrados = "0";
        string JSon = CeC_Tablas.ObtenListadoJson(Sesion.SUSCRIPCION_ID, "EC_V_CHATS_ULTIMOS", "USUARIO_ID", "MAIL_MENSAJE", "MAIL_FECHAHORA",
         "USUARIO_NOMBRE", "", false, "CHAT = '" + Sesion.USUARIO_ID + "' AND MAIL_ID NOT IN (" + Borrados + ")");
        if (Hash != null && Hash != "")
            if (MD5Core.GetHashString(JSon) == Hash)
                return "==";
        return JSon;
    }

    [OperationContract]
    public string ObtenChatsCon(string SesionSeguridad, int UsuarioIDCon, string Hash)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return null;

        int MailID = CeC_Mails.UltimoIDBorradoCon(UsuarioIDCon, Sesion);
        string JSon = CeC_Tablas.ObtenListadoJson(Sesion.SUSCRIPCION_ID, "EC_V_CHATS", "MAIL_ID", "USUARIO_NOMBRE", "MAIL_FECHAHORA",
         "MAIL_MENSAJE", "", false, "CHAT = '" + Sesion.USUARIO_ID + "' AND CHAT2 ='" + UsuarioIDCon + "' AND MAIL_ID > " + MailID);
        if (Hash != null && Hash != "")
            if (MD5Core.GetHashString(JSon) == Hash)
                return "==";
        return JSon;
    }

    [OperationContract]
    public string ObtenChatsConDesde(string SesionSeguridad, int UsuarioIDCon, int MailID)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return null;
        int MailIDBorrado = CeC_Mails.UltimoIDBorradoCon(UsuarioIDCon, Sesion);
        if (MailID < MailIDBorrado)
            MailID = MailIDBorrado;
        string JSon = CeC_Tablas.ObtenListadoJson(Sesion.SUSCRIPCION_ID, "EC_V_CHATS", "MAIL_ID", "USUARIO_NOMBRE", "MAIL_FECHAHORA",
         "MAIL_MENSAJE", "", false, "CHAT = '" + Sesion.USUARIO_ID + "' AND CHAT2 ='" + UsuarioIDCon + "' AND MAIL_ID > " + MailID);
        return JSon;
    }


    [OperationContract]
    public string ObtenContactos(string SesionSeguridad, string Hash)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return null;
        string UsuariosIDS = "";
        if (Sesion.PERSONA_ID > 0)
        {
            UsuariosIDS = CeC.AgregaSeparador(UsuariosIDS, CeC_Usuarios.ObtenEmpleados(Sesion.USUARIO_ID), ",");
            UsuariosIDS = CeC.AgregaSeparador(UsuariosIDS, CeC_Usuarios.ObtenJefes(Sesion.PERSONA_ID), ",");
            UsuariosIDS = CeC.AgregaSeparador(UsuariosIDS, CeC_Usuarios.ObtenCompaneros(Sesion.PERSONA_ID), ",");
            UsuariosIDS = "USUARIO_ID IN (" + UsuariosIDS + ")";
        }
        string JSon = CeC_Tablas.ObtenListadoJson(Sesion.SUSCRIPCION_ID, "EC_USUARIOS", "USUARIO_ID", "USUARIO_NOMBRE", "",
         "USUARIO_EMAIL", "", false, UsuariosIDS);
        if (Hash != null && Hash != "")
            if (MD5Core.GetHashString(JSon) == Hash)
                return "==";
        return JSon;
    }


    [OperationContract]
    public bool EnviaMensaje(string SesionSeguridad, int UsuarioIdDEstino, string Mensaje, string MsgBinario)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return false;
        byte[] Binario = null;
        if (MsgBinario != null && MsgBinario != "")
            Binario = JsonConvert.DeserializeObject<byte[]>(MsgBinario);
        if (CeC_Mails.EnviaMensajeUsuario(UsuarioIdDEstino, false, "", Mensaje, Binario, Sesion) > 0)
            return true;
        return false;
    }

    [OperationContract]
    public int BorraMensajesCon(string SesionSeguridad, int UsuarioIdCon)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return -1;
        
        return CeC_Mails.BorraMensajesCon(UsuarioIdCon, Sesion);
    }
    // Agregue aquí más operaciones y márquelas con [OperationContract]
}
