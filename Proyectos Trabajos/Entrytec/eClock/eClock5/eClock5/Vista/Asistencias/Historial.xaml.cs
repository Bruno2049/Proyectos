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

namespace eClock5.Vista.Asistencias
{
    /// <summary>
    /// Lógica de interacción para Historial.xaml
    /// </summary>
    public partial class Historial : UserControl
    {
        public Historial()
        {
            InitializeComponent();
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
                        Reportes.ParametrosGenerales = new eClockBase.Modelos.Asistencias.Model_Parametros(PersonaID, Agrupacion, RangoFechas.FechaInicio, RangoFechas.FechaFin.AddDays(1));
                        Reportes.Modelos = "eClockBase.Modelos.Asistencias.Model_AsistenciaTotalesSaldos,eClockBase.Modelos.Incidencias.Model_Historial";
                        UC_Listado.MuestraComoDialogo(this, Reportes, Colors.White);
                    }
                    break;
                case "Btn_Actualizar":
                    ActualizaDatos();
                    break;
                case "Btn_DeSeleccionar":
                    {
                        foreach (Model_Historial Lista in Datos)
                        {
                            if (Lista.Seleccionado)
                                Lista.Seleccionado = false;
                        }
                        Lvw_Datos.Items.Refresh();
                        TbarFP.Seleccionados = 0;
                    }
                    break;

                case "Btn_Revertir":
                    {
                        Corregir();
                    }
                    break;
            }
        }

        private void Corregir()
        {
            eClockBase.Controladores.Incidencias cInc = new eClockBase.Controladores.Incidencias(CeC_Sesion.ObtenSesion(this));
            foreach (Model_Historial Lst in Datos)
            {
                if (Lst.Seleccionado == true)
                {
                    cInc.CorrigeMovimientoInventario(Lst.ALMACEN_INC_ID);
                }
            }
        }

        string Agrupacion = "";
        int PersonaID = -1;

        public void ActualizaDatos()
        {
            if (Lvw_Datos == null)
                return;
            Lvw_Datos.ItemsSource = null;
            eClockBase.Controladores.Incidencias cIncidencias;
            string Parametros = eClockBase.CeC.Convierte2String(Clases.Parametros.Tag2Parametros(Tag).Parametro);

            if (Parametros == "")
                Agrupacion = "";
            else
                if (Parametros[0] == '|' || Parametros[0] == '@')
                    Agrupacion = Parametros;
                else
                    PersonaID = eClockBase.CeC.Convierte2Int(Parametros);


            cIncidencias = new eClockBase.Controladores.Incidencias(CeC_Sesion.ObtenSesion(this));
            cIncidencias.ObtenerHistorialFinalizado += cIncidencias_ObtenerHistorialFinalizado;
            cIncidencias.ObtenerHistorial(PersonaID, Agrupacion, RangoFechas.FechaInicio, RangoFechas.FechaFin.AddDays(1), "");
        }
        
        List<Model_Historial> Datos = null;
        void cIncidencias_ObtenerHistorialFinalizado(List<eClockBase.Modelos.Incidencias.Model_Historial> Historial)
        {
            try
            {
                string Json = eClockBase.Controladores.CeC_ZLib.Object2Json(Historial);

                Datos = eClockBase.Controladores.CeC_ZLib.Json2Object<List<Model_Historial>>(Json);
                bool PrimeraVez = Lvw_Datos.ItemsSource == null ? true : false;

                Lvw_Datos.ItemsSource = Datos;
                if (!PrimeraVez)
                    Lvw_Datos.Items.Refresh();
            }
            catch (Exception ex)
            {
                eClockBase.CeC_Log.AgregaError(ex);
            }
        }

        private void RangoFechas_CambioFechaEvent(bool Cargando)
        {
            if (!Cargando)
                ActualizaDatos();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CuentaSeleccionados();
        }

        int CuentaSeleccionados()
        {
            var results = from c in Datos
                          where c.Seleccionado == true
                          select new { c };
            int R = TbarFP.Seleccionados = results.Count();
            return R;
        }

        public class Model_Historial : eClockBase.Modelos.Incidencias.Model_Historial
        {
            public bool Seleccionado { get; set; }

        }
    }
}
