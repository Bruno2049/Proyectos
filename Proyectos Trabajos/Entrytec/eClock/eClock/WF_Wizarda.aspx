<%@ Page Language="C#" MasterPageFile="~/MasterPage2.master"  AutoEventWireup="true" CodeFile="WF_Wizarda.aspx.cs" Inherits="WF_Wizarda" %>

<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    &nbsp;<table width="100%">
        <tr>
            <td align="left" style="height: 188px">
                <img src="Imagenes/Wizarda.png" /></td>
            <td align="left" style="height: 188px">

        <igmisc:webpanel id="WebPanel1" runat="server" cssclass="igwpMainBlack2k7" font-size="Medium"
            height="100%" width="100%" BorderColor="SteelBlue" BorderStyle="Outset" Font-Names="Tahoma">
<PanelStyle CssClass="igwpPanelBlack2k7">
<Padding Left="15px" Right="15px"></Padding>
</PanelStyle>

<Header Text="ASISTENTE&amp;nbsp;DE CONFIGURACION">
<ExpandedAppearance>
<Styles ForeColor="White" CssClass="igwpHeaderExpandedBlack2k7" BackColor="SteelBlue"></Styles>
</ExpandedAppearance>

<HoverAppearance>
<Styles CssClass="igwpHeaderHoverBlack2k7"></Styles>
</HoverAppearance>

<CollapsedAppearance>
<Styles CssClass="igwpHeaderCollapsedBlack2k7"></Styles>
</CollapsedAppearance>

<ExpansionIndicator Height="0px" Width="0px"></ExpansionIndicator>
</Header>
<Template>
<BR /><BR />Este Asistente configurará el sistema, es muy importante que lea las intrucciones de cada etapa y que este seguro de los cambios a realizar.<BR />Para volver a usar el&nbsp;Asistente de Configuración, deberá accesar&nbsp;al módulo de configuración del&nbsp;sistema. Los cambios que se realicen posteriormente a la primera configuración, repercutirán en los registros guardados en la base&nbsp;de datos.<BR />
</Template>
</igmisc:webpanel>
            </td>
        </tr>
            <tr>
                <td align="center" style="height: 33px; text-align: center">
                </td>
                <td align="center" style="width: 100%; height: 33px; text-align: center;">
                    <igtxt:webimagebutton id="BGuardarCambios" runat="server" height="22px" onclick="BGuardarCambios_Click"
                        text="Iniciar" usebrowserdefaults="False" width="150px" ImageTextSpacing="4">
<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>

<Appearance>
<Image Url="./Imagenes/Next.png" Height="16px" Width="16px"></Image>

</Appearance>

<RoundedCorners HoverImageUrl="ig_butXP2wh.gif" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" MaxWidth="400" MaxHeight="80" ImageUrl="ig_butXP1wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
</igtxt:webimagebutton>
                </td>
            </tr>
        </table>
    

</asp:Content>