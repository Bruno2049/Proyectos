<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="WF_DatosEmpresa.aspx.cs" Inherits="WF_DatosEmpresa" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebToolbar.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebToolbar" TagPrefix="igtbar" %>
<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>

<%@ Register Src="WC_Menu.ascx" TagName="WC_Menu" TagPrefix="uc1" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Configuracion de Variables</title>

</head>
<body style="font-size: 8px; font-family: tahoma; text-align: center; margin: 0px; ">
    <form id="form1" runat="server" >
        <table width="100%">
            <tr>
                <td align="center">
                    <br />
                    <igmisc:webpanel id="WebPanel2" runat="server" EnableAppStyling="True" 
                        StyleSetName="Caribbean" Width="421px" Height="186px" >

<Header TextAlignment="Left" Text="Datos de la Empresa">
</Header>
<Template>
<TABLE><TBODY><TR><TD align=left >Nombre de Compañia&nbsp;</TD><TD align=left><BR />
        <asp:TextBox id="TxtNombre" runat="server" Width="218px"></asp:TextBox><BR /></TD></TR><TR>
            <TD align=left >Direccion&nbsp;</TD><TD align=left><BR />
            <asp:TextBox id="TxtDireccion" runat="server" Width="218px"></asp:TextBox><BR /></TD></TR><TR>
            <TD align=left >Telefono &nbsp;</TD><TD align=left><BR />
            <asp:TextBox id="TxtTelefono" runat="server" Width="218px"></asp:TextBox><BR /></TD></TR><TR>
            <TD align=left >URL&nbsp;</TD><TD style="HEIGHT: 26px" align=left><BR />
            <asp:TextBox id="TxtURL" runat="server" Width="218px"></asp:TextBox><BR /></TD></TR></TBODY></TABLE>
</Template>
</igmisc:webpanel>
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td align="center" style="height: 58px">
                    <asp:Label ID="lOperacion" runat="server"></asp:Label><br />
                    <asp:Label ID="Lbl_Error" runat="server" ForeColor="Red"></asp:Label><br />
                    <asp:Label ID="Lbl_Correcto" runat="server" ForeColor="Green"></asp:Label><br />
                    <br />
                    <igtxt:webimagebutton id="BGuardarCambios0" runat="server" height="22px" onclick="BGuardarCambios0_Click"
                        text="Inicializar Campos" usebrowserdefaults="False" width="150px" 
                        ImageTextSpacing="4">
							<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
							<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
								RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
							<Appearance>
								<Style Cursor="Default">
								</Style>
								<Image Url="./Imagenes/Save_as.png" Height="16px" Width="16px"></Image>
							</Appearance>
						</igtxt:webimagebutton>
                    &nbsp;&nbsp;
                    <igtxt:webimagebutton id="BDeshacerCambios" runat="server" height="22px"
                        text="Deshacer Cambios" usebrowserdefaults="False" width="150px" ImageTextSpacing="4">
							<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
							<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
								RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
							<Appearance>
								<Style Cursor="Default">
								</Style>
								<Image Url="./Imagenes/Undo.png" Height="16px" Width="16px"></Image>
							</Appearance>
						</igtxt:webimagebutton>
                    &nbsp; &nbsp; &nbsp;
                    <igtxt:webimagebutton id="BGuardarCambios" runat="server" height="22px" onclick="BGuardarCambios_Click"
                        text="Guardar Cambios" usebrowserdefaults="False" width="150px" ImageTextSpacing="4">
							<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
							<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
								RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
							<Appearance>
								<Style Cursor="Default">
								</Style>
								<Image Url="./Imagenes/Save_as.png" Height="16px" Width="16px"></Image>
							</Appearance>
						</igtxt:webimagebutton>
                    &nbsp; &nbsp; &nbsp;
                </td>
            </tr>
        </table>
    
    </form>
</body>
</html>

