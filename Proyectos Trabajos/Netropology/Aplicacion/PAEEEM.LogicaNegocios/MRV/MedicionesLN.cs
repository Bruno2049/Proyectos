using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using PAEEEM.AccesoDatos.MRV;
using PAEEEM.AccesoDatos.SolicitudCredito;
using PAEEEM.Entidades;
using PAEEEM.Entidades.MRV;

namespace PAEEEM.LogicaNegocios.MRV
{
    public class MedicionesLN
    {
        public MedicionesLN(){ }

        #region Metodos de Consulta

        public List<MRV_CAT_PERSONAL_MEDICION> ObtenPersonalMedicion(Expression<Func<MRV_CAT_PERSONAL_MEDICION, bool>> criterio)
        {
            var resultado = new Mediciones().ObtenPersonalMedicion(criterio);

            return resultado;
        }

        public List<MRV_MEDICION> ObtenMedicionesCredito(string idCredito, bool estatus)
        {
            var resultado = new Mediciones().ObtenMedicionesCredito(idCredito, estatus);

            return resultado;
        }

        public List<Medicion> ObtenListaMedicionesCredito(string idCredito)
        {
            var resultado = new Mediciones().ObtenListaMedicionesCredito(idCredito);

            foreach (var medicion in resultado)
            {
                medicion.DescripcionMedicion = "Medición fecha " + medicion.FechaMedicion.ToString("dd/MM/yyyy");
            }

            return resultado;
        }

        public MRV_MEDICION ObtenMedicion(int idMedicion)
        {
            var resultado = new Mediciones().ObtenMedicion(idMedicion);

            return resultado;
        }

        public List<MedicionDetalle> ObtenDetalleMedicion(int idMedicion)
        {
            var resultado = new Mediciones().ObtenDetalleMedicion(idMedicion);

            return resultado;
        }

        public List<MRV_EQUIPOS_MEDICION> ObtenEquiposMedicion(int idMedicion)
        {
            var resultado = new Mediciones().ObtenEquiposMedicion(idMedicion);

            return resultado;
        }

        public MRV_EQUIPOS_MEDICION ObtenEquipoMedicionConcepto(int idMedicion, int idEquipoMedicion)
        {
            var resultado = new Mediciones().ObtenEquipoMedicionConcepto(idMedicion, idEquipoMedicion);

            return resultado;
        }

        public List<CRE_GRUPOS_TECNOLOGIA> ObtenGruposCredito(string idCredito)
        {
            var resultado = new Mediciones().ObtenGruposCredito(idCredito);

            return resultado;
        }

        public List<GrupoCredito> ObtenGrupos(int idMedicion)
        {
            var resultado = new Mediciones().ObtenGrupos(idMedicion);

            return resultado;
        }

        public DatosCredito ObtenDatosCredito(string idCredito)
        {
            var resultado = new CREDITO_DAL().GetDatosCredito(idCredito);

            return resultado;
        }

        public bool GuardaFotoMedicion(MRV_FOTOS_MEDICION fotoMedicion, int idMedicion)
        {
            var resultado = new Mediciones().GuardaFotoMedicion(fotoMedicion, idMedicion);

            return resultado;
        }

        public MRV_FOTOS_MEDICION ObtenFotoMedicion(int idMedicion)
        {
            var resultado = new Mediciones().ObtenFotoMedicion(idMedicion);

            return resultado;
        }

        public CRE_GRUPOS_TECNOLOGIA ObtenGrupo(string idCredito, int idGrupo)
        {
            var resultado = new Mediciones().ObtenGrupo(idCredito, idGrupo);

            return resultado;
        }

        #endregion

        #region Metodos de Guardado

        public MRV_MEDICION GuardaMedicion(MRV_MEDICION medicion)
        {
            var resultado = new Mediciones().GuardaMedicion(medicion);

            return resultado;
        }

        public void GuardaGruposMedicion(string idCredito, int idMedicion, string usuario)
        {
            var lstGruposCredito = new Mediciones().ObtenGruposCredito(idCredito);

            foreach (var creGruposTecnologia in lstGruposCredito)
            {
                var grupoMedicion = new MRV_GRUPOS_MEDICION
                    {
                        IdMedicion = idMedicion,
                        IdGrupo = creGruposTecnologia.IdGrupo,
                        EsTrifasico = false,
                        Estatus = true,
                        FechaAdicion = DateTime.Now.Date,
                        AdicionadoPor = usuario
                    };

                new Mediciones().GuardaGruposMedicion(grupoMedicion);
            }            
        }

        public bool GuardaMedicionDetalle(List<MedicionDetalle> lstMedicionDetalle)
        {
            try
            {
                foreach (var mrvMedicionDetalle in lstMedicionDetalle.FindAll(me => me.IdMedicionDetalle == 0))
                {
                    var medicionDetalle = new MRV_MEDICION_DETALLE();
                    medicionDetalle.IdMedicion = mrvMedicionDetalle.IdMedicion;
                    medicionDetalle.IdCampoMedicion = Convert.ToByte(mrvMedicionDetalle.IdCampoMedicion);
                    medicionDetalle.IdGrupo = mrvMedicionDetalle.IdGrupo;
                    medicionDetalle.Grupo = mrvMedicionDetalle.Grupo;
                    medicionDetalle.EquipoIneficiente = mrvMedicionDetalle.EquipoIneficiente;
                    medicionDetalle.EquipoEficiente = mrvMedicionDetalle.EquipoIneficiente;
                    medicionDetalle.Unidad = mrvMedicionDetalle.Unidad;
                    medicionDetalle.Valor = mrvMedicionDetalle.Valor;
                    medicionDetalle.Porcentaje = mrvMedicionDetalle.Porcentaje;
                    medicionDetalle.Observaciones = mrvMedicionDetalle.Observaciones;
                    medicionDetalle.Estatus = mrvMedicionDetalle.Estatus;
                    medicionDetalle.FechaAdicion = mrvMedicionDetalle.FechaAdicion;
                    medicionDetalle.AdicionadoPor = mrvMedicionDetalle.AdicionadoPor;

                    var newMedicionDetalle = new Mediciones().GuardaMedicionDetalle(medicionDetalle);
                }

                foreach (var medicionDetalle in lstMedicionDetalle.FindAll(me => me.IdMedicionDetalle != 0))
                {
                    var medicionDetalleConcepto =
                        new Mediciones().ObtenMedicionDetalleConcepto(medicionDetalle.IdMedicion,
                                                                      medicionDetalle.IdMedicionDetalle);

                    if (medicionDetalleConcepto != null)
                    {
                        medicionDetalleConcepto.EquipoIneficiente = medicionDetalle.EquipoIneficiente;
                        medicionDetalleConcepto.EquipoEficiente = medicionDetalle.EquipoIneficiente;
                        medicionDetalleConcepto.Unidad = medicionDetalle.Unidad;
                        medicionDetalleConcepto.Valor = medicionDetalle.Valor;
                        medicionDetalleConcepto.Porcentaje = medicionDetalle.Porcentaje;
                        medicionDetalleConcepto.Observaciones = medicionDetalle.Observaciones;
                        medicionDetalleConcepto.Estatus = medicionDetalle.Estatus;

                        new Mediciones().ActualizaMedicionDetalle(medicionDetalleConcepto);
                    }
                }
            }
            catch
            {
                return false;
            }
            
            
            return true;
        }

        public MRV_EQUIPOS_MEDICION GuardaEquipoMedicion(MRV_EQUIPOS_MEDICION equiposMedicion)
        {
            var resultado = new Mediciones().GuardaEquipoMedicion(equiposMedicion);

            return resultado;
        }

        #endregion

        #region Metodos de Actualizacion

        public bool ActualizaMedicion(MRV_MEDICION medicion)
        {
            var resultado = new Mediciones().ActualizaMedicion(medicion);

            return resultado;
        }

        //public void ActualizaGruposMedicion(List<MRV_GRUPOS_MEDICION> lstGruposMedicion)
        //{
        //    foreach (var mrvGruposMedicion in lstGruposMedicion)
        //    {
        //        new Mediciones().ActualizaGrupoMedicion(mrvGruposMedicion);
        //    }
        //}

        public void ActualizaGruposMedicion(List<GrupoCredito> lstGruposMedicion, int idMedicion)
        {
            foreach (var grupoCredito in lstGruposMedicion)
            {
                var grupo = new Mediciones().ObtenGrupoMedicion(idMedicion, grupoCredito.IdGrupo);
                grupo.EsTrifasico = grupoCredito.EsTrifasico;

                new Mediciones().ActualizaGrupoMedicion(grupo);
            }
        }

        #endregion

        #region Metodos de Borrado

        public bool EliminaEquipoMedicion(int idEquipoMedicion)
        {
            var resultado = new Mediciones().EliminaEquipoMedicion(idEquipoMedicion);

            return resultado;
        }

        #endregion
    }
}
