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
    public partial class Form4 : Form
    {
        Persona Modificar = new Persona();
        public Form4()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Modificar.Id = Convert.ToInt16(textBox1.Text);
            Modificar.Nombre = textBox2.Text;
            Modificar.Apellido = textBox3.Text;
            Modificar.Edad = Convert.ToInt16(textBox4.Text);
            MessageBox.Show("id = "+Modificar.Id+"\nNombre = "+Modificar.Nombre+"\nApellido = "+Modificar.Apellido+"\nEdad = "+Modificar.Edad);
            int Resultado = PersonaDB.Actualizar(Modificar);

            if (Resultado > 0)
            {
                DialogResult Retornar = MessageBox.Show("Se actualizo el registro \nDeceas Modificar otro registro?", "Datos Guardados", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (Retornar == DialogResult.Yes)
                {
                    textBox2.ResetText();
                    textBox3.ResetText();
                    textBox4.ResetText(); 
                    textBox2.Enabled = false;
                    textBox3.Enabled = false;
                    textBox4.Enabled = false;
                }
                else if (Retornar == DialogResult.No)
                {
                    this.Close();
                }
            }

            else
            {
                MessageBox.Show("No Se inserto el registro", "Datos no guardados", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Form1 w = new Form1();
                w.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Modificar.Id = Convert.ToInt32(textBox1.Text);
                List<Persona> Dato = new List<Persona>(PersonaDB.Buscar(Modificar, 3));

                if (Dato.Count == 0)
                {
                    MessageBox.Show(("No se encontro Registro con esta Id = " + textBox1.Text + ""), "Sin registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else if (Dato.Count >= 1)
                {
                    textBox2.Enabled = true;
                    textBox3.Enabled = true;
                    textBox4.Enabled = true;
                    foreach (Persona A in Dato)
                    {
                        textBox2.Text = A.Nombre;
                        textBox3.Text = A.Apellido;
                        textBox4.Text = Convert.ToString(A.Edad);
                    }
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Solo se pueden ingresar numero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.ResetText();
                textBox3.ResetText();
                textBox4.ResetText();
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
