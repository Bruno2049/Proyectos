using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#if !NETFX_CORE
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;
#else
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Controls.Primitives;
#endif
namespace eClock5.Controles
{
    /// <summary>
    /// Lógica de interacción para UC_Color.xaml
    /// </summary>
    public partial class UC_Color : UserControl
    {
        public UC_Color()
        {
            InitializeComponent();
        }
        public Brush Fondo {get;set;}

        public int Color
        {
            get { return eClockBase.CeC.Convierte2Int(GetValue(ColorProperty)); }
            set
            {
                SetValue(ColorProperty, value);

            }
        }

        // Using a DependencyProperty as the backing store for Apps.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register("Color", typeof(int), typeof(UC_Color),
            new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnColorChanged
    )));
        private static void OnColorChanged(DependencyObject obj,
    DependencyPropertyChangedEventArgs args)
        {
            if (args.OldValue != args.NewValue)
            {
                //return;
                UC_Color control = (UC_Color)obj;
                if (control.Fondo == null)
                    control.Fondo = control.Background;

                control.Background = ObtenColor(control.Fondo, eClockBase.CeC.Convierte2Int(args.NewValue));
            }
        }
        

        public static Brush ObtenColor(Brush ColorFondo, int iColor)
        {

            DrawingBrush myBrush = new DrawingBrush();
            int B_MASK = 255;
            int G_MASK = 255 << 8; //65280 
            int R_MASK = 255 << 16; //16711680


            int r = (iColor & R_MASK) >> 16;
            int g = (iColor & G_MASK) >> 8;
            int b = iColor & B_MASK;

            SolidColorBrush SCB = new SolidColorBrush(System.Windows.Media.Color.FromRgb((byte)r, (byte)g, (byte)b));
            //return SCB;
            GeometryDrawing backgroundSquare =
                new GeometryDrawing(
                    ColorFondo,
                    null,
                    new RectangleGeometry(new Rect(0, 0, 1, 1)));
            GeometryDrawing ColorGroup =
                new GeometryDrawing(
                    SCB,
                    null,
                    new RectangleGeometry(new Rect(0, 0, 1, 1)));


            DrawingGroup checkersDrawingGroup = new DrawingGroup();
            checkersDrawingGroup.Children.Add(backgroundSquare);
            checkersDrawingGroup.Children.Add(ColorGroup);

            myBrush.Drawing = checkersDrawingGroup;
            myBrush.TileMode = TileMode.FlipX;
            myBrush.ViewportUnits = BrushMappingMode.Absolute;
            myBrush.Viewport = new Rect(0, 0, 100, 1);
            return myBrush;

        }
    }
}
