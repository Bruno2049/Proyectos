using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PAEEEM.MRV
{
    public partial class CuestionarioMRV : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Token"] != null && Request.QueryString["Token"] != "")
                {
                    var idCredito =
                        System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["Token"]));
                    var idCuestionario = Convert.ToInt32(Request.QueryString["Que"]);
                    var acccion = Convert.ToInt32(Request.QueryString["Acc"]);

                    CargaDatos(idCredito, idCuestionario, acccion);
                    wucEncabezadoMRV1.Inicializa(idCredito);
                }
            }
        }

        protected void CargaDatos(string idCredito, int idCuestionario, int accion)
        {
            switch (accion)
            {
                case 1:
                    wucCuestionario1.Inicializa(idCredito);
                    break;
                case 2:
                    wucCuestionario1.InicializaConsultaEdit(idCredito, idCuestionario, true);
                    break;
                case 3:
                    wucCuestionario1.InicializaConsultaEdit(idCredito, idCuestionario, false);
                    break;
            }
        }
    }
}