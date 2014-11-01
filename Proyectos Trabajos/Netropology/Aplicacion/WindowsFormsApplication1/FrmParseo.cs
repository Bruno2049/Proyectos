using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PAEEEM.Entidades.Tarifas;
using PAEEEM.LogicaNegocios.Tarifas;
using PAEEEM.LogicaNegocios.Trama;
using WindowsFormsApplication1.Consumos;
using WindowsFormsApplication1.Detalle_Consumos;
using WindowsFormsApplication1.Tarifa03;
using WindowsFormsApplication1.Tarifas;

namespace TesterProyecto
{
    public partial class FrmParseo : Form
    {

        #region Propiedades

        private ParseoTrama parseo;
        private AlgoritmoTarifa02 _t02;
        private AlgoritmoTarifa03 _t03;
        private AlgoritmoTarifaHM _tHm;
        private AlgoritmoTarifaOM _tOm;

        #endregion

        public FrmParseo()
        {
            InitializeComponent();
            
        }


        #region Eventos 
        
        private void btnParseo_Click(object sender, System.EventArgs e)
        {
            ValidaTipoTarifa();
        }

        private void btnVerConsumos_Click(object sender, EventArgs e)
        {
            VerConsumos();
        }

        private void btnPeriodoMesAnio_Click(object sender, EventArgs e)
        {
            PeriodoMesAnio();
        }

        private void btnDetConsDem_Click(object sender, EventArgs e)
        {
            VerConsumoDemanda();
        }

        private void btnFactorPotencia_Click(object sender, EventArgs e)
        {
            VerFactorPotencia();
        }

        #endregion


        #region Logica
        private void ValidaTipoTarifa()
        {
            if (cboTarifa.SelectedItem == "T02")
                Tarifa02();
            else if (cboTarifa.SelectedItem == "T03")
                Tarifa03();
            else if (cboTarifa.SelectedItem == "THM")
                TarifaHM();
            else if (cboTarifa.SelectedItem == "TOM")
                TarifaOM();
            else
                MessageBox.Show("Debe de seleccionar una tarifa valida.");
        }


        private void Tarifa02()
        {
            string trama = txtTrama.Text;


            //try
            //{
                parseo = new ParseoTrama(trama);
                _t02 = new AlgoritmoTarifa02(parseo.ComplexParseo);               

                CargaGrid();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

        }


        private void Tarifa03()
        {
            string trama = txtTrama.Text;


            try
            {
                parseo = new ParseoTrama(trama);
                _t03 = new AlgoritmoTarifa03(parseo.ComplexParseo);                

                CargaGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void TarifaHM()
        {
            string trama = txtTrama.Text;


            try
            {
                parseo = new ParseoTrama(trama);
                _tHm = new AlgoritmoTarifaHM(parseo.ComplexParseo);
                //parseo.ComplexParseo = _tHm.ParseoCompleto;

                CargaGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void TarifaOM()
        {
            string trama = txtTrama.Text;

            try
            {
                parseo = new ParseoTrama(trama);
                _tOm = new AlgoritmoTarifaOM(parseo.ComplexParseo);

                CargaGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CargaGrid()
        {
            //INFORMACION GENERAL
            dgtInfoGeneral.DataSource = parseo.ComplexParseo.InformacionGeneral.Conceptos;
            //HISTORIAL DE DETALLE DE CONSUMOS
            dtgHistDetalleConsumo.DataSource = parseo.ComplexParseo.HistorialDetconsumos;

            //INFORMACION DE CONSUMOS
            txtFecMinConsumo.Text = parseo.ComplexParseo.InfoConsumo.Detalle.FechaMin.Value.ToString("dd/MMMM/yyyy").ToUpper();
            txtFecMaxConsumo.Text = parseo.ComplexParseo.InfoConsumo.Detalle.FechaMax.Value.ToString("dd/MMMM/yyyy").ToUpper();
            txtPromConsumo.Text = parseo.ComplexParseo.InfoConsumo.Detalle.Promedio.ToString();
        
            

            //INFORMACION DE DEMANDAS
            txtFecMinDeman.Text = parseo.ComplexParseo.InfoDemanda.Detalle.FechaMin.Value.ToString("dd/MMMM/yyyy").ToUpper();
            txtFecMaxDemanda.Text = parseo.ComplexParseo.InfoDemanda.Detalle.FechaMax.Value.ToString("dd/MMMM/yyyy").ToUpper();
            txtDemanMax.Text = parseo.ComplexParseo.InfoDemanda.Detalle.DemandaMax.ToString();
          
        }

        private void VerConsumos()
        {
            var VerMensaje = false;

            if (parseo != null)
            {
                if (parseo.ComplexParseo != null)
                {
                    if (parseo.ComplexParseo.Consumo != null)
                    {
                        var frmConsumos = new FrmConsumos(parseo.ComplexParseo.Consumo);
                        frmConsumos.ShowDialog();
                    }
                    else
                        VerMensaje = true;
                }
                else                
                    VerMensaje = true;                

            }
            else            
                VerMensaje = true;            


            if (VerMensaje)
                MessageBox.Show("No hay información para mostrar.");

        }

        private void PeriodoMesAnio()
        {
            var verMensaje = false;

            if (parseo != null)
                if (parseo.ComplexParseo != null)
                    if (parseo.ComplexParseo.DetalleConsumos != null)
                    {
                        var frmPeriodoMA = new FrmPeriodoMesAnio(parseo.ComplexParseo.DetalleConsumos);
                        frmPeriodoMA.ShowDialog();
                    }
                    else
                        verMensaje = true;
                else
                    verMensaje = true;
            else
                verMensaje = true;

            if (verMensaje)
                MessageBox.Show("No hay información para mostrar.");

        }

        private void VerConsumoDemanda()
        {
            var verMensaje = false;

            if (parseo != null)
                if (parseo.ComplexParseo != null)
                    if (parseo.ComplexParseo.DetalleConsumos != null)
                    {
                        var frmConsumoDemanda = new FrmConsumoDemanda(parseo.ComplexParseo.DetalleConsumos);
                        frmConsumoDemanda.ShowDialog();
                    }
                    else
                        verMensaje = true;
                else
                    verMensaje = true;
            else
                verMensaje = true;

            if (verMensaje)
                MessageBox.Show("No hay información para mostrar.");
        }

        private void VerFactorPotencia()
        {
            var verMensaje = false;

            if (parseo != null)
                if (parseo.ComplexParseo != null)
                    if (parseo.ComplexParseo.DetalleConsumos != null)
                    {
                        var frmFactorPotencia = new FrmFactorPotencia(parseo.ComplexParseo.DetalleConsumos);
                        frmFactorPotencia.ShowDialog();
                    }
                    else
                        verMensaje = true;
                else
                    verMensaje = true;
            else
                verMensaje = true;

            if (verMensaje)
                MessageBox.Show("No hay información para mostrar.");
        }

        private void VerTarifas()
        {
            if (cboTarifa.SelectedItem == "T02")
            {
                if (_t02.T02 != null)
                {
                    var frmT02 = new FrmTarifa02(_t02.T02.FactSinAhorro);
                    frmT02.ShowDialog();
                }
            }
            else if (cboTarifa.SelectedItem == "T03")
            {
                if (_t03.T03 != null)
                {                   
                    var frmT03 = new FrmTarifa03(_t03.T03.FactSinAhorro);
                    frmT03.ShowDialog();
                }
            }
            else if (cboTarifa.SelectedItem == "THM")
            {
                if (_tHm.Thm != null)
                {
                    List<CompFacturacion> facturaciones = new List<CompFacturacion>();
                    facturaciones.Add(_tHm.Thm.FactSinAhorro);
                    var frmThm = new FrmTarifaHM(facturaciones);
                    frmThm.ShowDialog();
                }
                else if (_tHm.PeriodosFacturados != null)
                {
                    var frmThm = new FrmTarifaHM(_tHm.PeriodosFacturados);
                    frmThm.ShowDialog();
                }
            }
            else if (cboTarifa.SelectedItem == "TOM")
            {
                if (_tOm.Tom != null)
                {                    
                }
            }
        }


        #endregion                

        private void btnTarifa_Click(object sender, EventArgs e)
        {
            VerTarifas();
        }

        
    }
}
