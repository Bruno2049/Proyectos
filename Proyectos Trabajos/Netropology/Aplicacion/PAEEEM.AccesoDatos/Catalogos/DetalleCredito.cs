using System;
using System.Collections.Generic;
using System.Linq;
using PAEEEM.Entidades;
using PAEEEM.Entidades.ModuloCentral;

namespace PAEEEM.AccesoDatos.Catalogos
{
    [Serializable]
    public class DetalleCredito
    {
        private readonly PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();

        public SUBESTACIONES Subestacion(string nc)
        {
            var subest = from sub in _contexto.SUBESTACIONES
                         where sub.No_Credito == nc
                         select sub;
            return subest.FirstOrDefault();
        }

        public List<CAT_ESTATUS_CREDITO> EstatusCredito()
        {
            var query = from estCre in _contexto.CAT_ESTATUS_CREDITO
                        select estCre;
            return query.ToList();
        }

        public CAT_CODIGO_POSTAL_SEPOMEX Sepomex(string cp)
        {
            var query = (from spmx in _contexto.CAT_CODIGO_POSTAL_SEPOMEX.AsEnumerable()
                        where spmx.Codigo_Postal == cp
                        select spmx).FirstOrDefault();
            return query;
        }

        public CAT_ESTADO Estados(int cveEstado)
        {
            var query = (from est in _contexto.CAT_ESTADO
                        where est.Cve_Estado == cveEstado
                        select est).FirstOrDefault();
            return query;
        }

        public CAT_ESTADO Estados(string cveTrama)
        {
            var query = (from est in _contexto.CAT_ESTADO
                         where est.Dx_Cve_Trama == cveTrama
                         select est).FirstOrDefault();
            return query;
        }

        public CAT_DELEG_MUNICIPIO DelMun(int cveDm)
        {
            var query = (from dm in _contexto.CAT_DELEG_MUNICIPIO
                         where dm.Cve_Deleg_Municipio == cveDm
                         select dm).FirstOrDefault();
            return query;
        }

        public CAT_TIPO_PROPIEDAD TipoPropiedad(int clave)
        {
            var query = (from tp in _contexto.CAT_TIPO_PROPIEDAD
                         where tp.Cve_Tipo_Propiedad == clave
                         select tp).FirstOrDefault();
            return query;
        }

        public List<DatosConsulta> DatosBusquedaSolicitudes(string noCre, string rpu, string rfc, int estatus, int reg, string distrRs, string distrNc, int zon, DateTime? fechaIn, DateTime? fechaFin)
        {
         
            var query1 = from cre in _contexto.CRE_Credito
                         join cli in _contexto.CLI_Cliente on new { CP = (int)cre.Id_Proveedor, CB = (int)cre.Id_Branch, CC = (int)cre.IdCliente } equals new { CP = cli.Id_Proveedor, CB = cli.Id_Branch, CC = cli.IdCliente }
                         join catEc in _contexto.CAT_ESTATUS_CREDITO on cre.Cve_Estatus_Credito equals catEc.Cve_Estatus_Credito
                         join catPb in _contexto.CAT_PROVEEDORBRANCH on cre.Id_Branch equals catPb.Id_Branch into pb
                         from catPb in pb.DefaultIfEmpty()
                         join catP in _contexto.CAT_PROVEEDOR on cre.Id_Proveedor equals catP.Id_Proveedor
                         join catR in _contexto.CAT_REGION on (cre.Id_Branch==0?catP.Cve_Region:catPb.Cve_Region) equals catR.Cve_Region
                         join catZ in _contexto.CAT_ZONA on (cre.Id_Branch == 0 ? catP.Cve_Zona : catPb.Cve_Zona) equals catZ.Cve_Zona
                         
                         where (noCre == "" || cre.No_Credito == noCre)
                                 && (rpu == "" ? 1 == 1 : cre.RPU == rpu)
                                 && (rfc == "" ? 1 == 1 : cli.RFC == rfc)
                                 && (estatus == 0 ? 1 == 1 : cre.Cve_Estatus_Credito == estatus)
                                 && ((fechaIn == null) || (fechaFin == null) ? 1 == 1 : cre.Fecha_Ultmod >= fechaIn && cre.Fecha_Ultmod <= fechaFin) //cambiar > a >= y < a <=
                                 && (reg == 0 ? 1==1 :reg==(cre.Id_Branch == 0 ? catP.Cve_Region: catPb.Cve_Region))
                                 && (distrRs == "" ? 1 == 1 : distrRs == (cre.Id_Branch == 0 ? catP.Dx_Razon_Social : catPb.Dx_Razon_Social))
                                 && (distrNc == "" ? 1 == 1 : distrNc == (cre.Id_Branch == 0 ? catP.Dx_Nombre_Comercial : catPb.Dx_Nombre_Comercial))
                                 && (zon == 0 ? 1 == 1 : zon == (cre.Id_Branch == 0 ? catP.Cve_Zona : catPb.Cve_Zona))
                                
                         select new DatosConsulta
                         {
                             No_Credito = cre.No_Credito,
                             RPU = cre.RPU,
                             NombreRazonSocial = cli.Razon_Social ?? cli.Nombre + " " + cli.Ap_Paterno + " " + cli.Ap_Materno,
                             RFC = cli.RFC,
                             Monto_Solicitado = cre.Monto_Solicitado,
                             Dx_Estatus_Credito = catEc.Dx_Estatus_Credito,
                             Fecha_Ultmod = cre.Fecha_Ultmod,
                             Dx_Razon_Social = cre.Id_Branch==0?catP.Dx_Razon_Social:catPb.Dx_Razon_Social,
                             Dx_Nombre_Comercial = cre.Id_Branch==0?catP.Dx_Nombre_Comercial:catPb.Dx_Nombre_Comercial,
                             Dx_Nombre_Region = catR.Dx_Nombre_Region,
                             Dx_Nombre_Zona = catZ.Dx_Nombre_Zona
                         };
            
            
            var query = query1.OrderBy(c => c.No_Credito).ToList();
            return query;
        }


        public ResponseData DatosTrama(string rpu, DateTime fechaConsulta)
        {
            var trama = from t in _contexto.ResponseData
                        where t.ServiceCode == rpu
                        && t.FechaConsulta == fechaConsulta
                        && (t.ComStatus == "A4" || t.ComStatus == "A%")
                        select t;
            return trama.ToList().LastOrDefault();
        }

        public ResponseData DatosTrama(string NC)
        {
            var trama = (from cr in _contexto.CRE_Credito
                         join res in _contexto.ResponseData
                             on cr.Id_Trama equals res.Id_Trama
                         where cr.No_Credito == NC
                         select res
                            ).ToList().LastOrDefault();
            return trama;
        }

        public bool EsNuevo(string NC)
        {
            var c = from cre in _contexto.CRE_Credito
                    where cre.No_Credito==NC
                    select cre;
            if (c.ToList().FirstOrDefault().Id_Trama != null) return true;
            else return false;
        }

        public List<K_CREDITO_AMORTIZACION> TablaAmortizacion(string NC)
        {
            var tabla = from ca in _contexto.CRE_Credito
                        join ta in _contexto.K_CREDITO_AMORTIZACION on ca.No_Credito equals ta.No_Credito
                        where ca.No_Credito == NC
                        select ta;

            return tabla.ToList();
        }
    }
} 