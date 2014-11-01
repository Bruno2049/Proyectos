
<%@ Register TagPrefix="uc1" TagName="WC_Menu" Src="WC_Menu.ascx" %>
<%@ Page language="c#"  CodeFile="WF_ImportaAccesos.aspx.cs" AutoEventWireup="True" Inherits="WF_ImportaAccesos" %>

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
                        <strong>
                            <asp:Label ID="Lbl_ImportarAccesosTitulo" runat="server" Text="Importación de accesos"></asp:Label><br />
                        </strong><asp:Label ID="Lbl_ImportarAccesosSubtitulo" runat="server" Text="El formato que se usará para realizar la importación será:"></asp:Label>
                        <ul>
                            <li>
                                <asp:Label ID="Lbl_ImportarAccesosDescripcion1" runat="server" Text="9 digitos para el número de empleado."></asp:Label></li>
                            <li>
                                <asp:Label ID="Lbl_ImportarAccesosDescripcion2" runat="server" Text="10 dígitos para la fecha."></asp:Label></li>
                            <li>
                                <asp:Label ID="Lbl_ImportarAccesosDescripcion3" runat="server" Text="8 dígitos para la hora."></asp:Label></li>
                            <li>
                                <asp:Label ID="Lbl_ImportarAccesosDescripcion4" runat="server" Text="los campos deberán estar separados por |"></asp:Label></li>
                        </ul>
                        <p>
                            <asp:Label ID="Lbl_ImportarAccesosEjemplo" runat="server" Text="Ej. Empleado = 125, fecha = 25/03/2009,&nbsp; Hora = 15:31:18"></asp:Label></p>
                        <p>
                            <asp:Label ID="Lbl_ImportarAccesosLinea" runat="server" Text="000000125|2009/03/25|15:31:18"></asp:Label></p>
                    </td>
				</TR>
				<TR>
					<TD align="center" style="height: 30px">
                        <asp:Label ID="Lbl_ImportarAccesosInstrucciones1" runat="server" Text="1. Seleccione el archivo de accesos que desea importar"></asp:Label>
                        </TD>
                    <td align="center" style="height: 30px">
                        <asp:FileUpload ID="FileUpload1" runat="server" Width="251px" /></td>
				</TR>
                <tr>
                    <td align="center" style="height: 30px">
                        <asp:Label ID="Lbl_ImportarAccesosInstrucciones2" runat="server" Text="2. Seleccione la terminal a la que pertenecen los accesos"></asp:Label></td>
                    <td align="center" style="height: 30px">
                        <igcmbo:webcombo id="WC_Terminales" runat="server" backcolor="White" bordercolor="Silver"
                            borderstyle="Solid" borderwidth="1px" forecolor="Black" selbackcolor="DarkBlue"
                            selforecolor="White" version="4.00"><Columns>
<igtbl:UltraGridColumn HeaderText="Column0">
<Header Caption="Column0"></Header>
</igtbl:UltraGridColumn>
</Columns>

<ExpandEffects ShadowColor="LightGray"></ExpandEffects>

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
</igcmbo:webcombo>
                    </td>
                </tr>
				<TR>
					<TD style="HEIGHT: 0%" align="center">
                        <asp:Label ID="Lbl_ImportarAccesosInstrucciones3" runat="server" Text="3. De click en importar"></asp:Label></TD>
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