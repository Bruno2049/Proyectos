using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.AccesoDatos.MRV;
using PAEEEM.Entidades;
using PAEEEM.Entidades.MRV;

namespace PAEEEM.LogicaNegocios.MRV
{
    public class CuestionarioLN
    {
        public CuestionarioLN()
        { }

        #region metodos de consulta

        public List<Cuestionario> ObtenListaCuestionariosCredito(string idCredito)
        {
            var lstCuestionario = new CuestionarioDal().ObtenListaCuestionariosCredito(idCredito);

            foreach (var cuestionario in lstCuestionario)
            {
                cuestionario.DescripcionCuestionario = "Cuestionario de Seguimiento fecha " +
                                                       cuestionario.FechaCuestionario.ToString("dd/MM/yyyy");
            }

            return lstCuestionario;
        }

        public List<EquipoAltaEficienciaMrv> ObtenEquiposAlta(string idCredito, int idCuestionario)
        {
            var lstEquiposCuestionario = new CuestionarioDal().ObtenEquiposAltaCuestionario(idCredito, idCuestionario);

            if (lstEquiposCuestionario.Count > 0)
                return lstEquiposCuestionario;
            else
            {
                var resultado = new CuestionarioDal().ObtenEquiposAlta(idCredito, idCuestionario);

                return resultado;
            }           
        }

        public List<Medicion> ObtenCataglogoMediciones(string idCredito)
        {
            var resultado = new CuestionarioDal().ObtenCataglogoMediciones(idCredito);

            foreach (var medicion in resultado)
            {
                medicion.DescripcionMedicion = "Medición fecha " + medicion.FechaMedicion.ToString("dd/MM/yyyy");
            }

            return resultado;
        }

        public List<MRV_EQUIPOS_CUESTIONARIO> ObtenEquiposCuestionario(int idCuestionario)
        {
            var resultado = new CuestionarioDal().ObtenEquiposCuestionario(idCuestionario);

            return resultado;
        }

        public MRV_CUESTIONARIO ObtenCuestionario(int idCuestionario)
        {
            var resultado = new CuestionarioDal().ObtenCuestionario(idCuestionario);

            return resultado;
        }

        public List<MRV_CUESTIONARIO> ObtenListaCuestionarios(string idCredito, bool estatus)
        {
            var resultado = new CuestionarioDal().ObtenListaCuestionarios(idCredito, estatus);

            return resultado;
        }

        public List<MRV_HORARIOS_OPERACION> ObtenHorariosOperacionEquipo(int idCuestionario, int idEquipoCuestionario)
        {
            var resultado = new CuestionarioDal().ObtenHorariosOperacionEquipo(idCuestionario, idEquipoCuestionario);

            return resultado;
        }

        public List<MRV_HORARIOS_OPERACION> ObtenHorariosNegocio(int idCuestionario, int idTipoHorario)
        {
            var resultado = new CuestionarioDal().ObtenHorariosNegocio(idCuestionario, idTipoHorario);

            return resultado;
        }

        public MRV_HORAS_OPERACION_TOTAL ObtenTotalHorasEquipo(int idCuestionario, int idEquipo)
        {
            var resultado = new CuestionarioDal().ObtenTotalHorasEquipo(idCuestionario, idEquipo);

            return resultado;
        }

        public MRV_HORAS_OPERACION_TOTAL ObtenTotalHorasNegocio(int idCuestionario, int idTipoHorario)
        {
            var resultado = new CuestionarioDal().ObtenTotalHorasNegocio(idCuestionario, idTipoHorario);

            return resultado;
        }

        public List<MRV_HORAS_OPERACION_TOTAL> ObtenOperacionTotal(int idCuestionario)
        {
            var resultado = new CuestionarioDal().ObtenOperacionTotal(idCuestionario);

            return resultado;
        }

        public MRV_FOTOS_CUESTIONARIO ObtenFotoCuestionario(int idCuestionario)
        {
            var resultado = new CuestionarioDal().ObtenFotoCuestionario(idCuestionario);

            return resultado;
        }

        #endregion

        #region Metodos de Guardado

        public bool GuardaFotoCuestionario(MRV_FOTOS_CUESTIONARIO fotoCuestionario)
        {
            var resultado = new CuestionarioDal().GuardaFotoCuestionario(fotoCuestionario);

            return resultado;
        }

        public MRV_CUESTIONARIO GuardaCuestionario(MRV_CUESTIONARIO cuestionario)
        {
            var resultado = new CuestionarioDal().GuardaCuestionario(cuestionario);

            return resultado;
        }

        public void GuardaEquiposCuestionario(List<EquipoAltaEficienciaMrv> lstEquipos, int idCuestionario, string usuario)
        {
            foreach (var equipoAltaEficienciaMrv in lstEquipos)
            {
                var mrvEquipo = new MRV_EQUIPOS_CUESTIONARIO();
                mrvEquipo.IdCuestionario = idCuestionario;
                mrvEquipo.ID_CREDITO_PRODUCTO = equipoAltaEficienciaMrv.IdCreditoProducto;
                mrvEquipo.DescripcionEquipo = equipoAltaEficienciaMrv.NombreEquipo;
                mrvEquipo.EnOperacion = equipoAltaEficienciaMrv.EnOperacion;
                mrvEquipo.Estatus = true;
                mrvEquipo.FechaAdicion = DateTime.Now;
                mrvEquipo.AdicionadoPor = usuario;

                new CuestionarioDal().GuardaEquiposCuestionario(mrvEquipo);
            }
        }

        public void GuardaHorariosEquipo(List<MRV_HORARIOS_OPERACION> lstHorarios, int idEquipoCuestionario, int idCuestionario)
        {
            new CuestionarioDal().EliminaHorariosEquipo(idEquipoCuestionario);
            new CuestionarioDal().EliminaOperacionTotal(idCuestionario, 2, idEquipoCuestionario);

            foreach (var mrvHorariosOperacion in lstHorarios)
            {
                new CuestionarioDal().GuardaHorarioDiaEquipo(mrvHorariosOperacion);
            }
        }

        public void GuardaHorariosNegocio(List<MRV_HORARIOS_OPERACION> lstHorarios, int idCuestionario, int idTipoHorario)
        {
            new CuestionarioDal().EliminaHorariosNegocio(idCuestionario, idTipoHorario);
            new CuestionarioDal().EliminaOperacionTotalNegocio(idCuestionario, idTipoHorario);
            
            foreach (var mrvHorariosOperacion in lstHorarios)
            {
                new CuestionarioDal().GuardaHorarioDiaEquipo(mrvHorariosOperacion);
            }
        }

        public void GuardaOperacionTotalEquipo(MRV_HORAS_OPERACION_TOTAL horasOperacionTotal)
        {
            new CuestionarioDal().GuardaOperacionTotalEquipo(horasOperacionTotal);
        }

        #endregion

        #region Metodos de Actualizacion

        public bool ActualizaCuestionario(MRV_CUESTIONARIO cuestionario)
        {
            return new CuestionarioDal().ActualizaCuestionario(cuestionario);
        }

        #endregion
    }
}
