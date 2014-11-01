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

public partial class WF_CargaComidasExpress : System.Web.UI.Page
{
    CeC_Sesion Sesion;

    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        Sesion.TituloPagina = "Carga Manual de Comidas";
        Sesion.DescripcionPagina = "Seleccione la Fecha y teclee en cualquier casilla el numero de empleado al que se le cobrara " +
            "una comida, si se desean cobrar dos comidas, ponga dos veces el numero del empleado. Puede copiar los datos desde Excel "+
            "o Access.";

        // Permisos****************************************
        if (!Sesion.TienePermisoOHijos(eClock.CEC_RESTRICCIONES.S0Comidas0CargaExpress, true))
        {
            WIBtn_Guardar.Visible = false;
            WIBtn_Continuar.Visible = false;
            FIL_FECHA.Visible = false;
            Uwg_CopyPaste.Visible = false;
            return;
        }
        //**************************************************

        if (!this.IsPostBack)
        {
            FIL_FECHA.SelectedDate = DateTime.Today;
            int max_rows = 30;
                this.Uwg_CopyPaste.Columns.Add("Col" + 0, CeC_Campos.ObtenEtiqueta(CeC_Campos.CampoTE_Llave));
                this.Uwg_CopyPaste.Columns[0].Key = CeC_Campos.ObtenEtiqueta(CeC_Campos.CampoTE_Llave);
                this.Uwg_CopyPaste.Columns.Add("Col" + 1, CeC_Campos.ObtenEtiqueta(CeC_Campos.CampoTE_Llave));
                this.Uwg_CopyPaste.Columns[1].Key = CeC_Campos.ObtenEtiqueta(CeC_Campos.CampoTE_Llave);
                this.Uwg_CopyPaste.Columns.Add("Col" + 2, CeC_Campos.ObtenEtiqueta(CeC_Campos.CampoTE_Llave));
                this.Uwg_CopyPaste.Columns[2].Key = CeC_Campos.ObtenEtiqueta(CeC_Campos.CampoTE_Llave);

            //    this.CopyPasteGrid1.Columns.Add("Col" + 1, "NO. COMIDAS");
            //    this.CopyPasteGrid1.Columns[1].Key = "NO. COMIDAS";
            
            for (int i = 0; i < 30; i++)
            {
                this.Uwg_CopyPaste.Rows.Add();
            }
            //Agregar Módulo Log
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Carga de Comida Express", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
        }
    }

    protected void Guardar()
    {
        try
        {
            for (int x = 0; x < Uwg_CopyPaste.Rows.Count; x++)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (Uwg_CopyPaste.Rows[x].Cells[i].Value == null)
                        continue;
                    eClock.WS_Monedero WSMonedero = new eClock.WS_Monedero();
                    int PersonaID = CeC_Personas.ObtenPersonaID(Convert.ToInt32(Uwg_CopyPaste.Rows[x].Cells[i].ToString()));
                    bool Primera = Convert.ToBoolean(WSMonedero.TienePrimeraComida(PersonaID));
                    if (Primera == true)
                        WSMonedero.AgregarComidaFecha(PersonaID, 2, 0, FIL_FECHA.SelectedDate);
                    else
                        WSMonedero.AgregarComidaFecha(PersonaID, 1, 0, FIL_FECHA.SelectedDate);
                }
           } 
        }
        catch (Exception ex) { CIsLog2.AgregaError(ex); }
    }
    protected void WIBtn_Continuar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        Guardar();
        //Sesion.Redirige("WF_Main.aspx");
        Sesion.Redirige("WF_Comida.aspx");
    }
    protected void WIBtn_Guardar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        Guardar();
        Sesion.Redirige("WF_CargacomidasExpress.aspx");
    }
}