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

using eClock5.BaseModificada;
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
using Newtonsoft.Json;
using eClockBase.Controladores;



namespace eClock5.Vista.Campos
{
    /// <summary>
    /// Lógica de interacción para ReagruparEmpleados.xaml
    /// </summary>
    public partial class ReagruparEmpleados : UserControl
    {

        eClockBase.CeC_SesionBase m_SesionBase = null;
        List<ListadoJson> m_Listado = new List<ListadoJson>();
        public ReagruparEmpleados()
        {
            InitializeComponent();
        }

        void Tbar_OnEventClickToolBar(UC_ToolBar_Control Control)
        {

            switch (Control.Nombre)
            {
                case "Btn_Regresar":
                    Cerrar();
                    break;
                case "Btn_Borrar":
                    //Borrar();
                    break;
                case "Btn_Guardar":
                    {
                        m_SesionBase = CeC_Sesion.ObtenSesion(this);
                        eClockBase.Controladores.Personas Ps = new eClockBase.Controladores.Personas(m_SesionBase);
                        Ps.EmpleadosReagrupadosEvent += Ps_EmpleadosReagrupadosEvent;
                        string Elementos = "";
                        foreach (ListadoJson Elemento in m_Listado)
                        {

                            Elementos = eClockBase.CeC.AgregaSeparador(Elementos, Elemento.Llave.ToString(), ",");
                        }
                        Ps.ReagruparEmpleados(Elementos);

                    }
                    break;

            }
        }

        void Ps_EmpleadosReagrupadosEvent(bool Reagrupados)
        {
            if (Reagrupados)
            {
                eClock5.BaseModificada.Localizaciones.sLocaliza(Window.GetWindow(this));
            }
            m_SesionBase.MuestraMensaje("Finalizado",3);
        }
        // bool
        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            m_SesionBase = CeC_Sesion.ObtenSesion(this);
            eClockBase.Controladores.Personas Ps = new eClockBase.Controladores.Personas(m_SesionBase);
            Ps.AgrupacionObtenidaEvent += Ps_AgrupacionObtenidaEvent;
            Ps.ObtenAgrupacionEmpleados();
        }
        string m_Valores = "";
        void MuestraCampos()
        {
            if (!Cargado)
            {
                Cargado = true;
                return;
            }
            m_Listado.Clear();
            string[] sValores = CeC.ObtenArregoSeparador(m_Valores, ",");
            foreach (string sValor in sValores)
            {
                foreach (ListadoJson Listado in Cmb_Campos.m_Listado)
                {
                    if (Listado.Llave.ToString() == sValor)
                    {
                        m_Listado.Add(Listado);
                        break;
                    }
                }
            }
            Lst_Orden.ItemsSource = m_Listado;
            Lst_Orden.Items.Refresh();
        }
        bool Cargado = false;
        private void Cmb_Campos_DatosActualizados()
        {
            MuestraCampos();
        }

        void Ps_AgrupacionObtenidaEvent(string Valores)
        {
            m_Valores = Valores;
            MuestraCampos();
        }

        void Cerrar()
        {
            Clases.Parametros.ObtenPadre(this).Visibility = Visibility.Hidden;
        }

        private void Btn_Agregar_Click(object sender, RoutedEventArgs e)
        {
            if (Cmb_Campos.Seleccionado != null)
                if (!m_Listado.Contains(Cmb_Campos.SelectedItem))
                    m_Listado.Add(Cmb_Campos.SelectedItem);

            Lst_Orden.ItemsSource = m_Listado;
            Lst_Orden.Items.Refresh();
        }

        private void Btn_Quitar_Click(object sender, RoutedEventArgs e)
        {
            int num = Lst_Orden.SelectedItems.Count;
            for (int a = 0; a < num; a++)
            {
                ListadoJson item = (ListadoJson)Lst_Orden.SelectedItems[a];
                m_Listado.Remove(item);
            }

            Lst_Orden.ItemsSource = m_Listado;
            Lst_Orden.Items.Refresh();
        }




    }
}
