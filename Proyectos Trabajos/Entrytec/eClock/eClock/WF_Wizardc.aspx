<%@ Page Language="C#" MasterPageFile="~/MasterPage2.master"  AutoEventWireup="true" CodeFile="WF_Wizardc.aspx.cs" Inherits="WF_Wizardc" %>

<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register Assembly="Infragistics2.WebUI.WebCombo.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebCombo" TagPrefix="igcmbo" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

        <igmisc:WebPanel ID="WebPanel1" runat="server" Height="170px" Width="500px" CssClass="igwpMainBlack2k7" BorderColor="SteelBlue" BorderStyle="Outset" Font-Names="Tahoma">
            <Template>
                <br />
                &nbsp;&nbsp;
                <br />
                <table>
                    <tr>
                        <td align="left" style="width: 580px">
                <asp:Label ID="Label1" runat="server" Height="55px" Text="En esta etapa se eligira el campo por el que se van a agrupar los Empleados."
                    Width="477px"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width: 580px" align="left">
                            <br />
                            <asp:Label ID="Label2" runat="server" Height="55px" Text="Campo" Width="477px"></asp:Label></td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 580px; height: 52px;">
                <igcmbo:WebCombo ID="WCGpo1" runat="server" BackColor="White" BorderColor="LightGray"
                    BorderStyle="Solid" BorderWidth="1px" 
                     
                     Font-Names="Arial Narrow"
                    Font-Size="9pt" ForeColor="Black" Height="22px" SelBackColor="DodgerBlue" Version="3.00"
                    Width="192px" SelForeColor="White">
                    <Rows>
                        <igtbl:UltraGridRow Height="">
                        </igtbl:UltraGridRow>
                        <igtbl:UltraGridRow Height="">
                        </igtbl:UltraGridRow>
                        <igtbl:UltraGridRow Height="">
                        </igtbl:UltraGridRow>
                        <igtbl:UltraGridRow Height="">
                        </igtbl:UltraGridRow>
                        <igtbl:UltraGridRow Height="">
                        </igtbl:UltraGridRow>
                    </Rows>
                    <Columns>
                        <igtbl:UltraGridColumn HeaderText="Column0">
                            <header caption="Column0"></header>
                        </igtbl:UltraGridColumn>
                    </Columns>
                    <DropDownLayout BorderCollapse="Separate" ColWidthDefault="200px" DropdownWidth="300px"
                        GridLines="None" RowHeightDefault="20px" RowSelectors="No" Version="3.00">
                        <RowStyle BackColor="CornflowerBlue" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
                            ForeColor="White">
                            <Padding Left="3px" />
                            <BorderDetails WidthLeft="0px" WidthTop="0px" />
                        </RowStyle>
 
                         
                        <HeaderStyle BackColor="Navy" BorderColor="Black" BorderStyle="Solid" 
                            Font-Bold="True" Font-Names="Arial" Font-Size="X-Small" ForeColor="White">
                            <BorderDetails ColorBottom="DarkGray" ColorLeft="173, 197, 235" ColorTop="173, 197, 235"
                                StyleBottom="Solid" StyleTop="Outset" WidthBottom="2px" WidthLeft="1px" WidthTop="1px" />
                        </HeaderStyle>
                        <RowAlternateStyle BackColor="WhiteSmoke" ForeColor="Black">
                        </RowAlternateStyle>
                        <FrameStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Cursor="Default"
                            Font-Names="Verdana" Font-Size="8pt" ForeColor="#759AFD" Height="130px" Width="300px">
                        </FrameStyle>
                        <SelectedRowStyle BackColor="DodgerBlue" ForeColor="White" />
                    </DropDownLayout>
                    <ExpandEffects ShadowColor="LightGray" />
                </igcmbo:WebCombo>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="width: 580px;">
                <igtxt:webimagebutton id="BDeshacerCambios" runat="server" height="22px" onclick="BDeshacerCambios_Click"
                    text="Datos Por Default" usebrowserdefaults="False" width="150px" ImageTextSpacing="4">
                            <Alignments VerticalImage="Middle" VerticalAll="Bottom"  />
                            <RoundedCorners HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif"
                                MaxHeight="80" MaxWidth="400" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"  />
                            <Appearance>
                                <Style Cursor="Default">
								</Style>
                                <Image Url="./Imagenes/Undo.png" Height="16px" Width="16px"  />
                            </Appearance>
                        </igtxt:webimagebutton>
                &nbsp; &nbsp;&nbsp;&nbsp;
                <igtxt:webimagebutton id="BGuardarCambios" runat="server" height="22px" onclick="BGuardarCambios_Click"
                    text="Siguiente" usebrowserdefaults="False" width="150px" ImageTextSpacing="4">
                            <Alignments VerticalImage="Middle" VerticalAll="Bottom"  />
                            <RoundedCorners HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif"
                                MaxHeight="80" MaxWidth="400" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"  />
                            <Appearance>
                                <Style Cursor="Default">
								</Style>
                                <Image Url="./Imagenes/Next.png" Height="16px" Width="16px"  />
                            </Appearance>
                        </igtxt:webimagebutton>
                        </td>
                    </tr>
                </table>
                <br />
            </Template>
            <PanelStyle CssClass="igwpPanelBlack2k7">
                <Padding Left="15px" />
            </PanelStyle>
            <Header Text="Configuracion de Agrupamiento">
                <ExpandedAppearance>
                    <Styles CssClass="igwpHeaderExpandedBlack2k7" BackColor="SteelBlue" Font-Bold="True" Font-Names="Tahoma" Font-Size="Small" ForeColor="White">
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
        </igmisc:WebPanel>
    </asp:Content>