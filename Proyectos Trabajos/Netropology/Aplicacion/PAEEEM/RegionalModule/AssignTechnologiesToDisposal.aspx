<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AssignTechnologiesToDisposal.aspx.cs"
    Inherits="PAEEEM.RegionalModule.AssignTechnologiesToDisposal" Title="Asignación de Tecnología a Centro de Acopio y Destrucción" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .Label
        {
            width: 160px;
            color: #333333;
            font-size: 16px;
        }
        .Button
        {
            width: 100px;
        }
        .TextBox
        {
            width:300px;	
        }
        .ListBox
        {
            width:100%;
            height:300px;	
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="panel" runat="server">
        <ContentTemplate>
            <div id="container">
                <div>
                    <br/>
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/DisposalModule/images/t_equipos20.png" />
                </div>
                <div align="right">
                    <asp:Label ID="Label1" Text="Fecha" runat="server" CssClass="Label" />&nbsp
                    <asp:Literal ID="literalFecha" runat='server' />
                </div>
                <br />
                <table width="100%">
                <tr>
                <td width="18%"><asp:Label ID="Label4" Text="Nombre o Razón Social" runat="server" CssClass="Label" /></td>
                 <td width="32%">
                     <asp:TextBox ID="txtSociaNamel" runat="server" CssClass="TextBox" 
                         Enabled="False"></asp:TextBox></td>
                  <td width="18%"><asp:Label ID="Label5" Text="Nombre Comercial" runat="server" CssClass="Label" /></td>
                   <td width="32%">
                       <asp:TextBox ID="txtBusinessName" runat="server" CssClass="TextBox" 
                           Enabled="False"></asp:TextBox></td>
                </tr>
                </table>
                
                <br />
                <table width="100%">
                    <tr>
                        <td width="10%">
                        </td>
                        <td width="30%">
                            <asp:Label ID="Label2" runat="server" Text="Tecnología" CssClass="Label"></asp:Label>
                        </td>
                        <td width="20%">
                        </td>
                        <td width="30%">
                            <asp:Label ID="Label3" runat="server" Text="Tecnología Seleccionada" CssClass="Label"></asp:Label>
                        </td>
                        <td width="10%">
                        </td>
                    </tr>
                    <tr>
                        <td width="10%">
                        </td>
                        <td width="30%" style="vertical-align:top">
                            <asp:ListBox ID="listTechnology" runat="server" CssClass="ListBox" 
                                SelectionMode="Multiple"></asp:ListBox>
                        </td>
                        <td width="20%" style="text-align:center" height="300px">
                            <table width="100px" height="60%">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnSelectMultiple" runat="server" Text="> Agregar" CssClass="Button" 
                                            onclick="btnSelectMultiple_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnRemoveMultiple" runat="server" Text="< Quitar" CssClass="Button" 
                                            onclick="btnRemoveMultiple_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnSelectAll" runat="server" Text=">> Agregar Todo" CssClass="Button" 
                                            onclick="btnSelectAll_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnRemoveAll" runat="server" Text="<< Quitar Todo" CssClass="Button" 
                                            onclick="btnRemoveAll_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="30%" style="vertical-align:top">
                            <asp:ListBox ID="listSelectedTechnology" runat="server" CssClass="ListBox"
                                SelectionMode="Multiple"></asp:ListBox>
                        </td>
                         <td width="10%">
                        </td>
                    </tr>
                </table>

                <br />
                <table width="100%">
                <tr>
                <td width="45%" style="text-align:right">
                    <asp:Button ID="btnSave" runat="server" Text="Guardar" 
                        CssClass="Button" onclick="btnSave_Click" OnClientClick="return confirm('Confirmar Guardar Asignación de Tecnología al CAyD');"/></td>
                 <td width="10%"></td>
                  <td width="45%" style="text-align:left"><asp:Button ID="btnCancel" runat="server" 
                          Text="Salir" CssClass="Button" onclick="btnCancel_Click" OnClientClick="return confirm('¿ Desea Salir de ésta Pantalla ?');"/></td>
                </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
