<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WF_PersonasNoRegistradas.aspx.cs" Inherits="WF_PersonasNoRegistradas" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.WebUI.UltraWebGrid" tagprefix="igtbl" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.WebUI.WebDataInput" tagprefix="igtxt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            height: 30px;
        }
        .style2
        {
            height: 310px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table width="100%" style="height: 380px">
						<TBODY>
							<TR>
								<TD style="HEIGHT: 0%">
									Empleados registrados en la terminal pero no en el software, creelos manualmente desde empleados o bien 
                                    borre estos registros.</TD>
							</TR>
							<TR>
								<TD class="style2" align="center">
                                        <igtbl:UltraWebGrid ID="Grid" runat="server" 
                                            OnInitializeDataSource = "UltraWebGrid1_InitializeDataSource" 
                                            OnInitializeLayout="UltraWebGrid1_InitializeLayout" Width="100%">
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
                                                    BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt"
                                                    Width="100%">
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
                                        </igtbl:UltraWebGrid>
								</TD>
							</TR>
							<TR>
								<TD align="center" style="HEIGHT: 0%"><FONT face="Arial" size="3">
										<asp:Label id="LError" runat="server" ForeColor="#CC0033" Font-Names="Arial Narrow" Font-Size="Smaller"></asp:Label>
										<asp:Label id="LCorrecto" runat="server" ForeColor="Green" Font-Names="Arial Narrow" Font-Size="Smaller"></asp:Label></FONT></TD>
							</TR>
							<TR>
								<TD align="center" class="style1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<igtxt:WebImageButton id="BBorrarUsuarios" runat="server" Height="22px" 
                                        Text="Borrar el registro" UseBrowserDefaults="False"
										Width="165px" OnClick="BBorrarUsuarios_Click" ImageTextSpacing="4">
										<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
										<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
											RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
										<Appearance>
											<Style Cursor="Default">
											</Style>
											<Image Url="./Imagenes/Delete.png" Height="16px" Width="16px"></Image>

<ButtonStyle Cursor="Default"></ButtonStyle>
										</Appearance>
									</igtxt:WebImageButton>&nbsp;&nbsp;&nbsp;&nbsp;
									<igtxt:WebImageButton id="WebImageButton1" runat="server" 
                                        Text="Crear el empleado" UseBrowserDefaults="False" Width="154px" 
                                        ImageTextSpacing="4" Height="22px" onclick="WebImageButton1_Click" 
                                        Visible="False">
										<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
										<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
											RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
										<Appearance>
											<Style Cursor="Default" BackColor="Transparent">
											</Style>
											<Image Height="16px" Width="16px" Url="./Imagenes/New.png"></Image>

<ButtonStyle Cursor="Default" BackColor="Transparent"></ButtonStyle>
										</Appearance>
									</igtxt:WebImageButton>&nbsp;&nbsp;&nbsp;
									&nbsp;&nbsp;&nbsp;&nbsp;<igtxt:webimagebutton id="Btn_Recalcular" 
                                        runat="server" Text="Recalcular" Width="150px" Height="22px" 
                                        ImageTextSpacing="4" onclick="Btn_Recalcular_Click">
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
							</igtxt:webimagebutton></TD>
							</TR>
						</TBODY>
					</TABLE>
    
    </div>
    </form>
</body>
</html>
