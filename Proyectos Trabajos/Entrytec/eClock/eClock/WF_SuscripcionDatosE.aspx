<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WF_SuscripcionDatosE.aspx.cs" Inherits="WF_SuscripcionDatosE" %>

<%@ Register assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.WebUI.Misc" tagprefix="igmisc" %>
<%@ Register assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.WebUI.WebDataInput" tagprefix="igtxt" %>
<%@ Register assembly="Infragistics2.WebUI.WebCombo.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.WebUI.WebCombo" tagprefix="igcmbo" %>
<%@ Register assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.WebUI.UltraWebGrid" tagprefix="igtbl" %>
<%@ Register assembly="Infragistics2.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.Web.UI.LayoutControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics2.WebUI.WebDateChooser.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.WebUI.WebSchedule" tagprefix="igsch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style4
        {
            height: 380px;
            width: 685px;
        }
        .style8
        {
            text-align: left;
        }
        .style10
        {
        }
        .style11
        {
            width: 81px;
            text-align: center;
        }
        .style12
        {
            width: 98px;
        }
    </style>
</head>
<body style="font-family: 'Segoe UI'; font-size: small; width: auto; height: auto; padding:1% 15% 1% 15%">
    <form id="Frm_EC_SUSCRIP_DATOS" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
        </asp:ScriptManager>
        <ig:WebDialogWindow ID="Wdw_EC_SUSCRIP_DATOS" runat="server" Height="686px" 
            Width="705px" Resizer-Enabled="false" Font-Names="Segoe UI" 
            Font-Size="Small" WindowState="Normal">
            <Header><CloseBox Visible="false" /></Header>
    <ContentPane>
    <Template>
        <table width="600" class="style4">
				<TR>
					<TD style="HEIGHT: 0%" colspan="5"></TD>
				</TR>
				<TR>
					<TD align="center" 
                        style="BACKGROUND-POSITION: right center; BACKGROUND-IMAGE: url('http://localhost:49351/eClock/Imagenes/fondoeClock3.jpg'); BACKGROUND-REPEAT: no-repeat" 
                        colspan="5">
                            <PanelStyle BackgroundImage="./skins/spacer.gif">
                            </PanelStyle>

<Header TextAlignment="Left" Text="Datos de la Suscripción">
</Header>
<Template>
    <table id="Table11" cellspacing="1" cellpadding="1" width="450" border="0">
        <tr>
            <td align="left">
                <asp:Label ID="Lbl_SUSCRIPCION_ID" runat="server" Text="Suscripción ID"></asp:Label>
            </td>
            <td align="left">&nbsp;</td><td class="style8">
            <igtxt:WebNumericEdit ID="Wne_SUSCRIPCION_ID" runat="server" 
                Enabled="False">
            </igtxt:WebNumericEdit>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="Lbl_SUSCRIPCION_NOMBRE" runat="server" Text="Nombre"></asp:Label>
            </td>
            <td align="left">&nbsp;</td><td class="style8">
            <igcmbo:WebCombo ID="Wco_SUSCRIPCION_NOMBRE" runat="server" BackColor="White" 
                BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="Black" 
                SelBackColor="DarkBlue" SelForeColor="White" TabIndex="20" Version="4.00" 
                Enabled="False">
                <ExpandEffects ShadowColor="LightGray" />
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
                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" 
                        WidthTop="1px" />
                    </HeaderStyle>
                    <RowStyle BackColor="White" BorderColor="Gray" BorderStyle="Solid" 
                        BorderWidth="1px">
                    <BorderDetails WidthLeft="0px" WidthTop="0px" />
                    </RowStyle>
                    <SelectedRowStyle BackColor="DarkBlue" ForeColor="White" />
                </dropdownlayout>
            </igcmbo:WebCombo>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="Lbl_SUSCRIP_PRECIO_ID" runat="server" Text="Precio"></asp:Label>
            </td>
            <td align="left">&nbsp;</td><td class="style8">
            <igcmbo:WebCombo ID="Wco_SUSCRIP_PRECIO_ID" runat="server" BackColor="White" 
                BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="Black" 
                SelBackColor="DarkBlue" SelForeColor="White" TabIndex="1" Version="4.00" 
                Enabled="True">
                <ExpandEffects ShadowColor="LightGray" />
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
                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" 
                        WidthTop="1px" />
                    </HeaderStyle>
                    <RowStyle BackColor="White" BorderColor="Gray" BorderStyle="Solid" 
                        BorderWidth="1px">
                    <BorderDetails WidthLeft="0px" WidthTop="0px" />
                    </RowStyle>
                    <SelectedRowStyle BackColor="DarkBlue" ForeColor="White" />
                </dropdownlayout>
            </igcmbo:WebCombo>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="Lbl_SUSCRIP_DATOS_RAZON" runat="server" Text="Razón Social"></asp:Label>
            </td>
            <td align="left">&nbsp;</td><td class="style8">
            <igtxt:WebTextEdit ID="Wtx_SUSCRIP_DATOS_RAZON" runat="server" TabIndex="2">
            </igtxt:WebTextEdit>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="Lbl_SUSCRIP_DATOS_RFC" runat="server" Text="RFC"></asp:Label>
            </td>
            <td align="left">&nbsp;</td><td class="style8">
            <igtxt:WebTextEdit ID="Wtx_SUSCRIP_DATOS_RFC" runat="server" TabIndex="3">
            </igtxt:WebTextEdit>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="Lbl_SUSCRIP_DATOS_DIRECCION1" runat="server" Text="Dirección"></asp:Label>
            </td>
            <td align="left">&nbsp;</td><td class="style8">
            <igtxt:WebTextEdit ID="Wtx_SUSCRIP_DATOS_DIRECCION1" runat="server" 
                TabIndex="4">
            </igtxt:WebTextEdit>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="Lbl_SUSCRIP_DATOS_DIRECCION2" runat="server">Dirección 
                2</asp:Label>
            </td>
            <td align="left">&nbsp;</td><td class="style8">
            <igtxt:WebTextEdit ID="Wtx_SUSCRIP_DATOS_DIRECCION2" runat="server" 
                TabIndex="5">
            </igtxt:WebTextEdit>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="Lbl_SUSCRIP_DATOS_CIUDAD" runat="server" Text="Ciudad"></asp:Label>
            </td>
            <td align="left">&nbsp;</td><td class="style8">
            <igtxt:WebTextEdit ID="Wtx_SUSCRIP_DATOS_CIUDAD" runat="server" TabIndex="6">
            </igtxt:WebTextEdit>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="Lbl_SUSCRIP_DATOS_ESTADO" runat="server" Text="Estado"></asp:Label>
            </td>
            <td align="left">&nbsp;</td><td class="style8">
            <igtxt:WebTextEdit ID="Wtx_SUSCRIP_DATOS_ESTADO" runat="server" TabIndex="7">
            </igtxt:WebTextEdit>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="Lbl_SUSCRIP_DATOS_PAIS" runat="server" Text="País"></asp:Label>
            </td>
            <td align="left">&nbsp;</td><td class="style8">
            <igtxt:WebTextEdit ID="Wtx_SUSCRIP_DATOS_PAIS" runat="server" TabIndex="8">
            </igtxt:WebTextEdit>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="Lbl_EDO_SUSCRIPCION_ID" runat="server" 
                    Text="Estado de la Suscripción"></asp:Label>
            </td>
            <td align="left">&nbsp;</td><td class="style8">
            <igcmbo:WebCombo ID="Wco_EDO_SUSCRIPCION_ID" runat="server" BackColor="White" 
                BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="Black" 
                SelBackColor="DarkBlue" SelForeColor="White" Version="4.00" TabIndex="9">
                <ExpandEffects ShadowColor="LightGray" />
                <Columns>
                    <igtbl:UltraGridColumn>
                        <Header Caption="Column0">
                        </Header>
                    </igtbl:UltraGridColumn>
                </Columns>
                <DropDownLayout BorderCollapse="Separate" RowHeightDefault="20px" 
                    Version="4.00">
                    <FrameStyle BackColor="Silver" BorderStyle="Ridge" BorderWidth="2px" 
                        Cursor="Default" Font-Names="Verdana" Font-Size="10pt" Height="130px" 
                        Width="325px">
                    </FrameStyle>
                    <HeaderStyle BackColor="LightGray" BorderStyle="Solid">
                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" 
                        WidthTop="1px" />
                    </HeaderStyle>
                    <RowStyle BackColor="White" BorderColor="Gray" BorderStyle="Solid" 
                        BorderWidth="1px">
                    <BorderDetails WidthLeft="0px" WidthTop="0px" />
                    </RowStyle>
                    <SelectedRowStyle BackColor="DarkBlue" ForeColor="White" />
                </DropDownLayout>
            </igcmbo:WebCombo>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="Lbl_SUSCRIP_DATOS_FACTURAR" runat="server" Text="Factura"></asp:Label>
            </td>
            <td align="left">&nbsp;</td><td class="style8">
            <asp:CheckBox ID="Chb_SUSCRIP_DATOS_FACTURAR" runat="server" TabIndex="10" />
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="Lbl_SUSCRIP_DATOS_CONTRATACION" runat="server" 
                    Text="Fecha Contratación"></asp:Label>
            </td>
            <td align="left">&nbsp;</td><td class="style8">
            <igsch:WebDateChooser ID="Wdc_SUSCRIP_DATOS_CONTRATACION" runat="server" 
                TabIndex="11">
            </igsch:WebDateChooser>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="Lbl_SUSCRIP_DATOS_EMPLEADOS" runat="server" 
                    Text="Número de Empleados"></asp:Label>
            </td>
            <td align="left">&nbsp;</td><td class="style8">
            <igtxt:WebNumericEdit ID="Wne_SUSCRIP_DATOS_EMPLEADOS" runat="server" 
                TabIndex="12">
            </igtxt:WebNumericEdit>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="Lbl_SUSCRIP_DATOS_TERMINALES" runat="server" 
                    Text="Número de Terminales"></asp:Label>
            </td>
            <td align="left">&nbsp;</td><td class="style8">
            <igtxt:WebNumericEdit ID="Wne_SUSCRIP_DATOS_TERMINALES" runat="server" 
                TabIndex="13">
            </igtxt:WebNumericEdit>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="Lbl_SUSCRIP_DATOS_USUARIOS" runat="server" 
                    Text="Número de Usuarios"></asp:Label>
            </td>
            <td align="left">&nbsp;</td><td class="style8">
            <igtxt:WebNumericEdit ID="Wne_SUSCRIP_DATOS_USUARIOS" runat="server" 
                TabIndex="14">
            </igtxt:WebNumericEdit>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="Lbl_SUSCRIP_DATOS_ALUMNOS" runat="server" 
                    Text="Número de Alumnos"></asp:Label>
            </td>
            <td align="left">&nbsp;</td><td class="style8">
            <igtxt:WebNumericEdit ID="Wne_SUSCRIP_DATOS_ALUMNOS" runat="server" 
                TabIndex="15">
            </igtxt:WebNumericEdit>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="Lbl_SUSCRIP_DATOS_VISITANTES" runat="server" 
                    Text="Número de Visitantes"></asp:Label>
            </td>
            <td align="left">&nbsp;</td><td class="style8">
            <igtxt:WebNumericEdit ID="Wne_SUSCRIP_DATOS_VISITANTES" runat="server" 
                TabIndex="16">
            </igtxt:WebNumericEdit>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="Lbl_SUSCRIP_DATOS_ADICIONALES" runat="server" 
                    Text="Permite Adicionales"></asp:Label>
            </td>
            <td align="left">&nbsp;</td><td class="style8">
            <asp:CheckBox ID="Chb_SUSCRIP_DATOS_ADICIONALES" runat="server" TabIndex="17" />
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="Lbl_SUSCRIP_DATOS_OTROS" runat="server" Text="Otros datos"></asp:Label>
            </td>
            <td align="left">&nbsp;</td><td class="style8">
            <igtxt:WebTextEdit ID="Wtx_SUSCRIP_DATOS_OTROS" runat="server" TabIndex="18">
            </igtxt:WebTextEdit>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="Lbl_SUSCRIP_DATOS_MENSUAL" runat="server" Text="Mensualidad"></asp:Label>
            </td>
            <td align="left">&nbsp;</td><td class="style8">
            <igtxt:WebNumericEdit ID="Wne_SUSCRIP_DATOS_MENSUAL" runat="server" 
                TabIndex="19">
            </igtxt:WebNumericEdit>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="Lbl_SUSCRIP_DATOS_FINAL" runat="server" 
                    Text="Fecha Fin Contrato"></asp:Label>
            </td>
            <td align="left">&nbsp;</td><td class="style8">
            <igsch:WebDateChooser ID="Wdc_SUSCRIP_DATOS_FINAL" runat="server" TabIndex="20">
            </igsch:WebDateChooser>
            </td>
        </tr>
        
        
        <tr>
            <td align="left">
                <asp:Label ID="Lbl_SUSCRIPCION_BORRADO" runat="server" Text="Factura"></asp:Label>
            </td>
            <td align="left">
                &nbsp;</td>
            <td class="style8">
                <asp:CheckBox ID="Chb_SUSCRIPCION_BORRADO" runat="server" TabIndex="10" />
            </td>
        </tr>
        
        
    </table>
</Template>
                        </TD>
				</TR>
				<TR>
					<TD align="center" class="style10">
                        &nbsp;</TD>
					<td align="center" class="style10">
                        &nbsp;</td>
                    <td class="style11" align="center">
                        <asp:Label ID="Lbl_Error" runat="server" ForeColor="#CC0033"></asp:Label>
                        <asp:Label ID="Lbl_Correcto" runat="server" ForeColor="#00C000"></asp:Label>
                    </td>
					<TD align="center" class="style12">
                        &nbsp;</TD>
					<TD align="center" class="style12">
                        &nbsp;</TD>
				</TR>
				<tr>
                    <td align="center" class="style10" colspan="5">
                        <igtxt:WebImageButton ID="WIBtn_Deshacer" runat="server" Height="22px" 
                            ImageTextSpacing="4" onclick="WIBtn_Deshacer_Click" TabIndex="22" 
                            Text="Deshacer" UseBrowserDefaults="False" Width="150px">
                            <Alignments VerticalAll="Bottom" VerticalImage="Middle" />
                            <appearance>
                                <Image Height="16px" Url="./Imagenes/Undo.png" Width="16px" />
                                <style cursor="Default">



                        </style>
                            </appearance>
                            <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" 
                                FocusImageUrl="ig_butXP3wh.gif" HoverImageUrl="ig_butXP2wh.gif" 
                                ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400" 
                                PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                        </igtxt:WebImageButton>
                        <igtxt:WebImageButton ID="WIBtn_Guardar" runat="server" Height="22px" 
                            ImageTextSpacing="4" onclick="WIBtn_Guardar_Click" TabIndex="21" Text="Guardar" 
                            UseBrowserDefaults="False" Width="150px">
                            <Alignments VerticalAll="Bottom" VerticalImage="Middle" />
                            <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" 
                                FocusImageUrl="ig_butXP3wh.gif" HoverImageUrl="ig_butXP2wh.gif" 
                                ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400" 
                                PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                            <appearance>
                                <style cursor="Default">



                        </style>
                                <Image Height="16px" Url="./Imagenes/Save_as.png" Width="16px" />
                            </appearance>
                        </igtxt:WebImageButton>
                        <igtxt:WebImageButton ID="WIBtn_Salir" runat="server" Height="22px" 
                            ImageTextSpacing="4" onclick="WIBtn_Salir_Click" TabIndex="21" Text="Salir" 
                            UseBrowserDefaults="False" Width="150px">
                            <Alignments VerticalAll="Bottom" VerticalImage="Middle" />
                            <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" 
                                FocusImageUrl="ig_butXP3wh.gif" HoverImageUrl="ig_butXP2wh.gif" 
                                ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400" 
                                PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                            <appearance>
                                <style cursor="Default">



                        </style>
                                <Image Height="16px" Url="./Imagenes/Cancel.png" Width="16px" />
                                <ButtonStyle Cursor="Default">
                                </ButtonStyle>
                            </appearance>
                        </igtxt:WebImageButton>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                    </td>
                </tr>
				</TABLE>
                </Template>
                </ContentPane>
    </ig:WebDialogWindow>
    </div>
    </form>
</body>
</html>
