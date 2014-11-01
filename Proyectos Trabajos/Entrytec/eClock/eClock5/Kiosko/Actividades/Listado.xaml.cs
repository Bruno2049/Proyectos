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
namespace Kiosko.Actividades
{
    /// <summary>
    /// Lógica de interacción para Listado.xaml
    /// </summary>
    public partial class Listado : UserControl
    {
        List<eClockBase.Controladores.ListadoJson> ListadoActividades = null;
        public Listado()
        {
            InitializeComponent();
        }

        private void UC_ToolBar_OnEventClickToolBar(eClock5.UC_ToolBar_Control Control)
        {
            switch (Control.Nombre)
            {
                case "Btn_Regresar":
                    this.Visibility = System.Windows.Visibility.Hidden;
                    break;

            }
        }
        int Scroll = 0;
        void ActualizaVista()
        {
            Contenedor.ItemWidth = 300;
            Contenedor.ItemHeight = 200;
            foreach(eClockBase.Controladores.ListadoJson Actividad in ListadoActividades)
            {
                bool Encontrado = false;
                Controles.UC_LiveTile Bot = null;
                foreach (Controles.UC_LiveTile LT in LiveTiles)
                {
                    if (LT.Tag == Actividad)
                    {
                        Bot = LT;
                        Encontrado = true;
                        break;
                    }
                }

                if (!Encontrado)
                {
                    Bot = new Controles.UC_LiveTile();
                    LiveTiles.Add(Bot);
                    Bot.BorderThickness = new Thickness(10);
                    Bot.TimeOutCambio = 5 + LiveTiles.Count % 3;
                    Bot.ColorFondo = Color.FromRgb(100, 120, 34);
                    Bot.IniciaCambio();
                    Bot.MouseDown += Bot_MouseDown;
                    Contenedor.Children.Add(Bot);
                    Bot.Tag = Actividad;
                }

                Bot.Texto = Actividad.Nombre.ToString();
                Bot.Descripcion = Actividad.Descripcion.ToString();
                if (Actividad.Imagen != null)
                    Bot.Imagen = (BitmapImage)Actividad.Imagen;
            }

            foreach (Controles.UC_LiveTile LT in LiveTiles)
            {
                bool Encontrado = false;
                foreach (eClockBase.Controladores.ListadoJson Actividad in ListadoActividades)
                {
                    eClockBase.Controladores.ListadoJson LJson = LT.Tag as eClockBase.Controladores.ListadoJson;
                    if (LJson.Llave == Actividad.Llave)
                    {
                        Encontrado = true;
                        break;
                    }
                }
                if (!Encontrado)
                {
                    LT.Visibility = System.Windows.Visibility.Hidden;
                    Contenedor.Children.Remove(LT);
                }
            }
        }

        void ActualizaVista(eClockBase.Controladores.ListadoJson Actividad)
        {
            foreach (Controles.UC_LiveTile LT in LiveTiles)
            {
                eClockBase.Controladores.ListadoJson LJson = LT.Tag as eClockBase.Controladores.ListadoJson;
                if (LJson.Llave == Actividad.Llave)
                {
                    LT.Tag = Actividad;
                    break;
                }
            }
        }

        void ActualizaVista(int NoElemento)
        {
            if (NoElemento - Scroll >= LiveTiles.Count)
                return;
            Controles.UC_LiveTile Control = LiveTiles[NoElemento - Scroll];
            if (NoElemento >= ListadoActividades.Count)
            {
                Control.Visibility = System.Windows.Visibility.Hidden;
                return;
            }
            Control.Visibility = System.Windows.Visibility.Visible;
            eClockBase.Controladores.ListadoJson Elemento = ListadoActividades[NoElemento];

            Control.Texto = Elemento.Nombre.ToString();
            Control.Descripcion = Elemento.Descripcion.ToString();
            if (Elemento.Imagen != null)
                Control.Imagen = (BitmapImage)Elemento.Imagen;

        }

        List<Controles.UC_LiveTile> LiveTiles = new List<Controles.UC_LiveTile>();
       /* void InicializaLiveTiles()
        {

            foreach (UIElement Elemento in Grd_LiveTiles.Children)
            {
                if (Elemento.GetType().Name == "Grid")
                {
                    try
                    {
                        foreach (UIElement BotonElemento in ((Grid)Elemento).Children)
                        {
                            if (BotonElemento.GetType().Name == "UC_LiveTile")
                            {
                                Controles.UC_LiveTile Bot = (Controles.UC_LiveTile)BotonElemento;
                                LiveTiles.Add(Bot);
                                Bot.TimeOutCambio = 5 + LiveTiles.Count % 3;
                                Bot.ColorFondo = Color.FromRgb(100, 120, 34);
                                Bot.IniciaCambio();
                                Bot.MouseDown += Bot_MouseDown;
                            }
                        }
                    }
                    catch { }
                }
            }
        }*/

        void Bot_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Controles.UC_LiveTile Boton = (Controles.UC_LiveTile)sender;
                int Pos = LiveTiles.IndexOf(Boton);
                if (Pos + Scroll >= ListadoActividades.Count)
                    return;
                Detalle Dlg = new Detalle();
                Dlg.Tag = ListadoActividades[Pos + Scroll].Llave;
                Dlg.Registrado += Dlg_Registrado;
                Kiosko.Generales.Main.MuestraComoDialogo(this, Dlg, Boton.ColorFondo);
            }
            catch { }
        }

        void Dlg_Registrado()
        {
            this.Visibility = System.Windows.Visibility.Hidden;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //InicializaLiveTiles();
            eClockBase.CeC_SesionBase Sesion = eClock5.CeC_Sesion.ObtenSesion(this);
            eClockBase.Controladores.Sesion CSesion = new eClockBase.Controladores.Sesion(Sesion);
            CSesion.CambioListadoEvent += delegate(string sListado)
                {
                    try
                    {
                        ListadoActividades = JsonConvert.DeserializeObject<List<eClockBase.Controladores.ListadoJson>>(sListado);
                        // Datos[0].Seleccionado = true;
                        for (int NoElemento = 0; NoElemento < ListadoActividades.Count; NoElemento++)
                        {
                            eClockBase.Controladores.ListadoJson Elemento = ListadoActividades[NoElemento];
                            eClockBase.Controladores.Actividades Actividad = new eClockBase.Controladores.Actividades(Sesion);
                            Actividad.ObtenImagenFinalizado += delegate(byte[] Imagen)
                                {
                                    try
                                    {

                                        System.IO.MemoryStream MS = new System.IO.MemoryStream(Imagen);
                                        BitmapImage bi = new BitmapImage();
                                        bi.BeginInit();
                                        bi.StreamSource = MS;
                                        bi.EndInit();
                                        Elemento.Imagen = bi;
                                        ActualizaVista(Elemento);
                                    }
                                    catch { }
                                };
                            Actividad.ObtenImagen(eClockBase.CeC.Convierte2Int(Elemento.Llave));
                        }
                        ActualizaVista();

                    }
                    catch { }
                };
            CSesion.ObtenListado("EC_ACTIVIDADES", "ACTIVIDAD_ID", "ACTIVIDAD_NOMBRE", "", "ACTIVIDAD_DESCRIPCION", "", false, "");
        }


    }
}
