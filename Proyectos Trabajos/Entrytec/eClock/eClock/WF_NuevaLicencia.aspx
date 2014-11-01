<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WF_NuevaLicencia.aspx.cs" Inherits="WF_NuevaLicencia" MasterPageFile="MasterPage3.master" %>


<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.WebUI.WebDataInput" tagprefix="igtxt" %>

<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">

    <table style="width:100%;">
        <tr>
            <td>
                &nbsp;</td>
            <td colspan="2" style="text-align: left">
                <div class="C03_DarkDenim_TitleLine">
                    <div class="C03_Denim_DarkTitle">
                        <span id="dnn_ctr_dnnTITLE_lblTitle" class="style1">Licenciamiento de eClock </span></div>
                </div>
                <br />
                Para usar las funciones completas es indispen</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="text-align: right">
                No. de Empleado:</td>
            <td style="text-align: left">
                <igtxt:WebTextEdit ID="Tbx_Persona_link_ID" runat="server" BorderColor="#9FB672" 
                    BorderStyle="Solid" BorderWidth="1px" CellSpacing="1" 
                    UseBrowserDefaults="False" Width="221px">
                    <ButtonsAppearance CustomButtonDefaultTriangleImages="Arrow">
                        <ButtonPressedStyle BackColor="#99B079" BorderColor="#75805E">
                        </ButtonPressedStyle>
                        <ButtonStyle BackColor="#829762" BorderColor="#858C6C" BorderStyle="Solid" 
                            BorderWidth="1px" ForeColor="White" Width="13px">
                        </ButtonStyle>
                        <ButtonHoverStyle BackColor="#BCC794" BorderColor="#8C9762">
                        </ButtonHoverStyle>
                        <ButtonDisabledStyle BackColor="#E1E1DD" BorderColor="#D7D7D7" 
                            ForeColor="#BEBEBE">
                        </ButtonDisabledStyle>
                    </ButtonsAppearance>
                </igtxt:WebTextEdit>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="text-align: right">
                Correo Electrónico:</td>
            <td style="text-align: left">
                <igtxt:WebTextEdit ID="Tbx_eMail" runat="server" BorderColor="#9FB672" 
                    BorderStyle="Solid" BorderWidth="1px" CellSpacing="1" 
                    UseBrowserDefaults="False" Width="221px">
                    <ButtonsAppearance CustomButtonDefaultTriangleImages="Arrow">
                        <ButtonPressedStyle BackColor="#99B079" BorderColor="#75805E">
                        </ButtonPressedStyle>
                        <ButtonStyle BackColor="#829762" BorderColor="#858C6C" BorderStyle="Solid" 
                            BorderWidth="1px" ForeColor="White" Width="13px">
                        </ButtonStyle>
                        <ButtonHoverStyle BackColor="#BCC794" BorderColor="#8C9762">
                        </ButtonHoverStyle>
                        <ButtonDisabledStyle BackColor="#E1E1DD" BorderColor="#D7D7D7" 
                            ForeColor="#BEBEBE">
                        </ButtonDisabledStyle>
                    </ButtonsAppearance>
                </igtxt:WebTextEdit>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="text-align: right">
                &nbsp;</td>
            <td style="text-align: left">
                                            <asp:Label ID="LError" runat="server" Font-Size="Smaller" Font-Names="Arial Narrow"
                                                ForeColor="Red"></asp:Label><asp:Label ID="LCorrecto" runat="server" Font-Size="Smaller"
                                                    Font-Names="Arial Narrow" ForeColor="Green"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td style="text-align: left">
                <igtxt:WebImageButton ID="WebImageButton1" runat="server" 
                    onclick="WebImageButton1_Click" style="text-align: left" 
                    Text="Solicitar Contraseña" UseBrowserDefaults="False">
                    <RoundedCorners DisabledImageUrl="ig_butXP5o.gif" 
                        FocusImageUrl="ig_butXP3o.gif" HoverImageUrl="ig_butXP2o.gif" 
                        ImageUrl="ig_butXP1o.gif" MaxHeight="80" MaxWidth="400" 
                        PressedImageUrl="ig_butXP4o.gif" RenderingType="FileImages" />
                </igtxt:WebImageButton>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>

</asp:Content>

<asp:Content ID="Content2" runat="server" contentplaceholderid="head">

    <style type="text/css">
        #dnn_ctr_SendPassword_lblHelp
        {
            text-align: left;
        }
        .style1
        {
            font-weight: bold;
        }
    </style>

</asp:Content>


