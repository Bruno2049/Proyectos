using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

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
using Newtonsoft.Json;
using System.Windows.Controls.Primitives;
using System.Threading;

using eClock5;

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
namespace eClock5.Vista.Campos
{
    /// <summary>
    /// Lógica de interacción para ListadoCamposEmpleados.xaml
    /// </summary>
    public partial class ListadoCamposEmpleados : UserControl
    {
        eClock5.Vista.eClock.LiveTiles Lt = new eClock.LiveTiles();
        List<eClockBase.Modelos.Model_CAMPOS_DATOS> CamposDatos = new List<eClockBase.Modelos.Model_CAMPOS_DATOS>();
        eClockBase.Modelos.Model_CAMPOS_DATOS ValoresCampos = new eClockBase.Modelos.Model_CAMPOS_DATOS();
        int TipoPersonaID = 1;
        public ListadoCamposEmpleados()
        {
            InitializeComponent();
            Loaded += ListadoCamposEmpleados_Loaded;
            ToolBar.OnEventClickToolBar += ToolBar_OnEventClickToolBar;
        }

        void ListadoCamposEmpleados_Loaded(object sender, RoutedEventArgs e)
        {
            ActualizaDatos();
        }
        /// <summary>
        /// Se encarga de ejecutar las fucniones de los botones enlazados en la barra.
        /// </summary>
        /// <param name="Control">
        void ToolBar_OnEventClickToolBar(UC_ToolBar_Control Control)
        {
            switch (Control.Nombre)
            {
                case "Btn_Nuevo":
                    eClockBase.Modelos.Model_CAMPOS_DATOS ValoresCampos = new eClockBase.Modelos.Model_CAMPOS_DATOS();
                    CamposDatos.Add(ValoresCampos);
                    Lst_Datos.ItemsSource = CamposDatos;
                    Lst_Datos.Items.Refresh();
                    m_ActualizarCampos = true;
                    break;
                case "Btn_Guardar":
                    GuardarDatos();
                    break;

                case "Btn_Actualizar":
                    //Lst_Datos.Items.Refresh();
                    Lst_Datos.ItemsSource = null;
                    Lst_Datos.Items.Refresh();
                    ActualizaDatos();
                    break;
                case "Btn_DeSeleccionar":
                    Lst_Datos.SelectedIndex = -1;
                    break;
                case "Btn_Subir":
                    {
                        int Pos = Lst_Datos.SelectedIndex;
                        if (Pos > 0)
                        {
                            Pos--;
                            Object Elemento = Lst_Datos.SelectedItem;
                            CamposDatos.Remove((eClockBase.Modelos.Model_CAMPOS_DATOS)Elemento);
                            CamposDatos.Insert(Pos, (eClockBase.Modelos.Model_CAMPOS_DATOS)Elemento);
                            //Lst_Datos.ItemsSource = CamposDatos;
                            Lst_Datos.Items.Refresh();
                        }
                    }
                    break;
                case "Btn_Bajar":
                    {
                        int Pos = Lst_Datos.SelectedIndex;
                        if (Pos < Lst_Datos.Items.Count - 1)
                        {
                            Pos++;
                            Object Elemento = Lst_Datos.SelectedItem;
                            CamposDatos.Remove((eClockBase.Modelos.Model_CAMPOS_DATOS)Elemento);
                            CamposDatos.Insert(Pos, (eClockBase.Modelos.Model_CAMPOS_DATOS)Elemento);
                            //Lst_Datos.ItemsSource = CamposDatos;
                            Lst_Datos.Items.Refresh();
                        }
                    }
                    break;
                case "Btn_Borrar":
                    if (Lst_Datos.SelectedIndex >= 0)
                    {
                        CamposDatos.RemoveAt(Lst_Datos.SelectedIndex);
                        Lst_Datos.Items.Refresh();
                    }
                    break;
            }
        }
        /// <summary>
        /// Fucnion que se encarga de guardar los campos contenidos dentro
        /// del Lst_Datos, tanto Nombre, TipoDato(Combo), Etiqueta, Ayuda, Usabilidad(Combo), Default. 
        /// </summary>
        /// <param name="">
        public void GuardarDatos()
        {
            eClockBase.CeC_SesionBase Sesion = CeC_Sesion.ObtenSesion(this);
            eClockBase.Controladores.Campos CamposEmpleado = new eClockBase.Controladores.Campos(Sesion);

            int LastOrden = 0;
            for (int row = 0; row < Lst_Datos.Items.Count; row++)
            {
                CamposDatos[row].CAMPO_DATO_ORDEN = row;
                CamposDatos[row].SUSCRIPCION_ID = Sesion.SUSCRIPCION_ID_SELECCIONADA;
                CamposDatos[row].TIPO_PERSONA_ID = TipoPersonaID;
                /*
                Lst_Datos.Items[]
                ContentPresenter CP = Clases.CeC_Control.FindVisualChild<ContentPresenter>(Lst_Datos.ItemContainerGenerator.ContainerFromItem(Lst_Datos.Items[row]));
                DataTemplate DataTemplateObj2 = CP.ContentTemplate;
                Controles.UC_Combo CbX = DataTemplateObj2.FindName("Cbx_TIPO_DATO_ID", CP) as Controles.UC_Combo;
                CamposDatos[row].TIPO_DATO_ID = Convert.ToInt32(CbX.Seleccionado);
                CamposDatos[row].TIPO_PERSONA_ID = TipoPersonaID;
                CamposDatos[row].SUSCRIPCION_ID = Sesion.SUSCRIPCION_ID_SELECCIONADA;
                CamposDatos[row].CAMPO_DATO_ORDEN = row;
                CamposEmpleado.GuardarCampos(CamposDatos[row]);*/
            }
            CamposEmpleado.Guardar(CamposDatos);
        }
        /// <summary>
        /// Función que se encarga de traer de la base de datos los datos por medio del
        /// modelo y utilizando el controlador Campos.
        /// </summary>
        /// <param name="Control">
        bool ActualizaDatos()
        {
            try
            {
                eClockBase.CeC_SesionBase Sesion = CeC_Sesion.ObtenSesion(this);
                eClockBase.Controladores.Campos CamposEmpleado = new eClockBase.Controladores.Campos(Sesion);
                CamposEmpleado.ListaCamposFinalizado += CamposEmpleado_ListaCamposFinalizado;
                CamposEmpleado.ListaCampos(TipoPersonaID);
                return true;
            }
            catch (Exception ex)
            {
                eClockBase.CeC_Log.AgregaError(ex);
            }
            return false;
        }
        bool m_ActualizarCampos = false;
        void CamposEmpleado_ListaCamposFinalizado(List<eClockBase.Modelos.Model_CAMPOS_DATOS> lCamposDatos)
        {
            try
            {
                bool primeravez = Lst_Datos.ItemsSource == null ? true : false;

                CamposDatos = lCamposDatos;
                Lst_Datos.ItemsSource = CamposDatos;
                if (primeravez)
                    Lst_Datos.Items.Refresh();

                m_ActualizarCampos = true;


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

        private void Lst_Datos_LayoutUpdated(object sender, EventArgs e)
        {
            if (!m_ActualizarCampos)
                //Lt.primeravez = true;
                return;
            m_ActualizarCampos = false;
            DisableFields();

        }

        public void DisableFields()
        {
            if (Lst_Datos.Items[0] != null)
            {
                ContentPresenter CP = Clases.CeC_Control.FindVisualChild<ContentPresenter>(Lst_Datos.ItemContainerGenerator.ContainerFromItem(Lst_Datos.Items[0]));
                ContentPresenter CP1 = Clases.CeC_Control.FindVisualChild<ContentPresenter>(Lst_Datos.ItemContainerGenerator.ContainerFromItem(Lst_Datos.Items[1]));

                //ContentPresenter CP = Clases.CeC_Control.FindVisualChild<ContentPresenter>(Lst_Datos.ItemContainerGenerator.ContainerFromItem(Lst_Datos.Items[0]));
                if (CP != null)
                {
                    DataTemplate DataTemplateObj = CP.ContentTemplate;
                    TextBox Tbx = DataTemplateObj.FindName("Tbx_CAMPO_DATO_NOMBRE", CP) as TextBox;
                    Controles.UC_Combo CbX = DataTemplateObj.FindName("Cbx_TIPO_DATO_ID", CP) as Controles.UC_Combo;
                    if (Tbx.Text == "PERSONA_LINK_ID")
                    {
                        Tbx.IsEnabled = false;
                        CbX.Seleccionado = 2;
                        CbX.IsEnabled = false;
                    }

                    Tbx = DataTemplateObj.FindName("Tbx_CAMPO_DATO_NOMBRE", CP1) as TextBox;
                    CbX = DataTemplateObj.FindName("Cbx_TIPO_DATO_ID", CP1) as Controles.UC_Combo;
                    if (Tbx.Text == "PERSONA_EMAIL")
                    {
                        Tbx.IsEnabled = false;
                        CbX.Seleccionado = 1;
                        CbX.IsEnabled = false;
                    }

                }
            }
        }

        private void Lst_Datos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ToolBar.Seleccionados = Lst_Datos.SelectedItems.Count;
        }




    }
}
