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
using System.Windows.Threading;

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

namespace eClock5.Vista.eClock
{
    /// <summary>
    /// Lógica de interacción para LiveTiles.xaml
    /// </summary>
    public partial class LiveTiles : UserControl
    {
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        int i = 0;
        public LiveTiles()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            Btn_Migrar.Visibility = System.Windows.Visibility.Hidden;
            Btn_Importar.Visibility = System.Windows.Visibility.Hidden;
            Btn_AsignacionTurnos.Visibility = System.Windows.Visibility.Hidden;
            Btn_Campos.Visibility = System.Windows.Visibility.Hidden;
            Btn_Turnos.Visibility = System.Windows.Visibility.Hidden;
            Btn_DiasFestivos.Visibility = System.Windows.Visibility.Hidden;
            Btn_Checadores.Visibility = System.Windows.Visibility.Hidden;
            Btn_Empleados.Visibility = System.Windows.Visibility.Hidden;
            Btn_AsistenciaJustificaciones.Visibility = System.Windows.Visibility.Hidden;
            Tiempo();
            

        }

        public void Tiempo()
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);            
            dispatcherTimer.Start();
            
            
        }

        

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            i = i + 1;
            switch (i)
            {
                case 1:
                    if (Btn_Migrar.IsVisible == false)
                    {
                        Btn_Migrar.Visibility = System.Windows.Visibility.Visible;
                    }   
                    break;
                case 2:
                    if (Btn_Campos.IsVisible == false)
                    {
                        Btn_Campos.Visibility = System.Windows.Visibility.Visible;
                    }
                    break;
                case 3:
                    if (Btn_Checadores.IsVisible == false)
                    {
                        Btn_Checadores.Visibility = System.Windows.Visibility.Visible;
                    }
                    break;
                case 4:
                    if (Btn_Importar.IsVisible == false)
                    {
                        Btn_Importar.Visibility = System.Windows.Visibility.Visible;
                    }
                    break;
                case 5:
                    if (Btn_Turnos.IsVisible == false)
                    {
                        Btn_Turnos.Visibility = System.Windows.Visibility.Visible;
                    }
                    break;
                case 6:
                    if (Btn_Empleados.IsVisible == false)
                    {
                        Btn_Empleados.Visibility = System.Windows.Visibility.Visible;
                    }
                    break;
                case 7:
                    if (Btn_AsignacionTurnos.IsVisible == false)
                    {
                        Btn_AsignacionTurnos.Visibility = System.Windows.Visibility.Visible;
                    }
                    break;
                case 8:
                    if (Btn_DiasFestivos.IsVisible == false)
                    {
                        Btn_DiasFestivos.Visibility = System.Windows.Visibility.Visible;
                    }
                    break;
                case 9:
                    if (Btn_AsistenciaJustificaciones.IsVisible == false)
                    {
                        Btn_AsistenciaJustificaciones.Visibility = System.Windows.Visibility.Visible;
                    }
                    break;
                case 10:
                    return;
                    break;
                default:                    
                    break;
            }

                             
            

            
                
                
           
        }



        private void UC_Btn_LiveTile_Loaded_1(object sender, RoutedEventArgs e)
        {            
                
        }
        
        
      
        private void Grd_LiveTiles_Loaded(object sender, RoutedEventArgs e)
        {
            //Btn_Migrar.Visibility = System.Windows.Visibility.Hidden;
            //Btn_Importar.Visibility = System.Windows.Visibility.Hidden;
            //Btn_AsignacionTurnos.Visibility = System.Windows.Visibility.Hidden;
            //Btn_Campos.Visibility = System.Windows.Visibility.Hidden;
            //Btn_Turnos.Visibility = System.Windows.Visibility.Hidden;
            //Btn_DiasFestivos.Visibility = System.Windows.Visibility.Hidden;
            //Btn_Checadores.Visibility = System.Windows.Visibility.Hidden;
            //Btn_Empleados.Visibility = System.Windows.Visibility.Hidden;
            //Btn_AsistenciaJustificaciones.Visibility = System.Windows.Visibility.Hidden;
            //  MostrarLiveTiles();      
        }

        private void Btn_Campos_Click(object sender, RoutedEventArgs e)
        {
            UC_Listado.MuestraComoDialogo(this, new Vista.Campos.ListadoCamposEmpleados(), Colors.White);
        }

        private void Btn_Checadores_Click(object sender, RoutedEventArgs e)
        {
            UC_Listado.MuestraComoDialogo(this, new Vista.Terminales.ListadoTerminales(), Colors.White);
        }

        private void Btn_Importar_Click(object sender, RoutedEventArgs e)
        {
            UC_Listado.MuestraComoDialogo(this, new Vista.Empleados.Importar(), Colors.White);
        }

        private void Btn_Empleados_Click(object sender, RoutedEventArgs e)
        {
            UC_Listado.MuestraComoDialogo(this, new Vista.Empleados.ListadoPersonas(), Colors.White);
        }

        private void Btn_Turnos_Click(object sender, RoutedEventArgs e)
        {
            UC_Listado.MuestraComoDialogo(this, new Vista.Turnos.Listado(), Colors.White);
        }

        private void Btn_AsignacionTurnos_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_AsignacionTurnos_Click(object sender, RoutedEventArgs e)
        {
            UC_Listado.MuestraComoDialogo(this,new Vista.Turnos.AsignacionPorFechas(),Colors.White);
        }

        private void Btn_DiasFestivos_Click(object sender, RoutedEventArgs e)
        {
            UC_Listado.MuestraComoDialogo(this, new Vista.DiasFestivos.ListadoCalendariosDF(), Colors.White);
        }

        private void Btn_AsistenciaJustificaciones_Click(object sender, RoutedEventArgs e)
        {
            UC_Listado.MuestraComoDialogo(this, new Vista.Asistencias.Listado(), Colors.White);
        }
    }
}
