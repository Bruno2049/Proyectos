using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WF_Contacto : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected string ObtenTexto(Infragistics.WebUI.WebDataInput.WebNumericEdit Valor)
    {
        return Valor.Text;
    }
    protected string ObtenTexto(DropDownList Valor)
    {
        return Valor.Text;
    }
    protected string ObtenTexto(TextBox Valor)
    {
        return Valor.Text;
    }
    protected string ObtenTexto(RadioButtonList Valor)
    {
        return Valor.Text;
    }
    protected string ObtenTexto(CheckBoxList Valor)
    {
        return Valor.Text;
    }
    protected string ObtenTexto(Label Etiqueta, Infragistics.WebUI.WebDataInput.WebNumericEdit Valor)
    {
        return ObtenTexto(Etiqueta.Text, Valor.Text);
    }
    protected string ObtenTexto(Label Etiqueta, TextBox Valor)
    {
        return ObtenTexto(Etiqueta.Text, Valor.Text);
    }
    protected string ObtenTexto(Label Etiqueta, DropDownList Valor)
    {
        return ObtenTexto(Etiqueta.Text, Valor.Text);
    }

    protected string ObtenTexto(Label Etiqueta, RadioButtonList Valor)
    {
        return ObtenTexto(Etiqueta.Text, Valor.Text);
    }

    protected string ObtenTexto(Label Etiqueta, CheckBoxList Valor)
    {
        return ObtenTexto(Etiqueta.Text, Valor.Text);
    }
    protected string ObtenTexto(string Etiqueta, string Valor)
    {
        return "<b> " + Etiqueta + "</b> = " + Valor + " <br />";
    }

    
    protected void Button1_Click(object sender, EventArgs e)
    {
        
        string TextoHTML = "";
        if (ObtenTexto(Tbx_Nombre).Length < 1
            || ObtenTexto(Tbx_APaterno).Length < 1
            || ObtenTexto(Tbx_eMail).Length < 1
            || ObtenTexto(Tbx_Telefono).Length < 1
            || ObtenTexto(Cbx_Sector).Length < 1
            || ObtenTexto(Cbx_Sitio).Length < 1
            || ObtenTexto(Cbx_Pais).Length < 1
            || ObtenTexto(Tbx_NoEmpleados).Length < 1
            || ObtenTexto(Obt_Intereses).Length < 1
            || ObtenTexto(Cbl_Tecnologias).Length < 1
            || ObtenTexto(Tbx_eMail) != ObtenTexto(Tbx_eMailConf)
)
            return;

        TextoHTML += ObtenTexto(Lbl_Razon, Tbx_Razon);
        TextoHTML += ObtenTexto(Lbl_RFC, Tbx_RFC);
        TextoHTML += ObtenTexto(Lbl_Nombre, Tbx_Nombre);
        TextoHTML += ObtenTexto(Lbl_APaterno, Tbx_APaterno);
        TextoHTML += ObtenTexto(Lbl_AMaterno, Tbx_AMaterno);
        TextoHTML += ObtenTexto(Lbl_eMail, Tbx_eMail);
        TextoHTML += ObtenTexto(Lbl_Telefono, Tbx_Telefono);
        TextoHTML += ObtenTexto(Lbl_Extencion, Tbx_Extencion);
        TextoHTML += ObtenTexto(Lbl_Celular, Tbx_Celular);
        TextoHTML += ObtenTexto(Lbl_Puesto, Tbx_Puesto);
        TextoHTML += ObtenTexto(Lbl_Sector, Cbx_Sector);
        TextoHTML += ObtenTexto(Lbl_Sitio, Cbx_Sitio);
        TextoHTML += ObtenTexto(Lbl_Calle, Tbx_Calle);
        TextoHTML += ObtenTexto(Lbl_Colonia, Tbx_Colonia);
        TextoHTML += ObtenTexto(Lbl_Delegacion, Tbx_Delegacion);
        TextoHTML += ObtenTexto(Lbl_Estado, Tbx_Estado);
        TextoHTML += ObtenTexto(Lbl_CP, Tbx_CP);
        TextoHTML += ObtenTexto(Lbl_Pais, Cbx_Pais);
        TextoHTML += ObtenTexto(Lbl_NoEmpleados, Tbx_NoEmpleados);
        TextoHTML += ObtenTexto(Lbl_Intereses, Obt_Intereses);
        TextoHTML += ObtenTexto(Lbl_Tecnologias, Cbl_Tecnologias);
        TextoHTML += ObtenTexto(Cbx_Sucursales.Text, Cbx_Sucursales.Checked?"Si":"No");
        TextoHTML += ObtenTexto(Lbl_NoSucursales, Tbx_NoSucursales);
        TextoHTML += ObtenTexto(Lbl_Comentarios, Tbx_Comentarios);
        if (CeC_BD.EnviarMail("ventas@eClock.com.mx", ObtenTexto(Tbx_eMail), "Contacto", TextoHTML, ""))
        {
            Lbl_Correcto.Text = "Se ha enviado su información el correo ventas@eClock.com.mx con copia para " + ObtenTexto(Tbx_eMail)+" en breve uno de nuestros ejecutivos lo contactara, no olvide checar su correo y tambien la bandeja de correo no deseado";
        }
    }

}
