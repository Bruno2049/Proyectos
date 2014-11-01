<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Page language="c#" CodeFile="WF_Personas_DiarioE.aspx.cs" AutoEventWireup="True" Inherits="eClock.WF_Personas_DiarioE" %>
<%@ Register TagPrefix="uc1" TagName="WC_Menu" Src="WC_Menu.ascx" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics2.WebUI.WebCombo.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebCombo" TagPrefix="igcmbo" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <style type="text/css">
        .style1
        {
            height: 39px;
        }
        .style2
        {
            text-align: right;
        }
    </style>
</head>
<body style="font-size: 11px; font-family: tahoma; text-align: center; margin: 0px; ">
    <form id="form1" runat="server">
    <div>
        <table width="710" style="height: 400px">
				<TR>
					<TD>
						</TD>
				</TR>
				<TR>
					<TD>
						<asp:Panel id="Panel1" runat="server">
							<TABLE id="Table2" style="FONT-FAMILY: Arial" cellSpacing="1" cellPadding="1" align="center"
								border="0">
								<TR>
									<TD style="WIDTH: 140px" class="style2" >
                                        <FONT face="Arial Narrow">Entrada</FONT></TD>
									<TD style="WIDTH: 115px" >
										<igtxt:webdatetimeedit id="AccesoEId" runat="server" Width="120px" BorderStyle="Solid" BorderWidth="1px"
											BorderColor="#7F9DB9" UseBrowserDefaults="False" CellSpacing="1" EditModeFormat="H:mm:ss" DisplayModeFormat="H:mm:ss"
											Font-Names="Arial Narrow">
											<ButtonsAppearance CustomButtonDefaultTriangleImages="Arrow">
												<ButtonPressedStyle BackColor="#83A6F4"></ButtonPressedStyle>
												<ButtonDisabledStyle BorderColor="#D7D7D7" ForeColor="#BEBEBE" BackColor="#E1E1DD"></ButtonDisabledStyle>
												<ButtonStyle Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" ForeColor="#506080"
													BackColor="#C5D5FC"></ButtonStyle>
												<ButtonHoverStyle BackColor="#DCEDFD"></ButtonHoverStyle>
											</ButtonsAppearance>
											<SpinButtons DefaultTriangleImages="ArrowSmall" Width="15px"></SpinButtons>
										</igtxt:webdatetimeedit></TD>
									<TD style="WIDTH: 0px" >
										<asp:label id="LAccesoEId" runat="server" Font-Names="Arial Narrow" Visible="False"></asp:label></TD>
									<TD >
										<asp:label id="LAccesoET" runat="server" Font-Names="Arial Narrow"></asp:label></TD>
									<TD >
										<igtxt:WebImageButton id="BAccesoEId" runat="server" UseBrowserDefaults="False" Text="Usar Acceso Seleccionado"
											Height="22px" OnClick="BAcceso_Click" ImageTextSpacing="4">
											<Alignments VerticalImage="Bottom" HorizontalAll="NotSet" VerticalAll="NotSet"></Alignments>
											<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
												RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
											<Appearance>
												<Style Cursor="Default">
												</Style>
												<Image Url="./Imagenes/gtk-go-forward.png" Height="15px" Width="18px"></Image>
											</Appearance>
										</igtxt:WebImageButton></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 140px" class="style2" >
										<asp:label id="Label2" runat="server" Font-Names="Arial Narrow" Visible="False">Salida a Comer</asp:label></TD>
									<TD style="WIDTH: 115px" >
										<igtxt:webdatetimeedit id="AccesoCSId" runat="server" Width="120px" BorderStyle="Solid" BorderWidth="1px"
											BorderColor="#7F9DB9" UseBrowserDefaults="False" CellSpacing="1" EditModeFormat="H:mm:ss" DisplayModeFormat="H:mm:ss"
											Font-Names="Arial Narrow" Visible="False" Enabled="False">
											<ButtonsAppearance CustomButtonDefaultTriangleImages="Arrow">
												<ButtonPressedStyle BackColor="#83A6F4"></ButtonPressedStyle>
												<ButtonDisabledStyle BorderColor="#D7D7D7" ForeColor="#BEBEBE" BackColor="#E1E1DD"></ButtonDisabledStyle>
												<ButtonStyle Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" ForeColor="#506080"
													BackColor="#C5D5FC"></ButtonStyle>
												<ButtonHoverStyle BackColor="#DCEDFD"></ButtonHoverStyle>
											</ButtonsAppearance>
											<SpinButtons DefaultTriangleImages="ArrowSmall" Width="15px"></SpinButtons>
										</igtxt:webdatetimeedit></TD>
									<TD style="WIDTH: 0px" >
										<asp:label id="LAccesoCSId" runat="server" Font-Names="Arial Narrow" Visible="False"></asp:label></TD>
									<TD >
										<asp:label id="LAccesoCST" runat="server" Font-Names="Arial Narrow" Visible="False"></asp:label></TD>
									<TD >
										<igtxt:WebImageButton id="BAccesoSCId" runat="server" UseBrowserDefaults="False" Visible="False" Text="Usar Acceso Seleccionado"
											Height="22px" OnClick="BAcceso_Click" ImageTextSpacing="4">
											<Alignments VerticalImage="Bottom" HorizontalAll="NotSet" VerticalAll="NotSet"></Alignments>
											<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
												RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
											<Appearance>
												<Style Cursor="Default">
												</Style>
												<Image Url="./Imagenes/gtk-go-forward.png" Height="15px" Width="18px"></Image>
											</Appearance>
										</igtxt:WebImageButton></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 140px" class="style2" >
										<P>
											<asp:label id="Label1" runat="server" Font-Names="Arial Narrow" Visible="False">Regreso de Comer</asp:label></P>
									</TD>
									<TD style="WIDTH: 115px" >
										<igtxt:webdatetimeedit id="AccesoCRId" runat="server" Width="120px" BorderStyle="Solid" BorderWidth="1px"
											BorderColor="#7F9DB9" UseBrowserDefaults="False" CellSpacing="1" EditModeFormat="H:mm:ss" DisplayModeFormat="H:mm:ss"
											Font-Names="Arial Narrow" Visible="False" Enabled="False">
											<ButtonsAppearance CustomButtonDefaultTriangleImages="Arrow">
												<ButtonPressedStyle BackColor="#83A6F4"></ButtonPressedStyle>
												<ButtonDisabledStyle BorderColor="#D7D7D7" ForeColor="#BEBEBE" BackColor="#E1E1DD"></ButtonDisabledStyle>
												<ButtonStyle Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" ForeColor="#506080"
													BackColor="#C5D5FC"></ButtonStyle>
												<ButtonHoverStyle BackColor="#DCEDFD"></ButtonHoverStyle>
											</ButtonsAppearance>
											<SpinButtons DefaultTriangleImages="ArrowSmall" Width="15px"></SpinButtons>
										</igtxt:webdatetimeedit></TD>
									<TD style="WIDTH: 0px" >
										<asp:label id="LAccesoCRId" runat="server" Font-Names="Arial Narrow" Visible="False"></asp:label></TD>
									<TD >
										<asp:label id="LAccesoCRT" runat="server" Font-Names="Arial Narrow" Visible="False"></asp:label></TD>
									<TD >
										<igtxt:WebImageButton id="BAccesoRCId" runat="server" UseBrowserDefaults="False" Visible="False" Text="Usar Acceso Seleccionado"
											Height="22px" OnClick="BAcceso_Click" ImageTextSpacing="4">
											<Alignments VerticalImage="Bottom" HorizontalAll="NotSet" VerticalAll="NotSet"></Alignments>
											<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
												RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
											<Appearance>
												<Style Cursor="Default">
												</Style>
												<Image Url="./Imagenes/gtk-go-forward.png" Height="15px" Width="18px"></Image>
											</Appearance>
										</igtxt:WebImageButton></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 140px" class="style2" >
                                        <FONT face="Arial Narrow">Salida</FONT></TD>
									<TD style="WIDTH: 115px" >
										<igtxt:webdatetimeedit id="AccesoSId" runat="server" Width="120px" BorderStyle="Solid" BorderWidth="1px"
											BorderColor="#7F9DB9" UseBrowserDefaults="False" CellSpacing="1" EditModeFormat="H:mm:ss" DisplayModeFormat="H:mm:ss"
											Font-Names="Arial Narrow">
											<ButtonsAppearance CustomButtonDefaultTriangleImages="Arrow">
												<ButtonPressedStyle BackColor="#83A6F4"></ButtonPressedStyle>
												<ButtonDisabledStyle BorderColor="#D7D7D7" ForeColor="#BEBEBE" BackColor="#E1E1DD"></ButtonDisabledStyle>
												<ButtonStyle Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" ForeColor="#506080"
													BackColor="#C5D5FC"></ButtonStyle>
												<ButtonHoverStyle BackColor="#DCEDFD"></ButtonHoverStyle>
											</ButtonsAppearance>
											<SpinButtons DefaultTriangleImages="ArrowSmall" Width="15px"></SpinButtons>
										</igtxt:webdatetimeedit></TD>
									<TD style="WIDTH: 0px" >
										<asp:label id="LAccesoSId" runat="server" Font-Names="Arial Narrow" Visible="False"></asp:label></TD>
									<TD >
										<asp:label id="LAccesoST" runat="server" Font-Names="Arial Narrow"></asp:label></TD>
									<TD >
										<igtxt:WebImageButton id="BAccsesoSId" runat="server" UseBrowserDefaults="False" Text="Usar Acceso Seleccionado"
											Height="22px" OnClick="BAcceso_Click" ImageTextSpacing="4">
											<Alignments VerticalImage="Bottom" HorizontalAll="NotSet" VerticalAll="NotSet"></Alignments>
											<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
												RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
											<Appearance>
												<Style Cursor="Default">
												</Style>
												<Image Url="./Imagenes/gtk-go-forward.png" Height="15px" Width="18px"></Image>
											</Appearance>
										</igtxt:WebImageButton></TD>
								</TR>
							</TABLE>
						</asp:Panel>
					</TD>
				</TR>
				<TR>
					<TD height="100px">
						<igtbl:ultrawebgrid id=Grid runat="server" Width="100%" 
                            ImageDirectory="/ig_common/Images/" UseAccessibleHeader="False" 
                            DataSource="<%# DS_Personas_Diario1 %>" DataMember="EC_ACCESOSE" 
                            OnInitializeLayout="Grid_InitializeLayout" Height="136px">
							<DisplayLayout ColWidthDefault="" AllowSortingDefault="OnClient" RowHeightDefault="20px" Version="3.00"
								GridLinesDefault="Horizontal" SelectTypeRowDefault="Extended" HeaderClickActionDefault="SortSingle"
								BorderCollapseDefault="Separate" AllowColSizingDefault="Free" CellPaddingDefault="4" RowSelectorsDefault="No"
								Name="Grid" TableLayout="Fixed" CellClickActionDefault="RowSelect" AllowUpdateDefault="Yes">
								<ImageUrls ExpandImage="ig_tblcrm_rowarrow_right.gif" CollapseImage="ig_tblcrm_rowarrow_down.gif"></ImageUrls>
								<GroupByBox Prompt="Arrastre la columna que desea agrupar...">
									<Style BorderColor="Window" ForeColor="Navy" BackColor="LightSteelBlue">
									</Style>
<BoxStyle BackColor="LightSteelBlue" BorderColor="Window" ForeColor="Navy"></BoxStyle>
								</GroupByBox>
								<FrameStyle Width="100%" BorderWidth="1px" Font-Size="8pt" 
                                    Font-Names="Arial Narrow" BorderColor="Black"
									BorderStyle="Solid" ForeColor="#759AFD" Height="136px"></FrameStyle>



<Images ><CollapseImage Url="ig_tblcrm_rowarrow_down.gif"></CollapseImage><ExpandImage Url="ig_tblcrm_rowarrow_right.gif"></ExpandImage></Images>

								<RowAlternateStyleDefault ForeColor="Black" BackColor="WhiteSmoke"></RowAlternateStyleDefault>
								<Pager PageSize="20">
									<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

									</Style>
<PagerStyle BackColor="LightGray" BorderWidth="1px" BorderStyle="Solid">
<BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px"></BorderDetails>
</PagerStyle>
								</Pager>
								<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
								<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
									<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
								</FooterStyleDefault>
								<HeaderStyleDefault BackgroundImage="images/GridTitulo.gif" Font-Size="X-Small" Font-Names="Arial" Font-Bold="True" BorderColor="Black" BorderStyle="Solid"
									HorizontalAlign="Left" ForeColor="White" BackColor="Navy" >
									<BorderDetails WidthLeft="0px" WidthTop="0px" WidthRight="1px" WidthBottom="1px"></BorderDetails>
								</HeaderStyleDefault>
								<RowStyleDefault BorderWidth="1px" BorderColor="Black" BorderStyle="Solid" ForeColor="White" BackColor="CornflowerBlue">
									<Padding Left="3px"></Padding>
									<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
								</RowStyleDefault>
								<SelectedRowStyleDefault BackgroundImage="images/Office2003SelRow.png" ForeColor="WhiteSmoke" BackColor="Sienna" >
									<BorderDetails WidthLeft="0px" StyleBottom="Solid" ColorBottom="Black" WidthTop="0px" WidthRight="0px"
										StyleTop="None" StyleRight="None" WidthBottom="1px" StyleLeft="None"></BorderDetails>
								</SelectedRowStyleDefault>
								<AddNewBox>
									<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

									</Style>
<BoxStyle BackColor="LightGray" BorderWidth="1px" BorderStyle="Solid">
<BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px"></BorderDetails>
</BoxStyle>
								</AddNewBox>
								<ActivationObject BorderStyle="Dotted" BorderColor="1, 68, 208"></ActivationObject>
							</DisplayLayout>
							<Bands>
								<igtbl:UltraGridBand AddButtonCaption="EC_ACCESOSE" BaseTableName="EC_ACCESOSE" Key="EC_ACCESOSE">
<AddNewRow Visible="NotSet" View="NotSet"></AddNewRow>
									<Columns>
										<igtbl:UltraGridColumn HeaderText="ACCESO_ID" Key="ACCESO_ID" IsBound="True" Hidden="True" DataType="System.Decimal"
											BaseColumnName="ACCESO_ID">
											<Footer Key="ACCESO_ID"></Footer>
											<Header Key="ACCESO_ID" Caption="ACCESO_ID"></Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="Fecha y Hora" Key="ACCESO_FECHAHORA" IsBound="True" Width="200px" Format="dd/MM/yyyy hh:mm tt"
											DataType="System.DateTime" BaseColumnName="ACCESO_FECHAHORA">
											<Footer Key="ACCESO_FECHAHORA">
												<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
											</Footer>
											<Header Key="ACCESO_FECHAHORA" Caption="Fecha y Hora">
												<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
											</Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="Terminal" Key="TERMINAL_NOMBRE" IsBound="True" Width="200px" BaseColumnName="TERMINAL_NOMBRE">
											<Footer Key="TERMINAL_NOMBRE">
												<RowLayoutColumnInfo OriginX="2"></RowLayoutColumnInfo>
											</Footer>
											<Header Key="TERMINAL_NOMBRE" Caption="Terminal">
												<RowLayoutColumnInfo OriginX="2"></RowLayoutColumnInfo>
											</Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="Tipo de Acceso" Key="TIPO_ACCESO_NOMBRE" IsBound="True" Width="200px"
											BaseColumnName="TIPO_ACCESO_NOMBRE">
											<Footer Key="TIPO_ACCESO_NOMBRE">
												<RowLayoutColumnInfo OriginX="3"></RowLayoutColumnInfo>
											</Footer>
											<Header Key="TIPO_ACCESO_NOMBRE" Caption="Tipo de Acceso">
												<RowLayoutColumnInfo OriginX="3"></RowLayoutColumnInfo>
											</Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="PERSONA_ID" Key="PERSONA_ID" IsBound="True" Hidden="True" DataType="System.Decimal"
											BaseColumnName="PERSONA_ID">
											<Footer Key="PERSONA_ID">
												<RowLayoutColumnInfo OriginX="4"></RowLayoutColumnInfo>
											</Footer>
											<Header Key="PERSONA_ID" Caption="PERSONA_ID">
												<RowLayoutColumnInfo OriginX="4"></RowLayoutColumnInfo>
											</Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="TERMINAL_ID" Key="TERMINAL_ID" IsBound="True" Hidden="True" DataType="System.Decimal"
											BaseColumnName="TERMINAL_ID">
											<Footer Key="TERMINAL_ID">
												<RowLayoutColumnInfo OriginX="5"></RowLayoutColumnInfo>
											</Footer>
											<Header Key="TERMINAL_ID" Caption="TERMINAL_ID">
												<RowLayoutColumnInfo OriginX="5"></RowLayoutColumnInfo>
											</Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="TIPO_ACCESO_ID" Key="TIPO_ACCESO_ID" IsBound="True" Hidden="True" DataType="System.Decimal"
											BaseColumnName="TIPO_ACCESO_ID">
											<Footer Key="TIPO_ACCESO_ID">
												<RowLayoutColumnInfo OriginX="6"></RowLayoutColumnInfo>
											</Footer>
											<Header Key="TIPO_ACCESO_ID" Caption="TIPO_ACCESO_ID">
												<RowLayoutColumnInfo OriginX="6"></RowLayoutColumnInfo>
											</Header>
										</igtbl:UltraGridColumn>
									</Columns>
									
								</igtbl:UltraGridBand>
							</Bands>
						</igtbl:ultrawebgrid></TD>
				</TR>
				<TR>
					<TD >
						<asp:Panel id="Panel2" runat="server">
							<TABLE id="Table3" style="WIDTH: 713px; HEIGHT: 88px" cellSpacing="0" cellPadding="1" width="713"
								align="center" border="0">
								<TR>
									<TD style="WIDTH: 216px; HEIGHT: 20px" height="20" class="style2"><font 
                                    face="Arial Narrow">
                                        <span style="font-size: 10pt">Tipo de 
											Justificacion</span></font></TD>
									<TD style="WIDTH: 435px; HEIGHT: 20px; text-align: left;" height="20">
										<igcmbo:webcombo id=TipoIncidenciaId runat="server" Width="408px" BorderStyle="Solid" BorderWidth="1px" BorderColor="LightGray" Font-Names="Arial Narrow" Height="22px" DataMember="EC_TIPO_INCIDENCIAS" DataSource="<%# DS_Personas_Diario1 %>" BackColor="White" ForeColor="Black" Version="3.00"   SelBackColor="17, 69, 158" DataValueField="TIPO_INCIDENCIA_ID"   DataTextField="TIPO_INCIDENCIA_NOMBRE" OnInitializeLayout="TipoIncidenciaId_InitializeLayout">
											<Columns>
												<igtbl:UltraGridColumn HeaderText="TIPO_INCIDENCIA_ID" Key="TIPO_INCIDENCIA_ID" IsBound="True" Hidden="True"
													DataType="System.Decimal" BaseColumnName="TIPO_INCIDENCIA_ID">
													<Footer Key="TIPO_INCIDENCIA_ID"></Footer>
													<Header Key="TIPO_INCIDENCIA_ID" Caption="TIPO_INCIDENCIA_ID"></Header>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="Incidencia" Key="TIPO_INCIDENCIA_NOMBRE" IsBound="True" BaseColumnName="TIPO_INCIDENCIA_NOMBRE">
													<Footer Key="TIPO_INCIDENCIA_NOMBRE">
														<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
													</Footer>
													<Header Key="TIPO_INCIDENCIA_NOMBRE" Caption="Incidencia">
														<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
													</Header>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="Abreviatura" Key="TIPO_INCIDENCIA_ABR" IsBound="True" BaseColumnName="TIPO_INCIDENCIA_ABR">
													<Footer Key="TIPO_INCIDENCIA_ABR">
														<RowLayoutColumnInfo OriginX="2"></RowLayoutColumnInfo>
													</Footer>
													<Header Key="TIPO_INCIDENCIA_ABR" Caption="Abreviatura">
														<RowLayoutColumnInfo OriginX="2"></RowLayoutColumnInfo>
													</Header>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="TIPO_INCIDENCIA_BORRADO" Key="TIPO_INCIDENCIA_BORRADO" IsBound="True"
													Hidden="True" DataType="System.Decimal" BaseColumnName="TIPO_INCIDENCIA_BORRADO">
													<Footer Key="TIPO_INCIDENCIA_BORRADO">
														<RowLayoutColumnInfo OriginX="3"></RowLayoutColumnInfo>
													</Footer>
													<Header Key="TIPO_INCIDENCIA_BORRADO" Caption="TIPO_INCIDENCIA_BORRADO">
														<RowLayoutColumnInfo OriginX="3"></RowLayoutColumnInfo>
													</Header>
												</igtbl:UltraGridColumn>
											</Columns>
											<DropDownLayout BorderCollapse="Separate" RowHeightDefault="20px" HeaderClickAction="Select" ColWidthDefault="150px">
												<RowStyle BorderWidth="1px" BorderColor="Black" BorderStyle="Solid" ForeColor="White" BackColor="CornflowerBlue">
													<Padding Left="3px"></Padding>
													<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
												</RowStyle>
												<SelectedRowStyle ForeColor="WhiteSmoke" BackColor="Sienna" ></SelectedRowStyle>
												<HeaderStyle Font-Size="X-Small" Font-Names="Arial" Font-Bold="True" BorderColor="Black" BorderStyle="Solid"
													ForeColor="White" BackColor="Navy" >
													<BorderDetails WidthLeft="0px" WidthTop="0px" WidthRight="1px" WidthBottom="1px"></BorderDetails>
												</HeaderStyle>
												<RowAlternateStyle ForeColor="Black" BackColor="WhiteSmoke"></RowAlternateStyle>
												<FrameStyle Width="325px" Cursor="Default" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana"
													BorderColor="Black" BorderStyle="Solid" ForeColor="#759AFD" Height="130px"></FrameStyle>
											</DropDownLayout>
											<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
										</igcmbo:webcombo></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 216px; HEIGHT: 4px" height="4" class="style2"><font 
                                    face="Arial Narrow">
                                        <span style="font-size: 10pt">Motivo</span></font></TD>
									<TD style="WIDTH: 435px; HEIGHT: 4px; text-align: left;" height="4">
										<igtxt:webtextedit id="TipoIncidenciaMotivo" runat="server" Width="408px" BorderStyle="Solid" BorderWidth="1px"
											BorderColor="#7F9DB9" UseBrowserDefaults="False" CellSpacing="1" Font-Names="Arial Narrow">
											<ButtonsAppearance CustomButtonDefaultTriangleImages="Arrow">
												<ButtonPressedStyle BackColor="#83A6F4"></ButtonPressedStyle>
												<ButtonDisabledStyle BorderColor="#D7D7D7" ForeColor="#BEBEBE" BackColor="#E1E1DD"></ButtonDisabledStyle>
												<ButtonStyle Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" ForeColor="#506080"
													BackColor="#C5D5FC"></ButtonStyle>
												<ButtonHoverStyle BackColor="#DCEDFD"></ButtonHoverStyle>
											</ButtonsAppearance>
										</igtxt:webtextedit></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 216px; HEIGHT: 3px" height="3" class="style2"><font 
                                    face="Arial Narrow">
											<asp:Label ID="Label3" runat="server" Font-Size="Small">Tipo de Incidencia Sistema</asp:Label></font></TD>
									<TD style="WIDTH: 435px; HEIGHT: 3px; text-align: left;" height="3">
										<igcmbo:webcombo id=TipoIncSystem runat="server" Width="408px" BorderStyle="Solid" BorderWidth="1px" BorderColor="LightGray" Font-Names="Arial Narrow" Height="22px" DataMember="EC_TIPO_INC_SIS__" DataSource="<%# DS_Personas_Diario1 %>" BackColor="White" ForeColor="Black" Version="3.00"   SelBackColor="17, 69, 158" DataValueField="TIPO_INC_SIS_ID"   DataTextField="TIPO_INC_SIS_NOMBRE" OnInitializeLayout="TipoIncSystem_InitializeLayout">
											<Columns>
												<igtbl:UltraGridColumn HeaderText="TIPO_INC_SIS_ID" Key="TIPO_INC_SIS_ID" IsBound="True" Hidden="True"
													DataType="System.Decimal" BaseColumnName="TIPO_INC_SIS_ID">
													<Footer Key="TIPO_INC_SIS_ID"></Footer>
													<Header Key="TIPO_INC_SIS_ID" Caption="TIPO_INC_SIS_ID"></Header>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="TIPO_INC_SIS_NOMBRE" Key="TIPO_INC_SIS_NOMBRE" IsBound="True" BaseColumnName="TIPO_INC_SIS_NOMBRE">
													<Footer Key="TIPO_INC_SIS_NOMBRE">
														<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
													</Footer>
													<Header Key="TIPO_INC_SIS_NOMBRE" Caption="TIPO_INC_SIS_NOMBRE">
														<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
													</Header>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="TIPO_INC_SIS_ABR" Key="TIPO_INC_SIS_ABR" IsBound="True" Hidden="True"
													BaseColumnName="TIPO_INC_SIS_ABR">
													<Footer Key="TIPO_INC_SIS_ABR">
														<RowLayoutColumnInfo OriginX="2"></RowLayoutColumnInfo>
													</Footer>
													<Header Key="TIPO_INC_SIS_ABR" Caption="TIPO_INC_SIS_ABR">
														<RowLayoutColumnInfo OriginX="2"></RowLayoutColumnInfo>
													</Header>
												</igtbl:UltraGridColumn>
											</Columns>
											<DropDownLayout BorderCollapse="Separate" RowHeightDefault="20px" HeaderClickAction="Select" ColWidthDefault="150px">
												<RowStyle BorderWidth="1px" BorderColor="Black" BorderStyle="Solid" ForeColor="White" BackColor="CornflowerBlue">
													<Padding Left="3px"></Padding>
													<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
												</RowStyle>
												<SelectedRowStyle ForeColor="WhiteSmoke" BackColor="Sienna" ></SelectedRowStyle>
												<HeaderStyle Font-Size="X-Small" Font-Names="Arial" Font-Bold="True" BorderColor="Black" BorderStyle="Solid"
													ForeColor="White" BackColor="Navy" >
													<BorderDetails WidthLeft="0px" WidthTop="0px" WidthRight="1px" WidthBottom="1px"></BorderDetails>
												</HeaderStyle>
												<RowAlternateStyle ForeColor="Black" BackColor="WhiteSmoke"></RowAlternateStyle>
												<FrameStyle Width="325px" Cursor="Default" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana"
													BorderColor="Black" BorderStyle="Solid" ForeColor="#759AFD" Height="130px"></FrameStyle>
											</DropDownLayout>
											<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
										</igcmbo:webcombo></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 216px; HEIGHT: 32px"></TD>
									<TD style="WIDTH: 435px; HEIGHT: 32px">
										<asp:CheckBox id="CheckBox1" runat="server" Text="Sin Justificar" Font-Size="Small"></asp:CheckBox></TD>
								</TR>
							</TABLE>
						</asp:Panel>
					</TD>
				</TR>
				<TR>
					<TD >
						<P align="center">
							<asp:label id="LCorrecto" runat="server" Font-Names="Arial Narrow" ForeColor="Green"></asp:label>
							<asp:label id="LError" runat="server" Font-Names="Arial Narrow" ForeColor="Red"></asp:label></P>
					</TD>
				</TR>
				<TR>
					<TD class="style1">
						<P align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							<igtxt:WebImageButton id="BRegresar" runat="server" UseBrowserDefaults="False" Width="150px" Height="22px"
								Text="Regresar" OnClick="BRegresar_Click">
								<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
								<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
									RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
								<Appearance>
									<Style Cursor="Default">
									</Style>
									<Image Url="./Imagenes/Back.png" Height="16px" Width="16px"></Image>
								</Appearance>
							</igtxt:WebImageButton>&nbsp;&nbsp;
							<igtxt:WebImageButton id="BDeshacerCambios" runat="server" UseBrowserDefaults="False" Width="150px" Height="22px"
								Text="Deshacer Cambios">
								<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
								<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
									RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
								<Appearance>
									<Style Cursor="Default">
									</Style>
									<Image Url="./Imagenes/Undo.png" Height="16px" Width="16px"></Image>
								</Appearance>
							</igtxt:WebImageButton>&nbsp;&nbsp;
							<igtxt:WebImageButton id="BGuardarCambios" runat="server" UseBrowserDefaults="False" Width="150px" Height="22px"
								Text="Guardar Cambios" OnClick="BGuardarCambios_Click">
								<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
								<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
									RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
								<Appearance>
									<Style Cursor="Default">
									</Style>
									<Image Url="./Imagenes/Save_as.png" Height="16px" Width="16px"></Image>
								</Appearance>
							</igtxt:WebImageButton></P>
					</TD>
				</TR>
			</TABLE>
    
    </div>
    </form>
</body>
</html>