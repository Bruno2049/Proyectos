<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AssignProductToSupplier.aspx.cs"
    Inherits="PAEEEM.RegionalModule.AssignProductToSupplier" Title="Asignación de Productos a Proveedores" %>

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
            width: 300px;
        }
        .ListBox
        {
            width: 100%;
            height: 300px;
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
            <div id="container">
                <div>
                    <br>
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/DisposalModule/images/t_equipos21.png" />
                </div>
                <div align="right">
                    <asp:Label ID="Label1" Text="Fecha" runat="server" CssClass="Label" />&nbsp
                    <asp:Literal ID="literalFecha" runat='server' />
                </div>
                <br />
                <table width="100%">
                    <tr>
                        <td width="18%">
                            <asp:Label ID="Label4" Text="Nombre o Razón Social" runat="server" CssClass="Label" />
                        </td>
                        <td width="32%">
                            <asp:TextBox ID="txtSociaNamel" runat="server" CssClass="TextBox" Enabled="False"></asp:TextBox>
                        </td>
                        <td width="18%">
                            <asp:Label ID="Label5" Text="Nombre Comercial" runat="server" CssClass="Label" />
                        </td>
                        <td width="32%">
                            <asp:TextBox ID="txtBusinessName" runat="server" CssClass="TextBox" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <br />
                <table width="100%">
                <tr>
                <td width="100%">
                <asp:GridView ID="grdProducts" runat="server" AutoGenerateColumns="False" 
                        CssClass="GridViewStyle" ondatabound="grdProducts_DataBound" 
                        >
                 <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                    <Columns>
                        <asp:TemplateField HeaderText="Tecnología">
                            <ItemTemplate>
                                <asp:DropDownList ID="drpTechnology" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="drpTechnology_SelectedIndexChanged">
                                </asp:DropDownList>
                            </ItemTemplate>
                            <ItemStyle Width="24%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Marca">
                            <ItemTemplate>
                                <asp:DropDownList ID="drpMarca" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="drpMarca_SelectedIndexChanged">
                                </asp:DropDownList>
                            </ItemTemplate>
                             <ItemStyle Width="15%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Producto">
                            <ItemTemplate>
                                <asp:DropDownList Enabled="false" ID="drpProduct" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="drpProduct_SelectedIndexChanged">
                                </asp:DropDownList>
                            </ItemTemplate>
                             <ItemStyle Width="30%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Modelo">
                            <ItemTemplate>
                          <asp:DropDownList ID="drpModel" runat="server" Width="100%" AutoPostBack="true" 
                                                OnSelectedIndexChanged="drpModel_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                        <ItemStyle Width="15%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Precio Máximo c/IVA">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMaxPrice" runat="server" Text="Label" Width="100%"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="12%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Precio Unitario c/IVA">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtUnit" runat="server" Width="80%" OnTextChanged="txtUnit_Onchanged"
                                                AutoPostBack="true" CausesValidation="true"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUnit"
                                                ValidationGroup="Save" ErrorMessage=" (*)"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator runat="server" ValidationExpression="^\d+(\.\d+)?$"
                                                ValidationGroup="Save" ControlToValidate="txtUnit" ErrorMessage=" (*)" ID="ValidateDisposalUnit"></asp:RegularExpressionValidator>
                                        </ItemTemplate>
                                        <ItemStyle Width="15%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnDelete" runat="server" Text="Borrar" Width="100%" OnClick="btnDelete_Click" />
                                        </ItemTemplate>
                                        <ItemStyle Width="8%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProduct" runat="server" Text=""></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                
                <br />
                <table width="100%">
                <tr>
                <td width="30%" style="text-align:right">
                 <asp:Button ID="btnSave" runat="server" Text="Guardar" CssClass="Button" OnClick="btnSave_Click" OnClientClick="return confirm('Confirmar Guardar Asignación de Productos al Proveedor')" />
                </td>
                <td width="5%"></td>
                <td width="30%" style="text-align:center">
                 <asp:Button ID="btnCancel" runat="server" Text="Salir" CssClass="Button" OnClick="btnCancel_Click" OnClientClick="return confirm('¿ Desea Salir de ésta Pantalla ?')" />
                </td>
                <td width="5%"></td>
                <td width="30%" style="text-align:left">
                  <asp:Button ID="btnNewProduct" runat="server" Text="Agregar Producto" 
                        onclick="btnNewProduct_Click" />
                </td>
                </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
