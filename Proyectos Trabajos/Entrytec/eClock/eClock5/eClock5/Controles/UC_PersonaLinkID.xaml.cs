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

namespace eClock5.Controles
{
    /// <summary>
    /// Lógica de interacción para UC_PersonaLinkID.xaml
    /// </summary>
    public partial class UC_PersonaLinkID : UserControl
    {
        public int PersonaID
        {
            get { return eClockBase.CeC.Convierte2Int(GetValue(PersonaIDProperty)); }
            set
            {
                SetValue(PersonaIDProperty, value);
            }
        }


        public static readonly DependencyProperty PersonaIDProperty =
            DependencyProperty.Register("PersonaID", typeof(int), typeof(UC_PersonaLinkID),
            new FrameworkPropertyMetadata(-1, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnPersonaIDChanged
    )));
        private static void OnPersonaIDChanged(DependencyObject obj,
    DependencyPropertyChangedEventArgs args)
        {
            // When the color changes, set the icon color
            UC_PersonaLinkID control = (UC_PersonaLinkID)obj;
            control.CambioPersonaID();
        }
        eClockBase.Controladores.ListadoJson Persona = null;
        public bool CambioPersonaID(eClockBase.Controladores.ListadoJson PersonaEncontrada = null)
        {
            try
            {

                if (PersonaEncontrada != null)
                {
                    Persona = PersonaEncontrada;
                    PersonaID = eClockBase.CeC.Convierte2Int(Persona.Llave);
                }
                else
                {
                    if (Personas == null)
                        return false;
                    if (PersonaID <= 0)
                    {
                        Persona = null;
                    }
                    else
                    {
                        var PersonasFiltro = from p in Personas
                                             where (eClockBase.CeC.Convierte2Int(p.Llave) == PersonaID)
                                             select p;
                        if (PersonasFiltro.Count() > 0)
                        {
                            Persona = PersonasFiltro.Min();
                        }
                    }
                }
                if (Persona == null)
                {
                    Tbx_PersonaLinkID.Text = "";
                    Tbx_PersonaNombre.Text = "";
                }
                else
                {
                    Tbx_PersonaLinkID.Text = eClockBase.CeC.Convierte2Int(Persona.Adicional).ToString();
                    Tbx_PersonaNombre.Text = eClockBase.CeC.Convierte2String(Persona.Nombre);
                    Tbx_PersonaLinkID.ToolTip = Tbx_PersonaNombre.ToolTip = eClockBase.CeC.Convierte2String(Persona.Descripcion);
                }
                return true;
            }
            catch
            {
            }
            return false;
        }
        public UC_PersonaLinkID()
        {
            InitializeComponent();
        }
        public List<eClockBase.Controladores.ListadoJson> Personas = null;
        void ActualizaDatos()
        {
            eClockBase.CeC_SesionBase Sesion = CeC_Sesion.ObtenSesion(this);
            eClockBase.Controladores.Sesion CSesion = new eClockBase.Controladores.Sesion(Sesion);
            CSesion.CambioListadoEvent += CSesion_CambioListadoEvent;
            CSesion.ObtenListado("EC_PERSONAS", "PERSONA_ID", "PERSONA_NOMBRE", "PERSONA_LINK_ID", "AGRUPACION_NOMBRE", "TIPO_PERSONA_ID", true, "", "");
        }

        void CSesion_CambioListadoEvent(string Listado)
        {
            try
            {
                Personas = eClockBase.Controladores.CeC_ZLib.Json2Object<List<eClockBase.Controladores.ListadoJson>>(Listado);
                CambioPersonaID();
            }
            catch { }

        }

        private void Tbx_PersonaLinkID_TextChanged(object sender, TextChangedEventArgs e)
        {
            int PersonaLinkID = eClockBase.CeC.Convierte2Int(Tbx_PersonaLinkID.Text);
            if (Persona != null && PersonaLinkID == eClockBase.CeC.Convierte2Int(Persona.Adicional))
                return;
            var PersonasFiltro = from p in Personas
                                 where (eClockBase.CeC.Convierte2Int(p.Adicional) == PersonaLinkID)
                                 select p;
            eClockBase.Controladores.ListadoJson PersonaEncontrada = null;
            if (PersonasFiltro.Count() > 0)
            {
                PersonaEncontrada = PersonasFiltro.Min();
                CambioPersonaID(PersonaEncontrada);
            }
            else
            {
                PersonaID = 0;
                Persona = null;
                Tbx_PersonaNombre.Text = "";
            }
            
        }

        private void Tbx_PersonaNombre_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ActualizaDatos();
        }
    }
}
