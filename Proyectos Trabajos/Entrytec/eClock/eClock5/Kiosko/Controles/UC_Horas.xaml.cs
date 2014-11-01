using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#if !NETFX_CORE
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
#else
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
#endif
namespace eClock5.Controles
{
    /// <summary>
    /// Lógica de interacción para UC_Horas.xaml
    /// </summary>
    public partial class UC_Horas : UserControl
    {


        public string Text
        {
            get { return Tbx_Horas.Text; }
            set
            {
                string m_Text = Normaliza(value);
                Tbx_Horas.Text = m_Text;
                Value = eClockBase.CeC.Hora2DateTime(m_Text);
            }
        }
        private string m_Mask;

        public string Mask
        {
            get { return m_Mask; }
            set { m_Mask = value; }
        }
        private string m_Format;

        public string Format
        {
            get { return m_Format; }
            set { m_Format = value; }
        }

        /* private DateTime m_Value;

         public DateTime Value
         {
             get
             {
                 return eClockBase.CeC.Hora2DateTime(Text);
             }
             set
             {
                 m_Value = value;
                 if (value != null)
                 {
                     TimeSpan TS = eClockBase.CeC.DateTime2TimeSpan(value);
                     Text = TimeSpan2Hora(TS);

                 }
                 else
                     Text = "00:00";
             }
         }*/

        public DateTime Value
        {
            get { return eClockBase.CeC.Convierte2DateTime(GetValue(ValueProperty)); }
            set
            {
                SetValue(ValueProperty, value);

            }
        }

        // Using a DependencyProperty as the backing store for Apps.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(DateTime), typeof(UC_Horas),
            new FrameworkPropertyMetadata(DateTime.Today, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnValueChanged
    )));
        private static void OnValueChanged(DependencyObject obj,
    DependencyPropertyChangedEventArgs args)
        {
            if (args.OldValue != args.NewValue)
            {
                string Text = "";
                if (args.NewValue != null)
                {
                    TimeSpan TS = eClockBase.CeC.DateTime2TimeSpan(eClockBase.CeC.Convierte2DateTime(args.NewValue));
                    Text = TimeSpan2Hora(TS);

                }
                else
                    Text = "00:00";
                UC_Horas control = (UC_Horas)obj;
                control.Text = Text;
            }
        }



        public static string TimeSpan2Hora(TimeSpan TS)
        {
            int Horas = TS.Hours + TS.Days * 24;
            return Horas.ToString("00") + ":" + TS.Minutes.ToString("00");
        }
        public TimeSpan ValueTimeSpan
        {
            get
            {
                return eClockBase.CeC.Hora2TimeSpan(Text);
            }
            set
            {
                Text = TimeSpan2Hora(value);
            }
        }

        public UC_Horas()
        {
            InitializeComponent();

        }

        public string Normaliza(string Cadena)
        {
            if (Cadena == null)
                Cadena = "";
            string Texto = Cadena;
            Texto = Texto.Replace(":", "");
            Texto = Texto.PadRight(4, '0');
            if (Convert.ToInt32(Texto.Substring(2, 2)) > 59)
                Texto = Texto.Substring(0, 2) + "59";

            Texto = Texto.Substring(0, 2) + ":" + Texto.Substring(2, 2);
            return Texto;
        }
        private void Tbx_Horas_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Tbx_Horas.Text.Length > 5)
            {
                int Pos = Tbx_Horas.SelectionStart;
                if (Pos >= 0)
                {

                    string Texto = Tbx_Horas.Text;
                    string Inicio = "";
                    string Fin = "";
                    /*
                    if (Text[Pos] == ':')
                    {
                        |
                        return;
                    }*/
                    Texto = Texto.Replace(":", "");
                    if (Pos > 0)
                        Inicio = Texto.Substring(0, Pos <= 4 ? Pos : 4);
                    if (Pos + 1 < Texto.Length)
                        Fin = Texto.Substring(Pos + 1);
                    Texto = Inicio + Fin;
                    Texto = Normaliza(Texto);
                    Tbx_Horas.Text = Texto;
                    if (Texto.Length > Pos && Pos > 0 && Texto[Pos - 1] == ':')
                        Pos++;
                    Tbx_Horas.SelectionStart = Pos;
                    Value = eClockBase.CeC.Hora2DateTime(Texto);
                    if (ValorCambio != null)
                        ValorCambio(sender, e);
                }
            }
            if (Tbx_Horas.Text.Length < 5)
            {
                string Texto = Tbx_Horas.Text;

                int Pos = Tbx_Horas.SelectionStart;
                Texto = Normaliza(Texto);
                if (Tbx_Horas.Text == Texto)
                    return;
                Tbx_Horas.Text = Texto;
                if (Pos >= 0)
                    Tbx_Horas.SelectionStart = Pos;
                Value = eClockBase.CeC.Hora2DateTime(Texto);
                if (ValorCambio != null)
                    ValorCambio(sender, e);

            }
        }

        public event RoutedEventHandler ValorCambio;
    }
}
