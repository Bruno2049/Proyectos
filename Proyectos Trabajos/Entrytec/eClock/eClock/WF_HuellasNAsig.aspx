<%@ Register TagPrefix="uc1" TagName="WC_Menu" Src="WC_Menu.ascx" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Page language="c#" MasterPageFile="~/MasterPage.master"  CodeFile="WF_HuellasNAsig.aspx.cs" AutoEventWireup="True" Inherits="eClock.WF_HuellasNAsig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
			<TABLE id="Table2" style="WIDTH: 100%; HEIGHT: 100%" cellSpacing="1" cellPadding="1" align="center"
				border="0">
				<TR>
					<TD style="HEIGHT: 0%" height="1"></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 200px">
						<igtbl:ultrawebgrid id=Grid runat="server" ImageDirectory="/ig_common/Images/" Height="100%" Width="100%" DataSource="<%# dS_HuellasNAsig1 %>" UseAccessibleHeader="False" OnInitializeLayout="Grid_InitializeLayout">
							<DisplayLayout ColWidthDefault="" AllowSortingDefault="OnClient" RowHeightDefault="20px" Version="3.00"
								GridLinesDefault="Horizontal" SelectTypeRowDefault="Extended" HeaderClickActionDefault="SortSingle"
								BorderCollapseDefault="Separate" AllowColSizingDefault="Free" CellPaddingDefault="4" RowSelectorsDefault="No"
								Name="Grid" TableLayout="Fixed" CellClickActionDefault="RowSelect" AllowUpdateDefault="Yes">
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
								<igtbl:UltraGridBand AddButtonCaption="EC_PERSONAS" BaseTableName="EC_PERSONAS" Key="EC_PERSONAS">
									<Columns>
										<igtbl:UltraGridColumn HeaderText="Quitar" Key="QUITAR" IsBound="True" Width="50px" Type="CheckBox" DataType="System.Decimal"
											BaseColumnName="QUITAR">
											<Header Caption="Quitar"></Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="No. de empleado" Key="TERMINALES_DEXTRAS_TEXTO1" IsBound="True" BaseColumnName="TERMINALES_DEXTRAS_TEXTO1">
											<Footer>
												<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
											</Footer>
											<Header Caption="No. de empleado">
												<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
											</Header>
										</igtbl:UltraGridColumn>
									</Columns>
									
                                    <AddNewRow View="NotSet" Visible="NotSet">
                                    </AddNewRow>
								</igtbl:UltraGridBand>
							</Bands>
						</igtbl:ultrawebgrid></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 10px">
						<P align="center"><asp:label id="LError" runat="server" Font-Size="Smaller" ForeColor="#CC0033" Font-Names="Arial"></asp:label><asp:label id="LCorrecto" runat="server" Font-Size="Smaller" ForeColor="Green" Font-Names="Arial"></asp:label></P>
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 10px">
						<P align="center"><igtxt:webimagebutton id="Webimagebutton2" runat="server" Width="150px" Height="22px" UseBrowserDefaults="False"
								Text="Limpiar Selección">
								<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
								<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
									RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
								<Appearance>
									<Style Cursor="Default">
									</Style>
									<Image Url="./Imagenes/UncheckAll.png" Height="16px" Width="16px"></Image>
								</Appearance>
							</igtxt:webimagebutton>&nbsp;&nbsp;&nbsp;&nbsp;
							<igtxt:webimagebutton id="Webimagebutton3" runat="server" Width="150px" Height="22px" UseBrowserDefaults="False"
								Text="Seleccionar Todos">
								<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
								<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
									RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
								<Appearance>
									<Style Cursor="Default">
									</Style>
									<Image Url="./Imagenes/CheckAll.png" Height="16px" Width="16px"></Image>
								</Appearance>
							</igtxt:webimagebutton>&nbsp;&nbsp;&nbsp;&nbsp;
							<igtxt:webimagebutton id="BDeshacerCambios" runat="server" Width="164px" Height="22px" UseBrowserDefaults="False"
								Text="Borrar Seleccionados">
								<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
								<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
									RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
								<Appearance>
									<Style Cursor="Default">
									</Style>
									<Image Url="./Imagenes/Delete.png" Height="16px" Width="16px"></Image>
								</Appearance>
							</igtxt:webimagebutton>&nbsp;&nbsp;&nbsp;&nbsp;
						</P>
					</TD>
				</TR>
			</TABLE>
</asp:Content>