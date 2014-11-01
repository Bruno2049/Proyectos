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
                Loc.ObtenTextoEvent += delegate(object Destino, string Texto)
                {
                    CheckBox CbxFinal = ((CheckBox)Destino);
                    CbxFinal.Content = Texto;
                };
                Loc.ObtenEtiqueta(Padre + "." + Cbx.Name, Etiqueta, Cbx);
                return true;
            }
            catch { }
            return false;
        }

        public static bool sLocaliza(string Padre, TextBlock lbl)
        {
            try
            {
                Localizaciones Loc = new Localizaciones(CeC_Sesion.ObtenSesion(lbl));
                Loc.ObtenTextoEvent += delegate(object Destino, string Texto)
                {
                    TextBlock lblFinal = ((TextBlock)Destino);
                    lblFinal.Text = Texto;
                };
                Loc.ObtenEtiqueta(Padre + "." + lbl.Name, lbl.Text, lbl);
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
                Loc.ObtenTextoEvent += delegate(object Destino, string Texto)
                {
                    TreeViewItem TVIFinal = ((TreeViewItem)Destino);
                    TVIFinal.Header = Texto;
                };
                Loc.ObtenEtiqueta(Padre + "." + TVI.Name, TVI.Header.ToString(), TVI);
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
                Loc.ObtenTextoEvent += delegate(object Destino, string Texto)
                {
                    Button BtnFinal = ((Button)Destino);
                    BtnFinal.Content = Texto;
                    //lblFinal.
                };
                Loc.ObtenEtiqueta(Padre + "." + Btn.Name, Etiqueta, Btn);
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
                Loc.ObtenTextoEvent += delegate(object Destino, string Texto)
                {
                    ToggleButton BtnFinal = ((ToggleButton)Destino);
                    BtnFinal.Content = Texto;
                    //lblFinal.
                };
                Loc.ObtenEtiqueta(Padre + "." + Btn.Name, Etiqueta, Btn);
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
                    try
                    {
                        string Tipo = Elemento.GetType().ToString();
                        switch (Elemento.GetType().Name)
                        {
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
                            case "UC_Wizard":
                                //((Controles.UC_Wizard)Elemento).Localiza();
                                break;
                            default:
                                sLocaliza(Padre, (Panel)Elemento);
                                break;
                        }

                    }
                    catch { }
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
                    CeC_Sesion.ObtenSesion(Ventana).IDIOMA = Idioma;
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
                    CeC_Sesion.ObtenSesion(Ventana).IDIOMA = Idioma;
                if (Ventana.Content != null)
                    return sLocaliza(Ventana.GetType().FullName, (Panel)Ventana.Content);
            }
            catch
            { }
            return false;
        }


    }
}
