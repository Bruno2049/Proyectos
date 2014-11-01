using System;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.AccesoDatos.Catalogos;
using PAEEEM.LogicaNegocios.AltaBajaEquipos;
using PAEEEM.DataAccessLayer;
using PAEEEM.LogicaNegocios.Tarifas;

namespace PAEEEM.SolicitudCredito
{
    public partial class Equipos : System.Web.UI.Page
    {
        public string NoCredito
        {
            get { return ViewState["No_Credito"] as string; }
            set { ViewState["No_Credito"] = value; }
        }
        private string _rpu;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserInfo"] == null)
            {
                Response.Redirect("../Login/Login.aspx");
                return;
            }

            Page.Form.Attributes.Add("enctype", "multipart/form-data");

            if (IsPostBack) return;

            if (Request.QueryString["Token"] != null && Request.QueryString["Token"] != "")
            {
                NoCredito = Request.QueryString["Token"];
                _rpu = Request.QueryString["Token"];
            }
            LlenaEquiposAlta();
            LlenaEquiposBaja();
            LlenaResumePresupuesto();
            LlenaHistorico();
            LLenaBalanceMen();
            HPL_RegInfol.NavigateUrl = "../SolicitudCredito/DetalleCredito.aspx?creditno=" + NoCredito;
            HPL_HisMod.NavigateUrl = "../SolicitudCredito/HistorialModificaciones.aspx?Token=" + NoCredito;
        }


        protected void verFotoEqAltaEfc_Click(object sender, ImageClickEventArgs e)
        {
            var gridViewRow = (GridViewRow)((ImageButton)sender).NamingContainer;
            var dataKey = DGV_InfEquAltaEfic.DataKeys[gridViewRow.RowIndex].Values;
            var idTipoFoto = 0;
            var idConsecutivo = 0;
            var idCreditoProducto = 0;

            if (dataKey != null)
            {
                idCreditoProducto = dataKey[1] != null ? Convert.ToInt32(dataKey[1].ToString()) : 0;
                idTipoFoto = Convert.ToInt32(dataKey[2].ToString());
                idConsecutivo = Convert.ToInt32(dataKey[3].ToString());
            }

            if (idCreditoProducto != 0)
            {
                var url =
                    string.Format(
                        "window.open('../SupplierModule/VisorImagenes.aspx?CreditNumber={0}&IdCreditoSustitucion={1}&idTipoFoto={2}&IdConsecutivo={3}','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');",
                        NoCredito, idCreditoProducto, idTipoFoto, idConsecutivo);

                ScriptManager.RegisterStartupScript(this, GetType(), "PrintForm", url, true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Foto",
                        string.Format("alert('{0}');", "No hay Foto para este producto..."), true);
            }
        }

        protected void verFotoEqBajaEfc_Click(object sender, ImageClickEventArgs e)
        {
            var gridViewRow = (GridViewRow)((ImageButton)sender).NamingContainer;
            var dataKey = DGV_InfEquBajaEfic.DataKeys[gridViewRow.RowIndex].Values;
            var idTipoFoto = 0;
            var idConsecutivo = 0;
            var idCreditoSustitucion = 0;

            if (dataKey != null)
            {
                idCreditoSustitucion = dataKey[1] != null ? Convert.ToInt32(dataKey[1].ToString()) : 0;
                idTipoFoto = Convert.ToInt32(dataKey[2].ToString()); //2
                idConsecutivo = Convert.ToInt32(dataKey[3].ToString());
            }

           if(idCreditoSustitucion != 0)
           {

            var url = string.Format("window.open('../SupplierModule/VisorImagenes.aspx?CreditNumber={0}&IdCreditoSustitucion={1}&idTipoFoto={2}&IdConsecutivo={3}','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');",
                                                                        NoCredito, idCreditoSustitucion, idTipoFoto, idConsecutivo);

            ScriptManager.RegisterStartupScript(this, GetType(), "PrintForm", url, true);
           }
           else
           {
               ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Foto",
                       string.Format("alert('{0}');", "No hay Foto para este producto..."), true);
           }
        }

        protected void verFotoFachada_Click(object sender, ImageClickEventArgs e)
        {
            var fachada = DetalleResumenEquipos.ClassInstance.ObtenFotoFachada(NoCredito);
            if(fachada != null)
            {
            var url = string.Format("window.open('../SupplierModule/VisorImagenes.aspx?CreditNumber={0}&IdCreditoSustitucion={1}&idTipoFoto={2}&IdConsecutivo={3}','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');",
                                                                        fachada.No_Credito, fachada.idCreditoSustitucion, fachada.idtipoFoto, fachada.idConsecutivoFoto);

            ScriptManager.RegisterStartupScript(this, GetType(), "PrintForm", url, true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Foto",
                        string.Format("alert('{0}');", "No hay Foto para este producto..."), true);
            }
        }


        protected void DGV_InfEquBajaEfic_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DGV_InfEquAltaEfic_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void LlenaEquiposBaja()
        {
            var equiposBaja = DetalleResumenEquipos.ClassInstance.ObtenEquiposBajaEficienciaCredito(NoCredito);
            DGV_InfEquBajaEfic.DataSource = equiposBaja;
            DGV_InfEquBajaEfic.DataBind();
        }

        private void LlenaEquiposAlta()
        {
            var equiposAlta = DetalleResumenEquipos.ClassInstance.ObtenEquiposAltaEficienciaCredito(NoCredito);
            DGV_InfEquAltaEfic.DataSource = equiposAlta;
            DGV_InfEquAltaEfic.DataBind();
        }

        private void LlenaResumePresupuesto()
        {
            var iva = Convert.ToDecimal(
                    new ParametrosGlobales().ObtienePorCondicion(p => p.IDPARAMETRO == 3 && p.IDSECCION == 1).VALOR);
           
            var lstBajaEficiencia = new OpEquiposAbEficiencia().ObtenEquiposBajaEficienciaCredito(NoCredito);
            var lstAltaEficiencia = new OpEquiposAbEficiencia().ObtenEquiposAltaEficienciaCredito(NoCredito);
            var presupuesto = new ValidacionTarifas().ObtenResumenPresupuestoConsulta(lstAltaEficiencia, lstBajaEficiencia,
                                                                          iva, NoCredito);

            if (presupuesto == null)
            {
                var A = K_CREDITO_PRODUCTODal.ClassInstance.get_K_Credit_ProductByCreditNo(NoCredito);
            }
            if (presupuesto != null)
            {

                LBL_CostoEquipo.Text = presupuesto.First(p => p.IdPresupuesto == 3).Resultado.ToString("C2");
                LBL_GastosInstalacion.Text = (presupuesto.First(p => p.IdPresupuesto == 7).Resultado).ToString("C2");
                LBL_IVA.Text = (presupuesto.First(p => p.IdPresupuesto == 2).Resultado).ToString("C2");
                LBL_SubTotal.Text = (presupuesto.First(p => p.IdPresupuesto == 1).Resultado).ToString("C2");
                LBL_Incentivo.Text = (presupuesto.First(p => p.IdPresupuesto == 5).Resultado).ToString("C2");
                LBL_CostAcopDes.Text = presupuesto.First(p => p.IdPresupuesto == 4).Resultado.ToString("C2");
                LBL_Descuento.Text = (presupuesto.First(p => p.IdPresupuesto == 6).Resultado).ToString("C2");
                LBL_TOTAL.Text = (presupuesto.First(p => p.IdPresupuesto == 1).Resultado - presupuesto.First(p => p.IdPresupuesto == 6).Resultado).ToString("C2");

            }
        }

        private void LlenaHistorico()
        {
            var historico = DetalleResumenEquipos.ClassInstance.ObtenHistoricoResumen(NoCredito);
            if (historico == null) return;
            //LBL_ConsumoPromedio.Text = historico.Consumo.ToString(CultureInfo.InvariantCulture);
            //LBL_DemandaPromedio.Text = historico.Demanda.ToString(CultureInfo.InvariantCulture);
            //LBL_FacPotProm.Text = historico.FactorPotencia.ToString(CultureInfo.InvariantCulture);
            LBL_ConsumoPromedio.Text = historico.Consumo.ToString(CultureInfo.InvariantCulture);
            LBL_DemandaPromedio.Text = historico.Demanda.ToString(CultureInfo.InvariantCulture);
            LBL_FacPotProm.Text = historico.FactorPotencia.ToString(CultureInfo.InvariantCulture);
        }

        private void LLenaBalanceMen()
        {
            var mensual = DetalleResumenEquipos.ClassInstance.ObtenDetalleBalance(NoCredito);
            if (mensual == null)
            {

            }
            else
            {
                decimal a = (decimal)mensual.GastosMensuales;
                LBL_GastosMen.Text = a.ToString("C2");
                LBL_VentasMen.Text = ((decimal)mensual.Ventas_mes).ToString("C2");
            }
        }
    }
}