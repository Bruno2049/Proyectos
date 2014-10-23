using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Conexion.Controladores;


namespace CRUD_Stored_Procedure_
{
    public partial class FRM_Principal : Form
    {
        ControladorDB Controlador = new ControladorDB();
        
        public FRM_Principal()
        {
            InitializeComponent();
        }

        public bool ValidaInformacion()
        {
            return true;
        }

        private void BTN_Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidaInformacion())
                {
                    string SitioWeb = TXB_SitioWeb.Text.Trim();

                    if (SitioWeb.StartsWith("www."))
                    {
                        SitioWeb = "http://" + SitioWeb;
                        if (Controlador.GuardarRegistro("SP_Insertar", TXB_Nombre.Text, TXB_Apellido.Text, Convert.ToInt32(TXB_Edad.TabIndex), TXB_Direccion.Text
                            , TXB_Correo.Text, TXB_Telefono.Text, TXB_Celular.Text, SitioWeb, TXB_Compania.Text))
                        {
                            MessageBox.Show("Informacion", "El registro se a Guardado Correctamente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LimpiarCampos();
                        }

                        else
                        {
                            MessageBox.Show("No se pudieron Almacenar los registros!", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }

                    else
                    {
                        if (Controlador.GuardarRegistro("SP_Insertar", TXB_Nombre.Text, TXB_Apellido.Text, Convert.ToInt32(TXB_Edad.TabIndex), TXB_Direccion.Text
                            , TXB_Correo.Text, TXB_Telefono.Text, TXB_Celular.Text, null, TXB_Compania.Text))
                        {
                            MessageBox.Show("Informacion", "El registro se a Guardado Correctamente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LimpiarCampos();
                        }

                        else
                        {
                            MessageBox.Show("Informacion", "No se pudieron Almacenar los registros!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception Err)
            { 
            }
        }

        private void BTN_Eliminar_Click(object sender, EventArgs e)
        {
            if (ValidaInformacion())
            {
                if (TXB_Correo.Text == "")
                {
                    MessageBox.Show("Informacion", "Solo se puede borrar registros con correo", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Information);
                }
                else
                {
                    if (Controlador.BorrarRegistro("SP_Eliminar", TXB_Correo.Text))
                    {
                        MessageBox.Show("Se elimino el registro!", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    else
                    {
                        MessageBox.Show("No se pudo eliminor el registro!", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void LimpiarCampos()
        {
            TXB_Nombre.Clear();
            TXB_Apellido.Clear();
            TXB_Edad.Clear();
            TXB_Direccion.Clear();
            TXB_Correo.Clear();
            TXB_Telefono.Clear();
            TXB_Celular.Clear();
            TXB_SitioWeb.Clear();
            TXB_Compania.Clear();
        }
    }
}
