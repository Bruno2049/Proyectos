using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class WF_IncidenciasCargaMasiva : System.Web.UI.Page
{
    CeC_Sesion Sesion;
    String Mensaje;
    protected void Page_Load(object sender, EventArgs e)
    {
        // Se crea la Sesion
        Sesion = CeC_Sesion.Nuevo(this);
        Sesion.TituloPagina = "Carga Masiva de Incidencias Mensual";
        Sesion.DescripcionPagina = "Teclee en la columna correspondiente el Número de Empleado, y las incidencias por día";
        //Lbl_Mesaje_Correcto.Text = Sesion.Parametros;
        //Lbl_Mensaje_Error.Text = Sesion.Parametros;
        Lbl_Instrucciones.Text = "Introduzca o copie los datos en la columna que corresponda.";
        if (!this.IsPostBack)
        {
            int max_rows = 30;
            // Se llenan las cabeceras de las columnas
            // No. Trabajador, Nombre, Dia 1...30
            this.UWG_ASIGNACION_INCIDENCIAS.Columns.Add("Col" + 0, CeC_Campos.ObtenEtiqueta(CeC_Campos.CampoTE_Llave));
            this.UWG_ASIGNACION_INCIDENCIAS.Columns[0].Key = CeC_Campos.ObtenEtiqueta(CeC_Campos.CampoTE_Llave);
            this.UWG_ASIGNACION_INCIDENCIAS.Columns.Add("Col" + 1, "Nombre");
            this.UWG_ASIGNACION_INCIDENCIAS.Columns[1].Key = "Nombre";
            // 31 Dias para cubrir el mes
            for (int dia = 1; dia <= 31; dia++)
            {
                this.UWG_ASIGNACION_INCIDENCIAS.Columns.Add("Col" + dia+1, dia.ToString());
                this.UWG_ASIGNACION_INCIDENCIAS.Columns[dia+1].Key = dia.ToString();
            }
            
            // Se crea un Grid de tamaño max_rows
            for (int i = 0; i < max_rows; i++)
            {
                this.UWG_ASIGNACION_INCIDENCIAS.Rows.Add();
            }
            //Agregar Módulo Log
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Carga de Incidencias Mensual", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
        }
    }
    protected void Guardar()
    {
        Lbl_Mensaje_Correcto.Text = "";
        Lbl_Mensaje_Error.Text = "";
        string registrosConErrores = "";
        int registrosCorrectos = 0;
        bool ret = false;
        int Persona_Link_ID = 0;
        if (CeC.Convierte2Int(Tbx_Ano.Text) < 2006 || CeC.Convierte2Int(Tbx_Ano.Text) > 2013 || CeC.Convierte2Int(Tbx_Mes.Text) <= 0 || CeC.Convierte2Int(Tbx_Mes.Text) >= 13)
        {
            Lbl_Mensaje_Error.Text = "Las fechas parecen no ser correctas, introduzca fechas válidas.";
            return;
        }
        for (int fila = 0; fila < UWG_ASIGNACION_INCIDENCIAS.Rows.Count; fila++)
        {
            try
            {
                String Incidencias = "";
                // Checamos que se introduzcan los mínimos de datos necesarios
                if (this.UWG_ASIGNACION_INCIDENCIAS.Rows[fila].Cells[0].Value == null)
                    //&&
                    //this.UWG_ASIGNACION_INCIDENCIAS.Rows[fila].Cells[1].Value == null)
                    continue;
                // Se leen los datos necesarios y se guardan en un arreglo de incidencias
                for (int dia = 0; dia <= 32; dia++)
                {
                    
                    if (dia <= 30 && this.UWG_ASIGNACION_INCIDENCIAS.Rows[fila].Cells[dia + 1].Value == null)
                    {
                        this.UWG_ASIGNACION_INCIDENCIAS.Rows[fila].Cells[dia + 1].Value = "";
                    }
                    if (dia <= 30)
                    {
                        Incidencias += CeC.Convierte2String(this.UWG_ASIGNACION_INCIDENCIAS.Rows[fila].Cells[dia + 2].Value) + ",";
                        continue;
                    }
                }
                Persona_Link_ID = CeC.Convierte2Int(UWG_ASIGNACION_INCIDENCIAS.Rows[fila].Cells[0].Value);

                if (GuardaIncidencias(Persona_Link_ID, Incidencias, CeC.Convierte2Int(Tbx_Mes.Text), CeC.Convierte2Int(Tbx_Ano.Text)))
                {
                    registrosCorrectos = fila;
                    ret = true;
                }
                else
                {
                    registrosConErrores += (fila + 1).ToString() + ", ";
                    ret = false;
                }
            }
            catch (Exception ex) 
            {
                ret = false;
                registrosConErrores += (fila + 1).ToString() + ", ";
                CIsLog2.AgregaError(ex); 
            }
        }
        if (!ret || registrosConErrores != "")
        {
            Lbl_Mensaje_Correcto.Text = "";
            Lbl_Mensaje_Error.Text = "Existieron errores en la importacion del registro(s) No. " + registrosConErrores +
                                        "Revise que la información introducida sea correcta.";
        }
        else
        {
            Lbl_Mensaje_Error.Text = "";
            Lbl_Mensaje_Correcto.Text = "Se importaron con exito " + (registrosCorrectos + 1) + " registros. ";
        }
    }

    protected bool GuardaIncidencias(int Persona_Link_ID, string Incidencias, int Mes, int Ano)
    {
        try
        {
            int PersonaID = CeC_Empleados.ObtenPersonaID(Persona_Link_ID);
            int TipoIncidenciaID;
            string [] IncidenciasDia = CeC.ObtenArregoSeparador(Incidencias, ",", true);
            DateTime Fecha;
            for (int dia = 0; dia <= 30; dia++)
            {
                Fecha = new DateTime(Ano, Mes, dia + 1);
                TipoIncidenciaID = Cec_Incidencias.ObtenTipoIncidenciaID(Sesion.SUSCRIPCION_ID, IncidenciasDia[dia]);

                if (TipoIncidenciaID > 0)
                {
                    int IncidenciaID = Cec_Incidencias.CreaIncidencia(TipoIncidenciaID, "", Sesion.SESION_ID);
                    if (IncidenciaID > 0)
                    {
                        if (PersonaID <= 0)
                        {
                            CIsLog2.AgregaError("Numero de empleado " + Persona_Link_ID + " no existe.");
                            continue;
                            //return false;
                        }
                        if (Cec_Incidencias.AsignaIncidencia(Fecha, Fecha, PersonaID, IncidenciaID, Sesion.SESION_ID) > 0)
                        {
                            continue;
                            //if (AsignaIncidencias(IncidenciaID, TipoIncidenciaID, PersonaDiarioID, Persona_Link_ID, MotivoIncidencia, Folio, HoraInicio, HoraFin, FechaInicial, FechaFinal))
                                //return true;
                        }
                    }

                    //if (TipoIncidenciaID > 0)
                    //{
                    //    Cec_Incidencias.AsignaIncidencia(Fecha, Fecha, PersonaID, TipoIncidenciaID, Sesion.SESION_ID);
                    //}
                }
            }
            //int PersonaDiarioID = CeC_Asistencias.ObtenPersonaDiarioID(PersonaID, FechaInicial);
            //string NombreIncidencia = Cec_Incidencias.ObtenTipoIncidenciaNombre(TipoIncidenciaID);
            
            //Cec_Incidencias.AsignaIncidencia(FechaHora, FechaHora, Persona_ID, Incidencia, Sesion.SESION_ID)
            return true;
        }
        catch (Exception Ex)
        {
            CIsLog2.AgregaError(Ex);
            return false;
        }
    }
    
    protected void WIBtn_GuardarContinuar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        try
        {
            Guardar();
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }

    }

    //protected void WIBtn_Salir_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    //{
    //    try
    //    {
    //        Sesion.Redirige("eClock.aspx");
    //    }
    //    catch (Exception ex)
    //    {
    //        CIsLog2.AgregaError(ex);
    //    }
    //}
}