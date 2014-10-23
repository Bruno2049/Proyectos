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
    public partial class Form3 : Form
    {
        Persona Eliminar = new Persona();
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Eliminar.Id = Convert.ToInt32(textBox1.Text);
                List<Persona> Dato = new List<Persona>(PersonaDB.Buscar(Eliminar, 3));

                if (Dato.Count == 0)
                {
                    MessageBox.Show(("No se encontro Registro con esta Id = " + textBox1.Text + ""), "Sin registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox2.ResetText();
                    textBox3.ResetText();
                    textBox4.ResetText();
                }

                else if (Dato.Count >= 1)
                {
                    foreach (Persona A in Dato)
                    {
                        textBox2.Text = A.Nombre;
                        textBox3.Text = A.Apellido;
                        textBox4.Text = Convert.ToString(A.Edad);
                        button2.Enabled = true;
                    }
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Solo se pueden ingresar numero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.ResetText();
                textBox3.ResetText();
                textBox4.ResetText();
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            button2.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Eliminar.Id = Convert.ToInt16(textBox1.Text);
            Eliminar.Nombre = textBox2.Text;
            Eliminar.Apellido = textBox3.Text;
            Eliminar.Edad = Convert.ToInt16(textBox4.Text);

            int Resultado = PersonaDB.Eliminar(Eliminar);
            if (Resultado > 0)
            {
                DialogResult Retornar = MessageBox.Show("Se Elimino el registro \nDeceas Eliminar otro registro?", "Dato Eliminado", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (Retornar == DialogResult.Yes)
                {
                    textBox2.ResetText();
                    textBox3.ResetText();
                    textBox4.ResetText();
                    textBox1.ResetText();
                    button2.Enabled = false;
                }
                else if (Retornar == DialogResult.No)
                {
                    this.Close();
                }
            }

            else
            {
                MessageBox.Show("No Se Elimino el registro", "Datos no Eliminados", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Form1 w = new Form1();
                w.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
