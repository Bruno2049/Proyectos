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
using System.IO;
using System.Drawing;
public partial class WF_EmpleadosEdant : System.Web.UI.Page
{

    protected int Persona_ID;
    protected CeC_Sesion Sesion;
    CeC_SesionBD SesionBD;
    protected bool AgregaCampo(string NombreCampo, int Persona_ID)
    {
        try
        {
            object Obj = CeC_Campos.CreaCampo(NombreCampo, Persona_ID);
            if (Obj != null)
            {
                if (Obj.GetType().FullName == "Infragistics.WebUI.WebCombo.WebCombo")
                {
                    Infragistics.WebUI.WebCombo.WebCombo Combo = (Infragistics.WebUI.WebCombo.WebCombo)Obj;
                    Combo.InitializeLayout += new Infragistics.WebUI.WebCombo.InitializeLayoutEventHandler(Combo_InitializeLayout);

                }
                ((System.Web.UI.WebControls.WebControl)Obj).Width = Unit.Pixel(150);
                TableRow Fila = new TableRow();
                TableCell Cell1 = new TableCell();
                TableCell Cell2 = new TableCell();
                System.Web.UI.WebControls.Label Lbl = new Label();
                Lbl.Text = CeC_Campos.ObtenEtiqueta(NombreCampo);
                //Cell1.Text = CeC_Campos.ObtenEtiqueta(NombreCampo);
                //Cell1.Width = Unit.Pixel(150);
                Lbl.Width = Unit.Pixel(150);
                Cell1.Controls.Add(Lbl);
                Fila.Cells.Add(Cell1);

                Cell1.HorizontalAlign = HorizontalAlign.Left;
                Cell2.HorizontalAlign = HorizontalAlign.Left;
                Cell2.Controls.Add((System.Web.UI.Control)Obj);
                Fila.Cells.Add(Cell2);
                Tabla.Rows.Add(Fila);


                return true;
            }
        }
        catch (Exception ex)
        {
        }
        return false;
    }

    void Combo_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        CeC_Grid.AplicaFormato((Infragistics.WebUI.WebCombo.WebCombo)sender);
    }

    protected bool GuardaCampo(object Campo)
    {
        if (Campo == null)
            return false;
        string SError = CeC_Campos.GuardaCampo(Campo, Persona_ID, Sesion.SESION_ID);
        if (SError.Length > 0)
        {
            LError.Text += SError + "\n";
            return false;
        }
        return true;
    }

    protected object ObtenCampo(string Nombre)
    {
        foreach (TableRow TR in Tabla.Rows)
        {
            try
            {
                if (TR.Cells[1].Controls[0].ID == Nombre)
                    return TR.Cells[1].Controls[0];
            }
            catch
            {
            }
        }
        return this.FindControl(Nombre);
        foreach (System.Web.UI.Control Contr in this.Controls)
        {
            if (Contr.ID == Nombre)
                return Contr;
        }
        return null;
    }

    protected void GuardaDatos(bool edterminales)
    {
        int nuevo = 0;
        if (Persona_ID <= 0)
        {
            Persona_ID = CeC_Autonumerico.GeneraAutonumerico("EC_PERSONAS", "PERSONA_ID");
            if (Persona_ID <= 0)
            {
                LError.Text = "Ha Llegado al Limite de su Version";
                return; 
            }
            nuevo = 1;
        }
        string[] Campos = CeC_Campos.ObtenListaCamposTE().Split(new Char[] { ',' });
        int Errores = 0;
        int Correctos = 0;
        foreach (string Campo in Campos)
        {
            string NombreCampo = Campo.Trim();
            if (GuardaCampo(ObtenCampo(NombreCampo)))
                Correctos++;
            else
                Errores++;
        }
        if (GuardaCampo(ObtenCampo("PERSONA_EMAIL"))) Correctos++; else Errores++;
        if (GuardaCampo(ObtenCampo("TURNO_ID"))) Correctos++; else Errores++;
        if (GuardaCampo(ObtenCampo("PERSONA_BORRADO"))) Correctos++; else Errores++;

        if (Errores > 0)
            LCorrecto.Visible = false;
        else
        {
            LCorrecto.Visible = true;
            LCorrecto.Text = "Se guardo correctamente el registro";
        }

        //Actualiza el nombre de este empleado
        //CeC_BD.ActualizaNombreEmpleado(Persona_ID);
        //Indica que cambiaron los datos de un empleado
        CeC_Asistencias.CambioDatosEmp();
     
        if (nuevo == 1)
        {
            CeC_BD.AsignaTerminalAuto(Persona_ID);
        }
        CeC_Personas.AsignaFoto(Persona_ID, SesionBD.FotoNueva);
        if (Errores <= 0)
        {
            if (edterminales)
            {
                Sesion.WF_Empleados_PERSONA_ID = Persona_ID;
                Sesion.WF_Empleados_PERSONA_LINK_ID = CeC_BD.EjecutaEscalarInt("SELECT PERSONA_LINK_ID FROM EC_PERSONAS WHERE PERSONA_ID = " + Persona_ID);
                CeC_Sesion.Redirige(this, "WF_PersonasTerminales.aspx");
            }
            else
                CeC_Sesion.Redirige(this, "WF_EmpleadosN.aspx");
        }
        else
        {
            Sesion.WF_Empleados_PERSONA_ID = Persona_ID;
            Sesion.WF_Empleados_PERSONA_LINK_ID = CeC_BD.EjecutaEscalarInt("SELECT PERSONA_LINK_ID FROM EC_PERSONAS WHERE PERSONA_ID = "+Persona_ID);
        }
    }
    protected void CargaDatos()
    {
        CeC_Campos.Inicializa();
        string[] Campos = CeC_Campos.ObtenListaCamposTE().Split(new Char[] { ',' });

        foreach (string Campo in Campos)
        {
            string NombreCampo = Campo.Trim();
            AgregaCampo(NombreCampo, Persona_ID);
        }
        AgregaCampo("PERSONA_EMAIL", Persona_ID);
        AgregaCampo("TURNO_ID", Persona_ID);
        AgregaCampo("PERSONA_BORRADO", Persona_ID);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        SesionBD = new CeC_SesionBD(Sesion.SESION_ID);
        Tabla.Width = Unit.Pixel(300);
        if(!IsPostBack && Sesion.Parametros == "Nuevo")
            Sesion.WF_Empleados_PERSONA_ID = -1;


        // Permisos****************************************
        if (Sesion.WF_Empleados_PERSONA_ID > 0)
        {
            if (!Sesion.TienePermisoOHijos(eClock.CEC_RESTRICCIONES.S0Empleados0Editar, true))
            {
                WebPanel2.Visible = false;
                BDeshacerCambios.Visible = false;
                BGuardarCambios.Visible = false;
                return;
            }
        }
        else
        {
            if (!Sesion.TienePermisoOHijos(eClock.CEC_RESTRICCIONES.S0Empleados0Nuevo, true))
            {
                WebPanel2.Visible = false;
                BDeshacerCambios.Visible = false;
                BGuardarCambios.Visible = false;
                return;
            }
        }
        //**************************************************

        if (!CeC_Config.FotografiaActiva)
        {
            Image1.Visible = false;
            WebImageButton1.Visible = false;
            WebImageButton3.Visible = false;
            File1.Visible = false;
        }
        if (Sesion.WF_Empleados_PERSONA_ID > 0)
        {
            Persona_ID = Sesion.WF_Empleados_PERSONA_ID;
        }
        else
        {
            Persona_ID = -1;
        }
        if (!IsPostBack)
        {
            if (Persona_ID > 0)
                SesionBD.FotoNueva = CeC_Personas.ObtenFoto(Persona_ID);
            else
                SesionBD.FotoNueva = null;
            //Agregar Módulo Log
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Edición de Empleados", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
        }
        {
            CargaDatos();
        }
        Sesion.TituloPagina = "Edición de un Empleado";
        Sesion.DescripcionPagina ="Llene los campos con los datos correctos";
    }

    protected void BDeshacerCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        CeC_Sesion.Redirige(this, "WF_EmpleadosN.aspx");
    }

    protected void BGuardarCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        LError.Text = "";
        GuardaDatos(false);
    }

    protected void WebImageButton1_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        string StrFileName = File1.PostedFile.FileName.Substring(File1.PostedFile.FileName.LastIndexOf("\\") + 1);
        string StrFileType = File1.PostedFile.ContentType;
        int IntFileSize = File1.PostedFile.ContentLength;
        int PersonaId = Persona_ID;
        if (IntFileSize <= 0)
            Response.Write(" <font color='Red' size='2'>Uploading of file " + StrFileName + " failed </font>");
        else
        {
            byte[] Datos = new byte[File1.PostedFile.ContentLength];
            File1.PostedFile.InputStream.Read(Datos, 0, File1.PostedFile.ContentLength);
            SesionBD.FotoNueva = Datos;
            Image1.ImageUrl = ("WF_Personas_FotoN.aspx");
            /*
            if (Datos != null && CeC_Personas.AsignaFoto(PersonaId, Datos))
            {
                //Foto Guardada correctamente
                //LCorrecto.Text = "Foto Guardada correctamente";
                MemoryStream img = new MemoryStream(Datos);
                Bitmap imagen = new Bitmap(img);
                Image1.ImageUrl=("WF_Personas_ImaS.aspx");
            }
            else
            {
                //No se pudo guardar la foto
                //LError.Text = "No se pudo guardar la foto";
            }
      
            }
            if (Persona_ID < 1)
            {
                byte[] foto = new byte[File1.PostedFile.ContentLength];
                //SesionBD.FotoNueva = new byte[File1.PostedFile.ContentLength];
                File1.PostedFile.InputStream.Read(foto, 0, File1.PostedFile.ContentLength);
                if (foto != null)
                {
                    //Foto Guardada correctamente
                    //LCorrecto.Text = "Foto Guardada correctamente";
                    MemoryStream img = new MemoryStream(SesionBD.FotoNueva);
                    Bitmap imagen = new Bitmap(img);
                    Image1.ImageUrl = ("WF_Personas_ImaS.aspx");
                }
            }*/
        }
    }

    protected void WebImageButton3_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        SesionBD.FotoNueva = null;
        Image1.ImageUrl = ("WF_Personas_FotoN.aspx");
    }

    protected void btnEditarTerminales_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        GuardaDatos(true);
        Sesion.Redirige("WF_PersonasTerminales.aspx");
    }

    protected void BtnEditarHuellas_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        Sesion.Redirige("eClockDesc.application?SESION_ID=" + Sesion.SESION_ID + "&HUELLAS=1&PERSONA_ID="+Persona_ID);
    }
}
