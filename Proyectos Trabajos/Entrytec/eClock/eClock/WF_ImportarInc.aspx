<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WF_ImportarInc.aspx.cs" Inherits="WF_ImportarInc" %>


<%@ Register TagPrefix="uc1" TagName="WC_Menu" Src="WC_Menu.ascx" %>

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
        <table style="height: 400px; font-size: 10pt; font-family: Tahoma, Arial;" width="710">


				<TR>
                    <td colspan="2" style="height: 0%">
                        <strong>Importación de incidencias<br />
                        </strong>&nbsp;&nbsp; El formato que se usará para realizar la importación será:
                        &nbsp;
                        <ul>
                            <li>9 digitos para el número de empleado.</li>
                            <li>10 dígitos para la fecha.</li>
                            <li>Hasta 2 caracteres para la abreviatura (solo informativo).</li>
                            <li>Hasta 256 caracteres para el tipo de incidencia.</li>
                            <li>Hasta 256 caracteres para la descripcion de la incidencia</li>
                        </ul>
                        <p>
                            Ej. Empleado = 125, fecha = 25/03/2009,&nbsp; Abreviatura=PG, Tipo de 
                            incidencia = Permiso sin goce, Descripcion = Tramites personales</p>
                        <p>
                            000000125|2009/03/25|PG|Permiso sin goce|Tramites personales</p>
                        <p>
                            Nota: Tambien es aceptado el formato CSV de Holiday System</p>
                    </td>
				</TR>
				<TR>
					<TD align="center" style="height: 30px">
                        1.
                        Seleccione el archivo de accesos que desea importar &nbsp;</TD>
                    <td align="center" style="height: 30px">
                        <asp:FileUpload ID="FileUpload1" runat="server" Width="251px" /></td>
				</TR>
				<TR>
					<TD style="HEIGHT: 0%" align="center">&nbsp;&nbsp;&nbsp; &nbsp; &nbsp; 2. De click en importar</TD>
                    <td align="center" style="height: 0%">
						<igtxt:webimagebutton id="btnImportar" runat="server" Height="22px" UseBrowserDefaults="False"
							Text="Importar" Width="100px" ImageTextSpacing="4" OnClick="btnImportar_Click">
							<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
							<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
								RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
							<Appearance>
								<Style Cursor="Default">
								</Style>
								<Image Url="./Imagenes/New.png" Height="16px" Width="16px"></Image>
							</Appearance>
                            <ClientSideEvents MouseDown="btnImportar_MouseDown" />
						</igtxt:webimagebutton>
                    </td>
				</TR>
            <tr>
                <td align="center" colspan="2" style="height: 30px">
                    <asp:label id="LCorrecto" runat="server" Font-Size="Smaller" Font-Names="Arial Narrow" ForeColor="Green"></asp:label><asp:label id="LError" runat="server" Font-Size="Smaller" Font-Names="Arial Narrow" ForeColor="Red"></asp:label></td>
            </tr>
            <tr>
                <td align="center">
                </td>
                <td align="center">
                </td>
            </tr>

        </table>
    
    </div>
    </form>
</body>
</html>