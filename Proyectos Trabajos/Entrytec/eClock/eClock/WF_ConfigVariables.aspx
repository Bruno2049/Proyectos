<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WF_ConfigVariables.aspx.cs" Inherits="WF_ConfigVariables" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebToolbar.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebToolbar" TagPrefix="igtbar" %>
<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>

<%@ Register Src="WC_Menu.ascx" TagName="WC_Menu" TagPrefix="uc1" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Configuracion de Variables</title>
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
            <td>
                &nbsp;</td>
            <td>
    <igmisc:webpanel id="WebPanel2" runat="server" EnableAppStyling="True" 
                    StyleSetName="Caribbean" >


<Header TextAlignment="Left" Text="Configuracion de Variables">


<ExpansionIndicator Height="0px" Width="0px"></ExpansionIndicator>
</Header>
<Template>
<TABLE><TBODY><TR><TD align=center><TABLE style="WIDTH: 332px" id="Table2" language="javascript" onclick="return TABLE1_onclick()"><TBODY><TR><TD style="WIDTH: 216px" align=left>RutaReportesPDF&nbsp;</TD><TD align=left><BR /><asp:TextBox id="txtRutaPDF" runat="server" Width="327px"></asp:TextBox><BR /></TD></TR><TR><TD style="WIDTH: 216px" align=left>RutaReportesXLS&nbsp;</TD><TD align=left><BR /><asp:TextBox id="txtRutaXLS" runat="server" Width="327px"></asp:TextBox><BR /></TD></TR><TR><TD style="WIDTH: 216px" align=left>MensajeJScript &nbsp;</TD><TD align=left><BR /><asp:TextBox id="txtMensajeJscript" runat="server" Width="327px"></asp:TextBox><BR /></TD></TR><TR><TD style="WIDTH: 216px; HEIGHT: 26px" align=left>Nombre Persona&nbsp; </TD><TD style="HEIGHT: 26px" align=left><BR /><asp:TextBox id="txtNombrePersona" runat="server" Width="327px"></asp:TextBox><BR /></TD></TR>
    <tr>
        <td align="left" style="width: 216px; height: 26px">
            Leyenda de Reporte de Asistencia</td>
        <td align="left" style="height: 26px">
            <asp:TextBox ID="TxtLeyendaRep" runat="server" Width="327px"></asp:TextBox></td>
    </tr>
</TBODY></TABLE></TD></TR></TBODY></TABLE>
</Template>
</igmisc:webpanel>

    <TABLE>
        <tr>
            <TD style="HEIGHT: 58px" align=center><asp:Label id="lOperacion" runat="server"></asp:Label><BR /><igtxt:webimagebutton id="BDeshacerCambios" onclick="BDeshacerCambios_Click" runat="server" width="150px" usebrowserdefaults="False" text="Deshacer Cambios" height="22px" ImageTextSpacing="4">
							<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
							<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
								RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
							<Appearance>
								<Style Cursor="Default">
								</Style>
								<Image Url="./Imagenes/Undo.png" Height="16px" Width="16px"></Image>
							</Appearance>
						</igtxt:webimagebutton> &nbsp; &nbsp; &nbsp; <igtxt:webimagebutton id="BGuardarCambios" onclick="BGuardarCambios_Click" runat="server" width="150px" usebrowserdefaults="False" text="Guardar Cambios" height="22px" ImageTextSpacing="4">
							<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
							<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
								RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
							<Appearance>
								<Style Cursor="Default">
								</Style>
								<Image Url="./Imagenes/Save_as.png" Height="16px" Width="16px"></Image>
							</Appearance>
						</igtxt:webimagebutton> </TD>
        </tr>
    </table>
    
            </td>
            <td>
                &nbsp;</td>
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

