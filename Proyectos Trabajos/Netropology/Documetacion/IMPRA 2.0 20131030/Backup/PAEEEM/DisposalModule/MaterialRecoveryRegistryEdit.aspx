<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MaterialRecoveryRegistryEdit.aspx.cs"
    Inherits="PAEEEM.DisposalModule.MaterialRecoveryRegistryEdit" Title="Registro de Recuperación de Residuos y Materiales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .Label
        {
            width: 90px;
            color: #333333;
            font-size: 16px;
        }
        .Label_1
        {
            width: 260px;
            color: #333333;
            font-size: 16px;
        }
        .TextBox
        {
            width: 250px;
        }
        .Button
        {
            width: 150px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="panel" runat="server">
        <ContentTemplate>
            <div>
            <div align="center">
                    <br>
                    <asp:Label ID="lblTitle" runat="server" Text="REGISTRO DE RECUPERACION DE MATERIALES:"></asp:Label>
                    <asp:Literal ID="literalMaterial" runat='server' />
                </div>
                <div>
                    <br>
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/DisposalModule/images/t_equipos1.png" />
                </div>
                <div align="right">
                    <asp:Label ID="Label1" Text="Fecha" runat="server" CssClass="Label" />&nbsp
                    <asp:Literal ID="literalFecha" runat='server' />
                </div>
                <br />
                <table width="100%">
                    <tr>
                        <td width="11%">
                            <asp:Label ID="Label2" runat="server" Text="Programa" CssClass="Label"></asp:Label>
                        </td>
                        <td width="31%">
                            <asp:TextBox ID="txtProgram" runat="server" CssClass="TextBox" Enabled="false"></asp:TextBox>
                        </td>
                        <td width="28%">
                            <asp:Label ID="Label3" runat="server" Text="Seleccionar Fecha de Recuperación" CssClass="Label_1"></asp:Label>
                        </td>
                        <td width="30%">
                            <asp:TextBox ID="txtRecoveryDate" runat="server" CssClass="TextBox" Width="200px" 
                                Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="11%">
                            <asp:Label ID="Label4" runat="server" Text="Tecnología" CssClass="Label"></asp:Label>
                        </td>
                        <td width="31%">
                            <asp:TextBox ID="txtTechnology" runat="server" CssClass="TextBox" Enabled="False"></asp:TextBox>
                        </td>
                        <td width="28%">
                        </td>
                        <td width="30%">
                        </td>
                    </tr>
                </table>
                <br />
                <table width="100%">
                    <tr>
                        <td width="42%" colspan="2">
                            <asp:Label ID="lblMaterialName" runat="server" Text="Gas Refrigerante" CssClass="Label_1"></asp:Label>
                            &nbsp;
                        </td>
                        <td width="58%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td width="11%">
                            <asp:Label ID="Label7" runat="server" Text="Peso Recuperado" CssClass="Label"></asp:Label>
                        </td>
                        <td width="31%">
                            <asp:TextBox ID="txtWeight" runat="server" CssClass="TextBox" ToolTip="(*) Campo Vacío o con Formato Inválido"
                               ></asp:TextBox>
                            &nbsp;<asp:Literal ID="lblUnit" runat='server' Text="Kgs." />
                        </td>
                        <td width="58%">
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtWeight"
                                ErrorMessage="(*) Campo Vacío o con Formato Inválido"></asp:RequiredFieldValidator>--%>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                            ControlToValidate="txtWeight" ValidationExpression="^\d+(\.\d+)?$"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                </table>
                <br />
                <table width="100%">
                    <tr>
                        <td width="45%" style="text-align: right">
                            <asp:Button ID="btnRecovery" runat="server" Text="Registrar" CssClass="Button"
                                
                                OnClientClick="return confirm('Confirmar, ¡ se Sobreescribirá el Peso Inicialmente Capturado para este Lote de Equipos !');" 
                                onclick="btnRecovery_Click" />
                        </td>
                        <td width="10%">
                        </td>
                        <td width="45%" style="text-align: left">
                            <asp:Button ID="btnCancel" runat="server" Text="Salir" CssClass="Button" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
