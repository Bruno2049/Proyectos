using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#if !NETFX_CORE
using System.Windows.Controls;
using System.Windows;
using System.Windows.Controls.Primitives;
#else
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
#endif
namespace eClock5.BaseModificada
{
    class Localizaciones : eClockBase.Controladores.Localizaciones
    {
        public static string Dinamico = "_DIN";
        public Localizaciones(eClockBase.CeC_SesionBase SesionBase)
            : base(SesionBase)
        {
        }
        public static bool sLocaliza(string Padre, Label lbl)
        {
            try
            {
                string Etiqueta = lbl.Content as string;
                Localizaciones Loc = new Localizaciones(CeC_Sesion.ObtenSesion(lbl));
                Loc.ObtenTextoEvent += delegate(object Destino, string Texto)
                {
                    Label lblFinal = ((Label)Destino);
                    lblFinal.Content = Texto;

                };
                Loc.ObtenEtiqueta(Padre + "." + lbl.Name, Etiqueta, lbl);
                return true;
            }
            catch { }
            return false;
        }

        public static bool sLocaliza(string Padre, CheckBox Cbx)
        {
            try
            {
                string Etiqueta = Cbx.Content as string;
                Localizaciones Loc = new Localizaciones(CeC_Sesion.ObtenSesion(Cbx));
                string Nombre = Padre + "." + Cbx.Name;

                if (Cbx.Name.Contains(Dinamico))
                {
                    Nombre = Padre + "." + Cbx.Name.Replace(Dinamico, Texto2Nombre(Cbx.Content));
                }

                Loc.ObtenTextoEvent += delegate(object Destino, string Texto)
                {
                    CheckBox CbxFinal = ((CheckBox)Destino);
                    CbxFinal.Content = Texto;
#if NOMBRE_EN_TOOLTIP
                    CbxFinal.ToolTip = Nombre;
                    CbxFinal.MouseRightButtonDown += delegate(object sender, System.Windows.Input.MouseButtonEventArgs e)
                    {
                        Vista.Localizaciones.Traducciones.CargaTraduccion(Nombre);
                    };
#endif
                };
                Loc.ObtenEtiqueta(Nombre, Etiqueta, Cbx);
                return true;
            }
            catch { }
            return false;        
        }

        public static string Texto2Nombre(object Contenido)
        {
            string NuevoTexto = "";
            try
            {
                NuevoTexto = Contenido.ToString();
            }
            catch { }
            return Texto2Nombre(NuevoTexto);
        }
        public static string Texto2Nombre(string Texto)
        {
            string NuevoTexto = "";
            try
            {
                NuevoTexto = Texto .Replace(" ","_");
            }
            catch { }
            return NuevoTexto;
        }
        public static bool sLocaliza(string Padre, TextBlock lbl)
        {
            try
            {
                Localizaciones Loc = new Localizaciones(CeC_Sesion.ObtenSesion(lbl));
                string Nombre = Padre + "." + lbl.Name;

                if (lbl.Name.Contains(Dinamico))
                {
                    Nombre = Padre + "." + lbl.Name.Replace(Dinamico, Texto2Nombre(lbl.Text));
                }

                Loc.ObtenTextoEvent += delegate(object Destino, string Texto)
                {
                    TextBlock lblFinal = ((TextBlock)Destino);
                    lblFinal.Text = Texto;

#if NOMBRE_EN_TOOLTIP
                    lblFinal.ToolTip = Nombre;
                    lblFinal.MouseRightButtonDown += delegate(object sender, System.Windows.Input.MouseButtonEventArgs e)
                        {
                            Vista.Localizaciones.Traducciones.CargaTraduccion(Nombre);
                        };
#endif
                };
                Loc.ObtenEtiqueta(Nombre, lbl.Text, lbl);
                return true;
            }
            catch { }
            return false;
        }

        public static bool sLocaliza(string Padre, TreeViewItem TVI)
        {
            try
            {
                Localizaciones Loc = new Localizaciones(CeC_Sesion.ObtenSesion(TVI));
                string Nombre = Padre + "." + TVI.Name;

                Loc.ObtenTextoEvent += delegate(object Destino, string Texto)
                {
                    TreeViewItem TVIFinal = ((TreeViewItem)Destino);
                    TVIFinal.Header = Texto;
#if NOMBRE_EN_TOOLTIP
                    TVIFinal.ToolTip = Nombre;
                    TVIFinal.MouseRightButtonDown += delegate(object sender, System.Windows.Input.MouseButtonEventArgs e)
                    {
                        Vista.Localizaciones.Traducciones.CargaTraduccion(Nombre);
                    };
#endif

                };
                Loc.ObtenEtiqueta(Nombre, TVI.Header.ToString(), TVI);
                return true;
            }
            catch { }
            return false;
        }

        public static bool sLocaliza(string Padre, Button Btn)
        {
            try
            {
                string Etiqueta = Btn.Content as string;
                if (Etiqueta == null || Etiqueta == "")
                    return true;
                Localizaciones Loc = new Localizaciones(CeC_Sesion.ObtenSesion(Btn));
                string Nombre = Padre + "." + Btn.Name;
                Loc.ObtenTextoEvent += delegate(object Destino, string Texto)
                {
                    Button BtnFinal = ((Button)Destino);
                    BtnFinal.Content = Texto;
#if NOMBRE_EN_TOOLTIP
                    BtnFinal.ToolTip = Nombre;
                    BtnFinal.MouseRightButtonDown += delegate(object sender, System.Windows.Input.MouseButtonEventArgs e)
                    {
                        Vista.Localizaciones.Traducciones.CargaTraduccion(Nombre);
                    };
#endif

                };
                Loc.ObtenEtiqueta(Nombre, Etiqueta, Btn);
                return true;
            }
            catch { }
            return false;
        }
        public static bool sLocaliza(string Padre, ToggleButton Btn)
        {
            try
            {
                string Etiqueta = Btn.Content as string;
                if (Etiqueta == null || Etiqueta == "")
                    return true;
                Localizaciones Loc = new Localizaciones(CeC_Sesion.ObtenSesion(Btn));
                string Nombre = Padre + "." + Btn.Name;
                Loc.ObtenTextoEvent += delegate(object Destino, string Texto)
                {
                    ToggleButton BtnFinal = ((ToggleButton)Destino);
                    BtnFinal.Content = Texto;
#if NOMBRE_EN_TOOLTIP
                    BtnFinal.ToolTip = Nombre;
                    BtnFinal.MouseRightButtonDown += delegate(object sender, System.Windows.Input.MouseButtonEventArgs e)
                    {
                        Vista.Localizaciones.Traducciones.CargaTraduccion(Nombre);
                    };
#endif

                    //lblFinal.
                };
                Loc.ObtenEtiqueta(Nombre, Etiqueta, Btn);
                return true;
            }
            catch { }
            return false;
        }
        public static bool sLocaliza(string Padre, RadioButton Btn)
        {
            try
            {
                string Etiqueta = Btn.Content as string;
                if (Etiqueta == null || Etiqueta == "")
                    return true;
                Localizaciones Loc = new Localizaciones(CeC_Sesion.ObtenSesion(Btn));
                string Nombre = Padre + "." + Btn.Name;
                Loc.ObtenTextoEvent += delegate(object Destino, string Texto)
                {
                    RadioButton BtnFinal = ((RadioButton)Destino);
                    BtnFinal.Content = Texto;
#if NOMBRE_EN_TOOLTIP
                    BtnFinal.ToolTip = Nombre;
                    BtnFinal.MouseRightButtonDown += delegate(object sender, System.Windows.Input.MouseButtonEventArgs e)
                    {
                        Vista.Localizaciones.Traducciones.CargaTraduccion(Nombre);
                    };
#endif

                    //lblFinal.
                };
                Loc.ObtenEtiqueta(Nombre, Etiqueta, Btn);
                return true;
            }
            catch { }
            return false;
        }
        public static bool sLocaliza(string Padre, object Elemento)
        {
            try
            {

                switch (Elemento.GetType().Name)
                {
                    case "UC_Horas":
                        break;
                    case "TextBox":
                        break;
                    case "TextBlock":                        
                        sLocaliza(Padre, (TextBlock)Elemento);
                        break;
                    case "Label":
                        sLocaliza(Padre, (Label)Elemento);
                        break;
                    case "CheckBox":
                        sLocaliza(Padre, (CheckBox)Elemento);
                        break;

                    case "Button":
                        sLocaliza(Padre, (Button)Elemento);
                        break;
                    case "ToggleButton":
                        sLocaliza(Padre, (ToggleButton)Elemento);
                        break;
                    case "RadioButton":
                        sLocaliza(Padre, (RadioButton)Elemento);
                        break;                        
                    case "UC_Wizard":
                        ((Controles.UC_Wizard)Elemento).Localiza();
                        break;
                    case "UC_EdicionDePersonas":
                        break;
                    case "UC_Campos":
                        ((UC_Campos)Elemento).Localiza();
                        break;
                    case "UC_TurnoDia":
                        ((Vista.Turnos.UC_TurnoDia)Elemento).Localiza();
                        break;                        
                    case "ScrollViewer":
                        sLocaliza(Padre, ((ScrollViewer)Elemento).Content);
                        break;
                    case "UC_Acordion":
                        ((UC_Acordion)Elemento).Localiza();
                        break;
                    default:
                        sLocaliza(Padre, (Panel)Elemento);
                        break;
                }
                return true;

            }
            catch { }
            return false;

        }
        public static bool sLocaliza(string Padre, Panel Pnl)
        {
            try
            {
                foreach (UIElement Elemento in Pnl.Children)
                {
                    sLocaliza(Padre, Elemento);
                }
                return true;
            }
            catch { }
            return false;
        }

        public static bool sLocaliza(UserControl Ventana, string Idioma = "")
        {
            try
            {
                if (Idioma != "")
                {
                    eClockBase.CeC_SesionBase mSesion = CeC_Sesion.ObtenSesion(Ventana);
                    mSesion.IDIOMA = Idioma;
                    eClockBase.Controladores.Localizaciones.sObtenEtiquetasAyuda(mSesion);
                }
                if (Ventana.Content != null)
                    return sLocaliza(Ventana.GetType().FullName, (Panel)Ventana.Content);
            }
            catch
            { }
            return false;
        }
        public static bool sLocaliza(Window Ventana, string Idioma = "")
        {
            try
            {
                if (Idioma != "")
                {
                   eClockBase.CeC_SesionBase mSesion =  CeC_Sesion.ObtenSesion(Ventana);
                   mSesion.IDIOMA = Idioma;
                   eClockBase.Controladores.Localizaciones.sObtenEtiquetasAyuda(mSesion);
                }
                if (Ventana.Content != null)
                    return sLocaliza(Ventana.GetType().FullName, (Panel)Ventana.Content);
            }
            catch
            { }
            return false;
        }


    }
}
