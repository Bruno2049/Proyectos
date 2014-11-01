using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.Entidades;
using PAEEEM.Entidades.Utilizables;

namespace PAEEEM.AccesoDatos.CargaMasivaProductos
{
    public class CargaProductos
    {

        public List<CAT_TECNOLOGIA> ListaTecnologias()
        {
            using (var contexto = new PAEEEM_DESAEntidades())
            {
                var lstTecnologia = contexto.CAT_TECNOLOGIA.Where(t => t.Estatus == 1).Distinct().ToList();
                return lstTecnologia;
            }
        }

        public List<CAT_FABRICANTE> ListaFabricantes()
        {
            using (var contexto = new PAEEEM_DESAEntidades())
            {
                var lstFabricantes = (from f in contexto.CAT_FABRICANTE
                    select f).ToList();
                return lstFabricantes;
            }
        }

        public List<CAT_TIPO_PRODUCTO> ListaTiposProductos(int id)
        {
            using (var contexto = new PAEEEM_DESAEntidades())
            {
                var lstTP = (from f in contexto.CAT_TIPO_PRODUCTO
                             where f.Cve_Tecnologia == id  
                             select f).ToList();
                return lstTP;
            }
        }

        public List<CAT_SE_TIPO> ListaTipos()
        {
            using (var contexto = new PAEEEM_DESAEntidades())
            {
                var lstTP = (from f in contexto.CAT_SE_TIPO
                             where f.Atributo_2.Equals("1")
                             select f).ToList();
                return lstTP;
            }
        }

        public List<CAT_MARCA> ListaMarcas()
        {
            using (var contexto = new PAEEEM_DESAEntidades())
            {
                var lst = (from f in contexto.CAT_MARCA
                             select f).ToList();
                return lst;
            }
        }

        public List<CAT_CAPACIDAD_SUSTITUCION> ListaCapacidad(int id)
        {
            using (var contexto = new PAEEEM_DESAEntidades())
            {
                var lst = (from f in contexto.CAT_CAPACIDAD_SUSTITUCION
                           where f.Cve_Tecnologia == id     
                           select f).ToList();
                return lst;
            }
        }

        public List<CAT_SE_TRANSFORM_RELACION> ListaRelacion()
        {
            using (var contexto = new PAEEEM_DESAEntidades())
            {
                var lst = (from f in contexto.CAT_SE_TRANSFORM_RELACION
                           where f.Cve_Relacion > 0
                           select f).ToList();
                return lst;
            }
        }

        public bool NoExisteProducto(CAT_PRODUCTO producto)
        {
            using (var contexto = new PAEEEM_DESAEntidades())
            {
                var lst = (from f in contexto.CAT_PRODUCTO
                           where f.Cve_Tecnologia == producto.Cve_Tecnologia
                          && f.Cve_Fabricante == producto.Cve_Fabricante
                          && f.Ft_Tipo_Producto == producto.Ft_Tipo_Producto
                          && f.Dx_Nombre_Producto == producto.Dx_Nombre_Producto
                          && f.Cve_Marca == producto.Cve_Marca
                          && f.Dx_Modelo_Producto == producto.Dx_Modelo_Producto 
                          && f.Cve_Capacidad_Sust == producto.Cve_Capacidad_Sust
                          && f.Cve_Estatus_Producto == 1
                     select f).ToList();
                return lst.Count > 0;
            }
        }

        public bool NoExisteProductoSubEstacion(CAT_PRODUCTO producto)
        {
            using (var contexto = new PAEEEM_DESAEntidades())
            {
                var lst = (from f in contexto.CAT_PRODUCTO
                           where f.Cve_Tecnologia == producto.Cve_Tecnologia
                          && f.Cve_Fabricante == producto.Cve_Fabricante
                          && f.Dx_Modelo_Producto == producto.Dx_Modelo_Producto
                          && f.Cve_Tipo_SE == producto.Cve_Tipo_SE
                          && f.Ft_Tipo_Producto == 11
                          && f.Cve_Estatus_Producto == 1
                          select f).ToList();
                return lst.Count > 0;
            }
        }

        public void InsertarNuevoProducto(CAT_PRODUCTO prod)
        {
            using (var contexto = new PAEEEM_DESAEntidades())
            {
                contexto.CAT_PRODUCTO.Add(prod);
                contexto.SaveChanges();
            }
        }

        public int InsertarNuevoProductoReturnID(CAT_PRODUCTO prod)
        {
            using (var contexto = new PAEEEM_DESAEntidades())
            {
                contexto.CAT_PRODUCTO.Add(prod);
                contexto.SaveChanges();
                int ultimoID = prod.Cve_Producto;
                return ultimoID;
            }
        }

        public void InsertarModuloSE(CAT_MODULOS_SE prod)
        {
            using (var contexto = new PAEEEM_DESAEntidades())
            {
                contexto.CAT_MODULOS_SE.Add(prod);
                contexto.SaveChanges();
            }
        }

        public int InsertaLogHeader(Load_LogHeader logHeader)
        {
            using (var contexto = new PAEEEM_DESAEntidades())
            {
                contexto.Load_LogHeader.Add(logHeader);
                contexto.SaveChanges();
                int ultimoID = logHeader.LogHeaderId;
                return ultimoID;
            }
        }

        public int InsertaLogDetalle(Load_LogDetail logDetail)
        {
            using (var contexto = new PAEEEM_DESAEntidades())
            {
                      contexto.Load_LogDetail.Add(logDetail);
              return  contexto.SaveChanges();
            }
        }

        public int ActualizaLogHeader(Load_LogHeader logHeader)
        {
            using (var contexto = new PAEEEM_DESAEntidades())
            {
              var log = contexto.Load_LogHeader.FirstOrDefault(l => l.LogHeaderId == logHeader.LogHeaderId);
                  log.NoRegConError = logHeader.NoRegConError;
                  log.NoRegSinError = logHeader.NoRegSinError;
                  return contexto.SaveChanges();
            }
        }

        public List<Load_LogDetail> ObtenerDetailsErroresCarga(int idLogHeader)
        {
            using (var contexto = new PAEEEM_DESAEntidades())
            {
                var lst = (from f in contexto.Load_LogDetail
                             where f.LogHeaderId == idLogHeader
                             select f).ToList();
                return lst;
            }
        }

        public Load_LayOut ObtenerNombreArchivo(int idTecnologia, int? idClase)
        {
            using (var contexto = new PAEEEM_DESAEntidades())
            {
               var lst = new Load_LayOut();
                if (idClase != null)
                {
                    lst = (from f in contexto.Load_LayOut
                        where f.Cve_Tecnologia == idTecnologia
                        && f.Cve_clase == idClase
                        select f).FirstOrDefault();
                }
                else
                {
                    lst = (from f in contexto.Load_LayOut
                               where f.Cve_Tecnologia == idTecnologia
                           select f).FirstOrDefault();
                }
                return lst;
            }
        }

        public List<TIPO_ENCAPSULADO> TipoEncapsulado()
        {
            using (var contexto = new PAEEEM_DESAEntidades())
            {
                var lst = (from e in contexto.TIPO_ENCAPSULADO
                    select e).ToList();
                return lst;
            }
        }

        public List<TIPO_PROTECCION_INTERNA> TipoProteccionInterna()
        {
            using (var contexto = new PAEEEM_DESAEntidades())
            {
                var lst = (from e in contexto.TIPO_PROTECCION_INTERNA
                           select e).ToList();
                return lst;
            }
        }

        public List<TIPO_PROTECCION_EXTERNA> TipoProteccionExterna()
        {
            using (var contexto = new PAEEEM_DESAEntidades())
            {
                var lst = (from e in contexto.TIPO_PROTECCION_EXTERNA
                           select e).ToList();
                return lst;
            }
        }

        public List<MATERIAL_CUBIERTA> MaterialCubiertas()
        {
            using (var contexto = new PAEEEM_DESAEntidades())
            {
                var lst = (from e in contexto.MATERIAL_CUBIERTA
                           select e).ToList();
                return lst;
            }
        }

        public List<PERDIDAS> Perdidas()
        {
            using (var contexto = new PAEEEM_DESAEntidades())
            {
                var lst = (from e in contexto.PERDIDAS
                           select e).ToList();
                return lst;
            }
        }

        public List<PROTECCION_SIN_CORRIENTE> ProteccionSobreCorriente()
        {
            using (var contexto = new PAEEEM_DESAEntidades())
            {
                var lst = (from e in contexto.PROTECCION_SIN_CORRIENTE
                           select e).ToList();
                return lst;
            }
        }

        public List<PROTECCION_CONTRA_FUEGO> ProteccionContraFuego()
        {
            using (var contexto = new PAEEEM_DESAEntidades())
            {
                var lst = (from e in contexto.PROTECCION_CONTRA_FUEGO
                           select e).ToList();
                return lst;
            }
        }

        public List<PROTECCION_CONTRA_EXPLOSION> ProteccionContraExplosiones()
        {
            using (var contexto = new PAEEEM_DESAEntidades())
            {
                var lst = (from e in contexto.PROTECCION_CONTRA_EXPLOSION
                           select e).ToList();
                return lst;
            }
        }

        public List<ANCLAJE> Anclaje()
        {
            using (var contexto = new PAEEEM_DESAEntidades())
            {
                var lst = (from e in contexto.ANCLAJE
                           select e).ToList();
                return lst;
            }
        }

        public List<TERMINAL_TIERRA> TerminalTierra()
        {
            using (var contexto = new PAEEEM_DESAEntidades())
            {
                var lst = (from e in contexto.TERMINAL_TIERRA
                           select e).ToList();
                return lst;
            }
        }

        public List<CComboBox> TipoDeControlador()
        {
            var lst = new List<CComboBox>(){new CComboBox(0,"SIN"),new CComboBox(1,"CON")};
            return lst;
        }

        public List<CComboBox> ComboBinario()
        {
            var lst = new List<CComboBox>() { new CComboBox(0, "NO"), new CComboBox(1, "SI") };
            return lst;
        }

        public List<PROTECCION_GABINETE> ProteccionGabiente()
        {
            using (var contexto = new PAEEEM_DESAEntidades())
            {
                var lst = (from e in contexto.PROTECCION_GABINETE
                           select e).ToList();
                return lst;
            }
        }



    }
}
