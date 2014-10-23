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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            radioButton1.Select();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (radioButton1.Checked == true)
            {
                Persona Busca = new Persona();
                Busca.Nombre = textBox1.Text;
                dataGridView1.DataSource = PersonaDB.Buscar(Busca,1);
                if (dataGridView1.RowCount == 0)
                {
                    MessageBox.Show("No hay registro con estos criterios", "Busqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            else if (radioButton2.Checked == true)
            {
                Persona Busca = new Persona();
                Busca.Apellido = textBox2.Text;
                dataGridView1.DataSource = PersonaDB.Buscar(Busca, 2);
                if (dataGridView1.RowCount == 0)
                {
                    MessageBox.Show("No hay registro con estos criterios", "Busqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (radioButton3.Checked == true)
            {
                Persona Busca = new Persona();
                Busca.Id = Convert.ToInt16(textBox3.Text);
                dataGridView1.DataSource = PersonaDB.Buscar(Busca, 3);
                if (dataGridView1.RowCount == 0)
                {
                    MessageBox.Show("No hay registro con estos criterios", "Busqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }        

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Persona Busca = new Persona();
            dataGridView1.DataSource = PersonaDB.Buscar(null, 4);
            if (dataGridView1.RowCount == 0)
            {
                MessageBox.Show("No hay registro con estos criterios", "Busqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
