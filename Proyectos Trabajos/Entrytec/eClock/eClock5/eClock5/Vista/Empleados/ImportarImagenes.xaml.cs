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


using eClockBase;

using System.IO;
using System.Text;
using System.Windows.Forms;


namespace eClock5.Vista.Empleados
{
    /// <summary>
    /// Lógica de interacción para CargarFotos.xaml
    /// </summary>
    public partial class ImportarImagenes : System.Windows.Controls.UserControl
    {
        public bool Realizado = false;
        FolderBrowserDialog m_File = new FolderBrowserDialog();

        string RutaArchivo;
        string ArchivoDestino;
        CeC_SesionBase m_SesionBase = null;

        public FolderBrowserDialog File
        {
            get { return m_File; }
            set { m_File = value; }
        }

        public ImportarImagenes()
        {
            InitializeComponent();
        }


        private void UC_ToolBar_OnEventClickToolBar(UC_ToolBar_Control Control)
        {
            switch (Control.Nombre)
            {
                case "Btn_Regresar":
                    this.Visibility = Visibility.Hidden;
                    break;
                case "Btn_Guardar":
                    try
                    {
                        //m_SesionBase = new CeC_SesionBase();
                        //m_SesionBase.MuestraMensaje("Buscando Datos", 5);
                        //ArchivoDestino = "C:\\Fotos";
                        //CompressionEngine.Current.Decoder.DecodeIntoDirectory(file, ArchivoDestino);
                        //Tbx_ArchivoRAR.Clear();
                        Cargar(Tbx_Archivo.Text);
                    }
                    catch { }
                    break;

            }
        }

        public void Cargar(string CarpetaALeer)
        {

            int TipoImagenID =  1;
            if(CeC.Convierte2Bool( Opt_Firma.IsChecked))
                TipoImagenID =  2;

            string[] Files;

            Files = System.IO.Directory.GetFiles(CarpetaALeer);

            foreach (string File in Files)
            {
                try
                {
                    int Foto_PERSONA_LINK_ID = CeC.Convierte2Int(System.IO.Path.GetFileNameWithoutExtension(File));
                    if (Foto_PERSONA_LINK_ID <= 0)
                        continue;
                    eClockBase.CeC_SesionBase Sesion = CeC_Sesion.ObtenSesion(this);
                    Sesion.MuestraMensaje("Guardando " + File);
                    eClockBase.Controladores.Personas Ps = new eClockBase.Controladores.Personas(Sesion);
                    Ps.ObtenPersonaIDBySuscripcionEvent += delegate(int Resultado)
                          {
                              if (Resultado != null || Resultado != -9999)
                              {
                                  int PersonaID = Resultado;
                                  if (PersonaID > 0)
                                  {
                                      Ps.GuardaFotoEvent += Ps_GuardaFotoEvent;
                                      byte[] ImgByte = Imagen_A_Bytes(File);
                                      if (ImgByte.Length > 1000000)
                                      {
                                          ImgByte = eClock5.BaseModificada.CeC.Imagen2Thumbnail(ImgByte,2048,2048);
                                      }
                                      if (ImgByte!= null && ImgByte.Length <= 1000000)
                                      {
                                          Ps.GuardaImagen(PersonaID, ImgByte, TipoImagenID);
                                          //Ps.GuardaFoto(PersonaID, ImgByte);
                                      }
                                      else
                                      {
                                          CeC_Log.AgregaError("No se pudo guardar Archivo muy grande " + File);
                                      }
                                  }
                              }
                          };

                    Ps.ObtenPersonaIDBySuscripcion(Foto_PERSONA_LINK_ID);
                }
                catch (Exception ex){
                    CeC_Log.AgregaError("No se pudo guardar " + File);
                    CeC_Log.AgregaError(ex);
                }
            }
            string[] Carpetas;
            Carpetas = System.IO.Directory.GetDirectories(CarpetaALeer);
            foreach (string Carpeta in Carpetas)
            {
                try
                {
                    Cargar(Carpeta);
                }
                catch { }
            }
            //System.IO.FileStream FS = new FileStream(ArchivoALeer, FileMode.Open, FileAccess.Read);
            //StreamReader SR = new StreamReader(FS);
            //string contenido = SR.ReadLine();            
            //}
        }

        public Byte[] Imagen_A_Bytes(String Ruta)
        {
            try
            {
                return System.IO.File.ReadAllBytes(Ruta);

                FileStream Foto = new FileStream(Ruta, FileMode.OpenOrCreate, FileAccess.ReadWrite);

                Byte[] FotoEnBytes = new Byte[Foto.Length];

                BinaryReader BReader = new BinaryReader(Foto);

                FotoEnBytes = BReader.ReadBytes(Convert.ToInt32(Foto.Length));

                return FotoEnBytes;
            }
            catch (Exception ex)
            {
                CeC_Log.AgregaError(ex);
            }
            return null;
        }

        void Ps_GuardaFotoEvent(bool FotoGuardada)
        {
            throw new NotImplementedException();
        }



        private void Btn_Seleccionar_Click(object sender, RoutedEventArgs e)
        {
            //this.File.Filter = "Archivos de Aceptados |*.rar ";
            //dialogo.FilterIndex = 1;
            this.File.ShowDialog();
            Tbx_Archivo.Text = this.File.SelectedPath;
            

        }
    }
}
