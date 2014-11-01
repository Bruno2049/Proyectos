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
using System.Collections;
using System.IO;
namespace eClock5.Vista.Nomina
{
    /// <summary>
    /// Lógica de interacción para Importar.xaml
    /// </summary>
    public partial class ImportarRecNomina : UserControl
    {
        Clases.Interfaces.Interfaz Interfaz = new Clases.Interfaces.Interfaz();
        public ImportarRecNomina()
        {
            InitializeComponent();
        }
        eClockBase.CeC_SesionBase Sesion;
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //Obtiene la sesion Actual
            Sesion = CeC_Sesion.ObtenSesion(this);
            Tbx_Ano.Text = DateTime.Now.Year.ToString();
            Interfaz.CargaParametrosEvent += Interfaz_CargaParametrosEvent;
            Interfaz.CargaParametros(this);
            Lbl_eRed.Visibility = System.Windows.Visibility.Collapsed;
            Mostrar(false, false);
        }

        void Interfaz_CargaParametrosEvent(bool Cargados)
        {
            if (Cargados)
                Lbl_Cargando.Visibility = System.Windows.Visibility.Collapsed;
            if (Interfaz.Parametros.SISTEMA_NOMINA_ID == 0)
                Lbl_SinNomina.Visibility = System.Windows.Visibility.Visible;
            else
                Lbl_SinNomina.Visibility = System.Windows.Visibility.Collapsed;
        }
        public string TipoNominaEx()
        {
            if (Cmb_NominaID.SelectedItem == null || Cmb_NominaID.SelectedItem.Adicional == null)
                return "";
            return Cmb_NominaID.SelectedItem.Adicional.ToString();
        }
        void Mostrar(bool NoImportar, bool SinDatos)
        {
            if (NoImportar)
                Lbl_NoImportar.Visibility = System.Windows.Visibility.Visible;
            else
                Lbl_NoImportar.Visibility = System.Windows.Visibility.Collapsed;

            if (SinDatos)
                Lbl_SinDatos.Visibility = System.Windows.Visibility.Visible;
            else
                Lbl_SinDatos.Visibility = System.Windows.Visibility.Collapsed;
        }

        /// <summary>
        /// Returns file names from given folder that comply to given filters
        /// </summary>
        /// <param name="SourceFolder">Folder with files to retrieve</param>
        /// <param name="Filter">Multiple file filters separated by | character</param>
        /// <param name="searchOption">File.IO.SearchOption, 
        /// could be AllDirectories or TopDirectoryOnly</param>
        /// <returns>Array of FileInfo objects that presents collection of file names that 
        /// meet given filter</returns>
        public string[] getFiles(string SourceFolder, string Filter,
         System.IO.SearchOption searchOption)
        {
            // ArrayList will hold all file names
            ArrayList alFiles = new ArrayList();

            // Create an array of filter string
            string[] MultipleFilters = Filter.Split('|');

            // for each filter find mathing file names
            foreach (string FileFilter in MultipleFilters)
            {
                // add found file names to array list
                alFiles.AddRange(Directory.GetFiles(SourceFolder, FileFilter, searchOption));
            }

            // returns string array of relevant file names
            return (string[])alFiles.ToArray(typeof(string));
        }


        List<eClockBase.Modelos.Nomina.Model_RecNominasPDFyXML> RecNominas;
        int ReciboAImportar = 0;
        private void UC_ToolBar_OnEventClickToolBar(UC_ToolBar_Control Control)
        {
            switch (Control.Nombre)
            {
                case "Btn_Regresar":
                    Cerrar();
                    break;
                case "Btn_Guardar":
                    if (Lbl_eAnoValido.IsVisible == false &&
                        Lbl_Cargando.IsVisible == false &&
                        Lbl_ePeriodoValido.IsVisible == false &&
                        Lbl_SelNomina.IsVisible == false &&
                        Lbl_SinNomina.IsVisible == false &&
                        Lbl_eSelCarpeta.IsVisible == false)
                    {
                        Mostrar(false, false);

                        int NominaID = Cmb_NominaID.SeleccionadoInt;

                        string[] Archivos = getFiles(Lbl_Carpeta.Text, "*.pdf|*.xml", SearchOption.TopDirectoryOnly);

                        RecNominas = new List<eClockBase.Modelos.Nomina.Model_RecNominasPDFyXML>();

                        foreach (string Archivo in Archivos)
                        {
                            string sPersonaLinkID = System.IO.Path.GetFileNameWithoutExtension(Archivo);
                            int iPersonaLinkID = eClockBase.CeC.Convierte2Int(sPersonaLinkID);
                            if (iPersonaLinkID > 0)
                            {
                                eClockBase.Modelos.Nomina.Model_RecNominasPDFyXML RecNomina = null;
                                foreach (eClockBase.Modelos.Nomina.Model_RecNominasPDFyXML ExistenteRecNomina in RecNominas)
                                {
                                    if (ExistenteRecNomina.PERSONA_LINK_ID == iPersonaLinkID)
                                    {
                                        RecNomina = ExistenteRecNomina;
                                        break;
                                    }
                                }

                                if (RecNomina == null)
                                {
                                    RecNomina = new eClockBase.Modelos.Nomina.Model_RecNominasPDFyXML();
                                    RecNomina.PERSONA_LINK_ID = iPersonaLinkID;
                                    RecNomina.NOMINA_ID = NominaID;
                                    RecNominas.Add(RecNomina);
                                }
                                if (System.IO.Path.GetExtension(Archivo).ToUpper() == ".PDF")
                                    RecNomina.REC_NOMINA_PDF = System.IO.File.ReadAllBytes(Archivo);
                                else
                                    RecNomina.REC_NOMINA_XML = System.IO.File.ReadAllBytes(Archivo);
                            }
                            
                            //System.Threading.Thread.Sleep(1000);
                        }

                        Pbr_Recibos.Minimum = 0;
                        Pbr_Recibos.Maximum = RecNominas.Count;
                        Lbl_eRed.Visibility = System.Windows.Visibility.Collapsed;
                        //Inicializa El recibo a importar
                        ReciboAImportar = 0;
                        Importa();

                        break;
                    }

                    break;
                case "Btn_Archivo":
                    {
                        try
                        {
                            Mostrar(false, false);

                            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

                            // Set filter for file extension and default file extension
                            dlg.DefaultExt = ".imp";
                            dlg.Filter = "Imp files (.imp)|*.imp|Json files (.json)|*.json|All files (*.*)|*.*";

                            // Display OpenFileDialog by calling ShowDialog method
                            Nullable<bool> result = dlg.ShowDialog();

                            // Get the selected file name and display in a TextBox
                            if (result == true)
                            {
                                string Modelo = System.IO.Path.GetFileNameWithoutExtension(dlg.FileName);
                                string Datos = System.IO.File.ReadAllText(dlg.FileName);
                                eClockBase.Controladores.Sesion cSesion = new eClockBase.Controladores.Sesion(CeC_Sesion.ObtenSesion(this));
                                cSesion.ImportarEvent += cSesion_ImportarEvent;
                                cSesion.Importar(Modelo, Datos);
                            }
                        }
                        catch (Exception ex)
                        {
                            eClockBase.CeC_Log.AgregaError(ex);
                            Mostrar(true, true);
                        }
                    }
                    break;
            }
        }

        bool Importa()
        {
            if (ReciboAImportar >= RecNominas.Count)
                return false;
                        
            Lbl_Avance.Text = "Importando " + RecNominas[ReciboAImportar].PERSONA_LINK_ID.ToString();
            
            //Inicializa el controlador de nominas usando la sesion actual
            eClockBase.Controladores.Nominas cNominas = new eClockBase.Controladores.Nominas(Sesion);
            cNominas.ImportaRecibosEvent += cNominas_ImportaRecibosEvent;
            //Importa el recibo a importar ReciboAImportar
            cNominas.ImportaRecibos(RecNominas[ReciboAImportar]);
            ReciboAImportar++;
            return true;
        }

        void cNominas_ImportaRecibosEvent(string PersonasLinksIDs, string[] sPersonasLinksIDs, int NoRegistros)
        {
            if (NoRegistros < 0)
            {
                Lbl_eRed.Visibility = System.Windows.Visibility.Collapsed;
                return;
            }
            Pbr_Recibos.Value = ReciboAImportar;
            Importa();            
        }
        void Cerrar()
        {
            this.Visibility = Visibility.Hidden;
        }
        void cSesion_ImportarEvent(bool Errores, string Resultado)
        {
            if (!Errores)
                Cerrar();
            else
                Mostrar(true, false);
        }

        private void Tbx_Ano_TextChanged(object sender, TextChangedEventArgs e)
        {
            int Ano = eClockBase.CeC.Convierte2Int(Tbx_Ano.Text);
            if (Ano < 2000 || Ano > 2020)
                Lbl_eAnoValido.Visibility = System.Windows.Visibility.Visible;
            else
                Lbl_eAnoValido.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void Tbx_PeriodoNo_TextChanged(object sender, TextChangedEventArgs e)
        {
            int Periodo = eClockBase.CeC.Convierte2Int(Tbx_PeriodoNo.Text);
            if (Periodo < 0)
                Lbl_ePeriodoValido.Visibility = System.Windows.Visibility.Visible;
            else
                Lbl_ePeriodoValido.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void Cmb_NominaID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Cmb_NominaID.SelectedItem == null || Cmb_NominaID.SelectedItem.Adicional == null)
            {
                Lbl_SelNomina.Visibility = System.Windows.Visibility.Visible;
                return;
            }
            Tbx_Ano.Text = eClockBase.CeC.Convierte2String(Cmb_NominaID.SelectedItem.Descripcion);
            Tbx_PeriodoNo.Text = eClockBase.CeC.Convierte2String(Cmb_NominaID.SelectedItem.Adicional);
            Lbl_SelNomina.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void Btn_Elegir_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog FD = new System.Windows.Forms.FolderBrowserDialog();
            if (FD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Lbl_Carpeta.Text = FD.SelectedPath;
                Lbl_eSelCarpeta.Visibility = System.Windows.Visibility.Collapsed;
            }
        }
    }
}
