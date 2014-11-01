using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Data;


/// <summary>
/// Descripción breve de CeC_Publicidad
/// </summary>
public class CeC_Publicidad
{
    public CeC_Publicidad()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public static byte[] ObtenImagen(int PublicidadID, DateTime FechaHoraMinima)
    {
        DateTime Modificacion = CeC_Autonumerico.ObtenFechaModificacion("EC_PUBLICIDAD", "PUBLICIDAD_ID", PublicidadID, null);
        if (Modificacion < FechaHoraMinima)
            return null;
        return CeC_BD.ObtenBinario("EC_PUBLICIDAD", "PUBLICIDAD_ID", PublicidadID, "PUBLICIDAD");
    }
    public static DataSet ObtenListado(int TipoPublicidad)
    {
        return CeC_BD.EjecutaDataSet("SELECT PUBLICIDAD_ID, SUSCRIPCION_ID, TIPO_PUBLICIDAD_ID, PUBLICIDAD_NOMBRE, " +
            " PUBLICIDAD_COMENTARIO, PUBLICIDAD_LINK, PUBLICIDAD_ORDEN, PUBLICIDAD_DESDE, PUBLICIDAD_HASTA, PUBLICIDAD_SEGUNDOS, PUBLICIDAD_BORRADO " +
            " FROM EC_PUBLICIDAD " +
            " WHERE PUBLICIDAD_BORRADO = 0 AND TIPO_PUBLICIDAD_ID = " + TipoPublicidad);

    }

}