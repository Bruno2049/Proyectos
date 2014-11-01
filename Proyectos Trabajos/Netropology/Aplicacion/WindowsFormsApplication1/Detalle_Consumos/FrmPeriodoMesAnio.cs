using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PAEEEM.Entidades.Trama;

namespace WindowsFormsApplication1.Detalle_Consumos
{
    public partial class FrmPeriodoMesAnio : Form
    {
        public FrmPeriodoMesAnio(CompDetalleConsumo detalleConsumos)
        {
            InitializeComponent();

            VerPeriodoMEsAnio(detalleConsumos);
        }


        private void VerPeriodoMEsAnio(CompDetalleConsumo detalleConsumos)
        {

            try
            {
                dtgPeriodoMes.DataSource = detalleConsumos.PeriodoMes;
                dtgPeriodoAnio.DataSource = detalleConsumos.PeriodoAnio;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



    }
}
