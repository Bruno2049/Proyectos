<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="RoleManagement.aspx.cs"
    Inherits="PAEEEM.RoleManagement" Title="SIP" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Resources/Css/Pager.css" rel="stylesheet" type="text/css" />
    <link href="../Resources/Css/GridView.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .Label
        {
            width: 160px;
            color:Maroon
        }
        .CenterButton
        {
                width:120px;                
                margin-right:5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
        <ContentTemplate>
            <div  >
            <div>
            <br>
            <div>
            <asp:Image runat="server" ImageUrl="../SupplierModule/images/t_administrador.png" />
            </div>
            </div>
            <br />

                <asp:GridView ID="gvRoleList" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                    AllowPaging="True" PageSize="20" DataKeyNames="Id_Rol" 
                    OnRowCommand="OnRowCommand" OnRowCreated="OnRowCreated">
                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <PagerStyle CssClass="GridViewPagerStyle" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                    <EditRowStyle CssClass="GridViewEditStyle" />
                    <Columns>
                        <asp:BoundField DataField="Id_Rol" HeaderText="ID Rol" Visible="false"/>
                        <asp:BoundField DataField="Nombre_Rol" HeaderText="Nombre del Rol" />
                        <asp:TemplateField ShowHeader="true">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEdit" Text="Editar" CommandName="Editar" OnClientClick="return confirm('Confirmar Editar el Rol Seleccionado');" runat="server" CssClass="CenterButton"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerTemplate>
                    </PagerTemplate>
                </asp:GridView>
                <webdiyer:AspNetPager ID="AspNetPager1" CssClass="pagerDRUPAL" runat="server" PageSize="20"
                    AlwaysShow="True" ShowCustomInfoSection="Right" ShowDisabledButtons="true" ShowPageIndexBox="always"
                    PageIndexBoxType="DropDownList" CustomInfoHTML="Page:<font color='red'><b>%currentPageIndex%</b></font>/%PageCount%&nbsp;&nbsp;PageSize:%PageSize%&nbsp;&nbsp;Record:%StartRecordIndex%-%EndRecordIndex% of %RecordCount%"
                    UrlPaging="true" OnPageChanged="AspNetPager1_PageChanged" FirstPageText="Primero"
                    LastPageText="Último" NextPageText="Siguiente" PrevPageText="Anterior" CurrentPageButtonClass="cpb">
                </webdiyer:AspNetPager>
            </div>                                <br />
            <div align="right">
                <asp:Button ID="btnAddUser" runat="server" Text="Agregar" OnClientClick="return confirm('Confirmar Agregar un Nuevo Rol');"
                    CssClass="CenterButton" onclick="btnAddUser_Click" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
