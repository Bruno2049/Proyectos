<%@ Page Language="C#" MasterPageFile="~/MasterPage.master"  AutoEventWireup="true" CodeFile="WF_CostoComidas.aspx.cs" Inherits="WF_CostoComidas" %>

<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>

<%@ Register Src="WC_Menu.ascx" TagName="WC_Menu" TagPrefix="uc1" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 478px; height: 452px">
        <tr>
            <td style="height: 468px" valign="top">
                <br />
    <igmisc:webpanel id="WebPanel2" runat="server" backcolor="White" bordercolor="SteelBlue"
        borderstyle="Outset" borderwidth="2px" font-bold="False" font-names="ARIAL" forecolor="Black"
        stylesetname="PaneleClock" Height="215px" Width="384px">
<PanelStyle BorderStyle="Solid" ForeColor="Black" BorderWidth="1px" Font-Names="Arial">
<BorderDetails ColorTop="0, 45, 150" ColorLeft="158, 190, 245" ColorBottom="0, 45, 150" ColorRight="0, 45, 150"></BorderDetails>

<Padding Top="5px" Left="5px" Bottom="5px" Right="5px"></Padding>
</PanelStyle>

<Header Text="&lt;SPAN style=&quot;COLOR: white&quot;&gt;Costo de Comidas&lt;/SPAN&gt;">
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
<BR /><TABLE style="height: 142px"><TBODY><TR><TD style="TEXT-ALIGN: center; height: 158px;" vAlign=top><TABLE style="WIDTH: 368px; HEIGHT: 112px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="WIDTH: 207px; height: 5px;" align=left><SPAN style="FONT-FAMILY: Arial">Costo de la primera comida:</SPAN></TD><TD style="WIDTH: 93px; height: 5px;" align=left><igtxt:WebCurrencyEdit id="CostoComida1" runat="server" Width="100px" MinValue="0" MinDecimalPlaces="Two" MaxValue="1000" DataMode="Decimal">
                                </igtxt:WebCurrencyEdit> </TD></TR><TR><TD style="WIDTH: 207px" align=left><SPAN style="FONT-FAMILY: Arial">Costo de la segunda comida:</SPAN></TD><TD style="WIDTH: 93px" align=left><igtxt:WebCurrencyEdit id="CostoComida2" runat="server" Width="100px" MinValue="0" MinDecimalPlaces="Two" MaxValue="1000" DataMode="Decimal">
                                </igtxt:WebCurrencyEdit> </TD></TR></TBODY></TABLE><BR /><igtxt:WebImageButton id="BtnGuardar" onclick="BtnGuardar_Click" runat="server" Text="Guardar cambios" Width="160px" UseBrowserDefaults="False" Height="22px" ImageTextSpacing="4">
                        <RoundedCorners HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif"
                            MaxHeight="80" MaxWidth="400" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"  />
                                    <Appearance>
                                        <Style>
<Padding Top="4px"></Padding>
</Style>
                                        <Image Url="./Imagenes/Save_as.png" Height="16px" Width="16px" />
                                    </Appearance>
                                    <HoverAppearance>
                                        <InnerBorder ColorBottom="33, 27, 96" ColorLeft="33, 27, 96" ColorRight="33, 27, 96"
                                            ColorTop="33, 27, 96" StyleBottom="Solid" StyleLeft="Solid" StyleRight="Solid"
                                            StyleTop="Solid" WidthBottom="1px" WidthLeft="1px" WidthRight="1px" WidthTop="1px" />
                                        <Style BackColor="#F9DA9B"></Style>
                                    </HoverAppearance>
                                    <FocusAppearance>
                                        <InnerBorder ColorBottom="33, 27, 96" ColorLeft="33, 27, 96" ColorRight="33, 27, 96"
                                            ColorTop="33, 27, 96" StyleBottom="Solid" StyleLeft="Solid" StyleRight="Solid"
                                            StyleTop="Solid" WidthBottom="1px" WidthLeft="1px" WidthRight="1px" WidthTop="1px" />
                                        <Style BackColor="#FCE6AB"></Style>
                                    </FocusAppearance>
                                    <PressedAppearance>
                                        <InnerBorder ColorBottom="33, 27, 96" ColorLeft="33, 27, 96" ColorRight="33, 27, 96"
                                            ColorTop="33, 27, 96" StyleBottom="Solid" StyleLeft="Solid" StyleRight="Solid"
                                            StyleTop="Solid" WidthBottom="1px" WidthLeft="1px" WidthRight="1px" WidthTop="1px" />
                                        <Style BackColor="#F79646"></Style>
                                    </PressedAppearance>
                                    <Alignments VerticalAll="Bottom" VerticalImage="Middle" />
                    </igtxt:WebImageButton> <BR />
    <br />
    <asp:Label id="LblEstado" runat="server" Font-Names="Arial"></asp:Label></TD></TR></TBODY></TABLE>
</Template>
</igmisc:webpanel>
                <br />
                <br />
                <igmisc:WebPanel ID="Webpanel3" runat="server" BackColor="White" BorderColor="SteelBlue"
                    BorderStyle="Outset" BorderWidth="2px" Font-Bold="False" Font-Names="ARIAL" ForeColor="Black"
                    Height="117px" StyleSetName="PaneleClock" Width="84%">
                    <PanelStyle BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" ForeColor="Black">
                        <BorderDetails ColorBottom="0, 45, 150" ColorLeft="158, 190, 245" ColorRight="0, 45, 150"
                            ColorTop="0, 45, 150" />
                        <Padding Bottom="5px" Left="5px" Right="5px" Top="5px" />
                    </PanelStyle>
                    <Header Text="&lt;SPAN style=&quot;COLOR: white&quot;&gt;Comidas&lt;/SPAN&gt;" TextAlignment="Left">
                        <ExpandedAppearance>
                            <Styles BackgroundImage="./images/GridTitulo.gif" BorderColor="Transparent" BorderStyle="Ridge"
                                BorderWidth="1px" Font-Bold="True" Font-Names="Arial" Font-Size="9pt" ForeColor="Black"
                                Height="15px">
                                <BorderDetails ColorLeft="158, 190, 245" ColorRight="0, 45, 150" ColorTop="158, 190, 245"
                                    WidthBottom="0px" />
                                <Padding Bottom="1px" Left="4px" Top="1px" />
                            </Styles>
                        </ExpandedAppearance>
                        <HoverAppearance>
                            <Styles CssClass="igwpHeaderHoverBlue2k7">
                            </Styles>
                        </HoverAppearance>
                        <CollapsedAppearance>
                            <Styles CssClass="igwpHeaderCollapsedBlue2k7">
                            </Styles>
                        </CollapsedAppearance>
                        <ExpansionIndicator Height="10px" Width="10px" />
                    </Header>
                    <Template>
                        <table style="width: 367px">
                            <tr>
                                <td style="width: 1361px; height: 22px; text-align: left">
                                    Permitir Comer despues de su hora de comida:</td>
                                <td style="width: 181px; height: 22px">
                                    <asp:CheckBox ID="ChkDespuesComida" runat="server" Text="Si/No" /></td>
                            </tr>
                            <tr>
                                <td style="width: 1361px; text-align: left">
                                    Permitir credito:</td>
                                <td style="width: 181px">
                                    <asp:CheckBox ID="ChkCreditoComida" runat="server" Text="Si/No" /></td>
                            </tr>
                        </table>
                        <br />
                        <igtxt:WebImageButton ID="BtnGuardarComidas" runat="server" Height="22px" OnClick="BtnGuardarComidas_Click"
                            Text="Guardar cambios" UseBrowserDefaults="False" Width="160px" ImageTextSpacing="4">
                            <Appearance>
                                <Image Url="./Imagenes/Save_as.png" Height="16px" Width="16px" />
                                <Style>
<Padding Top="4px"></Padding>
</Style>
                            </Appearance>
                            <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                                HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400"
                                PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                            <Alignments VerticalAll="Bottom" VerticalImage="Middle" />
                        </igtxt:WebImageButton>
                    </Template>
                </igmisc:WebPanel>
                <br />
                <br />
                <igmisc:WebPanel ID="Webpanel1" runat="server" BackColor="White" BorderColor="SteelBlue"
                    BorderStyle="Outset" BorderWidth="2px" Font-Bold="False" Font-Names="ARIAL" ForeColor="Black"
                    Height="124px" StyleSetName="PaneleClock" Width="42%">
                    <PanelStyle BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" ForeColor="Black">
                        <BorderDetails ColorBottom="0, 45, 150" ColorLeft="158, 190, 245" ColorRight="0, 45, 150"
                            ColorTop="0, 45, 150" />
                        <Padding Bottom="5px" Left="5px" Right="5px" Top="5px" />
                    </PanelStyle>
                    <Header Text="&lt;SPAN style=&quot;COLOR: white&quot;&gt;Desayunos&lt;/SPAN&gt;"
                        TextAlignment="Left">
                        <ExpandedAppearance>
                            <Styles BackgroundImage="./images/GridTitulo.gif" BorderColor="Transparent" BorderStyle="Ridge"
                                BorderWidth="1px" Font-Bold="True" Font-Names="Arial" Font-Size="9pt" ForeColor="Black"
                                Height="15px">
                                <BorderDetails ColorLeft="158, 190, 245" ColorRight="0, 45, 150" ColorTop="158, 190, 245"
                                    WidthBottom="0px" />
                                <Padding Bottom="1px" Left="4px" Top="1px" />
                            </Styles>
                        </ExpandedAppearance>
                        <HoverAppearance>
                            <Styles CssClass="igwpHeaderHoverBlue2k7">
                            </Styles>
                        </HoverAppearance>
                        <CollapsedAppearance>
                            <Styles CssClass="igwpHeaderCollapsedBlue2k7">
                            </Styles>
                        </CollapsedAppearance>
                        <ExpansionIndicator Height="10px" Width="10px" />
                    </Header>
                    <Template>
                        <table style="width: 379px; height: 54px">
                            <tr>
                                <td style="width: 263px; text-align: left">
                                    Permitir Desayunar despues de su hora de entrada</td>
                                <td style="width: 39px; text-align: center">
                                    <asp:CheckBox ID="ChkDespuesEntrada" runat="server" Text="Si/No" /></td>
                            </tr>
                            <tr>
                                <td style="width: 263px; text-align: left">
                                    Permitir credito:</td>
                                <td style="width: 39px; text-align: center">
                                    <asp:CheckBox ID="ChkCreditoDesayuno" runat="server" Text="Si/No" /></td>
                            </tr>
                        </table>
                        <br />
                        <igtxt:WebImageButton ID="BtnGuardarDesayunos" runat="server" Height="22px" OnClick="BtnGuardarDesayunos_Click"
                            Text="Guadar Cambios" UseBrowserDefaults="False" Width="160px" ImageTextSpacing="4">
                            <Appearance>
                                <Image Url="./Imagenes/Save_as.png" Height="16px" Width="16px" />
                                <Style>
<Padding Top="4px"></Padding>
</Style>
                            </Appearance>
                            <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                                HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400"
                                PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                            <Alignments VerticalAll="Bottom" VerticalImage="Middle" />
                        </igtxt:WebImageButton>
                    </Template>
                </igmisc:WebPanel>
            </td>
        </tr>
    </table>
    &nbsp; &nbsp;<br />
    <br />
    <br />
    <br />
</asp:Content>