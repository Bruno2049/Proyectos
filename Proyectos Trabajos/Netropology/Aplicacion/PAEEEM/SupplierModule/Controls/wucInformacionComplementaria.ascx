<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucInformacionComplementaria.ascx.cs" Inherits="PAEEEM.SupplierModule.Controls.wucInformacionComplementaria" %>

<script type="text/javascript">
    var pre = "ctl00_MainContent_wizardPages_wucInformacionComplementaria_";

    function CalculaHorasTrabajadasDia(entrada, salida) {
        var horasTrabajadas;

        if ((entrada == 0.00) || (salida == 0.00))
        {
            horasTrabajadas = 0.00;
        }
        else
        {
            if (entrada > salida) {
                var horasEntrada = 24 - entrada;
                var horasSalida = 0.00 + salida;

                horasTrabajadas = horasEntrada + horasSalida;
            }

            else {
                horasTrabajadas = salida - entrada;
            }
        }
        

        return horasTrabajadas;
    }

    function CalculaTotalHorasTrabajadas() {
        var totalHorasTrabajadas = 0.00;
        var entrada;
        var salida;
        var lunes = document.getElementById(pre + 'ChkLunes');
        var martes = document.getElementById(pre + 'ChkMartes');
        var miercoles = document.getElementById(pre + 'ChkMiercoles');
        var jueves = document.getElementById(pre + 'ChkJueves');
        var viernes = document.getElementById(pre + 'ChkViernes');
        var sabado = document.getElementById(pre + 'ChkSabado');
        var domingo = document.getElementById(pre + 'ChkDomingo');

        if (lunes.checked == true) {
           
            entrada = parseFloat(document.getElementById(pre + 'DDXInicioLunes').value);
            salida = parseFloat(document.getElementById(pre + 'DDXFinLunes').value);

            if (document.getElementById(pre + 'DDXInicioLunes').value == "")
                entrada = 0.00;
            if (document.getElementById(pre + 'DDXFinLunes').value == "")
                salida = 0.00;

            var horasDia = CalculaHorasTrabajadasDia(entrada, salida);
            totalHorasTrabajadas = totalHorasTrabajadas + horasDia;
            document.getElementById(pre + 'HiddenFieldLunes').value = horasDia;
        }

        if (martes.checked == true) {

            entrada = parseFloat(document.getElementById(pre + 'DDXInicioMartes').value);
            salida = parseFloat(document.getElementById(pre + 'DDXFinMartes').value);

            if (document.getElementById(pre + 'DDXInicioMartes').value == "")
                entrada = 0.00;
            if (document.getElementById(pre + 'DDXFinMartes').value == "")
                salida = 0.00;

            var horasDia2 = CalculaHorasTrabajadasDia(entrada, salida);
            totalHorasTrabajadas = totalHorasTrabajadas + horasDia2;
            document.getElementById(pre + 'HiddenFieldMartes').value = horasDia2;
        }

        if (miercoles.checked == true) {

            entrada = parseFloat(document.getElementById(pre + 'DDXInicioMiercoles').value);
            salida = parseFloat(document.getElementById(pre + 'DDXFinMiercoles').value);

            if (document.getElementById(pre + 'DDXInicioMiercoles').value == "")
                entrada = 0.00;
            if (document.getElementById(pre + 'DDXFinMiercoles').value == "")
                salida = 0.00;

            var horasDia3 = CalculaHorasTrabajadasDia(entrada, salida);
            totalHorasTrabajadas = totalHorasTrabajadas + horasDia3;
            document.getElementById(pre + 'HiddenFieldMiercoles').value = horasDia3;
        }

        if (jueves.checked == true) {

            entrada = parseFloat(document.getElementById(pre + 'DDXInicioJueves').value);
            salida = parseFloat(document.getElementById(pre + 'DDXFinJueves').value);

            if (document.getElementById(pre + 'DDXInicioJueves').value == "")
                entrada = 0.00;
            if (document.getElementById(pre + 'DDXFinJueves').value == "")
                salida = 0.00;

            var horasDia4 = CalculaHorasTrabajadasDia(entrada, salida);
            totalHorasTrabajadas = totalHorasTrabajadas + horasDia4;
            document.getElementById(pre + 'HiddenFieldJueves').value = horasDia4;
        }

        if (viernes.checked == true) {

            entrada = parseFloat(document.getElementById(pre + 'DDXInicioViernes').value);
            salida = parseFloat(document.getElementById(pre + 'DDXFinViernes').value);

            if (document.getElementById(pre + 'DDXInicioViernes').value == "")
                entrada = 0.00;
            if (document.getElementById(pre + 'DDXFinViernes').value == "")
                salida = 0.00;

            var horasDia5 = CalculaHorasTrabajadasDia(entrada, salida);
            totalHorasTrabajadas = totalHorasTrabajadas + horasDia5;
            document.getElementById(pre + 'HiddenFieldViernes').value = horasDia5;
        }

        if (sabado.checked == true) {

            entrada = parseFloat(document.getElementById(pre + 'DDXInicioSabado').value);
            salida = parseFloat(document.getElementById(pre + 'DDXFinSabado').value);

            if (document.getElementById(pre + 'DDXInicioSabado').value == "")
                entrada = 0.00;
            if (document.getElementById(pre + 'DDXFinSabado').value == "")
                salida = 0.00;

            var horasDia6 = CalculaHorasTrabajadasDia(entrada, salida);
            totalHorasTrabajadas = totalHorasTrabajadas + horasDia6;
            document.getElementById(pre + 'HiddenFieldSabado').value = horasDia6;
        }

        if (domingo.checked == true) {

            entrada = parseFloat(document.getElementById(pre + 'DDXInicioDomingo').value);
            salida = parseFloat(document.getElementById(pre + 'DDXFinDomingo').value);

            if (document.getElementById(pre + 'DDXInicioDomingo').value == "")
                entrada = 0.00;
            if (document.getElementById(pre + 'DDXFinDomingo').value == "")
                salida = 0.00;

            var horasDia7 = CalculaHorasTrabajadasDia(entrada, salida);
            totalHorasTrabajadas = totalHorasTrabajadas + horasDia7;
            document.getElementById(pre + 'HiddenFieldDomingo').value = horasDia7;
        }

        document.getElementById(pre + 'TxtHorasSemana').value = totalHorasTrabajadas.toFixed(1);
        CalculaHorasAnio();
    }

    function CalculaHorasAnio() {

        var horasSemana = parseFloat(document.getElementById(pre + 'TxtHorasSemana').value);
        var semanasAnio = parseFloat(document.getElementById(pre + 'TxtSemanasAnio').value);
        
        if (document.getElementById(pre + 'TxtHorasSemana').value == "")
            horasSemana = 0.00;

        if (document.getElementById(pre + 'TxtSemanasAnio').value == "")
            semanasAnio = 0.00;

        var horasAnio = horasSemana * semanasAnio;

        document.getElementById(pre + 'TxtHorasAnio').value = horasAnio.toFixed(1);
    }

    function HabilitaHorarios(inicio, fin, check1) {
        var casilla = document.getElementById(pre + check1);

        if (casilla.checked == true) {
            document.getElementById(pre + inicio).disabled = false;
            document.getElementById(pre + fin).disabled = false;
        } else {
            document.getElementById(pre + inicio).disabled = true;
            document.getElementById(pre + fin).disabled = true;
        }
    }
</script>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.50508.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<div id="divForm" style="align-content: center">
    <br/><br/>
    <fieldset class="login" style="border: thin solid #6699FF; width:873px; border-color: #6699FF;">
        <legend style="border-color: #3333FF; color: #0066FF">REGISTRO DE HORARIOS DE OPERACION</legend>
        <table style="width:873px;">
            <tr class="trh">
                <td>
                    Horarios de Operación
                </td>
                <td>
                     Lunes
                    &nbsp;
                    <asp:CheckBox ID="ChkLunes" runat="server" onChange="HabilitaHorarios('DDXInicioLunes', 'DDXFinLunes', 'ChkLunes'); CalculaTotalHorasTrabajadas();" />
                 </td>
                <td>
                     Martes
                    &nbsp;
                    <asp:CheckBox ID="ChkMartes" runat="server" onChange="HabilitaHorarios('DDXInicioMartes', 'DDXFinMartes', 'ChkMartes'); CalculaTotalHorasTrabajadas();" />
                 </td>
                <td>
                     Miercoles
                    &nbsp;
                    <asp:CheckBox ID="ChkMiercoles" runat="server" onChange="HabilitaHorarios('DDXInicioMiercoles', 'DDXFinMiercoles', 'ChkMiercoles'); CalculaTotalHorasTrabajadas();" />
                 </td>
                <td>
                     Jueves
                    &nbsp;
                    <asp:CheckBox ID="ChkJueves" runat="server" onChange="HabilitaHorarios('DDXInicioJueves', 'DDXFinJueves', 'ChkJueves'); CalculaTotalHorasTrabajadas();" />
                 </td>
                <td>
                     Viernes
                    &nbsp;
                    <asp:CheckBox ID="ChkViernes" runat="server" onChange="HabilitaHorarios('DDXInicioViernes', 'DDXFinViernes', 'ChkViernes'); CalculaTotalHorasTrabajadas();" />
                 </td>
                <td>
                     Sabado
                    &nbsp;
                    <asp:CheckBox ID="ChkSabado" runat="server" onChange="HabilitaHorarios('DDXInicioSabado', 'DDXFinSabado', 'ChkSabado'); CalculaTotalHorasTrabajadas();" />
                 </td>
                <td>
                     Domingo
                    &nbsp;
                    <asp:CheckBox ID="ChkDomingo" runat="server" onChange="HabilitaHorarios('DDXInicioDomingo', 'DDXFinDomingo', 'ChkDomingo'); CalculaTotalHorasTrabajadas();" />
                 </td>
            </tr>
            
            <tr class="tr2">
                <td>
                    Inicio
                </td>
                <td>
                    <asp:DropDownList ID="DDXInicioLunes" runat="server" Width="65px" onChange="CalculaTotalHorasTrabajadas();" Font-Size="XX-Small"></asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="DDXInicioMartes" runat="server" Width="65px" onChange="CalculaTotalHorasTrabajadas();" Font-Size="XX-Small"></asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="DDXInicioMiercoles" runat="server" Width="65px" onChange="CalculaTotalHorasTrabajadas();" Font-Size="XX-Small"></asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="DDXInicioJueves" runat="server" Width="65px" onChange="CalculaTotalHorasTrabajadas();" Font-Size="XX-Small"></asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="DDXInicioViernes" runat="server" Width="65px" onChange="CalculaTotalHorasTrabajadas();" Font-Size="XX-Small"></asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="DDXInicioSabado" runat="server" Width="65px" onChange="CalculaTotalHorasTrabajadas();" Font-Size="XX-Small"></asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="DDXInicioDomingo" runat="server" Width="65px" onChange="CalculaTotalHorasTrabajadas();" Font-Size="XX-Small"></asp:DropDownList>
                </td>
            </tr>
            
            <tr class="tr1">
                <td>
                    Fin
                </td>
                <td>
                    <asp:DropDownList ID="DDXFinLunes" runat="server" Width="65px" onChange="CalculaTotalHorasTrabajadas();" Font-Size="XX-Small"></asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="DDXFinMartes" runat="server" Width="65px" onChange="CalculaTotalHorasTrabajadas();" Font-Size="XX-Small"></asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="DDXFinMiercoles" runat="server" Width="65px" onChange="CalculaTotalHorasTrabajadas();" Font-Size="XX-Small"></asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="DDXFinJueves" runat="server" Width="65px" onChange="CalculaTotalHorasTrabajadas();" Font-Size="XX-Small"></asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="DDXFinViernes" runat="server" Width="65px" onChange="CalculaTotalHorasTrabajadas();" Font-Size="XX-Small"></asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="DDXFinSabado" runat="server" Width="65px" onChange="CalculaTotalHorasTrabajadas();" Font-Size="XX-Small"></asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="DDXFinDomingo" runat="server" Width="65px" onChange="CalculaTotalHorasTrabajadas();" Font-Size="XX-Small"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="ok" runat="server" />
                </td>
                <td>
                    <asp:HiddenField ID="HiddenFieldLunes" runat="server" />
                </td>
                <td>
                    <asp:HiddenField ID="HiddenFieldMartes" runat="server" />
                </td>
                <td>
                    <asp:HiddenField ID="HiddenFieldMiercoles" runat="server" />
                </td>
                <td>
                    <asp:HiddenField ID="HiddenFieldJueves" runat="server" />
                </td>
                <td>
                    <asp:HiddenField ID="HiddenFieldViernes" runat="server" />
                </td>
                <td>
                    <asp:HiddenField ID="HiddenFieldSabado" runat="server" />
                </td>
                <td>
                    <asp:HiddenField ID="HiddenFieldDomingo" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    Horas a la semana
                    &nbsp;
                    <asp:TextBox ID="TxtHorasSemana" runat="server" Enabled="False"></asp:TextBox>
                </td>
                <td colspan="2">
                    Semanas al año
                    &nbsp;
                    <asp:TextBox ID="TxtSemanasAnio" runat="server" Text="52" MaxLength="2" onChange="CalculaHorasAnio();"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" 
                                                runat="server" 
                                                TargetControlID="TxtSemanasAnio" 
                                                FilterMode="ValidChars" 
                                                ValidChars="0123456789" />
                </td>
                <td colspan="2">
                    Horas al año
                    &nbsp;
                    <asp:TextBox ID="TxtHorasAnio" runat="server" Enabled="False"></asp:TextBox>
                </td>
            </tr>
        </table>
    </fieldset>
    
    <br/><br/>

    <fieldset class="login" style="border: thin solid #6699FF; width:873px; ">
        <legend style="color: #0066FF">DATOS DEL REPRESENTANTE LEGAL</legend>
        <table style="width:873px;">
            <tr>
                <td colspan="3" style="text-align: center">
                    <asp:CheckBox ID="ChkRepLegal" runat="server" AutoPostBack="True" OnCheckedChanged="ChkRepLegal_CheckedChanged" />
                    Marcar si el Representante Legal es el mismo que el Cliente
                </td>
            </tr>
            <tr>
                <td>
                    <br/>
                </td>
            </tr>
            <tr>
                <td style="width:290px; vertical-align:top;">
                    Nombre(s):<br/>
                    <asp:TextBox ID="TxtNombreRepLegal" runat="server" Width="250px" MaxLength="255"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" 
                            ControlToValidate="TxtNombreRepLegal" 
                            ErrorMessage="Se debe capturar el Nombre (Representante Legal)"
                            ValidationGroup="Basica" 
                            Display="Dynamic" 
                            Text="*"
                            EnableClientScript="true" 
                            ID="RFV1"
                            InitialValue="">
                            </asp:RequiredFieldValidator>
                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" 
                                                runat="server" 
                                                TargetControlID="TxtNombreRepLegal" 
                                                FilterMode="ValidChars" 
                                                ValidChars="0123456789ABCDEFGHIJKLMNOPQRSTUVWXZabcdefghijklmnopqrstuvwxyz " />
                </td>
                <td>
                    Apellido Paterno:<br/>
                    <asp:TextBox ID="TxtApPaternoRepLegal" runat="server" Width="250px" MaxLength="255"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" 
                            ControlToValidate="TxtApPaternoRepLegal" 
                            ErrorMessage="Se debe capturar el Apellido Patermo (Representante Legal)"
                            ValidationGroup="Basica" 
                            Display="Dynamic" 
                            Text="*"
                            EnableClientScript="true" 
                            ID="RequiredFieldValidator43"
                            InitialValue="">
                            </asp:RequiredFieldValidator>
                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender27" 
                                                runat="server" 
                                                TargetControlID="TxtApPaternoRepLegal" 
                                                FilterMode="ValidChars" 
                                                ValidChars="0123456789ABCDEFGHIJKLMNOPQRSTUVWXZabcdefghijklmnopqrstuvwxyz " />
                </td>
                <td>
                    Apellido Materno:<br/>
                    <asp:TextBox ID="TxtApMaternoRepLegal" runat="server" Width="250px" MaxLength="255"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" 
                            ControlToValidate="TxtApMaternoRepLegal" 
                            ErrorMessage="Se debe capturar el Apellido Matermo (Representante Legal)"
                            ValidationGroup="Basica" 
                            Display="Dynamic" 
                            Text="*"
                            EnableClientScript="true" 
                            ID="RequiredFieldValidator44"
                            InitialValue="">
                            </asp:RequiredFieldValidator>
                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender28" 
                                                runat="server" 
                                                TargetControlID="TxtApMaternoRepLegal" 
                                                FilterMode="ValidChars" 
                                                ValidChars="0123456789ABCDEFGHIJKLMNOPQRSTUVWXZabcdefghijklmnopqrstuvwxyz " />
                </td>
            </tr>
            <tr>
                <td style="width:290px; vertical-align:top;">
                    E-mail:
                    <asp:TextBox ID="TxtEmailRepLegal" runat="server" Width="250px"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" 
                            ControlToValidate="TxtEmailRepLegal" 
                            ErrorMessage="Se debe capturar el E-mail (Representante Legal)"
                            ValidationGroup="Basica" 
                            Display="Dynamic" 
                            Text="*"
                            EnableClientScript="true" 
                            ID="RFV2"
                            InitialValue="">
                            </asp:RequiredFieldValidator>
                    <br/>
                    <asp:RegularExpressionValidator 
                        ID="RegularExpressionValidator1" 
                        runat="server" 
                        ControlToValidate="TxtEmailRepLegal" 
                        ErrorMessage="Formato Incorrecto de E-mail" 
                        Display="Dynamic"
                        ValidationGroup="Basica"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                        Formato Incorrecto de E-mail
                    </asp:RegularExpressionValidator>

                </td>
                <td style="width:290px; vertical-align:top;">
                    Teléfono:
                    <asp:TextBox ID="TxtTelefonoRepLegal" runat="server" Width="250px" MaxLength="10"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" 
                            ControlToValidate="TxtTelefonoRepLegal" 
                            ErrorMessage="Se debe capturar el Telefono (Representante Legal)"
                            ValidationGroup="Basica" 
                            Display="Dynamic" 
                            Text="*"
                            EnableClientScript="true" 
                            ID="RequiredFieldValidator40"
                            InitialValue="">
                            </asp:RequiredFieldValidator>
                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" 
                                                runat="server" 
                                                TargetControlID="TxtTelefonoRepLegal" 
                                                FilterMode="ValidChars" 
                                                ValidChars="0123456789" />
                </td>
                </tr>
        </table>
    </fieldset>
    
    <br/><br/>
    
    <fieldset class="login" style="border: thin solid #6699FF; width:873px; border-color: #6699FF;">
        <legend style="border-color: #3333FF; color: #0066FF">DATOS DEL OBLIGADO SOLIDARIO</legend>
        <table style="width:873px;">
            <tr>
                <td style="text-align: center">
                    <asp:RadioButtonList ID="RBListTipoPersona" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="RBListTipoPersona_SelectedIndexChanged">
                        <asp:ListItem Value="2">Persona Moral</asp:ListItem>
                        <asp:ListItem Value="1">Persona Física</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
        </table>
        <asp:Panel ID="PanelPersonaFisica" runat="server">
            <table style="width:873px;">
                <tr>
                    <td style="width:290px; vertical-align:top;">
                        Nombre:
                        <asp:TextBox ID="TxtNombrePF" runat="server" Width="250px" MaxLength="100"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" 
                                ControlToValidate="TxtNombrePF" 
                                ErrorMessage="Se debe capturar el Nombre (Obligado Solidario)"
                                ValidationGroup="Basica" 
                                Display="Dynamic" 
                                Text="*"
                                EnableClientScript="true" 
                                ID="RequiredFieldValidator1"
                                InitialValue="">
                             </asp:RequiredFieldValidator>
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" 
                                                runat="server" 
                                                TargetControlID="TxtNombrePF" 
                                                FilterMode="ValidChars" 
                                                ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZÑ " />
                    </td>
                    <td style="width:290px; vertical-align:top;">
                        Apellido Paterno:
                        <asp:TextBox ID="TxtApPaterno" runat="server" Width="250px" MaxLength="50"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" 
                                ControlToValidate="TxtApPaterno" 
                                ErrorMessage="Se debe capturar el Apellido Paterno (Obligado Solidario)"
                                ValidationGroup="Basica" 
                                Display="Dynamic" 
                                Text="*"
                                EnableClientScript="true" 
                                ID="RequiredFieldValidator2"
                                InitialValue="">
                             </asp:RequiredFieldValidator>
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" 
                                                runat="server" 
                                                TargetControlID="TxtApPaterno" 
                                                FilterMode="ValidChars" 
                                                ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZÑ " />
                    </td>
                    <td style="width:290px; vertical-align:top;">
                        Apellido Materno:
                        <asp:TextBox ID="TxtApMaterno" runat="server" Width="250px"></asp:TextBox>
                    </td>
                </tr>
                
                <tr>
                    <td>
                        Sexo:<br/>
                        <asp:DropDownList ID="DDXSexo" runat="server" Width="252px"></asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" 
                                ControlToValidate="DDXSexo" 
                                ErrorMessage="Se debe seleccionar el Sexo (Obligado Solidario)"
                                ValidationGroup="Basica" 
                                Display="Dynamic" 
                                Text="*"
                                EnableClientScript="true" 
                                ID="RFV3"
                                InitialValue="">
                             </asp:RequiredFieldValidator>
                    </td>
                    <td>
                        Fecha de Nacimiento:
                        <asp:TextBox ID="TxtFechaNacimietoOS" runat="server" Width="230px"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server"
                        ControlToValidate="TxtFechaNacimietoOS"
                        ErrorMessage="Se debe capturar la Fecha de Constitición"
                        ValidationGroup="Basica"
                        Display="Dynamic"
                        Text="*"
                        EnableClientScript="true"
                        ID="RequiredFieldValidator21"
                        InitialValue="">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator runat="server"
                        ControlToValidate="TxtFechaNacimietoOS" 
                        ErrorMessage="*" 
                        ValidationGroup="Basica"
                        ID="RegularExpressionValidator6"
                        ValidationExpression="^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-9]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$" Height="0px">
                      Formato de Fecha AAAA-MM-DD
                    </asp:RegularExpressionValidator>
                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender10"
                        runat="server"
                        TargetControlID="TxtFechaNacimietoOS"
                        FilterMode="ValidChars"
                        ValidChars="0123456789-" />
                    </td>
                    <td>
                        RFC:<br/>
                        <asp:TextBox ID="TxtRFCOS" runat="server" Width="250px" MaxLength="15"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" 
                                ControlToValidate="TxtRFCOS" 
                                ErrorMessage="Se debe capturar el RFC (Obligado Solidario)"
                                ValidationGroup="Basica" 
                                Display="Dynamic" 
                                Text="*"
                                EnableClientScript="true" 
                                ID="RFV5"
                                InitialValue="">
                             </asp:RequiredFieldValidator>
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" 
                                                runat="server" 
                                                TargetControlID="TxtRFCOS" 
                                                FilterMode="ValidChars" 
                                                ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZÑ0123456789" />
                        <br/>
                        <asp:RegularExpressionValidator 
                            ID="RegularExpressionValidator3" 
                            runat="server" 
                            ControlToValidate="TxtRFCOS" 
                            ErrorMessage="Formato Incorrecto de RFC (AAAA9999XXX)" 
                            Display="Dynamic"
                            ValidationGroup="Basica"
                            ValidationExpression="^(([A-Z]|[a-z]){4})([0-9]{6})((([A-Z]|[a-z]|[0-9]){3}))">
                            Formato Incorrecto de RFC (AAAA9999XXX)
                        </asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        CURP:
                        <asp:TextBox ID="TxtCURPOS" runat="server" Width="250px"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" 
                                ControlToValidate="TxtCURPOS" 
                                ErrorMessage="Se debe capturar la CURP (Obligado Solidario)"
                                ValidationGroup="Basica" 
                                Display="Dynamic" 
                                Text="*"
                                EnableClientScript="true" 
                                ID="RFV6"
                                InitialValue="">
                             </asp:RequiredFieldValidator>
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" 
                                                runat="server" 
                                                TargetControlID="TxtCURPOS" 
                                                FilterMode="ValidChars" 
                                                ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZÑ0123456789" />
                        <asp:RegularExpressionValidator 
                            ID="RegularExpressionValidator4" 
                            runat="server" 
                            ControlToValidate="TxtCURPOS" 
                            ErrorMessage="Formato Incorrecto CURP" 
                            Display="Dynamic"
                            ValidationGroup="Basica"
                            ValidationExpression="^[a-zA-Z]{4}\d{6}[a-zA-Z]{6}\d{2}$" >
                            Formato Incorrecto CURP
                        </asp:RegularExpressionValidator>
                    </td>
                    <td>
                        Telefono:
                        <asp:TextBox ID="TxtTelefonoOS" runat="server"  Width="250px" MaxLength="10"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" 
                                    ControlToValidate="TxtTelefonoOS" 
                                    ErrorMessage="Se debe capturar el Telefono (Obligado Solidario)"
                                    ValidationGroup="Basica" 
                                    Display="Dynamic" 
                                    Text="*"
                                    EnableClientScript="true" 
                                    ID="RequiredFieldValidator9"
                                    InitialValue="">
                                </asp:RequiredFieldValidator>
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" 
                                                runat="server" 
                                                TargetControlID="TxtTelefonoOS" 
                                                FilterMode="ValidChars" 
                                                ValidChars="0123456789" />
                    </td>
                    <td>
                        <br/>
                    </td>
                </tr>
                <tr>
                    <td style="width:290px; vertical-align:top;">
                        Codigo Postal:
                        <asp:TextBox ID="TxtCPOS" runat="server"  Width="200px" MaxLength="5"></asp:TextBox>
                        &nbsp;
                        <asp:ImageButton ID="ImgBtnBuscarCP" runat="server" ImageUrl="~/SupplierModule/images/buscar.png" OnClick="ImgBtnBuscarCP_Click" />
                        <asp:RequiredFieldValidator runat="server" 
                                    ControlToValidate="TxtCPOS" 
                                    ErrorMessage="Se debe capturar el Codigo Postal (Obligado Solidario)"
                                    ValidationGroup="Basica" 
                                    Display="Dynamic" 
                                    Text="*"
                                    EnableClientScript="true" 
                                    ID="RequiredFieldValidator12"
                                    InitialValue="">
                                </asp:RequiredFieldValidator>
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" 
                                                    runat="server" 
                                                    TargetControlID="TxtCPOS" 
                                                    FilterMode="ValidChars" 
                                                    ValidChars="0123456789" />
                    </td>
                    <td style="width:290px; vertical-align:top;">
                        Estado:
                        <asp:DropDownList ID="DDXEstadoOS" runat="server"  Width="252px" AutoPostBack="True" OnSelectedIndexChanged="DDXEstadoOS_SelectedIndexChanged"></asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" 
                                    ControlToValidate="DDXEstadoOS" 
                                    ErrorMessage="Se debe seleccionar el Estado (Obligado Solidario)"
                                    ValidationGroup="Basica" 
                                    Display="Dynamic" 
                                    Text="*"
                                    EnableClientScript="true" 
                                    ID="RequiredFieldValidator13"
                                    InitialValue=""> </asp:RequiredFieldValidator>
                    </td>
                    <td style="width:290px; vertical-align:top;">
                        Delegación o Municipio:
                        <asp:DropDownList ID="DDXMunicipioOS"  Width="252px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDXMunicipioOS_SelectedIndexChanged"></asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" 
                                    ControlToValidate="DDXMunicipioOS" 
                                    ErrorMessage="Se debe seleccionar el Municipio o Delegación (Obligado Solidario)"
                                    ValidationGroup="Basica" 
                                    Display="Dynamic" 
                                    Text="*"
                                    EnableClientScript="true" 
                                    ID="RequiredFieldValidator14"
                                    InitialValue=""> </asp:RequiredFieldValidator>
                    </td>                   
                </tr>
                <tr>
                    <td>
                        Colonia:
                        <asp:DropDownList ID="DDXColoniaOS"  Width="252px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDXColoniaOS_SelectedIndexChanged"></asp:DropDownList>
                        <asp:DropDownList ID="DDXColoniaOSHidden"  Width="252px" runat="server" Visible="False"></asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" 
                                    ControlToValidate="DDXColoniaOS" 
                                    ErrorMessage="Se debe seleccionar la Colonia del Domicilio (Obligado Solidario)"
                                    ValidationGroup="Basica" 
                                    Display="Dynamic" 
                                    Text="*"
                                    EnableClientScript="true" 
                                    ID="RequiredFieldValidator15"
                                    InitialValue=""> </asp:RequiredFieldValidator>
                    </td>
                    <td>
                        Calle:
                        <asp:TextBox ID="TxtCalleOS" runat="server" Width="250px" MaxLength="50"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" 
                                    ControlToValidate="TxtCalleOS" 
                                    ErrorMessage="Se debe capturar la Calle (Obligado Solidario)"
                                    ValidationGroup="Basica" 
                                    Display="Dynamic" 
                                    Text="*"
                                    EnableClientScript="true" 
                                    ID="RequiredFieldValidator16"
                                    InitialValue="">
                                </asp:RequiredFieldValidator>
                    </td>
                    <td>
                        Número Exterior e Interior:
                        <asp:TextBox ID="TxtNoExteriorOS" runat="server" Width="250px" MaxLength="50"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" 
                                    ControlToValidate="TxtNoExteriorOS" 
                                    ErrorMessage="Se debe capturar el Número Exterior (Obligado Solidario)"
                                    ValidationGroup="Basica" 
                                    Display="Dynamic" 
                                    Text="*"
                                    EnableClientScript="true" 
                                    ID="RequiredFieldValidator3"
                                    InitialValue="">
                                </asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        
        <asp:Panel ID="PanelPersonaMoral" runat="server">
            <table style="width:873px;">
                <tr>
                    <td>
                        <br/>
                    </td>
                </tr>
                <tr>
                    <td>
                        Razón Social&nbsp;<asp:TextBox ID="TxtRazonSocialOS" runat="server" Width="40%" MaxLength="255"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" 
                                ControlToValidate="TxtRazonSocialOS" 
                                ErrorMessage="Se debe capturar la Razón Social (Obligado Solidario)"
                                ValidationGroup="Basica" 
                                Display="Dynamic" 
                                Text="*"
                                EnableClientScript="true" 
                                ID="RequiredFieldValidator41"
                                InitialValue="">
                                </asp:RequiredFieldValidator>
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender26" 
                                                runat="server" 
                                                TargetControlID="TxtRazonSocialOS" 
                                                FilterMode="ValidChars" 
                                                ValidChars="0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz " />
                    </td>
                </tr>
                <tr>
                    <td>
                        <br/>
                    </td>
                </tr>
            </table>
            <table style="width:873px;">
                <tr>
                    <td colspan="3" style="color: #0066FF">
                        DATOS DEL REPRESENTANTE LEGAL DEL OBLIGADO SOLIDARIO
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <br/>
                    </td>
                </tr>
                <tr>
                <td style="width:290px; vertical-align:top;">
                    Nombre(s):<br/>
                    <asp:TextBox ID="TxtNombreRepLegalOS" runat="server" Width="250px" MaxLength="255"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" 
                            ControlToValidate="TxtNombreRepLegal" 
                            ErrorMessage="Se debe capturar el Nombre (Representante Legal Obligado Solidario)"
                            ValidationGroup="Basica" 
                            Display="Dynamic" 
                            Text="*"
                            EnableClientScript="true" 
                            ID="RequiredFieldValidator4"
                            InitialValue="">
                            </asp:RequiredFieldValidator>
                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender29" 
                                                runat="server" 
                                                TargetControlID="TxtNombreRepLegal" 
                                                FilterMode="ValidChars" 
                                                ValidChars="0123456789ABCDEFGHIJKLMNOPQRSTUVWXZabcdefghijklmnopqrstuvwxyz " />
                </td>
                <td>
                    Apellido Paterno:<br/>
                    <asp:TextBox ID="TxtApPaternoRepLegalOS" runat="server" Width="250px" MaxLength="255"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" 
                            ControlToValidate="TxtApPaternoRepLegal" 
                            ErrorMessage="Se debe capturar el Apellido Patermo (Representante Legal Obligado Solidario)"
                            ValidationGroup="Basica" 
                            Display="Dynamic" 
                            Text="*"
                            EnableClientScript="true" 
                            ID="RequiredFieldValidator45"
                            InitialValue="">
                            </asp:RequiredFieldValidator>
                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender30" 
                                                runat="server" 
                                                TargetControlID="TxtApPaternoRepLegal" 
                                                FilterMode="ValidChars" 
                                                ValidChars="0123456789ABCDEFGHIJKLMNOPQRSTUVWXZabcdefghijklmnopqrstuvwxyz " />
                </td>
                <td>
                    Apellido Materno:<br/>
                    <asp:TextBox ID="TxtApMaternoRepLegalOS" runat="server" Width="250px" MaxLength="255"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" 
                            ControlToValidate="TxtApMaternoRepLegal" 
                            ErrorMessage="Se debe capturar el Apellido Matermo (Representante Legal Obligado Solidario)"
                            ValidationGroup="Basica" 
                            Display="Dynamic" 
                            Text="*"
                            EnableClientScript="true" 
                            ID="RequiredFieldValidator46"
                            InitialValue="">
                            </asp:RequiredFieldValidator>
                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender31" 
                                                runat="server" 
                                                TargetControlID="TxtApMaternoRepLegal" 
                                                FilterMode="ValidChars" 
                                                ValidChars="0123456789ABCDEFGHIJKLMNOPQRSTUVWXZabcdefghijklmnopqrstuvwxyz " />
                </td>
            </tr>
                <tr>
                    <td style="width:290px; vertical-align:top;">
                        E-mail:
                        <asp:TextBox ID="TxtEmailRepLegalOS" runat="server" Width="250px"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" 
                                ControlToValidate="TxtEmailRepLegalOS" 
                                ErrorMessage="Se debe capturar el Email (Representante legal- Obligado Solidario)"
                                ValidationGroup="Basica" 
                                Display="Dynamic" 
                                Text="*"
                                EnableClientScript="true" 
                                ID="RequiredFieldValidator6"
                                InitialValue="">
                                </asp:RequiredFieldValidator>
                        <br/>
                    <asp:RegularExpressionValidator 
                        ID="RegularExpressionValidator2" 
                        runat="server" 
                        ControlToValidate="TxtEmailRepLegalOS" 
                        ErrorMessage="Formato Incorrecto de E-mail" 
                        Display="Dynamic"
                        ValidationGroup="Basica"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                        Formato Incorrecto de E-mail
                    </asp:RegularExpressionValidator>
                    </td>
                    <td style="width:290px; vertical-align:top;">
                        Teléfono:
                        <asp:TextBox ID="TxtTelefonoRLOS" runat="server" Width="250px" MaxLength="10"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" 
                                ControlToValidate="TxtTelefonoRLOS" 
                                ErrorMessage="Se debe capturar el Telefono (Representante legal- Obligado Solidario)"
                                ValidationGroup="Basica" 
                                Display="Dynamic" 
                                Text="*"
                                EnableClientScript="true" 
                                ID="RequiredFieldValidator42"
                                InitialValue="">
                                </asp:RequiredFieldValidator>
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" 
                                                runat="server" 
                                                TargetControlID="TxtTelefonoRLOS" 
                                                FilterMode="ValidChars" 
                                                ValidChars="0123456789" />
                    </td>
                    <td>
                        <br/>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <br/>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="color: #0066FF">
                        PODER NOTARIAL DEL REPRESENTANTE LEGAL DEL OBLIGADO SOLIDARIO
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <br/>
                    </td>
                </tr>
                <tr>
                    <td style="width:290px; vertical-align:top;">
                        Número de Escritura:
                        <asp:TextBox ID="TxtNoEscrituraPN" runat="server" Width="250px" MaxLength="10"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" 
                                ControlToValidate="TxtNoEscrituraPN" 
                                ErrorMessage="Se debe capturar el Número de Escritura (Pooder Notarial)"
                                ValidationGroup="Basica" 
                                Display="Dynamic" 
                                Text="*"
                                EnableClientScript="true" 
                                ID="RequiredFieldValidator7"
                                InitialValue="">
                                </asp:RequiredFieldValidator>
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" 
                                                runat="server" 
                                                TargetControlID="TxtNoEscrituraPN" 
                                                FilterMode="ValidChars" 
                                                ValidChars="0123456789" />
                    </td>
                    <td style="width:290px; vertical-align:top;">
                        Fecha:<br/>
                        <asp:TextBox ID="TxtFechaEscrituraPN" runat="server" Width="230px"></asp:TextBox>
                          <asp:RequiredFieldValidator runat="server"
                        ControlToValidate="TxtFechaEscrituraPN"
                        ErrorMessage="Se debe capturar la Fecha de Constitición"
                        ValidationGroup="Basica"
                        Display="Dynamic"
                        Text="*"
                        EnableClientScript="true"
                        ID="RequiredFieldValidator5"
                        InitialValue="">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator runat="server"
                        ControlToValidate="TxtFechaEscrituraPN" 
                        ErrorMessage="*" 
                        ValidationGroup="Basica"
                        ID="RegularExpressionValidator5"
                        ValidationExpression="^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-9]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$" Height="0px">
                      Formato de Fecha AAAA-MM-DD
                    </asp:RegularExpressionValidator>
                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender16"
                        runat="server"
                        TargetControlID="TxtFechaEscrituraPN"
                        FilterMode="ValidChars"
                        ValidChars="0123456789-" />
                    </td>
                    <td style="width:290px; vertical-align:top;">
                        Nombre Completo del Notario:
                        <asp:TextBox ID="TxtNombreNotarioPN" runat="server" Width="250px" MaxLength="255"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" 
                                ControlToValidate="TxtNombreNotarioPN" 
                                ErrorMessage="Se debe capturar el Nombre del Notario (Poder Notarial)"
                                ValidationGroup="Basica" 
                                Display="Dynamic" 
                                Text="*"
                                EnableClientScript="true" 
                                ID="RequiredFieldValidator10"
                                InitialValue="">
                                </asp:RequiredFieldValidator>
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" 
                                                runat="server" 
                                                TargetControlID="TxtNombreNotarioPN" 
                                                FilterMode="ValidChars" 
                                                ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZÑ" />
                    </td>
                </tr>
                <tr>
                    <td style="width:290px; vertical-align:top;">
                        Estado:
                        <asp:DropDownList ID="DDXEstadoPN" runat="server"  Width="252px" AutoPostBack="True" OnSelectedIndexChanged="DDXEstadoPN_SelectedIndexChanged"></asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" 
                                    ControlToValidate="DDXEstadoPN" 
                                    ErrorMessage="Se debe seleccionar el Estado (Poder Notarial)"
                                    ValidationGroup="Basica" 
                                    Display="Dynamic" 
                                    Text="*"
                                    EnableClientScript="true" 
                                    ID="RequiredFieldValidator11"
                                    InitialValue=""> </asp:RequiredFieldValidator>
                    </td>
                    <td style="width:290px; vertical-align:top;">
                        Delegación o Municipio:
                        <asp:DropDownList ID="DDXMunicipioPN"  Width="252px" runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" 
                                    ControlToValidate="DDXMunicipioPN" 
                                    ErrorMessage="Se debe seleccionar el Municipio o Delegación (Poder Notarial)"
                                    ValidationGroup="Basica" 
                                    Display="Dynamic" 
                                    Text="*"
                                    EnableClientScript="true" 
                                    ID="RequiredFieldValidator17"
                                    InitialValue=""> </asp:RequiredFieldValidator>
                    </td>
                    <td style="width:290px; vertical-align:top;">
                        Número de Notaría:
                        <asp:TextBox ID="TxtNotariaPN" runat="server" Width="250px" MaxLength="10"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" 
                                ControlToValidate="TxtNotariaPN" 
                                ErrorMessage="Se debe capturar el Número de Notaria (Poder Notarial)"
                                ValidationGroup="Basica" 
                                Display="Dynamic" 
                                Text="*"
                                EnableClientScript="true" 
                                ID="RequiredFieldValidator18"
                                InitialValue="">
                                </asp:RequiredFieldValidator>
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" 
                                                runat="server" 
                                                TargetControlID="TxtNotariaPN" 
                                                FilterMode="ValidChars" 
                                                ValidChars="0123456789" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <br/>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="color: #0066FF">
                        ACTA CONSTITUTIVA DEL OBLIGADO SOLIDARIO
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <br/>
                    </td>
                </tr>
                <tr>
                    <td style="width:290px; vertical-align:top;">
                        Número de Escritura:
                        <asp:TextBox ID="TxtNumeroEscrituraAC" runat="server" Width="250px" MaxLength="10"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" 
                                ControlToValidate="TxtNumeroEscrituraAC" 
                                ErrorMessage="Se debe capturar el Número de Escritura del Acta Constitutiva"
                                ValidationGroup="Basica" 
                                Display="Dynamic" 
                                Text="*"
                                EnableClientScript="true" 
                                ID="RequiredFieldValidator19"
                                InitialValue="">
                                </asp:RequiredFieldValidator>
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" 
                                                runat="server" 
                                                TargetControlID="TxtNumeroEscrituraAC" 
                                                FilterMode="ValidChars" 
                                                ValidChars="0123456789" />
                    </td>
                    <td style="width:290px; vertical-align:top;">
                        Fecha:<br/>
                        <asp:TextBox ID="TxtFechaAC" runat="server" Width="230px"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server"
                        ControlToValidate="TxtFechaAC"
                        ErrorMessage="Se debe capturar la Fecha de Constitición"
                        ValidationGroup="Basica"
                        Display="Dynamic"
                        Text="*"
                        EnableClientScript="true"
                        ID="RequiredFieldValidator8"
                        InitialValue="">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator runat="server"
                        ControlToValidate="TxtFechaAC" 
                        ErrorMessage="*" 
                        ValidationGroup="Basica"
                        ID="RegularExpressionValidator7"
                        ValidationExpression="^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-9]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$" Height="0px">
                      Formato de Fecha AAAA-MM-DD
                    </asp:RegularExpressionValidator>
                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender32"
                        runat="server"
                        TargetControlID="TxtFechaAC"
                        FilterMode="ValidChars"
                        ValidChars="0123456789-" />
                    </td>
                    <td style="width:290px; vertical-align:top;">
                        Nombre Completo del Notario:
                        <asp:TextBox ID="TxtNombreNotarioAC" runat="server" Width="250px"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" 
                                ControlToValidate="TxtNombreNotarioAC" 
                                ErrorMessage="Se debe capturar el Nombre del Notario (Acta Constitutiva)"
                                ValidationGroup="Basica" 
                                Display="Dynamic" 
                                Text="*"
                                EnableClientScript="true" 
                                ID="RequiredFieldValidator231"
                                InitialValue="">
                                </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width:290px; vertical-align:top;">
                        Estado:
                        <asp:DropDownList ID="DDXEstadoAC" runat="server"  Width="252px" AutoPostBack="True" OnSelectedIndexChanged="DDXEstadoAC_SelectedIndexChanged"></asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" 
                                    ControlToValidate="DDXEstadoAC" 
                                    ErrorMessage="Se debe seleccionar el Estado del (Acta Constitutiva)"
                                    ValidationGroup="Basica" 
                                    Display="Dynamic" 
                                    Text="*"
                                    EnableClientScript="true" 
                                    ID="RequiredFieldValidator22"
                                    InitialValue=""> </asp:RequiredFieldValidator>
                    </td>
                    <td style="width:290px; vertical-align:top;">
                        Delegación o Municipio:
                        <asp:DropDownList ID="DDXMunicipioAC"  Width="252px" runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" 
                                    ControlToValidate="DDXMunicipioAC" 
                                    ErrorMessage="Se debe seleccionar el Municipio o Delegación del Domicilio (Acta Constitutiva)"
                                    ValidationGroup="Basica" 
                                    Display="Dynamic" 
                                    Text="*"
                                    EnableClientScript="true" 
                                    ID="RequiredFieldValidator23"
                                    InitialValue=""> </asp:RequiredFieldValidator>
                    </td>
                    <td style="width:290px; vertical-align:top;">
                        Número de Notaría:
                        <asp:TextBox ID="TxtNotariaAC" runat="server" Width="250px" MaxLength="10"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" 
                                ControlToValidate="TxtNotariaAC" 
                                ErrorMessage="Se debe capturar el Número de Notaría (Acta Constitutiva)"
                                ValidationGroup="Basica" 
                                Display="Dynamic" 
                                Text="*"
                                EnableClientScript="true" 
                                ID="RequiredFieldValidator284"
                                InitialValue="">
                                </asp:RequiredFieldValidator>
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender196" 
                                                runat="server" 
                                                TargetControlID="TxtNotariaAC" 
                                                FilterMode="ValidChars" 
                                                ValidChars="0123456789" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </fieldset>
    
    <br/><br/>
    
    <asp:Panel ID="PanelColonias" runat="server" Visible="False">
        <fieldset class="login"  style="border: thin solid #6699FF; width:873px;  border-color: #6699FF;">
        <legend style="border-color: #3333FF; color: #0066FF">ASENTAMIENTOS ENCONTRADOS</legend>
        <table style="text-align: center; width: 100%">
            <tr>
                <td>
                    <br/>
                </td>
            </tr> 
            <tr>
                <td style="text-align: center">
                    <asp:GridView ID="grdColonias" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                                AllowPaging="True" PageSize="20" DataKeyNames="CveCp" Width="100%" HorizontalAlign="Center" AutoGenerateSelectButton="True" OnSelectedIndexChanged="grdColonias_SelectedIndexChanged">
                                <FooterStyle CssClass="GridViewFooterStyle" />
                                <RowStyle CssClass="GridViewRowStyle" />
                                <HeaderStyle CssClass="GridViewHeaderStyleNet" />
                                <PagerStyle CssClass="GridViewPagerStyle" />
                                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                <Columns>
                                    <asp:BoundField DataField="CveCp" Visible="False"></asp:BoundField>
                                    <asp:BoundField DataField="CodigoPostal" HeaderText="C.P."></asp:BoundField>
                                    <asp:BoundField DataField="DxColonia" HeaderText="Asentamiento"></asp:BoundField>
                                    <asp:BoundField DataField="DxTipoColonia" HeaderText="Tipo Asentamiento"></asp:BoundField>
                                    <asp:BoundField DataField="DxDelegacionMunicipio" HeaderText="Delegación/Municipio"></asp:BoundField>
                                    <asp:BoundField DataField="DxEstado" HeaderText="Estado"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Seleccionar" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ckbSelect" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                </td>
            </tr>
        </table>
        </fieldset>
        <br/><br/>
    </asp:Panel>
    
    <fieldset class="login" style="border: thin solid #6699FF; width:873px; border-color: #6699FF;">
        <legend style="border-color: #3333FF; color: #0066FF">PODER NOTARIAL DEL REPRESENTANTE LEGAL</legend>
        <table>    
            <tr>
                    <td style="width:290px; vertical-align:top;">
                        Número de Escritura:
                        <asp:TextBox ID="TxtNumeroEscrituraPNCliente" runat="server" Width="250px" MaxLength="10"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" 
                                ControlToValidate="TxtNumeroEscrituraPNCliente" 
                                ErrorMessage="Se debe capturar el Número de Escritura (Poder Notarial - Rep. Legal)"
                                ValidationGroup="Basica" 
                                Display="Dynamic" 
                                Text="*"
                                EnableClientScript="true" 
                                ID="RequiredFieldValidator25"
                                InitialValue="">
                                </asp:RequiredFieldValidator>
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" 
                                                runat="server" 
                                                TargetControlID="TxtNumeroEscrituraPNCliente" 
                                                FilterMode="ValidChars" 
                                                ValidChars="0123456789" />
                    </td>
                    <td style="width:290px; vertical-align:top;">
                        Fecha:<br/>
                        <asp:TextBox ID="TxtPNCliente" runat="server" Width="230px"></asp:TextBox>
                                         <asp:RequiredFieldValidator runat="server"
                        ControlToValidate="TxtPNCliente"
                        ErrorMessage="Se debe capturar la Fecha de Constitición"
                        ValidationGroup="Basica"
                        Display="Dynamic"
                        Text="*"
                        EnableClientScript="true"
                        ID="RequiredFieldValidator24"
                        InitialValue="">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator runat="server"
                        ControlToValidate="TxtPNCliente" 
                        ErrorMessage="*" 
                        ValidationGroup="Basica"
                        ID="RegularExpressionValidator9"
                        ValidationExpression="^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-9]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$" Height="0px">
                      Formato de Fecha AAAA-MM-DD
                    </asp:RegularExpressionValidator>
                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender34"
                        runat="server"
                        TargetControlID="TxtPNCliente"
                        FilterMode="ValidChars"
                        ValidChars="0123456789-" />
                    </td>
                    <td style="width:290px; vertical-align:top;">
                        Nombre Completo del Notario:
                        <asp:TextBox ID="TxtNomNotarioPNCliente" runat="server" Width="250px" MaxLength="255"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" 
                                ControlToValidate="TxtNomNotarioPNCliente" 
                                ErrorMessage="Se debe capturar el Nombre del Notario (Poder Notarial Rep. Legal)"
                                ValidationGroup="Basica" 
                                Display="Dynamic" 
                                Text="*"
                                EnableClientScript="true" 
                                ID="RequiredFieldValidator27"
                                InitialValue="">
                                </asp:RequiredFieldValidator>
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender18" 
                                                runat="server" 
                                                TargetControlID="TxtNomNotarioPNCliente" 
                                                FilterMode="ValidChars" 
                                                ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZÑ" />
                    </td>
                </tr>
                <tr>
                    <td style="width:290px; vertical-align:top;">
                        Estado:
                        <asp:DropDownList ID="DDXEstadoPNRL" runat="server"  Width="252px" AutoPostBack="True" OnSelectedIndexChanged="DDXEstadoPNRL_SelectedIndexChanged"></asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" 
                                    ControlToValidate="DDXEstadoPNRL" 
                                    ErrorMessage="Se debe seleccionar el Estado (Poder Notarial - Rep. Legal)"
                                    ValidationGroup="Basica" 
                                    Display="Dynamic" 
                                    Text="*"
                                    EnableClientScript="true" 
                                    ID="RequiredFieldValidator28"
                                    InitialValue=""> </asp:RequiredFieldValidator>
                    </td>
                    <td style="width:290px; vertical-align:top;">
                        Delegación o Municipio:
                        <asp:DropDownList ID="DDXMunicipioPNRL"  Width="252px" runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" 
                                    ControlToValidate="DDXMunicipioPNRL" 
                                    ErrorMessage="Se debe seleccionar el Municipio o Delegación (Poder Notarial - Rep. Legal)"
                                    ValidationGroup="Basica" 
                                    Display="Dynamic" 
                                    Text="*"
                                    EnableClientScript="true" 
                                    ID="RequiredFieldValidator29"
                                    InitialValue=""> </asp:RequiredFieldValidator>
                    </td>
                    <td style="width:290px; vertical-align:top;">
                        Número de Notaría:
                        <asp:TextBox ID="TxtNotariaPNRL" runat="server" Width="250px" MaxLength="10"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" 
                                ControlToValidate="TxtNotariaPNRL" 
                                ErrorMessage="Se debe capturar el Número de Notaria (Poder Notarial - Rep. Legal)"
                                ValidationGroup="Basica" 
                                Display="Dynamic" 
                                Text="*"
                                EnableClientScript="true" 
                                ID="RequiredFieldValidator30"
                                InitialValue="">
                                </asp:RequiredFieldValidator>
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender19" 
                                                runat="server" 
                                                TargetControlID="TxtNotariaPNRL" 
                                                FilterMode="ValidChars" 
                                                ValidChars="0123456789" />
                    </td>
                </tr>
        </table>
    </fieldset>
    
    <br/><br/>
    
    <fieldset class="login" style="border: thin solid #6699FF; width:873px; border-color: #6699FF;">
        <legend style="border-color: #3333FF; color: #0066FF">ACTA CONSTITUTIVA</legend>
        <table style="width:873px;">
            <tr>
                    <td style="width:290px; vertical-align:top;">
                        Número de Escritura:
                        <asp:TextBox ID="TxtNoEscrituraClienteAC" runat="server" Width="250px" MaxLength="10"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" 
                                ControlToValidate="TxtNoEscrituraClienteAC" 
                                ErrorMessage="Se debe capturar el Número de Escritura (Acta Constitutiva)"
                                ValidationGroup="Basica" 
                                Display="Dynamic" 
                                Text="*"
                                EnableClientScript="true" 
                                ID="RequiredFieldValidator31"
                                InitialValue="">
                                </asp:RequiredFieldValidator>
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender20" 
                                                runat="server" 
                                                TargetControlID="TxtNoEscrituraClienteAC" 
                                                FilterMode="ValidChars" 
                                                ValidChars="0123456789" />
                    </td>
                    <td style="width:290px; vertical-align:top;">
                        Fecha:<br/>
                        <asp:TextBox ID="TxtFechaClienteAC" runat="server" Width="230px"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server"
                        ControlToValidate="TxtFechaClienteAC"
                        ErrorMessage="Se debe capturar la Fecha de Constitición"
                        ValidationGroup="Basica"
                        Display="Dynamic"
                        Text="*"
                        EnableClientScript="true"
                        ID="RequiredFieldValidator20"
                        InitialValue="">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator runat="server"
                        ControlToValidate="TxtFechaClienteAC" 
                        ErrorMessage="*" 
                        ValidationGroup="Basica"
                        ID="RegularExpressionValidator8"
                        ValidationExpression="^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-9]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$" Height="0px">
                      Formato de Fecha AAAA-MM-DD
                    </asp:RegularExpressionValidator>
                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender33"
                        runat="server"
                        TargetControlID="TxtFechaClienteAC"
                        FilterMode="ValidChars"
                        ValidChars="0123456789-" />
                    </td>
                    <td style="width:290px; vertical-align:top;">
                        Nombre Completo del Notario:
                        <asp:TextBox ID="TxtNomNotarioClienteAC" runat="server" Width="250px" MaxLength="255"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" 
                                ControlToValidate="TxtNomNotarioClienteAC" 
                                ErrorMessage="Se debe capturar el Nombre del Notario (Acta Constitutiva)"
                                ValidationGroup="Basica" 
                                Display="Dynamic" 
                                Text="*"
                                EnableClientScript="true" 
                                ID="RequiredFieldValidator33"
                                InitialValue="">
                                </asp:RequiredFieldValidator>
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender21" 
                                                runat="server" 
                                                TargetControlID="TxtNomNotarioClienteAC" 
                                                FilterMode="ValidChars" 
                                                ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZÑ" />
                    </td>
                </tr>
                <tr>
                    <td style="width:290px; vertical-align:top;">
                        Estado:
                        <asp:DropDownList ID="DDXEstadoClienteAC" runat="server"  Width="252px" AutoPostBack="True" OnSelectedIndexChanged="DDXEstadoClienteAC_SelectedIndexChanged"></asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" 
                                    ControlToValidate="DDXEstadoClienteAC" 
                                    ErrorMessage="Se debe seleccionar el Estado (Acta Constitutiva)"
                                    ValidationGroup="Basica" 
                                    Display="Dynamic" 
                                    Text="*"
                                    EnableClientScript="true" 
                                    ID="RequiredFieldValidator34"
                                    InitialValue=""> </asp:RequiredFieldValidator>
                    </td>
                    <td style="width:290px; vertical-align:top;">
                        Delegación o Municipio:
                        <asp:DropDownList ID="DDXMunicipipClienteAC"  Width="252px" runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" 
                                    ControlToValidate="DDXMunicipipClienteAC" 
                                    ErrorMessage="Se debe seleccionar el Municipio o Delegación Acta Constitutiva"
                                    ValidationGroup="Basica" 
                                    Display="Dynamic" 
                                    Text="*"
                                    EnableClientScript="true" 
                                    ID="RequiredFieldValidator35"
                                    InitialValue=""> </asp:RequiredFieldValidator>
                    </td>
                    <td style="width:290px; vertical-align:top;">
                        Número de Notaría:
                        <asp:TextBox ID="TxtNotariaClienteAC" runat="server" Width="250px" MaxLength="10"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" 
                                ControlToValidate="TxtNotariaClienteAC" 
                                ErrorMessage="Se debe capturar el Número de Notaría (Acta Constitutiva)"
                                ValidationGroup="Basica" 
                                Display="Dynamic" 
                                Text="*"
                                EnableClientScript="true" 
                                ID="RequiredFieldValidator36"
                                InitialValue="">
                                </asp:RequiredFieldValidator>
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender22" 
                                                runat="server" 
                                                TargetControlID="TxtNotariaClienteAC" 
                                                FilterMode="ValidChars" 
                                                ValidChars="0123456789" />
                    </td>
                </tr>
        </table>
    </fieldset>
    
    <br/><br/>
    
    <fieldset class="login" style="border: thin solid #6699FF; width:873px; border-color: #6699FF;">
        <legend style="border-color: #3333FF; color: #0066FF">ACTA DE MATRIMONIO DEL CLIENTE</legend>
        <table style="width:873px;">
            <tr>
                <td style="width:290px; vertical-align:top;">
                    Nombre Completo (Conyuge):
                    <asp:TextBox ID="TxtNombreConyuge" runat="server" Width="250px" MaxLength="255"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" 
                            ControlToValidate="TxtNombreConyuge" 
                            ErrorMessage="Se debe capturar el Nombre del Conyuge"
                            ValidationGroup="Basica" 
                            Display="Dynamic" 
                            Text="*"
                            EnableClientScript="true" 
                            ID="RequiredFieldValidator37"
                            InitialValue="">
                            </asp:RequiredFieldValidator>
                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender23" 
                                                runat="server" 
                                                TargetControlID="TxtNombreConyuge" 
                                                FilterMode="ValidChars" 
                                                ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZÑ" />
                </td>
                <td style="width:290px; vertical-align:top;">
                    Número de Acta de Matrimonio:
                    <asp:TextBox ID="TxtNumeroActaMat" runat="server" Width="250px" MaxLength="20"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" 
                            ControlToValidate="TxtNumeroActaMat" 
                            ErrorMessage="Se debe capturar el Número de Acta de Matrimonio"
                            ValidationGroup="Basica" 
                            Display="Dynamic" 
                            Text="*"
                            EnableClientScript="true" 
                            ID="RequiredFieldValidator38"
                            InitialValue="">
                            </asp:RequiredFieldValidator>
                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender24" 
                                                runat="server" 
                                                TargetControlID="TxtNumeroActaMat" 
                                                FilterMode="ValidChars" 
                                                ValidChars="0123456789" />
                </td>
                <td style="width:290px; vertical-align:top;">
                    Número de Registro Civil:
                    <asp:TextBox ID="TxtRegistroCivil" runat="server" Width="250px" MaxLength="50"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" 
                            ControlToValidate="TxtRegistroCivil" 
                            ErrorMessage="Se debe capturar el Número de Registro Civil"
                            ValidationGroup="Basica" 
                            Display="Dynamic" 
                            Text="*"
                            EnableClientScript="true" 
                            ID="RequiredFieldValidator39"
                            InitialValue="">
                            </asp:RequiredFieldValidator>
                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender25" 
                                                runat="server" 
                                                TargetControlID="TxtRegistroCivil" 
                                                FilterMode="ValidChars" 
                                                ValidChars="0123456789" />
                </td>
                </tr>
        </table>
    </fieldset>
    <p>
        <asp:ValidationSummary ID="Basica" runat="server" CssClass="failureNotification" 
                                 ValidationGroup="Basica" Font-Size="Small" HeaderText="Resumén Captura:"/>
    <p>
        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
    </p>
</div>