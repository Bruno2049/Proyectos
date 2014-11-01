using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.Entidades;
using PAEEEM.AccesoDatos;
using PAEEEM.Entidades.ModuloCentral;

namespace PAEEEM.LogicaNegocios.ModuloCentral
{
   public class AltaProveedor
    {
       public static CAT_PROVEEDOR buscarProveedorRFC(string RFC)
       {
           CAT_PROVEEDOR proveedor = null;
           using (var r = new Repositorio<CAT_PROVEEDOR>())
           {
               proveedor = r.Filtro(l=>l.Dx_RFC == RFC).FirstOrDefault();
           }
           return proveedor;
       }

       public static List<CAT_PROVEEDOR> obtieneTodos()
       {

           List<CAT_PROVEEDOR> todos = AccesoDatos.Catalogos.CAT_PROVEEDOR_ALTAPRO.todosProveedores();
               
            return todos;
       }

       public static CAT_PROVEEDOR obtieneProvID(int id)
       {
           CAT_PROVEEDOR proveedor = AccesoDatos.Catalogos.CAT_PROVEEDOR_ALTAPRO.obtieneProveedorPorID(id);
           return proveedor;
       }

       public static CAT_PROVEEDOR obtienePORidConsulta(int id)
       {
           CAT_PROVEEDOR proveedor = AccesoDatos.Catalogos.CAT_PROVEEDOR_ALTAPRO.obtieneProveedorXidConsulta(id);
           return proveedor;
       }

       public static CAT_PROVEEDORBRANCH agregarSucursal(CAT_PROVEEDORBRANCH objeto)
       {
           CAT_PROVEEDORBRANCH inserta = AccesoDatos.Catalogos.CAT_PROVEEDOR_ALTAPRO.agregaSucursal(objeto);
           return inserta;

       }

       public static CAT_PROVEEDOR agregarMatriz(CAT_PROVEEDOR objeto)
       {
           CAT_PROVEEDOR inserta = AccesoDatos.Catalogos.CAT_PROVEEDOR_ALTAPRO.agregarMatriz(objeto);
           return inserta;
       }

       public List<datosProveedorBranch> obtieneSucursales_x_idMatriz_Fisica(int id)
       {
           var lista = new AccesoDatos.Catalogos.CAT_PROVEEDOR_ALTAPRO().obtienetodosXmatriz_Fisica(id);
           return lista;
       }

       public List<datosSucursalVirtual> listaobtieneSucursales_x_idMatriz_Virtual_lista2(int id, int Cve_Estado_Part, int Cve_Deleg_Municipio_Part, string Dx_Domicilio_Part_CP, string Dx_Nombre_Comercial)
       {
           var lista = new AccesoDatos.Catalogos.CAT_PROVEEDOR_ALTAPRO().obtienetodos_Virtual2(id,Cve_Estado_Part, Cve_Deleg_Municipio_Part, Dx_Domicilio_Part_CP,Dx_Nombre_Comercial);
           return lista;
       }

       public List<datosSucursalVirtual> listaobtieneSucursales_x_idMatriz_Virtual_lista(int id)
       {
           var lista = new AccesoDatos.Catalogos.CAT_PROVEEDOR_ALTAPRO().obtienetodos_Virtual(id);
           return lista;
       }
       public static List<CAT_PROVEEDORBRANCH> obtieneSucursales_Fisica(int id)
       {
           var lista = AccesoDatos.Catalogos.CAT_PROVEEDOR_ALTAPRO.obtieneMatrizfisica(id);
           return lista;
       }

       public static List<CAT_PROVEEDORBRANCH> obtieneSucursales_x_idMatriz_Virtual(int id)
       {
           List<CAT_PROVEEDORBRANCH> lista = AccesoDatos.Catalogos.CAT_PROVEEDOR_ALTAPRO.obtienetodosXmatriz_Virtual(id);
           return lista;
       }

       public static CAT_PROVEEDORBRANCH buscarSucursalFisica(string nomComercial)
       {
           CAT_PROVEEDORBRANCH objeto = AccesoDatos.Catalogos.CAT_PROVEEDOR_ALTAPRO.BuscaNombreComercial(nomComercial);

           return objeto;
       }

       public static CAT_PROVEEDORBRANCH buscaSucursalVirtual(string nombreComercial)
       {
           CAT_PROVEEDORBRANCH objeto = AccesoDatos.Catalogos.CAT_PROVEEDOR_ALTAPRO.BuscaNombreComercial_Virtual(nombreComercial);
           return objeto;
       }

       public static CRE_Credito tieneSolicitud(int idBranch)
       {
           CRE_Credito objeto = AccesoDatos.Catalogos.CAT_PROVEEDOR_ALTAPRO.buscaSolicitudCredito(idBranch);
           return objeto;
       }

       public static CAT_PROVEEDORBRANCH objetoViejo(int id_brach)
       {
           CAT_PROVEEDORBRANCH objetoviejo = AccesoDatos.Catalogos.CAT_PROVEEDOR_ALTAPRO.buscaAnterior(id_brach);
           return objetoviejo;
       }

       public static CAT_PROVEEDORBRANCH objetoConsulta(int id_prov)
       {
           CAT_PROVEEDORBRANCH objetoviejo = AccesoDatos.Catalogos.CAT_PROVEEDOR_ALTAPRO.objetoBus(id_prov);
           return objetoviejo;
       }

       public bool ActalizaSucursalFisica(CAT_PROVEEDORBRANCH update)
       {
           return new AccesoDatos.Catalogos.CAT_PROVEEDOR_ALTAPRO().ActalizaSucursalFisica(update);

       }

       public bool actualizarSucursalVirtual(CAT_PROVEEDORBRANCH updateVir)
       {
           return new AccesoDatos.Catalogos.CAT_PROVEEDOR_ALTAPRO().ActalizaSucursalVir(updateVir);
       }

       public bool actualizaMatriz(CAT_PROVEEDOR updateMatriz)
       {
           return new AccesoDatos.Catalogos.CAT_PROVEEDOR_ALTAPRO().ActualizarMatriz(updateMatriz);
       }

       public static List<CAT_PROVEEDORBRANCH> listaActualizar(int id_Sucursal)
       {
           List<CAT_PROVEEDORBRANCH> listaARenovar = AccesoDatos.Catalogos.CAT_PROVEEDOR_ALTAPRO.obtieneParaActualizar(id_Sucursal);
               return listaARenovar;
       }

       public static List<CAT_PROVEEDORBRANCH> listaActualizarVirtual(int id_Sucursal)
       {
           List<CAT_PROVEEDORBRANCH> listaARenovar = AccesoDatos.Catalogos.CAT_PROVEEDOR_ALTAPRO.obtieneParaActualizarVirtual(id_Sucursal);
           return listaARenovar;
       }

       public static List<CAT_TIPO_SOCIEDAD> tipoPersonas()
       {

           List<CAT_TIPO_SOCIEDAD> todos = AccesoDatos.Catalogos.CAT_PROVEEDOR_ALTAPRO.tipoPersona();

           return todos;
       }

    }
}
