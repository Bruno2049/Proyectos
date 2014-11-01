<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    Async="true" CodeBehind="UserEdition.aspx.cs" Inherits="PAEEEM.UserEdition" Title="Edición de Usuarios"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<style type="text/css">
        .Label
        {
            width: 160px;
            color:#333333;
        }
        .TextBox
        {
            width: 480px;
        }
        .DropDownList
        {
            width: 480px;
        }
        .Button
        {
            width:120px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
        <ContentTemplate>
        
        <div>
        <br>
        <asp:Image runat="server" ImageUrl="../SupplierModule/images/t_altausu.png" />
        </div>
            <br />
            <div align="right">
                <asp:Label Text="Fecha" runat="server" CssClass="Label" />&nbsp &nbsp
                &nbsp &nbsp
                <asp:Literal ID="literalFecha" runat="server"/>
            </div>
            <div>
                <asp:Label ID="Label1" Text="Nombre Completo del usuario" runat="server" CssClass="Label" />&nbsp
                <asp:TextBox ID="txtFullUserName" runat="server" CssClass="TextBox"/>&nbsp<span style="color: Red;
                font-style: italic">(Campo Obligatorio)</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFullUserName"
                    ErrorMessage="(*) Campo Vacío o con Formato Inválido" ValidationGroup="Save"></asp:RequiredFieldValidator>
            </div>
            <div>
                    <asp:Label Text="Usuario" runat="server" CssClass="Label"/>&nbsp
                    <asp:TextBox ID="txtUserName" runat="server" CssClass="TextBox"/>&nbsp<span style="color: Red;
                    font-style: italic">(Campo Obligatorio)</span>
                    <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="txtUserName"
                        ErrorMessage="(*) Campo Vacío o con Formato Inválido" ValidationGroup="Save"></asp:RequiredFieldValidator>
                </div>
                <div>
                    <asp:Label Text="Correo Electrónico" runat="server" CssClass="Label"/>&nbsp
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="TextBox"/>&nbsp<span style="color: Red;
                    font-style: italic">(Campo Obligatorio)</span>
                    <asp:RegularExpressionValidator ID="EmailValidator" ControlToValidate="txtEmail"
                        ErrorMessage="(*) Campo Vacío o con Formato Inválido" ValidationExpression="^(([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+([;.](([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+)*$"
                        runat="server" ValidationGroup="Save"></asp:RegularExpressionValidator>
                </div>
                <div>
                    <asp:Label Text="Teléfono" runat="server" CssClass="Label" />&nbsp
                <asp:TextBox ID="txtPhone" runat="server" CssClass="TextBox" />&nbsp<span style="color: Red;
                    font-style: italic">(10 Dígitos Numéricos)</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                        ValidationGroup="Save" ControlToValidate="txtPhone"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="PhoneValidator" ControlToValidate="txtPhone"
                            ErrorMessage="(*) Campo Vacío o con Formato Inválido" runat="server" ValidationExpression="^((\d{10}))$"
                            ValidationGroup="Save" />
                </div>
                <div>
                    <asp:Label Text="Rol del Usuario" runat="server" CssClass="Label" />&nbsp
                <asp:DropDownList ID="drpRole" runat="server" AutoPostBack="true" CssClass="DropDownList"
                    OnSelectedIndexChanged="drpRole_SelectedIndexChanged" />&nbsp<span style="color: Red;
                    font-style: italic">(Campo Obligatorio)</span>
                </div>
                <div id="department" style="display:none" runat="server">
                <asp:Label ID="lblDepartment" Text="Departamento" runat="server" CssClass="Label" />&nbsp
                <asp:DropDownList ID="drpDepartment" runat="server" AutoPostBack="true" CssClass="DropDownList"
                    OnSelectedIndexChanged="drpDepartment_SelectedIndexChanged" />&nbsp<span style="color: Red;
                    font-style: italic">(Campo Obligatorio)</span>
                    <%--<asp:RequiredFieldValidator ID="Valdepartment" runat="server" ErrorMessage="(*) Debe Seleccionar un Valor"
                        ValidationGroup="Save" ControlToValidate="drpDepartment"></asp:RequiredFieldValidator>--%>
                </div>
                <div id="branch" style="display:none" runat="server">
                <asp:Label ID="lblBranch" Text="Sucursal" runat="server" CssClass="Label" />&nbsp
                    <asp:DropDownList ID="drpbranch" runat="server" CssClass="DropDownList" AutoPostBack="true"
                        onselectedindexchanged="drpbranch_SelectedIndexChanged" />&nbsp<span style="color: Red;
                    font-style: italic">(Campo Obligatorio)</span>
                </div>
                <div id="status">
                    <asp:Label Text="Estatus" runat="server" CssClass="Label" />&nbsp
                    <asp:DropDownList ID="drpStatus" runat="server" CssClass="DropDownList">
                        <asp:ListItem Text="Pendiente"  Value="P" />
                        <asp:ListItem Text="Activo"  Value="A" Selected="True"/>
                        <asp:ListItem Text="Inactivo"  Value="I" />
                        <asp:ListItem Text="Cancelado"  Value="C" />
                    </asp:DropDownList>
                </div>
            <br />
            <div>
                <asp:Button ID="btnSave" runat="server" Text="Guardar" OnClick="btnSaveUser_Click"
                    ValidationGroup="Save" CssClass="Button" OnClientClick="var result = confirm('Confirmar Guardar Usuario'); if(result){document.body.style.cursor = 'wait'; } return result;"/>&nbsp &nbsp
                <asp:Button ID="btnCancel" runat="server" Text="Salir" OnClick="btnCancelUser_Click"
                    CssClass="Button" OnClientClick="return confirm('¿ Desea Salir de ésta Pantalla ?');"/>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>