using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.Entidades;
using PAEEEM.Entidades.Alta_Solicitud;

namespace PAEEEM.AccesoDatos.SolicitudCredito
{
    public class CambioTarifaDal
    {
        private readonly PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();

        public List<CreditoCambioTarifa> ObtenCreditosCambioTarifa()
        {
            var resultado = (from credito in _contexto.CRE_Credito
                             join cambio in _contexto.Cambio_RPU
                                 on credito.No_Credito equals cambio.No_Credito
                             where cambio.RPU_Distribuidor == cambio.RPU_Jefe_Zona
                                   && credito.RPU != cambio.RPU_Distribuidor
                             select new CreditoCambioTarifa
                                 {
                                     IdCredito = credito.No_Credito,
                                     RpuActual = credito.RPU,
                                     RpuNuevo = cambio.RPU_Distribuidor,
                                     UsuarioDistribuidor = cambio.Usuario_Distribuidor
                                 }).ToList();

            return resultado;
        }

        public bool InsertaSubestacion(SUBESTACIONES subestacion)
        {
            bool inserta;

            using (var r = new Repositorio<SUBESTACIONES>())
            {
                var sub = r.Agregar(subestacion);

                inserta = sub != null;
            }

            return inserta;
        }



        public bool ActualizaCambioRPU(string idCredito)
        {
            
            bool actualiza = false;

            using (var r = new Repositorio<Cambio_RPU>())
            {
                var cambioRpu = r.Extraer(me => me.No_Credito == idCredito);
                cambioRpu.Proceso_Iniciado = 2;

                if(cambioRpu != null)
                    actualiza = r.Actualizar(cambioRpu);
            }

            return actualiza;
        }
    }
}
