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

namespace eClock5.Controles
{
    /// <summary>
    /// Lógica de interacción para UC_ColorPicker.xaml
    /// </summary>
    public partial class UC_ColorPicker : UserControl
    {
        public SolidColorBrush CurrentColor
        {
            get { return (SolidColorBrush)GetValue(CurrentColorProperty); }
            set { SetValue(CurrentColorProperty, value); }
        }

        public long CurrentColorLong
        {
            get { return eClockBase.CeC.Hex2Long(CurrentColor.Color.ToString()); }
            set { CurrentColor = new SolidColorBrush(UIntToColor(Convert.ToUInt32(value))); }
        }


        private Color UIntToColor(uint color)
        {
            byte a = (byte)(color >> 24);
            byte r = (byte)(color >> 16);
            byte g = (byte)(color >> 8);
            byte b = (byte)(color >> 0);
            return Color.FromArgb(a, r, g, b);
        }
        public static DependencyProperty CurrentColorProperty =
            DependencyProperty.Register("CurrentColor", typeof(SolidColorBrush), typeof(UC_ColorPicker), new PropertyMetadata(Brushes.Black));

        public static RoutedUICommand SelectColorCommand = new RoutedUICommand("SelectColorCommand", "SelectColorCommand", typeof(UC_ColorPicker));
        private Window _advancedPickerWindow;

        public UC_ColorPicker()
        {
            DataContext = this;
            InitializeComponent();
            CommandBindings.Add(new CommandBinding(SelectColorCommand, SelectColorCommandExecute));
        }
        private void SelectColorCommandExecute(object sender, ExecutedRoutedEventArgs e)
        {
            CurrentColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString(e.Parameter.ToString()));
        }

        private static void ShowModal(Window advancedColorWindow)
        {
            advancedColorWindow.Owner = Application.Current.MainWindow;
            advancedColorWindow.ShowDialog();
        }

        void AdvancedPickerPopUpKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                _advancedPickerWindow.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = false;
            e.Handled = false;
        }

        private void MoreColorsClicked(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = false;
            var advancedColorPickerDialog = new UC_ColorPickerDialog();
            _advancedPickerWindow = new Window
            {
                AllowsTransparency = true,
                Content = advancedColorPickerDialog,
                WindowStyle = WindowStyle.None,
                ShowInTaskbar = false,
                Background = new SolidColorBrush(Colors.Transparent),
                Padding = new Thickness(0),
                Margin = new Thickness(0),
                WindowState = WindowState.Normal,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                SizeToContent = SizeToContent.WidthAndHeight
            };
            _advancedPickerWindow.DragMove();
            _advancedPickerWindow.KeyDown += AdvancedPickerPopUpKeyDown;
            advancedColorPickerDialog.DialogResultEvent += AdvancedColorPickerDialogDialogResultEvent;
            advancedColorPickerDialog.Drag += advancedColorPickerDialog_Drag;
            ShowModal(_advancedPickerWindow);
        }

        void advancedColorPickerDialog_Drag(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            _advancedPickerWindow.DragMove();
        }
        public int iColorActual
        {
            get { return int.Parse(sColorActual.Substring(1), System.Globalization.NumberStyles.HexNumber); }
            set {
                try
                {
                    sColorActual = "#" +  value.ToString("X").PadLeft(8,'0');
                }
                catch { }
            }

        }
        public string sColorActual
        {
            get { return CurrentColor.Color.ToString(); }
            set { CurrentColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString(value)); }
        }

        void AdvancedColorPickerDialogDialogResultEvent(object sender, EventArgs e)
        {
            _advancedPickerWindow.Close();
            var dialogEventArgs = (UC_ColorPickerDialog.DialogEventArgs)e;
            if (dialogEventArgs.DialogResult == UC_ColorPickerDialog.DialogResult.Cancel)
                return;
            CurrentColor = dialogEventArgs.SelectedColor;
        }

    }
}
