<%@ Register Assembly="Infragistics2.WebUI.UltraWebNavigator.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebNavigator" TagPrefix="ignav" %>
<%@ Register TagPrefix="uc1" TagName="WC_Menu" Src="WC_Menu.ascx" %>
<%@ Page language="c#" MasterPageFile="~/MasterPage.master"  CodeFile="WF_PerfilesPermisos.aspx.cs" AutoEventWireup="True" Inherits="eClock.WF_PerfilesPermisos" %>

<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register Assembly="Infragistics2.WebUI.WebCombo.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebCombo" TagPrefix="igcmbo" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
			<TABLE id="Table1" width ="100%"; height="100%">
				<TR>
					<TD style="WIDTH: 100%; HEIGHT: 0%">
						</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 100%; HEIGHT: 100%" vAlign="top" align="center">
						<TABLE id="Table2" style="HEIGHT: 100%" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<TR>
								<TD style="HEIGHT: 1%" width="100%" align="left">
                                    <em><span style="font-size: 10pt">
                                        <asp:Label ID="LNombre" runat="server" Font-Italic="True" Font-Names="Arial" Font-Size="Small"
                                            ForeColor="Black" Visible="False"></asp:Label></span></em></TD>
							</TR>
                            <tr>
                                <td align="left" style="height: 1%; text-align: center" width="100%">
                                    <igmisc:webpanel id="Webpanel2" runat="server" backcolor="White" bordercolor="SteelBlue"
                                        borderstyle="Outset" borderwidth="2px" font-bold="False" font-names="ARIAL" forecolor="White"
                                        stylesetname="PaneleClock">
                                        <PanelStyle BorderStyle="Solid" ForeColor="Black" BorderWidth="1px" Font-Names="Arial">
                                            <BorderDetails ColorBottom="0, 45, 150" ColorLeft="158, 190, 245" ColorRight="0, 45, 150"
                                                ColorTop="0, 45, 150" />
                                            <Padding Bottom="5px" Left="5px" Right="5px" Top="5px" />
                                        </PanelStyle>
                                        <Header TextAlignment="Left" Text="Datos del Perfil">
                                            <ExpandedAppearance>
                                                <Styles BackgroundImage="./images/GridTitulo.gif" BorderStyle="Ridge" ForeColor="White" BorderWidth="1px" BorderColor="Transparent" Height="15px" Font-Size="9pt" Font-Names="Arial" Font-Bold="True">
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
                                            <ExpansionIndicator Height="0px" Width="0px" />
                                        </Header>
                                        <Template>
                                            <table style="width: 279px">
                                                <tr>
                                                    <td style="text-align: left">
                                                        ID Perfil</td>
                                                    <td>
                                                        <igtxt:WebNumericEdit ID="txtPerfilID" runat="server" Enabled="False">
                                                        </igtxt:WebNumericEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        Nombre</td>
                                                    <td>
                                                        <igtxt:WebTextEdit ID="txtPerfilNombre" runat="server">
                                                        </igtxt:WebTextEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <asp:CheckBox ID="CBBorrado" runat="server" Text="Inactivo" /></td>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </table>
                                        </Template>
                                    </igmisc:WebPanel>
                                </td>
                            </tr>
							<TR>
								<TD vAlign="top" width="100%">
                                    <igmisc:WebAsyncRefreshPanel ID="WebAsyncRefreshPanel1" runat="server" Height=""
                                        Width="">
                                        <igmisc:webpanel id="Webpanel1" runat="server" backcolor="White" bordercolor="SteelBlue"
                                        borderstyle="Outset" borderwidth="2px" font-bold="False" font-names="ARIAL" forecolor="White"
                                        stylesetname="PaneleClock" width="100%">
<PanelStyle BorderStyle="Solid" ForeColor="Black" BorderWidth="1px" Font-Names="Arial">
<BorderDetails ColorTop="0, 45, 150" ColorLeft="158, 190, 245" ColorBottom="0, 45, 150" ColorRight="0, 45, 150"></BorderDetails>

<Padding Top="5px" Left="5px" Bottom="5px" Right="5px"></Padding>
</PanelStyle>

<Header TextAlignment="Left" Text="Seleccione los Permisos">
<ExpandedAppearance>
<Styles BackgroundImage="./images/GridTitulo.gif" BorderStyle="Ridge" ForeColor="White" BorderWidth="1px" BorderColor="Transparent" Height="15px" Font-Size="9pt" Font-Names="Arial" Font-Bold="True">
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
<ignav:UltraWebTree id="UltraWebTree1" runat="server" Font-Names="Arial Narrow"><Levels>
<ignav:Level Index="0"></ignav:Level>
<ignav:Level Index="1"></ignav:Level>
</Levels>

<SelectedNodeStyle ForeColor="White" BackColor="Navy"></SelectedNodeStyle>
</ignav:UltraWebTree>
</Template>
</igmisc:webpanel>
                                    </igmisc:WebAsyncRefreshPanel>
                                    &nbsp;</TD>
							</TR>
							<TR>
								<TD style="WIDTH: 715px; HEIGHT: 0%; text-align: left;"></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 100%; HEIGHT: 0%" align="center">
						<asp:Label id="LError" runat="server" ForeColor="#CC0033" Font-Size="Smaller" Font-Names="Arial"></asp:Label>
						<asp:Label id="LCorrecto" runat="server" ForeColor="#00C000" Font-Size="Smaller" Font-Names="Arial"></asp:Label></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 100%; HEIGHT: 0%" align="center">
                        <igtxt:WebImageButton ID="btnregresar" runat="server" Height="22px" OnClick="btnregresar_Click"
                            Text="Regresar" UseBrowserDefaults="False" Width="150px" ImageTextSpacing="4">
                            <Alignments VerticalImage="Middle" VerticalAll="Bottom" />
                            <Appearance>
                                <Image Url="./Imagenes/Back.png" Height="16px" Width="16px" />
                                <Style Cursor="Default"></Style>
                            </Appearance>
                            <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                                HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400"
                                PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                        </igtxt:WebImageButton>
                        &nbsp; &nbsp;&nbsp;
						<igtxt:WebImageButton id="BDeshacerCambios" runat="server" Width="150px" Height="22px" Text="Deshacer Cambios"
							UseBrowserDefaults="False" ImageTextSpacing="4">
							<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
							<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
								RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
							<Appearance>
								<Style Cursor="Default">
								</Style>
								<Image Url="./Imagenes/Undo.png" Height="16px" Width="16px"></Image>
							</Appearance>
						</igtxt:WebImageButton>&nbsp;&nbsp;&nbsp;&nbsp;
						<igtxt:WebImageButton id="BGuardarCambios" runat="server" Width="150px" Height="22px" Text="Guardar Cambios"
							UseBrowserDefaults="False" OnClick="BGuardarCambios_Click" ImageTextSpacing="4">
							<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
							<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
								RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
							<Appearance>
								<Style Cursor="Default">
								</Style>
								<Image Url="./Imagenes/Save_as.png" Height="16px" Width="16px"></Image>
							</Appearance>
						</igtxt:WebImageButton></TD>
				</TR>
			</TABLE>
</asp:Content>