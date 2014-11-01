<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WF_SuscripcionE.aspx.cs" Inherits="WF_SuscripcionE" %>

<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.WebUI.Misc" tagprefix="igmisc" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.WebUI.WebDataInput" tagprefix="igtxt" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.WebUI.UltraWebGrid" tagprefix="igtbl" %>
<%@ Register Assembly="Infragistics2.WebUI.WebCombo.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.WebUI.WebCombo" tagprefix="igcmbo" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.ExcelExport.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" TagPrefix="igtblexp" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.DocumentExport.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebGrid.DocumentExport" TagPrefix="igtbldocexp" %>
<%@ Register Assembly="Infragistics2.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.Web.UI.DisplayControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics2.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.Web.UI.LayoutControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.WebUI.WebDataInput" tagprefix="igtxt" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebNavigator.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebNavigator" TagPrefix="ignav" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.WebUI.UltraWebGrid" tagprefix="igtbl" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDateChooser.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.WebUI.Misc" tagprefix="igmisc" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGauge.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebGauge" TagPrefix="igGauge" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGauge.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.UltraGauge.Resources" TagPrefix="igGaugeProp" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebChart.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebChart" TagPrefix="igchart" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebChart.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.UltraChart.Resources.Appearance" TagPrefix="igchartprop" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebChart.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.UltraChart.Data" TagPrefix="igchartdata" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/tr/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .style7
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: small;
        }
        .style12
        {
            height: auto;
            width: 612px;
        }
        .style13
        {
            height: 148px;
            }
        .style15
        {
        }
        .style23
        {
            text-align: center;
        }
        .style38
        {
            width: 607px;
            height: 144px;
        }
        .style47
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: small;
            width: 201px;
            height: 27px;
        }
        .style48
        {
            width: 202px;
            height: 27px;
        }
        .style49
        {
            width: 607px;
            height: 124px;
        }
        .style50
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: x-small;
            height: 27px;
            width: 202px;
        }
        .style59
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: small;
            width: 201px;
            height: 28px;
        }
        .style61
        {
            font-size: small;
        }
        .style62
        {
            width: 152px;
        }
        .style63
        {
            width: 153px;
        }
        .style64
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: small;
            width: 201px;
            height: 26px;
        }
        .style65
        {
            width: 202px;
            height: 26px;
        }
        .style66
        {
            width: 202px;
            height: 28px;
        }
        </style>
    <%--<script id="InfragisticseClock" type="text/javascript">
            function MuestraPagina(Pagina) {
            try{
                var edit = igedit_getById("Wpn_EC_SUSCRIPCION__Wtx_SUSCRIPCION_ID");
                var texto = edit.getValue();
//                    alert(texto);
                Pagina = Pagina + texto;
                $find('<%=DlgMain.ClientID%>').set_windowState($IG.DialogWindowState.Normal);
                $find('<%=DlgMain.ClientID%>').set_windowState($IG.DialogWindowState.Maximized);
                self.frames[0].location = Pagina;
                self.frames["FrameMain"].location = Pagina;
                document.getElementById("FrameMain").location = Pagina;
                }
                catch (err) {
                    alert(err.description);
                }
            }


            function Btn_Usuarios_Click(oButton, oEvent){
                MuestraPagina("WF_Usuarios.aspx?SuscripcionID=");
            }

            function Btn_Sitios_Click(oButton, oEvent){
                MuestraPagina("WF_Sitios.aspx?SuscripcionID=");
            }

            function Btn_Terminales_Click(oButton, oEvent){
                MuestraPagina("WF_Terminales.aspx?SuscripcionID=");
            }
        </script>--%>
</head>
<body style="font-family: 'Segoe UI'; font-size: small">
    <form id="form1" runat="server">
    <div>
    
        <table class="style12" style="padding:2% 20% 10% 20%">
            <tr>
                <td style="BACKGROUND-POSITION: right center; BACKGROUND-IMAGE: url(./Imagenes/fondoeClock3.jpg); BACKGROUND-REPEAT: no-repeat" align="center" class="style13" colspan="4">
                    <igmisc:WebPanel ID="Wpn_EC_SUSCRIPCION_" runat="server" EnableAppStyling="True" Height="91px" StyleSetName="Caribbean" Width="605px">
                        <Header Text="Datos de la suscripcion">
                        </Header>
                        <Template>
                            <table style="FONT-FAMILY: arial; " id="Table3" cellSpacing=2 cellPadding=1 align=left border=0 class="style38" >
                                <TBODY>
                                    <tr>
                                        <td class="style64">
                                            <asp:Label ID="Lbl_SUSCRIPCION_ID" runat="server" Text="Suscripcion ID"></asp:Label>
                                        </td>
                                        <td class="style65">
                                            <igtxt:WebTextEdit ID="Wtx_SUSCRIPCION_ID" runat="server" BorderColor="#7F9DB9" 
                                                BorderStyle="Solid" BorderWidth="1px" CellSpacing="1" Enabled="False" 
                                                Font-Names="Segoe UI" ReadOnly="True" UseBrowserDefaults="False" 
                                                Width="200px" TabIndex="1" Font-Size="X-Small">
                                                <buttonsappearance custombuttondefaulttriangleimages="Arrow">
                                                    <buttonpressedstyle backcolor="#83A6F4">
                                                    </buttonpressedstyle>
                                                    <buttondisabledstyle backcolor="#E1E1DD" bordercolor="#D7D7D7" 
                                                        forecolor="#BEBEBE">
                                                    </buttondisabledstyle>
                                                    <buttonstyle backcolor="#C5D5FC" bordercolor="#ABC1F4" borderstyle="Solid" 
                                                        borderwidth="1px" forecolor="#506080" width="13px">
                                                    </buttonstyle>
                                                    <buttonhoverstyle backcolor="#DCEDFD">
                                                    </buttonhoverstyle>
                                                </buttonsappearance>
                                            </igtxt:WebTextEdit>
                                        </td>
                                        <td style="font-size: x-small" class="style65">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="style64">
                                            <asp:Label ID="Lbl_SUSCRIPCION_NOMBRE" runat="server" Text="Contrato"></asp:Label>
                                        </td>
                                        <td align="left" class="style65">
                                            <igtxt:WebTextEdit ID="Wtx_SUSCRIPCION_NOMBRE" runat="server" BorderColor="#7F9DB9" 
                                                BorderStyle="Solid" BorderWidth="1px" CellSpacing="1" Font-Names="Segoe UI" 
                                                UseBrowserDefaults="False" Width="200px" TabIndex="2" Font-Size="X-Small">
                                                <buttonsappearance custombuttondefaulttriangleimages="Arrow">
                                                    <buttonpressedstyle backcolor="#83A6F4">
                                                    </buttonpressedstyle>
                                                    <buttondisabledstyle backcolor="#E1E1DD" bordercolor="#D7D7D7" 
                                                        forecolor="#BEBEBE">
                                                    </buttondisabledstyle>
                                                    <buttonstyle backcolor="#C5D5FC" bordercolor="#ABC1F4" borderstyle="Solid" 
                                                        borderwidth="1px" forecolor="#506080" width="13px">
                                                    </buttonstyle>
                                                    <buttonhoverstyle backcolor="#DCEDFD">
                                                    </buttonhoverstyle>
                                                </buttonsappearance>
                                            </igtxt:WebTextEdit>
                                        </td>
                                        <td align="left" style="font-size: x-small" class="style65">
                                            <asp:RequiredFieldValidator ID="VLbl_SUSCRIPCION_NOMBRE" runat="server" 
                                                ControlToValidate="Wtx_SUSCRIPCION_ID" 
                                                ErrorMessage="Se requiere Numero de contrato" Font-Names="Segoe UI" 
                                                Font-Size="X-Small" CssClass="style61">* No de contrato</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr style="COLOR: #000000">
                                        <td align=left class="style64">
                                            <asp:Label ID="Lbl_SUSCRIP_DATOS_RAZON" runat="server" Text="Responsable"></asp:Label>
                                        </td>
                                        <td align=left class="style65">
                                            <igtxt:webtextedit id="Wtx_SUSCRIP_DATOS_RAZON" runat="server" BorderColor="#7F9DB9" 
                Font-Names="Segoe UI" BorderWidth="1px" BorderStyle="Solid" Width="200px" 
                UseBrowserDefaults="False" CellSpacing="1" MaxLength="45" TabIndex="3" Font-Size="X-Small">
                                                <ButtonsAppearance CustomButtonDefaultTriangleImages="Arrow">
                                                    <ButtonPressedStyle BackColor="#83A6F4">
                                                    </ButtonPressedStyle>
                                                    <ButtonDisabledStyle BorderColor="#D7D7D7" ForeColor="#BEBEBE" BackColor="#E1E1DD">
                                                    </ButtonDisabledStyle>
                                                    <ButtonStyle Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" ForeColor="#506080"
												BackColor="#C5D5FC">
                                                    </ButtonStyle>
                                                    <ButtonHoverStyle BackColor="#DCEDFD">
                                                    </ButtonHoverStyle>
                                                </ButtonsAppearance>
                                            </igtxt:webtextedit>
                                        </td>
                                        <td align=left style="font-size: x-small" class="style65">
                                            <asp:RequiredFieldValidator ID="VLbl_SUSCRIP_DATOS_RAZON" runat="server" 
                                                ControlToValidate="Wtx_SUSCRIPCION_NOMBRE" 
                                                ErrorMessage="Se requiere nombre del responsable" Font-Names="Segoe UI" 
                                                Font-Size="X-Small" CssClass="style61">* Nombre del usuario responsable (cliente)</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="style64">
                                            <asp:Label ID="Lbl_SUSCRIP_DATOS_EMAIL" runat="server" 
                                                Text="Correo Electrónico"></asp:Label>
                                        </td>
                                        <td align="left" class="style65">
                                            <igtxt:webtextedit id="Wtx_SUSCRIP_DATOS_EMAIL" runat="server" BorderColor="#7F9DB9" 
            Font-Names="Segoe UI" BorderWidth="1px" BorderStyle="Solid" Width="200px" 
            UseBrowserDefaults="False" CellSpacing="1" MaxLength="255" 
                                                TabIndex="4" Font-Size="X-Small">
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
                                        <td align="left" style="font-size: x-small" class="style65">
                                            <asp:RequiredFieldValidator ID="VLbl_SUSCRIP_DATOS_EMAIL" runat="server" 
                                                ControlToValidate="Wtx_SUSCRIP_DATOS_EMAIL" 
                                                ErrorMessage="Se requiere correo electronico" Font-Names="Segoe UI" 
                                                Font-Size="X-Small" CssClass="style61">* Direccion del correo electronico</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="style64">
                                            <asp:Label ID="LblC_SUSCRIP_DATOS_EMAIL" runat="server" 
                                                Text="Confirmación Correo"></asp:Label>
                                        </td>
                                        <td align="left" class="style65">
                                            <igtxt:WebTextEdit ID="WtxC_SUSCRIP_DATOS_EMAIL" runat="server" BorderColor="#7F9DB9" 
                                                BorderStyle="Solid" BorderWidth="1px" CellSpacing="1" Font-Names="Segoe UI" 
                                                MaxLength="255" UseBrowserDefaults="False" Width="200px" TabIndex="5" 
                                                Font-Size="X-Small">
                                                <buttonsappearance custombuttondefaulttriangleimages="Arrow">
                                                    <buttonstyle backcolor="#C5D5FC" bordercolor="#ABC1F4" borderstyle="Solid" 
                                                        borderwidth="1px" forecolor="#506080" width="13px">
                                                    </buttonstyle>
                                                    <buttondisabledstyle backcolor="#E1E1DD" bordercolor="#D7D7D7" 
                                                        forecolor="#BEBEBE">
                                                    </buttondisabledstyle>
                                                    <buttonhoverstyle backcolor="#DCEDFD">
                                                    </buttonhoverstyle>
                                                    <buttonpressedstyle backcolor="#83A6F4">
                                                    </buttonpressedstyle>
                                                </buttonsappearance>
                                            </igtxt:WebTextEdit>
                                        </td>
                                        <td align="left" style="font-size: x-small" class="style65">
                                            <asp:CompareValidator ID="CVLblC_SUSCRIP_DATOS_EMAIL" runat="server" 
                                                ControlToCompare="Wtx_SUSCRIP_DATOS_EMAIL" ControlToValidate="WtxC_SUSCRIP_DATOS_EMAIL" 
                                                ErrorMessage="No coincide el correo electrónico" Font-Names="Segoe UI" 
                                                Font-Size="X-Small" CssClass="style61">*Confirmación de coreo 
                                            elecctrónico</asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="style64">
                                            &nbsp;</td>
                                        <td align="left" style="font-size: small;" class="style65">
                                            <asp:CheckBox ID="Chb_SUSCRIPCION_BORRADO" runat="server" Text="Inactivo" 
                                                TabIndex="6" />
                                        </td>
                                        <td align="left" style="font-size: x-small" class="style65">
                                            &nbsp;</td>
                                    </tr>
                                </TBODY>
                            </table>
                        </Template>
                    </igmisc:WebPanel>
				</td>
			</tr>
			<tr>
				<td style="BACKGROUND-POSITION: right center; BACKGROUND-IMAGE: url(./Imagenes/fondoeClock3.jpg); BACKGROUND-REPEAT: no-repeat"
					class="style62">
					&nbsp;</td>
				<td style="BACKGROUND-POSITION: right center; BACKGROUND-IMAGE: url(./Imagenes/fondoeClock3.jpg); BACKGROUND-REPEAT: no-repeat"
					class="style62">
					<igtxt:webimagebutton id="WIBtn_Editar" runat="server" Height="24px" 
                        Text="Editar Datos" UseBrowserDefaults="False"
						Width="120px" OnClick="WIBtn_Editar_Click" ImageTextSpacing="4" TabIndex="8">
						<Alignments VerticalImage="Middle"></Alignments>
						<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
							RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
						<Appearance>
							<Style Cursor="Default">
							</Style>
							<Image Url="./Imagenes/Edit.png" Height="16px" Width="16px"></Image>

<ButtonStyle Cursor="Default"></ButtonStyle>
						</Appearance>
					</igtxt:webimagebutton>
					</td>
				<td style="BACKGROUND-POSITION: right center; BACKGROUND-IMAGE: url(./Imagenes/fondoeClock3.jpg); BACKGROUND-REPEAT: no-repeat"
					class="style63">
					<igtxt:webimagebutton id="WIBtn_Guardar" runat="server" Height="24px" 
                        UseBrowserDefaults="False" Text="Guardar" ImageTextSpacing="4" 
                        onclick="WIBtn_Guardar_Click" Width="120px" TabIndex="7">
						<Alignments VerticalImage="Middle"></Alignments>
						<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
							RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
						<Appearance>
							<Style Cursor="Default">
							</Style>
							<Image Url="./Imagenes/Save_as.png" Height="16px" Width="16px"></Image>

<ButtonStyle Cursor="Default"></ButtonStyle>
						</Appearance>
					</igtxt:webimagebutton></td>
				<td style="BACKGROUND-POSITION: right center; BACKGROUND-IMAGE: url(./Imagenes/fondoeClock3.jpg); BACKGROUND-REPEAT: no-repeat"
					class="style63">
					&nbsp;</td>
			</tr>
			<tr>
				<td style="BACKGROUND-POSITION: right center; BACKGROUND-IMAGE: url(./Imagenes/fondoeClock3.jpg); BACKGROUND-REPEAT: no-repeat"
					align="center" class="style15" colspan="4">
					&nbsp;</td>
			</tr>
			<tr>
				<td style="BACKGROUND-POSITION: right center; BACKGROUND-IMAGE: url(./Imagenes/fondoeClock3.jpg); BACKGROUND-REPEAT: no-repeat"
					align="center" class="style15" colspan="4">
                    <igmisc:WebPanel ID="Wpn_EC_USUARIOS_" runat="server" EnableAppStyling="True" 
                        Height="113px" StyleSetName="Caribbean" Width="606px" Visible="False">
                        <Header Text="Crear Usuario">
                        </Header>
                        <Template>
                            <table style="FONT-FAMILY: arial; " id="Table4" 
cellSpacing=2 cellPadding=1 align=left border=0 class="style49">
                                <TBODY>
                                    <tr>
                                        <td align=left class="style47">
                                            <asp:Label ID="Lbl_PERFIL_ID" runat="server" Text="Perfil"></asp:Label>
                                        </td>
                                        <td align=left class="style48">
                                            <igcmbo:webcombo id="Wco_PERFIL_ID" runat="server" BorderColor="Silver" 
            BackColor="White" ForeColor="Black" BorderWidth="1px" 
            BorderStyle="Solid" Version="4.00" 
            SelBackColor="DarkBlue" SelForeColor="White" oninitializelayout="CmbPerfil_InitializeLayout" TabIndex="9" 
                                                Font-Names="Segoe UI" Font-Size="X-Small">
                                                <ExpandEffects ShadowColor="LightGray">
                                                </ExpandEffects>
                                                <Columns>
                                                    <igtbl:UltraGridColumn>
                                                        <header caption="Column0">
                                                        </header>
                                                    </igtbl:UltraGridColumn>
                                                </Columns>
                                                <dropdownlayout bordercollapse="Separate" rowheightdefault="20px" 
                                                    version="4.00">
                                                    <framestyle backcolor="Silver" borderstyle="Ridge" borderwidth="2px" 
                                                        cursor="Default" font-names="Verdana" font-size="10pt" height="130px" 
                                                        width="325px">
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
                                            </igcmbo:webcombo>
                                        </td>
                                        <td align=left class="style50">
                                            <asp:Label ID="LblA_PERFIL_ID" runat="server" 
                                                Text="Seleccione Perfil de Usuario" Font-Names="Segoe UI" 
                                                Font-Size="X-Small" CssClass="style61"></asp:Label>
                                            &nbsp;</td>
                                    </tr>
                                    <tr style="COLOR: #000000">
                                        <td align=left class="style47">
                                            <asp:Label ID="Lbl_USUARIO_USUARIO" runat="server" Text="Nombre del Usuario"></asp:Label>
                                        </td>
                                        <td align=left class="style48">
                                            <igtxt:WebTextEdit ID="Wtx_USUARIO_USUARIO" runat="server" BorderColor="#7F9DB9" BorderStyle="Solid" BorderWidth="1px" CellSpacing="1" Font-Names="Segoe UI" Font-Size="X-Small" TabIndex="10" UseBrowserDefaults="False" Width="200px">
                                                <buttonsappearance custombuttondefaulttriangleimages="Arrow">
                                                    <buttonpressedstyle backcolor="#83A6F4">
                                                    </buttonpressedstyle>
                                                    <buttondisabledstyle backcolor="#E1E1DD" bordercolor="#D7D7D7" forecolor="#BEBEBE">
                                                    </buttondisabledstyle>
                                                    <buttonstyle backcolor="#C5D5FC" bordercolor="#ABC1F4" borderstyle="Solid" borderwidth="1px" forecolor="#506080" width="13px">
                                                    </buttonstyle>
                                                    <buttonhoverstyle backcolor="#DCEDFD">
                                                    </buttonhoverstyle>
                                                </buttonsappearance>
                                            </igtxt:WebTextEdit>
                                        </td>
                                        <td align=left class="style48">
                                            <asp:RequiredFieldValidator ID="VLbl_USUARIO_USUARIO" runat="server" ControlToValidate="Wtx_USUARIO_NOMBRE" CssClass="style7" ErrorMessage="El Usuario es un Dato obligatorio" Font-Names="Segoe UI" Font-Size="X-Small"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr style="COLOR: #000000">
                                        <td align=left class="style47">
                                            <asp:Label ID="Lbl_USUARIO_NOMBRE" runat="server" Text="Nombre del Usuario"></asp:Label>
                                        </td>
                                        <td align=left class="style48">
                                            <igtxt:webtextedit id="Wtx_USUARIO_NOMBRE" runat="server" BorderColor="#7F9DB9" 
                Font-Names="Segoe UI" BorderWidth="1px" BorderStyle="Solid" Width="200px" 
                UseBrowserDefaults="False" CellSpacing="1" TabIndex="10" Font-Size="X-Small">
                                                <ButtonsAppearance CustomButtonDefaultTriangleImages="Arrow">
                                                    <ButtonPressedStyle BackColor="#83A6F4">
                                                    </ButtonPressedStyle>
                                                    <ButtonDisabledStyle BorderColor="#D7D7D7" ForeColor="#BEBEBE" BackColor="#E1E1DD">
                                                    </ButtonDisabledStyle>
                                                    <ButtonStyle Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" ForeColor="#506080"
												BackColor="#C5D5FC">
                                                    </ButtonStyle>
                                                    <ButtonHoverStyle BackColor="#DCEDFD">
                                                    </ButtonHoverStyle>
                                                </ButtonsAppearance>
                                            </igtxt:webtextedit>
                                        </td>
                                        <td align=left class="style48">
                                            <asp:RequiredFieldValidator ID="VLbl_USUARIO_NOMBRE" runat="server" ControlToValidate="Wtx_USUARIO_NOMBRE" CssClass="style7" ErrorMessage="El nombre es un Dato obligatorio" Font-Names="Segoe UI" Font-Size="X-Small"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="style47">
                                            <asp:Label ID="Lbl_USUARIO_DESCRIPCION" runat="server" Text="Descripción&nbsp;"></asp:Label>
                                        </td>
                                        <td align="left" class="style48">
                                            <igtxt:webtextedit id="Wtx_USUARIO_DESCRIPCION" runat="server" BorderColor="#7F9DB9" 
            Font-Names="Segoe UI" BorderWidth="1px" BorderStyle="Solid" Width="200px" 
            UseBrowserDefaults="False" CellSpacing="1" MaxLength="45" TabIndex="11" Font-Size="X-Small">
                                                <ButtonsAppearance CustomButtonDefaultTriangleImages="Arrow">
                                                    <ButtonPressedStyle BackColor="#83A6F4">
                                                    </ButtonPressedStyle>
                                                    <ButtonDisabledStyle BorderColor="#D7D7D7" ForeColor="#BEBEBE" BackColor="#E1E1DD">
                                                    </ButtonDisabledStyle>
                                                    <ButtonStyle Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" ForeColor="#506080"
												BackColor="#C5D5FC">
                                                    </ButtonStyle>
                                                    <ButtonHoverStyle BackColor="#DCEDFD">
                                                    </ButtonHoverStyle>
                                                </ButtonsAppearance>
                                            </igtxt:WebTextEdit>
                                        </td>
                                        <td align="left" class="style48">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="style47">
                                            <asp:Label ID="Lbl_USUARIO_EMAIL" runat="server" Text="Correo Electronico"></asp:Label>
                                        </td>
                                        <td align="left" class="style48">
                                            <igtxt:WebTextEdit ID="Wtx_USUARIO_EMAIL" runat="server" BorderColor="#7F9DB9" 
                                                BorderStyle="Solid" BorderWidth="1px" CellSpacing="1" Font-Names="Segoe UI" 
                                                MaxLength="255" 
                                                UseBrowserDefaults="False" Width="200px" TabIndex="12" Font-Size="X-Small">
                                                <buttonsappearance custombuttondefaulttriangleimages="Arrow">
                                                    <buttonstyle backcolor="#C5D5FC" bordercolor="#ABC1F4" borderstyle="Solid" 
                                                        borderwidth="1px" forecolor="#506080" width="13px">
                                                    </buttonstyle>
                                                    <buttonhoverstyle backcolor="#DCEDFD">
                                                    </buttonhoverstyle>
                                                    <buttonpressedstyle backcolor="#83A6F4">
                                                    </buttonpressedstyle>
                                                    <buttondisabledstyle backcolor="#E1E1DD" bordercolor="#D7D7D7" 
                                                        forecolor="#BEBEBE">
                                                    </buttondisabledstyle>
                                                </buttonsappearance>
                                            </igtxt:WebTextEdit>
                                        </td>
                                        <td align="left" class="style48">
                                            <asp:RequiredFieldValidator ID="VLbl_USUARIO_EMAIL" runat="server" ControlToValidate="Wtx_USUARIO_EMAIL" CssClass="style7" ErrorMessage="Será su usuario y correo" Font-Names="Segoe UI" Font-Size="X-Small"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="style59">
                                            <asp:Label ID="LblC_USUARIO_EMAIL" runat="server" Text="Confirmación Correo"></asp:Label>
                                        </td>
                                        <td align="left" class="style66">
                                            <igtxt:WebTextEdit ID="WtxC_USUARIO_EMAIL" runat="server" BorderColor="#7F9DB9" BorderStyle="Solid" BorderWidth="1px" CellSpacing="1" Font-Names="Segoe UI" Font-Size="X-Small" MaxLength="255" TabIndex="13" UseBrowserDefaults="False" Width="200px">
                                                <buttonsappearance custombuttondefaulttriangleimages="Arrow">
                                                    <buttonstyle backcolor="#C5D5FC" bordercolor="#ABC1F4" borderstyle="Solid" borderwidth="1px" forecolor="#506080" width="13px">
                                                    </buttonstyle>
                                                    <buttondisabledstyle backcolor="#E1E1DD" bordercolor="#D7D7D7" forecolor="#BEBEBE">
                                                    </buttondisabledstyle>
                                                    <buttonhoverstyle backcolor="#DCEDFD">
                                                    </buttonhoverstyle>
                                                    <buttonpressedstyle backcolor="#83A6F4">
                                                    </buttonpressedstyle>
                                                </buttonsappearance>
                                            </igtxt:WebTextEdit>
                                        </td>
                                        <td align="left" class="style66" style="font-size: x-small">
                                            <asp:CompareValidator ID="CVLblC_USUARIO_EMAIL" runat="server" ControlToCompare="Wtx_USUARIO_EMAIL" ControlToValidate="WtxC_USUARIO_EMAIL" CssClass="style61" ErrorMessage="No coincide el correo electrónico" Font-Names="Segoe UI" Font-Size="X-Small">*Confirmación de coreo elecctrónico</asp:CompareValidator>
                                        </td>
                                    </tr>
                                </TBODY>
                            </table>
                        </Template>
                    </igmisc:WebPanel>
				</td>
			</tr>
			<tr>
				<td class="style23" colspan="4" style="padding:1% 40% 1% 40%">
					<igtxt:webimagebutton id="WIBtn_AgregarUsuario" runat="server" Height="24px" 
                        UseBrowserDefaults="False" Text="Agregar Usuario" ImageTextSpacing="4" 
                        onclick="WIBtn_AgregarUsuario_Click" Visible="False" Width="120px" 
                        TabIndex="14">
						<Alignments VerticalImage="Middle"></Alignments>
						<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
							RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif">
                        </RoundedCorners>
						<Appearance>
							<Image Url="./images/add_down.gif" Height="16px" Width="16px"></Image>
                            <ButtonStyle Cursor="Default"></ButtonStyle>
						</Appearance>
					</igtxt:webimagebutton>
                </td>
			</tr>
			<tr>
				<td class="style23" colspan="4">
					<asp:label id="Lbl_Error" runat="server" Font-Names="Segoe UI" 
                        ForeColor="#CC0033" Font-Size="Small"></asp:label>
                    <asp:label id="Lbl_Correcto" runat="server" Font-Names="Segoe UI" 
                        ForeColor="#00C000" Font-Size="Small"></asp:label></td>
			</tr>
			<tr>
				<td class="style23">
					<igtxt:webimagebutton id="WIBtn_Regresar" runat="server" Height="24px" 
                        UseBrowserDefaults="False" Text="Regresar" ImageTextSpacing="4" 
                        CausesValidation="False" onclick="WIBtn_Regresar_Click" Width="120px" 
                        TabIndex="15">
						<Alignments VerticalImage="Middle"></Alignments>
						<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
							RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
						<Appearance>
							<Style Cursor="Default">
							</Style>
							<Image Url="./Imagenes/Undo.png" Height="16px" Width="16px"></Image>

<ButtonStyle Cursor="Default"></ButtonStyle>
						</Appearance>
                        <HoverAppearance>
<ButtonStyle BackColor="#F9DA9B"></ButtonStyle>

                            <InnerBorder ColorBottom="33, 27, 96" ColorLeft="33, 27, 96" ColorRight="33, 27, 96"
                                ColorTop="33, 27, 96" StyleBottom="Solid" StyleLeft="Solid" StyleRight="Solid"
                                StyleTop="Solid" WidthBottom="1px" WidthLeft="1px" WidthRight="1px" WidthTop="1px" />
                            <Style BackColor="#F9DA9B"></Style>
                        </HoverAppearance>
                        <FocusAppearance>
<ButtonStyle BackColor="#FCE6AB"></ButtonStyle>

                            <InnerBorder ColorBottom="33, 27, 96" ColorLeft="33, 27, 96" ColorRight="33, 27, 96"
                                ColorTop="33, 27, 96" StyleBottom="Solid" StyleLeft="Solid" StyleRight="Solid"
                                StyleTop="Solid" WidthBottom="1px" WidthLeft="1px" WidthRight="1px" WidthTop="1px" />
                            <Style BackColor="#FCE6AB"></Style>
                        </FocusAppearance>
                        <PressedAppearance>
<ButtonStyle BackColor="#F79646"></ButtonStyle>

                            <InnerBorder ColorBottom="33, 27, 96" ColorLeft="33, 27, 96" ColorRight="33, 27, 96"
                                ColorTop="33, 27, 96" StyleBottom="Solid" StyleLeft="Solid" StyleRight="Solid"
                                StyleTop="Solid" WidthBottom="1px" WidthLeft="1px" WidthRight="1px" WidthTop="1px" />
                            <Style BackColor="#F79646"></Style>
                        </PressedAppearance>
					</igtxt:webimagebutton>
					</td>
				<td class="style23">
					<igtxt:webimagebutton id="WIBtn_Sitios" runat="server" Height="24px" 
                        UseBrowserDefaults="False" Text="Ver Sitios" ImageTextSpacing="4" 
                        onclick="WIBtn_Sitios_Click" Visible="False" CausesValidation="False" Width="120px" TabIndex="17">
						<Alignments VerticalImage="Middle"></Alignments>
						<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
							RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
						<Appearance>
							<Style Cursor="Default">
							</Style>

<ButtonStyle Cursor="Default"></ButtonStyle>
						</Appearance>
					</igtxt:webimagebutton>
					</td>
				<td class="style23">
					<igtxt:webimagebutton id="WIBtn_Usuarios" runat="server" Height="24px" 
                        UseBrowserDefaults="False" Text="Ver Usuarios" ImageTextSpacing="4" 
                        onclick="WIBtn_Usuarios_Click" Visible="False" Width="120px" TabIndex="16">
						<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
							RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
						<Alignments VerticalImage="Middle" />
						<Appearance>
							<Style Cursor="Default">
							</Style>

<ButtonStyle Cursor="Default"></ButtonStyle>
						</Appearance>
					</igtxt:webimagebutton>
					</td>
				<td class="style23">
					<igtxt:webimagebutton id="WIBtn_Terminales" runat="server" Height="24px" 
                        UseBrowserDefaults="False" Text="Ver Terminales" ImageTextSpacing="4" 
                        onclick="WIBtn_Terminales_Click" Visible="False" CausesValidation="False" Width="120px" TabIndex="18">
						<Alignments VerticalImage="Middle"></Alignments>
						<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
							RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
						<Appearance>
							<Style Cursor="Default">
							</Style>

<ButtonStyle Cursor="Default"></ButtonStyle>
						</Appearance>
					</igtxt:webimagebutton>
					</td>
			</tr>
		</table>
    
    </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <ig:WebDialogWindow ID="DlgMain" runat="server" Height="345px" InitialLocation="Centered"
            Width="785px" modal="True" Moveable="False" 
        StyleSetName="Caribbean" WindowState="Hidden">
<Header CaptionText="Atenci&#243;n">
</Header>

            <ContentPane ScrollBars="Hidden">
                <Template>
                    <iframe id="FrameMain" height="100%" width="100%" frameborder="0" 
                        scrolling="auto"></iframe>
                </Template>
            </ContentPane>
</ig:WebDialogWindow>

    </form>
</body>
</html>
