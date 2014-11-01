using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.Entidades;
using PAEEEM.LogicaNegocios.MRV;

namespace PAEEEM.MRV
{
    public partial class wucEncabezadoMRV : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Inicializa(string idNumeroCredito)
        {
            LLenaDatosCliente(idNumeroCredito);
        }

        protected void LLenaDatosCliente(string idNumeroCredito)
        {
            var datosCredito = new MedicionesLN().ObtenDatosCredito(idNumeroCredito);

            if (datosCredito != null)
            {
                RadTxtNumSolicitud.Text = idNumeroCredito;
                var nombreCliente = datosCredito.CveTipoSociedad == 1
                                        ? (datosCredito.Nombre + " " + datosCredito.ApPaterno + " " +
                                           datosCredito.ApMaterno)
                                        : datosCredito.RazonSocial;
                RadTxtNombreCliente.Text = nombreCliente;
                RadTxtEstado.Text = datosCredito.Estado;
                RadTxtDelegMunicipio.Text = datosCredito.DelegMunicipio;
            }
        }
    }
}