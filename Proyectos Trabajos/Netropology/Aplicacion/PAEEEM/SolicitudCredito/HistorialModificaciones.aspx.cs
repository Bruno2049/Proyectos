using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.Entidades.Alta_Solicitud;
using PAEEEM.LogicaNegocios.LOG;

namespace PAEEEM.SolicitudCredito
{
    public partial class HistorialModificaciones : System.Web.UI.Page
    {
        public string NumeroCredito
        {
            get { return ViewState["NumeroCredito"] as string; }
            set { ViewState["NumeroCredito"] = value; }
        }

        private List<HistoricoCredito> LstHistoricoCredito
        {
            get
            {
                return ViewState["LstHistoricoCredito"] == null
                           ? new List<HistoricoCredito>()
                           : ViewState["LstHistoricoCredito"] as List<HistoricoCredito>;
            }
            set { ViewState["LstHistoricoCredito"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Token"] != null && Request.QueryString["Token"] != "")
                {
                    var NumeroCredito = Request.QueryString["Token"];
                    this.NumeroCredito = NumeroCredito;
                    CargaHistoricoCredito(NumeroCredito);
                    LstHistoricoCredito = new List<HistoricoCredito>();
                }
            }
        }

        protected void CargaHistoricoCredito(string idCredito)
        {
            LstHistoricoCredito = new ConsultaHistoricoBL().ObtenHistoricoCredito(idCredito);

            if (LstHistoricoCredito.Count > 0)
            {
                grdMovsSolicitud.DataSource = LstHistoricoCredito;
                grdMovsSolicitud.DataBind();
                AspNetPager.RecordCount = LstHistoricoCredito.Count;
                // ADD ////
                lblSinRegistros.Visible = false;
            }
            else
            {
                lblSinRegistros.Visible = true;
            }
        }

        protected void BtnSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("DatosCredito.aspx");
        }

        protected void AspNetPager_PageChanged(object sender, EventArgs e)
        {
            var paginaActual = AspNetPager.CurrentPageIndex;
            var tamanioPagina = AspNetPager.PageSize;

            grdMovsSolicitud.DataSource = LstHistoricoCredito.FindAll(me =>
                                                                      me.IdSecuencia >=
                                                                      (((paginaActual - 1)*tamanioPagina) + 1) &&
                                                                      me.IdSecuencia <= (paginaActual * tamanioPagina));
            grdMovsSolicitud.DataBind();
        }

        //ADD
        protected void btnRegresar_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("DetalleCredito.aspx?creditno="+NumeroCredito);
        }
    }
}