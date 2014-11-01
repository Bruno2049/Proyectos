using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.Entidades.CirculoCredito;
using PAEEEM.LogicaNegocios.CirculoCredito;
namespace PAEEEM.CirculoCredito
{
    public partial class wucConsultaPaquetes : System.Web.UI.UserControl
    {

        private List<ConsultaPaquetes> CosultarPak
        {
            get
            {
                return Session["CosultarPak"] == null
                           ? new List<ConsultaPaquetes>()
                           : Session["CosultarPak"] as List<ConsultaPaquetes>;
            }
            set { Session["CosultarPak"] = value; }
        }


        protected void LLenarpaquete()
        {
            //CosultarPak = ConsultPaquete.consultar(
            //rgPaquetePendiente.DataSource = LstDatos;

            //ValidacionesRadGrid();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void rgConsultaPaquete_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {

        }

        protected void rgConsultaPaquete_DataBound(object sender, EventArgs e)
        {

        }
    }
}