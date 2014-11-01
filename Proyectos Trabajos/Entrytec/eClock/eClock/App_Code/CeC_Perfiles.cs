using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

/// <summary>
/// Descripción breve de CeC_Perfiles
/// </summary>
public class CeC_Perfiles
{
	public CeC_Perfiles()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}
    /// <summary>
    /// Obtiene la lista de los Perfiles del Sistema
    /// </summary>
    /// <param name="PerfilBorrado">Si se desea ver los perfiles borrados se estable en true</param>
    /// <returns>DataSet con los datos de los Perfiles</returns>
    public static DataSet ObtenPerfilesDS(bool MostrarBorrados)
    {
        string Qry = " SELECT PERFIL_ID, PERFIL_NOMBRE " +
                     " FROM EC_PERFILES ";
        if (!MostrarBorrados)
        {
            Qry += " WHERE PERFIL_BORRADO = 0 ";
        }
        return (DataSet)CeC_BD.EjecutaDataSet(Qry);
    }
}