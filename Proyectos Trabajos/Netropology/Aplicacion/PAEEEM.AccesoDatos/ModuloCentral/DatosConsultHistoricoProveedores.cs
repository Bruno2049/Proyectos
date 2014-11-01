using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.Entidades;
using PAEEEM.Entidades.ModuloCentral;

namespace PAEEEM.AccesoDatos.ModuloCentral
{
    public class DatosConsultHistoricoProveedores
    {
        public DatosConsultHistoricoProveedores() { }

        public static List<DatHistoricoProveedores> historicoProveedor(string NC, string RS, int Reg, int Zon, int Status)
        {
            PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();

            var P = (from catP in _contexto.CAT_PROVEEDOR.AsEnumerable()
                     join catReg in _contexto.CAT_REGION on catP.Cve_Region equals catReg.Cve_Region
                     join catZon in _contexto.CAT_ZONA on catP.Cve_Zona equals catZon.Cve_Zona
                     join klo in _contexto.K_LOG on new { id = (int)catP.Id_Proveedor } equals new { id = int.Parse(klo.IDTIPOPROCESO == 2 ? klo.TAREA_LOTE_NOCRED : "0") }
                     join staP in _contexto.CAT_ESTATUS_PROVEEDOR on new { id = int.Parse(klo.DATOSACTUALES!=""?klo.DATOSACTUALES[klo.DATOSACTUALES.Length-1].ToString():"0") } equals new {id=(int)staP.Cve_Estatus_Proveedor }
                     join usu in _contexto.US_USUARIO on klo.IDUSUARIO equals usu.Id_Usuario

                     where (NC == "" ? 1 == 1 : catP.Dx_Nombre_Comercial == NC)
                          && (RS == "" ? 1 == 1 : catP.Dx_Razon_Social == RS)
                          && (Reg == 0 ? 1 == 1 : catReg.Cve_Region == Reg)
                          && (Zon == 0 ? 1 == 1 : catZon.Cve_Zona == Zon)
                          && (Status == 0 ? 1 == 1 : staP.Cve_Estatus_Proveedor == Status)
                     select new DatHistoricoProveedores
                     {
                         region = catReg.Dx_Nombre_Region,
                         zona = catZon.Dx_Nombre_Zona,
                         idProveedor = catP.Id_Proveedor,
                         tipo = "MATRIZ",
                         NomNC = catP.Dx_Nombre_Comercial,
                         NomRS = catP.Dx_Razon_Social,
                         Status = staP.Dx_Estatus_Proveedor,
                         fechaEstatus = klo.FECHA_ADICION,
                         motivo = klo.MOTIVO,
                         usuario = usu.Nombre_Usuario
                     }).OrderBy(O => O.idProveedor).ToList();


            var PB = (from catPB in _contexto.CAT_PROVEEDORBRANCH.AsEnumerable()
                     join catReg in _contexto.CAT_REGION on catPB.Cve_Region equals catReg.Cve_Region
                     join catZon in _contexto.CAT_ZONA on catPB.Cve_Zona equals catZon.Cve_Zona
                     join klo in _contexto.K_LOG on new { id = (int)catPB.Id_Branch } equals new { id = int.Parse(klo.IDTIPOPROCESO == 2 ? klo.TAREA_LOTE_NOCRED : "0") }
                     join staP in _contexto.CAT_ESTATUS_PROVEEDOR on new { id = int.Parse(klo.DATOSACTUALES != "" ? klo.DATOSACTUALES[klo.DATOSACTUALES.Length - 1].ToString() : "0") } equals new { id = (int)staP.Cve_Estatus_Proveedor }
                     join usu in _contexto.US_USUARIO on klo.IDUSUARIO equals usu.Id_Usuario

                      where (NC == "" ? 1 == 1 : catPB.Dx_Nombre_Comercial == NC)
                           && (RS == "" ? 1 == 1 : catPB.Dx_Razon_Social == RS)
                           && (Reg == 0 ? 1 == 1 : catReg.Cve_Region == Reg)
                           && (Zon == 0 ? 1 == 1 : catZon.Cve_Zona == Zon)
                           && (Status == 0 ? 1 == 1 : staP.Cve_Estatus_Proveedor == Status)
                     select new DatHistoricoProveedores
                     {
                         region = catReg.Dx_Nombre_Region,
                         zona = catZon.Dx_Nombre_Zona,
                         idProveedor = catPB.Id_Branch,
                         tipo = catPB.Tipo_Sucursal == "SB_F" ? "SUCURSAL FÍSICA" : "SUCURSAL VIRTUAL",
                         NomNC = catPB.Dx_Nombre_Comercial,
                         NomRS = catPB.Dx_Razon_Social,
                         Status = staP.Dx_Estatus_Proveedor,
                         fechaEstatus = klo.FECHA_ADICION,
                         motivo = klo.MOTIVO,
                         usuario = usu.Nombre_Usuario
                     }).OrderBy(O => O.idProveedor).ToList();


            return P.Concat(PB).OrderBy(o => o.idProveedor).ToList();
        }



    

      
    }
}
