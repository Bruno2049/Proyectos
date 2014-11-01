using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using PAEEEM.AccesoDatos.Operacion_Datos;
using PAEEEM.Entidades;
using PAEEEM.Entidades.AdminUsuarios;
using PAEEEM.Entidades.Alta_Equipos;
using PAEEEM.Entidades.Alta_Solicitud;

namespace PAEEEM.AccesoDatos.SolicitudCredito
{
    public class CreCredito
    {
        private readonly PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();

        #region Metodos de Insercion 

        public CRE_Credito InsertaCredito(CRE_Credito credito)
        {
            CRE_Credito newCredito = null;

            using (var r = new Repositorio<CRE_Credito>())
            {
                newCredito = r.Agregar(credito);
            }

            return newCredito;
        }

        public bool BuscaRpu(string noRpu)
        {
            var estatusActivos = new List<byte> {1, 2, 3, 4};
            var existe = false;
            using (var r = new Repositorio<CRE_Credito>())
            {
                var rpuExiste = r.Filtro(d => d.RPU == noRpu && estatusActivos.Contains((byte) d.Cve_Estatus_Credito));
                if (rpuExiste.Count > 0)
                    existe = true;
            }
            return existe;
        }

        public bool ActualizaCredito(CRE_Credito credito)
        {
            bool actualizo = false;

            using (var r = new Repositorio<CRE_Credito>())
            {
                actualizo = r.Actualizar(credito);
            }

            return actualizo;
        }

        public static CRE_Credito ObtieneCredito(string noCredito)
        {
            CRE_Credito cred;

            using (var r = new Repositorio<CRE_Credito>())
            {
                cred = r.Filtro(cre => cre.No_Credito == noCredito).FirstOrDefault();
            }

            return cred;
        }

        public K_CREDITO_PRODUCTO InsertaProductoAlta(K_CREDITO_PRODUCTO producto)
        {
            K_CREDITO_PRODUCTO newProducto = null;

            using (var r = new Repositorio<K_CREDITO_PRODUCTO>())
            {
                newProducto = r.Agregar(producto);
            }

            return newProducto;
        }

        public K_CREDITO_SUSTITUCION InsertaProductoBaja(K_CREDITO_SUSTITUCION productoBaja)
        {
            K_CREDITO_SUSTITUCION newProductoBaja = null;

            using (var r = new Repositorio<K_CREDITO_SUSTITUCION>())
            {
                newProductoBaja = r.Agregar(productoBaja);
            }

            return newProductoBaja;
        }

        public CRE_CREDITO_EQUIPOS_BAJA InsertaCreProductoBaja(CRE_CREDITO_EQUIPOS_BAJA productoBaja)
        {
            CRE_CREDITO_EQUIPOS_BAJA newProductoBaja = null;

            using (var r = new Repositorio<CRE_CREDITO_EQUIPOS_BAJA>())
            {
                newProductoBaja = r.Agregar(productoBaja);
            }

            return newProductoBaja;
        }

        public K_CREDITO_AMORTIZACION InsertaAmortizacionCredito(K_CREDITO_AMORTIZACION creditoAmortizacion)
        {
            K_CREDITO_AMORTIZACION newCreditoAmortizacion = null;

            using (var r = new Repositorio<K_CREDITO_AMORTIZACION>())
            {
                newCreditoAmortizacion = r.Agregar(creditoAmortizacion);
            }

            return newCreditoAmortizacion;
        }

        public K_CREDITO_COSTO InsertaCreditoCosto(K_CREDITO_COSTO creditoCosto)
        {
            K_CREDITO_COSTO newCreditoCosto = null;

            using (var r = new Repositorio<K_CREDITO_COSTO>())
            {
                newCreditoCosto = r.Agregar(creditoCosto);
            }

            return newCreditoCosto;
        }

        public K_CREDITO_DESCUENTO InsertaCreditoDescuento(K_CREDITO_DESCUENTO creditoDescuento)
        {
            K_CREDITO_DESCUENTO newCreditoDescuento = null;

            using (var r = new Repositorio<K_CREDITO_DESCUENTO>())
            {
                newCreditoDescuento = r.Agregar(creditoDescuento);
            }

            return newCreditoDescuento;
        }

        public CRE_CAPACIDAD_PAGO InsertaCapacidadPago(CRE_CAPACIDAD_PAGO capacidadPago)
        {
            CRE_CAPACIDAD_PAGO newCapacidadPago = null;

            using (var r = new Repositorio<CRE_CAPACIDAD_PAGO>())
            {
                newCapacidadPago = r.Agregar(capacidadPago);
            }

            return newCapacidadPago;
        }

        public CRE_Facturacion InsertaFacturacion(CRE_Facturacion facturacion)
        {
            CRE_Facturacion newFacturacion = null;

            using (var r = new Repositorio<CRE_Facturacion>())
            {
                newFacturacion = r.Agregar(facturacion);
            }

            return newFacturacion;
        }

        public CRE_FACTURACION_DETALLE InsertaFacturaciondetalle(CRE_FACTURACION_DETALLE facturacionDetalle)
        {
            CRE_FACTURACION_DETALLE newFacturciondetalle = null;

            using (var r = new Repositorio<CRE_FACTURACION_DETALLE>())
            {
                newFacturciondetalle = r.Agregar(facturacionDetalle);
            }

            return newFacturciondetalle;
        }

        public CRE_HISTORICO_CONSUMO InsertaHistoricoConsumo(CRE_HISTORICO_CONSUMO historicoConsumo)
        {
            CRE_HISTORICO_CONSUMO newHistoricoConsumo = null;

            using (var r = new Repositorio<CRE_HISTORICO_CONSUMO>())
            {
                newHistoricoConsumo = r.Agregar(historicoConsumo);
            }

            return newHistoricoConsumo;
        }

        public CRE_PSR InsertaPSR(CRE_PSR psr)
        {
            CRE_PSR newPsr = null;

            using (var r = new Repositorio<CRE_PSR>())
            {
                newPsr = r.Agregar(psr);
            }

            return newPsr;
        }

        public CRE_GRUPOS_TECNOLOGIA InsertaGrupoTecnologia(CRE_GRUPOS_TECNOLOGIA grupo)
        {
            CRE_GRUPOS_TECNOLOGIA newGrupo = null;

            using (var r = new Repositorio<CRE_GRUPOS_TECNOLOGIA>())
            {
                newGrupo = r.Agregar(grupo);
            }

            return newGrupo;
        }

        public void InsertaFoto(CRE_FOTOS foto)
        {
            using (var r = new Repositorio<CRE_FOTOS>())
            {
                r.Agregar(foto);
            }
        }

        public void InsertaDatosCayd(K_PRODUCTO_CHARACTERS productoCharacters)
        {
            using (var r = new Repositorio<K_PRODUCTO_CHARACTERS>())
            {
                r.Agregar(productoCharacters);
            }
        }

        public void InsertaHorarioOperacion(CLI_HORARIOS_OPERACION horarioOperacion)
        {
            using (var r = new Repositorio<CLI_HORARIOS_OPERACION>())
            {
                r.Agregar(horarioOperacion);
            }
        }

        public void InsertaOperacionTotal(H_OPERACION_TOTAL operacionTotal)
        {
            using (var r = new Repositorio<H_OPERACION_TOTAL>())
            {
                r.Agregar(operacionTotal);
            }
        }

        #endregion

        #region Metodos de Consulta- Alta y Baja de Equipos

        public CRE_Credito ObtenCreditoPorCondicion(Expression<Func<CRE_Credito, bool>> criterio)
        {
            CRE_Credito creCredito = null;

            using (var r = new Repositorio<CRE_Credito>())
            {
                creCredito = r.Extraer(criterio);
            }

            return creCredito;
        }

        public List<CRE_Credito> ObtenListaCreditos(Expression<Func<CRE_Credito, bool>> criterio)
        {
            List<CRE_Credito> lstCreditos = null;

            using (var r = new Repositorio<CRE_Credito>())
            {
                lstCreditos = r.Filtro(criterio);
            }

            return lstCreditos;
        }

        public List<EquipoBajaEficiencia> ObtenEquiposBajaEficienciaCredito(string idCredito)
        {
            var resultado = (from creditoSustitucion in _contexto.CRE_CREDITO_EQUIPOS_BAJA
                             join t in _contexto.CAT_TECNOLOGIA
                                 on creditoSustitucion.Cve_Tecnologia
                                 equals t.Cve_Tecnologia
                             where creditoSustitucion.No_Credito == idCredito
                             select new EquipoBajaEficiencia
                                 {
                                     ID = creditoSustitucion.Id_Credito_Sustitucion,
                                     Cve_Grupo = (int) creditoSustitucion.IdGrupo,
                                     Dx_Grupo = creditoSustitucion.Grupo,
                                     Cve_Tecnologia = creditoSustitucion.Cve_Tecnologia,
                                     Dx_Tecnologia = t.Dx_Nombre_General,
                                     Dx_Tipo_Producto = creditoSustitucion.Dx_Modelo_Producto,
                                     Dx_Consumo = creditoSustitucion.CapacidadSistema,
                                     Dx_Unidad = creditoSustitucion.Unidad,
                                     Cantidad = (int) creditoSustitucion.No_Unidades
                                 }
                            ).ToList();

            return resultado;

        }

        public List<EquipoAltaEficiencia> ObtenEquiposAltaEficienciaCredito(string idCredito)
        {
            var resultado = (from creditoProducto in _contexto.K_CREDITO_PRODUCTO
                             join c in _contexto.CRE_Credito
                                 on creditoProducto.No_Credito
                                 equals c.No_Credito
                             join d in _contexto.K_PROVEEDOR_PRODUCTO
                                 on c.Id_Proveedor
                                 equals d.Id_Proveedor
                             join p in _contexto.CAT_PRODUCTO
                                 on creditoProducto.Cve_Producto
                                 equals p.Cve_Producto
                             join t in _contexto.CAT_TECNOLOGIA
                                 on p.Cve_Tecnologia
                                 equals t.Cve_Tecnologia
                             join m in _contexto.CAT_MARCA
                                 on p.Cve_Marca
                                 equals m.Cve_Marca
                             where
                                 creditoProducto.No_Credito == idCredito &&
                                 creditoProducto.Cve_Producto == d.Cve_Producto
                                 ////Add By @l3x////
                                 &&d.Mt_Precio_Unitario<=p.Mt_Precio_Max
                                 ////<---------////
                             select new EquipoAltaEficiencia
                                 {
                                     ID = creditoProducto.ID_CREDITO_PRODUCTO,
                                     ID_Baja = creditoProducto.ID_CREDITO_PRODUCTO,
                                     Cve_Grupo = creditoProducto.IdGrupo != null ? (int)creditoProducto.IdGrupo : 1,
                                     Dx_Grupo = creditoProducto.Grupo,
                                     Cve_Marca = p.Cve_Marca,
                                     Dx_Marca = m.Dx_Marca,
                                     Cve_Modelo = p.Cve_Producto,
                                     Dx_Modelo = p.Dx_Modelo_Producto,
                                     Dx_Sistema = creditoProducto.CapacidadSistema,
                                     Cantidad = (int) creditoProducto.No_Cantidad,
                                     Precio_Unitario = (decimal) creditoProducto.Mt_Precio_Unitario_Sin_IVA,
                                     Precio_Distribuidor = d.Mt_Precio_Unitario,
                                     Importe_Total_Sin_IVA =
                                         (decimal) creditoProducto.Mt_Precio_Unitario_Sin_IVA*
                                         (int) creditoProducto.No_Cantidad, //(decimal)creditoProducto.Mt_Total,
                                     Gasto_Instalacion = (decimal) creditoProducto.Mt_Gastos_Instalacion_Mano_Obra,
                                     MontoIncentivo =
                                         creditoProducto.Incentivo != null ? (decimal) creditoProducto.Incentivo : 0.0M
                                 }
                            ).ToList();

            return resultado;
        }

        public decimal ValidaMontoMaximoCredito(string RFC)
        {
            try
            {
                var montocreditos = (from cr in _contexto.CRE_Credito
                                     join cl in _contexto.CLI_Cliente
                                         on cr.IdCliente equals cl.IdCliente
                                     where cl.RFC == RFC
                                     select new Credito
                                         {
                                             NoCredito = cr.No_Credito,
                                             MontoSolicitado = (decimal) cr.Monto_Solicitado,
                                             Estatus = cr.Cve_Estatus_Credito
                                         }
                                    ).ToList()
                                     .FindAll(me => new byte?[] {1, 2, 3, 4, 6, 8}.Contains(me.Estatus))
                                     .Sum(me => me.MontoSolicitado);

                return montocreditos;
            }
            catch
            {
                return 0.0M;
            }

        }

        public decimal ValidaMontoMaximoCreditoEdit(string RFC, string idCredito)
        {
            try
            {
                var montocreditos = (from cr in _contexto.CRE_Credito
                                     join cl in _contexto.CLI_Cliente
                                         on cr.IdCliente equals cl.IdCliente
                                     where cl.RFC == RFC
                                     && cr.No_Credito != idCredito
                                     select new Credito
                                     {
                                         NoCredito = cr.No_Credito,
                                         MontoSolicitado = (decimal)cr.Monto_Solicitado,
                                         Estatus = cr.Cve_Estatus_Credito
                                     }
                           ).ToList().FindAll(me => new byte?[] { 1, 2, 3, 4, 6, 8 }.Contains(me.Estatus)).Sum(me => me.MontoSolicitado);

                return montocreditos;
            }
            catch
            {
                return 0.0M;
            }

        }

        public string ObtenTrama(string idCredito)
        {
            var trama = (from cr in _contexto.CRE_Credito
                         join res in _contexto.ResponseData
                             on cr.Id_Trama equals res.Id_Trama
                         where cr.No_Credito == idCredito
                         select new Credito
                             {
                                 NoCredito = cr.No_Credito,
                                 Trama = res.Trama
                             }
                        ).ToList().FirstOrDefault().Trama;

            return trama;
        }

        public PAEEEM.Entidades.ResponseData ObtenResponseData(string RPU, DateTime fechaConsulta)
        {
            PAEEEM.Entidades.ResponseData responseData = null;
            var fechaLimite = fechaConsulta.AddDays(1);

            using (var r = new Repositorio<PAEEEM.Entidades.ResponseData>())
            {
                var lstResponseData = r.Filtro(me => me.ServiceCode == RPU && me.FechaConsulta >= fechaConsulta.Date && me.FechaConsulta <= fechaLimite);

                if (lstResponseData != null && lstResponseData.Count > 0)
                {
                    responseData = lstResponseData.LastOrDefault();
                }
            }

            return responseData;
        }

        public string ObtenCN(int idTrama)
        {
            string cnTrama = "";

            using (var r = new Repositorio<PAEEEM.Entidades.ResponseData>())
            {
                var resultado = r.Extraer(me => me.Id_Trama == idTrama);

                if (resultado != null)
                    cnTrama = resultado.CN.Substring(4, 3);
            }

            return cnTrama;
        }

        public decimal ObtenMinimoCapacidadBC()
        {
            List<CAT_CAPACIDAD_SUSTITUCION> lstCapacidades = null;
            decimal capacidadMinima = 0.0M;

            using (var r = new Repositorio<CAT_CAPACIDAD_SUSTITUCION>())
            {
                lstCapacidades = r.Filtro(me => me.Cve_Tecnologia == 7);
            }

            if (lstCapacidades != null && lstCapacidades.Count > 0)
            {
                capacidadMinima = Convert.ToDecimal(lstCapacidades.Min(me => me.No_Capacidad));
            }

            return capacidadMinima;
        }


        public decimal ObtenMontoChatarrizacion(string idCredito)
        {
            var montoChatarrizacion = 0.0M;

            using (var r = new Repositorio<K_CREDITO_COSTO>())
            {
                var costo = r.Extraer(me => me.No_Credito == idCredito);

                if (costo != null)
                    montoChatarrizacion = (decimal) costo.Mt_Costo;
            }

            return montoChatarrizacion;
        }

        public decimal ObtenMontoDescuento(string idCredito)
        {
            var montoDescuento = 0.0M;

            using (var r = new Repositorio<K_CREDITO_DESCUENTO>())
            {
                var descuento = r.Extraer(me => me.No_Credito == idCredito);

                if (descuento != null)
                    montoDescuento = (decimal) descuento.Mt_Descuento;
            }

            return montoDescuento;
        }

        public List<K_CREDITO_PRODUCTO> ObtenCreditoProductos(string idCredito)
        {
            List<K_CREDITO_PRODUCTO> lstProductos = null;

            using (var r = new Repositorio<K_CREDITO_PRODUCTO>())
            {
                lstProductos = r.Filtro(me => me.No_Credito == idCredito);
            }

            return lstProductos;
        }

        public List<K_CREDITO_SUSTITUCION> ObtenCreditoSustitucion(string idCredito)
        {
            List<K_CREDITO_SUSTITUCION> lstSustitucion = null;

            using (var r = new Repositorio<K_CREDITO_SUSTITUCION>())
            {
                lstSustitucion = r.Filtro(me => me.No_Credito == idCredito);
            }

            return lstSustitucion;
        }

        public K_CREDITO_COSTO ObtenCreditoCosto(string idCredito)
        {
            K_CREDITO_COSTO creditoCosto = null;

            using (var r = new Repositorio<K_CREDITO_COSTO>())
            {
                creditoCosto = r.Extraer(me => me.No_Credito == idCredito);
            }

            return creditoCosto;
        }

        public K_CREDITO_DESCUENTO ObtenCreditoDescuento(string idCredito)
        {
            K_CREDITO_DESCUENTO creditoDescuento = null;

            using (var r = new Repositorio<K_CREDITO_DESCUENTO>())
            {
                creditoDescuento = r.Extraer(me => me.No_Credito == idCredito);
            }

            return creditoDescuento;
        }

        public List<K_CREDITO_AMORTIZACION> ObtenAmortizacionesCredito(string idCredito)
        {
            List<K_CREDITO_AMORTIZACION> lstAmortizaciones = null;

            using (var r = new Repositorio<K_CREDITO_AMORTIZACION>())
            {
                lstAmortizaciones = r.Filtro(me => me.No_Credito == idCredito);
            }

            return lstAmortizaciones;
        }

        public List<CRE_CREDITO_EQUIPOS_BAJA> ObtenEquiposBajaCredito(string idCredito)
        {
            List<CRE_CREDITO_EQUIPOS_BAJA> lstEquiposBaja = null;

            using (var r = new Repositorio<CRE_CREDITO_EQUIPOS_BAJA>())
            {
                lstEquiposBaja = r.Filtro(me => me.No_Credito == idCredito);
            }

            return lstEquiposBaja;
        }

        public CRE_CAPACIDAD_PAGO ObtenCapacidadPago(string idCredito)
        {
            CRE_CAPACIDAD_PAGO capacidadPago = null;

            using (var r = new Repositorio<CRE_CAPACIDAD_PAGO>())
            {
                capacidadPago = r.Extraer(me => me.No_Credito == idCredito);
            }

            return capacidadPago;
        }

        public CRE_PSR ObtenPSR(string idCredito)
        {
            CRE_PSR psr = null;

            using (var r = new Repositorio<CRE_PSR>())
            {
                psr = r.Extraer(me => me.No_Credito == idCredito);
            }

            return psr;
        }

        public CRE_Facturacion ObtenFacturacion(string idCredito, byte tipoFacturacion)
        {
            CRE_Facturacion facturacion = null;

            using (var r = new Repositorio<CRE_Facturacion>())
            {
                facturacion = r.Extraer(me => me.No_Credito == idCredito && me.IdTipoFacturacion == tipoFacturacion);
            }

            return facturacion;
        }

        public List<CRE_FACTURACION_DETALLE> ObtenFacturacionDetalle(int idFactura)
        {
            List<CRE_FACTURACION_DETALLE> lstFacturaciondetalle = null;

            using (var r = new Repositorio<CRE_FACTURACION_DETALLE>())
            {
                lstFacturaciondetalle = r.Filtro(me => me.IdFactura == idFactura);
            }

            return lstFacturaciondetalle;
        }

        public List<CRE_HISTORICO_CONSUMO> ObtenHistoricoConsumo(string idCredito)
        {
            List<CRE_HISTORICO_CONSUMO> lstHistoricoConsumo = null;

            using (var r = new Repositorio<CRE_HISTORICO_CONSUMO>())
            {
                lstHistoricoConsumo = r.Filtro(me => me.No_Credito == idCredito);
            }

            return lstHistoricoConsumo;
        }

        public List<CRE_GRUPOS_TECNOLOGIA> ObtenGruposTecnologia(string idCredito)
        {
            List<CRE_GRUPOS_TECNOLOGIA> lstGrupos = null;

            using (var r = new Repositorio<CRE_GRUPOS_TECNOLOGIA>())
            {
                lstGrupos = r.Filtro(me => me.No_Credito == idCredito);
            }

            return lstGrupos;
        }

        public List<CAT_ACCIONES> ObtenAcciones(int estatus, int idRol)
        {
            var resultado = (from acciones in _contexto.CAT_ACCIONES
                             join rol in _contexto.ACCIONES_ROL
                                on acciones.ID_Acciones equals  rol.ID_Acciones
                             join accEstatus in _contexto.ACCION_ESTATUS
                                on acciones.ID_Acciones equals accEstatus.ID_Acciones
                            where rol.Id_Rol == idRol 
                            && accEstatus.Cve_Estatus_Credito == estatus
                            select acciones).Distinct().ToList<CAT_ACCIONES>();
            return resultado;
        }

        public List<AccionUsuario> ObtenAccionesUsuario(int estatus, int idUsuario)
        {
            var resultado = (from acciones in _contexto.CAT_ACCIONES
                             join usuario in _contexto.ACCIONES_USUARIO
                                on acciones.ID_Acciones equals usuario.ID_Acciones
                             join accEstatus in _contexto.ACCION_ESTATUS
                                on acciones.ID_Acciones equals accEstatus.ID_Acciones
                             where usuario.Id_Usuario == idUsuario
                             && accEstatus.Cve_Estatus_Credito == estatus
                             select new AccionUsuario
                                 {
                                     IdAccion = acciones.ID_Acciones,
                                     NombreAccion = acciones.Nombre_Accion,
                                     EstatusAccion = (bool)usuario.Estatus
                                 }).ToList();
            return resultado;
        }

        public List<CRE_FOTOS> ObtenFotosCredito(string idCredito)
        {
            List<CRE_FOTOS> lstFotos = null;

            using (var r = new Repositorio<CRE_FOTOS>())
            {
                lstFotos = r.Filtro(me => me.No_Credito == idCredito);
            }

            return lstFotos;
        }

        public K_PRODUCTO_CHARACTERS ObtenDatosCayD(int idCreditoSust)
        {
            K_PRODUCTO_CHARACTERS kProductoCharacters = null;

            using (var r = new Repositorio<K_PRODUCTO_CHARACTERS>())
            {
                kProductoCharacters = r.Extraer(me => me.Id_Credito_Sustitucion == idCreditoSust);
            }

            return kProductoCharacters;
        }

        public List<CLI_HORARIOS_OPERACION> ObtenHorariosOperacionProd(string idCredito, int idCreditoProducto)
        {
            List<CLI_HORARIOS_OPERACION> lstHorariosOperacion = null;

            using (var r = new Repositorio<CLI_HORARIOS_OPERACION>())
            {
                lstHorariosOperacion = r.Filtro(me => me.No_Credito == idCredito && me.ID_CREDITO_PRODUCTO == idCreditoProducto);
            }

            return lstHorariosOperacion;
        }

        public List<H_OPERACION_TOTAL> ObtenOperacionTotalProd(string idCredito, int idCreditoProducto)
        {
            List<H_OPERACION_TOTAL> lstOperacionTotal = null;

            using (var r = new Repositorio<H_OPERACION_TOTAL>())
            {
                lstOperacionTotal = r.Filtro(me => me.No_Credito == idCredito && me.ID_CREDITO_PRODUCTO == idCreditoProducto);
            }

            return lstOperacionTotal;
        }

        public List<CLI_HORARIOS_OPERACION> ObtenHorariosOperacionSust(string idCredito, int idCreditoSust)
        {
            List<CLI_HORARIOS_OPERACION> lstHorariosOperacion = null;

            using (var r = new Repositorio<CLI_HORARIOS_OPERACION>())
            {
                lstHorariosOperacion = r.Filtro(me => me.No_Credito == idCredito && me.Id_Credito_Sustitucion == idCreditoSust);
            }

            return lstHorariosOperacion;
        }

        public List<CLI_HORARIOS_OPERACION> ObtenHorariosOperacionNegocio(string idCredito, int idTipoHorario)
        {
            List<CLI_HORARIOS_OPERACION> lstHorariosOperacion = null;

            using (var r = new Repositorio<CLI_HORARIOS_OPERACION>())
            {
                lstHorariosOperacion = r.Filtro(me => me.No_Credito == idCredito && me.IDTIPOHORARIO == idTipoHorario);
            }

            return lstHorariosOperacion;
        }

        public List<H_OPERACION_TOTAL> ObtenOperacionTotalSust(string idCredito, int idCreditoSust)
        {
            List<H_OPERACION_TOTAL> lstOperacionTotal = null;

            using (var r = new Repositorio<H_OPERACION_TOTAL>())
            {
                lstOperacionTotal = r.Filtro(me => me.No_Credito == idCredito && me.Id_Credito_Sustitucion == idCreditoSust);
            }

            return lstOperacionTotal;
        }

        public List<H_OPERACION_TOTAL> ObtenOperacionTotalNegocio(string idCredito, int idTipoHorario)
        {
            List<H_OPERACION_TOTAL> lstOperacionTotal = null;

            using (var r = new Repositorio<H_OPERACION_TOTAL>())
            {
                lstOperacionTotal = r.Filtro(me => me.No_Credito == idCredito && me.IDTIPOHORARIO == idTipoHorario);
            }

            return lstOperacionTotal;
        }

        public List<CRE_FOTOS> ObtenFotosProducto(string idCredito, int idCreditoProducto)
        {
            List<CRE_FOTOS> lsFotos = null;

            using (var r = new Repositorio<CRE_FOTOS>())
            {
                lsFotos = r.Filtro(me => me.No_Credito == idCredito && me.idCreditoProducto == idCreditoProducto);
            }

            return lsFotos;
        }

        public List<CRE_FOTOS> ObtenFotosSustitucion(string idCredito, int idCreditoSustitucion)
        {
            List<CRE_FOTOS> lsFotos = null;

            using (var r = new Repositorio<CRE_FOTOS>())
            {
                lsFotos = r.Filtro(me => me.No_Credito == idCredito && me.IdCreditoSustitucion == idCreditoSustitucion);
            }

            return lsFotos;
        }

        public List<CRE_FOTOS> ObtenFotosNegocio(string idCredito, int idTipoFoto)
        {
            List<CRE_FOTOS> lsFotos = null;

            using (var r = new Repositorio<CRE_FOTOS>())
            {
                lsFotos = r.Filtro(me => me.No_Credito == idCredito && me.idTipoFoto == idTipoFoto);
            }

            return lsFotos;
        }

        public K_CREDITO_SUSTITUCION_EXTENSION ObtenSustitucionExtension(int idCreditoSustitucion)
        {
            K_CREDITO_SUSTITUCION_EXTENSION sustitucionExtension = null;

            using (var r = new Repositorio<K_CREDITO_SUSTITUCION_EXTENSION>())
            {
                sustitucionExtension = r.Extraer(me => me.Id_Credito_Sustitucion == idCreditoSustitucion);
            }

            return sustitucionExtension;
        }

        public K_RECUPERACION_PRODUCTO ObtenRecuperacionProducto(int idCreditoSustitucion)
        {
            K_RECUPERACION_PRODUCTO recuperacionProducto = null;

            using (var r = new Repositorio<K_RECUPERACION_PRODUCTO>())
            {
                recuperacionProducto = r.Extraer(me => me.Id_Credito_Sustitucion == idCreditoSustitucion);
            }

            return recuperacionProducto;
        }

    #endregion

        #region Metodos de Borrado

        public void EliminaProductosBaja(string idCredito)
        {
            using (var r = new Repositorio<K_CREDITO_SUSTITUCION>())
            {
                var lstProductosBaja = r.Filtro(me => me.No_Credito == idCredito);

                if (lstProductosBaja != null)
                {
                    foreach (var kCreditoSustitucion in lstProductosBaja)
                    {
                        r.Eliminar(kCreditoSustitucion);
                    }
                }
            }
        }

        public void EliminaCreProductosBaja(string idCredito)
        {
            using (var r = new Repositorio<CRE_CREDITO_EQUIPOS_BAJA>())
            {
                var lstProductosBaja = r.Filtro(me => me.No_Credito == idCredito);

                if (lstProductosBaja != null)
                {
                    foreach (var kCreditoSustitucion in lstProductosBaja)
                    {
                        r.Eliminar(kCreditoSustitucion);
                    }
                }
            }
        }

        public void EliminaProductosAlta(string idCredito)
        {
            using (var r = new Repositorio<K_CREDITO_PRODUCTO>())
            {
                var lstProdutosAlta = r.Filtro(me => me.No_Credito == idCredito);

                if (lstProdutosAlta != null)
                {
                    foreach (var kCreditoProducto in lstProdutosAlta)
                    {
                        r.Eliminar(kCreditoProducto);
                    }
                }
            }
        }

        public void EliminaAmortizacioncredito(string idCredito)
        {
            using (var r = new Repositorio<K_CREDITO_AMORTIZACION>())
            {
                var lstAmortizaciones = r.Filtro(me => me.No_Credito == idCredito);

                if (lstAmortizaciones != null)
                {
                    foreach (var kCreditoAmortizacion in lstAmortizaciones)
                    {
                        r.Eliminar(kCreditoAmortizacion);
                    }
                }
            }
        }

        public void EliminaCreditoCosto(string idCredito)
        {
            using (var r = new Repositorio<K_CREDITO_COSTO>())
            {
                var creditoCosto = r.Extraer(me => me.No_Credito == idCredito);

                if (creditoCosto != null)
                    r.Eliminar(creditoCosto);
            }
        }

        public void EliminaCreditoDescuento(string idCredito)
        {
            using (var r = new Repositorio<K_CREDITO_DESCUENTO>())
            {
                var creditoDescuento = r.Extraer(me => me.No_Credito == idCredito);

                if (creditoDescuento != null)
                    r.Eliminar(creditoDescuento);
            }
        }

        public void EliminaFacturacion(string idCredito)
        {
            using (var r = new Repositorio<CRE_Facturacion>())
            {
                var lstFacturacion = r.Filtro(me => me.No_Credito == idCredito);

                if (lstFacturacion != null)
                {
                    foreach (var creFacturacion in lstFacturacion)
                    {
                        EliminaFacturacionDetalle(creFacturacion.IdFactura);
                        r.Eliminar(creFacturacion);
                    }
                }
            }
        }

        public void EliminaFacturacionDetalle(int idFacturacion)
        {
            using (var r = new Repositorio<CRE_FACTURACION_DETALLE>())
            {
                var lstFacturacionDetalle = r.Filtro(me => me.IdFactura == idFacturacion);

                if (lstFacturacionDetalle != null)
                {
                    foreach (var creFacturacionDetalle in lstFacturacionDetalle)
                    {
                        r.Eliminar(creFacturacionDetalle);
                    }
                }
            }
        }

        public void EliminaHistoricoConsumos(string idCredito)
        {
            using (var r = new Repositorio<CRE_HISTORICO_CONSUMO>())
            {
                var lstHistoricoConsumos = r.Filtro(me => me.No_Credito == idCredito);

                if (lstHistoricoConsumos != null)
                {
                    foreach (var lstHistoricoConsumo in lstHistoricoConsumos)
                    {
                        r.Eliminar(lstHistoricoConsumo);
                    }
                }
            }
        }

        public void EliminaCapacidadPago(string idCredito)
        {
            using (var r = new Repositorio<CRE_CAPACIDAD_PAGO>())
            {
                var capacidadPago = r.Extraer(me => me.No_Credito == idCredito);

                if (capacidadPago != null)
                    r.Eliminar(capacidadPago);
            }
        }

        public void EliminaPsr(string idCredito)
        {
            using (var r = new Repositorio<CRE_PSR>())
            {
                var psr = r.Extraer(me => me.No_Credito == idCredito);

                if (psr != null)
                    r.Eliminar(psr);
            }
        }

        public void EliminaHorariosOperacion(string idCredito, int tipoHorario)
        {
            using (var r = new Repositorio<CLI_HORARIOS_OPERACION>())
            {
                var lstHorarios = r.Filtro(me => me.No_Credito == idCredito && me.IDTIPOHORARIO == tipoHorario);

                if (lstHorarios != null)
                {
                    foreach (var cliHorariosOperacion in lstHorarios)
                    {
                        r.Eliminar(cliHorariosOperacion);
                    }
                }
            }
        }

        public void EliminaOperacionTotal(string idCredito, int tipoHorario)
        {
            using (var r = new Repositorio<H_OPERACION_TOTAL>())
            {
                var lstOperacionTotal = r.Filtro(me => me.No_Credito == idCredito && me.IDTIPOHORARIO == tipoHorario);

                if (lstOperacionTotal != null)
                {
                    foreach (var hOperacionTotal in lstOperacionTotal)
                    {
                        r.Eliminar(hOperacionTotal);
                    }
                }
            }
        }

        public void EliminaGruposTecnologia(string idCredito)
        {
            using (var r = new Repositorio<CRE_GRUPOS_TECNOLOGIA>())
            {
                var lstGrupos = r.Filtro(me => me.No_Credito == idCredito);

                if (lstGrupos != null)
                {
                    foreach (var creGruposTecnologia in lstGrupos)
                    {
                        r.Eliminar(creGruposTecnologia);
                    }
                }
            }
        }

        public void EliminaFotos(string idCredito)
        {
            using (var r = new Repositorio<CRE_FOTOS>())
            {
                var lstFotos = r.Filtro(me => me.No_Credito == idCredito);

                if (lstFotos != null)
                {
                    foreach (var creFotos in lstFotos)
                    {
                        r.Eliminar(creFotos);
                    }
                }
            }
        }

        public bool EliminaCredito(CRE_Credito credito)
        {
            bool borro = false;

            using (var r = new Repositorio<CRE_Credito>())
            {
                borro = r.Eliminar(credito);
            }

            return borro;
        }
        
        public CAT_PROGRAMA ObtenPrograma(int idPrograma)
        {
            CAT_PROGRAMA programa = null;

            using (var r = new Repositorio<CAT_PROGRAMA>())
            {
                programa = r.Extraer(me => me.ID_Prog_Proy == idPrograma);
            }

            return programa;
        }

        #endregion

        #region Tabla de Amortizacion Temp

        public CRE_PRESUPUESTO_INVERSION InsertaPresupuestoTemp(CRE_PRESUPUESTO_INVERSION presupuesto)
        {
            CRE_PRESUPUESTO_INVERSION newPresupuesto = null;

            using (var r = new Repositorio<CRE_PRESUPUESTO_INVERSION>())
            {
                newPresupuesto = r.Agregar(presupuesto);
            }

            return newPresupuesto;
        }

        public CRE_CREDITO_AMORTIZACION InsertaAmortizacionCreditoTemp(CRE_CREDITO_AMORTIZACION creditoAmortizacion)
        {
            CRE_CREDITO_AMORTIZACION newCreditoAmortizacion = null;

            using (var r = new Repositorio<CRE_CREDITO_AMORTIZACION>())
            {
                newCreditoAmortizacion = r.Agregar(creditoAmortizacion);
            }

            return newCreditoAmortizacion;
        }

        public void EliminaAmortizacioncreditoTemp(string rpu)
        {
            using (var r = new Repositorio<CRE_CREDITO_AMORTIZACION>())
            {
                var lstAmortizaciones = r.Filtro(me => me.RPU == rpu);

                if (lstAmortizaciones != null)
                {
                    foreach (var kCreditoAmortizacion in lstAmortizaciones)
                    {
                        r.Eliminar(kCreditoAmortizacion);
                    }
                }
            }
        }

        public void EliminaPresupuestoTemp(string rpu)
        {
            using (var r = new Repositorio<CRE_PRESUPUESTO_INVERSION>())
            {
                var presupuesto = r.Extraer(me => me.RPU == rpu);

                if (presupuesto != null)
                    r.Eliminar(presupuesto);
            }
        }

        public List<CAT_ESTATUS_CREDITO> ObtenEstatusCreditos()
        {
            List<CAT_ESTATUS_CREDITO> lstEstatus = null;

            using (var r = new Repositorio<CAT_ESTATUS_CREDITO>())
            {
                lstEstatus = r.Filtro(me => me.Cve_Estatus_Credito > 0);
            }

            foreach (var catEstatusCredito in lstEstatus)
            {
                catEstatusCredito.Dt_Fecha_Estatus_Credito = null;
            }

            return lstEstatus;
        }

        #endregion

        #region Metodos de Actualizacion

        public void ActualizaDatosCayd(string preFolio, int idCreditoSustOld, int idCreditoSustNew)
        {
            var sqlConn = new SqlConnection(_contexto.Database.Connection.ConnectionString);

            try
            {
                using (var cmd = new SqlCommand
                    {
                        CommandType = CommandType.StoredProcedure,
                        CommandText = "CRE_SP_ACTUALIZA_CAYD",
                        Connection = sqlConn
                    })
                {
                    ConfiguracionComando.AgregaParametro(cmd, "@PREFOLIO", SqlDbType.VarChar, preFolio, ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@ID_CRED_SUST_OLD", SqlDbType.Int, idCreditoSustOld, ParameterDirection.Input);
                    ConfiguracionComando.AgregaParametro(cmd, "@ID_CRED_SUST_NEW", SqlDbType.Int, idCreditoSustNew, ParameterDirection.Input);

                    sqlConn.Open();

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (sqlConn.State == ConnectionState.Open) sqlConn.Close();
                sqlConn.Dispose(); 
            }
        }
        
        public void ActualizaEquipoBaja(K_CREDITO_SUSTITUCION creditoSustitucion)
        {
            using (var r = new Repositorio<K_CREDITO_SUSTITUCION>())
            {
                r.Actualizar(creditoSustitucion);
            }
        }

        public void ActualizaProductCharacters(K_PRODUCTO_CHARACTERS productoCharacters)
        {
            using (var r = new Repositorio<K_PRODUCTO_CHARACTERS>())
            {
                r.Actualizar(productoCharacters);
            }
        }

        public void ActualizaSustitucionExtension(K_CREDITO_SUSTITUCION_EXTENSION sustitucionExtension)
        {
            using (var r = new Repositorio<K_CREDITO_SUSTITUCION_EXTENSION>())
            {
                r.Actualizar(sustitucionExtension);
            }
        }

        public void ActualizaRecuperacionProducto(K_RECUPERACION_PRODUCTO recuperacionProducto)
        {
            using (var r = new Repositorio<K_RECUPERACION_PRODUCTO>())
            {
                r.Actualizar(recuperacionProducto);
            }
        }

        #endregion

        public EQUIPOS_ALTA_NP InsertaProductoAltaNoPasaron(EQUIPOS_ALTA_NP producto)
        {
            EQUIPOS_ALTA_NP newProducto;

            using (var r = new Repositorio<EQUIPOS_ALTA_NP>())
            {
                newProducto = r.Agregar(producto);
            }

            return newProducto;
        }

        public EQUIPO_BAJA_NP InsertaProductoBajaNoPasaron(EQUIPO_BAJA_NP productoBaja)
        {
            EQUIPO_BAJA_NP newProductoBaja;

            using (var r = new Repositorio<EQUIPO_BAJA_NP>())
            {
                newProductoBaja = r.Agregar(productoBaja);
            }

            return newProductoBaja;
        }

        public static int ObtenIntentoMaxEB_NP()
        {
            int max;

            using (var r = new Repositorio<EQUIPO_BAJA_NP>())
            {
                var lstProductosBaja = r.Filtro(me => true);
                var i = lstProductosBaja.Max(me => me.Secuencia_Intento);
                if (i != null)
                    max = (int) i;
                else
                {
                    max = 0;
                }
            }

            return max + 1;

        }

        public static int ObtenIntentoMaxEA_NP()
        {
            int max;

            using (var r = new Repositorio<EQUIPOS_ALTA_NP>())
            {
                var lstProductosBaja = r.Filtro(me => true);
                var i = lstProductosBaja.Max(me => me.Secuencia_Intento);
                if (i != null)
                    max = (int)i;
                else
                {
                    max = 0;
                }
            }

            return max + 1;

        }
    }
}
