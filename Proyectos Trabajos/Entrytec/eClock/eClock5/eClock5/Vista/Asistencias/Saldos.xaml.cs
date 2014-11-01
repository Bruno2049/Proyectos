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
using eClockBase;

namespace eClock5.Vista.Asistencias
{
    /// <summary>
    /// Lógica de interacción para Saldos.xaml
    /// </summary>
    public partial class Saldos : UserControl
    {
        public Saldos()
        {
            InitializeComponent();
        }

        string Agrupacion = "";
        int PersonaID = -1;
        void ActualizaDatos()
        {
            if (Lvw_Datos == null)
                return;
            Lvw_Datos.ItemsSource = null;
            eClockBase.Controladores.Asistencias Asistencia;
            string Parametros = eClockBase.CeC.Convierte2String(Clases.Parametros.Tag2Parametros(Tag).Parametro);

            if (Parametros == "")
                Agrupacion = "";
            else
                if (Parametros[0] == '|' || Parametros[0] == '@')
                    Agrupacion = Parametros;
                else
                    PersonaID = eClockBase.CeC.Convierte2Int(Parametros);
            


            Asistencia = new eClockBase.Controladores.Asistencias(CeC_Sesion.ObtenSesion(this));
            Asistencia.ObtenAsistenciaTotalesSaldosFinalizado += Asistencia_ObtenAsistenciaTotalesSaldosFinalizado;
            Asistencia.ObtenAsistenciaTotalesSaldos(CeC.Convierte2Bool(Chb_Agrupacion.IsChecked),
                CeC.Convierte2Bool(Chb_Empleado.IsChecked), PersonaID, Agrupacion);
        }
        List<eClockBase.Modelos.Asistencias.Model_AsistenciaTotalesSaldos> Datos = null;
        void Asistencia_ObtenAsistenciaTotalesSaldosFinalizado(List<eClockBase.Modelos.Asistencias.Model_AsistenciaTotalesSaldos> Asistencia)
        {
            try
            {
                Datos = Asistencia;
                bool PrimeraVez = Lvw_Datos.ItemsSource == null ? true : false;
                
                Lvw_Datos.ItemsSource = Asistencia;
                if (!PrimeraVez)
                    Lvw_Datos.Items.Refresh();
            }
            catch (Exception ex)
            {
                CeC_Log.AgregaError(ex);
            }
        }

        private void Tbar_OnEventClickToolBar(UC_ToolBar_Control Control)
        {
            switch (Control.Nombre)
            {
                case "Btn_Regresar":
                    this.Visibility = System.Windows.Visibility.Hidden;
                    break;
                case "Btn_Reportes":
                    {
                        Controles.UC_Reportes Reportes = new Controles.UC_Reportes();
                        Reportes.ParametrosGenerales = new eClockBase.Modelos.Asistencias.Model_Parametros(PersonaID, Agrupacion, DateTime.Now, DateTime.Now);
                        Reportes.Modelos = "eClockBase.Modelos.Asistencias.Model_AsistenciaTotalesSaldos";
                        UC_Listado.MuestraComoDialogo(this, Reportes, Colors.White);
                    }
                    break;
                case "Btn_Actualizar":
                    ActualizaDatos();
                    break;
                case "Btn_Modificar":
                    {
                        SaldosCorreccion Corre = new Vista.Asistencias.SaldosCorreccion();
                        Corre.IsVisibleChanged += delegate(object sender, DependencyPropertyChangedEventArgs e)
                        {
                            if (Corre.Realizado)
                                ActualizaDatos();
                        };
                        Clases.Parametros.MuestraControl(Grid, Corre, new Clases.Parametros(true, ""));
                    }
                    break;
            }
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (eClockBase.CeC.Convierte2Bool(e.NewValue) == true)
            {
                ActualizaDatos();
            }
        }

        private void ActualizaDatos(object sender, RoutedEventArgs e)
        {
            ActualizaDatos();
        }
    }
}
