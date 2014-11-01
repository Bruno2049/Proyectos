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

public partial class WF_ConfigLogos : System.Web.UI.Page
{
    CeC_Sesion Sesion;  
    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        if(!Sesion.TienePermiso(eClock.CEC_RESTRICCIONES.S0Configuracion))
        {
            CIT_Perfiles.CrearVentana(this, Sesion.MensajeVentanaJScript(), Sesion.TituloPagina, "Aceptar", "WF_Main.aspx", "", "");
            UltraWebToolbar2.Visible = Webpanel1.Visible = WebPanel2.Visible = Webpanel3.Visible = false;
        }
        Sesion.TituloPagina = ("Configuracion de logotipos");
        Sesion.DescripcionPagina = ("Configure los logotipos que apareceran en el Login, Encabezado y los Reportes");

        if (!IsPostBack)
        {
            //Agregar Módulo Log
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Configuración de Logos", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
        }
    }

    protected void btnsubir1_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        byte[] Datos = new byte[File1.PostedFile.ContentLength];
        if (Datos.Length > 0)
        {
            File1.PostedFile.InputStream.Read(Datos, 0, File1.PostedFile.ContentLength);
            CeC_Config.imglogin = Datos;
        }
    }

    protected void guardaimagen(string nomimagen, string StrFileName, string StrFileType, int IntFileSize)
    {
        if (IntFileSize <= 0)
            Response.Write(" <font color='Red' size='2'>Uploading of file " + StrFileName + " failed </font>");
        else
        {
            byte[] Datos = new byte[IntFileSize];
            switch(nomimagen)
            {
                case "imglogin":
                    File1.PostedFile.InputStream.Read(Datos, 0, File1.PostedFile.ContentLength);
                    break;
                case "imgencabezado":
                    File2.PostedFile.InputStream.Read(Datos, 0, File2.PostedFile.ContentLength);
                    break;
                case "imgreporte":
                    File3.PostedFile.InputStream.Read(Datos, 0, File3.PostedFile.ContentLength);
                    break;
            }
            int ID;
            ID = CeC_BD.EjecutaEscalarInt("SELECT CONFIG_USUARIO_ID FROM EC_CONFIG_USUARIO WHERE USUARIO_ID = 0 AND CONFIG_USUARIO_VARIABLE = '" + nomimagen + "'");

            if (Datos != null && CeC_BD.AsignaImagen(Datos, ID, nomimagen))
            {
                //Foto Guardada correctamente
                //LCorrecto.Text = "Foto Guardada correctamente";
                MemoryStream img = new MemoryStream(Datos);
                Bitmap imagen = new Bitmap(img);
                switch(nomimagen)
                {         
                    case "imglogin":
                        Image1.ImageUrl = ("WF_Logos_" + nomimagen + ".aspx");
                        break;
                    case "imgencabezado":
                        Image2.ImageUrl = ("WF_Logos_" + nomimagen + ".aspx");
                        break;
                    case "imgreporte":
                        Image3.ImageUrl = ("WF_Logos_" + nomimagen + ".aspx");
                        break;
                 }
            }
            else
            {
                //No se pudo guardar la foto
                //LError.Text = "No se pudo guardar la foto";
            }
        }
    }

    protected void btnsubir2_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        byte[] Datos = new byte[File2.PostedFile.ContentLength];
        if (Datos.Length > 0)
        {
            File2.PostedFile.InputStream.Read(Datos, 0, File2.PostedFile.ContentLength);
            CeC_Config.imgencabezado = Datos;
        }
    }


    protected void btnsubir3_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        byte[] Datos = new byte[File3.PostedFile.ContentLength];
        if (Datos.Length > 0)
        {
            File3.PostedFile.InputStream.Read(Datos, 0, File3.PostedFile.ContentLength);
            CeC_Config.imgreporte = Datos;
        }
    }
}