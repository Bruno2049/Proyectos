<%@ Page language="c#" CodeFile="WF_AccesosNAsig.aspx.cs" AutoEventWireup="false" Inherits="eClock.WF_AccesosNAsig" %>

<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register TagPrefix="uc1" TagName="WC_Menu" Src="WC_Menu.ascx" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Importar Empleados</title>
</head>
<body style="font-size: 11px; font-family: tahoma; text-align: center; margin: 0px; ">
    <form id="form1" runat="server" >
    <div>  
<TABLE style="WIDTH: 100%; HEIGHT: 100%" id="Table1" cellSpacing=1 cellPadding=1 align=center border=0><TBODY><TR><TD style="HEIGHT: 200px"><igtbl:ultrawebgrid id="Grid" runat="server" Width="100%" OnInitializeLayout="Grid_InitializeLayout" DataSource="<%# dS_AccesosNAsig1 %>" Height="100%" DataMember="EC_PERSONAS" style="left: -6px; top: 8px">
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
                                    <CollapseImage Url="ig_tblcrm_rowarrow_down.gif"  />
                                    <ExpandImage Url="ig_tblcrm_rowarrow_right.gif"  />
                                </Images>
							</DisplayLayout>
							<Bands>
								<igtbl:UltraGridBand AddButtonCaption="EC_PERSONAS" BaseTableName="EC_PERSONAS" Key="EC_PERSONAS">
									<Columns>
										<igtbl:UltraGridColumn HeaderText="QUITAR" Key="QUITAR" IsBound="True" EditorControlID="" FormulaErrorValue=""
											Width="50px" Type="CheckBox" Format="" DataType="System.Decimal" BaseColumnName="QUITAR"
											FooterText="">
											<Footer Caption="" Title=""></Footer>
											<Header Caption="QUITAR" Title=""></Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="TERMINALES_DEXTRAS_ID" Key="TERMINALES_DEXTRAS_ID" IsBound="True" EditorControlID=""
											FormulaErrorValue="" Hidden="True" Format="" DataType="System.Decimal" BaseColumnName="TERMINALES_DEXTRAS_ID"
											FooterText="">
											<Footer Caption="" Title="">
												<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
											</Footer>
											<Header Caption="TERMINALES_DEXTRAS_ID" Title="">
												<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
											</Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="Identificador" Key="TERMINALES_DEXTRAS_TEXTO1" IsBound="True" EditorControlID=""
											FormulaErrorValue="" Width="120px" Format="" BaseColumnName="TERMINALES_DEXTRAS_TEXTO1"
											FooterText="">
											<Footer Caption="" Title="">
												<RowLayoutColumnInfo OriginX="2"></RowLayoutColumnInfo>
											</Footer>
											<Header Caption="Identificador" Title="">
												<RowLayoutColumnInfo OriginX="2"></RowLayoutColumnInfo>
											</Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="Detalles" Key="TERMINALES_DEXTRAS_TEXTO2" IsBound="True" EditorControlID=""
											FormulaErrorValue="" Width="400px" Format="" BaseColumnName="TERMINALES_DEXTRAS_TEXTO2"
											FooterText="">
											<Footer Caption="" Title="">
												<RowLayoutColumnInfo OriginX="3"></RowLayoutColumnInfo>
											</Footer>
											<Header Caption="Detalles" Title="">
												<RowLayoutColumnInfo OriginX="3"></RowLayoutColumnInfo>
											</Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="Terminal" Key="TERMINAL_NOMBRE" IsBound="True" EditorControlID="" FormulaErrorValue=""
											Width="150px" Format="" BaseColumnName="TERMINAL_NOMBRE" FooterText="">
											<Footer Caption="" Title="">
												<RowLayoutColumnInfo OriginX="4"></RowLayoutColumnInfo>
											</Footer>
											<Header Caption="Terminal" Title="">
												<RowLayoutColumnInfo OriginX="4"></RowLayoutColumnInfo>
											</Header>
										</igtbl:UltraGridColumn>
									</Columns>
									
                                    <AddNewRow View="NotSet" Visible="NotSet">
                                    </AddNewRow>
								</igtbl:UltraGridBand>
							</Bands>
						</igtbl:ultrawebgrid></TD></TR><TR><TD style="HEIGHT: 10px"><P align=center><asp:label id="LError" runat="server" Font-Names="Arial" ForeColor="#CC0033" Font-Size="Smaller"></asp:label><asp:label id="LCorrecto" runat="server" Font-Names="Arial" ForeColor="Green" Font-Size="Smaller"></asp:label></P></TD></TR><TR><TD style="HEIGHT: 10px"><P align=center>
    <igtxt:webimagebutton id="Btn_Recalcular" runat="server" Text="Recalcular" 
        Width="150px" Height="22px" ImageTextSpacing="4" onclick="Btn_Recalcular_Click">
								<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
								<RoundedCorners MaxHeight="80" ImageUrl="[ig_butXP1wh.gif]" MaxWidth="400" HoverImageUrl="[ig_butXP2wh.gif]"
									RenderingType="FileImages" PressedImageUrl="[ig_butXP4wh.gif]" DisabledImageUrl="[ig_butXP5wh.gif]" FocusImageUrl="[ig_butXP3wh.gif]"></RoundedCorners>
								<Appearance>
									<Style Cursor="Default">
									</Style>

<ButtonStyle Cursor="Default"></ButtonStyle>

								</Appearance>
                            <DisabledAppearance>
                                <Style Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False"></Style>
<ButtonStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"></ButtonStyle>
                            </DisabledAppearance>
                            <HoverAppearance>
                                <Style Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False"></Style>
<ButtonStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"></ButtonStyle>
                            </HoverAppearance>
                            <FocusAppearance>
                                <Style Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False"></Style>
<ButtonStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"></ButtonStyle>
                            </FocusAppearance>
                            <PressedAppearance>
                                <Style Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False"></Style>
<ButtonStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"></ButtonStyle>
                            </PressedAppearance>
							</igtxt:webimagebutton>&nbsp;&nbsp;&nbsp; <igtxt:webimagebutton id="Webimagebutton2" runat="server" Text="Limpiar Selección" Width="150px" Height="22px" UseBrowserDefaults="False" ImageTextSpacing="4">
								<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
								<RoundedCorners MaxHeight="80" ImageUrl="[ig_butXP1wh.gif]" MaxWidth="400" HoverImageUrl="[ig_butXP2wh.gif]"
									RenderingType="FileImages" PressedImageUrl="[ig_butXP4wh.gif]" DisabledImageUrl="[ig_butXP5wh.gif]" FocusImageUrl="[ig_butXP3wh.gif]"></RoundedCorners>
								<Appearance>
									<Style Cursor="Default">
									</Style>
                                    <Image Url="./Imagenes/UncheckAll.png" Height="16px" Width="16px" />
                                    <InnerBorder WidthLeft="5px" WidthRight="5px" />
								</Appearance>
                            <DisabledAppearance>
                                <Style Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False"></Style>
                            </DisabledAppearance>
                            <HoverAppearance>
                                <Style Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False"></Style>
                            </HoverAppearance>
                            <FocusAppearance>
                                <Style Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False"></Style>
                            </FocusAppearance>
                            <PressedAppearance>
                                <Style Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False"></Style>
                            </PressedAppearance>
							</igtxt:webimagebutton>&nbsp;&nbsp;&nbsp;&nbsp; <igtxt:webimagebutton id="Webimagebutton3" runat="server" Text="Seleccionar Todos" Width="150px" Height="22px" UseBrowserDefaults="False" ImageTextSpacing="4">
								<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
								<RoundedCorners MaxHeight="80" ImageUrl="[ig_butXP1wh.gif]" MaxWidth="400" HoverImageUrl="[ig_butXP2wh.gif]"
									RenderingType="FileImages" PressedImageUrl="[ig_butXP4wh.gif]" DisabledImageUrl="[ig_butXP5wh.gif]" FocusImageUrl="[ig_butXP3wh.gif]"></RoundedCorners>
								<Appearance>
									<Style Cursor="Default">
									</Style>
                                    <Image Url="./Imagenes/CheckAll.png" Height="16px" Width="16px" />
                                    <InnerBorder WidthLeft="5px" WidthRight="5px" />
								</Appearance>
                                <DisabledAppearance>
                                    <Style Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False"></Style>
                                </DisabledAppearance>
                                <HoverAppearance>
                                    <Style Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False"></Style>
                                </HoverAppearance>
                                <FocusAppearance>
                                    <Style Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False"></Style>
                                </FocusAppearance>
                                <PressedAppearance>
                                    <Style Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False"></Style>
                                </PressedAppearance>
							</igtxt:webimagebutton>&nbsp;&nbsp;&nbsp;&nbsp; <igtxt:webimagebutton id="BDeshacerCambios" runat="server" Text="Borrar Seleccionados" Width="158px" Height="22px" UseBrowserDefaults="False" ImageTextSpacing="4">
								<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
								<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
									RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
								<Appearance>
									<Image Url="./Imagenes/Delete.png" Height="16px" Width="16px"></Image>
                                    <ButtonStyle Cursor="Default">
                                    </ButtonStyle>
                                    <InnerBorder WidthLeft="5px" WidthRight="5px" />
								</Appearance>
                                <DisabledAppearance>
                                    <ButtonStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False">
                                    </ButtonStyle>
                                </DisabledAppearance>
                                <HoverAppearance>
                                    <ButtonStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False">
                                    </ButtonStyle>
                                </HoverAppearance>
                                <FocusAppearance>
                                    <ButtonStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False">
                                    </ButtonStyle>
                                </FocusAppearance>
                                <PressedAppearance>
                                    <ButtonStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False">
                                    </ButtonStyle>
                                </PressedAppearance>
							</igtxt:webimagebutton>&nbsp;&nbsp;&nbsp;&nbsp; </P></TD></TR></TBODY></TABLE> 

   </div>
    </form>
</body>
</html>