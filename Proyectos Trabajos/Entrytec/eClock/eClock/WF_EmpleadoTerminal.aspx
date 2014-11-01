<%@ Page language="c#" MasterPageFile="~/MasterPage.master" CodeFile="WF_EmpleadoTerminal.aspx.cs" AutoEventWireup="True" Inherits="eClock.WF_EmpleadoTerminal" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebListbar.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebListbar" TagPrefix="iglbar" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebCalcManager.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebCalcManager" TagPrefix="igcalc" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register TagPrefix="uc1" TagName="WC_Menu" Src="WC_Menu.ascx" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
			<TABLE id="Table1" style="WIDTH: 100%; HEIGHT: 100%" cellSpacing="1" cellPadding="1" width="408"
				border="0">
				<TR>
					<TD style="HEIGHT: 0%"></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 100%; HEIGHT: 0%" align="right" colSpan="2">
                        <asp:CheckBox ID="CBListado" runat="server" AutoPostBack="false" Font-Names="Arial Narrow"
                            Font-Size="Small" Text="Aplicar a todo el Listado" /></TD>
				</TR>
				<TR>
					<TD style="FONT-SIZE: 10pt; FONT-FAMILY: 'Arial Narrow'; HEIGHT: 0%" align="right"><asp:checkbox id="EmpleadoCheckBox1" runat="server" Text="Ver Empleados Borrados" AutoPostBack="True" oncheckedchanged="EmpleadoCheckBox1_CheckedChanged"></asp:checkbox></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 400px" align="center">
						<P>
							<igtbl:ultrawebgrid id=UltraWebGrid1 runat="server" UseAccessibleHeader="False" DataSource="<%# dS_EmpleadoxTerminal1 %>" Width="100%" ImageDirectory="/ig_common/Images/" Height="100%" DataMember="EC_EMPLEADOXTERMINAL" OnInitializeLayout="UltraWebGrid1_InitializeLayout" style="left: 6px; top: 0px">
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
									<Pager ChangeLinksColor="True" QuickPages="5" PageSize="200" PagerAppearance="Both" AllowCustomPaging="True"
										StyleMode="CustomLabels" Alignment="Center">
										<Style BorderWidth="1px" Font-Names="Arial Narrow" BorderStyle="Solid" ForeColor="Blue"
											BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

										</Style>
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
										<Style BorderColor="Window" ForeColor="Navy" BackColor="LightSteelBlue">
										</Style>
									</GroupByBox>
									<SelectedRowStyleDefault BackgroundImage="images/Office2003SelRow.png" ForeColor="DodgerBlue" BackColor="Sienna" >
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
									<igtbl:UltraGridBand AddButtonCaption="EC_EMPLEADOXTERMINAL" BaseTableName="EC_EMPLEADOXTERMINAL" Key="EC_EMPLEADOXTERMINAL">
										<Columns>
											<igtbl:UltraGridColumn HeaderText="PERSONA_ID" Key="PERSONA_ID" IsBound="True" Hidden="True" DataType="System.Decimal"
												BaseColumnName="PERSONA_ID">
												<Header Caption="PERSONA_ID"></Header>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="No. de empleado" Key="TRACVE" IsBound="True" Width="120px" DataType="System.Decimal"
												BaseColumnName="TRACVE">
												<Footer>
													<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
												</Footer>
												<Header Caption="No. de empleado">
													<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
												</Header>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="Nombre completo" Key="TRANOM" IsBound="True" Width="400px" BaseColumnName="TRANOM">
												<Footer>
													<RowLayoutColumnInfo OriginX="2"></RowLayoutColumnInfo>
												</Footer>
												<Header Caption="Nombre completo">
													<RowLayoutColumnInfo OriginX="2"></RowLayoutColumnInfo>
												</Header>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="&#193;rea" Key="DATARE" IsBound="True" Width="120px" BaseColumnName="DATARE">
												<Footer>
													<RowLayoutColumnInfo OriginX="3"></RowLayoutColumnInfo>
												</Footer>
												<Header Caption="&#193;rea">
													<RowLayoutColumnInfo OriginX="3"></RowLayoutColumnInfo>
												</Header>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="Departamento" Key="DATDEP" IsBound="True" Width="250px" BaseColumnName="DATDEP">
												<Footer>
													<RowLayoutColumnInfo OriginX="4"></RowLayoutColumnInfo>
												</Footer>
												<Header Caption="Departamento">
													<RowLayoutColumnInfo OriginX="4"></RowLayoutColumnInfo>
												</Header>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="Centro de Costos" Key="DATCCT" IsBound="True" Width="100px" BaseColumnName="DATCCT">
												<Footer>
													<RowLayoutColumnInfo OriginX="5"></RowLayoutColumnInfo>
												</Footer>
												<Header Caption="Centro de Costos">
													<RowLayoutColumnInfo OriginX="5"></RowLayoutColumnInfo>
												</Header>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="N&#243;mina" Key="CNOCVE" IsBound="True" Width="100px" BaseColumnName="CNOCVE">
												<Footer>
													<RowLayoutColumnInfo OriginX="6"></RowLayoutColumnInfo>
												</Footer>
												<Header Caption="N&#243;mina">
													<RowLayoutColumnInfo OriginX="6"></RowLayoutColumnInfo>
												</Header>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="Compa&#241;&#237;a" Key="CIACVE" IsBound="True" Width="100px" BaseColumnName="CIACVE">
												<Footer>
													<RowLayoutColumnInfo OriginX="7"></RowLayoutColumnInfo>
												</Footer>
												<Header Caption="Compa&#241;&#237;a">
													<RowLayoutColumnInfo OriginX="7"></RowLayoutColumnInfo>
												</Header>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="NS" Key="NS" IsBound="True" Width="150px" BaseColumnName="NS">
												<Footer>
													<RowLayoutColumnInfo OriginX="8"></RowLayoutColumnInfo>
												</Footer>
												<Header Caption="NS">
													<RowLayoutColumnInfo OriginX="8"></RowLayoutColumnInfo>
												</Header>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="Estatus" Key="STATUS" IsBound="True" Width="80px" BaseColumnName="STATUS">
												<Footer>
													<RowLayoutColumnInfo OriginX="9"></RowLayoutColumnInfo>
												</Footer>
												<Header Caption="Estatus">
													<RowLayoutColumnInfo OriginX="9"></RowLayoutColumnInfo>
												</Header>
											</igtbl:UltraGridColumn>
										</Columns>
										
                                        <AddNewRow View="NotSet" Visible="NotSet">
                                        </AddNewRow>
									</igtbl:UltraGridBand>
									<igtbl:UltraGridBand AddButtonCaption="EC_PERSONAS_TERMINALES_LINK" BaseTableName="EC_PERSONAS_TERMINALES_LINK"
										Key="EC_PERSONAS_TERMINALES_LINK">
										<Columns>
											<igtbl:UltraGridColumn HeaderText="PERSONA_ID" Key="PERSONA_ID" IsBound="True" DataType="System.Decimal"
												BaseColumnName="PERSONA_ID">
												<Header Caption="PERSONA_ID"></Header>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="TERMINAL_ID" Key="TERMINAL_ID" IsBound="True" DataType="System.Decimal"
												BaseColumnName="TERMINAL_ID">
												<Footer>
													<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
												</Footer>
												<Header Caption="TERMINAL_ID">
													<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
												</Header>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="TERMINAL_NOMBRE" Key="TERMINAL_NOMBRE" IsBound="True" BaseColumnName="TERMINAL_NOMBRE">
												<Footer>
													<RowLayoutColumnInfo OriginX="2"></RowLayoutColumnInfo>
												</Footer>
												<Header Caption="TERMINAL_NOMBRE">
													<RowLayoutColumnInfo OriginX="2"></RowLayoutColumnInfo>
												</Header>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="TERMINAL_BORRADO" Key="TERMINAL_BORRADO" IsBound="True" DataType="System.Decimal"
												BaseColumnName="TERMINAL_BORRADO">
												<Footer>
													<RowLayoutColumnInfo OriginX="3"></RowLayoutColumnInfo>
												</Footer>
												<Header Caption="TERMINAL_BORRADO">
													<RowLayoutColumnInfo OriginX="3"></RowLayoutColumnInfo>
												</Header>
											</igtbl:UltraGridColumn>
										</Columns>
                                        <AddNewRow View="NotSet" Visible="NotSet">
                                        </AddNewRow>
									</igtbl:UltraGridBand>
								</Bands>
							</igtbl:ultrawebgrid></P>
					</TD>
				</TR>
				<TR>
					<TD vAlign="middle">
						<TABLE id="Table2" style="WIDTH: 100%; HEIGHT: 100%" cellSpacing="1" cellPadding="1" width="752"
							border="0">
							<TR>
								<TD style="WIDTH: 196px">
									<iframe src="WF_ControlTermnales.aspx" style="WIDTH: 224px; HEIGHT: 134px"></iframe>
								</TD>
								<TD style="WIDTH: 100%">
									<P style="FONT-SIZE: 10pt; FONT-FAMILY: 'Arial Narrow'">
										&nbsp;</P>
								</TD>
							</TR>
						</TABLE>
						&nbsp;&nbsp;&nbsp;&nbsp;</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 0%" align="center"><asp:label id="LCorrecto" runat="server" ForeColor="Green" Font-Size="Smaller" Font-Names="Arial"></asp:label><asp:label id="LError" runat="server" ForeColor="#CC0033" Font-Size="Smaller" Font-Names="Arial"></asp:label></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 0%" align="center">&nbsp;&nbsp;&nbsp;
						<igtxt:webimagebutton id="BDeshacerCambios" runat="server" Text="Regresar" Height="22px" Width="150px"
							UseBrowserDefaults="False" ImageTextSpacing="4">
							<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
							<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
								RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
							<Appearance>
								<Style Cursor="Default">
								</Style>
								<Image Url="./Imagenes/Back.png" Height="16px" Width="16px"></Image>
							</Appearance>
						</igtxt:webimagebutton>&nbsp;&nbsp;&nbsp;
						<igtxt:webimagebutton id="Button1" runat="server" Text="Guardar Cambios" Height="22px" Width="150px" UseBrowserDefaults="False" ImageTextSpacing="4">
							<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
							<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
								RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
							<Appearance>
								<Style Cursor="Default">
								</Style>
								<Image Url="./Imagenes/Save_as.png" Height="16px" Width="16px"></Image>
							</Appearance>
						</igtxt:webimagebutton>&nbsp;&nbsp;&nbsp;
						<igtxt:webimagebutton id="Webimagebutton1" runat="server" Text="Quitar Terminales" Width="150px" Height="22px"
							UseBrowserDefaults="False" ImageTextSpacing="4">
							<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
							<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
								RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
							<Appearance>
								<Style Cursor="Default">
								</Style>
								<Image Url="./Imagenes/Remover.png" Height="18px" Width="20px"></Image>
							</Appearance>
						</igtxt:webimagebutton></TD>
				</TR>
			</TABLE>
</asp:Content>