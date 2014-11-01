<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WF_IncidenciasNueva.aspx.cs" Inherits="WF_IncidenciasNueva" %>

<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>

<%@ Register Assembly="Infragistics2.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.Web.UI.LayoutControls" tagprefix="ig" %>

<%@ Register src="WUC__IncidenciaEd.ascx" tagname="WUC__IncidenciaEd" tagprefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <style type="text/css">
        .style1
        {        	
        	font-size: 9pt;
            font-family: Arial; 
        }

        .style5
        {
            width: 676px;
            text-align: center;
        }
        .style6
        {
            font-size: 9pt;
            font-family: Arial;
            }
    </style>
    <script language="javascript" type="text/javascript">
        function OnPageLoad() {
            CambiaSeleccion('1');
        }

        function CambiaSeleccion(Opcion) {
            var lHora1 = document.getElementById('Pnl_Incdencia_Gbx_Accesos_Lbl_Hora1');
            var lHora2 = document.getElementById('Pnl_Incdencia_Gbx_Accesos_Lbl_Hora2');
            switch (Opcion) {
                case '1':
                    lHora1.innerHTML = 'Hora de salida';
                    lHora2.innerHTML = 'Tiempo a justificar';
                    break;
                case '2':
                    lHora1.innerHTML = 'Hora de entrada';
                    lHora2.innerHTML = 'Tiempo justificado';
                    break;
                case '3':
                    lHora1.innerHTML = 'Hora de salida';
                    lHora2.innerHTML = 'Hora de regreso';
                    break;
            }

        } 

        function Rbl_TipoPermiso_OnClick(ctlOpcion) {

            CambiaSeleccion(ctlOpcion.value);
        }        

</script>
</head>
<body >
    <form id="form1" runat="server">
        <table style="width: 100%; height: 331px">
            <tr>
                <td>
                    &nbsp;</td>
                <td class="style6">
                    &nbsp;<igmisc:WebPanel 
                        ID="Pnl_Incdencia" runat="server"
                        
                         Width="667px" EnableAppStyling="True" StyleSetName="Caribbean">

                        <Header TextAlignment="Left" Text="Nueva Incidencia">
                        </Header>
                        <Template>
                            <table style="width: 645px; height: 100%" id="Table2" cellspacing="1" cellpadding="1"
                                align="center" border="0">
                                <tbody>
                                    <tr>
                                        <td align="left" class="style6">
                                            Tipo Incidencia</td>
                                        <td class="style1">
                                            <asp:Label ID="Lbl_TipoIncidencia" runat="server" Text="Label"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="style6">
                                            Personas y Fechas</td>
                                        <td align="left" class="style1">
                                            <asp:Label ID="Lbl_PerFechas" runat="server" Text="Label" Font-Size="XX-Small"></asp:Label>
                                        </td>
                                    </tr>
                                                                        <tr>
                                        <td align="left" class="style1" colspan="2">
                                            <uc1:WUC__IncidenciaEd ID="CamposIncidencia" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="style6">
                                            <asp:Label ID="Lbl_Comentario" runat="server" Text="Comentario"></asp:Label>
                                        </td>
                                        <td style="width: 250px; height: 12px" align="left">
                                            <font face="Arial">
                                                <igtxt:WebTextEdit ID="Tbx_Comentario" runat="server" 
                                                HorizontalAlign="Left" Width="455px" MaxLength="255">
                                                </igtxt:WebTextEdit>
                                            </font>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="style1" colspan="2" style="text-align: center">
                                            <igmisc:WebGroupBox ID="Gbx_Accesos" runat="server" EnableAppStyling="True" 
                                                StyleSetName="Caribbean" Text="Permiso de accesos" Visible="False" 
                                                Width="444px">
                                                <Template>
                                                    <table class="style6" style="width:100%;">
                                                        <tr>
                                                            <td colspan="4">
                                                                <asp:RadioButtonList ID="Rbl_TipoPermiso" runat="server" class="style6" 
                                                                    RepeatDirection="Horizontal" Width="404px" AutoPostBack="True" 
                                                                    onselectedindexchanged="Rbl_TipoPermiso_SelectedIndexChanged">
                                                                    <asp:ListItem Value="1" Selected="True">Justificar Salida</asp:ListItem>
                                                                    <asp:ListItem Value="2">Justificar Entrada</asp:ListItem>
                                                                    <asp:ListItem Value="3">Justificar Intervalo</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Lbl_Hora1" runat="server" Text="Hora de inicio"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <igtxt:WebDateTimeEdit ID="Tbx_Hora1" runat="server" 
                                                                    BorderColor="#7F9DB9" BorderStyle="Solid" BorderWidth="1px" 
                                                                    CellSpacing="1" DisplayModeFormat="H:mm:ss" UseBrowserDefaults="False" 
                                                                    Width="81px" EditModeFormat="H:mm:ss">
                                                                    <SpinButtons DefaultTriangleImages="ArrowSmall" Width="15px" />
                                                                    <ButtonsAppearance CustomButtonDefaultTriangleImages="Arrow">
                                                                        <ButtonStyle BackColor="#C5D5FC" BorderColor="#ABC1F4" BorderStyle="Solid" 
                                                                            BorderWidth="1px" ForeColor="#506080" Width="13px">
                                                                        </ButtonStyle>
                                                                        <ButtonHoverStyle BackColor="#DCEDFD" BorderColor="#8C9762">
                                                                        </ButtonHoverStyle>
                                                                        <ButtonPressedStyle BackColor="#83A6F4" BorderColor="#75805E">
                                                                        </ButtonPressedStyle>
                                                                        <ButtonDisabledStyle BackColor="#E1E1DD" BorderColor="#D7D7D7" 
                                                                            ForeColor="#BEBEBE">
                                                                        </ButtonDisabledStyle>
                                                                    </ButtonsAppearance>
                                                                </igtxt:WebDateTimeEdit>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Lbl_Hora2" runat="server" Text="Hora de Fin"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <igtxt:WebDateTimeEdit ID="Tbx_Hora2" runat="server" 
                                                                    BorderColor="#7F9DB9" BorderStyle="Solid" BorderWidth="1px" 
                                                                    CellSpacing="1" DisplayModeFormat="H:mm:ss" UseBrowserDefaults="False" 
                                                                    Width="81px" EditModeFormat="H:mm:ss">
                                                                    <SpinButtons DefaultTriangleImages="ArrowSmall" Width="15px" />
                                                                    <ButtonsAppearance CustomButtonDefaultTriangleImages="Arrow">
                                                                        <ButtonStyle BackColor="#C5D5FC" BorderColor="#ABC1F4" BorderStyle="Solid" 
                                                                            BorderWidth="1px" ForeColor="#506080" Width="13px">
                                                                        </ButtonStyle>
                                                                        <ButtonHoverStyle BackColor="#DCEDFD" BorderColor="#8C9762">
                                                                        </ButtonHoverStyle>
                                                                        <ButtonPressedStyle BackColor="#83A6F4" BorderColor="#75805E">
                                                                        </ButtonPressedStyle>
                                                                        <ButtonDisabledStyle BackColor="#E1E1DD" BorderColor="#D7D7D7" 
                                                                            ForeColor="#BEBEBE">
                                                                        </ButtonDisabledStyle>
                                                                    </ButtonsAppearance>
                                                                </igtxt:WebDateTimeEdit>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4">
                                                                <asp:Label ID="Lbl_Hora3" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </Template>
                                            </igmisc:WebGroupBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td align="left" class="style1" colspan="2">
                                            <asp:Label ID="Lbl_Html" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td align="left" class="style1" colspan="2">
                                            <asp:Label ID="LCorrecto" runat="server" ForeColor="#009933"></asp:Label>
                                            <asp:Label ID="LError" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </Template>
                    </igmisc:WebPanel>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td class="style6">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center">
                    &nbsp;</td>
                <td align="center" class="style5">
                    &nbsp;<table style="width:100%;">
                        <tr>
                            <td style="text-align: center">
                                <asp:Button ID="Btn_Regresar" runat="server" onclick="Btn_Regresar_Click" 
                                    Text="Regresar" />
                            </td>
                            <td>
                                &nbsp;</td>
                            <td style="text-align: center">
                                <asp:Button ID="Btn_Guardar" runat="server" onclick="Btn_Guardar_Click" 
                                    Text="Guardar" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td align="center">
                    &nbsp;</td>
            </tr>
        </table>

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

    </form>
</body>
</html>
