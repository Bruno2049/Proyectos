<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="MaterialRecoveryRegistryMonitor.aspx.cs"
    Inherits="PAEEEM.DisposalModule.MaterialRecoveryRegistryMonitor" Title="Edición/Borrado Lotes de Recuperación" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<link href="../Resources/Css/Pager.css" rel="stylesheet" type="text/css" />
    <link href="../Resources/Css/GridView.css" rel="stylesheet" type="text/css" />

    <script src="../Resources/Script/Calendar/WdatePicker.js" type="text/javascript"></script>

    <link href="../Resources/Script/Calendar/skin/default/datepicker.css" type="text/css" />
    <style type="text/css">
        .Label
        {
            width: 120px;
            color: #333333;
            font-size: 16px;
        }
        .DropDownList
        {
            width: 280px;
        }
        .Button
        {
            width: 120px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<asp:UpdatePanel ID="panel" runat="server">
        <ContentTemplate>
            <div>
               
                <div>
                    <br/>
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/edicion_borrador.png" />
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
                                <asp:Label ID="Label2" Text="Programa" runat="server" CssClass="Label" /></div>
                        </td>
                        <td>
                            <div>
                                <asp:DropDownList ID="drpProgram" runat="server" AutoPostBack="true" CssClass="DropDownList"
                                    OnSelectedIndexChanged="drpProgram_SelectedIndexChanged" />
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:Label ID="Label3" Text="Fecha Registro" runat="server" CssClass="Label" /></div>
                        </td>
                        <td>
                            <div>
                                <asp:TextBox ID="txtRegistryDate" runat="server" CssClass="Wdate" Width="200px" onFocus="WdatePicker({maxDate:'#F{$dp.$D(\'ctl00_MainContent_txtRegistryToDate\')}'})"></asp:TextBox>
                            </div>
                        </td>
                        <td>
                        <asp:Label ID="Label5" Text="Hasta" runat="server" CssClass="Label" />
                        </td>
                        <td>
                        <asp:TextBox ID="txtRegistryToDate" runat="server" CssClass="Wdate" Width="150px" onFocus="WdatePicker({minDate:'#F{$dp.$D(\'ctl00_MainContent_txtRegistryDate\')}'})"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div>
                                <asp:Label ID="Label8" Text="Tecnología" runat="server" CssClass="Label" /></div>
                        </td>
                        <td>
                            <div>
                                <asp:DropDownList ID="drpTechnology" runat="server" AutoPostBack="true" CssClass="DropDownList"
                                    OnSelectedIndexChanged="drpTechnology_SelectedIndexChanged" />
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:Label ID="Label4" Text="Residuo/Material" runat="server" CssClass="Label" /></div>
                        </td>
                        <td>
                            <div>
                                <asp:DropDownList ID="drpMaterialType" runat="server" AutoPostBack="true" 
                                    CssClass="DropDownList" 
                                    onselectedindexchanged="drpMaterialType_SelectedIndexChanged" />
                            </div>
                        </td>
                    </tr>
                </table>
                <br />
                <table width="100%">
                    <tr>
                        <td width="45%" style="text-align: right">
                            <asp:Button ID="btnSearch" runat="server" Text="Buscar" CssClass="Button" OnClick="btnSearch_Click" />
                        </td>
                        <td width="10%">
                        </td>
                        <td width="45%" style="text-align: left">
                            <asp:Button ID="btnCancel" runat="server" Text="Salir" OnClientClick="return confirm('¿ Desea Salir de ésta Pantalla ?');"
                                CssClass="Button" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                </table>
                <br />
                <div>
                    <asp:GridView ID="grdRecoveryMaterial" runat="server" AutoGenerateColumns="False"
                        CssClass="GridViewStyle" AllowPaging="True" PageSize="20" OnDataBound="grdRecoveryMaterial_DataBound"
                        DataKeyNames="Id_Recuperacion, Fg_Inspeccionado,Id_Orden">
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <Columns>
                            <asp:BoundField DataField="Dx_Nombre_Programa" HeaderText="Programa"></asp:BoundField>
                            <asp:BoundField DataField="Dx_Nombre_General" HeaderText="Tecnología" />
                            <asp:BoundField DataField="Dt_Fecha_Recuperacion" HeaderText="Fecha Registro" DataFormatString="{0:dd-MM-yyyy}" />
                            <asp:BoundField DataField="MaterialType" HeaderText="Residuo/Material"></asp:BoundField>
                            <asp:BoundField DataField="GasType" HeaderText="Tipo Residuo"></asp:BoundField>
                            <asp:BoundField DataField="No_Material" HeaderText="Recuperación"></asp:BoundField>
                            <asp:HyperLinkField DataNavigateUrlFormatString="~/DisposalModule/OldProductList.aspx?RecuperacionID={0}"  HeaderText="No. Equipos"
                                DataTextField="ProductCount" DataNavigateUrlFields="Id_Recuperacion" />
                            <asp:BoundField DataField="Id_Acta_Recuperacion" HeaderText="Acta Circunstanciada">
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEdit" runat="server" OnClientClick="return confirm('Confirmar Edición del Lote Seleccionado');"
                                        OnClick="btnEdit_Click">Editar</asp:LinkButton>
                                    <asp:LinkButton ID="btnDelete" runat="server" OnClientClick="return confirm('Confirmar, ¡ será Borrado definitivamente el Lote Seleccionado !');"
                                        OnClick="btnDelete_Click">Borrar</asp:LinkButton>
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
