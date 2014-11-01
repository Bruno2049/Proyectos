using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PAEEEM.Entidades;

namespace PAEEEM.AccesoDatos.Catalogos
{
    public class DelegacionMunicipio
    {

        public CAT_DELEG_MUNICIPIO Agregar(CAT_DELEG_MUNICIPIO delgeMunicipio)
        {
            CAT_DELEG_MUNICIPIO catDelgMun = null;

            using (var r = new Repositorio<CAT_DELEG_MUNICIPIO>())
            {
                catDelgMun = r.Extraer(c => c.Cve_Deleg_Municipio == delgeMunicipio.Cve_Deleg_Municipio);

                if (catDelgMun == null)
                    catDelgMun = r.Agregar(delgeMunicipio);
                else
                    throw new Exception("La Delegacion o Municipio ya existe en la BD.");
            }

            return catDelgMun;
        }


        public CAT_DELEG_MUNICIPIO ObtienePorCondicion(Expression<Func<CAT_DELEG_MUNICIPIO, bool>> criterio)
        {
            CAT_DELEG_MUNICIPIO ctDelgMun = null;

            using (var r = new Repositorio<CAT_DELEG_MUNICIPIO>())
            {
                ctDelgMun = r.Extraer(criterio);
            }

            return ctDelgMun;
        }

        public List<CAT_DELEG_MUNICIPIO> ObtieneTodos()
        {
            List<CAT_DELEG_MUNICIPIO> listDelgMun = null;

            using (var r = new Repositorio<CAT_DELEG_MUNICIPIO>())
            {
                listDelgMun = r.Filtro(null);
            }

            return listDelgMun;
        }

        public bool Actualizar(CAT_DELEG_MUNICIPIO delegMunicipio)
        {
            bool actualiza = false;

            var catDelegMun = ObtienePorCondicion(c => c.Cve_Deleg_Municipio == delegMunicipio.Cve_Deleg_Municipio);

            if (catDelegMun != null)
            {
                using (var r = new Repositorio<CAT_DELEG_MUNICIPIO>())
                {
                    actualiza = r.Actualizar(catDelegMun);
                }
            }
            else
            {
                throw new Exception("La Delegacion o Municipio con Id: " + delegMunicipio.Cve_Deleg_Municipio + " no fue encontrado.");
            }

            return actualiza;
        }

        public bool Eliminar(int idDelegMun)
        {
            bool elimina = false;

            var catDelegMun = ObtienePorCondicion(c => c.Cve_Deleg_Municipio == idDelegMun);

            if (catDelegMun != null)
            {
                using (var r = new Repositorio<CAT_DELEG_MUNICIPIO>())
                {
                    elimina = r.Eliminar(catDelegMun);
                }
            }
            else
            {
                throw new Exception("No se encontro la delegacion o municipio indicado.");
            }

            return elimina;
        }

        //Augusto
        public List<CAT_DELEG_MUNICIPIO> FitrarPorCondicion(Expression<Func<CAT_DELEG_MUNICIPIO, bool>> criterio)
        {
            List<CAT_DELEG_MUNICIPIO> listEstado = null;

            using (var r = new Repositorio<CAT_DELEG_MUNICIPIO>())
            {
                listEstado = r.Filtro(criterio).ToList();
            }

            return listEstado;
        }

        public static List<CAT_ESTADO> obtieneTodosEstados()
        {
            List<CAT_ESTADO> lista = null;
            using (var r = new Repositorio<CAT_ESTADO>())
            {
                lista = r.Filtro(l=>l.Cve_Estado!=null);
            }
            return lista;
        }

        public static List<CAT_DELEG_MUNICIPIO> cveEstado(int cveEstado)
        {
            List<CAT_DELEG_MUNICIPIO> lista = null;

            using (var r = new Repositorio<CAT_DELEG_MUNICIPIO>())
            {
                lista = r.Filtro(me=>me.Cve_Estado==cveEstado).ToList();
            }

            return lista;
        }

        public static List<CAT_CODIGO_POSTAL_SEPOMEX> listColonias(int cveMunicipio)
        {
            List<CAT_CODIGO_POSTAL_SEPOMEX> listaCol = null;
            using (var r = new Repositorio<CAT_CODIGO_POSTAL_SEPOMEX>())
            {
                listaCol = r.Filtro(l => l.Cve_Deleg_Municipio == cveMunicipio).ToList();
            }
            return listaCol;
            
        }

        public static CAT_CODIGO_POSTAL_SEPOMEX CodigoPostal(string CodPos)
        {
            CAT_CODIGO_POSTAL_SEPOMEX codigo = null;
            using (var r = new Repositorio<CAT_CODIGO_POSTAL_SEPOMEX>())
            {
                 codigo = r.Filtro(l => l.Codigo_Postal == CodPos).FirstOrDefault();
            }
            return codigo;

        }

        public static CAT_CODIGO_POSTAL_SEPOMEX obtieneCodPostal(int CodPos)
        {
            CAT_CODIGO_POSTAL_SEPOMEX codigo = null;
            using (var r = new Repositorio<CAT_CODIGO_POSTAL_SEPOMEX>())
            {
                codigo = r.Filtro(l => l.Cve_CP == CodPos).FirstOrDefault();
            }
            return codigo;

        }


        public static List<CAT_CODIGO_POSTAL_SEPOMEX> obtieneCodPostalLista(string CodPos)
        {
            List<CAT_CODIGO_POSTAL_SEPOMEX> codigo = null;
            using (var r = new Repositorio<CAT_CODIGO_POSTAL_SEPOMEX>())
            {
                codigo = r.Filtro(l => l.Codigo_Postal == CodPos);
            }
            return codigo;

        }

        public static List<CAT_REGION> obtieneRegiones()
        {
            List<CAT_REGION> lista = null;
            using (var r = new Repositorio<CAT_REGION>())
            {
                lista = r.Filtro(l => l.Cve_Region != null);
            }
            return lista;
        }

        


    }
}
