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
using System.Reflection;
using System.ComponentModel;
using eClock5.Controles;

namespace Kiosko.Controles
{
    /// <summary>
    /// Lógica de interacción para UC_CamposTextoEdicion.xaml
    /// </summary>
    public partial class UC_CamposTextoEdicion : UserControl
    {
        private string PreficoCampo = "Ctr_";
        public UC_CamposTextoEdicion()
        {
            InitializeComponent();
        }

        public int NoCampos
        {
            get;
            set;
        }

        private List<eClockBase.Modelos.Campos.Model_CampoTexto> m_Configuracion = null;
        public bool Asigna(string Configuracion, string Valores)
        {
            try
            {
                xGrid.RowDefinitions.Clear();
                xGrid.Children.Clear();
                m_Configuracion = JsonConvert.DeserializeObject<List<eClockBase.Modelos.Campos.Model_CampoTexto>>(Configuracion);
                int NoFila = 0;
                //Newtonsoft.Json.Linq.JContainer Contenedor = (Newtonsoft.Json.Linq.JContainer)JsonConvert.DeserializeObject(ModeloJson);

                foreach (eClockBase.Modelos.Campos.Model_CampoTexto CampoTexto in m_Configuracion)
                {
                    RowDefinition Fila = new RowDefinition();
                    //Fila.Height = new GridLength(36);

                    xGrid.RowDefinitions.Add(Fila);

                    FrameworkElement Fe = AgregaEtiqueta(Valores, CampoTexto, null, NoFila, 0);

                    AgregaCampo(Valores, CampoTexto, null, NoFila, 1);
                    NoFila++;
                }
                NoCampos = NoFila;
                return true;
            }
            catch { }
            return false;
        }

        public string Vista2JSon(object Contexto = null)
        {
            string FinalJson = "";

            if (m_Configuracion != null)
            {
                foreach (eClockBase.Modelos.Campos.Model_CampoTexto CampoTexto in m_Configuracion)
                {
                    string ValorJson = Vista2JSon(CampoTexto.Nombre, null);
                    if (ValorJson != "")
                        FinalJson = eClockBase.CeC.AgregaSeparador(FinalJson, ValorJson, ",");
                }
            }
            if (Contexto != null)
            {
                PropertyInfo[] properties = Contexto.GetType().GetProperties();
                foreach (PropertyInfo pi in properties)
                {
                    object Valor = pi.GetValue(Contexto, null);
                    string ValorStr = ObtenValorString(Valor);
                    string ValorJson = Json(pi.Name, ValorStr);

                    if (ValorJson != "")
                        FinalJson = eClockBase.CeC.AgregaSeparador(FinalJson, ValorJson, ",");

                }
            }
            return "{" + FinalJson + "}";
        }

        /// <summary>
        /// Obtiene el valor actual ctde una propiedad leyendola de la vista
        /// </summary>
        /// <param name="Propiedad"></param>
        /// <param name="ValorAnterior"></param>
        /// <returns></returns>
        public string Vista2JSon(string Propiedad, object ValorAnterior)
        {
            try
            {
                object Valor = ValorAnterior;
                foreach (FrameworkElement Control in xGrid.Children)
                {
                    if (Control.Name == PreficoCampo + Propiedad)
                    {


                        string ValorStr = ObtenValorString(Control);
                        return Json(Propiedad, ValorStr);
                    }
                }
            }
            catch (Exception ex)
            {
                eClockBase.CeC_Log.AgregaError(ex);
            }

            return "";
        }
        public string Json(string Campo, string Valor)
        {
            return eClockBase.CeC.AgregaSeparador("\"" + Campo + "\"", Valor, ":");
        }

        public static string ObtenValorString(object Objeto)
        {
            String TextoAGuardar = "";
            switch (Objeto.GetType().Name)
            {
                case "DatePicker":
                    TextoAGuardar = JsonConvert.SerializeObject(((DatePicker)Objeto).SelectedDate);
                    break;
                case "CheckBox":
                    TextoAGuardar = JsonConvert.SerializeObject(((CheckBox)Objeto).IsChecked);//.ToString();
                    break;
                case "PasswordBox":
                    TextoAGuardar = JsonConvert.SerializeObject(((PasswordBox)Objeto).Password);
                    break;
                case "UC_Combo":
                    TextoAGuardar = JsonConvert.SerializeObject(((UC_Combo)Objeto).Seleccionado);
                    break;
                case "DateTime":
                    TextoAGuardar = JsonConvert.SerializeObject(((DateTime)Objeto));
                    break;
                case "TimeSpan":
                    TextoAGuardar = JsonConvert.SerializeObject(((TimeSpan)Objeto));
                    break;
                case "String":
                    TextoAGuardar = JsonConvert.SerializeObject(((string)Objeto));
                    break;
                case "UC_Horas":
                    TextoAGuardar = JsonConvert.SerializeObject(((UC_Horas)Objeto).Text);
                    break;
                default:
                    TextoAGuardar = JsonConvert.SerializeObject(((TextBox)Objeto).Text);
                    break;
            }
            return TextoAGuardar;
        }

        public FrameworkElement NuevaEtiqueta(string Nombre, string Idioma, string Campo, bool Ayuda)
        {
            FrameworkElement Etiqueta = new TextBlock();
            Etiqueta.Name = Nombre;
            ((TextBlock)Etiqueta).Text = Campo;
            ((TextBlock)Etiqueta).TextWrapping = TextWrapping.Wrap;

            return Etiqueta;
        }

        public FrameworkElement AgregaEtiqueta(object Contexto, eClockBase.Modelos.Campos.Model_CampoTexto CampoTexto, object Valor, int Fila, int Columna)
        {
            try
            {
                FrameworkElement Etiqueta = NuevaEtiqueta("Lbl_" + CampoTexto.Nombre, "", CampoTexto.Etiqueta, false);
                Etiqueta.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                Etiqueta.VerticalAlignment = System.Windows.VerticalAlignment.Center;

                xGrid.Children.Add(Etiqueta);
                Grid.SetColumn(Etiqueta, Columna);
                Grid.SetRow(Etiqueta, Fila);
                return Etiqueta;
            }
            catch (Exception ex)
            {
                eClockBase.CeC_Log.AgregaError(ex);
            }
            return null;
        }

        public FrameworkElement AgregaCampo(object Contexto, eClockBase.Modelos.Campos.Model_CampoTexto CampoTexto, object Valor, int Fila, int Columna)
        {
            try
            {
                FrameworkElement Campo = null;
                Campo = NuevoCampo(Contexto, CampoTexto, Valor);
                Campo.Margin = new Thickness(5);
                //Campo.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                xGrid.Children.Add(Campo);
                Grid.SetColumn(Campo, Columna);
                Grid.SetRow(Campo, Fila);
                return Campo;
            }
            catch (Exception ex)
            {
                eClockBase.CeC_Log.AgregaError(ex);
            }
            return null;
        }

        public FrameworkElement NuevoCampo(object Contexto, eClockBase.Modelos.Campos.Model_CampoTexto CampoTexto, object Valor)
        {
            FrameworkElement Campo = null;


            ///Si no se encuentra definido el atributo de tipo de campo usará un predeterminado
            switch (CampoTexto.TIPO_DATO_ID)
            {
                case 1:
                    Campo = NuevoTextBox(eClockBase.CeC.Convierte2String(Valor), -1, CampoTexto.Requerido, false);
                    break;
                case 2:
                    Campo = NuevoTextBox(eClockBase.CeC.Convierte2String(Valor), -1, CampoTexto.Requerido, false);
                    break;
                case 8:
                    Campo = NuevoCheckBox(eClockBase.CeC.Convierte2Bool(Valor, false), CampoTexto.Requerido, false);
                    break;
                case 5:
                    Campo = NuevoDatePicker(eClockBase.CeC.Convierte2DateTimeN(Valor, null), null, null, CampoTexto.Requerido, false);
                    break;
                case 7:
                    Campo = NuevoHoras(eClockBase.CeC.Convierte2String(Valor), CampoTexto.Requerido, false);
                    break;
                case 3:
                    Campo = NuevoTextBox(eClockBase.CeC.Convierte2String(Valor), -1, CampoTexto.Requerido, false);
                    break;
                case 10:
                    Campo = NuevoComboBox(Valor, CampoTexto.Requerido, false);
                    break;
                default:
                    Campo = new Label();
                    ((Label)Campo).Content = "No Soportado";
                    break;
            }



            Campo.Name = PreficoCampo + CampoTexto.Nombre;
            return Campo;
        }


        public FrameworkElement NuevoTextBox(string Texto, int LongitudMaxima, bool Requerido, bool SoloLectura)
        {
            TextBox R = new TextBox();
            R.Text = Texto;
            if (LongitudMaxima > 0)
                R.MaxLength = LongitudMaxima;
            R.IsEnabled = !SoloLectura;
            return R;
        }


        public FrameworkElement NuevoCheckBox(bool Checado, bool Requerido, bool SoloLectura)
        {
            CheckBox R = new CheckBox();
            R.IsChecked = Checado;
            R.IsEnabled = !SoloLectura;
            return R;
        }

        public FrameworkElement NuevoDatePicker(DateTime? Fecha, DateTime? FechaMinima, DateTime? FechaMaxima, bool Requerido, bool SoloLectura)
        {
            DatePicker R = new DatePicker();
            if (Fecha != null)
            {
                R.DisplayDate = eClockBase.CeC.Convierte2DateTime(Fecha);
                R.Text = R.DisplayDate.ToString();
            }
            R.DisplayDateStart = FechaMinima;
            R.DisplayDateEnd = FechaMaxima;
            R.IsEnabled = !SoloLectura;
            return R;
        }

        public FrameworkElement NuevoHoras(string Horas, bool Requerido, bool SoloLectura)
        {
            UC_Horas R = new UC_Horas();
            if (Horas != null)
            {
                R.Text = Horas;
            }

            R.IsEnabled = !SoloLectura;
            return R;
        }

        public FrameworkElement NuevoComboBox(object Seleccionado, bool Requerido, bool SoloLectura)
        {
            UC_Combo Combo = new UC_Combo();
            //Combo.Name = Nombre;
            Combo.Seleccionado = Seleccionado;
            Combo.IsEnabled = !SoloLectura;
            return Combo;
        }
    }
}
