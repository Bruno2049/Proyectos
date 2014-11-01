<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register TagPrefix="uc1" TagName="WC_Menu" Src="WC_Menu.ascx" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Page language="c#"   CodeFile="WF_Usuarios.aspx.cs" AutoEventWireup="True" Inherits="eClock.WF_Usuarios" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.DocumentExport.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebGrid.DocumentExport" TagPrefix="igtbldocexp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Usuarios</title>
    <style type="text/css">
        .style1
        {
            height: 380px;
            width: 606px;
        }
        .style2
        {
            height: 23px;
            width: 151px;
        }
    </style>
</head>
<body style="font-size: 11px; font-family: tahoma; text-align: center; margin: 0px; ">
    <form id="form1" runat="server" >
    <div style="margin:10px 50px 10 px 50px">
        <table width="600" class="style1">
						<TBODY>
							<TR>
								<TD style="HEIGHT: 0%" colspan="4">
									</TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 0%" align="right" colspan="4">
									<asp:CheckBox id="UsuariosCheckBox1" runat="server" Font-Size="Small" Font-Names="Arial Narrow"
										Text="Ver Usuarios Borrados" AutoPostBack="True" oncheckedchanged="UsuariosCheckBox1_CheckedChanged"></asp:CheckBox></TD>
							</TR>
							<TR>
								<TD height="100%" colspan="4">
									<P align="center">
                                        <igtbl:UltraWebGrid ID="Grid" runat="server" 
                                            OnInitializeDataSource = "UltraWebGrid1_InitializeDataSource" 
                                            OnInitializeLayout="UltraWebGrid1_InitializeLayout" Height="100%" 
                                            Width="800px">
                                            <Bands>
                                                <igtbl:UltraGridBand>
                                                    <AddNewRow View="NotSet" Visible="NotSet">
                                                    </AddNewRow>
                                                </igtbl:UltraGridBand>
                                            </Bands>
                                            <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" BorderCollapseDefault="Separate"
                                                HeaderClickActionDefault="SortMulti" Name="UltraWebGrid1" RowHeightDefault="20px"
                                                RowSelectorsDefault="No" SelectTypeRowDefault="Extended" StationaryMargins="Header"
                                                StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy">
                                                <GroupByBox>
                                                    <Style BackColor="ActiveBorder" BorderColor="Window"></Style>
<BoxStyle BackColor="ActiveBorder" BorderColor="Window"></BoxStyle>
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
                                                    BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="100%"
                                                    Width="800px">
                                                </FrameStyle>
                                                <Pager MinimumPagesForDisplay="2">
                                                    <Style BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
</Style>
<PagerStyle BackColor="LightGray" BorderWidth="1px" BorderStyle="Solid">
<BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px"></BorderDetails>
</PagerStyle>
                                                </Pager>
                                                <AddNewBox Hidden="False">
                                                    <Style BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid" BorderWidth="1px">
<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
</Style>
<BoxStyle BackColor="Window" BorderColor="InactiveCaption" BorderWidth="1px" BorderStyle="Solid">
<BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px"></BorderDetails>
</BoxStyle>
                                                </AddNewBox>
                                            </DisplayLayout>
                                        </igtbl:UltraWebGrid>&nbsp;</P>
								</TD>
							</TR>
							<TR>
								<TD align="center" style="HEIGHT: 0%" colspan="4"><FONT face="Arial" size="3">
										<asp:Label id="Lbl_Error" runat="server" ForeColor="#CC0033" 
                                        Font-Names="Arial Narrow" Font-Size="Smaller"></asp:Label>
										<asp:Label id="Lbl_Correcto" runat="server" ForeColor="Green" 
                                        Font-Names="Arial Narrow" Font-Size="Smaller"></asp:Label></FONT></TD>
							</TR>
							<TR>
								<TD align="center" class="style2">
									<igtxt:WebImageButton id="WIBtn_Borrar" runat="server" Height="22px" 
                                        Text="Borrar" UseBrowserDefaults="False"
										Width="100px" OnClick="BBorrarUsuarios_Click" ImageTextSpacing="4">
										<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
										<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
											RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
										<Appearance>
											<Style Cursor="Default">
											</Style>
											<Image Url="./Imagenes/Delete.png" Height="16px" Width="16px"></Image>
										</Appearance>
									</igtxt:WebImageButton></TD>
								<TD align="center" class="style2">
									<igtxt:WebImageButton id="WIBtn_Nuevo" runat="server" Text="Nuevo" 
                                        UseBrowserDefaults="False" Width="100px" ImageTextSpacing="4" Height="22px" 
                                        onclick="WIBtn_Nuevo_Click">
										<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
										<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
											RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
										<Appearance>
											<Style Cursor="Default" BackColor="Transparent">
											</Style>
											<Image Height="16px" Width="16px" Url="./Imagenes/New.png"></Image>
										</Appearance>
									</igtxt:WebImageButton>
                                </TD>
								<TD align="center" class="style2">
									<igtxt:WebImageButton id="WIBtn_Editar" runat="server" Height="22px" 
                                        Text="Editar" UseBrowserDefaults="False"
										Width="100px" OnClick="WIBtn_Editar_Click" ImageTextSpacing="4">
										<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
										<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
											RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
										<Appearance>
											<Style Cursor="Default">
											</Style>
											<Image Url="./Imagenes/Edit.png" Height="16px" Width="16px"></Image>
										</Appearance>
									</igtxt:WebImageButton>
                                </TD>
								<TD align="center" class="style2"><igtxt:WebImageButton ID="WIBtn_Imprimir" 
                                        runat="server" Height="22px"
                                        ImageTextSpacing="4" OnClick="btImprimir_Click" Text="Imprimir" UseBrowserDefaults="False"
                                        Width="100px">
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
						</TBODY>
					</TABLE>
	 </div>
        <igtbldocexp:UltraWebGridDocumentExporter ID="GridExporter" runat="server" OnBeginExport="GridExporter_BeginExport">
        </igtbldocexp:UltraWebGridDocumentExporter>
    </form>
</body>
</html>

