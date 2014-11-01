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
    /// Lógica de interacción para UC_Mes.xaml
    /// </summary>
    public partial class UC_Mes : UserControl
    {

        public class DiasColorClass
        {
            public string ID;
            public DateTime Dia;
            public Color Color;
            public string Texto;
            public object Tag;
            public DiasColorClass()
            { }
            public DiasColorClass(DateTime dtDia, Color cColor)
            {

                Dia = dtDia;
                Color = cColor;

            }
            public DiasColorClass(string sID, DateTime dtDia, Color cColor, string sTexto, object oTag)
            {
                ID = sID;
                Dia = dtDia;
                Color = cColor;
                Texto = sTexto;
                Tag = oTag;
            }

        }

        public List<DiasColorClass> DiasColor = new List<DiasColorClass>();

        public void ActualizaDiasColor(List<DiasColorClass> DiasColorNuevo)
        {
            DiasColor = DiasColorNuevo;
            ActualizaCalendario();
        }
        public DiasColorClass BuscaDia(DateTime Dia)
        {
            foreach (DiasColorClass DiaColor in DiasColor)
            {
                if (DiaColor.Dia == Dia)
                    return DiaColor;
            }
            return null;
        }
        private bool m_MultipleSeleccion = true;

        public bool MultipleSeleccion
        {
            get { return m_MultipleSeleccion; }
            set { m_MultipleSeleccion = value; }
        }

        private DateTime m_Mes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        public delegate void MesChangedArgs(UC_Mes ControlMes);
        public event MesChangedArgs MesChanged;
        public DateTime Mes
        {
            get { return m_Mes; }
            set
            {
                if (m_Mes == value)
                    return;
                m_Mes = value;
                if (MesChanged != null)
                    MesChanged(this);
                ActualizaCalendario();
            }
        }
        private DateTime m_DiaSeleccionado = new DateTime();

        public DateTime DiaSeleccionado
        {
            get { return m_DiaSeleccionado; }
            set { m_DiaSeleccionado = value; }
        }

        UC_Dia m_ControlDiaSeleccionado = null;

        List<DateTime> m_DiasSeleccionados = new List<DateTime>();

        public List<DateTime> DiasSeleccionados
        {
            get { return m_DiasSeleccionados; }
            set { m_DiasSeleccionados = value; }
        }

        public int NoSeleccionados
        {
            get { return m_DiasSeleccionados.Count; }
        }

        public UC_Mes()
        {
            InitializeComponent();
        }
        private bool m_MostrarDiasFueraMes = true;

        public bool MostrarDiasFueraMes
        {
            get { return m_MostrarDiasFueraMes; }
            set { m_MostrarDiasFueraMes = value; }
        }
        public bool DeSeleccionar()
        {
            m_DiasSeleccionados = new List<DateTime>();
            m_DiaSeleccionado = new DateTime();
            if (SeleccionadoChanged != null)
                SeleccionadoChanged(this);
            return ActualizaCalendario();
        }
        public bool ActualizaCalendario()
        {
            // return false;
            DateTime Dia = Mes;
            int Fila = 1;
            int Columna = 0;
            if (Mes.DayOfWeek == DayOfWeek.Sunday)
                Dia = Mes.AddDays(-7);
            else
                Dia = Mes.AddDays(-(int)Mes.DayOfWeek);
            string Texto = Mes.ToString("MMMM yyyy");
            Texto = Texto.Substring(0, 1).ToUpper() + Texto.Substring(1);

            Lbl_Mes.Text = Texto ;


            //while (Dia.Month == Mes.Month)
            for (int Cont = 0; Cont < 42; Cont++)
            {
                if (Dia.DayOfWeek == DayOfWeek.Sunday && Cont > 0)
                    Fila++;
                Columna = (int)Dia.DayOfWeek;
                UC_Dia Control = null;
                foreach (UIElement element in Grd_Mes.Children)
                {
                    if (Grid.GetRow(element) == Fila && Grid.GetColumn(element) == Columna)
                    {
                        Control = (UC_Dia)element;
                        break;
                    }
                }
                //UC_Dia Control = (UC_Dia )Grd_Mes.Children.Cast<UIElement>().First(e => Grid.GetRow(e) == Fila && Grid.GetColumn(e) == Columna);

                if (Dia.Month != Mes.Month)
                {
                    Control.Tenue = true;
                    Control.Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {
                    Control.Tenue = false;
                    Control.Visibility = System.Windows.Visibility.Visible;
                }
                Control.Dia = Dia;
                if (DiasSeleccionados.IndexOf(Dia) >= 0)
                    Control.Seleccionado = true;
                else
                    Control.Seleccionado = false;

                DiasColorClass DiaColor = BuscaDia(Dia);
                if (DiaColor != null)
                {
                    Control.Tag = DiaColor;
                    Control.ColorDia = DiaColor.Color;
                }
                else
                    Control.ColorDia = Colors.White;
                Dia = Dia.AddDays(1);
            }


            return true;
        }

        private void Btn_Anterior_Click(object sender, RoutedEventArgs e)
        {

            Mes = Mes.AddMonths(-1);

        }

        private void Btn_Siguiente_Click(object sender, RoutedEventArgs e)
        {

            Mes = Mes.AddMonths(1);

        }

        private void Grd_Mes_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ActualizaCalendario();
        }

        private void UC_Dia_SeleccionadoChanged(UC_Dia ControlDia, bool Seleccionado)
        {
            if (MultipleSeleccion)
            {
                if (Seleccionado)
                {
                    DiasSeleccionados.Add(ControlDia.Dia);
                }
                else
                {
                    DiasSeleccionados.Remove(ControlDia.Dia);
                }
            }
            else
            {
                if (m_ControlDiaSeleccionado != null && ControlDia != m_ControlDiaSeleccionado)
                    m_ControlDiaSeleccionado.Seleccionado = false;
                m_ControlDiaSeleccionado = ControlDia;
                DiaSeleccionado = ControlDia.Dia;
                if (!Seleccionado)
                    ControlDia.Seleccionado = true;

            }
            if (SeleccionadoChanged != null)
                SeleccionadoChanged(this);
        }

        public delegate void SeleccionadoChangedArgs(UC_Mes ControlMes);
        public event SeleccionadoChangedArgs SeleccionadoChanged;


    }
}
