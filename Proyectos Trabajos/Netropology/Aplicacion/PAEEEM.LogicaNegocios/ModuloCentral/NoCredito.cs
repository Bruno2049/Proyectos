using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using PAEEEM.Entidades.ModuloCentral;
using PAEEEM.AccesoDatos.Catalogos;

namespace PAEEEM.LogicaNegocios.ModuloCentral
{
    public class NoCredito
    {
        public List<MotivosCredito> ConsultaValidacionRpu(string rpu)
        {
            var c = new MotivosNoCreditos().ConsultaValidacionRpu(rpu);
            return c;
        }

        public List<MotivosCredito> ConsultaPresupuestoInversion(string rpu)
        {
            var c = new MotivosNoCreditos().ConsultaPresupuestoInversion(rpu);
            return c;
        }

        public List<MotivosCredito> ConsultaCancelaciones(string rpu)
        {
            var c = new MotivosNoCreditos().ConsultaCancelaciones(rpu);
            return c;
        }

        public List<MotivosCredito> ConsultaCrediticiaMopNoValido(string rpu)
        {
            var c = new MotivosNoCreditos().ConsultaCrediticiaMopNoValido(rpu);
            return c;
        }

        public List<MotivosCredito> ExcedeConsultasCrediticias(string rpu)
        {
            var c = new MotivosNoCreditos().ExcedeConsultasCrediticias(rpu);
            return c;
        }

        public List<MotivosCredito> Consulta(string rpu)
        {
            var c = new MotivosNoCreditos().Consulta2(rpu);
            return c;
        }

        public List<EquiposAlta> EquiposAltas(string rpu, int secuencia, string noIntento)
        {
            var e = new MotivosNoCreditos().ObtenEquiposAltaEficienciaCredito(rpu,secuencia,noIntento);
            return e;
        }

        public List<EquiposBaja> EquiposBajas(string rpu,int secuencia, string noIntento)
        {
            var e = new MotivosNoCreditos().ObtenEquiposBajaEficienciaCredito(rpu,secuencia,noIntento);
            return e;
        }
 
    }
   
}
