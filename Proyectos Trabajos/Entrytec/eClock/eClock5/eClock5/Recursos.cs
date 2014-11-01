using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#if !NETFX_CORE
using System.Windows.Media;
using System.Windows.Media.Imaging;
#else
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
#endif
using System.IO;

namespace eClock5
{
    class Recursos
    {
        public static Brush AzulOscuro_Brush = null;
        public static Brush AzulClaro_Brush = null;
        public static Brush AzulMuyClaro_Brush = null;
        public static Brush GrisOscuro_Brush = null;
        public static Brush GrisMedio_Brush = null;
        public static Brush GrisClaro_Brush = null;
        public static Brush Blanco_Brush = null;
        public static Brush Negro_Brush = null;
        public static Brush FlechaArriba_Brush = null;
        public static FontFamily EntryTecFont = null;
        public static Double AltoToolBar = 0;
        
        public static Double FontSizeParrafo = 0;
        public static Double FontSizeSubTitulo = 0;
        public static Double FontSizeTitulo = 0;

       
        public static bool Inicia(System.Windows.Window Ventana)
        {
            AzulOscuro_Brush = (Brush)Ventana.FindResource("AzulOscuro_Brush");
            AzulClaro_Brush = (Brush)Ventana.FindResource("AzulClaro_Brush");
            AzulMuyClaro_Brush = (Brush)Ventana.FindResource("AzulMuyClaro_Brush");
            GrisOscuro_Brush = (Brush)Ventana.FindResource("GrisOscuro_Brush");
            GrisMedio_Brush = (Brush)Ventana.FindResource("GrisMedio_Brush");
            GrisClaro_Brush = (Brush)Ventana.FindResource("GrisClaro_Brush");
            Blanco_Brush = (Brush)Ventana.FindResource("Blanco_Brush");
            Negro_Brush = (Brush)Ventana.FindResource("Negro_Brush");
            FlechaArriba_Brush = (Brush)Ventana.FindResource("FlechaArriba_Brush");
            EntryTecFont = (FontFamily)Ventana.FindResource("EntryTecFont");
            AltoToolBar = (Double)Ventana.FindResource("AltoToolBar");
            FontSizeParrafo = (Double)Ventana.FindResource("FontSizeParrafo");
            FontSizeSubTitulo = (Double)Ventana.FindResource("FontSizeSubTitulo");
            FontSizeTitulo = (Double)Ventana.FindResource("FontSizeTitulo");
            

            return true;
        }
    }
}
