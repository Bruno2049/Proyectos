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
    public static string ObtenLocalizacionEtiqueta(string LocalizacionIdioma, string LocalizacionLlave)
    {
        string Qry = "SELECT LOCALIZACION_ETIQUETA FROM EC_LOCALIZACIONES WHERE LOCALIZACION_IDIOMA = '" + LocalizacionIdioma + "' AND LOCALIZACION_LLAVE = @LOCALIZACION_LLAVE@";
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
        if (Loc.Actualiza(0, LocalizacionIdioma, LocalizacionLlave, LocalizacionEtiqueta, LocalizacionDescripcion, LocalizacionAyuda, LocalizacionImagen, LocalizacionAltmenu, LocalizacionHtml, Sesion))
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
    public static bool AplicaLocalizacion(System.Web.UI.Control Contr, CeC_Sesion Sesion)
    {
        return AplicaLocalizacion(Contr, "", Sesion);
    }
    public static bool AplicaLocalizacion(System.Web.UI.Control Contr, string UsarNombre, CeC_Sesion Sesion)
    {
        try
        {
            if (UsarNombre != "")

                foreach (System.Web.UI.Control NContr in Contr.Controls)
                {
                    AplicaLocalizacion(NContr, Sesion);
                }

            string TipoControl = Contr.GetType().ToString();

            if (TipoControl == "System.Web.UI.LiteralControl"
                || TipoControl == "Infragistics.WebUI.UltraWebNavigator.UltraWebTree"
                || TipoControl == "Infragistics.WebUI.UltraWebListbar.TemplateGroup"
                || TipoControl == "Infragistics.Web.UI.TemplateContainer"
                || TipoControl == "Infragistics.Web.UI.LayoutControls.SplitterPane"
                || TipoControl == "Infragistics.WebUI.UltraWebTab.ContentPane"
                || TipoControl == "System.Web.UI.ResourceBasedLiteralControl"
                || TipoControl == "Infragistics.WebUI.UltraWebGrid.UltraWebGrid"

                )
                return false;

            string Nombre = Contr.ClientID;
            if (UsarNombre != "")
                Nombre = UsarNombre;
            int PosSeparador = Nombre.IndexOf("_");
            if (PosSeparador <= 0)
                return false;

            string Tipo = Nombre.Substring(0, PosSeparador);
            string CampoNombre = Nombre.Substring(PosSeparador + 1, Nombre.Length - 1 - PosSeparador);

            CeC_Localizaciones Loc = ObtenLocalizacion(Sesion.m_PaginaWeb.ClientID, Nombre, "", CampoNombre, Sesion);


            switch (TipoControl)
            {
                case "Infragistics.WebUI.WebDataInput.WebImageButton":
                    {
                        Infragistics.WebUI.WebDataInput.WebImageButton Obj = (Infragistics.WebUI.WebDataInput.WebImageButton)Contr;
                        if (Loc.Localizacion_Etiqueta.Length > 0)
                            Obj.Text = Loc.Localizacion_Etiqueta;
                        if (Loc.Localizacion_Ayuda.Length > 0)
                            Obj.ToolTip = Loc.Localizacion_Ayuda;
                    }
                    break;
                case "Infragistics.WebUI.WebDataInput.WebTextEdit":
                    {
                        Infragistics.WebUI.WebDataInput.WebTextEdit Obj = (Infragistics.WebUI.WebDataInput.WebTextEdit)Contr;
                        if (Loc.Localizacion_Ayuda.Length > 0)
                            Obj.ToolTip = Loc.Localizacion_Ayuda;
                    }
                    break;
                case "System.Web.UI.WebControls.TextBox":
                    {
                        System.Web.UI.WebControls.TextBox Obj = (System.Web.UI.WebControls.TextBox)Contr;
                        if (Loc.Localizacion_Ayuda.Length > 0)
                            Obj.ToolTip = Loc.Localizacion_Ayuda;
                    }
                    break;
                case "Infragistics.Web.UI.EditorControls.WebNumericEditor":
                    {
                        Infragistics.Web.UI.EditorControls.WebNumericEditor Obj = (Infragistics.Web.UI.EditorControls.WebNumericEditor)Contr;
                        if (Loc.Localizacion_Ayuda.Length > 0)
                            Obj.ToolTip = Loc.Localizacion_Ayuda;
                    }
                    break;
                case "Infragistics.WebUI.WebDataInput.WebNumericEdit":
                    {
                        Infragistics.WebUI.WebDataInput.WebNumericEdit Obj = (Infragistics.WebUI.WebDataInput.WebNumericEdit)Contr;
                        if (Loc.Localizacion_Ayuda.Length > 0)
                            Obj.ToolTip = Loc.Localizacion_Ayuda;
                    }
                    break;
                case "Infragistics.WebUI.WebCombo.WebCombo":
                    {
                        Infragistics.WebUI.WebCombo.WebCombo Obj = (Infragistics.WebUI.WebCombo.WebCombo)Contr;
                        if (Loc.Localizacion_Ayuda.Length > 0)
                            Obj.ToolTip = Loc.Localizacion_Ayuda;
                    }
                    break;
                case "System.Web.UI.WebControls.Label":
                    {
                        System.Web.UI.WebControls.Label Obj = (System.Web.UI.WebControls.Label)Contr;
                        if (Loc.Localizacion_Etiqueta.Length > 0)
                            Obj.Text = Loc.Localizacion_Etiqueta;
                        if (Loc.Localizacion_Ayuda.Length > 0)
                            Obj.ToolTip = Loc.Localizacion_Ayuda;
                    }
                    break;
                case "System.Web.UI.WebControls.Panel":
                    {
                        System.Web.UI.WebControls.Panel Obj = (System.Web.UI.WebControls.Panel)Contr;
                        if (Loc.Localizacion_Ayuda.Length > 0)
                            Obj.ToolTip = Loc.Localizacion_Ayuda;
                    }
                    break;
                case "Infragistics.WebUI.UltraWebTab.UltraWebTab":
                    {
                        Infragistics.WebUI.UltraWebTab.UltraWebTab Obj = (Infragistics.WebUI.UltraWebTab.UltraWebTab)Contr;
                        if (Loc.Localizacion_Ayuda.Length > 0)
                            Obj.ToolTip = Loc.Localizacion_Ayuda;
                    }
                    break;
                case "Infragistics.WebUI.WebSchedule.WebCalendar":
                    {
                        Infragistics.WebUI.WebSchedule.WebCalendar Obj = (Infragistics.WebUI.WebSchedule.WebCalendar)Contr;
                        if (Loc.Localizacion_Ayuda.Length > 0)
                            Obj.ToolTip = Loc.Localizacion_Ayuda;
                    }
                    break;
                case "Infragistics.WebUI.WebSchedule.WebDateChooser":
                    {
                        Infragistics.WebUI.WebSchedule.WebDateChooser Obj = (Infragistics.WebUI.WebSchedule.WebDateChooser)Contr;
                        if (Loc.Localizacion_Ayuda.Length > 0)
                            Obj.ToolTip = Loc.Localizacion_Ayuda;
                    }
                    break;
                case "System.Web.UI.WebControls.HyperLink":
                    {
                        System.Web.UI.WebControls.HyperLink Obj = (System.Web.UI.WebControls.HyperLink)Contr;
                        if (Loc.Localizacion_Etiqueta.Length > 0)
                            Obj.Text = Loc.Localizacion_Etiqueta;
                        if (Loc.Localizacion_Ayuda.Length > 0)
                            Obj.ToolTip = Loc.Localizacion_Ayuda;
                    }
                    break;
                case "Infragistics.WebUI.WebDropDown.DropDownPanel":
                    {
                        Infragistics.WebUI.WebDropDown.DropDownPanel Obj = (Infragistics.WebUI.WebDropDown.DropDownPanel)Contr;
                        if (Loc.Localizacion_Etiqueta.Length > 0)
                            Obj.GroupingText = Loc.Localizacion_Etiqueta;
                        if (Loc.Localizacion_Ayuda.Length > 0)
                            Obj.ToolTip = Loc.Localizacion_Ayuda;
                    }
                    break;
                case "Infragistics.Web.UI.LayoutControls.WebDialogWindow":
                    {
                        Infragistics.Web.UI.LayoutControls.WebDialogWindow Obj = (Infragistics.Web.UI.LayoutControls.WebDialogWindow)Contr;
                        if (Loc.Localizacion_Etiqueta.Length > 0)
                            Obj.Header.CaptionText = Loc.Localizacion_Etiqueta;
                        if (Loc.Localizacion_Ayuda.Length > 0)
                            Obj.ToolTip = Loc.Localizacion_Ayuda;
                    }
                    break;
                case "Infragistics.Web.UI.LayoutControls.DialogWindowHeader":
                    {
                        Infragistics.Web.UI.LayoutControls.DialogWindowHeader Obj = (Infragistics.Web.UI.LayoutControls.DialogWindowHeader)Contr;
                        if (Loc.Localizacion_Etiqueta.Length > 0)
                            Obj.CaptionText = Loc.Localizacion_Etiqueta;
                        if (Loc.Localizacion_Ayuda.Length > 0)
                            Obj.ToolTip = Loc.Localizacion_Ayuda;
                    }
                    break;
                case "Infragistics.Web.UI.LayoutControls.DialogWindowContentPane":
                    {
                        Infragistics.Web.UI.LayoutControls.DialogWindowContentPane Obj = (Infragistics.Web.UI.LayoutControls.DialogWindowContentPane)Contr;
                    }
                    break;
                case "System.Web.UI.WebControls.RequiredFieldValidator":
                    {
                        System.Web.UI.WebControls.RequiredFieldValidator Obj = (System.Web.UI.WebControls.RequiredFieldValidator)Contr;
                        if (Loc.Localizacion_Etiqueta.Length > 0)
                            Obj.Text = Loc.Localizacion_Etiqueta;
                        if (Loc.Localizacion_Ayuda.Length > 0)
                            Obj.ToolTip = Loc.Localizacion_Ayuda;
                    }
                    break;
                case "Infragistics.WebUI.WebDataInput.WebDateTimeEdit":
                    {
                        Infragistics.WebUI.WebDataInput.WebDateTimeEdit Obj = (Infragistics.WebUI.WebDataInput.WebDateTimeEdit)Contr;
                        if (Loc.Localizacion_Ayuda.Length > 0)
                            Obj.ToolTip = Loc.Localizacion_Ayuda;
                    }
                    break;
                case "System.Web.UI.WebControls.CheckBox":
                    {
                        System.Web.UI.WebControls.CheckBox Obj = (System.Web.UI.WebControls.CheckBox)Contr;
                        if (Loc.Localizacion_Etiqueta.Length > 0)
                            Obj.Text = Loc.Localizacion_Etiqueta;
                        if (Loc.Localizacion_Ayuda.Length > 0)
                            Obj.ToolTip = Loc.Localizacion_Ayuda;
                    }
                    break;
                case "System.Web.UI.HtmlControls.HtmlInputFile":
                    {
                        System.Web.UI.HtmlControls.HtmlInputFile Obj = (System.Web.UI.HtmlControls.HtmlInputFile)Contr;
                    }
                    break;
                case "System.Web.UI.WebControls.Image":
                    {
                        System.Web.UI.WebControls.Image Obj = (System.Web.UI.WebControls.Image)Contr;
                        if (Loc.Localizacion_Imagen.Length > 0)
                            Obj.ImageUrl = Loc.Localizacion_Imagen;
                        if (Loc.Localizacion_Ayuda.Length > 0)
                            Obj.ToolTip = Loc.Localizacion_Ayuda;
                    }
                    break;
                case "System.Web.UI.WebControls.LinkButton":
                    {
                        System.Web.UI.WebControls.LinkButton Obj = (System.Web.UI.WebControls.LinkButton)Contr;
                        if (Loc.Localizacion_Etiqueta.Length > 0)
                            Obj.Text = Loc.Localizacion_Etiqueta;
                        if (Loc.Localizacion_Ayuda.Length > 0)
                            Obj.ToolTip = Loc.Localizacion_Ayuda;
                    }
                    break;
                case "Infragistics.WebUI.UltraWebListbar.UltraWebListbar":
                    {
                        Infragistics.WebUI.UltraWebListbar.UltraWebListbar Obj = (Infragistics.WebUI.UltraWebListbar.UltraWebListbar)Contr;

                    }
                    break;
                default:
                    CIsLog2.AgregaError("Localizacion Objeto " + Contr.GetType().ToString());
                    break;
            }


            //Contr
            return true;
        }
        catch { }
        return false;
    }
    public static bool AplicaLocalizacion(CeC_Sesion Sesion)
    {
        try
        {
            foreach (System.Web.UI.Control Contr in Sesion.m_PaginaWeb.Controls)
            {
                AplicaLocalizacion(Contr, Sesion);
            }
            return true;
        }
        catch { }
        return false;
    }
}