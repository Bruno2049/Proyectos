using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.AccesoDatos.ModuloCentral;
using PAEEEM.Entidades.ModuloCentral;
using PAEEEM.Entidades;

namespace PAEEEM.LogicaNegocios.ModuloCentral
{
    public class HistoricoProveedor
    {
        public static List<DatHistoricoProveedores> histoProveedor(string NC, string RS, int Reg, int Zon, int Status)
        {
            return DatosConsultHistoricoProveedores.historicoProveedor(NC,RS,Reg,Zon,Status);
        }

        public static List<CAT_ESTATUS_PROVEEDOR> catstatus()
        {
            List<CAT_ESTATUS_PROVEEDOR> sta = AccesoDatos.Catalogos.CAT_PROVEEDOR_ALTAPRO.catalogoEstatus();
            return sta;
        }
    }
}
