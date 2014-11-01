using System.Collections.Generic;
using System.Linq;
using PAEEEM.Entidades;
using PAEEEM.Entidades.Alta_Solicitud;

namespace PAEEEM.AccesoDatos.Catalogos
{
    public class CapturaSolicitud
    {
        //private readonly PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();

        public static List<CAT_TIPO_INDUSTRIA> ObtenCatTipoIndustrias()
        {
            List<CAT_TIPO_INDUSTRIA> lstCatTipoIndustrias;

            using (var r = new Repositorio<CAT_TIPO_INDUSTRIA>())
            {
                lstCatTipoIndustrias = new List<CAT_TIPO_INDUSTRIA>(r.Filtro(me => me.DESCRIPCION_SCIAN != "" && me.ESTATUS == true).OrderBy(d=> d.DESCRIPCION_SCIAN));
                
            }

            return lstCatTipoIndustrias; 
        }

        public static CAT_TIPO_INDUSTRIA ObtenTipoIndustria(int idTipoIndustria)
        {
            CAT_TIPO_INDUSTRIA tipoIndustria;

            using (var r = new Repositorio<CAT_TIPO_INDUSTRIA>())
            {
                tipoIndustria = r.Extraer(me => me.Cve_Tipo_Industria == idTipoIndustria);
            }

            return tipoIndustria;
        }
        
        public static List<CAT_ESTADO> ObtenCatEstadosRepublica()
        {
            List<CAT_ESTADO> lstCatEstados;

            using (var r = new Repositorio<CAT_ESTADO>())
            {
                lstCatEstados = r.Filtro(me => me.Cve_Estado > 0);
            }

            return lstCatEstados;
        }

        public static CAT_ESTADO ObtenEstadoXId(int idEstado)
        {
            CAT_ESTADO estado;

            using (var r = new Repositorio<CAT_ESTADO>())
            {
                estado = r.Extraer(me => me.Cve_Estado == idEstado);
            }

            return estado;
        }

        public static List<CAT_DELEG_MUNICIPIO> ObtenDelegMunicipios(int idEstado)
        {
            List<CAT_DELEG_MUNICIPIO> lstCatDelegMunicipios;

            using (var r = new Repositorio<CAT_DELEG_MUNICIPIO>())
            {
                lstCatDelegMunicipios = new List<CAT_DELEG_MUNICIPIO>(r.Filtro(me => me.Cve_Estado == idEstado).OrderBy(b => b.Dx_Deleg_Municipio));
            }

            return lstCatDelegMunicipios;
        }

        public static CAT_DELEG_MUNICIPIO ObtenMunicipioXId(int idMunicipio)
        {
            CAT_DELEG_MUNICIPIO municipio;

            using (var r = new Repositorio<CAT_DELEG_MUNICIPIO>())
            {
                municipio = r.Extraer(me => me.Cve_Deleg_Municipio == idMunicipio);
            }

            return municipio;
        }

        public static List<CAT_CODIGO_POSTAL_SEPOMEX> ObtenCatCodigoPostals(int idEstado, int idDelMunicipio)
        {
            List<CAT_CODIGO_POSTAL_SEPOMEX> lstCatCodigoPostals;

            using (var r = new Repositorio<CAT_CODIGO_POSTAL_SEPOMEX>())
            {
                lstCatCodigoPostals =
                 new List<CAT_CODIGO_POSTAL_SEPOMEX>(r.Filtro(me => me.Cve_Estado == idEstado && me.Cve_Deleg_Municipio == idDelMunicipio).OrderBy(d => d.Dx_Colonia));
            }

            return lstCatCodigoPostals;
        }

        public static CAT_CODIGO_POSTAL_SEPOMEX ObtenColoniaSepomex(int cveCp)
        {
            using (var r = new Repositorio<CAT_CODIGO_POSTAL_SEPOMEX>())
            {
                return r.Extraer(me => me.Cve_CP == cveCp);
            }
        }

        public static List<Colonia> ObtenColoniasXCp(string codigoPostal)
        {
            var _contexto = new PAEEEM_DESAEntidades();

            var resultado = (from colonias in _contexto.CAT_CODIGO_POSTAL_SEPOMEX
                            join estados in _contexto.CAT_ESTADO on colonias.Cve_Estado equals estados.Cve_Estado
                            join municipios in _contexto.CAT_DELEG_MUNICIPIO on colonias.Cve_Deleg_Municipio equals municipios.Cve_Deleg_Municipio
                            where colonias.Codigo_Postal == codigoPostal
                            && municipios.Cve_Estado == colonias.Cve_Estado
                            select new Colonia
                            {
                                CveCp = colonias.Cve_CP,
                                CodigoPostal = colonias.Codigo_Postal,
                                DxColonia = colonias.Dx_Colonia,
                                DxTipoColonia = colonias.Dx_Tipo_Colonia,
                                CveDelegMunicipio = colonias.Cve_Deleg_Municipio,
                                DxDelegacionMunicipio = municipios.Dx_Deleg_Municipio,
                                CveEstado = colonias.Cve_Estado,
                                DxEstado = estados.Dx_Nombre_Estado
                            }).OrderBy(b=> b.DxColonia).ToList();

            return resultado;
        }

        public static K_DATOS_PYME GuardDatosPyme(K_DATOS_PYME datos)
        {
            K_DATOS_PYME resuDatosPyme;

            using (var r = new Repositorio<K_DATOS_PYME>())
            {
                resuDatosPyme = r.Agregar(datos);
            }

            return resuDatosPyme;
        }


        public static K_DATOS_PYME BuscaDatosPyme(string noRpu)
        {
            K_DATOS_PYME resultado;

            using (var r = new Repositorio<K_DATOS_PYME>())
            {
                var max = r.Filtro(me => me.No_RPU == noRpu).Max(me => me.Cve_Dato_Pyme);

                resultado = r.Filtro(me => me.No_RPU == noRpu).FirstOrDefault(me => me.Cve_Dato_Pyme == max);
            }

            return resultado;
        }

        public static bool ActualizaDatosPyme(K_DATOS_PYME pyme)
        {
            bool actualiza;

            using (var r = new Repositorio<K_DATOS_PYME>())
            {
                actualiza = r.Actualizar(pyme);
            }

            return actualiza;
        }

        public static List<CAT_SEXO> ObtenCatSexos()
        {
            List<CAT_SEXO> lstCatSexos;

            using (var r = new Repositorio<CAT_SEXO>())
            {
                lstCatSexos = r.Filtro(me => me.FG_Sexo > 0);
            }

            return lstCatSexos;
        }

        public static List<CAT_CLI_REGIMEN_CONYUGAL> ObtenCatRegimenConyugal()
        {
            List<CAT_CLI_REGIMEN_CONYUGAL> lsCatRegimenConyugals;

            using (var r = new Repositorio<CAT_CLI_REGIMEN_CONYUGAL>())
            {
                lsCatRegimenConyugals = r.Filtro(me => me.Estatus == 1);
            }

            return lsCatRegimenConyugals;
        }

        public static List<CAT_CLI_TIPO_ACREDITACION> ObtenCatTipoAcreditacion()
        {
            List<CAT_CLI_TIPO_ACREDITACION> lsTipoAcreditacions;

            using (var r = new Repositorio<CAT_CLI_TIPO_ACREDITACION>())
            {
                lsTipoAcreditacions = r.Filtro(me => me.Estatus == 1);
            }

            return lsTipoAcreditacions;
        }

        public static List<CAT_CLI_TIPO_IDENTIFICACION> ObtenCatIdentificacion()
        {
            List<CAT_CLI_TIPO_IDENTIFICACION> lstCatIdentificacions;

            using (var r = new Repositorio<CAT_CLI_TIPO_IDENTIFICACION>())
            {
                lstCatIdentificacions = r.Filtro(me => me.Estatus == 1);
            }

            return lstCatIdentificacions;
        }

        public static List<CAT_TIPO_PROPIEDAD> ObtenCatTipoPropiedad()
        {
            List<CAT_TIPO_PROPIEDAD> lsTipoPropiedad;

            using (var r = new Repositorio<CAT_TIPO_PROPIEDAD>())
            {
                lsTipoPropiedad = r.Filtro(me => me.Cve_Tipo_Propiedad > 0);
            }

            return lsTipoPropiedad;
        }

        public static List<CAT_ESTADO_CIVIL> ObtenCatEstadoCivil()
        {
            List<CAT_ESTADO_CIVIL> lstCatEstadoCivil;

            using (var r = new Repositorio<CAT_ESTADO_CIVIL>())
            {
                lstCatEstadoCivil = r.Filtro(me => me.Cve_Estado_Civil > 0);
            }

            return lstCatEstadoCivil;
        }

        public static List<CAT_TIPO_SOCIEDAD> ObtenCatTipoSociedad()
        {
            List<CAT_TIPO_SOCIEDAD> lstCatTipoSociedad;

            using (var r = new Repositorio<CAT_TIPO_SOCIEDAD>())
            {
                lstCatTipoSociedad = r.Filtro(me => me.Cve_Tipo_Sociedad != 3);
            }

            return lstCatTipoSociedad;
        }

        public static List<CAT_TIPO_PRODUCTO> ObtenTipoProducto(int idTecnologia)
        {
            List<CAT_TIPO_PRODUCTO> lstCatTipoProductos;

            using (var r = new Repositorio<CAT_TIPO_PRODUCTO>())
            {
                lstCatTipoProductos = r.Filtro(me => me.Cve_Tecnologia == idTecnologia);
            }

            return lstCatTipoProductos;
        }

        public static K_CREDITO_TEMP InsertaCreditoTemp(K_CREDITO_TEMP creditoTemp)
        {
            K_CREDITO_TEMP resultado;

            using (var r = new Repositorio<K_CREDITO_TEMP>())
            {
                resultado = r.Agregar(creditoTemp);
            }

            return resultado;
        }

        //public static C_DATOS_CLIENTE InsertaDatosCliente(C_DATOS_CLIENTE datosCliente)
        //{
        //    C_DATOS_CLIENTE resultado = null;

        //    using (var r = new Repositorio<C_DATOS_CLIENTE>())
        //    {
        //        resultado = r.Agregar(datosCliente);
        //    }

        //    return resultado;
        //}

        public static List<CLI_HORARIOS_OPERACION> ObtenHorariosOperacion(string noCredito)
        {
            List<CLI_HORARIOS_OPERACION> lstHorariosOperacion;

            using (var r = new Repositorio<CLI_HORARIOS_OPERACION>())
            {
                lstHorariosOperacion = r.Filtro(me => me.No_Credito == noCredito);
            }
            return lstHorariosOperacion;
        }

        public static List<CLI_HORARIOS_OPERACION> ObtenHorariosXTipoOperacion(string noCredito, int tipoOperacion)
        {
            List<CLI_HORARIOS_OPERACION> lstHorariosOperacion;

            using (var r = new Repositorio<CLI_HORARIOS_OPERACION>())
            {
                lstHorariosOperacion = r.Filtro(me => me.No_Credito == noCredito && me.IDTIPOHORARIO == tipoOperacion);
            }
            return lstHorariosOperacion;
        }

        public static CLI_HORARIOS_OPERACION ObtenHorariosOperacionPorDiaOperacion(string noCredito, int tipoHorario, byte? diaOperacion)
        {
            CLI_HORARIOS_OPERACION lstHorariosOperacion;

            using (var r = new Repositorio<CLI_HORARIOS_OPERACION>())
            {
                lstHorariosOperacion =
                    r.Filtro(
                        me =>
                            me.No_Credito == noCredito && me.IDTIPOHORARIO == tipoHorario &&
                            me.ID_DIA_OPERACION == diaOperacion
                        ).FirstOrDefault();
            }
            return lstHorariosOperacion;
        }

        public static H_OPERACION_TOTAL ObtenTotalHorariosOperacion_negocio(string noCredito)
        {
            H_OPERACION_TOTAL totHorariosOperacion;

            using (var r = new Repositorio<H_OPERACION_TOTAL>())
            {
                totHorariosOperacion = r.Filtro(me => me.No_Credito == noCredito && me.Id_Credito_Sustitucion == null && me.ID_CREDITO_PRODUCTO == null).FirstOrDefault();
            }
            return totHorariosOperacion;
        }


        public static CLI_HORARIOS_OPERACION ObtenHorariosOperacionPorDiaOperacion_IdCredSust(string noCredito, int tipoHorario,byte? diaOperacion, int idEquipo)
        {
            CLI_HORARIOS_OPERACION lstHorariosOperacion;

            using (var r = new Repositorio<CLI_HORARIOS_OPERACION>())
            {
                lstHorariosOperacion =
                    r.Filtro(
                        me =>
                            me.No_Credito == noCredito && me.IDTIPOHORARIO == tipoHorario &&
                            me.ID_DIA_OPERACION == diaOperacion && me.Id_Credito_Sustitucion == idEquipo
                        ).FirstOrDefault();


            }
            return lstHorariosOperacion;
        }

        public static H_OPERACION_TOTAL ObtenTotalHorariosOperacion_idCredSust(string noCredito, int idEquipo)
        {
            H_OPERACION_TOTAL totHorariosOperacion;

            using (var r = new Repositorio<H_OPERACION_TOTAL>())
            {
                totHorariosOperacion =r.Filtro(me =>me.No_Credito == noCredito && me.Id_Credito_Sustitucion == idEquipo).FirstOrDefault();
            }
            return totHorariosOperacion;
        }

        public static H_OPERACION_TOTAL ObtenTotalHorariosXTipoOperacion(string noCredito, int tipoOperacion)
        {
            H_OPERACION_TOTAL totHorariosOperacion;

            using (var r = new Repositorio<H_OPERACION_TOTAL>())
            {
                totHorariosOperacion = r.Filtro(me => me.No_Credito == noCredito && me.IDTIPOHORARIO == tipoOperacion).FirstOrDefault();
            }
            return totHorariosOperacion;
        }

        public static CLI_HORARIOS_OPERACION ObtenHorariosOperacionPorDiaOperacion_IdCredProd(string noCredito, int tipoHorario, byte? diaOperacion, int idEquipo,byte? idConsecutivo)
        {
            CLI_HORARIOS_OPERACION lstHorariosOperacion;

            using (var r = new Repositorio<CLI_HORARIOS_OPERACION>())
            {
                lstHorariosOperacion =
                    r.Filtro(
                        me =>
                            me.No_Credito == noCredito && me.IDTIPOHORARIO == tipoHorario &&
                            me.ID_DIA_OPERACION == diaOperacion && me.ID_CREDITO_PRODUCTO == idEquipo
                            && me.IDCONSECUTIVO == idConsecutivo
                            ).FirstOrDefault();
            }
            return lstHorariosOperacion;
        }

        public static H_OPERACION_TOTAL ObtenTotalHorariosOperacion_idCredProd(string noCredito, int idEquipo, byte? idConsecutivo,byte idTipoHorario)
        {
            H_OPERACION_TOTAL totHorariosOperacion;

            using (var r = new Repositorio<H_OPERACION_TOTAL>())
            {
                totHorariosOperacion = r.Filtro(me => me.No_Credito == noCredito && me.ID_CREDITO_PRODUCTO == idEquipo && me.IDCONSECUTIVO == idConsecutivo && me.IDTIPOHORARIO == idTipoHorario).FirstOrDefault();
            }
            return totHorariosOperacion;
        }

        public static H_OPERACION_TOTAL ObtenTotalHorariosOperacion_idCredProd(string noCredito, int idEquipo, byte idConsecutivo)
        {
            H_OPERACION_TOTAL totHorariosOperacion;

            using (var r = new Repositorio<H_OPERACION_TOTAL>())
            {
                totHorariosOperacion = r.Filtro(me => me.No_Credito == noCredito && me.ID_CREDITO_PRODUCTO == idEquipo && me.IDCONSECUTIVO == idConsecutivo).FirstOrDefault();
            }
            return totHorariosOperacion;
        }



        public static CLI_HORARIOS_OPERACION InsertaHorariosOperacion(CLI_HORARIOS_OPERACION horario)
        {
            CLI_HORARIOS_OPERACION resultado;

            using (var r = new Repositorio<CLI_HORARIOS_OPERACION>())
            {
                resultado = r.Agregar(horario);
            }

            return resultado;
        }

        public static H_OPERACION_TOTAL InsertaTotalHorasOperacion(H_OPERACION_TOTAL totalHoras)
        {
            H_OPERACION_TOTAL resultado;

            using (var r = new Repositorio<H_OPERACION_TOTAL>())
            {
                resultado = r.Agregar(totalHoras);
            }

            return resultado;
        }

        public static bool ActualizaTotalHorasOperacion(H_OPERACION_TOTAL totalHoras)
        {
            bool resultado;

            using (var r = new Repositorio<H_OPERACION_TOTAL>())
            {
                resultado = r.Actualizar(totalHoras);
            }

            return resultado;
        }

        public static bool ActualizaHorarioOperacion(CLI_HORARIOS_OPERACION horario)
        {
            bool resultado;

            using (var r = new Repositorio<CLI_HORARIOS_OPERACION>())
            {
                resultado = r.Actualizar(horario);
            }

            return resultado;
        }

        public static K_CREDITO_PRODUCTO ObtenDatoskCreditoProducto(int idCreditoProd)
        {
            K_CREDITO_PRODUCTO datosCreditoProducto;

            using (var r = new Repositorio<K_CREDITO_PRODUCTO>())
            {
                datosCreditoProducto = r.Filtro(me => me.ID_CREDITO_PRODUCTO == idCreditoProd).FirstOrDefault();
            }
            return datosCreditoProducto;
        }

        public static K_CREDITO_SUSTITUCION ObtenDatoskCreditoSustitucion(int idCreditoSust)
        {
            K_CREDITO_SUSTITUCION datosCreditoProducto;

            using (var r = new Repositorio<K_CREDITO_SUSTITUCION>())
            {
                datosCreditoProducto = r.Filtro(me => me.Id_Credito_Sustitucion == idCreditoSust).FirstOrDefault();
            }
            return datosCreditoProducto;
        }



        public static byte[] ObtenFoto_IdCredProducto(int idCredProducto,string tipo)
        {
            K_CREDITO_PRODUCTO datos;
            byte[] existeFoto = null;
           // using (var r = new Repositorio<K_CREDITO_PRODUCTO>())
           // {
           //     datos =
           //         r.Filtro(
           //             me =>
           //                 me.ID_CREDITO_PRODUCTO == idCredProducto
           //                 ).FirstOrDefault();
           // }
           // if(tipo== "Nuevo")
           //  existeFoto =  datos != null ? datos.Fotografia_equipo_Nuevo : null;
           //if(tipo == "Viejo")
           //    existeFoto = datos != null ? datos.Fotografia_equipo_Nuevo : null;
            
            return existeFoto;
        }


        public static byte[] ObtenFoto_IdCredSustitucion(int idCredSust)
        {
            K_CREDITO_SUSTITUCION datos;
            byte[] existeFoto = null;
            //using (var r = new Repositorio<K_CREDITO_SUSTITUCION>())
            //{
            //    datos =
            //        r.Filtro(
            //            me =>
            //                me.Id_Credito_Sustitucion == idCredSust
            //                ).FirstOrDefault();
            //}
            //    existeFoto = datos != null ? datos.Fotografia_equipo : null;
            return existeFoto;
        }

        public static bool actualizaFoto_IdCredProducto(K_CREDITO_PRODUCTO datos)
        {
            bool resultado;

            using (var r = new Repositorio<K_CREDITO_PRODUCTO>())
            {
                resultado = r.Actualizar(datos);
            }

            return resultado;
        }

        public static bool actualizaFoto_IdCredSustitucion(K_CREDITO_SUSTITUCION datos)
        {
            bool resultado;

            using (var r = new Repositorio<K_CREDITO_SUSTITUCION>())
            {
                resultado = r.Actualizar(datos);
            }

            return resultado;
        }

        public static MOTIVOS_RECHAZOS_CANCELACIONES ObtenEstatusCreditoCancelarRechazar(int idMotivo)
        {
            MOTIVOS_RECHAZOS_CANCELACIONES datosMotivosRechazosCancelaciones;

            using (var r = new Repositorio<MOTIVOS_RECHAZOS_CANCELACIONES>())
            {
                datosMotivosRechazosCancelaciones = r.Filtro(me => me.ID_MOTIVO == idMotivo).FirstOrDefault();
            }

            return datosMotivosRechazosCancelaciones;
        }
    }
}
