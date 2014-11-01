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
using Newtonsoft.Json;
namespace eClock5.Vista.Terminales
{
    /// <summary>
    /// Lógica de interacción para ListadoSimple.xaml
    /// </summary>
    public partial class ListadoSimple : UserControl
    {
        /*public class Terminales
        {
            public
        }*/
        public ListadoSimple()
        {
            InitializeComponent();

        }


        public int Sitio_ID = -1;
        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (eClockBase.CeC.Convierte2Bool(e.NewValue))
                ActualizaDatos();
        }

        public bool ActualizaDatos()
        {
            try
            {
                //Lst_Datos.Items.Clear();
                Lst_Datos.ItemsSource = null;
                eClockBase.CeC_SesionBase Sesion = CeC_Sesion.ObtenSesion(this);


                eClockBase.Controladores.Sesion CSesion = new eClockBase.Controladores.Sesion(Sesion);
                eClockBase.Modelos.Model_TERMINALES Terminal = new eClockBase.Modelos.Model_TERMINALES();
                CSesion.ObtenDatosEvent += CSesion_ObtenDatosEvent;
                if (Sitio_ID > 0)
                {
                    Terminal.SITIO_ID = Sitio_ID;
                    CSesion.ObtenDatos("EC_TERMINALES", "SITIO_ID", Terminal,"","TERMINAL_BORRADO = 0");
                }
                else
                    CSesion.ObtenDatos("EC_TERMINALES", "", Terminal, "", "TERMINAL_BORRADO = 0");
                return true;
            }
            catch (Exception ex)
            {
                eClockBase.CeC_Log.AgregaError(ex);
            }
            return false;
        }
        List<eClockBase.Modelos.Model_TERMINALES> Terminales = null;
        void CSesion_ObtenDatosEvent(int Resultado, string Datos)
        {
            TerminalesABorrar = new List<eClockBase.Modelos.Model_TERMINALES>();
            if (Resultado > 0)
            {
                Datos = eClockBase.CeC.Json2JsonList(Datos);
                Terminales = eClockBase.Controladores.CeC_ZLib.Json2Object<List<eClockBase.Modelos.Model_TERMINALES>>(Datos);
            }
            else
                Terminales = null;

            bool PrimeraVez = Lst_Datos.ItemsSource == null ? true : false;
            Lst_Datos.ItemsSource = Terminales;
            if (!PrimeraVez)
                Lst_Datos.Items.Refresh();
        }
        List<eClockBase.Modelos.Model_TERMINALES> TerminalesABorrar = new List<eClockBase.Modelos.Model_TERMINALES>();
        private void ToolBar_OnEventClickToolBar_1(UC_ToolBar_Control Control)
        {
            switch (Control.Nombre)            
            {
                case "Btn_Regresar":
                    this.Visibility = System.Windows.Visibility.Hidden;
                    break;
                case "Btn_Nuevo":
                    {
                        eClockBase.Modelos.Model_TERMINALES Terminal = new eClockBase.Modelos.Model_TERMINALES();
                        if(Sitio_ID > 0)
                            Terminal.SITIO_ID = Sitio_ID;
                        Terminales.Add(Terminal);
                        Lst_Datos.ItemsSource = Terminales;
                        Lst_Datos.Items.Refresh();
                    }
                    break;
                case "Btn_Borrar":
                    {
                        eClockBase.Modelos.Model_TERMINALES Terminal = Terminales[Lst_Datos.SelectedIndex];
                        if (Terminal.TERMINAL_ID > 0)
                            TerminalesABorrar.Add(Terminal);
                        Terminales.RemoveAt(Lst_Datos.SelectedIndex);
                        Lst_Datos.ItemsSource = Terminales;
                        Lst_Datos.Items.Refresh();
                    }
                    break;
                case "Btn_Guardar":
                    Guardar();
                    break;
            }
        }
        public delegate void GuardarArgs(bool Guardados);
        public event GuardarArgs GuardarEvent;

        public void Guardar()
        {
            eClockBase.Controladores.Terminales cTerminales = new eClockBase.Controladores.Terminales(CeC_Sesion.ObtenSesion(this));
            cTerminales.GuardadoEvent += cTerminales_GuardadoEvent;
            
            foreach (eClockBase.Modelos.Model_TERMINALES Terminal  in TerminalesABorrar)
            {
                Terminal.TERMINAL_BORRADO = true;
                Terminales.Add(Terminal);
            }
            cTerminales.Guardar(Terminales);
        }
        void cTerminales_GuardadoEvent(bool Guardado)
        {
            string G = "";
            G = Guardado.ToString();
            if (GuardarEvent != null)
                GuardarEvent(Guardado);
        }

        private void Lst_Datos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ToolBar.Seleccionados = Lst_Datos.SelectedItems.Count;
        }



    }
}
