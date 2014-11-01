<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WF_TurnoImportaAsignacion.aspx.cs" Inherits="WF_TurnoImportaAsignacion" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.WebUI.WebDataInput" tagprefix="igtxt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Listado de campos</title>
    <style type="text/css">
html, body, #wrapper,#Tabla {
	    border-style: none;
            border-color: inherit;
            border-width: medium;
            height:100%;
	        width:100%;
	        margin: 0;
	        padding: 0;
	        text-align: center;
            font-family: "Segoe UI";
            font-size: small;
        }

</style>
</head>
<body>
    <form id="Tabla" runat="server">

        <table> 
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="Lbl_Importacion" runat="server" Text="
                        &lt;strong&gt;Importación de asignación de turnos&lt;br /&gt;
                        &lt;/strong&gt;&amp;nbsp;&amp;nbsp; El formato que se usará para realizar la importación será:
                        &amp;nbsp;
                        &lt;ul&gt;
                            &lt;li&gt;9 dígitos para el número de empleado, se debe agregar ceros a la izquierda para completar los 9 dígitos.&lt;/li&gt;
                            &lt;li&gt;Nombre del turno, verificando que este exactamente escrito como se encuentra 
                                dado de alta en el sistema.&lt;/li&gt;
                            &lt;li&gt;Fecha de Inicio de asignación DD/MM/YYYY.&lt;/li&gt;
                            &lt;li&gt;Fecha de Fin de asignación DD/MM/YYYY.&lt;/li&gt;
                            &lt;li&gt;Los campos deberán estar separados por coma (&lt;strong&gt;,&lt;/strong&gt;). Se puede usar MS Excel y guardar el 
                                archivo como CSV.&lt;/li&gt;
                            &lt;li&gt;Se usará una línea por registro.&lt;/li&gt;
                        &lt;/ul&gt;
                        &lt;p&gt;
                            Ejemplo: 
                            &lt;p&gt;
                            Para el empleado número 125 se tienen los datos siguientes: Empleado = 125, Turno = Matutino, Fecha Inicio = 25/03/2009, Fecha Final = 
                            30/03/2009&lt;/p&gt;
                        &lt;p&gt;
                            Se debe hacer de este modo:
                            &lt;p&gt;
                            000000125,Matutino,25/03/2009,30/03/2009&lt;/p&gt;">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:label id="Lbl_Correcto" runat="server" Font-Size="Small" 
                        Font-Names="Segoe UI" ForeColor="Green"></asp:label>
                    <asp:label id="Lbl_Error" runat="server" Font-Size="Small" 
                        Font-Names="Segoe UI" ForeColor="Red"></asp:label></td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td rowspan="2">
                                <asp:Label ID="Lbl_Importacion2" runat="server" Text="
                        1. Seleccione el archivo de asignación de turnos que desea importar &amp;nbsp;&lt;br /&gt;
                                2. De click en importar
                                "></asp:Label>
                                </td>
                            <td>
                        <asp:FileUpload ID="Fup_Importar" runat="server" Width="251px" />
                            </td>
                        </tr>
                        <tr>
                            <td>
						<igtxt:webimagebutton id="WIBtn_Importar" runat="server" Height="22px" UseBrowserDefaults="False"
							Text="Importar" Width="100px" ImageTextSpacing="4" OnClick="WIBtn_Importar_Click">
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
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            
        </table>
    </form>
</body>
</html>
