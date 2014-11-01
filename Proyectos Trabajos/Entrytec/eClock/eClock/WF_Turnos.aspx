<%@ Register TagPrefix="uc1" TagName="WC_Menu" Src="WC_Menu.ascx" %>
<%@ Page language="c#" CodeFile="WF_Turnos.aspx.cs" AutoEventWireup="True" Inherits="eClock.WF_Turnos1" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.DocumentExport.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebGrid.DocumentExport" TagPrefix="igtbldocexp" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
</head>
<body style="font-size: 11px; font-family: tahoma; text-align: center; margin: 0px; ">
    <form id="form1" runat="server">
    <div>
        <table width="710" style="height: 400px">
				<TR>
					<TD style="HEIGHT: 0%" align="right">
						<asp:CheckBox id="TurnosCheckBox1" runat="server" Text="Ver Turnos Borrados" AutoPostBack="True"
							Font-Size="Small" Font-Names="Arial Narrow" oncheckedchanged="TurnosCheckBox1_CheckedChanged"></asp:CheckBox></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 200px" align="center">
                        <igtbl:UltraWebGrid ID="Grid" runat="server" Browser="Xml" Height="200px" OnInitializeLayout="Grid_InitializeLayout" OnInitializeDataSource="Grid_InitializeDataSource" Width="100%">
                            <Bands>
                                <igtbl:UltraGridBand>
                                    <AddNewRow View="NotSet" Visible="NotSet">
                                    </AddNewRow>
                                </igtbl:UltraGridBand>
                            </Bands>
                            <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowSortingDefault="OnClient"
                                AllowUpdateDefault="Yes" BorderCollapseDefault="Separate" HeaderClickActionDefault="SortMulti"
                                LoadOnDemand="Xml" Name="Grid" RowHeightDefault="20px" RowSelectorsDefault="No"
                                SelectTypeRowDefault="Single" StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True"
                                TableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy">
                                <GroupByBox>
                                    <Style BackColor="ActiveBorder" BorderColor="Window"></Style>
                                </GroupByBox>
                                <GroupByRowStyleDefault BackColor="Control" BorderColor="Window">
                                </GroupByRowStyleDefault>
                                <ActivationObject BorderColor="" BorderWidth="">
                                </ActivationObject>
                                <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                </FooterStyleDefault>
                                <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Names="Microsoft Sans Serif" Font-Size="8.25pt">
                                    <BorderDetails ColorLeft="Window" ColorTop="Window" />
                                    <Padding Left="3px" />
                                </RowStyleDefault>
                                <FilterOptionsDefault>
                                    <FilterOperandDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid"
                                        BorderWidth="1px" CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                        Font-Size="11px">
                                        <Padding Left="2px" />
                                    </FilterOperandDropDownStyle>
                                    <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                    </FilterHighlightRowStyle>
                                    <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                        CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                        Font-Size="11px" Height="300px" Width="200px">
                                        <Padding Left="2px" />
                                    </FilterDropDownStyle>
                                </FilterOptionsDefault>
                                <HeaderStyleDefault BackgroundImage="images/GridTitulo.gif" BackColor="LightGray" BorderStyle="Solid" HorizontalAlign="Left">
                                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                </HeaderStyleDefault>
                                <EditCellStyleDefault BorderStyle="None" BorderWidth="0px">
                                </EditCellStyleDefault>
                                <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                    BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="200px"
                                    Width="100%">
                                </FrameStyle>
                                <Pager MinimumPagesForDisplay="2">
                                    
                                </Pager>
                                <AddNewBox>
                                    <Style BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid" BorderWidth="1px">
<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
</Style>
                                </AddNewBox>
                            </DisplayLayout>
                        </igtbl:UltraWebGrid></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 0%" align="center">
						<asp:Label id="LError" runat="server" ForeColor="Red" Font-Names="Arial Narrow" Font-Size="Smaller"></asp:Label>
						<asp:Label id="LCorrecto" runat="server" ForeColor="Green" Font-Names="Arial Narrow" Font-Size="Smaller"></asp:Label></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 0%" align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<igtxt:webimagebutton id="BBorrarTurno" runat="server" Height="22px" Text="Borrar" UseBrowserDefaults="False"
							Width="100px" ImageTextSpacing="4" OnClick="BBorrarTurno_Click1">
							<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
							<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
								RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
							<Appearance>
								<Style Cursor="Default">
								</Style>
								<Image Url="./Imagenes/Delete.png" Height="16px" Width="16px"></Image>
							</Appearance>
						</igtxt:webimagebutton>&nbsp;&nbsp;&nbsp;
						<igtxt:webimagebutton id="BAgregarTurno" runat="server" Height="22px" Text="Nuevo" UseBrowserDefaults="False"
							Width="100px" ImageTextSpacing="4" OnClick="BAgregarTurno_Click1">
							<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
							<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
								RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
							<Appearance>
								<Style Cursor="Default">
								</Style>
								<Image Url="./Imagenes/New.png" Height="16px" Width="16px"></Image>
							</Appearance>
						</igtxt:webimagebutton>&nbsp;&nbsp;&nbsp; &nbsp;<igtxt:webimagebutton id="BtnDuplicar" runat="server" Height="22px" Text="Duplicar" UseBrowserDefaults="False"
							Width="100px" OnClick="Webimagebutton1_Click" ImageTextSpacing="4">
                            <Alignments VerticalImage="Middle" VerticalAll="Bottom" />
                            <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                                HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400"
                                PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                            <Appearance>
                                <Image Url="./Imagenes/Duplicate.png" Height="16px" Width="16px" />
                                <ButtonStyle Cursor="Default">
                                </ButtonStyle>
                            </Appearance>
                        </igtxt:WebImageButton>
                        &nbsp; &nbsp;
						<igtxt:webimagebutton id="BEditarTurno" runat="server" Height="22px" Text="Editar" UseBrowserDefaults="False"
							Width="100px" ImageTextSpacing="4" OnClick="BEditarTurno_Click1">
							<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
							<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
								RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
							<Appearance>
                                <ButtonStyle Cursor="Default">
                                </ButtonStyle>
								<Image Url="./Imagenes/Edit.png" Height="16px" Width="16px"></Image>
							</Appearance>
						</igtxt:webimagebutton>
                        &nbsp;&nbsp;
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