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
namespace eClock5.Vista.Empleados
{
    /// <summary>
    /// Lógica de interacción para Supervisores.xaml
    /// </summary>
    public partial class Supervisores : UserControl
    {
        public Supervisores()
        {
            InitializeComponent();
        }
        eClockBase.CeC_SesionBase Sesion;
        string Agrupacion;
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Sesion = CeC_Sesion.ObtenSesion(this);
            Agrupacion = eClockBase.CeC.Convierte2String(Clases.Parametros.Tag2Parametros(Tag).Parametro);
            Actualiza();
        }
        void Actualiza()
        {
            if (Sesion == null)
                return;
            eClockBase.Controladores.Personas Persona = new eClockBase.Controladores.Personas(Sesion);
            Persona.ObtenPermisosAgrupacionEvent += Persona_ObtenPermisosAgrupacionEvent;
            Persona.ObtenPermisosAgrupacion(Agrupacion);
        }
        void Persona_ObtenPermisosAgrupacionEvent(string ListadoPermisos)
        {
            Lst_Main.CambiarListado(ListadoPermisos);
        }

        private void Btn_Otorgar_Click(object sender, RoutedEventArgs e)
        {
            eClockBase.Controladores.Personas Persona = new eClockBase.Controladores.Personas(Sesion);
            Persona.AsignaPermisoAgrupacionEvent += Persona_AsignaPermisoAgrupacionEvent;
            Persona.AsignaPermisoAgrupacion(Tbx_Usuario.Text, Agrupacion, Cmb_Permisos.SeleccionadoInt);
        }

        void Persona_AsignaPermisoAgrupacionEvent(int Resultado)
        {
            Actualiza();
        }

        private void Btn_Quitar_Click(object sender, RoutedEventArgs e)
        {
            List<UC_Listado.Listado> Datos = Lst_Main.DatosSeleccionados;

            foreach (UC_Listado.Listado Dato in Datos)
            {
                eClockBase.Controladores.Personas Persona = new eClockBase.Controladores.Personas(Sesion);
                Persona.QuitaPermisoAgrupacionEvent += delegate(int Resultado)
                {
                    if(Resultado > 0)
                        Lst_Main.QuitarElemento(Dato);
                };
                Persona.QuitaPermisoAgrupacion(eClockBase.CeC.Convierte2Int(Dato.Llave));
            }
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue.Equals(true))
            {

                Actualiza();
            }
        }

    }
}
