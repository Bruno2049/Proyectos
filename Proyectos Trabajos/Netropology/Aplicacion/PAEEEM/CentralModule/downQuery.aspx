<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="downQuery.aspx.cs" Inherits="PAEEEM.CentralModule.downQuery" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .Etiqueta {
            padding: 5px 15px;
        }

        .EtiquetaSubEstacion {
            padding: 5px 15px;
            display: none;
        }

        .dvMainPanel {
            clear: both;
            width: 100%;
            display: inline;
            margin-left: auto;
            margin-right: auto;
        }

        .rowVisible {
            display: none;
        }

        #DivGrid {
            clear: both;
        }
    </style>
    <link href="../Resources/Css/GridView.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" id="telerikClientEvents2">
        //<![CDATA[

        //function CargaArchivo_FilesUploaded(sender, args) {
        //    //Add JavaScript handler code here
        //    var btn = document.getElementById("btnAttach");
        //    btn.click();
        //}

        function RadPanel_OnRequestStart(sender, args) {
            args.EnableAjax = false;
        }

        //]]>
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server"></telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default"></telerik:RadAjaxLoadingPanel>

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" Skin="Default" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="RadPanel_OnRequestStart">
        <table id="tblBase" class="dvMainPanel">
            <tr>
                <td colspan="4">
                    <h2>
                        <asp:Label ID="lblTitulo" runat="server" Font-Size="Large" Font-Bold="True" Text="Descarga consultas"></asp:Label>
                    </h2>
                    <hr class="rule" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <fieldset class="legend_info">
                        <table class="dvMainPanel">
                            <tr>
                                <td>
                                    <asp:Label ID="lblConsulta" runat="server" CssClass="Etiqueta" Text="Nombre Consulta: "></asp:Label></td>
                                <td>
                                    <asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator3"
                                        runat="server"
                                        ErrorMessage="El nombre de la consulta es requerida..."
                                        ValidationGroup="Save"
                                        Display="Dynamic"
                                        Text="*"
                                        EnableClientScript="true"
                                        ControlToValidate="txtConsulta"
                                        InitialValue="">
                                    </asp:RequiredFieldValidator>
                                    <telerik:RadTextBox ID="txtConsulta" ValidationGroup="Save" runat="server" Font-Size="11px" Width="200px" EmptyMessage="Nombre de consulta"></telerik:RadTextBox>
                                    <asp:TextBox ID="txtCVE" runat="server" Visible="False" Enabled="False"></asp:TextBox>
                                </td>
                                <td style="text-align: justify;">
                                    <asp:Label ID="lblFechaCreacion" runat="server" Text="Fecha creación : " CssClass="Etiqueta"></asp:Label></td>
                                <td>
                                    <telerik:RadDatePicker ID="rdpFecCrea" runat="server" Culture="es-ES" Enabled="False">
                                        <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" EnableKeyboardNavigation="true">
                                        </Calendar>
                                        <DateInput runat="server" AutoPostBack="false" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelWidth="40%" SelectedDate="2014-09-12" Enabled="false">
                                        </DateInput>

                                    </telerik:RadDatePicker>

                                </td>
                                <td></td>
                            </tr>


                            <tr>
                                <td>
                                    <asp:Label ID="lblDescripcion" ValidationGroup="Save" runat="server" CssClass="Etiqueta" Text="Descripción: "></asp:Label>
                                </td>
                                <td colspan="3" style="text-align: left">
                                    <asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator2"
                                        runat="server"
                                        ErrorMessage="La descripción de la consulta es requerida..."
                                        ValidationGroup="Save"
                                        Display="Dynamic"
                                        Text="*"
                                        EnableClientScript="true"
                                        ControlToValidate="txtDescripcion"
                                        InitialValue="">

                                    </asp:RequiredFieldValidator>
                                    <telerik:RadTextBox ID="txtDescripcion" runat="server" EmptyMessage="Captura la descripción de la consulta" Enabled="False" Height="25px" Rows="3" Style="top: 0px; left: 0px" Width="90%">
                                    </telerik:RadTextBox>
                                </td>
                                <td style="text-align: justify;"></td>
                            </tr>
                            <tr>
                                <td colspan="5" style="text-align: left">
                                    <asp:Label ID="lblQ" ValidationGroup="Save" runat="server" CssClass="Etiqueta" Text="Consulta : "></asp:Label></td>
                            </tr>
                            <tr>
                                <td colspan="5" style="text-align: left">
                                    <asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator1"
                                        runat="server"
                                        ErrorMessage="La consulta es requerida..."
                                        ValidationGroup="Save"
                                        Display="Dynamic"
                                        Text="*"
                                        EnableClientScript="true"
                                        ControlToValidate="txtQuery"
                                        InitialValue="">
                                    </asp:RequiredFieldValidator>
                                    <telerik:RadTextBox ID="txtQuery" ValidationGroup="Save" runat="server" Enabled="False" Height="150px" Resize="Both" Rows="20" TextMode="MultiLine" Width="98%" />
                                </td>

                            </tr>                           
                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:GridView ID="grdQuery" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle" OnRowCommand="grdQuery_RowCommand" OnRowEditing="grdQuery_RowEditing">
                        <Columns>
                            <asp:TemplateField HeaderText="CVE" SortExpression="CVE_Consulta">
                                <ItemTemplate>
                                    <asp:Label ID="lblgCVE" runat="server" Text='<%# Bind("CVE_Consulta") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Consulta" SortExpression="Nombre_Consulta">
                                <ItemTemplate>
                                    <asp:Label ID="lblNameQ" runat="server" Text='<%# Bind("Nombre_Consulta") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="20%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Descripción" SortExpression="Descripcion_Consulta">
                                <ItemTemplate>
                                    <asp:Label ID="lblDescripcionQ" runat="server" Text='<%# Bind("Descripcion_Consulta") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="70%" />
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <div style="width: 100px; border-spacing: 10px">
                                        <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Edit" ImageUrl="~/images/lupa.png" Text="Edit" />
                                        <asp:ImageButton ID="btnDownLoad" runat="server" CausesValidation="false" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="DownLoad" Height="25px" ImageUrl="~/CentralModule/images/Descargar.png" />
                                    </div>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="rowVisible" ItemStyle-CssClass="rowVisible">
                                <ItemTemplate>
                                    <asp:Label ID="lblQuery" runat="server" Text='<%# Bind("Consulta") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="rowVisible" />
                                <ItemStyle CssClass="rowVisible" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblCreacion" runat="server" Text='<%# Bind("Fecha_Adicion") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="rowVisible" />
                                <ItemStyle CssClass="rowVisible" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblAdicionado" runat="server" Text='<%# Bind("Adicionado_Por") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="rowVisible" />
                                <ItemStyle CssClass="rowVisible" />
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                    </asp:GridView>
                </td>
            </tr>

        </table>
    </telerik:RadAjaxPanel>
</asp:Content>
