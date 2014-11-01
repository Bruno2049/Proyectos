using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.Entidades;
using PAEEEM.Entidades.CirculoCredito;

namespace PAEEEM.AccesoDatos.CirculoCredito
{
    public class PaquetesPendientes
    {
        public PaquetesPendientes(){}
        #region Mostrar
        public static List<DatosPackPendiente> datosPendientes(string usu,int status) 
        {
            PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();


            if (status == 4)
            {
                var query = (from cre in _contexto.CRE_Credito
                             join cir in _contexto.PAQUETES_CIRCULO_CREDITO on cre.No_Credito equals cir.No_Credito into leftcir
                             from cir in leftcir.DefaultIfEmpty()
                             join staPaq in _contexto.CAT_ESTATUS_PAQUETE on (cir.ID_ESTATUS_PAQUETE == null ? 1 : cir.ID_ESTATUS_PAQUETE) equals staPaq.ID_ESTATUS_PAQUETE into leftsta
                             from staPaq in leftsta.DefaultIfEmpty()
                             join catPb in _contexto.CAT_PROVEEDORBRANCH on cre.Id_Branch equals catPb.Id_Branch into pb
                             from catPb in pb.DefaultIfEmpty()
                             join catP in _contexto.CAT_PROVEEDOR on cre.Id_Proveedor equals catP.Id_Proveedor
                             join usuarios in _contexto.US_USUARIO on (cre.Id_Branch == 0 ? catP.Id_Proveedor : catPb.Id_Branch) equals usuarios.Id_Departamento

                             where   usuarios.Nombre_Usuario == usu
                             select new DatosPackPendiente
                             {
                                 Nocredit = cre.No_Credito,
                                 folioConsulta = cre.Folio_Consulta,
                                 fechaConsulta = (DateTime)cre.Fecha_Consulta,
                                 statusPaquete = staPaq.DESCRIPCION,
                                 idstatus = (int)staPaq.ID_ESTATUS_PAQUETE,
                                 comentarios = cir.Comentarios,
                                 ///
                                 fechaAdicion=cir.FECHA_ADICION,
                                 x = cir.NO_PAQUETE == null ? 0 : cir.NO_PAQUETE

                             }).OrderBy(o => o.Nocredit).ToList();


                List<DatosPackPendiente> Filtro = new List<DatosPackPendiente>();
                //List<DatosPackPendiente> temp = new List<DatosPackPendiente>();
                //for (int i = 0; i < query.Count; i++)
                //{
                //    List<DatosPackPendiente> nocredits = query.FindAll(o => o.Nocredit == query[i].Nocredit);
                //    if (nocredits != null)
                //    {

                //        if (nocredits.LastOrDefault().idstatus == 4 && temp.Exists(o => o.Nocredit == query[i].Nocredit) == true)
                //        {
                //            Filtro.Add(nocredits.LastOrDefault());
                //        }
                //        temp.Add(query.Find(o => o.Nocredit == query[i].Nocredit));
                //    }
                //    if (nocredits.Count == 1 && (query[i].idstatus == 4))
                //    {
                //        Filtro.Add(query[i]);
                //    }
                //}

                
                //var r = query.GroupBy(g => g.Nocredit).Select(s => s.OrderBy(f=>f.fechaAdicion)).ToList();
                //r.OrderBy(r=>r.)

                var t = query.GroupBy(g => g.Nocredit).Select(s => s.OrderBy(f=>f.x).LastOrDefault()).ToList();

                for (int i = 0; i < t.Count; i++)
                {
                    if (t[i].idstatus == 4)
                    {
                        Filtro.Add(t[i]);
                    }
                }


                return Filtro.ToList();
            }
            else
            {


                var query = (from cre in _contexto.CRE_Credito
                             join cir in _contexto.PAQUETES_CIRCULO_CREDITO on cre.No_Credito equals cir.No_Credito into leftcir
                             from cir in leftcir.DefaultIfEmpty()
                             join staPaq in _contexto.CAT_ESTATUS_PAQUETE on (cir.ID_ESTATUS_PAQUETE == null ? 1 : cir.ID_ESTATUS_PAQUETE) equals staPaq.ID_ESTATUS_PAQUETE into leftsta
                             from staPaq in leftsta.DefaultIfEmpty()
                             join catPb in _contexto.CAT_PROVEEDORBRANCH on cre.Id_Branch equals catPb.Id_Branch into pb
                             from catPb in pb.DefaultIfEmpty()
                             join catP in _contexto.CAT_PROVEEDOR on cre.Id_Proveedor equals catP.Id_Proveedor
                             join usuarios in _contexto.US_USUARIO on (cre.Id_Branch == 0 ? catP.Id_Proveedor : catPb.Id_Branch) equals usuarios.Id_Departamento

                             where (cir.ID_ESTATUS_PAQUETE == 4 || (cre.No_Consultas_Crediticias >= 1 && cre.No_MOP != null && cre.Folio_Consulta != null && cre.Fecha_Consulta != null)) && usuarios.Nombre_Usuario == usu

                             select new DatosPackPendiente
                             {
                                 Nocredit = cre.No_Credito,
                                 folioConsulta = cre.Folio_Consulta,
                                 fechaConsulta = (DateTime)cre.Fecha_Consulta,
                                 statusPaquete = staPaq.DESCRIPCION,
                                 idstatus = (int)staPaq.ID_ESTATUS_PAQUETE,
                                 comentarios = cir.Comentarios,
                                 ///
                                 fechaAdicion=cir.FECHA_ADICION,
                                 x = cir.NO_PAQUETE == null ? 0 : cir.NO_PAQUETE
                             }).OrderBy(o => o.Nocredit).ToList();


                List<DatosPackPendiente> Filtro = new List<DatosPackPendiente>();
                //List<DatosPackPendiente> temp = new List<DatosPackPendiente>();
                //for (int i = 0; i < query.Count; i++)
                //{
                //    List<DatosPackPendiente> nocredits = query.FindAll(o => o.Nocredit == query[i].Nocredit);
                //    if (nocredits != null)
                //    {

                //        if (nocredits.LastOrDefault().idstatus == 4 && temp.Exists(o => o.Nocredit == query[i].Nocredit) == true)
                //        {
                //            Filtro.Add(nocredits.LastOrDefault());
                //        }
                //        temp.Add(query.Find(o => o.Nocredit == query[i].Nocredit));
                //    }
                //    if (nocredits.Count == 1 && (query[i].idstatus == 4 || query[i].idstatus == 1))
                //    {
                //        Filtro.Add(query[i]);
                //    }

                //}
                var t = query.GroupBy(g => g.Nocredit).Select(s => s.OrderBy(f => f.x).LastOrDefault()).ToList();

                for (int i = 0; i < t.Count; i++)
                {
                    if (t[i].idstatus == 4|| t[i].idstatus==1)
                    {
                        Filtro.Add(t[i]);
                    }
                }


                return Filtro.ToList();


            }


        }

        public static List<ConsultaPaquetes> ConsultaPaquetes(int status, string credit, int Nopaquete, string folio, string usu)
        {
            PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();

            if (status == 4)
            {
                var query = (from cre in _contexto.CRE_Credito
                             join cir in _contexto.PAQUETES_CIRCULO_CREDITO on cre.No_Credito equals cir.No_Credito into leftcir
                             from cir in leftcir.DefaultIfEmpty()
                             join staPaq in _contexto.CAT_ESTATUS_PAQUETE on (cir.ID_ESTATUS_PAQUETE == null ? 1 : cir.ID_ESTATUS_PAQUETE) equals staPaq.ID_ESTATUS_PAQUETE into leftsta
                             from staPaq in leftsta.DefaultIfEmpty()
                             join catPb in _contexto.CAT_PROVEEDORBRANCH on cre.Id_Branch equals catPb.Id_Branch into pb
                             from catPb in pb.DefaultIfEmpty()
                             join catP in _contexto.CAT_PROVEEDOR on cre.Id_Proveedor equals catP.Id_Proveedor
                             join usuarios in _contexto.US_USUARIO on (cre.Id_Branch == 0 ? catP.Id_Proveedor : catPb.Id_Branch) equals usuarios.Id_Departamento

                             where (cir.ID_ESTATUS_PAQUETE == 4 || cre.No_Consultas_Crediticias >= 1 ) && usuarios.Nombre_Usuario == usu
                              && (credit == "" ? 1 == 1 : cir.No_Credito == credit)
                              && (Nopaquete == 0 ? 1 == 1 : cir.NO_PAQUETE == Nopaquete)
                              && (folio == "" ? 1 == 1 : cir.FOLIO == folio)
                             select new ConsultaPaquetes
                             {
                                 status = staPaq.DESCRIPCION,
                                 noCredit = cir.No_Credito,
                                 folioConsulta = cre.Folio_Consulta,
                                 fechaConsulta = (DateTime)cre.Fecha_Consulta,
                                 noPakete = cir.NO_PAQUETE == null ? 0 : cir.NO_PAQUETE,
                                 folioPakete = cir.FOLIO == null ? "" : cir.FOLIO,
                                 idstatus = (int)staPaq.ID_ESTATUS_PAQUETE
                                 ///

                             }).OrderBy(o => o.noCredit).ToList();


                List<ConsultaPaquetes> Filtro = new List<ConsultaPaquetes>();
                List<ConsultaPaquetes> temp = new List<ConsultaPaquetes>();
                for (int i = 0; i < query.Count; i++)
                {
                    List<ConsultaPaquetes> nocredits = query.FindAll(o => o.noCredit == query[i].noCredit);
                    if (nocredits != null)
                    {

                        if (nocredits.LastOrDefault().idstatus == 4 && temp.Exists(o => o.noCredit == query[i].noCredit) == true)
                        {
                            Filtro.Add(nocredits.LastOrDefault());
                        }
                        temp.Add(query.Find(o => o.noCredit == query[i].noCredit));
                    }
                    if (nocredits.Count == 1 && (query[i].idstatus == 4))
                    {
                        Filtro.Add(query[i]);
                    }
                }

                return Filtro.GroupBy(g => g.noCredit).Select(s => s.LastOrDefault()).ToList();

            }else  if (status==0)
            {
                var query = (from cre in _contexto.CRE_Credito
                         join cir in _contexto.PAQUETES_CIRCULO_CREDITO on cre.No_Credito equals cir.No_Credito into leftcir
                         from cir in leftcir.DefaultIfEmpty()
                         join staPaq in _contexto.CAT_ESTATUS_PAQUETE on (cir.ID_ESTATUS_PAQUETE == null ? 1 : cir.ID_ESTATUS_PAQUETE) equals staPaq.ID_ESTATUS_PAQUETE into leftsta
                         from staPaq in leftsta.DefaultIfEmpty()
                         join catPb in _contexto.CAT_PROVEEDORBRANCH on cre.Id_Branch equals catPb.Id_Branch into pb
                         from catPb in pb.DefaultIfEmpty()
                         join catP in _contexto.CAT_PROVEEDOR on cre.Id_Proveedor equals catP.Id_Proveedor
                         join usuarios in _contexto.US_USUARIO on (cre.Id_Branch == 0 ? catP.Id_Proveedor : catPb.Id_Branch) equals usuarios.Id_Departamento

                         where (cre.No_Consultas_Crediticias >= 1 && cre.No_MOP != null && cre.Folio_Consulta != null && cre.Fecha_Consulta != null) && usuarios.Nombre_Usuario == usu
                          && (status == 0 ? 1 == 1 : cir.ID_ESTATUS_PAQUETE == status)
                          && (credit == "" ? 1 == 1 : (cir.No_Credito ==null?cre.No_Credito:cir.No_Credito)== credit)
                          && (Nopaquete == 0 ? 1 == 1 : cir.NO_PAQUETE == Nopaquete)
                          && (folio == "" ? 1 == 1 : cir.FOLIO == folio)

                         select new ConsultaPaquetes
                         {
                             status = staPaq.DESCRIPCION,
                             distribuidor = cre.Id_Branch == 0 ? catP.Dx_Nombre_Comercial : catPb.Dx_Nombre_Comercial,
                             noCredit = cre.No_Credito,
                             folioConsulta = cre.Folio_Consulta,
                             fechaConsulta = (DateTime)cre.Fecha_Consulta,
                             noPakete = cir.NO_PAQUETE==null?0:cir.NO_PAQUETE,
                             folioPakete = cir.FOLIO,
                             fechaRevision = cir.FECHA_REVISION,
                             carta = cir.Carta_Autorizacion,
                             acta = cir.Acta_Ministerio
                             ///

                         }).OrderBy(o => o.noCredit).ToList();




            return query.GroupBy(g=>g.noCredit).Select(s=>s.LastOrDefault()).ToList();
            }
            else
            {
                var query1 = (from cir in _contexto.PAQUETES_CIRCULO_CREDITO
                              join cre in _contexto.CRE_Credito on cir.No_Credito equals cre.No_Credito 
                              join staPaq in _contexto.CAT_ESTATUS_PAQUETE on cir.ID_ESTATUS_PAQUETE  equals staPaq.ID_ESTATUS_PAQUETE 
                              join catPb in _contexto.CAT_PROVEEDORBRANCH on cre.Id_Branch equals catPb.Id_Branch into pb
                              from catPb in pb.DefaultIfEmpty()
                              join catP in _contexto.CAT_PROVEEDOR on cre.Id_Proveedor equals catP.Id_Proveedor
                              join usuarios in _contexto.US_USUARIO on (cre.Id_Branch == 0 ? catP.Id_Proveedor : catPb.Id_Proveedor) equals usuarios.Id_Departamento

                              where usuarios.Nombre_Usuario == usu
                               && (status == 0 ? 1 == 1 : cir.ID_ESTATUS_PAQUETE == status)
                               && (credit == "" ? 1 == 1 : cir.No_Credito == credit)
                               && (Nopaquete == 0 ? 1 == 1 : cir.NO_PAQUETE == Nopaquete)
                               && (folio == "" ? 1 == 1 : cir.FOLIO == folio)

                              select new ConsultaPaquetes
                              {
                                  status = staPaq.DESCRIPCION,
                                  noCredit = cir.No_Credito,
                                  folioConsulta = cre.Folio_Consulta,
                                  fechaConsulta = (DateTime)cre.Fecha_Consulta,
                                  noPakete = cir.NO_PAQUETE,
                                  folioPakete = cir.FOLIO,
                                  fechaRevision = cir.FECHA_REVISION == null ? null : cir.FECHA_REVISION,
                                  carta = cir.Carta_Autorizacion,
                                  acta = cir.Acta_Ministerio,
                                  idstatus = (int)staPaq.ID_ESTATUS_PAQUETE
                                  ///

                              }).OrderBy(o => o.noCredit).ToList();

                return query1;
            }


        }


        public static int zona(int usu)
        {
            PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();

            var query = (from us in _contexto.US_USUARIO
                         join zon in _contexto.CAT_ZONA on us.Id_Departamento equals zon.Cve_Zona
                         where us.Id_Usuario == usu

                         select new DatosPackPendiente
                         {
                             x = zon.Cve_Zona
                         });
            int idzon = query.SingleOrDefault().x;
            return idzon;
        }

        public static List<DatosPackPendiente> noPaquete(string folio, string usu)
        {
            PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();

            var query = (from paq in _contexto.PAQUETES_CIRCULO_CREDITO
                         where paq.FOLIO == folio && paq.ADICIONADO_POR == usu

                         select new DatosPackPendiente
                         {
                             x = paq.NO_PAQUETE
                         }).OrderBy(o => o.x).ToList();
            return query;
        }
        #endregion

        #region MostrarCatStatus
        public static List<CAT_ESTATUS_PAQUETE> catalogo() 
        {
            PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();
            List<CAT_ESTATUS_PAQUETE> n = (from paque in _contexto.CAT_ESTATUS_PAQUETE
                                                select paque).ToList();
            return n;
        }

        public static List<CAT_ESTATUS_PAQUETE> catalogoEstatusZona()
        {
            PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();
            List<CAT_ESTATUS_PAQUETE> n = (from paque in _contexto.CAT_ESTATUS_PAQUETE
                                           where paque.ID_ESTATUS_PAQUETE >=1 && paque.ID_ESTATUS_PAQUETE<=3
                                           select paque).ToList();
            return n;
        }
        #endregion

        #region guardar
        private readonly PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();

        public bool GuardaPaquete(PAQUETES_CIRCULO_CREDITO circulo)
        {
            using (var context = new PAEEEM_DESAEntidades())
            {
                if (context.PAQUETES_CIRCULO_CREDITO.Any(pa => pa.FOLIO != circulo.FOLIO && pa.ADICIONADO_POR!=circulo.ADICIONADO_POR && pa.NO_PAQUETE!=circulo.NO_PAQUETE && pa.No_Credito!=circulo.No_Credito))
                {
                    context.PAQUETES_CIRCULO_CREDITO.Add(circulo);
                    context.SaveChanges();
                }

            }

            return true;
        }

        public static  void Guardapaquete2(PAQUETES_CIRCULO_CREDITO paquete)
        {
            using (var r = new Repositorio<PAQUETES_CIRCULO_CREDITO>())
            {
                r.Agregar(paquete);
            }
        }
        #endregion


    }
}
