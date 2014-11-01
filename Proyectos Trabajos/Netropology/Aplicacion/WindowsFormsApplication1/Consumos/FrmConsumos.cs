using System;
using System.Windows.Forms;
using PAEEEM.Entidades.Trama;

namespace WindowsFormsApplication1.Consumos
{
    public partial class FrmConsumos : Form
    {
        public FrmConsumos(CompConsumo consumos)
        {
            InitializeComponent();

            VerConsumos(consumos);
        }

        private void VerConsumos(CompConsumo consumos)
        {
            try
            {
                dtgConFechas.DataSource = consumos.Fechas;
                dtgConNum.DataSource = consumos.Consumos;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
