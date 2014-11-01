<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreditMonitor.aspx.cs"
    Inherits="PAEEEM.DisposalModule.CreditMonitor" Title="Monitor de Credito" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Resources/Css/Pager.css" rel="stylesheet" type="text/css" />
    <link href="../Resources/Css/GridView.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
            var last=null;
            function judge(obj)
            {
              if(last==null)
              {
                 last=obj.id;
              }
              else
              {
                var lo=document.getElementById(last);
                lo.checked=false;
                last=obj.name;
              }
              obj.checked="checked";
    }
    </script>

    <style type="text/css">
        .Label
        {
            width: 160px;
            color: #333333;
            font-size: 16px;
        }
        .DropDownList
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
                                <asp:Label ID="Label2" Text="Distribuidor" runat="server" Font-Size="11pt" CssClass="Label"
                                    Width="70px" /></div>
                        </td>
                        <td>
                            <div>
                                <asp:DropDownList ID="drpDistributor" runat="server" Font-Size="11px" AutoPostBack="true"
                                    CssClass="DropDownList" OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged" />                                   
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
                                <asp:DropDownList ID="drpTechnology" AutoPostBack="true" Font-Size="11px" runat="server"
                                    CssClass="DropDownList" OnSelectedIndexChanged="drpTechnology_SelectedIndexChanged" />
                            </div>
                        </td>
                    </tr>
                </table>
                <br />
                <div>
                    <asp:GridView ID="grdCredit" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                        AllowPaging="True" PageSize="20" DataKeyNames="No_Credito" OnRowDataBound="grdCredit_RowDataBound">
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <Columns>
                            <asp:BoundField DataField="No_Credito" HeaderText="Num. Crédito"></asp:BoundField>
                            <asp:BoundField DataField="Dx_Razon_Social" HeaderText="Beneficiario" />
                            <asp:BoundField DataField="Dx_Domicilio_Fisc" HeaderText="Domicilio" />
                            <asp:BoundField DataField="Dx_Nombre_Programa" HeaderText="Programa" />
                            <asp:BoundField DataField="proveedorName" HeaderText="Distribuidor">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Seleccionar" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:RadioButton ID="RadioButton1" runat="server" GroupName="Seleccionar" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerTemplate>
                        </PagerTemplate>
                    </asp:GridView>
                    <webdiyer:AspNetPager ID="AspNetPager" CssClass="pagerDRUPAL" runat="server" PageSize="20"
                        AlwaysShow="True" ShowCustomInfoSection="Right" ShowDisabledButtons="true" ShowPageIndexBox="always"
                        PageIndexBoxType="DropDownList" CustomInfoHTML="Page:<font color='red'><b>%currentPageIndex%</b></font>/%PageCount%&nbsp;&nbsp;PageSize:%PageSize%&nbsp;&nbsp;Record:%StartRecordIndex%-%EndRecordIndex% of %RecordCount%"
                        UrlPaging="true" OnPageChanged="AspNetPager_PageChanged" OnPageChanging="AspNetPager_PageChanging"
                        FirstPageText="Primero" LastPageText="Último" NextPageText="Siguiente" PrevPageText="Anterior"
                        CurrentPageButtonClass="cpb">
                    </webdiyer:AspNetPager>
                </div>
                <br />
                <div align="right">
                    <asp:Button ID="btnScanCredit" Text="Recepción Equipo" runat="server" OnClientClick="return confirm(' ¿ Desea realizar la Recepción del (los) equipo(s) incluido(s) en ésta Solicitud de Crédito ?');"
                        CssClass="CenterButton" OnClick="btnScanCredit_Click" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
