<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DisposalPartialCutsApprovalMonitor.aspx.cs"
    Inherits="PAEEEM.CentralModule.DisposalPartialCutsApprovalMonitor" Title="Monitor Actas Circunstanciadas" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

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
        .DropDownList
        {
            width: 300px;
        }
        .CenterButton
        {
            width: 150px;
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
                                <asp:Label ID="Label3" Text="Programa" runat="server" Font-Size="11pt" CssClass="Label"
                                    Width="120px" /></div>
                        </td>
                        <td>
                            <div>
                                <asp:DropDownList ID="drpProgram" runat="server" Font-Size="11px" AutoPostBack="true"
                                    CssClass="DropDownList" OnSelectedIndexChanged="drpProgram_SelectedIndexChanged" />
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:Label ID="Label5" Text="CAyD" Font-Size="11pt" runat="server"
                                    CssClass="Label" Width="150px" /></div>
                        </td>
                        <td>
                            <div>
                                <asp:DropDownList ID="drpDisposal" AutoPostBack="true" Font-Size="11px" runat="server"
                                    CssClass="DropDownList" OnSelectedIndexChanged="drpDisposal_SelectedIndexChanged" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div>
                                <asp:Label ID="Label2" Text="Fecha Creación" runat="server" Font-Size="11pt" CssClass="Label"
                                    Width="120px" /></div>
                        </td>
                        <td>
                            <div>
                                <asp:DropDownList ID="drpCreateDate" runat="server" Font-Size="11px" AutoPostBack="true"
                                    CssClass="DropDownList" OnSelectedIndexChanged="drpCreateDate_SelectedIndexChanged" />
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:Label ID="Label4" Text="Estatus" Font-Size="11pt" runat="server" CssClass="Label"
                                    Width="150px" /></div>
                        </td>
                        <td>
                            <div>
                                <asp:DropDownList ID="drpEstatus" AutoPostBack="true" Font-Size="11px" runat="server"
                                    CssClass="DropDownList" OnSelectedIndexChanged="drpEstatus_SelectedIndexChanged" />
                            </div>
                        </td>
                    </tr>
                </table>
                <br />
                <div>
                    <asp:GridView ID="grdPartialCuts" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                        DataKeyNames="Codigo_Partial" AllowPaging="True" PageSize="20" OnRowCommand="grdPartialCuts_RowCommand"
                        OnDataBound="grdPartialCuts_DataBound">
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <Columns>
                            <asp:BoundField DataField="Codigo_Partial" HeaderText="ID Acta"></asp:BoundField>
                            <asp:BoundField DataField="Dx_Nombre_Programa" HeaderText="Programa" />
                            <asp:BoundField DataField="Dt_Fecha_Creacion" HeaderText="Fecha Creación" DataFormatString="{0:yyyy-MM-dd}" />
                            <asp:BoundField DataField="Dx_Razon_Social" HeaderText="CAyD" />
                            <asp:BoundField DataField="Num_Products" HeaderText="Num. Productos" />
                            <asp:BoundField DataField="Dx_Estatus_Disposicion" HeaderText="Estatus" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkApproval" runat="server" CommandName="Approval" OnClientClick="return confirm('Confirmar Revisar Detalle">Revisar para Aprobación</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerTemplate>
                        </PagerTemplate>
                    </asp:GridView>
                    <webdiyer:AspNetPager ID="AspNetPager" CssClass="pagerDRUPAL" runat="server" PageSize="20"
                        AlwaysShow="True" ShowCustomInfoSection="Right" ShowDisabledButtons="true" ShowPageIndexBox="always"
                        PageIndexBoxType="DropDownList" CustomInfoHTML="Page:<font color='red'><b>%currentPageIndex%</b></font>/%PageCount%&nbsp;&nbsp;PageSize:%PageSize%&nbsp;&nbsp;Record:%StartRecordIndex%-%EndRecordIndex% of %RecordCount%"
                        UrlPaging="false" OnPageChanged="AspNetPager_PageChanged" OnPageChanging="AspNetPager_PageChanging"
                        FirstPageText="Primero" LastPageText="Último" NextPageText="Siguiente" PrevPageText="Anterior"
                        CurrentPageButtonClass="cpb">
                    </webdiyer:AspNetPager>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
