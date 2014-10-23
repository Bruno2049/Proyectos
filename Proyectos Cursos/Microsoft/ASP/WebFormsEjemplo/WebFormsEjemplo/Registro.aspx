<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="WebFormsEjemplo.Formulario_web2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="logo_colour">
        <tr>
            <td>Nombre Del Proyecto</td>
            <td>
                <asp:TextBox ID="TextBox1" runat="server" Width="167px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Tipo de Proyecto</td>
            <td>
                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource1" DataTextField="Nom_Tipo" DataValueField="COD_Tipo">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ProyectosConnectionString %>" SelectCommand="SELECT * FROM [Tipos_Proyectos]"></asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Guardar" />
            </td>
        </tr>
    </table>
</asp:Content>
