using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.Entidades;
using PAEEEM.Entidades.Alta_Solicitud;
using PAEEEM.LogicaNegocios;
using PAEEEM.LogicaNegocios.SolicitudCredito;
using PAEEEM.SupplierModule.Controls;

namespace PAEEEM.SupplierModule
{
    public partial class PruebaUc : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUserControl_Click(object sender, EventArgs e)
        {
            //wucValidaPyme1.Page.Validate();

            //if (wucValidaPyme1.Page.IsValid)
            //{
            //    var datosPyme = wucValidaPyme1.ObtenDatosPyme();
            //    var esPyme = CatalogosSolicitud.ValidaClasificacionPyme(datosPyme);

            //    if (esPyme)
            //        datosPyme.Cve_Es_Pyme = 1;

            //    var resultado = SolicitudCreditoAcciones.GuarDatosPyme(datosPyme);

            //    if (resultado != null)
            //    {
            //        ScriptManager.RegisterClientScriptBlock
            //            (form1, typeof (Page), "Actualizar", "alert('Se guardarón los datos con Exitó ')", true);
            //    }
            //}

        }

        protected void btnCP_Click(object sender, EventArgs e)
        {
        }

    }
}