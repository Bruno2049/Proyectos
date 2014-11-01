using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Descripción breve de CeC_TablaBD
/// </summary>
public class CeC_TablaBD : CeC_Tabla
{
    CeC_Tablas m_TablaConfiguracion = null;
    public CeC_TablaBD(CeC_Tablas Tabla, CeC_Sesion Sesion)
        : base(Tabla.Tabla_Nombre, Tabla.Tabla_Llave, Sesion)
    {
        //m_Tabla = Tabla.Tabla_Nombre;
        m_TablaConfiguracion = Tabla;
        string[] Parametros = CeC.ObtenArregoSeparador(Sesion.Parametros, ",");
        m_TablaConfiguracion.Carga(Parametros[0], Sesion);
        m_Tabla = Tabla.Tabla_Nombre;
        m_CamposLlave = Tabla.Tabla_Llave;
        m_Campos = ObtenCampos(Sesion, TipoRegistro.Consulta);
        if (Parametros.Length > 1)
        {
            string[] Filas = CeC.ObtenArregoSeparador(Parametros[1],"~");
            Carga(CeC.ObtenArregoSeparador(Filas[0],"|"),Sesion);
        }
        else
            Carga("-1", Sesion);
    }
    public override string ObtenGridQry(CeC_Sesion Sesion, bool MuestraBorrados)
    {
        string NQry = ObtenQry(Sesion,"","");
        if(m_TablaConfiguracion.Tabla_C_Borrado.Length > 0 && !MuestraBorrados)
            NQry = ObtenQry(Sesion, m_TablaConfiguracion.Tabla_C_Borrado + "=0", "");
        
        string NoTabla = "Tabla";

            if (m_TablaConfiguracion.Tabla_Val_Suscripcion && Sesion != null)
                NQry = "SELECT * FROM (" + NQry + ")T WHERE " +  CeC_Autonumerico.ValidaSuscripcion(m_Tabla, m_CamposLlave, Sesion.SuscripcionParametro);

        return NQry;
    }
    public override string ObtenQry(CeC_Sesion Sesion, string Where, string OrderBy)
    {
        string Qry = "SELECT " + m_Campos + " FROM " + m_Tabla;
        if (m_TablaConfiguracion.Tabla_Grid_Qry.Length > 0)
            Qry = m_TablaConfiguracion.Tabla_Grid_Qry;
        if (Where.Length > 0)
            Qry += " WHERE " + Where;

        if (OrderBy.Length > 0)
            Qry += " ORDER BY  " + OrderBy;
        return Qry;
    }
    public override string ObtenExportarQry(CeC_Sesion Sesion, bool MuestraBorrados)
    {
        return ObtenQry (Sesion,"","");
    }
    public override bool Borrar(object Identificador, CeC_Sesion Sesion)
    {
        if (m_TablaConfiguracion.Tabla_C_Borrado == "")
            return base.Borrar(Identificador, Sesion);
        string Qry = "";
        Qry = "UPDATE " + m_Tabla + " SET " + m_TablaConfiguracion.Tabla_C_Borrado + "=1 WHERE ";
        string Where = "";
        string[] sCamposLlave = CeC.ObtenArregoSeparador(m_CamposLlave, ",");
        string[] Identificadores = (string[])Identificador;
        int Pos = 0;
        foreach (string CampoLlave in sCamposLlave)
        {
            Where = CeC.AgregaSeparador(Where, CampoLlave + " = " + Valor2Sql(Identificadores[Pos]) + "", " AND ");
            Pos++;
        }
        Qry += Where;
        if (CeC_BD.EjecutaComando(Qry) > 0)
            return true;
        return false;
    }

    public override string ObtenCampos(CeC_Sesion Sesion, TipoRegistro Tipo)
    {
        if (m_Campos != "")
            return m_Campos;
        m_Campos = CeC_TablasCampos.ObtenCamposTabla(m_Tabla);
        return m_Campos;
    }
}
