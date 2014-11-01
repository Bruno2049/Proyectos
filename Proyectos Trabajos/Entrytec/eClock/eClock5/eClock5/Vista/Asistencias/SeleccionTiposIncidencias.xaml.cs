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

namespace eClock5.Vista.Asistencias
{
    /// <summary>
    /// Lógica de interacción para SeleccionTiposIncidencias.xaml
    /// </summary>
    public partial class SeleccionTiposIncidencias : UserControl
    {
        public SeleccionTiposIncidencias()
        {
            InitializeComponent();
        }

        
        private void UC_ToolBar_OnEventClickToolBar(UC_ToolBar_Control Control)
        {
            switch (Control.Nombre)
            {
                case "Btn_Filtrar":
                    {
                        FiltradoPor FP =  new FiltradoPor();
                        FP.TipoIncIDS = Lst_TipoInc.ObtenIDsSeleccionados();
                        FP.TipoIncSisIDS = Lst_TipoIncSis.ObtenIDsSeleccionados();
                        Clases.Parametros.MuestraControl(Grid, FP, new Clases.Parametros(true, Clases.Parametros.Tag2Parametros(Tag).Parametro));
                    }
                    break;
                case "Btn_Regresar":
                    this.Visibility = System.Windows.Visibility.Hidden;
                    break;
            }
        }
    }
}
