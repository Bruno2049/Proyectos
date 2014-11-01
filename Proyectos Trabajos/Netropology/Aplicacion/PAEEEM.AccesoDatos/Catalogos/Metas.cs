using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.Entidades;

namespace PAEEEM.AccesoDatos.Catalogos
{
    public class Metas
    {
        private readonly PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();

        public decimal? montoTotalPrograma()
        {
            var monto = from m in _contexto.CAT_PROGRAMA
                  select m;

            return monto.ToList().FirstOrDefault().Mt_Fondo_Total_Programa;
        }

        public decimal montoMinimoPrograma()
        {
            var monto = from m in _contexto.TR_PARAMETROS_GLOBALES
                        where m.IDPARAMETRO == 10 && m.IDSECCION == 17
                        select m;

            return decimal.Parse(monto.FirstOrDefault().VALOR);
        }

        public decimal? montoDisponibleIncentivo()
        {
            var monto = from m in _contexto.CAT_PROGRAMA
                        select m;

            return monto.ToList().FirstOrDefault().Mt_Fondo_Disponible_Incentivo;
        }

        public decimal? montoDisponiblePrograma()
        {
            var monto = from m in _contexto.CAT_PROGRAMA
                        select m;

            return monto.ToList().FirstOrDefault().Mt_Fondo_Disponible;
        }

        public decimal montoMinimoIncentivo()
        {
            var monto = from m in _contexto.TR_PARAMETROS_GLOBALES
                        where m.IDPARAMETRO == 11 && m.IDSECCION == 17
                        select m;

            return decimal.Parse(monto.FirstOrDefault().VALOR);
        }

        public decimal montoTotalIncentivo()
        {
            var monto = from m in _contexto.TR_PARAMETROS_GLOBALES
                        where m.IDPARAMETRO == 6 && m.IDSECCION == 17
                        select m;

            return decimal.Parse(monto.FirstOrDefault().VALOR);
        }

        

        public bool ProveedorTecAdquisicion(int ID)
        {
            var pro = from k in _contexto.K_PROVEEDOR_PRODUCTO
                      join s in _contexto.CAT_PRODUCTO on k.Cve_Producto equals s.Cve_Producto
                      join p in _contexto.CAT_PROVEEDOR on k.Id_Proveedor equals p.Id_Proveedor
                      join t in _contexto.CAT_TECNOLOGIA on s.Cve_Tecnologia equals t.Cve_Tecnologia into pt
                      from t in pt.DefaultIfEmpty()
                      where t.Cve_Tipo_Movimiento == "1" 
                      && p.Id_Proveedor == ID
                      select k;
            if (pro.ToList() != null && pro.ToList().Count > 1)
                return true;
            else
                return false;
        }

        public string TipoMovimiento(int ID)
        {
            var t = from s in _contexto.CAT_TECNOLOGIA
                    where s.Cve_Tecnologia == ID
                    select s;
            return t.FirstOrDefault().Cve_Tipo_Movimiento;
        }

        public List<CAT_TECNOLOGIA> ObtieneTecnologiasAquisicion(int CveTarifa, int idProveedor)
        {
            var tecnologia = new List<CAT_TECNOLOGIA>();

            using (var r = new Repositorio<CAT_TECNOLOGIA>())
            {
                tecnologia = (from tecnologias in _contexto.CAT_TECNOLOGIA
                              join tar in _contexto.CAT_TARIFAS_X_TECNOLOGIA
                                on tecnologias.Cve_Tecnologia equals tar.Cve_tecnologia
                              join p in _contexto.CAT_PRODUCTO
                                on tecnologias.Cve_Tecnologia equals p.Cve_Tecnologia
                              join k2 in _contexto.K_PROVEEDOR_PRODUCTO
                                on p.Cve_Producto equals k2.Cve_Producto
                              where tar.Cve_Tarifa == CveTarifa
                              && k2.Id_Proveedor == idProveedor && tecnologias.Cve_Tipo_Movimiento=="1"
                              select tecnologias).Distinct().ToList<CAT_TECNOLOGIA>();
            }

            return tecnologia;
        }

        public List<TR_PARAMETROS_GLOBALES> CorreoUsuario()
        {
            var us2 = (from u in _contexto.TR_PARAMETROS_GLOBALES
                      where u.IDSECCION == 18
                      select u).ToList();

            return us2;
        }


    }
}
