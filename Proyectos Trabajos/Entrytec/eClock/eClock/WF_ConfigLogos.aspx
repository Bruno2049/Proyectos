<%@ Page Language="C#" MasterPageFile="~/MasterPage.master"  AutoEventWireup="true" CodeFile="WF_ConfigLogos.aspx.cs" Inherits="WF_ConfigLogos" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebToolbar.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebToolbar" TagPrefix="igtbar" %>
<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>

<%@ Register Src="WC_Menu.ascx" TagName="WC_Menu" TagPrefix="uc1" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
    <asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <igtbar:ultrawebtoolbar id="UltraWebToolbar2" runat="server" backcolor="SteelBlue"
            backgroundimage="none" bordercolor="SteelBlue" borderstyle="Solid" borderwidth="1px"
            font-bold="True" font-names="Arial" font-size="9pt" forecolor="White" imagedirectory="/ig_common/images/"
            itemspacing="0" itemwidthdefault="150px" movableimage="" textalign="Center">
                                            <HoverStyle BackColor="LightBlue" BackgroundImage="none" BorderColor="Navy" BorderStyle="Solid"
                                                BorderWidth="1px" Cursor="Default" Font-Names="Arial" Font-Size="9pt" ForeColor="Black">
                                            </HoverStyle>
                                            <DefaultStyle BackColor="SteelBlue" BorderColor="BlanchedAlmond" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="Arial" Font-Size="9pt" ForeColor="White">
                                            </DefaultStyle>
                                            <Items>
                                                <igtbar:TBarButton DisabledImage="" HoverImage="" Image="" SelectedImage="" TargetURL="WF_Config.aspx"
                                                    Text="Configuracion Principal">
                                                </igtbar:TBarButton>
                                                <igtbar:TBarButton DisabledImage="" HoverImage="" Image="" SelectedImage="" TargetURL="WF_ConfigSMTP.aspx"
                                                    Text="Configurar SMTP">
                                                </igtbar:TBarButton>
                                                <%--<igtbar:TBarButton DisabledImage="" HoverImage="" Image="" SelectedImage="" TargetURL="WF_ConfigVariables.aspx"
                                                    Text="Configurar Variables">--%>
                                                <igtbar:TBarButton DisabledImage="" HoverImage="" Image="" SelectedImage="" TargetURL="WF_ConfigUsuario.aspx"
                                                    Text="Configurar Variables">
                                                </igtbar:TBarButton>
                                                <igtbar:TBarButton DisabledImage="" HoverImage="" Image="" SelectedImage="" TargetURL="WF_DatosEmpresa.aspx"
                                                    Text="Datos de la Empresa">
                                                </igtbar:TBarButton>
                                                <igtbar:TBarButton DisabledImage="" HoverImage="" Image="" SelectedImage="" TargetURL="WF_Wizardb.aspx"
                                                    Text="Configurar Etiquetas">
                                                </igtbar:TBarButton>
                                                <igtbar:TBarButton DisabledImage="" HoverImage="" Image="" SelectedImage="" TargetURL="WF_ConfigLogos.aspx"
                                                    Text="Configurar Logotipos">
                                                </igtbar:TBarButton>
                                            </Items>
                                            <SelectedStyle BackColor="Gainsboro" BackgroundImage="none" BorderColor="Navy" BorderStyle="Solid"
                                                BorderWidth="1px" Cursor="Default" Font-Names="Arial" Font-Size="9pt" ForeColor="Black">
                                            </SelectedStyle>
                                            <LabelStyle Width="80px" ></LabelStyle>
                                        </igtbar:ultrawebtoolbar>
        <table >
            <tr>
                <td style="height: 62%" align="center">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center" colspan="2" style="height: 84px">
                    <igmisc:webpanel id="WebPanel2" runat="server" backcolor="White" bordercolor="SteelBlue"
                        borderstyle="Outset" borderwidth="2px" font-bold="True" font-names="ARIAL" forecolor="Black"
                        stylesetname="PaneleClock" width="100%">
<PanelStyle BorderStyle="Solid" ForeColor="Black" BorderWidth="1px" Font-Names="Arial">
<BorderDetails ColorTop="0, 45, 150" ColorLeft="158, 190, 245" ColorBottom="0, 45, 150" ColorRight="0, 45, 150"></BorderDetails>

<Padding Top="5px" Left="5px" Bottom="5px" Right="5px"></Padding>
</PanelStyle>

<Header TextAlignment="Left" Text="Login">
<ExpandedAppearance>
<Styles BackgroundImage="./images/GridTitulo.gif" BorderStyle="Ridge" ForeColor="Black" BorderWidth="1px" BorderColor="Transparent" Height="15px" Font-Size="9pt" Font-Names="Arial">
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
<BR /><asp:Image id="Image1" runat="server" ImageUrl="WF_Logos_imglogin.aspx" Width="770px" Height="400px"></asp:Image>&nbsp;<BR />
    <asp:FileUpload ID="File1" runat="server" /><BR /><asp:Label id="Label4" runat="server" Text="Se Recomienda un Tamaño de 400 x 770 px" Font-Names="arial" ForeColor="Blue" Font-Size="Small"></asp:Label><BR /><igtxt:webimagebutton id="btnsubir1" onclick="btnsubir1_Click" runat="server" width="150px" usebrowserdefaults="False" text="Subir Imagen" height="22px" ImageTextSpacing="4">
<Alignments VerticalImage="Bottom" HorizontalAll="NotSet" VerticalAll="NotSet"></Alignments>

<Appearance>
<Image Url="./Imagenes/panel-screenshot.png" Height="18px" Width="20px"></Image>

<Style Cursor="Default"></Style>
</Appearance>

<RoundedCorners HoverImageUrl="ig_butXP2wh.gif" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" MaxWidth="400" MaxHeight="80" ImageUrl="ig_butXP1wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
</igtxt:webimagebutton> 
</Template>
</igmisc:webpanel>
                </td>
            </tr>
            <tr>
                <td align="center" valign="middle">
                    <igmisc:webpanel id="Webpanel1" runat="server" backcolor="White" bordercolor="SteelBlue"
                        borderstyle="Outset" borderwidth="2px" font-bold="True" font-names="ARIAL" forecolor="Black"
                        height="244px" stylesetname="PaneleClock" width="100%">
<PanelStyle BorderStyle="Solid" ForeColor="Black" BorderWidth="1px" Font-Names="Arial">
<BorderDetails ColorTop="0, 45, 150" ColorLeft="158, 190, 245" ColorBottom="0, 45, 150" ColorRight="0, 45, 150"></BorderDetails>

<Padding Top="5px" Left="5px" Bottom="5px" Right="5px"></Padding>
</PanelStyle>

<Header TextAlignment="Left" Text="Encabezado">
<ExpandedAppearance>
<Styles BackgroundImage="./images/GridTitulo.gif" BorderStyle="Ridge" ForeColor="Black" BorderWidth="1px" BorderColor="Transparent" Height="15px" Font-Size="9pt" Font-Names="Arial">
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
<BR /><BR /><BR /><BR /><BR /><asp:Image id="Image2" runat="server" ImageUrl="WF_Logos_imgencabezado.aspx" Width="450px" Height="47px"></asp:Image><BR /><BR /><asp:Label id="Label5" runat="server" Text="Se Recomienda un Tamaño de 451 x 48 px" Font-Names="arial" ForeColor="Blue" Width="299px" Font-Size="Small"></asp:Label><BR />
    &nbsp;<asp:FileUpload ID="File2" runat="server" /><BR /><BR /><igtxt:webimagebutton id="btnsubir2" onclick="btnsubir2_Click" runat="server" width="150px" usebrowserdefaults="False" text="Subir Imagen" height="22px" ImageTextSpacing="4">
<Alignments VerticalImage="Bottom" HorizontalAll="NotSet" VerticalAll="NotSet"></Alignments>

<Appearance>
<Image Url="./Imagenes/panel-screenshot.png" Height="18px" Width="20px"></Image>

<Style Cursor="Default"></Style>
</Appearance>

<RoundedCorners HoverImageUrl="ig_butXP2wh.gif" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" MaxWidth="400" MaxHeight="80" ImageUrl="ig_butXP1wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
</igtxt:webimagebutton>
</Template>
</igmisc:webpanel>
                </td>
                <td align="center">
                    <igmisc:webpanel id="Webpanel3" runat="server" backcolor="White" bordercolor="SteelBlue"
                        borderstyle="Outset" borderwidth="2px" font-bold="True" font-names="ARIAL" forecolor="Black"
                        height="242px" stylesetname="PaneleClock" width="100%">
<PanelStyle BorderStyle="Solid" ForeColor="Black" BorderWidth="1px" Font-Names="Arial">
<BorderDetails ColorTop="0, 45, 150" ColorLeft="158, 190, 245" ColorBottom="0, 45, 150" ColorRight="0, 45, 150"></BorderDetails>

<Padding Top="5px" Left="5px" Bottom="5px" Right="5px"></Padding>
</PanelStyle>

<Header TextAlignment="Left" Text="Reportes">
<ExpandedAppearance>
<Styles BackgroundImage="./images/GridTitulo.gif" BorderStyle="Ridge" ForeColor="Black" BorderWidth="1px" BorderColor="Transparent" Height="15px" Font-Size="9pt" Font-Names="Arial">
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
<BR /><asp:Image id="Image3" runat="server" ImageUrl="WF_Logos_imgreporte.aspx" Width="150px" Height="105px"></asp:Image><BR /><BR /><asp:Label id="Label6" runat="server" Text="Se Recomienda un Tamaño de 150 x 105 px" Font-Names="arial" ForeColor="Blue" Width="306px" Font-Size="Small"></asp:Label><BR />
    <asp:FileUpload ID="File3" runat="server" /><BR /><BR /><igtxt:webimagebutton id="btnsubir3" onclick="btnsubir3_Click" runat="server" width="150px" usebrowserdefaults="False" text="Subir Imagen" height="22px" ImageTextSpacing="4">
<Alignments VerticalImage="Bottom" HorizontalAll="NotSet" VerticalAll="NotSet"></Alignments>

<Appearance>
<Image Url="./Imagenes/panel-screenshot.png" Height="18px" Width="20px"></Image>

<Style Cursor="Default"></Style>
</Appearance>

<RoundedCorners HoverImageUrl="ig_butXP2wh.gif" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" MaxWidth="400" MaxHeight="80" ImageUrl="ig_butXP1wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
</igtxt:webimagebutton>
</Template>
</igmisc:webpanel>
                </td>
            </tr>
        </table>
        </asp:Content>