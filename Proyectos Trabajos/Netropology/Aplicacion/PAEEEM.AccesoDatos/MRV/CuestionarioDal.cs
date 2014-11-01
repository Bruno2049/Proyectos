using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.Entidades;
using PAEEEM.Entidades.MRV;

namespace PAEEEM.AccesoDatos.MRV
{
    public class CuestionarioDal
    {
        private readonly PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();

        public CuestionarioDal()
        {}

        public bool GuardaFotoCuestionario(MRV_FOTOS_CUESTIONARIO fotoCuestionario)
        {
            using (var context = new PAEEEM_DESAEntidades())
            {
                if (context.MRV_FOTOS_CUESTIONARIO.Any(me => me.IdCuestionario == fotoCuestionario.IdCuestionario))
                {
                    var fotoExistente =
                        context.MRV_FOTOS_CUESTIONARIO.FirstOrDefault(
                            me => me.IdCuestionario == fotoCuestionario.IdCuestionario);
                    fotoExistente.Nombre = fotoCuestionario.Nombre;
                    fotoExistente.Extension = fotoCuestionario.Extension;
                    fotoExistente.Longitud = fotoCuestionario.Longitud;
                    fotoExistente.Foto = fotoCuestionario.Foto;
                    fotoExistente.FechaAdicion = fotoCuestionario.FechaAdicion;
                    fotoExistente.AdicionadoPor = fotoCuestionario.AdicionadoPor;
                    context.SaveChanges();
                }
                else
                {
                    context.MRV_FOTOS_CUESTIONARIO.Add(fotoCuestionario);
                    context.SaveChanges();
                }
            }

            return true;
        }

        #region Metodos de Consulta

        public List<EquipoAltaEficienciaMrv> ObtenEquiposAlta(string idCredito, int idCuestionario)
        {
            List<EquipoAltaEficienciaMrv> lstEquipos = null;

            var resultado = (from creditoProducto in _contexto.K_CREDITO_PRODUCTO
                             join producto in _contexto.CAT_PRODUCTO
                                 on creditoProducto.Cve_Producto
                                 equals producto.Cve_Producto
                             join tipoProducto in _contexto.CAT_TIPO_PRODUCTO
                                 on producto.Ft_Tipo_Producto
                                 equals tipoProducto.Ft_Tipo_Producto
                             join tecnologia in _contexto.CAT_TECNOLOGIA
                                 on tipoProducto.Cve_Tecnologia
                                 equals tecnologia.Cve_Tecnologia
                             where creditoProducto.No_Credito == idCredito
                             select new EquipoAltaEficienciaMrv
                                 {
                                     IdEquipo = creditoProducto.ID_CREDITO_PRODUCTO,
                                     IdCreditoProducto = creditoProducto.ID_CREDITO_PRODUCTO,
                                     IdTecnologia = tecnologia.Cve_Tecnologia,
                                     NombreEquipo =
                                         tecnologia.Dx_Nombre_General + " - " + tipoProducto.Dx_Tipo_Producto + " - " +
                                         producto.Dx_Modelo_Producto,
                                     EnOperacion = false,
                                     Estatus = false
                                 }
                            ).ToList();

            return resultado.FindAll(me => !new int?[] {5, 7}.Contains(me.IdTecnologia));
        }

        public List<EquipoAltaEficienciaMrv> ObtenEquiposAltaCuestionario(string idCredito, int idCuestionario)
        {
            List<EquipoAltaEficienciaMrv> lstEquipos = null;

            var resultado = (from equipos in _contexto.MRV_EQUIPOS_CUESTIONARIO
                             where equipos.IdCuestionario == idCuestionario
                             select new EquipoAltaEficienciaMrv
                                 {
                                     IdEquipo = equipos.IdEquipoCuestionario,
                                     IdCreditoProducto = (int) equipos.ID_CREDITO_PRODUCTO,
                                     NombreEquipo = equipos.DescripcionEquipo,
                                     EnOperacion = (bool) equipos.EnOperacion,
                                     Estatus = (bool) equipos.Estatus
                                 }).ToList();

            return resultado;
        }

        public List<Medicion> ObtenCataglogoMediciones(string idCredito)
        {
            var lstMedicones = new Mediciones().ObtenListaMedicionesCredito(idCredito);
            var lstCuestionarios = ObtenListaCuestionariosCredito(idCredito);
            
            if (lstCuestionarios.Count > 0)
            {
                var idsCuestionarios = lstCuestionarios.Select(p => p.IdMedicion);
                lstMedicones = lstMedicones.Where(p => idsCuestionarios.Contains(p.IdMedicion)).ToList();
            }

            return lstMedicones;
        }

        public List<Cuestionario> ObtenListaCuestionariosCredito(string idCredito)
        {
            var resultado = (from cuestionario in _contexto.MRV_CUESTIONARIO
                             where cuestionario.No_Credito == idCredito
                             select new Cuestionario
                                 {
                                     IdCuestionario = cuestionario.IdCuestionario,
                                     //IdMedicion = (int) cuestionario.IdMedicion,
                                     FechaCuestionario = (DateTime) cuestionario.FechaCuestionario,
                                     Estatus = (bool) cuestionario.Estatus
                                 }).ToList();

            return resultado;
        }

        public List<MRV_EQUIPOS_CUESTIONARIO> ObtenEquiposCuestionario(int idCuestionario)
        {
            List<MRV_EQUIPOS_CUESTIONARIO> lstEquiposCuestionario = null;

            using (var r = new Repositorio<MRV_EQUIPOS_CUESTIONARIO>())
            {
                lstEquiposCuestionario = r.Filtro(me => me.IdCuestionario == idCuestionario && me.EnOperacion == true);
            }

            return lstEquiposCuestionario;
        }

        public MRV_CUESTIONARIO ObtenCuestionario(int idCuestionario)
        {
            MRV_CUESTIONARIO cuestionario = null;

            using (var r = new Repositorio<MRV_CUESTIONARIO>())
            {
                cuestionario = r.Extraer(me => me.IdCuestionario == idCuestionario);
            }

            return cuestionario;
        }

        public List<MRV_CUESTIONARIO> ObtenListaCuestionarios(string idCredito, bool estatus)
        {
            List<MRV_CUESTIONARIO> lstCuestionarios = null;

            using (var r = new Repositorio<MRV_CUESTIONARIO>())
            {
                lstCuestionarios = r.Filtro(me => me.No_Credito == idCredito);
            }

            return lstCuestionarios;
        }

        public List<MRV_HORARIOS_OPERACION> ObtenHorariosOperacionEquipo(int idCuestionario, int idEquipoCuestionario)
        {
            List<MRV_HORARIOS_OPERACION> lstHorarios = null;

            using (var r = new Repositorio<MRV_HORARIOS_OPERACION>())
            {
                lstHorarios =
                    r.Filtro(
                        me => me.IdCuestionario == idCuestionario && me.IdEquipoCuestionario == idEquipoCuestionario);
            }

            return lstHorarios;
        }

        public List<MRV_HORARIOS_OPERACION> ObtenHorariosNegocio(int idCuestionario, int idTipoHorario)
        {
            List<MRV_HORARIOS_OPERACION> lstHorarios = null;

            using (var r = new Repositorio<MRV_HORARIOS_OPERACION>())
            {
                lstHorarios =
                    r.Filtro(
                        me => me.IdCuestionario == idCuestionario && me.IDTIPOHORARIO == 1);
            }

            return lstHorarios;
        }

        public MRV_HORAS_OPERACION_TOTAL ObtenTotalHorasEquipo(int idCuestionario, int idEquipo)
        {
            MRV_HORAS_OPERACION_TOTAL horasOperacionTotal = null;

            using (var r = new Repositorio<MRV_HORAS_OPERACION_TOTAL>())
            {
                horasOperacionTotal =
                    r.Extraer(me => me.IdCuestionario == idCuestionario && me.IdEquipoCuestionario == idEquipo);
            }

            return horasOperacionTotal;
        }

        public MRV_HORAS_OPERACION_TOTAL ObtenTotalHorasNegocio(int idCuestionario, int idTipoHorario)
        {
            MRV_HORAS_OPERACION_TOTAL horasOperacionTotal = null;

            using (var r = new Repositorio<MRV_HORAS_OPERACION_TOTAL>())
            {
                horasOperacionTotal =
                    r.Extraer(me => me.IdCuestionario == idCuestionario && me.IDTIPOHORARIO == idTipoHorario);
            }

            return horasOperacionTotal;
        }

        public List<MRV_HORAS_OPERACION_TOTAL> ObtenOperacionTotal(int idCuestionario)
        {
            List<MRV_HORAS_OPERACION_TOTAL> lstOperacionTotal = null;

            using (var r = new Repositorio<MRV_HORAS_OPERACION_TOTAL>())
            {
                lstOperacionTotal = r.Filtro(me => me.IdCuestionario == idCuestionario && me.IDTIPOHORARIO == 2);
            }

            return lstOperacionTotal;
        }

        public MRV_FOTOS_CUESTIONARIO ObtenFotoCuestionario(int idCuestionario)
        {
            MRV_FOTOS_CUESTIONARIO foto = null;

            using (var r = new Repositorio<MRV_FOTOS_CUESTIONARIO>())
            {
                foto = r.Extraer(me => me.IdCuestionario == idCuestionario);
            }

            return foto;
        }

        #endregion

        #region Metodos de Guardado

        public MRV_CUESTIONARIO GuardaCuestionario(MRV_CUESTIONARIO cuestionario)
        {
            MRV_CUESTIONARIO newCuestionario = null;

            using (var r = new Repositorio<MRV_CUESTIONARIO>())
            {
                newCuestionario = r.Agregar(cuestionario);
            }

            return newCuestionario;
        }

        public void GuardaEquiposCuestionario(MRV_EQUIPOS_CUESTIONARIO equipo)
        {
            using (var r = new Repositorio<MRV_EQUIPOS_CUESTIONARIO>())
            {
                r.Agregar(equipo);
            }
        }

        public void GuardaHorarioDiaEquipo(MRV_HORARIOS_OPERACION horario)
        {
            using (var r = new Repositorio<MRV_HORARIOS_OPERACION>())
            {
                r.Agregar(horario);
            }
        }

        public void GuardaOperacionTotalEquipo(MRV_HORAS_OPERACION_TOTAL horasOperacionTotal)
        {
            using (var r = new Repositorio<MRV_HORAS_OPERACION_TOTAL>())
            {
                r.Agregar(horasOperacionTotal);
            }
        }

        #endregion

        #region Metodos de Actualizacion

        public bool ActualizaCuestionario(MRV_CUESTIONARIO cuestionario)
        {
            bool actualiza = false;

            using (var r = new Repositorio<MRV_CUESTIONARIO>())
            {
                actualiza = r.Actualizar(cuestionario);
            }

            return actualiza;
        }

        #endregion

        #region Metodos de Borrado

        public void EliminaHorariosEquipo(int idEquipoCuestionario)
        {
            using (var r = new Repositorio<MRV_HORARIOS_OPERACION>())
            {
                var lstHorarios = r.Filtro(me => me.IdEquipoCuestionario == idEquipoCuestionario);

                if (lstHorarios.Count > 0)
                {
                    foreach (var mrvHorariosOperacion in lstHorarios)
                    {
                        r.Eliminar(mrvHorariosOperacion);
                    }
                }
            }
        }

        public void EliminaHorariosNegocio(int idCuestionario, int idTipoHorario)
        {
            using (var r = new Repositorio<MRV_HORARIOS_OPERACION>())
            {
                var lstHorarios =
                    r.Filtro(me => me.IdCuestionario == idCuestionario && me.IDTIPOHORARIO == idTipoHorario);

                if (lstHorarios.Count > 0)
                {
                    foreach (var mrvHorariosOperacion in lstHorarios)
                    {
                        r.Eliminar(mrvHorariosOperacion);
                    }
                }
            }
        }

        public void EliminaOperacionTotal(int idCuestionario, int idTipoHorario, int idEquipo)
        {
            using (var r = new Repositorio<MRV_HORAS_OPERACION_TOTAL>())
            {
                var operacionTotal =
                    r.Extraer(me => me.IdCuestionario == idCuestionario && me.IDTIPOHORARIO == idTipoHorario && me.IdEquipoCuestionario == idEquipo);

                if (operacionTotal != null)
                {
                    r.Eliminar(operacionTotal);
                }
            }
        }

        public void EliminaOperacionTotalNegocio(int idCuestionario, int idTipoHorario)
        {
            using (var r = new Repositorio<MRV_HORAS_OPERACION_TOTAL>())
            {
                var operacionTotal =
                    r.Extraer(me => me.IdCuestionario == idCuestionario && me.IDTIPOHORARIO == idTipoHorario);

                if (operacionTotal != null)
                {
                    r.Eliminar(operacionTotal);
                }
            }
        }

        #endregion
    }
}
