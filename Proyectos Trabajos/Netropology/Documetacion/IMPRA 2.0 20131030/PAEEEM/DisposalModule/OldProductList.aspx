<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="OldProductList.aspx.cs"
Inherits="PAEEEM.DisposalModule.OldProductList"  Title="Información de Equipos Baja Eficiencia"%>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Resources/Css/Pager.css" rel="stylesheet" type="text/css" />
    <link href="../Resources/Css/GridView.css" rel="stylesheet" type="text/css" />
    <link href="../Resources/Css/PageMask.css" type="text/css" rel="Stylesheet" />

    <style type="text/css">
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
                <div align="center">
                    <br>
                    <asp:Label ID="lblTitle" runat="server" Text="Información de Equipos Baja Eficiencia"></asp:Label>
                </div>
                <div align="right">
                    <asp:Label ID="Label1" Text="Fecha" runat="server" />&nbsp
                    <asp:Literal ID="literalFecha" runat='server' />
                </div>
                <br />
                <div>
                    <asp:GridView ID="grdProductList" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                        AllowPaging="True" PageSize="20">
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <Columns>
                            <asp:BoundField DataField="No_Credito" HeaderText="Num. Crédito"></asp:BoundField>
                            <asp:BoundField DataField="Dx_Nombre" HeaderText="Tecnología" />
                            <asp:BoundField DataField="Id_Folio" HeaderText="Folio" />
                            <asp:BoundField DataField="Dt_Fecha_Recepcion" HeaderText="Fecha Recepción" DataFormatString="{0:dd-MM-yyyy}" />                            
                        </Columns>
                        <PagerTemplate>
                        </PagerTemplate>
                    </asp:GridView>
                    <webdiyer:AspNetPager ID="AspNetPager" CssClass="pagerDRUPAL" runat="server" PageSize="20"
                        AlwaysShow="True" ShowCustomInfoSection="Right" ShowDisabledButtons="true" ShowPageIndexBox="always"
                        PageIndexBoxType="DropDownList" CustomInfoHTML="Page:<font color='red'><b>%currentPageIndex%</b></font>/%PageCount%&nbsp;&nbsp;PageSize:%PageSize%&nbsp;&nbsp;Record:%StartRecordIndex%-%EndRecordIndex% of %RecordCount%"
                        UrlPaging="false" OnPageChanged="AspNetPager_PageChanged" FirstPageText="Primero"
                        LastPageText="Último" NextPageText="Siguiente" PrevPageText="Anterior" CurrentPageButtonClass="cpb">
                    </webdiyer:AspNetPager>
                </div>
                <br />
                <div align="center">
                    <asp:Button ID="btnReturn" runat="server" Text="Regresar" 
                        onclick="btnReturn_Click"/>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
