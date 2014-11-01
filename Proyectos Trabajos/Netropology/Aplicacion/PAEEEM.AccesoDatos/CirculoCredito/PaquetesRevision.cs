using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.Entidades;
using PAEEEM.Entidades.CirculoCredito;


namespace PAEEEM.AccesoDatos.CirculoCredito
{
    public class PaquetesRevision
    {
        public static List<ConsultaPaquetes> datosRevision(int status,string credit, int Nopaquete,string folio,string dist,string usu)
        {
            PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();

            var query = (from cir in _contexto.PAQUETES_CIRCULO_CREDITO
                         join cre in _contexto.CRE_Credito on cir.No_Credito equals cre.No_Credito
                         join staPaq in _contexto.CAT_ESTATUS_PAQUETE on cir.ID_ESTATUS_PAQUETE equals staPaq.ID_ESTATUS_PAQUETE
                         join catPb in _contexto.CAT_PROVEEDORBRANCH on cre.Id_Branch equals catPb.Id_Branch into pb
                         from catPb in pb.DefaultIfEmpty()
                         join catP in _contexto.CAT_PROVEEDOR on cre.Id_Proveedor equals catP.Id_Proveedor
                         join usuarios in _contexto.US_USUARIO on (cre.Id_Branch == 0 ? catP.Cve_Zona : catPb.Cve_Zona) equals usuarios.Id_Departamento
                         

                         where usuarios.Nombre_Usuario==usu
                          && (status == 0 ? 1 == 1 : cir.ID_ESTATUS_PAQUETE == status)
                          && (credit == "" ? 1 == 1 : cir.No_Credito == credit)
                          && (Nopaquete == 0 ? 1 == 1 : cir.NO_PAQUETE == Nopaquete)
                          && (folio == "seleccione" ? 1 == 1 : cir.FOLIO == folio)
                          && (dist == "seleccione" ? 1 == 1 : dist == (cre.Id_Branch == 0 ? catP.Dx_Nombre_Comercial : catPb.Dx_Nombre_Comercial))

                         select new ConsultaPaquetes
                         {
                             status = staPaq.DESCRIPCION,
                             distribuidor=cre.Id_Branch == 0 ? catP.Dx_Nombre_Comercial : catPb.Dx_Nombre_Comercial,
                             noCredit = cir.No_Credito,
                             folioConsulta = cre.Folio_Consulta,
                             fechaConsulta = (DateTime)cre.Fecha_Consulta,
                             noPakete = cir.NO_PAQUETE,
                             folioPakete = cir.FOLIO,
                             fechaRevision = cir.FECHA_REVISION,
                             carta = cir.Carta_Autorizacion,
                             acta = cir.Acta_Ministerio
                             ///

                         }).OrderBy(o => o.folioPakete).ToList();




            return query;
        }


      ///////////
        public static List<PAQUETES_CIRCULO_CREDITO> catLofios(string usu)
        {
            PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();

            var query = (from cir in _contexto.PAQUETES_CIRCULO_CREDITO
                         join cre in _contexto.CRE_Credito on cir.No_Credito equals cre.No_Credito
                         join catPb in _contexto.CAT_PROVEEDORBRANCH on cre.Id_Branch equals catPb.Id_Branch into pb
                         from catPb in pb.DefaultIfEmpty()
                         join catP in _contexto.CAT_PROVEEDOR on cre.Id_Proveedor equals catP.Id_Proveedor
                         join usuarios in _contexto.US_USUARIO on (cre.Id_Branch == 0 ? catP.Cve_Zona : catPb.Cve_Zona) equals usuarios.Id_Departamento

                         where cir.ID_ESTATUS_PAQUETE==2 && usuarios.Nombre_Usuario==usu
                         select cir//new PAQUETES_CIRCULO_CREDITO{}
                         ).GroupBy(o=>o.FOLIO).Select(o=>o.FirstOrDefault()).ToList();

            return query;
        }

        public static List<PAQUETES_CIRCULO_CREDITO> catLofiosDis(string usu)
        {
            PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();

            var query = (from cir in _contexto.PAQUETES_CIRCULO_CREDITO
                         join cre in _contexto.CRE_Credito on cir.No_Credito equals cre.No_Credito
                         join catPb in _contexto.CAT_PROVEEDORBRANCH on cre.Id_Branch equals catPb.Id_Branch into pb
                         from catPb in pb.DefaultIfEmpty()
                         join catP in _contexto.CAT_PROVEEDOR on cre.Id_Proveedor equals catP.Id_Proveedor
                         join usuarios in _contexto.US_USUARIO on (cre.Id_Branch == 0 ? catP.Id_Proveedor : catPb.Id_Branch) equals usuarios.Id_Departamento

                         where cir.ID_ESTATUS_PAQUETE == 2 && usuarios.Nombre_Usuario == usu
                         select cir//new PAQUETES_CIRCULO_CREDITO{}
                         ).GroupBy(o => o.FOLIO).Select(o => o.FirstOrDefault()).ToList();

            return query;
        }
        
        public static List<PAQUETES_CIRCULO_CREDITO> catPaquetesXFolio(string folio, int status)
        {
            PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();

            var query = (from cir in _contexto.PAQUETES_CIRCULO_CREDITO
                         where cir.FOLIO==folio && cir.ID_ESTATUS_PAQUETE==status
                         select cir//new PAQUETES_CIRCULO_CREDITO{}
                         ).GroupBy(o => o.NO_PAQUETE).Select(o => o.FirstOrDefault()).ToList();

            return query;
        }

        public static List<PAQUETES_CIRCULO_CREDITO> catPaquetesXFoliorevi(string folio)
        {
            PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();

            var query = (from cir in _contexto.PAQUETES_CIRCULO_CREDITO
                         where cir.FOLIO==folio&&cir.ID_ESTATUS_PAQUETE==2
                         select cir//new PAQUETES_CIRCULO_CREDITO{}
                         ).GroupBy(o => o.NO_PAQUETE).Select(o => o.FirstOrDefault()).ToList();

            return query;
        }

        public static List<ConsultaPaquetes> catDistXfoliNopaque(int status,string folio, int nopak,string usu)
        {
            PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();

            var query = (from cir in _contexto.PAQUETES_CIRCULO_CREDITO
                         join cre in _contexto.CRE_Credito on cir.No_Credito equals cre.No_Credito
                         join staPaq in _contexto.CAT_ESTATUS_PAQUETE on cir.ID_ESTATUS_PAQUETE equals staPaq.ID_ESTATUS_PAQUETE
                         join catPb in _contexto.CAT_PROVEEDORBRANCH on cre.Id_Branch equals catPb.Id_Branch into pb
                         from catPb in pb.DefaultIfEmpty()
                         join catP in _contexto.CAT_PROVEEDOR on cre.Id_Proveedor equals catP.Id_Proveedor
                         join usuarios in _contexto.US_USUARIO on (cre.Id_Branch == 0 ? catP.Cve_Zona : catPb.Cve_Zona) equals usuarios.Id_Departamento

                         where cir.FOLIO==folio && cir.NO_PAQUETE==nopak && cir.ID_ESTATUS_PAQUETE==status && usuarios.Nombre_Usuario==usu
                         select new ConsultaPaquetes
                         {
                             distribuidor = cre.Id_Branch == 0 ? catP.Dx_Nombre_Comercial : catPb.Dx_Nombre_Comercial
                             ///

                         }).GroupBy(o => o.distribuidor).Select(o=>o.FirstOrDefault()).ToList();
            return query;
        }

        public static List<ConsultaPaquetes> catDist(string usu)
        {
            PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();

            var query = (from cre in _contexto.CRE_Credito 
                         join catPb in _contexto.CAT_PROVEEDORBRANCH on cre.Id_Branch equals catPb.Id_Branch into pb
                         from catPb in pb.DefaultIfEmpty()
                         join catP in _contexto.CAT_PROVEEDOR on cre.Id_Proveedor equals catP.Id_Proveedor
                         join usuarios in _contexto.US_USUARIO on (cre.Id_Branch == 0 ? catP.Cve_Zona : catPb.Cve_Zona) equals usuarios.Id_Departamento


                         where usuarios.Nombre_Usuario == usu
                         select new ConsultaPaquetes
                         {
                             distribuidor = cre.Id_Branch == 0 ? catP.Dx_Nombre_Comercial : catPb.Dx_Nombre_Comercial
                             ///

                         }).GroupBy(o => o.distribuidor).Select(o => o.FirstOrDefault()).ToList();
            return query;
        }

        
        public static List<ConsultaPaquetes> ConsultaPaqAceptados(int status, string credit, int Nopaquete, string folio, string dist,string usu)
        {
            PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();

            var query = (from cir in _contexto.PAQUETES_CIRCULO_CREDITO
                         join cre in _contexto.CRE_Credito on cir.No_Credito equals cre.No_Credito
                         join staPaq in _contexto.CAT_ESTATUS_PAQUETE on cir.ID_ESTATUS_PAQUETE equals staPaq.ID_ESTATUS_PAQUETE
                         join catPb in _contexto.CAT_PROVEEDORBRANCH on cre.Id_Branch equals catPb.Id_Branch into pb
                         from catPb in pb.DefaultIfEmpty()
                         join catP in _contexto.CAT_PROVEEDOR on cre.Id_Proveedor equals catP.Id_Proveedor
                         join usuarios in _contexto.US_USUARIO on (cre.Id_Branch == 0 ? catP.Cve_Zona : catPb.Cve_Zona) equals usuarios.Id_Departamento
                         
                         where usuarios.Nombre_Usuario==usu 
                          && (status == 0 ? 1 == 1 : cir.ID_ESTATUS_PAQUETE == status)
                          && (credit == "" ? 1 == 1 : cir.No_Credito == credit)
                          && (Nopaquete == 0 ? 1 == 1 : cir.NO_PAQUETE == Nopaquete)
                          && (folio == "" ? 1 == 1 : cir.FOLIO == folio)
                          && (dist == "seleccione" ? 1 == 1 : dist == (cre.Id_Branch == 0 ? catP.Dx_Nombre_Comercial : catPb.Dx_Nombre_Comercial))


                         select new ConsultaPaquetes
                         {
                             status = staPaq.DESCRIPCION,
                             distribuidor = cre.Id_Branch == 0 ? catP.Dx_Nombre_Comercial : catPb.Dx_Nombre_Comercial,
                             noCredit = cir.No_Credito,
                             folioConsulta = cre.Folio_Consulta,
                             fechaConsulta = (DateTime)cre.Fecha_Consulta,
                             noPakete = cir.NO_PAQUETE,
                             folioPakete = cir.FOLIO,
                             fechaRevision = cir.FECHA_REVISION,
                             carta = cir.Carta_Autorizacion,
                             acta = cir.Acta_Ministerio
                             ///

                         }).OrderBy(o => o.noCredit).ToList();



            return query;
        }


        public static List<ConsultaPaquetes> ConsultaPaq(int status, string credit, int Nopaquete, string folio, string dist, string usu)
        {
            PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();

            var query = (from cre in _contexto.CRE_Credito
                         join cir in _contexto.PAQUETES_CIRCULO_CREDITO on cre.No_Credito equals cir.No_Credito into leftcir
                         from cir in leftcir.DefaultIfEmpty()
                         join staPaq in _contexto.CAT_ESTATUS_PAQUETE on (cir.ID_ESTATUS_PAQUETE == null ? 1 : cir.ID_ESTATUS_PAQUETE) equals staPaq.ID_ESTATUS_PAQUETE into leftsta
                         from staPaq in leftsta.DefaultIfEmpty()
                         join catPb in _contexto.CAT_PROVEEDORBRANCH on cre.Id_Branch equals catPb.Id_Branch into pb
                         from catPb in pb.DefaultIfEmpty()
                         join catP in _contexto.CAT_PROVEEDOR on cre.Id_Proveedor equals catP.Id_Proveedor
                         join usuarios in _contexto.US_USUARIO on (cre.Id_Branch == 0 ? catP.Cve_Zona : catPb.Cve_Zona) equals usuarios.Id_Departamento

                         where (cre.No_Consultas_Crediticias >= 1 && cre.No_MOP != null && cre.Folio_Consulta != null && cre.Fecha_Consulta != null) && usuarios.Nombre_Usuario == usu
                          && (status == 0 ? 1 == 1 : cir.ID_ESTATUS_PAQUETE == status)
                          && (credit == "" ? 1 == 1 : (cir.No_Credito ==null?cre.No_Credito:cir.No_Credito)== credit)
                          && (Nopaquete == 0 ? 1 == 1 : cir.NO_PAQUETE == Nopaquete)
                          && (folio == "" ? 1 == 1 : cir.FOLIO == folio)
                          && (dist == "seleccione" ? 1 == 1 : dist == (cre.Id_Branch == 0 ? catP.Dx_Nombre_Comercial : catPb.Dx_Nombre_Comercial))


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


                        //select new ConsultaPaquetes
                        // {
                        //     noCredit = cre.No_Credito,
                        //     distribuidor = cre.Id_Branch == 0 ? catP.Dx_Nombre_Comercial : catPb.Dx_Nombre_Comercial,
                        //     folioConsulta = cre.Folio_Consulta,
                        //     fechaConsulta = (DateTime)cre.Fecha_Consulta,
                        //     status = staPaq.DESCRIPCION,
                        //     idstatus = (int)staPaq.ID_ESTATUS_PAQUETE
                             
                            
                        // }).OrderBy(o=>o.noCredit).ToList();




            return query.GroupBy(g => g.noCredit).Select(s => s.OrderBy(f => f.noPakete).LastOrDefault()).ToList();
        }


        public static List<ConsultaPaquetes> ConsultaPaqPendientes(int status, string credit, string dist,string usu)
        {
            PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();

            //var query = (from cre in _contexto.CRE_Credito
            //             join cir in _contexto.PAQUETES_CIRCULO_CREDITO on cre.No_Credito equals cir.No_Credito into leftcir
            //             from cir in leftcir.DefaultIfEmpty()
            //             join staPaq in _contexto.CAT_ESTATUS_PAQUETE on (cir.ID_ESTATUS_PAQUETE == null ? 1 : cir.ID_ESTATUS_PAQUETE) equals staPaq.ID_ESTATUS_PAQUETE into leftsta
            //             from staPaq in leftsta.DefaultIfEmpty()
            //             join catPb in _contexto.CAT_PROVEEDORBRANCH on cre.Id_Branch equals catPb.Id_Branch into pb
            //             from catPb in pb.DefaultIfEmpty()
            //             join catP in _contexto.CAT_PROVEEDOR on cre.Id_Proveedor equals catP.Id_Proveedor
            //             join usuarios in _contexto.US_USUARIO on (cre.Id_Branch == 0 ? catP.Cve_Zona : catPb.Cve_Zona) equals usuarios.Id_Departamento
                         
            //             where cre.No_Consultas_Crediticias >= 1 || (cre.No_MOP != null && cre.Folio_Consulta != null && cre.Fecha_Consulta != null) && usuarios.Nombre_Usuario == usu
            //              && (credit == "" ? 1 == 1 : cre.No_Credito == credit)
            //              && (dist == "seleccione" ? 1 == 1 : dist == (cre.Id_Branch == 0 ? catP.Dx_Nombre_Comercial : catPb.Dx_Nombre_Comercial))

            //             select new ConsultaPaquetes
            //             {
            //                 noCredit = cre.No_Credito,
            //                 distribuidor = cre.Id_Branch == 0 ? catP.Dx_Nombre_Comercial : catPb.Dx_Nombre_Comercial,
            //                 folioConsulta = cre.Folio_Consulta,
            //                 fechaConsulta = (DateTime)cre.Fecha_Consulta,
            //                 status = staPaq.DESCRIPCION,
            //                 idstatus=(int)staPaq.ID_ESTATUS_PAQUETE
                             

            //                 ///

            //             }).OrderBy(o => o.noCredit).ToList();


            var query = (from cre in _contexto.CRE_Credito
                         join cir in _contexto.PAQUETES_CIRCULO_CREDITO on cre.No_Credito equals cir.No_Credito into leftcir
                         from cir in leftcir.DefaultIfEmpty()
                         join staPaq in _contexto.CAT_ESTATUS_PAQUETE on ( cir.ID_ESTATUS_PAQUETE==null ? 1 : cir.ID_ESTATUS_PAQUETE) equals staPaq.ID_ESTATUS_PAQUETE into leftsta
                         from staPaq in leftsta.DefaultIfEmpty()
                         join catPb in _contexto.CAT_PROVEEDORBRANCH on cre.Id_Branch equals catPb.Id_Branch into pb
                         from catPb in pb.DefaultIfEmpty()
                         join catP in _contexto.CAT_PROVEEDOR on cre.Id_Proveedor equals catP.Id_Proveedor
                         join usuarios in _contexto.US_USUARIO on (cre.Id_Branch == 0 ? catP.Cve_Zona : catPb.Cve_Zona) equals usuarios.Id_Departamento

                         where (cir.ID_ESTATUS_PAQUETE == 4 || (cre.No_Consultas_Crediticias >= 1 && cre.No_MOP != null && cre.Folio_Consulta != null && cre.Fecha_Consulta != null)) && usuarios.Nombre_Usuario == usu
                         && (credit == "" ? 1 == 1 : cre.No_Credito == credit)
                         && (dist == "seleccione" ? 1 == 1 : dist == (cre.Id_Branch == 0 ? catP.Dx_Nombre_Comercial : catPb.Dx_Nombre_Comercial))
                         select new ConsultaPaquetes
                         {
                             noCredit = cre.No_Credito,
                             distribuidor = cre.Id_Branch == 0 ? catP.Dx_Nombre_Comercial : catPb.Dx_Nombre_Comercial,
                             folioConsulta = cre.Folio_Consulta,
                             fechaConsulta = (DateTime)cre.Fecha_Consulta,
                             status = staPaq.DESCRIPCION,
                             idstatus = (int)staPaq.ID_ESTATUS_PAQUETE
                             ///
                            
                         }).OrderBy(o=>o.noCredit).ToList();


            List<ConsultaPaquetes> Filtro = new List<ConsultaPaquetes>();
            List<ConsultaPaquetes> temp = new List<ConsultaPaquetes>();
            for (int i = 0; i < query.Count; i++)
            {
                List<ConsultaPaquetes> nocredits = query.FindAll(o => o.noCredit == query[i].noCredit);
                if (nocredits != null)
                {

                    if (nocredits.Last().idstatus == 4 && temp.Exists(o => o.noCredit == query[i].noCredit) == true)
                    {
                        Filtro.Add(nocredits.Last());
                    }
                    temp.Add(query.Find(o => o.noCredit == query[i].noCredit));
                }
                if (nocredits.Count == 1 && (query[i].idstatus == 4 || query[i].idstatus == 1))
                {
                    Filtro.Add(query[i]);
                }
            }



            ///////////////////////
            return Filtro.ToList().GroupBy(g => g.noCredit).Select(s => s.LastOrDefault()).ToList();
        }


        public static PAQUETES_CIRCULO_CREDITO ObtienePorId(string folio, int noPack,string credit)
        {

            PAQUETES_CIRCULO_CREDITO Circulo;

            using (var r = new Repositorio<PAQUETES_CIRCULO_CREDITO>())
            {
                Circulo = r.Extraer(cir => cir.FOLIO == folio && cir.NO_PAQUETE == noPack && cir.No_Credito == credit);
            }
            return Circulo;
        }


        public static bool Actualizarpac(PAQUETES_CIRCULO_CREDITO informacion)
        {
            bool actualiza;

            var creInfoGeneral = ObtienePorId(informacion.FOLIO,informacion.NO_PAQUETE,informacion.No_Credito);

            if (creInfoGeneral != null)
            {
                using (var r = new Repositorio<PAQUETES_CIRCULO_CREDITO>())
                {
                    actualiza = r.Actualizar(informacion);
                }
            }
            else
            {
                throw new Exception("Error");
            }

            return actualiza;
        }
    }
}
