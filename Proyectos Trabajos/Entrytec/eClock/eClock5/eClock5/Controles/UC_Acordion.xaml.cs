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
#endif
using Newtonsoft.Json;
using System.ComponentModel;
using eClockBase.Controladores;

namespace eClock5
{
    public class UC_Acordion_Pestanas
    {
        public string Nombre { get; set; }
        public string Etiqueta { get; set; }
        public UserControl Vista { get; set; }
        public UC_Acordion_Herencia Padre { get; set; }
        /// <summary>
        /// Indica que permisos deberá tener para que se muestre el elemento
        /// </summary>
        public string Permisos { get; set; }
    }
    public class UC_Acordion_Herencia
    {
        public string Nombre { get; set; }
        public string Etiqueta { get; set; }

        [Description("Nombre de la tabla que almacena los datos."), Category("Datos")]
        public string Tabla { get; set; }
        [Description("Campo con el id que se usará para identificar al elemento."), Category("Datos")]
        public string CampoLlave { get; set; }
        [Description("Campo con el Nombre o texto que mostrará de manera principal."), Category("Datos")]
        public string CampoNombre { get; set; }
        [Description("Campo Adicional, esquina superior derecha."), Category("Datos")]
        public string CampoAdicional { get; set; }
        [Description("Campo con el Nombre o texto que mostrará debajo del nombre."), Category("Datos")]
        public string CampoDescripcion { get; set; }
        [Description("Filtro que aplicará."), Category("Datos")]
        public string Filtro { get; set; }

        private int m_PensenaSeleccionada = -1;

        public int PensenaSeleccionada
        {
            get { return m_PensenaSeleccionada; }
            set { m_PensenaSeleccionada = value; }
        }

        private List<UC_Acordion_Pestanas> m_Pestanas = new List<UC_Acordion_Pestanas>();
        public List<UC_Acordion_Pestanas> Pestanas { get { return m_Pestanas; } }
        private List<UC_Acordion_Herencia> m_Herencia = new List<UC_Acordion_Herencia>();
        public List<UC_Acordion_Herencia> Herencia { get { return m_Herencia; } }

        public object Tag { get; set; }
        public object Parametro { get; set; }


        /// <summary>
        /// Indica que permisos deberá tener para que se muestre el elemento
        /// </summary>
        public string Permisos { get; set; }
    }
    public class UC_Acordion_Seccion
    {
        [Description("Nombre que se usará como identificador del elemento.")]
        public string Nombre { get; set; }
        public string Etiqueta { get; set; }
        private List<UC_Acordion_Herencia> m_Herencia = new List<UC_Acordion_Herencia>();
        public List<UC_Acordion_Herencia> Herencia { get { return m_Herencia; } }
        public UC_Acordion_Herencia ObtenHerencia(List<UC_Acordion_Herencia> Herencias, object Tag)
        {
            foreach (UC_Acordion_Herencia Herencia in Herencias)
            {
                if (Herencia.Tag == Tag)
                    return Herencia;
                if (Herencia.Herencia != null && Herencia.Herencia.Count > 0)
                    return ObtenHerencia(Herencia.Herencia, Tag);
            }
            return null;
        }

    }

    /// <summary>
    /// Lógica de interacción para UC_Acordion.xaml
    /// </summary>
    public partial class UC_Acordion : UserControl
    {
        eClockBase.CeC_SesionBase Sesion;
        private List<UC_Acordion_Seccion> m_Secciones = new List<UC_Acordion_Seccion>();
        [Description("Secciones en la que estará dividido el acordión.")]
        public List<UC_Acordion_Seccion> Secciones
        {
            get { return m_Secciones; }
        }

        public Controles.UC_Pestanas ControlPestanas { get; set; }

        public UC_Acordion_Herencia ObtenHerencia(object Tag)
        {

            foreach (UC_Acordion_Seccion Seccion in m_Secciones)
            {
                UC_Acordion_Herencia Herencia = Seccion.ObtenHerencia(Seccion.Herencia, Tag);
                if (Herencia != null)
                    return Herencia;
            }
            return null;
        }
        public UC_Acordion()
        {
            Sesion = CeC_Sesion.ObtenSesion(this);
            InitializeComponent();
            Loaded += UC_Acordion_Loaded;
            SizeChanged += UC_Acordion_SizeChanged;
        }
        void ActualizaAltoContenedor()
        {
            m_AltoContenedor = this.ActualHeight - (Secciones.Count + 1) * (AltoSeccionTitulo + m_Separacion);
            if (m_AltoContenedor <= 0)
                m_AltoContenedor = (Secciones.Count + 1) * (AltoSeccionTitulo + m_Separacion);

        }
        void UC_Acordion_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ActualizaAltoContenedor();
            try
            {
                foreach (TreeView OTree in m_Trees)
                    OTree.Height = m_AltoContenedor;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }

        void UC_Acordion_Loaded(object sender, RoutedEventArgs e)
        {



        }

        void cRestricciones_TieneDerechosEvent(string RestriccionesPermitidas)
        {
            Crea(RestriccionesPermitidas);

        }
        private double m_AltoSeccionTitulo = 46;

        public double AltoSeccionTitulo
        {
            get { return m_AltoSeccionTitulo; }
            set { m_AltoSeccionTitulo = value; }
        }

        private ToggleButton m_UltimoSeleccionado = null;
        private List<TreeView> m_Trees = new List<TreeView>();
        private double m_AltoContenedor = 0;
        private double m_Separacion = 4;
        Controles.UC_TextBoxButton m_Tbx_Buscar;
        string m_RestriccionesPermitidas = "";
        public bool Crea(string RestriccionesPermitidas)
        {
            try
            {
                m_RestriccionesPermitidas = RestriccionesPermitidas;
                Pnl.Children.Clear();
                ActualizaAltoContenedor();
                m_Tbx_Buscar = new Controles.UC_TextBoxButton();
                m_Tbx_Buscar.Background = Recursos.GrisClaro_Brush;
                m_Tbx_Buscar.Name = "Tbx_Buscar";
                m_Tbx_Buscar.Height = m_AltoSeccionTitulo;
                m_Tbx_Buscar.Width = double.NaN;
                m_Tbx_Buscar.Margin = new Thickness(0, 0, 0, 5);
                m_Tbx_Buscar.Click += TB_Click;
                Pnl.Children.Add(m_Tbx_Buscar);
                bool Primero = true;
                foreach (UC_Acordion_Seccion Seccion in Secciones)
                {

                    ToggleButton Btn = new ToggleButton();
                    Btn.Name = Seccion.Nombre;
                    Btn.Content = Seccion.Etiqueta;
                    Btn.Style = (Style)FindResource("ToggleButtonAcordionStyle");
                    /*
                    Btn.Width = double.NaN;
                    Btn.Height = AltoSeccionTitulo;
                    Btn.Background = Recursos.GrisClaro_Brush;
                    Btn.FontFamily = Recursos.EntryTecFont;
                    Btn.FontSize = Recursos.FontSizeTitulo;
                    Btn.Foreground = Recursos.Negro_Brush;
                    Btn.Margin = new Thickness(0, m_Separacion, 0, 0);*/
                    BaseModificada.Localizaciones.sLocaliza(GetType().FullName, Btn);
                    LocalizaPestanas(GetType().FullName + "." + Seccion.Nombre, Seccion.Herencia);
                    Pnl.Children.Add(Btn);
                    TreeView Tree = new TreeView();
                    m_Trees.Add(Tree);
                    Btn.Tag = Tree;
                    Btn.Click += Btn_Click;
                    Tree.Name = "Tree_" + Seccion.Nombre;
                    Tree.BorderThickness = new Thickness(0);
                    Tree.Height = m_AltoContenedor;
                    Tree.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                    Tree.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch;
                    Tree.Background = Recursos.GrisMedio_Brush;
                    Tree.FontFamily = Recursos.EntryTecFont;
                    Tree.FontSize = Recursos.FontSizeSubTitulo;

                    if (Primero)
                    {
                        Btn.IsChecked = true;
                        Btn_Click(Btn, null);
                        Primero = false;
                    }
                    else
                        Tree.Visibility = Visibility.Collapsed;
                    CreaTreeViewItem(GetType().FullName + "." + Seccion.Nombre, Tree.Items, Seccion.Herencia, RestriccionesPermitidas, true);

                    Tree.GotFocus += Tree_GotFocus;
                    Tree.MouseLeftButtonDown += Tree_MouseLeftButtonDown;
                    Tree.SelectedItemChanged += Tree_SelectedItemChanged;

                    //Tree.Items
                    Pnl.Children.Add(Tree);
                }

                return true;
            }
            catch (Exception ex)
            {
                eClockBase.CeC_Log.AgregaError(ex);
            }
            return false;
        }

        public string ObtenPermisosAValidar()
        {
            string Elementos = "";
            foreach (UC_Acordion_Seccion Seccion in Secciones)
            {
                foreach (UC_Acordion_Herencia Elemento in Seccion.Herencia)
                {
                    Elementos = eClockBase.CeC.AgregaSeparador(Elementos, ObtenPermisosAValidar(Elemento), ",");
                }
            }
            string ElementosFiltrados = "";
            string[] AElementos = eClockBase.CeC.ObtenArregoSeparador(Elementos, ",");
            foreach (string Elemento in AElementos)
            {
                string sElemento = Elemento.Trim();
                if (sElemento != "" && !eClockBase.CeC.ExisteEnSeparador(ElementosFiltrados, sElemento, ","))
                    ElementosFiltrados = eClockBase.CeC.AgregaSeparador(ElementosFiltrados, sElemento, ",");
            }
            return ElementosFiltrados;
        }

        public string ObtenPermisosAValidar(UC_Acordion_Herencia Elemento)
        {
            string Elementos = "";
            if (Elemento.Permisos != null && Elemento.Permisos != "")
                Elementos = eClockBase.CeC.AgregaSeparador(Elementos, Elemento.Permisos, ",");

            foreach (UC_Acordion_Pestanas Pestana in Elemento.Pestanas)
            {
                if (Pestana.Permisos != null && Pestana.Permisos != "")
                    Elementos = eClockBase.CeC.AgregaSeparador(Elementos, Pestana.Permisos, ",");
            }

            if (Elemento == null || Elemento.Herencia == null)
                return Elementos;

            foreach (UC_Acordion_Herencia ElementoHer in Elemento.Herencia)
            {
                /* if (ElementoHer.Permisos != null && ElementoHer.Permisos != "")
                     Elementos = eClockBase.CeC.AgregaSeparador(Elementos, ElementoHer.Permisos, ",");*/
                Elementos = eClockBase.CeC.AgregaSeparador(Elementos, ObtenPermisosAValidar(ElementoHer), ",");

            }
            return Elementos;
        }
        void TB_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                eClockBase.Controladores.Personas Personas = new Personas(Sesion);
                Personas.BuscaPersonasFinalizado += Personas_BuscaPersonasFinalizado;

                Personas.BuscaPersonas(m_Tbx_Buscar.Texto);
            }
            catch { }
        }

        public UC_Acordion_Herencia ObtenUltimo(UC_Acordion_Herencia HerenciaAConsultar)
        {
            if (HerenciaAConsultar.Herencia.Count > 0)
                return ObtenUltimo(HerenciaAConsultar.Herencia[0]);
            return HerenciaAConsultar; 
        }
        void Personas_BuscaPersonasFinalizado(List<eClockBase.Modelos.Personas.Model_PersonasBusqueda> Resultado, string TextoBuscado)
        {
            try
            {
                TreeViewItem ItemTEmp = (TreeViewItem)m_Trees[0].Items[0];
                ItemTEmp.IsExpanded = false;
                if (m_Trees[0].Items.Count >= 2)
                    m_Trees[0].Items.RemoveAt(1);
                if (m_Trees[0].Items.Count < 2)
                {
                    m_Trees[0].Items.Add(NuevoTreeViewItem(null, "Resultado de la busqueda", null, Secciones[0].Herencia[0].Herencia[0], "@PERSONA_DATO like '%" + TextoBuscado + "%'"));
                }
                TreeViewItem ItemResultado = (TreeViewItem)m_Trees[0].Items[1];
                ItemResultado.Items.Clear();
                foreach (eClockBase.Modelos.Personas.Model_PersonasBusqueda Persona in Resultado)
                {
                    TreeViewItem ItemCampo = BuscaItem(ItemResultado.Items, Persona.CAMPO_ETIQUETA);
                    if (ItemCampo == null)
                    {
                        ItemCampo = NuevoTreeViewItem(null, Persona.CAMPO_ETIQUETA, null, Secciones[0].Herencia[0].Herencia[0], "@CAMPO_NOMBRE='" + Persona.CAMPO_NOMBRE + "' AND PERSONA_DATO like'%" + TextoBuscado + "%'");
                        ItemResultado.Items.Add(ItemCampo);
                    }
                    TreeViewItem ItemDato = BuscaItem(ItemCampo.Items, Persona.PERSONA_DATO);
                    if (ItemDato == null)
                    {
                        ItemDato = NuevoTreeViewItem(null, Persona.PERSONA_DATO, null, Secciones[0].Herencia[0].Herencia[0], "@CAMPO_NOMBRE='" + Persona.CAMPO_NOMBRE + "' AND PERSONA_DATO ='" + Persona.PERSONA_DATO + "'");
                        ItemCampo.Items.Add(ItemDato);
                    }

                    TreeViewItem ItemPersona = NuevoTreeViewItem(null, Persona.ToString(), null, ObtenUltimo(Secciones[0].Herencia[0]), Persona.PERSONA_ID.ToString());
                    ItemDato.Items.Add(ItemPersona);

                }
                ItemResultado.IsExpanded = true;
                /*var Datos = from R in Resultado
                            orderby R.CAMPO_ETIQUETA, R.PERSONA_DATO, R.PERSONA_NOMBRE
                            select R;

                foreach (var Dato in Datos)
                {
                    Dato.
                }*/

            }
            catch { }
        }

        void Tree_GotFocus(object sender, RoutedEventArgs e)
        {
            Tree_SelectedItemChanged(sender, null);
        }

        void Tree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            /*if (e == null)
                return;*/
            TreeView Tree = (TreeView)sender;
            if (Tree.SelectedItem == null)
                return;
            TreeViewItem TVI = (TreeViewItem)Tree.SelectedItem;
            TVI.IsExpanded = true;
            UC_Acordion_Herencia Elemento = (UC_Acordion_Herencia)TVI.Tag;

            ControlPestanas.CreaPestanas(Elemento, m_RestriccionesPermitidas);

        }

        void Tree_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Tree_SelectedItemChanged(sender, null);
        }
        public TreeViewItem BuscaItem(ItemCollection Items, string Header)
        {
            if (Items == null || Items.Count < 1)
                return null;
            foreach (object Item in Items)
            {
                if (((TreeViewItem)Item).Header.ToString() == Header)
                    return ((TreeViewItem)Item);
            }
            return null;
        }
        private TreeViewItem NuevoTreeViewItem(string Nombre, object Etiqueta, object Tag, object ToolTip)
        {
            TreeViewItem TVI = new TreeViewItem();
            if (Nombre != null)
                TVI.Name = Nombre;
            TVI.Header = Etiqueta;
            TVI.Tag = Tag;
            TVI.ToolTip = ToolTip;
            TVI.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            TVI.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch;
            //TVI.Expanded += TVI_Expanded;
            return TVI;
        }
        private TreeViewItem NuevoTreeViewItem(string Nombre, object Etiqueta, object ToolTip, UC_Acordion_Herencia Elemento, string Parametros)
        {
            UC_Acordion_Herencia ElementoNuevo = new UC_Acordion_Herencia();
            ElementoNuevo.Pestanas.AddRange(Elemento.Pestanas);
            ElementoNuevo.Etiqueta = Etiqueta.ToString();
            //ElementoNuevo.Nombre
            ElementoNuevo.Parametro = Parametros;

            return NuevoTreeViewItem(Nombre, Etiqueta, ElementoNuevo, ToolTip);
        }
        public bool LocalizaPestanas(string NombrePadre, List<UC_Acordion_Herencia> Herencia)
        {
            if (Herencia == null || Herencia.Count < 1)
                return false;
            string NombreP = NombrePadre;
            eClockBase.CeC_SesionBase Base = CeC_Sesion.ObtenSesion(this);
            foreach (UC_Acordion_Herencia Local in Herencia)
            {
                NombreP = NombrePadre + "." + Local.Nombre;
                foreach (UC_Acordion_Pestanas Pestana in Local.Pestanas)
                {
                    try
                    {
                        Localizaciones Loc = new Localizaciones(Base);
                        Loc.ObtenTextoEvent += delegate(object Destino, string Texto)
                        {
                            UC_Acordion_Pestanas PestanaFinal = ((UC_Acordion_Pestanas)Destino);
                            PestanaFinal.Etiqueta = Texto;
                        };
                        Loc.ObtenEtiqueta(NombreP + "." + Pestana.Nombre, Pestana.Etiqueta, Pestana);
                    }
                    catch { }
                }
                LocalizaPestanas(NombreP, Local.Herencia);
            }
            return true;
        }

        public bool CreaTreeViewItem(string NombrePadre, ItemCollection Items, List<UC_Acordion_Herencia> Herencia, string RestriccionesPermitidas, bool Expandido = false)
        {
            try
            {
                if (Herencia == null || Herencia.Count < 1)
                    return false;
                foreach (UC_Acordion_Herencia Elemento in Herencia)
                {
                    if (Elemento.Tabla != null && Elemento.Tabla.Length > 0)
                    {
                        //continue;
                        eClockBase.Controladores.Sesion CSesion = new eClockBase.Controladores.Sesion(Sesion);

                        CSesion.CambioListadoEvent += delegate(string Listado)
                        {
                            try
                            {

                                List<eClockBase.Controladores.ListadoJson> Datos = eClockBase.Controladores.CeC_ZLib.Json2Object<List<eClockBase.Controladores.ListadoJson>>(Listado);
                                // Datos[0].Seleccionado = true;
                                Items.Clear();
                                foreach (eClockBase.Controladores.ListadoJson ElementoDato in Datos)
                                {
                                    bool EsAgrupacion = false;
                                    string Agrupacion = "";
                                    if (ElementoDato.Descripcion != null)
                                    {
                                        Agrupacion = ElementoDato.Descripcion.ToString();
                                        if (Agrupacion != null && Agrupacion != "" && Agrupacion[0] == '|')
                                            EsAgrupacion = true;
                                    }
                                    if (EsAgrupacion)
                                    {
                                        ItemCollection ItemsLocal = Items;
                                        string[] Separados = Agrupacion.Split(new char[] { '|' });
                                        string Agrup = "";
                                        foreach (string Separacion in Separados)
                                        {
                                            Agrup += Separacion + "|";
                                            if (Separacion != null && Separacion.Length > 0)
                                            {
                                                TreeViewItem TVI = BuscaItem(ItemsLocal, Separacion);
                                                if (TVI == null)
                                                {
                                                    //Elemento.Parametro = new Clases.Parametros(false, Agrup);
                                                    TVI = NuevoTreeViewItem(null, Separacion, null, Elemento, Agrup);
                                                    ItemsLocal.Add(TVI);
                                                }
                                                ItemsLocal = TVI.Items;
                                            }
                                        }
                                        UC_Acordion_Herencia ElementoHijo = Elemento;
                                        if (Elemento.Herencia.Count == 1)
                                        {
                                            ElementoHijo = Elemento.Herencia[0];
                                        }


                                        TreeViewItem TVIDato = NuevoTreeViewItem(ElementoHijo.Nombre + "_" + ElementoDato.Llave.ToString(), ElementoDato.Nombre + eClockBase.CeC.Parentesis(ElementoDato.Adicional.ToString()),
                                            ElementoDato.Descripcion, ElementoHijo, ElementoDato.Llave.ToString());
                                        CreaTreeViewItem("", TVIDato.Items, ElementoHijo.Herencia, RestriccionesPermitidas);
                                        ItemsLocal.Add(TVIDato);
                                    }
                                    else
                                    {

                                        TreeViewItem TVI = NuevoTreeViewItem(Elemento.Nombre + "_" + ElementoDato.Llave.ToString(),
                                            ElementoDato.Nombre, ElementoDato.Descripcion, Elemento, ElementoDato.Llave.ToString());

                                        CreaTreeViewItem("", TVI.Items, Elemento.Herencia, RestriccionesPermitidas);

                                        Items.Add(TVI);
                                    }
                                }

                            }
                            catch (Exception ex)
                            {
                                eClockBase.CeC_Log.AgregaError(ex);
                            }
                            //e.Result;
                        };
                        if (Elemento.CampoLlave != null && Elemento.CampoLlave.Length > 0
                            && Elemento.CampoNombre != null && Elemento.CampoNombre.Length > 0)
                            CSesion.ObtenListado(Elemento.Tabla, Elemento.CampoLlave, Elemento.CampoNombre, Elemento.CampoAdicional, Elemento.CampoDescripcion, "", false, Elemento.Filtro);
                    }
                    else
                    {
                        if (ValidaRestricciones(Elemento.Permisos, RestriccionesPermitidas))
                        {
                            TreeViewItem TVI = NuevoTreeViewItem(Elemento.Nombre, Elemento.Etiqueta, Elemento, "");
                            if (Controles.UC_Pestanas.TienePestanas(Elemento, RestriccionesPermitidas))
                            {
                                CreaTreeViewItem(eClockBase.CeC.AgregaSeparador(NombrePadre, Elemento.Nombre, "."), TVI.Items, Elemento.Herencia, RestriccionesPermitidas);
                                BaseModificada.Localizaciones.sLocaliza(NombrePadre, TVI);

                                TVI.IsExpanded = Expandido;
                                Items.Add(TVI);
                            }
                        }

                        //TVI.it
                    }

                }
                return true;
            }
            catch (Exception ex)
            {
                eClockBase.CeC_Log.AgregaError(ex);
            }
            return false;
        }
        public static bool ValidaRestricciones(string Permisos, string RestriccionesPermitidas)
        {
            if (Permisos == null || Permisos == "")
                return true;
            return eClockBase.CeC.ExisteEnSeparador(RestriccionesPermitidas, Permisos, ",");
        }

        void Btn_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton Btn = (ToggleButton)sender;
            TreeView Tree = (TreeView)Btn.Tag;
            if (Btn == m_UltimoSeleccionado)
                return;
            if (m_UltimoSeleccionado != null)
            {
                m_UltimoSeleccionado.IsChecked = false;
                //                m_UltimoSeleccionado.Foreground = Recursos.Negro_Brush;
                //                m_UltimoSeleccionado.Background = Recursos.GrisClaro_Brush;
                ((TreeView)m_UltimoSeleccionado.Tag).Visibility = Visibility.Collapsed;
            }
            //            Btn.Background = Recursos.AzulClaro_Brush;
            //            Btn.Foreground = Recursos.Blanco_Brush;

            m_UltimoSeleccionado = Btn;
            /*
            foreach(TreeView OTree in m_Trees)
                OTree.Visibility = Visibility.Collapsed;*/
            Tree.Visibility = Visibility.Visible;
        }

        public void Localiza()
        {
            eClockBase.Controladores.Restricciones cRestricciones = new Restricciones(Sesion);
            cRestricciones.TieneDerechosEvent += cRestricciones_TieneDerechosEvent;
            cRestricciones.TieneDerechos(ObtenPermisosAValidar());
        }
    }
}
