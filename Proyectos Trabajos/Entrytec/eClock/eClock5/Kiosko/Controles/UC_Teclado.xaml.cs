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
using System.Collections.ObjectModel;
using System.Windows.Threading;
using System.Runtime.InteropServices;


namespace Kiosko.Controles
{
    /// <summary>
    /// Lógica de interacción para UC_Teclado.xaml
    /// </summary>
    public partial class UC_Teclado : UserControl
    {
        UC_Tecla.TipoMuestra m_TipoMuestra = UC_Tecla.TipoMuestra.Tecla;

        public string convertido;


        public UC_Teclado()
        {
            InitializeComponent();
        }
        bool Mayus = false;
        public static System.Windows.Input.Key ToWPFKey(string inputKey)
        {
            // Put special case logic here if there's a key you need but doesn't map...  
            try
            {
                return (System.Windows.Input.Key)Enum.Parse(typeof(System.Windows.Input.Key), inputKey);
            }
            catch
            {
                // There wasn't a direct mapping...    
                return System.Windows.Input.Key.None;
            }
        }

        public static int Asc(string s)
        {
            byte a = Encoding.ASCII.GetBytes(s)[0];
            return Encoding.ASCII.GetBytes(s)[0];
        }

        public static char Chr(int c)
        {
            return Convert.ToChar(c);
        }

        private void UC_Tecla_Clickeado(UC_Tecla ControlTecla)
        {
            var F = Keyboard.FocusedElement;
            switch (ControlTecla.TeclaPresionada.ToUpper())
            {
                case "ENTER":
                    //WindowsInput.InputSimulator.SimulateKeyPress(WindowsInput.VirtualKeyCode.RETURN);
                    if (ControlesEnter.IndexOf(F) >= 0)
                    {
                        if (BotonEnter != null)
                        {
                            BotonEnter.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Primitives.ButtonBase.ClickEvent));
                        }
                        else
                            WindowsInput.InputSimulator.SimulateKeyPress(WindowsInput.VirtualKeyCode.RETURN);
                    }
                    else
                        WindowsInput.InputSimulator.SimulateKeyPress(WindowsInput.VirtualKeyCode.TAB);
                    break;
                case "BORRAR":
                    WindowsInput.InputSimulator.SimulateKeyPress(WindowsInput.VirtualKeyCode.BACK);
                    break;
                case "Ñ":
                    if (Mayus)
                        WindowsInput.InputSimulator.SimulateKeyDown(WindowsInput.VirtualKeyCode.SHIFT);
                    WindowsInput.InputSimulator.SimulateKeyPress(WindowsInput.VirtualKeyCode.OEM_3);
                    if (Mayus)
                        WindowsInput.InputSimulator.SimulateKeyUp(WindowsInput.VirtualKeyCode.SHIFT);
                    //WindowsInput.InputSimulator.SimulateModifiedKeyStroke(
                    //WindowsInput.InputSimulator.SimulateModifiedKeyStroke(new [] {WindowsInput.VirtualKeyCode.MENU, WindowsInput.VirtualKeyCode.NUMPAD1, WindowsInput.VirtualKeyCode.NUMPAD6, WindowsInput.VirtualKeyCode.NUMPAD4}, WindowsInput.VirtualKeyCode.MENU);
                    //WindowsInput.InputSimulator.SimulateModifiedKeyStroke( WindowsInput.VirtualKeyCode.
                    /*WindowsInput.InputSimulator.SimulateModifiedKeyStroke(
                        new[] { WindowsInput.VirtualKeyCode.CONTROL, WindowsInput.VirtualKeyCode.LWIN, WindowsInput.VirtualKeyCode.TAB },
                        null);*/
                    //System.Windows.Forms.SendKeys.SendWait("%(164)");


                    break;
                case "ESPACIO":
                    WindowsInput.InputSimulator.SimulateKeyPress(WindowsInput.VirtualKeyCode.SPACE);
                    break;
                case "MAYÚS":
                    Mayus = !Mayus;
                    //WindowsInput.InputSimulator.SimulateKeyPress(WindowsInput.VirtualKeyCode.CAPITAL);
                    ActualizaMayus();
                    break;
                case "?123":
                    m_TipoMuestra = UC_Tecla.TipoMuestra.TeclaAlterna;
                    ActualizaTipoMuestra(Grd_Main);
                    break;
                case "ABC":
                    m_TipoMuestra = UC_Tecla.TipoMuestra.Tecla;
                    ActualizaTipoMuestra(Grd_Main);
                    break;
                default:
                    if (m_TipoMuestra == UC_Tecla.TipoMuestra.Tecla)
                        WindowsInput.InputSimulator.SimulateTextEntry(ControlTecla.Tecla);
                    else
                        WindowsInput.InputSimulator.SimulateTextEntry(ControlTecla.TeclaAlterna);
                    break;
            }

        }
        void ActualizaTipoMuestra(Grid Grd)
        {
            foreach (UIElement element in Grd.Children)
            {
                try
                {
                    UC_Tecla Tecla = (UC_Tecla)element;
                    Tecla.TeclaAMostrar = m_TipoMuestra;
                    continue;
                }
                catch
                {
                }
                try
                {
                    ActualizaTipoMuestra((Grid)element);
                }
                catch { }
            }
        }
        void ActualizaMayus(bool Mayus, Grid Grd)
        {
            foreach (UIElement element in Grd.Children)
            {
                try
                {
                    UC_Tecla Tecla = (UC_Tecla)element;
                    if (Mayus)
                    {
                        Tecla.Tecla = Tecla.Tecla.ToUpper();
                    }
                    else
                    {
                        Tecla.Tecla = Tecla.Tecla.ToLower();
                    }
                    continue;
                }
                catch
                {
                }
                try
                {
                    ActualizaMayus(Mayus, (Grid)element);

                }
                catch { }
            }
        }
        public void ActualizaMayus()
        {
            //bool Mayus = Mayus;// Keyboard.IsKeyDown(Key.CapsLock);
            ActualizaMayus(Mayus, Grd_Main);
        }
        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //ActualizaMayus();
            Console.WriteLine("UserControl_IsVisibleChanged");
            WindowsInput.InputSimulator.SimulateKeyPress(WindowsInput.VirtualKeyCode.TAB);
        }

        public IInputElement FocoPrincipal = null;
        IInputElement ElementoAnterior = null;
        public List<IInputElement> ControlesEnter = new List<IInputElement>();

        public Button BotonEnter = null;

        private void UserControl_LayoutUpdated_1(object sender, EventArgs e)
        {
            /*if (Keyboard.FocusedElement == ElementoAnterior)
                return;
            this.
            ElementoAnterior = Keyboard.FocusedElement;
            if (Keyboard.FocusedElement == null || Keyboard.FocusedElement == null)
            return;
            ElementoAnterior = Keyboard.FocusedElement;
            
            if (Keyboard.FocusedElement == null)
                Keyboard.Focus(FocoPrincipal);
            else
            {
                string Tipo = ElementoAnterior.GetType().Name;
                if ( Tipo != "TextBox" || Tipo != "PasswordBox" || Tipo != "Button")
                {
                    Keyboard.Focus(FocoPrincipal);

                }
            }*/
        }
        DispatcherTimer Timer = new DispatcherTimer();
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //return;
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                // Design-mode specific functionality
                Keyboard.Focus(FocoPrincipal);

                Timer.Tick += Timer_Tick;
                Timer.Interval = new TimeSpan(0, 0, 1);
                Timer.Start();

            }
        }
        private void AsignaFocusPrincipal()
        {
            Keyboard.Focus(FocoPrincipal);
            Timer.Stop();
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            if (Keyboard.FocusedElement == null)
                AsignaFocusPrincipal();
            else
            {
                string Tipo = Keyboard.FocusedElement.GetType().Name;
                if (Tipo != "TextBox" && Tipo != "PasswordBox" && Tipo != "Button")
                {
                    AsignaFocusPrincipal();
                    //Keyboard.Focus(FocoPrincipal);
                }
            }
        }
    }
}
