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

namespace Properties_and_Events_in_XAML
{
    /// <summary>
    /// Lógica de interacción para SimpleProperties.xaml
    /// </summary>
    public partial class SimpleProperties : Window
    {
        public SimpleProperties()
        {
            InitializeComponent();
        }

        private void cmdAnswer_Click(object sender, RoutedEventArgs e)
        {
            //this.Cursor = Cursors.Wait;
            //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
            //AnswerGenerator generator = new AnswerGenerator();
            //txtAnswer.Text = generator.GetRandomAnswer(txtQuestion.Text);
            //this.Cursor = null;
        }
    }
}
