<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="DisposalCenterEdit.aspx.cs"
    Inherits="PAEEEM.RegionalModule.DisposalCenterEdit" Title="Alta de Centro de Acopio y Destrucción" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Resources/Css/PageMask.css" type="text/css" rel="Stylesheet" />
    <style type="text/css">
        .Label {
            width: 160px;
            color: #333333;
            font-size: 16px;
        }

        .CheckBox {
            width: 100px;
            color: #333333;
            font-size: 16px;
        }

        .Label_1 {
            width: 120px;
            color: #333333;
            font-size: 16px;
        }

        .Label_2 {
            width: 230px;
            color: #333333;
            font-size: 16px;
        }

        .DropDownList {
            width: 300px;
        }

        .DropDownList_1 {
            width: 500px;
        }

        .TextBox {
            width: 500px;
        }

        .TextBox_1 {
            width: 180px;
        }

        .Button {
            width: 180px;
        }
    </style>

    <script type="text/javascript">
        function lockScreen() {
            var lock = document.getElementById('lock');
            lock.style.width = '300px';
            lock.style.height = '30px';
            // lock.style.top = document.documentElement.clientHeight/2 - lock.style.height.replace('px','')/2 + 'px';
            lock.style.top = document.body.offsetHeight / 1.5 - lock.style.height.replace('px', '') / 2 + 'px';
            lock.style.left = document.body.offsetWidth / 2 - lock.style.width.replace('px', '') / 2 + 'px';
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
                    <img src="../images/progress.gif" alt="¡ En Proceso, Por favor Espere !" style="vertical-align: middle; position: relative;" />
                </div>
                <div>
                    <br />
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/disposal_center.png" />
                </div>
                <div align="right">
                    <asp:Label ID="Label1" Text="Fecha" runat="server" CssClass="Label" />&nbsp
                    <asp:Literal ID="literalFecha" runat='server' />
                </div>
                <br />
                <asp:UpdatePanel ID="chkRefresh" runat="server">
                    <ContentTemplate>
                        <div>
                            <br />
                            <asp:Image ID="Image2" runat="server" ImageUrl="../DisposalModule/images/datos_empresa.png" />
                        </div>
                        <br />
                        <table width="100%">
                            <tr>
                                <td width="30%">
                                    <asp:CheckBox ID="ckbBranch" runat="server" Text="Sucursal" CssClass="CheckBox" AutoPostBack="True"
                                        OnCheckedChanged="ckbBranch_CheckedChanged" />
                                </td>
                                <td width="10%">
                                    <asp:Label ID="lblMainCenter" runat="server" Text="Nombre o Razón Social (Matriz)" CssClass="Label_1"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="drpMainCenter" runat="server" CssClass="DropDownList">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="drpMainCenter" ErrorMessage="(*) Campo Obligatorio"
                                        ValidationGroup="Save" ID="drpMainCenterRequired">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <table width="100%">
                    <tr>
                        <td width="25%">
                            <asp:Label ID="lblClave" runat="server" Text="Clave" CssClass="Label_2"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtClave" runat="server" CssClass="TextBox" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <br />
                <table width="100%">
                    <tr>
                        <td width="25%">
                            <asp:Label ID="lblCaptura" runat="server" Text="Nombre o Razón Social (Matriz)" CssClass="Label_2"></asp:Label>
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
                <div>
                    <asp:Image ID="Image3" runat="server" ImageUrl="../DisposalModule/images/domicilio_negocio.png" />
                </div>
                <br />
                <table width="100%">
                    <tr>
                        <td width="11%">
                            <asp:Label ID="Label5" runat="server" Text="Calle del Domicilio" CssClass="Label_1"></asp:Label>
                        </td>
                        <td width="18%">
                            <asp:TextBox ID="txtPartCalle" runat="server" CssClass="TextBox_1"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPartCalle" ErrorMessage="(*) Campo Obligatorio"
                                ValidationGroup="Save" ID="txtPartCalleRequired">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td width="11%">
                            <asp:Label ID="Label6" runat="server" Text="Número" CssClass="Label_1"></asp:Label>
                        </td>
                        <td width="20%">
                            <asp:TextBox ID="txtPartNum" runat="server" CssClass="TextBox_1"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPartNum" ErrorMessage="(*) Campo Obligatorio"
                                ValidationGroup="Save" ID="txtPartNumRequired">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td width="11%">
                            <asp:Label ID="Label7" runat="server" Text="Código Postal" CssClass="Label_1"></asp:Label>
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
                <asp:UpdatePanel runat="server" ID="estadoRefresh">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td width="11%">
                                    <asp:Label ID="Label8" runat="server" Text="Estado" CssClass="Label_1"></asp:Label>
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
                                    <asp:Label ID="Label9" runat="server" Text="Delegación o Municipio" CssClass="Label_1"></asp:Label>
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
                    </ContentTemplate>
                </asp:UpdatePanel>
                <br />
                <div>
                    <asp:Image ID="Image4" runat="server" ImageUrl="../DisposalModule/images/domicilio_fiscal.png" />
                </div>
                <table width="100%">
                    <tr>
                        <td width="5%"></td>
                        <td width="23%">
                            <asp:Label ID="Label10" runat="server" Text="Marcar si el Domicilio Fiscal es el mismo que el Domicilio del Negocio" CssClass="Label_2"></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox ID="ckbFiscal" runat="server" AutoPostBack="True" OnCheckedChanged="ckbFiscal_CheckedChanged"
                                onclick="lockScreen();" />
                        </td>
                    </tr>
                    <caption>
                        <br />
                    </caption>
                </table>
                <br />
                <table width="100%">
                    <tr>
                        <td width="11%">
                            <asp:Label ID="Label11" runat="server" Text="Calle del Domicilio" CssClass="Label_1"></asp:Label>
                        </td>
                        <td width="18%">
                            <asp:TextBox ID="txtFiscalCalle" runat="server" CssClass="TextBox_1"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFiscalCalle" ErrorMessage="(*) Campo Obligatorio"
                                ValidationGroup="Save" ID="txtFiscalCalleRequired">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td width="11%">
                            <asp:Label ID="Label12" runat="server" Text="Número" CssClass="Label_1"></asp:Label>
                        </td>
                        <td width="20%">
                            <asp:TextBox ID="txtFiscalNum" runat="server" CssClass="TextBox_1"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFiscalNum" ErrorMessage="(*) Campo Obligatorio"
                                ValidationGroup="Save" ID="txtFiscalNumRequired">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td width="11%">
                            <asp:Label ID="Label13" runat="server" Text="Código Postal" CssClass="Label_1"></asp:Label>
                        </td>
                        <td width="29%">
                            <div>
                                <asp:TextBox ID="txtFiscalCP" runat="server" CssClass="TextBox_1"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFiscalCP" ErrorMessage="(*) Campo Obligatorio"
                                    ValidationGroup="Save" ID="txtFiscalCPRequired">
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator runat="server" ValidationExpression="^\d{5}$" ControlToValidate="txtFiscalCP"
                                    ErrorMessage="(*) Campo Vacío o con Formato Inválido" ID="RegularExpressionValidator1">
                                </asp:RegularExpressionValidator>
                            </div>
                        </td>
                    </tr>
                </table>
                <asp:UpdatePanel runat="server" ID="estadoRefresh2">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td width="11%">
                                    <asp:Label ID="Label14" runat="server" Text="Estado" CssClass="Label_1"></asp:Label>
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
                                    <asp:Label ID="Label15" runat="server" Text="Delegación o  Municipio" CssClass="Label_1"></asp:Label>
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
                    </ContentTemplate>
                </asp:UpdatePanel>
                <br />
                <table width="100%">
                    <div>
                        <asp:Image ID="Image5" runat="server" ImageUrl="../DisposalModule/images/informacion_encargado.png" />
                    </div>
                    <caption>
                        <br />
                        <tr>
                            <td width="25%">
                                <asp:Label ID="Label16" runat="server" CssClass="Label_2" Text="Nombre del Responsable"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtRepresentativeName" runat="server" CssClass="TextBox"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="txtRepresentativeNameRequired" runat="server" ControlToValidate="txtRepresentativeName" ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Save">
                            </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td width="25%">
                                <asp:Label ID="lblDxApPaternoRepre" runat="server" CssClass="Label_2" Text="Apellido Paterno Responsable"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDxApPaternoRepre" runat="server" CssClass="TextBox"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="txtDxApPaternoRepreRequired" runat="server" ControlToValidate="txtDxApPaternoRepre" ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Save">
                            </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td width="25%">
                                <asp:Label ID="lblDxApMaternoRepre" runat="server" CssClass="Label_2" Text="Apellido Materno Responsable"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDxApMaternoRepre" runat="server" CssClass="TextBox"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="txtDxApMaternoRepreRequired" runat="server" ControlToValidate="txtDxApMaternoRepre" ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Save">
                            </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td width="25%">
                                <asp:Label ID="lblDxCelularRepre" runat="server" CssClass="Label_2" Text="Celular Responsable"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDxCelularRepre" runat="server" CssClass="TextBox"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="txtDxCelularRepreRequired" runat="server" ControlToValidate="txtDxCelularRepre" ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Save">
                            </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td width="25%">
                                <asp:Label ID="Label17" runat="server" CssClass="Label_2" Text="E-mail del Responsable"></asp:Label>
                            </td>
                            <td>
                                <div>
                                    <asp:TextBox ID="txtRepresentativeEmail" runat="server" CssClass="TextBox"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="txtRepresentativeEmailRequired" runat="server" ControlToValidate="txtRepresentativeEmail" ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Save">
                                </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ControlToValidate="txtRepresentativeEmail" ErrorMessage="(*) Campo Obligatorio" ValidationExpression="^(([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+([;.](([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+)*$">
                                </asp:RegularExpressionValidator>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td width="25%">
                                <asp:Label ID="Label18" runat="server" CssClass="Label_2" Text="Teléfono"></asp:Label>
                            </td>
                            <td>
                                <div>
                                    <asp:TextBox ID="txtTelephone" runat="server" CssClass="TextBox" ToolTip="(10 Dígitos Numéricos)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="txtTelephoneRequired" runat="server" ControlToValidate="txtTelephone" ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Save">
                                </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" ControlToValidate="txtTelephone" ErrorMessage="(*) Campo Obligatorio" ValidationExpression="^((\d{10}))$">
                                </asp:RegularExpressionValidator>
                                </div>
                            </td>
                        </tr>
                    </caption>
                </table>
                <br />
                <div>
                    <asp:Image ID="Image6" runat="server" ImageUrl="../DisposalModule/images/informacion_representante.png" />
                </div>
                <br />
                <table width="100%">
                    <tr>
                        <td width="25%">
                            <asp:Label ID="Label19" runat="server" Text="Nombre del Representante Legal" CssClass="Label_2"></asp:Label></td>
                        <td>
                            <div>
                            </div>
                            <asp:TextBox ID="txtLegalName" runat="server" CssClass="TextBox"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtLegalName" ErrorMessage="(*) Campo Obligatorio"
                                ValidationGroup="Save" ID="txtLegalNameRequired">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%">
                            <asp:Label ID="lblDxApPaternoRepLeg" runat="server" Text="Apellido Paterno Representante Legal" CssClass="Label_2"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtDxApPaternoRepLeg" runat="server" CssClass="TextBox"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDxApPaternoRepLeg" ErrorMessage="(*) Campo Obligatorio"
                                ValidationGroup="Save" ID="txtDxApPaternoRepLegRequired">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%">
                            <asp:Label ID="lblDxApMaternoRepLeg" runat="server" Text="Apellido Materno Representante Legal" CssClass="Label_2"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtDxApMaternoRepLeg" runat="server" CssClass="TextBox"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDxApMaternoRepLeg" ErrorMessage="(*) Campo Obligatorio"
                                ValidationGroup="Save" ID="txtDxApMaternoRepLegRequired">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%">
                            <asp:Label ID="lblDxEmailRepreLegal" runat="server" Text="Email Representante Legal" CssClass="Label_2"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtDxEmailRepreLegal" runat="server" CssClass="TextBox"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDxEmailRepreLegal" ErrorMessage="(*) Campo Obligatorio"
                                ValidationGroup="Save" ID="txtDxEmailRepreLegalRequired">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%">
                            <asp:Label ID="lblDxTelefonoRepreLeg" runat="server" Text="Telefono Representante Legal" CssClass="Label_2"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtDxTelefonoRepreLeg" runat="server" CssClass="TextBox"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDxTelefonoRepreLeg" ErrorMessage="(*) Campo Obligatorio"
                                ValidationGroup="Save" ID="txtDxTelefonoRepreLegRequired">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%">
                            <asp:Label ID="lblDxCelularRepreLeg" runat="server" Text="Celular Representante Legal" CssClass="Label_2"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtDxCelularRepreLeg" runat="server" CssClass="TextBox"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDxCelularRepreLeg" ErrorMessage="(*) Campo Obligatorio"
                                ValidationGroup="Save" ID="txtDxCelularRepreLegRequired">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <div>
            <asp:Image ID="Image7" runat="server" ImageUrl="../DisposalModule/images/informacion_general.png" />
        </div>
        <br />
        <table width="100%">
            <tr>
                <td width="25%">
                    <asp:Label ID="Label20" runat="server" Text="Cargar Imagen Digital del Poder Notarial" CssClass="Label_2"></asp:Label></td>
                <td>
                    <asp:FileUpload ID="fileUploadAttorneyPower" runat="server" Width="570px" />
                </td>
            </tr>
            <tr>
                <td width="25%">
                    <asp:Label ID="Label21" runat="server" Text="Cargar Imagen Digital del Acta Constitutiva" CssClass="Label_2"></asp:Label></td>
                <td>
                    <asp:FileUpload ID="fileUploadConstitutiva" runat="server" Width="570px" />
                </td>
            </tr>

            <tr>
                <td width="25%">
                    <asp:Label ID="lblNoEmpleados" runat="server" Text="No Empleados" CssClass="Label_2"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtNoEmpleados" runat="server" CssClass="TextBox"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNoEmpleados" ErrorMessage="(*) Campo Obligatorio"
                        ValidationGroup="Save" ID="txtNoEmpleadosRequired">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
        <%--        <br />
        --%><%--        <table width="100%">
            <tr>
                <td width="25%" colspan="2" style="width: 50%">
                    <asp:Label ID="Label22" runat="server" CssClass="Label_2" Text="Información Financiera de Depósito"></asp:Label></td><td>
                </td>
            </tr>
            <tr style="display:none;">
                <td width="3%">
                </td>
                <td width="21%">
                    <asp:Label ID="Label23" runat="server" CssClass="Label_2" Text="Nombre del Banco"></asp:Label></td><td>
                    <asp:TextBox ID="txtBankName" runat="server" CssClass="TextBox"></asp:TextBox>
				</td></tr><tr>
                <td width="3%">
                </td>
                <td width="21%">
                    <asp:Label ID="Label24" runat="server" CssClass="Label_2" Text="Número Cuenta CLABE"></asp:Label></td><td>
                    <asp:TextBox ID="txtBankAccount" runat="server" CssClass="TextBox"></asp:TextBox>
                    </td></tr></table><br />
        --%>
        <table width="100%">
            <tr>
                <td width="25%">
                    <%-- <asp:Label ID="Label25" runat="server" Text="Coordinación Regional" CssClass="Label_2"></asp:Label>--%>
                </td>
                <td>
                    <%--<asp:DropDownList ID="drpRegional" runat="server" CssClass="DropDownList_1">
                    </asp:DropDownList>--%>
                </td>
            </tr>
            <br />
            <div>
                <asp:Image ID="Image8" runat="server" ImageUrl="../DisposalModule/images/analizador_gases.png" />
            </div>
            <br />
            <tr>
                <td width="25%">
                    <asp:Label ID="lblMarcaAnalizadorGas" runat="server" Text="Marca Analizador Gas" CssClass="Label_2"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtMarcaAnalizadorGas" runat="server" CssClass="TextBox"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtMarcaAnalizadorGas" ErrorMessage="(*) Campo Obligatorio"
                        ValidationGroup="Save" ID="txtMarcaAnalizadorGasRequired">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td width="25%">
                    <asp:Label ID="lblModeloAnalizadorGas" runat="server" Text="Modelo Analizador Gas" CssClass="Label_2"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtModeloAnalizadorGas" runat="server" CssClass="TextBox"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtModeloAnalizadorGas" ErrorMessage="(*) Campo Obligatorio"
                        ValidationGroup="Save" ID="txtModeloAnalizadorGasRequired">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td width="25%">
                    <asp:Label ID="lblSerieAnalizadorGas" runat="server" Text="Serie Analizador Gas" CssClass="Label_2"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtSerieAnalizadorGas" runat="server" CssClass="TextBox"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtSerieAnalizadorGas" ErrorMessage="(*) Campo Obligatorio"
                        ValidationGroup="Save" ID="txtSerieAnalizadorGasRequired">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td width="25%">
                    <asp:Label ID="lblNoRegistroAmbiental" runat="server" Text="No. Registro Ambiental" CssClass="Label_2"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtNoRegistroAmbiental" runat="server" CssClass="TextBox"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNoRegistroAmbiental" ErrorMessage="(*) Campo Obligatorio"
                        ValidationGroup="Save" ID="txtNoRegistroAmbientalRequired">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td width="25%">
                    <asp:Label ID="lblTipo" runat="server" Text="Tipo" CssClass="Label_2"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlTipo" runat="server">
                        <asp:ListItem Value="" Text=""></asp:ListItem>
                        <asp:ListItem Value="Centro de Acopio" Text="Centro de Acopio"></asp:ListItem>
                        <asp:ListItem Value="Centro de Acopio y Destrucción" Text="Centro de Acopio y Destrucción"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlTipo" ErrorMessage="(*) Campo Obligatorio"
                        ValidationGroup="Save" ID="txtTipoRequired">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
        <table width="100%">
            <br />
            <div>
                <asp:Image ID="Image9" runat="server" ImageUrl="../DisposalModule/images/horario_atencion.png" />
            </div>
            <br />
            <tr>
                <td width="25%">
                    <asp:Label ID="lblHorarioDesde" runat="server" Text="Horario Desde" CssClass="Label_2" ToolTip="HH:MM:SS"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtHorarioDesde" runat="server" CssClass="TextBox" ToolTip="HH:MM:SS"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtHorarioDesde" ErrorMessage="(*) Campo Obligatorio"
                        ValidationGroup="Save" ID="txtHorarioDesdeRequired">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td width="25%">
                    <asp:Label ID="lblHorarioHasta" runat="server" Text="Horario Hasta" CssClass="Label_2" ToolTip="HH:MM:SS"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtHorarioHasta" runat="server" CssClass="TextBox" ToolTip="HH:MM:SS"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtHorarioHasta" ErrorMessage="(*) Campo Obligatorio"
                        ValidationGroup="Save" ID="txtHorarioHastaRequired">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td width="25%">
                    <asp:Label ID="lblDiasSemana" runat="server" Text="Días Semana" CssClass="Label_2"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtDiasSemana" runat="server" CssClass="TextBox"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDiasSemana" ErrorMessage="(*) Campo Obligatorio"
                        ValidationGroup="Save" ID="txtDiasSemanaRequired">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td>&nbsp;
                </td>
            </tr>
            <%--<tr>
                        <td width="25%">
                            <asp:Label ID="lblEstatusRegistro" runat="server" Text="Estatus Registro" CssClass="Label_2"></asp:Label></td><td>
                            <asp:TextBox ID="txtEstatusRegistro" runat="server" CssClass="TextBox"></asp:TextBox>
							<asp:RequiredFieldValidator runat="server" ControlToValidate="txtEstatusRegistro" ErrorMessage="(*) Campo Obligatorio"
							ValidationGroup="Save" ID="txtEstatusRegistroRequired">
							</asp:RequiredFieldValidator>
						</td></tr>--%>
            <tr>
                <td width="25%">
                    <asp:Label ID="lblTelefonoAtn1" runat="server" Text="Teléfono Atención 1" CssClass="Label_2"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtTelefonoAtn1" runat="server" CssClass="TextBox"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtTelefonoAtn1" ErrorMessage="(*) Campo Obligatorio"
                        ValidationGroup="Save" ID="txtTelefonoAtn1Required">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td width="25%">
                    <asp:Label ID="lblTelefonoAtn2" runat="server" Text="Teléfono Atención 2" CssClass="Label_2"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtTelefonoAtn2" runat="server" CssClass="TextBox"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtTelefonoAtn2" ErrorMessage="(*) Campo Obligatorio"
                        ValidationGroup="Save" ID="txtTelefonoAtn2Required">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td width="25%">
                    <asp:Label ID="Label26" runat="server" Text="Zona correspondiente" CssClass="Label_2"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="drpZone" runat="server" CssClass="DropDownList_1">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="drpZone" ErrorMessage="(*) Campo Obligatorio"
                        ValidationGroup="Save" ID="drpZoneRequired">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblMotivos" Text="Motivos" runat="server" CssClass="Label" Height="74px" Width="63px" Visible="False" />&nbsp
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtMotivos" CssClass="TextBox" Height="71px" TextMode="MultiLine" ToolTip="Ingrese los motivos por los cuales se realizaran cambios al usuario" Width="546px" Visible="False" />
                </td>
            </tr>
        </table>
        <br />
        <table width="100%">
            <tr>
                <td width="10%"></td>
                <td width="25%">
                    <asp:Button ID="btnSave" runat="server" Text="Guardar" CssClass="Button" OnClick="btnSave_Click"
                        OnClientClick="return confirm('Confirmar Guardar Centro de Acopio y Destrucción');" />
                </td>
                <td width="25%">
                    <asp:Button ID="btnAssignTechnology" runat="server" Text="Asignar Tecnología" CssClass="Button"
                        OnClick="btnAssignTechnology_Click" OnClientClick="return confirm('Confirmar Asignación de Tecnología al CAyD');" />
                </td>
                <td>
                    <asp:Button ID="btnCancel" runat="server" Text="Salir" CssClass="Button" OnClick="btnCancel_Click"
                        OnClientClick="return confirm('¿ Desea Salir de ésta Pantalla ?');" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
