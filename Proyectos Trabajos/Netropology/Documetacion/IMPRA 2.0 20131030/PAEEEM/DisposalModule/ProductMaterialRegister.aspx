<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductMaterialRegister.aspx.cs"
    Inherits="PAEEEM.DisposalModule.ProductMaterialRegister" Title="Recuperación de Residuos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Resources/Css/Pager.css" rel="stylesheet" type="text/css" />
    <link href="../Resources/Css/GridView.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .Label
        {
            width: 160px;
            color: #333333;
            font-size: 16px;
        }
        .TextBox
        {
            width: 330px;
        }
        .Button
        {
            width: 120px;
        }
        .CenterButton
        {
            width: 120px;
            margin-left: 50%;
            margin-right: auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="panel" runat="server">
        <ContentTemplate>
            <div id="container">
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
                        <td>
                            <div>
                                <asp:Label ID="Label2" Text="Programa" runat="server" Font-Size="11pt" CssClass="Label"
                                    Width="70px" /></div>
                        </td>
                        <td>
                            <div>
                                <asp:TextBox ID="txtProgram" runat="server" Font-Size="11px" CssClass="TextBox" Enabled="False"></asp:TextBox>
                            </div>
                        </td>
                        <td>
                            <div>
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:Label ID="Label3" Text="Tecnología" Font-Size="11pt" runat="server" CssClass="Label"
                                    Width="70px" /></div>
                        </td>
                        <td>
                            <div>
                                <asp:TextBox ID="txtTechnology" runat="server" Font-Size="11px" CssClass="TextBox"
                                    Enabled="False"></asp:TextBox></div>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:GridView ID="grdMaterial" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                    DataKeyNames="ID" OnRowDataBound="grdMaterial_RowDataBound">
                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <PagerStyle CssClass="GridViewPagerStyle" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                    <Columns>
                        <asp:BoundField HeaderText="Material Recuperado" DataField="Dx_Material">
                        </asp:BoundField>
                        <asp:BoundField DataField="Dx_Subtipo" HeaderText="Subtipo" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="ckbSelect" runat="server" AutoPostBack="True" OnCheckedChanged="ckbSelect_CheckedChanged" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Dx_Metrica" HeaderText="Métrica" />
                        <asp:TemplateField HeaderText="Peso Inicial">
                            <ItemTemplate>
                                <asp:TextBox ID="txtWeight" runat="server"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="debe ser un valor numérico"
                                    ValidationExpression="^[0-9]+(\.[0-9]{0,2})?$" ControlToValidate="txtWeight"
                                    ValidationGroup="weight"></asp:RegularExpressionValidator>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Cve_Material" HeaderText="Material"></asp:BoundField>
                    </Columns>
                </asp:GridView>
                <br />
                <div align="right">
                    <asp:Button ID="btnSave" Text="Guardar" runat="server" OnClientClick="return confirm(' Confirmar Guardar el Registro de Recuperación de Residuos');"
                        CssClass="CenterButton" OnClick="btnSave_Click" />
                    <asp:Button ID="btnCancel" Text="Cancelar" runat="server" OnClientClick="return confirm(' Confirmar salir de ésta Pantalla');"
                        CssClass="CenterButton" OnClick="btnCancel_Click" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
