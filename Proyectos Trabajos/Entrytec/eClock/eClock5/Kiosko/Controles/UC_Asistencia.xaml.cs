using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

using System.Windows.Controls;

namespace Kiosko.Controles
{
    /// <summary>
    /// Lógica de interacción para UC_Asistencia.xaml
    /// </summary>
    public partial class UC_Asistencia : UserControl
    {
        int m_AnchoControl = 0;

        public int AnchoControl
        {
            get { return m_AnchoControl; }
            set
            {
                m_AnchoControl = value;

            }
        }

        List<ToggleButton> m_Btns_Dias = new List<ToggleButton>();

        public UC_Asistencia()
        {
            InitializeComponent();
        }

        Brush ObtenColor(Brush ColorFondo, int iColor)
        {

            DrawingBrush myBrush = new DrawingBrush();
            int B_MASK = 255;
            int G_MASK = 255 << 8; //65280 
            int R_MASK = 255 << 16; //16711680


            int r = (iColor & R_MASK) >> 16;
            int g = (iColor & G_MASK) >> 8;
            int b = iColor & B_MASK;

            SolidColorBrush SCB = new SolidColorBrush(Color.FromRgb((byte)r, (byte)g, (byte)b));
            //return SCB;
            GeometryDrawing backgroundSquare =
                new GeometryDrawing(
                    ColorFondo,
                    null,
                    new RectangleGeometry(new Rect(0, 0, 0, 1)));
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

        public bool Modelo2Vista(List<eClockBase.Modelos.Asistencias.Model_Asistencia_Lineal_N> Asistencia)
        {
            bool MostrarHorasExtras = false;
            bool MostrarComidas = false;

            foreach (eClockBase.Modelos.Asistencias.Model_Asistencia_Lineal_N Objeto in Asistencia)
            {
                if (Objeto.PHEX)
                    MostrarHorasExtras = true;
                if (Objeto.PCOMI)
                    MostrarComidas = true;
            }

            m_AnchoControl = 0;
            xGridUC_Asistencia.RowDefinitions.Clear();
            xGridUC_Asistencia.ColumnDefinitions.Clear();
            RowDefinition FilaTitulos = new RowDefinition();
            xGridUC_Asistencia.RowDefinitions.Add(FilaTitulos);


            //NuevaColumna("Seleccionado", 10, "");
            NuevaColumna("Color", 10, "");
            NuevaColumna("Fecha", 140, "Fecha");
            NuevaColumna("Entrada", 80, "Entrada");
            if (MostrarComidas)
            {
                NuevaColumna("CS", 80, "Salida a comer");
                NuevaColumna("CR", 80, "Regreso de comer");
            }
            NuevaColumna("Salida", 80, "Salida");
            NuevaColumna("Turnos", 150, "Turno");
            NuevaColumna("Incidencias", 250, "Incidencia");
            //NuevaColumna("Abreviaturas", 80, "Abreviaturas");
            if (MostrarComidas)
                NuevaColumna("COMI", 250, "Comida Incidencia");
            NuevaColumna("TT", 80, "Trabajado");
            NuevaColumna("TDE", 80, "Retardo");
            NuevaColumna("TE", 80, "Estancia");
            if (MostrarComidas)
                NuevaColumna("TC", 80, "Comida");
            if (MostrarHorasExtras)
            {
                NuevaColumna("Hes", 80, "Extra Trabajado");
                NuevaColumna("Hec", 80, "Extra Calculado");
                NuevaColumna("HEA", 80, "Autorizadas");
                NuevaColumna("HED", 80, "Dobles");
                NuevaColumna("HET", 80, "Triples");

            }
            int NoDia = 0;



            foreach (eClockBase.Modelos.Asistencias.Model_Asistencia_Lineal_N Objeto in Asistencia)
            {
                RowDefinition Fila = new RowDefinition();

                xGridUC_Asistencia.RowDefinitions.Add(Fila);

                int NoFila = xGridUC_Asistencia.RowDefinitions.IndexOf(Fila);
                Objeto.SELECCIONADO = false;

                TextBlock TB_Fecha = NuevaEtiqueta(Objeto.FECHA.ToShortDateString(), NoFila, "Fecha");
                //TB_Fecha.Background =
                NuevaEtiqueta("", NoFila, "Color").Background = ObtenColor(TB_Fecha.Background, Objeto.IC);
                NuevaEtiqueta(validaFecha(Objeto.E), NoFila, "Entrada");
                //NuevaEtiqueta(Objeto.E+"", NoFila, "Entrada");
                if (MostrarComidas)
                {

                    NuevaEtiqueta(validaFecha(Objeto.CS), NoFila, "CS");
                    NuevaEtiqueta(validaFecha(Objeto.CR), NoFila, "CR");
                }
                NuevaEtiqueta(validaFecha(Objeto.S), NoFila, "Salida");
                NuevaEtiqueta(Objeto.TURNO, NoFila, "Turnos");
                NuevaEtiqueta(Objeto.INC, NoFila, "Incidencias").FontSize = Recursos.FontSizeSubTitle;
                NuevaEtiqueta(Objeto.INC, NoFila, "Incidencias").Background = ObtenColor(TB_Fecha.Background, Objeto.IC);
                if (MostrarComidas)
                    NuevaEtiqueta(Objeto.COMI, NoFila, "COMI").FontSize = Recursos.FontSizeSubTitle;
                NuevaEtiqueta(validaFecha(Objeto.TT), NoFila, "TT");
                NuevaEtiqueta(validaFecha(Objeto.TDE), NoFila, "TDE");
                NuevaEtiqueta(validaFecha(Objeto.TE), NoFila, "TE");
                NuevaEtiqueta(validaFecha(Objeto.TC), NoFila, "TC");

                if (MostrarHorasExtras)
                {
                    NuevaEtiqueta(validaFecha(Objeto.HES), NoFila, "Hes");
                    NuevaEtiqueta(validaFecha(Objeto.HEC), NoFila, "Hec");
                    NuevaEtiqueta(validaFecha(Objeto.HEA), NoFila, "HEA");
                    NuevaEtiqueta(validaFecha(Objeto.HED), NoFila, "HED");
                    NuevaEtiqueta(validaFecha(Objeto.HET), NoFila, "HET");
                }

            }
            FilaTitulos = new RowDefinition();
            xGridUC_Asistencia.RowDefinitions.Add(FilaTitulos);
            return true;
        }
        public TextBlock NuevaEtiqueta(string Texto, int Fila, string NombreColumna)
        {
            int NoColumna = ObtenColumnaNO(NombreColumna);
            bool EsPar = false;
            if (Fila++ % 2 == 0)
                EsPar = true;
            TextBlock Etiqueta = new TextBlock();
            Etiqueta.Margin = new Thickness(1);
            Texto.Trim();
            Etiqueta.Text = Texto;
            //if (Texto != "")
            //    string [] valor = Texto.Split(' ');
            //Etiqueta.Name = "Tbk_" + Texto.Split(' ');
            Etiqueta.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            Etiqueta.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            Etiqueta.TextWrapping = TextWrapping.Wrap;
            Etiqueta.TextTrimming = TextTrimming.WordEllipsis;
            if (EsPar)
                Etiqueta.Background = Recursos.GrisClaro_Brush;
            else
                Etiqueta.Background = Recursos.Blanco_Brush;
            Etiqueta.FontSize = Recursos.FontSizeSubTitle;
            Etiqueta.TextAlignment = TextAlignment.Center;
            //Etiqueta.Height = 40;

            //double x = 225;
            //if (Texto == "--")            
            //Etiqueta.Width = 75;
            //else
            //Etiqueta.Width = x;

            Grid.SetRow(Etiqueta, Fila);
            Grid.SetColumn(Etiqueta, NoColumna);
            xGridUC_Asistencia.Children.Add(Etiqueta);
            return Etiqueta;
        }
        public int ObtenColumnaNO(string NombreColumna)
        {

            try
            {
                var Columna = from c in xGridUC_Asistencia.ColumnDefinitions
                              where c.Name == NombreColumna
                              select xGridUC_Asistencia.ColumnDefinitions.IndexOf(c);

                return Columna.ElementAt<int>(0);

            }
            catch { return -1; }
        }
        public ColumnDefinition NuevaColumna(string Nombre, int Ancho, string Titulo = null)
        {
            ColumnDefinition Columna = new ColumnDefinition();
            Columna.Name = Nombre;
            /*if (Nombre == "Seleccionado" || Nombre == "Color")
                Columna.Width = new GridLength(Ancho);
            else
                Columna.Width = GridLength.Auto;*/
            Columna.Width = new GridLength(Ancho);
            xGridUC_Asistencia.ColumnDefinitions.Add(Columna);
            if (Titulo == null)
                Titulo = Nombre;
            m_AnchoControl += Ancho;
            TextBlock TB = NuevaEtiqueta(Titulo, 0, Nombre);
            TB.FontSize = Recursos.FontSizeTitle;

            return Columna;
        }
        public CheckBox NuevaCheckBox(bool Checado, int Fila, string NombreColumna)
        {
            int NoColumna = ObtenColumnaNO(NombreColumna);
            bool EsPar = false;
            if (Fila++ % 2 == 0)
                EsPar = true;
            CheckBox Chk = new CheckBox();
            Chk.IsChecked = Checado;
            if (EsPar)
                Chk.Foreground = Recursos.GrisClaro_Brush;
            Chk.Background = Recursos.GrisClaro_Brush;
            Grid.SetRow(Chk, Fila);
            Grid.SetColumn(Chk, NoColumna);
            xGridUC_Asistencia.Children.Add(Chk);
            return Chk;
        }
        private string validaFecha(DateTime objeto)
        {
            DateTime fnull = new DateTime(0001, 01, 01);
            DateTime fnula = new DateTime(2006, 01, 01);
            if (fnull == objeto)
                return "--";
            else if (fnula == objeto)
                return "--";
            else
                return objeto.ToString("HH:mm");

        }       
        
    }
}
