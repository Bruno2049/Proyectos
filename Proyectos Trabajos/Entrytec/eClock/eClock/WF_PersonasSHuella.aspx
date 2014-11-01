<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Page language="c#"   CodeFile="WF_PersonasSHuella.aspx.cs" AutoEventWireup="True" Inherits="eClock.WF_PersonasSHuella" %>

<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Listado de campos</title>
    <style type="text/css">
html, body, #wrapper,#Tabla {
	height:100%;
	width:100%;
	margin: 0;
	padding: 0;
	border: none;
	text-align: center;
}

</style>
    <script type="text/javascript" id="igClientScript">
<!--

function Wtx_PERSONA_LINK_ID_S_HUELLA_KeyPress(oEdit, keyCode, oEvent){
	//Add code to handle your event here.
    if (keyCode < 48 || keyCode > 57)
        oEvent.cancel = true;
} 
// -->
</script>
</head>
<body>
    <form id="Tabla" runat="server">
			<TABLE id="wrapper" cellSpacing="1" cellPadding="1" border="0"
				align="center">
				<TR>
					<TD style="HEIGHT: 0%" height="1">
                        <igmisc:webpanel id="WebPanel2" runat="server" 
                            
                             Width="600px" EnableAppStyling="True" StyleSetName="Caribbean">



                            <Header Text="Agregar a persona sin huella">
                            </Header>



<Template>
<TABLE style="WIDTH: 100%; HEIGHT: 10px" id="Table1" cellSpacing=5 cellPadding=1 border=0><TBODY><TR><TD style="WIDTH: 116px" align=left>
    <asp:Label ID="Lbl_PERSONA_LINK_ID_S_HUELLA" runat="server" Font-Names="Arial" 
        Font-Size="Small" Text="No. de Empleado"></asp:Label>
    </TD><TD style="WIDTH: 111px" align=left>
        <igtxt:WebTextEdit ID="Wtx_PERSONA_LINK_ID_S_HUELLA" runat="server">
            <ClientSideEvents KeyPress="Wtx_PERSONA_LINK_ID_S_HUELLA_KeyPress" />
        </igtxt:WebTextEdit>
    </TD>
    <td align="left" style="WIDTH: 385px">
        <asp:Label ID="Lbl_PERSONA_S_HUELLA_CLAVE" runat="server" Font-Names="Arial" 
            Font-Size="Small" Text="Clave opcional"></asp:Label>
    </td>
    <TD align=left style="WIDTH: 385px">
        <igtxt:WebTextEdit ID="Wtx_PERSONA_S_HUELLA_CLAVE" runat="server">
            <ClientSideEvents KeyPress="Wtx_PERSONA_LINK_ID_S_HUELLA_KeyPress" />
        </igtxt:WebTextEdit>
    </TD>
    <td align="left">
        <p align="center">
            <igtxt:WebImageButton ID="Webimagebutton1" runat="server" Height="22px" 
                OnClick="Webimagebutton1_Click" Text="Agregar y Guardar" 
                UseBrowserDefaults="False" Width="150px">
                <alignments verticalall="Bottom" verticalimage="Middle" />
                <roundedcorners disabledimageurl="ig_butXP5wh.gif" 
                    focusimageurl="ig_butXP3wh.gif" hoverimageurl="ig_butXP2wh.gif" 
                    imageurl="ig_butXP1wh.gif" maxheight="80" maxwidth="400" 
                    pressedimageurl="ig_butXP4wh.gif" renderingtype="FileImages" />
                <appearance>
                    <style cursor="Default">
                    </style>
                    <image height="16px" url="./Imagenes/Save_as.png" width="16px" />
                </appearance>
            </igtxt:WebImageButton>
        </p>
    </td>
    </TR></TBODY></TABLE>
</Template>
</igmisc:webpanel>
						</TD>
				</TR>
				<TR>
					<TD colSpan="1">
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 250px">
						<igtbl:ultrawebgrid id=Grid runat="server" Height="100%" Width="100%" DataMember="EC_PERSONAS" DataSource="<%# dS_PersonasSHuella1 %>" OnInitializeLayout="Grid_InitializeLayout">
							<DisplayLayout ColWidthDefault="" AllowSortingDefault="OnClient" RowHeightDefault="20px" Version="3.00"
								GridLinesDefault="Horizontal" SelectTypeRowDefault="Extended" HeaderClickActionDefault="SortSingle"
								BorderCollapseDefault="Separate" AllowColSizingDefault="Free" CellPaddingDefault="4" RowSelectorsDefault="No"
								Name="Grid" TableLayout="Fixed" CellClickActionDefault="RowSelect" AllowUpdateDefault="Yes">
								<AddNewBox>
									<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

									</Style>
<BoxStyle BackColor="LightGray" BorderWidth="1px" BorderStyle="Solid">
<BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px"></BorderDetails>
</BoxStyle>
								</AddNewBox>
								<Pager PageSize="20">
									<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

									</Style>
<PagerStyle BackColor="LightGray" BorderWidth="1px" BorderStyle="Solid">
<BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px"></BorderDetails>
</PagerStyle>
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
<BoxStyle BackColor="LightSteelBlue" BorderColor="Window" ForeColor="Navy"></BoxStyle>
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
								<igtbl:UltraGridBand AddButtonCaption="EC_PERSONAS" BaseTableName="EC_PERSONAS" Key="EC_PERSONAS">
									<Columns>
										<igtbl:UltraGridColumn HeaderText="Quitar" Key="QUITAR" IsBound="True" Width="50px" Type="CheckBox" DataType="System.Decimal"
											BaseColumnName="QUITAR">
											<Header Caption="Quitar"></Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="PERSONA_ID" Key="PERSONA_ID" IsBound="True" Hidden="True" DataType="System.Decimal"
											BaseColumnName="PERSONA_ID" AllowUpdate="No">
											<Footer>
												<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
											</Footer>
											<Header Caption="PERSONA_ID">
												<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
											</Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="No. de Empleado" Key="PERSONA_LINK_ID" IsBound="True" Width="120px"
											DataType="System.Decimal" BaseColumnName="PERSONA_LINK_ID" AllowUpdate="No">
											<Footer>
												<RowLayoutColumnInfo OriginX="2"></RowLayoutColumnInfo>
											</Footer>
											<Header Caption="No. de Empleado">
												<RowLayoutColumnInfo OriginX="2"></RowLayoutColumnInfo>
											</Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="Nombre completo" Key="PERSONA_NOMBRE" IsBound="True" Width="400px" BaseColumnName="PERSONA_NOMBRE" AllowUpdate="No">
											<Footer>
												<RowLayoutColumnInfo OriginX="3"></RowLayoutColumnInfo>
											</Footer>
											<Header Caption="Nombre completo">
												<RowLayoutColumnInfo OriginX="3"></RowLayoutColumnInfo>
											</Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="Alta" Key="PERSONA_S_HUELLA_FECHA" IsBound="True" Width="180px" DataType="System.DateTime"
											BaseColumnName="PERSONA_S_HUELLA_FECHA" AllowUpdate="No">
											<Footer>
												<RowLayoutColumnInfo OriginX="4"></RowLayoutColumnInfo>
											</Footer>
											<Header Caption="Alta">
												<RowLayoutColumnInfo OriginX="4"></RowLayoutColumnInfo>
											</Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="Correo Electronico" Key="PERSONA_EMAIL" 
                                            IsBound="True" Width="200px"
											BaseColumnName="PERSONA_EMAIL" AllowUpdate="No">
											<Footer>
												<RowLayoutColumnInfo OriginX="5"></RowLayoutColumnInfo>
											</Footer>
											<Header Caption="Correo Electronico">
												<RowLayoutColumnInfo OriginX="5"></RowLayoutColumnInfo>
											</Header>
										</igtbl:UltraGridColumn>
									    <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="PERSONA_S_HUELLA_CLAVE" 
                                            IsBound="True" Key="PERSONA_S_HUELLA_CLAVE" Width="200px">
                                            <Header Caption="Clave">
                                                <RowLayoutColumnInfo OriginX="6" />
                                            </Header>
                                            <Footer>
                                                <RowLayoutColumnInfo OriginX="6" />
                                            </Footer>
                                        </igtbl:UltraGridColumn>
									</Columns>
									
                                    <AddNewRow View="NotSet" Visible="NotSet">
                                    </AddNewRow>
								</igtbl:UltraGridBand>
							</Bands>
						</igtbl:ultrawebgrid></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 10px">
						<P align="center">
							<asp:Label id="Lbl_Error" runat="server" Font-Names="Arial" ForeColor="#CC0033" 
                                Font-Size="Smaller"></asp:Label>
							<asp:Label id="Lbl_Correcto" runat="server" Font-Names="Arial" 
                                ForeColor="Green" Font-Size="Smaller"></asp:Label></P>
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 10px">
						<P align="center">
							&nbsp;&nbsp;&nbsp;&nbsp;
							&nbsp;&nbsp;&nbsp;&nbsp;
							&nbsp;&nbsp;&nbsp;&nbsp;
						    <table style="width: 39%; height: 51px;">
                                <tr>
                                    <td>
							<igtxt:webimagebutton id="Btn_Terminales" runat="server" Height="22px" Width="30px"
								UseBrowserDefaults="False" OnClick="Btn_Terminales_Click">
								<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
								<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
									RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
								<Appearance>
                                    <Image Url="./Imagenes/Editar16.png" Height="16px" Width="16px" />

<ButtonStyle Cursor="Default"></ButtonStyle>
								</Appearance>
							</igtxt:webimagebutton>
                                    </td>
                                    <td>
							<igtxt:webimagebutton id="Webimagebutton2" runat="server" Height="22px" Width="150px" Text="Limpiar Selección"
								UseBrowserDefaults="False" OnClick="Webimagebutton2_Click">
								<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
								<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
									RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
								<Appearance>
									<Style Cursor="Default">
									</Style>
                                    <Image Url="./Imagenes/UncheckAll.png" Height="16px" Width="16px" />
								</Appearance>
							</igtxt:webimagebutton>
                                    </td>
                                    <td>
							<igtxt:webimagebutton id="Webimagebutton3" runat="server" Height="22px" Width="150px" Text="Seleccionar Todos"
								UseBrowserDefaults="False" OnClick="Webimagebutton3_Click">
								<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
								<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
									RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
								<Appearance>
									<Style Cursor="Default">
									</Style>
                                    <Image Url="./Imagenes/CheckAll.png" Height="16px" Width="16px" />
								</Appearance>
							</igtxt:webimagebutton>
                                    </td>
                                    <td>
							<igtxt:webimagebutton id="BtnBorrar" runat="server" Height="22px" Width="168px" UseBrowserDefaults="False"
								Text="Borrar Seleccionados" OnClick="BtnBorrar_Click">
								<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
								<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
									RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
								<Appearance>
									<Style Cursor="Default">
									</Style>
									<Image Url="./Imagenes/Delete.png" Height="16px" Width="16px"></Image>
								</Appearance>
							</igtxt:webimagebutton>
                                    </td>
                                </tr>
                            </table>
						</P>
					</TD>
				</TR>
			</TABLE>
        
    </form>
</body>
</html>