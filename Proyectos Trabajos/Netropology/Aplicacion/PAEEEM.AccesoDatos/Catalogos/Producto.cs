using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PAEEEM.Entidades;
using PAEEEM.Entidades.Alta_Equipos;

namespace PAEEEM.AccesoDatos.Catalogos
{
    public class Producto
    {
        private readonly PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();

        #region Metodos de Consulta

        public CAT_PRODUCTO Agregar(CAT_PRODUCTO producto)
        {
            CAT_PRODUCTO catProducto = null;

            using (var r = new Repositorio<CAT_PRODUCTO>())
            {
                catProducto = r.Extraer(c => c.Cve_Producto == producto.Cve_Producto);

                if (catProducto == null)
                    catProducto = r.Agregar(producto);
                else
                    throw new Exception("El producto ya existe en la BD.");
            }

            return catProducto;
        }


        public CAT_PRODUCTO ObtienePorCondicion(Expression<Func<CAT_PRODUCTO, bool>> criterio)
        {
            CAT_PRODUCTO catProducto = null;

            using (var r = new Repositorio<CAT_PRODUCTO>())
            {
                catProducto = r.Extraer(criterio);
            }

            return catProducto;
        }


        public List<CAT_PRODUCTO> ObtieneTodos()
        {
            List<CAT_PRODUCTO> listProductos = null;

            using (var r = new Repositorio<CAT_PRODUCTO>())
            {
                listProductos = r.Filtro(null);
            }

            return listProductos;
        }


        public bool Actualizar(CAT_PRODUCTO producto)
        {
            bool actualiza = false;

            var catProducto = ObtienePorCondicion(c => c.Cve_Producto == producto.Cve_Producto);

            if (catProducto != null)
            {
                using (var r = new Repositorio<CAT_PRODUCTO>())
                {
                    actualiza = r.Actualizar(catProducto);
                }
            }
            else
            {
                throw new Exception("El producto con Id: " + catProducto.Cve_Producto + " no fue encontrado.");
            }

            return actualiza;
        }


        public bool Eliminar(int idProducto)
        {
            bool elimina = false;

            var catProducto = ObtienePorCondicion(c => c.Cve_Producto == idProducto);

            if (catProducto != null)
            {
                using (var r = new Repositorio<CAT_PRODUCTO>())
                {
                    elimina = r.Eliminar(catProducto);
                }
            }
            else
            {
                throw new Exception("No se encontro el producto indicado.");
            }

            return elimina;
        }


        public List<CAT_PRODUCTO> FitrarPorCondicion(Expression<Func<CAT_PRODUCTO, bool>> criterio)
        {
            List<CAT_PRODUCTO> listProducto = null;

            using (var r = new Repositorio<CAT_PRODUCTO>())
            {
                listProducto = r.Filtro(criterio).ToList();
            }

            return listProducto;
        }

        #endregion

        #region Metodos Alta y Baja de Equipos

        //METODOS PARA LLENAR CATALOGOS DE ALTA Y BAJA DE EQUIPOS (DPF)
        public K_PROVEEDOR_PRODUCTO FitrarPorCondicionProvvedorProd(
            Expression<Func<K_PROVEEDOR_PRODUCTO, bool>> criterio)
        {
            K_PROVEEDOR_PRODUCTO producto = null;

            using (var r = new Repositorio<K_PROVEEDOR_PRODUCTO>())
            {
                producto = r.Extraer(criterio);
            }

            return producto;
        }

        public List<EquipoAltaEficiencia> ObtenProductosAltaEficiencia(int idTipoProducto, int cveMarca, int idProveedor, double
             capacidadMaxima, int idTecnologia)
        {
            //List<CAT_PRODUCTO> lstProductosAlta = null;

            //var lstProductosAlta = (from productos in _contexto.CAT_PRODUCTO
            //                        join capacidades in _contexto.CAT_CAPACIDAD_SUSTITUCION
            //                            on productos.Cve_Capacidad_Sust equals capacidades.Cve_Capacidad_Sust
            //                        join provedorProducto in _contexto.K_PROVEEDOR_PRODUCTO
            //                            on productos.Cve_Producto equals provedorProducto.Cve_Producto
            //                        where productos.Ft_Tipo_Producto == idTipoProducto
            //                              && provedorProducto.Id_Proveedor == idProveedor
            //                              && capacidades.No_Capacidad <= capacidadMaxima
            //                        select new EquipoAltaEficiencia
            //                            {
            //                                Cve_Modelo = productos.Cve_Producto,
            //                                Dx_Modelo = productos.Dx_Modelo_Producto,
            //                                No_Capacidad = (double)capacidades.No_Capacidad
            //                            }).ToList();

            var lstProductosAlta = (from productos in _contexto.CAT_PRODUCTO
                                    join capacidades in _contexto.CAT_CAPACIDAD_SUSTITUCION
                                        on productos.Cve_Capacidad_Sust equals capacidades.Cve_Capacidad_Sust
                                    join provedorProducto in _contexto.K_PROVEEDOR_PRODUCTO
                                        on productos.Cve_Producto equals provedorProducto.Cve_Producto
                                    where provedorProducto.Id_Proveedor == idProveedor
                                          && capacidades.No_Capacidad <= capacidadMaxima
                                          && productos.Cve_Marca == cveMarca
                                          && productos.Cve_Tecnologia == idTecnologia
                                          && productos.Cve_Estatus_Producto == 1
                                          //&& productos.Ft_Tipo_Producto == idTipoProducto

                                    //add by @l3x
                                    && provedorProducto.Mt_Precio_Unitario <= productos.Mt_Precio_Max

                                    select new EquipoAltaEficiencia
                                    {
                                        Cve_Modelo = productos.Cve_Producto,
                                        Dx_Modelo = productos.Dx_Modelo_Producto,
                                        CapacidadAquipo = (double)capacidades.No_Capacidad
                                    }).ToList();

            return lstProductosAlta;
        }

        public List<EquipoAltaEficiencia> ObtenProductosSistemasAltaEficiencia(int idProveedor, int cveMarca, int idSistemaArreglo, int idTecnologia)
        {
            var lstProductosAlta = (from productos in _contexto.CAT_PRODUCTO
                                    join capacidades in _contexto.CAT_CAPACIDAD_SUSTITUCION
                                        on productos.Cve_Capacidad_Sust equals capacidades.Cve_Capacidad_Sust
                                    join provedorProducto in _contexto.K_PROVEEDOR_PRODUCTO
                                        on productos.Cve_Producto equals provedorProducto.Cve_Producto
                                    where provedorProducto.Id_Proveedor == idProveedor
                                            && productos.Cve_Tecnologia == idTecnologia
                                          //&& productos.IdSistemaArreglo == idSistemaArreglo
                                          && productos.Cve_Marca == cveMarca
                                          && productos.Cve_Estatus_Producto == 1
                                    //add by @l3x
                                    && provedorProducto.Mt_Precio_Unitario <= productos.Mt_Precio_Max


                                    select new EquipoAltaEficiencia
                                    {
                                        Cve_Modelo = productos.Cve_Producto,
                                        Dx_Modelo = productos.Dx_Modelo_Producto,
                                        Dx_Sistema = capacidades.Dx_Capacidad
                                    }).ToList();

            return lstProductosAlta;
        }

        public List<EquipoAltaEficiencia> ObtenSubestacionesElectricas(int cveMarca, int idProveedor)
        {

            var lstProductosAlta = (from productos in _contexto.CAT_PRODUCTO
                                    join capacidades in _contexto.CAT_CAPACIDAD_SUSTITUCION
                                        on productos.Cve_Capacidad_Sust equals capacidades.Cve_Capacidad_Sust
                                    join provedorProducto in _contexto.K_PROVEEDOR_PRODUCTO
                                        on productos.Cve_Producto equals provedorProducto.Cve_Producto
                                    where provedorProducto.Id_Proveedor == idProveedor
                                          && productos.Cve_Marca == cveMarca
                                          && productos.Cve_Estatus_Producto == 1
                                          //add by @l3x
                                          && provedorProducto.Mt_Precio_Unitario<= productos.Mt_Precio_Max
                                    select new EquipoAltaEficiencia
                                    {
                                        Cve_Modelo = productos.Cve_Producto,
                                        Dx_Modelo = productos.Dx_Modelo_Producto,
                                        CapacidadAquipo = (double)capacidades.No_Capacidad
                                    }).ToList();

            return lstProductosAlta;
        }

        public List<EquipoAltaEficiencia> ObtenBancoCapacitores(int cveMarca, int idProveedor, int idTecnologia, double kvars)
        {

            var lstProductosAlta = (from productos in _contexto.CAT_PRODUCTO
                                    join capacidades in _contexto.CAT_CAPACIDAD_SUSTITUCION
                                        on productos.Cve_Capacidad_Sust equals capacidades.Cve_Capacidad_Sust
                                    join provedorProducto in _contexto.K_PROVEEDOR_PRODUCTO
                                        on productos.Cve_Producto equals provedorProducto.Cve_Producto
                                    where provedorProducto.Id_Proveedor == idProveedor
                                          && productos.Cve_Marca == cveMarca
                                          && productos.Cve_Tecnologia == idTecnologia
                                          && productos.Cve_Estatus_Producto == 1
                                          && capacidades.No_Capacidad <= kvars
                                          //add by @l3x
                                          && provedorProducto.Mt_Precio_Unitario <= productos.Mt_Precio_Max
                                    select new EquipoAltaEficiencia
                                    {
                                        Cve_Modelo = productos.Cve_Producto,
                                        Dx_Modelo = productos.Dx_Modelo_Producto,
                                        CapacidadAquipo = (double)capacidades.No_Capacidad
                                    }).ToList();

            return lstProductosAlta;
        }

        public CAT_PROGRAMA ObtenDatosPrograma(int idPrograma)
        {
            CAT_PROGRAMA programa = null;

            using (var r = new Repositorio<CAT_PROGRAMA>())
            {
                programa = r.Extraer(me => me.ID_Prog_Proy == idPrograma);
            }

            return programa;
        }

        public ABE_HORAS_TECNOLOGIA ObtenHorasOPeracion(int IdSector, int IdEstado, int IdMunicipio)
        {
            ABE_HORAS_TECNOLOGIA horas = null;

            using (var r = new Repositorio<ABE_HORAS_TECNOLOGIA>())
            {
                horas =
                    r.Extraer(me => me.IDSECTOR == IdSector && me.IDESTADO == IdEstado && me.IDMUNICIPIO == IdMunicipio);
            }

            return horas;
        }

        public ABE_VALORES_PRODUCTO ObtenValoresProducto(int IdTipoProducto)
        {
            ABE_VALORES_PRODUCTO valores = null;

            using (var r = new Repositorio<ABE_VALORES_PRODUCTO>())
            {
                valores = r.Extraer(me => me.FT_TIPO_PRODUCTO == IdTipoProducto);
            }

            return valores;
        }

        public ABE_TABLAS_AHORRO ObtenNomEse22(int idTipoProducto, decimal capacidad)
        {
            ABE_TABLAS_AHORRO ahorros = null;

            using (var r = new Repositorio<ABE_TABLAS_AHORRO>())
            {
                ahorros = r.Extraer(me => me.FT_TIPO_PRODUCTO == idTipoProducto && me.CAP_INICIAL <= capacidad && me.CAP_FINAL >= capacidad);
            }

            if(ahorros == null)
            {
                //ahorros = new ABE_TABLAS_AHORRO();
                //ahorros.NOM_022_ESE_2000 = 0.0M;

                using (var r = new Repositorio<ABE_TABLAS_AHORRO>())
                {
                    var lstAhorros = r.Filtro(me => me.FT_TIPO_PRODUCTO == idTipoProducto);
                    var maxCapacidad = lstAhorros.Max(me => me.CAP_FINAL);
                    ahorros = lstAhorros.FirstOrDefault(me => me.CAP_FINAL == maxCapacidad);
                }
            }

            return ahorros;
        }

        public int ObtenTipoProducto(int IdProducto)
        {
            CAT_PRODUCTO producto = null;

            using (var r = new Repositorio<CAT_PRODUCTO>())
            {
                producto = r.Extraer(me => me.Cve_Producto == IdProducto);
            }

            if (producto != null)
                return (int) producto.Ft_Tipo_Producto;
            else
                return 0;
        }

        public ABE_TABLAS_AHORRO ObtenAhorrosAireAcondicionado(int idTecnologia, decimal capacidad)
        {
            ABE_TABLAS_AHORRO ahorros = null;

            using (var r = new Repositorio<ABE_TABLAS_AHORRO>())
            {
                ahorros = r.Extraer(me => me.CVE_TECNOLOGIA == idTecnologia && me.CAP_INICIAL == capacidad);
            }

            return ahorros ?? (ahorros = new ABE_TABLAS_AHORRO());
        }

        public SISTEMA_ARREGLO ObtenSistemaEb(int idSistemaArreglo)
        {
            SISTEMA_ARREGLO sistema = null;

            using (var r = new Repositorio<SISTEMA_ARREGLO>())
            {
                sistema = r.Extraer(me => me.IdSistemaArreglo == idSistemaArreglo);
            }

            return sistema ?? (sistema = new SISTEMA_ARREGLO());
        }

        public decimal ObtenPotenciaEaIl(int idProducto)
        {
            var potencia = (from p in _contexto.CAT_PRODUCTO
                            join c in _contexto.CAT_CAPACIDAD_SUSTITUCION
                                on p.Cve_Capacidad_Sust equals c.Cve_Capacidad_Sust
                            where p.Cve_Producto == idProducto
                            select c).First();

            return Convert.ToDecimal(potencia.No_Capacidad);
        }

        public ABE_TABLAS_AHORRO ObtenPotenciaNominalEa(decimal capacidad, int cveTecnologia)
        {
            ABE_TABLAS_AHORRO ahorro = null;

            using (var r = new Repositorio<ABE_TABLAS_AHORRO>())
            {
                ahorro = r.Extraer(me => me.CAP_INICIAL == capacidad && me.CVE_TECNOLOGIA == cveTecnologia);
            }

            return ahorro ?? (ahorro = new ABE_TABLAS_AHORRO());
        }

        public CAT_TARIFA ObtenTarifa(string tarifa)
        {
            CAT_TARIFA catTarifa = null;

            using (var r = new Repositorio<CAT_TARIFA>())
            {
                catTarifa = r.Extraer(me => me.Dx_Tarifa == tarifa);
            }

            return catTarifa;
        }

        public CLI_Cliente BuscaClientePorCondicion(Expression<Func<CLI_Cliente, bool>> criterio)
        {
            CLI_Cliente cliente = null;

            using (var r = new Repositorio<CLI_Cliente>())
            {
                cliente = r.Extraer(criterio);
            }

            return cliente;
        }

        #endregion
    }
}
