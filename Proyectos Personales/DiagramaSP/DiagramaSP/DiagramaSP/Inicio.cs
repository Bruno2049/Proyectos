using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DiagramaSP.Clases;
using DiagramaSP.Modelo;

namespace DiagramaSP
{
    public partial class Inicio : Form
    {
        private List<Nodo> _listaNodos;
        private ControlDeArchivos _controlDeArchivos = new ControlDeArchivos();

        public Inicio()
        {
            InitializeComponent();
        }

        private void CargarArbol()
        {
        }

        private void Limpiar()
        {
            txbCodigoFuncion.Text = string.Empty;
            txbCodigoFuncionHijo.Text = string.Empty;
            txbIdNodo.Text = string.Empty;
            txbIdNodoPadre.Text = string.Empty;
            txbNoLinea.Text = string.Empty;
            tbxNombreFuncion.Text = string.Empty;

            CargarArbol();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {

                var nodo = new Nodo
                {
                    IdFuncion = Convert.ToInt32(txbIdNodo.Text),
                    IdFuncionPadre = Convert.ToInt32(txbIdNodoPadre.Text),
                    NoLineaFuncion = Convert.ToInt32(txbNoLinea.Text),
                    NombreFuncion = tbxNombreFuncion.Text,
                    Codigo = txbCodigoFuncion.Text,
                    CodigoInterno = txbCodigoFuncionHijo.Text
                };

                if (_controlDeArchivos.InsetarRegistro(nodo))
                {
                    MessageBox.Show(@"El registro fue guardado correctamente", @"Registro guardado", MessageBoxButtons.OK,
                    MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    Limpiar();
                }

                else
                {
                    {
                        MessageBox.Show(@"El registro no fue guardado", @"Error al guardar registro", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        Limpiar();
                    }
                }

            }
            catch (Exception)
            {
                MessageBox.Show(@"Hay datos no validos en los datos ingresador", @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                Limpiar();
            }
        }
    }
}
