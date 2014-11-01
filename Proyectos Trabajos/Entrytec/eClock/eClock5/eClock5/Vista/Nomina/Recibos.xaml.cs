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

namespace eClock5.Vista.Nomina
{
    /// <summary>
    /// Lógica de interacción para Recibos.xaml
    /// </summary>
    public partial class Recibos : UserControl
    {
        public Recibos()
        {
            InitializeComponent();
        }
        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {

        }



        private void ToolBar_OnEventClickToolBar(UC_ToolBar_Control Control)
        {
            switch (Control.Nombre)
            {
                case "Btn_Reportes":
                    if (Lbl_eAnoValido.IsVisible == false &&
                        Lbl_ePeriodoValido.IsVisible == false)
                    {
                        eClockBase.Modelos.Nomina.Model_Parametros Param = new eClockBase.Modelos.Nomina.Model_Parametros();
                        Param.REC_NOMINA_ANO = eClockBase.CeC.Convierte2Int(Tbx_Ano.Text);
                        Param.REC_NOMINA_NO = eClockBase.CeC.Convierte2Int(Tbx_PeriodoNo.Text);
                        Param.AGRUPACION = Agrupacion;
                        Param.PERSONA_ID = PersonaID;

                        Controles.UC_Reportes Reportes = new Controles.UC_Reportes();
                        Reportes.ParametrosGenerales = Param;
                        Reportes.Modelos = "eClockBase.Modelos.Nomina.Reporte_RecNomina";
                        UC_Listado.MuestraComoDialogo(this, Reportes, Colors.White);
                    }

                    break;
            }
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
        string Agrupacion = "";
        int PersonaID = -1;
        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Tbx_Ano.Text = DateTime.Now.Year.ToString();
            string Parametros = eClockBase.CeC.Convierte2String(Clases.Parametros.Tag2Parametros(Tag).Parametro);
            PersonaID = -1;
            Agrupacion = "";
            if (Parametros == "")
                Agrupacion = "";
            else
                if (Parametros[0] == '|' || Parametros[0] == '@')
                    Agrupacion = Parametros;
                else
                    PersonaID = eClockBase.CeC.Convierte2Int(Parametros);

            
        }


    }
}
