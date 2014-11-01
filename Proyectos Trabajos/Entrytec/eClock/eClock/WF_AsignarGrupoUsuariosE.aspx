<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register TagPrefix="uc1" TagName="WC_Menu" Src="WC_Menu.ascx" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Page language="c#"  CodeFile="WF_AsignarGrupoUsuariosE.aspx.cs" AutoEventWireup="false" Inherits="eClock.WF_AsignarGrupoUsuariosE" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
</head>
<body style="font-size: 11px; font-family: tahoma; text-align: center; margin: 0px; ">
    <form id="form1" runat="server" >
    <div>
        <table width="600" style="height: 380px">
				<TR>
					<TD >
						</TD>
				</TR>
				<TR>
					<TD height="400">
						<igtbl:ultrawebgrid id=Grid runat="server" UseAccessibleHeader="False" ImageDirectory="/ig_common/Images/" DataMember="EC_PERMISOS_SUSCRIP_Asignados" DataSource="<%# dS_AsignaGrupoUsuario1 %>" Height="100%" Width="100%" OnInitializeLayout="Grid_InitializeLayout" style="left: 0px; top: 0px">
							<DisplayLayout ColWidthDefault="" AllowSortingDefault="OnClient" RowHeightDefault="20px" Version="3.00"
								GridLinesDefault="Horizontal" SelectTypeRowDefault="Extended" HeaderClickActionDefault="SortSingle"
								BorderCollapseDefault="Separate" AllowColSizingDefault="Free" CellPaddingDefault="4" RowSelectorsDefault="No"
								Name="Ultrawebgrid1" TableLayout="Fixed" CellClickActionDefault="RowSelect" AllowUpdateDefault="Yes">
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
								<igtbl:UltraGridBand AddButtonCaption="EC_PERMISOS_SUSCRIP_Asignados" BaseTableName="EC_PERMISOS_SUSCRIP_Asignados"
									Key="EC_PERMISOS_SUSCRIP_Asignados">
									<Columns>
										<igtbl:UltraGridColumn HeaderText="USUARIO_ID" Key="USUARIO_ID" IsBound="True" Hidden="True" DataType="System.Decimal"
											BaseColumnName="USUARIO_ID">
											<Footer Key="USUARIO_ID"></Footer>
											<Header Key="USUARIO_ID" Caption="USUARIO_ID"></Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="SUSCRIPCION_ID" Key="SUSCRIPCION_ID" IsBound="True" Hidden="True" DataType="System.Decimal"
											BaseColumnName="SUSCRIPCION_ID">
											<Footer Key="SUSCRIPCION_ID">
												<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
											</Footer>
											<Header Key="SUSCRIPCION_ID" Caption="SUSCRIPCION_ID">
												<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
											</Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="Nombre de grupo" Key="SUSCRIPCION_NOMBRE" IsBound="True" BaseColumnName="SUSCRIPCION_NOMBRE">
											<Footer Key="SUSCRIPCION_NOMBRE">
												<RowLayoutColumnInfo OriginX="2"></RowLayoutColumnInfo>
											</Footer>
											<Header Key="SUSCRIPCION_NOMBRE" Caption="Nombre de grupo">
												<RowLayoutColumnInfo OriginX="2"></RowLayoutColumnInfo>
											</Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="Borrar" Key="BORRADO" IsBound="True" Width="50px" Type="CheckBox" DataType="System.Decimal"
											BaseColumnName="BORRADO">
											<Footer Key="BORRADO">
												<RowLayoutColumnInfo OriginX="3"></RowLayoutColumnInfo>
											</Footer>
											<Header Key="BORRADO" Caption="Borrar">
												<RowLayoutColumnInfo OriginX="3"></RowLayoutColumnInfo>
											</Header>
										</igtbl:UltraGridColumn>
									</Columns>
									
								</igtbl:UltraGridBand>
							</Bands>
						</igtbl:ultrawebgrid></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 100%; HEIGHT: 0%" align="center">
						<asp:Label id="LCorrecto" runat="server" ForeColor="Green" Font-Names="Arial Narrow" Font-Size="Smaller"></asp:Label>
						<asp:Label id="LError" runat="server" ForeColor="#CC0033" Font-Names="Arial Narrow" Font-Size="Smaller"></asp:Label></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 100%; HEIGHT: 0%" align="center">
						<igtxt:WebImageButton id="WebImageButton2" runat="server" Text="Regresar" UseBrowserDefaults="False" Width="140px"
							Height="26px" ImageTextSpacing="4">
							<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
							<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
								RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
							<Appearance>
								<Image Url="./Imagenes/Back.png" Height="16px" Width="16px"></Image>
							</Appearance>
						</igtxt:WebImageButton>&nbsp;&nbsp;&nbsp;
						<igtxt:WebImageButton id="WebImageButton1" runat="server" Text="Guardar Cambios" UseBrowserDefaults="False"
							Width="140px" Height="26px" ImageTextSpacing="4">
							<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
							<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1o.gif" MaxWidth="400" HoverImageUrl="ig_butXP2o.gif"
								RenderingType="FileImages" PressedImageUrl="ig_butXP4o.gif" DisabledImageUrl="ig_butXP5o.gif" FocusImageUrl="ig_butXP3o.gif"></RoundedCorners>
							<Appearance>
								<Image Url="./Imagenes/Save_as.png" Height="16px" Width="16px"></Image>
							</Appearance>
						</igtxt:WebImageButton></TD>
				</TR>
			</TABLE>
   </div>
    </form>
</body>
</html>