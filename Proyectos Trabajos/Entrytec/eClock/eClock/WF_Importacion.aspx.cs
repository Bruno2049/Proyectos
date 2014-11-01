using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class WF_Importacion : System.Web.UI.Page
{
    CeC_Sesion Sesion;
    private void Habilitarcontroles()
    {
        Uwg_Importacion.Visible = WIBtn_Guardar.Visible = false;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        // Permisos****************************************
        if (!Sesion.TienePermiso(eClock.CEC_RESTRICCIONES.S0Empleados0Nuevo))
        {
            Habilitarcontroles();
            return;
        }
        //**************************************************

        Sesion.TituloPagina = "Asistente de Configuración";
        /*  LDescripcion.Text = "Copie y pegue los datos (Ctrl + C , Ctrl + V) a importar desde una tabla de EXCEL o ACCESS," +
              " los encabezados muestran en que campo se guardarán los datos, si el registro existe, se actualizará, en caso contrario," +
              " se creará un nuevo registro con los datos especificados. El Nombre es OBLIGATORIO, en caso de encontrar un nombre vacío," +
              " se interrumpirá el proceso, los errores se listarán al pie de la página. Si requiere de campos diferentes," +
              " entre a la configuracián avanzada de campos del sistema y regrese a la creación de empleados Express." +
              " Usted puede establecer el estado de su empleado, si desea que este inactivo asigne 1 de lo contrario asigne 0.";*/
        if (!this.IsPostBack)
        {
            int max_rows = 50;
            string[] Campos = (CeC_Campos.ObtenListaCamposTE() + ", PERSONA_EMAIL, AGRUPACION_NOMBRE, PERSONA_BORRADO ").Split(new char[] { ',' });

            for (int j = 0; j < Campos.Length; j++)
                Campos[j] = Campos[j].Trim();
            //Se omite el primer campo que es persona_ID
            for (int j = 0; j < Campos.Length - 1; j++)
            {
                this.Uwg_Importacion.Columns.Add("Col" + j, CeC_Campos.ObtenEtiqueta(Campos[j + 1]));
                this.Uwg_Importacion.Columns[j].Key = CeC_Campos.ObtenCampoNombre(Campos[j + 1]);
            }
            for (int i = 0; i < max_rows; i++)
            {
                this.Uwg_Importacion.Rows.Add();
            }
            //Agregar Módulo Log
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Importación", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
        }
        Sesion.ControlaBoton(ref WIBtn_Guardar);
        //   Sesion.ControlaBoton(ref WebImageButton2);
    }

    protected void RBIgnorar_SelectedIndexChanged(object sender, EventArgs e)
    {
        Lbl_Advertencia.Visible = Convert.ToBoolean(Rbn_Ignorar.SelectedIndex);
    }


    protected void BGuardarCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        CPYPasteGrig();
        if (Sesion.EsWizard > 0)
            Sesion.Redirige("WF_CreacionTurnosExpress.aspx");
        else
            Sesion.Redirige("WF_Main.aspx");
    }

    private static object ObtenValor(DS_Campos.EC_CAMPOSRow Campo, int Persona_ID)
    {
        object Valor = null;
        if (Campo.CAMPO_ES_TEMPLEADOS != 0)
            Valor = CeC_BD.EjecutaEscalar("SELECT EC_PERSONAS_DATOS." + Campo.CAMPO_NOMBRE + " FROM EC_PERSONAS_DATOS, EC_PERSONAS WHERE PERSONA_LINK_ID = " + CeC_Campos.CampoTE_Llave + " AND PERSONA_ID = " + Persona_ID.ToString());
        else
            Valor = CeC_BD.EjecutaEscalar("SELECT " + Campo.CAMPO_NOMBRE + " FROM EC_PERSONAS WHERE PERSONA_ID = " + Persona_ID.ToString());
        if (Valor == System.DBNull.Value)
            return null;
        return Valor;
    }



    public bool CPYPasteGrig()
    {
        bool Res;
        int Persona_ID;
        object Valor;
        /*for (int i = 0; i < CopyPasteGrid1.Rows.Count; i++)
        {
            string numEmp = (CopyPasteGrid1.Rows[i].Cells[0].Value == null) ? "null" : CopyPasteGrid1.Rows[i].Cells[0].Value.ToString();
            string nombre = (CopyPasteGrid1.Rows[i].Cells[1].Value == null) ? "null" : CopyPasteGrid1.Rows[i].Cells[1].Value.ToString();
            if (numEmp == "null")
            {
                    return false;
            }

        }*/

        for (int x = 0; x < Uwg_Importacion.Rows.Count; x++)
        {
            if (Uwg_Importacion.Rows[x].Cells[0].Value == null && Uwg_Importacion.Rows[x].Cells[1].Value == null && Uwg_Importacion.Rows[x].Cells[2].Value == null)
                continue;
            string CampoLlave = "PERSONA_LINK_ID";
            int IDCampoLlave = Uwg_Importacion.Columns.FromKey(CampoLlave).Index;
            int Persona_Link_ID = Convert.ToInt32(Uwg_Importacion.Rows[x].Cells[IDCampoLlave].Value);
            if (Persona_Link_ID <= 0)
                Persona_Link_ID = CeC_Autonumerico.GeneraAutonumerico("EC_PERSONAS_DATOS", CampoLlave, Sesion);
            Persona_ID = CeC_Personas.ObtenPersonaID(Persona_Link_ID, Sesion.USUARIO_ID);
            if (Persona_ID <= 0)
            {
                Persona_ID = CeC_Empleados.Agrega(Persona_Link_ID, Sesion);
                if (Persona_ID <= 0)
                    continue;
            }
            bool ActualizaNombre = true;
            for (int Cont = 0; Cont < Uwg_Importacion.Columns.Count; Cont++)
            {
                string CampoEmp = Uwg_Importacion.Columns[Cont].Key;
                Valor = Uwg_Importacion.Rows[x].Cells[Cont].Value;
                if (Valor != null || Rbn_Ignorar.SelectedIndex == 1)
                    try
                    {
                        if (Valor.ToString().ToUpper() != "NULL")
                        {
                            CeC_Empleados.GuardaValor(Persona_ID, CampoEmp, Valor, Sesion.SESION_ID);
                            if (CampoEmp == "NOMBRE_COMPLETO")
                                ActualizaNombre = false;
                        }
                    }
                    catch (Exception ex) { }
            }
            if (ActualizaNombre)
                CeC_Empleados.AcutalizaPersonaNOMBRE_COMPLETO(Persona_ID, Sesion.SUSCRIPCION_ID, Sesion.SESION_ID, false);
        }
        //CeC_Personas.AcutalizaPersonaNombredeNOMBRE_COMPLETO(Sesion.SUSCRIPCION_ID);
        CeC_Agrupaciones.AutogeneraAgrupaciones(Sesion.SUSCRIPCION_ID, false);
        CeC_Asistencias.CambioDatosEmp();
        return true;
    }

    protected void Btn_Importar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        Lbl_Error.Text = "";
        Lbl_Correcto.Text = "";
        string Errores = "";
        if (Fup_Importar.HasFile)
        {
            if (Fup_Importar.PostedFile.ContentType == "application/octet-stream" || Fup_Importar.PostedFile.ContentType == "text/plain")
            {
                try
                {

                    int Importados = 0;
                    System.IO.StreamReader StreamR = new System.IO.StreamReader(Fup_Importar.PostedFile.InputStream, System.Text.Encoding.GetEncoding(437), true);
                    string Separador = "\t";
                    string Informacion = StreamR.ReadToEnd();
                    string[] Lineas = CeC.ObtenArregoSeparador(Informacion, CeC.SaltoLinea);
                    string[] Columnas = CeC.ObtenArregoSeparador(Lineas[0], Separador);
                    DataTable DT = new DataTable();
                    //return;
                    foreach (string Columna in Columnas)
                        DT.Columns.Add(Columna);
                    for (int Pos = 1; Pos < Lineas.Length; Pos++)
                    {
                        try
                        {
                            string[] Campos = CeC.ObtenArregoSeparador(Lineas[Pos], Separador, true);
                            if (Campos.Length != Columnas.Length)
                            {
                                Errores = CeC.AgregaSeparador(Errores, "La linea tiene campos diferentes a los definidos, >>" + Lineas[Pos], CeC.SaltoLinea);
                            }
                            else
                            {
                                DataRow Fila = DT.Rows.Add(Campos);
                                string CampoLlave = "PERSONA_LINK_ID";
                                int Persona_Link_ID = CeC.Convierte2Int(Fila[CampoLlave]);
                                if (Persona_Link_ID <= 0)
                                    continue;
                                int Persona_ID = CeC_Personas.ObtenPersonaID(Persona_Link_ID, Sesion.USUARIO_ID);
                                if (Persona_ID <= 0)
                                {
                                    Persona_ID = CeC_Empleados.Agrega(Persona_Link_ID, Sesion);
                                    if (Persona_ID <= 0)
                                        continue;
                                }

                                foreach (DataColumn Col in DT.Columns)
                                {
                                    string Columna = Col.ColumnName.ToUpper();
                                    if (Col.ColumnName.ToUpper() == CampoLlave || Col.ColumnName.ToUpper() == "PERSONA_ID")
                                    {
                                    }
                                    else
                                        if (Columna == "TURNO")
                                        {
                                            int TurnoID = CeC_Turnos.ObtenTurnoID(Fila[Col].ToString(), Sesion.SUSCRIPCION_ID);
                                            CeC_Empleados.GuardaValor(Persona_ID, "TURNO_ID", TurnoID, Sesion.SESION_ID);
                                        }
                                        else
                                            CeC_Empleados.GuardaValor(Persona_ID, Columna, Fila[Col], Sesion.SESION_ID);
                                }
                                Importados++;
                            }
                        }
                        catch (Exception Ex)
                        {
                            Errores = CeC.AgregaSeparador(Errores, "No se pudo guardar la fila >>" + Lineas[Pos], CeC.SaltoLinea);
                            CIsLog2.AgregaError(Ex);
                        }
                    }
                    CeC_Agrupaciones.AutogeneraAgrupaciones(Sesion.SUSCRIPCION_ID, false);
                    CeC_Asistencias.CambioDatosEmp();
                    Lbl_Correcto.Text = "Se han importado " + Importados + " registros";
                    if (Errores.Length > 0)
                        Lbl_Error.Text = Errores.Replace(CeC.SaltoLinea, "<BR>");
                    return;
                }
                catch (Exception Ex)
                {
                    CIsLog2.AgregaError(Ex);
                    Lbl_Error.Text = Ex.Message;
                }
            }
            else
                Lbl_Error.Text = "Formato de archivo desconocido";
        }
        else
            Lbl_Error.Text = "No se ha seleccionado el archivo";
    }
    protected void WIBtn_GuardarCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        if (CPYPasteGrig())
            Sesion.Redirige("WF_Importacion.aspx");
        else
            Lbl_Error.Text = "Se requieren el numero de empleado y el nombre completo de los empleados. Verifique la información";
    }
}
