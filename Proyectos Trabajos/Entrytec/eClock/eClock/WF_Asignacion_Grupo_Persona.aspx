<%@ Page language="c#" CodeFile="WF_Asignacion_Grupo_Persona.aspx.cs" AutoEventWireup="false" Inherits="eClock.WF_Asignacion_Grupo_Persona" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics2.WebUI.WebCombo.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebCombo" TagPrefix="igcmbo" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register TagPrefix="uc1" TagName="WC_Menu" Src="WC_Menu.ascx" %>
<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Importar Empleados</title>
</head>
<body style="font-size: 11px; font-family: tahoma; text-align: center; margin: 0px; ">
    <form id="form1" runat="server" >
    <div>
        <table width="600" style="height: 380px">
				<TR>
					<TD style="WIDTH: 100%; HEIGHT: 100%" align="center">
						<TABLE id="Table5" style="WIDTH: 100%; HEIGHT: 100%" cellSpacing="1" cellPadding="1" width="412"
							border="0">
							<TR>
								<TD style="width: 550px; height: 350px" valign="top">
									<igtbl:ultrawebgrid id="Grid" runat="server" ImageDirectory="/ig_common/Images/" Height="100%" Width="100%"
										UseAccessibleHeader="False" OnInitializeLayout="Grid_InitializeLayout" style="left: 1px; top: 0px">
										<DisplayLayout ColWidthDefault="" AllowSortingDefault="OnClient" RowHeightDefault="20px" Version="3.00"
											GridLinesDefault="Horizontal" SelectTypeRowDefault="Extended" ViewType="Hierarchical" HeaderClickActionDefault="SortSingle"
											BorderCollapseDefault="Separate" AllowColSizingDefault="Free" CellPaddingDefault="4" RowSelectorsDefault="No"
											Name="Grid" TableLayout="Fixed" CellClickActionDefault="RowSelect">
											<Pager PageSize="20">
											</Pager>
											<HeaderStyleDefault BackgroundImage="images/GridTitulo.gif" Font-Size="X-Small" Font-Names="Arial" Font-Bold="True" BorderColor="Black"
												HorizontalAlign="Left" ForeColor="White" BackColor="Navy" >
												<BorderDetails WidthLeft="0px" WidthTop="0px" WidthRight="1px" WidthBottom="1px"></BorderDetails>
											</HeaderStyleDefault>
											<FrameStyle Width="100%" Font-Names="Arial Narrow" BorderColor="Black" ForeColor="#759AFD" Height="100%"></FrameStyle>
											<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
												<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
											</FooterStyleDefault>
											<ActivationObject BorderStyle="Dotted" BorderColor="1, 68, 208" BorderWidth=""></ActivationObject>
											<GroupByBox Prompt="Arrastre la columna que desea agrupar...">

											</GroupByBox>
											<SelectedRowStyleDefault BackgroundImage="images/Office2003SelRow.png" ForeColor="WhiteSmoke" BackColor="Sienna" >
												<BorderDetails WidthLeft="0px" StyleBottom="Solid" ColorBottom="Black" WidthTop="0px" WidthRight="0px"
													StyleTop="None" StyleRight="None" WidthBottom="1px" StyleLeft="None"></BorderDetails>
											</SelectedRowStyleDefault>
											<RowAlternateStyleDefault ForeColor="Black" BackColor="WhiteSmoke"></RowAlternateStyleDefault>
											<RowStyleDefault BorderColor="Black" ForeColor="White" BackColor="CornflowerBlue" Font-Names="Verdana" Font-Size="8pt">
												<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
											</RowStyleDefault>
                                            <Images>
                                                <CollapseImage Url="ig_tblcrm_rowarrow_down.gif" />
                                                <ExpandImage Url="ig_tblcrm_rowarrow_right.gif" />
                                            </Images>
										</DisplayLayout>
										<Bands>
											<igtbl:UltraGridBand>
												
                                                <AddNewRow View="NotSet" Visible="NotSet">
                                                </AddNewRow>
											</igtbl:UltraGridBand>
										</Bands>
									</igtbl:ultrawebgrid></TD>
                                <td style="width: 30px" valign="top">
                                </td>
								<TD valign="top">
									<TABLE id="Table3" style="WIDTH: 100%;" cellSpacing="1" cellPadding="1" width="304"
										border="0">
										<TR>
											<TD align="center" valign="top">
                                                <igmisc:WebPanel ID="WebPanel1" runat="server" BackColor="White" BorderColor="SteelBlue"
                                                    BorderStyle="Outset" BorderWidth="2px" Font-Bold="True" Font-Names="ARIAL" ForeColor="Black" StyleSetName="PaneleClock" Width="100%">
                                                    <PanelStyle BorderStyle="Solid" BorderWidth="1px">
                                                        <BorderDetails ColorBottom="0, 45, 150" ColorLeft="158, 190, 245" ColorRight="0, 45, 150"
                                                            ColorTop="0, 45, 150" />
                                                        <Padding Bottom="5px" Left="5px" Right="5px" Top="5px" />
                                                    </PanelStyle>
                                                    <Header TextAlignment="Left">
                                                        <ExpandedAppearance>
                                                            <Styles BackgroundImage="./images/GridTitulo.gif" BorderColor="Transparent" BorderStyle="Ridge"
                                                                BorderWidth="1px" Font-Names="Arial" Font-Size="9pt" Height="15px">
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
                                                        <ExpansionIndicator CollapsedImageUrl="./skins/panel-bg-bl2.gif" ExpandedImageUrl="./skins/panel-bg-bl2.gif" />
                                                    </Header>
                                                    <Template>
												<asp:Panel id="Panel2" runat="server" BorderWidth="1px" BorderColor="Highlight">&nbsp; 
                  <TABLE id="Table2" style="WIDTH: 277px;" cellSpacing="1" cellPadding="1" width="277"
														border="0">
														<TR>
															<TD style="WIDTH: 102px; HEIGHT: 16px" colSpan="2" align="left">
																<asp:Label id="Label5" runat="server" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow">Asignación</asp:Label></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 102px; HEIGHT: 16px" colSpan="2" align="left">
																<P>
																	<asp:Label id="Label1" runat="server" Font-Size="Small" Font-Names="Arial Narrow" Width="113px">Asignar Empleado(a)</asp:Label></P>
															</TD>
														</TR>
														<TR>
															<TD style="WIDTH: 102px; HEIGHT: 16px" align="left">
																<P>
																	<asp:Label id="Label3" runat="server" Font-Size="Small" Font-Names="Arial Narrow">No. Empleado</asp:Label></P>
															</TD>
															<TD style="HEIGHT: 16px" align="left">
																<P>
																	<asp:TextBox id="NoEmpleado1" runat="server" Width="112px"></asp:TextBox></P>
															</TD>
														</TR>
														<TR>
															<TD style="WIDTH: 102px; HEIGHT: 29px" align="left">
																<P align="center">
																	<asp:Label id="Label2" runat="server" Font-Size="Small" Font-Names="Arial Narrow">a</asp:Label></P>
															</TD>
															<TD style="HEIGHT: 29px" align="left">
																<P>
																	<asp:DropDownList id="ComboGrupo1" runat="server" Width="168px"></asp:DropDownList>&nbsp;</P>
															</TD>
														</TR>
														<TR>
															<TD style="WIDTH: 102px" align="left">
																<P>
																	<igtxt:webimagebutton id="BAsignacionGrupo" runat="server" Width="100px" Height="22px" Text="Asignar"
																		UseBrowserDefaults="False" ImageTextSpacing="4">
																		<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
																		<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
																			RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
																		<Appearance> 

																			<Image Url="./Imagenes/Assign.png" Height="16px" Width="16px"></Image>
                                                                            <Style>
<Padding Top="4px"></Padding>
</Style>
																		</Appearance>
																	</igtxt:webimagebutton>&nbsp;</P>
															</TD>
															<TD align="left">
																<P align="right">
                                                                    &nbsp;</P>
															</TD>
														</TR>
													</TABLE></asp:Panel>
                                                    </Template>
                                                </igmisc:WebPanel>
											</TD>
										</TR>
										<TR>
											<TD align="center" valign="top">
                                                <igmisc:WebPanel ID="WebPanel3" runat="server" BackColor="White" BorderColor="SteelBlue"
                                                    BorderStyle="Outset" BorderWidth="2px" Font-Bold="True" Font-Names="ARIAL" ForeColor="Black" StyleSetName="PaneleClock" Width="100%">
                                                    <PanelStyle BorderStyle="Solid" BorderWidth="1px">
                                                        <BorderDetails ColorBottom="0, 45, 150" ColorLeft="158, 190, 245" ColorRight="0, 45, 150"
                                                            ColorTop="0, 45, 150" />
                                                        <Padding Bottom="5px" Left="5px" Right="5px" Top="5px" />
                                                    </PanelStyle>
                                                    <Header TextAlignment="Left">
                                                        <ExpandedAppearance>
                                                            <Styles BackgroundImage="./images/GridTitulo.gif" BorderColor="Transparent" BorderStyle="Ridge"
                                                                BorderWidth="1px" Font-Names="Arial" Font-Size="9pt" Height="15px">
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
                                                        <ExpansionIndicator CollapsedImageUrl="./skins/panel-bg-bl2.gif" ExpandedImageUrl="./skins/panel-bg-bl2.gif" />
                                                    </Header>
                                                    <Template>
												<asp:Panel id="Panel1" runat="server" BorderWidth="1px" BorderColor="Highlight">
													<TABLE id="Table7" style="WIDTH: 272px;" cellSpacing="1" cellPadding="1"
														width="272" border="0">
														<TR>
															<TD style="WIDTH: 165px" vAlign="bottom" colSpan="3" align="left">
																<asp:Label id="Label6" runat="server" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow">Mover Empleados </asp:Label></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 165px" vAlign="bottom" colSpan="3" align="left"></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 165px" vAlign="bottom" colSpan="3" align="left">
																<asp:Label id="Label4" runat="server" Font-Size="Small" Font-Names="Arial Narrow" Width="195px">Mover Empleados Seleccionados</asp:Label></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 165px; HEIGHT: 66px" align="left" colSpan="3">
																<asp:DropDownList id="ComboGrupo2" runat="server" Width="168px"></asp:DropDownList></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 165px" align="left" colSpan="3">
																<igtxt:webimagebutton id="Moverempleados" runat="server" Width="100px" Height="22px" Text="Mover" UseBrowserDefaults="False" ImageTextSpacing="4">
																	<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
																	<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
																		RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
																	<Appearance>

																		<Image Url="./Imagenes/Assign.png" Height="16px" Width="16px"></Image>
                                                                        <Style>
<Padding Top="4px"></Padding>
</Style>
																	</Appearance>
																</igtxt:webimagebutton></TD>
														</TR>
													</TABLE>
												</asp:Panel>
                                                    </Template>
                                                </igmisc:WebPanel>
											</TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 100%; HEIGHT: 0%" align="center">
						<asp:label id="LError" runat="server" ForeColor="Red" Font-Names="Arial Narrow" Font-Size="Smaller"></asp:label>
						<asp:label id="LCorrecto" runat="server" ForeColor="Green" Font-Names="Arial Narrow" Font-Size="Smaller"></asp:label></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 100%; HEIGHT: 0%"></TD>
				</TR>
			</TABLE>
    		
   </div>
    </form>
</body>
</html>