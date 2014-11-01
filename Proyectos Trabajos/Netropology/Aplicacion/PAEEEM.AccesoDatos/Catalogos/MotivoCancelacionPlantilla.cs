using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.Entidades;
using PAEEEM.Entidades.CancelarRechazar;

using PAEEEM.Entidades;
using PAEEEM.Entidades.ModuloCentral;
using PAEEEM.AccesoDatos.Catalogos;
using System.Linq.Expressions;


namespace PAEEEM.AccesoDatos.Catalogos
{
    public class MotivoCancelacionPlantilla
    {
        public static List<PlantillaMotivoCancel> datosemail(string credit)
        {
            PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();
            var dat = (from cre in _contexto.CRE_Credito
                       join cli in _contexto.CLI_Cliente on new { CP = (int)cre.Id_Proveedor, CB = (int)cre.Id_Branch, CC = (int)cre.IdCliente } equals new { CP = (int)cli.Id_Proveedor, CB = (int)cli.Id_Branch, CC = (int)cli.IdCliente }
                       join usu in _contexto.US_USUARIO on (int)cre.Id_Proveedor equals (int)usu.Id_Departamento

                       where cre.No_Credito == credit

                       select new PlantillaMotivoCancel
                       {
                           addres = usu.CorreoElectronico,
                           usuario = usu.Nombre_Usuario,
                           nocredito = cre.No_Credito,
                           rpu = cre.RPU,
                           nombre = cli.Nombre + " " + cli.Ap_Paterno + " " + cli.Ap_Materno

                       }).ToList();

            return dat;

        }
    }
}
