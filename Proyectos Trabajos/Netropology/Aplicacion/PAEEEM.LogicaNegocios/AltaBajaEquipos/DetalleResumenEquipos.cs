using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.AccesoDatos;
using PAEEEM.Entidades.AltaBajaEquipos;
using PAEEEM.AccesoDatos.AltaBajaEquipos;
using PAEEEM.LogicaNegocios.AltaBajaEquipos;
using PAEEEM.Entidades.Alta_Equipos;
using PAEEEM.Entidades.ModuloCentral;
using PAEEEM.Entidades;

namespace PAEEEM.LogicaNegocios.AltaBajaEquipos
{
    public class DetalleResumenEquipos
    {
        private static readonly DetalleResumenEquipos _classInstance = new DetalleResumenEquipos();

        public static DetalleResumenEquipos ClassInstance
        {
            get { return _classInstance; }
        }

        public DetalleResumenEquipos()
        {  
        }

        public List<DetalleEquiposBajaEfic> ObtenEquiposBajaEficienciaCredito(string NO_Credito)
        {
            return DetalleEquiposAltaBajaEfic.ClassInstance.ObtenEquiposBajaEficienciaCredito(NO_Credito);
        }

        public List<DetalleEquipoAltaEfic> ObtenEquiposAltaEficienciaCredito(string No_Credito)
        {
            return DetalleEquiposAltaBajaEfic.ClassInstance.ObtenEquiposAltaEficienciaCredito(No_Credito);
        }

        public DetallePresupuesto ObtenResumenPresupuesto(string No_Credito)
        {
            decimal iva = 0.16M;
            List<EquipoBajaEficiencia> LstBajaEficiencia = new List<EquipoBajaEficiencia>();
            LstBajaEficiencia = new OpEquiposAbEficiencia().ObtenEquiposBajaEficienciaCredito(No_Credito);
            List<DetalleEquipoAltaEficiencia> LstAltaEficiencia = new List<DetalleEquipoAltaEficiencia>();
            LstAltaEficiencia = DetalleEquiposAltaBajaEfic.ClassInstance.ObtenEquiposAltaEficienciaCreditoDetalle(No_Credito);
            DetallePresupuesto Detalle = DetalleEquiposAltaBajaEfic.ClassInstance.ObtenResumenPres(LstAltaEficiencia, LstBajaEficiencia, iva, No_Credito);

            return Detalle;
        }

        public CRE_HISTORICO_CONSUMO ObtenHistoricoResumen(string No_Credito)
        {
            return DetalleEquiposAltaBajaEfic.ClassInstance.ObtenHistoricoResumen(No_Credito);
        }

        public DetalleBalenceMensual ObtenDetalleBalance(string No_Credito)
        {
            return DetalleEquiposAltaBajaEfic.ClassInstance.ObtenDetalleBalance(No_Credito);
        }

        public Fotos ObtenFotoFachada(string No_Credito)
        {
            return DetalleEquiposAltaBajaEfic.ClassInstance.ObtenFotoFachada(No_Credito);
        }
        public List<HistoricoConsultas> ObtenHistorico(string RPU)
        {
            return DetalleEquiposAltaBajaEfic.ClassInstance.ObtenHistorico(RPU);
        }
    }
}
