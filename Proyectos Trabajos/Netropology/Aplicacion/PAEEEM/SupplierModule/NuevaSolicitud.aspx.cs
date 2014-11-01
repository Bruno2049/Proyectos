using System;
using System.Web.UI;
using PAEEEM.Helpers;
using PAEEEM.LogicaNegocios.ModuloCentral;

namespace PAEEEM.SupplierModule
{
    public partial class NuevaSolicitud : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            if (null == Session["UserInfo"])
            {
                ValidaMontoMeta();
            }
        }

        private void ValidaMontoMeta()
        {
              
            //Updated by Edu
            var montoTotal = new DatosMontos().montoTotalPrograma();
            var montoTotalDisponible = new DatosMontos().montoDisponiblePrograma();
            var montoMinimoTotal = new DatosMontos().montoMinimoPrograma();

            var montoincentivo = new DatosMontos().montoTotalIncentivo();
            var montoMinimoIncentivo = new DatosMontos().montoMinimoIncentivo();
            var montoDisponibleIncentivo = new DatosMontos().montoDisponibleIncentivo();
            string tipoMonto;
            string cantidad;

            if (((montoDisponibleIncentivo * 100) / montoincentivo) > 90)
            {
                tipoMonto = "Monto del Incentivo Energético";
                cantidad = montoincentivo.ToString();
                try
                {
                    MailUtility.MetasMontoTotal("MontoMetas.html", "eduardohernandez.s159@gmail.com", DateTime.Now.ToLongDateString(), tipoMonto, cantidad);
                    //var correos = new DatosMontos().CorreoUsuario();

                    //foreach (var correo in correos)
                    //{
                    //    MailUtility.MetasMontoTotal("MontoMetas.html", correo.VALOR, DateTime.Now.ToLongDateString(),
                    //        tipoMonto, cantidad);
                    //}
                }
                catch (Exception w)
                {
                    var a = w.Message;
                }
            }

            if (((montoTotalDisponible * 100) / montoTotal) > 90)
            {
                tipoMonto = "Monto de Financiamiento";
                cantidad = montoTotal.ToString();
                try
                {
                    MailUtility.MetasMontoTotal("MontoMetas.html", "eduardohernandez.s159@gmail.com",
                        DateTime.Now.ToLongDateString(), tipoMonto, cantidad);
                    //var correos = new DatosMontos().CorreoUsuario();

                    //foreach (var correo in correos)
                    //{
                    //    MailUtility.MetasMontoTotal("MontoMetas.html", correo.VALOR, DateTime.Now.ToLongDateString(),
                    //        tipoMonto, cantidad);
                    //}
                }
                catch (Exception w)
                {
                    var a = w.Message;
                }
            }

            if (montoTotalDisponible > montoMinimoTotal)
            {
                if (montoDisponibleIncentivo > montoMinimoIncentivo)
                {
                    Response.Redirect("../Captcha/valida.aspx");
                }
                else
                {
                    RadWindowManager1.RadConfirm("Por el momento solo podrá registrar solicitudes de tecnologías consideradas como adquisición.", "confirmCallBackFn", 300, 100, null, "Nueva Solicitud");
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "AlertInform",
                        "alert('Por el momento no se puede registrar esta solicitud debido a que los recursos del programa se encuentran comprometidos');",
                        true);
            }
        }
    }
}