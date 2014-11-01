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
using eClock5.BaseModificada;
using eClock5;
using eClockBase;

namespace Kiosko.Tramites
{
    /// <summary>
    /// Lógica de interacción para Detalles.xaml
    /// </summary>
    public partial class Detalles : UserControl
    {
        eClockBase.Modelos.Model_TIPO_TRAMITE TipoTramite = null;
        public int Llave;

        public Detalles()
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
                case "Btn_Siguiente":
                    eClockBase.CeC_SesionBase Sesion = CeC_Sesion.ObtenSesion(this);
                    eClockBase.Controladores.Tramites Tts = new eClockBase.Controladores.Tramites(Sesion);
                    Tts.NuevoEvent += Tts_NuevoEvent;
                    Tts.Nuevo(Llave, Sesion.Mdl_Sesion.PERSONA_ID, 1, Campos.Vista2JSon());


                    //Modelo.TRAMITE_ID = -1;
                    //Modelo.TIPO_TRAMITE_ID = Llave;
                    //Modelo.TIPO_PRIORIDAD_ID = 1;
                    //Modelo.PERSONA_ID = Sesion.Mdl_Sesion.PERSONA_ID;
                    //Modelo.TRAMITE_FECHA = System.DateTime.Now;
                    //Modelo.TRAMITE_DESCRIPCION = Campos.Vista2JSon();
                    //Se.GuardaDatosEvent += Se_GuardaDatosEvent;
                    //string DatosModelo = JsonConvert.SerializeObject(Modelo);
                    //Se.GuardaDatos("EC_TRAMITES", "TRAMITE_ID", DatosModelo, true);
                    break;

            }
        }
        bool Cerrar = false;
        void Tts_NuevoEvent(int Resultado)
        {
            Controles.UC_MessageBoxFullScreen Msg = new Controles.UC_MessageBoxFullScreen();
            if (Resultado > 0)
            {
                Msg.Mensaje = "Tramite Solicitado";

                Cerrar = true;
            }
            else
            {
                Msg.Mensaje = "Error al Solicitar el Tramite";
                Msg.Imagen = null;
                Cerrar = false;
            }
            Msg.Cerrado += Msg_Cerrado;
            Msg.Mostrar(this);
        }


        void Msg_Cerrado()
        {
            if (Cerrar)
            {
                this.Visibility = System.Windows.Visibility.Hidden;
            }
        }


        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            //Obtiene la variable de sesion de la vista actual.
            eClockBase.CeC_SesionBase Sesion = CeC_Sesion.ObtenSesion(this);
            //Creamos un nuevo objeto de tipo controlador de Sesion; usando la sesion actual.
            eClockBase.Controladores.Sesion Se = new eClockBase.Controladores.Sesion(Sesion);
            //Agregamos(Ligamos) una funcion al evento que fue creado dentro de nuestro controlador Sesion; 
            Se.ObtenDatosEvent += Se_ObtenDatosEvent;
            //Obtiene parametros especificados por la vista Anterior y almacenados en la propiedad Tag del objeto (control) actual
            //En este caso Solicitudes\Detalles.
            //eClock5.Clases.Parametros Parametros = eClock5.Clases.Parametros.Tag2Parametros(this.Tag);
            //Creamos una variable "TipoTramite" del tipo "eClockBase.Modelos.Model_TIPO_TRAMITE",....
            ///..Despues del signo igual(=) Convertimos la variable "Parametros.Parametro" de tipo OBJECT a STRING porque asumimos
            ///que este es un Jsone, debido a que esta ventana (Detalles) fue llamada desde UCListado y este control al darle click a un 
            ///..elemento del listado crea un objeto json y lo manda de parametro en TAG
            ///..posteriormente este parametro TAG, se deserializa en tipo eClockBase.Modelos.Model_TIPO_TRAMITE,
            ///..para ser alamcenado dentro de la variable "TipoTramite" que es del mismo Tipo.
            //TipoTramite = JsonConvert.DeserializeObject<eClockBase.Modelos.Model_TIPO_TRAMITE>(Parametros.Parametro.ToString());
            TipoTramite = new eClockBase.Modelos.Model_TIPO_TRAMITE();
            TipoTramite.TIPO_TRAMITE_ID = Llave;
            ///Llamamos ala funcion ObtenDatos que esta dentro del CONTROLADOR SESION, mandandole como parametros..
            ///..el nombre de nuestra tabla, el nombre de id del tipo de tramite, en donde este ultimo debe de venir dentro de nuestra variable..
            ///..tipo tramite que es de tipo "eClockBase.Modelos.Model_TIPO_TRAMITE", y que nos devolvera los datos correspondientes...
            ///..para ser ligados en nuestra vista (Detalles).
            Se.ObtenDatos("EC_TIPO_TRAMITE", "TIPO_TRAMITE_ID", TipoTramite);
        }

        void Se_ObtenDatosEvent(int Resultado, string Datos)
        {
            try
            {
                if (Resultado > 0)
                {
                    TipoTramite = JsonConvert.DeserializeObject<eClockBase.Modelos.Model_TIPO_TRAMITE>(Datos);
                    Lbl_Titulo.Text = TipoTramite.TIPO_TRAMITE_NOMBRE;
                    //Lbl_Descripcion.Text = TipoTramite.TIPO_TRAMITE_DESCRIPCION;
                    Campos.Asigna(TipoTramite.TIPO_TRAMITE_CAMPOS, "");
                    if (Campos.NoCampos > 0)
                        Teclado.Visibility = System.Windows.Visibility.Visible;
                    else
                        Teclado.Visibility = System.Windows.Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                eClockBase.CeC_Log.AgregaError(ex);
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

    }
}
