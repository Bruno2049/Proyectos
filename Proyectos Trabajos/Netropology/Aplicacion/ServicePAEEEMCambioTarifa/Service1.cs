using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using PAEEEM.Captcha;
using PAEEEM.Entidades;
using PAEEEM.AccesoDatos.SolicitudCredito;
using PAEEEM.LogicaNegocios.Credito;

namespace ServicePAEEEMCambioTarifa
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
        }

        protected override void OnStop()
        {
        }

        private void ValidaRPU_Tick(object sender, EventArgs e)
        {

        }

        protected void ValidaCreditosCambioTarifa()
        {
            var lstCreCambioTarifa = new CambioTarifa().ObtenCreditosCambioTarifa();
            var validator = new CodeValidator();

            if (lstCreCambioTarifa != null && lstCreCambioTarifa.Count > 0)
            {
                foreach (var creditoCambioTarifa in lstCreCambioTarifa)
                {
                    var error = "";
                    var estado = "";

                    if (validator.ValidateServiceCode(creditoCambioTarifa.RpuNuevo, out error, ref estado))
                    {
                        var credito = blCredito.Obtener(creditoCambioTarifa.IdCredito);
                        credito.RPU = creditoCambioTarifa.RpuNuevo;
                        blCredito.Actualizar(credito);
                    }
                }
            }
        }
    }
}
