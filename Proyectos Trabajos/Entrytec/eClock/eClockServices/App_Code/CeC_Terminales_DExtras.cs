using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
/// <summary>
/// Descripción breve de CeC_Terminales_DExtras
/// </summary>
public class CeC_Terminales_DExtras
{
    public CeC_Terminales_DExtras()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public enum Tipo_Term_DEXTRAS
    {
        Conexion_Correcta = 101,
        Error_Conexion,
        ComunicacionCorrecta,
        Error_Comunicacion,
        Log_Comunicacion,
        FechaHora_Enviada,
        FechaHora_Error,
        Checadas_Descargadas,
        Checadas_Error,
        Vectores_Descargados,
        Vectores_Error_Desc,
        Vectores_Enviados,
        Vectores_Error_Env
    }
    public static byte[] ObtenBin(int Terminal_ID, int TipoTermDExtras)
    {
        int IDExtras = CeC_BD.EjecutaEscalarInt("SELECT TERMINALES_DEXTRAS_ID FROM EC_TERMINALES_DEXTRAS WHERE TIPO_TERM_DEXTRAS_ID = " + TipoTermDExtras + " AND TERMINAL_ID = " + Terminal_ID + "ORDER BY TERMINALES_DEXTRAS_ID DESC");
        if (IDExtras <= 0)
            return null;
        byte[] Bytes = CeC_BD.ObtenBinario("EC_TERMINALES_DEXTRAS", "TERMINALES_DEXTRAS_ID", IDExtras, "TERMINALES_DEXTRAS_BIN");
        return Bytes;
    }
    /// <summary>
    /// Borra los datos de EC_TERMINALES_DEXTRAS.
    /// </summary>
    /// <param name="Terminal_ID">ID de la Terminal.</param>
    /// <param name="TipoTermDExtras">Parámetro usado para borrar solo ese tipo de registros de la terminal, se obtiene de EC_TIPO_TERM_DEXTRAS.</param>
    /// <returns>Verdadero en caso de que se borren los registro. Falso en caso de error.</returns>
    public static bool BorrarDExtras(int Terminal_ID, int TipoTermDExtras)
    {
        // Se borran los datos binarios de EC_TERMINALES_DEXTRAS
        int IDExtras = CeC_BD.EjecutaEscalarInt("DELETE EC_TERMINALES_DEXTRAS WHERE TIPO_TERM_DEXTRAS_ID = " + TipoTermDExtras + " AND TERMINAL_ID = " + Terminal_ID);
        if (IDExtras <= 0)
            return false;
        return true;
    }
    public static string ObtenLog(int Terminal_ID)
    {
        int IDLog = CeC_BD.EjecutaEscalarInt("SELECT TERMINALES_DEXTRAS_ID FROM EC_TERMINALES_DEXTRAS WHERE TIPO_TERM_DEXTRAS_ID = " + Convert.ToInt32(Tipo_Term_DEXTRAS.Log_Comunicacion) + " AND TERMINAL_ID = " + Terminal_ID);
        if (IDLog <= 0)
            return "";
        byte[] Bytes = CeC_BD.ObtenBinario("EC_TERMINALES_DEXTRAS", "TERMINALES_DEXTRAS_ID", IDLog, "TERMINALES_DEXTRAS_BIN");
        return CeC.ObtenString(Bytes);
    }
    public static string ObtenLogHtml(int Terminal_ID)
    {
        string Log = ObtenLog(Terminal_ID);
        return Log.Replace("\n", "<br>");
    }
    public static bool Borra(int TERMINALES_DEXTRAS_ID)
    {
        CeC_BD.EjecutaComando("DELETE EC_TERMINALES_DEXTRAS WHERE TERMINALES_DEXTRAS_ID = " + TERMINALES_DEXTRAS_ID);
        return true;
    }

    public static bool PasaARespaldo(int TerminalDExtraID)
    {
        int R = CeC_BD.EjecutaComando("INSERT INTO EC_TERMINALES_DEXTRAS_BAK (TERMINALES_DEXTRAS_ID, TERMINAL_ID, TIPO_TERM_DEXTRAS_ID, TERMINALES_DEXTRAS_TEXTO1) " +
            "SELECT TERMINALES_DEXTRAS_ID, TERMINAL_ID, TIPO_TERM_DEXTRAS_ID, TERMINALES_DEXTRAS_TEXTO1 FROM EC_TERMINALES_DEXTRAS WHERE TERMINALES_DEXTRAS_ID = " + TerminalDExtraID);
        if (R <= 0)
            return false;
        CeC_Terminales_DExtras.Borra(TerminalDExtraID);
        return true;
    }

    public static bool AgregaDExtra(int Terminal_ID, int TipoTermDExtras, int SesionID, int SuscripcionID, string Texto1, string Texto2, byte[] Bin)
    {

        int TerminalDExtraID = CeC_Autonumerico.GeneraAutonumerico("EC_TERMINALES_DEXTRAS", "TERMINALES_DEXTRAS_ID");
        int R = CeC_BD.EjecutaComando("INSERT INTO EC_TERMINALES_DEXTRAS (TERMINALES_DEXTRAS_ID, TERMINAL_ID, TIPO_TERM_DEXTRAS_ID, TERMINALES_DEXTRAS_TEXTO1,TERMINALES_DEXTRAS_TEXTO2) VALUES(" +
            TerminalDExtraID + "," + Terminal_ID + "," + TipoTermDExtras + ",'" + Texto1 + "','" + Texto2 + "')");

        if (R <= 0)
        {
            CIsLog2.AgregaError("AgregaTerminalesDExtras No se pudo insertar el registro (int TerminalID, Tipo_DEXTRAS Tipo, string Texto1, string Texto2) values(" +
                Terminal_ID + ", " + TipoTermDExtras.ToString() + ", " + Texto1 + ", " + Texto2 + ")");

            return false;
        }

        if (R > 0 && Bin != null && Bin.Length > 0)
            CeC_BD.AsignaBinario("EC_TERMINALES_DEXTRAS", "TERMINALES_DEXTRAS_ID", TerminalDExtraID, "TERMINALES_DEXTRAS_BIN", Bin);
        if (TipoTermDExtras >= Convert.ToInt32(Tipo_Term_DEXTRAS.ComunicacionCorrecta))
        {
            Tipo_Term_DEXTRAS Tipo = (Tipo_Term_DEXTRAS)TipoTermDExtras;
            CeC_Terminales.ActualizaCom(Terminal_ID, Tipo);
            if (Tipo == Tipo_Term_DEXTRAS.Error_Comunicacion || Tipo == Tipo_Term_DEXTRAS.Error_Conexion
                || Tipo == Tipo_Term_DEXTRAS.FechaHora_Error || Tipo == Tipo_Term_DEXTRAS.Checadas_Error
                || Tipo == Tipo_Term_DEXTRAS.Vectores_Error_Desc || Tipo == Tipo_Term_DEXTRAS.Vectores_Error_Env)
            {
                CMd_Mails Mails = new CMd_Mails();
                DataSet DS = Mails.ObtenUsuariosTerminalNoResponde(SuscripcionID);
                if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
                {
                    string Texto = "";
                    string Link = "";
                    string Titulo = "";
                    switch (Tipo)
                    {
                        case Tipo_Term_DEXTRAS.Error_Comunicacion:
                            Titulo = "Error de Comunicación";
                            Link = "https://sites.google.com/a/entrytec.com.mx/ayuda-eclock/tips/error-de-comunicacion";
                            break;
                        case Tipo_Term_DEXTRAS.Error_Conexion:
                            Titulo = "Error de Conexión";
                            Link = "https://sites.google.com/a/entrytec.com.mx/ayuda-eclock/tips/terminal-no-responde";
                            break;
                        case Tipo_Term_DEXTRAS.FechaHora_Error:
                            Titulo = "Error al transmitir la fecha y hora";
                            Link = "https://sites.google.com/a/entrytec.com.mx/ayuda-eclock/tips/error-de-comunicacion";
                            break;
                        case Tipo_Term_DEXTRAS.Checadas_Error:
                            Titulo = "Error al polear checadas";
                            Link = "https://sites.google.com/a/entrytec.com.mx/ayuda-eclock/tips/error-de-comunicacion";
                            break;
                        case Tipo_Term_DEXTRAS.Vectores_Error_Desc:
                            Titulo = "Error al descargar los vectores del enrolador";
                            Link = "https://sites.google.com/a/entrytec.com.mx/ayuda-eclock/tips/error-de-comunicacion";
                            break;
                        case Tipo_Term_DEXTRAS.Vectores_Error_Env:
                            Titulo = "Error al enviar los vectores al checador";
                            Link = "https://sites.google.com/a/entrytec.com.mx/ayuda-eclock/tips/error-de-comunicacion";
                            break;
                    }
                    CeC_Terminales Terminal = new CeC_Terminales(Terminal_ID, null);
                    Titulo = Terminal.Terminal_Nombre + "(" + Titulo + ")";
                    if (CMd_Mails.CuentaMails(Titulo, DateTime.Now) < 3)
                    {
                        Texto = "Apreciable <USUARIO_NOMBRE><br>";
                        Texto += "Le informamos se encontró un " + Titulo + "<br>";
                        Texto += "con el equipo " + Terminal.Terminal_Nombre + "(TerminaID = " + Terminal_ID + ",  Dirección" + Terminal.Terminal_Dir + ")" + "<br>";
                        Texto += "le recomendamos ver el siguiente link para obtener más información<br>";
                        Texto += "<a href=\"" + Link + "\">" + Link + "</a><br><br>";
                        //Texto += "Historico <br>" + Extras + "</a>";                
                        Mails.EnviaMails(DS, Titulo, Texto, Bin);
                    }
                }
            }

        }

        if (R > 0)
            return true;
        return false;
    }

    public static bool AgregaDExtra(int Terminal_ID, Tipo_Term_DEXTRAS Tipo, int SesionID, int SuscripcionID, string Extras)
    {
        return AgregaDExtra(Terminal_ID, CeC.Convierte2Int(Tipo), SesionID, SuscripcionID, DateTime.Now.ToString(), "", CeC.ObtenArregloBytes(Extras));
    }

}