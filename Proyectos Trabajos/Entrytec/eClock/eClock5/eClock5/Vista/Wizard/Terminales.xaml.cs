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

namespace eClock5.Vista.Wizard
{
    /// <summary>
    /// Lógica de interacción para Terminales.xaml
    /// </summary>
    public partial class Terminales : UserControl
    {
        public Terminales()
        {
            InitializeComponent();
            IsVisibleChanged += Terminales_IsVisibleChanged;
        }
        public int Sitio_ID
        {
            get { return Lst_Terminales.Sitio_ID; }
            set { Lst_Terminales.Sitio_ID = value; }
        }
        void Terminales_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ActualizaBotones();
        }
        public delegate void GuardarArgs(bool Guardados);
        public event GuardarArgs GuardarEvent;

        public void Guardar()
        {
            Lst_Terminales.GuardarEvent += Lst_Terminales_GuardarEvent;
            Lst_Terminales.Guardar();
        }

        void ActualizaBotones()
        {

            Controles.UC_Wizard.sMostrarBotones(true, false, true, true);
        }



        void Lst_Terminales_GuardarEvent(bool Guardados)
        {
            if (GuardarEvent != null)
                GuardarEvent(Guardados);
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }
    }
}
