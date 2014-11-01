using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using System.IO;

public partial class WF_TurnoImportaAsignacion : System.Web.UI.Page
{
    CeC_Sesion Sesion;
    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        Sesion.ControlaBoton(ref WIBtn_Importar);
    }
    protected void WIBtn_Importar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        Lbl_Error.Text = "";
        Lbl_Correcto.Text = "";
        if (Fup_Importar.HasFile )
        {
            if (Fup_Importar.PostedFile.ContentType == "application/octet-stream" || Fup_Importar.PostedFile.ContentType == "text/plain" || Fup_Importar.PostedFile.ContentType == "application/vnd.ms-excel")
            {
                try
                {

                    System.IO.StreamReader StreamR = new System.IO.StreamReader(Fup_Importar.PostedFile.InputStream);
                    
                    int Registros = 0;
                    CeC_Importacion Importacion = new CeC_Importacion(CeC_Turnos.ObtenDataSetImportaAsignacionTurnos());
                    String Ext = Path.GetExtension(Fup_Importar.FileName.ToString());
                    if (Ext == ".csv")
                    {
                        Registros = Importacion.ImportaCSV(CeC_Importacion.String2Strings(StreamR.ReadToEnd()));
                    }
                    if (Ext == ".txt")
                    {
                        Registros = Importacion.ImportaCSVTabulador(CeC_Importacion.String2Strings(StreamR.ReadToEnd()));
                    }
                    CeC_Turnos Turnos = new CeC_Turnos(Sesion);
                    int Importados = Turnos.ImportaAsignacionTurnos(Importacion.m_DataSetDestino, Sesion);
                    Lbl_Error.Text = Importacion.m_Errores;
                    Lbl_Correcto.Text = Registros.ToString() + " Registros importados correctamente";
                }
                catch (Exception Ex) { CIsLog2.AgregaError(Ex); }
            }
            else
                Lbl_Error.Text = "Formato de archivo desconocido";
        }
        else
            Lbl_Error.Text = "No se ha seleccionado el archivo";

    }
}
