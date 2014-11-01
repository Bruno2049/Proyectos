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

namespace eClock5.Controles
{
    /// <summary>
    /// Lógica de interacción para UC_CamposTextoDiseno.xaml
    /// </summary>
    public partial class UC_CamposTextoDiseno : UserControl
    {
        public UC_CamposTextoDiseno()
        {
            InitializeComponent();
        }
        List<eClockBase.Modelos.Campos.Model_CampoTexto> Campos = new List<eClockBase.Modelos.Campos.Model_CampoTexto>();
        public string Valor
        {
            get {
                Campos = (List<eClockBase.Modelos.Campos.Model_CampoTexto>)Lst_Campos.ItemsSource;
                return JsonConvert.SerializeObject(Campos); }
            set
            {
                try
                {
                    if (value != null && value != "")
                    {
                        Campos = eClockBase.Controladores.CeC_ZLib.Json2Object<List<eClockBase.Modelos.Campos.Model_CampoTexto>>(value);
                        Actualiza();
                        return;
                    }
                }
                catch {
                    
                }
                Campos = new List<eClockBase.Modelos.Campos.Model_CampoTexto>();
                Actualiza();

            }
        }
        public void Actualiza()
        {
            Lst_Campos.ItemsSource = Campos;
            Lst_Campos.Items.Refresh();
        }
        private void Lst_Campos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Lst_Campos.SelectedIndex >= 0)
                ToolBar.Seleccionados = 1;
            else
                ToolBar.Seleccionados = 0;
        }

        private void ToolBar_OnEventClickToolBar(UC_ToolBar_Control Control)
        {
        //    Campos = (List<eClockBase.Modelos.Campos.Model_CampoTexto>)Lst_Campos.ItemsSource;
            switch (Control.Nombre)
            {
                case "Btn_Nuevo":
                    Campos.Add(new eClockBase.Modelos.Campos.Model_CampoTexto());
                    break;
                case "Btn_Subir":
                    if (Lst_Campos.SelectedIndex > 0)
                    {
                        eClockBase.Modelos.Campos.Model_CampoTexto  Campo = Campos[Lst_Campos.SelectedIndex];
                        Campos.RemoveAt(Lst_Campos.SelectedIndex);
                        Campos.Insert(Lst_Campos.SelectedIndex - 1, Campo);
                    }
                    break;
                case "Btn_Bajar":
                    if (Lst_Campos.SelectedIndex < Lst_Campos.SelectedIndex-1)
                    {
                        eClockBase.Modelos.Campos.Model_CampoTexto Campo = Campos[Lst_Campos.SelectedIndex];
                        Campos.RemoveAt(Lst_Campos.SelectedIndex);
                        Campos.Insert(Lst_Campos.SelectedIndex + 1, Campo);
                    }

                    break;
                case "Btn_Borrar":
                    if (Lst_Campos.SelectedIndex >= 0)
                        Campos.RemoveAt(Lst_Campos.SelectedIndex);

                    break;
            }
            Actualiza();
        }
    }
}
