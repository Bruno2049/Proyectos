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
    /// Lógica de interacción para UC_MovimientosNomina.xaml
    /// </summary>
    public partial class UC_MovimientosNomina : UserControl
    {
        private int m_AnchoControl = 0;
        public int ClasificMovID { get; set; }
        public bool MostrarSaldo { get; set; }
        public List<eClockBase.Modelos.Model_REC_NOMI_MOV> Movimientos { get; set; }

        public decimal SumImporte = 0;
        int numFilas;

        public UC_MovimientosNomina()
        {
            InitializeComponent();
        }
        public bool ActualizaVista(List<eClockBase.Modelos.Model_REC_NOMI_MOV> mMovimientos, int iClasificMovID, bool bMostrarSaldo)
        {
            Movimientos = mMovimientos;
            ClasificMovID = iClasificMovID;
            MostrarSaldo = bMostrarSaldo;
            Grd_Movimientos.RowDefinitions.Clear();
            Grd_Movimientos.ColumnDefinitions.Clear();
            RowDefinition FilaTitulos = new RowDefinition();
            Grd_Movimientos.RowDefinitions.Add(FilaTitulos);
            if (iClasificMovID == 1)
            {
                NuevaColumna("CVE", 90, "Clave");
                NuevaColumna("Concepto", 222, "Concepto");
                NuevaColumna("Tiempo", 80, "Hrs o Días");
                NuevaColumna("Importe", 120, "Importe");
            }
            else
            {
                NuevaColumna("CVE", 70, "Clave");
                NuevaColumna("Concepto", 170, "Concepto");
                NuevaColumna("Tiempo", 67, "Hrs o Días");
                NuevaColumna("Importe", 100, "Importe");
            }
            if (bMostrarSaldo)
            {
                NuevaColumna("Sal", 105, "Acumulado o Saldo");
            }
            ColumnDefinition Columna = new ColumnDefinition();
            Columna.Width = new GridLength(100, GridUnitType.Star);
            Grd_Movimientos.ColumnDefinitions.Add(Columna);

            numFilas = Movimientos.Count() + 1;

            foreach (eClockBase.Modelos.Model_REC_NOMI_MOV Movimiento in Movimientos)
            {
                SumImporte = Movimiento.REC_NOMI_MOV_IMPORTE + SumImporte;
                if (Movimiento.CLASIFIC_MOV_ID != ClasificMovID)
                    continue;
                RowDefinition Fila = new RowDefinition();

                Grd_Movimientos.RowDefinitions.Add(Fila);

                int NoFila = Grd_Movimientos.RowDefinitions.IndexOf(Fila);

                NuevaEtiqueta(Movimiento.REC_NOMI_MOV_CVE, NoFila, "CVE");
                NuevaEtiqueta(Movimiento.REC_NOMI_MOV_CONCEPTO, NoFila, "Concepto");
                NuevaEtiqueta(Movimiento.REC_NOMI_MOV_UNIDAD.ToString(), NoFila, "Tiempo");
                NuevaEtiqueta(Movimiento.REC_NOMI_MOV_IMPORTE.ToString("C"), NoFila, "Importe");
                if (bMostrarSaldo)
                    NuevaEtiqueta(Movimiento.REC_NOMI_MOV_SALDO.ToString(), NoFila, "Sal");

            }

            //RowDefinition FilaImporte = new RowDefinition();
            //Grd_Movimientos.RowDefinitions.Add(FilaImporte);
            //NuevaEtiqueta(eClockBase.CeC.Convierte2String(SumImporte), numFilas + 1, "Importe");

            RowDefinition FilaVacia = new RowDefinition();
            FilaVacia.Height = new GridLength(100, GridUnitType.Star);
            Grd_Movimientos.RowDefinitions.Add(FilaVacia);

            return true;
        }

        public TextBlock NuevaEtiqueta(string Texto, int Fila, string NombreColumna)
        {
            int NoColumna = ObtenColumnaNO(NombreColumna);
            bool EsPar = false;
            if (Fila++ % 2 == 0)
                EsPar = true;
            TextBlock Etiqueta = new TextBlock();
            Etiqueta.Margin = new Thickness(1);
            
            if(Texto != null)
                Etiqueta.Text = Texto.Trim(); ;

            Etiqueta.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            Etiqueta.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            Etiqueta.TextWrapping = TextWrapping.Wrap;
            Etiqueta.TextTrimming = TextTrimming.WordEllipsis;
            if (EsPar)
                Etiqueta.Background = Recursos.GrisClaro_Brush;
            else
                Etiqueta.Background = Recursos.Blanco_Brush;
            Etiqueta.FontSize = Recursos.FontSizeSubTitle;
            if ((NombreColumna != "Importe") || (Texto == "Importe"))
            {
                Etiqueta.TextAlignment = TextAlignment.Center;
            }
            else
            {
                Etiqueta.TextAlignment = TextAlignment.Right;
            }

            Grid.SetRow(Etiqueta, Fila);
            Grid.SetColumn(Etiqueta, NoColumna);
            Grd_Movimientos.Children.Add(Etiqueta);
            return Etiqueta;
        }
        public int ObtenColumnaNO(string NombreColumna)
        {

            try
            {
                var Columna = from c in Grd_Movimientos.ColumnDefinitions
                              where c.Name == NombreColumna
                              select Grd_Movimientos.ColumnDefinitions.IndexOf(c);

                return Columna.ElementAt<int>(0);

            }
            catch { return -1; }
        }
        public ColumnDefinition NuevaColumna(string Nombre, int Ancho, string Titulo = null)
        {
            ColumnDefinition Columna = new ColumnDefinition();
            Columna.Name = Nombre;
            /*if (Nombre == "Seleccionado" || Nombre == "Color")
                Columna.Width = new GridLength(Ancho);
            else
                Columna.Width = GridLength.Auto;*/
            Columna.Width = new GridLength(Ancho);
            Grd_Movimientos.ColumnDefinitions.Add(Columna);
            if (Titulo == null)
                Titulo = Nombre;
            m_AnchoControl += Ancho;
            TextBlock TB = NuevaEtiqueta(Titulo, 0, Nombre);
            TB.FontSize = Recursos.FontSizeTitle;

            return Columna;
        }
    }
}
