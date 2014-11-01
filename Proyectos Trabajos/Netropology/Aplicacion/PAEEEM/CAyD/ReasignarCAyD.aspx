<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReasignarCAyD.aspx.cs" Inherits="PAEEEM.CAyD.ReasignarCAyD" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
        <script type="text/javascript">
            function confirmCallBackFn(arg) {
                if (arg == true) {
                    var oButton = document.getElementById("ctl00_MainContent_" + "HiddenButton");
                    oButton.click();
                }
            }
            </script>
    </telerik:RadScriptBlock>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="LabelEncabezado" runat="server" Text="Reasignar CAyD"
        Font-Size="Small" ForeColor="#00A3D9" Font-Bold="True"></asp:Label>
    <hr class="ruleNet" />

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Office2010Blue"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" LoadingPanelID="RadAjaxLoadingPanel1" runat="server" Width="100%">
        <fieldset class="fieldset_Netro">
            <legend style="font-size: x-small">BUSQUEDA</legend>
            <table align="center" style="width: 90%">

                <tr style="height: 60px">
                    <td>
                        <asp:Label ID="lbl1" runat="server" Text="Distribuidor RS:" Font-Size="X-SMALL"></asp:Label>
                        <telerik:RadComboBox ID="cmbRS" runat="server" Width="200"></telerik:RadComboBox>
                    </td>
                    <td>
                        <asp:Label ID="lbl2" runat="server" Text="CAyD" Font-Size="X-SMALL"></asp:Label>
                        <telerik:RadComboBox ID="cmbCAyD" runat="server" Width="200" ></telerik:RadComboBox>
                    </td>
                    <td>
                        <asp:Label ID="lbl3" runat="server" Text="No. Solicitud:" Font-Size="X-SMALL"></asp:Label>
                        <telerik:RadTextBox runat="server" ID="txtSolicitud" MaxLength="16"></telerik:RadTextBox>
                        &nbsp;
                    </td>
                    <td>
                        <asp:Label ID="lbl4" runat="server" Text="Folio Pre-boleta" Font-Size="X-SMALL"></asp:Label>
                        <telerik:RadTextBox ID="txtFolio" runat="server" MaxLength="21"></telerik:RadTextBox>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <br />
                        <telerik:RadButton ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click"></telerik:RadButton>
                    </td>
                </tr>
            </table>
        </fieldset>
        <br />
        <br />

        <telerik:RadGrid ID="rgSolicitudes" runat="server" AutoGenerateColumns="False" Visible="True" AllowSorting="true"
            AllowMultiRowSelection="true"
            OnNeedDataSource="rgSolicitudes_NeedDataSource"
            OnDataBound="rgSolicitudes_DataBound"
            GridLines="None" Skin="Office2010Silver" CellSpacing="0" PageSize="10"  AllowPaging="True">
            <PagerStyle Mode="NextPrevAndNumeric" Position="Bottom" ></PagerStyle>
            <ClientSettings EnableRowHoverStyle="True">
                <Selecting CellSelectionMode="None" />
            </ClientSettings>
            <MasterTableView ClientDataKeyNames="NoSolicitud" NoMasterRecordsText="No se encontrarón solicitudes" Font-Size="X-SMALL"
                AutoGenerateColumns="False">

                <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>

                <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column"></RowIndicatorColumn>

                <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column"></ExpandCollapseColumn>

                <Columns>
                    <telerik:GridBoundColumn DataField="NoSolicitud" HeaderText="No. DE SOLICITUD" UniqueName="NoSolicitud"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="DistrNC" HeaderText="DISTRIBUIDOR NC"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="DistrRS" HeaderText="DISTRIBUIDOR RS"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Folio" HeaderText="FOLIO PRE-BOLETA"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="CAyD" HeaderText="CAyD ORIGEN"></telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn HeaderText="SELECCIONE" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:CheckBox ID="ckbSelect" runat="server" AutoPostBack="true" OnCheckedChanged="ckbSelect_CheckedChanged"/>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>

                <EditFormSettings>
                    <EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
                </EditFormSettings>
            </MasterTableView>

        </telerik:RadGrid>
        <br />
        <br />
        <table style="width: 100%">
            <tr>
                <td align="center">
                    <asp:Label ID="Label1" runat="server" Text="CAyD" Font-Size="X-SMALL"></asp:Label>&nbsp;&nbsp;
                        <telerik:RadComboBox ID="cmbAsignarCAyD" Enabled="false" runat="server"></telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <br />
                    <telerik:RadButton ID="txtAsignar" runat="server" Text="Asignar" Enabled="false" OnClick="txtAsignar_Click"></telerik:RadButton>
                    &nbsp;&nbsp;
                    <telerik:RadButton ID="txtSalir" runat="server" Text="Salir" OnClick="txtSalir_Click"></telerik:RadButton>
                </td>
            </tr>
        </table>
    </telerik:RadAjaxPanel>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true"></telerik:RadWindowManager>
    <div style="display: none">
        <asp:Button ID="HiddenButton" BackColor="#FFFFFF" OnClick="HiddenButton_Click"  runat="server" Width="0px" />
    </div>
</asp:Content>
