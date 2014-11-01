<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WF_ConfigSMTP.aspx.cs" Inherits="WF_ConfigSMTP" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebToolbar.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebToolbar" TagPrefix="igtbar" %>
<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>

<%@ Register Src="WC_Menu.ascx" TagName="WC_Menu" TagPrefix="uc1" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Edicion de Incidencias</title>
    <style type="text/css">
        .style1
        {
            height: 59px;
        }
        .style2
        {
            height: 287px;
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
            <td class="style2">
                </td>
            <td class="style2">

    <igmisc:webpanel id="WebPanel2" runat="server" EnableAppStyling="True" Height="189px" StyleSetName="Caribbean" 
       >


<Header TextAlignment="Left" Text="Configuracion de SMTP">


</Header>
<Template>
<TABLE><TBODY><TR><TD align=center><BR /><TABLE style="WIDTH: 332px" id="Table2" language="javascript" onclick="return TABLE1_onclick()"><TBODY><TR><TD style="WIDTH: 122px" align=left>
    Correo&nbsp;</TD><TD align=left><igtxt:WebTextEdit id="txtServidorCorreo" runat="server" Width="216px">
                                </igtxt:WebTextEdit> </TD></TR><TR><TD style="WIDTH: 122px" align=left>
                                    Sevidor</TD><TD align=left><igtxt:WebTextEdit id="txtServidorSMTP" runat="server" Width="216px">
                                </igtxt:WebTextEdit> </TD></TR><TR><TD style="WIDTH: 122px" align=left>
                                    Puerto &nbsp;</TD><TD align=left><igtxt:WebTextEdit id="txtServidorSMTPPuerto" runat="server" Width="216px">
                                </igtxt:WebTextEdit> </TD></TR><TR><TD style="WIDTH: 122px;" align=left>
                                    Nombre de Usuario &nbsp;</TD><TD style="HEIGHT: 26px" align=left><igtxt:WebTextEdit id="txtSMTPNombre" runat="server" Width="216px">
                                </igtxt:WebTextEdit> </TD></TR><TR><TD style="WIDTH: 122px;" align=left>
                                    Password
                                </TD><TD style="HEIGHT: 26px" align=left><igtxt:WebTextEdit id="txtSMTPPass" runat="server" Width="216px">
                                </igtxt:WebTextEdit> </TD></TR></TBODY></TABLE></TD></TR></TBODY></TABLE>
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
            <td class="style2">
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

