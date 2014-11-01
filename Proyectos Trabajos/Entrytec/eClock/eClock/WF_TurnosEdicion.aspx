<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="WF_TurnosEdicion.aspx.cs" Inherits="WF_TurnosEdicion" %>

<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics2.WebUI.WebCombo.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.WebUI.WebCombo" tagprefix="igcmbo" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.WebUI.UltraWebGrid" tagprefix="igtbl" %>
<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.WebUI.Misc" tagprefix="igmisc" %>


 

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>eClock</title>
     <script type="text/javascript" src="JS_TurnosEdicion.js"></script> 
    <script id="igClientScript" type="text/javascript">
<!--

function BlurText(oEdit, text, oEvent){
	var txt = document.getElementById(oEvent.srcElement.id);
    var txtID = String(txt.id);
    var txtEd = document.getElementById(txtID.substring(5)+"_p");
    var txtHdn = document.getElementById(txtID.substring(6));
    txt.value = text;
    txtEd.value = text;
    txtHdn.value = text;
	alert("Text: " + text + "     Txt.Value: " + txt.value + "     Txt.Edit: " + txtEd.value + "     Txt.Hdn: " + txtHdn.value);
	txt.value = text;
}
	




// -->
</script>

    <style type="text/css">
        .Dias
        {
            text-align: center;
            width: 122px;
            height: 24px;
        }        
        .CheckBox
        {
            text-align: center;
            width: 122px;
            height: 24px;
        }
        .style1
        {
            width: 17px;
        }
        .style2
        {
            width: 13px;
        }
    </style>

</head>
<body style="font-size: 8px; font-family: tahoma; text-align: center; margin: 0px; ">

    <form id="form1" runat="server">
<asp:HiddenField ID="hdnGenAsist" runat="server" />
<asp:HiddenField ID="hdnTipoAsist" runat="server" />
<asp:HiddenField ID="hdnMismoHorario" runat="server" />
<asp:HiddenField ID="hdnRestAcceso" runat="server" />
<asp:HiddenField ID="hdnComida" runat="server" Value="0"  />
<asp:HiddenField ID="hdnLun" runat="server" Value="1" />                                                                                                                  
<asp:HiddenField ID="hdnJue" runat="server" Value="1" />
<asp:HiddenField ID="hdnMie" runat="server" Value="1" />
<asp:HiddenField ID="hdnVie" runat="server" Value="1" />
<asp:HiddenField ID="hdnDom" runat="server" Value="1" />
<asp:HiddenField ID="hdnTurno" runat="server" />
<asp:HiddenField ID="hdnSab" runat="server" Value="1" />
<asp:HiddenField ID="hdnMar" runat="server" Value="1" />        
                                      
    <div style="font-size: xx-small; vertical-align: top; text-align: left">
        <table style="width: 417px; height: 400px;">
            <tr>
                <td style="vertical-align: top; text-align: left">
                    <igmisc:WebPanel ID="WebPanel3" runat="server" Height="389px"
            EnableAppStyling="True" StyleSetName="Caribbean" ExpandEffect="None" 
                        OnExpandedStateChanged="WebPanel3_ExpandedStateChanged" 
                        OnExpandedStateChanging="WebPanel3_ExpandedStateChanging" Width="205px">
            <Header Text="Detalles del turno" >
            </Header>
            <Template>
                                        <table style="width: 175px; font-size: x-small;">
                                            <tr>
                                                <td align="left" valign="middle" style="width: 284px; font-size: xx-small;">
                                                    <asp:Label ID="Label1" runat="server" Text="Nombre del Turno:" Font-Size="XX-Small"></asp:Label>
                                                    <br />
                                                    <igtxt:WebTextEdit ID="txtNomTurno" runat="server" Font-Size="XX-Small" 
                                                        Height="21px" Width="159px">
                                                    </igtxt:WebTextEdit>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="width: 284px; height: 13px; font-size: xx-small;">
                                                    <input ID="chkGenera" checked="CHECKED" onclick="ActualizaTurnoFEnd()" 
                                                        type="checkbox" /><asp:Label ID="Label2" runat="server" 
                                                        Text="El Turno Genera Asistencia" Font-Size="XX-Small"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                        <table ID="tblTipoAsist" style="width: 175px; height: 1px;">
                                            <tr>
                                                <td align="left" style="width: 284px; height: 1px; font-size: xx-small;">
                                                    <input ID="optAsistHora" checked="checked" name="TipoAsistencia" 
                                                        onclick="ActualizaTurnoFEnd();" type="radio" value="1" />
                                                    <asp:Label ID="Label8" runat="server" Text="Por Horario" Font-Size="XX-Small"></asp:Label>
                                                    <br />
                                                    <input ID="optAsistJor" name="TipoAsistencia" onclick="ActualizaTurnoFEnd();" 
                                                        type="radio" value="2" />
                                                    <asp:Label ID="Label9" runat="server" Text="Por Jornada de trabajo" 
                                                        Font-Size="XX-Small"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                        <table ID="tblBloque" style="width: 175px">
                                            <tr>
                                                <td align="left" style="padding-left: 32px; width: 276px; height: 13px;">
                                                    <asp:Label ID="Label26" runat="server" Text="Bloque" Font-Size="XX-Small"></asp:Label>
                                                    <igtxt:WebNumericEdit ID="txtBloque" runat="server" DataMode="Int" MaxValue="120" 
                                                        MinValue="1" ValueText="60" Width="30px" Font-Size="XX-Small">
                                                    </igtxt:WebNumericEdit>
                                                    <asp:Label ID="Label31" runat="server" Text="min." Font-Size="XX-Small"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                        <table ID="tblTolerancia" style="width: 175px">
                                            <tr>
                                                <td align="left" style="padding-left: 32px; width: 276px; height: 13px;">
                                                    <asp:Label ID="Label11" runat="server" Text="Tolerancia" Font-Size="XX-Small"></asp:Label>
                                                    <igtxt:WebNumericEdit ID="txtTol" runat="server" DataMode="Int" MaxValue="120" 
                                                        MinValue="1" ValueText="1" Width="30px" Font-Size="XX-Small">
                                                    </igtxt:WebNumericEdit>
                                                    <asp:Label ID="Label30" runat="server" Text="min." Font-Size="XX-Small"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                        <table ID="tblToleranciaCom" style="width: 175px">
                                            <tr>
                                                <td align="left" style="padding-left: 32px; width: 276px; height: 13px;">
                                                    <asp:Label ID="Label18" runat="server" Text="Tol. Comida" Font-Size="XX-Small"></asp:Label>
                                                    <igtxt:WebNumericEdit ID="txtTolComida" runat="server" DataMode="Int" MaxValue="120" 
                                                        MinValue="1" ValueText="15" Width="30px" Font-Size="XX-Small">
                                                    </igtxt:WebNumericEdit>
                                                    <asp:Label ID="Label32" runat="server" Text="min." Font-Size="XX-Small"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                        <table ID="tblTiempoEx" style="width: 175px">
                                            <tr>
                                                <td align="left" style="width: 286px; height: 22px;">
                                                    <asp:CheckBox ID="chkTiempoEx" runat="server" Text="Permite Tiempo Extra" Font-Size="XX-Small" /></td>
                                            </tr>
                                        </table>
                                        <table ID="tblMismoHorario" style="width: 175px; height: 1px;">
                                            <tr>
                                                <td align="left" style="width: 280px; height: 0px; font-size: xx-small;">
                                                    <input ID="chkAsistDias" checked="checked" onclick="ActualizaTurnoFEnd();" 
                                                        type="checkbox" />
                                                    <asp:Label ID="Label3" runat="server" Text="Mismo Horario para cada Día" Font-Size="XX-Small"></asp:Label>

                                                </td>
                                            </tr>
                                        </table>
                                        <table ID="tblRestAcceso" style="width: 175px">
                                            <tr>
                                                <td align="left" style="height: 1px; width: 305px; font-size: xx-small;">
                                                    <input ID="chkRestAcceso" onclick="ActualizaTurnoFEnd();" type="checkbox" />
                                                    <asp:Label ID="Label4" runat="server" Text="Restringir Acceso" Font-Size="XX-Small"></asp:Label>

                                                </td>
                                            </tr>
                                        </table>
                                        <table ID="tblComida" style="width: 175px">
                                            <tr>
                                                <td align="left" style="width: 241px; height: 1px; font-size: xx-small;">
                                                    <asp:Panel ID="Panel1" runat="server" BackColor="Transparent" ForeColor="Black" 
                                                        GroupingText="Horario para Comer" Height="50px" Width="180px" 
                                                        Font-Size="XX-Small">
                                                        <table style="width: 105%;">
                                                            <tr>
                                                                <td>
                                                                    <input ID="optSinComida" checked="true" name="TipoComida" 
                                                            
                onclick="ActualizaTurnoFEnd();" type="radio" value="1" />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label5" runat="server" Font-Size="XX-Small" Text="Sin Comida"></asp:Label>
                                                                </td>
                                                                <td class="style2">
                                                                    &nbsp;</td>
                                                                <td>
                                                                    <input ID="optComidaTiempo" name="TipoComida" onclick="ActualizaTurnoFEnd();" 
                                                            type="radio" value="3" />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label7" runat="server" Font-Size="XX-Small" Text="por Tiempo"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <input ID="optComidaHorario" name="TipoComida" onclick="ActualizaTurnoFEnd();" 
                                                            type="radio" value="2" />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label6" runat="server" Font-Size="XX-Small" Text="Horario Fijo"></asp:Label>
                                                                </td>
                                                                <td class="style2">
                                                                    &nbsp;</td>
                                                                <td>
                                                                    <input ID="optComidaAvanzado" name="TipoComida" onclick="ActualizaTurnoFEnd();" 
                                                            type="radio" value="4" />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label10" runat="server" Font-Size="XX-Small" Text="Avanzado"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        &nbsp;</asp:Panel>

                                                </td>
                                            </tr>
                                        </table>
            </Template>
            <PanelStyle BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial">
                <Padding Bottom="5px" Left="5px" Right="5px" Top="5px" />
                <BorderDetails ColorBottom="0, 45, 150" ColorLeft="158, 190, 245" ColorRight="0, 45, 150"
                    ColorTop="0, 45, 150" />
            </PanelStyle>
        </igmisc:WebPanel>
                </td>
                <td style="width: 4px; height: 357px; font-size: xx-small; vertical-align: top; text-align: left;" align="left" valign="top">

    <igmisc:WebPanel ID="WebPanel1" runat="server" 
        EnableAppStyling="True" StyleSetName="Caribbean">

        <Header Text="Configuraci&#243;n" TextAlignment="Left">
        </Header>
        <Template>
    <table id="tblHorarioDias" border="0" cellpadding="0" cellspacing="0" style="height: 1px">
        <tr>
            <td style="width: 71px" valign="top">
                <table style="width:100%; font-size: xx-small;">
                    <tr>
                        <td class="CheckBox"></td> <td class="Dias">
                            <asp:Label ID="Label13" runat="server" Font-Bold="True" Text="Día" Font-Size="XX-Small"></asp:Label></td>
                    </tr>
                    <tr>
                        <td id="A1" class="CheckBox">
                            <input id="chkAsistLun" type="checkbox" checked="checked" onclick="AplicarTurnoDia(this.id);" /></td> <td id="AA1" class="Dias"><asp:Label 
                                ID="lblLun" runat="server" Text="Lunes" Font-Size="XX-Small"></asp:Label><asp:Label ID="lblGral" runat="server" Text="Horario" Font-Size="XX-Small"></asp:Label>

                        </td>
                    </tr>
                    <tr>
                        <td id="A2" class="CheckBox">
                            <input id="chkAsistMar" type="checkbox" checked="checked" onclick="AplicarTurnoDia(this.id);" /></td> <td id="AA2" class="Dias"><asp:Label ID="Label22" runat="server" Text="Martes" Font-Size="XX-Small"></asp:Label>

                        </td>
                    </tr>
                    <tr>
                        <td id="A3" class="CheckBox">
                            <input id="chkAsistMie" type="checkbox" checked="checked" onclick="AplicarTurnoDia(this.id);" /></td> <td id="AA3" class="Dias"><asp:Label ID="Label21" runat="server" Text="Miércoles" Font-Size="XX-Small"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td id="A4" class="CheckBox">
                            <input id="chkAsistJue" type="checkbox" checked="checked" onclick="AplicarTurnoDia(this.id);" /></td> <td id="AA4" class="Dias"><asp:Label ID="Label20" runat="server" Text="Jueves" Font-Size="XX-Small"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td id="A5" class="CheckBox">
                            <input id="chkAsistVie" type="checkbox" checked="checked" onclick="AplicarTurnoDia(this.id);" /></td> <td id="AA5" class="Dias"><asp:Label ID="Label24" runat="server" Text="Viernes" Font-Size="XX-Small"></asp:Label>

                        </td>
                    </tr>
                    <tr>
                        <td id="A6" class="CheckBox">
                            <input id="chkAsistSab" type="checkbox" checked="checked" onclick="AplicarTurnoDia(this.id);" /></td> <td id="AA6" class="Dias"><asp:Label ID="Label25" runat="server" Text="Sábado" Font-Size="XX-Small"></asp:Label>

                        </td>
                    </tr>
                    <tr>
                        <td id="A7" class="CheckBox">
                            <input id="chkAsistDom" type="checkbox" checked="checked" onclick="AplicarTurnoDia(this.id);" /></td> <td id="AA7" class="Dias"><asp:Label ID="Label23" runat="server" Text="Domingo" Font-Size="XX-Small"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
            <td id="EntMin" style="width: 65px" valign="top">
                <table>
                    <tr>
                        <td class="Dias" >
                            <asp:Label ID="Label27" runat="server" Font-Bold="True" Text="Entrada Mínima" Font-Size="XX-Small"></asp:Label></td>
                    </tr>
                    <tr>
                        <td id="B1" class="Dias">
                            <igtxt:WebDateTimeEdit ID="EntMinLun" runat="server" DisplayModeFormat="HH:mm" Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="1" Font-Size="XX-Small">
                            </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                    <tr>
                        <td id="B2" class="Dias">
                            <igtxt:WebDateTimeEdit ID="EntMinMar" runat="server" DisplayModeFormat="HH:mm" 
                                PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="8" Font-Size="XX-Small">
                            </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                    <tr>
                        <td id="B3" class="Dias">
                            <igtxt:WebDateTimeEdit ID="EntMinMie" runat="server" DisplayModeFormat="HH:mm" 
                                PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="15" Font-Size="XX-Small">
                            </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                    <tr>
                        <td id="B4" class="Dias">
                            <igtxt:WebDateTimeEdit ID="EntMinJue" runat="server" DisplayModeFormat="HH:mm" 
                                PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="22" Font-Size="XX-Small">
                            </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                    <tr>
                        <td id="B5" class="Dias">
                            <igtxt:WebDateTimeEdit ID="EntMinVie" runat="server" DisplayModeFormat="HH:mm" 
                                PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="29" Font-Size="XX-Small">
                            </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                    <tr>
                        <td id="B6" class="Dias">
                            <igtxt:WebDateTimeEdit ID="EntMinSab" runat="server" DisplayModeFormat="HH:mm" 
                                PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="36" Font-Size="XX-Small">
                            </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                    <tr>
                        <td id="B7" class="Dias">
                            <igtxt:WebDateTimeEdit ID="EntMinDom" runat="server" DisplayModeFormat="HH:mm" 
                                PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="43" Font-Size="XX-Small">
                            </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                </table>
            </td>
            <td id="Ent" valign="top" width="55" style="width: 65px">
                <table>
                    <tr>
                        <td  class="Dias">
                            <asp:Label ID="Label28" runat="server" Font-Bold="True" Text="Entrada" Font-Size="XX-Small"></asp:Label></td>
                    </tr>
                    <tr>
                        <td id="C1" class="Dias">
                            <igtxt:WebDateTimeEdit ID="EntLun" runat="server" DisplayModeFormat="HH:mm" 
                                PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="2" Font-Size="XX-Small">
                            </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                    <tr>
                        <td id="C2" class="Dias">
                            <igtxt:WebDateTimeEdit ID="EntMar" runat="server" DisplayModeFormat="HH:mm" 
                                PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="9" Font-Size="XX-Small">
                            </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                    <tr>
                        <td id="C3" class="Dias">
                            <igtxt:WebDateTimeEdit ID="EntMie" runat="server" DisplayModeFormat="HH:mm" 
                                PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="16" Font-Size="XX-Small">
                            </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                    <tr>
                        <td id="C4" class="Dias">
                            <igtxt:WebDateTimeEdit ID="EntJue" runat="server" DisplayModeFormat="HH:mm" 
                                PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="23" Font-Size="XX-Small">
                            </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                    <tr>
                        <td id="C5" class="Dias">
                            <igtxt:WebDateTimeEdit ID="EntVie" runat="server" DisplayModeFormat="HH:mm" 
                                PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="30" Font-Size="XX-Small">
                            </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                    <tr>
                        <td id="C6" class="Dias">
                            <igtxt:WebDateTimeEdit ID="EntSab" runat="server" DisplayModeFormat="HH:mm" 
                                PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="37" Font-Size="XX-Small">
                            </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                    <tr>
                        <td id="C7" class="Dias">
                            <igtxt:WebDateTimeEdit ID="EntDom" runat="server" DisplayModeFormat="HH:mm" 
                                PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="44" Font-Size="XX-Small">
                            </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                </table>
            </td>
            <td id="EntMax" valign="top" width="55" style="width: 65px">
                <table>
                    <tr>
                        <td  class="Dias">
                            <asp:Label ID="Label29" runat="server" Font-Bold="True" Text="Entrada Máxima" Font-Size="XX-Small"></asp:Label></td>
                    </tr>
                    <tr>
                        <td id="D1" class="Dias">
                            <igtxt:WebDateTimeEdit ID="EntMaxLun" runat="server" DisplayModeFormat="HH:mm" 
                                PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="3" Font-Size="XX-Small">
                            </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                    <tr>
                        <td id="D2" class="Dias">
                            <igtxt:WebDateTimeEdit ID="EntMaxMar" runat="server" DisplayModeFormat="HH:mm" 
                                PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="10" Font-Size="XX-Small">
                            </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                    <tr>
                        <td id="D3" class="Dias">
                            <igtxt:WebDateTimeEdit ID="EntMaxMie" runat="server" DisplayModeFormat="HH:mm" 
                                PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="17" Font-Size="XX-Small">
                            </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                    <tr>
                        <td id="D4" class="Dias">
                            <igtxt:WebDateTimeEdit ID="EntMaxJue" runat="server" DisplayModeFormat="HH:mm" 
                                PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="24" Font-Size="XX-Small">
                            </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                    <tr>
                        <td id="D5" class="Dias">
                            <igtxt:WebDateTimeEdit ID="EntMaxVie" runat="server" DisplayModeFormat="HH:mm" 
                                PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="31" Font-Size="XX-Small">
                            </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                    <tr>
                        <td id="D6" class="Dias">
                            <igtxt:WebDateTimeEdit ID="EntMaxSab" runat="server" DisplayModeFormat="HH:mm" 
                                PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="38" Font-Size="XX-Small">
                            </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                    <tr>
                        <td id="D7" class="Dias">
                            <igtxt:WebDateTimeEdit ID="EntMaxDom" runat="server" DisplayModeFormat="HH:mm" 
                                PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="45" Font-Size="XX-Small">
                            </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                </table>
            </td>
            <td id="ComFija" valign="top" width="110" style="width: 130px">
                <table id="HorarioComida">
                    <tr>
                        <td class="Dias" >
                            <asp:Label ID="Label16" runat="server" Font-Bold="True" Text="Salida Comida" Font-Size="XX-Small"></asp:Label></td>
                        <td class="Dias">
                            <asp:Label ID="Label15" runat="server" Font-Bold="True" Text="Regreso Comida" Font-Size="XX-Small"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="Dias" id="E1" >
                            <igtxt:WebDateTimeEdit ID="SalComLun" runat="server" DisplayModeFormat="HH:mm" PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="4" Font-Size="XX-Small">
                            </igtxt:WebDateTimeEdit>
                        </td>
                        <td class="Dias" id="F1">
                            <igtxt:WebDateTimeEdit ID="RegComLun" runat="server" DisplayModeFormat="HH:mm" PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="5" Font-Size="XX-Small">
                            </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                    <tr>
                        <td class="Dias" id="E2" >
                            <igtxt:WebDateTimeEdit ID="SalComMar" runat="server" DisplayModeFormat="HH:mm" PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="11" Font-Size="XX-Small">
                            </igtxt:WebDateTimeEdit>
                        </td>
                        <td class="Dias" id="F2">
                            <igtxt:WebDateTimeEdit ID="RegComMar" runat="server" DisplayModeFormat="HH:mm" PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="12" Font-Size="XX-Small">
                            </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                    <tr>
                        <td class="Dias" id="E3" >
                            <igtxt:WebDateTimeEdit ID="SalComMie" runat="server" DisplayModeFormat="HH:mm" PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="18" Font-Size="XX-Small">
                            </igtxt:WebDateTimeEdit>
                        </td>
                        <td class="Dias" id="F3">
                            <igtxt:WebDateTimeEdit ID="RegComMie" runat="server" DisplayModeFormat="HH:mm" PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="19" Font-Size="XX-Small">
                            </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                    <tr>
                        <td class="Dias" id="E4" >
                            <igtxt:WebDateTimeEdit ID="SalComJue" runat="server" DisplayModeFormat="HH:mm" PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="25" Font-Size="XX-Small">
                            </igtxt:WebDateTimeEdit>
                        </td>
                        <td class="Dias" id="F4">
                            <igtxt:WebDateTimeEdit ID="RegComJue" runat="server" DisplayModeFormat="HH:mm" PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="26" Font-Size="XX-Small">
                            </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                    <tr>
                        <td class="Dias" id="E5" style="width: 55px">
                            <igtxt:WebDateTimeEdit ID="SalComVie" runat="server" DisplayModeFormat="HH:mm" PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="32" Font-Size="XX-Small">
                            </igtxt:WebDateTimeEdit>
                        </td>
                        <td class="Dias" id="F5">
                            <igtxt:WebDateTimeEdit ID="RegComVie" runat="server" DisplayModeFormat="HH:mm" PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="33" Font-Size="XX-Small">
                            </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                    <tr>
                        <td class="Dias" id="E6" style="width: 55px">
                            <igtxt:WebDateTimeEdit ID="SalComSab" runat="server" DisplayModeFormat="HH:mm" PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="39" Font-Size="XX-Small">
                            </igtxt:WebDateTimeEdit>
                        </td>
                        <td class="Dias" id="F6">
                            <igtxt:WebDateTimeEdit ID="RegComSab" runat="server" DisplayModeFormat="HH:mm" PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="40" Font-Size="XX-Small">
                            </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                    <tr>
                        <td class="Dias" id="E7" style="width: 55px">
                            <igtxt:WebDateTimeEdit ID="SalComDom" runat="server" DisplayModeFormat="HH:mm" PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="46" Font-Size="XX-Small">
                            </igtxt:WebDateTimeEdit>
                        </td>
                        <td class="Dias" id="F7">
                            <igtxt:WebDateTimeEdit ID="RegComDom" runat="server" DisplayModeFormat="HH:mm" PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="47" Font-Size="XX-Small">
                            </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                </table>
            </td>
            <td id="TieCom" valign="top" width="55" style="width: 65px">
                <table id="TiempoComida">
                    <tr>
                        <td class="Dias" height="30">
                            <asp:Label ID="Label17" runat="server" Font-Bold="True" Text="Tiempo Comida" Font-Size="XX-Small"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="Dias" id="G1">
                            <igtxt:WebDateTimeEdit ID="TieComLun" runat="server" DisplayModeFormat="H:mm" PromptChar=" " Width="35px" 
                                EditModeFormat="H:mm" MinimumNumberOfValidFields="1" TabIndex="6" Font-Size="XX-Small">
                            </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                    <tr>
                        <td class="Dias" id="G2"><igtxt:WebDateTimeEdit ID="TieComMar" 
                                runat="server" DisplayModeFormat="H:mm" PromptChar=" " Width="35px" 
                                EditModeFormat="H:mm" MinimumNumberOfValidFields="1" TabIndex="13" Font-Size="XX-Small">
                        </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                    <tr>
                        <td class="Dias" id="G3">
                            <igtxt:WebDateTimeEdit ID="TieComMie" runat="server" DisplayModeFormat="H:mm" PromptChar=" " Width="35px" 
                                EditModeFormat="H:mm" MinimumNumberOfValidFields="1" TabIndex="20" Font-Size="XX-Small">
                        </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                    <tr>
                        <td class="Dias" id="G4">
                            <igtxt:WebDateTimeEdit ID="TieComJue" runat="server" DisplayModeFormat="H:mm" PromptChar=" " Width="35px" 
                                EditModeFormat="H:mm" MinimumNumberOfValidFields="1" TabIndex="27" Font-Size="XX-Small">
                        </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                    <tr>
                        <td class="Dias" id="G5">
                            <igtxt:WebDateTimeEdit ID="TieComVie" runat="server" DisplayModeFormat="H:mm" PromptChar=" " Width="35px" 
                                EditModeFormat="H:mm" MinimumNumberOfValidFields="1" TabIndex="34" Font-Size="XX-Small">
                        </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                    <tr>
                        <td class="Dias" id="G6">
                            <igtxt:WebDateTimeEdit ID="TieComSab" runat="server" DisplayModeFormat="H:mm" PromptChar=" " Width="35px" 
                                EditModeFormat="H:mm" MinimumNumberOfValidFields="1" TabIndex="41" Font-Size="XX-Small">
                        </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                    <tr>
                        <td class="Dias" id="G7"><igtxt:WebDateTimeEdit ID="TieComDom" 
                                runat="server" DisplayModeFormat="H:mm" PromptChar=" " Width="35px" 
                                EditModeFormat="H:mm" MinimumNumberOfValidFields="1" TabIndex="48" Font-Size="XX-Small">
                        </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                </table>
            </td>
            <td id="SalMin" style="width: 65px" valign="top">
                <table>
                    <tr>
                        <td id="SalMin0" class="Dias" >
                            <asp:Label ID="Label12" runat="server" Font-Bold="True" Text="Salida Mínima" Font-Size="XX-Small"></asp:Label></td>
                    </tr>
                    <tr>
                        <td id="SalMin1" class="Dias">
                            <igtxt:WebDateTimeEdit ID="SalMinLun" runat="server" DisplayModeFormat="HH:mm" Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="1" Font-Size="XX-Small">
                            </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                    <tr>
                        <td id="SalMin2" class="Dias">
                            <igtxt:WebDateTimeEdit ID="SalMinMar" runat="server" DisplayModeFormat="HH:mm" 
                                PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="8" Font-Size="XX-Small">
                            </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                    <tr>
                        <td id="SalMin3" class="Dias">
                            <igtxt:WebDateTimeEdit ID="SalMinMie" runat="server" DisplayModeFormat="HH:mm" 
                                PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="15" Font-Size="XX-Small">
                            </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                    <tr>
                        <td id="SalMin4" class="Dias">
                            <igtxt:WebDateTimeEdit ID="SalMinJue" runat="server" DisplayModeFormat="HH:mm" 
                                PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="22" Font-Size="XX-Small">
                            </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                    <tr>
                        <td id="SalMin5" class="Dias">
                            <igtxt:WebDateTimeEdit ID="SalMinVie" runat="server" DisplayModeFormat="HH:mm" 
                                PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="29" Font-Size="XX-Small">
                            </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                    <tr>
                        <td id="SalMin6" class="Dias">
                            <igtxt:WebDateTimeEdit ID="SalMinSab" runat="server" DisplayModeFormat="HH:mm" 
                                PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="36" Font-Size="XX-Small">
                            </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                    <tr>
                        <td id="SalMin7" class="Dias">
                            <igtxt:WebDateTimeEdit ID="SalMinDom" runat="server" DisplayModeFormat="HH:mm" 
                                PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="43" Font-Size="XX-Small">
                            </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                </table>
            </td>
            <td id="Sal" valign="top" style="width: 65px">
                <table>
                    <tr>
                        <td class="Dias" >
                            <asp:Label ID="Lbl_Salida" runat="server" Font-Bold="True" Text="Salida" Font-Size="XX-Small"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="Dias" id="H1">
                            <igtxt:WebDateTimeEdit ID="SalLun" runat="server" DisplayModeFormat="HH:mm" PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="7" Font-Size="XX-Small">
                            </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                    <tr>
                        <td class="Dias" id="H2">
                            <igtxt:WebDateTimeEdit ID="SalMar" runat="server" DisplayModeFormat="HH:mm" PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="14" Font-Size="XX-Small">
                            </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                    <tr>
                        <td class="Dias" id="H3">
                            <igtxt:WebDateTimeEdit ID="SalMie" runat="server" DisplayModeFormat="HH:mm" PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="21" Font-Size="XX-Small">
                            </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                    <tr>
                        <td class="Dias" id="H4">
                            <igtxt:WebDateTimeEdit ID="SalJue" runat="server" DisplayModeFormat="HH:mm" PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="28" Font-Size="XX-Small">
                        </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                    <tr>
                        <td class="Dias" id="H5">
                            <igtxt:WebDateTimeEdit ID="SalVie" runat="server" DisplayModeFormat="HH:mm" PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="35" Font-Size="XX-Small">
                            </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                    <tr>
                        <td class="Dias" id="H6">
                            <igtxt:WebDateTimeEdit ID="SalSab" runat="server" DisplayModeFormat="HH:mm" PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="42" Font-Size="XX-Small">
                        </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                    <tr>
                        <td class="Dias" id="H7">
                            <igtxt:WebDateTimeEdit ID="SalDom" runat="server" DisplayModeFormat="HH:mm" PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="49" Font-Size="XX-Small">
                            </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                </table>
            </td>
            <td id="SalMax" style="width: 65px" valign="top">
                <table>
                    <tr>
                        <td id="SalMax0"  class="Dias" >
                            <asp:Label ID="Label14" runat="server" Font-Bold="True" Text="Salida Maxima" Font-Size="XX-Small"></asp:Label></td>
                    </tr>
                    <tr>
                        <td id="SalMax1" class="Dias">
                            <igtxt:WebDateTimeEdit ID="SalMaxLun" runat="server" DisplayModeFormat="HH:mm" Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="1" Font-Size="XX-Small">
                            </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                    <tr>
                        <td id="SalMax2" class="Dias">
                            <igtxt:WebDateTimeEdit ID="SalMaxMar" runat="server" DisplayModeFormat="HH:mm" 
                                PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="8" Font-Size="XX-Small">
                            </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                    <tr>
                        <td id="SalMax3" class="Dias">
                            <igtxt:WebDateTimeEdit ID="SalMaxMie" runat="server" DisplayModeFormat="HH:mm" 
                                PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="15" Font-Size="XX-Small">
                            </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                    <tr>
                        <td id="SalMax4" class="Dias">
                            <igtxt:WebDateTimeEdit ID="SalMaxJue" runat="server" DisplayModeFormat="HH:mm" 
                                PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="22" Font-Size="XX-Small">
                            </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                    <tr>
                        <td id="SalMax5" class="Dias">
                            <igtxt:WebDateTimeEdit ID="SalMaxVie" runat="server" DisplayModeFormat="HH:mm" 
                                PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="29" Font-Size="XX-Small">
                            </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                    <tr>
                        <td id="SalMax6" class="Dias">
                            <igtxt:WebDateTimeEdit ID="SalMaxSab" runat="server" DisplayModeFormat="HH:mm" 
                                PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="36" Font-Size="XX-Small">
                            </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                    <tr>
                        <td id="SalMax7" class="Dias">
                            <igtxt:WebDateTimeEdit ID="SalMaxDom" runat="server" DisplayModeFormat="HH:mm" 
                                PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="43" Font-Size="XX-Small">
                            </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                </table>
            </td>
            <td id="Jornada" valign="top" style="width: 65px">
                <table>
                    <tr>
                        <td class="Dias" >
                            <asp:Label ID="Lbl_Jornada" runat="server" Font-Bold="True" 
                                Font-Size="XX-Small" Text="Jornada"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="Dias" id="Jornada1">
                            <igtxt:WebDateTimeEdit ID="JornadaLun" runat="server" DisplayModeFormat="HH:mm" 
                                PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="7" 
                                Font-Size="XX-Small" Fields="2010-5-30-8-0-0-0">
                            </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                    <tr>
                        <td class="Dias" id="Jornada2">
                            <igtxt:WebDateTimeEdit ID="JornadaMar" runat="server" DisplayModeFormat="HH:mm" 
                                PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="14" 
                                Font-Size="XX-Small" Fields="2010-5-30-8-0-0-0">
                            </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                    <tr>
                        <td class="Dias" id="Jornada3">
                            <igtxt:WebDateTimeEdit ID="JornadaMie" runat="server" DisplayModeFormat="HH:mm" PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="21" Font-Size="XX-Small" Fields="2010-5-30-8-0-0-0">
                            </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                    <tr>
                        <td class="Dias" id="Jornada4">
                            <igtxt:WebDateTimeEdit ID="JornadaJue" runat="server" DisplayModeFormat="HH:mm" PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="28" Font-Size="XX-Small" Fields="2010-5-30-8-0-0-0">
                        </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                    <tr>
                        <td class="Dias" id="Jornada5">
                            <igtxt:WebDateTimeEdit ID="JornadaVie" runat="server" DisplayModeFormat="HH:mm" PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="35" Font-Size="XX-Small" Fields="2010-5-30-8-0-0-0">
                            </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                    <tr>
                        <td class="Dias" id="Jornada6">
                            <igtxt:WebDateTimeEdit ID="JornadaSab" runat="server" DisplayModeFormat="HH:mm" PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="42" Font-Size="XX-Small" Fields="2010-5-30-8-0-0-0">
                        </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                    <tr>
                        <td class="Dias" id="Jornada7">
                            <igtxt:WebDateTimeEdit ID="JornadaDom" runat="server" DisplayModeFormat="HH:mm" PromptChar=" " Width="35px" 
                                EditModeFormat="HH:mm" MinimumNumberOfValidFields="1" TabIndex="49" Font-Size="XX-Small" Fields="2010-5-30-8-0-0-0">
                            </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                </table>
            </td>
            <td valign="top" style="width: 55px">
                <table style="width:100%;">
                    <tr>
                        <td class="CheckBox">
                            <asp:Label ID="Label19" runat="server" Text="Siguente Día" Font-Bold="True" Font-Size="XX-Small"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td id="I1" class="CheckBox">
                            <asp:CheckBox ID="chkSigDiaLun1" runat="server" Font-Size="XX-Small" /></td>
                    </tr>
                    <tr>
                        <td id="I2" class="CheckBox">
                            <asp:CheckBox ID="chkSigDiaMar1" runat="server" Font-Size="XX-Small" /></td>
                    </tr>
                    <tr>
                        <td id="I3" class="CheckBox">
                            <asp:CheckBox ID="chkSigDiaMie1" runat="server" Font-Size="XX-Small" /></td>
                    </tr>
                    <tr>
                        <td id="I4" class="CheckBox">
                            <asp:CheckBox ID="chkSigDiaJue1" runat="server" Font-Size="XX-Small" /></td>
                    </tr>
                    <tr>
                        <td id="I5" class="CheckBox">
                            <asp:CheckBox ID="chkSigDiaVie1" runat="server" Font-Size="XX-Small" /></td>
                    </tr>
                    <tr>
                        <td id="I6" class="CheckBox">
                            <asp:CheckBox ID="chkSigDiaSab1" runat="server" Font-Size="XX-Small" /></td>
                    </tr>
                    <tr>
                        <td id="I7" class="CheckBox">
                            <asp:CheckBox ID="chkSigDiaDom1" runat="server" Font-Size="XX-Small" /></td>
                    </tr>
                </table>
                </td>
        </tr>
    </table>
            <table id="tblDiasGral" style="width: 404px">
                <tr>
                    <td>
                <asp:CheckBox ID="chkAsistGralLun" runat="server" Text="Lunes" Width="65px" 
                    Checked="True" Font-Size="XX-Small" /></td>
                    <td>
                        <asp:CheckBox ID="chkAsistGralMar"
                    runat="server" Text="Martes" Width="65px" Checked="True" Font-Size="XX-Small" /></td>
                    <td>
                        <asp:CheckBox ID="chkAsistGralMie" runat="server" Text="Miércoles" Width="65px" 
                    Checked="True" Font-Size="XX-Small" /></td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="chkAsistGralJue" runat="server" Text="Jueves" Width="65px" 
                    Checked="True" Font-Size="XX-Small" /></td>
                    <td>
                <asp:CheckBox ID="chkAsistGralVie" runat="server" Text="Viernes" Width="65px" 
                    Checked="True" Font-Size="XX-Small" /></td>
                    <td>
                <asp:CheckBox ID="chkAsistGralSab" runat="server" Text="Sábado" Width="65px" 
                    Checked="True" Font-Size="XX-Small" /></td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                <asp:CheckBox ID="chkAsistGralDom" runat="server" Text="Domingo" Width="65px" 
                    Checked="True" Font-Size="XX-Small" /></td>
                    <td>
                    </td>
                </tr>
            </table>
        </Template>
    </igmisc:WebPanel>
    <table style="width: 404px"><tr><td><asp:Label ForeColor="Red" Font-Size="X-Small" ID="lblError" runat="server"></asp:Label><asp:Label ID="lblCorrecto" runat="server" ForeColor="Green" Font-Size="X-Small"></asp:Label></td></tr>
    <tr>
                <td colspan="2" style="font-size: xx-small; height: 30px; text-align: center;">
                    
                                                    <igtxt:WebImageButton ID="btnRegresar" runat="server" AutoSubmit="True" Height="22px"
                                                        ImageTextSpacing="4" JavaScriptFileName="JS_TurnosEdicion.js" OnClick="btnRegresar_Click"
                                                        Text="Regresar" UseBrowserDefaults="False" Width="100px">
                                                        <Appearance>
                                                            <Image Height="16px" Url="./Imagenes/Back.png" Width="16px" />
                                                        </Appearance>
                                                        <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                                                            HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400"
                                                            PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                                                        <Alignments HorizontalAll="NotSet" VerticalAll="NotSet" VerticalImage="Middle" />
                                                    </igtxt:WebImageButton>
                                                    &nbsp;<igtxt:WebImageButton ID="btnGuardar" runat="server" JavaScriptFileName="JS_TurnosEdicion.js" OnClick="btnGuardar_Click1"
                                                        Text="Guardar" Width="113px" UseBrowserDefaults="False">
                                                        <Appearance>
                                                            <Image Url="./Imagenes/Save_As.png" />
                                                        </Appearance>
                                                        <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                                                            HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400"
                                                            PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                                                    </igtxt:WebImageButton>

                </td>
            </tr></table>
    
            
            
        </table>
        &nbsp;

    
    </div>
    </form>
</body>
</html>
