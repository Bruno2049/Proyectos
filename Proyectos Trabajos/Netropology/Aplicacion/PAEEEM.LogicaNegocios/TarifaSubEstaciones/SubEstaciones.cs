using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using PAEEEM.Entidades.SubEstaciones;
using PAEEEM.Entidades;
using PAEEEM.AccesoDatos;
using PAEEEM.AccesoDatos.TarifaSubEstaciones;


namespace PAEEEM.LogicaNegocios.TarifaSubEstaciones
{
    public class SubEstaciones
    {
        private static readonly SubEstaciones _classInstance = new SubEstaciones();

        public static SubEstaciones ClassInstance
        {
            get { return _classInstance; }
        }

        public SubEstaciones()
        {  
        }

        public CambioRPU IrPorDatos (string No_Credito)
        {
            return SubEstacionesDatos.ClassInstance.IrPorDatos(No_Credito);
        }

        public bool EsSubestaciones(string No_Credito)
        {
            return SubEstacionesDatos.ClassInstance.EsSubestacion(No_Credito);
        }

        public Cambio_RPU InsertaNuevoRPUDist(Cambio_RPU NuevoRPUDis)
        {
            return SubEstacionesDatos.ClassInstance.InsertaRPUDistr(NuevoRPUDis);
        }

        public bool HayRegistroDeNuevoRPUDist(string No_credito)
        {
            return SubEstacionesDatos.ClassInstance.HayRegistroDeNuevoRPUDist(No_credito);
        }
        
        public Correos IrPorCorreoZona (string usuarioDis , string tipoUsuario) 
        {
            return SubEstacionesDatos.ClassInstance.ObtenCorreoZona(usuarioDis, tipoUsuario);
        }

        public Cambio_RPU ActualizaRPUJefeZona(string No_Credito, string RPU_Jefe_zona, out bool actualizo, out bool elimino)
        {
           return SubEstacionesDatos.ClassInstance.ActualizaRPUJefeZona(No_Credito, RPU_Jefe_zona, out actualizo, out elimino);
        }

        public bool HayRegistroDeNuevoRPUJefeZona(string No_Credito)
        {
            return SubEstacionesDatos.ClassInstance.HayRegistroDeNuevoRPUJefeZona(No_Credito);
        }

        public Correos ObtenCorreoJefeZona(string usuarioJefeZona,string usuarioDist)
        {
            return SubEstacionesDatos.ClassInstance.ObtenCorreoJefeZona(usuarioJefeZona, usuarioDist);
        }

        public Cambio_RPU_Entidad ObtenDatosRpuDist(string noCredito)
        {
            return SubEstacionesDatos.ClassInstance.ObtenDatosRpuDist(noCredito);
        }

        public bool RPUActivo(string No_Credito)
        {
            return SubEstacionesDatos.ClassInstance.RPUActivo(No_Credito);
        }
    }
}
