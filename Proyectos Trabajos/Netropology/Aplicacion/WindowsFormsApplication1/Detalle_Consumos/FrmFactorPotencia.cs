using System;
using System.Windows.Forms;
using PAEEEM.Entidades.Trama;

namespace WindowsFormsApplication1.Detalle_Consumos
{
    public partial class FrmFactorPotencia : Form
    {
        public FrmFactorPotencia(CompDetalleConsumo detalleConsumos)
        {
            InitializeComponent();
            VerConsumosDemandas(detalleConsumos);
        }


        private void VerConsumosDemandas(CompDetalleConsumo detalleConsumos)
        {
            try
            {
                dtgFactorPotencia.DataSource = detalleConsumos.FactorPotencia;                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
