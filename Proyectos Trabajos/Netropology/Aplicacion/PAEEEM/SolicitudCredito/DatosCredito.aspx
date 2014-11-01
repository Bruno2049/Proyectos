<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DatosCredito.aspx.cs" Inherits="PAEEEM.SolicitudCredito.DatosCredito" EnableEventValidation="false" %>
<%--<%@ PreviousPageType VirtualPath="~/SolicitudCredito/DetalleCredito.aspx" %>--%>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager, Version=7.3.2.0, Culture=neutral, PublicKeyToken=fb0a0fe055d40fd4" %>
<%--<%@ Register TagPrefix="qsf" Namespace="Telerik.QuickStart" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        function Validarfiltros() {
            var nc = document.getElementById('<%= txtSolicitud.ClientID %>');
            var rpu = document.getElementById('<%= txtRPU.ClientID %>');
            var rfc = document.getElementById('<%= txtRFC.ClientID %>');
            var esta = document.getElementById('<%= drpEstatus.ClientID %>');
            var reg = document.getElementById('<%= drpRegion.ClientID %>');
            var zon = document.getElementById('<%= drpZona.ClientID %>');
            var drs = document.getElementById('<%= txtDistrRS.ClientID %>');
            var dnc = document.getElementById('<%= txtDistrNC.ClientID %>');
            var fini = document.getElementById('<%= rdpFechaInicio.ClientID %>');
            var ffin = document.getElementById('<%= rdpFechaFin.ClientID %>');

            if (nc.value.length > 0 || rpu.value.length > 0 || rfc.value.length > 0 || drs.value.length > 0 || dnc.value.length > 0) {

                document.getElementById('<%=btnBuscar.ClientID%>').disabled = false;
                document.getElementById('<%=btnLimpiar.ClientID%>').disabled = false;
            }
            else {
                document.getElementById('<%=btnBuscar.ClientID%>').disabled = true;
                document.getElementById('<%=btnLimpiar.ClientID%>').disabled = true;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:Table runat="server" ID="tblFitros" BackColor="#99CCFF" Width="100%">
            <asp:TableRow>
                <asp:TableCell ColumnSpan="5">
                    <asp:Label ID="Label1" runat="server" Text="Busqueda de Solicitudes"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="Label2" runat="server" Text="Número de solicitud" Font-Size="X-Small" Font-Bold="true"></asp:Label><br />
                    <telerik:RadTextBox ID="txtSolicitud" runat="server"  Font-Size="X-Small" MaxLength="16" ></telerik:RadTextBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="Label3" runat="server" Text="RPU" Font-Size="X-Small" Font-Bold="true"></asp:Label><br />
                    <telerik:RadTextBox ID="txtRPU" runat="server" Font-Size="Smaller" MaxLength="12" ></telerik:RadTextBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="Label4" runat="server" Text="RFC" Font-Size="X-Small" Font-Bold="true"></asp:Label><br />
                    <telerik:RadTextBox ID="txtRFC" runat="server" Font-Size="Smaller" MaxLength="13" ></telerik:RadTextBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="Label5" runat="server" Text="Estatus" Font-Size="X-Small" Font-Bold="true"></asp:Label><br />
                    <asp:DropDownList ID="drpEstatus" runat="server" Font-Size="X-Small" AutoPostBack="true" OnTextChanged="drpEstatus_TextChanged"></asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="Label6" runat="server" Text="Region" Font-Size="X-Small" Font-Bold="true"></asp:Label><br />
                    <asp:DropDownList ID="drpRegion" runat="server" Font-Size="X-Small" AutoPostBack="true" OnSelectedIndexChanged="drpRegion_SelectedIndexChanged">
                    </asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="Label7" runat="server" Text="Distribuidor RS" Font-Size="X-Small" Font-Bold="true"></asp:Label><br />
                    <telerik:RadTextBox ID="txtDistrRS" runat="server" Font-Size="Smaller" MaxLength="60" ></telerik:RadTextBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="Label8" runat="server" Text="Distribuidor NC" Font-Size="X-Small" Font-Bold="true"></asp:Label><br />
                    <telerik:RadTextBox ID="txtDistrNC" runat="server" Font-Size="Smaller" MaxLength="60" ></telerik:RadTextBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="Label9" runat="server" Text="Fecha Inicio" Font-Size="X-Small" Font-Bold="true"></asp:Label><br />
                    <telerik:RadDatePicker ID="rdpFechaInicio" runat="server" OnSelectedDateChanged="rdpFechaInicio_SelectedDateChanged" Font-Size="Smaller" Width="130" EnableAriaSupport="true" AutoPostBack="true"></telerik:RadDatePicker>

                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="Label10" runat="server" Text="Fecha Fin" Font-Size="X-Small" Font-Bold="true"></asp:Label><br />
                    <telerik:RadDatePicker ID="rdpFechaFin" runat="server" Font-Size="Smaller" OnSelectedDateChanged="rdpFechaFin_SelectedDateChanged" AutoPostBack="true"></telerik:RadDatePicker>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="Label11" runat="server" Text="Zona" Font-Size="X-Small" Font-Bold="true"></asp:Label><br />
                    <asp:DropDownList ID="drpZona" runat="server" Font-Size="X-Small" AutoPostBack="true" OnTextChanged="drpZona_TextChanged">
                    </asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="5" HorizontalAlign="Center">
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" Enabled="false"/>&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click"  Enabled="false"/>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>

    <br />
    <div>
    </div>
   

       <%-- <asp:UpdatePanel ID="UpdatePanelRB" runat="server" Visible="true">--%>
            <%--<ContentTemplate>--%>
                <table id="Table6" width="100%" runat="server">
                    <tr>
                        <td class="tbright" width="90%"></td>
                        <td class="auto-style11">
                            <asp:ImageButton ID="imgExportaExcel" ImageUrl="~/CentralModule/images/ImgExcel.gif" runat="server" OnClick="imgExportaExcel_Click" Height="35px" ToolTip="Exportar a Excel" />
                        </td>
                        <td class="auto-style11">
                            <asp:ImageButton ID="imgExportaPDF" ImageUrl="~/CentralModule/images/Pdf.png" runat="server" OnClick="imgExportaPDF_Click" Height="35px" ToolTip="Exportar a PDF" />
                        </td>
                    </tr>
                </table>
 <div id="div_resultadosBusqueda" style="width: 100%" align="center" runat="server">
                <asp:GridView ID="gvresultadosBusqueda" runat="server" aling="center" Width="80%" AutoGenerateColumns="False" Font-Names="Arial" Font-Size="11pt" AllowPaging="True" BorderStyle="Groove"
                    CssClass="GridViewStyle" PageSize="5" OnRowCommand="gvresultadosBusqueda_RowCommand" >
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" BorderStyle="Solid" />
                    <HeaderStyle BackColor="Silver" />
                    <%--<PagerSettings Visible="false" />--%>
                    <PagerStyle CssClass="GridViewPagerStyle" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                    <EditRowStyle CssClass="GridViewEditStyle" />
                    <Columns>
                       
                        <asp:TemplateField ItemStyle-Width="9%" HeaderText="SOLICITUD" ControlStyle-Font-Size="X-Small" Visible="true">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="btnCredito" PostBackUrl="~/SolicitudCredito/DetalleCredito.aspx" CommandName="Buscar" CommandArgument='<%# Eval("No_Credito")%>' Text='<%# Eval("No_Credito")%>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="9%" HeaderText="RPU" ControlStyle-Font-Size="X-Small" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lblRPU" runat="server" Text='<%# Eval("RPU")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="9%" HeaderText="NOMBRE/RAZÓN SOCIAL" ControlStyle-Font-Size="X-Small" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lblNOMRS" runat="server" Text='<%# Eval("NombreRazonSocial")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="9%" HeaderText="RFC" ControlStyle-Font-Size="X-Small">
                            <ItemTemplate>
                                <asp:Label ID="lblRFC" runat="server" Text='<%# Eval("RFC")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="5%" HeaderText="MONTO DE FINANCIAMIENTO" ControlStyle-Font-Size="X-Small">
                            <ItemTemplate>
                                <asp:Label ID="lblMTP" runat="server" Text='<%# "$ " + Math.Round(decimal.Parse(Eval("Monto_Solicitado").ToString()),2) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="9%" HeaderText="ESTATUS" Visible="true" ControlStyle-Font-Size="X-Small">
                            <ItemTemplate>
                                <asp:Label ID="lblEC" runat="server" Text='<%# Eval("Dx_Estatus_Credito")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="9%" HeaderText="FECHA ESTATUS" Visible="true" ControlStyle-Font-Size="X-Small">
                            <ItemTemplate>
                                <asp:Label ID="lblFUM" runat="server" Text='<%# Convert.ToDateTime(Eval("Fecha_Ultmod")).ToShortDateString()%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="9%" HeaderText="DISTRIBUIDOR RAZÓN SOCIAL" Visible="true" ControlStyle-Font-Size="X-Small">
                            <ItemTemplate>
                                <asp:Label ID="lblRS" runat="server" Text='<%# Eval("Dx_Razon_Social")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="9%" HeaderText="DISTRIBUIDOR NOMBRE COMERCIAL" Visible="true" ControlStyle-Font-Size="X-Small">
                            <ItemTemplate>
                                <asp:Label ID="lblNC" runat="server" Text='<%# Eval("Dx_Nombre_Comercial")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="11%" HeaderText="REGIÓN" Visible="true" ControlStyle-Font-Size="X-Small">
                            <ItemTemplate>
                                <asp:Label ID="lblR" runat="server" Text='<%# Eval("Dx_Nombre_Region")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="11%" HeaderText="ZONA" Visible="true" ControlStyle-Font-Size="X-Small">
                            <ItemTemplate>
                                <asp:Label ID="lblZ" runat="server" Text='<%# Eval("Dx_Nombre_Zona")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerTemplate>
            </PagerTemplate>
                </asp:GridView>
            <%--</ContentTemplate>--%>

            <%--<Triggers>
                <asp:PostBackTrigger ControlID="imgExportaExcel" />
                <asp:PostBackTrigger ControlID="imgExportaPDF"></asp:PostBackTrigger>
            </Triggers>--%>

        <%--</asp:UpdatePanel>--%>

        <webdiyer:AspNetPager ID="AspNetPager2" CssClass="pagerDRUPAL" runat="server" PageSize="5" OnPageChanged="AspNetPager2_PageChanged"
            AlwaysShow="True" ShowCustomInfoSection="Right" ShowDisabledButtons="true" ShowPageIndexBox="always" Visible="false"
            PageIndexBoxType="DropDownList" CustomInfoHTML="Página:<font color='red'><b>%currentPageIndex%</b></font>/%PageCount%&nbsp;&nbsp;PageSize:%PageSize%&nbsp;&nbsp;Record:%StartRecordIndex%-%EndRecordIndex% of %RecordCount%"
            UrlPaging="false" FirstPageText="Primero" LastPageText="Último" NextPageText="Siguiente" PrevPageText="Anterior"
            CurrentPageButtonClass="cpb">
        </webdiyer:AspNetPager>

    </div>

    <br />

    <%--PRUEBA--%>

    
</asp:Content>
