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
/// Descripción breve de CeC_Localizaciones
/// </summary>
public class CeC_Localizaciones : CeC_Tabla
{
    int m_Localizacion_Id = 0;
    [Description("Identificador unico de la tabla")]
    [DisplayNameAttribute("Localizacion_Id")]
    public int Localizacion_Id { get { return m_Localizacion_Id; } set { m_Localizacion_Id = value; } }
    string m_Localizacion_Idioma = "es-MX";
    [Description("Código ISO 639-1 del lenguaje a mostrar")]
    [DisplayNameAttribute("Localizacion_Idioma")]
    public string Localizacion_Idioma { get { return m_Localizacion_Idioma; } set { m_Localizacion_Idioma = value; } }
    string m_Localizacion_Llave = "";
    [Description("Texto en el idioma adecuado que se mostrara en el comando, botón o elemento")]
    [DisplayNameAttribute("Localizacion_Llave")]
    public string Localizacion_Llave { get { return m_Localizacion_Llave; } set { m_Localizacion_Llave = value; } }
    string m_Localizacion_Etiqueta = "";
    [Description("Texto en el idioma adecuado que se mostrara en la etiqueta")]
    [DisplayNameAttribute("Localizacion_Etiqueta")]
    public string Localizacion_Etiqueta { get { return m_Localizacion_Etiqueta; } set { m_Localizacion_Etiqueta = value; } }
    string m_Localizacion_Descripcion = "";
    [Description("Descripción del elemento mostrado")]
    [DisplayNameAttribute("Localizacion_Descripcion")]
    public string Localizacion_Descripcion { get { return m_Localizacion_Descripcion; } set { m_Localizacion_Descripcion = value; } }
    string m_Localizacion_Ayuda = "";
    [Description("Ayuda de como usar el comando, campo o elemento")]
    [DisplayNameAttribute("Localizacion_Ayuda")]
    public string Localizacion_Ayuda { get { return m_Localizacion_Ayuda; } set { m_Localizacion_Ayuda = value; } }
    string m_Localizacion_Imagen = "";
    [Description("Ruta relativa de la imagen que se mostrara en pantalla")]
    [DisplayNameAttribute("Localizacion_Imagen")]
    public string Localizacion_Imagen { get { return m_Localizacion_Imagen; } set { m_Localizacion_Imagen = value; } }
    string m_Localizacion_Altmenu = "";
    [Description("Acceso directo para los menus y funciones usando la tecla ALT")]
    [DisplayNameAttribute("Localizacion_Altmenu")]
    public string Localizacion_Altmenu { get { return m_Localizacion_Altmenu; } set { m_Localizacion_Altmenu = value; } }
    string m_Localizacion_Html = "";
    [Description("Ruta al archivo HTML que contiene el texto, solo valido para ayudas o textos grandes.")]
    [DisplayNameAttribute("Localizacion_Html")]
    public string Localizacion_Html { get { return m_Localizacion_Html; } set { m_Localizacion_Html = value; } }

    public CeC_Localizaciones(CeC_Sesion Sesion)
        : base("EC_LOCALIZACIONES", "LOCALIZACION_ID", Sesion)
    {

    }
    public CeC_Localizaciones(int LocalizacionID, CeC_Sesion Sesion)
        : base("EC_LOCALIZACIONES", "LOCALIZACION_ID", Sesion)
    {
        Carga(LocalizacionID.ToString(), Sesion);
    }


    public bool Actualiza(int LocalizacionId, string LocalizacionIdioma, string LocalizacionLlave, string LocalizacionEtiqueta, string LocalizacionDescripcion, string LocalizacionAyuda, string LocalizacionImagen, string LocalizacionAltmenu, string LocalizacionHtml,
CeC_Sesion Sesion)
    {
        try
        {
            bool Nuevo = false;
            if (!Carga(LocalizacionId.ToString(), Sesion))
                Nuevo = true;
            m_EsNuevo = Nuevo;
            Localizacion_Id = LocalizacionId; Localizacion_Idioma = LocalizacionIdioma; Localizacion_Llave = LocalizacionLlave; Localizacion_Etiqueta = LocalizacionEtiqueta; Localizacion_Descripcion = LocalizacionDescripcion; Localizacion_Ayuda = LocalizacionAyuda; Localizacion_Imagen = LocalizacionImagen; Localizacion_Altmenu = LocalizacionAltmenu; Localizacion_Html = LocalizacionHtml;

            if (Guarda(Sesion))
            {
                return true;
            }
        }
        catch { }
        return false;
    }

    public static int ObtenLocalizacionID(string LocalizacionIdioma, string LocalizacionLlave)
    {
        string Qry = "SELECT LOCALIZACION_ID FROM EC_LOCALIZACIONES WHERE LOCALIZACION_IDIOMA = '" + LocalizacionIdioma + "' AND LOCALIZACION_LLAVE = @LOCALIZACION_LLAVE@";
        return CeC_BD.EjecutaEscalarInt(CeC_BD.AsignaParametro(Qry, "LOCALIZACION_LLAVE", LocalizacionLlave));
    }
    public static string ObtenVistaLocalizacion(string LocalizacionIdioma, int Suscripcion, bool UsarNeutral)
    {
        string[] Idiomas = CeC.ObtenArregoSeparador(LocalizacionIdioma, "-");
        string Loc = "";
        string QryIdioma = "LOCALIZACION_IDIOMA = 'Z." + Suscripcion + "' ";
        foreach (string Idioma in Idiomas)
        {
            Loc = CeC.AgregaSeparador(Loc, Idioma, "-");
            QryIdioma = CeC.AgregaSeparador(QryIdioma, "LOCALIZACION_IDIOMA = '" + Loc + "' ", " OR ");
        }
        if (UsarNeutral)
            QryIdioma = CeC.AgregaSeparador(QryIdioma, "LOCALIZACION_IDIOMA = '' ", " OR ");
        string Qry = "SELECT LOCALIZACION_LLAVE, MAX(LOCALIZACION_IDIOMA) AS LOCALIZACION_IDIOMA FROM EC_LOCALIZACIONES WHERE (" + QryIdioma + ")  GROUP BY LOCALIZACION_LLAVE";
        Qry = "(SELECT EC_LOCALIZACIONES.* FROM EC_LOCALIZACIONES, (" + Qry + ") AS T WHERE EC_LOCALIZACIONES.LOCALIZACION_LLAVE = T.LOCALIZACION_LLAVE AND EC_LOCALIZACIONES.LOCALIZACION_IDIOMA = T.LOCALIZACION_IDIOMA) AS EC_LOCALIZACIONES";
        return Qry;

    }

    public static DataSet ObtenEtiquetasAyuda(string LocalizacionIdioma, int Suscripcion, bool UsarNeutral)
    {
        string Qry = "SELECT LOCALIZACION_LLAVE,LOCALIZACION_ETIQUETA,LOCALIZACION_AYUDA FROM " + ObtenVistaLocalizacion(LocalizacionIdioma, Suscripcion, UsarNeutral) + " ";
        return CeC_BD.EjecutaDataSet(Qry);

    }

    public static int ActualizaEtiqueta(int LocalizacionID, string Etiqueta)
    {
        return CeC_BD.EjecutaComando("UPDATE EC_LOCALIZACIONES SET LOCALIZACION_ETIQUETA = '" + CeC_BD.ObtenParametroCadena(Etiqueta) + "' WHERE LOCALIZACION_ID = " + LocalizacionID);
    }
    /// <summary>
    /// Obtiene la lozalizacion de la etiqueta
    /// </summary>
    /// <param name="LocalizacionIdioma"></param>
    /// <param name="LocalizacionLlave"></param>
    /// <returns></returns>
    public static string ObtenLocalizacionEtiqueta(string LocalizacionIdioma, string LocalizacionLlave)
    {
        return ObtenLocalizacionEtiqueta(LocalizacionIdioma, LocalizacionLlave, LocalizacionLlave);
    }
    /// <summary>
    /// Obtiene la localizacion de la etiqueta con respecto ala llave de localizacion.
    /// </summary>
    /// <param name="LocalizacionIdioma"></param>
    /// <param name="LocalizacionLlave"></param>
    /// <param name="TextoPredeterminado"></param>
    /// <param name="AutoGuardar"></param>
    /// <returns></returns>
    public static string ObtenLocalizacionEtiqueta(string LocalizacionIdioma, string LocalizacionLlave, string TextoPredeterminado, bool AutoGuardar = false)
    {
        string Qry = "SELECT LOCALIZACION_ETIQUETA FROM " + ObtenVistaLocalizacion(LocalizacionIdioma, 0, !AutoGuardar) + " WHERE LOCALIZACION_LLAVE = @LOCALIZACION_LLAVE@";
        string Etiqueta = CeC_BD.EjecutaEscalarString(CeC_BD.AsignaParametro(Qry, "LOCALIZACION_LLAVE", LocalizacionLlave));
        if (Etiqueta == "")
        {
            Etiqueta = TextoPredeterminado;
            if (AutoGuardar)
            {
                int LocalizacionID = ObtenLocalizacionID("", LocalizacionLlave);
                if (LocalizacionID <= 0)
                    AgregaLocalizacion("", LocalizacionLlave, TextoPredeterminado, null);
                else
                    ActualizaEtiqueta(LocalizacionID, TextoPredeterminado);
            }
        }
        return Etiqueta;

    }
    public static string ObtenLocalizacionAyuda(string LocalizacionIdioma, string LocalizacionLlave)
    {
        string Qry = "SELECT LOCALIZACION_AYUDA FROM " + ObtenVistaLocalizacion(LocalizacionIdioma, 0, true) + " WHERE LOCALIZACION_LLAVE = @LOCALIZACION_LLAVE@";
        //        string Qry = "SELECT LOCALIZACION_AYUDA FROM EC_LOCALIZACIONES WHERE LOCALIZACION_IDIOMA = '" + LocalizacionIdioma + "' AND LOCALIZACION_LLAVE = @LOCALIZACION_LLAVE@";
        return CeC_BD.EjecutaEscalarString(CeC_BD.AsignaParametro(Qry, "LOCALIZACION_LLAVE", LocalizacionLlave));
    }
    public static CeC_Localizaciones ObtenLocalizacion(string Formulario, string Control, string Tabla, string CampoNombre, CeC_Sesion Sesion)
    {
        if (Sesion.Localizacion == "")
        {
            CeC_Localizaciones SinLoc = new CeC_Localizaciones(Sesion);
            SinLoc.Localizacion_Idioma = Sesion.Localizacion;
            SinLoc.Localizacion_Llave = CampoNombre;
            SinLoc.Localizacion_Etiqueta = "[" + Control + "]";
            SinLoc.Localizacion_Ayuda = "[" + Control + "->" + CampoNombre + "]";
            return SinLoc;
        }

        string Qry = "SELECT LOCALIZACION_ID FROM EC_LOCALIZACIONES WHERE LOCALIZACION_IDIOMA = '" + Sesion.Localizacion + "' AND LOCALIZACION_LLAVE = @LOCALIZACION_LLAVE@";
        int LocalizacionID = ObtenLocalizacionID(Sesion.Localizacion, Formulario + "." + Control);
        if (LocalizacionID <= 0 && Tabla != "")
            LocalizacionID = ObtenLocalizacionID(Sesion.Localizacion, Tabla + "." + CampoNombre);
        if (LocalizacionID <= 0)
            LocalizacionID = ObtenLocalizacionID(Sesion.Localizacion, CampoNombre);
        if (LocalizacionID > 0)
            return new CeC_Localizaciones(LocalizacionID, Sesion);
        CeC_Localizaciones Loc = new CeC_Localizaciones(Sesion);
        Loc.Localizacion_Idioma = Sesion.Localizacion;
        Loc.Localizacion_Llave = CampoNombre;
        Loc.Localizacion_Etiqueta = CeC_Campos.ObtenEtiqueta(CampoNombre, "");
        Loc.Localizacion_Ayuda = CeC_TablasCampos.ObtenCampoAyuda(Tabla, CampoNombre);
        return Loc;
    }
    public static int AgregaLocalizacion(string LocalizacionIdioma, string LocalizacionLlave, string LocalizacionEtiqueta,
CeC_Sesion Sesion)
    {
        return AgregaLocalizacion(LocalizacionIdioma, LocalizacionLlave, LocalizacionEtiqueta, "", "", "", "", "", Sesion);
    }
    public static int AgregaLocalizacion(string LocalizacionIdioma, string LocalizacionLlave, string LocalizacionEtiqueta, string LocalizacionDescripcion, string LocalizacionAyuda, string LocalizacionImagen, string LocalizacionAltmenu, string LocalizacionHtml,
CeC_Sesion Sesion)
    {
        CeC_Localizaciones Loc = new CeC_Localizaciones(Sesion);
        if (Loc.Actualiza(-1, LocalizacionIdioma, LocalizacionLlave, LocalizacionEtiqueta, LocalizacionDescripcion, LocalizacionAyuda, LocalizacionImagen, LocalizacionAltmenu, LocalizacionHtml, Sesion))
            return Loc.Localizacion_Id;
        return -1;
    }

    public static string ObtenLlave(string Codigo, CeC_Sesion Sesion)
    {
        return Sesion.m_PaginaWeb.AppRelativeVirtualPath + "." + Codigo;
    }
    /// <summary>
    /// Obtiene un texto validando la localizacion
    /// </summary>
    /// <param name="Sesion">Sesion de uso</param>
    /// <param name="Codigo"></param>
    /// <returns>regresa la etiqueta previamente agregada o la llave en caso de no tener ninguna configurada</returns>
    public static string ObtenMensaje(CeC_Sesion Sesion, string Codigo)
    {
        string Llave = ObtenLlave(Codigo, Sesion);
        if (Sesion.Localizacion == "")
            return Llave;

        string Msg = ObtenLocalizacionEtiqueta(Sesion.Localizacion, Llave);
        if (Msg.Length > 0)
            return Msg;
        return Llave;
    }
    /// <summary>
    /// Agrega un menseje a la tabla de localizaciónes
    /// </summary>
    /// <param name="Sesion"></param>
    /// <param name="Codigo"></param>
    /// <param name="Mensaje"></param>
    /// <returns></returns>
    public bool AgregaMenseje(CeC_Sesion Sesion, string Codigo, string Mensaje)
    {
        string Llave = ObtenLlave(Codigo, Sesion);
        if (Sesion.Localizacion == "")
            return false;
        if (ObtenLocalizacionID(Sesion.Localizacion, Llave) > 0)
            return true;
        if (AgregaLocalizacion(Sesion.Localizacion, Llave, Mensaje, Sesion) > 0)
            return true;
        return false;
    }

}