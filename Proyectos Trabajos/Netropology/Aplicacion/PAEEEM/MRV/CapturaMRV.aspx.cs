using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PAEEEM.MRV
{
    public partial class CapturaMRV : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Token"] != null && Request.QueryString["Token"] != "")
                {
                    var idCredito =
                        System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["Token"]));
                    var idMedicion = Convert.ToInt32(Request.QueryString["Med"]);
                    var acccion = Convert.ToInt32(Request.QueryString["Acc"]);

                    CargaDatos(idCredito, idMedicion, acccion);
                    wucEncabezadoMRV1.Inicializa(idCredito);
                }
            }
        }

        protected void CargaDatos(string idCredito, int idMedicion, int accion)
        {
            switch (accion)
            {
                case 1:
                    wucMediciones1.Inicializa(idCredito, idMedicion);
                    break;
                case 2:
                    wucMediciones1.InicializaConsultaEdit(idCredito, idMedicion, true);
                    break;
                case 3:
                    wucMediciones1.InicializaConsultaEdit(idCredito, idMedicion, false);
                    break;
                default:
                    break;
            }
        }
    }
}