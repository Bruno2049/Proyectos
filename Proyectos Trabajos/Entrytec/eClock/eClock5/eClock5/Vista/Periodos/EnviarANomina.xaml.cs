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

namespace eClock5.Vista.Periodos
{
    /// <summary>
    /// Lógica de interacción para EnviarANomina.xaml
    /// </summary>
    public partial class EnviarANomina : UserControl
    {
        public EnviarANomina()
        {
            InitializeComponent();
        }
        int Correctos = 0;
        int Erroneos = 0;
        private void Lbl_Estado_Loaded(object sender, RoutedEventArgs e)
        {
            eClockBase.CeC_SesionBase Sesion = CeC_Sesion.ObtenSesion(this);

            string Parametros = eClockBase.CeC.Convierte2String(Clases.Parametros.Tag2Parametros(Tag).Parametro);
            string[] sParametros = eClockBase.CeC.ObtenArregoSeparador(Parametros, ",");
            Correctos = 0;
            Erroneos = 0;
            foreach (string sParametro in sParametros)
            {
                eClockBase.Controladores.Incidencias Incidencias = new eClockBase.Controladores.Incidencias(Sesion);
                Incidencias.EnviaANominaFinalizado += Incidencias_EnviaANominaFinalizado;
                Incidencias.EnviaANomina(eClockBase.CeC.Convierte2Int(sParametro));
            }
        }

        void Incidencias_EnviaANominaFinalizado(bool Resultado)
        {
            if (Resultado)
                Correctos++;
            else
                Erroneos++;
            string Texto = "";
            if (Correctos > 0)
                Texto = "Se enviaron " + Correctos + " periodo(s) satisfactoriamente";
            if (Erroneos > 0)
                Texto = eClockBase.CeC.AgregaSeparador(Texto, "No se pudieron enviar " + Erroneos + " periodo(s)", " y ");
            Lbl_Estado.Text = Texto;
        }

        private void UC_ToolBar_OnEventClickToolBar(UC_ToolBar_Control Control)
        {
            switch (Control.Nombre)
            {
                case "Btn_Regresar":
                    this.Visibility = System.Windows.Visibility.Hidden;
                    break;
            }
        }
    }
}
