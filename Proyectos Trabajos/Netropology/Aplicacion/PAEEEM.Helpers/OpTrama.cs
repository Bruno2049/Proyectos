using System;
using PAEEEM.Helpers.SICOM1;

namespace PAEEEM.Helpers
{
    public static class OpTrama
    {
        public static string GetTrama(string rpu, out string error)
        {
            // SICOM1.WSServicioSICOM wssicom = new SICOM1.WSServicioSICOM();
            var wssicom = new TraeRecibo();         
            string trama = "";
            

            try
            {
                wssicom.Timeout = 30000; // RSA timeout in milliseconds               
                trama = wssicom.ConsultarTrama(rpu);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                throw new Exception(ex.Message);
            }
            finally
            {
                wssicom.Abort();
                wssicom.Dispose();
            }

            error = null;
            return trama;
        }
    }
}
