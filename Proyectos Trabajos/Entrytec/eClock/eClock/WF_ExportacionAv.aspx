
<%@ Register TagPrefix="uc1" TagName="WC_Menu" Src="WC_Menu.ascx" %>
<%@ Page language="c#"  CodeFile="WF_ExportacionAv.aspx.cs" AutoEventWireup="True" Inherits="WF_ExportacionAv" %>

<%@ Register Assembly="Infragistics2.WebUI.WebDateChooser.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>

<%@ Register Assembly="Infragistics2.WebUI.WebCombo.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebCombo" TagPrefix="igcmbo" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>eClock</title>
    <script id="igClientScript" type="text/javascript">
<!--

<%-- 

function VerTerminales(){
	window.showModalDialog('WF_TerminalesGrid.aspx', '', 'dialogWidth:330px;dialogHeight:450px');
}
--%>
var abrirTerminalesGrid = false;
function btnImportar_MouseDown(oButton, oEvent){
    if (abrirTerminalesGrid)
        VerTerminales();
	//Add code to handle your event here.
}
// -->
</script>
</head>
<body style="font-size: 11px; font-family: tahoma; text-align: center; margin: 0px; ">

    <form id="form1" runat="server">
    <div>
        <table style="height: 400px; font-size: 10pt; font-family: Tahoma, Arial; width: 538px;">


				<TR>
                    <td colspan="2" style="height: 0%">
                        <strong>Exportacion Avanzada<br />
                        </strong>&nbsp; El formato en el que se exportaran los registros será:
                        &nbsp;
                        <ul>
                            <li>Los nombres de los campos iran la principio del archivo</li>
                            <li>9 digitos para el número de empleado. </li>
                            <li>10 dígitos para la fecha.</li>
                            <li>8 dígitos para la hora.</li>
                            <li>los campos estaran separados por un caracter configurable &nbsp;[Ej, \ , | , ; ,
                                - , etc]</li>
                        </ul>
                        <p>
                            Ej. Empleado = 125, fecha = 25/03/2009,&nbsp; Hora = 15:31:18</p>
                        <p>
                            000000125|2009/03/25|15:31:18</p>
                    </td>
				</TR>
            <tr>
                <td align="left" style="width: 572px; height: 21px" valign="top">
                        1.
                        Seleccione el periodo a exportar</td>
                <td align="left" style="height: 21px; width: 127px;">
                    de:<igsch:WebDateChooser ID="cmbFechaInicio" runat="server" Font-Size="XX-Small"
                        Height="14px" Width="150px">
                    </igsch:WebDateChooser>
                    a:
                    <igsch:WebDateChooser ID="cmbFechaFinal" runat="server" Font-Size="XX-Small" Height="13px"
                        Width="150px">
                    </igsch:WebDateChooser>
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 572px; height: 1px" valign="top">
                        2. Escribe el caracter que&nbsp; usaras como separador</td>
                <td align="left" style="height: 1px; width: 127px;">
                    <igtxt:WebTextEdit ID="txtSeparador" runat="server" Font-Size="X-Small" Height="22px"
                        Text="|" Width="94px">
                    </igtxt:WebTextEdit>
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 572px; height: 3px" valign="top">
                    3. Selecciona que tipo de Informacion desas exportar</td>
                <td align="left" style="height: 3px; width: 127px;">
                    <asp:RadioButtonList ID="rbAccesos" runat="server" Font-Size="X-Small" RepeatDirection="Horizontal"
                        Width="133px">
                        <asp:ListItem Selected="True">Accesos</asp:ListItem>
                        <asp:ListItem>Asistencias</asp:ListItem>
                    </asp:RadioButtonList></td>
            </tr>
				<TR>
					<TD style="HEIGHT: 0%; width: 572px;" align="left" valign="top">
                        4. De click en Exportar</TD>
                    <td align="right" style="height: 0%; width: 127px;">
                        &nbsp;<igtxt:webimagebutton id="btnImportar" runat="server" Height="22px" UseBrowserDefaults="False"
							Text="Exportar" Width="100px" ImageTextSpacing="4" OnClick="btnImportar_Click">
							<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
							<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
								RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
							<Appearance>
								<Image Url="./Imagenes/New.png" Height="16px" Width="16px"></Image>
                                <ButtonStyle Cursor="Default">
                                </ButtonStyle>
							</Appearance>
                            <ClientSideEvents MouseDown="btnImportar_MouseDown" />
						</igtxt:webimagebutton>
                    </td>
				</TR>
            <tr>
                <td align="center" colspan="2" style="height: 30px" valign="top">
                    <asp:label id="LCorrecto" runat="server" Font-Size="Smaller" Font-Names="Arial Narrow" ForeColor="Green"></asp:label><asp:label id="LError" runat="server" Font-Size="Smaller" Font-Names="Arial Narrow" ForeColor="Red"></asp:label></td>
            </tr>
            <tr>
                <td align="center" style="width: 572px">
                </td>
                <td align="center" style="width: 127px">
                </td>
            </tr>

        </table>
    
    </div>
    </form>
</body>
</html>