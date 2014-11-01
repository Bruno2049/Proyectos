<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DisposedProductsMonitor.aspx.cs"
    Inherits="PAEEEM.DisposalModule.DisposedProductsMonitor" Title="Inhabilitación de Equipos" %>

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
            width: 240px;
        }
        .Button
        {
            width: 120px;
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
                                <asp:Label ID="Label2" Text="Fecha Recepción" runat="server" Font-Size="11pt" CssClass="Label"
                                    Width="120px" /></div>
                        </td>
                        <td>
                            <div>
                                <asp:DropDownList ID="drpReceiptDate" runat="server" Font-Size="11px" AutoPostBack="true"
                                    CssClass="DropDownList" OnSelectedIndexChanged="drpReceiptDate_SelectedIndexChanged" />
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
                        <td>
                            <div>
                                <asp:Label ID="Label4" Text="Estatus" Font-Size="11pt" runat="server" CssClass="Label"
                                    Width="70px" /></div>
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
                    <asp:GridView ID="grdDisposedProducts" runat="server" AutoGenerateColumns="False"
                        CssClass="GridViewStyle" AllowPaging="True" PageSize="20" DataKeyNames="No_Credito,Cve_Tecnologia"
                        OnDataBound="grdDisposedProducts_DataBound" OnRowCommand="grdDisposedProducts_RowCommand"
                        OnRowCreated="grdDisposedProducts_RowCreated">
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <Columns>
                            <asp:BoundField DataField="Cod_Producto" HeaderText="Código Producto"></asp:BoundField>
                            <asp:BoundField DataField="Barcode_Solicitud" HeaderText="Crédito" />
                            <asp:BoundField DataField="Dx_Nombre_General" HeaderText="Tecnología" />
                            <asp:BoundField DataField="Dx_Nombre_Programa" HeaderText="Programa" />
                            <asp:BoundField DataField="Dt_Fecha_Recepcion" HeaderText="Fecha Recepción" DataFormatString="{0:dd-MM-yyyy}" />
                            <asp:BoundField DataField="Dt_Fecha_Inhabilitacion" HeaderText="Fecha Inhabilitación"
                                DataFormatString="{0:dd-MM-yyyy}" />
                            <asp:BoundField DataField="Dt_Fecha_Recuperacion" HeaderText="Fecha Rec. Residuos"
                                DataFormatString="{0:dd-MM-yyyy}" />
                            <asp:BoundField DataField="Fg_Conformidad" HeaderText="Conformidad"></asp:BoundField>
                            <asp:BoundField DataField="No_Credito" HeaderText="Credito"></asp:BoundField>
                            <asp:BoundField DataField="Cve_Tecnologia" HeaderText="Tecnologia"></asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkRegistro" runat="server" CommandName="Registry" OnClientClick="return confirm(' ¿ Desea realizar el registro de Residuos Recuperados del (los) equipo(s)\n incluido(s) en ésta Solicitud de Crédito ?')">Recuperación Residuos</asp:LinkButton>
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
                <br />
                <div align="right">
                    <asp:Button ID="btnGeneratePartialCut" Text="Generar Corte Parcial" runat="server"
                        OnClientClick="return confirm(' ¿ Desea generar un Corte Parcial ?');" CssClass="CenterButton"
                        OnClick="btnGeneratePartialCut_Click" />
                    <asp:Button ID="btnGenerateAct" Text="Generar Acta" runat="server" OnClientClick="return confirm(' ¿ Desea generar el Acta Circunstaciada ?');"
                        CssClass="CenterButton" OnClick="btnGenerateAct_Click" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
