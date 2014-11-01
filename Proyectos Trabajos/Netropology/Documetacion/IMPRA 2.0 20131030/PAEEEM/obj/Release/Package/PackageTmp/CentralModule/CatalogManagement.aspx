<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CatalogManagement.aspx.cs"
    Title="Administrador de Catálogos" Inherits="PAEEEM.CentralModule.CatalogManagement" %>
    <%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
	<link href="../Resources/Css/Pager.css" rel="stylesheet" type="text/css" />
    <link href="../Resources/Css/GridView.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<%--    <asp:UpdatePanel runat="server" ID="ManagementPanel">
        <ContentTemplate>--%><div>
         
         			<br>
            		<asp:Image runat="server" ImageUrl="../images/t_gestion.png" />
                          
                </div>
            <br />
            <div>
                <asp:Label runat="server" ID="lblCatalog" Text="Catálogo" />&nbsp;&nbsp;&nbsp;&nbsp
                <asp:DropDownList runat="server" ID="drpCatalog" AutoPostBack="true" Width="20%"
                    OnSelectedIndexChanged="drpCatalog_SelectedIndexChanged" />
            </div>
            <br />
            <div align="center">
                <asp:GridView runat="server" ID="gridViewCatalogRecords" AutoGenerateColumns="false"
                    CssClass="GridViewStyle" OnRowDataBound="gridViewCatalogRecords_RowDataBound"
                    OnRowDeleting="gridViewCatalogRecords_RowDeleting" OnRowCommand="gridViewCatalogRecords_RowCommand"
                    OnRowCreated="gridViewCatalogRecords_RowCreated">
                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <PagerStyle CssClass="GridViewPagerStyle" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                </asp:GridView>
                <webdiyer:AspNetPager ID="AspNetPager" CssClass="pagerDRUPAL" runat="server" PageSize="20"
                        AlwaysShow="True" ShowCustomInfoSection="Right" 
                    ShowDisabledButtons="true" ShowPageIndexBox="always"
                        PageIndexBoxType="DropDownList" CustomInfoHTML="Page:<font color='red'><b>%currentPageIndex%</b></font>/%PageCount%&nbsp;&nbsp;PageSize:%PageSize%&nbsp;&nbsp;Record:%StartRecordIndex%-%EndRecordIndex% of %RecordCount%"
                        UrlPaging="false" FirstPageText="First" LastPageText="Last" NextPageText="Next"
                        PrevPageText="Prev" CurrentPageButtonClass="cpb" 
                    OnPageChanged="AspNetPager_PageChanged" Visible="false">
                    </webdiyer:AspNetPager>
            </div>
            <br />
            <div align="center">
                <asp:Button ID="btnAdd" runat="server" Text="Agregar" Visible="false" OnClientClick="return confirm('Confirmar Agregar Nuevo Elemento')"
                    OnClick="btnAdd_Click" />
            </div>
<%--        </ContentTemplate>
    </asp:UpdatePanel>--%></asp:Content>
