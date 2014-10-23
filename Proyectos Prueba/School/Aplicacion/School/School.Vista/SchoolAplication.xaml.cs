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
using System.Windows.Navigation;
using System.Windows.Shapes;
using School.AccesoDatos;

namespace School.Vista
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class SchoolAplication : Window
    {
        public SchoolAplication()
        {
            InitializeComponent();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
        }

        private void ViewStudents_Click(object sender, RoutedEventArgs e)
        {
            ViewStudents Students = new ViewStudents();
            Students.ShowDialog();
        }

        private void MI_Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MI_AddStudent_Click_1(object sender, RoutedEventArgs e)
        {
            AddStudent AddStudent = new AddStudent();
            AddStudent.ShowDialog();
        }
    }
}
