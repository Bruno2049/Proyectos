using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using School.AccesoDatos;

namespace School.Vista
{
    /// <summary>
    /// Lógica de interacción para ViewStudents.xaml
    /// </summary>
    public partial class ViewStudents : Window
    {
        public ViewStudents()
        {
            InitializeComponent();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            var Lista = ViewStudentsA.ClassIntance.GetListStudents();
            StudentGrid.ItemsSource = Lista;
        }
    }
}
