<%@ Register TagPrefix="uc1" TagName="WC_Menu" Src="WC_Menu.ascx" %>
<%@ Page language="c#" CodeFile="WF_Dias_Festivos.aspx.cs" AutoEventWireup="True" Inherits="eClock.WF_Dias_Festivos" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.DocumentExport.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebGrid.DocumentExport" TagPrefix="igtbldocexp" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Días Festivos</title>
    <style type="text/css">
        .style1
        {
            height: 35px;
        }
        .style2
        {
            height: 310px;
        }
    </style>
</head>
<body style="font-size: 11px; font-family: tahoma; text-align: center; margin: 0px; ">
    <form id="form1" runat="server" >
    <div>
        <table width="600" style="height: 380px">
				<TR>
					<TD style="HEIGHT: 0%"></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 0%" align="right">
						<asp:CheckBox id="DiasCheckBox1" runat="server" Font-Names="arial narrow" Font-Size="Small"
							Text="Ver dias Festivos Borrados" AutoPostBack="True" oncheckedchanged="CheckBox1_CheckedChanged"></asp:CheckBox></TD>
				</TR>
				<TR>
					<TD align="center" class="style2">
						<igtbl:ultrawebgrid id=Grid runat="server" Width="100%" 
                            OnInitializeLayout="Grid_InitializeLayout" style="left: 1px; top: 1px" 
                            oninitializedatasource="Grid_InitializeDataSource">
							<DisplayLayout ColWidthDefault="" RowHeightDefault="20px" Version="3.00"
								GridLinesDefault="Horizontal" SelectTypeRowDefault="Extended"
								BorderCollapseDefault="Separate" CellPaddingDefault="4" RowSelectorsDefault="No"
								Name="Grid" TableLayout="Fixed" CellClickActionDefault="RowSelect" AllowUpdateDefault="Yes">
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
									HorizontalAlign="Left" ForeColor="White" BackColor="Navy"  BorderStyle="Solid">
									<BorderDetails WidthLeft="0px" WidthTop="0px" WidthRight="1px" WidthBottom="1px" ColorLeft="White" ColorTop="White"></BorderDetails>
								</HeaderStyleDefault>
								<FrameStyle Width="100%" Font-Names="Arial Narrow" BorderColor="Black" 
                                    BorderStyle="Solid" BorderWidth="1px" Font-Size="8pt"></FrameStyle>
								<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
									<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
								</FooterStyleDefault>
								<ActivationObject BorderStyle="Dotted" BorderColor="1, 68, 208" BorderWidth=""></ActivationObject>
								<GroupByBox Prompt="Arrastre la columna que desea agrupar...">
                                    <BoxStyle BackColor="LightSteelBlue" BorderColor="Window" ForeColor="Navy">
                                    </BoxStyle>
								</GroupByBox>
								<SelectedRowStyleDefault BackgroundImage="images/Office2003SelRow.png" ForeColor="WhiteSmoke" >
									<BorderDetails WidthLeft="0px" StyleBottom="Solid" ColorBottom="Black" WidthTop="0px" WidthRight="0px"
										StyleTop="None" StyleRight="None" WidthBottom="1px" StyleLeft="None"></BorderDetails>
								</SelectedRowStyleDefault>
								<RowAlternateStyleDefault ForeColor="Black" BackColor="WhiteSmoke"></RowAlternateStyleDefault>
								<RowStyleDefault BorderColor="Black" ForeColor="White" BackColor="CornflowerBlue" Font-Names="Verdana" Font-Size="8pt" BorderStyle="Solid" BorderWidth="1px">
									<BorderDetails WidthLeft="0px" WidthTop="0px" ColorLeft="CornflowerBlue" ColorTop="CornflowerBlue"></BorderDetails>
                                    <Padding Left="3px" />
								</RowStyleDefault>
                                <Images>
                                    <CollapseImage Url="ig_tblcrm_rowarrow_down.gif" />
                                    <ExpandImage Url="ig_tblcrm_rowarrow_right.gif" />
                                </Images>
                                <EditCellStyleDefault BorderStyle="None" BorderWidth="0px">
                                </EditCellStyleDefault>
							</DisplayLayout>
							<Bands>
								<igtbl:UltraGridBand AddButtonCaption="EC_DIAS_FESTIVOS" BaseTableName="EC_DIAS_FESTIVOS" Key="EC_DIAS_FESTIVOS"
									DataKeyField="DIA_FESTIVO_ID">
									<Columns>
										<igtbl:UltraGridColumn Key="DIA_FESTIVO_ID" IsBound="True" Hidden="True" DataType="System.Decimal"
											BaseColumnName="DIA_FESTIVO_ID">
											<Header Caption="DIA_FESTIVO_ID"></Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn Key="DIA_FESTIVO_FECHA" IsBound="True" DataType="System.DateTime"
											BaseColumnName="DIA_FESTIVO_FECHA">
											<Footer>
												<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
											</Footer>
											<Header Caption="Fecha">
												<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
											</Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn Key="DIA_FESTIVO_NOMBRE" IsBound="True" BaseColumnName="DIA_FESTIVO_NOMBRE">
											<Footer>
												<RowLayoutColumnInfo OriginX="2"></RowLayoutColumnInfo>
											</Footer>
											<Header Caption="Nombre de dia festivo">
												<RowLayoutColumnInfo OriginX="2"></RowLayoutColumnInfo>
											</Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn Key="DIA_FESTIVO_BORRADO" IsBound="True" Hidden="True"
											DataType="System.Decimal" BaseColumnName="DIA_FESTIVO_BORRADO">
											<Footer>
												<RowLayoutColumnInfo OriginX="3"></RowLayoutColumnInfo>
											</Footer>
											<Header Caption="DIA_FESTIVO_BORRADO">
												<RowLayoutColumnInfo OriginX="3"></RowLayoutColumnInfo>
											</Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn Key="STATUS" IsBound="True" BaseColumnName="STATUS">
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
					<TD style="HEIGHT: 0%" align="center"><asp:label id="LError" runat="server" Font-Size="Smaller" Font-Names="Arial Narrow" ForeColor="Red"></asp:label><asp:label id="LCorrecto" runat="server" Font-Size="Smaller" Font-Names="Arial Narrow" ForeColor="Green"></asp:label></TD>
				</TR>
				<TR>
					<TD align="center" class="style1"><igtxt:webimagebutton id="BBorrarDiaFestivo" 
                            runat="server" Height="22px" UseBrowserDefaults="False" Text="Borrar"
							Width="100px" ImageTextSpacing="4" onclick="BBorrarDiaFestivo_Click">
							<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
							<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
								RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
							<Appearance>
								<Style Cursor="Default">
								</Style>
								<Image Url="./Imagenes/Delete.png" Height="16px" Width="16px"></Image>
							</Appearance>
						</igtxt:webimagebutton>&nbsp;&nbsp;&nbsp;
						<igtxt:webimagebutton id="BAgregarDiaFestivo" runat="server" Height="22px" UseBrowserDefaults="False"
							Text="Nuevo" Width="100px" ImageTextSpacing="4" onclick="BAgregarDiaFestivo_Click">
							<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
							<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
								RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
							<Appearance>
								<Style Cursor="Default">
								</Style>
								<Image Url="./Imagenes/New.png" Height="16px" Width="16px"></Image>
							</Appearance>
						</igtxt:webimagebutton>&nbsp;&nbsp;&nbsp;
						<igtxt:webimagebutton id="BEditarDiaFestivo" runat="server" Height="22px" 
                            UseBrowserDefaults="False" Text="Editar"
							Width="100px" ImageTextSpacing="4" onclick="BEditarDiaFestivo_Click">
							<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
							<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
								RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
							<Appearance>
								<Style Cursor="Default">
								</Style>
								<Image Url="./Imagenes/Edit.png" Height="16px" Width="16px"></Image>
							</Appearance>
						</igtxt:webimagebutton>
                        &nbsp; &nbsp;
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
        <igtbldocexp:ultrawebgriddocumentexporter id="GridExporter" runat="server" 
        onbeginexport="GridExporter_BeginExport" DataExportMode="DataInGridOnly">
        </igtbldocexp:ultrawebgriddocumentexporter>
    </form>
</body>
</html>