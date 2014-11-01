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
using System.Collections.ObjectModel;

namespace eClock5.Controles
{
    /// <summary>
    /// Lógica de interacción para UC_Listado1N.xaml
    /// </summary>
    public partial class UC_Listado1N : UserControl
    {
#if !NETFX_CORE
        [Description("Nombre de la tabla que almacena los datos UNO."), Category("Datos")]
#endif
        public string TablaUNO { get; set; }
#if !NETFX_CORE
        [Description("Campo con el id que se usará para identificar al elemento UNO."), Category("Datos")]
#endif
        public string CampoLlaveUNO { get; set; }
#if !NETFX_CORE
        [Description("Campo con el Nombre o texto que mostrará de manera principal UNO."), Category("Datos")]
#endif
        public string CampoNombreUNO { get; set; }
#if !NETFX_CORE
        [Description("Campo Adicional, esquina superior derecha UNO."), Category("Datos")]
#endif
        public string CampoAdicionalUNO { get; set; }
#if !NETFX_CORE
        [Description("Campo con el Nombre o texto que mostrará debajo del nombre UNO."), Category("Datos")]
#endif
        public string CampoDescripcionUNO { get; set; }


#if !NETFX_CORE
        [Description("Nombre de la tabla que almacena los datos N."), Category("Datos")]
#endif
        public string TablaN { get; set; }
#if !NETFX_CORE
        [Description("Campo con el id que se usará para identificar al elemento N."), Category("Datos")]
#endif
        public string CampoLlaveN { get; set; }
#if !NETFX_CORE
        [Description("Campo con el Nombre o texto que mostrará de manera principal N."), Category("Datos")]
#endif
        public string CampoNombreN { get; set; }
#if !NETFX_CORE
        [Description("Campo Adicional, esquina superior derecha N."), Category("Datos")]
#endif
        public string CampoAdicionalN { get; set; }
#if !NETFX_CORE
        [Description("Campo con el Nombre o texto que mostrará debajo del nombre N."), Category("Datos")]
#endif
        public string CampoDescripcionN { get; set; }
#if !NETFX_CORE
        [Description("Filtro que aplicará N."), Category("Datos")]
#endif
        public string FiltroN { get; set; }

        private bool m_MostrarBorradosN = false;
#if !NETFX_CORE
        [Description("Indica si mostrará borrados N."), Category("Datos")]
#endif
        public bool MostrarBorradosN { get { return m_MostrarBorradosN; } set { m_MostrarBorradosN = value; } }

#if !NETFX_CORE
        [Description("Nombre de la tabla que almacena los datos de relacion uno a N."), Category("Datos")]
#endif
        public string TablaUNOaN { get; set; }



        public UC_Listado1N()
        {
            InitializeComponent();
            Loaded += UC_Listado_Loaded;
            ToolBar.OnEventClickToolBar += ToolBar_OnEventClickToolBar;
            Lst_Datos.SelectionChanged += Lst_Datos_SelectionChanged;
            IsVisibleChanged += UC_Listado1N_IsVisibleChanged;
        }

        void UC_Listado1N_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue.Equals(true))
            {
                ActualizaDatos();
            }
        }

        private bool m_MostrarToolBar = true;
        public bool MostrarToolBar
        {
            get
            {
                return m_MostrarToolBar;
            }
            set
            {
                m_MostrarToolBar = value;
                if (MostrarToolBar)
                {
                    ToolBar.Visibility = System.Windows.Visibility.Visible;
                    Lst_Datos.Margin = new Thickness(0, ToolBar.Height, 0, 0);
                }
                else
                {
                    ToolBar.Visibility = System.Windows.Visibility.Hidden;
                    Lst_Datos.Margin = new Thickness(0, 0, 0, 0);
                }
            }
        }

        public string PermisoConsultar { get; set; }
        public string PermisoBorrar { get; set; }
        public string PermisoAlta { get; set; }
        public string PermisoMostrarBorrados { get; set; }
        private bool m_ControlEdicionAgregado = false;



        void Lst_Datos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


        }

        void Cerrar()
        {
            Clases.Parametros.ObtenPadre(this).Visibility = Visibility.Hidden;
        }
        string ObtenValores()
        {
            string Activos = "";
            foreach (Listado Lst in Datos)
            {
                if (Lst.Seleccionado)
                    Activos = eClockBase.CeC.AgregaSeparador(Activos, Lst.Llave.ToString(), ",");
            }
            return Activos;
        }

        void Guardar(string Activos)
        {
            
            eClockBase.CeC_SesionBase Sesion = CeC_Sesion.ObtenSesion(this);
            eClockBase.Controladores.Sesion CSesion= new eClockBase.Controladores.Sesion(Sesion);
            CSesion.GuardaDatos1aNEvent += CSesion_GuardaDatos1aNEvent;
            CSesion.GuardaDatos1aN(TablaUNOaN, CampoLlaveUNO, ValorLlaveUno, CampoLlaveN, Activos);
        }

        void CSesion_GuardaDatos1aNEvent(int NoModificados)
        {
            if (NoModificados >= 0)
                Cerrar();
        }
        public class Valores1aN
        {
            public string ValoresSeparadosComas { get; set; }
            public Valores1aN()
            {

            }
            public Valores1aN(string Valores)
            {
                ValoresSeparadosComas = Valores;
            }
        }
        private List<Listado> Datos = null;
        private List<Listado> DatosUNOaN = null;
        void ToolBar_OnEventClickToolBar(UC_ToolBar_Control Control)
        {
            switch (Control.Nombre)
            {
                case "Btn_Exportar":
                    {
                        Controles.UC_Exportar Exp = new Controles.UC_Exportar();
                        Exp.JsonValores = JsonConvert.SerializeObject(new Valores1aN(ObtenValores()));
                        UC_Listado.MuestraComoDialogo(this, Exp, Colors.White);
                    }
                    break;
                case "Btn_Importar":
                    {
                        Controles.UC_Importar Imp = new Controles.UC_Importar();
                        Imp.ImportarEvent += delegate(string Datos)
                            {
                                try
                                {
                                    Valores1aN Valores = eClockBase.Controladores.CeC_ZLib.Json2Object<Valores1aN>(Datos);
                                    Guardar(Valores.ValoresSeparadosComas);
                                    return;
                                }
                                catch { }
                                //Mostrar mensaje de error;
                            };
                        UC_Listado.MuestraComoDialogo(this, Imp, Colors.White);
                    }
                    break;

                case "Btn_Regresar":
                    Cerrar();
                    break;
                case "Btn_Guardar":
                    Guardar(ObtenValores());
                    break;
                case "Btn_Actualizar":
                    ActualizaDatos();
                    break;
                case "Btn_MostrarBorrados":
                    m_MostrarBorradosN = true;
                    ActualizaDatos();
                    break;
                case "Btn_DeSeleccionar":
                    {
                        var results = from c in Datos
                                      where c.Seleccionado == true
                                      select c;

                        foreach (var List in results)
                        {
                            List.Seleccionado = false;
                        }
                        ToolBar.Seleccionados = 0;
                        Lst_Datos.Items.Refresh();
                    }
                    break;
                case "Btn_SeleccionarTodos":
                    foreach (var Dato in Datos)
                    {
                        Dato.Seleccionado = true;
                    }
                    ToolBar.Seleccionados = Datos.Count();
                    Lst_Datos.Items.Refresh();
                    break;

                case "Btn_Filtrar":
                    Filtrar(((eClock5.Controles.UC_TextBoxButton)Control.ControlCreado).Texto);
                    break;

            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Texto"></param>
        public void Filtrar(string Texto)
        {
            if (Datos == null)
                return;
            Texto = Texto.ToLower();
            var results = from c in Datos
                          where (c.Nombre != null && c.Nombre.ToString().ToLower().Contains(Texto)) || (c.Descripcion != null && c.Descripcion.ToString().ToLower().Contains(Texto))
                          select c;
            Lst_Datos.ItemsSource = results;
        }
        string ValorLlaveUno = "";
        bool ActualizaDatos()
        {
            try
            {
                Clases.Parametros Param = null;
                string Filtro = "";
                if (CampoLlaveUNO != null && CampoLlaveUNO.Length > 0)
                {
                    Tag = Param = Clases.Parametros.ObtenParametrosPadre(this);
                    try
                    {
                        Newtonsoft.Json.Linq.JContainer Contenedor = (Newtonsoft.Json.Linq.JContainer)Newtonsoft.Json.JsonConvert.DeserializeObject(Param.Parametro.ToString());
                        ValorLlaveUno = eClockBase.CeC.Convierte2String(((Newtonsoft.Json.Linq.JValue)(Contenedor[CampoLlaveUNO])).Value);
                    }
                    catch
                    {
                        ValorLlaveUno = Param.Parametro.ToString();
                    }

                    Filtro = CampoLlaveUNO + " = '" + ValorLlaveUno + "'";
                }

                eClockBase.CeC_SesionBase Sesion = CeC_Sesion.ObtenSesion(this);


                eClockBase.Controladores.Sesion CSesionListadoN = new eClockBase.Controladores.Sesion(Sesion);
                CSesionListadoN.CambioListadoEvent += CSesionListadoN_CambioListadoEvent;
                if (TablaN != null && TablaN.Length > 0
                    && CampoLlaveN != null && CampoLlaveN.Length > 0
                    && CampoNombreN != null && CampoNombreN.Length > 0)
                    CSesionListadoN.ObtenListado(TablaN, CampoLlaveN, CampoNombreN, CampoAdicionalN, CampoDescripcionN, "", MostrarBorradosN, FiltroN);

                eClockBase.Controladores.Sesion CSesionListadoUNOaN = new eClockBase.Controladores.Sesion(Sesion);
                CSesionListadoUNOaN.CambioListadoEvent += CSesionListadoUNOaN_CambioListadoEvent;
                if (TablaUNOaN != null && TablaUNOaN.Length > 0
                    && CampoLlaveN != null && CampoLlaveN.Length > 0)
                    CSesionListadoUNOaN.ObtenListado(TablaUNOaN, CampoLlaveN, "", "", "", "", true, Filtro);

                eClockBase.Controladores.Sesion CSesionListadoUNO = new eClockBase.Controladores.Sesion(Sesion);
                CSesionListadoUNO.CambioListadoEvent += CSesionListadoUNO_CambioListadoEvent;
                if (TablaUNO != null && TablaUNO.Length > 0
                    && CampoLlaveUNO != null && CampoLlaveUNO.Length > 0
                    && CampoNombreUNO != null && CampoNombreUNO.Length > 0)
                    CSesionListadoUNO.ObtenListado(TablaUNO, CampoLlaveUNO, CampoNombreUNO, CampoAdicionalUNO, CampoDescripcionUNO, "", true, Filtro);

                return true;
            }
            catch (Exception ex)
            {
                eClockBase.CeC_Log.AgregaError(ex);
            }
            return false;
        }

        void CSesionListadoUNO_CambioListadoEvent(string Listado)
        {
            List<Listado> ListadoUno = eClockBase.Controladores.CeC_ZLib.Json2Object<List<Listado>>(Listado);
            if (ListadoUno.Count > 0)
            {
                Lbl_Nombre.Text = eClockBase.CeC.Convierte2String(ListadoUno[0].Nombre);
                Lbl_Descripcion.Text = eClockBase.CeC.Convierte2String(ListadoUno[0].Descripcion);
                Lbl_Adicional.Text = eClockBase.CeC.Convierte2String(ListadoUno[0].Adicional);
            }
        }

        void CSesionListadoUNOaN_CambioListadoEvent(string Listado)
        {
            DatosUNOaN = eClockBase.Controladores.CeC_ZLib.Json2Object<List<Listado>>(Listado);
            ActualizaCheckBox();
        }

        void ActualizaCheckBox()
        {
            if (DatosUNOaN == null || Datos == null)
                return;
            foreach (Listado Dato in Datos)
            {
                Dato.Seleccionado = false;
                foreach (Listado DatoUnoAN in DatosUNOaN)
                {
                    if (Dato.Llave.ToString() == DatoUnoAN.Llave.ToString())
                    {
                        Dato.Seleccionado = true;
                        break;
                    }
                }
            }
            Lst_Datos.ItemsSource = Datos;
            Lst_Datos.Items.Refresh();
            CheckBox_Click(this, null);
        }
        void CSesionListadoN_CambioListadoEvent(string Listado)
        {
            try
            {
                Datos = eClockBase.Controladores.CeC_ZLib.Json2Object<List<Listado>>(Listado);
                // Datos[0].Seleccionado = true;
                bool PrimeraVez = Lst_Datos.ItemsSource == null ? true : false;
                Lst_Datos.ItemsSource = Datos;
                if (!PrimeraVez)
                    Lst_Datos.Items.Refresh();

                ActualizaCheckBox();
            }
            catch (Exception ex)
            {
                eClockBase.CeC_Log.AgregaError(ex);
            }
        }
        void UC_Listado_Loaded(object sender, RoutedEventArgs e)
        {
            if (Lst_Datos.ItemsSource == null)
                ActualizaDatos();
        }

        public class Listado : eClockBase.Controladores.ListadoJson
        {
            public bool Seleccionado { get; set; }
        }

        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {


        }

        private void CheckBox_Click_1(object sender, RoutedEventArgs e)
        {

            var results = from c in Datos
                          where c.Seleccionado == true
                          select new { c };
            ToolBar.Seleccionados = results.Count();
        }
        /// <summary>
        /// Regresa un pseudo modelo donde solo se contiene el o los campos llave con su valor
        /// </summary>
        /// <param name="Elemento"></param>
        /// <returns></returns>
        public string ObtenIDLlave(Listado Elemento)
        {
            if (CampoLlaveN == null || CampoLlaveN.Length < 1)
                return "";
            return "{\"" + CampoLlaveN + "\":\"" + Elemento.Llave.ToString() + "\"}";
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            var results = from c in Datos
                          where c.Seleccionado == true
                          select new { c };
            ToolBar.Seleccionados = results.Count();
        }
    }
}
