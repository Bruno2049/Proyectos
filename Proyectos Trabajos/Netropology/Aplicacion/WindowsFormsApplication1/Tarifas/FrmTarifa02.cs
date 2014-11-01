using System;
using System.Windows.Forms;
using PAEEEM.Entidades.Tarifas;

namespace WindowsFormsApplication1.Tarifas
{
    public partial class FrmTarifa02 : Form
    {
        public FrmTarifa02(CompFacturacion facturacion)
        {
            InitializeComponent();

            VerTarifa02(facturacion);
        }


        private void VerTarifa02(CompFacturacion facturacion)
        {
            try
            {
                dtgT02.DataSource = facturacion.ConceptosFacturacion;

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
    }
}
