<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WF_ConfigUsuarioE.aspx.cs" Inherits="WF_ConfigUsuarioE" %>

<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB"
    Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>

<%@ Register assembly="Infragistics2.WebUI.WebCombo.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.WebUI.WebCombo" tagprefix="igcmbo" %>
<%@ Register assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.WebUI.UltraWebGrid" tagprefix="igtbl" %>
<%@ Register assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.WebUI.WebDataInput" tagprefix="igtxt" %>
<%@ Register assembly="Infragistics2.WebUI.WebSchedule.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.WebUI.WebSchedule" tagprefix="igsch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style3
        {
        }
        .style4
        {
            height: 25px;
        }
        #Table10
        {
            margin-top: 0px;
        }
        .style5
        {
            height: 17px;
        }
        .style6
        {
            height: 14px;
        }
        .style7
        {
            height: 7px;
        }
        </style>
</head>
<body style="font-size: 11px; font-family: tahoma; text-align: center; margin: 0px">
    <form id="form1" runat="server" >
    <div style="margin-left:200px; float:left">
        <igmisc:WebPanel ID="Wpn_EC_CONFIG_USUARIO_" runat="server" Width="440px" Height="350px" 
            StyleSetName="Caribbean">
        <Template>
        <table style="height: 380px; width: 400px;">
				
				<TR>
					<TD align="center" 
                        
                        
                        style="BACKGROUND-POSITION: right center; BACKGROUND-IMAGE: url('http://localhost:49351/eClock/Imagenes/fondoeClock3.jpg'); BACKGROUND-REPEAT: no-repeat">
                            <PanelStyle BackgroundImage="./skins/spacer.gif">
                            </PanelStyle>


<Header TextAlignment="Left" Text="Variables de Configuración">


</Header>
<Template>

<TABLE style="HEIGHT: 226px" id="Table5" cellSpacing=1 cellPadding=1 width=450 border=0><TBODY>
    <tr>
        <td align="left" class="style7">
        <asp:Label ID="Lbl_CONFIG_USUARIO_ID" runat="server" Enabled="False" 
                Visible="False">ID</asp:Label>
        </td>
        <td align="left" class="style7">
            <igtxt:WebTextEdit ID="Tbx_CONFIG_USUARIO_ID" runat="server" BorderColor="#7F9DB9" 
                BorderStyle="Solid" BorderWidth="1px" CellSpacing="1" Font-Names="" 
                UseBrowserDefaults="False" Width="200px" Enabled="False" Visible="False">
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
        <td align="left" class="style7">
            </td>
    </tr>
    <tr>
        <td align="left" class="style5">
        <asp:Label ID="Lbl_USUARIO_ID" runat="server">Usuario</asp:Label>
        </td>
        <td align="left" class="style5">
        <igcmbo:WebCombo ID="Cbx_USUARIO_ID" runat="server" BackColor="White" 
            BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="Black" 
            SelBackColor="DarkBlue" SelForeColor="White" Version="4.00" 
                EnableViewState="False">
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
        <td align="left" class="style5">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="left" class="style6">
    <asp:Label id="Lbl_CONFIG_USUARIO_VARIABLE" runat="server">Variable</asp:Label>
        </td>
        <td align="left" class="style6">
            <igtxt:WebTextEdit ID="Tbx_CONFIG_USUARIO_VARIABLE" runat="server" BorderColor="#7F9DB9" 
                BorderStyle="Solid" BorderWidth="1px" CellSpacing="1" Font-Names="" 
                UseBrowserDefaults="False" Width="200px">
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
        <td align="left" class="style6">
            </td>
    </tr>
    <tr>
        <td align="left" class="style3">
            <asp:Label ID="Lbl_CONFIG_USUARIO_VALOR" runat="server">Valor de la Variable</asp:Label>
        </td>
        <td align="left" class="style4">
            <igtxt:WebTextEdit ID="Tbx_CONFIG_USUARIO_VALOR" runat="server" BorderColor="#7F9DB9" 
                BorderStyle="Solid" BorderWidth="1px" CellSpacing="1" Font-Names="" 
                UseBrowserDefaults="False" Width="200px">
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
        <td align="left" class="style4">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="left" class="style3">
            <asp:Label ID="Lbl_CONFIG_USUARIO_VALOR_BIN" runat="server">Valor Binario</asp:Label>
            <br />
            <asp:CheckBox ID="Chb_CONFIG_USUARIO_VALOR_BIN" runat="server" 
                AutoPostBack="True" Font-Names=""
                Font-Size="" 
                oncheckedchanged="Chb_CONFIG_USUARIO_VALOR_BIN_CheckedChanged" 
                Checked="True" />
        </td>
        <td align="left" class="style4">
            <asp:FileUpload ID="Fup_CONFIG_USUARIO_VALOR_BIN" runat="server" />
        </td>
        <td align="left" class="style4">
            &nbsp;</td>
    </tr>
</TBODY></TABLE>
<TABLE id="Table10" cellSpacing=1 cellPadding=1 width=450 border=0>
    <tr>
        <td align="left" width="200" style="height: 1px">
            &nbsp;</td>
    </tr>
</table>

    <TABLE id="Table11" cellSpacing=1 cellPadding=1 width=450 border=0>
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
                <igtxt:WebImageButton ID="Btn_Deshacer0" runat="server" Height="22px" 
                    ImageTextSpacing="4" onclick="Btn_DeshacerCambios_Click" Text="Deshacer" 
                    UseBrowserDefaults="False" Width="150px">
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
                &nbsp;</td>
            <td align="left">
                <igtxt:WebImageButton ID="Btn_Guardar0" runat="server" Height="22px" 
                    ImageTextSpacing="4" onclick="Btn_GuardarCambios_Click" Text="Guardar" 
                    UseBrowserDefaults="False" Width="150px">
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
                        &nbsp;&nbsp;</TD>
				</TR>
				<TR>
					<TD align="center">
						&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                        </TD>
				</TR>
				</TABLE>
                </Template>
                </igmisc:WebPanel>
 </div>
    </form>
</body>
</html>
