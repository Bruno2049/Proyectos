<%@ Page language="c#" MasterPageFile="~/MasterPage.master"  CodeFile="WF_EmpleadosBus.aspx.cs" AutoEventWireup="True" Inherits="eClock.WF_EmpleadosBus" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics2.WebUI.WebCombo.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebCombo" TagPrefix="igcmbo" %>
<%@ Register TagPrefix="uc1" TagName="WC_Menu" Src="WC_Menu.ascx" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="0" style="WIDTH: 100%; HEIGHT: 100%">
				<TR>
					<TD style="HEIGHT: 0%"></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 10px">
						<TABLE id="Table3" style="WIDTH: 633px; HEIGHT: 226px" cellSpacing="1" cellPadding="1"
							width="633" border="0" align="center">
							<TR>
								<TD style="FONT-SIZE: 11pt; WIDTH: 179px; FONT-FAMILY: Arial" align="left"><FONT face="Arial Narrow"><FONT face="Arial Narrow">No. 
											de Empleado(TRACVE)</FONT></FONT></TD>
								<TD style="WIDTH: 210px" align="left">
									<igtxt:webnumericedit id="Tracve" runat="server" Font-Names="Arial Narrow" Width="200px" BorderColor="#7F9DB9"
										BorderWidth="1px" BorderStyle="Solid" UseBrowserDefaults="False" CellSpacing="1">
										<ButtonsAppearance CustomButtonDefaultTriangleImages="Arrow">
											<ButtonPressedStyle BackColor="#83A6F4"></ButtonPressedStyle>
											<ButtonDisabledStyle BorderColor="#D7D7D7" ForeColor="#BEBEBE" BackColor="#E1E1DD"></ButtonDisabledStyle>
											<ButtonStyle Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" ForeColor="#506080"
												BackColor="#C5D5FC"></ButtonStyle>
											<ButtonHoverStyle BackColor="#DCEDFD"></ButtonHoverStyle>
										</ButtonsAppearance>
										<SpinButtons DefaultTriangleImages="ArrowSmall" Width="15px"></SpinButtons>
									</igtxt:webnumericedit></TD>
								<TD align="right">
									<P align="right">
										<asp:CheckBox id="EmpleadosBusCheckBox1" runat="server" Text="Ver Empleados Borrados" Font-Size="Smaller"
											Font-Names="Arial Narrow"></asp:CheckBox></P>
								</TD>
							</TR>
							<TR>
								<TD style="FONT-SIZE: 11pt; WIDTH: 179px; FONT-FAMILY: Arial" align="left"><FONT face="Arial Narrow">Nombre</FONT></TD>
								<TD style="WIDTH: 210px" align="left">
									<igtxt:webtextedit id="Tranom" runat="server" Font-Names="Arial Narrow" Width="200px" BorderColor="#7F9DB9"
										BorderWidth="1px" BorderStyle="Solid" UseBrowserDefaults="False" CellSpacing="1" MaxLength="60">
										<ButtonsAppearance CustomButtonDefaultTriangleImages="Arrow">
											<ButtonPressedStyle BackColor="#83A6F4"></ButtonPressedStyle>
											<ButtonDisabledStyle BorderColor="#D7D7D7" ForeColor="#BEBEBE" BackColor="#E1E1DD"></ButtonDisabledStyle>
											<ButtonStyle Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" ForeColor="#506080"
												BackColor="#C5D5FC"></ButtonStyle>
											<ButtonHoverStyle BackColor="#DCEDFD"></ButtonHoverStyle>
										</ButtonsAppearance>
									</igtxt:webtextedit></TD>
								<TD align="right"></TD>
							</TR>
							<TR>
								<TD style="FONT-SIZE: 11pt; WIDTH: 179px; FONT-FAMILY: Arial" align="left">
									<P><FONT face="Arial Narrow">Área</FONT></P>
								</TD>
								<TD style="WIDTH: 210px" align="left">
									<igtxt:webtextedit id="Datare" runat="server" Font-Names="Arial Narrow" Width="200px" BorderColor="#7F9DB9"
										BorderWidth="1px" BorderStyle="Solid" UseBrowserDefaults="False" CellSpacing="1" MaxLength="40">
										<ButtonsAppearance CustomButtonDefaultTriangleImages="Arrow">
											<ButtonPressedStyle BackColor="#83A6F4"></ButtonPressedStyle>
											<ButtonDisabledStyle BorderColor="#D7D7D7" ForeColor="#BEBEBE" BackColor="#E1E1DD"></ButtonDisabledStyle>
											<ButtonStyle Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" ForeColor="#506080"
												BackColor="#C5D5FC"></ButtonStyle>
											<ButtonHoverStyle BackColor="#DCEDFD"></ButtonHoverStyle>
										</ButtonsAppearance>
									</igtxt:webtextedit></TD>
								<TD align="right"></TD>
							</TR>
							<TR>
								<TD style="FONT-SIZE: 11pt; WIDTH: 179px; FONT-FAMILY: Arial; HEIGHT: 26px" align="left"><FONT face="Arial Narrow">Departamento</FONT></TD>
								<TD style="WIDTH: 210px; HEIGHT: 26px" align="left">
									<igtxt:webtextedit id="Datdep" runat="server" Font-Names="Arial Narrow" Width="200px" BorderColor="#7F9DB9"
										BorderWidth="1px" BorderStyle="Solid" UseBrowserDefaults="False" CellSpacing="1" MaxLength="40">
										<ButtonsAppearance CustomButtonDefaultTriangleImages="Arrow">
											<ButtonPressedStyle BackColor="#83A6F4"></ButtonPressedStyle>
											<ButtonDisabledStyle BorderColor="#D7D7D7" ForeColor="#BEBEBE" BackColor="#E1E1DD"></ButtonDisabledStyle>
											<ButtonStyle Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" ForeColor="#506080"
												BackColor="#C5D5FC"></ButtonStyle>
											<ButtonHoverStyle BackColor="#DCEDFD"></ButtonHoverStyle>
										</ButtonsAppearance>
									</igtxt:webtextedit></TD>
								<TD style="HEIGHT: 26px" align="right"></TD>
							</TR>
							<TR>
								<TD style="FONT-SIZE: 11pt; WIDTH: 179px; FONT-FAMILY: Arial" align="left"><FONT face="Arial Narrow">Centro 
										de Costos</FONT></TD>
								<TD style="WIDTH: 210px" align="left">
									<igtxt:webtextedit id="Datcct" runat="server" Font-Names="Arial Narrow" Width="200px" BorderColor="#7F9DB9"
										BorderWidth="1px" BorderStyle="Solid" UseBrowserDefaults="False" CellSpacing="1" MaxLength="10">
										<ButtonsAppearance CustomButtonDefaultTriangleImages="Arrow">
											<ButtonPressedStyle BackColor="#83A6F4"></ButtonPressedStyle>
											<ButtonDisabledStyle BorderColor="#D7D7D7" ForeColor="#BEBEBE" BackColor="#E1E1DD"></ButtonDisabledStyle>
											<ButtonStyle Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" ForeColor="#506080"
												BackColor="#C5D5FC"></ButtonStyle>
											<ButtonHoverStyle BackColor="#DCEDFD"></ButtonHoverStyle>
										</ButtonsAppearance>
									</igtxt:webtextedit></TD>
								<TD align="right"></TD>
							</TR>
							<TR>
								<TD style="FONT-SIZE: 11pt; WIDTH: 179px; FONT-FAMILY: Arial" align="left"><FONT face="Arial Narrow">Tipo 
										de Nómina</FONT></TD>
								<TD style="WIDTH: 210px" align="left">
									<igtxt:webtextedit id="Cnocve" runat="server" Font-Names="Arial Narrow" Width="200px" BorderColor="#7F9DB9"
										BorderWidth="1px" BorderStyle="Solid" UseBrowserDefaults="False" CellSpacing="1" MaxLength="4">
										<ButtonsAppearance CustomButtonDefaultTriangleImages="Arrow">
											<ButtonPressedStyle BackColor="#83A6F4"></ButtonPressedStyle>
											<ButtonDisabledStyle BorderColor="#D7D7D7" ForeColor="#BEBEBE" BackColor="#E1E1DD"></ButtonDisabledStyle>
											<ButtonStyle Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" ForeColor="#506080"
												BackColor="#C5D5FC"></ButtonStyle>
											<ButtonHoverStyle BackColor="#DCEDFD"></ButtonHoverStyle>
										</ButtonsAppearance>
									</igtxt:webtextedit></TD>
								<TD align="right"></TD>
							</TR>
							<TR>
								<TD style="FONT-SIZE: 11pt; WIDTH: 179px; FONT-FAMILY: Arial" align="left"><FONT face="Arial Narrow">Compañia</FONT></TD>
								<TD style="WIDTH: 210px" align="left">
									<igtxt:webtextedit id="Ciacve" runat="server" Font-Names="Arial Narrow" Width="200px" BorderColor="#7F9DB9"
										BorderWidth="1px" BorderStyle="Solid" UseBrowserDefaults="False" CellSpacing="1" MaxLength="4">
										<ButtonsAppearance CustomButtonDefaultTriangleImages="Arrow">
											<ButtonPressedStyle BackColor="#83A6F4"></ButtonPressedStyle>
											<ButtonDisabledStyle BorderColor="#D7D7D7" ForeColor="#BEBEBE" BackColor="#E1E1DD"></ButtonDisabledStyle>
											<ButtonStyle Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" ForeColor="#506080"
												BackColor="#C5D5FC"></ButtonStyle>
											<ButtonHoverStyle BackColor="#DCEDFD"></ButtonHoverStyle>
										</ButtonsAppearance>
									</igtxt:webtextedit></TD>
								<TD align="right"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 179px" align="left"><FONT face="Arial Narrow">Email</FONT></TD>
								<TD style="WIDTH: 210px" align="left">
									<igtxt:webtextedit id="Persona_email" runat="server" Font-Names="Arial Narrow" Width="200px" BorderColor="#7F9DB9"
										BorderWidth="1px" BorderStyle="Solid" UseBrowserDefaults="False" CellSpacing="1" MaxLength="32">
										<ButtonsAppearance CustomButtonDefaultTriangleImages="Arrow">
											<ButtonPressedStyle BackColor="#83A6F4"></ButtonPressedStyle>
											<ButtonDisabledStyle BorderColor="#D7D7D7" ForeColor="#BEBEBE" BackColor="#E1E1DD"></ButtonDisabledStyle>
											<ButtonStyle Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" ForeColor="#506080"
												BackColor="#C5D5FC"></ButtonStyle>
											<ButtonHoverStyle BackColor="#DCEDFD"></ButtonHoverStyle>
										</ButtonsAppearance>
									</igtxt:webtextedit></TD>
								<TD style="FONT-SIZE: 10pt; COLOR: red; FONT-FAMILY: Arial" align="right"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 179px" align="left"><FONT face="Arial Narrow"><FONT face="Arial Narrow">Grupo</FONT></FONT></TD>
								<TD style="WIDTH: 210px" align="left">
									<igtxt:WebTextEdit id="Grupo" runat="server" Width="200px" BorderColor="#7F9DB9" BorderWidth="1px"
										BorderStyle="Solid" UseBrowserDefaults="False" CellSpacing="1">
										<ButtonsAppearance CustomButtonDefaultTriangleImages="Arrow">
											<ButtonPressedStyle BackColor="#83A6F4"></ButtonPressedStyle>
											<ButtonDisabledStyle BorderColor="#D7D7D7" ForeColor="#BEBEBE" BackColor="#E1E1DD"></ButtonDisabledStyle>
											<ButtonStyle Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" ForeColor="#506080"
												BackColor="#C5D5FC"></ButtonStyle>
											<ButtonHoverStyle BackColor="#DCEDFD"></ButtonHoverStyle>
										</ButtonsAppearance>
									</igtxt:WebTextEdit></TD>
								<TD style="FONT-SIZE: 10pt; COLOR: red; FONT-FAMILY: Arial" align="right"><FONT face="Arial Narrow"></FONT></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 179px" align="left"><FONT face="Arial Narrow"><FONT face="Arial Narrow">Horarios</FONT></FONT></TD>
								<TD style="WIDTH: 210px" align="left">
									<igcmbo:webcombo id=TIPO_TURNO_COMBO runat="server" Font-Size="9pt" Font-Names="Arial Narrow" Width="200px" BorderColor="LightGray" BorderWidth="1px" BorderStyle="Solid" DataValueField="TURNO_ID" DataTextField="TURNO_NOMBRE" DataMember="EC_TURNOS" DataSource="<%# dS_EmpleadoBuscar1 %>"   SelBackColor="17, 69, 158"   Version="3.00" BackColor="White" Height="22px" ForeColor="Black" OnInitializeLayout="TIPO_TURNO_COMBO_InitializeLayout">
										<Columns>
											<igtbl:UltraGridColumn HeaderText="TURNO_ID" Key="TURNO_ID" IsBound="True" Hidden="True" DataType="System.Decimal"
												BaseColumnName="TURNO_ID">
												<Footer Key="TURNO_ID"></Footer>
												<Header Key="TURNO_ID" Caption="TURNO_ID"></Header>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="TIPO_TURNO_ID" Key="TIPO_TURNO_ID" IsBound="True" Hidden="True" DataType="System.Decimal"
												BaseColumnName="TIPO_TURNO_ID">
												<Footer Key="TIPO_TURNO_ID">
													<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
												</Footer>
												<Header Key="TIPO_TURNO_ID" Caption="TIPO_TURNO_ID">
													<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
												</Header>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="Nombre" Key="TURNO_NOMBRE" IsBound="True" BaseColumnName="TURNO_NOMBRE">
												<Footer Key="TURNO_NOMBRE">
													<RowLayoutColumnInfo OriginX="2"></RowLayoutColumnInfo>
												</Footer>
												<Header Key="TURNO_NOMBRE" Caption="Nombre">
													<RowLayoutColumnInfo OriginX="2"></RowLayoutColumnInfo>
												</Header>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="Asistencia" Key="TURNO_ASISTENCIA" IsBound="True" Hidden="True" DataType="System.Decimal"
												BaseColumnName="TURNO_ASISTENCIA">
												<Footer Key="TURNO_ASISTENCIA">
													<RowLayoutColumnInfo OriginX="3"></RowLayoutColumnInfo>
												</Footer>
												<Header Key="TURNO_ASISTENCIA" Caption="Asistencia">
													<RowLayoutColumnInfo OriginX="3"></RowLayoutColumnInfo>
												</Header>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="TURNO_BORRADO" Key="TURNO_BORRADO" IsBound="True" Hidden="True" DataType="System.Decimal"
												BaseColumnName="TURNO_BORRADO">
												<Footer Key="TURNO_BORRADO">
													<RowLayoutColumnInfo OriginX="4"></RowLayoutColumnInfo>
												</Footer>
												<Header Key="TURNO_BORRADO" Caption="TURNO_BORRADO">
													<RowLayoutColumnInfo OriginX="4"></RowLayoutColumnInfo>
												</Header>
											</igtbl:UltraGridColumn>
										</Columns>
										<DropDownLayout DropdownWidth="120px" BorderCollapse="Separate" RowSelectors="No" RowHeightDefault="20px"
											HeaderClickAction="Select" GridLines="None">
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
											<FrameStyle Width="120px" Cursor="Default" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana"
												BorderColor="Black" BorderStyle="Solid" ForeColor="#759AFD" Height="130px"></FrameStyle>
										</DropDownLayout>
										<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
									</igcmbo:webcombo></TD>
								<TD style="FONT-SIZE: 10pt; COLOR: red; FONT-FAMILY: Arial" align="center">
									<igtxt:webimagebutton id="Button1" runat="server" Text="Mostrar Resultados" Width="150px" UseBrowserDefaults="False"
										Height="22px">
										<Alignments VerticalImage="Bottom"></Alignments>
										<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
											RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
										<Appearance>
											<Style Cursor="Default">
											</Style>
											<Image Url="./Imagenes/stock-bookmarks.png"></Image>
										</Appearance>
									</igtxt:webimagebutton></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top" align="center" height="100%">
						<P>
							<igtbl:ultrawebgrid id=UltraWebGrid1 runat="server" Width="100%" Height="100%" DataSource="<%# dS_EmpleadoBuscar1 %>" DataMember="EC_PERSONAS" ImageDirectory="/ig_common/Images/" UseAccessibleHeader="False" OnInitializeLayout="UltraWebGrid1_InitializeLayout">
								<DisplayLayout ColWidthDefault="" AllowSortingDefault="OnClient" RowHeightDefault="20px" Version="3.00"
									GridLinesDefault="Horizontal" SelectTypeRowDefault="Extended" HeaderClickActionDefault="SortSingle"
									BorderCollapseDefault="Separate" AllowColSizingDefault="Free" CellPaddingDefault="4" RowSelectorsDefault="No"
									Name="UltraWebGrid1" TableLayout="Fixed" CellClickActionDefault="RowSelect" AllowUpdateDefault="Yes">
									<AddNewBox>
										<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

										</Style>
									</AddNewBox>
									<Pager PageSize="20">
										<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

										</Style>
									</Pager>
									<HeaderStyleDefault BackgroundImage="images/GridTitulo.gif" Font-Size="X-Small" Font-Names="Arial" Font-Bold="True" BorderColor="Black" BorderStyle="Solid"
										HorizontalAlign="Left" ForeColor="White" BackColor="Navy" >
										<BorderDetails WidthLeft="0px" WidthTop="0px" WidthRight="1px" WidthBottom="1px"></BorderDetails>
									</HeaderStyleDefault>
									<FrameStyle Width="100%" BorderWidth="1px" Font-Size="8pt" Font-Names="Arial Narrow" BorderColor="Black"
										BorderStyle="Solid" ForeColor="#759AFD" Height="100%"></FrameStyle>
									<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
										<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
									</FooterStyleDefault>
									<ActivationObject BorderStyle="Dotted" BorderColor="1, 68, 208"></ActivationObject>
									<GroupByBox Prompt="Arrastre la columna que desea agrupar...">
										<Style BorderColor="Window" ForeColor="Navy" BackColor="LightSteelBlue">
										</Style>
									</GroupByBox>
									<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
									<SelectedRowStyleDefault BackgroundImage="images/Office2003SelRow.png" ForeColor="WhiteSmoke" BackColor="Sienna" >
										<BorderDetails WidthLeft="0px" StyleBottom="Solid" ColorBottom="Black" WidthTop="0px" WidthRight="0px"
											StyleTop="None" StyleRight="None" WidthBottom="1px" StyleLeft="None"></BorderDetails>
									</SelectedRowStyleDefault>
									<RowAlternateStyleDefault ForeColor="Black" BackColor="WhiteSmoke"></RowAlternateStyleDefault>
									<RowStyleDefault BorderWidth="1px" BorderColor="Black" BorderStyle="Solid" ForeColor="White" BackColor="CornflowerBlue">
										<Padding Left="3px"></Padding>
										<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
									</RowStyleDefault>
									<ImageUrls ExpandImage="ig_tblcrm_rowarrow_right.gif" CollapseImage="ig_tblcrm_rowarrow_down.gif"></ImageUrls>
								</DisplayLayout>
								<Bands>
									<igtbl:UltraGridBand AddButtonCaption="EC_PERSONAS" BaseTableName="EC_PERSONAS" Key="EC_PERSONAS">
										<Columns>
											<igtbl:UltraGridColumn HeaderText="PERSONA_ID" Key="PERSONA_ID" IsBound="True" Hidden="True" DataType="System.Decimal"
												BaseColumnName="PERSONA_ID">
												<Footer Key="PERSONA_ID"></Footer>
												<Header Key="PERSONA_ID" Caption="PERSONA_ID"></Header>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="PERSONA_LINK_ID" Key="PERSONA_LINK_ID" IsBound="True" Hidden="True"
												DataType="System.Decimal" BaseColumnName="PERSONA_LINK_ID">
												<Footer Key="PERSONA_LINK_ID">
													<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
												</Footer>
												<Header Key="PERSONA_LINK_ID" Caption="PERSONA_LINK_ID">
													<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
												</Header>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="E- mail" Key="PERSONA_EMAIL" IsBound="True" Width="250px" BaseColumnName="PERSONA_EMAIL">
												<Footer Key="PERSONA_EMAIL">
													<RowLayoutColumnInfo OriginX="2"></RowLayoutColumnInfo>
												</Footer>
												<Header Key="PERSONA_EMAIL" Caption="E- mail">
													<RowLayoutColumnInfo OriginX="2"></RowLayoutColumnInfo>
												</Header>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="N&#243;mina" Key="CNOCVE" IsBound="True" Width="100px" BaseColumnName="CNOCVE">
												<Footer Key="CNOCVE">
													<RowLayoutColumnInfo OriginX="3"></RowLayoutColumnInfo>
												</Footer>
												<Header Key="CNOCVE" Caption="N&#243;mina">
													<RowLayoutColumnInfo OriginX="3"></RowLayoutColumnInfo>
												</Header>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="Compa&#241;&#237;a" Key="CIACVE" IsBound="True" Width="100px" BaseColumnName="CIACVE">
												<Footer Key="CIACVE">
													<RowLayoutColumnInfo OriginX="4"></RowLayoutColumnInfo>
												</Footer>
												<Header Key="CIACVE" Caption="Compa&#241;&#237;a">
													<RowLayoutColumnInfo OriginX="4"></RowLayoutColumnInfo>
												</Header>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="No. de Empleado" Key="TRACVE" IsBound="True" Width="120px" DataType="System.Decimal"
												BaseColumnName="TRACVE">
												<Footer Key="TRACVE">
													<RowLayoutColumnInfo OriginX="5"></RowLayoutColumnInfo>
												</Footer>
												<Header Key="TRACVE" Caption="No. de Empleado">
													<RowLayoutColumnInfo OriginX="5"></RowLayoutColumnInfo>
												</Header>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="Nombre Completo" Key="TRANOM" IsBound="True" Width="400px" BaseColumnName="TRANOM">
												<Footer Key="TRANOM">
													<RowLayoutColumnInfo OriginX="6"></RowLayoutColumnInfo>
												</Footer>
												<Header Key="TRANOM" Caption="Nombre Completo">
													<RowLayoutColumnInfo OriginX="6"></RowLayoutColumnInfo>
												</Header>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="&#193;rea" Key="DATARE" IsBound="True" Width="120px" BaseColumnName="DATARE">
												<Footer Key="DATARE">
													<RowLayoutColumnInfo OriginX="7"></RowLayoutColumnInfo>
												</Footer>
												<Header Key="DATARE" Caption="&#193;rea">
													<RowLayoutColumnInfo OriginX="7"></RowLayoutColumnInfo>
												</Header>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="Departamento" Key="DATDEP" IsBound="True" Width="250px" BaseColumnName="DATDEP">
												<Footer Key="DATDEP">
													<RowLayoutColumnInfo OriginX="8"></RowLayoutColumnInfo>
												</Footer>
												<Header Key="DATDEP" Caption="Departamento">
													<RowLayoutColumnInfo OriginX="8"></RowLayoutColumnInfo>
												</Header>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="Centro de Costos" Key="DATCCT" IsBound="True" Width="100px" BaseColumnName="DATCCT">
												<Footer Key="DATCCT">
													<RowLayoutColumnInfo OriginX="9"></RowLayoutColumnInfo>
												</Footer>
												<Header Key="DATCCT" Caption="Centro de Costos">
													<RowLayoutColumnInfo OriginX="9"></RowLayoutColumnInfo>
												</Header>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="TIPO_TURNO_ID" Key="TIPO_TURNO_ID" IsBound="True" Hidden="True" DataType="System.Decimal"
												BaseColumnName="TIPO_TURNO_ID">
												<Footer Key="TIPO_TURNO_ID">
													<RowLayoutColumnInfo OriginX="10"></RowLayoutColumnInfo>
												</Footer>
												<Header Key="TIPO_TURNO_ID" Caption="TIPO_TURNO_ID">
													<RowLayoutColumnInfo OriginX="10"></RowLayoutColumnInfo>
												</Header>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="Turno" Key="TURNO_NOMBRE" IsBound="True" Width="150px" BaseColumnName="TURNO_NOMBRE">
												<Footer Key="TURNO_NOMBRE">
													<RowLayoutColumnInfo OriginX="11"></RowLayoutColumnInfo>
												</Footer>
												<Header Key="TURNO_NOMBRE" Caption="Turno">
													<RowLayoutColumnInfo OriginX="11"></RowLayoutColumnInfo>
												</Header>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="TURNO_ID" Key="TURNO_ID" IsBound="True" Hidden="True" DataType="System.Decimal"
												BaseColumnName="TURNO_ID">
												<Footer Key="TURNO_ID">
													<RowLayoutColumnInfo OriginX="12"></RowLayoutColumnInfo>
												</Footer>
												<Header Key="TURNO_ID" Caption="TURNO_ID">
													<RowLayoutColumnInfo OriginX="12"></RowLayoutColumnInfo>
												</Header>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="PERSONA_BORRADO" Key="PERSONA_BORRADO" IsBound="True" Hidden="True"
												DataType="System.Decimal" BaseColumnName="PERSONA_BORRADO">
												<Footer Key="PERSONA_BORRADO">
													<RowLayoutColumnInfo OriginX="13"></RowLayoutColumnInfo>
												</Footer>
												<Header Key="PERSONA_BORRADO" Caption="PERSONA_BORRADO">
													<RowLayoutColumnInfo OriginX="13"></RowLayoutColumnInfo>
												</Header>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="STATUS" Key="STATUS" IsBound="True" Hidden="True" BaseColumnName="STATUS">
												<Footer Key="STATUS">
													<RowLayoutColumnInfo OriginX="14"></RowLayoutColumnInfo>
												</Footer>
												<Header Key="STATUS" Caption="STATUS">
													<RowLayoutColumnInfo OriginX="14"></RowLayoutColumnInfo>
												</Header>
											</igtbl:UltraGridColumn>
										</Columns>
										
									</igtbl:UltraGridBand>
								</Bands>
							</igtbl:ultrawebgrid></P>
					</TD>
				</TR>
				<TR>
					<TD align="center" vAlign="top">
						<asp:label id="LError" runat="server" Font-Names="Arial Narrow" Font-Size="Smaller" ForeColor="#CC0033"></asp:label>
						<asp:label id="LCorrecto" runat="server" Font-Names="Arial Narrow" Font-Size="Smaller" ForeColor="#00C000"></asp:label>
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 0%" align="center">
						<igtxt:webimagebutton id="BBuscarEmpleado" runat="server" Width="150px" UseBrowserDefaults="False" Height="22px"
							Text="Abrir Empleado">
							<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
							<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
								RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
							<Appearance>
								<Style Cursor="Default">
								</Style>
								<Image Url="./Imagenes/Search.png" Height="16px" Width="16px"></Image>
							</Appearance>
						</igtxt:webimagebutton></TD>
				</TR>
			</TABLE>
</asp:Content>