using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WF_ImportarInc : System.Web.UI.Page
{
    CeC_Sesion Sesion;
    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);

        // Permisos****************************************
        if (!Sesion.TienePermisoOHijos(eClock.CEC_RESTRICCIONES.S0ImportarAccesos, true))
        {
            FileUpload1.Visible = false;
            LError.Visible = false;
            LCorrecto.Visible = false;
            btnImportar.Visible = false;
            return;
        }
        //**************************************************

        if (!IsPostBack)
        {

        }
        Sesion.ControlaBoton(ref btnImportar);
    }
    int EncuentraPosCampo(string[] Campos, string CampoBuscado)
    {
        int Pos = 0;
        foreach (string Campo in Campos)
        {
            if (Campo == CampoBuscado)
                return Pos;
            Pos++;
        }
        return -1;
    }
    protected void btnImportar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        LError.Text = "";
        LCorrecto.Text = "";
        int Errores = 0;
        if (FileUpload1.HasFile)
        {
            if (System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName) == ".csv" || System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName) == ".txt")
            {
                try
                {
                    ///Campos Holiday
                    int EmployeeId = 0;//= EncuentraPosCampo(Campos, "EmployeeId");
                    int LeaveType = 4;//= EncuentraPosCampo(Campos, "LeaveType");
                    int CreatedDate = 5;//= EncuentraPosCampo(Campos, "CreatedDate");
                    int LeaveDateFrom = 6;//= EncuentraPosCampo(Campos, "LeaveDateFrom");
                    int LeaveDateTo = 8;//= EncuentraPosCampo(Campos, "LeaveDateTo");
                    int LeaveDays = 10;//= EncuentraPosCampo(Campos, "LeaveDays");



                    System.IO.StreamReader StreamR = new System.IO.StreamReader(FileUpload1.PostedFile.InputStream);
                    /*   StreamR.
                       System.IO.StringReader SR = new System.IO.StringReader(StreamR.ToString());*/
                    string Linea = "";
                    int Registros = 0;
                    int NLinea = 0;
                    do
                    {
                        NLinea++;
                        Linea = StreamR.ReadLine();
                        if (Linea != null && Linea.Length > 22)
                        {
                            try
                            {
                                string[] Campos = Linea.Split(new char[] { '|' });
                                if (Campos.Length == 5)
                                {
                                    int Empleado = Convert.ToInt32(Linea.Substring(0, 9).Trim());
                                    string Fecha = Linea.Substring(10, 10);

                                    DateTime FechaHora = new DateTime(Convert.ToInt16(Fecha.Substring(0, 4)), Convert.ToInt16(Fecha.Substring(5, 2)),
                                                                      Convert.ToInt16(Fecha.Substring(8, 2)));
                                    int TipoIncidenciaID = Cec_Incidencias.TipoIncidenciaAgrega(Sesion, Campos[3], Campos[2]);
                                    if (TipoIncidenciaID > 0)
                                    {

                                        int Incidencia = Cec_Incidencias.CreaIncidencia(TipoIncidenciaID, Campos[4], Sesion.SESION_ID);
                                        if (Incidencia > 0)
                                        {
                                            int Persona_ID = CeC_Empleados.ObtenPersonaID(Empleado, Sesion.USUARIO_ID);
                                            if (Persona_ID <= 0)
                                            {
                                                LError.Text += "<br> No Empleado no existe (" + Empleado + "), Linea " + NLinea;
                                                Errores++;
                                                continue;
                                            }
                                            if (Cec_Incidencias.AsignaIncidencia(FechaHora, FechaHora, Persona_ID, Incidencia, Sesion.SESION_ID) > 0)
                                                Registros++;
                                            else
                                            {
                                                LError.Text += "<br> No se pudo crear la incidencia Linea " + NLinea;
                                                Errores++;
                                            }
                                        }
                                        else
                                        {
                                            LError.Text += "<br> No se pudo crear la incidencia Linea " + NLinea;
                                            Errores++;
                                        }
                                    }
                                    else
                                    {
                                        LError.Text += "<br> No se pudo crear el tipo de incidencia Linea " + NLinea;
                                        Errores++;
                                    }
                                }
                                else
                                {
                                    Campos = Linea.Split(new char[] { ',' });
                                    if (Campos.Length >= 23)
                                    {
                                        if (NLinea == 1)
                                        {
                                            EmployeeId = EncuentraPosCampo(Campos, "EmployeeId");
                                            LeaveType = EncuentraPosCampo(Campos, "LeaveType");
                                            CreatedDate = EncuentraPosCampo(Campos, "CreatedDate");
                                            LeaveDateFrom = EncuentraPosCampo(Campos, "LeaveDateFrom");
                                            LeaveDateTo = EncuentraPosCampo(Campos, "LeaveDateTo");
                                            LeaveDays = EncuentraPosCampo(Campos, "LeaveDays");
                                        }
                                        else
                                        {

                                            if (Campos[EmployeeId] == "")
                                                continue;
                                            int PersonaID = CeC_Empleados.ClaveEmpl2PersonaID(Campos[EmployeeId], Sesion.USUARIO_ID);
                                            if (PersonaID <= 0)
                                            {
                                                LError.Text += "<br> No Empleado no existe (" + Campos[EmployeeId] + "), Linea " + NLinea;
                                                Errores++;
                                                continue;
                                            }
                                            string Comentario = "Creado el día " + Campos[CreatedDate] + " Desde " + Campos[LeaveDateFrom] +
                                                " Hasta " + Campos[LeaveDateTo] + " Tipo " + Campos[LeaveType] + " Tiempo " + Campos[LeaveDays];
                                            DateTime FechaInicio = Convert.ToDateTime(Campos[LeaveDateFrom]);
                                            DateTime FechaFin = Convert.ToDateTime(Campos[LeaveDateTo]);
                                            int TipoIncidenciaID = Cec_Incidencias.TipoIncidenciaAgrega(Sesion, Campos[LeaveType], "IM");
                                            if (TipoIncidenciaID > 0)
                                            {

                                                int Incidencia = Cec_Incidencias.CreaIncidencia(TipoIncidenciaID, Comentario, Sesion.SESION_ID);
                                                if (Incidencia > 0)
                                                {

                                                    if (Cec_Incidencias.AsignaIncidencia(FechaInicio, FechaFin, PersonaID, Incidencia, Sesion.SESION_ID) > 0)
                                                        Registros++;
                                                    else
                                                    {
                                                        LError.Text += "<br> No se pudo crear la incidencia Linea " + NLinea;
                                                        Errores++;
                                                    }

                                                }
                                                else
                                                {
                                                    LError.Text += "<br> No se pudo crear la incidencia Linea " + NLinea;
                                                    Errores++;
                                                }
                                            }
                                            else
                                            {
                                                LError.Text += "<br> No se pudo crear el tipo de incidencia Linea " + NLinea;
                                                Errores++;
                                            }

                                        }

                                    }
                                }
                            }
                            catch
                            {
                                LError.Text += "<br>Error en linea " + NLinea;
                                Errores++;
                            }
                        }
                    } while (Linea != null);
                    LCorrecto.Text = Registros.ToString() + " Registros importados correctamente";
                }
                catch (Exception Ex) { CIsLog2.AgregaError(Ex); }
            }
            else
                LError.Text = "Formato de archivo desconocido";
        }
        else
            LError.Text = "No se ha seleccionado el archivo";
        if (Errores > 0)
        {
            LError.Text += "<br> Existieron " + Errores + " error(es) en la importacion";
        }
    }
}
