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
using System.Drawing;

public partial class WF_CostoComidas : System.Web.UI.Page
{
    private CeC_Sesion Sesion;
    private string qry = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        Sesion.TituloPagina = "Costo de comidas";
        Sesion.DescripcionPagina = "Costo de comidas";
        this.Title = "Costo de comidas";
        
        // Permisos****************************************
        if (!Sesion.TienePermisoOHijos(eClock.CEC_RESTRICCIONES.S0Comidas0Costo, true))
        {
            Webpanel1.Visible = false;
            WebPanel2.Visible = false;
            Webpanel3.Visible = false;
            return;
        }
        //**************************************************
        
        if (!IsPostBack)
        {
            qry = "SELECT TIPO_COMIDA_COSTO FROM EC_TIPO_COMIDA WHERE TIPO_COMIDA_ID = 1";
            decimal Costo1 = Convert.ToDecimal(CeC_BD.EjecutaEscalar(qry));
            qry = "SELECT TIPO_COMIDA_COSTO FROM EC_TIPO_COMIDA WHERE TIPO_COMIDA_ID = 2";
            decimal Costo2 = Convert.ToDecimal(CeC_BD.EjecutaEscalar(qry));
            CostoComida1.Value = Costo1;
            CostoComida2.Value = Costo2;
            ChkDespuesComida.Checked = CeC_Config.PermiteComerDespues;
            ChkCreditoComida.Checked = CeC_Config.PermiteCreditoComida;
            ChkDespuesEntrada.Checked = CeC_Config.PermiteDesayunarDespues;
            ChkCreditoDesayuno.Checked = CeC_Config.PermiteCreditoDesayuno;
            
            //Agregar Módulo Log
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Costo de Comidas", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
        }
    }

    protected void BtnGuardar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        try
        {
            qry = "UPDATE EC_TIPO_COMIDA SET TIPO_COMIDA_COSTO = " + CostoComida1.ValueDecimal.ToString() + "WHERE TIPO_COMIDA_ID = 1";
            int ret1 = CeC_BD.EjecutaComando(qry);
            qry = "UPDATE EC_TIPO_COMIDA SET TIPO_COMIDA_COSTO = " + CostoComida2.ValueDecimal.ToString() + "WHERE TIPO_COMIDA_ID = 2";
            int ret2 = CeC_BD.EjecutaComando(qry);
            if (ret1 == 1 && ret2 == 1)
            {
                LblEstado.ForeColor = Color.Green;
                LblEstado.Text = "Los costos de las comidas se han guardado satisfactoriamente";
            }
            else if (ret1 == 1 && ret2 == 0)
            {
                LblEstado.ForeColor = Color.Green;
                LblEstado.Text = "El costo de la primera comida se ha guardado satisfactoriamente";
            }
            else if (ret1 == 0 && ret2 == 1)
            {
                LblEstado.ForeColor = Color.Green;
                LblEstado.Text = "El costo de la segunda comida se ha guardado satisfactoriamente";
            }
            else if (ret1 == 0 && ret2 == 0)
            {
                LblEstado.ForeColor = Color.Red;
                LblEstado.Text = "Los costos de la comida no se han guardado";
            }
        }
        catch (Exception ex)
        {
            LblEstado.ForeColor = Color.Red;
            LblEstado.Text = "Los costos de la comida no se han guardado";
        }
    }

    protected void BtnGuardarComidas_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        CeC_Config.PermiteComerDespues = ChkDespuesComida.Checked;
        CeC_Config.PermiteCreditoComida = ChkCreditoComida.Checked;
    }

    protected void BtnGuardarDesayunos_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        CeC_Config.PermiteCreditoDesayuno = ChkCreditoDesayuno.Checked;
        CeC_Config.PermiteDesayunarDespues = ChkDespuesEntrada.Checked;
    }
}