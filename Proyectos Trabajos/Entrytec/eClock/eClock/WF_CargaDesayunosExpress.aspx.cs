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

public partial class WF_CargaDesayunosExpress : System.Web.UI.Page
{
    CeC_Sesion Sesion;

    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        Sesion.TituloPagina = "Carga Manual de Monedero";
        Sesion.DescripcionPagina = "Seleccione la Fecha y Teclee en la columna correspondiente el numero de empleado al que se le cobrara " +
            "el consumo y en la siguiente casilla el monto a cobrar. Puede copiar los datos desde Excel " +
            "o Access.";

        // Permisos****************************************
        if (!Sesion.TienePermisoOHijos(eClock.CEC_RESTRICCIONES.S0Monedero0CargaDesayunos, true))
        {
            BGuardarCambios.Visible = false;
            FIL_FECHA.Visible = false;
            Webimagebutton1.Visible = false;
            CopyPasteGrid1.Visible = false;
            return;
        }
        //**************************************************

        if (!this.IsPostBack)
        {
            FIL_FECHA.SelectedDate = DateTime.Today;
            int max_rows = 30;
            this.CopyPasteGrid1.Columns.Add("Col" + 0, CeC_Campos.ObtenEtiqueta(CeC_Campos.CampoTE_Llave));
            this.CopyPasteGrid1.Columns[0].Key = CeC_Campos.ObtenEtiqueta(CeC_Campos.CampoTE_Llave);
            this.CopyPasteGrid1.Columns.Add("Col" + 1, "MONTO");
            this.CopyPasteGrid1.Columns[1].Key = "MONTO";
            for (int i = 0; i < 30; i++)
            {
                this.CopyPasteGrid1.Rows.Add();
            }
            //Agregar Módulo Log
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Carga de Desayunos Express", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
        }
    }

    protected void BGuardarCambiosM_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        Guardar();
        Sesion.Redirige("WF_CargaDesayunosExpress.aspx");
    }

    protected void BGuardarCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        Guardar();
        Sesion.Redirige("WF_Main.aspx");
    }

    protected void Guardar()
    {
        for (int x = 0; x < CopyPasteGrid1.Rows.Count; x++)
        {
            try
            {
                if (CopyPasteGrid1.Rows[x].Cells[0].Value == null || CopyPasteGrid1.Rows[x].Cells[1].Value == null)
                    continue;
                int Persona_Link_ID = Convert.ToInt32(CopyPasteGrid1.Rows[x].Cells[0].Value.ToString());
                decimal Cantidad = Convert.ToDecimal(CopyPasteGrid1.Rows[x].Cells[1].Value.ToString());
                DateTime Fecha = FIL_FECHA.SelectedDate;
                eClock.WS_Monedero WSMonedero = new eClock.WS_Monedero();
                WSMonedero.CobrarMonedero(Persona_Link_ID, Cantidad, Fecha);
            }
            catch (Exception ex) { CIsLog2.AgregaError(ex); }
        }
    }
}