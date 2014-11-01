<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics2.WebUI.WebCombo.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebCombo" TagPrefix="igcmbo" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register TagPrefix="uc1" TagName="WC_Menu" Src="WC_Menu.ascx" %>
<%@ Page language="c#" CodeFile="WF_TurnosE.aspx.cs" AutoEventWireup="True" Inherits="WF_TurnosE" %>

<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Edición de Turno</title>
    <script id="igClientScript" type="text/javascript">
<!--

// -->
</script>

</head>
<body style="font-size: 8px; font-family: tahoma; text-align: center; margin: 0px; ">

    <form id="form1" runat="server">
    <div style="font-size: xx-small; vertical-align: top; text-align: left">
                    <igmisc:WebPanel ID="WebPanel2" runat="server" EnableAppStyling="True" Font-Size="XX-Small"
                        Height="137px" StyleSetName="Caribbean" Width="328px">
                        <Header Text="Datos generales">
                        </Header>
                        <Template>
                            <table style="font-size: x-small; width: 100%">
                                <tr>
                                    <td>
                                        Nombre del Turno:</td>
                                    <td>
                                                    <igtxt:WebTextEdit ID="txtNomTurno" runat="server" Font-Size="XX-Small">
                                                    </igtxt:WebTextEdit>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:CheckBox ID="CheckBox1" runat="server" Text="Deberá calcular asistencia" /></td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <igmisc:WebGroupBox ID="WebGroupBox1" runat="server" EnableAppStyling="False" StyleSetName="Caribbean"
                                            Text="¿Cómo se calculara la comida?" Width="283px">
                                            <Template>
                                                <table style="font-size: x-small; width: 100%; height: 100%">
                                                    <tr>
                                                        <td>
                                                            <asp:RadioButton ID="RadioButton1" runat="server" Text="Por Horario" Width="97px" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="RadioButton2" runat="server" Text="Por Tiempo" Width="95px" /></td>
                                                        <td style="width: 59px">
                                                            <asp:Label ID="Label12" runat="server" Font-Size="XX-Small" Text="hrs."></asp:Label><igtxt:WebDateTimeEdit ID="WebDateTimeEdit1" runat="server" 
                                                        DisplayModeFormat="H:mm" EditModeFormat="H:mm" PromptChar=" " Width="35px" 
                                                        MinimumNumberOfValidFields="1" Font-Size="XX-Small">
                                                            </igtxt:WebDateTimeEdit>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </Template>
                                        </igmisc:WebGroupBox>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 43px">
                                    </td>
                                    <td style="height: 43px">
                                    </td>
                                    <td style="height: 43px">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </Template>
                    </igmisc:WebPanel>
        <igmisc:WebPanel ID="WebPanel1" runat="server">
        </igmisc:WebPanel>
        &nbsp;

    
    </div>
    </form>
</body>
</html>
