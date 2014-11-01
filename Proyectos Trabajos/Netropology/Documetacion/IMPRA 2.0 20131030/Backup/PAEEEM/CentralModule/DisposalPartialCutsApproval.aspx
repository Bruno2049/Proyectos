<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DisposalPartialCutsApproval.aspx.cs"
    Inherits="PAEEEM.CentralModule.DisposalPartialCutsApproval" Title="Monitor de Actas Circunstanciadas" %>

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
                <div align="left">
                    <asp:Label ID="Label2" Text="Num. Acta" runat="server" Font-Size="11pt" Width="120px" />
                    <asp:TextBox ID="txtPartialCode" runat="server" Enabled="False" Width="200px"></asp:TextBox>
                </div>
                <br />
                <div>
                    <asp:GridView ID="grdProductsForApproval" runat="server" AutoGenerateColumns="False"
                        CssClass="GridViewStyle" AllowPaging="True" PageSize="20" 
                        DataKeyNames="Codigo_Producto" 
                        onrowdatabound="grdProductsForApproval_RowDataBound">
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <Columns>
                            <asp:BoundField DataField="Codigo_Producto" HeaderText="Código Producto"></asp:BoundField>
                            <asp:BoundField DataField="Barcode_Solicitud" HeaderText="Crédito" />
                            <asp:BoundField DataField="Dx_Nombre_Programa" HeaderText="Programa" />
                            <asp:BoundField DataField="Dt_Fecha_Recepcion" HeaderText="Fecha Recepción" DataFormatString="{0:yyyy-MM-dd}" />
                            <asp:BoundField DataField="Dt_Fecha_Inhabilitacion" HeaderText="Fecha Inhabilitación"
                                DataFormatString="{0:yyyy-MM-dd}" />
                            <asp:BoundField DataField="Dt_Fecha_Recuperacion" HeaderText="Fecha Rec. Residuos"
                                DataFormatString="{0:yyyy-MM-dd}" />
                            <asp:TemplateField HeaderText="Autorizar">
                                <ItemTemplate>
                                    <asp:CheckBox ID="ckbSelect" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerTemplate>
                        </PagerTemplate>
                    </asp:GridView>
                    <div align="right">
                        <asp:CheckBox ID="ckbSelectAll" runat="server" Text="Seleccionar todos" AutoPostBack="True"
                            OnCheckedChanged="ckbSelectAll_CheckedChanged" />
                    </div>
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
                    <asp:Button ID="btnApproval" Text="Guardar" runat="server" OnClientClick="return confirm('Confirmar Guardar');"
                        CssClass="CenterButton" OnClick="btnApproval_Click" />
                    <asp:Button ID="btnCancel" Text="Salir" runat="server" OnClientClick="return confirm('¿ Desea Salir de ésta Pantalla ?');"
                        CssClass="CenterButton" OnClick="btnCancel_Click" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
