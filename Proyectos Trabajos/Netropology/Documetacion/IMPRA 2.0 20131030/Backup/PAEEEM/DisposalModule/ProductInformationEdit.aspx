<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ProductInformationEdit.aspx.cs"
    Inherits="PAEEEM.DisposalModule.ProductInformationEdit" Title="Editar Equipo Baja Eficiencia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .Label
        {
            width: 120px;
            color: #333333;
            font-size: 16px;
        }
        .TextBox
        {
            width: 70%;
        }
        .Button
        {
            width: 120px;
        }
        .style1
        {
            height: 26px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
                <br>
                <asp:Image ID="Image1" runat="server" ImageUrl="" />
            </div>
            <div id="dtFecha" align="right">
                <asp:Label ID="lblFecha" runat="server" Text="Fecha" CssClass="Label"></asp:Label>
                &nbsp;<asp:TextBox ID="txtFecha" runat="server" Enabled="False" BorderWidth="0" /></div>
            <br />
            <div>
                <table width="100%">
                    <tr>
                        <td width="100px">
                            <div>
                                <asp:Label ID="lblTechnology" runat="server" Text="Tecnología" CssClass="Label"></asp:Label>
                            </div>
                        </td>
                        <td width="280px">
                            <div>
                                <asp:TextBox ID="txtTechnology" runat="server" CssClass="TextBox" Enabled="false"></asp:TextBox>
                            </div>
                        </td>
                        <td width="100px">
                            <div>
                                <asp:Label ID="lblTypeProduct" runat="server" Text="Tipo Producto" CssClass="Label"></asp:Label>
                            </div>
                        </td>
                        <td width="280px">
                            <div>
                                <asp:TextBox ID="txtTipoProduct" runat="server" CssClass="TextBox" Enabled="false"></asp:TextBox>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td width="100px">
                            <div>
                                <asp:Label ID="lblModelo" runat="server" Text="Modelo" CssClass="Label"></asp:Label>
                            </div>
                        </td>
                        <td width="280px">
                            <div>
                                <asp:TextBox ID="txtModelo" runat="server" CssClass="TextBox"></asp:TextBox>
                            </div>
                        </td>
                        <td width="100px">
                            <div>
                                <asp:Label ID="lblMarca" runat="server" Text="Marca" CssClass="Label"></asp:Label>
                            </div>
                        </td>
                        <td width="280px">
                            <div>
                                <asp:TextBox ID="txtMarca" runat="server" CssClass="TextBox"></asp:TextBox>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td width="100px">
                            <div>
                                <asp:Label ID="lblNoSerie" runat="server" Text="No. Serie" CssClass="Label"></asp:Label>
                            </div>
                        </td>
                        <td width="280px">
                            <div>
                                <asp:TextBox ID="txtNoSerie" runat="server" CssClass="TextBox"></asp:TextBox>
                            </div>
                        </td>
                        <td width="100px">
                            <div>
                                <asp:Label ID="Label1" runat="server" Text="Color" CssClass="Label"></asp:Label>
                            </div>
                        </td>
                        <td width="280px">
                            <div>
                                <asp:TextBox ID="txtColor" runat="server" CssClass="TextBox"></asp:TextBox>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td width="100px">
                            <div>
                                <asp:Label ID="lblPeso" runat="server" Text="Peso" CssClass="Label"></asp:Label>
                            </div>
                        </td>
                        <td width="280px">
                            <div>
                                <asp:TextBox ID="txtPeso" runat="server" CssClass="TextBox"></asp:TextBox>
                                <asp:RegularExpressionValidator runat="server" ValidationExpression="^\d+(\.\d+)?$"
                                    ControlToValidate="txtPeso" ErrorMessage="(*) Campo Vacío o con Formato Inválido" ValidationGroup="btnSave" ID="RegularExpressionValidator15"></asp:RegularExpressionValidator>
                            </div>
                        </td>
                        <td width="100px">
                            <div>
                                <asp:Label ID="lblCapacidad" runat="server" Text="Capacidad" CssClass="Label"></asp:Label>
                            </div>
                        </td>
                        <td width="280px">
                            <div>
                                <asp:TextBox ID="txtCapacidad" runat="server" CssClass="TextBox"></asp:TextBox>
                                <asp:RegularExpressionValidator runat="server" ValidationExpression="^\d+(\.\d+)?$"
                                    ControlToValidate="txtCapacidad" ErrorMessage="(*) Campo Vacío o con Formato Inválido" ValidationGroup="btnSave" ID="RegularExpressionValidator1"></asp:RegularExpressionValidator>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td width="100px">
                            <div>
                                <asp:Label ID="lblAntiguedad" runat="server" Text="Antiguedad" CssClass="Label"></asp:Label>
                            </div>
                        </td>
                        <td width="280px">
                            <div>
                                <asp:TextBox ID="txtAntiguedad" runat="server" CssClass="TextBox"></asp:TextBox>
                            </div>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <div>
                <asp:Button ID="btnSave" runat="server" Text="Guardar" CssClass="Button" OnClick="btnSave_Click"
                    OnClientClick="return confirm('Confirmar Guardar Datos')" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnCancel" runat="server" Text="Salir" CssClass="Button" OnClick="btnCancel_Click"
                    OnClientClick="return confirm('¿ Desea Salir de ésta Pantalla ?')" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
