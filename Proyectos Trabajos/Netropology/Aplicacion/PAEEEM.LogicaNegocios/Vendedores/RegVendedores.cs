using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.AccesoDatos.Vendedores;
using PAEEEM.Entidades;
using PAEEEM.Entidades.Vendedores;


namespace PAEEEM.LogicaNegocios.Vendedores
{
    public class RegVendedores
    {
        public List<DatosVendedoresDist> ConsultarVendedores(string curp, string nombre, int estatus, int idBranch)
        {
            var resul = new RegistrarVendedor().ConsultarVendedores(curp,nombre,estatus,idBranch);
            return resul;
        }

        public List<DatosVendedoresDist> ConsultarVendedoresInicial(int idBranch)
        {
            var resul = new RegistrarVendedor().ConsultarVendedoresInicial(idBranch);
            return resul;
        }

        public VENDEDORES GuardarVendedor(VENDEDORES informacion)
        {
            var resul = new RegistrarVendedor().GuardarVendedor(informacion);
            return resul;
        }

        public RELACION_VENDEDOR_DISTRIBUIDOR GuardarRegistro(RELACION_VENDEDOR_DISTRIBUIDOR informacion)
        {
            var resul = new RegistrarVendedor().GuardarRegistro(informacion);    
            return resul;
        }

        public VENDEDORES ObtenerVendedor(string curp)
        {
            var resul = new RegistrarVendedor().ObtenerVendedor(curp);
            return resul;
        }

        public List<TipoIdentificacion> ObtenerTipoIdentificacionVendedores()
        {
            var resul = new RegistrarVendedor().ObtenerTipoIdentificacionVendedores();
            return resul;
        }

        public bool ObtenerRelacionVendedorDist(int idVendedor, int idBranch)
        {
            var resul = new RegistrarVendedor().ObtenerRelacionVendedorDist(idVendedor, idBranch);
            return resul;
        }

        public List<ESTATUS_VENDEDORES> ObtenerEstatusVendedores()
        {
            var resul = new RegistrarVendedor().ObtenerEstatusVendedores();
            return resul;
        }

        public ANOMALIAS_VENDEDORES ObtenerAnomalias(string curp)
        {
            var resul = new RegistrarVendedor().ObtenerAnomalias(curp);
            return resul;
        }
    }
}
