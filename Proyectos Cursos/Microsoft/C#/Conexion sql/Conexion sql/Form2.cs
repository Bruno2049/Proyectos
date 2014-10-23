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
    public partial class Form2 : Form
    {
        private String Nombre;
        private String Apellido;
        private int Edad;

        Persona Datos = new Persona();

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Nombre = textBox1.Text;
            Apellido = textBox2.Text;
            try
            {
                Edad = Convert.ToInt16(textBox3.Text);

            }
            catch (FormatException)
            {
                MessageBox.Show("Solo se pueden ingresar numeros", "Error al ingresar registro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox3.ResetText();
            }
            Datos.Nombre = this.Nombre;
            Datos.Apellido = this.Apellido;
            Datos.Edad = this.Edad;
            int Resultado = PersonaDB.Insertar(Datos);

            if (Resultado > 0)
            {
                DialogResult Retornar = MessageBox.Show("Se inserto el registro \nDeceas ingresar otro registro?","Datos Guardados",MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                Form2 A = new Form2();

                if (Retornar == DialogResult.Yes)
                {
                    textBox1.ResetText();
                    textBox2.ResetText();
                    textBox3.ResetText();
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
    }
}