using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PAEEEM.Entidades;
using PAEEEM.Entidades.ModuloCentral;

namespace PAEEEM.AccesoDatos.Catalogos
{

    public class CAT_PROVEEDOR_ALTAPRO
    {

        private readonly PAEEEM_DESAEntidades _context = new PAEEEM_DESAEntidades();

        public static List<CAT_PROVEEDOR> todosProveedores()
        {

            List<CAT_PROVEEDOR> listaProveedor = null;

            using (var r = new Repositorio<CAT_PROVEEDOR>())
            {
                listaProveedor = r.Filtro(l => l.Cve_Estatus_Proveedor == 2).ToList();
            }

            return listaProveedor;

        }

        public static CAT_PROVEEDOR obtieneProveedorPorID(int id)
        {
            CAT_PROVEEDOR provedor = null;
            using (var r = new Repositorio<CAT_PROVEEDOR>())
            {
                provedor = r.Filtro(l => l.Cve_Estatus_Proveedor == 2 && l.Id_Proveedor == id).FirstOrDefault();
            }

            return provedor;
        }

        public static CAT_PROVEEDOR obtieneProveedorXidConsulta(int id)
        {
            CAT_PROVEEDOR provedor = null;
            using (var r = new Repositorio<CAT_PROVEEDOR>())
            {
                provedor = r.Filtro(l =>l.Id_Proveedor == id).FirstOrDefault();
            }

            return provedor;
        }

        public static CAT_PROVEEDORBRANCH agregaSucursal(CAT_PROVEEDORBRANCH proveedor)
        {
            CAT_PROVEEDORBRANCH obj;

            using (var r = new Repositorio<CAT_PROVEEDORBRANCH>())
            {
                obj = r.Agregar(proveedor);
            }

            return obj;
        }

        public static CAT_PROVEEDOR agregarMatriz(CAT_PROVEEDOR mat)
        {
            CAT_PROVEEDOR obj;

            using (var r = new Repositorio<CAT_PROVEEDOR>())
            {
                obj = r.Agregar(mat);
            }

            return obj;
        }

        public static List<CAT_PROVEEDORBRANCH> obtieneMatrizfisica(int id)
        {
            List<CAT_PROVEEDORBRANCH> lista = null;
            using (var r = new Repositorio<CAT_PROVEEDORBRANCH>())
            {
                lista = r.Filtro(l => l.Id_Proveedor == id && l.Tipo_Sucursal == "SB_F" && l.Cve_Estatus_Proveedor == 2).ToList();
            }
            return lista;
        }
  

        public List<datosProveedorBranch> obtienetodosXmatriz_Fisica(int id)
        {
            var h = (from branch in _context.CAT_PROVEEDORBRANCH
                    join estado in _context.CAT_ESTADO on branch.Cve_Estado_Fisc equals estado.Cve_Estado
                    join deleg in _context.CAT_DELEG_MUNICIPIO on branch.Cve_Deleg_Municipio_Fisc equals deleg.Cve_Deleg_Municipio
                    join codPos in _context.CAT_CODIGO_POSTAL_SEPOMEX on branch.Cve_CP_Part equals codPos.Cve_CP
                    join zona in _context.CAT_ZONA on branch.Cve_Zona equals zona.Cve_Zona
                    where branch.Id_Proveedor == id && branch.Tipo_Sucursal=="SB_F" && branch.Cve_Estatus_Proveedor ==2
                    select new datosProveedorBranch
                    {
                        Id_Branch = branch.Id_Branch,
                        Dx_Nombre_Comercial = branch.Dx_Nombre_Comercial,
                        Codigo_Postal = codPos.Codigo_Postal,//
                        Dx_Nombre_Estado = estado.Dx_Nombre_Estado,
                        Dx_Deleg_Municipio = deleg.Dx_Deleg_Municipio,
                        Dx_Colonia = codPos.Dx_Colonia,//
                        Dx_Domicilio_Part_Calle = branch.Dx_Domicilio_Part_Calle,
                        Dx_Domicilio_Part_Num = branch.Dx_Domicilio_Part_Num,
                        Dx_Nombre_Repre = branch.Nombre_Responsable,
                        Dx_Email_Repre = branch.Dx_Email_Repre,
                        Dx_Telefono_Repre = branch.Dx_Telefono_Repre,
                        Dx_Nombre_Repre_Legal = branch.Nombre_Rep_Legal,
                        Acta_Constitutiva = branch.Acta_Constitutiva,
                        Dx_Nombre_Zona = zona.Dx_Nombre_Zona,
                        Dx_Domicilio_Part_CP = branch.Dx_Domicilio_Part_CP,
                        Cve_Deleg_Municipio_Part = branch.Cve_Deleg_Municipio_Part,
                        Cve_Estado_Part=branch.Cve_Estado_Part,
                        Cve_Zona = branch.Cve_Zona,
                        
                        Apellido_Paterno_Resp = branch.Apellido_Paterno_Resp,
                        Apellido_Materno_Resp = branch.Apellido_Materno_Resp,

                        Apellido_Paterno_Rep_Legal = branch.Apellido_Paterno_Rep_Legal,
                        Apellido_Materno_Rep_Legal = branch.Apellido_Materno_Rep_Legal,

                      
                        
                    }).Distinct().ToList();

          

            //var query = h.OrderBy(c => c.Id_Branch).ToList();
            return h;
            
        }

        public List<datosSucursalVirtual> obtienetodos_Virtual2(int iddep, int Cve_Estado_Part, int Cve_Deleg_Municipio_Part, string Dx_Domicilio_Part_CP, string Dx_Nombre_Comercial)
        {

            var h = (from branch in _context.CAT_PROVEEDORBRANCH
                     join estado in _context.CAT_ESTADO on branch.Cve_Estado_Fisc equals estado.Cve_Estado
                     join deleg in _context.CAT_DELEG_MUNICIPIO on branch.Cve_Deleg_Municipio_Fisc equals deleg.Cve_Deleg_Municipio
                     join codPos in _context.CAT_CODIGO_POSTAL_SEPOMEX on branch.Cve_CP_Part equals codPos.Cve_CP
//                     join codPosF in _context.CAT_CODIGO_POSTAL_SEPOMEX on branch.Cve_CP_Fiscal equals codPos.Cve_CP
                     join zona in _context.CAT_ZONA on branch.Cve_Zona equals zona.Cve_Zona
                     join region in _context.CAT_REGION on branch.Cve_Region equals region.Cve_Region
                     where branch.id_Dependencia == iddep && branch.Tipo_Sucursal == "SB_V" && branch.Cve_Estatus_Proveedor ==2
                     select new datosSucursalVirtual
                     {
                         Id_Branch = branch.Id_Branch,

                         Cve_Region = branch.Cve_Region,
                         Dx_Nombre_Region = region.Dx_Nombre_Region,

                         Cve_Zona = branch.Cve_Zona,
                         Dx_Nombre_Zona = zona.Dx_Nombre_Zona,

                         Dx_Nombre_Estado = estado.Dx_Nombre_Estado,
                         Cve_Estado_Part = Cve_Estado_Part,

                         Cve_Deleg_Municipio_Part = Cve_Deleg_Municipio_Part,
                         Dx_Deleg_Municipio = deleg.Dx_Deleg_Municipio,

                         Dx_Domicilio_Part_CP = Dx_Domicilio_Part_CP,
                         Dx_Colonia = codPos.Dx_Colonia,
                        

                         Dx_Nombre_Comercial = branch.Dx_Nombre_Comercial,

                         id_Dependencia = branch.id_Dependencia,

                         DX_Nombre_Vinculado = Dx_Nombre_Comercial,

                         id_S_FIS = iddep,




                     }).Distinct().ToList();

            return h;

        }

        public List<datosSucursalVirtual> obtienetodos_Virtual(int iddep)
        {
            //var p = obtienetodosXmatriz_Fisica(id);

            var h = (from branch in _context.CAT_PROVEEDORBRANCH
                     join estado in _context.CAT_ESTADO on branch.Cve_Estado_Fisc equals estado.Cve_Estado
                     join deleg in _context.CAT_DELEG_MUNICIPIO on branch.Cve_Deleg_Municipio_Fisc equals deleg.Cve_Deleg_Municipio
                     join codPos in _context.CAT_CODIGO_POSTAL_SEPOMEX on branch.Dx_Domicilio_Fiscal_CP equals codPos.Codigo_Postal
                     join zona in _context.CAT_ZONA on branch.Cve_Zona equals zona.Cve_Zona
                     join region in _context.CAT_REGION on branch.Cve_Region equals region.Cve_Region
                     //join dependencia in _context.CAT_PROVEEDORBRANCH on branch.Id_Proveedor equals dependencia.Id_Proveedor
                     where branch.id_Dependencia == iddep && branch.Tipo_Sucursal == "SB_V"
                     select new datosSucursalVirtual
                     {
                         Id_Branch = branch.Id_Branch,

                         Cve_Region = branch.Cve_Region,
                         Dx_Nombre_Region = region.Dx_Nombre_Region,

                         Cve_Zona = branch.Cve_Zona,
                         Dx_Nombre_Zona = zona.Dx_Nombre_Zona,

                         Dx_Nombre_Estado = estado.Dx_Nombre_Estado,
                         //Cve_Estado_Part = Cve_Estado_Part,

                         //Cve_Deleg_Municipio_Part = Cve_Deleg_Municipio_Part,
                         Dx_Deleg_Municipio = deleg.Dx_Deleg_Municipio,

                         //Dx_Domicilio_Part_CP = Dx_Domicilio_Part_CP,
                         Dx_Colonia = codPos.Dx_Colonia,

                         Dx_Nombre_Comercial = branch.Dx_Nombre_Comercial,

                         id_Dependencia = branch.id_Dependencia,


                         id_S_FIS = iddep,




                     }).ToList();

            //var query = h.OrderBy(c => c.Id_Branch).ToList();
            return h;

        }
        public static List<CAT_PROVEEDORBRANCH> obtienetodosXmatriz_Virtual(int id)
        {
            List<CAT_PROVEEDORBRANCH> lista = null;
            using (var r = new Repositorio<CAT_PROVEEDORBRANCH>())
            {
                lista = r.Filtro(l => l.Id_Proveedor == id && l.Tipo_Sucursal == "SB_V"&&l.Cve_Estatus_Proveedor==2).ToList();
            }
            return lista;
        }

        public static CAT_PROVEEDORBRANCH BuscaNombreComercial(string nombreComercial)
        {
            CAT_PROVEEDORBRANCH lista = null;
            using (var r = new Repositorio<CAT_PROVEEDORBRANCH>())
            {
                lista = r.Filtro(l => l.Dx_Nombre_Comercial==nombreComercial && l.Cve_Estatus_Proveedor == 2).FirstOrDefault();
            }
            return lista;
        }

        public static CAT_PROVEEDORBRANCH BuscaNombreComercial_Virtual(string nombreComercial)
        {
            CAT_PROVEEDORBRANCH lista = null;
            using (var r = new Repositorio<CAT_PROVEEDORBRANCH>())
            {
                lista = r.Filtro(l => l.Dx_Nombre_Comercial == nombreComercial && l.Cve_Estatus_Proveedor == 2).FirstOrDefault();
            }
            return lista;
        }

        public static CRE_Credito buscaSolicitudCredito(int idBranch)
        {
            CRE_Credito credito = null;
            using (var r = new Repositorio<CRE_Credito>())
            {
                credito = r.Filtro(l => l.Id_Branch == idBranch).First();
            }
            return credito;
        }

        public static CAT_PROVEEDORBRANCH buscaAnterior(int idBranch)
        {
            CAT_PROVEEDORBRANCH objetoViejo = null;
            using(var r = new Repositorio<CAT_PROVEEDORBRANCH>())
            {
                objetoViejo = r.Filtro(l=>l.Id_Branch == idBranch).FirstOrDefault();
            }
            return objetoViejo;
        }

        public static CAT_PROVEEDORBRANCH objetoBus(int id_prov)
        {
            CAT_PROVEEDORBRANCH objetoViejo = null;
            using (var r = new Repositorio<CAT_PROVEEDORBRANCH>())
            {
                objetoViejo = r.Filtro(l => l.Id_Proveedor == id_prov).FirstOrDefault();
            }
            return objetoViejo;
        }

        public bool ActalizaSucursalFisica(CAT_PROVEEDORBRANCH update)
        {
            bool actualiza = false;

            using (var r = new Repositorio<CAT_PROVEEDORBRANCH>())
            {
                actualiza = r.Actualizar(update);
            }

            return actualiza;
        }

        public bool ActalizaSucursalVir(CAT_PROVEEDORBRANCH update)
        {
            bool actualiza = false;

            using (var r = new Repositorio<CAT_PROVEEDORBRANCH>())
            {
                actualiza = r.Actualizar(update);
            }

            return actualiza;
        }

        public bool ActualizarMatriz(CAT_PROVEEDOR update)
        {
            bool actualizo = false;
            using (var r = new Repositorio<CAT_PROVEEDOR>())
            {
                actualizo = r.Actualizar(update);
            }

            return actualizo;

        }

        public static List<CAT_PROVEEDORBRANCH> obtieneParaActualizar(int id)
        {
            List<CAT_PROVEEDORBRANCH> lista = null;
            using (var r = new Repositorio<CAT_PROVEEDORBRANCH>())
            {
                lista = r.Filtro(l => l.Id_Proveedor == id && l.Tipo_Sucursal=="SB_F" && l.Cve_Estatus_Proveedor == 2).ToList();
            }
            return lista;
        }


        public static List<CAT_PROVEEDORBRANCH> obtieneParaActualizarVirtual(int id)
        {
            List<CAT_PROVEEDORBRANCH> lista = null;
            using (var r = new Repositorio<CAT_PROVEEDORBRANCH>())
            {
                lista = r.Filtro(l => l.Id_Proveedor == id && l.Tipo_Sucursal == "SB_V" && l.Cve_Estatus_Proveedor == 2).ToList();
            }
            return lista;
        }

        public static List<CAT_ESTATUS_PROVEEDOR> catalogoEstatus()
        {
            PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();
            List<CAT_ESTATUS_PROVEEDOR> n = (from status in _contexto.CAT_ESTATUS_PROVEEDOR
                                             select status).ToList();
            return n;
        }

        public static List<CAT_TIPO_SOCIEDAD> tipoPersona()
        {
            List<CAT_TIPO_SOCIEDAD> lista = null;

            using (var r = new Repositorio<CAT_TIPO_SOCIEDAD>())
            {
                lista = r.Filtro(l => l.Estatus != 0).ToList();
            }
            return lista;
        }
    
    }
    
}
