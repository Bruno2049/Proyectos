using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EjemploTreeView.Clases;

namespace EjemploTreeView
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var GestionEmp = new GestionEmpleados();
            var lista = GestionEmp.RegresaEmpleados(0);
        }
    }
}
