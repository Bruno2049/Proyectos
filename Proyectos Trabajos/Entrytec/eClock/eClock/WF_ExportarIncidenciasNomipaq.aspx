<%@ Page Language="C#"AutoEventWireup="true" CodeFile="WF_ExportarIncidenciasNomipaq.aspx.cs" Inherits="WF_ExportarIncidenciasNomipaq" %>

<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDateChooser.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Exportar a NOI</title>
    <style type="text/css">
        .style1
        {
            height: 31px;
        }
    </style>
</head>
<body style="font-size: 11px; font-family: tahoma; text-align: center; margin: 0px; ">
    <form id="form1" runat="server" >
    <div>
    <igmisc:WebPanel ID="WebPanel2" runat="server" EnableAppStyling="True" 
            Height="356px" StyleSetName="Caribbean" >
        <Header Text="Exportación de incidencias Nomipaq" >
            
        </Header>
        <Template>
            <table style="width: 401px; height: 328px;">
                <tr>
                    <td colspan="2">
                        Rango de Fechas</td>
                </tr>
                <tr>
                    <td style="width: 146px">
                        <igsch:WebCalendar ID="FechaI" runat="server">
                            <Layout DayNameFormat="FirstLetter" FooterFormat="" ShowFooter="False" ShowTitle="False">
                                <TodayDayStyle BackColor="Transparent" BorderColor="CornflowerBlue" />
                                <WeekendDayStyle BackColor="#E0E0E0" />
                                <SelectedDayStyle BackColor="CornflowerBlue" />
                                <DayStyle BackColor="White" Font-Names="Arial" Font-Size="9pt" />
                                <OtherMonthDayStyle ForeColor="#ACA899" />
                                <DayHeaderStyle BackColor="#7A96DF" Font-Names="Tahoma" Font-Size="9pt" ForeColor="White" />
                                <TitleStyle BackColor="#9EBEF5" />
                                <CalendarStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False">
                                </CalendarStyle>
                            </Layout>
                        </igsch:WebCalendar>
                    </td>
                    <td>
                        <igsch:WebCalendar ID="FechaF" runat="server">
                            <Layout DayNameFormat="FirstLetter" FooterFormat="" ShowFooter="False" ShowTitle="False">
                                <TodayDayStyle BackColor="Transparent" BorderColor="CornflowerBlue" />
                                <WeekendDayStyle BackColor="#E0E0E0" />
                                <SelectedDayStyle BackColor="CornflowerBlue" />
                                <DayStyle BackColor="White" Font-Names="Arial" Font-Size="9pt" />
                                <OtherMonthDayStyle ForeColor="#ACA899" />
                                <DayHeaderStyle BackColor="#7A96DF" Font-Names="Tahoma" Font-Size="9pt" ForeColor="White" />
                                <TitleStyle BackColor="#9EBEF5" />
                                <CalendarStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False">
                                </CalendarStyle>
                            </Layout>
                        </igsch:WebCalendar>
                    </td>
                </tr>
                <tr>
                    <td style="width: 146px">
                        Valor</td>
                    <td>
                        <igtxt:WebNumericEdit ID="txtValor" runat="server" MaxValue="9" MinDecimalPlaces="Two"
                            MinValue="1" Enabled="False">
                        </igtxt:WebNumericEdit>
                    </td>
                </tr>
                <tr>
                    <td style="width: 146px">
                        Num de Periodo</td>
                    <td>
                        <igtxt:WebNumericEdit ID="txtPeriodo" runat="server" MaxValue="999" MinValue="0">
                        </igtxt:WebNumericEdit>
                    </td>
                </tr>
                <tr>
                    <td style="width: 146px">
                        Ejercicio</td>
                    <td>
                        <igtxt:WebNumericEdit ID="txtEjercicio" runat="server" MaxValue="2100" MinValue="1990">
                        </igtxt:WebNumericEdit>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="style1">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="txtPeriodo" 
                            ErrorMessage="* Se requiere introducir el periodo"></asp:RequiredFieldValidator>
                        <asp:Label ID="LCorrecto" runat="server" Font-Names="Arial Narrow" 
                            Font-Size="Small" ForeColor="Green"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style1" colspan="2">
                        <igtxt:WebImageButton ID="BDeshacerCambios" runat="server" Height="22px" 
                            ImageTextSpacing="4" OnClick="BDeshacerCambios_Click" Text="Regresar" 
                            UseBrowserDefaults="False" Width="150px">
                            <Alignments VerticalAll="Bottom" VerticalImage="Middle" />
                            <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" 
                                FocusImageUrl="ig_butXP3wh.gif" HoverImageUrl="ig_butXP2wh.gif" 
                                ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400" 
                                PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                            <Appearance>
                                <style cursor="Default">
                                </style>
                                <Image Height="16px" Url="./Imagenes/Back.png" Width="16px" />
                            </Appearance>
                        </igtxt:WebImageButton>
                        &nbsp; &nbsp; &nbsp;
                        <igtxt:WebImageButton ID="BGuardarCambios" runat="server" Height="22px" 
                            ImageTextSpacing="4" OnClick="BGuardarCambios_Click" Text="Generar Archivo" 
                            UseBrowserDefaults="False" Width="150px">
                            <Alignments VerticalAll="Bottom" VerticalImage="Middle" />
                            <Appearance>
                                <Image Height="16px" Url="./Imagenes/Save_as.png" Width="16px" />
                                <style cursor="Default">
                                </style>
                            </Appearance>
                            <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" 
                                FocusImageUrl="ig_butXP3wh.gif" HoverImageUrl="ig_butXP2wh.gif" 
                                ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400" 
                                PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                        </igtxt:WebImageButton>
                    </td>
                </tr>
            </table>
        </Template>
    </igmisc:WebPanel>
 </div>
    </form>
</body>
</html>
