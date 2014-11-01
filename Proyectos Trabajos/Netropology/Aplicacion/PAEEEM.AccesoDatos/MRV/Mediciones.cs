using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using PAEEEM.Entidades;
using PAEEEM.Entidades.MRV;

namespace PAEEEM.AccesoDatos.MRV
{
    public class Mediciones
    {
        private readonly PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();

        public Mediciones()
        {

        }

        #region Metodos de Consulta

        public List<MRV_CAT_PERSONAL_MEDICION> ObtenPersonalMedicion(Expression<Func<MRV_CAT_PERSONAL_MEDICION, bool>> criterio)
        {
            List<MRV_CAT_PERSONAL_MEDICION> lstPersonal = null;

            using (var r = new Repositorio<MRV_CAT_PERSONAL_MEDICION>())
            {
                lstPersonal = r.Filtro(criterio);
            }

            return lstPersonal;
        }

        public List<MRV_MEDICION> ObtenMedicionesCredito(string idCredito, bool estatus)
        {
            List<MRV_MEDICION> lstMedicionesCredito = null;

            using (var r = new Repositorio<MRV_MEDICION>())
            {
                lstMedicionesCredito = r.Filtro(me => me.No_Credito == idCredito && me.Estatus == estatus);
            }

            return lstMedicionesCredito;
        }

        public MRV_MEDICION ObtenMedicion(int idMedicion)
        {
            MRV_MEDICION medicion = null;

            using (var r = new Repositorio<MRV_MEDICION>())
            {
                medicion = r.Extraer(me => me.IdMedicion == idMedicion);
            }

            return medicion;
        }

        public List<Medicion> ObtenListaMedicionesCredito(string idCredito)
        {
            var resultado = (from medicion in _contexto.MRV_MEDICION
                             where medicion.No_Credito == idCredito
                             select new Medicion
                                 {
                                     IdMedicion = medicion.IdMedicion,
                                     FechaMedicion = (DateTime) medicion.FechaMedicion,
                                     Estatus = (bool) medicion.Estatus
                                 }).ToList();
            return resultado;
        }

        public List<MRV_MEDICION_DETALLE> ObtenMedicionDetalle(int idMedicion)
        {
            List<MRV_MEDICION_DETALLE> lstMedicionDetalle = null;

            using (var r = new Repositorio<MRV_MEDICION_DETALLE>())
            {
                lstMedicionDetalle = r.Filtro(me => me.IdMedicion == idMedicion);
            }

            return lstMedicionDetalle;
        }

        public List<MedicionDetalle> ObtenDetalleMedicion(int idMedicion)
        {
            var resultado = (from medicion in _contexto.MRV_MEDICION_DETALLE
                             where medicion.IdMedicion == idMedicion
                             select new MedicionDetalle
                                 {
                                     IdMedicionDetalle = medicion.IdMedicionDetalle,
                                     IdMedicion = (int) medicion.IdMedicion,
                                     IdCampoMedicion = medicion.IdCampoMedicion,
                                     IdGrupo = (int) medicion.IdGrupo,
                                     Grupo = medicion.Grupo,
                                     EquipoIneficiente = (decimal) medicion.EquipoEficiente,
                                     EquipoEficiente = (decimal) medicion.EquipoEficiente,
                                     Unidad = medicion.Unidad,
                                     Valor = (decimal) medicion.Valor,
                                     Porcentaje = (decimal) medicion.Porcentaje,
                                     Observaciones = medicion.Observaciones,
                                     Estatus = (bool) medicion.Estatus,
                                     FechaAdicion = (DateTime) medicion.FechaAdicion,
                                     AdicionadoPor = medicion.AdicionadoPor
                                 }).ToList();

            return resultado;
        }

        public MRV_MEDICION_DETALLE ObtenMedicionDetalleConcepto(int idMedicion, int idMedicionDetalle)
        {
            MRV_MEDICION_DETALLE medicionDetalle = null;

            using (var r = new Repositorio<MRV_MEDICION_DETALLE>())
            {
                medicionDetalle = r.Extraer(me => me.IdMedicion == idMedicion && me.IdMedicionDetalle == idMedicionDetalle);
            }

            return medicionDetalle;
        }

        public List<MRV_EQUIPOS_MEDICION> ObtenEquiposMedicion(int idMedicion)
        {
            List<MRV_EQUIPOS_MEDICION> lstEquiposMedicion = null;

            using (var r = new Repositorio<MRV_EQUIPOS_MEDICION>())
            {
                lstEquiposMedicion = r.Filtro(me => me.IdMedicion == idMedicion);
            }

            return lstEquiposMedicion;
        }

        public MRV_EQUIPOS_MEDICION ObtenEquipoMedicionConcepto(int idMedicion, int idEquipoMedicion)
        {
            MRV_EQUIPOS_MEDICION equiposMedicion = null;

            using (var r = new Repositorio<MRV_EQUIPOS_MEDICION>())
            {
                equiposMedicion = r.Extraer(me => me.IdMedicion == idMedicion && me.IdEquipoMedicion == idEquipoMedicion);
            }

            return equiposMedicion;
        }

        public List<CRE_GRUPOS_TECNOLOGIA> ObtenGruposCredito(string idCredito)
        {
            List<CRE_GRUPOS_TECNOLOGIA> lstGruposCredito = null;

            using (var r = new Repositorio<CRE_GRUPOS_TECNOLOGIA>())
            {
                lstGruposCredito = r.Filtro(me => me.No_Credito == idCredito && !new int?[] { 5,7 }.Contains(me.IdTecnologia));
            }

            return lstGruposCredito;
        }

        public List<GrupoCredito> ObtenGrupos(int idMedicion)
        {
            var resultado = (from grupos in _contexto.MRV_GRUPOS_MEDICION
                             where grupos.IdMedicion == idMedicion
                             select new GrupoCredito
                                 {
                                     IdGrupo = grupos.IdGrupo,
                                     EsTrifasico = (bool)grupos.EsTrifasico
                                 }).ToList();

            return resultado;
        }

        public MRV_GRUPOS_MEDICION ObtenGrupoMedicion(int idMedicion, int idGrupo)
        {
            MRV_GRUPOS_MEDICION grupoMedicion = null;

            using (var r = new Repositorio<MRV_GRUPOS_MEDICION>())
            {
                grupoMedicion = r.Extraer(me => me.IdMedicion == idMedicion && me.IdGrupo == idGrupo);
            }

            return grupoMedicion;
        }

        #endregion

        #region Metodos de Guardado

        public MRV_MEDICION GuardaMedicion(MRV_MEDICION medicion)
        {
            MRV_MEDICION newMedicion = null;

            using (var r = new Repositorio<MRV_MEDICION>())
            {
                newMedicion = r.Agregar(medicion);
            }

            return newMedicion;
        }

        public MRV_MEDICION_DETALLE GuardaMedicionDetalle(MRV_MEDICION_DETALLE medicionDetalle)
        {
            MRV_MEDICION_DETALLE mrvMedicionDetalle = null;

            using (var r = new Repositorio<MRV_MEDICION_DETALLE>())
            {
                mrvMedicionDetalle = r.Agregar(medicionDetalle);
            }

            return mrvMedicionDetalle;
        }

        public MRV_EQUIPOS_MEDICION GuardaEquipoMedicion(MRV_EQUIPOS_MEDICION equiposMedicion)
        {
            MRV_EQUIPOS_MEDICION mrvEquipo = null;

            using (var r = new Repositorio<MRV_EQUIPOS_MEDICION>())
            {
                mrvEquipo = r.Agregar(equiposMedicion);
            }

            return mrvEquipo;
        }

        public MRV_GRUPOS_MEDICION GuardaGruposMedicion(MRV_GRUPOS_MEDICION mrvGruposMedicion)
        {
            MRV_GRUPOS_MEDICION newMrvGruposMedicion = null;

            using (var r = new Repositorio<MRV_GRUPOS_MEDICION>())
            {
                newMrvGruposMedicion = r.Agregar(mrvGruposMedicion);
            }

            return newMrvGruposMedicion;
        }

        public bool GuardaFotoMedicion(MRV_FOTOS_MEDICION fotoMedicion, int idMedicion)
        {
            using (var context = new PAEEEM_DESAEntidades())
            {
                if (context.MRV_FOTOS_MEDICION.Any(me => me.IdMedicion == idMedicion))
                {
                    var fotoExistente =
                        context.MRV_FOTOS_MEDICION.FirstOrDefault(me => me.IdMedicion == idMedicion);
                    fotoExistente.Nombre = fotoMedicion.Nombre;
                    fotoExistente.Extension = fotoMedicion.Extension;
                    fotoExistente.Longitud = fotoMedicion.Longitud;
                    fotoExistente.Foto = fotoMedicion.Foto;
                    fotoExistente.FechaAdicion = fotoMedicion.FechaAdicion;
                    fotoExistente.AdicionadoPor = fotoMedicion.AdicionadoPor;
                    context.SaveChanges();
                }
                else
                {
                    fotoMedicion.IdMedicion = idMedicion;
                    context.MRV_FOTOS_MEDICION.Add(fotoMedicion);
                    context.SaveChanges();
                }
            }

            return true;
        }

        public MRV_FOTOS_MEDICION ObtenFotoMedicion(int idMedicion)
        {
            MRV_FOTOS_MEDICION foto = null;

            using (var r = new Repositorio<MRV_FOTOS_MEDICION>())
            {
                foto = r.Extraer(me => me.IdMedicion == idMedicion);
            }

            return foto;
        }

        public CRE_GRUPOS_TECNOLOGIA ObtenGrupo(string idCredito, int idGrupo)
        {
            CRE_GRUPOS_TECNOLOGIA grupo = null;

            using (var r = new Repositorio<CRE_GRUPOS_TECNOLOGIA>())
            {
                grupo = r.Extraer(me => me.No_Credito == idCredito && me.IdGrupo == idGrupo);
            }

            return grupo;
        }

        #endregion

        #region Metodos de Actualizacion

        public bool ActualizaMedicion(MRV_MEDICION medicion)
        {
            bool actualiza;

            using (var r = new Repositorio<MRV_MEDICION>())
            {
                actualiza = r.Actualizar(medicion);
            }

            return actualiza;
        }

        public bool ActualizaMedicionDetalle(MRV_MEDICION_DETALLE medicionDetalle)
        {
            bool actualiza;

            using (var r = new Repositorio<MRV_MEDICION_DETALLE>())
            {
                actualiza = r.Actualizar(medicionDetalle);
            }

            return actualiza;
        }

        public bool ActualizaEquipoMedicion(MRV_EQUIPOS_MEDICION equiposMedicion)
        {
            bool actualiza;

            using (var r = new Repositorio<MRV_EQUIPOS_MEDICION>())
            {
                actualiza = r.Actualizar(equiposMedicion);
            }

            return actualiza;
        }

        public bool ActualizaGrupoMedicion(MRV_GRUPOS_MEDICION gruposMedicion)
        {
            bool actualiza;

            using (var r = new Repositorio<MRV_GRUPOS_MEDICION>())
            {
                actualiza = r.Actualizar(gruposMedicion);
            }

            return actualiza;
        }

        #endregion

        #region Metodos de Borrado

        public bool EliminaEquipoMedicion(int idEquipoMedicion)
        {
            bool elimina = false;

            using (var r = new Repositorio<MRV_EQUIPOS_MEDICION>())
            {
                var equipoEliminar = r.Extraer(me => me.IdEquipoMedicion == idEquipoMedicion);

                if(equipoEliminar!= null)
                    elimina = r.Eliminar(equipoEliminar);
            }

            return elimina;
        }

        #endregion
    }
}
