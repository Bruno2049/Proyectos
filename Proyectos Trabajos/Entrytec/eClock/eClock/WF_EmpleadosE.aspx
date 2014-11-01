<%@ Page language="c#" MasterPageFile="~/MasterPage.master"  CodeFile="WF_EmpleadosE.aspx.cs" AutoEventWireup="True" Inherits="eClock.WF_EmpleadosE" %>
<%@ Register TagPrefix="uc1" TagName="WC_Menu" Src="WC_Menu.ascx" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics2.WebUI.WebCombo.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebCombo" TagPrefix="igcmbo" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
			<TABLE id="Table1" style="LEFT: 0px; WIDTH: 100%; POSITION: absolute; TOP: 0px; HEIGHT: 100%"
				height="100%" cellSpacing="1" cellPadding="1" width="100%" border="0">
				<TR>
					<TD style="HEIGHT: 0.01%"><uc1:wc_menu id="WC_Menu1" runat="server"></uc1:wc_menu></TD>
				</TR>
				<TR>
					<TD style="BACKGROUND-POSITION: right center; BACKGROUND-IMAGE: url(./Imagenes/fondoeClock3.jpg); BACKGROUND-REPEAT: no-repeat"
						align="center"><asp:panel id="Panel2" runat="server">
							<TABLE id="Table3" cellSpacing="1" cellPadding="1" width="300" border="0">
								<TR>
									<TD>
										<TABLE id="Table2" style="WIDTH: 410px; HEIGHT: 371px" cellSpacing="1" cellPadding="1"
											width="410" border="0">
											<TR>
												<TD style="FONT-SIZE: 11pt; WIDTH: 179px; FONT-FAMILY: Arial">No. Empleado</TD>
												<TD style="WIDTH: 210px">
													<igtxt:WebNumericEdit id="Tracve" runat="server" Font-Names="Arial Narrow" CellSpacing="1" UseBrowserDefaults="False"
														BorderStyle="Solid" BorderWidth="1px" BorderColor="#7F9DB9" Width="200px">
														<ButtonsAppearance CustomButtonDefaultTriangleImages="Arrow">
															<ButtonPressedStyle BackColor="#83A6F4"></ButtonPressedStyle>
															<ButtonDisabledStyle BorderColor="#D7D7D7" ForeColor="#BEBEBE" BackColor="#E1E1DD"></ButtonDisabledStyle>
															<ButtonStyle Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" ForeColor="#506080"
																BackColor="#C5D5FC"></ButtonStyle>
															<ButtonHoverStyle BackColor="#DCEDFD"></ButtonHoverStyle>
														</ButtonsAppearance>
														<SpinButtons DefaultTriangleImages="ArrowSmall" Width="15px"></SpinButtons>
													</igtxt:WebNumericEdit>
													<asp:RequiredFieldValidator id="RVTracve" runat="server" Font-Names="Arial Narrow" Font-Size="Smaller" ControlToValidate="Tracve"
														ErrorMessage="La clave es un dato obligatorio"></asp:RequiredFieldValidator><FONT face="Arial"></FONT></TD>
											</TR>
											<TR>
												<TD style="FONT-SIZE: 11pt; WIDTH: 179px; FONT-FAMILY: Arial">Nombre</TD>
												<TD style="WIDTH: 210px">
													<igtxt:WebTextEdit id="Tranom" runat="server" Font-Names="Arial Narrow" CellSpacing="1" UseBrowserDefaults="False"
														BorderStyle="Solid" BorderWidth="1px" BorderColor="#7F9DB9" Width="200px" MaxLength="60">
														<ButtonsAppearance CustomButtonDefaultTriangleImages="Arrow">
															<ButtonPressedStyle BackColor="#83A6F4"></ButtonPressedStyle>
															<ButtonDisabledStyle BorderColor="#D7D7D7" ForeColor="#BEBEBE" BackColor="#E1E1DD"></ButtonDisabledStyle>
															<ButtonStyle Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" ForeColor="#506080"
																BackColor="#C5D5FC"></ButtonStyle>
															<ButtonHoverStyle BackColor="#DCEDFD"></ButtonHoverStyle>
														</ButtonsAppearance>
													</igtxt:WebTextEdit>
													<asp:RequiredFieldValidator id="RVTranom" runat="server" Font-Names="Arial Narrow" Font-Size="Smaller" ControlToValidate="Tranom"
														ErrorMessage="Nombre de empleado es un dato obligatorio" InitialValue="0"></asp:RequiredFieldValidator><FONT face="Arial"></FONT></TD>
											</TR>
											<TR>
												<TD style="FONT-SIZE: 11pt; WIDTH: 179px; FONT-FAMILY: Arial; HEIGHT: 12px">Area</TD>
												<TD style="WIDTH: 210px; HEIGHT: 12px"><FONT face="Arial">
														<igcmbo:webcombo id="DatareWC" runat="server" Font-Names="Arial Narrow" BorderStyle="Solid" BorderWidth="1px"
															BorderColor="LightGray" Width="200px" Editable="True" ForeColor="Black" BackColor="White" Height="22px"
															Version="3.00"  
															SelBackColor="17, 69, 158"   OnInitializeLayout="DatareWC_InitializeLayout">
															<DropDownLayout BorderCollapse="Separate" RowHeightDefault="20px" Version="3.00">
																<RowStyle BorderWidth="1px" BorderColor="Black" BorderStyle="Solid" ForeColor="White" BackColor="CornflowerBlue">
																	<Padding Left="3px"></Padding>
																	<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
																</RowStyle>
																<SelectedRowStyle ForeColor="WhiteSmoke" BackColor="Sienna" ></SelectedRowStyle>
																<HeaderStyle Font-Size="X-Small" Font-Names="Arial" Font-Bold="True" BorderColor="Black" BorderStyle="Solid"
																	ForeColor="White" BackColor="Navy" >
																	<BorderDetails WidthLeft="0px" WidthTop="0px" WidthRight="1px" WidthBottom="1px"></BorderDetails>
																</HeaderStyle>
																<RowAlternateStyle ForeColor="Black" BackColor="WhiteSmoke"></RowAlternateStyle>
																<FrameStyle Width="325px" Cursor="Default" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana"
																	BorderColor="Black" BorderStyle="Solid" ForeColor="#759AFD" Height="130px"></FrameStyle>
															</DropDownLayout>
															<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
                                                            <Columns>
                                                                <igtbl:UltraGridColumn HeaderText="Column0">
                                                                    <header caption="Column0"></header>
                                                                </igtbl:UltraGridColumn>
                                                            </Columns>
														</igcmbo:webcombo></FONT></TD>
											</TR>
											<TR>
												<TD style="FONT-SIZE: 11pt; WIDTH: 179px; FONT-FAMILY: Arial; HEIGHT: 6px">Departamento</TD>
												<TD style="WIDTH: 210px; HEIGHT: 6px"><FONT face="Arial">
														<igcmbo:webcombo id="DatdepWC" runat="server" Font-Names="Arial Narrow" BorderStyle="Solid" BorderWidth="1px"
															BorderColor="LightGray" Width="200px" Editable="True" ForeColor="Black" BackColor="White" Height="22px"
															Version="3.00"  
															SelBackColor="17, 69, 158"   OnInitializeLayout="DatdepWC_InitializeLayout">
															<DropDownLayout BorderCollapse="Separate" RowHeightDefault="20px" Version="3.00">
																<RowStyle BorderWidth="1px" BorderColor="Black" BorderStyle="Solid" ForeColor="White" BackColor="CornflowerBlue">
																	<Padding Left="3px"></Padding>
																	<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
																</RowStyle>
																<SelectedRowStyle ForeColor="WhiteSmoke" BackColor="Sienna" ></SelectedRowStyle>
																<HeaderStyle Font-Size="X-Small" Font-Names="Arial" Font-Bold="True" BorderColor="Black" BorderStyle="Solid"
																	ForeColor="White" BackColor="Navy" >
																	<BorderDetails WidthLeft="0px" WidthTop="0px" WidthRight="1px" WidthBottom="1px"></BorderDetails>
																</HeaderStyle>
																<RowAlternateStyle ForeColor="Black" BackColor="WhiteSmoke"></RowAlternateStyle>
																<FrameStyle Width="325px" Cursor="Default" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana"
																	BorderColor="Black" BorderStyle="Solid" ForeColor="#759AFD" Height="130px" BackColor="White"></FrameStyle>
															</DropDownLayout>
															<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
                                                            <Columns>
                                                                <igtbl:UltraGridColumn HeaderText="Column0">
                                                                    <header caption="Column0"></header>
                                                                </igtbl:UltraGridColumn>
                                                            </Columns>
														</igcmbo:webcombo></FONT></TD>
											</TR>
											<TR>
												<TD style="FONT-SIZE: 11pt; WIDTH: 179px; FONT-FAMILY: Arial; HEIGHT: 18px">Centro 
													de Costos</TD>
												<TD style="WIDTH: 210px; HEIGHT: 18px"><FONT face="Arial">
														<igcmbo:webcombo id="DatcctWC" runat="server" Font-Names="Arial Narrow" BorderStyle="Solid" BorderWidth="1px"
															BorderColor="LightGray" Width="200px" ForeColor="Black" BackColor="White" Height="22px" Version="3.00"
															 
															SelBackColor="17, 69, 158"   OnInitializeLayout="DatcctWC_InitializeLayout">
															<DropDownLayout BorderCollapse="Separate" RowHeightDefault="20px" Version="3.00">
																<RowStyle BorderWidth="1px" BorderColor="Black" BorderStyle="Solid" ForeColor="White" BackColor="CornflowerBlue">
																	<Padding Left="3px"></Padding>
																	<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
																</RowStyle>
																<SelectedRowStyle ForeColor="WhiteSmoke" BackColor="Sienna" ></SelectedRowStyle>
																<HeaderStyle Font-Size="X-Small" Font-Names="Arial" Font-Bold="True" BorderColor="Black" BorderStyle="Solid"
																	ForeColor="White" BackColor="Navy" >
																	<BorderDetails WidthLeft="0px" WidthTop="0px" WidthRight="1px" WidthBottom="1px"></BorderDetails>
																</HeaderStyle>
																<RowAlternateStyle ForeColor="Black" BackColor="WhiteSmoke"></RowAlternateStyle>
																<FrameStyle Width="325px" Cursor="Default" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana"
																	BorderColor="Black" BorderStyle="Solid" ForeColor="#759AFD" Height="130px"></FrameStyle>
															</DropDownLayout>
															<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
                                                            <Columns>
                                                                <igtbl:UltraGridColumn HeaderText="Column0">
                                                                    <header caption="Column0"></header>
                                                                </igtbl:UltraGridColumn>
                                                            </Columns>
														</igcmbo:webcombo></FONT></TD>
											</TR>
											<TR>
												<TD style="FONT-SIZE: 11pt; WIDTH: 179px; FONT-FAMILY: Arial; HEIGHT: 10px">Tipo de 
													Nomina</TD>
												<TD style="WIDTH: 210px; HEIGHT: 10px"><FONT face="Arial">
														<igcmbo:webcombo id="CnocveWC" runat="server" Font-Names="Arial Narrow" BorderStyle="Solid" BorderWidth="1px"
															BorderColor="LightGray" Width="200px" Editable="True" ForeColor="Black" BackColor="White" Height="22px"
															Version="3.00"  
															SelBackColor="17, 69, 158"   OnInitializeLayout="CnocveWC_InitializeLayout">
															<DropDownLayout BorderCollapse="Separate" RowHeightDefault="20px" Version="3.00">
																<RowStyle BorderWidth="1px" BorderColor="Black" BorderStyle="Solid" ForeColor="White" BackColor="CornflowerBlue">
																	<Padding Left="3px"></Padding>
																	<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
																</RowStyle>
																<SelectedRowStyle ForeColor="WhiteSmoke" BackColor="Sienna" ></SelectedRowStyle>
																<HeaderStyle Font-Size="X-Small" Font-Names="Arial" Font-Bold="True" BorderColor="Black" BorderStyle="Solid"
																	ForeColor="White" BackColor="Navy" >
																	<BorderDetails WidthLeft="0px" WidthTop="0px" WidthRight="1px" WidthBottom="1px"></BorderDetails>
																</HeaderStyle>
																<RowAlternateStyle ForeColor="Black" BackColor="WhiteSmoke"></RowAlternateStyle>
																<FrameStyle Width="325px" Cursor="Default" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana"
																	BorderColor="Black" BorderStyle="Solid" ForeColor="#759AFD" Height="130px"></FrameStyle>
															</DropDownLayout>
															<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
                                                            <Columns>
                                                                <igtbl:UltraGridColumn HeaderText="Column0">
                                                                    <header caption="Column0"></header>
                                                                </igtbl:UltraGridColumn>
                                                            </Columns>
														</igcmbo:webcombo></FONT></TD>
											</TR>
											<TR>
												<TD style="FONT-SIZE: 11pt; WIDTH: 179px; FONT-FAMILY: Arial">Compañia</TD>
												<TD style="WIDTH: 210px"><FONT face="Arial">
														<igcmbo:webcombo id="CiacveWC" runat="server" Font-Names="Arial Narrow" BorderStyle="Solid" BorderWidth="1px"
															BorderColor="LightGray" Width="200px" Editable="True" ForeColor="Black" BackColor="White" Height="22px"
															Version="3.00"  
															SelBackColor="17, 69, 158"   OnInitializeLayout="CiacveWC_InitializeLayout">
															<DropDownLayout BorderCollapse="Separate" RowHeightDefault="20px" Version="3.00">
																<RowStyle BorderWidth="1px" BorderColor="Black" BorderStyle="Solid" ForeColor="White" BackColor="CornflowerBlue">
																	<Padding Left="3px"></Padding>
																	<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
																</RowStyle>
																<SelectedRowStyle ForeColor="WhiteSmoke" BackColor="Sienna" ></SelectedRowStyle>
																<HeaderStyle Font-Size="X-Small" Font-Names="Arial" Font-Bold="True" BorderColor="Black" BorderStyle="Solid"
																	ForeColor="White" BackColor="Navy" >
																	<BorderDetails WidthLeft="0px" WidthTop="0px" WidthRight="1px" WidthBottom="1px"></BorderDetails>
																</HeaderStyle>
																<RowAlternateStyle ForeColor="Black" BackColor="WhiteSmoke"></RowAlternateStyle>
																<FrameStyle Width="325px" Cursor="Default" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana"
																	BorderColor="Black" BorderStyle="Solid" ForeColor="#759AFD" Height="130px"></FrameStyle>
															</DropDownLayout>
															<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
                                                            <Columns>
                                                                <igtbl:UltraGridColumn HeaderText="Column0">
                                                                    <header caption="Column0"></header>
                                                                </igtbl:UltraGridColumn>
                                                            </Columns>
														</igcmbo:webcombo></FONT></TD>
											</TR>
											<TR>
												<TD style="FONT-SIZE: 11pt; WIDTH: 179px; FONT-FAMILY: Arial">Numero de Tarjeta 
													(Serie)</TD>
												<TD style="WIDTH: 210px">
													<igtxt:WebTextEdit id="Ns" runat="server" Font-Names="Arial Narrow" CellSpacing="1" UseBrowserDefaults="False"
														BorderStyle="Solid" BorderWidth="1px" BorderColor="#7F9DB9" Width="200px" MaxLength="32">
														<ButtonsAppearance CustomButtonDefaultTriangleImages="Arrow">
															<ButtonPressedStyle BackColor="#83A6F4"></ButtonPressedStyle>
															<ButtonDisabledStyle BorderColor="#D7D7D7" ForeColor="#BEBEBE" BackColor="#E1E1DD"></ButtonDisabledStyle>
															<ButtonStyle Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" ForeColor="#506080"
																BackColor="#C5D5FC"></ButtonStyle>
															<ButtonHoverStyle BackColor="#DCEDFD"></ButtonHoverStyle>
														</ButtonsAppearance>
													</igtxt:WebTextEdit><FONT face="Arial"></FONT></TD>
											</TR>
											<TR>
												<TD style="FONT-SIZE: 11pt; WIDTH: 179px; FONT-FAMILY: Arial; HEIGHT: 24px">Grupo</TD>
												<TD style="WIDTH: 210px; HEIGHT: 24px"><FONT face="Arial">
														<igcmbo:webcombo id="GrupoWC" runat="server" Font-Names="Arial Narrow" BorderStyle="Solid" BorderWidth="1px"
															BorderColor="LightGray" Width="200px" ForeColor="Black" BackColor="White" Height="22px" Version="3.00"
															 
															SelBackColor="17, 69, 158"   OnInitializeLayout="GrupoWC_InitializeLayout">
															<DropDownLayout BorderCollapse="Separate" RowHeightDefault="20px" Version="3.00">
																<RowStyle BorderWidth="1px" BorderColor="Black" BorderStyle="Solid" ForeColor="White" BackColor="CornflowerBlue">
																	<Padding Left="3px"></Padding>
																	<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
																</RowStyle>
																<SelectedRowStyle ForeColor="WhiteSmoke" BackColor="Sienna" ></SelectedRowStyle>
																<HeaderStyle Font-Size="X-Small" Font-Names="Arial" Font-Bold="True" BorderColor="Black" BorderStyle="Solid"
																	ForeColor="White" BackColor="Navy" >
																	<BorderDetails WidthLeft="0px" WidthTop="0px" WidthRight="1px" WidthBottom="1px"></BorderDetails>
																</HeaderStyle>
																<RowAlternateStyle ForeColor="Black" BackColor="WhiteSmoke"></RowAlternateStyle>
																<FrameStyle Width="325px" Cursor="Default" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana"
																	BorderColor="Black" BorderStyle="Solid" ForeColor="#759AFD" Height="130px"></FrameStyle>
															</DropDownLayout>
															<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
                                                            <Columns>
                                                                <igtbl:UltraGridColumn HeaderText="Column0">
                                                                    <header caption="Column0"></header>
                                                                </igtbl:UltraGridColumn>
                                                            </Columns>
														</igcmbo:webcombo></FONT></TD>
											</TR>
											<TR>
												<TD style="FONT-SIZE: 11pt; WIDTH: 179px; FONT-FAMILY: Arial; HEIGHT: 19px">Horario</TD>
												<TD style="WIDTH: 210px; HEIGHT: 19px">
													<igcmbo:WebCombo id=CBTurno runat="server" Font-Names="Arial Narrow" BorderStyle="Solid" BorderWidth="1px" BorderColor="LightGray" Font-Size="9pt" ForeColor="Black" BackColor="White" Version="3.00"   SelBackColor="17, 69, 158"   DataValueField="TURNO_ID" DataTextField="TURNO_NOMBRE" DataSource="<%# dS_Turnos1 %>" OnInitializeLayout="TIPO_TURNO_COMBO_InitializeLayout" SelForeColor="">
														<Columns>
                                                            <igtbl:UltraGridColumn HeaderText="Column0">
                                                                <header caption="Column0"></header>
                                                            </igtbl:UltraGridColumn>
														</Columns>
														<DropDownLayout BorderCollapse="Separate" RowSelectors="No" RowHeightDefault="20px" Version="3.00">
															<RowStyle BorderWidth="1px" BorderColor="Black" BorderStyle="Solid" ForeColor="White" BackColor="CornflowerBlue">
																<Padding Left="3px"></Padding>
																<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
															</RowStyle>
															<SelectedRowStyle ForeColor="White" BackColor="Sienna" ></SelectedRowStyle>
															<HeaderStyle Font-Size="X-Small" Font-Names="Arial" Font-Bold="True" BorderColor="Black" BorderStyle="Solid"
																ForeColor="White" BackColor="Navy" >
																<BorderDetails ColorTop="173, 197, 235" WidthLeft="1px" StyleBottom="Solid" ColorBottom="DarkGray"
																	WidthTop="1px" StyleTop="Outset" WidthBottom="2px" ColorLeft="173, 197, 235"></BorderDetails>
															</HeaderStyle>
															<RowAlternateStyle ForeColor="Black" BackColor="WhiteSmoke"></RowAlternateStyle>
															<FrameStyle Width="325px" Cursor="Default" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana"
																BorderColor="Black" BorderStyle="Solid" ForeColor="#759AFD" Height="130px"></FrameStyle>
														</DropDownLayout>
														<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
													</igcmbo:WebCombo><FONT face="Arial"></FONT></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 179px"><FONT face="Arial">Email</FONT></TD>
												<TD style="WIDTH: 210px">
													<igtxt:WebTextEdit id="Persona_email" runat="server" Font-Names="Arial Narrow" CellSpacing="1" UseBrowserDefaults="False"
														BorderStyle="Solid" BorderWidth="1px" BorderColor="#7F9DB9" Width="200px" MaxLength="45">
														<ButtonsAppearance CustomButtonDefaultTriangleImages="Arrow">
															<ButtonPressedStyle BackColor="#83A6F4"></ButtonPressedStyle>
															<ButtonDisabledStyle BorderColor="#D7D7D7" ForeColor="#BEBEBE" BackColor="#E1E1DD"></ButtonDisabledStyle>
															<ButtonStyle Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" ForeColor="#506080"
																BackColor="#C5D5FC"></ButtonStyle>
															<ButtonHoverStyle BackColor="#DCEDFD"></ButtonHoverStyle>
														</ButtonsAppearance>
													</igtxt:WebTextEdit><FONT face="Arial"></FONT></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 179px"><FONT face="Arial">
														<asp:Label id="LBorrar" runat="server" Font-Names="Arial Narrow" Font-Size="X-Small" ForeColor="Red">* Seleccione esta opción si lo que quiere es borrar el empleado</asp:Label></FONT></TD>
												<TD style="WIDTH: 210px">
													<asp:CheckBox id="CBBorrar" runat="server" Font-Names="Arial Narrow" Font-Size="Smaller" Text="Borrar Empleado"></asp:CheckBox>&nbsp;
                                                    <FONT face="Arial"></FONT></TD>
											</TR>
										</TABLE>
									</TD>
									<TD align="center">
										<P>
											<asp:Image id="Image1" runat="server" Width="259px" Height="305px" ImageUrl="WF_Personas_ImaS.aspx"></asp:Image></P>
										<P align="center">
											<asp:Panel id="Panel1" runat="server" Height="24px">
												<INPUT id="File1" type="file" name="File1" runat="server"></asp:Panel></P>
										<P>
											<igtxt:WebImageButton id="WebImageButton1" runat="server" UseBrowserDefaults="False" Width="150px" Height="22px"
												Text="Subir Foto" OnClick="WebImageButton1_Click">
												<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
												<RoundedCorners MaxHeight="40" ImageUrl="ig_butCRM1.gif" MaxWidth="400" HoverImageUrl="ig_butCRM2.gif"
													RenderingType="FileImages" HeightOfBottomEdge="2" PressedImageUrl="ig_butCRM2.gif" WidthOfRightEdge="2"></RoundedCorners>
												<Appearance>
													<Style Cursor="Default">
													</Style>
													<Image Url="./Imagenes/panel-screenshot.png"></Image>
												</Appearance>
											</igtxt:WebImageButton></P>
										<P>
											<igtxt:WebImageButton id="WebImageButton2" runat="server" UseBrowserDefaults="False" Width="150px" Height="22px"
												Text="Eliminar Foto">
												<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
												<RoundedCorners MaxHeight="40" ImageUrl="ig_butCRM1.gif" MaxWidth="400" HoverImageUrl="ig_butCRM2.gif"
													RenderingType="FileImages" HeightOfBottomEdge="2" PressedImageUrl="ig_butCRM2.gif" WidthOfRightEdge="2"></RoundedCorners>
												<Appearance>
													<Style Cursor="Default">
													</Style>
													<Image Url="./Imagenes/gtk-no.png"></Image>
												</Appearance>
											</igtxt:WebImageButton></P>
									</TD>
								</TR>
							</TABLE>
						</asp:panel>&nbsp;
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 0%" align="center"><asp:label id="LError" runat="server" Font-Names="Arial" Font-Size="Smaller" ForeColor="#CC0033"></asp:label><asp:label id="LCorrecto" runat="server" Font-Names="Arial" Font-Size="Smaller" ForeColor="#00C000"></asp:label></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 0%" align="center">&nbsp;&nbsp;&nbsp;&nbsp;
						<igtxt:webimagebutton id="BDeshacerCambios" runat="server" Width="150px" UseBrowserDefaults="False" Height="22px"
							Text="Deshacer Cambios">
							<Alignments VerticalImage="Middle"></Alignments>
							<RoundedCorners MaxHeight="40" ImageUrl="ig_butCRM1.gif" MaxWidth="400" HoverImageUrl="ig_butCRM2.gif"
								RenderingType="FileImages" HeightOfBottomEdge="2" PressedImageUrl="ig_butCRM2.gif" WidthOfRightEdge="2"></RoundedCorners>
							<Appearance>
								<Style Cursor="Default">
								</Style>
								<Image Url="./Imagenes/Copia de deshacer.png"></Image>
							</Appearance>
						</igtxt:webimagebutton>&nbsp;&nbsp;&nbsp;
						<igtxt:webimagebutton id="BGuardarCambios" runat="server" Width="150px" UseBrowserDefaults="False" Height="22px"
							Text="Guardar Cambios" OnClick="BGuardarCambios_Click2" >
							<Alignments VerticalImage="Middle"></Alignments>
							<RoundedCorners MaxHeight="40" ImageUrl="ig_butCRM1.gif" MaxWidth="400" HoverImageUrl="ig_butCRM2.gif"
								RenderingType="FileImages" HeightOfBottomEdge="2" PressedImageUrl="ig_butCRM2.gif" WidthOfRightEdge="2"></RoundedCorners>
							<Appearance>
								<Style Cursor="Default">
								</Style>
								<Image Url="./Imagenes/Copia de GuardarComo.png"></Image>
							</Appearance>
						</igtxt:webimagebutton></TD>
				</TR>
			</TABLE>
</asp:Content>