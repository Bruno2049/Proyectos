<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WF_TerminalesEd.aspx.cs" Inherits="WF_TerminalesEd" %>

<%@ Register assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.WebUI.Misc" tagprefix="igmisc" %>
<%@ Register assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.WebUI.WebDataInput" tagprefix="igtxt" %>
<%@ Register assembly="Infragistics2.WebUI.WebCombo.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.WebUI.WebCombo" tagprefix="igcmbo" %>
<%@ Register assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.WebUI.UltraWebGrid" tagprefix="igtbl" %>
<%@ Register assembly="Infragistics2.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.Web.UI.LayoutControls" tagprefix="ig" %>

<%@ Register assembly="Infragistics2.WebUI.WebDateChooser.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.WebSchedule" tagprefix="igsch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 150px;
            height: 23px;
        }
        .style2
        {
            height: 23px;
        }
        .style3
        {
            width: 191px;
        }
    </style>
</head>
<body style="font-size: 11px; font-family: tahoma; text-align: center; margin: 0px">
    <form id="form1" runat="server" >
    <div>
        <table width="600" style="height: 380px">
				<TR>
					<TD style="HEIGHT: 0%">
						</TD>
				</TR>
				<TR>
					<TD align="center" style="BACKGROUND-POSITION: right center; BACKGROUND-IMAGE: url(./Imagenes/fondoeClock3.jpg); BACKGROUND-REPEAT: no-repeat">
                            <PanelStyle BackgroundImage="./skins/spacer.gif">
                            </PanelStyle>


<Header TextAlignment="Left" Text="Datos de Terminal">


</Header>
<Template>

<TABLE style="HEIGHT: 226px" id="Table5" cellSpacing=1 cellPadding=1 width=450 border=0><TBODY><TR><TD style="WIDTH: 150px" align=left><P>
<asp:Label id="Lbl_TERMINAL_ID" runat="server">Id Terminal</asp:Label></P></TD>
    <TD align=left colspan="3">
        <igtxt:WebNumericEdit id="Wne_TERMINAL_ID" runat="server" BorderColor="#7F9DB9" 
            BorderWidth="1px" BorderStyle="Solid" Width="200px" Enabled="False" 
            CellSpacing="1" UseBrowserDefaults="False" DataMode="Text" TabIndex="1">
										<ButtonsAppearance CustomButtonDefaultTriangleImages="Arrow">
											<ButtonPressedStyle BackColor="#83A6F4"></ButtonPressedStyle>
											<ButtonDisabledStyle BorderColor="#D7D7D7" ForeColor="#BEBEBE" BackColor="#E1E1DD"></ButtonDisabledStyle>
											<ButtonStyle Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" ForeColor="#506080"
												BackColor="#C5D5FC"></ButtonStyle>
											<ButtonHoverStyle BackColor="#DCEDFD"></ButtonHoverStyle>
										</ButtonsAppearance>
										<SpinButtons DefaultTriangleImages="ArrowSmall" Width="15px"></SpinButtons>
									</igtxt:WebNumericEdit><FONT face=""></FONT></TD>
    <td align="left">
        &nbsp;</td>
    </TR>
    <tr>
        <td align="left" style="WIDTH: 150px">
            <asp:Label ID="Lbl_TIPO_TERMINAL_ACCESO_ID" runat="server">Tipo Terminal</asp:Label>
        </td>
        <td align="left" colspan="3">
            <igcmbo:WebCombo ID="Wco_TIPO_TERMINAL_ACCESO_ID" runat="server" 
                BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" 
                ForeColor="Black" SelBackColor="DarkBlue" SelForeColor="White" 
                Version="4.00" TabIndex="2">
                <expandeffects shadowcolor="LightGray" />
                <Columns>
                    <igtbl:UltraGridColumn>
                        <header caption="Column0">
                        </header>
                    </igtbl:UltraGridColumn>
                </Columns>
                <dropdownlayout bordercollapse="Separate" rowheightdefault="20px" 
                    version="4.00">
                    <framestyle backcolor="Silver" borderstyle="Ridge" borderwidth="2px" 
                        cursor="Default" font-names="Verdana" height="130px" width="325px">
                    </framestyle>
                    <HeaderStyle BackColor="LightGray" BorderStyle="Solid">
                    <borderdetails colorleft="White" colortop="White" widthleft="1px" 
                        widthtop="1px" />
                    </HeaderStyle>
                    <RowStyle BackColor="White" BorderColor="Gray" BorderStyle="Solid" 
                        BorderWidth="1px">
                    <borderdetails widthleft="0px" widthtop="0px" />
                    </RowStyle>
                    <SelectedRowStyle BackColor="DarkBlue" ForeColor="White" />
                </dropdownlayout>
            </igcmbo:WebCombo>
        </td>
        <td align="left">
            &nbsp;</td>
    </tr>
    <TR><TD style="WIDTH: 150px" align=left>
    <asp:Label id="Lbl_TERMINAL_NOMBRE" runat="server">Nombre</asp:Label></TD>
        <TD align=left colspan="3">
    <igtxt:WebTextEdit id="Wtx_TERMINAL_NOMBRE" runat="server" 
        BorderColor="#7F9DB9" Font-Names="" BorderWidth="1px" BorderStyle="Solid" 
        Width="200px" CellSpacing="1" UseBrowserDefaults="False" TabIndex="3">
										<ButtonsAppearance CustomButtonDefaultTriangleImages="Arrow">
											<ButtonPressedStyle BackColor="#83A6F4"></ButtonPressedStyle>
											<ButtonDisabledStyle BorderColor="#D7D7D7" ForeColor="#BEBEBE" BackColor="#E1E1DD"></ButtonDisabledStyle>
											<ButtonStyle Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" ForeColor="#506080"
												BackColor="#C5D5FC"></ButtonStyle>
											<ButtonHoverStyle BackColor="#DCEDFD"></ButtonHoverStyle>
										</ButtonsAppearance>
									</igtxt:WebTextEdit><FONT face=""></FONT></TD>
    <td align="left">
        <asp:RequiredFieldValidator ID="Rfv_TERMINAL_NOMBRE" runat="server" 
            ControlToValidate="Wtx_TERMINAL_NOMBRE" ErrorMessage="Este dato es obligatorio" 
            Font-Names="" Font-Size="">*</asp:RequiredFieldValidator>
    </td>
    </TR>
    <TR><TD style="WIDTH: 150px" align=left>
    <asp:Label id="Lbl_TERMINAL_AGRUPACION" runat="server">Agrupación</asp:Label></TD>
        <TD align=left colspan="3">
    <igtxt:WebTextEdit id="Wtx_TERMINAL_AGRUPACION" runat="server" 
        BorderColor="#7F9DB9" Font-Names="" BorderWidth="1px" BorderStyle="Solid" 
        Width="200px" CellSpacing="1" UseBrowserDefaults="False" TabIndex="3">
										<ButtonsAppearance CustomButtonDefaultTriangleImages="Arrow">
											<ButtonPressedStyle BackColor="#83A6F4"></ButtonPressedStyle>
											<ButtonDisabledStyle BorderColor="#D7D7D7" ForeColor="#BEBEBE" BackColor="#E1E1DD"></ButtonDisabledStyle>
											<ButtonStyle Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" ForeColor="#506080"
												BackColor="#C5D5FC"></ButtonStyle>
											<ButtonHoverStyle BackColor="#DCEDFD"></ButtonHoverStyle>
										</ButtonsAppearance>
									</igtxt:WebTextEdit></TD>
    <td align="left">
        <asp:RequiredFieldValidator ID="Rfv_TERMINAL_AGRUPACION" runat="server" 
            ControlToValidate="Wtx_TERMINAL_AGRUPACION" ErrorMessage="Este dato es obligatorio" 
            Font-Names="" Font-Size="">*</asp:RequiredFieldValidator>
    </td>
    </TR><TR><TD style="WIDTH: 150px;" align=left>
    <asp:Label id="Lbl_ALMACEN_VEC_ID" runat="server" Width="139px">Almacen de Vectores</asp:Label></TD>
        <TD align=left colspan="3">
    <igcmbo:WebCombo id="Wco_ALMACEN_VEC_ID" runat="server" BorderColor="Silver" 
        BackColor="White" ForeColor="Black" BorderWidth="1px" BorderStyle="Solid" 
        Version="4.00" SelForeColor="White" SelBackColor="DarkBlue" TabIndex="4">

<ExpandEffects ShadowColor="LightGray"></ExpandEffects>

    <Columns>
<igtbl:UltraGridColumn>
<Header Caption="Column0"></Header>
</igtbl:UltraGridColumn>
</Columns>

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
</igcmbo:WebCombo> </TD>
    <td align="left">
        &nbsp;</td>
    </TR><TR><TD style="WIDTH: 150px;" align=left>
    <asp:Label id="Lbl_SITIO_ID" runat="server">Sitio</asp:Label></TD><TD align=left 
            colspan="3">
    <igcmbo:WebCombo id="Wco_SITIO_ID" runat="server" BorderColor="Silver" 
        BackColor="White" ForeColor="Black" BorderWidth="1px" BorderStyle="Solid" 
        Version="4.00" SelForeColor="White" SelBackColor="DarkBlue" TabIndex="5">

<ExpandEffects ShadowColor="LightGray"></ExpandEffects>

    <Columns>
<igtbl:UltraGridColumn>
<Header Caption="Column0"></Header>
</igtbl:UltraGridColumn>
</Columns>

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
</igcmbo:WebCombo> </TD>
    <td align="left">
        &nbsp;</td>
    </TR><TR><TD style="WIDTH: 150px;" align=left>
    <asp:Label id="Lbl_MODELO_TERMINAL_ID" runat="server">Modelo</asp:Label></TD>
        <TD align=left colspan="3"><FONT face="">
    <igcmbo:WebCombo id="Wco_MODELO_TERMINAL_ID" runat="server" 
        BorderColor="Silver" BackColor="White" ForeColor="Black" BorderWidth="1px" 
        BorderStyle="Solid" Version="4.00" SelForeColor="White" 
            SelBackColor="DarkBlue" TabIndex="6">

<ExpandEffects ShadowColor="LightGray"></ExpandEffects>

    <Columns>
<igtbl:UltraGridColumn>
<Header Caption="Column0"></Header>
</igtbl:UltraGridColumn>
</Columns>

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
</igcmbo:WebCombo> </FONT></TD>
    <td align="left">
        &nbsp;</td>
    </TR><TR><TD style="WIDTH: 150px; height: 13px;" align=left><P>
    <asp:Label id="Lbl_TIPO_TECNOLOGIA_ID" runat="server">Tecnologia </asp:Label></P></TD>
        <TD align=left style="height: 13px" colspan="3">
    <igcmbo:WebCombo id="Wco_TIPO_TECNOLOGIA_ID" runat="server" 
        BorderColor="Silver" BackColor="White" ForeColor="Black" BorderWidth="1px" 
        BorderStyle="Solid" Version="4.00" SelForeColor="White" 
            SelBackColor="DarkBlue" TabIndex="7">

<ExpandEffects ShadowColor="LightGray"></ExpandEffects>

    <Columns>
<igtbl:UltraGridColumn>
<Header Caption="Column0"></Header>
</igtbl:UltraGridColumn>
</Columns>

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
</igcmbo:WebCombo> </TD>
    <td align="left" style="height: 13px">
        &nbsp;</td>
    </TR><TR><TD style="WIDTH: 150px; height: 15px;" 135px" align=left>
    <asp:Label id="Lbl_TIPO_TECNOLOGIA_ADD_ID" runat="server">Tecnologia Adicional</asp:Label></TD>
        <TD align=left style="height: 15px" colspan="3">
        <igcmbo:WebCombo id="Wco_TIPO_TECNOLOGIA_ADD_ID" runat="server" 
            BorderColor="Silver" BackColor="White" ForeColor="Black" BorderWidth="1px" 
            BorderStyle="Solid" Version="4.00" SelForeColor="White" SelBackColor="DarkBlue" 
            Visible="False" TabIndex="9">

<ExpandEffects ShadowColor="LightGray"></ExpandEffects>

            <Columns>
<igtbl:UltraGridColumn>
<Header Caption="Column0"></Header>
</igtbl:UltraGridColumn>
</Columns>

<DropDownLayout RowHeightDefault="20px" BorderCollapse="Separate" Version="4.00">

<FrameStyle BorderWidth="2px" BorderStyle="Ridge" Font-Names="Verdana" BackColor="Silver" Width="325px" Height="130px" Cursor="Default"></FrameStyle>

<HeaderStyle BorderStyle="Solid" BackColor="LightGray">
    <BorderDetails ColorLeft="White" ColorTop="White" 
                                WidthLeft="1px" WidthTop="1px" />
</HeaderStyle>

<RowStyle BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="White">
    <BorderDetails WidthLeft="0px" WidthTop="0px" />
</RowStyle>

<SelectedRowStyle ForeColor="White" BackColor="DarkBlue"></SelectedRowStyle>
</DropDownLayout>
</igcmbo:WebCombo> 
</TD>
    <td align="left" style="height: 15px">
        <asp:CheckBox ID="Chb_TIPO_TECNOLOGIA_ADD_ID" runat="server" 
            AutoPostBack="True" 
            oncheckedchanged="Chb_TIPO_TECNOLOGIA_ADD_ID_CheckedChanged" 
            TabIndex="8" />
    </td>
    </TR><TR><TD style="WIDTH: 150px;" align=left>
    <asp:Label id="Lbl_TERMINAL_SINCRONIZACION" runat="server">Sincronizacion</asp:Label></TD>
        <TD align=left colspan="3">
    <igtxt:WebNumericEdit id="Wne_TERMINAL_SINCRONIZACION" runat="server" 
        Width="200px" DataMode="Text" TabIndex="10">
                                    </igtxt:WebNumericEdit> </TD>
    <td align="left">
        <asp:Label ID="Lbl_TERMINAL_SINCRONIZACION_SEGS" runat="server">Segundos</asp:Label>
    </td>
    </TR><TR><TD style="WIDTH: 150px;" align=left>
    <asp:Label id="Lbl_TERMINAL_CAMPO_LLAVE" runat="server">Identificador Unico</asp:Label></TD>
        <TD align=left colspan="3">
    <igcmbo:WebCombo id="Wco_TERMINAL_CAMPO_LLAVE" runat="server" 
        BorderColor="Silver" BackColor="White" ForeColor="Black" BorderWidth="1px" 
        BorderStyle="Solid" Version="4.00" SelForeColor="White" SelBackColor="DarkBlue" 
                TabIndex="11">
                                        <ExpandEffects ShadowColor="LightGray" />
                                        <Columns>
                                            <igtbl:UltraGridColumn>
                                                <header caption="Column0"></header>
                                            </igtbl:UltraGridColumn>
                                        </Columns>
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
                                    </TD>
    <td align="left">
        &nbsp;</td>
    </TR><TR><TD style="WIDTH: 150px;" align=left>
    <asp:Label id="Lbl_TERMINAL_CAMPO_ADICIONAL" runat="server">Datos Adicionales</asp:Label></TD>
        <TD align=left colspan="3">
                                        
                                            <igcmbo:WebCombo id="Wco_TERMINAL_CAMPO_ADICIONAL" runat="server" 
                                                BorderColor="Silver" BackColor="White" ForeColor="Black" BorderWidth="1px" 
                                                BorderStyle="Solid" Version="4.00" SelForeColor="White" SelBackColor="DarkBlue" 
                                                Visible="False" TabIndex="13">
    <ExpandEffects ShadowColor="LightGray" />
    <Columns>
        <igtbl:UltraGridColumn>
            <header caption="Column0"></header>
        </igtbl:UltraGridColumn>
    </Columns>
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
                                        
</TD>
    <td align="left">
        <asp:CheckBox ID="Chb_TERMINAL_CAMPO_ADICIONAL" runat="server" 
            AutoPostBack="True" 
            oncheckedchanged="Chb_TERMINAL_CAMPO_ADICIONAL_CheckedChanged" 
            TabIndex="12" />
    </td>
    </TR><TR><TD style="WIDTH: 150px;" align=left>
    <asp:Label id="Lbl_TERMINAL_ASISTENCIA" runat="server">Genera Asistencia</asp:Label></TD>
        <TD align=left colspan="3">
    <asp:CheckBox id="Chb_TERMINAL_ASISTENCIA" runat="server" Font-Names="" 
        Font-Size="" TabIndex="14"></asp:CheckBox></TD>
    <td align="left">
        &nbsp;</td>
    </TR><TR><TD align=left class="style1">
    <asp:Label id="Lbl_TERMINAL_COMIDA" runat="server">Cobra Comidas</asp:Label></TD>
    <TD align=left class="style2" colspan="3">
    <asp:CheckBox id="Chb_TERMINAL_COMIDA" runat="server" Font-Names="" 
        Font-Size="" TabIndex="15"></asp:CheckBox></TD>
    <td align="left" class="style2">
        </td>
    </TR><TR><TD style="WIDTH: 150px;" align=left>
    <asp:Label id="Lbl_TERMINAL_MINUTOS_DIF" runat="server" Width="147px">Minutos de Diferencia</asp:Label></TD>
        <TD align=left colspan="3">
    <igtxt:WebNumericEdit id="Wne_TERMINAL_MINUTOS_DIF" runat="server" 
        Width="200px" DataMode="Text" TabIndex="16">
                                    </igtxt:WebNumericEdit> </TD>
    <td align="left">
        &nbsp;</td>
    </TR><TR><TD style="WIDTH: 150px;" align=left>
    <asp:Label id="Lbl_TERMINAL_VALIDAHORARIOE" runat="server" Width="158px">Valida Horario de Entrada</asp:Label></TD>
        <TD align=left colspan="3">
    <asp:CheckBox id="Chb_TERMINAL_VALIDAHORARIOE" runat="server" Font-Names="" 
        Font-Size="" TabIndex="17" /> </asp:CheckBox></TD>
    <td align="left">
        &nbsp;</td>
    </TR><TR><TD style="WIDTH: 150px;" align=left>
    <asp:Label id="Lbl_TERMINA_VALIDAHORARIOS" runat="server" Width="144px">Valida Horario de Salida</asp:Label></TD>
        <TD align=left colspan="3">
    <asp:CheckBox id="Chb_TERMINAL_VALIDAHORARIOS" runat="server" Font-Names="" 
        Font-Size="" TabIndex="18"></asp:CheckBox></TD>
    <td align="left">
        &nbsp;</td>
    </TR>
    <tr>
        <td align="left" style="width: 150px">
            <asp:Label ID="Lbl_Controladora" runat="server" Width="144px">Es Controladora</asp:Label></td>
        <td align="left" colspan="3">
            <asp:CheckBox ID="Chb_Controladora" runat="server" AutoPostBack="True" Font-Names=""
                Font-Size="" TabIndex="19" /></td>
        <td align="left">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="left" style="width: 150px;">
            <asp:Label ID="Lbl_TERMINAL_ESENTRADA" runat="server" Text="Es de Entrada"></asp:Label>
        </td>
        <td align="left" colspan="3">
            <asp:CheckBox ID="Chb_TERMINAL_ESENTRADA" runat="server" TabIndex="20" />
        </td>
        <td align="left">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="left" style="width: 150px">
            <asp:Label ID="Lbl_TERMINAL_ESSALIDA" runat="server" Text="Es de Salida"></asp:Label>
        </td>
        <td align="left" colspan="3">
            <asp:CheckBox ID="Chb_TERMINAL_ESSALIDA" runat="server" TabIndex="21" />
        </td>
        <td align="left">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="left" style="width: 150px">
            <asp:Label ID="Lbl_TERMINAL_ACEPTA_TA" runat="server" 
                Text="Aceptar tipo Acceso (ZK y Anviz)"></asp:Label>
        </td>
        <td align="left" colspan="3">
            <asp:CheckBox ID="Chb_TERMINAL_ACEPTA_TA" runat="server" TabIndex="22" />
        </td>
        <td align="left">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="left" style="width: 150px">
            <asp:Label ID="Lbl_TERMINAL_MODELO" runat="server" 
                Text="Modelo de Terminal (Fabricante)"></asp:Label>
        </td>
        <td align="left" colspan="3">
    <igtxt:WebTextEdit id="Wtx_TERMINAL_MODELO" runat="server" 
        BorderColor="#7F9DB9" Font-Names="" BorderWidth="1px" BorderStyle="Solid" 
        Width="200px" CellSpacing="1" UseBrowserDefaults="False" TabIndex="3">
										<ButtonsAppearance CustomButtonDefaultTriangleImages="Arrow">
											<ButtonPressedStyle BackColor="#83A6F4"></ButtonPressedStyle>
											<ButtonDisabledStyle BorderColor="#D7D7D7" ForeColor="#BEBEBE" BackColor="#E1E1DD"></ButtonDisabledStyle>
											<ButtonStyle Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" ForeColor="#506080"
												BackColor="#C5D5FC"></ButtonStyle>
											<ButtonHoverStyle BackColor="#DCEDFD"></ButtonHoverStyle>
										</ButtonsAppearance>
									</igtxt:WebTextEdit>
        </td>
        <td align="left">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="left" style="width: 150px">
            <asp:Label ID="Lbl_TERMINAL_NO_SERIE" runat="server" Text="No Serie"></asp:Label>
        </td>
        <td align="left" colspan="3">
    <igtxt:WebTextEdit id="Wtx_TERMINAL_NO_SERIE" runat="server" 
        BorderColor="#7F9DB9" Font-Names="" BorderWidth="1px" BorderStyle="Solid" 
        Width="200px" CellSpacing="1" UseBrowserDefaults="False" TabIndex="3">
										<ButtonsAppearance CustomButtonDefaultTriangleImages="Arrow">
											<ButtonPressedStyle BackColor="#83A6F4"></ButtonPressedStyle>
											<ButtonDisabledStyle BorderColor="#D7D7D7" ForeColor="#BEBEBE" BackColor="#E1E1DD"></ButtonDisabledStyle>
											<ButtonStyle Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" ForeColor="#506080"
												BackColor="#C5D5FC"></ButtonStyle>
											<ButtonHoverStyle BackColor="#DCEDFD"></ButtonHoverStyle>
										</ButtonsAppearance>
									</igtxt:WebTextEdit>
        </td>
        <td align="left">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="left" style="width: 150px">
            <asp:Label ID="Lbl_TERMINAL_FIRMWARE_VER" runat="server" 
                Text="Version Firmware"></asp:Label>
        </td>
        <td align="left" colspan="3">
    <igtxt:WebTextEdit id="Wtx_TERMINAL_FIRMWARE_VER" runat="server" 
        BorderColor="#7F9DB9" Font-Names="" BorderWidth="1px" BorderStyle="Solid" 
        Width="200px" CellSpacing="1" UseBrowserDefaults="False" TabIndex="3">
										<ButtonsAppearance CustomButtonDefaultTriangleImages="Arrow">
											<ButtonPressedStyle BackColor="#83A6F4"></ButtonPressedStyle>
											<ButtonDisabledStyle BorderColor="#D7D7D7" ForeColor="#BEBEBE" BackColor="#E1E1DD"></ButtonDisabledStyle>
											<ButtonStyle Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" ForeColor="#506080"
												BackColor="#C5D5FC"></ButtonStyle>
											<ButtonHoverStyle BackColor="#DCEDFD"></ButtonHoverStyle>
										</ButtonsAppearance>
									</igtxt:WebTextEdit>
        </td>
        <td align="left">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="left" style="width: 150px">
            <asp:Label ID="Lbl_TERMINAL_NO_HUELLAS" runat="server" Text="No Huellas"></asp:Label>
        </td>
        <td align="left" colspan="3">
    <igtxt:WebNumericEdit id="Wne_TERMINAL_NO_HUELLAS" runat="server" 
        Width="200px" DataMode="Text" TabIndex="10">
                                    </igtxt:WebNumericEdit> 
        </td>
        <td align="left">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="left" style="width: 150px">
            <asp:Label ID="Lbl_TERMINAL_NO_EMPLEADOS" runat="server" 
                Text="Número de Empleados"></asp:Label>
        </td>
        <td align="left" colspan="3">
    <igtxt:WebNumericEdit id="Wne_TERMINAL_NO_EMPLEADOS" runat="server" 
        Width="200px" DataMode="Text" TabIndex="10">
                                    </igtxt:WebNumericEdit> 
        </td>
        <td align="left">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="left" style="width: 150px">
            <asp:Label ID="Lbl_TERMINAL_NO_TARJETAS" runat="server" 
                Text="Número de Tarjetas"></asp:Label>
        </td>
        <td align="left" colspan="3">
    <igtxt:WebNumericEdit id="Wne_TERMINAL_NO_TARJETAS" runat="server" 
        Width="200px" DataMode="Text" TabIndex="10">
                                    </igtxt:WebNumericEdit> 
        </td>
        <td align="left">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="left" style="width: 150px">
            <asp:Label ID="Lbl_TERMINAL_NO_ROSTROS" runat="server" Text="Número de Rostros"></asp:Label>
        </td>
        <td align="left" colspan="3">
    <igtxt:WebNumericEdit id="Wne_TERMINAL_NO_ROSTROS" runat="server" 
        Width="200px" DataMode="Text" TabIndex="10">
                                    </igtxt:WebNumericEdit> 
        </td>
        <td align="left">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="left" style="width: 150px">
            <asp:Label ID="Lbl_TERMINAL_NO_CHECADAS" runat="server" 
                Text="Número de Checadas"></asp:Label>
        </td>
        <td align="left" colspan="3">
    <igtxt:WebNumericEdit id="Wne_TERMINAL_NO_CHECADAS" runat="server" 
        Width="200px" DataMode="Text" TabIndex="10">
                                    </igtxt:WebNumericEdit> 
        </td>
        <td align="left">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="left" style="width: 150px">
            <asp:Label ID="Lbl_TERMINAL_NO_PALMAS" runat="server" Text="Número de Palmas"></asp:Label>
        </td>
        <td align="left" colspan="3">
    <igtxt:WebNumericEdit id="Wne_TERMINAL_NO_PALMAS" runat="server" 
        Width="200px" DataMode="Text" TabIndex="10">
                                    </igtxt:WebNumericEdit> 
        </td>
        <td align="left">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="left" style="width: 150px">
            <asp:Label ID="Lbl_TERMINAL_NO_IRIS" runat="server" Text="Número de Iris"></asp:Label>
        </td>
        <td align="left" colspan="3">
    <igtxt:WebNumericEdit id="Wne_TERMINAL_NO_IRIS" runat="server" 
        Width="200px" DataMode="Text" TabIndex="10">
                                    </igtxt:WebNumericEdit> 
        </td>
        <td align="left">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="left" style="width: 150px">
            <asp:Label ID="Lbl_TERMINAL_GARANTIA_INICIO" runat="server" 
                Text="Fecha de Inicio de la Garantía "></asp:Label>
        </td>
        <td align="left" colspan="3">
            <igsch:WebDateChooser ID="Wdc_TERMINAL_GARANTIA_INICIO" runat="server">
            </igsch:WebDateChooser>
        </td>
        <td align="left">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="left" style="width: 150px">
            <asp:Label ID="Lbl_TERMINAL_GARANTIA_FIN" runat="server" 
                Text="Fecha de Fin de Garantía"></asp:Label>
        </td>
        <td align="left" colspan="3">
            <igsch:WebDateChooser ID="Wdc_TERMINAL_GARANTIA_FIN" runat="server">
            </igsch:WebDateChooser>
        </td>
        <td align="left">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="left" style="width: 150px">
            <asp:Label ID="Lbl_TERMINAL_DESCRIPCION" runat="server" 
                Text="Descripción de la Terminal"></asp:Label>
        </td>
        <td align="left" colspan="3">
            <asp:TextBox ID="Tbx_TERMINAL_DESCRIPCION" runat="server" TextMode="MultiLine" 
                TabIndex="23"></asp:TextBox>
        </td>
        <td align="left">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="left" style="width: 150px" rowspan="3">
            <asp:Label ID="Lbl_TERMINALES_DEXTRAS_BIN" runat="server" 
                Text="Imagen de Terminal"></asp:Label>
            <br />
            <asp:CheckBox ID="Chb_TERMINALES_DEXTRAS_BIN" runat="server" 
                AutoPostBack="True" Font-Names=""
                Font-Size="" 
                oncheckedchanged="Chb_TERMINALES_DEXTRAS_BIN_CheckedChanged" 
                Checked="True" TabIndex="24" Visible="False" />
            <br />
                <asp:Label id="Lbl_TERMINALES_DEXTRAS_BIN0" runat="server" Width="145px" 
                ForeColor="Red" Height="40px" Visible="False">* Seleccione esta opción <br />si desea agregar una imagen</asp:Label>
        </td>
        <td align="left" colspan="3">
            <asp:Image ID="Img_TERMINALES_DEXTRAS_BIN" runat="server" Height="256px" 
                Width="256px" Visible="False" />
            <br />
        </td>
        <td align="left" rowspan="3">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="left" colspan="3">
                <input id="Fup_TERMINALES_DEXTRAS_BIN" runat="server" name="File1" 
                style="width: 240px; height: 25px;" type="file" tabindex="25" 
                    visible="False" /></td>
    </tr>
    <tr>
        <td align="left">
                <igtxt:WebImageButton ID="WIBtn_TERMINALES_DEXTRAS_BIN" runat="server" 
                    ImageTextSpacing="4" OnClick="WIBtn_TERMINALES_DEXTRAS_BIN_Click" Text="Subir Foto"
                            UseBrowserDefaults="False" Visible="False">
                    <Appearance>
                        <Image Height="18px" Url="./Imagenes/panel-screenshot.png" Width="20px" />
                        <ButtonStyle Cursor="Default" Font-Size="X-Small">
                        </ButtonStyle>
                    </Appearance>
                    <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                                HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" 
                        MaxHeight="80" MaxWidth="400"
                                PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                    <PressedAppearance>
                        <ButtonStyle Font-Size="Small">
                        </ButtonStyle>
                    </PressedAppearance>
                    <Alignments VerticalAll="Bottom" VerticalImage="Middle" />
                </igtxt:WebImageButton>
            </td>
        <td align="left">
                &nbsp;</td>
        <td align="left">
                <igtxt:WebImageButton ID="WIBtn_TERMINALES_DEXTRAS_BIN_ELIMINAR" runat="server" ImageTextSpacing="4"
                            OnClick="WIBtn_TERMINALES_DEXTRAS_BIN_ELIMINAR_Click" Text="Eliminar Foto" 
                    UseBrowserDefaults="False" Visible="False">
                    <Appearance>
                        <Image Height="16px" Url="./Imagenes/Cancel.png" Width="16px" />
                        <ButtonStyle Cursor="Default" Font-Size="X-Small">
                        </ButtonStyle>
                    </Appearance>
                    <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                                HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" 
                        MaxHeight="80" MaxWidth="400"
                                PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                    <PressedAppearance>
                        <ButtonStyle Font-Size="Small">
                        </ButtonStyle>
                    </PressedAppearance>
                    <Alignments VerticalAll="Bottom" VerticalImage="Middle" />
                </igtxt:WebImageButton>
            </td>
    </tr>
</TBODY></TABLE>
<TABLE id="Table10" cellSpacing=1 cellPadding=1 width=450 border=0>
    <tr>
        <td align="left" width="200" style="height: 1px">
            <asp:Label id="Lbl_TERMINAL_DIR" runat="server">Tipo de Dirección</asp:Label></td>
        <td align="left" style="width: 298px; height: 1px">
        <TABLE id="Table9" cellSpacing=1 cellPadding=1 border=0 style="height: 1px">
            <tr>
                <TD align=left valign="top">
                                        <asp:RadioButtonList ID="Rbn_TERMINAL_DIR" runat="server" 
                                            OnSelectedIndexChanged="Rbn_TERMINAL_DIR_SelectedIndexChanged" AutoPostBack="True" 
                                            RepeatDirection="Horizontal" TabIndex="26" >
                                            <asp:ListItem>USB</asp:ListItem>
                                            <asp:ListItem>Serial</asp:ListItem>
                                            <asp:ListItem>RS485</asp:ListItem>
                                            <asp:ListItem>Modem</asp:ListItem>
                                            <asp:ListItem>Red</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </TD>
            </tr>
        </table>
                                        <TABLE id="Table2" cellSpacing=1 cellPadding=1 
                border=0 style="height: 4px" width="200">
                                            <tr>
                                                <TD align=left width="200">
                                                    <igmisc:WebPanel ID="Wpn_USB_" runat="server"  Visible="False" 
                                                        EnableAppStyling="True" StyleSetName="Caribbean" 
                                                        Width="250px">

                                                        <Header Text="USB" TextAlignment="Left">

                                                        </Header>
                                                        <Template>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Lbl_TERMINAL_DIR_USB_NOMBRE" runat="server" Text="Nombre "></asp:Label>
                                                                    </td>
                                                                    <td style="margin-left: 40px">
                                                            <igtxt:WebTextEdit id="Wtx_TERMINAL_DIR_USB_NOMBRE" runat="server" Font-Names="" 
                                                                            Width="183px" CellSpacing="1" TabIndex="27">
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
                                                                    <td style="text-align: left" width="200">
                                                                        <asp:Label ID="Lbl_TERMINAL_DIR_USB_ID" runat="server" Text="ID"></asp:Label>
                                                                    </td>
                                                                    <td width="200">
                                                                        <igtxt:WebNumericEdit ID="Wne_TERMINAL_DIR_USB_ID" runat="server" 
                                                                            DataMode="Text" Width="184px" TabIndex="28">
                                                                        </igtxt:WebNumericEdit>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </Template>
                                                    </igmisc:WebPanel>
                                                    <igmisc:WebPanel ID="Wpn_SERIAL_" runat="server"  Visible="False" 
                                                        EnableAppStyling="True" StyleSetName="Caribbean" 
                                                        Width="250px">
        
                                                        <Header Text="Serial" TextAlignment="Left">

                                                        </Header>
                                                        <Template>
                                                            <table style="width: 250px">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Lbl_TERMINAL_DIR_SERIAL_PUERTO" runat="server" Text="Puerto"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                            <igtxt:WebNumericEdit ID="Wne_TERMINAL_DIR_SERIAL_PUERTO" runat="server" Width="184px" 
                                                                            DataMode="Text" TabIndex="27">
                                                            </igtxt:WebNumericEdit>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Lbl_TERMINAL_DIR_SERIAL_VELOCIDAD" runat="server" 
                                                                            Text="Velocidad"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                            <igtxt:WebNumericEdit ID="Wne_TERMINAL_DIR_SERIAL_VELOCIDAD" runat="server" Width="184px" 
                                                                            DataMode="Text" TabIndex="28">
                                                            </igtxt:WebNumericEdit>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </Template>
                                                    </igmisc:WebPanel>
                                                    <igmisc:WebPanel ID="Wpn_485_" runat="server"  Visible="False" 
                                                        EnableAppStyling="True" StyleSetName="Caribbean" 
                                                        Width="250px">
                                                        
                                                        <Header Text="485" TextAlignment="Left">

                                                        </Header>
                                                        <Template>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Lbl_TERMINAL_DIR_485_PUERTO" runat="server" Text="Puerto"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                            <igtxt:WebNumericEdit ID="Wne_TERMINAL_DIR_485_PUERTO" runat="server" Width="184px" 
                                                                            DataMode="Text" TabIndex="27">
                                                            </igtxt:WebNumericEdit>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Lbl_TERMINAL_DIR_485_VELOCIDAD" runat="server" Text="Velocidad"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                            <igtxt:WebNumericEdit ID="Wne_TERMINAL_DIR_485_VELOCIDAD" runat="server" Width="184px" 
                                                                            DataMode="Text" TabIndex="28">
                                                            </igtxt:WebNumericEdit>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="Lbl_TERMINAL_DIR_485_ID" runat="server" Text="ID"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <igtxt:WebNumericEdit ID="Wne_TERMINAL_DIR_485_ID" runat="server" Width="184px" 
                                                                            DataMode="Text" TabIndex="29">
                                                                        </igtxt:WebNumericEdit>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </Template>
                                                    </igmisc:WebPanel>
                                                    <igmisc:WebPanel ID="Wpn_MODEM_" runat="server"  Visible="False" 
                                                        EnableAppStyling="True" StyleSetName="Caribbean" 
                                                        Width="250px">
                                                        <Header Text="Modem" TextAlignment="Left">

                                                        </Header>
                                                        <Template>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Lbl_TERMINAL_DIR_MODEM_TEL" runat="server" Text="Telefono"></asp:Label>
&nbsp;
                                                                    </td>
                                                                    <td width="183">
                                                            <igtxt:WebNumericEdit ID="Wne_TERMINAL_DIR_MODEM_TEL" runat="server" Width="184px" 
                                                                            DataMode="Text" TabIndex="27">
                                                            </igtxt:WebNumericEdit>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Lbl_TERMINAL_DIR_MODEM_PUERTO" runat="server" Text="Puerto"></asp:Label>
                                                                    </td>
                                                                    <td width="183">
                                                            <igtxt:WebNumericEdit ID="Wne_TERMINAL_DIR_MODEM_PUERTO" runat="server" Width="184px" 
                                                                            DataMode="Text" TabIndex="28">
                                                            </igtxt:WebNumericEdit>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;<asp:Label ID="Lbl_TERMINAL_DIR_MODEM_VELOCIDAD" runat="server" 
                                                                            Text="Velocidad"></asp:Label>
                                                                    </td>
                                                                    <td width="183">
                                                            <igtxt:WebNumericEdit ID="Wne_TERMINAL_DIR_MODEM_VELOCIDAD" runat="server" Width="184px" 
                                                                            DataMode="Text" TabIndex="29">
                                                            </igtxt:WebNumericEdit>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </Template>
                                                    </igmisc:WebPanel>
                                                    <igmisc:WebPanel ID="Wpn_RED_" runat="server"  Visible="False" 
                                                        EnableAppStyling="True" StyleSetName="Caribbean" 
                                                        Width="250px">
                                                        
                                                        <Header Text="Red" TextAlignment="Left">

                                                        </Header>
                                                        <Template>
                                                            <table style="width: 247px">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Lbl_TERMINAL_DIR_RED_DIR" runat="server" Text="Direccion&nbsp;"></asp:Label>
&nbsp;
                                                                    </td>
                                                                    <td class="style3">
                                                            <igtxt:WebTextEdit id="Wtx_TERMINAL_DIR_RED_DIR" runat="server" BorderColor="#7F9DB9" 
                                                                            Font-Names="" BorderWidth="1px" BorderStyle="Solid" Width="184px" 
                                                                            CellSpacing="1" UseBrowserDefaults="False" TabIndex="27">
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
                                                                        <asp:Label ID="Lbl_TERMINAL_DIR_RED_PUERTO" runat="server" Text="Puerto"></asp:Label>
                                                                    </td>
                                                                    <td class="style3">
                                                            <igtxt:WebNumericEdit ID="Wne_TERMINAL_DIR_RED_PUERTO" runat="server" Width="184px" 
                                                                            DataMode="Text" TabIndex="28">
                                                            </igtxt:WebNumericEdit>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Lbl_TERMINAL_DIR_RED_ID" runat="server" Text="ID"></asp:Label>
                                                                    </td>
                                                                    <td class="style3">
                                                                        <igtxt:WebNumericEdit ID="Wne_TERMINAL_DIR_RED_ID" runat="server" Width="184px" 
                                                                            DataMode="Text" TabIndex="29">
                                                                        </igtxt:WebNumericEdit>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </Template>
                                                    </igmisc:WebPanel>
                                                </TD>
                                            </tr>
                                        </table>
        </td>
    </tr>
</table>

    <TABLE id="Table11" cellSpacing=1 cellPadding=1 width=450 border=0>
        <tr>
            <TD style="WIDTH: 135px" align=left><asp:Label id="Lbl_TERMINAL_ENROLA" 
                    runat="server" Width="80px">Permite Enrolar</asp:Label></TD><TD align=left style="width: 62px">
                <asp:CheckBox id="Chb_TERMINAL_ENROLA" runat="server" Font-Names="" 
                    Font-Size="" TabIndex="30"></asp:CheckBox>&nbsp;</TD><TD align=left></TD>
        </tr>
        <tr>
            <TD style="WIDTH: 135px" align=left></TD><TD align=left style="width: 62px">
            <asp:CheckBox id="Chb_TERMINAL_BORRADO" runat="server" Text="Borrar Registro" 
                Font-Names="" Font-Size="" TabIndex="31"></asp:CheckBox></TD><TD style="COLOR: red" align=left>
                <asp:Label id="Lbl_TERMINAL_BORRADO" runat="server" Width="326px">* Seleccione esta opción si lo que quiere es borrar la Terminal</asp:Label></TD>
        </tr>
        <tr>
            <td align="left" style="WIDTH: 135px">
                &nbsp;</td>
            <td align="left" style="width: 62px">
                &nbsp;</td>
            <td align="left" style="COLOR: red">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="left" style="WIDTH: 135px">
                &nbsp;</td>
            <td align="left" style="width: 62px">
                <asp:Label ID="Lbl_Error" runat="server" ForeColor="#CC0033"></asp:Label>
                <asp:Label ID="Lbl_Correcto" runat="server" ForeColor="#00C000"></asp:Label>
            </td>
            <td align="left" style="COLOR: red">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="left" style="WIDTH: 135px">
                <igtxt:WebImageButton ID="WIBtn_Deshacer" runat="server" Height="22px" 
                    ImageTextSpacing="4" onclick="WIBtn_Deshacer_Click" Text="Deshacer" 
                    UseBrowserDefaults="False" Width="150px" TabIndex="34">
                    <Alignments VerticalImage="Middle" VerticalAll="Bottom" />
                    <appearance>
                        <Image Url="./Imagenes/Undo.png" Height="16px" Width="16px" />
                        <style cursor="Default">
                        </style>
                    </appearance>
                    <RoundedCorners HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif"
                                MaxHeight="80" MaxWidth="400" PressedImageUrl="ig_butXP4wh.gif" 
                                RenderingType="FileImages" DisabledImageUrl="ig_butXP5wh.gif" 
                                FocusImageUrl="ig_butXP3wh.gif" />
                </igtxt:WebImageButton>
            </td>
            <td align="left" style="width: 62px">
                <igtxt:WebImageButton ID="WIBtn_AccesoPersonas" runat="server" Height="22px" 
                    ImageTextSpacing="4" onclick="WIBtn_AccesoPersonas_Click" Text="Acceso de Personas" 
                    UseBrowserDefaults="False" Width="193px" TabIndex="32">
                    <Alignments VerticalImage="Bottom" HorizontalAll="NotSet" VerticalAll="NotSet" />
                    <appearance>
                        <style cursor="Default">


                        </style>
                        <Image Height="18px" Url="./Imagenes/Empleado.png" Width="20px" />
                    </appearance>
                    <RoundedCorners HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif"
                                MaxHeight="80" MaxWidth="400" PressedImageUrl="ig_butXP4wh.gif" 
                                RenderingType="FileImages" DisabledImageUrl="ig_butXP5wh.gif" 
                                FocusImageUrl="ig_butXP3wh.gif" />
                </igtxt:WebImageButton>
            </td>
            <td align="left">
                <igtxt:WebImageButton ID="WIBtn_Guardar" runat="server" Height="22px" 
                    ImageTextSpacing="4" onclick="WIBtn_Guardar_Click" Text="Guardar" 
                    UseBrowserDefaults="False" Width="150px" TabIndex="33">
                    <Alignments VerticalImage="Middle" VerticalAll="Bottom" />
                    <RoundedCorners HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif"
                                MaxHeight="80" MaxWidth="400" PressedImageUrl="ig_butXP4wh.gif" 
                                RenderingType="FileImages" DisabledImageUrl="ig_butXP5wh.gif" 
                                FocusImageUrl="ig_butXP3wh.gif" />
                    <appearance>
                        <style cursor="Default">
                        </style>
                        <Image Url="./Imagenes/Save_as.png" Height="16px" Width="16px" />
                    </appearance>
                </igtxt:WebImageButton>
            </td>
        </tr>
    </table>
</Template>
                        &nbsp;&nbsp;<br />
                        &nbsp;</TD>
				</TR>
				<TR>
					<TD align="center">
						&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                        </TD>
				</TR>
				</TABLE>
 </div>
    </form>
</body>
</html>
