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

namespace Kiosko.Controles
{
    /// <summary>
    /// Lógica de interacción para UC_Tecla.xaml
    /// </summary>
    public partial class UC_Tecla : UserControl
    {
        private string m_Tecla = "";

        public string Tecla
        {
            get { return m_Tecla; }
            set
            {
                m_Tecla = value;
                ActualizaBoton();
            }
        }

        public string TeclaPresionada
        {
            get {
                switch (m_TeclaAMostrar)
                {
                    case TipoMuestra.Tecla:
                        return Tecla;
                        break;
                        
                    case TipoMuestra.TeclaAlterna:
                        return TeclaAlterna;
                       
                        break;
                    case TipoMuestra.TeclaCodigos:
                       return TeclaCodigos;
                        break;
                }
                return ""; }
            
        }

        private string m_TeclaAlterna = "";

        public string TeclaAlterna
        {
            get { return m_TeclaAlterna; }
            set
            {
                m_TeclaAlterna = value;
                ActualizaBoton();
            }
        }
        private string m_TeclaCodigos = "";

        public string TeclaCodigos
        {
            get { return m_TeclaCodigos; }
            set
            {
                m_TeclaCodigos = value;
                ActualizaBoton();
            }
        }
        public enum TipoMuestra
        {
            Tecla, TeclaAlterna, TeclaCodigos
        };
        private TipoMuestra m_TeclaAMostrar = TipoMuestra.Tecla;

        public TipoMuestra TeclaAMostrar
        {
            get { return m_TeclaAMostrar; }
            set
            {
                m_TeclaAMostrar = value;
                ActualizaBoton();
            }
        }
        public void ActualizaBoton()
        {
            switch (m_TeclaAMostrar)
            {
                case TipoMuestra.Tecla:
                    Btn_Tecla.Content = Tecla;
                    break;
                case TipoMuestra.TeclaAlterna:
                    Btn_Tecla.Content = TeclaAlterna;
                    break;
                case TipoMuestra.TeclaCodigos:
                    Btn_Tecla.Content = TeclaCodigos;
                    break;
            }
        }

        public UC_Tecla()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            ActualizaBoton();
            
        }
        public delegate void ClickArgs(UC_Tecla ControlTecla);
        public event ClickArgs Clickeado;
        private void Btn_Tecla_Click(object sender, RoutedEventArgs e)
        {
            
            if (Clickeado != null)
                Clickeado(this);
        }

        

       

        
    }
}
