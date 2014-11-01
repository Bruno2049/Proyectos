<%@ Page Language="C#" MasterPageFile="~/MasterPage2.master"  AutoEventWireup="true" CodeFile="WF_Wizardd.aspx.cs" Inherits="WF_Wizardd" %>

<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register Assembly="Infragistics2.WebUI.WebCombo.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebCombo" TagPrefix="igcmbo" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <igmisc:WebPanel ID="WebPanel1" runat="server" CssClass="igwpMainBlack2k7" Height="190px"
            Width="500px" BorderColor="SteelBlue" BorderStyle="Outset" Font-Names="Tahoma">
            <PanelStyle CssClass="igwpPanelBlack2k7">
                <Padding Left="15px" />
            </PanelStyle>
            <Header Text="Cambio de Password">
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
                <br />
                &nbsp;&nbsp;
                <br />
                <table>
                    <tr>
                        <td align="left" colspan="2" style="height: 28px">
                <asp:Label ID="Label1" runat="server" Height="55px" Text="En esta etapa, por seguridad, debe cambiar el Password del Administrador del    Sistema."
                    Width="547px"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width: 384px; height: 28px;" align="left" valign="middle">
                            <asp:Label ID="Label2" runat="server" Text="Password: "></asp:Label></td>
                        <td style="width: 580px; height: 28px;" align="left">
                            <igtxt:webtextedit id="TxtPassword" runat="server" PasswordMode="True"></igtxt:webtextedit>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 384px" align="left">
                            <asp:Label ID="Label3" runat="server" Text="Confirme su Password: "></asp:Label></td>
                        <td align="left">
                            <igtxt:webtextedit id="TxtConfirmaPassword" runat="server" PasswordMode="True"></igtxt:webtextedit>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td align="center" style="width: 100%">
                            <br />
                            <asp:Label ID="LError" runat="server" Font-Size="Small" ForeColor="Red"></asp:Label><br />
                            <igtxt:webimagebutton id="BDeshacerCambios" runat="server" height="22px" onclick="BDeshacerCambios_Click"
                                text="Datos Por Default" usebrowserdefaults="False" width="150px" ImageTextSpacing="4">
                            <Alignments VerticalImage="Middle" VerticalAll="Bottom"   />
                            <RoundedCorners HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif"
                                MaxHeight="80" MaxWidth="400" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"   />
                            <Appearance>
                               
                                <Image Url="./Imagenes/Undo.png" Height="16px" Width="16px"   />
                            </Appearance>
                        </igtxt:webimagebutton>
                            &nbsp; &nbsp;
                            <igtxt:webimagebutton id="BGuardarCambios" runat="server" height="22px" onclick="BGuardarCambios_Click"
                                text="Siguiente" usebrowserdefaults="False" width="150px" ImageTextSpacing="4">
                            <Alignments VerticalImage="Middle" VerticalAll="Bottom"   />
                            <RoundedCorners HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif"
                                MaxHeight="80" MaxWidth="400" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"   />
                            <Appearance>
				
                                <Image Url="./Imagenes/Next.png" Height="16px" Width="16px"   />
                            </Appearance>
                        </igtxt:webimagebutton>
                        </td>
                    </tr>
                </table>
            </Template>
        </igmisc:WebPanel>
        </asp:Content>