<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Page language="c#" MasterPageFile="~/MasterPage.master"  CodeFile="WF_UsuarioEmpleado.aspx.cs" AutoEventWireup="True" Inherits="eClock.WF_UsuarioEmpleado" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register TagPrefix="uc1" TagName="WC_Menu" Src="WC_Menu.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

			<TABLE id="Table1" style="WIDTH: 100%; HEIGHT: 100%"
				cellSpacing="1" cellPadding="1" width="792" border="0">
				<TR>
					<TD style="WIDTH: 100%; HEIGHT: 0%; text-align: right;">
                        <asp:CheckBox ID="CBListado" runat="server" AutoPostBack="false" Font-Names="Arial Narrow"
                            Font-Size="Small" Text="Aplicar a todo el Listado" /></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 100%; height: 200px;" align="center">
						<igtbl:ultrawebgrid id=UltraWebGrid1  OnInitializeDataSource = "UltraWebGrid1_InitializeDataSource" runat="server" Height="100%" Width="100%" ImageDirectory="/ig_common/Images/" style="left: 0px; top: 1px" Browser="Xml" OnInitializeLayout="UltraWebGrid1_InitializeLayout">
							<DisplayLayout ColWidthDefault="" AllowSortingDefault="OnClient" RowHeightDefault="20px" Version="3.00"
								GridLinesDefault="Horizontal" SelectTypeRowDefault="Extended" HeaderClickActionDefault="SortSingle"
								BorderCollapseDefault="Separate" AllowColSizingDefault="Free" CellPaddingDefault="4" RowSelectorsDefault="No"
								Name="UltraWebGrid1" TableLayout="Fixed" CellClickActionDefault="RowSelect" AllowUpdateDefault="Yes" ScrollBarView="Vertical">
								<Pager PageSize="20">
									
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
                                <Images ImageDirectory="/ig_common/Images/">
                                </Images>
							</DisplayLayout>
							<Bands>
								<igtbl:UltraGridBand AddButtonCaption="EC_PERSONAS" BaseTableName="EC_PERSONAS" Key="EC_PERSONAS"
									DataKeyField="PERSONA_ID">
									<Columns>
										<igtbl:UltraGridColumn HeaderText="PERSONA_ID" Key="PERSONA_ID" IsBound="True" Hidden="True" DataType="System.Decimal"
											BaseColumnName="PERSONA_ID">
											<Header Caption="PERSONA_ID"></Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="No. de Empleado" Key="PERSONA_LINK_ID" IsBound="True" Width="120px"
											DataType="System.Decimal" BaseColumnName="PERSONA_LINK_ID">
											<Footer>
												<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
											</Footer>
											<Header Caption="No. de Empleado">
												<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
											</Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="TIPO_PERSONA_ID" Key="TIPO_PERSONA_ID" IsBound="True" Hidden="True"
											DataType="System.Decimal" BaseColumnName="TIPO_PERSONA_ID">
											<Footer>
												<RowLayoutColumnInfo OriginX="2"></RowLayoutColumnInfo>
											</Footer>
											<Header Caption="TIPO_PERSONA_ID">
												<RowLayoutColumnInfo OriginX="2"></RowLayoutColumnInfo>
											</Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="SUSCRIPCION_ID" Key="SUSCRIPCION_ID" IsBound="True" Hidden="True" DataType="System.Decimal"
											BaseColumnName="SUSCRIPCION_ID">
											<Footer>
												<RowLayoutColumnInfo OriginX="3"></RowLayoutColumnInfo>
											</Footer>
											<Header Caption="SUSCRIPCION_ID">
												<RowLayoutColumnInfo OriginX="3"></RowLayoutColumnInfo>
											</Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="Nombre completo" Key="PERSONA_NOMBRE" IsBound="True" Width="400px" BaseColumnName="PERSONA_NOMBRE">
											<Footer>
												<RowLayoutColumnInfo OriginX="4"></RowLayoutColumnInfo>
											</Footer>
											<Header Caption="Nombre completo">
												<RowLayoutColumnInfo OriginX="4"></RowLayoutColumnInfo>
											</Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="Correo Electronico" Key="PERSONA_EMAIL" IsBound="True" Width="250px"
											BaseColumnName="PERSONA_EMAIL">
											<Footer>
												<RowLayoutColumnInfo OriginX="5"></RowLayoutColumnInfo>
											</Footer>
											<Header Caption="Correo Electronico">
												<RowLayoutColumnInfo OriginX="5"></RowLayoutColumnInfo>
											</Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="PERSONA_BORRADO" Key="PERSONA_BORRADO" IsBound="True" Hidden="True"
											DataType="System.Decimal" BaseColumnName="PERSONA_BORRADO">
											<Footer>
												<RowLayoutColumnInfo OriginX="6"></RowLayoutColumnInfo>
											</Footer>
											<Header Caption="PERSONA_BORRADO">
												<RowLayoutColumnInfo OriginX="6"></RowLayoutColumnInfo>
											</Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="Password" Key="PERSONA_CLAVE" IsBound="True" BaseColumnName="PERSONA_CLAVE">
											<Footer>
												<RowLayoutColumnInfo OriginX="7"></RowLayoutColumnInfo>
											</Footer>
											<Header Caption="Password">
												<RowLayoutColumnInfo OriginX="7"></RowLayoutColumnInfo>
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
					<TD style="WIDTH: 100%; HEIGHT: 0%" align="center">
						<asp:Label id="LError" runat="server" ForeColor="#CC0033" Font-Names="Arial Narrow" Font-Size="Smaller"></asp:Label>
						<asp:Label id="LCorrecto" runat="server" ForeColor="Green" Font-Names="Arial Narrow" Font-Size="Smaller"></asp:Label>
				</TR>
				<TR>
					<TD style="WIDTH: 100%; HEIGHT: 0%" align="center">
						<igtxt:WebImageButton id="WebImageButton2" runat="server" Height="26px" Width="140px" Text="Generar Usuarios"
							UseBrowserDefaults="False" ImageTextSpacing="4">
							<Alignments VerticalImage="Bottom" HorizontalAll="NotSet" VerticalAll="NotSet"></Alignments>
							<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
								RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
							<Appearance>
								<Image Url="./Imagenes/stock-convert.png" Height="18px" Width="20px"></Image>
                                <Style>
<Padding Top="4px"></Padding>
</Style>
							</Appearance>
						</igtxt:WebImageButton></TD>
				</TR>
			</TABLE>
	</asp:Content>  