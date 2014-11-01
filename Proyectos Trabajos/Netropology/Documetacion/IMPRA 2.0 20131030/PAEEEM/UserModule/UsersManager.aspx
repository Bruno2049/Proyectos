<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="UsersManager.aspx.cs" Inherits="PAEEEM.UsersManager" Async="true" Title="Administrador de Usuarios"%>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<link href="../Resources/Css/Pager.css" rel="stylesheet" type="text/css" />
    <link href="../Resources/Css/GridView.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .Label
        {
            width: 160px;
            color:#333333;
        }
        .CenterButton
        {
                width:120px;
                margin-left:100%;
                margin-right:auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
            <br>
            <div>
            <asp:Image runat="server" ImageUrl="../SupplierModule/images/t_admusuarios.png" />
            </div>
            </div>
            <br />
            <div align="right">
                <asp:Label Text="Fecha" runat="server" CssClass="Label" />&nbsp &nbsp
                &nbsp &nbsp
                <asp:Literal ID="literalFecha" runat="server"  />
            </div>
            <div>
                <br>
                <table width="100%">
                    <tr>
                        <td width="5%">
                            <asp:Label ID="lblRole" runat="server" Text="Rol Asignado" CssClass="Label_1"></asp:Label>
                        </td>
                        <td width="20%">
                            <asp:DropDownList ID="ddlRole" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlRole_SelectedIndexChanged" CssClass="DropDownList">
                            </asp:DropDownList>
                        </td>
                        <td width="5%">
                            <asp:Label ID="lblDepartment" runat="server" CssClass="Label_1" Text="Región"></asp:Label>
                        </td>
                        <td width="20%">
                            <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="True"  OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged"
                                CssClass="DropDownList">
                            </asp:DropDownList>
                        </td>
                        <td width="15%">
                            <asp:Label ID="lblUserName" runat="server" CssClass="Label_1" 
                                Text="Usuario"></asp:Label>
                        </td>
                        <td width="20%">
                            <asp:DropDownList ID="ddlUserName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlUserName_SelectedIndexChanged"
                                CssClass="DropDownList">
                            </asp:DropDownList>
                        </td>
                        <td width="15%">
                        </td>
                    </tr>
                </table>
                <br />
                <asp:GridView ID="gvUserList" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                    AllowPaging="True" PageSize="20" DataKeyNames="Id_Usuario" OnRowCommand="OnRowCommand"
                    OnRowCreated="OnRowCreated" OnDataBound="OnDataBound">
                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <PagerStyle CssClass="GridViewPagerStyle" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                    <Columns>
                        <asp:BoundField DataField="Id_Usuario" HeaderText="UserID" Visible="false" />
                        <asp:BoundField DataField="Nombre_Usuario" HeaderText="Usuario" />
                        <asp:BoundField DataField="Nombre_Rol" HeaderText="Rol Asignado" />
                        <asp:BoundField DataField="Numero_Telefono" HeaderText="Teléfono" />
                        <asp:BoundField DataField="CorreoElectronico" HeaderText="Correo Electrónico" />
                        <asp:BoundField DataField="Estatus" HeaderText="Estatus" />                        
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="linkButtonEdit" runat="server" CausesValidation="false" CommandName="Editar"
                                    OnClientClick="return confirm('Confirmar Editar el Usuario Seleccionado');" Text="Editar" />
                                <asp:LinkButton ID="linkButtonDelete" runat="server" CausesValidation="false" CommandName="Borrar"
                                    OnClientClick="return confirm('Confirmar Borrar el Usuario Seleccionado');" Text="Borrar" />
                                <asp:LinkButton ID="linkButtonActivate" runat="server" CausesValidation="false" CommandName="Activar"
                                    OnClientClick="return confirm('Confirmar Activación del Usuario Seleccionado');" Text="Activar" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerTemplate>
                    </PagerTemplate>
                </asp:GridView>
                <webdiyer:AspNetPager ID="AspNetPager" CssClass="pagerDRUPAL" runat="server" PageSize="20"
                    AlwaysShow="True" ShowCustomInfoSection="Right" ShowDisabledButtons="true" ShowPageIndexBox="always"
                    PageIndexBoxType="DropDownList" CustomInfoHTML="Page:<font color='red'><b>%currentPageIndex%</b></font>/%PageCount%&nbsp;&nbsp;PageSize:%PageSize%&nbsp;&nbsp;Record:%StartRecordIndex%-%EndRecordIndex% of %RecordCount%"
                    UrlPaging="false" OnPageChanged="AspNetPager_PageChanged" FirstPageText="Primero"
                    LastPageText="Último" NextPageText="Siguiente" PrevPageText="Anterior" CurrentPageButtonClass="cpb" onpagechanging="AspNetPager_PageChanging">
                </webdiyer:AspNetPager>
                <div align="right">
                <asp:Button ID="btnAddUser" runat="server" Text="Agregar" OnClick="btnAddUser_Click" CssClass="CenterButton" OnClientClick="return confirm('Confirmar Realizar el Alta de un Nuevo Usuario');"/>
				</div>            
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
