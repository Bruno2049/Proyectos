<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MonitorVendedores.aspx.cs" Inherits="PAEEEM.Vendedores.MonitorVendedores" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function poponload(curp) {
            var testwindow = window.open('VisorImagenes.aspx?tipo=2&Id='+curp +'', '', 'top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');
            testwindow.moveTo(0, 0);
        }


        function OnClientClicking(sender, args) {
            var event = args.get_domEvent();
            if (event.keyCode == 13) {
                args.set_cancel(true);
            }
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="LabelEncabezado" runat="server" Text="Monitor de vendedores"
        Font-Size="Small" ForeColor="#00A3D9" Font-Bold="True"></asp:Label>
    <hr class="ruleNet" />

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Office2010Blue"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" LoadingPanelID="RadAjaxLoadingPanel1" runat="server" Width="100%">
        <fieldset class="fieldset_Netro">
            <table align="center">
                <tr>
                    <td colspan="3" align="center">
                        <asp:Label ID="Label1" runat="server" Text="Busqueda"
                            Font-Size="Small" ForeColor="#00A3D9" Font-Bold="True"></asp:Label>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCURP" runat="server" Text="CURP:" Font-Size="X-SMALL"></asp:Label>
                        <telerik:RadTextBox ID="txtCURP" runat="server"></telerik:RadTextBox>&nbsp;
                    </td>
                    <td>
                        <asp:Label ID="lblNombre" runat="server" Text="Nombre del Vendedor" Font-Size="X-SMALL"></asp:Label>
                        <telerik:RadTextBox ID="txtNombre" runat="server"></telerik:RadTextBox>&nbsp;
                    </td>
                    <td>
                        <asp:Label ID="lblEstatus" runat="server" Text="Estatus" Font-Size="X-SMALL"></asp:Label>
                        <telerik:RadComboBox ID="cmbEstatus" runat="server"></telerik:RadComboBox>
                        &nbsp;
                </tr>
                <tr>
                    <td colspan="3" align="center">
                        <br />
                        <telerik:RadButton ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_OnClick"></telerik:RadButton>
                        &nbsp;&nbsp;
                        <telerik:RadButton ID="txtLimpiar" runat="server" Text="Limpiar" OnClick="txtLimpiar_Click"></telerik:RadButton>
                    </td>
                </tr>
            </table>
        </fieldset>
        <br />
        <br />

        <telerik:RadGrid ID="rgVendedores" runat="server" AutoGenerateColumns="False" Visible="True"
            GridLines="None" Skin="Office2010Silver" CellSpacing="0" 
            AllowSorting="true"
        OnPageSizeChanged="rgVendedores_OnPageSizeChanged"
        OnPageIndexChanged="rgVendedores_OnPageIndexChanged"
        OnSortCommand="rgVendedores_OnSortCommand" 
            PageSize="5" AllowPaging="True">
            <PagerStyle Mode="NextPrevAndNumeric" Position="Bottom" ></PagerStyle>
            <ClientSettings EnableRowHoverStyle="True">
                <Selecting CellSelectionMode="None" />
            </ClientSettings>
            <MasterTableView NoMasterRecordsText="No se encontrarón Vendedores"
                AutoGenerateColumns="False">

                <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>

                <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column"></RowIndicatorColumn>

                <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column"></ExpandCollapseColumn>

                <Columns>
                    <telerik:GridBoundColumn DataField="IdVendedor" HeaderText="Id VENDEDOR"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Nombre" HeaderText="NOMBRE"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Curp" HeaderText="CURP"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataFormatString="{0:dd/MM/yyyy}" DataField="FechaNacimiento" HeaderText="FECHA DE NACIMIENTO"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="TipoIdentificacion" HeaderText="TIPO IDENTIFICACION"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="NoIdentificacion" HeaderText="No. IDENTIFICACION"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Estatus" HeaderText="ESTATUS"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="AccesoSistema" HeaderText="ACCESO AL SISTEMA"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Incidencia" HeaderText="INCIDENCIA"></telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn HeaderText="ARCHIVO">
                        <ItemTemplate>
                            <asp:ImageButton runat="server" ID="verImagen" CommandName="Ver" ToolTip="Ver" Height="20px" Width="20px" 
                                ImageUrl="~/CirculoCredito/imagenes/Buscar.png" Visible="True" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>

                <EditFormSettings>
                    <EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
                </EditFormSettings>
            </MasterTableView>
        </telerik:RadGrid>
    </telerik:RadAjaxPanel>
</asp:Content>
