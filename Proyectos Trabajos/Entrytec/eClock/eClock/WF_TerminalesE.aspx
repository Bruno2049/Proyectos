<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %> 
<%@ Register Assembly="Infragistics2.WebUI.WebCombo.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebCombo" TagPrefix="igcmbo" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Page language="c#" CodeFile="WF_TerminalesE.aspx.cs" AutoEventWireup="True" Inherits="WF_TerminalesE" EnableEventValidation = "false" %>

<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register TagPrefix="uc1" TagName="WC_Menu" Src="WC_Menu.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Terminales</title>
</head>
<body style="font-size: 11px; font-family: tahoma; text-align: center; margin: 0px; ">
    <form id="form1" runat="server" >
    <div>
        <table width="600" style="height: 380px">
				<TR>
					<TD style="HEIGHT: 0%">
						</TD>
				</TR>
				<TR>
					<TD align="center" style="BACKGROUND-POSITION: right center; BACKGROUND-IMAGE: url(./Imagenes/fondoeClock3.jpg); BACKGROUND-REPEAT: no-repeat">
                        <igmisc:webpanel id="WebPanel2" runat="server" EnableAppStyling="True" 
                            StyleSetName="Caribbean" >


                            <PanelStyle BackgroundImage="./skins/spacer.gif">
                            </PanelStyle>


<Header TextAlignment="Left" Text="Datos de Terminal">


</Header>
<Template>
    &nbsp;<igmisc:WebAsyncRefreshPanel ID="WebAsyncRefreshPanel4" runat="server" Height=""
        Width="">
<TABLE style="HEIGHT: 226px" id="Table5" cellSpacing=1 cellPadding=1 width=450 border=0><TBODY><TR><TD style="WIDTH: 150px" align=left><P>
<asp:Label id="Label1" runat="server">Id Terminal</asp:Label></P></TD><TD align=left><igtxt:WebNumericEdit id="TerminalId" runat="server" BorderColor="#7F9DB9" BorderWidth="1px" BorderStyle="Solid" Width="200px" Enabled="False" CellSpacing="1" UseBrowserDefaults="False">
										<ButtonsAppearance CustomButtonDefaultTriangleImages="Arrow">
											<ButtonPressedStyle BackColor="#83A6F4"></ButtonPressedStyle>
											<ButtonDisabledStyle BorderColor="#D7D7D7" ForeColor="#BEBEBE" BackColor="#E1E1DD"></ButtonDisabledStyle>
											<ButtonStyle Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" ForeColor="#506080"
												BackColor="#C5D5FC"></ButtonStyle>
											<ButtonHoverStyle BackColor="#DCEDFD"></ButtonHoverStyle>
										</ButtonsAppearance>
										<SpinButtons DefaultTriangleImages="ArrowSmall" Width="15px"></SpinButtons>
									</igtxt:WebNumericEdit><FONT face=""></FONT></TD><TD align=left><FONT face=""></FONT></TD></TR><TR><TD style="WIDTH: 150px" align=left><asp:Label id="Label2" runat="server">Nombre</asp:Label></TD><TD align=left><igtxt:WebTextEdit id="TerminalNombre" runat="server" BorderColor="#7F9DB9" Font-Names="" BorderWidth="1px" BorderStyle="Solid" Width="200px" CellSpacing="1" UseBrowserDefaults="False">
										<ButtonsAppearance CustomButtonDefaultTriangleImages="Arrow">
											<ButtonPressedStyle BackColor="#83A6F4"></ButtonPressedStyle>
											<ButtonDisabledStyle BorderColor="#D7D7D7" ForeColor="#BEBEBE" BackColor="#E1E1DD"></ButtonDisabledStyle>
											<ButtonStyle Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" ForeColor="#506080"
												BackColor="#C5D5FC"></ButtonStyle>
											<ButtonHoverStyle BackColor="#DCEDFD"></ButtonHoverStyle>
										</ButtonsAppearance>
									</igtxt:WebTextEdit><FONT face=""></FONT></TD><TD align=left><asp:RequiredFieldValidator id="RVTerminalNombre" runat="server" Font-Names="" Font-Size="" ControlToValidate="TerminalNombre" ErrorMessage="Este dato es obligatorio">*</asp:RequiredFieldValidator><FONT face=""></FONT></TD></TR><TR><TD style="WIDTH: 150px;" align=left><asp:Label id="Label9" runat="server" Width="139px">Almacen de Vectores</asp:Label></TD><TD align=left><igcmbo:WebCombo id="AlmacenVectores" runat="server" BorderColor="Silver" BackColor="White" ForeColor="Black" BorderWidth="1px" BorderStyle="Solid" Version="4.00" SelForeColor="White" SelBackColor="DarkBlue"><Columns>
<igtbl:UltraGridColumn>
<Header Caption="Column0"></Header>
</igtbl:UltraGridColumn>
</Columns>

<ExpandEffects ShadowColor="LightGray"></ExpandEffects>

<DropDownLayout RowHeightDefault="20px" BorderCollapse="Separate" Version="4.00">

<FrameStyle BorderWidth="2px" BorderStyle="Ridge" Font-Names="Verdana" BackColor="Silver" Width="325px" Height="130px" Cursor="Default"></FrameStyle>

<HeaderStyle BorderStyle="Solid" BackColor="LightGray">
<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
</HeaderStyle>

<RowStyle BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="White">
<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
</RowStyle>

<SelectedRowStyle ForeColor="White" BackColor="DarkBlue"></SelectedRowStyle>
</DropDownLayout>
</igcmbo:WebCombo> </TD><TD style="HEIGHT: 21px" align=left></TD></TR><TR><TD style="WIDTH: 150px;" align=left><asp:Label id="Label10" runat="server">Sitio</asp:Label></TD><TD align=left><igcmbo:WebCombo id="Sitio" runat="server" BorderColor="Silver" BackColor="White" ForeColor="Black" BorderWidth="1px" BorderStyle="Solid" Version="4.00" SelForeColor="White" SelBackColor="DarkBlue" OnSelectedRowChanged="Sitio_SelectedRowChanged"><Columns>
<igtbl:UltraGridColumn>
<Header Caption="Column0"></Header>
</igtbl:UltraGridColumn>
</Columns>

<ExpandEffects ShadowColor="LightGray"></ExpandEffects>

<DropDownLayout RowHeightDefault="20px" BorderCollapse="Separate" Version="4.00">

<FrameStyle BorderWidth="2px" BorderStyle="Ridge" Font-Names="Verdana" BackColor="Silver" Width="325px" Height="130px" Cursor="Default"></FrameStyle>

<HeaderStyle BorderStyle="Solid" BackColor="LightGray">
<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
</HeaderStyle>

<RowStyle BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="White">
<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
</RowStyle>

<SelectedRowStyle ForeColor="White" BackColor="DarkBlue"></SelectedRowStyle>
</DropDownLayout>
</igcmbo:WebCombo> </TD><TD style="HEIGHT: 21px" align=left></TD></TR><TR><TD style="WIDTH: 150px;" align=left><asp:Label id="Label3" runat="server">Modelo</asp:Label></TD><TD align=left><FONT face=""><igcmbo:WebCombo id="ModeloTerminalId" runat="server" BorderColor="Silver" BackColor="White" ForeColor="Black" BorderWidth="1px" BorderStyle="Solid" Version="4.00" SelForeColor="White" SelBackColor="DarkBlue" OnSelectedRowChanged="ModeloTerminalId_SelectedRowChanged"><Columns>
<igtbl:UltraGridColumn>
<Header Caption="Column0"></Header>
</igtbl:UltraGridColumn>
</Columns>

<ExpandEffects ShadowColor="LightGray"></ExpandEffects>

<DropDownLayout RowHeightDefault="20px" BorderCollapse="Separate" Version="4.00">

<FrameStyle BorderWidth="2px" BorderStyle="Ridge" Font-Names="Verdana" BackColor="Silver" Width="325px" Height="130px" Cursor="Default"></FrameStyle>

<HeaderStyle BorderStyle="Solid" BackColor="LightGray">
<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
</HeaderStyle>

<RowStyle BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="White">
<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
</RowStyle>

<SelectedRowStyle ForeColor="White" BackColor="DarkBlue"></SelectedRowStyle>
</DropDownLayout>
</igcmbo:WebCombo> </FONT></TD><TD style="HEIGHT: 26px" align=left><FONT face=""></FONT></TD></TR><TR><TD style="WIDTH: 150px; height: 13px;" align=left><P><asp:Label id="Label4" runat="server">Tecnologia </asp:Label></P></TD><TD align=left style="height: 13px"><igcmbo:WebCombo id="TipoTecnologiaId" runat="server" BorderColor="Silver" BackColor="White" ForeColor="Black" BorderWidth="1px" BorderStyle="Solid" Version="4.00" SelForeColor="White" SelBackColor="DarkBlue" OnSelectedRowChanged="TipoTecnologiaId_SelectedRowChanged1"><Columns>
<igtbl:UltraGridColumn>
<Header Caption="Column0"></Header>
</igtbl:UltraGridColumn>
</Columns>

<ExpandEffects ShadowColor="LightGray"></ExpandEffects>

<DropDownLayout RowHeightDefault="20px" BorderCollapse="Separate" Version="4.00">

<FrameStyle BorderWidth="2px" BorderStyle="Ridge" Font-Names="Verdana" BackColor="Silver" Width="325px" Height="130px" Cursor="Default"></FrameStyle>

<HeaderStyle BorderStyle="Solid" BackColor="LightGray">
<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
</HeaderStyle>

<RowStyle BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="White">
<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
</RowStyle>

<SelectedRowStyle ForeColor="White" BackColor="DarkBlue"></SelectedRowStyle>
</DropDownLayout>
</igcmbo:WebCombo> </TD><TD align=left style="height: 13px"></TD></TR><TR><TD style="WIDTH: 150px; height: 15px;" 135px" align=left><asp:Label id="Label5" runat="server">Tecnologia Adicional</asp:Label></TD><TD align=left style="height: 15px">
    <igmisc:WebAsyncRefreshPanel ID="WebAsyncRefreshPanel2" runat="server" Height=""
        Width="" Direction="LeftToRight" Display="Inline" HorizontalAlign="Left">
        <asp:CheckBox ID="CBTecAd" runat="server" AutoPostBack="True" OnCheckedChanged="CBTecAd_CheckedChanged" /><igcmbo:WebCombo id="TipoTecnologiaAddId" runat="server" BorderColor="Silver" BackColor="White" ForeColor="Black" BorderWidth="1px" BorderStyle="Solid" Version="4.00" SelForeColor="White" SelBackColor="DarkBlue" Visible="False"><Columns>
<igtbl:UltraGridColumn>
<Header Caption="Column0"></Header>
</igtbl:UltraGridColumn>
</Columns>

<ExpandEffects ShadowColor="LightGray"></ExpandEffects>

<DropDownLayout RowHeightDefault="20px" BorderCollapse="Separate" Version="4.00">
<HeaderStyle BorderStyle="Solid" BackColor="LightGray">
<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
</HeaderStyle>

<FrameStyle BorderWidth="2px" BorderStyle="Ridge" Font-Names="Verdana" BackColor="Silver" Width="325px" Height="130px" Cursor="Default"></FrameStyle>

<RowStyle BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="White">
<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
</RowStyle>

<SelectedRowStyle ForeColor="White" BackColor="DarkBlue"></SelectedRowStyle>
</DropDownLayout>
</igcmbo:WebCombo> 
    </igmisc:WebAsyncRefreshPanel>
</TD><TD align=left style="height: 15px"></TD></TR><TR><TD style="WIDTH: 150px;" align=left><asp:Label id="Label11" runat="server">Sincronizacion</asp:Label></TD><TD align=left><igtxt:WebNumericEdit id="Sincronizacion" runat="server" Width="200px">
                                    </igtxt:WebNumericEdit> </TD><TD style="HEIGHT: 26px" align=left>Segs</TD></TR><TR><TD style="WIDTH: 150px;" align=left><asp:Label id="Label6" runat="server">Identificador Unico</asp:Label></TD><TD align=left><igcmbo:WebCombo id="TerminalCampoLlave" runat="server" BorderColor="Silver" BackColor="White" ForeColor="Black" BorderWidth="1px" BorderStyle="Solid" Version="4.00" SelForeColor="White" SelBackColor="DarkBlue">
                                        <Columns>
                                            <igtbl:UltraGridColumn>
                                                <header caption="Column0"></header>
                                            </igtbl:UltraGridColumn>
                                        </Columns>
                                        <ExpandEffects ShadowColor="LightGray" />
                                        <DropDownLayout RowHeightDefault="20px" BorderCollapse="Separate" Version="4.00">
                                            <FrameStyle BorderWidth="2px" BorderStyle="Ridge" Font-Names="Verdana" BackColor="Silver" Width="325px" Height="130px" Cursor="Default">
                                            </FrameStyle>
                                            <HeaderStyle BorderStyle="Solid" BackColor="LightGray">
                                                <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                            </HeaderStyle>
                                            <RowStyle BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="White">
                                                <BorderDetails WidthLeft="0px" WidthTop="0px" />
                                            </RowStyle>
                                            
                                        </DropDownLayout>
                                    </igcmbo:WebCombo>
                                    </TD><TD style="HEIGHT: 24px" align=left></TD></TR><TR><TD style="WIDTH: 150px;" align=left><asp:Label id="Label7" runat="server">Datos Adicionales</asp:Label></TD><TD align=left>
                                        &nbsp;<igmisc:WebAsyncRefreshPanel ID="WebAsyncRefreshPanel3" runat="server" Height=""
                                            Width="" Direction="LeftToRight" Display="Inline" HorizontalAlign="Left">
                                            <asp:CheckBox ID="CBDatAd" runat="server" AutoPostBack="True" OnCheckedChanged="CBDatAd_CheckedChanged" /><igcmbo:WebCombo id="TerminalCampoAdicional" runat="server" BorderColor="Silver" BackColor="White" ForeColor="Black" BorderWidth="1px" BorderStyle="Solid" Version="4.00" SelForeColor="White" SelBackColor="DarkBlue" Visible="False">
    <Columns>
        <igtbl:UltraGridColumn>
            <header caption="Column0"></header>
        </igtbl:UltraGridColumn>
    </Columns>
    <ExpandEffects ShadowColor="LightGray" />
    <DropDownLayout RowHeightDefault="20px" BorderCollapse="Separate" Version="4.00">
        <HeaderStyle BorderStyle="Solid" BackColor="LightGray">
            <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
        </HeaderStyle>
        <FrameStyle BorderWidth="2px" BorderStyle="Ridge" Font-Names="Verdana" BackColor="Silver" Width="325px" Height="130px" Cursor="Default">
        </FrameStyle>
        <RowStyle BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="White">
            <BorderDetails WidthLeft="0px" WidthTop="0px" />
        </RowStyle>
        
    </DropDownLayout>
</igcmbo:WebCombo>
                                        </igmisc:WebAsyncRefreshPanel>
                                        &nbsp;
</TD><TD style="HEIGHT: 24px" align=left></TD></TR><TR><TD style="WIDTH: 150px;" align=left><asp:Label id="Label12" runat="server">Genera Asistencia</asp:Label></TD><TD align=left><asp:CheckBox id="GeneraAsistencia" runat="server" Font-Names="" Font-Size=""></asp:CheckBox></TD><TD style="HEIGHT: 21px" align=left></TD></TR><TR><TD style="WIDTH: 150px;" align=left><asp:Label id="Label13" runat="server">Cobra Comidas</asp:Label></TD><TD align=left><asp:CheckBox id="CobraComidas" runat="server" Font-Names="" Font-Size=""></asp:CheckBox></TD><TD align=left></TD></TR><TR><TD style="WIDTH: 150px;" align=left><asp:Label id="Label14" runat="server" Width="147px">Minutos de Diferencia</asp:Label></TD><TD align=left><igtxt:WebNumericEdit id="MinutosDiferencia" runat="server" Width="200px">
                                    </igtxt:WebNumericEdit> </TD><TD align=left></TD></TR><TR><TD style="WIDTH: 150px;" align=left><asp:Label id="Label15" runat="server" Width="158px">Valida Horario de Entrada</asp:Label></TD><TD align=left><asp:CheckBox id="ValidaHorarioEntrada" runat="server" Font-Names="" Font-Size=""></asp:CheckBox></TD><TD align=left></TD></TR><TR><TD style="WIDTH: 150px;" align=left><asp:Label id="Label16" runat="server" Width="144px">Valida Horario de Salida</asp:Label></TD><TD align=left><asp:CheckBox id="ValidaHorarioSalida" runat="server" Font-Names="" Font-Size=""></asp:CheckBox></TD><TD align=left style="height: 22px"></TD></TR>
    <tr>
        <td align="left" style="width: 150px">
            <asp:Label ID="Label18" runat="server" Width="144px">Es Controladora</asp:Label></td>
        <td align="left">
            <asp:CheckBox ID="Controladora" runat="server" AutoPostBack="True" Font-Names=""
                Font-Size="" /></td>
        <td align="left" style="height: 22px">
        </td>
    </tr>
    <tr>
        <td align="left" colspan="3" style="height: 22px; text-align: center">
            <asp:CheckBox ID="CBEntrada" runat="server" Text="Es de Entrada" />
            &nbsp; &nbsp;&nbsp;
            <asp:CheckBox ID="CBSalida" runat="server" Text="Es de Salida" />&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:CheckBox ID="CBAceptarTA" runat="server" 
                Text="Aceptar tipo Acceso (zk y anviz)" />
        </td>
    </tr>
</TBODY></TABLE><TABLE id="Table10" cellSpacing=1 cellPadding=1 width=450 border=0>
    <tr>
        <td align="left" width="200" style="height: 1px">
            <asp:Label id="Label8" runat="server">Tipo de Dirección</asp:Label></td>
        <td align="left" style="width: 298px; height: 1px">
        <TABLE id="Table9" cellSpacing=1 cellPadding=1 border=0 style="height: 1px">
            <tr>
                <TD align=left valign="top">
                                        <asp:RadioButtonList ID="RBTipoDireccion" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RBTipoDireccion_SelectedIndexChanged" RepeatDirection="Horizontal">
                                            <asp:ListItem>USB</asp:ListItem>
                                            <asp:ListItem>Serial</asp:ListItem>
                                            <asp:ListItem>RS485</asp:ListItem>
                                            <asp:ListItem>Modem</asp:ListItem>
                                            <asp:ListItem>Red</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </TD>
            </tr>
        </table>
                                        <TABLE id="Table2" cellSpacing=1 cellPadding=1 border=0 style="height: 4px">
                                            <tr>
                                                <TD align=left width="200">
                                                    <igmisc:WebPanel ID="WPUsb" runat="server"  Visible="False" 
                                                        EnableAppStyling="True" StyleSetName="Caribbean">

                                                        <Header Text="USB" TextAlignment="Left">

                                                        </Header>
                                                        <Template>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                            Nombre
                                                                    </td>
                                                                    <td>
                                                            <igtxt:WebTextEdit id="txtUsbNombre" runat="server" BorderColor="#7F9DB9" Font-Names="" BorderWidth="1px" BorderStyle="Solid" Width="200px" CellSpacing="1" UseBrowserDefaults="False">
                                                                <ButtonsAppearance CustomButtonDefaultTriangleImages="Arrow">
                                                                    <ButtonStyle Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" ForeColor="#506080"
												BackColor="#C5D5FC">
                                                                    </ButtonStyle>
                                                                    <ButtonDisabledStyle BorderColor="#D7D7D7" ForeColor="#BEBEBE" BackColor="#E1E1DD">
                                                                    </ButtonDisabledStyle>
                                                                    <ButtonHoverStyle BackColor="#DCEDFD">
                                                                    </ButtonHoverStyle>
                                                                    <ButtonPressedStyle BackColor="#83A6F4">
                                                                    </ButtonPressedStyle>
                                                                </ButtonsAppearance>
                                                            </igtxt:WebTextEdit>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        ID</td>
                                                                    <td>
                                                                        <igtxt:WebNumericEdit ID="txtUsbID" runat="server" Width="200px">
                                                                        </igtxt:WebNumericEdit>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            &nbsp;
                                                        </Template>
                                                    </igmisc:WebPanel>
                                                    <igmisc:WebPanel ID="WPSerial" runat="server"  Visible="False" 
                                                        EnableAppStyling="True" StyleSetName="Caribbean">
        
                                                        <Header Text="Serial" TextAlignment="Left">

                                                        </Header>
                                                        <Template>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        Puerto</td>
                                                                    <td style="width: 8px">
                                                            <igtxt:WebNumericEdit ID="txtSerialPuerto" runat="server" Width="200px">
                                                            </igtxt:WebNumericEdit>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Velocidad&nbsp;</td>
                                                                    <td style="width: 8px">
                                                            <igtxt:WebNumericEdit ID="txtSerialVelocidad" runat="server" Width="200px">
                                                            </igtxt:WebNumericEdit>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </Template>
                                                    </igmisc:WebPanel>
                                                    <igmisc:WebPanel ID="WP485" runat="server" Visible="False" 
                                                        EnableAppStyling="True" StyleSetName="Caribbean">
                                                        
                                                        <Header Text="485" TextAlignment="Left">

                                                        </Header>
                                                        <Template>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        Puerto</td>
                                                                    <td>
                                                            <igtxt:WebNumericEdit ID="txt485Puerto" runat="server" Width="200px">
                                                            </igtxt:WebNumericEdit>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Velocidad</td>
                                                                    <td>
                                                            <igtxt:WebNumericEdit ID="txt485Velocidad" runat="server" Width="200px">
                                                            </igtxt:WebNumericEdit>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: left">
                                                            ID&nbsp;
                                                                    </td>
                                                                    <td>
                                                                        <igtxt:WebNumericEdit ID="txt485ID" runat="server" Width="200px">
                                                                        </igtxt:WebNumericEdit>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </Template>
                                                    </igmisc:WebPanel>
                                                    <igmisc:WebPanel ID="WPModem" runat="server" B Visible="False" 
                                                        EnableAppStyling="True" StyleSetName="Caribbean">
                                                        <Header Text="Modem" TextAlignment="Left">

                                                        </Header>
                                                        <Template>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                            Telefono&nbsp;
                                                                    </td>
                                                                    <td>
                                                            <igtxt:WebNumericEdit ID="txtModemTelefono" runat="server" Width="200px">
                                                            </igtxt:WebNumericEdit>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Puerto&nbsp;
                                                                    </td>
                                                                    <td>
                                                            <igtxt:WebNumericEdit ID="txtModemPuerto" runat="server" Width="200px">
                                                            </igtxt:WebNumericEdit>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Velocidad&nbsp;
                                                                    </td>
                                                                    <td>
                                                            <igtxt:WebNumericEdit ID="txtModemVelocidad" runat="server" Width="200px">
                                                            </igtxt:WebNumericEdit>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </Template>
                                                    </igmisc:WebPanel>
                                                    <igmisc:WebPanel ID="WPRed" runat="server" B Visible="False" 
                                                        EnableAppStyling="True" StyleSetName="Caribbean">
                                                        
                                                        <Header Text="Red" TextAlignment="Left">

                                                        </Header>
                                                        <Template>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                            Direccion&nbsp;&nbsp;
                                                                    </td>
                                                                    <td>
                                                            <igtxt:WebTextEdit id="txtRedDireccion" runat="server" BorderColor="#7F9DB9" Font-Names="" BorderWidth="1px" BorderStyle="Solid" Width="200px" CellSpacing="1" UseBrowserDefaults="False">
                                                                <ButtonsAppearance CustomButtonDefaultTriangleImages="Arrow">
                                                                    <ButtonStyle Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" ForeColor="#506080"
												BackColor="#C5D5FC">
                                                                    </ButtonStyle>
                                                                    <ButtonDisabledStyle BorderColor="#D7D7D7" ForeColor="#BEBEBE" BackColor="#E1E1DD">
                                                                    </ButtonDisabledStyle>
                                                                    <ButtonHoverStyle BackColor="#DCEDFD">
                                                                    </ButtonHoverStyle>
                                                                    <ButtonPressedStyle BackColor="#83A6F4">
                                                                    </ButtonPressedStyle>
                                                                </ButtonsAppearance>
                                                            </igtxt:WebTextEdit>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Puerto&nbsp;
                                                                    </td>
                                                                    <td>
                                                            <igtxt:WebNumericEdit ID="txtRedPuerto" runat="server" Width="200px">
                                                            </igtxt:WebNumericEdit>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        ID</td>
                                                                    <td>
                                                                        <igtxt:WebNumericEdit ID="txtRedID" runat="server" Width="200px">
                                                                        </igtxt:WebNumericEdit>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </Template>
                                                    </igmisc:WebPanel>
                                                    <igtxt:WebTextEdit id="TIP" runat="server" BorderColor="#7F9DB9" Font-Names="" BorderWidth="1px" BorderStyle="Solid" Width="200px" CellSpacing="1" UseBrowserDefaults="False" Visible="False">
                                                        <ButtonsAppearance CustomButtonDefaultTriangleImages="Arrow">
                                                            <ButtonStyle Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" ForeColor="#506080"
												BackColor="#C5D5FC">
                                                            </ButtonStyle>
                                                            <ButtonDisabledStyle BorderColor="#D7D7D7" ForeColor="#BEBEBE" BackColor="#E1E1DD">
                                                            </ButtonDisabledStyle>
                                                            <ButtonHoverStyle BackColor="#DCEDFD">
                                                            </ButtonHoverStyle>
                                                            <ButtonPressedStyle BackColor="#83A6F4">
                                                            </ButtonPressedStyle>
                                                        </ButtonsAppearance>
                                                    </igtxt:WebTextEdit>
                                                </TD>
                                            </tr>
                                        </table>
        </td>
    </tr>
</table>
    </igmisc:WebAsyncRefreshPanel>
    <TABLE id="Table11" cellSpacing=1 cellPadding=1 width=450 border=0>
        <tr>
            <TD style="WIDTH: 135px" align=left><asp:Label id="Label17" runat="server" Width="80px">Permite Enrolar</asp:Label></TD><TD align=left style="width: 62px"><asp:CheckBox id="Enrolar" runat="server" Font-Names="" Font-Size=""></asp:CheckBox>&nbsp;</TD><TD align=left></TD>
        </tr>
        <tr>
            <TD style="WIDTH: 135px" align=left></TD><TD align=left style="width: 62px"><asp:CheckBox id="CBBorrar" runat="server" Text="Borrar Registro" Font-Names="" Font-Size=""></asp:CheckBox></TD><TD style="COLOR: red" align=left><asp:Label id="LBorrar" runat="server" Width="326px">* Seleccione esta opción si lo que quiere es borrar la Terminal</asp:Label></TD>
        </tr>
    </table>
</Template>
</igmisc:webpanel>
                        &nbsp;&nbsp;<br />
                        &nbsp;<igtxt:WebImageButton ID="btnacceso" runat="server" Height="22px" OnClick="btnacceso_Click"
                            Text="Acceso de Personas" UseBrowserDefaults="False" Width="193px" ImageTextSpacing="4">
                            <Alignments VerticalImage="Bottom" HorizontalAll="NotSet" VerticalAll="NotSet" />
                            <Appearance>
                                <Style Cursor="Default"></Style>
                                <Image Height="18px" Url="./Imagenes/Empleado.png" Width="20px" />
                            </Appearance>
                            <RoundedCorners HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif"
                                MaxHeight="80" MaxWidth="400" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif" />
                        </igtxt:WebImageButton>
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 0%" align="center">
						<asp:Label id="LError" runat="server" ForeColor="#CC0033" Font-Size="" Font-Names=""></asp:Label>
						<asp:Label id="LCorrecto" runat="server" ForeColor="#00C000" Font-Size="" Font-Names=""></asp:Label></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 0%" align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<igtxt:WebImageButton ID="BDeshacerCambios" runat="server"
                            Height="22px" OnClick="BDeshacerCambios_Click" Text="Deshacer Cambios" UseBrowserDefaults="False"
                            Width="150px" ImageTextSpacing="4">
                            <Alignments VerticalImage="Middle" VerticalAll="Bottom" />
                            <Appearance>
                                <Image Url="./Imagenes/Undo.png" Height="16px" Width="16px" />
                                <Style Cursor="Default"></Style>
                            </Appearance>
                            <RoundedCorners HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif"
                                MaxHeight="80" MaxWidth="400" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif" />
                        </igtxt:WebImageButton>
                        &nbsp; &nbsp;
                        <igtxt:WebImageButton ID="BGuardarCambios" runat="server" Height="22px" OnClick="BGuardarCambios_Click"
                            Text="Guardar Cambios" UseBrowserDefaults="False" Width="150px" ImageTextSpacing="4">
                            <Alignments VerticalImage="Middle" VerticalAll="Bottom" />
                            <RoundedCorners HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif"
                                MaxHeight="80" MaxWidth="400" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif" />
                            <Appearance>
                                <Style Cursor="Default">
								</Style>
                                <Image Url="./Imagenes/Save_as.png" Height="16px" Width="16px" />
                            </Appearance>
                        </igtxt:WebImageButton>
                    </TD>
				</TR>
			</TABLE>
 </div>
    </form>
</body>
</html>

