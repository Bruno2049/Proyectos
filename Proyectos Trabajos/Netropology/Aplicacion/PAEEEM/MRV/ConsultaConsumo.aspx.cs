using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.Captcha;
using PAEEEM.Entidades;
using PAEEEM.Entities;
using PAEEEM.LogicaNegocios.MRV;
using PAEEEM.LogicaNegocios.Credito;
using Telerik.Web.UI;

namespace PAEEEM.MRV
{
    public partial class ConsultaConsumo : System.Web.UI.Page
    {
        protected string NumeroCredito
        {
            get { return ViewState["NumeroCredito"] as string; }
            set { ViewState["NumeroCredito"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Token"] != null && Request.QueryString["Token"] != "")
                {
                    var idCredito =
                        System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["Token"]));
                    NumeroCredito = idCredito;

                    wucEncabezadoMRV1.Inicializa(NumeroCredito);
                    ConsumoMesPrimeraVez();
                    //ConsultaConsumoMes();
                }
            }
        }

        protected void rgConsumos_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            var lstConsumos = new ConsumosLN().ObtenListaConsumos(NumeroCredito);
            rgConsumos.DataSource = lstConsumos.OrderByDescending(me => me.IdConsultaConsumo);
        }

        protected void ConsumoMesPrimeraVez()
        {
            try
            {
                var lstConsumos = new ConsumosLN().ObtenListaConsumos(NumeroCredito);

                if (lstConsumos.Count > 0)
                {
                    ConsultaConsumoMes();
                }
                else
                {
                    var credito = blCredito.Obtener(NumeroCredito);
                    var usuario = ((US_USUARIOModel)Session["UserInfo"]).Nombre_Usuario;

                    var consultaConsumo = new MRV_CONSULTA_CONSUMOS
                    {
                        No_Credito = NumeroCredito,
                        FechaConsumo = credito.Fecha_Pendiente,
                        Estatus = true,
                        FechaAdicion = DateTime.Now.Date,
                        AdicionadoPor = usuario
                    };

                    var newConsultaConsumo = new ConsumosLN().GuardaConsumo(consultaConsumo);

                    if (newConsultaConsumo != null)
                    {
                        new ConsumosLN().CopiaHistoricosCredito(NumeroCredito, newConsultaConsumo.IdConsultaConsumo, usuario);
                    }
                }
            }
            catch (Exception)
            {
                
            }            
        }

        protected void ConsultaConsumoMes()
        {
            if (ValidaMes())
            {
                var credito = blCredito.Obtener(NumeroCredito);

                if (credito != null)
                {
                    var error = "";
                    var estado = "";
                    var validator = new CodeValidator();

                    if (validator.ValidateServiceCodeEdit(credito.RPU, out error, ref estado))
                    {
                        var parseo = validator.ParseoTrama;

                        var usuario = ((US_USUARIOModel)Session["UserInfo"]).Nombre_Usuario;

                        var consultaConsumo = new MRV_CONSULTA_CONSUMOS
                            {
                                No_Credito = NumeroCredito,
                                FechaConsumo = DateTime.Now.Date,
                                Estatus = true,
                                FechaAdicion = DateTime.Now.Date,
                                AdicionadoPor = usuario
                            };

                        var newConsultaConsumo = new ConsumosLN().GuardaConsumo(consultaConsumo);

                        if (newConsultaConsumo != null)
                        {
                            new ConsumosLN().GuardaHistoricoConsumos(parseo.ComplexParseo.HistorialDetconsumos,
                                                                        newConsultaConsumo.IdConsultaConsumo, usuario);
                        }
                    }
                }
            }
        }

        protected bool ValidaMes()
        {
            var lstConsumos = new ConsumosLN().ObtenListaConsumos(NumeroCredito);

            if (lstConsumos.Count > 0)
            {
                var ultimaFecha = lstConsumos.LastOrDefault().FechaConsumo.Value.Date;
                var fechaAcual = DateTime.Now.Date;

                if (ultimaFecha.Year == fechaAcual.Year && ultimaFecha.Month == fechaAcual.Month)
                {
                    return false;
                }
            }

            return true;
        }

        protected void rgConsumos_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "ExportToExcel")
            {
                rgConsumos.ExportSettings.ExportOnlyData = false;
                rgConsumos.ExportSettings.IgnorePaging = true;
                rgConsumos.ExportSettings.OpenInNewWindow = true;
                rgConsumos.ExportSettings.FileName = "Consulta_Consumos_" + NumeroCredito;
                rgConsumos.MasterTableView.ExportToCSV();
            }

            if (e.CommandName == "ExportToPdf")
            {
                rgConsumos.ExportSettings.ExportOnlyData = false;
                rgConsumos.ExportSettings.IgnorePaging = true;
                rgConsumos.ExportSettings.OpenInNewWindow = true;
                rgConsumos.ExportSettings.FileName = "Consulta_Consumos_" + NumeroCredito;
                rgConsumos.MasterTableView.ExportToPdf();
            }

            if (e.CommandName == "View")
            {
                var item = (GridDataItem)e.Item;
                var id = int.Parse(item.GetDataKeyValue("IdConsultaConsumo").ToString());

                var lstHistConsumo = new ConsumosLN().ObtenHistoricoConsumos(id);
                rgHistoricoConsumos.DataSource = lstHistConsumo;
                rgHistoricoConsumos.DataBind();
            }
        }

        protected void RadBTnSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdmonMRV.aspx?Token=" +
                                          Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(NumeroCredito)));
        }

        protected void rgHistoricoConsumos_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var lstHistConsumo = new List<MRV_HIST_CONSULTA_CONSUMOS>();
            rgConsumos.DataSource = lstHistConsumo;
        }

        protected void rgHistoricoConsumos_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "ExportToExcel")
            {
                rgHistoricoConsumos.ExportSettings.ExportOnlyData = false;
                rgHistoricoConsumos.ExportSettings.IgnorePaging = true;
                rgHistoricoConsumos.ExportSettings.OpenInNewWindow = true;
                rgHistoricoConsumos.ExportSettings.FileName = "Consulta_Hist_Consumos_" + NumeroCredito;
                rgHistoricoConsumos.MasterTableView.ExportToCSV();
            }

            if (e.CommandName == "ExportToPdf")
            {
                rgHistoricoConsumos.ExportSettings.ExportOnlyData = false;
                rgHistoricoConsumos.ExportSettings.IgnorePaging = true;
                rgHistoricoConsumos.ExportSettings.OpenInNewWindow = true;
                rgHistoricoConsumos.ExportSettings.FileName = "Consulta_Hist_Consumos_" + NumeroCredito;
                rgHistoricoConsumos.MasterTableView.ExportToPdf();
            }
        }
    }
}