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

namespace eClock5.Vista.Periodos
{
    /// <summary>
    /// Lógica de interacción para Abiertos.xaml
    /// </summary>
    public partial class Abiertos : UserControl
    {
        public Abiertos()
        {
            InitializeComponent();
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(eClockBase.CeC.Convierte2Bool(e.NewValue))
                ActualizaDatos();
        }
        private void ActualizaDatos()
        {
            eClockBase.CeC_SesionBase Sesion =  CeC_Sesion.ObtenSesion(this);
            eClockBase.Controladores.Periodos Per = new eClockBase.Controladores.Periodos(Sesion);
            Per.ObtenListadoFinalizado += Per_ObtenListadoFinalizado;
            Per.ObtenListado(DateTime.Now.AddMonths(-2), DateTime.Now, 0);
        }

        void Per_ObtenListadoFinalizado(List<eClockBase.Modelos.Periodos.Model_Listado> Listado)
        {
            List<UC_Listado.Listado> ListadoResumen = new List<UC_Listado.Listado>();
            if(Listado != null)
                foreach (eClockBase.Modelos.Periodos.Model_Listado Lst in Listado)
                {
                    UC_Listado.Listado LstResumen = new UC_Listado.Listado();
                    LstResumen.Llave = Lst.PERIODO_ID;
                    LstResumen.Nombre = Lst.TIPO_NOMINA_NOMBRE + " (" + Lst.PERIODO_NO + ")";
                    LstResumen.Descripcion = "Nomina (" + Lst.PERIODO_NOM_INICIO.ToShortDateString() + "-" + Lst.PERIODO_NOM_FIN.ToShortDateString() + "), Asistencia (" +
                         Lst.PERIODO_ASIS_INICIO.ToShortDateString() + "-" + Lst.PERIODO_ASIS_FIN.ToShortDateString() + ")";
                    LstResumen.Adicional = Lst.PERIODO_ID;
                    ListadoResumen.Add(LstResumen);
                }
            Lst_Datos.CambiarListado(ListadoResumen);
        }


    }
}
