using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.Entidades;
using PAEEEM.Entidades.ModuloCentral;
using PAEEEM.AccesoDatos.Catalogos;
using System.Linq.Expressions;
using PAEEEM.AccesoDatos.SolicitudCredito;


namespace PAEEEM.AccesoDatos.ReactivarCredito
{
    public class DatosRequeridos
    {
        private readonly PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();
        private static readonly DatosRequeridos _classInstance = new DatosRequeridos();
        public static DatosRequeridos ClassInstance { get { return _classInstance; } }


        public List<DATOS_REACTIVACION> DatosObtenidos(string RPU, string NoCre, string Cliente, string NomComercial, int Reg, int Zon)
        {
            int no_reactivaciones = VariablesGlobales.ObtienePorIdValor(1, 17);
            #region consulta
            var query1 = (from cre in _contexto.CRE_Credito
                          join catEc in _contexto.CAT_ESTATUS_CREDITO on cre.Cve_Estatus_Credito equals catEc.Cve_Estatus_Credito
                          join cli in _contexto.CLI_Cliente on new { CP = (int)cre.Id_Proveedor, CB = (int)cre.Id_Branch, CC = (int)cre.IdCliente } equals new { CP = cli.Id_Proveedor, CB = cli.Id_Branch, CC = cli.IdCliente }

                          join can in _contexto.CANCELAR_RECHAZAR on cre.No_Credito equals can.No_Credito 

                          join usu in _contexto.US_USUARIO on can.ADICIONADO_POR  equals usu.Nombre_Usuario 
                          
                          join moti in _contexto.MOTIVOS_RECHAZOS_CANCELACIONES on can.ID_MOTIVO equals moti.ID_MOTIVO

                          join nego in _contexto.CLI_Negocio on new { np = (int)cre.Id_Proveedor, nb = (int)cre.Id_Branch, nc = (int)cre.IdCliente, ng = (int)cre.IdNegocio } equals new { np = (int)nego.Id_Proveedor, nb = (int)nego.Id_Branch, nc = (int)nego.IdCliente, ng = (int)nego.IdNegocio }
                          
                          join catPb in _contexto.CAT_PROVEEDORBRANCH on cre.Id_Branch equals catPb.Id_Branch into pb
                          from catPb in pb.DefaultIfEmpty()

                          join catP in _contexto.CAT_PROVEEDOR on cre.Id_Proveedor equals catP.Id_Proveedor 

                          join catR in _contexto.CAT_REGION on (cre.Id_Branch == 0 ? catP.Cve_Region : catPb.Cve_Region) equals catR.Cve_Region
                          join catZ in _contexto.CAT_ZONA on (cre.Id_Branch == 0 ? catP.Cve_Zona : catPb.Cve_Zona) equals catZ.Cve_Zona
                          join catEP in _contexto.CAT_ESTATUS_PROVEEDOR on (cre.Id_Branch==0?catP.Cve_Estatus_Proveedor:catPb.Cve_Estatus_Proveedor) equals catEP.Cve_Estatus_Proveedor
                          

                          where cre.Cve_Estatus_Credito == 7 && cre.Consumo_Promedio!=null
                          && catEP.Cve_Estatus_Proveedor == 2
                          && (cre.No_Reactivaciones == null ? 0 : cre.No_Reactivaciones) < no_reactivaciones
                          && (usu.Id_Rol == 1 || usu.Id_Rol == 2 || usu.Id_Rol == 6)
                          && (RPU==""?1==1:cre.RPU==RPU)
                          && (NoCre == "" ? 1 == 1 : cre.No_Credito == NoCre)
                          && (Cliente == "" ? 1 == 1 : cli.Nombre + " " + cli.Ap_Paterno + " " + cli.Ap_Materno == Cliente)
                          && (NomComercial == "" ? 1 == 1 : nego.Nombre_Comercial == NomComercial)
                          && (Reg == 0 ? 1 == 1 : catR.Cve_Region == Reg)
                          && (Zon == 0 ? 1 == 1 : catZ.Cve_Zona == Zon)

                          select new DATOS_REACTIVACION
                    {
                        RPU = cre.RPU,
                        NoCredito = cre.No_Credito,
                        Cliente =cli.Nombre==null?cli.Razon_Social: cli.Nombre + " " + cli.Ap_Paterno + " " + cli.Ap_Materno,
                        NomComercial = nego.Nombre_Comercial,
                        FechCancelacion = (DateTime)cre.Fecha_Cancelado,
                        UltimoStatus = cre.Fecha_Autorizado == null ? (cre.Fecha_En_revision == null ? (cre.Fecha_Por_entregar == null ? "PENDIENTE" : "POR ENTREGAR") : "EN REVISION") : "AUTORIZADO",
                        MotivoCancel = moti.DESCRIPCION,
                        Region = catR.Dx_Nombre_Region,
                        Zona = catZ.Dx_Nombre_Zona,
                        ///
                        Fecha_Consulta = (DateTime)cre.Fecha_Consulta,
                        Monto_Solicitado = (decimal)cre.Monto_Solicitado,
                        RFC = cli.RFC,
                        fechaMotiCancel = can.FECHA_ADICION,
                        NoCreditoMotivoCancel = can.No_Credito


                    }).OrderBy(t => t.RPU);

            var query2 = (from cre in _contexto.CRE_Credito
                          join catEc in _contexto.CAT_ESTATUS_CREDITO on cre.Cve_Estatus_Credito equals catEc.Cve_Estatus_Credito
                          join cli in _contexto.CLI_Cliente on new { CP = (int)cre.Id_Proveedor, CB = (int)cre.Id_Branch, CC = (int)cre.IdCliente } equals new { CP = cli.Id_Proveedor, CB = cli.Id_Branch, CC = cli.IdCliente }

                          join bita in _contexto.BitacoraSis on cre.No_Credito equals bita.No_Credito 

                          join nego in _contexto.CLI_Negocio on new { np = (int)cre.Id_Proveedor, nb = (int)cre.Id_Branch, nc = (int)cre.IdCliente, ng = (int)cre.IdNegocio } equals new { np = (int)nego.Id_Proveedor, nb = (int)nego.Id_Branch, nc = (int)nego.IdCliente, ng = (int)nego.IdNegocio }

                          join catPb in _contexto.CAT_PROVEEDORBRANCH on cre.Id_Branch equals catPb.Id_Branch into pb
                          from catPb in pb.DefaultIfEmpty()

                          join catP in _contexto.CAT_PROVEEDOR on cre.Id_Proveedor equals catP.Id_Proveedor

                          join catR in _contexto.CAT_REGION on (cre.Id_Branch == 0 ? catP.Cve_Region : catPb.Cve_Region) equals catR.Cve_Region
                          join catZ in _contexto.CAT_ZONA on (cre.Id_Branch == 0 ? catP.Cve_Zona : catPb.Cve_Zona) equals catZ.Cve_Zona
                          join catEP in _contexto.CAT_ESTATUS_PROVEEDOR on (cre.Id_Branch == 0 ? catP.Cve_Estatus_Proveedor : catPb.Cve_Estatus_Proveedor) equals catEP.Cve_Estatus_Proveedor


                          where cre.Cve_Estatus_Credito == 7 && cre.Consumo_Promedio!=null
                          && catEP.Cve_Estatus_Proveedor == 2
                          && (cre.No_Reactivaciones == null ? 0 : cre.No_Reactivaciones) < no_reactivaciones
                          && (bita.Motivo == "VENCIMIENTO DE PRIMER FECHA DE AMORTIZACIÓN" || bita.Motivo == "POR 30 DIAS DE INACTIVIDAD" )
                          && (RPU == "" ? 1 == 1 : cre.RPU == RPU)
                          && (NoCre == "" ? 1 == 1 : cre.No_Credito == NoCre)
                          && (Cliente == "" ? 1 == 1 : cli.Nombre + " " + cli.Ap_Paterno + " " + cli.Ap_Materno == Cliente)
                          && (NomComercial == "" ? 1 == 1 : nego.Nombre_Comercial == NomComercial)
                          && (Reg == 0 ? 1 == 1 : catR.Cve_Region == Reg)
                          && (Zon == 0 ? 1 == 1 : catZ.Cve_Zona == Zon)

                          select new DATOS_REACTIVACION
                          {
                              RPU = cre.RPU,
                              NoCredito = cre.No_Credito,
                              Cliente = cli.Nombre == null ? cli.Razon_Social : cli.Nombre + " " + cli.Ap_Paterno + " " + cli.Ap_Materno,
                              NomComercial = nego.Nombre_Comercial,
                              FechCancelacion = (DateTime)cre.Fecha_Cancelado,
                              UltimoStatus = cre.Fecha_Autorizado == null ? (cre.Fecha_En_revision == null ? (cre.Fecha_Por_entregar == null ? "PENDIENTE" : "POR ENTREGAR") : "EN REVISION") : "AUTORIZADO",
                              MotivoCancel = bita.Motivo,
                              Region = catR.Dx_Nombre_Region,
                              Zona = catZ.Dx_Nombre_Zona,
                              ///
                              Fecha_Consulta = (DateTime)cre.Fecha_Consulta,
                              Monto_Solicitado = (decimal)cre.Monto_Solicitado,
                              RFC = cli.RFC,
                              fechaMotiCancel = bita.Fecha,
                              NoCreditoMotivoCancel = bita.No_Credito


                          }).OrderBy(t => t.RPU);
            #endregion
            #region codigo
            //var datos = (from cre in _contexto.CRE_Credito
            //             join estatus in _contexto.CAT_ESTATUS_CREDITO on cre.Cve_Estatus_Credito equals estatus.Cve_Estatus_Credito
            //             join cli in _contexto.CLI_Cliente on new { CP = (int)cre.Id_Proveedor, CB = (int)cre.Id_Branch, CC = (int)cre.IdCliente } equals new { CP = (int)cli.Id_Proveedor, CB = (int)cli.Id_Branch, CC = (int)cli.IdCliente }
            //             join bita in _contexto.BitacoraSis on cre.No_Credito equals bita.No_Credito into leftbita
            //             from bita in leftbita.DefaultIfEmpty()
            //             join can in _contexto.CANCELAR_RECHAZAR on cre.No_Credito equals can.No_Credito into leftcan
            //             from can in leftcan.DefaultIfEmpty()
            //             join motivo in _contexto.MOTIVOS_RECHAZOS_CANCELACIONES on can.ID_MOTIVO equals motivo.ID_MOTIVO into leftmoti
            //             from motivo in leftmoti.DefaultIfEmpty()
            //             join usu in _contexto.US_USUARIO on can.ADICIONADO_POR equals usu.Nombre_Usuario into leftusu
            //             from usu in leftusu.DefaultIfEmpty()
            //             join nego in _contexto.CLI_Negocio on new { NP = (int)cre.Id_Proveedor, NB = (int)cre.Id_Branch, NC = (int)cre.IdCliente, NN = (int)cre.IdNegocio } equals new { NP = (int)nego.Id_Proveedor, NB = (int)nego.Id_Branch, NC = (int)nego.IdCliente, NN = (int)nego.IdNegocio }
            //             join provBra in _contexto.CAT_PROVEEDORBRANCH on cre.Id_Branch equals provBra.Id_Branch
            //             join zon in _contexto.CAT_ZONA on provBra.Cve_Zona equals zon.Cve_Zona
            //             join reg in _contexto.CAT_REGION on provBra.Cve_Region equals reg.Cve_Region
            //             join estPro in _contexto.CAT_ESTATUS_PROVEEDOR on provBra.Cve_Estatus_Proveedor equals estPro.Cve_Estatus_Proveedor

            //             where cre.Cve_Estatus_Credito == 7
            //                   && estPro.Dx_Estatus_Proveedor == "ACTIVO"
            //                   && (bita.Motivo == "POR 30 DIAS DE INACTIVIDAD" || bita.Motivo == "VENCIMIENTO DE PRIMER FECHA DE AMORTIZACIÓN")
            //                   && (usu.Id_Rol==1||usu.Id_Rol==2||usu.Id_Rol==6)
            //                   && (cre.No_Reactivaciones == null ? 0 : cre.No_Reactivaciones) < no_reactivaciones
            //                   && (NoCre == "" ? 1 == 1 : cre.No_Credito == NoCre)
            //                   && (RPU == "" ? 1 == 1 : cre.RPU == RPU)
            //                   && (Cliente == "" ? 1 == 1 : cli.Nombre + " " + cli.Ap_Paterno + " " + cli.Ap_Materno == Cliente)
            //                   && (NomComercial == "" ? 1 == 1 : nego.Nombre_Comercial == NomComercial)
            //                   && (Reg == 0 ? 1 == 1 : reg.Cve_Region == Reg)
            //                   && (Zon == 0 ? 1 == 1 : zon.Cve_Zona == Zon)


            //             select new DATOS_REACTIVACION
            //             {
            //                 RPU = cre.RPU,
            //                 NoCredito = cre.No_Credito,
            //                 Cliente = cli.Nombre + " " + cli.Ap_Paterno + " " + cli.Ap_Materno,
            //                 NomComercial = nego.Nombre_Comercial,
            //                 FechCancelacion = (DateTime)cre.Fecha_Cancelado,
            //                 UltimoStatus = cre.Fecha_Autorizado == null ? (cre.Fecha_En_revision == null ? (cre.Fecha_Por_entregar == null ? "PENDIENTE" : "POR ENTREGAR") : "EN REVISION") : "AUTORIZADO",
            //                 MotivoCancel = (bita.Motivo != null && (bita.Motivo == "POR 30 DIAS DE INACTIVIDAD" || bita.Motivo == "VENCIMIENTO DE PRIMER FECHA DE AMORTIZACIÓN") ? bita.Motivo : motivo.DESCRIPCION),
            //                 Region = reg.Dx_Nombre_Region,
            //                 Zona = zon.Dx_Nombre_Zona,
            //                 /////
            //                 Rol_user = usu.Id_Rol == null ? 0 : (int)usu.Id_Rol,
            //                 Id_Proveedor = (int)cre.Id_Proveedor,
            //                 Id_Branch = (int)cre.Id_Branch,
            //                 IdCliente = (int)cre.IdCliente,
            //                 Fecha_Consulta = (DateTime)cre.Fecha_Consulta,
            //                 Monto_Solicitado = (decimal)cre.Monto_Solicitado,
            //                 RFC = cli.RFC,
            //                 NumReactivar = cre.No_Reactivaciones == null ? 0 : (int)cre.No_Reactivaciones,
            //                 fechaMotiCancel = can.FECHA_ADICION,
            //                 NoCreditoMotivoCancel = can.No_Credito

            //             }).OrderBy(t => t.RPU);

            //var datos2 = (from cre in _contexto.CRE_Credito
            //              join estatus in _contexto.CAT_ESTATUS_CREDITO on cre.Cve_Estatus_Credito equals estatus.Cve_Estatus_Credito
            //              join cli in _contexto.CLI_Cliente on new { CP = (int)cre.Id_Proveedor, CB = (int)cre.Id_Branch, CC = (int)cre.IdCliente } equals new { CP = (int)cli.Id_Proveedor, CB = (int)cli.Id_Branch, CC = (int)cli.IdCliente }
            //              join bita in _contexto.BitacoraSis on cre.No_Credito equals bita.No_Credito into leftbita
            //              from bita in leftbita.DefaultIfEmpty()
            //              join can in _contexto.CANCELAR_RECHAZAR on cre.No_Credito equals can.No_Credito into leftcan
            //              from can in leftcan.DefaultIfEmpty()
            //              join motivo in _contexto.MOTIVOS_RECHAZOS_CANCELACIONES on can.ID_MOTIVO equals motivo.ID_MOTIVO into leftmoti
            //              from motivo in leftmoti.DefaultIfEmpty()
            //              join usu in _contexto.US_USUARIO on can.ADICIONADO_POR equals usu.Nombre_Usuario into leftusu
            //              from usu in leftusu.DefaultIfEmpty()
            //              join nego in _contexto.CLI_Negocio on new { NP = (int)cre.Id_Proveedor, NB = (int)cre.Id_Branch, NC = (int)cre.IdCliente, NN = (int)cre.IdNegocio } equals new { NP = (int)nego.Id_Proveedor, NB = (int)nego.Id_Branch, NC = (int)nego.IdCliente, NN = (int)nego.IdNegocio }
            //              join prov in _contexto.CAT_PROVEEDOR on cre.Id_Proveedor equals prov.Id_Proveedor
            //              join zon in _contexto.CAT_ZONA on prov.Cve_Zona equals zon.Cve_Zona
            //              join reg in _contexto.CAT_REGION on prov.Cve_Region equals reg.Cve_Region
            //              join estPro in _contexto.CAT_ESTATUS_PROVEEDOR on prov.Cve_Estatus_Proveedor equals estPro.Cve_Estatus_Proveedor

            //              where cre.Cve_Estatus_Credito == 7
            //                    && estPro.Dx_Estatus_Proveedor == "ACTIVO" && cre.Id_Branch == 0
            //                    && (bita.Motivo == "POR 30 DIAS DE INACTIVIDAD" || bita.Motivo == "VENCIMIENTO DE PRIMER FECHA DE AMORTIZACIÓN")
            //                    && (usu.Id_Rol == 1 || usu.Id_Rol == 2 || usu.Id_Rol == 6)
            //                    && (cre.No_Reactivaciones == null ? 0 : cre.No_Reactivaciones) < no_reactivaciones
            //                    && (NoCre == "" ? 1 == 1 : cre.No_Credito == NoCre)
            //                    && (RPU == "" ? 1 == 1 : cre.RPU == RPU)
            //                    && (Cliente == "" ? 1 == 1 : cli.Nombre + " " + cli.Ap_Paterno + " " + cli.Ap_Materno == Cliente)
            //                    && (NomComercial == "" ? 1 == 1 : nego.Nombre_Comercial == NomComercial)
            //                    && (Reg == 0 ? 1 == 1 : reg.Cve_Region == Reg)
            //                    && (Zon == 0 ? 1 == 1 : zon.Cve_Zona == Zon)


            //              select new DATOS_REACTIVACION
            //              {
            //                  RPU = cre.RPU,
            //                  NoCredito = cre.No_Credito,
            //                  Cliente = cli.Nombre + " " + cli.Ap_Paterno + " " + cli.Ap_Materno,
            //                  NomComercial = nego.Nombre_Comercial,
            //                  FechCancelacion = (DateTime)cre.Fecha_Cancelado,
            //                  UltimoStatus = cre.Fecha_Autorizado == null ? (cre.Fecha_En_revision == null ? (cre.Fecha_Por_entregar == null ? "PENDIENTE" : "POR ENTREGAR") : "EN REVISION") : "AUTORIZADO",
            //                  MotivoCancel = bita.Motivo != null && (bita.Motivo == "POR 30 DIAS DE INACTIVIDAD" || bita.Motivo == "VENCIMIENTO DE PRIMER FECHA DE AMORTIZACIÓN") ? bita.Motivo : motivo.DESCRIPCION,
            //                  Region = reg.Dx_Nombre_Region,
            //                  Zona = zon.Dx_Nombre_Zona,
            //                  /////
            //                  Rol_user = usu.Id_Rol == null ? 0 : (int)usu.Id_Rol,
            //                  Id_Proveedor = (int)cre.Id_Proveedor,
            //                  Id_Branch = (int)cre.Id_Branch,
            //                  IdCliente = (int)cre.IdCliente,
            //                  Fecha_Consulta = (DateTime)cre.Fecha_Consulta,
            //                  Monto_Solicitado = (decimal)cre.Monto_Solicitado,
            //                  RFC = cli.RFC,
            //                  NumReactivar = cre.No_Reactivaciones == null ? 0 : (int)cre.No_Reactivaciones,
            //                  fechaMotiCancel = can.FECHA_ADICION,
            //                  NoCreditoMotivoCancel = can.No_Credito
            //              }).OrderBy(t => t.RPU);
            #endregion
            var Filtro1 = query1.Concat(query2).ToList(); 


            List<decimal> sumas = new List<decimal>();
            List<DATOS_REACTIVACION> Filtro2 = new List<DATOS_REACTIVACION>();
            List<DATOS_REACTIVACION> FiltroFinal = new List<DATOS_REACTIVACION>();


            for (int i = 0; i < Filtro1.Count; i++)
            {
                decimal sumasrfc = extraer(Filtro1[i].RFC);
                sumasrfc += Filtro1[i].Monto_Solicitado;
                List<DATOS_REACTIVACION> motivo = new List<DATOS_REACTIVACION>();
                bool ban2 = false;
                for (int j = 0; j < Filtro1.Count; j++)
                {
                    if (Filtro1[i].NoCreditoMotivoCancel == Filtro1[j].NoCreditoMotivoCancel)
                    { 
                        motivo.Add(Filtro1[j]);  
                    }
                }
                /////Saca el motivo de ese noCredito el mas reciente
                foreach (var item in Filtro2)
                {
                    if (Filtro1[i].NoCredito == item.NoCredito)
                    {
                        ban2 = true;
                    }
                }
                
                if (Filtro1[i].fechaMotiCancel!=null)
                {
                    if (!ban2)
                    {
                        Filtro2.Add(motivo.OrderByDescending(c => c.fechaMotiCancel).First());
                        sumas.Add(sumasrfc);
                    }
                }
                else
                {
                    Filtro2.Add(Filtro1[i]);
                    sumas.Add(sumasrfc);
                }
                /////////<------
            }

            var noConsultaCrediticia = VariablesGlobales.ObtienePorId(3, 17);
            for (int i = 0; i < Filtro2.Count(); i++)
            {
                int consultas = 0;
                var consultasRFCRPU = CREDITO_DAL.ClassInstance.consultasCrediticias(Filtro2[i].NoCredito).ToList();
                for (int j = 0; j < consultasRFCRPU.Count(); j++)
                {
                    consultas += consultasRFCRPU[j].no_consultasCrediticias;
                }
                if (consultas<=Convert.ToInt32(noConsultaCrediticia.VALOR))
                {
                    if (sumas[i] <= (decimal)VariablesGlobales.ObtienePorIdValor(4, 16))
                    {
                        List<DatosCreditxRPU> rpus = ObtenerRpu(Filtro2[i].RPU);
                        int cont = 0;
                        for (int t = 0; t < rpus.Count(); t++)
                        {
                            if (rpus[t].cve_estatusCredito >0&&rpus[t].cve_estatusCredito<=4)
                            {
                                cont++;
                            }
                        }

                        if (cont==0)
                        {
                            FiltroFinal.Add(Filtro2[i]);
                        }
                    }
                  }
            }
            
            return FiltroFinal.OrderBy(l => l.RPU).ToList();
        }

        public decimal extraer(string rfc)
        {

            decimal credits = 0;
            var rfcs = (from cre in _contexto.CRE_Credito
                        join cli in _contexto.CLI_Cliente on new { CP = (int)cre.Id_Proveedor, CB = (int)cre.Id_Branch, CC = (int)cre.IdCliente } equals new { CP = (int)cli.Id_Proveedor, CB = (int)cli.Id_Branch, CC = (int)cli.IdCliente }

                        where cre.Cve_Estatus_Credito > 0 && cre.Cve_Estatus_Credito < 6// 1 || cre.Cve_Estatus_Credito == 2 || cre.Cve_Estatus_Credito == 1 || cre.Cve_Estatus_Credito == 3 || cre.Cve_Estatus_Credito == 4 || cre.Cve_Estatus_Credito == 5
                              && cli.RFC == rfc
                        select new DATOS_REACTIVACION
                        {
                            RFC = cli.RFC,
                            NoCredito = cre.No_Credito,
                            Monto_Solicitado = (int)cre.Monto_Solicitado
                        }).ToList();
            decimal sum = 0;

            for (int i = 0; i < rfcs.Count(); i++)
            {
                sum += rfcs[i].Monto_Solicitado;
            }
            credits = sum;

            return credits;
        }

        public List<DatosCreditxRPU> ObtenerRpu(string rpu)
        {
            //PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();
            var rpus=(from cre in _contexto.CRE_Credito
                          where cre.RPU==rpu
                          select new DatosCreditxRPU
                          {
                            RPU=cre.RPU,
                            cve_estatusCredito=(int)cre.Cve_Estatus_Credito
                          }).ToList();
            return rpus;
        }

        public static CRE_Credito ObtienePorId(string noCredito)
        {
            CRE_Credito credito;

            using (var r = new Repositorio<CRE_Credito>())
            {
                credito = r.Extraer(cre => cre.No_Credito == noCredito);
            }
            return credito;
        }

        public static bool Actualizar(CRE_Credito informacion)
        {
            bool actualiza;

            var creInfoGeneral = ObtienePorId(informacion.No_Credito);

            if (creInfoGeneral != null)
            {
                using (var r = new Repositorio<CRE_Credito>())
                {
                    actualiza = r.Actualizar(informacion);
                }
            }
            else
            {
                throw new Exception("El Id: " + informacion.No_Credito + " no fue encontrado.");
            }

            return actualiza;
        }

        public CANCELAR_RECHAZAR HayMotivosCancelacion(Expression<Func<CANCELAR_RECHAZAR, bool>> No_Credito)
        {
            CANCELAR_RECHAZAR Verifica = null;

            using (var r = new Repositorio<CANCELAR_RECHAZAR>())
            {
                Verifica = r.Extraer(No_Credito);
            }

            return Verifica;
        }

        public bool ExisteMotivosCancelacion(string NO_Credito)
        {
            var Existe = HayMotivosCancelacion(c => c.No_Credito == NO_Credito);

            if (Existe == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
