<%@ Page Language="C#" MasterPageFile="~/MasterPage.master"  AutoEventWireup="true" CodeFile="WF_ProductosE.aspx.cs" Inherits="WF_ProductosE" %>

<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>

<%@ Register Src="WC_Menu.ascx" TagName="WC_Menu" TagPrefix="uc1" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>

<%@ Register Assembly="PdfViewer CS 2k3" Namespace="PdfViewer" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <table style=" width: 100%; height: 100%">
            <tr>
                <td style="width: 100%">
                    &nbsp;<igmisc:webpanel id="WebPanel2" runat="server" backcolor="White" bordercolor="SteelBlue"
                        borderstyle="Outset" borderwidth="2px" font-bold="False" font-names="ARIAL" forecolor="Black"
                        stylesetname="PaneleClock">
<PanelStyle BorderStyle="Solid" ForeColor="Black" BorderWidth="1px" Font-Names="Arial">
<BorderDetails ColorTop="0, 45, 150" ColorLeft="158, 190, 245" ColorBottom="0, 45, 150" ColorRight="0, 45, 150"></BorderDetails>

<Padding Top="5px" Left="5px" Bottom="5px" Right="5px"></Padding>
</PanelStyle>

<Header TextAlignment="Left">
<ExpandedAppearance>
<Styles BackgroundImage="./images/GridTitulo.gif" BorderStyle="Ridge" ForeColor="Black" BorderWidth="1px" BorderColor="Transparent" Height="15px" Font-Size="9pt" Font-Names="Arial" Font-Bold="True">
<BorderDetails ColorTop="158, 190, 245" WidthBottom="0px" ColorLeft="158, 190, 245" ColorRight="0, 45, 150"></BorderDetails>

<Padding Top="1px" Left="4px" Bottom="1px"></Padding>
</Styles>
</ExpandedAppearance>

<HoverAppearance>
<Styles CssClass="igwpHeaderHoverBlue2k7"></Styles>
</HoverAppearance>

<CollapsedAppearance>
<Styles CssClass="igwpHeaderCollapsedBlue2k7"></Styles>
</CollapsedAppearance>

<ExpansionIndicator Height="0px" Width="0px"></ExpansionIndicator>
</Header>
<Template>
<TABLE style="WIDTH: 410px; HEIGHT: 100%" id="Table2" cellSpacing=1 cellPadding=1 width="100%" align=center border=0><TBODY><TR><TD style="FONT-SIZE: 11pt; WIDTH: 179px; FONT-FAMILY: Arial; HEIGHT: 25px" align=left>ID Producto</TD><TD style="WIDTH: 210px; HEIGHT: 25px" align=left><igtxt:WebNumericEdit id="txtidproducto" runat="server" Enabled="False">
                                </igtxt:WebNumericEdit> </TD></TR><TR><TD style="FONT-SIZE: 11pt; WIDTH: 179px; FONT-FAMILY: Arial; HEIGHT: 12px" align=left>No. Producto</TD><TD style="WIDTH: 210px; HEIGHT: 12px" align=left><FONT face="Arial"><igtxt:WebNumericEdit id="txtnumprod" runat="server">
                                    </igtxt:WebNumericEdit> </FONT></TD></TR><TR><TD style="FONT-SIZE: 11pt; WIDTH: 179px; FONT-FAMILY: Arial; HEIGHT: 12px" align=left>Nombre de Producto</TD><TD style="WIDTH: 210px; HEIGHT: 12px" align=left><FONT face="Arial"><igtxt:WebTextEdit id="txtnombre" runat="server" HorizontalAlign="Right">
                                    </igtxt:WebTextEdit> </FONT></TD></TR><TR><TD style="FONT-SIZE: 11pt; WIDTH: 179px; FONT-FAMILY: Arial; HEIGHT: 26px" align=left>Costo de Producto</TD><TD style="WIDTH: 210px; HEIGHT: 26px" align=left><FONT face="Arial"><igtxt:WebNumericEdit id="txtcosto" runat="server">
                                    </igtxt:WebNumericEdit> </FONT></TD></TR><TR><TD style="WIDTH: 179px" align=left><FONT face="Arial"><asp:Label id="LBorrar" runat="server" Font-Names="Arial Narrow" ForeColor="Red" Font-Size="X-Small">* Seleccione esta opción si lo que quiere es borrar el producto</asp:Label></FONT></TD><TD style="WIDTH: 210px" align=left>&nbsp; <asp:CheckBox id="CBBorrar" runat="server" Text="Borrar Producto" Font-Names="Arial Narrow" Font-Size="Small" Font-Strikeout="False"></asp:CheckBox><FONT face="Arial"></FONT></TD></TR></TBODY></TABLE>
    <asp:Label ID="LError" runat="server" ForeColor="Red"></asp:Label>
</Template>
</igmisc:webpanel>
                </td>
            </tr>
            <tr>
                <td align="center">
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;<igtxt:WebImageButton
                        ID="BDeshacerCambios" runat="server" Height="22px" Text="Deshacer Cambios" UseBrowserDefaults="False"
                        Width="150px" ImageTextSpacing="4">
                        <Alignments VerticalImage="Middle" VerticalAll="Bottom" />
                        <RoundedCorners HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif"
                            MaxHeight="80" MaxWidth="400" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif" />
                        <Appearance>
                            <Style Cursor="Default">
								</Style>
                            <Image Url="./Imagenes/Undo.png" Height="16px" Width="16px" />
                        </Appearance>
                    </igtxt:WebImageButton>
                    &nbsp; &nbsp;&nbsp;
                    <igtxt:WebImageButton ID="BGuardarCambios" runat="server" Height="22px" OnClick="BGuardarCambios_Click"
                        Text="Guardar Cambios" UseBrowserDefaults="False" Width="150px" ImageTextSpacing="4">
                        <Alignments VerticalImage="Middle" VerticalAll="Bottom" />
                        <RoundedCorners HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif"
                            MaxHeight="80" MaxWidth="400" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif" />
                        <Appearance>
                            <Style Cursor="Default">
								</Style>
                            <Image Url="./Imagenes/Save_as.png" Height="16px" Width="16px" />
                        </Appearance>
                    </igtxt:WebImageButton>
                </td>
            </tr>
        </table>
    
</asp:Content>