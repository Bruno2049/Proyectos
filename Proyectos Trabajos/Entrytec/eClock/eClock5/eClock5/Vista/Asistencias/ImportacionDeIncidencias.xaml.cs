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
using Microsoft.Win32;

namespace eClock5.Vista.Asistencias
{
    /// <summary>
    /// Lógica de interacción para ImportacionDeIncidencias.xaml
    /// </summary>
    public partial class ImportacionDeIncidencias : UserControl
    {
        public bool Realizado = false;
        OpenFileDialog m_File = new OpenFileDialog();

        public OpenFileDialog File
        {
            get { return m_File; }
            set { m_File = value; }
        }
        public ImportacionDeIncidencias()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Examinar_Click(object sender, RoutedEventArgs e)
        {
            this.File.Filter = "Archivos de Aceptados |*.txt;*.csv;*.xls;*.xlsx ";
            //dialogo.FilterIndex = 1;
            this.File.ShowDialog();
            Tbx_Examinar.Text = this.File.FileName;

        }
        private void Importar()
        {
            eClockBase.Modelos.Incidencias.Model_Incidencias Incidencia = new eClockBase.Modelos.Incidencias.Model_Incidencias();
            List<eClockBase.Modelos.Incidencias.Model_Incidencias> IncidenciasAgregar = new List<eClockBase.Modelos.Incidencias.Model_Incidencias>();
            //Incidencias.

            if (this.File.CheckFileExists)
            {
                if (System.IO.Path.GetExtension(this.File.FileName) == ".csv" || System.IO.Path.GetExtension(this.File.FileName) == ".txt")
                {
                    try
                    {
                        System.IO.StreamReader StreamR = new System.IO.StreamReader(this.File.FileName);
                        string Linea = "";
                        string DatosJSON = "";
                        int NLinea = 0;
                        do
                        {
                            NLinea++;
                            Linea = StreamR.ReadLine();
                            if (Linea != null && Linea.Length > 22)
                            {
                                string[] Campos = Linea.Split(new char[] { '|' });
                                // Campos[0] Persona_Link_ID
                                // Campos[1] Fecha
                                // Campos[2] Abreviatura
                                // Campos[3] Nombre de incidencia
                                // Campos[4] Comentario
                                if (Campos.Length == 5)
                                {
                                    Incidencia.Persona_Link_ID = eClockBase.CeC.Convierte2Int(Campos[0]);
                                    Incidencia.FInicio = eClockBase.CeC.Convierte2DateTime(Campos[1]);
                                    Incidencia.FFin = eClockBase.CeC.Convierte2DateTime(Campos[1]);
                                    Incidencia.Abreviatura = eClockBase.CeC.Convierte2String(Campos[2]);
                                    Incidencia.Nombre = eClockBase.CeC.Convierte2String(Campos[3]);
                                    Incidencia.Comentario = eClockBase.CeC.Convierte2String(Campos[3]);

                                    IncidenciasAgregar.Add(Incidencia);
                                    Incidencia = new eClockBase.Modelos.Incidencias.Model_Incidencias();
                                }
                            }
                        } while (Linea != null);
                        eClockBase.Controladores.Incidencias ControladorIncidencias = new eClockBase.Controladores.Incidencias(CeC_Sesion.ObtenSesion(this));
                        ControladorIncidencias.CargarIncidenciasEvent += ControladorIncidencias_CargarIncidenciasEvent;
                        ControladorIncidencias.CargarIncidencias(IncidenciasAgregar);
                    }
                    catch (Exception ex)
                    {
                        eClockBase.CeC_Log.AgregaError(ex);
                    }
                }
            }
        }
        private void Btn_Importar_Click(object sender, RoutedEventArgs e)
        {
            Importar();
        }

        void ControladorIncidencias_CargarIncidenciasEvent(bool Guardado)
        {
            if (Guardado)
            {
                Realizado = true;
                this.Visibility = Visibility.Hidden;
            }
            //throw new NotImplementedException();
        }

        private void UC_ToolBar_OnEventClickToolBar(UC_ToolBar_Control Control)
        {
            switch (Control.Nombre)
            {
                case "Btn_Regresar":
                    this.Visibility = Visibility.Hidden;
                    break;
                case "Btn_Guardar":
                    Importar();
                    break;
            }
        }
    }
}