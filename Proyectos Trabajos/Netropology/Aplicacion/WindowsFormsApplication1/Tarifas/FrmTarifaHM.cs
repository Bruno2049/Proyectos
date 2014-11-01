using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using PAEEEM.Entidades.Tarifas;

namespace WindowsFormsApplication1.Tarifas
{
    public partial class FrmTarifaHM : Form
    {
        public FrmTarifaHM(List<CompFacturacion> facturaciones)
        {
            InitializeComponent();

            //bindingNavFacturaciones.BindingSource.PositionChanged += new EventHandler(BindingSource_PositionChanged);

            VerTarifaHm(facturaciones);
        }


        private void VerTarifaHm(List<CompFacturacion> facturaciones)
        {
            try
            {
                bindigSourceFacturacion.DataSource = facturaciones;
                bindingNavFacturaciones.BindingSource = bindigSourceFacturacion;

                dtgTHm.DataSource = facturaciones[0].ConceptosFacturacion;

                txtIva.DataBindings.Add(new Binding("Text", bindigSourceFacturacion, "Iva"));
                txtMontFactMnSnIva.DataBindings.Add(new Binding("Text", bindigSourceFacturacion, "MontoFactMensualSNIVA"));
                txtMontoMaxFact.DataBindings.Add(new Binding("Text", bindigSourceFacturacion, "MontoMaxFacturar"));
                txtPagFacturacion.DataBindings.Add(new Binding("Text", bindigSourceFacturacion, "PagoFactBiMen"));
                txtSubTotal.DataBindings.Add(new Binding("Text", bindigSourceFacturacion, "Subtotal"));
                txtTotal.DataBindings.Add(new Binding("Text", bindigSourceFacturacion, "Total"));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private void BindingSource_PositionChanged(object sender, EventArgs e)
        {
            CompFacturacion facturacion = (CompFacturacion)bindingNavFacturaciones.BindingSource.Current;
            dtgTHm.DataSource = facturacion.ConceptosFacturacion;
        }


    }
}
    ;