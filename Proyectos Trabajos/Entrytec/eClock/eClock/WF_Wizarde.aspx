<%@ Page Language="C#" MasterPageFile="~/MasterPage2.master"  AutoEventWireup="true" CodeFile="WF_Wizarde.aspx.cs" Inherits="WF_Wizarde" %>

<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 320px; height: 62px">
        <tr>
            <td>
        <igmisc:WebPanel ID="WebPanel1" runat="server" CssClass="igwpMainBlack2k7"
            StyleSetName="" Width="200px" BorderColor="SteelBlue" BorderStyle="Outset" Font-Names="Tahoma">
            <PanelStyle CssClass="igwpPanelBlack2k7">
                <Padding Left="15px" />
            </PanelStyle>
            <Header Text="Campos de Imagen">
                <ExpandedAppearance>
                    <Styles CssClass="igwpHeaderExpandedBlack2k7" BackColor="SteelBlue" BorderColor="SteelBlue" Font-Bold="True" Font-Names="Tahoma" Font-Size="Small" ForeColor="White">
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
                <table style="width: 94px">
                    <tr>
                        <td align="left" style="width: 104px">
                                                <asp:CheckBox ID="CBFotografia" runat="server" AutoPostBack="false" type="checkbox" Text="Fotografía" /></td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 104px; height: 22px">
                                                <asp:CheckBox ID="CBFirma" runat="server" AutoPostBack="false" type="checkbox" Text="Firma" /></td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 104px">
                                                <asp:CheckBox ID="CBHuella" runat="server" AutoPostBack="false" type="checkbox" Text="Huella" /></td>
                    </tr>
                </table>
            </Template>
        </igmisc:WebPanel>
            </td>
        </tr>
        <tr>
            <td style="height: 15px">
                <asp:Label ID="LError" runat="server" Font-Names="Arial Narrow" Font-Size="Smaller"
                    ForeColor="Red"></asp:Label><asp:Label ID="LCorrecto" runat="server" Font-Names="Arial Narrow"
                        Font-Size="Smaller" ForeColor="Green"></asp:Label></td>
        </tr>
        <tr>
            <td>
                            <igtxt:WebImageButton ID="BDeshacerCambios" runat="server" Height="22px" OnClick="BDeshacerCambios_Click"
                                Text="Inicio" UseBrowserDefaults="False" ImageTextSpacing="4">
                                <Alignments VerticalImage="Middle" VerticalAll="Bottom" />
                                <RoundedCorners HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif"
                                    MaxHeight="80" MaxWidth="400" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif" />
                                <Appearance>
                                    <Style Cursor="Default">
								</Style>
                                    <Image Url="./Imagenes/Home.png" Height="16px" Width="16px" />
                                </Appearance>
                            </igtxt:WebImageButton>
                            &nbsp;&nbsp;&nbsp;<igtxt:WebImageButton ID="BGuardarCambios" runat="server" Height="22px" OnClick="BGuardarCambios_Click"
                                Text="Guardar Cambios" UseBrowserDefaults="False" ImageTextSpacing="4">
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