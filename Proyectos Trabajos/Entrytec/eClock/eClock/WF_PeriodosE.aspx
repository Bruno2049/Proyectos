<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WF_PeriodosE.aspx.cs" Inherits="WF_PeriodosE" %>
<%@ Register assembly="Infragistics2.WebUI.WebCombo.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.WebUI.WebCombo" tagprefix="igcmbo" %>
<%@ Register assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.WebUI.Misc" tagprefix="igmisc" %>
<%@ Register assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.WebUI.UltraWebGrid" tagprefix="igtbl" %>
<%@ Register assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.WebUI.WebDataInput" tagprefix="igtxt" %>
<%@ Register assembly="Infragistics2.WebUI.WebSchedule.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.WebUI.WebSchedule" tagprefix="igsch" %>

<%@ Register assembly="Infragistics2.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>

<%@ Register assembly="Infragistics2.WebUI.WebDateChooser.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.WebUI.WebSchedule" tagprefix="igsch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">

        .style3
        {
            width: 150px;
            height: 25px;
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
        }
        </style>
</head>
<body style="font-size: 11px; font-family: tahoma; text-align: center; margin: 0px">
    <form id="form1" runat="server" >
    <div>
        <table style="height: 380px; width: 400px;">
				<TR>
					<TD style="HEIGHT: 0%">
						</TD>
				</TR>
				<TR>
					<TD align="center" 
                        
                        style="BACKGROUND-POSITION: right center; BACKGROUND-IMAGE: url(./Imagenes/fondoeClock3.jpg); BACKGROUND-REPEAT: no-repeat">
                            <PanelStyle BackgroundImage="./skins/spacer.gif">
                            </PanelStyle>


<Header TextAlignment="Left" Text="Datos de Terminal">


</Header>
<Template>

<TABLE style="HEIGHT: 226px" id="Table5" cellSpacing=1 cellPadding=1 width=450 border=0><TBODY>
    <tr>
        <td align="left" class="style3">
        <asp:Label ID="Lbl_PERIODO_N_ID" runat="server">Nombre del Período</asp:Label>
        </td>
        <td align="left" class="style4">
        <igcmbo:WebCombo ID="Cbx_PERIODO_N_ID" runat="server" BackColor="White" 
            BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="Black" 
            SelBackColor="DarkBlue" SelForeColor="White" Version="4.00" Enabled="False" 
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
        <td align="left" class="style4">
            &nbsp;</td>
    </tr>
    <TR>
    <TD align=left class="style3">
            <asp:Label ID="Lbl_EDO_PERIODO_ID" runat="server">Estado del Período</asp:Label>
    </TD><TD align=left class="style4">
            <igcmbo:WebCombo ID="Cbx_EDO_PERIODO_ID" runat="server" BackColor="White" 
                BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="Black" 
                SelBackColor="DarkBlue" SelForeColor="White" Version="4.00">
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
    </TD>
    <td align="left" class="style4">
        </td>
    </TR>
    <tr>
        <td align="left" class="style3">
    <asp:Label id="Lbl_PERIODO_NOM_INICIO" runat="server">Inicio de Período de Nómina</asp:Label>
        </td>
        <td align="left" class="style4">
            <igsch:WebDateChooser ID="Wdc_PERIODO_NOM_INICIO" runat="server">
            </igsch:WebDateChooser>
        </td>
        <td align="left" class="style4">
            </td>
    </tr>
    <TR><TD align=left class="style3">
            <asp:Label ID="Lbl_PERIODO_NOM_FIN" runat="server">Fin de Período de Nómina</asp:Label>
        </TD>
        <TD align=left class="style4">
            <igsch:WebDateChooser ID="Wdc_PERIODO_NOM_FIN" runat="server">
            </igsch:WebDateChooser>
        </TD>
    <td align="left" class="style4">
        </td>
    </TR>
    <TR><TD align=left class="style3">
    <asp:Label id="Lbl_PERIODO_ASIS_INICIO" runat="server" Width="139px" Height="16px">Inicio de 
        Período Asistencia</asp:Label></TD>
        <TD align=left class="style4">
            <igsch:WebDateChooser ID="Wdc_PERIODO_ASIS_INICIO" runat="server">
            </igsch:WebDateChooser>
        </TD>
    <td align="left" class="style4">
        </td>
    </TR>
    <TR><TD align=left class="style3">
    <asp:Label id="Lbl_PERIODO_ASIS_FIN" runat="server">Fin de Período de Asistencia</asp:Label></TD>
        <TD align=left class="style4">
            <igsch:WebDateChooser ID="Wdc_PERIODO_ASIS_FIN" runat="server">
            </igsch:WebDateChooser>
        </TD>
    <td align="left" class="style4">
        </td>
    </TR><TR><TD style="WIDTH: 150px; height: 13px;" align=left><P>
    <asp:Label id="Lbl_PERIODO_NO" runat="server">Número de Período</asp:Label></P></TD>
        <TD align=left style="height: 13px">
            <igtxt:WebTextEdit ID="Tbx_PERIODO_NO" runat="server" BorderColor="#7F9DB9" 
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
        </TD>
    <td align="left" style="height: 13px">
        &nbsp;</td>
    </TR>
    <TR><TD align=left class="style3">
    <asp:Label id="Lbl_PERIODO_ANO" runat="server">Año del Período</asp:Label></TD>
        <TD align=left class="style4">
            <igtxt:WebTextEdit ID="Tbx_PERIODO_ANO" runat="server" BorderColor="#7F9DB9" 
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
        </TD>
    <td align="left" class="style4">
        </td>
    </TR>
    <tr>
        <td align="left" class="style5" colspan="3">
        </td>
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
 </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    </form>
</body>
</html>