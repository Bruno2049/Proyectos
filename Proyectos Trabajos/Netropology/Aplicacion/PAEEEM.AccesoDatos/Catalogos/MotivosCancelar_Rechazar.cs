using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.Entidades.CancelarRechazar;
using PAEEEM.Entidades;

namespace PAEEEM.AccesoDatos.Catalogos
{
    public class MotivosCancelar_Rechazar
    {
        PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();

        public static List<CAT_Motivos> cat_motivos(int rol, int estatus) 
        {
             PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();

            var datos = (from motivo in _contexto.MOTIVOS_RECHAZOS_CANCELACIONES
                         join relacion in _contexto.RELACION_ROL_MOTIVO_CANCELACION on new { m = (int)motivo.ID_MOTIVO } equals new { m = (int)relacion.ID_MOTIVO }

                         where relacion.Id_Rol == rol && motivo.Cve_Estatus_Credito == estatus

                         select new CAT_Motivos
                         {
                             motivo = motivo.DESCRIPCION,
                             id_Motivo=motivo.ID_MOTIVO
                         }).OrderBy(m => m.motivo);

            return datos.ToList();
        }

       
    }
}
