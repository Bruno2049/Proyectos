using System;
//using System.Collections.Generic;
//using System.ComponentModel;
using System.Data;
//using System.Drawing;
using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using EjemploTreeView.Clases;
using EjemploTreeView.Modelos;

namespace EjemploTreeView
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCargarTreeView_Click(object sender, EventArgs e)
        {
            trvMostrarJefes.Nodes.Clear();

            var gestionEmp = new GestionEmpleados();
            var lista = gestionEmp.NodoHijos(0);

            TreeNode padre = null;
            TreeNode hijo = null;

            if (lista != null)
            {
                foreach (var item in lista)
                {
                    padre = new TreeNode(item.NombreEmpleado);
                    padre = InsertaHijo(item,padre);
                    trvMostrarJefes.Nodes.Add(padre);
                }
            }
            else
            {
                MessageBox.Show(@"No hay registros");
            }

        }

        private static TreeNode InsertaHijo(Empleados item,TreeNode padre)
        {
            var gestionEmp = new GestionEmpleados();
            var listaHijos = gestionEmp.NodoHijos(item.IdEmpleados);

            //foreach (var subItem in listaHijos)
            //{
            //    if (subItem.IdJeje != 0)
            //    {
            //        var hijo = new TreeNode(subItem.NombreEmpleado);
            //        hijo = InsertaHijo(subItem, hijo);
            //        padre.Nodes.Add(hijo);
            //    }
            //}

            foreach (var hijo in from subItem in listaHijos where subItem.IdJeje != 0 let hijo = new TreeNode(subItem.NombreEmpleado) select InsertaHijo(subItem, hijo))
            {
                padre.Nodes.Add(hijo);
            }
            return padre;
        }

        private void btnCargarGrid_Click(object sender, EventArgs e)
        {
            var empleados = new GestionEmpleados().ListaEmpleados;
            dgvEmpleados.DataSource = empleados;
        }
    }
}
