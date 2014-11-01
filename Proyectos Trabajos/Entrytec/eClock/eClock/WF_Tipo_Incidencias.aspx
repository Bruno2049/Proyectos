<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register TagPrefix="uc1" TagName="WC_Menu" Src="WC_Menu.ascx" %>
<%@ Page language="c#"   CodeFile="WF_Tipo_Incidencias.aspx.cs" AutoEventWireup="True" Inherits="eClock.WF_Tipo_Incidencias" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.DocumentExport.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebGrid.DocumentExport" TagPrefix="igtbldocexp" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Edicion de Incidencias</title>
</head>
<body style="font-size: 11px; font-family: tahoma; text-align: center; margin: 0px; ">
    <form id="form1" runat="server" >
    <div>
        <table width="600" style="height: 380px">
				
				<TR>
					<TD style="HEIGHT: 0%"></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 0%" align="right"><asp:checkbox id="IncidenciasCheckBox1" runat="server" Font-Size="Smaller" Font-Names="Arial Narrow"
							AutoPostBack="True" Text="Ver Incidencias Borradas" oncheckedchanged="IncidenciasCheckBox1_CheckedChanged"></asp:checkbox></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 200px" align="center">
						<igtbl:ultrawebgrid id=Grid runat="server" Width="100%" style="left: 0px; top: 0px">
							<DisplayLayout ColWidthDefault="" AllowSortingDefault="OnClient" RowHeightDefault="20px" Version="3.00"
								GridLinesDefault="Horizontal" SelectTypeRowDefault="Extended" HeaderClickActionDefault="SortSingle"
								BorderCollapseDefault="Separate" AllowColSizingDefault="Free" CellPaddingDefault="4"
								Name="Grid" TableLayout="Fixed" CellClickActionDefault="RowSelect" AllowUpdateDefault="Yes" RowSizingDefault="Free">
								<AddNewBox>
                                    <BoxStyle BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                        <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                    </BoxStyle>
								</AddNewBox>
								<Pager PageSize="20">
                                    <PagerStyle BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                        <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                    </PagerStyle>
								</Pager>
								<HeaderStyleDefault BackgroundImage="images/GridTitulo.gif" Font-Size="X-Small" Font-Names="Arial" Font-Bold="True" BorderColor="Black"
									HorizontalAlign="Left" ForeColor="White" BackColor="Navy" >
									<BorderDetails WidthLeft="0px" WidthTop="0px" WidthRight="1px" WidthBottom="1px"></BorderDetails>
								</HeaderStyleDefault>
								<FrameStyle Width="100%" Font-Names="Arial Narrow" BorderColor="Black" ForeColor="#759AFD"></FrameStyle>
								<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
									<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
								</FooterStyleDefault>
								<ActivationObject BorderStyle="Dotted" BorderColor="1, 68, 208" BorderWidth=""></ActivationObject>
								<GroupByBox Prompt="Arrastre la columna que desea agrupar...">
                                    <BoxStyle BackColor="LightSteelBlue" BorderColor="Window" ForeColor="Navy">
                                    </BoxStyle>
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
								<igtbl:UltraGridBand AddButtonCaption="EC_TIPO_INCIDENCIAS" BaseTableName="EC_TIPO_INCIDENCIAS" Key="EC_TIPO_INCIDENCIAS"
									DataKeyField="TIPO_INCIDENCIA_ID">
									<Columns>
										<igtbl:UltraGridColumn Key="TIPO_INCIDENCIA_ID" IsBound="True" Hidden="True"
											DataType="System.Decimal" BaseColumnName="TIPO_INCIDENCIA_ID">
											<Header Caption="TIPO_INCIDENCIA_ID"></Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn Key="TIPO_INCIDENCIA_NOMBRE" IsBound="True"
											Width="400px" BaseColumnName="TIPO_INCIDENCIA_NOMBRE">
											<Footer>
												<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
											</Footer>
											<Header Caption="Nombre de la Incidencia">
												<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
											</Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn Key="TIPO_INCIDENCIA_ABR" IsBound="True" Width="100px"
											BaseColumnName="TIPO_INCIDENCIA_ABR">
											<Footer>
												<RowLayoutColumnInfo OriginX="2"></RowLayoutColumnInfo>
											</Footer>
											<Header Caption="Abreviatura">
												<RowLayoutColumnInfo OriginX="2"></RowLayoutColumnInfo>
											</Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn Key="TIPO_INCIDENCIA_BORRADO" IsBound="True"
											Hidden="True" DataType="System.Decimal" BaseColumnName="TIPO_INCIDENCIA_BORRADO">
											<Footer>
												<RowLayoutColumnInfo OriginX="3"></RowLayoutColumnInfo>
											</Footer>
											<Header Caption="TIPO_INCIDENCIA_BORRADO">
												<RowLayoutColumnInfo OriginX="3"></RowLayoutColumnInfo>
											</Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn Key="STATUS" IsBound="True" Width="100px" BaseColumnName="STATUS">
											<Footer>
												<RowLayoutColumnInfo OriginX="4"></RowLayoutColumnInfo>
											</Footer>
											<Header Caption="Estatus">
												<RowLayoutColumnInfo OriginX="4"></RowLayoutColumnInfo>
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
					<TD style="HEIGHT: 0%" align="center"><asp:label id="LError" runat="server" Font-Size="Smaller" Font-Names="Arial Narrow" ForeColor="#CC0033"></asp:label><asp:label id="LCorrecto" runat="server" Font-Size="Smaller" Font-Names="Arial Narrow" ForeColor="#00C000"></asp:label></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 0%" align="center">
                        <igtxt:WebImageButton ID="btn_Actualizar" runat="server" Height="17px" ImageTextSpacing="4"
                            OnClick="btn_Actualizar_Click" ToolTip="Actualiza el listado de empleados con respecto al Sistema de Nomina"
                            UseBrowserDefaults="False" Width="30px">
                            <Appearance>
                                <Image Height="16px" Url="./Imagenes/gtk-refresh.png" Width="16px" />
                                <Style>
<Padding Top="4px"></Padding>
</Style>
                            </Appearance>
                            <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                                HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400"
                                PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                            <Alignments HorizontalAll="NotSet" VerticalAll="NotSet" VerticalImage="Middle" />
                        </igtxt:WebImageButton>
                        &nbsp;&nbsp;&nbsp;
						<igtxt:webimagebutton id="BBorrarTipoIncidencia" runat="server" Text="Borrar" Width="100px" Height="22px"
							UseBrowserDefaults="False" ImageTextSpacing="4">
							<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
							<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
								RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
							<Appearance>
								<Style Cursor="Default">
								</Style>
								<Image Url="./Imagenes/Delete.png" Height="16px" Width="16px"></Image>
							</Appearance>
						</igtxt:webimagebutton>&nbsp;&nbsp;&nbsp;
						<igtxt:webimagebutton id="BAgregarTipoIncidencia" runat="server" Text="Nuevo" Width="100px" Height="22px"
							UseBrowserDefaults="False" ImageTextSpacing="4" OnClick="BAgregarTipoIncidencia_Click1">
							<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
							<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
								RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
							<Appearance>
								<Style Cursor="Default">
								</Style>
								<Image Url="./Imagenes/New.png" Height="16px" Width="16px"></Image>
							</Appearance>
						</igtxt:webimagebutton>&nbsp;&nbsp;&nbsp;
						<igtxt:webimagebutton id="BEditarTipoIncidencia" runat="server" Text="Editar" Width="100px" Height="22px"
							UseBrowserDefaults="False" ImageTextSpacing="4">
							<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
							<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
								RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
							<Appearance>
								<Style Cursor="Default">
								</Style>
								<Image Url="./Imagenes/Edit.png" Height="16px" Width="16px"></Image>
							</Appearance>
						</igtxt:webimagebutton>&nbsp;&nbsp;&nbsp;
                        <igtxt:WebImageButton ID="btImprimir" runat="server" Height="22px" ImageTextSpacing="4"
                            OnClick="btImprimir_Click" Text="Imprimir" UseBrowserDefaults="False" Width="100px">
                            <Appearance>
                                <Image Height="16px" Url="./Imagenes/printer-inkjet.png" Width="16px" />
                                <ButtonStyle Cursor="Default">
                                </ButtonStyle>
                            </Appearance>
                            <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                                HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400"
                                PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                            <Alignments VerticalAll="Bottom" VerticalImage="Middle" />
                        </igtxt:WebImageButton>
					</TD>
				</TR>
			</TABLE>
 </div>
        <igtbldocexp:ultrawebgriddocumentexporter id="GridExporter" runat="server" onbeginexport="GridExporter_BeginExport">
        </igtbldocexp:ultrawebgriddocumentexporter>
    </form>
</body>
</html>

