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
    public partial class Recibos_Emp : UserControl
    {
        public Recibos_Emp()
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

                    {
                        eClockBase.Modelos.Nomina.Model_Parametros Param = new eClockBase.Modelos.Nomina.Model_Parametros();


                        //Param.PERSONA_ID = PersonaID;

                        Controles.UC_Reportes Reportes = new Controles.UC_Reportes();
                        Reportes.ParametrosGenerales = Param;
                        Reportes.Modelos = "eClockBase.Modelos.Nomina.Reporte_RecNomina";
                        UC_Listado.MuestraComoDialogo(this, Reportes, Colors.White);
                    }

                    break;
            }
        }

        string Agrupacion = "";
        int PersonaID = -1;
        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            
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

            Lst_Nominas.Filtro = "PERSONA_ID = " + PersonaID;
            Lst_Nominas.ActualizaDatos();
        }

        private void Lst_Nominas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            eClockBase.Modelos.Nomina.Model_Parametros Param = new eClockBase.Modelos.Nomina.Model_Parametros();
            //Param.REC_NOMINA_ANO = eClockBase.CeC.Convierte2Int(Tbx_Ano.Text);
            //Param.REC_NOMINA_NO = eClockBase.CeC.Convierte2Int(Tbx_PeriodoNo.Text);
            //Param.AGRUPACION = Agrupacion;
            //Param.PERSONA_ID = PersonaID;
            Param.REC_NOMINA_ID = eClockBase.CeC.Convierte2Int( Lst_Nominas.Seleccionado.Llave);

            Controles.UC_Reportes Reportes = new Controles.UC_Reportes();
            Reportes.ParametrosGenerales = Param;
            Reportes.Modelos = "eClockBase.Modelos.Nomina.Reporte_RecNomina";
            UC_Listado.MuestraComoDialogo(this, Reportes, Colors.White);
        }

       

    }
}
