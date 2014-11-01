<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebToolbar.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebToolbar" TagPrefix="igtbar" %>
<%@ Page language="c#" MasterPageFile="~/MasterPage.master"  CodeFile="WF_Horario.aspx.cs" AutoEventWireup="True" Inherits="eClock.WF_Horario" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics2.WebUI.WebCombo.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebCombo" TagPrefix="igcmbo" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
				<TABLE id="Table3" style="WIDTH: 169px; HEIGHT: 284px" cellSpacing="1" cellPadding="1" border="1">

					<TR>
						<TD background="Imagenes\MonthHeaderCaption_bg.png" style="width: 290px">
							<P align="right">
								<asp:Label id="LFecha" runat="server" Width="161px">Label</asp:Label></P>
						</TD>
					</TR>
					<TR>
						<TD style="width: 290px">
							
								<igcmbo:WebCombo id="CTipoTurno" runat="server" SelectedIndex="0" BorderColor="LightGray" BorderWidth="1px"
									BorderStyle="Solid"  
									SelBackColor="17, 69, 158"  
									Version="3.00" BackColor="White" ForeColor="Black" Height="1px" Width="160px" OnInitializeLayout="CTipoTurno_InitializeLayout">
									<Rows>
										<igtbl:UltraGridRow Height="">
											<Cells>
<igtbl:UltraGridCell Text="Semanal"></igtbl:UltraGridCell>
</Cells>
										</igtbl:UltraGridRow>
										<igtbl:UltraGridRow Height="">
											<Cells>
<igtbl:UltraGridCell Text="Flexible"></igtbl:UltraGridCell>
</Cells>
										</igtbl:UltraGridRow>
									</Rows>
									<Columns>
										<igtbl:UltraGridColumn BaseColumnName="NombreTurno">
										</igtbl:UltraGridColumn>
									</Columns>
									<DropDownLayout DropdownWidth="160px" BorderCollapse="Separate" RowHeightDefault="20px"
										ColWidthDefault="150px" Version="3.00">
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
										<FrameStyle Width="160px" Cursor="Default" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana"
											BorderColor="Black" BorderStyle="Solid" ForeColor="#759AFD" Height="130px"></FrameStyle>
									</DropDownLayout>
									<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
								</igcmbo:WebCombo>
                       
						</TD>
					</TR>
					<TR>
						<TD style="width: 290px">
							<P><IFRAME id="IFrameTurno" style="WIDTH: 168px; HEIGHT: 192px" tabIndex="0" src="" frameBorder="no"
									scrolling="no"></IFRAME>
							</P>
						</TD>
					</TR>
					<TR>
						<TD style="width: 290px">
							<P align="center">
								<igtbar:UltraWebToolbar id="UltraWebToolbar1" runat="server" BackColor="#7288AC" ForeColor="White" Height="22px"
									Font-Names="Microsoft Sans Serif" Font-Size="8pt" MovableImage="ig_tb_move00.gif" ItemWidthDefault=" "
									ImageDirectory="/ig_common/images/">
									<HoverStyle BorderColor="Blue" BorderStyle="Solid" ForeColor="White" BackColor="#64799C">
										<BorderDetails WidthLeft="1px" WidthTop="1px" WidthRight="1px" WidthBottom="1px"></BorderDetails>
									</HoverStyle>
									<Items>
										<igtbar:TBarButton Key="Limpiar" Image="./Imagenes/Remover.png"></igtbar:TBarButton>
										<igtbar:TBarButton Image="./Imagenes/Pegar.png"></igtbar:TBarButton>
										<igtbar:TBarButton Image="./Imagenes/copiar.png"></igtbar:TBarButton>
									</Items>
									<DefaultStyle BorderStyle="Solid" ForeColor="White" BackColor="#7288AC">
										<BorderDetails ColorTop="114, 136, 172" WidthLeft="1px" ColorBottom="114, 136, 172" WidthTop="1px"
											ColorRight="114, 136, 172" WidthRight="1px" WidthBottom="1px" ColorLeft="114, 136, 172"></BorderDetails>
									</DefaultStyle>
								</igtbar:UltraWebToolbar>
                                &nbsp;</P>
						</TD>
					</TR>
				</TABLE>
</asp:Content>