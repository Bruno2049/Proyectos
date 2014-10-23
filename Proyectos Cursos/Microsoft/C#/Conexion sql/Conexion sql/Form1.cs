using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Conexion_sql
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            radioButton1.Select();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 Insertar = new Form2();
            Form3 Borrar = new Form3();
            Form4 Modificar = new Form4();
            Form5 Buscar = new Form5();

            if (radioButton1.Checked == true)
            {
                DialogResult A = MessageBox.Show("Deseas insertar registro?","Insertar",MessageBoxButtons.YesNo);

                if (A == DialogResult.Yes)
                {
                    Insertar.Show();
                }

            }

            else if (radioButton2.Checked == true)
            {
                 DialogResult B = MessageBox.Show("Deseas Borrar registro?","Eliminar",MessageBoxButtons.YesNo);

                if (B == DialogResult.Yes)
                {
                    Borrar.Show();
                }
            }
            else if (radioButton3.Checked == true)
            {
                DialogResult C = MessageBox.Show("Deseas Modifica registro?", "Modificar", MessageBoxButtons.YesNo);

                if (C == DialogResult.Yes)
                {
                    Modificar.Show();
                }
            }

            else if (radioButton4.Checked == true)
            {
                Buscar.Show();
            }
    
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
