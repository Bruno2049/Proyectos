using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using PAEEEM.Entidades;
using PAEEEM.Entidades.Vendedores;

namespace PAEEEM.AccesoDatos.Vendedores
{
    public class ValidarVendedor
    {
        private  PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();

        public List<ESTATUS_VENDEDORES> ObtenerEstatusVendedores()
        {
            var estatus = from est in _contexto.ESTATUS_VENDEDORES
                          select est;

            return estatus.ToList();
        }

        public List<CAT_ZONA> CatZonaid(int idZona)
        {
            List<CAT_ZONA> zon = null;
            using (var r = new Repositorio<CAT_ZONA>())
            {
                zon = r.Filtro(me => me.Cve_Zona == idZona);
            }
            return zon;
        }

        public List<CAT_REGION> CatRegion(int idRegion)
        {
            List<CAT_REGION> reg = null;
            using (var r = new Repositorio<CAT_REGION>())
            {
                reg = r.Filtro(me => me.Cve_Region == idRegion);
            }
            return reg;
        }

        public List<CAT_ZONA> CatZonaxIdRegion(int idRegion)
        {
            List<CAT_ZONA> zon = null;
            using (var r = new Repositorio<CAT_ZONA>())
            {
                zon = r.Filtro(me => me.Cve_Region == idRegion);
            }
            return zon;
        }

        public List<DatosVendedoresRegZon> DatosVendedores(string curp, string nombre, int estatus, int reg, int zon,string distrRs, string distrNc)
        {

            var query1 = from ven in _contexto.VENDEDORES
                         join relacion in _contexto.RELACION_VENDEDOR_DISTRIBUIDOR on ven.ID_VENDEDOR equals relacion.ID_VENDEDOR
                         join est in _contexto.ESTATUS_VENDEDORES on ven.Cve_Estatus_Vendedor equals est.Cve_Estatus_Vendedor
                         join tipoIdent in _contexto.TIPO_IDENTIFICACION_VENDEDORES on ven.ID_TIPO equals tipoIdent.ID_TIPO
                         join anomalia in _contexto.ANOMALIAS_VENDEDORES on ven.ID_VENDEDOR equals anomalia.ID_VENDEDOR into leftVen
                         from anomalia in leftVen.DefaultIfEmpty()
                         join catPb in _contexto.CAT_PROVEEDORBRANCH on relacion.Id_Branch equals catPb.Id_Branch 
                         join catR in _contexto.CAT_REGION on  catPb.Cve_Region equals catR.Cve_Region
                         join catZ in _contexto.CAT_ZONA on  catPb.Cve_Zona equals catZ.Cve_Zona

                         where (curp == "" || ven.CURP == curp)
                                 && (nombre == "" ? 1 == 1 : ven.NOMBRE == nombre)
                                 && (estatus == 0 ? 1 == 1 : ven.Cve_Estatus_Vendedor == estatus)
                                 && (reg == 0 ? 1 == 1 : reg ==  catPb.Cve_Region)
                                 && (distrRs == "" ? 1 == 1 : distrRs == catPb.Dx_Razon_Social)
                                 && (distrNc == "" ? 1 == 1 : distrNc == catPb.Dx_Nombre_Comercial)
                                 && (zon == 0 ? 1 == 1 : zon == catPb.Cve_Zona)

                         select new DatosVendedoresRegZon
                         {
                             IdDistribuidor = catPb.Id_Branch,
                             DistrNC = catPb.Dx_Nombre_Comercial,
                             DistrRS = catPb.Dx_Razon_Social,
                             IdVendedor = ven.ID_VENDEDOR,
                             Nombre = ven.NOMBRE + " " + ven.APELLIDO_PATERNO + " " + ven.APELLIDO_MATERNO,
                             Curp = ven.CURP,
                             FechaNacimiento = ven.FEC_NACIMINIENTO,
                             Region = catR.Dx_Nombre_Region,
                             Zona = catZ.Dx_Nombre_Zona,
                             TipoIdentificacion = tipoIdent.DESCRIPCION,
                             NoIdentificacion = ven.NUMERO_IDENTIFICACION,
                             AccesoSistema = ven.ACCESO_SISTEMA ? "SI" : "NO",
                             Incidencia = anomalia!=null ? "SI":"NO",
                             Anomalia = anomalia != null ? anomalia.DESCRIPCION : "",
                             Estatus = est.Descripcion,// ESTATUS"></telerik:GridBoundColumn>
                             Archivo = ven.FOTO_IDENTIFICACION
                         };


            var query = query1.ToList();// OrderBy(c => c.No_Credito).ToList();
            return query;
        }

        public ANOMALIAS_VENDEDORES GuardarAnomalia(ANOMALIAS_VENDEDORES informacion)
        {
            ANOMALIAS_VENDEDORES newAnomalia = null;

            using (var r = new Repositorio<ANOMALIAS_VENDEDORES>())
            {
                newAnomalia = r.Agregar(informacion);
            }

            return newAnomalia;
        }

        public bool ActualizarVendedor(VENDEDORES informacion)
        {
            bool actualiza;

            using (var r = new Repositorio<VENDEDORES>())
            {
                actualiza = r.Actualizar(informacion);
            }

            return actualiza;
        }

        public VENDEDORES ObtienePorId(int idVendedor)
        {
            VENDEDORES InfoGeneral;

            using (var r = new Repositorio<VENDEDORES>())
            {
                InfoGeneral = r.Extraer(me => me.ID_VENDEDOR == idVendedor);
            }
            return InfoGeneral;
        }

        public VENDEDORES ObtienePorCurp(string curp)
        {
            VENDEDORES InfoGeneral;

            using (var r = new Repositorio<VENDEDORES>())
            {
                InfoGeneral = r.Extraer(me => me.CURP == curp);
            }
            return InfoGeneral;
        }

        public bool ExisteUsuario(int idVendedor,int idDepartamento)
        {
            US_USUARIO relacion = null;

            using (var r = new Repositorio<US_USUARIO>())
            {
                relacion = r.Extraer(me => me.ID_VENDEDOR == idVendedor && me.Id_Departamento == idDepartamento);
            }

            return relacion != null;
        }

        public US_USUARIO GuardarUsuario(US_USUARIO informacion)
        {
            US_USUARIO newAnomalia = null;

            using (var r = new Repositorio<US_USUARIO>())
            {
                newAnomalia = r.Agregar(informacion);
            }

            return newAnomalia;
        }

        public List<US_USUARIO> ObtenerUsuarios(int idVendedor)
        {
            var usuarios = from us in _contexto.US_USUARIO
                where us.ID_VENDEDOR == idVendedor
                select us;
            return usuarios.ToList();
        }

        public bool ActualizarUsuario(US_USUARIO informacion)
        {
            bool actualiza;

            //using (var r = new Repositorio<US_USUARIO>())
            //{
            //    actualiza = r.Actualizar(informacion);
            //}

            var us = (from u in _contexto.US_USUARIO
                     where u.Id_Usuario == informacion.Id_Usuario
                     select u).FirstOrDefault();
            us.Estatus = informacion.Estatus;
            
            actualiza =_contexto.SaveChanges()>0;

            return actualiza;
        }
    }
}
