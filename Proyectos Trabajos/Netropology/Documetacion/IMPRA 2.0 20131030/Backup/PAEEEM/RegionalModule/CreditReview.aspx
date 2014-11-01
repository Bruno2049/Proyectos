<%@ Page Language="C#" AutoEventWireup="True" MasterPageFile="~/Site.Master" CodeBehind="CreditReview.aspx.cs"
    Inherits="PAEEEM.CreditReview" Title="Revisión de Créditos" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Resources/Css/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <style type="text/css">
        .Label
        {
            width: 20%;
            color: Maroon;
        }
        .titulo
        {
            color: #FFFFFF;
            font-size: 16px;
            background-image: url(   '../Resources/Images/tabla.png' );
            background-repeat: repeat-x;
            width: 100%;
            height: 100%;
            border: 0px;
        }
        .Label_1
        {
            width: 45%;
            color: Maroon;
        }
        .Label_2
        {
            width: 10%;
            color: Maroon;
        }
        .TextBox
        {
            width: 25%;
        }
        .TextBox_1
        {
            width: 12.5%;
        }
        .TextBox_gdvc
        {
            width: 100%;
        }
        .TextBox_gdv
        {
            width: 100%;
        }
        .DropDownList
        {
            width: 25%;
        }
        .DropDownList_gdv
        {
            width: 100%;
            font-size:11px;
        }
        .Button
        {
            width: 120px;
        }
        .Button_1
        {
            width: 200px;
        }
        .RadioButton
        {
            width: 12.5%;
            color: Maroon;
        }
        .CheckBox
        {
            width: 25%;
            color: Maroon;
        }
        .style1 {
            border-width: 11pt;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div align="left">
        <h6>
        <br>
        <asp:Image runat="server" ImageUrl="images/t_informacion.png"/>
        </h6>
    </div>
    <table width="100%" border="0">
        <tr>
            <td>
                <div align="left">
                    <asp:Label ID="lblCredito" Text="No. Crédito" runat="server" CssClass="Label" Width="150px" ForeColor="#333333" />
                    <asp:TextBox ID="txtCredito" runat="server" CssClass="TextBox_1" Width="200px" Enabled="false">
                    </asp:TextBox>
                </div>
            </td>
            <td>
                <div align="right">
                    <asp:Label ID="lblFecha" Text="Fecha" runat="server" CssClass="Label" ForeColor="#333333" />
                    <asp:TextBox ID="txtFecha" runat="server" BorderWidth="0" Enabled="false"></asp:TextBox>
                </div>
            </td>
        </tr>
    </table>

    <asp:Panel ID="panelCompany" runat="server" >
    <table>
    <tr>
    <td>
    <br>
    <asp:Image runat="server" ImageUrl="../SupplierModule/images/t1.png"/>
    </td>
    </tr>
    </table>

    <table width="100%">
        <tr>
            <%--add by coco 2011-07-29--%><td>
                <asp:Label ID="lblPfisica" Text="P. Física/P.Moral" Font-Size="11pt" runat="server" CssClass="Label" Width="150px" ForeColor="#666666" />
            </td>
            <td>
                <asp:DropDownList ID="ddlPfisica" runat="server" CssClass="DropDownList" Font-Size="11px" OnSelectedIndexChanged="ddlPfisica_SelectedIndexChanged" AutoPostBack="true" Width="200px">
                </asp:DropDownList>
            </td>
            <td></td>
            <td>
                <asp:Label ID="lblName" runat="server" Text="Nombre o Razón Social"  Font-Size="11pt" CssClass="Label_2" Width="150px" ForeColor="#666666">
                </asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtName" runat="server" Font-Size="11px" CssClass="TextBox_1" Width="200px">
                </asp:TextBox>
            </td>
            <td style="width: 150px; " class="style1">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="(*) Campo Obligatorio" ControlToValidate="txtName" ValidationGroup="Guardar" Width="50px" Font-Size="8pt">
                </asp:RequiredFieldValidator>
            </td>
      </tr>
      <tr>
            <td>
                <asp:Label ID="lblLastname" runat="server" Text="Apellido Paterno P. Física" Font-Size="11pt" CssClass="Label_2" Width="150px" ForeColor="#666666">
                </asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtLastname" Font-Size="11px" runat="server" CssClass="TextBox_1" Width="200px">
                </asp:TextBox>
            </td>
            <td></td>
            <td>
                <asp:Label ID="lblMotherName" runat="server" Text="Apellido Materno P. Física" Font-Size="11pt" CssClass="Label_2" Width="150px" ForeColor="#666666">
                </asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtMotherName" runat="server" Font-Size="11px" CssClass="TextBox_1" Width="200px">
                </asp:TextBox>
            </td>
            <td></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblBirthDate" runat="server" Text="Fecha de Nacimiento" Font-Size="11pt" CssClass="Label" Width="150px" ForeColor="#666666">
                </asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtBirthDate" runat="server" Font-Size="11px" CssClass="TextBox_1" Width="200px">
                </asp:TextBox>
            </td>
            <td>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator15" ControlToValidate="txtBirthDate" ErrorMessage="(*) Campo Obligatorio" runat="server" ValidationGroup="Guardar" ValidationExpression="^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-9]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$" Width="150px" Font-Size="8pt" />
            </td>

            <td>
                <asp:Label ID="lblGiroEmpresa" Text="Giro de la Empresa" Font-Size="11pt" runat="server" CssClass="Label" Width="150px" ForeColor="#666666" />
            </td>
            <td>
                <asp:DropDownList ID="ddlGiroEmpresa" Font-Size="11px" runat="server" CssClass="DropDownList" Width="200px">
                </asp:DropDownList>
            </td>
            <td></td>
        </tr>
        <%--  add by coco 2012-07-16--%>
        <tr>
            <td>
                <asp:Label ID="lblTelephone" runat="server" Text="Teléfono" Font-Size="11pt"
                    CssClass="Label_2" Width="150px" ForeColor="#666666">
                </asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtTelephone" Font-Size="11px" runat="server" CssClass="TextBox_1"
                    Width="200px">
                </asp:TextBox>
            </td>
            <td>
            </td>
            <td>
                <asp:Label ID="lblEmail" runat="server" Text="E-mail" Font-Size="11pt"
                    CssClass="Label_2" Width="150px" ForeColor="#666666">
                </asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtEmail" runat="server" Font-Size="11px" CssClass="TextBox_1"
                    Width="200px">
                </asp:TextBox>
            </td>
            <td>
            </td>
        </tr>
        <%--  end add coco--%>
        <tr>
            <td>
                <asp:Label ID="lblCURP" Text="Clave Única Registro Población (CURP)" runat="server" Font-Size="11pt" CssClass="Label_2" Width="150px" ForeColor="#666666" />
            </td>
            <td>
                <asp:TextBox ID="txtCURP" runat="server" Font-Size="11px" CssClass="TextBox_1" Enabled="false" Width="200px">
                </asp:TextBox>
            </td>
            <td></td>
            <td>
                <asp:Label ID="lblRFC" Text="Registro Federal Causantes (RFC)" runat="server" Font-Size="11pt" CssClass="Label_2" Width="150px" ForeColor="#666666" />
            </td>
            <td>
                <asp:TextBox ID="txtRFC" runat="server" Font-Size="11px" CssClass="TextBox_1" Width="200px">
                </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblRepresLegal" Text="Representante Legal" Font-Size="11pt" runat="server" CssClass="Label" Width="150px" ForeColor="#666666" />
            </td>
            <td>
                <asp:TextBox ID="txtRepresLegal" runat="server" Font-Size="11px" CssClass="TextBox" Width="200px">
                </asp:TextBox>
            </td>
            <td></td>
            <td>
                <asp:Label ID="lblAcreditado" Text="Acredita Ocupación de Negocio con" runat="server" Font-Size="11pt" CssClass="Label" Width="150px" ForeColor="#666666" />
            </td>
            <td>
                <asp:DropDownList ID="ddlAcreditado" runat="server" CssClass="DropDownList" Font-Size="11px" Width="200px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblSexo" Text="Sexo" runat="server" CssClass="Label" Width="150px" Font-Size="11pt" ForeColor="#666666" />
            </td>
            <td>
                <asp:RadioButtonList ID="rblSex" runat="server" RepeatDirection="Horizontal" Width="150px" Font-Size="11px" RepeatLayout="Flow">
                <asp:ListItem Text="M" Value="1">
                </asp:ListItem>
                <asp:ListItem Text="F" Value="2">
                </asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td></td>
            <td>
                <asp:Label ID="lblRPU" Text="Número de Servicio (RPU)"  Font-Size="11pt" runat="server" CssClass="Label" Width="150px" ForeColor="#666666" />
            </td>
            <td>
                <asp:TextBox ID="txtRPU" runat="server" CssClass="TextBox_1" Font-Size="11px" OnTextChanged="txtRPU_TextChanged" AutoPostBack="true" Width="200px">
                </asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="(*) Campo Obligatorio" ControlToValidate="txtRPU" ValidationGroup="Guardar" Width="140px" Font-Size="8pt">
                </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblEstadoCivil" Text="Estado Civil" runat="server" Font-Size="11pt" CssClass="Label" Width="150px" ForeColor="#666666" />
            </td>
            <td>
                <asp:RadioButtonList ID="rblEstadoCivil" runat="server" RepeatDirection="Horizontal" Font-Size="11px" RepeatLayout="Flow" OnSelectedIndexChanged="rblEstadoCivil_SelectedIndexChanged" AutoPostBack="true" Width="200px">
                <asp:ListItem Text="Soltero(a)" Value="1">
                </asp:ListItem>
                <asp:ListItem Text="Casado(a)" Value="2">
                </asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td></td>
            <td>
                <asp:Label ID="lblRegimenMatri" Text="Régimen Matrimonial" runat="server" CssClass="Label" Font-Size="11pt" Width="150px" ForeColor="#666666" />
            </td>
            <td>
                <asp:DropDownList ID="ddlRegimenMatri" runat="server" CssClass="DropDownList" Font-Size="11px" OnSelectedIndexChanged="ddlRegimenMatri_SelectedIndexChanged" AutoPostBack="true" Width="200px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblTipoIdenti" Text="Tipo de Identificación Oficial" runat="server" CssClass="Label" Font-Size="11pt" Width="150px" ForeColor="#666666" />
            </td>
            <td>
                <asp:DropDownList ID="ddlTipoIdenti" runat="server" CssClass="DropDownList" Font-Size="11px" Width="200px">
                </asp:DropDownList>
            </td>
            <td></td>
            <td>
                <asp:Label ID="lblNumero" Text="Número Identificación" runat="server" CssClass="Label" Width="150px" Font-Size="11pt" ForeColor="#666666" />
            </td>
            <td>
                <asp:TextBox ID="txtNumero" runat="server" CssClass="TextBox" Font-Size="11px" Width="200px">
                </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPromedioMV" Text="Promedio Mensual de Ventas" Font-Size="11pt" runat="server" CssClass="Label_2" Width="150px" ForeColor="#666666" />
            </td>
            <td>
                <asp:TextBox ID="txtPromedioMV" runat="server" CssClass="TextBox_1" Font-Size="11px" Width="200px">
                </asp:TextBox>
            </td>
            <td>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" ControlToValidate="txtPromedioMV" ErrorMessage="(*) Campo Obligatorio" runat="server" ValidationGroup="Guardar" ValidationExpression="^\d+(\.\d+)?$" Width="150px" Font-Size="8pt" />
            </td>
            <td>
                <asp:Label ID="lblTotalGastosMensual" Text="Total de Gastos Mensuales" runat="server" Font-Size="11pt" CssClass="Label_2" Width="150px" ForeColor="#666666" />
            </td>
            <td>
                <asp:TextBox ID="txtTotalGastosMensual" runat="server" CssClass="TextBox_1" Font-Size="11px" Width="200px">
                </asp:TextBox>
            </td>
            <td>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ControlToValidate="txtTotalGastosMensual" ErrorMessage="(*) Campo Obligatorio" runat="server" ValidationGroup="Guardar" ValidationExpression="^\d+(\.\d+)?$" Width="150px" Font-Size="8pt" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblRLemail" Text="E-mail del Representante Legal" runat="server" Font-Size="11pt" CssClass="Label" Width="150px" ForeColor="#666666" />
            </td>
            <td>
                <asp:TextBox ID="txtRLemail" runat="server" CssClass="TextBox" Font-Size="11px" Width="200px">
                </asp:TextBox>
            </td>
            <td>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator10" ControlToValidate="txtRLemail" ErrorMessage="(*) Campo Obligatorio" runat="server" ValidationGroup="Guardar" ValidationExpression="^(([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+([;.](([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+)*$" Width="150px" Font-Size="8pt" />
            </td>
        </tr>
    </table>

    <%--  end add--%></asp:Panel>
    <asp:Panel ID="panelFical" runat="server" >
    <table>
        <tr>
            <td>
                <br>
                <asp:Image runat="server" ImageUrl="../SupplierModule/images/t2.png"/>
            </td>
        </tr>
    </table>

    <table width="100%">
        <tr>
            <td>
                <asp:Label ID="lblFiscalCalle" Text="Calle del Domicilio" runat="server" Font-Size="11pt" CssClass="Label_2" Width="150px" ForeColor="#666666" />
            </td>
            <td>
                <asp:TextBox ID="txtFiscalCalle" runat="server" Font-Size="11px" CssClass="TextBox_1" Width="200px">
                </asp:TextBox>
            </td>
            <td></td>
            <td>
                <asp:Label ID="lblFiscalNumero" Text="Número e interior" runat="server" Font-Size="11pt" CssClass="Label_2" Width="150px" ForeColor="#666666" />
            </td>
            <td>
                <asp:TextBox ID="txtFiscalNumero" runat="server" CssClass="TextBox_1" Font-Size="11px" Width="200px">
                </asp:TextBox>
            </td>
            <td width="150px"></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblFiscalCP" Text="Código Postal" runat="server" CssClass="Label_2" Width="150px" Font-Size="11pt" ForeColor="#666666" />
            </td>
            <td>
                <asp:TextBox ID="txtFiscalCP" runat="server" CssClass="TextBox_1" MaxLength="5" Font-Size="11px" Width="200px">
                </asp:TextBox>
            </td>
            <td>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtFiscalCP" ErrorMessage="(*) Campo Obligatorio" runat="server" ValidationGroup="Guardar" ValidationExpression="^\d{5}$" Width="150px" Font-Size="8pt" />
            </td>
            <td>
                <asp:Label ID="lblDx_Domicilio_Fisc_Colonia" runat="server" Text="Colonia" Font-Size="11pt" CssClass="Label" Width="150px" ForeColor="#666666">
                </asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtDx_Domicilio_Fisc_Colonia" runat="server" CssClass="TextBox" Font-Size="11px" Width="200px">
                </asp:TextBox>
            </td>
            <td width="150px"></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblFiscalEstado" Text="Estado" runat="server" CssClass="Label" Width="150px" Font-Size="11pt" ForeColor="#666666" />
            </td>
            <td>
                <asp:DropDownList ID="ddlFiscalEstado" runat="server" CssClass="DropDownList" Font-Size="11px" OnSelectedIndexChanged="ddlFiscalEstado_SelectedIndexChanged" AutoPostBack="true" Width="200px">
                </asp:DropDownList>
            </td>
            <td></td>
            <td>
                <asp:Label ID="lblFiscalDM" Text="Delegación o Municipio" runat="server" CssClass="Label" Font-Size="11pt" Width="150px" ForeColor="#666666" />
            </td>
            <td>
                <asp:DropDownList ID="ddlFiscalDM" runat="server" CssClass="DropDownList" Font-Size="11px" Width="200px">
                </asp:DropDownList>
            </td>
            <td width="150px"></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblFiscalTele" Text="Teléfono" runat="server" CssClass="Label_2" Width="150px" Font-Size="11pt" ForeColor="#666666" />
            </td>
            <td>
                <asp:TextBox ID="txtFiscalTele" runat="server" CssClass="TextBox_1" Font-Size="11px" Width="200px">
                </asp:TextBox>
            </td>
            <td>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator12" ControlToValidate="txtFiscalTele" ErrorMessage="(*) Campo Obligatorio" runat="server" ValidationGroup="Guardar" ValidationExpression="^((\d{3}-\d{3}-\d{4})|(\d{2}-\d{4}-\d{4}))$" Width="150px" Font-Size="8pt" />
            </td>
            <td>
                <asp:Label ID="lblFiscalTipoPropie" Text="Tipo de Propiedad" runat="server" Font-Size="11pt" CssClass="Label" Width="150px" ForeColor="#666666" />
            </td>
            <td>
                <asp:DropDownList ID="ddlFiscalTipoPropie" runat="server" CssClass="DropDownList" Font-Size="11px" Width="200px">
                </asp:DropDownList>
            </td>
            <td width="150px"></td>
        </tr>
    </table>

    </asp:Panel>
    <asp:Panel ID="panelNegocio" runat="server">
    <table>
        <tr>
            <td>
                <br>
                <asp:Image runat="server" ImageUrl="../SupplierModule/images/t3.png"/>
            </td>
        </tr>
    </table>

    <table width="100%">
        <tr>
            <td>
                <asp:CheckBox ID="cbMismoFiscal" runat="server" AutoPostBack="true" Text="Marcar Sí el Domicilio del Negocio es el Mismo que el Domicilio Fiscal" OnCheckedChanged="cbMismoFiscal_CheckedChanged" ForeColor="#666666" />
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td>
                <asp:Label ID="lblNegocioCalle" Text="Calle del Domicilio" runat="server" Font-Size="11pt" CssClass="Label_2" Width="150px" ForeColor="#666666" />
            </td>
            <td>
                <asp:TextBox ID="txtNegocioCalle" runat="server" CssClass="TextBox_1" Font-Size="11px" Width="200px">
                </asp:TextBox>
            </td>
            <td width="150px"></td>
            <td>
                <asp:Label ID="lblNegocioNumero" Text="Número e Interior" runat="server" CssClass="Label_2" Font-Size="11pt" Width="150px" ForeColor="#666666" />
            </td>
            <td>
                <asp:TextBox ID="txtNegocioNumero" runat="server" CssClass="TextBox_1" Font-Size="11px" Width="200px">
                </asp:TextBox>
            </td>
            <td width="150px"></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblNegocioCP" Text="Código Postal" runat="server" CssClass="Label_2" Width="150px"  Font-Size="11pt" ForeColor="#666666" />
            </td>
            <td>
                <asp:TextBox ID="txtNegocioCP" runat="server" CssClass="TextBox_1" MaxLength="5" Font-Size="11px" Width="200px">
                </asp:TextBox>
            </td>
            <td>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtNegocioCP" ErrorMessage="(*) Campo Obligatorio" runat="server" ValidationGroup="Guardar" ValidationExpression="^\d{5}$" Width="150px" Font-Size="8pt" />
            </td>
            <td>
                <asp:Label ID="lblDx_Domicilio_Neg_Colonia" runat="server" Text="Colonia" Font-Size="11pt" CssClass="Label" Width="150px" ForeColor="#666666">
                </asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtDx_Domicilio_Neg_Colonia" runat="server" CssClass="TextBox" Font-Size="11px" Width="200px">
                </asp:TextBox>
            </td>
            <td></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblNegocioEstado" Text="Estado" runat="server" CssClass="Label_2" Width="150px" Font-Size="11pt" ForeColor="#666666" />
            </td>
            <td>
                <asp:DropDownList ID="ddlNegocioEstado" runat="server" CssClass="TextBox_1" AutoPostBack="true" Font-Size="11px" OnSelectedIndexChanged="ddlNegocioEstado_SelectedIndexChanged" Width="200px">
                </asp:DropDownList>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="(*) Campo Obligatorio" ControlToValidate="ddlNegocioEstado" ValidationGroup="Guardar" Width="150px" Font-Size="8pt">
                </asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="lblNegocioDM" Text="Delegación o Municipio" runat="server" CssClass="Label" Font-Size="11pt" Width="150px" ForeColor="#666666" />
            </td>
            <td>
                <asp:DropDownList ID="ddlNegocioDM" runat="server" CssClass="DropDownList" Font-Size="11px" Width="200px">
                </asp:DropDownList>
            </td>
            <td></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblNegocioTele" Text="Teléfono" runat="server" CssClass="Label_2" Width="150px" Font-Size="11pt" ForeColor="#666666" />
            </td>
            <td>
                <asp:TextBox ID="txtNegocioTele" runat="server" CssClass="TextBox_1" Font-Size="11px" Width="200px">
                </asp:TextBox>
            </td>
            <td>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator13" ControlToValidate="txtNegocioTele" ErrorMessage="(*) Campo Obligatorio" runat="server" ValidationGroup="Guardar" ValidationExpression="^((\d{3}-\d{3}-\d{4})|(\d{2}-\d{4}-\d{4}))$" Width="150px" Font-Size="8pt" />
            </td>
            <td>
                <asp:Label ID="lblNegocioTipoPropie" Text="Tipo de Propiedad" runat="server" Font-Size="11pt" CssClass="Label_2" Width="150px" ForeColor="#666666" />
            </td>
            <td>
                <asp:DropDownList ID="ddlNegocioTipoPropie" runat="server" CssClass="DropDownList" Font-Size="11px" Width="200px">
                </asp:DropDownList>
            </td>
            <td></td>
        </tr>
    </table>

    </asp:Panel>
    <asp:Panel ID="panelAval" runat="server">
    <table>
        <tr>
            <td>
                <br>
                <asp:Image runat="server" ImageUrl="../SupplierModule/images/t4.png"/>
            </td>
        </tr>
    </table>


    <table width="100%">
        <tr>
            <td>
                <asp:Label ID="lblNobreAvalRS" Text="Nombre o Razón Social" runat="server" Font-Size="11pt" CssClass="Label" Width="150px" ForeColor="#666666" />
            </td>
            <td>
                <asp:TextBox ID="txtNobreAvalRS" runat="server" CssClass="TextBox" Font-Size="11px" Width="200px">
                </asp:TextBox>
            </td>
            <td></td>
            <td>
                <asp:Label ID="lblAvalCURP" Text="CURP/RFC" runat="server" CssClass="Label" Font-Size="11pt" Width="150px" ForeColor="#666666" />
            </td>
            <td>
                <asp:TextBox ID="txtAvalCURP" runat="server" CssClass="TextBox" Font-Size="11px" Width="200px">
                </asp:TextBox>
            </td>
            <td></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblAvalSexo" Text="Sexo" runat="server" CssClass="Label" Font-Size="11pt" Width="150px" ForeColor="#666666" />
            </td>
            <td>
                <asp:RadioButtonList ID="rblAvalSexo" runat="server" RepeatDirection="Horizontal" Font-Size="11px" Width="150px" RepeatLayout="Flow">
                <asp:ListItem Text="M" Value="1">
                </asp:ListItem>
                <asp:ListItem Text="F" Value="2">
                </asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td></td>
            <td>
                <asp:Label ID="lblAvalTele" Text="Teléfono" runat="server" CssClass="Label_2" Font-Size="11pt" Width="150px" ForeColor="#666666" />
            </td>
            <td>
                <asp:TextBox ID="txtAvalTele" runat="server" CssClass="TextBox_1" Font-Size="11px" Width="200px">
                </asp:TextBox>
            </td>
            <td>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ControlToValidate="txtAvalTele" ErrorMessage="(*) Campo Obligatorio" runat="server" ValidationGroup="Guardar" ValidationExpression="^((\d{3}-\d{3}-\d{4})|(\d{2}-\d{4}-\d{4}))$" Width="150px" Font-Size="8pt" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblAvalCalle" Text="Calle del Domicilio" runat="server" CssClass="Label_2" Font-Size="11pt" Width="150px" ForeColor="#666666" />
            </td>
            <td>
                <asp:TextBox ID="txtAvalCalle" runat="server" CssClass="TextBox_1" Font-Size="11px" Width="200px">
                </asp:TextBox>
            </td>
            <td></td>
            <td>
                <asp:Label ID="lblAvalNumero" Text="Número e Interior" runat="server" Font-Size="11pt" CssClass="Label_2" Width="150px" ForeColor="#666666" />
            </td>
            <td>
                <asp:TextBox ID="txtAvalNumero" runat="server" CssClass="TextBox_1" Font-Size="11px" Width="200px">
                </asp:TextBox>
            </td>
            <td></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblAvalCP" Text="Código Postal" runat="server" CssClass="Label_2" Font-Size="11pt" Width="150px" ForeColor="#666666" />
            </td>
            <td>
                <asp:TextBox ID="txtAvalCP" runat="server" CssClass="TextBox_1" MaxLength="5" Font-Size="11px" Width="200px">
                </asp:TextBox>
            </td>
            <td>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtAvalCP" ErrorMessage="(*) Campo Obligatorio" runat="server" ValidationGroup="Guardar" ValidationExpression="^\d{5}$" Width="150px" Font-Size="8pt" />
            </td>
            <td>
                <asp:Label ID="lblDx_Domicilio_Aval_Colonia" runat="server" Text="Colonia" Font-Size="11pt" CssClass="Label" Width="150px" ForeColor="#666666">
                </asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtDx_Domicilio_Aval_Colonia" runat="server" CssClass="TextBox" Font-Size="11px" Width="200px">
                </asp:TextBox>
            </td>
            <td></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblAvalEstado" Text="Estado" runat="server" CssClass="Label" Width="150px" Font-Size="11pt" ForeColor="#666666" />
            </td>
            <td>
                <asp:DropDownList ID="ddlAvalEstado" runat="server" Font-Size="11px" CssClass="DropDownList" OnSelectedIndexChanged="ddlAvalEstado_SelectedIndexChanged" AutoPostBack="true" Width="200px">
                </asp:DropDownList>
            </td>
            <td></td>
            <td>
                <asp:Label ID="lblAvalDM" Text="Delegación o Municipio" runat="server" Font-Size="11pt" CssClass="Label" Width="150px" ForeColor="#666666" />
            </td>
            <td>
                <asp:DropDownList ID="ddlAvalDM" runat="server" CssClass="DropDownList" Font-Size="11px" Width="200px">
                </asp:DropDownList>
            </td>
            <td></td>
        </tr>
    </table>
    </asp:Panel>
    <div id="CO" runat="server">
        <asp:Panel ID="panelCoAcreditado" runat="server">
        <table>
            <tr>
                <td>
                    <asp:Image runat="server" ImageUrl="../SupplierModule/images/t5.png"/>
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td>
                    <asp:Label ID="lblNobreCoAcreditado" Text="Nombre Completo" runat="server" Font-Size="11pt" CssClass="Label" Width="150px" ForeColor="#666666" />
                </td>
                <td>
                    <asp:TextBox ID="txtNobreCoAcreditadoRS" runat="server" CssClass="TextBox" Font-Size="11px" Width="200px">
                    </asp:TextBox>
                </td>
                <td></td>
                <td>
                    <asp:Label ID="lblCoAcreditadoCURP" Text="CURP/RFC" Font-Size="11pt" runat="server" CssClass="Label" Width="150px" ForeColor="#666666" />
                </td>
                <td>
                    <asp:TextBox ID="txtCoAcreditadoCURP" runat="server" Font-Size="11px" CssClass="TextBox" Width="200px">
                    </asp:TextBox>
                </td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCoAcreditadoSexo" Text="Sexo" runat="server" Font-Size="11pt" CssClass="Label" Width="150px" ForeColor="#666666" />
                </td>
                <td>
                    <asp:RadioButtonList ID="rblCoAcreditadoSexo" runat="server" Font-Size="11px" RepeatDirection="Horizontal" Width="150px" RepeatLayout="Flow">
                    <asp:ListItem Text="M" Value="1">
                    </asp:ListItem>
                    <asp:ListItem Text="F" Value="2">
                    </asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td></td>
                <td>
                    <asp:Label ID="lblCoAcreditadoTele" Text="Teléfono" runat="server" Font-Size="11pt" CssClass="Label_2" Width="150px" ForeColor="#666666" />
                </td>
                <td>
                    <asp:TextBox ID="txtCoAcreditadoTele" runat="server" CssClass="TextBox_1" Font-Size="11px" Width="200px">
                    </asp:TextBox>
                </td>
                <td>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator11" ControlToValidate="txtCoAcreditadoTele" ErrorMessage="(*) Campo Obligatorio" runat="server" ValidationGroup="Guardar" ValidationExpression="^((\d{3}-\d{3}-\d{4})|(\d{2}-\d{4}-\d{4}))$" Width="150px" Font-Size="8pt" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCoAcreditadoCalle" Text="Calle del Domicilio" runat="server" CssClass="Label_2" Font-Size="11pt" Width="150px" ForeColor="#666666" />
                </td>
                <td>
                    <asp:TextBox ID="txtCoAcreditadoCalle" runat="server" CssClass="TextBox_1" Font-Size="11px" Width="200px">
                    </asp:TextBox>
                </td>
                <td></td>
                <td>
                    <asp:Label ID="lblCoAcreditadoNumero" Text="Número e Interior" runat="server" CssClass="Label_2" Font-Size="11pt" Width="150px" ForeColor="#666666" />
                </td>
                <td>
                    <asp:TextBox ID="txtCoAcreditadoNumero" runat="server" CssClass="TextBox_1" Font-Size="11px" Width="200px">
                    </asp:TextBox>
                </td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCoAcreditadoCP" Text="Código Postal" runat="server" CssClass="Label_2" Width="150px" Font-Size="11pt" ForeColor="#666666" />
                </td>
                <td>
                    <asp:TextBox ID="txtCoAcreditadoCP" runat="server" CssClass="TextBox_1" MaxLength="5" Font-Size="11px" Width="200px">
                    </asp:TextBox>
                </td>
                <td>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ControlToValidate="txtCoAcreditadoCP" ErrorMessage="(*) Campo Obligatorio" runat="server" ValidationGroup="Guardar" ValidationExpression="^\d{5}$" Width="150px" Font-Size="8pt" />
                </td>
                <td>
                    <asp:Label ID="lblDx_Domicilio_Coacreditado_Colonia" runat="server" Text="Colonia"  Font-Size="11pt" CssClass="Label" Width="150px" ForeColor="#666666">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDx_Domicilio_Coacreditado_Colonia" runat="server" Font-Size="11px" CssClass="TextBox" Width="200px">
                    </asp:TextBox>
                </td>
                <td></td>
            </tr>
        </table>
        <asp:updatepanel ID="Panel1" runat="server">
        <ContentTemplate>
        <table width="100%">
            <tr>
                <td>
                    <asp:Label ID="lblCoAcreditadoEstado" Text="Estado" Font-Size="11pt" runat="server" CssClass="Label" Width="150px" ForeColor="#666666" />
                </td>
                <td>
                    <asp:DropDownList ID="ddlCoAcreditadoEstado" runat="server" Font-Size="11px" CssClass="DropDownList" OnSelectedIndexChanged="ddlCoAcreditadoEstado_SelectedIndexChanged" AutoPostBack="true" Width="200px">
                    </asp:DropDownList>
                </td>
                <td width="154px">
                </td>
                <td>
                    <asp:Label ID="lblCoAcreditadoDM" Text="Delegación o Municipio" runat="server" Font-Size="11pt" CssClass="Label" Width="150px" ForeColor="#666666" />
                </td>
                <td>
                    <asp:DropDownList ID="ddlCoAcreditadoDM" runat="server" CssClass="DropDownList" Font-Size="11px" Width="200px">
                    </asp:DropDownList>
                </td>
                <td width="154px">
                </td>
            </tr>
        </table>
        </ContentTemplate>
        </asp:updatepanel>
        </asp:Panel>
    </div>
    <br>

    <asp:Panel ID="panel" runat="server">
    <asp:Image runat="server" ImageUrl="../SupplierModule/images/t8.png"/>
    <table Xwidth="50px">
        <tr>
            <td>
                <div >
                    <asp:GridView ID="gvTecPro" runat="server" AutoGenerateColumns="False"  CellPadding="4" OnDataBound="gvTecPro_DataBound" XWidth="50px">
                    <Columns>
                    <asp:templatefield>
                    <HeaderTemplate>
                    <asp:Image runat="server" ImageUrl="../Resources/Images/tec2.png" />
                    </HeaderTemplate>
                    <ItemTemplate>
                    <asp:DropDownList ID="ddlTecnolog" runat="server" AutoPostBack="true" CssClass="DropDownList_gdv" OnSelectedIndexChanged="ddlTecnolog_SelectedIndexChanged" width="141px">
                    </asp:DropDownList>
                    </ItemTemplate>
                    </asp:templatefield>
                    <asp:templatefield>
                    <HeaderTemplate>
                    <asp:Image runat="server" ImageUrl="../Resources/Images/marca.png" />
                    </HeaderTemplate>
                    <ItemTemplate>
                    <asp:DropDownList ID="ddlMarca" runat="server" AutoPostBack="true" CssClass="DropDownList_gdv" OnSelectedIndexChanged="ddlMarca_SelectedIndexChanged" width="80px">
                    </asp:DropDownList>
                    </ItemTemplate>
                    </asp:templatefield>
                    <asp:templatefield>
                    <HeaderTemplate>
                    <asp:Image runat="server" ImageUrl="../Resources/Images/modelo.png" />
                    </HeaderTemplate>
                    <ItemTemplate>
                    <asp:DropDownList ID="ddlProduct" runat="server" AutoPostBack="true" CssClass="DropDownList_gdv" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged" width="110px">
                    </asp:DropDownList>
                    </ItemTemplate>
                    </asp:templatefield>
                    <%--  add by coco 20110823--%><asp:templatefield>
                    <HeaderTemplate>
                    <asp:Image ID="Image1" runat="server" ImageUrl="../Resources/Images/tipo.png" />
                    </HeaderTemplate>
                    <ItemTemplate>
                    <asp:DropDownList ID="ddlTypeOfProduct" runat="server" AutoPostBack="true" CssClass="DropDownList_gdv" OnSelectedIndexChanged="ddlTypeOfProduct_SelectedIndexChanged" width="250px">
                    </asp:DropDownList>
                    </ItemTemplate>
                    </asp:templatefield>
                    <%--     end add--%><asp:templatefield>
                    <HeaderTemplate>
                    <asp:Label ID="lblCantidad" runat="server" CssClass="titulo" Text="Cantidad"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                    <asp:TextBox ID="txtCantidad" runat="server" CssClass="TextBox_gdvc" AutoPostBack="true" OnTextChanged="txtCantidad_TextChanged"></asp:TextBox>
                    </ItemTemplate>
                    </asp:templatefield>
                    <asp:templatefield>
                    <HeaderTemplate>
                    <asp:Label runat="server" CssClass="titulo" Text="Precio Unitario s/IVA"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                    <asp:TextBox ID="txtGridText1" runat="server" CssClass="TextBox_gdv" style="text-align:right" Enabled="false" width="90px"></asp:TextBox>
                    </ItemTemplate>
                    </asp:templatefield>
                    <asp:templatefield>
                    <HeaderTemplate>
                    <asp:Label ID="lblCapacidad" runat="server" CssClass="titulo" Text="Capacidad"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                    <asp:DropDownList ID="ddlCapacidad" runat="server" CssClass="DropDownList_gdv">
                    </asp:DropDownList>
                    </ItemTemplate>
                    </asp:templatefield>
                    <asp:templatefield>
                    <HeaderTemplate>
                    <asp:Label runat="server" CssClass="titulo" Text="Importe Total s/IVA"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                    <asp:TextBox ID="txtSubtotal" runat="server" style="text-align:right" CssClass="TextBox_gdv" Enabled="false" width="80px"></asp:TextBox>
                    </ItemTemplate>
                    </asp:templatefield>
                    <asp:templatefield>
                    <HeaderTemplate>
                    <asp:Label runat="server" CssClass="titulo" Text="Gastos de Intalación y Mano de Obra s/IVA"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                    <asp:TextBox ID="txtGastos" runat="server" style="text-align:right" CssClass="TextBox_gdv" Enabled="false" Xwidth="80px"></asp:TextBox>
                    </ItemTemplate>
                    </asp:templatefield>
                    </Columns>
                    </asp:GridView>
                    <div style="float:left;">
                        <asp:Literal ID="lDescription" runat="server" Visible="false"></asp:Literal>
                    </div>
                    <div align="right">
                        &nbsp;
                        <asp:Label ID="Label5" Text="Subtotal" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="txtSubTotal" runat="server" Enabled="false" Width="127px" style="text-align:right"/><br />
                        <asp:Label Text="IVA" runat="server" ID="Label6" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="txtIVA" runat="server" Enabled="false" Width="127px" style="text-align:right"/><br />
                        <asp:Label Text="Costo Equipo(s)" runat="server" ID="Label7" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="txtCostEquipo" runat="server" Enabled="false" Width="127px" style="text-align:right"/><br />
                        <asp:Label ID="Label1" Text="Costo Acopio y Destrucción" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="txtTotalCost" runat="server" Enabled="false" Width="127px" style="text-align:right"/><br />
                        <asp:Label ID="Label2" Text="Incentivo (10%)" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="txtTotalDescount" runat="server" Enabled="false" Width="127px" style="text-align:right" /><br />
                        <asp:Label Text="Descuento" runat="server" ID="Label8" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="txtDescount" runat="server" Enabled="false" Width="127px" style="text-align:right"/><br />
                        <asp:Label ID="lblTotal" runat="server" Text="Total">
                        </asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="txtTotal" runat="server" Enabled="false" style="text-align:right">
                        </asp:TextBox>
                        &nbsp;</div>
                </div>
            </td>
        </tr>
    </table>
    <div id="GridBtn" runat="server">
        <asp:Button ID="btnAddRow" Text="Agregar" runat="server" CssClass="Button" OnClientClick="return confirm('¿ Desea Agregar una Tecnología al Presupuesto de Inversión ?');"
        OnClick="btnAddRow_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnClear" Text="Borrar Presupuesto" runat="server" CssClass="Button"
        OnClick="btnClear_Click" OnClientClick="return confirm('Confirmar Borrar el Presupuesto de Inversión Calculado');" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </div>

    </asp:Panel>
    <br />
    <asp:Panel ID="Page4" runat="server" >
    <asp:Image runat="server" ImageUrl="../SupplierModule/images/t_solicitud2.png"/>
    <table width="100%">
        <tr>
            <td width="150px">
                <asp:Label ID="lblNo_Plazo_Pago" Text="Número de Pagos" Font-Size="11pt" runat="server" CssClass="Label" Width="150px" ForeColor="#666666">
                </asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtNo_Plazo_Pago" runat="server" CssClass="TextBox" Font-Size="11px" Enabled="false" Width="200px">
                </asp:TextBox>
            </td>
            <td width="150px">
                <asp:Label ID="lblDx_periodo_pago" Text="Periodicidad de Pago" runat="server" Font-Size="11pt" CssClass="Label" Width="150px" ForeColor="#666666">
                </asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlDx_periodo_pago" runat="server" CssClass="DropDownList" Font-Size="11px" Enabled="false" Width="200px">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    </asp:Panel>
    <br />
    <div>
        <div>
            <asp:Button ID="btnSave" runat="server" Text="Guardar" CssClass="Button" OnClick="btnSave_Click"
            OnClientClick="return confirm('Confirmar Guardar Solicitud de Crédito');" />&nbsp
            &nbsp
            <asp:Button ID="btnCancel" runat="server" Text="Salir" CssClass="Button" OnClientClick="return confirm('¿ Desea Salir de ésta Pantalla ?');"
            OnClick="btnCancel_Click" />
            <%--Add by Tina 2011/08/09--%>&nbsp &nbsp
            <asp:Button ID="btnCreditHistroyReview" runat="server" Text="Otros Datos"
            CssClass="Button_1" OnClientClick="return confirm('¿ Desea Revisar la Segunda Sección de la Solicitud de Crédito ?');"
            onclick="btnCreditHistroyReview_Click" />
            <%--End--%></div>
    </div>
    </asp:Content>
