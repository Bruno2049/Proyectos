using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.Entidades.CancelarRechazar;

namespace PAEEEM.LogicaNegocios.Credito
{
    public class CAT_MotivosCancelRechaza
    {

        public static List<CAT_Motivos> cat_motivos(int rol,int status)
        {
            List<CAT_Motivos> datos = AccesoDatos.Catalogos.MotivosCancelar_Rechazar.cat_motivos(rol, status);


            return datos;
        }
    }
}
