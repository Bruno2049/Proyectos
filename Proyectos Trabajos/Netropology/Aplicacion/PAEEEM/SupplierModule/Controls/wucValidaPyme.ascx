<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucValidaPyme.ascx.cs" Inherits="PAEEEM.SupplierModule.Controls.WucValidaPyme" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.50508.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<style type="text/css">
    .auto-style1 {
        width: 295px;
    }
    .auto-style4 {
        width: 279px;
    }
    .auto-style5 {
        width: 286px;
    }
</style>

<script type="text/javascript">
    var pre = "ctl00_MainContent_wizardPages_wucValidaPyme_";

    function MostrarColonias() {
       // var codigoPostal = document.getElementById(pre + 'TxtCP').value;
        var sUrl = "SeleccionColonia.aspx";
        var sOpciones = "toolbar=no, location=no, directories=no, status=yes, menubar=no, scrollbars=yes, resizable=yes, copyhistory=no, width=800, height=400";
        window.open(sUrl, "_blank", sOpciones);
    }
    function getValues() {

    }

    function NumCheck(e, field) {
        var key = e.keyCode ? e.keyCode : e.which;
        // backspace 
        if (key == 8) return true;
        // 0-9 
        if (key > 47 && key < 58) {
            if (field.value == "") return true;
            var regexp = /.[0-9]{12}$/;
            return !(regexp.test(field.value));
        } // . 
        if (key == 46) {
            if (field.value == "") return false;
            regexp = /^[0-9]+$/;
            return regexp.test(field.value);
        } // other key 
        return false;
    }

</script>
<div>
         <fieldset class="legend_info">
        <legend style="font-size: 14px; align-content: initial">
                <asp:Image ID="Image1" runat="server" ImageUrl="~/SupplierModule/images/t1.png" />
            </legend>
            <table style="width: 873px;">
                <tr>
                    <td colspan="3">
                        <br />
                    </td>
                </tr>
                </table>
             <table style="width: 873px;">
                 <tr>
                <td colspan="3">
                    Número de Servicio RPU:&nbsp;&nbsp;
                    <asp:TextBox ID="TxtNoRPU" runat="server" Width="164px" ReadOnly="True" Enabled="False"></asp:TextBox>
                </td>        
            </tr>
                 </table>
             <br/>
              <table style="width: 873px;">
                <tr>
                    <td colspan="3" style="color: #0099FF; font-weight: bold;">DATOS DEL NEGOCIO
                    </td>
                </tr>
             </table>
             <br/>
             <table>
                 <tr>
                      <td class="auto-style1">
                <asp:Label ID="Label26" Width="250px" CssClass="td_label" Text="Tipo de Persona: " runat="server" />
                <asp:DropDownList ID="DDXTipoPersona" runat="server" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="DDXTipoPersona_SelectedIndexChanged"></asp:DropDownList>
                <asp:RequiredFieldValidator runat="server"
                    ControlToValidate="DDXTipoPersona"
                    ErrorMessage="Se debe seleccionar el Tipo de Persona"
                    ValidationGroup="Pyme"
                    Display="Dynamic"
                    Text="*"
                    EnableClientScript="true"
                    ID="RequiredFieldValidator20"
                    InitialValue=""> </asp:RequiredFieldValidator>
            </td>
                
                     <td class="auto-style4">
                         <div id="divRFCFisica" runat="server">
                    <asp:Label ID="lblRFCPErsonaFisica" Width="250px" CssClass="td_label" Text="RFC: " runat="server" />
                    <asp:TextBox ID="TxtRFCFisica" runat="server" Width="250px" MaxLength="15" Enabled="False"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server"
                        ControlToValidate="TxtRFCFisica"
                        ErrorMessage="Se debe capturar el RFC"
                        ValidationGroup="Pyme"
                        Display="Dynamic"
                        Text="*"
                        EnableClientScript="true"
                        ID="RFV5"
                        InitialValue="">
                    </asp:RequiredFieldValidator>
<%--                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4"
                        runat="server"
                        TargetControlID="TxtRFCFisica"
                        FilterMode="ValidChars"
                        ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZÑ0123456789" />--%>
                    <asp:RegularExpressionValidator
                        ID="RegularExpressionValidator4"
                        runat="server"
                        ControlToValidate="TxtRFCFisica"
                        ErrorMessage="Formato Incorrecto de RFC (AAAA999999XXX)"
                        Display="Dynamic"
                        ValidationGroup="Pyme"
                        ValidationExpression="^(([A-Z]|[a-z]){4})([0-9]{6})((([A-Z]|[a-z]|[0-9]){3}))">
                            Formato Incorrecto de RFC (AAAA999999XXX)
                    </asp:RegularExpressionValidator>
                         </div>
                         <div id="divRFCMoral" runat="server" Visible="False">
                    <asp:Label ID="lblRFCPErsonaMoral" Width="250px" CssClass="td_label" Text="RFC: "  runat="server" style="margin-left: 0px"/>
                         <br />
                    <asp:TextBox ID="TxtRFCMoral" runat="server" Width="250px" MaxLength="12"  Enabled="False"></asp:TextBox>
                    <%--<asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2"
                        runat="server"
                        TargetControlID="TxtRFCMoral"
                        FilterMode="ValidChars"
                        ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZÑ0123456789" />--%>
                    <asp:RequiredFieldValidator runat="server"
                        ControlToValidate="TxtRFCMoral"
                        ErrorMessage="Se debe capturar el RFC"
                        ValidationGroup="Pyme"
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
                        ErrorMessage="Formato Incorrecto de RFC (AAA999999XXX)"
                        Display="Dynamic"
                        ValidationGroup="Pyme"
                        ValidationExpression="^(([A-Z]|[a-z]){3})([0-9]{6})((([A-Z]|[a-z]|[0-9]){3}))">
                            Formato Incorrecto de RFC (AAA999999XXX)
                    </asp:RegularExpressionValidator>
                             </div>
                </td>
                     <td class="auto-style5"></td>
           </tr>
             </table>
             <br/>
             <table style="width: 873px;">
                <tr>
                    <td>
                        <asp:Label ID="lblTipoTarifa" Width="250px" CssClass="td_label" Text="Giro de la Empresa: " runat="server" />
                        <asp:DropDownList ID="DDXGiroEmpresa" runat="server" Width="250px" Font-Size="Small"></asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server"
                            ControlToValidate="DDXGiroEmpresa"
                            ErrorMessage="Se debe seleccionar el Giro de la Empresa"
                            ValidationGroup="Pyme"
                            Display="Dynamic"
                            Text="*"
                            EnableClientScript="true"
                            ID="RFVDDXGiroEmpresa"
                            InitialValue="">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                    <td></td>
                </tr>
                 <tr>
                     <td colspan="3" style="height: 4px"> </td>
                 </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label1" Width="250px" CssClass="td_label" Text="Nombre Comercial: " runat="server" />
                        
                        <asp:TextBox ID="TxtNombreComercial" Width="250px" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server"
                            ControlToValidate="TxtNombreComercial"
                            ErrorMessage="Se debe capturar el Nombre Comercial"
                            ValidationGroup="Pyme"
                            Display="Dynamic"
                            Text="*"
                            EnableClientScript="true"
                            ID="RFVTxtNombreComercial"
                            InitialValue="">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Label ID="Label2" Width="250px" CssClass="td_label" Text="Sector " runat="server" />
                        <asp:DropDownList ID="DDXSector" runat="server" Width="250px" Font-Size="Small"></asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server"
                            ControlToValidate="DDXSector"
                            ErrorMessage="Se debe seleccionar el Sector Ecónomico"
                            ValidationGroup="Pyme"
                            Display="Dynamic"
                            Text="*"
                            EnableClientScript="true"
                            ID="RFVDDXSector"
                            InitialValue="">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Label ID="Label3" Width="250px" CssClass="td_label" Text="Número de Empleados: " runat="server" />
                        <asp:TextBox ID="TxtNoEmpleados" Width="250px" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server"
                            ControlToValidate="TxtNoEmpleados"
                            ErrorMessage="Se debe capturar el Numero de Empleados"
                            ValidationGroup="Pyme"
                            Display="Dynamic"
                            Text="*"
                            EnableClientScript="true"
                            ID="RFVTxtNoEmpleados"
                            InitialValue="">
                        </asp:RequiredFieldValidator>
                      <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender13"
                    runat="server"
                    TargetControlID="TxtNoEmpleados"
                    FilterMode="ValidChars"
                    ValidChars="0123456789" />
                    </td>
                </tr>
                 <tr>
                     <td colspan="3" style="height: 4px"> </td>
                 </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label4" Width="250px" CssClass="td_label" Text="Total de Gastos Mensuales: " runat="server" />
                        <asp:TextBox ID="TxtGastosMensuales" Width="250px"  runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server"
                            ControlToValidate="TxtGastosMensuales"
                            ErrorMessage="Se debe capturar los Gastos Mensuales"
                            ValidationGroup="Pyme"
                            Display="Dynamic"
                            Text="*"
                            EnableClientScript="true"
                            ID="RFVTxtGastosMensuales"
                            InitialValue="">
                        </asp:RequiredFieldValidator>
                          <asp:RegularExpressionValidator
                        ID="RegularExpressionValidator1"
                        runat="server"
                        ControlToValidate="TxtGastosMensuales"
                        ErrorMessage="Solo Números"
                        Display="Dynamic"
                        ValidationGroup="Pyme"
                         ValidationExpression= "^[0-9]{1,18}(\.[0-9]{0,2})?$">
                    </asp:RegularExpressionValidator>
                         <asp:FilteredTextBoxExtender 
                            ID="FilteredTextBoxExtender155"
                    runat="server"
                    TargetControlID="TxtGastosMensuales"
                    FilterMode="ValidChars"
                    ValidChars="0123456789." >
                        </asp:FilteredTextBoxExtender>
                    </td>
                    <td>
                        <asp:Label ID="Label5" Width="250px" CssClass="td_label" Text="Promedio de Ventas Anuales: " runat="server" />
                        <asp:TextBox ID="TxtVentasAnuales" Width="204px" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server"
                            ControlToValidate="TxtVentasAnuales"
                            ErrorMessage="Se deben capturar las Ventas Anuales"
                            ValidationGroup="Pyme"
                            Display="Dynamic"
                            Text="*"
                            EnableClientScript="true"
                            ID="RFVTxtVentasAnuales"
                            InitialValue="">
                        </asp:RequiredFieldValidator>
                          <asp:RegularExpressionValidator
                        ID="RegularExpressionValidator2"
                        runat="server"
                        ControlToValidate="TxtVentasAnuales"
                        ErrorMessage="Solo Números"
                        Display="Dynamic"
                        ValidationGroup="Pyme"
                         ValidationExpression= "^[0-9]{1,18}(\.[0-9]{0,2})?$">
                    </asp:RegularExpressionValidator>
                        <asp:FilteredTextBoxExtender 
                            ID="FilteredTextBoxExtender145"
                    runat="server"
                    TargetControlID="TxtVentasAnuales"
                    FilterMode="ValidChars"
                    ValidChars="0123456789." >
                        </asp:FilteredTextBoxExtender>
                    </td>
                    <td>
                        <br />
                    </td>
                </tr>
                 </table>
             <br/>
             <table style="width: 873px;">
                <tr>
                    <td colspan="3" style="color: #0099FF; font-weight: bold;">DATOS DEL DOMICILIO DEL NEGOCIO
                    </td>
                </tr>
                <tr>
                     <td colspan="3" style="height: 4px"> </td>
                 </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label6" Width="250px" CssClass="td_label" Text="Código Postal: " runat="server" />
                        <asp:TextBox ID="TxtCP" runat="server" Width="250px"></asp:TextBox>
                        &nbsp;
                   <%-- <asp:ImageButton ID="ImgBtnBuscarCP" runat="server" ImageUrl="~/SupplierModule/images/buscar.png" OnClientClick ="MostrarColonias()" />--%>
                        <asp:RequiredFieldValidator runat="server"
                            ControlToValidate="TxtCP"
                            ErrorMessage="Se deben capturar el Codigo Postal"
                            ValidationGroup="Pyme"
                            Display="Dynamic"
                            Text="*"
                            EnableClientScript="true"
                            ID="RFVTxtCP"
                            InitialValue="">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Label ID="Label7" Width="250px" CssClass="td_label" Text="Estado " runat="server" />
                        <asp:DropDownList ID="DDXEstado" runat="server" AutoPostBack="True" Width="250px" OnSelectedIndexChanged="DDXEstado_SelectedIndexChanged" Font-Size="Small"></asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server"
                            ControlToValidate="DDXEstado"
                            ErrorMessage="Se deben seleccionar el Estado"
                            ValidationGroup="Pyme"
                            Display="Dynamic"
                            Text="*"
                            EnableClientScript="true"
                            ID="RFVDDXEstado"
                            InitialValue="">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Label ID="Label8" Width="250px" CssClass="td_label" Text="Delegación o Municipio: " runat="server" />
                        
                        <asp:DropDownList ID="DDXMunicipio" Width="250px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDXMunicipio_SelectedIndexChanged" Font-Size="Small"></asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server"
                            ControlToValidate="DDXMunicipio"
                            ErrorMessage="Se deben seleccionar el Municipio o Delegación"
                            ValidationGroup="Pyme"
                            Display="Dynamic"
                            Text="*"
                            EnableClientScript="true"
                            ID="RFVDDXMunicipio"
                            InitialValue="">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                     <td colspan="3" style="height: 4px"> </td>
                 </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label9" Width="250px" CssClass="td_label" Text="Colonia: " runat="server" />
                        <asp:DropDownList ID="DDXColonia" Width="250px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDXColonia_SelectedIndexChanged" Font-Size="Small"></asp:DropDownList>
                        <asp:DropDownList ID="DDXColoniaHidden" runat="server" Visible="False"></asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server"
                            ControlToValidate="DDXColonia"
                            ErrorMessage="Se deben seleccionar la Colonia"
                            ValidationGroup="Pyme"
                            Display="Dynamic"
                            Text="*"
                            EnableClientScript="true"
                            ID="RFVDDXColonia"
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
                <tr>
                    <td colspan="3">
                        <asp:ValidationSummary ID="Pyme" runat="server" CssClass="failureNotification"
                            ValidationGroup="Pyme" Font-Size="Small" HeaderText="Resumén Captura:" />
                    </td>
                </tr>
            </table>
        </fieldset>
    <%--    <asp:Panel runat="server" ID="panelBusquedaColonia" Visible="False" HorizontalAlign="Center" Width="100%">
        <table runat="server" style="text-align: center; width: 100%">
            <tr>
                <td style="text-align: center">
                    <asp:GridView ID="grdColonias" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                                AllowPaging="True" PageSize="8" DataKeyNames="CveCp" Width="100%" HorizontalAlign="Center">
                                <FooterStyle CssClass="GridViewFooterStyle" />
                                <RowStyle CssClass="GridViewRowStyle2" />
                                <HeaderStyle BackColor="#0033CC" ForeColor="White" Font-Size="Small" />
                                <PagerStyle CssClass="GridViewPagerStyle" />
                                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle2" />
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
                                            <asp:ImageButton ID="BtnImgSeleccionar" runat="server" ImageUrl="~/SupplierModule/images/Select.png" 
                                                OnClientClick="return confirm('Esta seguro de seleccionar el Asentamiento');" OnClick="BtnImgSeleccionar_Click" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                </td>
            </tr>
        </table>    
        <div  runat="server" style="width: 100%; text-align: right;">
            <asp:Button ID="BtnRegresar" runat="server" Text="Regresar" OnClick="BtnRegresar_Click" />
        </div>
    </asp:Panel>--%>
</div>
