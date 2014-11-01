using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#if !NETFX_CORE
using System.Windows.Controls;
using System.Windows;
#else
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
#endif

namespace eClock5.Clases
{
    class Parametros
    {
        public bool MostrarRegresar = false;
        public object Parametro = "";
        public UC_Listado Listado = null;
        public Parametros()
        {
        }
        public Parametros(bool bMostrarRegresar, object sParametro)
        {
            MostrarRegresar = bMostrarRegresar;
            Parametro = sParametro;
        }
        public Parametros(bool bMostrarRegresar, object sParametro, UC_Listado lListado)
        {
            MostrarRegresar = bMostrarRegresar;
            Parametro = sParametro;
            Listado = lListado;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Tag"></param>
        /// <returns></returns>
        public static Parametros Tag2Parametros(object Tag)
        {

            if (Tag != null)
            {
                try
                {
                    return (Parametros)Tag;
                }
                catch { }
            }
            return new Parametros();
        }
        public static object AgregaParametro(object Tag, string Variable, object Valor)
        {
            Parametros Param = Tag2Parametros(Tag);
            string Parametro = Param.Parametro.ToString();
            string Nvalor = "\"" + Variable + "\": \"" + Valor.ToString() + "\"";
            if (Parametro.Length > 0)
            {
                int Pos = Parametro.LastIndexOf("}");
                Parametro = Parametro.Substring(0, Pos);
                Parametro += "," + Nvalor + "}";
            }
            else
            {
                Parametro = "{" + Nvalor + "}";
            }
            Param.Parametro = Parametro;
            return Param;
        }

        public static FrameworkElement ObtenPadre(UserControl Actual)
        {
            try
            {
                if (Actual == null || Actual.Parent == null)
                    return null;
                DependencyObject ucParent = Actual.Parent;

                while (!(ucParent is UserControl))
                {
                    ucParent = LogicalTreeHelper.GetParent(ucParent);
                }
                if (ucParent == null)
                    return null;
                return ((FrameworkElement)ucParent);
            }
            catch (Exception ex)
            {
            }
            return null;
        }
        public static object ObtenTagPadre(UserControl Actual)
        {

            FrameworkElement ucParent = ObtenPadre(Actual);
            if (ucParent == null)
                return null;
            return ucParent.Tag;
        }
        public static Parametros ObtenParametrosPadre(UserControl Actual)
        {
            return ObtenTagPadre(Actual) as Parametros;
        }

        public static bool MuestraControl(Grid GridContenedor, UserControl Control, Parametros Param)
        {
            //GridContenedor.Children.Clear();
            Control.Tag = Param;
            GridContenedor.Children.Add(Control);
            if (!Control.IsLoaded)
                BaseModificada.Localizaciones.sLocaliza(Control);
            return true;
        }
    }
}
