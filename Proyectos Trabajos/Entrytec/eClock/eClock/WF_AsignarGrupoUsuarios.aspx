<%@ Page language="c#"  CodeFile="WF_AsignarGrupoUsuarios.aspx.cs" AutoEventWireup="false" Inherits="eClock.WF_AsignarGrupoUsuarios" %>

<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register TagPrefix="uc1" TagName="WC_Menu" Src="WC_Menu.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Importar Empleados</title>
</head>
<body style="font-size: 11px; font-family: tahoma; text-align: center; margin: 0px; ">
    <form id="form1" runat="server" >
    <div>
        <table width="600" style="height: 380px">
				<tr>
					<td style="WIDTH: 100%;" colSpan="2" align="right">
                        <asp:CheckBox ID="CBListado" runat="server" AutoPostBack="false"
                            Font-Size="Small" Text="Aplicar a todo el Listado" /></TD>
				</tr>
				<tr>
					<td style="HEIGHT: 17px"></TD>
					<td style="FONT-SIZE: 10pt; WIDTH: 100%; HEIGHT: 6.62%"
						align="center">Seleccione&nbsp;el/los grupo(s) que desea Asignar</TD>
				</tr>
				<tr>
					<td style="WIDTH: 75%; HEIGHT: 300px" valign="top">
						<igtbl:ultrawebgrid id="Grid" runat="server" Width="100%" Height="100%" ImageDirectory="/ig_common/Images/"
							UseAccessibleHeader="False" OnInitializeLayout="Grid_InitializeLayout">
							<DisplayLayout ColWidthDefault="" AllowSortingDefault="OnClient" RowHeightDefault="20px" Version="3.00"
								GridLinesDefault="Horizontal" SelectTypeRowDefault="Extended" ViewType="Hierarchical" HeaderClickActionDefault="SortSingle"
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
								<HeaderStyleDefault BackgroundImage="images/GridTitulo.gif" Font-Size="X-Small" Font-Names="" Font-Bold="True" BorderColor="Black"
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
								<igtbl:UltraGridBand>
									
                                    <AddNewRow View="NotSet" Visible="NotSet">
                                    </AddNewRow>
								</igtbl:UltraGridBand>
							</Bands>
						</igtbl:ultrawebgrid></TD>
					<TD style="WIDTH: 100%; HEIGHT: 320px;" valign="middle">
						<asp:Panel id="Panel1" runat="server" Height="100%" Width="100%">
                            <igmisc:webpanel id="WebPanel2" runat="server" backcolor="White" bordercolor="SteelBlue"
                                borderstyle="Outset" borderwidth="2px" font-bold="True" font-names="ARIAL" forecolor="Black"
                                height="300px" stylesetname="PaneleClock" width="100%">
<PanelStyle BorderStyle="Solid" ForeColor="Black" BorderWidth="1px" Font-Names="Arial">
<BorderDetails ColorTop="0, 45, 150" ColorLeft="158, 190, 245" ColorBottom="0, 45, 150" ColorRight="0, 45, 150"></BorderDetails>

<Padding Top="5px" Left="5px" Bottom="5px" Right="5px"></Padding>
</PanelStyle>

<Header TextAlignment="Left">
<ExpandedAppearance>
<Styles BackgroundImage="./images/GridTitulo.gif" BorderStyle="Ridge" ForeColor="Black" BorderWidth="1px" BorderColor="Transparent" Height="15px" Font-Size="9pt" Font-Names="Arial">
<BorderDetails ColorTop="158, 190, 245" WidthBottom="0px" ColorLeft="158, 190, 245" ColorRight="0, 45, 150"></BorderDetails>

<Padding Top="1px" Left="4px" Bottom="1px"></Padding>
</Styles>
</ExpandedAppearance>

<HoverAppearance>
<Styles CssClass="igwpHeaderHoverBlue2k7"></Styles>
</HoverAppearance>

<CollapsedAppearance>
<Styles CssClass="igwpHeaderCollapsedBlue2k7"></Styles>
</CollapsedAppearance>

<ExpansionIndicator Height="0px" Width="0px"></ExpansionIndicator>
</Header>
<Template>
<IFRAME src="WF_Grupos1Control.aspx" width="100%" height="100%"></IFRAME>&nbsp;
</Template>
</igmisc:webpanel>
						</asp:Panel>
                        &nbsp;
					</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 76.09%; HEIGHT: 0%" colSpan="2" align="center">
						<asp:Label id="LError" runat="server" ForeColor="#CC0033" Font-Names="" Font-Size="Smaller"></asp:Label>
						<asp:Label id="LCorrecto" runat="server" ForeColor="Green" Font-Names="" Font-Size="Smaller"></asp:Label></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 76.09%; HEIGHT: 35px" colSpan="2" align="center">&nbsp;
						<igtxt:WebImageButton id="BBRegresar" runat="server" Height="22px" Width="140px" UseBrowserDefaults="False"
							Text="Regresar" ImageTextSpacing="4">
							<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
							<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
								RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
							<Appearance>
								<Image Url="./Imagenes/Back.png" Height="16px" Width="16px"></Image>
                                <Style>
<Padding Top="4px"></Padding>
</Style>
							</Appearance>
						</igtxt:WebImageButton>&nbsp;&nbsp;&nbsp;&nbsp;
						<igtxt:WebImageButton id="WebImageButton2" runat="server" Height="22px" Width="140px" Text="Asignar Grupos"
							UseBrowserDefaults="False" ImageTextSpacing="4">
							<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
							<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
								RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
							<Appearance>
								<Image Url="./Imagenes/Assign.png" Height="16px" Width="16px"></Image>
                                <Style>
<Padding Top="4px"></Padding>
</Style>
							</Appearance>
						</igtxt:WebImageButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<igtxt:WebImageButton id="WebImageButton1" runat="server" Height="22px" Width="140px" UseBrowserDefaults="False"
							Text="Quitar Grupos" ImageTextSpacing="4">
							<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
							<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
								RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
							<Appearance>
								<Image Url="./Imagenes/Remover.png" Height="16px" Width="16px"></Image>
                                <Style>
<Padding Top="4px"></Padding>
</Style>
							</Appearance>
						</igtxt:WebImageButton></TD>
				</TR>
			</TABLE>
   </div>
    </form>
</body>
</html>