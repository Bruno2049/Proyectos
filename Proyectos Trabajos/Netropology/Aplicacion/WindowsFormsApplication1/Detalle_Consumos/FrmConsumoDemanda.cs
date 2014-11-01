using System;
using System.Windows.Forms;
using PAEEEM.Entidades.Trama;

namespace WindowsFormsApplication1.Detalle_Consumos
{
    public partial class FrmConsumoDemanda : Form
    {
        public FrmConsumoDemanda(CompDetalleConsumo detalleConsumos)
        {
            InitializeComponent();

            VerConsumosDemandas(detalleConsumos);

        }


        private void VerConsumosDemandas(CompDetalleConsumo detalleConsumos)
        {
            try
            {
                dtgConsumos.DataSource = detalleConsumos.Consumo;
                dtgDemandas.DataSource = detalleConsumos.Demanda;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
