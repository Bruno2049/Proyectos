using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PAEEEM.Entidades.Tarifas;

namespace WindowsFormsApplication1.Tarifa03
{
    public partial class FrmTarifa03 : Form
    {
        public FrmTarifa03(CompFacturacion facturacion)
        {
            InitializeComponent();

            VerTarifa03(facturacion);
        }


        private void VerTarifa03(CompFacturacion facturacion)
        {
            try
            {
                dtgT03.DataSource = facturacion.ConceptosFacturacion;

                txtIva.Text = facturacion.Iva.ToString();
                txtMontFactMnSnIva.Text = facturacion.MontoFactMensualSNIVA.ToString();
                txtMontoMaxFact.Text = facturacion.MontoMaxFacturar.ToString();
                txtPagFacturacion.Text = facturacion.PagoFactBiMen.ToString();
                txtSubTotal.Text = facturacion.Subtotal.ToString();
                txtTotal.Text = facturacion.Total.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FrmTarifa03_Load(object sender, EventArgs e)
        {

        }

       

    }
}
