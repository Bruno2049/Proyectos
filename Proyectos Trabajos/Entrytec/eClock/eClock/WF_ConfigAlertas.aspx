<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WF_ConfigAlertas.aspx.cs" Inherits="WF_ConfigAlertas" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebToolbar.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebToolbar" TagPrefix="igtbar" %>
<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>

<%@ Register Src="WC_Menu.ascx" TagName="WC_Menu" TagPrefix="uc1" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Configuración de alertas</title>
    <style type="text/css">
        .style1
        {
            height: 241px;
        }
    </style>
</head>
<body style="font-size: 11px; font-family: tahoma; text-align: center; margin: 0px; ">
    <form id="form1" runat="server" >
    <table style="width:100%;">
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                </td>
            <td class="style1">

    <igmisc:webpanel id="WebPanel2" runat="server" EnableAppStyling="True" Height="189px" 
                    StyleSetName="Caribbean" Width="506px" 
       >


<Header TextAlignment="Left" Text="Alertas via correo electronico (activas)">


</Header>
<Template>
<TABLE><TBODY><TR><TD align=center><BR /><TABLE style="WIDTH: 332px" id="Table2" language="javascript" onclick="return TABLE1_onclick()"><TBODY><TR><TD align=left>
    <asp:CheckBox ID="Cbx_RetardosSupervisor" runat="server" 
        Text="Retardos a supervisor" />
    </TD>
    <td align="left">
        <asp:CheckBox ID="Cbx_RetardosEmpleado" runat="server" 
            Text="Retardos al empleado" />
    </td>
    <td align="left">
        &nbsp;</td>
    </TR><TR><TD align=left>
        <asp:CheckBox ID="Cbx_FaltasSupervisor" runat="server" 
            Text="Faltas a supervisor" />
        </TD>
        <td align="left">
            <asp:CheckBox ID="Cbx_FaltasEmpleado" runat="server" 
                Text="Faltas a empleado " />
        </td>
        <td align="left">
            &nbsp;</td>
    </TR>
    <tr>
        <td align="left">
            <asp:CheckBox ID="Cbx_Terminal" runat="server" Text="Terminal no responde" />
        </td>
        <td align="left">
            &nbsp;</td>
        <td align="left">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="left">
            &nbsp;</td>
        <td align="left">
            &nbsp;</td>
        <td align="left">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="left">
            &nbsp;</td>
        <td align="left">
            &nbsp;</td>
        <td align="left">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="left">
            &nbsp;</td>
        <td align="left">
            &nbsp;</td>
        <td align="left">
            &nbsp;</td>
    </tr>
    </TBODY></TABLE></TD></TR></TBODY></TABLE>
</Template>
</igmisc:webpanel>
                <br />
                <asp:Label id="LOperacion" runat="server"></asp:Label>
                <br />
                <igtxt:WebImageButton id="BDeshacerCambios" 
            runat="server" Text="Deshacer Cambios" Width="150px" UseBrowserDefaults="False" 
            Height="22px" ImageTextSpacing="4">
                    <Alignments VerticalImage="Middle" VerticalAll="Bottom"  />
                    <RoundedCorners HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif"
                            MaxHeight="80" MaxWidth="400" PressedImageUrl="ig_butXP4wh.gif" 
                            RenderingType="FileImages" DisabledImageUrl="ig_butXP5wh.gif" 
                            FocusImageUrl="ig_butXP3wh.gif"  />
                    <Appearance>
                        <Style Cursor="Default">
                        </Style>
                        <Image Url="./Imagenes/Undo.png" Height="16px" Width="16px"  />
                    </Appearance>
                </igtxt:WebImageButton>
                <igtxt:WebImageButton 
            id="BGuardarCambios" onclick="BGuardarCambios_Click" runat="server" 
            Text="Guardar Cambios" Width="150px" UseBrowserDefaults="False" Height="22px" 
            ImageTextSpacing="4">
                    <Alignments VerticalImage="Middle" VerticalAll="Bottom"  />
                    <RoundedCorners HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif"
                            MaxHeight="80" MaxWidth="400" PressedImageUrl="ig_butXP4wh.gif" 
                            RenderingType="FileImages" DisabledImageUrl="ig_butXP5wh.gif" 
                            FocusImageUrl="ig_butXP3wh.gif"  />
                    <Appearance>
                        <Style Cursor="Default">
                        </Style>
                        <Image Url="./Imagenes/Save_as.png" Height="16px" Width="16px"  />
                    </Appearance>
                </igtxt:WebImageButton>
            </td>
            <td class="style1">
                </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    </form>
</body>
</html>


