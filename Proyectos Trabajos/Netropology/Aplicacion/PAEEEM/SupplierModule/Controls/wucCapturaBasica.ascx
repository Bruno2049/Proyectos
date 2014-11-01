<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucCapturaBasica.ascx.cs" Inherits="PAEEEM.SupplierModule.Controls.wucCapturaBasica" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.50508.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>


<style type="text/css">
    .auto-style1 {
        height: 12px;
    }

    .auto-style2 {
        width: 290px;
        height: 12px;
    }
</style>
<script type="text/javascript" language="javascript">
    var pre = "ctl00_MainContent_wizardPages_wucCapturaBasica_";

    function MostrarColonias() {
        var sUrl = "SeleccionColonia.aspx";
        var sOpciones = "toolbar=no, location=no, directories=no, status=yes, menubar=no, scrollbars=yes, resizable=yes, copyhistory=no, width=800, height=400";
        window.open(sUrl, "_blank", sOpciones);
    }
    function getValues() {

    }
</script>

<fieldset class="legend_info">
    <legend style="font-size: 14px; align-content: initial">ALTA SOLICITUD CREDITO</legend>
    <table style="width: 100%;">
        <tr>
            <td style="text-align: center">
                <asp:Label ID="Label26" CssClass="td_label" Text="Tipo de Persona: " runat="server" />

                <asp:DropDownList ID="DDXTipoPersona" runat="server" AutoPostBack="True" />
                <%--OnSelectedIndexChanged="DDXTipoPersona_SelectedIndexChanged"--%>
                <asp:RequiredFieldValidator runat="server"
                    ControlToValidate="DDXTipoPersona"
                    ErrorMessage="Se debe seleccionar el Tipo de Persona"
                    ValidationGroup="Basica"
                    Display="Dynamic"
                    Text="*"
                    EnableClientScript="true"
                    ID="RequiredFieldValidator20"
                    InitialValue=""> </asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>
</fieldset>

<br />
<div runat="server" id="PanelPersonaFisica" visible="True">
    <fieldset class="legend_info">
        <legend style="font-size: 14px; align-content: initial">DATOS DEL CLIENTE</legend>
        <table style="width: 100%">
            <tr>
                <td style="vertical-align: top;" class="auto-style1">
                    <asp:Label ID="lblTipoTarifa" Width="250px" CssClass="td_label" Text="Nombre " runat="server" />
                </td>
                <td style="vertical-align: top;" class="auto-style2">
                    <asp:Label ID="Label1" Width="250px" CssClass="td_label" Text="Apellido Paterno: " runat="server" />
                </td>
                <td style="vertical-align: top;" class="auto-style2">
                    <asp:Label ID="Label2" Width="250px" CssClass="td_label" Text="Apellido Materno: " runat="server" />
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top;" class="auto-style2">
                    <asp:TextBox ID="TxtNombrePFisica" runat="server" Width="250px" MaxLength="100"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server"
                        ControlToValidate="TxtNombrePFisica"
                        ErrorMessage="Se debe capturar el Nombre"
                        ValidationGroup="Basica"
                        Display="Dynamic"
                        Text="*"
                        EnableClientScript="true"
                        ID="RFV1"
                        InitialValue="">
                    </asp:RequiredFieldValidator>
                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender6"
                        runat="server"
                        TargetControlID="TxtNombrePFisica"
                        FilterMode="ValidChars"
                        ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZÑ " />
                </td>
                <td>
                    <asp:TextBox ID="TxtApellidoPaterno" runat="server" Width="250px" MaxLength="50"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server"
                        ControlToValidate="TxtApellidoPaterno"
                        ErrorMessage="Se debe capturar el Apellido Paterno"
                        ValidationGroup="Basica"
                        Display="Dynamic"
                        Text="*"
                        EnableClientScript="true"
                        ID="RFV2"
                        InitialValue="">
                    </asp:RequiredFieldValidator>
                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender7"
                        runat="server"
                        TargetControlID="TxtApellidoPaterno"
                        FilterMode="ValidChars"
                        ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZÑ " />
                </td>

                <td>
                    <asp:TextBox ID="TxtApellidoMaterno" runat="server" Width="250px" MaxLength="50"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender8"
                        runat="server"
                        TargetControlID="TxtApellidoMaterno"
                        FilterMode="ValidChars"
                        ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZÑ " />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" Width="250px" CssClass="td_label" Text="Sexo: " runat="server" />
                    <asp:DropDownList ID="DDXSexo" runat="server" Width="252px"></asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server"
                        ControlToValidate="DDXSexo"
                        ErrorMessage="Se debe seleccionar el Sexo"
                        ValidationGroup="Basica"
                        Display="Dynamic"
                        Text="*"
                        EnableClientScript="true"
                        ID="RFV3"
                        InitialValue="">
                    </asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:Label ID="lblFechaNcimiento" Width="250px" CssClass="td_label" Text="Fecha Nacimiento: " runat="server" />
                    <asp:TextBox ID="TxtFechaNacimieto" runat="server" Width="230px" ></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server"
                        ControlToValidate="TxtFechaNacimieto"
                        ErrorMessage="Se debe capturar la Fecha de Constitición"
                        ValidationGroup="Basica"
                        Display="Dynamic"
                        Text="*"
                        EnableClientScript="true"
                        ID="RequiredFieldValidator21"
                        InitialValue="">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator runat="server"
                        ControlToValidate="TxtFechaNacimieto" 
                        ErrorMessage="*" 
                        ValidationGroup="Basica"
                        ID="RegularExpressionValidator6"
                        ValidationExpression="^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-9]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$" Height="0px">
                      Formato de Fecha AAAA-MM-DD
                    </asp:RegularExpressionValidator>
                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3"
                        runat="server"
                        TargetControlID="TxtFechaNacimieto"
                        FilterMode="ValidChars"
                        ValidChars="0123456789-" />
                  <%--  <asp:ImageButton runat="Server" ID="Img1" ImageUrl="~/SupplierModule/images/Calendar.png" OnClick="Img1_Click" />
                    <%--OnClientClick="popupCalendar('divMuestraCal'); return false;" --%>
                    <%--<div id="divMuestraCal" runat="server" visible="False">
                        <asp:Calendar ID="Calendar1" runat="server"
                            OnSelectionChanged="Calendar1_SelectionChanged"></asp:Calendar>
                    </div>
                    <asp:RequiredFieldValidator runat="server"
                        ControlToValidate="TxtFechaNacimieto"
                        ErrorMessage="Se debe capturar la Fecha de Nacimiento"
                        ValidationGroup="Basica"
                        Display="Dynamic"
                        Text="*"
                        EnableClientScript="true"
                        ID="RFV4"
                        InitialValue="">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="TxtFechaNacimietoValidator"
                        Operator="DataTypeCheck" Type="Date"
                        ErrorMessage="Selecione Una Fecha Correcta"
                        ControlToValidate="TxtFechaNacimieto"
                        Display="dynamic" runat="server"
                        ValidationGroup="Basica" />
                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3"
                        runat="server"
                        TargetControlID="TxtFechaNacimieto"
                        FilterMode="ValidChars"
                        ValidChars="0123456789-" />--%>
                </td>
                <td>
                    <asp:Label ID="Label4" Width="250px" CssClass="td_label" Text="RFC: " runat="server" />
                    <asp:TextBox ID="TxtRFCFisica" runat="server" Width="250px" MaxLength="15" Enabled="False"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server"
                        ControlToValidate="TxtRFCFisica"
                        ErrorMessage="Se debe capturar el RFC"
                        ValidationGroup="Basica"
                        Display="Dynamic"
                        Text="*"
                        EnableClientScript="true"
                        ID="RFV5"
                        InitialValue="">
                    </asp:RequiredFieldValidator>
                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4"
                        runat="server"
                        TargetControlID="TxtRFCFisica"
                        FilterMode="ValidChars"
                        ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZÑ0123456789" />
                    <asp:RegularExpressionValidator
                        ID="RegularExpressionValidator4"
                        runat="server"
                        ControlToValidate="TxtRFCFisica"
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
                    <asp:Label ID="Label5" Width="250px" CssClass="td_label" Text="CURP " runat="server" />
                    <asp:TextBox ID="TxtCURP" runat="server" Width="250px" MaxLength="18"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server"
                        ControlToValidate="TxtCURP"
                        ErrorMessage="Se debe capturar la CURP"
                        ValidationGroup="Basica"
                        Display="Dynamic"
                        Text="*"
                        EnableClientScript="true"
                        ID="RFV6"
                        InitialValue="">
                    </asp:RequiredFieldValidator>
                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender9"
                        runat="server"
                        TargetControlID="TxtCURP"
                        FilterMode="ValidChars"
                        ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZÑ0123456789" />
                    <asp:RegularExpressionValidator
                        ID="RegularExpressionValidator5"
                        runat="server"
                        ControlToValidate="TxtCURP"
                        ErrorMessage="Formato Incorrecto de CURP"
                        Display="Dynamic"
                        ValidationGroup="Basica"
                        ValidationExpression="^[a-zA-Z]{4}\d{6}[a-zA-Z]{6}\d{2}$">
                            Formato Incorrecto de CURP  (AAAA999999AAAAAA99)
                    </asp:RegularExpressionValidator>
                </td>
                <td>
                    <asp:Label ID="Label6" Width="250px" CssClass="td_label" Text="Estado Civil: " runat="server" />
                    <asp:DropDownList ID="RBEstadoCivil" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RBEstadoCivil_SelectedIndexChanged" Height="16px" Width="250px" />
                </td>
                <td>
                    <asp:Label ID="Label7" Width="250px" CssClass="td_label" Text="Regimen Matrimonial: " runat="server" />
                    <asp:DropDownList ID="DDXRegimenMatrimonial" runat="server" Width="250px"></asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server"
                        ControlToValidate="DDXRegimenMatrimonial"
                        ErrorMessage="Se debe seleccionar el Regimen Matrimonial"
                        ValidationGroup="Basica"
                        Display="Dynamic"
                        Text="*"
                        EnableClientScript="true"
                        ID="RFV7"
                        InitialValue="">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="Label8" Width="250px" CssClass="td_label" Text="Acredita Ocupación del Negocio: " runat="server" />
                    <asp:DropDownList ID="DDXOcupacion" runat="server" Width="250px"></asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server"
                        ControlToValidate="DDXOcupacion"
                        ErrorMessage="Se debe seleccionar si acredita Ocupación del Negocio"
                        ValidationGroup="Basica"
                        Display="Dynamic"
                        Text="*"
                        EnableClientScript="true"
                        ID="RFV8"
                        InitialValue="">
                    </asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:Label ID="Label9" Width="250px" CssClass="td_label" Text="Tipo de Identificación: " runat="server" />
                    <asp:DropDownList ID="DDXTipoIdentificaFisica" runat="server" Width="250px"></asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server"
                        ControlToValidate="DDXTipoIdentificaFisica"
                        ErrorMessage="Se debe seleccionar el Tipo de Identificación"
                        ValidationGroup="Basica"
                        Display="Dynamic"
                        Text="*"
                        EnableClientScript="true"
                        ID="RFV9"
                        InitialValue="">
                    </asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:Label ID="Label10" Width="250px" CssClass="td_label" Text="Número de Identificación: " runat="server" />
                    <asp:TextBox ID="TxtNroIdentificacion" runat="server" Width="250px" MaxLength="20"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server"
                        ControlToValidate="TxtNroIdentificacion"
                        ErrorMessage="Se debe capturar el Número de Identificación"
                        ValidationGroup="Basica"
                        Display="Dynamic"
                        Text="*"
                        EnableClientScript="true"
                        ID="RFV10"
                        InitialValue="">
                    </asp:RequiredFieldValidator>
                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender10"
                        runat="server"
                        TargetControlID="TxtNroIdentificacion"
                        FilterMode="ValidChars"
                        ValidChars="0123456789" />
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="Label11" Width="250px" CssClass="td_label" Text="Email: " runat="server" />
                    <asp:TextBox ID="TxtEmail" runat="server" Width="250px" MaxLength="50"></asp:TextBox>

                    <asp:RequiredFieldValidator runat="server"
                        ControlToValidate="TxtEmail"
                        ErrorMessage="Se debe capturar el E-mail"
                        ValidationGroup="Basica"
                        Display="Dynamic"
                        Text="*"
                        EnableClientScript="true"
                        ID="RFV11"
                        InitialValue="">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator
                        ID="RegularExpressionValidator1"
                        runat="server"
                        ControlToValidate="TxtEmail"
                        ErrorMessage="Formato Incorrecto de E-mail XXX@XXX.XXX"
                        Display="Dynamic"
                        ValidationGroup="Basica"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                            Formato Incorrecto de E-mail XXX@XXX.XXX
                    </asp:RegularExpressionValidator>
                </td>
                <td>
                    <br />
                </td>
                <td>
                    <br />
                </td>
            </tr>
        </table>
    </fieldset>
</div>

<div runat="server" id="PanelPersonaMoral" visible="False">
    <fieldset class="legend_info">
        <legend style="font-size: 14px; align-content: initial">DATOS DEL CLIENTE</legend>
        <table style="width: 100%">
            <tr>
                <td style="width: 290px; vertical-align: top;">
                    <asp:Label ID="Label12" Width="250px" CssClass="td_label" Text="Razón Social: " runat="server" />
                    <asp:TextBox ID="TxtRazonSocial" runat="server" Width="250px" MaxLength="255"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server"
                        ControlToValidate="TxtRazonSocial"
                        ErrorMessage="Se debe capturar la Razón Social"
                        ValidationGroup="Basica"
                        Display="Dynamic"
                        Text="*"
                        EnableClientScript="true"
                        ID="RequiredFieldValidator1"
                        InitialValue="">
                    </asp:RequiredFieldValidator>
                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender11"
                        runat="server"
                        TargetControlID="TxtRazonSocial"
                        FilterMode="ValidChars"
                        ValidChars="0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz " />
                </td>
                <td style="width: 250px; vertical-align: top;">Fecha de Constitución:
                   <asp:TextBox ID="TxtFechaConstitucion" runat="server" Width="250px" MaxLength="12" ></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server"
                        ControlToValidate="TxtFechaConstitucion"
                        ErrorMessage="Se debe capturar la Fecha de Constitición"
                        ValidationGroup="Basica"
                        Display="Dynamic"
                        Text="*"
                        EnableClientScript="true"
                        ID="RequiredFieldValidator2"
                        InitialValue="">
                    <asp:RegularExpressionValidator runat="server"
                        ValidationExpression="^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-9]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$" Height="0px"
                        ControlToValidate="TxtFechaConstitucion" ErrorMessage="*" ValidationGroup="Basica"
                        ID="revTxtFechaCons">
                      Formato de Fecha AAAA-MM-DD
                    </asp:RegularExpressionValidator>
                    </asp:RequiredFieldValidator>
                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5"
                        runat="server"
                        TargetControlID="TxtFechaConstitucion"
                        FilterMode="ValidChars"
                        ValidChars="0123456789-" />
                    <%--<asp:ImageButton runat="Server" ID="Img2" ImageUrl="~/SupplierModule/images/Calendar.png" OnClick="Img2_Click" />
                    <div id="dateField2"  runat="server" Visible="False">
                            <asp:Calendar ID="Calendar2" runat="server" 
                             onselectionchanged="Calendar2_SelectionChanged"></asp:Calendar>   
                    </div>--%>
                    <%--  <asp:CalendarExtender ID="TxtFechaConstitucion_CalendarExtender" runat="server" Enabled="True"
                        TargetControlID="TxtFechaConstitucion" PopupButtonID="calendario01" Format="dd/MM/yyyy">
                    </asp:CalendarExtender>
                    <img id="calendario01" alt="" src="~/SupplierModule/images/Calendar.png" />--%>
                </td>
                <td style="width: 290px; vertical-align: top;">
                    <asp:Label ID="Label13" Width="250px" CssClass="td_label" Text="RFC: " runat="server" />
                    <asp:TextBox ID="TxtRFCMoral" runat="server" Width="250px" MaxLength="14" Enabled="False"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2"
                        runat="server"
                        TargetControlID="TxtRFCMoral"
                        FilterMode="ValidChars"
                        ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZÑ0123456789" />
                    <asp:RequiredFieldValidator runat="server"
                        ControlToValidate="TxtRFCMoral"
                        ErrorMessage="Se debe capturar el RFC"
                        ValidationGroup="Basica"
                        Display="Dynamic"
                        Text="*"
                        EnableClientScript="true"
                        ID="RequiredFieldValidator3"
                        InitialValue="">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator
                        ID="RegularExpressionValidator3"
                        runat="server"
                        ControlToValidate="TxtRFCMoral"
                        ErrorMessage="Formato Incorrecto de RFC (AAAA999999XXX)"
                        Display="Dynamic"
                        ValidationGroup="Basica"
                        ValidationExpression="^(([A-Z]|[a-z]){3})([0-9]{6})((([A-Z]|[a-z]|[0-9]){3}))">
                            Formato Incorrecto de RFC (AAAA999999XXX)
                    </asp:RegularExpressionValidator>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="Label14" Width="250px" CssClass="td_label" Text="Tipo Identificación: " runat="server" />
                    <asp:DropDownList ID="DDXTipoIdentificaMoral" runat="server" Width="250px"></asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server"
                        ControlToValidate="DDXTipoIdentificaMoral"
                        ErrorMessage="Se debe seleccionar el Tipo de Identificación"
                        ValidationGroup="Basica"
                        Display="Dynamic"
                        Text="*"
                        EnableClientScript="true"
                        ID="RequiredFieldValidator4"
                        InitialValue="">
                    </asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:Label ID="Label15" Width="250px" CssClass="td_label" Text="Número Identificación: " runat="server" />
                    <asp:TextBox ID="TxtNumeroIdentMoral" runat="server" Width="250px" MaxLength="20"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server"
                        ControlToValidate="TxtNumeroIdentMoral"
                        ErrorMessage="Se debe capturar el Número de Identificación"
                        ValidationGroup="Basica"
                        Display="Dynamic"
                        Text="*"
                        EnableClientScript="true"
                        ID="RequiredFieldValidator5"
                        InitialValue="">
                    </asp:RequiredFieldValidator>
                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender12"
                        runat="server"
                        TargetControlID="TxtNumeroIdentMoral"
                        FilterMode="ValidChars"
                        ValidChars="0123456789" />
                </td>
                <td>
                    <asp:Label ID="Label16" Width="250px" CssClass="td_label" Text="E-mail: " runat="server" />
                    <asp:TextBox ID="TxtEmailMoral" runat="server" Width="250px" MaxLength="50"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server"
                        ControlToValidate="TxtEmailMoral"
                        ErrorMessage="Se debe capturar el E-mail"
                        ValidationGroup="Basica"
                        Display="Dynamic"
                        Text="*"
                        EnableClientScript="true"
                        ID="RequiredFieldValidator6"
                        InitialValue="">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator
                        ID="RegularExpressionValidator2"
                        runat="server"
                        ControlToValidate="TxtEmailMoral"
                        ErrorMessage="Formato Incorrecto de E-mail"
                        Display="Dynamic"
                        ValidationGroup="Basica"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                            Formato Incorrecto de E-mail
                    </asp:RegularExpressionValidator>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="Label17" Width="250px" CssClass="td_label" Text="Acredita Ocupación del negocio: " runat="server" />
                    <asp:DropDownList ID="DDXAcredita" runat="server" Width="250px"></asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server"
                        ControlToValidate="DDXAcredita"
                        ErrorMessage="Se debe seleccionar si Acredita Ocupación del Negocio"
                        ValidationGroup="Basica"
                        Display="Dynamic"
                        Text="*"
                        EnableClientScript="true"
                        ID="RequiredFieldValidator7"
                        InitialValue="">
                    </asp:RequiredFieldValidator>
                </td>
                <td>
                    <br />
                </td>
                <td>
                    <br />
                </td>
            </tr>
        </table>
    </fieldset>
</div>
<br />

<fieldset class="legend_info">
    <legend style="font-size: 14px; align-content: initial">DATOS DEL NEGOCIO</legend>
    <table style="width: 100%">
        <tr>
            <td style="width: 290px; vertical-align: top;">
                <asp:Label ID="Label18" Width="250px" CssClass="td_label" Text="Código Postal: " runat="server" />
                <asp:TextBox ID="TxtCP" runat="server" Width="250px" Enabled="False" MaxLength="6"></asp:TextBox>
            </td>
            <td style="width: 290px; vertical-align: top;">
                <asp:Label ID="Label19" Width="250px" CssClass="td_label" Text="Estado: " runat="server" />
                <asp:DropDownList ID="DDXEstado" runat="server" Width="250px" Enabled="False"></asp:DropDownList>
            </td>
            <td style="width: 290px; vertical-align: top;">
                <asp:Label ID="Label20" Width="250px" CssClass="td_label" Text="Delegación o Municipio: " runat="server" />

                <asp:DropDownList ID="DDXMunicipio" runat="server" Width="250px" Enabled="False"></asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td>
                <asp:Label ID="Label21" Width="250px" CssClass="td_label" Text="Colonia: " runat="server" />
                <asp:DropDownList ID="DDXColonia" runat="server" Width="250px" Enabled="False"></asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="Label22" Width="250px" CssClass="td_label" Text="Calle: " runat="server" />

                <asp:TextBox ID="TxtCalle" runat="server" Width="250px" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server"
                    ControlToValidate="TxtCalle"
                    ErrorMessage="Se debe capturar la Calle del Domicilio del Negocio"
                    ValidationGroup="Basica"
                    Display="Dynamic"
                    Text="*"
                    EnableClientScript="true"
                    ID="RequiredFieldValidator8"
                    InitialValue="">
                </asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="Label23" Width="250px" CssClass="td_label" Text="Télefono: " runat="server" />

                <asp:TextBox ID="TxtTelefono" runat="server" Width="250px" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server"
                    ControlToValidate="TxtTelefono"
                    ErrorMessage="Se debe capturar el Telefono del Domicilio del Negocio"
                    ValidationGroup="Basica"
                    Display="Dynamic"
                    Text="*"
                    EnableClientScript="true"
                    ID="RequiredFieldValidator9"
                    InitialValue="">
                </asp:RequiredFieldValidator>
                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender13"
                    runat="server"
                    TargetControlID="TxtTelefono"
                    FilterMode="ValidChars"
                    ValidChars="0123456789" />
            </td>
        </tr>

        <tr>
            <td>
                <asp:Label ID="Label24" Width="250px" CssClass="td_label" Text="Número Exterior e Interior: " runat="server" />

                <asp:TextBox ID="TxtNumeroExterior" runat="server" Width="250px" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server"
                    ControlToValidate="TxtNumeroExterior"
                    ErrorMessage="Se debe capturar el Número Exterior del Domicilio del Negocio"
                    ValidationGroup="Basica"
                    Display="Dynamic"
                    Text="*"
                    EnableClientScript="true"
                    ID="RequiredFieldValidator10"
                    InitialValue="">
                </asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="Label25" Width="250px" CssClass="td_label" Text="Tipo de Propiedad: " runat="server" />

                <asp:DropDownList ID="DDXTipoPropiedad" runat="server" Width="250px"></asp:DropDownList>
                <asp:RequiredFieldValidator runat="server"
                    ControlToValidate="DDXTipoPropiedad"
                    ErrorMessage="Se debe seleccionar el Tipo de Propiedad del Domicilio del Negocio"
                    ValidationGroup="Basica"
                    Display="Dynamic"
                    Text="*"
                    EnableClientScript="true"
                    ID="RequiredFieldValidator11"
                    InitialValue="">
                </asp:RequiredFieldValidator>
            </td>
            <td>
                <br />
            </td>
        </tr>
    </table>
</fieldset>

<br />
<fieldset class="legend_info">
    <legend style="font-size: 14px; align-content: initial">DOMICILIO FISCAL</legend>
    <table style="width: 100%">
        <tr>
            <td colspan="3" style="text-align: center">
                <asp:CheckBox ID="ChkDomFiscal" runat="server" AutoPostBack="True" OnCheckedChanged="ChkDomFiscal_CheckedChanged" />
                <br />
                <asp:Label ID="Label27" Width="478px" CssClass="td_label" Text=" Marcar si el Domicilio Fiscal es el mismo que el Domicilio del Negocio " runat="server" />

            </td>
        </tr>
        <tr>
            <td colspan="3">
                <br />
            </td>
        </tr>
        <tr>
            <td style="width: 290px; vertical-align: top;">
                <asp:Label ID="Label28" Width="250px" CssClass="td_label" Text="Código Postal: " runat="server" />
                <asp:TextBox ID="TxtCPFiscal" runat="server" Width="250px" MaxLength="5" AutoPostBack="True" OnTextChanged="TxtCPFiscal_TextChanged"></asp:TextBox>
                &nbsp;
                    <asp:RequiredFieldValidator runat="server"
                    ControlToValidate="TxtCPFiscal"
                    ErrorMessage="Se debe capturar el Codigo Postal del Domicilio Fiscal"
                    ValidationGroup="Basica"
                    Display="Dynamic"
                    Text="*"
                    EnableClientScript="true"
                    ID="RequiredFieldValidator12"
                    InitialValue="">
                </asp:RequiredFieldValidator>
                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1"
                    runat="server"
                    TargetControlID="TxtCPFiscal"
                    FilterMode="ValidChars"
                    ValidChars="0123456789" />
            </td>
            <td style="width: 290px; vertical-align: top;">
                <asp:Label ID="Label29" Width="250px" CssClass="td_label" Text="Estado: " runat="server" />
                <asp:DropDownList ID="DDXEstadoFiscal" runat="server" Width="250px" OnSelectedIndexChanged="DDXEstadoFiscal_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                <asp:RequiredFieldValidator runat="server"
                    ControlToValidate="DDXEstadoFiscal"
                    ErrorMessage="Se debe seleccionar el Estado del Domicilio Fiscal"
                    ValidationGroup="Basica"
                    Display="Dynamic"
                    Text="*"
                    EnableClientScript="true"
                    ID="RequiredFieldValidator13"
                    InitialValue=""> </asp:RequiredFieldValidator>
            </td>
            <td style="width: 290px; vertical-align: top;">
                <asp:Label ID="Label30" Width="250px" CssClass="td_label" Text="Delegación o Municipio: " runat="server" />
                <asp:DropDownList ID="DDXMunicipioFiscal" Width="250px" runat="server" OnSelectedIndexChanged="DDXMunicipioFiscal_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                <asp:RequiredFieldValidator runat="server"
                    ControlToValidate="DDXMunicipioFiscal"
                    ErrorMessage="Se debe seleccionar el Municipio o Delegación del Domicilio Fiscal"
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
                <asp:Label ID="Label31" Width="250px" CssClass="td_label" Text="Colonia: " runat="server" />
                <asp:DropDownList ID="DDXColoniaFiscal" Width="250px" runat="server" OnSelectedIndexChanged="DDXColoniaFiscal_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                <asp:DropDownList ID="DDXColoniaFiscalHidden" Width="250px" runat="server" Visible="False"></asp:DropDownList>
                <asp:RequiredFieldValidator runat="server"
                    ControlToValidate="DDXColoniaFiscal"
                    ErrorMessage="Se debe seleccionar la Colonia del Domicilio Fiscal"
                    ValidationGroup="Basica"
                    Display="Dynamic"
                    Text="*"
                    EnableClientScript="true"
                    ID="RequiredFieldValidator15"
                    InitialValue=""> </asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="Label32" Width="250px" CssClass="td_label" Text="Calle: " runat="server" />
                <asp:TextBox ID="TxtCalleFiscal" runat="server" Width="250px" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server"
                    ControlToValidate="TxtCalleFiscal"
                    ErrorMessage="Se debe capturar la Calle del Domicilio Fiscal"
                    ValidationGroup="Basica"
                    Display="Dynamic"
                    Text="*"
                    EnableClientScript="true"
                    ID="RequiredFieldValidator16"
                    InitialValue="">
                </asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="Label33" Width="250px" CssClass="td_label" Text="Télefono: " runat="server" />
                <asp:TextBox ID="TxttelefonoFiscal" runat="server" Width="250px" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server"
                    ControlToValidate="TxttelefonoFiscal"
                    ErrorMessage="Se debe capturar el Telefono del Domicilio Fiscal"
                    ValidationGroup="Basica"
                    Display="Dynamic"
                    Text="*"
                    EnableClientScript="true"
                    ID="RequiredFieldValidator17"
                    InitialValue="">
                </asp:RequiredFieldValidator>
                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender14"
                    runat="server"
                    TargetControlID="TxttelefonoFiscal"
                    FilterMode="ValidChars"
                    ValidChars="0123456789" />
            </td>
        </tr>

        <tr>
            <td>
                <asp:Label ID="Label34" Width="250px" CssClass="td_label" Text="Número Exterior e Interior: " runat="server" />
                <asp:TextBox ID="TxtExteriorFiscal" runat="server" Width="250px" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server"
                    ControlToValidate="TxttelefonoFiscal"
                    ErrorMessage="Se debe capturar el Número Exterior e Interior del Domicilio Fiscal"
                    ValidationGroup="Basica"
                    Display="Dynamic"
                    Text="*"
                    EnableClientScript="true"
                    ID="RequiredFieldValidator18"
                    InitialValue="">
                </asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="Label35" Width="250px" CssClass="td_label" Text="Tipo de Propiedad: " runat="server" />
                <asp:DropDownList ID="DDXTipoPropiedadFiscal" runat="server" Width="250px"></asp:DropDownList>
                <asp:RequiredFieldValidator runat="server"
                    ControlToValidate="DDXTipoPropiedadFiscal"
                    ErrorMessage="Se debe seleccionar el Tipo de Propiedad del Domicilio Fiscal"
                    ValidationGroup="Basica"
                    Display="Dynamic"
                    Text="*"
                    EnableClientScript="true"
                    ID="RequiredFieldValidator19"
                    InitialValue="">
                </asp:RequiredFieldValidator>
            </td>
            <td>
                <br />
            </td>
        </tr>
    </table>
</fieldset>

<p>
    <asp:ValidationSummary ID="Basica" runat="server" CssClass="failureNotification"
        ValidationGroup="Basica" Font-Size="Small" HeaderText="Resumén Captura:" />
    <p style="text-align: center; width: 873px">
        <asp:Button ID="BtnGuardaTermporal" runat="server" Text="Guardado Temporal" OnClick="BtnGuardaTermporal_Click" />
    </p>