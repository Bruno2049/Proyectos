using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using PAEEEM.AccesoDatos.CargaMasivaProductos;
using PAEEEM.AccesoDatos.Catalogos;
using PAEEEM.Entidades;
using PAEEEM.Entidades.CreditosAutorizadosE;


namespace PAEEEM.AccesoDatos.CreditosAutorizadosA
{
    public class CreditosAutorizadosA
    {
        private static readonly CreditosAutorizadosA _classInstance = new CreditosAutorizadosA();

        public static CreditosAutorizadosA ClassInstance
        {
            get { return _classInstance; }
        }

        private readonly PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();

        public CreditosAutorizadosA()
        {
        }

        public List<CAT_REGION> ObtenListaRegion()
        {
            List<CAT_REGION> Lista = null;

            using (var r = new Repositorio<CAT_REGION>())
            {
                Lista = r.Filtro(me => me.Cve_Region > 0);
            }

            return Lista;
        }

        public List<CAT_ZONA> ObtenListaZona()
        {
            List<CAT_ZONA> Lista = null;

            using (var r = new Repositorio<CAT_ZONA>())
            {
                Lista = r.Filtro(me => me.Cve_Region > 0);
            }

            return Lista;
        }

        public List<CAT_ZONA> ObtenListaZonaFiltro(int Cve_Region)
        {
            List<CAT_ZONA> Lista = null;

            using (var r = new Repositorio<CAT_ZONA>())
            {
                Lista = r.Filtro(me => me.Cve_Region == Cve_Region);
            }

            return Lista;
        }

        public CAT_ZONA ObtenUbicacion(int Cve_Zona)
        {
            CAT_ZONA Lista = null;

            using (var r = new Repositorio<CAT_ZONA>())
            {
                Lista = r.Extraer(me => me.Cve_Zona == Cve_Zona);
            }

            return Lista;
        }

        public CAT_ZONA ObtenUbuacionZona(int Cve_Region, int Cve_Zona)
        {
            CAT_ZONA Lista = null;

            using (var r = new Repositorio<CAT_ZONA>())
            {
                Lista = r.Extraer(me => me.Cve_Zona == Cve_Zona && me.Cve_Region == Cve_Region);
            }

            return Lista;
        }

        
        public List<CAT_ESTADO> ObtenListaEstado()
        {
            List<CAT_ESTADO> Lista = null;

            using (var r = new Repositorio<CAT_ESTADO>())
            {
                Lista = r.Filtro(me => me.Cve_Estado > 0);
            }

            return Lista;
        }

        public List<CAT_DELEG_MUNICIPIO> ObtenListaMunicipios(int Cve_Estado)
        {
            List<CAT_DELEG_MUNICIPIO> Lista = null;

            using (var r = new Repositorio<CAT_DELEG_MUNICIPIO>())
            {
                Lista = r.Filtro(me => me.Cve_Estado == Cve_Estado);
            }

            return Lista;
        }

        public List<GridCreditosLiberados> ListaCreditosLiberados(int Cve_Region, int Cve_Zona, int Cve_Estado, int Cve_Municipio,string No_Credito,DateTime? Desde, DateTime? Hasta)
        {
            var Lista = (from CC in _contexto.CRE_Credito
                         join CN in _contexto.CLI_Negocio on new { CC.IdNegocio, CC.IdCliente, CC.Id_Branch } equals new { IdNegocio = (short?)CN.IdNegocio, IdCliente = (int?)CN.IdCliente, Id_Branch = (int?)CN.Id_Branch }
                         join CF_A in _contexto.CRE_Facturacion on new { CC.No_Credito, IdTipoFacturacion = (byte)2 } equals new { No_Credito = (string)CF_A.No_Credito, IdTipoFacturacion = CF_A.IdTipoFacturacion } into A
                         from CF_A in A.DefaultIfEmpty()
                         join CT_A in _contexto.CAT_TARIFA on CF_A.Cve_Tarifa equals CT_A.Cve_Tarifa into B
                         from CT_A in B.DefaultIfEmpty()
                         join CF_O in _contexto.CRE_Facturacion on new { CC.No_Credito, IdTipoFacturacion = (byte)1 } equals new { No_Credito = (string)CF_O.No_Credito, IdTipoFacturacion = CF_O.IdTipoFacturacion } into C
                         from CF_O in C.DefaultIfEmpty()
                         join CT_O in _contexto.CAT_TARIFA on CF_O.Cve_Tarifa equals CT_O.Cve_Tarifa into D
                         from CT_O in D.DefaultIfEmpty()
                         join CCLI in _contexto.CLI_Cliente on new { CC.Id_Branch, CC.IdCliente } equals new { Id_Branch = (int?)CCLI.Id_Branch, IdCliente = (int?)CCLI.IdCliente } into E
                         from CCLI in E.DefaultIfEmpty()
                         join CPB in _contexto.CAT_PROVEEDORBRANCH on CC.Id_Branch equals CPB.Id_Branch into F
                         from CPB in F.DefaultIfEmpty()
                         join CTI in _contexto.CAT_TIPO_INDUSTRIA on CN.Cve_Tipo_Industria equals CTI.Cve_Tipo_Industria into G
                         from CTI in G.DefaultIfEmpty()
                         join KCA in _contexto.K_CREDITO_AMORTIZACION on new { CC.No_Credito, No_Pago = (int)1 } equals new { No_Credito = (string)KCA.No_Credito, No_Pago = (int)KCA.No_Pago } into H
                         from KCA in H.DefaultIfEmpty()
                         join DD in _contexto.DIR_Direcciones on new { CN.IdCliente, CN.IdNegocio, IdTipoDomicilio = (byte?)1 } equals new { IdCliente = (int)DD.IdCliente, IdNegocio = (short)DD.IdNegocio, IdTipoDomicilio = (byte?)DD.IdTipoDomicilio } into I
                         from DD in I.DefaultIfEmpty()
                         join CE in _contexto.CAT_ESTADO on DD.Cve_Estado equals CE.Cve_Estado
                         join CDM in _contexto.CAT_DELEG_MUNICIPIO on DD.Cve_Deleg_Municipio equals CDM.Cve_Deleg_Municipio
                         join CZ in _contexto.CAT_ZONA on CPB.Cve_Zona equals CZ.Cve_Zona
                         join CR in _contexto.CAT_REGION on CPB.Cve_Region equals CR.Cve_Region
                         where CC.Cve_Estatus_Credito == 1 
                         && (Cve_Estado == -1 ? 
                         ((Cve_Region == 0 || CR.Cve_Region == Cve_Region) && (Cve_Zona == 0 || CZ.Cve_Zona == Cve_Zona ))
                         :
                         ((Cve_Estado == 0 || CE.Cve_Estado ==Cve_Estado) && (Cve_Municipio == 0 || CDM.Cve_Deleg_Municipio == Cve_Municipio)))
                         &&  (No_Credito == "" || CC.No_Credito == No_Credito)
                         /*&&  (CC.Afectacion_SICOM_fecha >= Desde && CC.Afectacion_SICOM_fecha <= Hasta)*/
                         //(Cve_Region == 0 || CC.Id_Proveedor == Cve_Region)
                         //(Zona != 0 ? CC.Id_Branch == Zona : false)
                         //&& (swith())
                         select new GridCreditosLiberados
                         {
                             No_Credito = CC.No_Credito,
                             FechaIngreso = CC.Fecha_Pendiente,
                             FechaAutorizado = CC.Afectacion_SICOM_fecha,
                             intelisis = CC.ID_Intelisis,
                             MontoFinanciado = CC.Monto_Solicitado,
                             GastosInstalacion = CC.Gastos_Instalacion,
                             kwhPromedio = CC.Consumo_Promedio,
                             kwPromedio = CC.Demanda_Maxima,
                             FactorPotencia = CC.Factor_Potencia,
                             kwhAhorro = CC.No_Ahorro_Consumo,
                             kwAhorro=CC.No_Ahorro_Demanda,
                             Tarifa = CT_A.Dx_Tarifa,
                             TarifaOrigen = CT_O.Dx_Tarifa,
                             RazonSocial = CCLI.Cve_Tipo_Sociedad == 1 ? CCLI.Nombre+" "+CCLI.Ap_Paterno+" "+CCLI.Ap_Materno : CCLI.Razon_Social,
                             RFC = CCLI.RFC,
                             NombreComercial = CN.Nombre_Comercial,
                             GiroComercial = CTI.DESCRIPCION_SCIAN,
                             Amortizacion = KCA.Mt_Pago,
                             PAmortizacion = KCA.Dt_Fecha,
                             Estado = CE.Dx_Nombre_Estado,
                             Municipio = CDM.Dx_Deleg_Municipio,
                             RazonSocialDist = CPB.Dx_Razon_Social,
                             NombreComercialDist = CPB.Dx_Nombre_Comercial,
                             TipoSucuralDist = CPB.Tipo_Sucursal,
                             Region = CR.Dx_Nombre_Region,
                             Zona = CZ.Dx_Nombre_Zona,
                             FechaLiberado = CC.Afectacion_SICOM_fecha,
                             //Filtro
                             Cve_region = CPB.Cve_Region,
                             Cve_Zona = CPB.Cve_Zona,
                             Cve_Estado = DD.Cve_Estado,
                             Cve_Municipio = DD.Cve_Deleg_Municipio

                         }
                         )
                         .ToList();

            foreach (var item in Lista)
            {
                item.NO_EA_RC = (int?)ObtenEA(item.No_Credito, 1);
                item.NO_EA_AA = (int?) ObtenEA(item.No_Credito, 2);
                item.NO_EA_IL = (int?) ObtenEA(item.No_Credito, 3);
                item.NO_EA_ME = (int?) ObtenEA(item.No_Credito, 4);
                item.NO_EA_SE = (int?) ObtenEA(item.No_Credito, 5);
                item.NO_EA_ILED = (int?) ObtenEA(item.No_Credito, 6);
                item.NO_EA_BC = (int?) ObtenEA(item.No_Credito, 7);
                item.NO_EA_II = (int?) ObtenEA(item.No_Credito, 8);
                item.NO_EA_CR = (int?) ObtenEA(item.No_Credito, 9);

                item.NO_EB_RC = (int?) ObtenEB(item.No_Credito, 1);
                item.NO_EB_AA = (int?) ObtenEB(item.No_Credito, 2);
                item.NO_EB_ME = (int?) ObtenEB(item.No_Credito, 4);
                item.NO_EB_CR = (int?) ObtenEB(item.No_Credito, 9);
            }

            return Lista;
        }

        public int? ObtenEA(string No_Credito, int Tecnologia)
        {
            Equipos Reg = new Equipos();
            Reg = (from KPP in _contexto.K_CREDITO_PRODUCTO
                               join CP in _contexto.CAT_PRODUCTO on KPP.Cve_Producto equals CP.Cve_Producto
                               where KPP.No_Credito == No_Credito && CP.Cve_Tecnologia == Tecnologia
                               group new {KPP, CP} by CP.Cve_Tecnologia into G
                               select new Equipos
                               {
                                   NO_Unidades = (G.Sum(P => P.KPP.No_Cantidad)) != null ? G.Sum(P => P.KPP.No_Cantidad) : 0
                               }
                           ).FirstOrDefault();
            return Reg != null ? Reg.NO_Unidades : 0;
        }

        public int? ObtenEB(string No_Credito, int tecnologia)
        {
            Equipos Reg = new Equipos();

            Reg = (from KCS in _contexto.K_CREDITO_SUSTITUCION
                join CT in _contexto.CAT_TECNOLOGIA on KCS.Cve_Tecnologia equals CT.Cve_Tecnologia
                where KCS.No_Credito == No_Credito && KCS.Cve_Tecnologia == tecnologia
                group new {KCS, CT} by KCS.Cve_Tecnologia
                into g
                select new Equipos
                {
                    NO_Unidades = g.Sum(P => P.KCS.No_Unidades) 
                }).FirstOrDefault();
            
            return Reg != null ? Reg.NO_Unidades : 0;
        }
    }
} 
