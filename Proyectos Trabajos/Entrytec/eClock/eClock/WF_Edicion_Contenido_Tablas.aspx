<%@ Page language="c#"  MasterPageFile="~/MasterPage.master" CodeFile="WF_Edicion_Contenido_Tablas.aspx.cs" AutoEventWireup="True" Inherits="eClock.WF_Edicion_Contenido_Tablas" %>

<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register TagPrefix="uc1" TagName="WC_Menu" Src="WC_Menu.ascx" %>
<%@ Register Assembly="Infragistics2.WebUI.WebCombo.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebCombo" TagPrefix="igcmbo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
			<TABLE width="100%" >
				<TR>
					<TD style="WIDTH: 100%; HEIGHT: 0%"></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 100%; HEIGHT: 200px" align="center"><asp:panel id="Panel2" runat="server" Visible="False" Height="171px" Width="600px">
							<P>
                                <igmisc:webpanel id="WebPanel2" runat="server" backcolor="White" bordercolor="SteelBlue"
                                    borderstyle="Outset" borderwidth="2px" font-bold="False" font-names="ARIAL" forecolor="Black"
                                    stylesetname="PaneleClock">
<PanelStyle BorderStyle="Solid" ForeColor="Black" BorderWidth="1px" Font-Names="Arial">
<BorderDetails ColorTop="0, 45, 150" ColorLeft="158, 190, 245" ColorBottom="0, 45, 150" ColorRight="0, 45, 150"></BorderDetails>

<Padding Top="5px" Left="5px" Bottom="5px" Right="5px"></Padding>
</PanelStyle>

<Header TextAlignment="Left" Text="Borrado de Grupo">
<ExpandedAppearance>
<Styles BackgroundImage="./images/GridTitulo.gif" BorderStyle="Ridge" ForeColor="Black" BorderWidth="1px" BorderColor="Transparent" Height="15px" Font-Size="9pt" Font-Names="Arial" Font-Bold="True">
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
<TABLE style="WIDTH: 491px; HEIGHT: 116px" id="Table1" cellSpacing=1 cellPadding=1 width=491 border=0><TBODY><TR><TD align=left colSpan=4><asp:Label id="LGrupoIdBorrado" runat="server" Visible="False"></asp:Label> <asp:Label id="LGrupoBorrado" runat="server" Visible="False"></asp:Label></TD></TR><TR><TD align=left colSpan=4><asp:Label id="Mensaje" runat="server" Font-Names="Arial Narrow"></asp:Label></TD></TR><TR><TD align=left>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </TD><TD align=left><asp:DropDownList id="DD_Grupos" runat="server" Width="150px"></asp:DropDownList></TD><TD align=left><igtxt:WebImageButton id="BBorrarAceptar" onclick="BBorrarAceptar_Click" runat="server" Text="Aceptar" Width="95px" Height="22px" UseBrowserDefaults="False" ImageTextSpacing="4">
											<Alignments VerticalImage="Bottom" HorizontalAll="NotSet" VerticalAll="NotSet"></Alignments>
											<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
												RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
											<Appearance>
												<Style Cursor="Default">
												</Style>
												<Image Url="./Imagenes/OK.png" Height="15px" Width="18px"></Image>
											</Appearance>
										</igtxt:WebImageButton></TD><TD align=left><igtxt:WebImageButton id="BBorrarCancelar" runat="server" Text="Cancelar" Width="95px" Height="22px" UseBrowserDefaults="False" ImageTextSpacing="4">
											<Alignments VerticalImage="Bottom" HorizontalAll="NotSet" VerticalAll="NotSet"></Alignments>
											<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
												RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
											<Appearance>
												<Style Cursor="Default">
												</Style>
												<Image Url="./Imagenes/gtk-no.png" Height="15px" Width="18px"></Image>
											</Appearance>
										</igtxt:WebImageButton></TD></TR></TBODY></TABLE>
</Template>
</igmisc:webpanel>
                                &nbsp;</P>
						</asp:panel><asp:panel id="Panel1" runat="server" Visible="False" Width="600px">
                            <igmisc:webpanel id="Webpanel1" runat="server" backcolor="White" bordercolor="SteelBlue"
                                borderstyle="Outset" borderwidth="2px" font-bold="False" font-names="ARIAL" forecolor="Black"
                                stylesetname="PaneleClock" width="100%">
<PanelStyle BorderStyle="Solid" ForeColor="Black" BorderWidth="1px" Font-Names="Arial">
<BorderDetails ColorTop="0, 45, 150" ColorLeft="158, 190, 245" ColorBottom="0, 45, 150" ColorRight="0, 45, 150"></BorderDetails>

<Padding Top="5px" Left="5px" Bottom="5px" Right="5px"></Padding>
</PanelStyle>

<Header TextAlignment="Left" Text="Edicion de Grupo">
<ExpandedAppearance>
<Styles BackgroundImage="./images/GridTitulo.gif" BorderStyle="Ridge" ForeColor="Black" BorderWidth="1px" BorderColor="Transparent" Height="15px" Font-Size="9pt" Font-Names="Arial" Font-Bold="True">
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
<TABLE style="WIDTH: 587px; HEIGHT: 53px" id="Table3" cellSpacing=1 cellPadding=1 width=587 border=0><TBODY><TR><TD style="HEIGHT: 21px" align=left></TD><TD style="HEIGHT: 21px" align=left><asp:Label id="Grupo_id_l" runat="server" Visible="False"></asp:Label></TD><TD style="HEIGHT: 21px" align=left colSpan=2><asp:Label id="Grupo_Nombre" runat="server" Visible="False"></asp:Label></TD></TR><TR><TD align=left><asp:Label id="Label1" runat="server" Font-Names="Arial Narrow">Nombre de </asp:Label></TD><TD align=left>&nbsp;<igtxt:WebTextEdit id="TextBox1" runat="server">
                                        </igtxt:WebTextEdit> </TD><TD align=left><igtxt:WebImageButton id="BGuardar" runat="server" Text="Guardar" Width="150px" Height="22px" UseBrowserDefaults="False" ImageTextSpacing="4">
											<Alignments VerticalImage="Bottom" HorizontalAll="NotSet" VerticalAll="NotSet"></Alignments>
											<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
												RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
											<Appearance>
												<Style Cursor="Default">
												</Style>
												<Image Url="./Imagenes/Copia de GuardarComo.png" Height="15px" Width="18px"></Image>
											</Appearance>
										</igtxt:WebImageButton></TD><TD vAlign=middle align=left><igtxt:WebImageButton id="BCancelar" runat="server" Text="Cancelar" Width="150px" Height="22px" UseBrowserDefaults="False" ImageTextSpacing="4">
											<Alignments VerticalImage="Bottom" HorizontalAll="NotSet" VerticalAll="NotSet"></Alignments>
											<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
												RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
											<Appearance>
												<Style Cursor="Default">
												</Style>
												<Image Url="./Imagenes/CANCELAR.png" Height="15px" Width="18px"></Image>
											</Appearance>
										</igtxt:WebImageButton></TD></TR></TBODY></TABLE><BR />
</Template>
</igmisc:webpanel>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 

      <TABLE id="Table2" style="WIDTH: 587px; HEIGHT: 53px" cellSpacing="1" cellPadding="1" width="587"
								border="0">
								<TR>
									<TD vAlign="middle" align="left" colSpan="4">
										</TD>
								</TR>
							</TABLE></asp:panel>
						<P>
							<igtbl:ultrawebgrid id="Grid" runat="server" Width="100%" Height="100%" OnInitializeLayout="Grid_InitializeLayout">
								<DisplayLayout ColWidthDefault="" AllowSortingDefault="OnClient" RowHeightDefault="20px" Version="3.00"
									GridLinesDefault="Horizontal" SelectTypeRowDefault="Extended" HeaderClickActionDefault="SortMulti"
									BorderCollapseDefault="Separate" AllowColSizingDefault="Free" CellPaddingDefault="4" RowSelectorsDefault="No"
									Name="Grid" TableLayout="Fixed" CellClickActionDefault="RowSelect" AllowUpdateDefault="Yes" FixedColumnScrollType="Delay" FixedHeaderIndicatorDefault="Button" UseFixedHeaders="True">
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
									<GroupByBox Prompt="Arrastre la columna que desea agrupar..." Hidden="True">
										<Style BorderColor="Window" ForeColor="Navy" BackColor="LightSteelBlue">
										</Style>
									</GroupByBox>
									<SelectedRowStyleDefault BackgroundImage="images/Office2003SelRow.png" ForeColor="WhiteSmoke" BackColor="Sienna" >
										<BorderDetails WidthLeft="0px" StyleBottom="Solid" ColorBottom="Black" WidthTop="0px" WidthRight="0px"
											StyleTop="None" StyleRight="None" WidthBottom="1px" StyleLeft="None"></BorderDetails>
									</SelectedRowStyleDefault>
									<RowAlternateStyleDefault ForeColor="Black" BackColor="WhiteSmoke"></RowAlternateStyleDefault>
									<RowStyleDefault BorderColor="Black" ForeColor="White" BackColor="CornflowerBlue" Font-Names="Verdana" Font-Size="8pt">
										<BorderDetails WidthLeft="0px" WidthTop="0px" ColorLeft="CornflowerBlue" ColorTop="CornflowerBlue"></BorderDetails>
									</RowStyleDefault>
                                    <FilterOptionsDefault AllowRowFiltering="OnClient" ApplyOnAdd="True" FilterComparisonType="CaseSensitive"
                                        FilterRowView="Top" FilterUIType="HeaderIcons" RowFilterMode="AllRowsInBand"
                                        ShowAllCondition="Yes" ShowEmptyCondition="Yes" ShowNonEmptyCondition="Yes">
                                    </FilterOptionsDefault>
                                    <Images>
                                        <CollapseImage Url="ig_tblcrm_rowarrow_down.gif" />
                                        <ExpandImage Url="ig_tblcrm_rowarrow_right.gif" />
                                    </Images>
								</DisplayLayout>
								<Bands>
									<igtbl:UltraGridBand CellTitleModeDefault="Always" ColHeadersVisible="Yes" HeaderTitleModeDefault="Always">
										
                                        <AddNewRow View="NotSet" Visible="NotSet">
                                        </AddNewRow>
									</igtbl:UltraGridBand>
								</Bands>
							</igtbl:ultrawebgrid></P>
					</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 100%; HEIGHT: 0%" align="center"><asp:label id="LError" runat="server" Font-Names="Arial Narrow" Font-Size="Smaller" ForeColor="Red"></asp:label><asp:label id="LCorrecto" runat="server" Font-Names="Arial Narrow" Font-Size="Smaller" ForeColor="Green"></asp:label></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 100%; HEIGHT: 0%" align="center"><igtxt:webimagebutton id="BEditarGrupo" runat="server" Height="22px" Width="100px" Text="Editar" UseBrowserDefaults="False" ImageTextSpacing="4" >
							<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
							<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
								RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
							<Appearance>
								<Style Cursor="Default">
								</Style>
								<Image Url="./Imagenes/Edit.png" Height="16px" Width="16px"></Image>
							</Appearance>
						</igtxt:webimagebutton>&nbsp;&nbsp;&nbsp;
						<igtxt:webimagebutton id="BBorrarGrupo" runat="server" Height="22px" Width="100px" Text="Borrar" UseBrowserDefaults="False" ImageTextSpacing="4">
							<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
							<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
								RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
							<Appearance>
								<Style Cursor="Default">
								</Style>
								<Image Url="./Imagenes/Delete.png" Height="16px" Width="16px"></Image>
							</Appearance>
						</igtxt:webimagebutton>&nbsp;&nbsp;&nbsp;
						<igtxt:webimagebutton id="BAgregarGrupo" runat="server" Height="22px" Width="100px" Text="Nuevo" UseBrowserDefaults="False" ImageTextSpacing="4">
							<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
							<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
								RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
							<Appearance>
								<Style Cursor="Default">
								</Style>
								<Image Url="./Imagenes/New.png" Height="16px" Width="16px"></Image>
							</Appearance>
						</igtxt:webimagebutton></TD>
				</TR>
			</TABLE>
	</asp:Content>