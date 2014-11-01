using System;
using System.Data;

/// <summary>
/// Descripción breve de CeC_Restricciones.
/// </summary>
public class CeC_Restricciones
{
    /// <summary>
    /// Verifica si un perfil tiene derecho sobre determinada restricción
    /// </summary>
    /// <param name="Perfil_ID"></param>
    /// <param name="Restriccion"></param>
    /// <returns></returns>
    public static bool TieneDerecho(int Perfil_ID, string Restriccion)
    {
        string Qry = "";
        string sRestriccion = Restriccion.Trim();
        while (true)
        {
            Qry = CeC.AgregaSeparador(Qry, "RESTRICCION='" + sRestriccion + "'", " OR ");
            int Punto = sRestriccion.LastIndexOf('.');
            if (Punto > 0)
                sRestriccion = sRestriccion.Substring(0, Punto);
            else
                break;

        }
        if (CeC_BD.EjecutaEscalarInt("SELECT COUNT(*) AS NO FROM EC_RESTRICCIONES INNER JOIN EC_RESTRICCIONES_PERFILES ON " +
            "EC_RESTRICCIONES.RESTRICCION_ID = EC_RESTRICCIONES_PERFILES.RESTRICCION_ID " +
            "WHERE PERFIL_ID = " + Perfil_ID + " AND EC_RESTRICCIONES_PERFILES.RESTRICCION_ID >= 999 AND (" + Qry + ")") > 0)
            return true;
        return false;
    }
    /// <summary>
    /// Verifica si un perfil tiene derecho sobre determinadas restricciones
    /// </summary>
    /// <param name="Perfil_ID"></param>
    /// <param name="Restricciones">Listado de restricciones separadas por coma</param>
    /// <returns>una cadena con 1 por cada restriccion autorizada y 0 por cada restriccion rechazada</returns>
    public static string TieneDerechos(int Perfil_ID, string Restricciones)
    {
        string[] sRestricciones = CeC.ObtenArregoSeparador(Restricciones, ",");
        string Resultado = "";
        /*
        foreach (string Restriccion in sRestricciones)
        {
            if (TieneDerecho(Perfil_ID, Restriccion))
                Resultado += "1";
            else
                Resultado += "0";
        }*/
        string QryIn = "";
        foreach (string Restriccion in sRestricciones)
        {
            QryIn = CeC.AgregaSeparador(QryIn, Restriccion, "',\n'");
        }
        QryIn = "'" + QryIn + "'";

        string Qry = "SELECT RESTRICCION FROM EC_V_RESTRICCIONES_PERFILES \n "+
            "WHERE RESTRICCION IN ("+ QryIn +") AND PERFIL_ID = " + Perfil_ID;
         DataSet DS = CeC_BD.EjecutaDataSet(Qry);
        if(DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
        {
            
            foreach (string Restriccion in sRestricciones)
            {
                string AAgregar = "0";
                foreach (DataRow DR in DS.Tables[0].Rows)
                {
                    if (Restriccion == CeC.Convierte2String(DR[0]))
                    {
                        AAgregar = "1";
                        break;
                    }
                }
                Resultado += AAgregar;
            }


        }

        /*
        SELECT        dbo.EC_RESTRICCIONES.RESTRICCION, dbo.EC_RESTRICCIONES_PERFILES.PERFIL_ID
FROM            dbo.EC_RESTRICCIONES AS EC_RESTRICCIONES_1 INNER JOIN
                         dbo.EC_RESTRICCIONES_PERFILES ON EC_RESTRICCIONES_1.RESTRICCION_ID = dbo.EC_RESTRICCIONES_PERFILES.RESTRICCION_ID CROSS JOIN
                         dbo.EC_RESTRICCIONES
WHERE        (dbo.EC_RESTRICCIONES.RESTRICCION + '.' LIKE EC_RESTRICCIONES_1.RESTRICCION + '%')
          */

        return Resultado;
    }

    /*		public bool ValidaRestriccion (string Restriccion,int Perfil_ID)
    {
        char[] Separador = new char[]{Convert.ToChar(".")};
        string Restriccion_Array = Restriccion.Split(Separador);
        string permiso = "";
        string[] Res_IdArray = new string[Restriccion_Array.Length];
        string Qry = "";
        ///Regresa el ID de los Permisos Correspondientes
        for (int i = 0; i<Restriccion_Array.Length; i++)
        {
            permiso += Restriccion_Array[i]; 
             Qry = "SELECT RESTRICCION_ID FROM EC_RESTRICCIONES WHERE RESTRICCION = '"+permiso+"'";
            if(CeC_BD.EjecutaEscalarInt(Qry)> -9999)
                Res_IdArray[i] = CeC_BD.EjecutaEscalarInt(Qry);
            else
                Res_IdArray[i] = 0;
        }
        Qry = ""; Qry = "SELECT RESTRICCION_ID FROM eC_RESTRICIIONES_PERFILES WHERE PERFIL_ID = ";
    }
    */
    /// <summary>
    /// Verifica si existe una restriccion y manda verdadero
    /// </summary>
    /// <param name="Restriccion"></param>
    /// <returns></returns>
    public static bool ExisteRestriccion(string Restriccion)
    {
        String Qry = "SELECT RESTRICCION_ID FROM EC_RESTRICCIONES WHERE RESTRICCION = '" + Restriccion + "' or RESTRICCION = '''" + Restriccion + "'''";
        if (CeC_BD.EjecutaEscalarInt(Qry) > 0)
            return true;

        return false;
    }
    /// <summary>
    /// Obtiene una descripcion de las restricciones
    /// </summary>
    /// <param name="Restriccion"></param>
    /// <returns></returns>
    public static string ObtenDescripcion(string Restriccion)
    {
        string Descripcion = "";
        String Qry = "Select * from EC_RESTRICCIONES where restriccion = '''" + Restriccion + "''' or restriccion =''" + Restriccion + "''";
        //Console.Write(Qry);
        Descripcion = CeC_BD.EjecutaEscalarString(Qry);
        //Console.Write(Descripcion);
        //Console.ReadLine();
        //Console.Write("OK");
        return Descripcion;
    }
    /// <summary>
    /// Inserta Restricciones en Base de datos
    /// </summary>
    /// <param name="Restriccion"></param>
    public static void InsertaRestricion(string Restriccion)
    {
        if (!ExisteRestriccion(Restriccion))
            CeC_BD.EjecutaComando("INSERT INTO EC_RESTRICCIONES (RESTRICCION_ID, RESTRICCION, RESTRICCION_NOMBRE, RESTRICCION_BORRADO) VALUES(" +
                CeC_Autonumerico.GeneraAutonumerico("RESTRICCION_ID", "EC_RESTRICCIONES").ToString() + ",'" + Restriccion + "', '" + ObtenDescripcion(Restriccion) + "', 0 ");

    }

    public CeC_Restricciones()
    {
        //
        // TODO: agregar aquí la lógica del constructor
        //
    }
}