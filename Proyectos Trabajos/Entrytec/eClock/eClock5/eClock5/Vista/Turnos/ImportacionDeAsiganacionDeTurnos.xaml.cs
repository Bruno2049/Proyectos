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
using System.Windows.Forms;
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

namespace eClock5.Vista.Turnos
{
    /// <summary>
    /// Lógica de interacción para ImportacionDeAsiganacionDeTurnos.xaml
    /// </summary>
    public partial class ImportacionDeAsiganacionDeTurnos : System.Windows.Controls.UserControl
    {
        OpenFileDialog m_File = new OpenFileDialog();

        public OpenFileDialog File
        {
            get { return m_File; }
            set { m_File = value; }
        }
        public ImportacionDeAsiganacionDeTurnos()
        {
            InitializeComponent();
        }

        private void Btn_Examinar_Click(object sender, RoutedEventArgs e)
        {
            this.File.Filter = "Archivos de Aceptados |*.txt;*.csv;*.xls;*.xlsx ";
            //dialogo.FilterIndex = 1;
            this.File.ShowDialog();
            Tbx_Examinar.Text = this.File.FileName;
        }
        /// <summary>
        /// Control que contiene las acciones a realizar para el turno.
        /// </summary>
        /// <param name="Control">Control en pantalla</param>
        private void UC_ToolBar_OnEventClickToolBar(UC_ToolBar_Control Control)
        {
            switch (Control.Nombre)
            {
                case "Btn_Guardar":
                    {
                        Importar();
                    }
                    break;
                case "Btn_Regresar":
                    this.Visibility = System.Windows.Visibility.Hidden;
                    break;
            }

        }
        public void Importar()
        {
            eClockBase.Modelos.Turnos.Model_TurnoImportacion TurnoImportacion = new eClockBase.Modelos.Turnos.Model_TurnoImportacion();
            List<eClockBase.Modelos.Turnos.Model_TurnoImportacion> TurnosImportacion = new List<eClockBase.Modelos.Turnos.Model_TurnoImportacion>();

            if (this.File.CheckFileExists)
            {
                if (System.IO.Path.GetExtension(this.File.FileName) == ".csv" || System.IO.Path.GetExtension(this.File.FileName) == ".txt")
                {
                    try
                    {
                        System.IO.StreamReader StreamR = new System.IO.StreamReader(this.File.FileName);
                        string Linea = "";
                        int NLinea = 0;
                        do
                        {
                            NLinea++;
                            Linea = StreamR.ReadLine();
                            if (Linea != null && Linea.Length > 22)
                            {
                                string[] Campos = Linea.Split(new char[] { ',' });
                                // Campos[0] Persona_Link_ID
                                // Campos[1] Nombre del Turno
                                // Campos[2] FechaInicial
                                // Campos[3] FechaFinal
                                if (Campos.Length == 4)
                                {
                                    TurnoImportacion.PERSONA_LINK_ID = eClockBase.CeC.Convierte2Int(Campos[0]);
                                    TurnoImportacion.TURNO_NOMBRE = eClockBase.CeC.Convierte2String(Campos[1]);
                                    TurnoImportacion.FECHA_INICIAL = eClockBase.CeC.Convierte2DateTime(Campos[2]);
                                    TurnoImportacion.FECHA_FINAL = eClockBase.CeC.Convierte2DateTime(Campos[3]);

                                    TurnosImportacion.Add(TurnoImportacion);
                                    TurnoImportacion = new eClockBase.Modelos.Turnos.Model_TurnoImportacion();
                                }
                                else
                                {
                                    eClockBase.CeC_Log.AgregaError("Faltan datos en la linea " + NLinea + " del archivo: " + this.File.FileName);
                                }
                            }
                        } while (Linea != null);
                        eClockBase.Controladores.Turnos ControladorTurnos = new eClockBase.Controladores.Turnos(CeC_Sesion.ObtenSesion(this));
                        ControladorTurnos.AsignadoTurnoEvent += ControladorTurnos_AsignadoTurnoEvent;
                        ControladorTurnos.AsignarTurno(TurnosImportacion);
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
   
        }

        void ControladorTurnos_AsignadoTurnoEvent(bool Guardado)
        {
            //throw new NotImplementedException();
        }
    }
}
