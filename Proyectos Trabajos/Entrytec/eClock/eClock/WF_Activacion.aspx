<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WF_Activacion.aspx.cs" Inherits="WF_Activacion" %>

<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>

<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Activación de eClock</title>
</head>
<body style="text-align: center">
    <form id="form1" runat="server">
    <div>
        &nbsp;</div>
        <igmisc:WebPanel ID="WebPanel1" runat="server" BorderColor="SteelBlue" BorderStyle="Outset"
            CssClass="igwpMainBlack2k7" Font-Names="Tahoma" Font-Size="Medium" Height="100%">
            <PanelStyle CssClass="igwpPanelBlack2k7">
                <Padding Left="15px" Right="15px" />
            </PanelStyle>
            <Header Text="ACTIVACI&#211;N DE eClock">
                <ExpandedAppearance>
                    <Styles BackColor="SteelBlue" CssClass="igwpHeaderExpandedBlack2k7" ForeColor="White">
                    </Styles>
                </ExpandedAppearance>
                <HoverAppearance>
                    <Styles CssClass="igwpHeaderHoverBlack2k7">
                    </Styles>
                </HoverAppearance>
                <CollapsedAppearance>
                    <Styles CssClass="igwpHeaderCollapsedBlack2k7">
                    </Styles>
                </CollapsedAppearance>
                <ExpansionIndicator Height="0px" Width="0px" />
            </Header>
            <Template>
                <br />
                <img src="Imagenes/imgencabezado.jpg" /><br />
                <br />
                <table style="width: 809px">
                    <tr>
                        <td>
                            <img src="Imagenes/Warningshield.jpg" /></td>
                        <td>
        <table>
            <tr>
                <td colspan="2" style="height: 26px; text-align: left">
                    <span style="font-size: 11pt">Llave de Producto.<br />
                        Teclee la llave del producto que viene acompañando a su disco de instalación, si
                        no posee dicha clave, póngase en contacto con su vendedor.</span></td>
            </tr>
            <tr>
                <td style="height: 26px; text-align: left">
                    <span style="font-size: 10pt; font-family: Arial">
                    Llave del producto:</span></td>
                <td style="height: 26px; text-align: left; width: 598px;">
                    &nbsp;<igtxt:webmaskedit id="txtLlave" runat="server" inputmask="CCCC-CCCC-CCCC-CCCC" Font-Names="Arial" Font-Size="10pt"></igtxt:webmaskedit>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="height: 39px; text-align: left">
                    <asp:RadioButtonList ID="RBTActiv" runat="server" RepeatColumns="2" Width="383px" Font-Names="Arial" Font-Size="10pt">
                        <asp:ListItem>Activacion Telefonica</asp:ListItem>
                        <asp:ListItem>Activacion por Internet</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:Label ID="LError" runat="server" Font-Names="Arial" Font-Size="10pt" ForeColor="Red"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="2" style="height: 16px">
                    <igtxt:webimagebutton id="BGuardarCambios" runat="server" height="22px" onclick="BGuardarCambios_Click"
                        text="Siguiente" usebrowserdefaults="False" width="150px">
                            <Alignments VerticalImage="Middle" VerticalAll="Bottom"     />
                            <RoundedCorners HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif"
                                MaxHeight="80" MaxWidth="400" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"     />
                            <Appearance>
				
                                <Image Url="./Imagenes/Next.png"     />
                            </Appearance>
                        </igtxt:webimagebutton>
                </td>
            </tr>
        </table>
                        </td>
                    </tr>
                </table>
                <br />
            </Template>
        </igmisc:WebPanel>
    </form>
</body>
</html>
