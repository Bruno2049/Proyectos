<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="SupplierAdd.aspx.cs"
    Inherits="PAEEEM.RegionalModule.SupplierAdd" Title="Alta de Proveedores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Resources/Css/PageMask.css" type="text/css" rel="Stylesheet" />
    <style type="text/css">
        .Label
        {
            width: 160px;
            color: #333333;
            font-size: 16px;
        }
        .CheckBox
        {
            width: 200px;
            color: #333333;
            font-size: 16px;
        }
        .Label_1
        {
            width: 150px;
            color: #333333;
            font-size: 16px;
        }
        .Label_2
        {
            width: 260px;
            color: #333333;
            font-size: 16px;
        }
        .Label_3
        {
            width: 120px;
            color: #333333;
            font-size: 16px;
        }
        .DropDownList
        {
            width: 300px;
        }
        .DropDownList_1
        {
            width: 500px;
        }
        .TextBox
        {
            width: 500px;
        }
        .TextBox_1
        {
            width: 180px;
        }
        .Button
        {
            width: 180px;
        }
    </style>

    <script type="text/javascript">
        function lockScreen() {
            var lock = document.getElementById('lock');        
             lock.style.width = '300px';
            lock.style.height = '30px';        
            // lock.style.top = document.documentElement.clientHeight/2 - lock.style.height.replace('px','')/2 + 'px';
            lock.style.top = document.body.offsetHeight/1.5 - lock.style.height.replace('px','')/2 + 'px';
            lock.style.left = document.body.offsetWidth/2 - lock.style.width.replace('px','')/2 + 'px';
            if (lock)
                lock.className = 'LockOn'; 
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="container">
        <asp:UpdatePanel ID="panel1" runat="server">
            <ContentTemplate>
                <div id="lock" class="LockOff">
                    <img src="../images/progress.gif" alt="¡ En Proceso, Por favor Espere !" style="vertical-align: middle;
                        position: relative;" />
                </div>
                <div>
                    <br>
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/supplieradd.png" />
                </div>
                <div align="right">
                    <asp:Label ID="Label1" Text="Fecha" runat="server" CssClass="Label" />&nbsp
                    <asp:Literal ID="literalFecha" runat='server' />
                </div>
                <br />
                <table width="100%">
                <div>
                      <asp:Image ID="Image2" runat="server" ImageUrl="../SupplierModule/images/datos_empresa.png" />
                </div>
                <br />
                      <tr>
                        <td width="30%">
                            <asp:CheckBox ID="ckbBranch" runat="server" Text="Sucursal" CssClass="CheckBox"
                                AutoPostBack="True" OnCheckedChanged="ckbBranch_CheckedChanged" />
                        </td>
                        <td width="15%">
                            <asp:Label ID="lblSupplier" runat="server" Text="Nombre o Razón Social (Matriz)" CssClass="Label_1"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpSupplier" runat="server" CssClass="DropDownList">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="drpSupplier" ErrorMessage="(*) Campo Obligatorio"
                                ValidationGroup="Save" ID="drpSupplierRequired">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td width="25%">
                            <asp:Label ID="lblClave" runat="server" Text="Clave" CssClass="Label_2"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtClave" runat="server" CssClass="TextBox"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtClave" ErrorMessage="(*) Campo Obligatorio"
                                ValidationGroup="Save" ID="txtClaveRequired">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
                <br />
                <table width="100%">
                    <tr>
                        <td width="25%">
                            <asp:Label ID="lblCaptura" runat="server" Text="Nombre o Razón Social (Matriz)"
                                CssClass="Label_2"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSocialName" runat="server" CssClass="TextBox"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtSocialName" ErrorMessage="(*) Campo Obligatorio"
                                ValidationGroup="Save" ID="txtSocialNameRequired">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%">
                            <asp:Label ID="Label3" runat="server" Text="Nombre Comercial" CssClass="Label_2"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtBusinessName" runat="server" CssClass="TextBox"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtBusinessName" ErrorMessage="(*) Campo Obligatorio"
                                ValidationGroup="Save" ID="txtBusinessNameRequired">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%">
                            <asp:Label ID="Label4" runat="server" Text="Registro Federal Causantes (RFC)" CssClass="Label_2"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtRFC" runat="server" CssClass="TextBox"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtRFC" ErrorMessage="(*) Campo Obligatorio"
                                ValidationGroup="Save" ID="txtRFCRequired">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
                <br />
                <table width="100%">
                <div>
                    <br>
                    <asp:Image ID="Image3" runat="server" ImageUrl="../SupplierModule/images/domicilio_negocio.png" />
                </div>
                <br />
                    <tr>
                        <td width="11%">
                            <asp:Label ID="Label5" runat="server" Text="Calle del Domicilio" CssClass="Label_3"></asp:Label>
                        </td>
                        <td width="18%">
                            <asp:TextBox ID="txtPartCalle" runat="server" CssClass="TextBox_1"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPartCalle" ErrorMessage="(*) Campo Obligatorio"
                                ValidationGroup="Save" ID="txtPartCalleRequired">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td width="11%">
                            <asp:Label ID="Label6" runat="server" Text="Número" CssClass="Label_3"></asp:Label>
                        </td>
                        <td width="20%">
                            <asp:TextBox ID="txtPartNum" runat="server" CssClass="TextBox_1"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPartNum" ErrorMessage="(*) Campo Obligatorio"
                                ValidationGroup="Save" ID="txtPartNumRequired">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td width="11%">
                            <asp:Label ID="Label7" runat="server" Text="Código Postal" CssClass="Label_3"></asp:Label>
                        </td>
                        <td width="29%">
                            <div>
                                <asp:TextBox ID="txtPartCP" runat="server" CssClass="TextBox_1"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPartCP" ErrorMessage="(*) Campo Obligatorio"
                                ValidationGroup="Save" ID="txtPartCPRequired">
                            </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator runat="server" ValidationExpression="^\d{5}$" ControlToValidate="txtPartCP"
                                    ErrorMessage="(*) Campo Vacío o con Formato Inválido" ID="RegularExpressionValidator3">
                                </asp:RegularExpressionValidator>
                            </div>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td width="11%">
                            <asp:Label ID="Label8" runat="server" Text="Estado" CssClass="Label_3"></asp:Label>
                        </td>
                        <td width="35%">
                            <asp:DropDownList ID="drpPartEstado" runat="server" CssClass="DropDownList" AutoPostBack="True"
                                OnSelectedIndexChanged="drpPartEstado_SelectedIndexChanged" onChange="lockScreen();">
                            </asp:DropDownList>
							<asp:RequiredFieldValidator runat="server" ControlToValidate="drpPartEstado" ErrorMessage="(*) Campo Obligatorio"
							ValidationGroup="Save" ID="drpPartEstadoRequired">
							</asp:RequiredFieldValidator>
                        </td>
                        <td width="13%">
                            <asp:Label ID="Label9" runat="server" Text="Delegación o Municipio" CssClass="Label_3"></asp:Label>
                        </td>
                        <td width="41%">
                            <asp:DropDownList ID="drpPartDelegMunicipio" runat="server" CssClass="DropDownList">
                            </asp:DropDownList>
							<asp:RequiredFieldValidator runat="server" ControlToValidate="drpPartDelegMunicipio" ErrorMessage="(*) Campo Obligatorio"
							ValidationGroup="Save" ID="drpPartDelegMunicipioRequired">
							</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
                <br />
                <div>
                      <asp:Image ID="Image4" runat="server" ImageUrl="../SupplierModule/images/domicilio_fiscal.png" />
                </div>
                <br />
                <table width="100%">
                    <tr>
                        <td width="5%">
                        </td>
                        <td width="23%">
                            <asp:Label ID="Label10" runat="server" Text="Marcar si el Domicilio Fiscal es el mismo que el Domicilio del Negocio" CssClass="Label_2"></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox ID="ckbFiscal" runat="server" AutoPostBack="True" OnCheckedChanged="ckbFiscal_CheckedChanged" onclick="lockScreen();" />
                        </td>
                    </tr>
                </table>
                <br />
                <table width="100%">
                    <tr>
                        <td width="11%">
                            <asp:Label ID="Label11" runat="server" Text="Calle del Domicilio" CssClass="Label_3"></asp:Label>
                        </td>
                        <td width="18%">
                            <asp:TextBox ID="txtFiscalCalle" runat="server" CssClass="TextBox_1"></asp:TextBox>
							<asp:RequiredFieldValidator runat="server" ControlToValidate="txtFiscalCalle" ErrorMessage="(*) Campo Obligatorio"
							ValidationGroup="Save" ID="txtFiscalCalleRequired">
							</asp:RequiredFieldValidator>
                        </td>
                        <td width="11%">
                            <asp:Label ID="Label12" runat="server" Text="Número" CssClass="Label_3"></asp:Label>
                        </td>
                        <td width="20%">
                            <asp:TextBox ID="txtFiscalNum" runat="server" CssClass="TextBox_1"></asp:TextBox>
							<asp:RequiredFieldValidator runat="server" ControlToValidate="txtFiscalNum" ErrorMessage="(*) Campo Obligatorio"
							ValidationGroup="Save" ID="txtFiscalNumRequired">
							</asp:RequiredFieldValidator>
                        </td>
                        <td width="11%">
                            <asp:Label ID="Label13" runat="server" Text="Código Postal" CssClass="Label_3"></asp:Label>
                        </td>
                        <td width="29%">
                            <div>
                                <asp:TextBox ID="txtFiscalCP" runat="server" CssClass="TextBox_1"></asp:TextBox>
								<asp:RequiredFieldValidator runat="server" ControlToValidate="txtFiscalCP" ErrorMessage="(*) Campo Obligatorio"
								ValidationGroup="Save" ID="txtFiscalCPRequired">
								</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator runat="server" ValidationExpression="^\d{5}$" ControlToValidate="txtFiscalCP"
                                    ErrorMessage="(*) Campo Vacío o con Formato Inválido" ID="RegularExpressionValidator2">
                                </asp:RegularExpressionValidator>
                            </div>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td width="11%">
                            <asp:Label ID="Label14" runat="server" Text="Estado" CssClass="Label_3"></asp:Label>
                        </td>
                        <td width="35%">
                            <asp:DropDownList ID="drpFiscalEstado" runat="server" CssClass="DropDownList" AutoPostBack="True"
                                OnSelectedIndexChanged="drpFiscalEstado_SelectedIndexChanged" onChange="lockScreen();">
                            </asp:DropDownList>
							<asp:RequiredFieldValidator runat="server" ControlToValidate="drpFiscalEstado" ErrorMessage="(*) Campo Obligatorio"
							ValidationGroup="Save" ID="drpFiscalEstadoRequired">
							</asp:RequiredFieldValidator>
                        </td>
                        <td width="13%">
                            <asp:Label ID="Label15" runat="server" Text="Delegación o Municipio" CssClass="Label_3"></asp:Label>
                        </td>
                        <td width="41%">
                            <asp:DropDownList ID="drpFiscalDelegMunicipio" runat="server" CssClass="DropDownList">
                            </asp:DropDownList>
							<asp:RequiredFieldValidator runat="server" ControlToValidate="drpFiscalDelegMunicipio" ErrorMessage="(*) Campo Obligatorio"
							ValidationGroup="Save" ID="drpFiscalDelegMunicipioRequired">
							</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
                <br />
                <div>
                      <asp:Image ID="Image5" runat="server" ImageUrl="../SupplierModule/images/informacion_responsable.png" />
                </div>
                <br />
                <table width="100%">
                    <tr>
                        <td width="25%">
                            <asp:Label ID="Label16" runat="server" Text="Nombre del Responsable" CssClass="Label_2"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtRepresentativeName" runat="server" CssClass="TextBox"></asp:TextBox>
							<asp:RequiredFieldValidator runat="server" ControlToValidate="txtRepresentativeName" ErrorMessage="(*) Campo Obligatorio"
							ValidationGroup="Save" ID="txtRepresentativeNameRequired">
							</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%">
                            <asp:Label ID="Label17" runat="server" Text="E-mail del Responsable" CssClass="Label_2"></asp:Label>
                        </td>
                        <td>
                            <div>
                                <asp:TextBox ID="txtRepresentativeEmail" runat="server" CssClass="TextBox"></asp:TextBox>
								<asp:RequiredFieldValidator runat="server" ControlToValidate="txtRepresentativeEmail" ErrorMessage="(*) Campo Obligatorio"
								ValidationGroup="Save" ID="txtRepresentativeEmailRequired">
								</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator runat="server" ValidationExpression="^(([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+([;.](([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+)*$"
                                    ControlToValidate="txtRepresentativeEmail" ErrorMessage="(*) Campo Vacío o con Formato Inválido" ID="RegularExpressionValidator10">
                                </asp:RegularExpressionValidator>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%">
                            <asp:Label ID="Label18" runat="server" Text="Teléfono" CssClass="Label_2"></asp:Label>
                        </td>
                        <td>
                            <div>
                                <asp:TextBox ID="txtTelephone" runat="server" CssClass="TextBox" ToolTip="(10 Dígitos Numéricos)"></asp:TextBox>
								<asp:RequiredFieldValidator runat="server" ControlToValidate="txtTelephone" ErrorMessage="(*) Campo Obligatorio"
								ValidationGroup="Save" ID="txtTelephoneRequired">
								</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator runat="server" ValidationExpression="^((\d{10}))$"
                                    ControlToValidate="txtTelephone" ErrorMessage="(*) Campo Vacío o con Formato Inválido" ID="RegularExpressionValidator12">
                                </asp:RegularExpressionValidator>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%">
                            <asp:Label ID="Label19" runat="server" Text="Nombre del Representante Legal" CssClass="Label_2"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtLegalName" runat="server" CssClass="TextBox"></asp:TextBox>
							<asp:RequiredFieldValidator runat="server" ControlToValidate="txtLegalName" ErrorMessage="(*) Campo Obligatorio"
							ValidationGroup="Save" ID="txtLegalNameRequired">
							</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
                <div>
                      <asp:Image ID="Image6" runat="server" ImageUrl="../SupplierModule/images/informacion_general.png" />
                </div>
                <table width="100%">
        <tr>
                        <td width="25%">
                            <asp:Label ID="Label27" runat="server" Text="Pct Tasa IVA" CssClass="Label_2"></asp:Label>
                        </td>
                        <td>
                            <%--<asp:TextBox ID="txtRate" runat="server" CssClass="TextBox"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtRate"
                                ErrorMessage="(*) Campo Vacío o con Formato Inválido" ValidationExpression="^[+|-]?\d*\.?\d*$"></asp:RegularExpressionValidator>--%>
                            <asp:DropDownList ID="drpRate" runat="server">
                            
                                <asp:ListItem Value="0" Text=""> </asp:ListItem>
                                <asp:ListItem Value="11" Text="11%"></asp:ListItem>
                                <asp:ListItem Value="16" Text="16%"></asp:ListItem>
                            
                            </asp:DropDownList>
                            <asp:RegularExpressionValidator runat="server" ControlToValidate="drpRate" ValidationGroup="Save"
                                ErrorMessage="(*) Campo Obligatorio" ID="drpRateValid" ValidationExpression="^[1-9][0-9]?$">
                            </asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <br />
            <tr>
                <td width="25%">
                    <asp:Label ID="Label20" runat="server" Text="Cargar Imagen Digital del Poder Notarial" CssClass="Label_2"></asp:Label>
                </td>
                <td>
                    <asp:FileUpload ID="fileUploadAttorneyPower" runat="server" Width="570px" />
                </td>
            </tr>
            <tr>
                <td width="25%">
                    <asp:Label ID="Label21" runat="server" Text="Cargar Imagen Digital del Acta Constitutiva" CssClass="Label_2"></asp:Label>
                </td>
                <td>
                    <asp:FileUpload ID="fileUploadConstitutiva" runat="server" Width="570px" />
                </td>
            </tr>
        </table>
        <br />
        <asp:UpdatePanel ID="panel2" runat="server">
            <ContentTemplate>
                <div>
                      <asp:Image ID="Image7" runat="server" ImageUrl="../SupplierModule/images/informacion_financiera.png" />
                </div>
                <br />
                <table width="100%">
                    <tr>
                        <td width="24%" colspan="2">
                            <asp:Label ID="Label22" runat="server" CssClass="Label_2" Text="Información Financiera de Depósito"></asp:Label>
                        </td>
                        <td width="76%">
                        </td>
                    </tr>
                    <tr>
                        <td width="3%">
                        </td>
                        <td width="21%">
                            <asp:Label ID="Label23" runat="server" CssClass="Label_1" Text="Nombre del Banco"></asp:Label>
                        </td>
                        <td width="76%">
                            <asp:TextBox ID="txtBankName" runat="server" CssClass="TextBox"></asp:TextBox>
							<asp:RequiredFieldValidator runat="server" ControlToValidate="txtBankName" ErrorMessage="(*) Campo Obligatorio"
							ValidationGroup="Save" ID="txtBankNameRequired">
							</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td width="3%">
                        </td>
                        <td width="21%">
                            <asp:Label ID="Label24" runat="server" CssClass="Label_1" Text="Número Cuenta CLABE"></asp:Label>
                        </td>
                        <td width="76%">
                            <asp:TextBox ID="txtBankAccount" runat="server" CssClass="TextBox"></asp:TextBox>
							<asp:RequiredFieldValidator runat="server" ControlToValidate="txtBankAccount" ErrorMessage="(*) Campo Obligatorio"
							ValidationGroup="Save" ID="txtBankAccountRequired">
							</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
                <br />
                <table width="100%">
                    <tr>
                        <td width="25%">
                            <%-- <asp:Label ID="Label25" runat="server" Text="Coordinación Regional" CssClass="Label_2"></asp:Label>--%>
                        </td>
                        <td>
                            <%--  <asp:DropDownList ID="drpRegional" runat="server" CssClass="DropDownList_1">
                            </asp:DropDownList>--%>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%">
                            <asp:Label ID="Label26" runat="server" Text="Zona correspondiente" CssClass="Label_2"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpZone" runat="server" CssClass="DropDownList_1">
                            </asp:DropDownList>
							<asp:RequiredFieldValidator runat="server" ControlToValidate="drpZone" ErrorMessage="(*) Campo Obligatorio"
							ValidationGroup="Save" ID="drpZoneRequired">
							</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <table width="100%">
            <tr>
                <td width="10%">
                </td>
                <td width="20%">
                    <asp:Button ID="btnSave" runat="server" Text="Guardar" CssClass="Button" OnClick="btnSave_Click" />
                </td>
                <td width="20%">
                    <asp:Button ID="btnAssignProduct" runat="server" Text="Asignar Productos" CssClass="Button"
                        OnClick="btnAssignProduct_Click" />
                </td>
                <td width="20%">
                    <asp:Button ID="btnAssignDisposal" runat="server" Text="Asignar CAyD" CssClass="Button"
                        OnClick="btnAssignDisposal_Click" />
                </td>
                <td>
                    <asp:Button ID="btnCancel" runat="server" Text="Salir" CssClass="Button" OnClick="btnCancel_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
