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
    /// Lógica de interacción para UC_AsistenciaHorizontal.xaml
    /// </summary>
    public partial class UC_AsistenciaHorizontal : UserControl
    {

        private bool m_Seleccionado = false;

        public bool Seleccionado
        {
            get { return m_Seleccionado; }
            set { m_Seleccionado = value; }
        }

        private bool m_EsTitulo = false;

        public bool EsTitulo
        {
            get { return m_EsTitulo; }
            set { m_EsTitulo = value; }
        }
        private int m_Dias = 0;

        public int Dias
        {
            get { return m_Dias; }
            set { m_Dias = value; }
        }

        int Persona_Diario_ID = 0;
        int Persona_Diario_IDMax = 0;
        Grid m_Grid = null;


        public UC_AsistenciaHorizontal()
        {
            InitializeComponent();/*
            Panel.Children.Clear();
            TextBlock tex = new TextBlock();
            tex.Text="Nombre";
            Panel.Children.Add(tex);*/
            Loaded += UC_AsistenciaHorizontal_Loaded;
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
                    new RectangleGeometry(new Rect(0, 0, 12, 1)));
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

        public Brush ObtenColorColumna(Newtonsoft.Json.Linq.JContainer Datos, int Pos)
        {

            //return ObtenColor(Recursos.AzulMuyClaro_Brush, 255 << 16);
            return ObtenColor(Recursos.AzulMuyClaro_Brush, Datos.Value<int>("IC" + Pos));
            return ObtenColor(Recursos.AzulMuyClaro_Brush, Datos.Value<int>("TC" + Pos));
        }
        string ObtenValor(Newtonsoft.Json.Linq.JContainer Datos, string Campo)
        {
            if (m_EsTitulo)
            {
                switch (Campo)
                {
                    case "ID":
                        return "0";
                        break;
                    case "NOMBRE":
                        return "Nombre";
                        break;
                    case "LINK":
                        return "No Emp";
                        break;
                    case "TURNO":
                        return "No Emp";
                        break;
                }
                return "";
            }
            else
                return Datos.Value<string>(Campo);
        }
        List<ToggleButton> m_Btns_Dias;

        public void Actualiza()
        {
            UC_AsistenciaHorizontal_Loaded(null, null);
        }
        void UC_AsistenciaHorizontal_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Panel.Children.Clear();

                Newtonsoft.Json.Linq.JContainer Datos = ((Newtonsoft.Json.Linq.JContainer)(DataContext));


                Persona_Diario_ID = Datos.Value<int>("ID");
                ToggleButton BtnNombre = new ToggleButton();
                TextBlock TextoNombre = new TextBlock();
                TextoNombre.Text = ObtenValor(Datos, "NOMBRE") + "(" + ObtenValor(Datos, "LINK") + ")";
                //TextoNombre.TextAlignment = TextAlignment.Left;
                //TextoNombre.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                TextoNombre.TextWrapping = TextWrapping.Wrap;
                BtnNombre.Content = TextoNombre;
                BtnNombre.Width = 180;
                BtnNombre.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left;
                BtnNombre.BorderBrush = BtnNombre.Background = Recursos.AzulMuyClaro_Brush;
                if (EsTitulo)
                {

                }
                else
                {
                    BtnNombre.Checked += BtnNombre_Checked;
                    BtnNombre.Unchecked += BtnNombre_Unchecked;
                }
                Panel.Children.Add(BtnNombre);
                m_Btns_Dias = new List<ToggleButton>();
                string Turno = ObtenValor(Datos, "TURNO");
                DateTime Persona_Diario_Fecha = Datos.Value<DateTime>("PERSONA_DIARIO_FECHA");
                for (int Cont = 0; Cont < Datos.Count; Cont++)
                {
                    try
                    {

                        Newtonsoft.Json.Linq.JProperty Propiedad = ((Newtonsoft.Json.Linq.JProperty)(Datos.ElementAt(Cont)));
                        bool EsEntradaSalida = Propiedad.Name.Contains("IO");
                        bool EsTurno = Propiedad.Name.Contains("TD");
                        bool EsAbreviatura = Propiedad.Name.Contains("ABR");
                        if (!EsEntradaSalida && !EsTurno && !EsAbreviatura)
                            continue;
                        int PosReferencia = 0;
                        if (EsEntradaSalida || EsTurno)
                            PosReferencia = eClockBase.CeC.Convierte2Int(Propiedad.Name.Substring(2));
                        else
                            PosReferencia = eClockBase.CeC.Convierte2Int(Propiedad.Name.Substring(3));
                        Persona_Diario_IDMax = Persona_Diario_ID + PosReferencia;

                        ToggleButton Dia = new ToggleButton();

                        m_Btns_Dias.Add(Dia);
                        Dia.Name = "Btn_" + Persona_Diario_IDMax;//Propiedad.Name;
                        //Dia.Tag = Propiedad;

                        Dia.Width = 120;
                        //Dia.BorderThickness = new Thickness(4);

                        if (!EsTitulo)
                        {
                            Dia.BorderBrush = Dia.Background = ObtenColorColumna(Datos, PosReferencia);
                            Dia.Content = Propiedad.Value.ToString();
                            Dia.Checked += Dia_Checked;
                            Dia.Unchecked += Dia_Unchecked;
                        }
                        else
                        {
                            
                            Dia.Background = Recursos.AzulMuyClaro_Brush;
                            Dia.BorderBrush = Recursos.Blanco_Brush;
                            int NoDia = Persona_Diario_IDMax % 10000;
                            int Ano = NoDia / 366;
                            int DiaAno = NoDia % 366;
                            DateTime Fecha = new DateTime(2000 + Ano, 01, 01).AddDays(DiaAno - 1);

                            Dia.Content = Fecha.ToString("dddd") + "\n" + Fecha.ToShortDateString();
                        }
                        Panel.Children.Add(Dia);

                        m_Dias++;
                    }
                    catch (Exception ex)
                    {
                        eClockBase.CeC_Log.AgregaError(ex);
                    }
                }
            }
            catch
            {
            }
        }

        void BtnNombre_Unchecked(object sender, RoutedEventArgs e)
        {
            if (m_Grid != null)
                m_Grid.Children.Clear();

        }

        void BtnNombre_Checked(object sender, RoutedEventArgs e)
        {
            eClockBase.Controladores.Asistencias Asistencia = new eClockBase.Controladores.Asistencias(CeC_Sesion.ObtenSesion(this));
            Asistencia.EventoObtenAsistenciaLinealNFinalizado += Asistencia_EventoObtenAsistenciaLinealNFinalizado;
            Asistencia.ObtenAsistenciaLinealN(Persona_Diario_ID, Persona_Diario_IDMax);

        }

        public delegate void CambioSeleccionadoArgs(int PersonaDiarioID, bool Seleccionado);
        public event CambioSeleccionadoArgs CambioSeleccionado;
        public bool fCambioSeleccionado(ToggleButton Btn)
        {
            if (CambioSeleccionado != null)
            {
                CambioSeleccionado(eClockBase.CeC.Convierte2Int(Btn.Name.Substring(4)), Btn.IsChecked == true ? true : false);
                return true;
            }
            return false;

        }
        /// <summary>
        /// Funcion para SUMAR los días seleccionados y poder mandarlos al label del TOOLBAR
        /// </summary>
        /// <param name="NoSeleccionados"></param>
        /// <param name="NoSeleccionadosAnterior"></param>
        /// <returns></returns>
        public delegate void NoSeleccionadosCambioArgs(int NoSeleccionados, int NoSeleccionadosAnterior);
        public event NoSeleccionadosCambioArgs NoSeleccionadosCambio;
        int m_NoSeleccionados = 0;
        public int NoSeleccionados()
        {
            ToggleButton Dia = new ToggleButton();
            string Tipo = Dia.GetType().ToString();
            int Seleccion = 0;
            foreach (Object Elemento in Panel.Children)
            {
                if (Elemento.GetType().ToString() == Tipo)
                {

                    Dia = (ToggleButton)Elemento;
                    if (Dia.IsChecked == true)
                        Seleccion++;
                }
            }
            if (NoSeleccionadosCambio != null)
                NoSeleccionadosCambio(Seleccion, m_NoSeleccionados);

            return m_NoSeleccionados = Seleccion;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="NoSeleccionados"></param>
        /// <param name="NoSeleccionadosAnterior"></param>
        /// <returns></returns>
        void CambiaCheckDia(ToggleButton Btn)
        {
            if (Btn.Tag != null)
            {
                CheckBox Chk = ((CheckBox)Btn.Tag);
                Chk.IsChecked = Btn.IsChecked;

            }
        }
        void Dia_Unchecked(object sender, RoutedEventArgs e)
        {
            ToggleButton Btn = ((ToggleButton)sender);
            Btn.Background = ((ToggleButton)sender).BorderBrush;
            //Funcion agregada por OT para restar los botones de horarios por dia.
            int Sel = NoSeleccionados();
            fCambioSeleccionado(Btn);
            CambiaCheckDia(Btn);
        }
        void Dia_Checked(object sender, RoutedEventArgs e)
        {
            ToggleButton Btn = ((ToggleButton)sender);
            Btn.Background = Recursos.Blanco_Brush;
            int Sel = NoSeleccionados();
            fCambioSeleccionado(Btn);
            CambiaCheckDia(Btn);
        }

        private void UserControl_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            //            Pnl_Vertical.Children.Add()


        }
        public int ObtenColumnaNO(string NombreColumna)
        {

            try
            {
                var Columna = from c in m_Grid.ColumnDefinitions
                              where c.Name == NombreColumna
                              select m_Grid.ColumnDefinitions.IndexOf(c);

                return Columna.ElementAt<int>(0);

            }
            catch { return -1; }
        }
        public TextBlock NuevaEtiqueta(string Texto, int Fila, string NombreColumna)
        {
            int NoColumna = ObtenColumnaNO(NombreColumna);
            bool EsPar = false;
            if (Fila++ % 2 == 0)
                EsPar = true;
            TextBlock Etiqueta = new TextBlock();
            Etiqueta.Margin = new Thickness(2);
            Etiqueta.Text = Texto;
            //if (EsPar)
            Etiqueta.Background = Recursos.GrisClaro_Brush;
            Grid.SetRow(Etiqueta, Fila);
            Grid.SetColumn(Etiqueta, NoColumna);
            m_Grid.Children.Add(Etiqueta);
            return Etiqueta;
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
            //Chk.Background = Recursos.GrisClaro_Brush;
            Grid.SetRow(Chk, Fila);
            Grid.SetColumn(Chk, NoColumna);
            m_Grid.Children.Add(Chk);
            return Chk;
        }
        int m_AnchoControl = 0;

        public int AnchoControl
        {
            get { return m_AnchoControl; }
            set
            {
                m_AnchoControl = value;

            }
        }

        public ColumnDefinition NuevaColumna(string Nombre, int Ancho, string Titulo = null)
        {
            ColumnDefinition Columna = new ColumnDefinition();
            Columna.Name = Nombre;
            Columna.Width = new GridLength(Ancho, GridUnitType.Star);
            m_Grid.ColumnDefinitions.Add(Columna);
            if (Titulo == null)
                Titulo = Nombre;
            m_AnchoControl += Ancho;
            NuevaEtiqueta(Titulo, 0, Nombre).FontSize = Recursos.FontSizeSubTitulo;
            return Columna;
        }
        void Asistencia_EventoObtenAsistenciaLinealNFinalizado(List<eClockBase.Modelos.Asistencias.Model_Asistencia_Lineal_N> Asistencia)
        {
            if (m_Grid == null)
            {
                m_Grid = new Grid();
                m_Grid.Background = Recursos.Blanco_Brush;
                Pnl_Vertical.Children.Add(m_Grid);
            }
            else
                m_Grid.Children.Clear();

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
            RowDefinition FilaTitulos = new RowDefinition();
            m_Grid.RowDefinitions.Add(FilaTitulos);
            NuevaColumna("Seleccionado", 25, "");
            NuevaColumna("Color", 7, "");
            NuevaColumna("Fecha", 100, "Fecha");
            NuevaColumna("Entrada", 80, "Entrada");
            if (MostrarComidas)
            {
                NuevaColumna("Comida_salida", 100, "Comida Salida");
                NuevaColumna("Comida_regreso", 80, "Comida Regreso");
            }
            NuevaColumna("Salida", 80, "Salida");
            NuevaColumna("Turno", 80, "Turno");
            NuevaColumna("Incidencias", 80, "Incidencia");
            //NuevaColumna("Abreviaturas", 80, "Abreviaturas");
            NuevaColumna("Comida_incidencia", 80, "Comida Incidencia");
            NuevaColumna("Tde", 80, "Tiempo Deuda(Retardo)");
            NuevaColumna("TT", 80, "Tiempo Trabajado");
            NuevaColumna("Timpo_de_comida", 80, "Tiempo de Comida");           
            NuevaColumna("TE", 80, "Tiempo de estancia");
            if (MostrarHorasExtras)
            {
                NuevaColumna("Hes", 80, "Tiempo Extra Trabajado");
                NuevaColumna("Hec", 80, "Tiempo Extra Calculado");
            }
            int NoDia = 0;

            foreach (eClockBase.Modelos.Asistencias.Model_Asistencia_Lineal_N Objeto in Asistencia)
            {
                RowDefinition Fila = new RowDefinition();

                m_Grid.RowDefinitions.Add(Fila);
                ToggleButton Btn_Dia = m_Btns_Dias[NoDia++];
                int NoFila = m_Grid.RowDefinitions.IndexOf(Fila);
                Objeto.SELECCIONADO = Btn_Dia.IsChecked == true ? true : false;

                Btn_Dia.Tag = NuevaCheckBox(Objeto.SELECCIONADO, NoFila, "Seleccionado");
                NuevaEtiqueta(" ", NoFila, "Color").Background = Btn_Dia.BorderBrush;

                NuevaEtiqueta(Objeto.FECHA.ToShortDateString(), NoFila, "Fecha");//.Background = Btn_Dia.Background;
                NuevaEtiqueta(validaFecha(Objeto.E), NoFila, "Entrada");
                //NuevaEtiqueta(Objeto.E+"", NoFila, "Entrada");
                if (MostrarComidas)
                {

                    NuevaEtiqueta(validaFecha(Objeto.CS), NoFila, "Comida_salida");
                    NuevaEtiqueta(validaFecha(Objeto.CR), NoFila, "Comida_regreso");
                }
                NuevaEtiqueta(validaFecha(Objeto.S), NoFila, "Salida");
                NuevaEtiqueta(Objeto.TURNO, NoFila, "Turno");
                NuevaEtiqueta(Objeto.INC, NoFila, "Incidencias");
                // NuevaEtiqueta(Objeto.ABR, NoFila, "Abreviaturas");
                NuevaEtiqueta(Objeto.COMI, NoFila, "Comida_incidencia");
                NuevaEtiqueta(validaFecha(Objeto.TT), NoFila, "TT");
                NuevaEtiqueta(validaFecha(Objeto.TDE), NoFila, "Tde");
                NuevaEtiqueta(validaFecha(Objeto.TE), NoFila, "TE");
                NuevaEtiqueta(validaFecha(Objeto.TC), NoFila, "Timpo_de_comida");
                /*
                NuevaEtiqueta(Objeto.TURNO, NoFila, 5);
                NuevaEtiqueta(Objeto.INC, NoFila, 6);
                NuevaEtiqueta(Objeto.ABR, NoFila, 7);
                NuevaEtiqueta(Objeto.COMI, NoFila, 8);
                NuevaEtiqueta(Objeto.TT.ToShortTimeString(), NoFila, 9);
                NuevaEtiqueta(Objeto.TDE.ToShortTimeString(), NoFila, 10);
                NuevaEtiqueta(Objeto.TC.ToShortTimeString(), NoFila, 11);
                */
                if (MostrarHorasExtras)
                {
                    NuevaEtiqueta(validaFecha(Objeto.HES), NoFila, "Hes");
                    NuevaEtiqueta(validaFecha(Objeto.HEC), NoFila, "Hec");
                }

            }
            FilaTitulos = new RowDefinition();
            m_Grid.RowDefinitions.Add(FilaTitulos);
        }
        private string validaFecha(DateTime objeto)
        {
            DateTime fnull = new DateTime(0001, 01, 01);
            DateTime fnula = new DateTime(2006, 01, 01);
            if (fnull == objeto || fnula == objeto)
                return "--";
            else
                return objeto.ToString("HH:mm:ss");

        }
        private void UserControl_LostFocus_1(object sender, RoutedEventArgs e)
        {
        }

        private void UserControl_GotFocus_1(object sender, RoutedEventArgs e)
        {


        }

        private void UserControl_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            //bool Resultado = this.Focus();

        }

        public string ObtenPersonasDiarioIdsSeleccionadas()
        {
            ToggleButton Dia = new ToggleButton();
            string Tipo = Dia.GetType().ToString();
            string PersonasDiarioIds = "";
            foreach (Object Elemento in Panel.Children)
            {
                if (Elemento.GetType().ToString() == Tipo)
                {

                    Dia = (ToggleButton)Elemento;
                    if (Dia.IsChecked == true && Dia.Name.Length > 4)
                    {
                        try
                        {
                            PersonasDiarioIds = eClockBase.CeC.AgregaSeparador(PersonasDiarioIds, Dia.Name.Substring(4), ",");
                        }
                        catch { }
                    }
                }
            }
            return PersonasDiarioIds;
        }

        /// <summary>
        /// Find Child Control from ContentPresenter
        /// </summary>
        /// <typeparam name="ChildControl"></typeparam>
        /// <param name="DependencyObj"></param>
        /// <returns></returns>
        public static ChildControl FindVisualChild<ChildControl>(DependencyObject DependencyObj)
        where ChildControl : DependencyObject
        {
            if (DependencyObj == null)
                return null;
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(DependencyObj); i++)
            {
                DependencyObject Child = VisualTreeHelper.GetChild(DependencyObj, i);

                if (Child != null && Child is ChildControl)
                {
                    return (ChildControl)Child;
                }
                else
                {
                    ChildControl ChildOfChild = FindVisualChild<ChildControl>(Child);

                    if (ChildOfChild != null)
                    {
                        return ChildOfChild;
                    }
                }
            }
            return null;
        }

        public static UC_AsistenciaHorizontal ObtenElementoAsistenciaHorizontal(ListBox Lista, int NoElemento)
        {
            try
            {
                ContentPresenter CP = FindVisualChild<ContentPresenter>(Lista.ItemContainerGenerator.ContainerFromItem(Lista.Items[NoElemento]));
                if (CP == null)
                    return null;
                DataTemplate DataTemplateObj = CP.ContentTemplate;
                return DataTemplateObj.FindName("AsistenciaHorizontal", CP) as UC_AsistenciaHorizontal;
            }
            catch (Exception ex)
            {
                eClockBase.CeC_Log.AgregaError(ex);
            }
            return null;
        }
    }
}

