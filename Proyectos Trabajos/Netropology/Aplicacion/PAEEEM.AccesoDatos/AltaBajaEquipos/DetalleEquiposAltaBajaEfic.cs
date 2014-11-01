using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Text;
using PAEEEM.Entidades;
using PAEEEM.AccesoDatos;
using PAEEEM.Entidades.AltaBajaEquipos;
using PAEEEM.Entidades.Alta_Equipos;
using PAEEEM.Entidades.ModuloCentral;
using PAEEEM.AccesoDatos.SolicitudCredito;

namespace PAEEEM.AccesoDatos.AltaBajaEquipos
{
    public class DetalleEquiposAltaBajaEfic
    {

        private static readonly DetalleEquiposAltaBajaEfic _classInstance = new DetalleEquiposAltaBajaEfic();

        public static DetalleEquiposAltaBajaEfic ClassInstance
        {
            get { return _classInstance; }
        }

        public DetalleEquiposAltaBajaEfic()
        {
        }
        private PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();

        public List<DetalleEquiposBajaEfic> ObtenEquiposBajaEficienciaCredito(string No_Credito)
        {
            var resultado = (
                from 

                    EBA in _contexto.K_CREDITO_SUSTITUCION
                join TEC in _contexto.CAT_TECNOLOGIA on EBA.Cve_Tecnologia equals TEC.Cve_Tecnologia
                join p in _contexto.CAT_TIPO_PRODUCTO on EBA.Dx_Tipo_Producto equals p.Ft_Tipo_Producto
                join  capSus in _contexto.CAT_CAPACIDAD_SUSTITUCION on EBA.Cve_Capacidad_Sust equals capSus.Cve_Capacidad_Sust
                into t from capSus in t.DefaultIfEmpty()


                join FT in _contexto.CRE_FOTOS on EBA.No_Credito equals FT.No_Credito into t1
                from FT  
                in t1.Where(a => a.idTipoFoto == 2 && a.IdCreditoSustitucion == EBA.Id_Credito_Sustitucion).DefaultIfEmpty()

                join CCD in _contexto.CAT_CENTRO_DISP on EBA.Id_Centro_Disp equals CCD.Id_Centro_Disp into t2
                from CCD in t2.DefaultIfEmpty()


                join r in _contexto.CAT_REGION on CCD.Cve_Region equals r.Cve_Region into t4
                from r in t4.DefaultIfEmpty()


                join CCDS in _contexto.CAT_CENTRO_DISP_SUCURSAL on EBA.Id_Centro_Disp equals CCDS.Id_Centro_Disp into t3
                from CCDS in t3.DefaultIfEmpty()


                join z in _contexto.CAT_ZONA on CCD.Cve_Zona equals z.Cve_Zona into t5
                from z in t5.DefaultIfEmpty()

                join re in _contexto.CAT_REGION on CCDS.Cve_Region equals re.Cve_Region into t6
                from re in t6.DefaultIfEmpty()

                join zo in _contexto.CAT_ZONA on CCDS.Cve_Zona equals zo.Cve_Zona into t7
                from zo in t7.DefaultIfEmpty()

                where EBA.No_Credito == No_Credito 

                select new DetalleEquiposBajaEfic
                {
                    No_credito = EBA.No_Credito,
                    Dx_Grupo = EBA.Grupo,
                    Dx_Tecnologia = TEC.Dx_Nombre_General,
                    Dx_Tipo_Producto = p.Dx_Tipo_Producto,
                    CapSis = EBA.CapacidadSistema ?? SqlFunctions.StringConvert(capSus.No_Capacidad),
                    Dx_Unidad = EBA.Unidad ?? capSus.Dx_Unidades,
                    Cantidad = EBA.No_Unidades,
                    Marca = EBA.Dx_Marca,
                    Modelo = EBA.Dx_Modelo_Producto,
                    Color = EBA.Dx_Color,
                    Antiguedad = EBA.Dx_Antiguedad,
                    PreFolio = EBA.Id_Pre_Folio,
                    Folio = EBA.Id_Folio,
                    FechaIngr = EBA.Dt_Fecha_Recepcion,//F
                    CAyD = (EBA.Fg_Tipo_Centro_Disp == "M") ? CCD.Dx_Nombre_Comercial : CCDS.Dx_Nombre_Comercial,//F
                    RazonSocial = (EBA.Fg_Tipo_Centro_Disp == "M") ? CCD.Dx_Razon_Social : CCDS.Dx_Razon_Social,//F
                    Zona = (EBA.Fg_Tipo_Centro_Disp == "M") ? z.Dx_Nombre_Zona : zo.Dx_Nombre_Zona,//F
                    Region = (EBA.Fg_Tipo_Centro_Disp == "M") ? r.Dx_Nombre_Region : re.Dx_Nombre_Region,//f
                    IdConsecutivo = FT.idConsecutivoFoto != 0 ? FT.idConsecutivoFoto : 0,
                    idTipoFoto = FT.idTipoFoto != 0 ? FT.idTipoFoto : 0,
                    IdCreditoSustitucion = FT.IdCreditoSustitucion != 0 ? FT.IdCreditoSustitucion : 0,

                }
                            ).ToList();

            return resultado;

        }



        public List<DetalleEquipoAltaEfic> ObtenEquiposAltaEficienciaCredito(string No_Credito)
        {
            if (CreditoViejo(No_Credito) == false)
            {
                var resultado = (
                    from CRE in _contexto.CRE_Credito
                    join PRO in _contexto.K_CREDITO_PRODUCTO on CRE.No_Credito equals PRO.No_Credito
                    join PPRO in _contexto.K_PROVEEDOR_PRODUCTO on CRE.Id_Proveedor equals PPRO.Id_Proveedor
                    join CPRO in _contexto.CAT_PRODUCTO on PRO.Cve_Producto equals CPRO.Cve_Producto
                    join TEC in _contexto.CAT_TECNOLOGIA on CPRO.Cve_Tecnologia equals TEC.Cve_Tecnologia
                    join CM in _contexto.CAT_MARCA on CPRO.Cve_Marca equals CM.Cve_Marca
                    
                    join FT in _contexto.CRE_FOTOS on CRE.No_Credito equals FT.No_Credito into t1
                    from FT in t1.Where(a => a.idTipoFoto == 3 && a.idCreditoProducto == PRO.ID_CREDITO_PRODUCTO).DefaultIfEmpty()
                    
                    join FAC1 in _contexto.CRE_Facturacion on CRE.No_Credito equals FAC1.No_Credito into T2
                    from FAC1 in T2.Where(a => a.IdTipoFacturacion == 1).DefaultIfEmpty()

                    join FAC2 in _contexto.CRE_Facturacion on CRE.No_Credito equals FAC2.No_Credito into T3
                    from FAC2 in T3.Where(a => a.IdTipoFacturacion == 2).DefaultIfEmpty()

                    join CFA in _contexto.CAT_FABRICANTE on CPRO.Cve_Fabricante equals CFA.Cve_Fabricante

                    join CT1 in _contexto.CAT_TARIFA on FAC1.Cve_Tarifa equals CT1.Cve_Tarifa into T4
                    from CT1 in T4.DefaultIfEmpty()

                    join CT2 in _contexto.CAT_TARIFA on FAC2.Cve_Tarifa equals CT2.Cve_Tarifa into T5
                    from CT2 in T5.DefaultIfEmpty()
                    
                    where CPRO.Cve_Producto == PPRO.Cve_Producto  && CRE.No_Credito == No_Credito 
                    
                    select new DetalleEquipoAltaEfic
                    {
                        NO_CREDITO = CRE.No_Credito,
                        Dx_Grupo = PRO.Grupo,
                        Dx_Tecnologia = TEC.Dx_Nombre_General,
                        Fabricante = CFA.Dx_Nombre_Fabricante,//f
                        Dx_Marca = CM.Dx_Marca,
                        Dx_Modelo = CPRO.Dx_Modelo_Producto,
                        Cantidad = PRO.No_Cantidad,
                        No_Capacidad = PRO.CapacidadSistema,
                        Precio_Distribuidor = CPRO.Mt_Precio_Max,
                        Precio_Unitario = PPRO.Mt_Precio_Unitario,
                        Importe_Total_Sin_IVA = PRO.Mt_Precio_Unitario_Sin_IVA * PRO.No_Cantidad,
                        Gasto_Instalacion = PRO.Mt_Gastos_Instalacion_Mano_Obra,
                        CostAcopDes = null,//F
                        MontoIncentivo = PRO.Incentivo,
                        TarifaFutura = CT2.Dx_Tarifa,
                        TarifaOrigen = CT1.Dx_Tarifa,//F
                        IdConsecutivo = FT.idConsecutivoFoto != 0 ? FT.idConsecutivoFoto : 0,
                        idTipoFoto = FT.idTipoFoto != 0 ? FT.idTipoFoto : 0,
                        IdCreditoSustitucion = FT.IdCreditoSustitucion != 0 ? FT.IdCreditoSustitucion : 0,
                        IdCreditoProducto = PRO.ID_CREDITO_PRODUCTO

                    }
                                ).ToList();
                return resultado;
            }
            else
            {

                var resultado = (
                    from CRE in _contexto.CRE_Credito.AsEnumerable()
                    join PRO in _contexto.K_CREDITO_PRODUCTO on CRE.No_Credito equals PRO.No_Credito
                    join PPRO in _contexto.K_PROVEEDOR_PRODUCTO on CRE.Id_Proveedor equals PPRO.Id_Proveedor
                    join CPRO in _contexto.CAT_PRODUCTO on PRO.Cve_Producto equals CPRO.Cve_Producto
                    join TEC in _contexto.CAT_TECNOLOGIA on CPRO.Cve_Tecnologia equals TEC.Cve_Tecnologia
                    join CM in _contexto.CAT_MARCA on CPRO.Cve_Marca equals CM.Cve_Marca
                    join CFA in _contexto.CAT_FABRICANTE on CPRO.Cve_Fabricante equals CFA.Cve_Fabricante
                    // ADD////////
                    join csus in _contexto.CAT_CAPACIDAD_SUSTITUCION on CPRO.Cve_Capacidad_Sust equals csus.Cve_Capacidad_Sust 
                    join cDes in _contexto.K_CREDITO_DESCUENTO on CRE.No_Credito equals cDes.No_Credito
                    where CPRO.Cve_Producto == PPRO.Cve_Producto && CRE.No_Credito == No_Credito


                    select new DetalleEquipoAltaEfic
                    {
                        NO_CREDITO = CRE.No_Credito,
                        Dx_Grupo = PRO.Grupo,
                        Dx_Tecnologia = TEC.Dx_Nombre_General,
                        Fabricante = CFA.Dx_Nombre_Fabricante,//f
                        Dx_Marca = CM.Dx_Marca,
                        Dx_Modelo = CPRO.Dx_Modelo_Producto,
                        Cantidad = PRO.No_Cantidad,
                        Precio_Unitario = PPRO.Mt_Precio_Unitario,
                        Importe_Total_Sin_IVA = PRO.Mt_Precio_Unitario_Sin_IVA * PRO.No_Cantidad,
                        Gasto_Instalacion = PRO.Mt_Gastos_Instalacion_Mano_Obra,
                        CostAcopDes = null,//F
                        MontoIncentivo = cDes.Mt_Descuento, ////// CAMBIO //////////
                        TarifaFutura = "N/A",
                        TarifaOrigen = "N/A",//F
                        IdConsecutivo = 0,
                        idTipoFoto = 0,
                        IdCreditoSustitucion = 0,

                        // ADD////
                        Precio_Distribuidor = CPRO.Mt_Precio_Max,
                        No_Capacidad = csus.No_Capacidad.ToString()

                    }
                                ).ToList();
                return resultado;
            }
        }

        public DetallePresupuesto ObtenResumenPres(List<DetalleEquipoAltaEficiencia> LstAltaEficiencia, List<EquipoBajaEficiencia> LstBajaEficiencia, decimal valorIva, string No_Credito)
        {
            DetallePresupuesto Presupuesto = new DetallePresupuesto();
            //if (CreditoViejo(No_Credito) == false)
            //{
            if (LstAltaEficiencia.Count > 0)
            {
                var costoEquipos = 0.0M;
                var montoIncentivo = 0.0M;
                var gastosInstalacion = 0.0M;

                foreach (var equipoAltaEficiencia in LstAltaEficiencia)
                {
                    costoEquipos = costoEquipos + equipoAltaEficiencia.Importe_Total_Sin_IVA;
                    montoIncentivo = montoIncentivo + equipoAltaEficiencia.MontoIncentivo;
                    gastosInstalacion = gastosInstalacion + equipoAltaEficiencia.Gasto_Instalacion;
                }

                //var montoChatarrizacion = LstBajaEficiencia.Aggregate(0.0M,
                //                                                      (current, equipoBajaEficiencia) =>
                //                                                      current +
                //                                                      (equipoBajaEficiencia.MontoChatarrizacion *
                //                                                       equipoBajaEficiencia.Cantidad));

                var montoChatarrizacion =
                    LstBajaEficiencia.Where(
                        equipoBajaEficiencia =>
                        LstAltaEficiencia.Any(p => p.Cve_Grupo.ToString() == equipoBajaEficiencia.Cve_Grupo.ToString()))
                                     .Sum(
                                         equipoBajaEficiencia =>
                                         equipoBajaEficiencia.MontoChatarrizacion * equipoBajaEficiencia.Cantidad);

                var montoIva = valorIva * (costoEquipos + gastosInstalacion);
                var subTotal = (costoEquipos + gastosInstalacion) + montoIva;
                var descuento = montoIncentivo - montoChatarrizacion;

                Presupuesto.CostoEquipos = costoEquipos;
                Presupuesto.CostoAcopioDesc = montoChatarrizacion;
                Presupuesto.Incentivo = montoIncentivo;
                Presupuesto.Descuento = descuento;
                Presupuesto.GastosInstalacion = gastosInstalacion;
                Presupuesto.IVA = montoIva;
                Presupuesto.SubTotal = subTotal;

                var total = subTotal - Presupuesto.Descuento;

                Presupuesto.TOTAL = total;


                if (descuento < 0)
                    throw new Exception("Capture un monto mayor para el equipo de Alta");

            }
            return Presupuesto;
            //}
            //else
            //{
            //    var Lista = (from KCP in _contexto.K_CREDITO_PRODUCTO
            //                 join CP in _contexto.CAT_PRODUCTO on KCP.Cve_Producto equals CP.Cve_Producto
            //                 join CTP in _contexto.CAT_TIPO_PRODUCTO on CP.Ft_Tipo_Producto equals CTP.Ft_Tipo_Producto
            //                 join C in _contexto.CRE_Credito on KCP.No_Credito equals C.No_Credito
            //                 join KCC in _contexto.K_CREDITO_COSTO on KCP.No_Credito equals KCC.No_Credito
            //                 join KCD in _contexto.K_CREDITO_DESCUENTO on KCP.No_Credito equals KCD.No_Credito
            //                 where KCP.No_Credito == No_Credito

            //                 select new UnidadesPresupuesto
            //                 {
            //                     Tecnologia = CP.Cve_Tecnologia,
            //                     Modelo = KCP.Cve_Producto,
            //                     TipoProducto = CTP.Ft_Tipo_Producto,
            //                     Marca = CP.Cve_Marca,
            //                     Cantidad = KCP.No_Cantidad,
            //                     ImporteSinIVa = KCP.Mt_Precio_Unitario_Sin_IVA * KCP.No_Cantidad,
            //                     SubTotal = KCP.Mt_Precio_Unitario_Sin_IVA * KCP.No_Cantidad,
            //                     Capacidad = CP.Cve_Capacidad_Sust,
            //                     GastosInst = KCP.Mt_Gastos_Instalacion_Mano_Obra / 1.16M,
            //                     Incentivo = KCP.Incentivo,
            //                     Descuento = KCD.Mt_Descuento,
            //                     AcopioDes = KCC.Mt_Costo
            //                 }).ToList();


            //    UnidadesPresupuesto Total = new UnidadesPresupuesto();
            //    var IVA = 0.16M;
            //    Total.ImporteSinIVa = 0.0M;
            //    Total.SubTotal = 0.0M;
            //    Total.GastosInst = 0.0M;


            //    foreach (var item in Lista)
            //    {
            //        Total.Descuento = item.Descuento;
            //        Total.ImporteSinIVa += item.ImporteSinIVa;
            //        Total.SubTotal += item.SubTotal;
            //        Total.GastosInst += item.GastosInst;
            //        Total.AcopioDes = item.AcopioDes;
            //        Total.Incentivo = item.Incentivo;

            //    }

            //    Presupuesto.IVA = IVA * (Total.ImporteSinIVa + Total.GastosInst);
            //    Presupuesto.SubTotal = (Total.ImporteSinIVa + Total.AcopioDes) + Presupuesto.IVA;
            //    Presupuesto.Descuento = Total.Incentivo - Total.AcopioDes;
            //    Presupuesto.GastosInstalacion = Total.GastosInst;
            //    Presupuesto.Incentivo = Total.Incentivo ?? 0;
            //    Presupuesto.Descuento = Total.Descuento ?? 0;
            //    Presupuesto.CostoAcopioDesc = Total.AcopioDes ?? 0;
            //    Presupuesto.CostoEquipos = Total.SubTotal;

            //return Presupuesto;

        }


        //IVA = (((KCP.Mt_Precio_Unitario_Sin_IVA * KCP.No_Cantidad) + ((KCP.Mt_Gastos_Instalacion_Mano_Obra) / 1.16M) + KCC.Mt_Costo) - (KCD.Mt_Descuento + KCD.Mt_Descuento))*0.16M,
        //                             SubTotal= ((KCP.Mt_Precio_Unitario_Sin_IVA * KCP.No_Cantidad) + ((KCP.Mt_Gastos_Instalacion_Mano_Obra)/1.16M) + KCC.Mt_Costo) - (KCD.Mt_Descuento + KCD.Mt_Descuento),
        //                             Incentivo= KCP.Incentivo,
        //                             GastosInstalacion = (KCP.Mt_Gastos_Instalacion_Mano_Obra),
        //                             CostoAcopioDesc = KCC.Mt_Costo,
        //                             CostoEquipos= KCP.Mt_Precio_Unitario_Sin_IVA * KCP.No_Cantidad,
        //                             Descuento = KCD.Mt_Descuento,
        //                             TOTAL = (((KCP.Mt_Precio_Unitario_Sin_IVA * KCP.No_Cantidad) + ((KCP.Mt_Gastos_Instalacion_Mano_Obra) / 1.16M) + KCC.Mt_Costo) - (KCD.Mt_Descuento + KCD.Mt_Descuento)) * 1.16M


        public CRE_HISTORICO_CONSUMO ObtenHistoricoResumen(string No_Credito)
        {
            CRE_HISTORICO_CONSUMO Detalle;

            using (var r = new Repositorio<CRE_HISTORICO_CONSUMO>())
            {
                Detalle = r.Extraer(me => me.No_Credito == No_Credito);
            }

            return Detalle;
        }

        public DetalleBalenceMensual ObtenDetalleBalance(string No_Credito)
        {
            var Detalle = (from Cre in _contexto.CRE_Credito
                           join Neg in _contexto.CLI_Negocio on Cre.No_Credito equals Neg.No_Credito
                           where Cre.IdCliente == Neg.IdCliente && Cre.IdNegocio == Neg.IdNegocio && Cre.Id_Branch == Neg.Id_Branch && Cre.Id_Proveedor == Neg.Id_Proveedor


                           select new DetalleBalenceMensual
                           {
                               GastosMensuales = Neg.Gastos_Mes,
                               Ventas_mes = Neg.Ventas_Mes
                           }).ToList().FirstOrDefault();

            return Detalle;
        }

        public Fotos ObtenFotoFachada(string No_Credito)
        {
            var Foto = (from CRE in _contexto.CRE_Credito
                        join FT in _contexto.CRE_FOTOS on CRE.No_Credito equals FT.No_Credito
                        where CRE.No_Credito == No_Credito && FT.idTipoFoto == 1

                        select new Fotos
                        {
                            No_Credito = CRE.No_Credito,
                            idtipoFoto = FT.idTipoFoto,
                            idCreditoProducto = FT.idCreditoProducto,
                            idCreditoSustitucion = FT.IdCreditoSustitucion,
                            idConsecutivoFoto = FT.idConsecutivoFoto
                        }
                        ).ToList().FirstOrDefault();

            return Foto;
        }


        public List<HistoricoConsultas> ObtenHistorico(string RPU)
        {
            var Historico = (from cre in _contexto.CRE_Credito
                             where cre.RPU == RPU
                             orderby cre.Fecha_Consulta descending

                             select new HistoricoConsultas
                             {
                                 No_RPU = cre.RPU,
                                 No_Solicitud = cre.No_Credito,
                                 Folio = cre.Folio_Consulta,
                                 MOP = cre.No_MOP,
                                 Fecha_Consulta = cre.Fecha_Consulta
                             }).ToList();

            return Historico;
        }

        public bool CreditoViejo(string noCredito)
        {
            CRE_Credito detalle;

            using (var r = new Repositorio<CRE_Credito>())
            {
                detalle = r.Extraer(me => me.No_Credito == noCredito);
            }

            return detalle.Consumo_Promedio == null && detalle.Demanda_Maxima == null;
        }


        public List<DetalleEquipoAltaEficiencia> ObtenEquiposAltaEficienciaCreditoDetalle(string idCredito)
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
                             select new DetalleEquipoAltaEficiencia
                             {
                                 ID = creditoProducto.ID_CREDITO_PRODUCTO,
                                 ID_Baja = creditoProducto.ID_CREDITO_PRODUCTO,
                                 Cve_Grupo = creditoProducto.IdGrupo != null ? (int)creditoProducto.IdGrupo : 0,
                                 Dx_Grupo = creditoProducto.Grupo,
                                 Cve_Marca = p.Cve_Marca,
                                 Dx_Marca = m.Dx_Marca,
                                 Cve_Modelo = p.Cve_Producto,
                                 Dx_Modelo = p.Dx_Modelo_Producto,
                                 Dx_Sistema = creditoProducto.CapacidadSistema,
                                 Cantidad = (int)creditoProducto.No_Cantidad,
                                 Precio_Unitario = (decimal)creditoProducto.Mt_Precio_Unitario_Sin_IVA != null ? (decimal)creditoProducto.Mt_Precio_Unitario_Sin_IVA : 0,
                                 Precio_Distribuidor = d.Mt_Precio_Unitario,
                                 Importe_Total_Sin_IVA =
                                     ((decimal)creditoProducto.Mt_Precio_Unitario_Sin_IVA != null ? (decimal)creditoProducto.Mt_Precio_Unitario_Sin_IVA : 1)*
                                     (int)creditoProducto.No_Cantidad, //(decimal)creditoProducto.Mt_Total,
                                 Gasto_Instalacion = creditoProducto.Mt_Gastos_Instalacion_Mano_Obra != null ?  (decimal)creditoProducto.Mt_Gastos_Instalacion_Mano_Obra: 0.0M,
                                 MontoIncentivo =
                                     creditoProducto.Incentivo != null ? (decimal)creditoProducto.Incentivo : 0.0M
                             }
                                ).ToList();

            return resultado;
        }
    }
}
