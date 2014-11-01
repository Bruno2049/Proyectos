<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WF_TerminalE2.aspx.cs" Inherits="WF_TerminalE2" %>

<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.WebUI.WebDataInput" tagprefix="igtxt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <style type="text/css">
        .style1
        {
            height: 30px;
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
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
                <td>
                    Número de serie:</td>
                <td>
                    <igtxt:WebTextEdit ID="Tbx_Serie" runat="server">
                    </igtxt:WebTextEdit>
                </td>
                <td>
                    * Se encuentra en una etiqueta pegada al equipo</td>
            </tr>
            <tr>
                <td>
                    Clave de activación:</td>
                <td>
                    <igtxt:WebTextEdit ID="Tbx_Clave" runat="server">
                    </igtxt:WebTextEdit>
                </td>
                <td>
                    * Si no cuenta con una solicitela a su proveedor</td>
            </tr>
            <tr>
                <td>
                    Nombre:</td>
                <td>
                    <igtxt:WebTextEdit ID="Tbx_Nombre" runat="server" Text="Checador">
                    </igtxt:WebTextEdit>
                </td>
                <td>
                    * Asignele el nombre que desee, le servirá para identificarla en el futuro</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="3" class="style1">
						<asp:Label id="LError" runat="server" ForeColor="#CC0033" Font-Size="" Font-Names=""></asp:Label>
						<asp:Label id="LCorrecto" runat="server" ForeColor="#00C000" Font-Size="" Font-Names=""></asp:Label>
                    </td>
            </tr>
            <tr>
                <td colspan="3" class="style1">
                    &nbsp;
                        <igtxt:WebImageButton ID="BGuardarCambios" runat="server" Height="22px" OnClick="BGuardarCambios_Click"
                            Text="Guardar Cambios" UseBrowserDefaults="False" Width="150px" >
                            <RoundedCorners DisabledImageUrl="ig_butXP5o.gif" 
                                FocusImageUrl="ig_butXP3o.gif" HoverImageUrl="ig_butXP2o.gif" 
                                ImageUrl="ig_butXP1o.gif" MaxHeight="80" MaxWidth="400" 
                                PressedImageUrl="ig_butXP4o.gif" RenderingType="FileImages" />
                            
                        </igtxt:WebImageButton>
                    </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
