using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using PAEEEM.Entidades;
using PAEEEM.Entidades.Tecnologias;

namespace PAEEEM.AccesoDatos.Log
{
    public class Registar
    {
        private static readonly PAEEEM_DESAEntidades Contexto = new PAEEEM_DESAEntidades();

        public static K_LOG Insertar(K_LOG informacion)
        {
            K_LOG trInformacion;

            using (var r = new Repositorio<K_LOG>())
            {
                trInformacion = r.Extraer(c => c.IDSECUENCIA == informacion.IDSECUENCIA);

                if (trInformacion == null)
                    trInformacion = r.Agregar(informacion);
                else
                    throw new Exception("El registro ya existe en la BD.");
            }
            return trInformacion;
        }

        public static List<CAT_TAREAS_PROCESOS> GetIdProceso(string tipoProceso, string descripcionProceso)
        {
            var infoProcesos =
                (from tProceso in Contexto.CAT_TIPO_PROCESOS
                    from procesos in Contexto.CAT_TAREAS_PROCESOS
                    where tProceso.IDTIPOPROCESO == procesos.IDTIPOPROCESO
                          && tProceso.DESCRIPCION == tipoProceso
                          && procesos.DESCRIPCION == descripcionProceso
                    select procesos).ToList();

            return infoProcesos;
        }

        public static List<US_USUARIO> GetIdUserName(string nombreUser)
        {
            var infoUser = (from user in Contexto.US_USUARIO
                where user.Nombre_Usuario == nombreUser
                select user).ToList();
            return infoUser;
        }

        public static int GetIdProducto()
        {
            var infoUser =
                (from p in Contexto.CAT_PRODUCTO
                    select p.Cve_Producto).Max();
            return infoUser;
        }

        public static int GetIdCentroDisp()
        {
            var infoCentroDisp =
                (from p in Contexto.CAT_CENTRO_DISP
                    select p.Id_Centro_Disp).Max();
            return infoCentroDisp;
        }

        public static int GetIdCentroDispBranch()
        {
            var infoCentroDispBranch =
                (from p in Contexto.CAT_CENTRO_DISP_SUCURSAL
                    select p.Id_Centro_Disp_Sucursal).Max();
            return infoCentroDispBranch;
        }

        public static int GetIdProveedor()
        {
            var infoProv =
                (from p in Contexto.CAT_PROVEEDOR
                    select p.Id_Proveedor).Max();
            return infoProv;
        }

        public static int GetIdBranchProveedor()
        {
            var infoBrachProv =
                (from p in Contexto.CAT_PROVEEDORBRANCH
                    select p.Id_Branch).Max();
            return infoBrachProv;
        }

        public static DescTecnologias GetDesTecnologias(int idTecnologia)
        {
            var query = (from cat in Contexto.CAT_TECNOLOGIA
                join ea in Contexto.CAT_EQUIPOS_BAJA_ALTA
                    on cat.Cve_Equipos_Alta equals ea.Cve_Equipos_Baja_Alta
                join eb in Contexto.CAT_EQUIPOS_BAJA_ALTA
                    on cat.Cve_Equipos_Baja equals eb.Cve_Equipos_Baja_Alta
                join tipoTec in Contexto.CAT_TIPO_TECNOLOGIA
                    on cat.Cve_Tipo_Tecnologia equals tipoTec.Cve_Tipo_Tecnologia
                join tipoMov in Contexto.CAT_TIPO_MOVIMIENTO
                    on cat.Cve_Tipo_Movimiento equals tipoMov.Cve_Tipo_Movimiento
                join facSust in Contexto.CAT_FACTOR_SUSTITUCION on cat.Cve_Factor_Sustitucion
                    equals facSust.Cve_Factor_Sutitucion
                join plantilla in Contexto.CAT_PLANTILLA on cat.Cve_Plantilla equals plantilla.Cve_Plantilla
                //where cat.Cve_Tecnologia == cveTecnologia
                select new DescTecnologias
                {
                    IdTecnologia = cat.Cve_Tecnologia,
                    Nombre = cat.Dx_Nombre_General,
                    Clave = cat.Dx_Cve_CC,
                    TipoTecnologia = tipoTec.Dx_Nombre,
                    Estatus = cat.Estatus == 0 ? "INACTIVO" : "ACTIVO",
                    Tipo = tipoMov.Dx_Tipo_Movimiento,
                    Chatarrizacion = cat.Cve_Chatarrizacion == 1 ? "SI" : "NO",
                    MontoChatarrizacion = cat.Monto_Chatarrizacion,
                    EquipoBaja = eb.Dx_Equipos_Baja_Alta,
                    EquiposAlta = ea.Dx_Equipos_Baja_Alta,
                    FactorSustitucion = facSust.Dx_Factor_Sustitucion,
                    Incentivo = cat.Cve_Incentivo == 0 ? "NO" : "SI",
                    MontoIncentivo = cat.Monto_Incentivo,
                    Plantilla = plantilla.Dx_Descripcion,
                    ImprimirLeyendaDescriptiva = cat.Cve_Leyenda == 1 ? "SI" : "NO",
                    CombinacionTecnologia = cat.Combina_Tecnologias == 1 ? "SI" : "NO"
                }).ToList().FirstOrDefault(me => me.IdTecnologia == idTecnologia);
            return query;
        }
    }
}