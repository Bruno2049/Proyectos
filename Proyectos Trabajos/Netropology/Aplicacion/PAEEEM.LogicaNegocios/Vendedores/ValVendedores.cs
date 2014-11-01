using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.AccesoDatos.Catalogos;
using PAEEEM.AccesoDatos.Vendedores;
using PAEEEM.Entidades;
using PAEEEM.Entidades.Vendedores;

namespace PAEEEM.LogicaNegocios.Vendedores
{
    public class ValVendedores
    {
        public List<CAT_ZONA> catZona(int idZona)
        {
            var zon = new ValidarVendedor().CatZonaid(idZona);
            return zon;
        }

        public List<CAT_ZONA> CatZonaxIdRegion(int idRegion)
        {
            var zon = new ValidarVendedor().CatZonaxIdRegion(idRegion);
            return zon;
        }

        public List<CAT_REGION> CatRegion(int idRegion)
        {
            var reg = new ValidarVendedor().CatRegion(idRegion);
            return reg;
        }

        public List<ESTATUS_VENDEDORES> ObtenerEstatusVendedores()
        {
            var estatus = new ValidarVendedor().ObtenerEstatusVendedores();
            return estatus;
        }

        public List<DatosVendedoresRegZon> DatosVendedores(string curp, string nombre, int estatus, int reg, int zon,
            string distrRs, string distrNc)
        {
            var resul = new ValidarVendedor().DatosVendedores(curp, nombre, estatus, reg, zon, distrRs, distrNc);
            return resul;
        }

        public ANOMALIAS_VENDEDORES GuardarAnomalia(ANOMALIAS_VENDEDORES informacion)
        {
            var resul = new ValidarVendedor().GuardarAnomalia(informacion);
            return resul;
        }

        public bool ActualizarVendedor(VENDEDORES informacion)
        {
            var resul = new ValidarVendedor().ActualizarVendedor(informacion);
            return resul;
        }

        public VENDEDORES ObtienePorId(int idVendedor)
        {
            var resul = new ValidarVendedor().ObtienePorId(idVendedor);
            return resul;
        }

        public VENDEDORES ObtienePorCurp(string curp)
        {
            var resul = new ValidarVendedor().ObtienePorCurp(curp);
            return resul;
        }

        public bool ExisteUsuario(int idVendedor,int idDepartamento)
        {
            var resul = new ValidarVendedor().ExisteUsuario(idVendedor,idDepartamento);
            return resul;
        }

        public US_USUARIO GuardarUsuario(US_USUARIO informacion)
        {
            var resul = new ValidarVendedor().GuardarUsuario(informacion);
            return resul;
        }

        public List<US_USUARIO> ObtenerUsuarios(int idVendedor)
        {
            var resul = new ValidarVendedor().ObtenerUsuarios(idVendedor);
            return resul;
        }

        public bool ActualizarUsuario(US_USUARIO informacion)
        {
            return new ValidarVendedor().ActualizarUsuario(informacion);;
        }
    }
}
