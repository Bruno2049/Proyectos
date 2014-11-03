using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EjemploTreeView
{
    public partial class UserControl1 : UserControl
    {
        public string Texto 
        {
            get { return _texto; } 
            set { _texto = value; }
        }

        private string _texto;
        
        public UserControl1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Texto = textBox1.Text;
        }
    }
}
