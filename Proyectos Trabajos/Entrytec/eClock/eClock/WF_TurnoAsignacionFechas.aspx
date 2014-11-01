<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WF_TurnoAsignacionFechas.aspx.cs" Inherits="WF_TurnoAsignacionFechas" %>

<%@ Register Assembly="Infragistics2.WebUI.WebDateChooser.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>

<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics2.WebUI.WebCombo.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebCombo" TagPrefix="igcmbo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Página sin título</title>
    <style type="text/css">
        .style1
        {
            height: 160px;
        }
    </style>
</head>
<body style="font-size: 11px; font-family: tahoma; text-align: left; margin: 0px; vertical-align: top;">

    <form id="form1" runat="server">
        <table>
            <tr>
                <td>
        <igmisc:webgroupbox id="WebGroupBox1" runat="server" enableappstyling="True" height="100%"
            stylesetname="Caribbean" text="Periodo de Asignación" width="710px"><Template>
            <TABLE style="WIDTH: 100%; HEIGHT: 100%" id="Table1" cellSpacing=5 cellPadding=1 border=0>
            <TBODY><TR><TD class="style1">
                </TD>
                <td align="left" class="style1" >
                    <asp:Label ID="Lbl_Desde" runat="server" Text="Desde:"></asp:Label>
                </td>
                <TD align=left class="style1" >
                    <asp:Calendar ID="Cal_Desde" runat="server"></asp:Calendar>
                </TD><TD align=left class="style1" >
                    <asp:Label ID="Lbl_Hasta" runat="server" Text="Hasta:"></asp:Label>
                </TD><TD align=left class="style1" >
                    <asp:Calendar ID="Cal_Hasta" runat="server"></asp:Calendar>
                </TD>
                <td align="left" class="style1">
                </td>
                </TR>
            </TBODY>
            </TABLE>
</Template>
</igmisc:webgroupbox>
                </td>
            </tr>
            <tr>
                <td>
        <igmisc:webgroupbox id="WebGroupBox3" runat="server" enableappstyling="True" height="100%"
            stylesetname="Caribbean" text="Empleados" width="710px"><Template>
<TABLE><TBODY><TR>
    <TD align=left>
        <asp:Label ID="Lbl_Instrucciones" runat="server" Text="Introduzca los Números de Empleados separados por coma (,)"></asp:Label>
        <asp:TextBox ID="Tbx_PERSONA_LINK_ID" runat="server" Height="180px" 
            TextMode="MultiLine" Width="640px"></asp:TextBox>
    </TD></TR>
    </TBODY></TABLE>
</Template>
</igmisc:webgroupbox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width: 716px">
        <igmisc:webgroupbox id="WebGroupBox2" runat="server" enableappstyling="True" height="55px"
            stylesetname="Caribbean" text="Mover empleados a otro turno" width="710px"><Template>
<TABLE style="WIDTH: 100%; HEIGHT: 10px" id="Table2" cellSpacing=5 cellPadding=1 border=0><TBODY><TR><TD style="WIDTH: 485px" align=left>
    <asp:Label ID="Lbl_Turnos" runat="server" 
        Text="Seleccione el turno que se les asignará"></asp:Label>
    </TD><TD style="WIDTH: 385px" align=left><igcmbo:WebCombo id="Wco_EC_TURNOS" 
            runat="server" BorderWidth="1px" BorderStyle="Solid" BorderColor="Silver" 
            EnableAppStyling="True" StyleSetName="Caribbean" Version="4.00" 
            BackColor="White" ForeColor="Black" SelBackColor="DarkBlue" 
            SelForeColor="White">

<ExpandEffects ShadowColor="LightGray"></ExpandEffects>

        <Columns>
<igtbl:UltraGridColumn>
<Header Caption="Column0"></Header>
</igtbl:UltraGridColumn>
</Columns>

<DropDownLayout BorderCollapse="Separate" RowHeightDefault="20px" Version="4.00">
<FrameStyle Cursor="Default" BackColor="Silver" BorderWidth="2px" BorderStyle="Ridge" Font-Names="Verdana" Font-Size="10pt" Height="130px" Width="325px"></FrameStyle>

<HeaderStyle BackColor="LightGray" BorderStyle="Solid">
<BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px"></BorderDetails>
</HeaderStyle>

<RowStyle BackColor="White" BorderColor="Gray" BorderWidth="1px" BorderStyle="Solid">
<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
</RowStyle>

<SelectedRowStyle BackColor="DarkBlue" ForeColor="White"></SelectedRowStyle>
</DropDownLayout>
</igcmbo:WebCombo></TD><TD align=left><P align=center>
        <igtxt:webimagebutton id="WIBtn_Asignar_Turno" runat="server" Height="22px" Width="150px" 
            Text="Asignar Turno" UseBrowserDefaults="False" OnClick="BtnMover_Click">
<Appearance>
<Image Height="16px" Width="16px" Url="./Imagenes/Save_as.png"></Image>

<ButtonStyle Cursor="Default"></ButtonStyle>
</Appearance>

<RoundedCorners RenderingType="FileImages" MaxWidth="400" MaxHeight="80" ImageUrl="ig_butXP1wh.gif" HoverImageUrl="ig_butXP2wh.gif" FocusImageUrl="ig_butXP3wh.gif" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif"></RoundedCorners>

<Alignments VerticalAll="Bottom" VerticalImage="Middle"></Alignments>
</igtxt:webimagebutton></P></TD></TR></TBODY></TABLE>
</Template>
</igmisc:webgroupbox>
                </td>
            </tr>
            <tr>
                <td style="width: 716px; text-align: center;">
                    <asp:Label ID="Lbl_Correcto" runat="server" ForeColor="#00CC00" 
                        style="text-align: left"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 716px; text-align: center;">
                    <asp:Label ID="Lbl_Error" runat="server" ForeColor="Red" 
                        style="text-align: left"></asp:Label>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
