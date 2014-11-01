using System;
using System.Collections.Generic;
using System.Linq;

using System.Runtime.Serialization;
using PAEEEM.Entidades;
using PAEEEM.Entidades.ModuloCentral;

namespace PAEEEM.AccesoDatos.Catalogos
{
    public class VariablesGlobales
    {
        private static readonly VariablesGlobales _classInstance = new VariablesGlobales();
        public static VariablesGlobales ClassInstance { get { return _classInstance; } }


        private readonly PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();

        public List<DetalleVariablesGlobales> ObtenerVariablesGlobales()
        {
            
            var query = (from variables in _contexto.TR_PARAMETROS_GLOBALES.AsEnumerable()
                         where variables.PARAMETRO_MODIFICABLE.HasValue == true
                         select new DetalleVariablesGlobales
                         {
                           IdParametro=variables.IDPARAMETRO,
                           IdSeccion=variables.IDSECCION,
                           Descripcion=variables.DESCRIPCION.ToUpper(),
                           valor=variables.VALOR,
                           estatus=variables.ESTATUS,
                           fecha_adiccion=variables.FECHA_ADICION,
                           adicionadoPor=variables.ADICIONADO_POR,
                           parametro_modificable=variables.PARAMETRO_MODIFICABLE,
                           ID_Prog_Proy=0,
                           ////total_incentivo=0
                         }).ToList();

            var query2 = (from variables in _contexto.CAT_PROGRAMA.AsEnumerable()
                          select new DetalleVariablesGlobales
                          {
                              IdParametro = 0,
                              IdSeccion = 0,
                              ID_Prog_Proy = variables.ID_Prog_Proy,
                              valor = variables.Mt_Fondo_Total_Programa.ToString(),
                              Descripcion = "Fondo del Programa ("+variables.Dx_Nombre_Programa+")",
                              //total_incentivo=1
                          }
                              ).ToList();

            ////var query3 = (from variables in _contexto.CAT_PROGRAMA.AsEnumerable()
            ////              select new DetalleVariablesGlobales
            ////              {
            ////                  IdParametro = 0,
            ////                  IdSeccion = 0,
            ////                  ID_Prog_Proy = variables.ID_Prog_Proy,
            ////                  valor = variables.Mt_Fondo_Disponible_Incentivo.ToString(),
            ////                  Descripcion = "Fondo de incentivo",
            ////                  total_incentivo=2
            ////              }
            ////                  ).ToList();


            var queryFinal = query.Concat(query2).OrderBy(d=> d.Descripcion).ToList();          
            return queryFinal;
        }

        public static TR_PARAMETROS_GLOBALES ObtienePorId(int idP, int idS)
        {
            TR_PARAMETROS_GLOBALES trInfoGeneral;

            using (var r = new Repositorio<TR_PARAMETROS_GLOBALES>())
            {
                trInfoGeneral = r.Extraer(tr => tr.IDPARAMETRO == idP && tr.IDSECCION == idS);
            }
            return trInfoGeneral;
        }

        //add by alex...
        public static int ObtienePorIdValor(int idP, int idS)
        {
            TR_PARAMETROS_GLOBALES trInfoGeneral;
            int valor = 0;
            using (var r = new Repositorio<TR_PARAMETROS_GLOBALES>())
            {
                trInfoGeneral = r.Extraer(tr => tr.IDPARAMETRO == idP && tr.IDSECCION == idS);
                valor = Convert.ToInt32(trInfoGeneral.VALOR);
            }
            return valor;
        }

        public static decimal Obtienesaldo(int idProg)
        {
            CAT_PROGRAMA saldodisponible;
            decimal valor = 0;
            using (var r = new Repositorio<CAT_PROGRAMA>())
            {
                saldodisponible = r.Extraer(pro => pro.ID_Prog_Proy == idProg);
                valor = (decimal)saldodisponible.Mt_Fondo_Disponible;
            }
            return valor;
        }

        public static int ObxId(int idP, int idS)
        {
            int trInfoGeneral;

            using (var r = new Repositorio<TR_PARAMETROS_GLOBALES>())
            {
                trInfoGeneral = Convert.ToInt16(r.Extraer(tr => tr.IDPARAMETRO == idP && tr.IDSECCION == idS));
            }
            return trInfoGeneral;
        }


        public static CAT_PROGRAMA ObtienePorIdPro(int idPP)
        {
            CAT_PROGRAMA trInfoGeneral;

            using (var r = new Repositorio<CAT_PROGRAMA>())
            {
                trInfoGeneral = r.Extraer(tr => tr.ID_Prog_Proy == idPP);
            }
            return trInfoGeneral;
        }

        public static bool Actualizar(TR_PARAMETROS_GLOBALES informacion)
        {
            bool actualiza;

            var trInfoGeneral = ObtienePorId(informacion.IDPARAMETRO,informacion.IDSECCION);

            if (trInfoGeneral != null)
            {
                using (var r = new Repositorio<TR_PARAMETROS_GLOBALES>())
                {
                    actualiza = r.Actualizar(informacion);
                }
            }
            else
            {
                throw new Exception("El Id: " + informacion.IDPARAMETRO + " no fue encontrado.");
            }

            return actualiza;
        }

        public static bool ActualizarPro(CAT_PROGRAMA informacion)
        {
            bool actualiza;

            var trInfoGeneral = ObtienePorIdPro(informacion.ID_Prog_Proy);

            if (trInfoGeneral != null)
            {
                using (var r = new Repositorio<CAT_PROGRAMA>())
                {
                    actualiza = r.Actualizar(informacion);
                }
            }
            else
            {
                throw new Exception("El Id: " + informacion.ID_Prog_Proy + " no fue encontrado.");
            }

            return actualiza;
        }

    }
}
