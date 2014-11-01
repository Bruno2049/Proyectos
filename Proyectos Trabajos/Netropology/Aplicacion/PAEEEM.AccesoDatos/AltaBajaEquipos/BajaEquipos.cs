using System;
using System.Collections.Generic;
using System.Linq;
using PAEEEM.Entidades;
using PAEEEM.Entidades.AltaBajaEquipos;

namespace PAEEEM.AccesoDatos.AltaBajaEquipos
{
    public class BajaEquipos
    {

        private static readonly PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();

        public static K_CREDITO_SUSTITUCION Insertar(K_CREDITO_SUSTITUCION informacion)
        {
            K_CREDITO_SUSTITUCION trInformacion;

            using (var r = new Repositorio<K_CREDITO_SUSTITUCION>())
            {
                trInformacion = r.Extraer(c => c.Id_Credito_Sustitucion == informacion.Id_Credito_Sustitucion);

                if (trInformacion == null)
                    trInformacion = r.Agregar(informacion);
                else
                    throw new Exception("El registro ya existe en la BD.");
            }
            return trInformacion;
        }

        public static K_CREDITO_SUSTITUCION ObtieneKCreditoSustitucionPorId(int idCredSust)
        {
            K_CREDITO_SUSTITUCION trInfoGeneral;

            using (var r = new Repositorio<K_CREDITO_SUSTITUCION>())
            {
                trInfoGeneral = r.Extraer(tr => tr.Id_Credito_Sustitucion == idCredSust);
            }
            return trInfoGeneral;
        }

        public static bool Actualizar(K_CREDITO_SUSTITUCION informacion)
        {
            bool actualiza;

            var trInfoGeneral = ObtieneKCreditoSustitucionPorId(informacion.Id_Credito_Sustitucion);

            if (trInfoGeneral != null)
            {
                using (var r = new Repositorio<K_CREDITO_SUSTITUCION>())
                {
                    actualiza = r.Actualizar(informacion);
                }
            }
            else
            {
                throw new Exception("El Id: " + informacion.Id_Credito_Sustitucion + " no fue encontrado.");
            }

            return actualiza;
        }


        public static CAT_TECNOLOGIA GetInfoTecnologia(string tecnologia)
        {
            CAT_TECNOLOGIA trInformacion;

            using (var r = new Repositorio<CAT_TECNOLOGIA>())
            {
                trInformacion = r.Extraer(c => c.Dx_Nombre_Particular == tecnologia);
            }
            return trInformacion;
        }

        public static List<GridEquiposBaja> Get_Info_Equipos_Baja(string noCredito)
        {
            var listEquipos = (from eb in _contexto.K_CREDITO_SUSTITUCION
                               join t in _contexto.CAT_TECNOLOGIA on eb.Cve_Tecnologia equals t.Cve_Tecnologia
                               join tp in _contexto.CAT_TIPO_PRODUCTO on eb.Dx_Tipo_Producto equals tp.Ft_Tipo_Producto
                               join capSus in _contexto.CAT_CAPACIDAD_SUSTITUCION on eb.Cve_Capacidad_Sust equals
                                   capSus.Cve_Capacidad_Sust
                                   into temp
                               from capSus in temp.DefaultIfEmpty()
                               where eb.No_Credito == noCredito
                               select new GridEquiposBaja
                               {
                                   Id_Credito_Sustitucion = eb.Id_Credito_Sustitucion,
                                   Grupo = eb.Grupo,
                                   Tecnologia = t.Dx_Nombre_General,
                                   Cve_Tecnologia = t.Cve_Tecnologia,
                                   Producto = tp.Dx_Tipo_Producto,
                                   Capacidad = eb.CapacidadSistema,
                                   Unidades = eb.Unidad,
                                   Cantidad = eb.No_Unidades ?? 1,
                                   No_Credito = eb.No_Credito,
                                   idConsecutivo = 0
                               }).ToList();

            return listEquipos;
        }
    }
}
