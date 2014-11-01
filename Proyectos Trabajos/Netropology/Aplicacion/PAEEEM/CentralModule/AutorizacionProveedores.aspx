<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AutorizacionProveedores.aspx.cs" Inherits="PAEEEM.CentralModule.AutorizacionProveedores" Title="Autorizacion de Proveedores" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        div.RadListBox .rlbText
        {
            white-space: nowrap;
            display: inline-block;
        }

        div.RadListBox .rlbGroup
        {
            overflow: auto;
        }

        div.RadListBox .rlbList
        {
            display: inline-block;
            min-width: 100%;
        }

        * + html div.RadListBox .rlbList
        {
            display: inline;
        }

        * html div.RadListBox .rlbList
        {
            display: inline;
        }
    </style>
    <script type="text/javascript">
        var pre = "ctl00_MainContent_";

        function confirmCallBackFn(arg) {
            if (arg == true) {
                var oButton = document.getElementById("ctl00_MainContent_" + "HiddenButton");
                oButton.click();
            }
        }
        function ValidarCbxMotivo() {
            var indice = document.getElementById('<%=Check_Motivos_asp.ClientID%>');
            var chk = indice.getElementsByTagName("input");
            var label = indice.getElementsByTagName("label");
            var txt = document.getElementById('<%=txt_OTROS.ClientID%>');

            if (chk[22].checked) {
                document.getElementById('<%=txt_OTROS.ClientID%>').style.display = 'block';
                document.getElementById('<%=lbl_otrosMotivos.ClientID%>').style.display = 'block';

            } else {
                document.getElementById('<%=txt_OTROS.ClientID%>').style.display = 'none';
                document.getElementById('<%=lbl_otrosMotivos.ClientID%>').style.display = 'none';

            }

        }

        function ValidarObservaciones(textareaControl, maxlength) {
            var obser = document.getElementById('<%=txtObservaciones.ClientID%>');
            if (obser.value.length > maxlength) {
                textareaControl.value = textareaControl.value.substring(0, maxlength);
            }

            if (obser.value.length > 0) {
                document.getElementById('<%=btn_AceptarPOP.ClientID%>').disabled = false;
            }
            else {
                document.getElementById('<%=btn_AceptarPOP.ClientID%>').disabled = true;
            }
        }

        function ValidarObservacionesReactivar(textareaControl, maxlength) {
            var obser = document.getElementById('<%=txt_motivosReactivar.ClientID%>');
            if (obser.value.length > maxlength) {
                textareaControl.value = textareaControl.value.substring(0, maxlength);
            }

            if (obser.value.length > 0) {
                document.getElementById('<%=btnFinalizarReactivar.ClientID%>').disabled = false;
            }
            else {
                document.getElementById('<%=btnFinalizarReactivar.ClientID%>').disabled = true;
            }
        }



    </script>

</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
     <telerik:RadAjaxPanel ID="RadAjaxPanelAuto" runat="server" Width="100%" LoadingPanelID="RadAjaxLoadingPanel2">
    <%-- <div>--%>
    <fieldset class="fieldset_Netro">

        <table align="center">
            <tr style="width: 350px;">
                <td align="center">
                    <asp:Label runat="server" Text="Regional:" Font-Size="Small"></asp:Label>
                    <telerik:RadComboBox Font-Size="Smaller" ID="rad_cmbRegional" runat="server" Width="150px" AutoPostBack="true" OnSelectedIndexChanged="rad_cmbRegional_SelectedIndexChanged"></telerik:RadComboBox>
                </td>
                <td style="align-items: stretch">
                    <asp:Label runat="server" Text="Zona:" Font-Size="Small"></asp:Label>
                    <telerik:RadComboBox Font-Size="11px" runat="server" ID="rad_cmbZona" AutoPostBack="true" OnSelectedIndexChanged="rad_cmbZona_SelectedIndexChanged"></telerik:RadComboBox>
                </td>
                <td align="center">
                    <asp:Label runat="server" Text="Tipo:" Font-Size="Small"></asp:Label>
                    <telerik:RadComboBox Font-Size="11px" ID="rad_cmbTipo" runat="server" Width="150px" AutoPostBack="true" OnSelectedIndexChanged="rad_cmbTipo_SelectedIndexChanged"></telerik:RadComboBox>

                </td>
                <td align="center">
                    <asp:Label runat="server" Text="Estatus:" Font-Size="Small"></asp:Label>
                    <telerik:RadComboBox Font-Size="11px" ID="rad_cmbEstatus" runat="server" Width="150px" AutoPostBack="true" OnSelectedIndexChanged="rad_cmbEstatus_SelectedIndexChanged"></telerik:RadComboBox>
                </td>
            </tr>
        </table>
    </fieldset>
    <%-- </div>
    <div>--%>
    <fieldset class="fieldset_Netro">

        <telerik:RadGrid ID="rg_Distribuidor" runat="server" AllowSorting="true" AutoGenerateColumns="False" Skin="Office2010Silver" CellSpacing="0" AllowMultiRowSelection="true" OnNeedDataSource="rg_Distribuidor_NeedDataSource"
            OnItemDataBound="rg_Distribuidor_DataBound" AllowPaging="True" PageSize="20" Width="100%" Font-Size="Smaller">
            <PagerStyle Mode="NextPrevAndNumeric" Position="Bottom" />
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting CellSelectionMode="None" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID,Cve_Estatus_Proveedor,Type" NoMasterRecordsText="No hay registros"
                AllowAutomaticUpdates="False" TableLayout="Fixed">

                <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                </RowIndicatorColumn>
                <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
                </ExpandCollapseColumn>
                <Columns>

                    <telerik:GridTemplateColumn DataField="Clave" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" FilterControlAltText="Filter clave column" HeaderText="Clave" UniqueName="clave" Visible="true">

                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "ID")%>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn DataField="Nombre_RazonSocial" HeaderStyle-HorizontalAlign="Center" FilterControlAltText="Filter Nombre_RazonSocial column" HeaderText="Nombre o Razón Social" UniqueName="Nombre_RazonSocial" ItemStyle-Font-Size="Smaller">

                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "Dx_Razon_Social")%>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn DataField="NombreComercial" HeaderStyle-HorizontalAlign="Center" FilterControlAltText="Filter NombreComercial column" HeaderText="Nombre Comercial" UniqueName="NombreComercial" ItemStyle-Font-Size="Smaller">

                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "Dx_Nombre_Comercial")%>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn DataField="tipo" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" FilterControlAltText="Filter tipo column" HeaderText="Tipo" UniqueName="tipo">

                        <ItemTemplate>
                            <asp:Label ID="lblTipo" runat="server" Text='<%# Eval("Type").ToString()=="M" ? "Matriz" :Eval("Type").ToString()=="SB_F" ? "Sucursal Fisica" : "Sucursal Virtual" %>' />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn DataField="Estatus" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" FilterControlAltText="Filter estatus column" HeaderText="Estatus" UniqueName="estatus">

                        <ItemTemplate>
                            <%--<%#DataBinder.Eval(Container.DataItem, ' Eval("Dx_Estatus_Proveedor").ToString()=="1" ? "PENDIENTE" :Eval("Dx_Estatus_Proveedor").ToString()=="2" ? "ACTIVO" : Eval("Dx_Estatus_Proveedor").ToString()=="3" ? "INACTIVO":Eval("Dx_Estatus_Proveedor").ToString()=="4" ? "CANCELADO":"RECHAZADO"' )%>--%>
                            <asp:Label ID="lblEstatus" runat="server" Text='<%# Eval("Dx_Estatus_Proveedor") %>' />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>



                    <telerik:GridTemplateColumn DataField="acciones" FilterControlAltText="Filter acciones column" HeaderText="Acciones" UniqueName="acciones" HeaderStyle-HorizontalAlign="Center">

                        <ItemTemplate>
                            <telerik:RadComboBox runat="server" ID="LSB_Acciones" Enabled="false" Width="80%" OnSelectedIndexChanged="LSB_Acciones_SelectedIndexChanged" AutoPostBack="true">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Elegir Opcion" />
                                </Items>
                            </telerik:RadComboBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>



                    <telerik:GridTemplateColumn FilterControlAltText="Filter colSelect column" HeaderStyle-HorizontalAlign="Center" HeaderText="Seleccionar" UniqueName="colSelect" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                        <ItemTemplate>
                            <asp:CheckBox ID="ckbSelect" runat="server" AutoPostBack="true" OnCheckedChanged="ckbSelect_OnCheckedChanged" />
                        </ItemTemplate>

                    </telerik:GridTemplateColumn>



                </Columns>
                <EditFormSettings>
                    <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                    </EditColumn>
                </EditFormSettings>
            </MasterTableView>
        </telerik:RadGrid>
        <table>
            <tr>
                <td></td>
            </tr>
            <tr>
                <td></td>
            </tr>
            <tr>
                <td style="text-align: center"></td>
            </tr>

        </table>
        <telerik:RadButton runat="server" ID="rad_btnAceptar" Text="Aceptar" Enabled="false" OnClick="rad_btnAceptar_Click"></telerik:RadButton>

    </fieldset>

    <%--</div>--%>
         
    <div style="display: none">
        <asp:Button ID="HiddenButton" BackColor="#FFFFFF" OnClick="HiddenButton_Click" runat="server" Width="0px" />
    </div>
         </telerik:RadAjaxPanel>
    <%--    
      
    <%--  </telerik:RadAjaxPanel>--%>
    <%--ctl00_MainContent_modalPopupCancelar_C_txt_OTROS--%>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" LoadingPanelID="RadAjaxLoadingPanel2" runat="server" Width="100%">
        <telerik:RadWindow ID="modalPopupCancelar" MaxHeight="620px" MaxWidth="800px" MinHeight="420px" MinWidth="500px" runat="server" Width="800px" Height="620px" Modal="true" Title="Confirmar" AutoSize="False">
            <ContentTemplate>
                <div style="padding: 10px">
                    <table style="width: 100%">
                        <tr>
                            <td style="text-align: center">
                                <asp:Label runat="server" Text="Encabezado" ID="lbl_MODAL_Encabezado" Font-Size="X-Large" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label runat="server" Text="Titulo" ID="lbl_Modal_TituloMenor" Font-Size="Medium"></asp:Label>

                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="text-align: left">
                                <br />
                                <asp:Label ID="lbl_Motivos" Visible="true" runat="server" Text="Motivos:" /><br />
                                <%--<telerik:RadComboBox runat="server" CheckBoxes="true" ID="cmb_Motivos" EnableCheckAllItemsCheckBox="true" Width="80%" CheckedItemsTexts="FitInInput"></telerik:RadComboBox>--%>
                                <%--<telerik:RadListBox ID="check_Motivos" runat="server" CheckBoxes="true" ShowCheckAll="true" Width="100%" Height="200px" OnItemCheck="check_Motivos_ItemCheck" AutoPostBack="true"></telerik:RadListBox>--%>
                                <asp:CheckBoxList ID="Check_Motivos_asp" runat="server" Width="100%" Height="200px"></asp:CheckBoxList>
                                <br />
                                <br />
                                <asp:Label ID="lbl_otrosMotivos" Style="display: none" runat="server" Text="Otros Motivos:" /><br />
                                <asp:TextBox ID="txt_OTROS" runat="server" Height="50" Width="95%" TextMode="MultiLine" MaxLength="100" Style="display: none"></asp:TextBox><br />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                    ErrorMessage="Debe ingresar hasta un maximo de 200 caracteres"
                                    ValidationExpression="^([\S\s]{0,200})$"
                                    ControlToValidate="txt_OTROS"
                                    Display="Dynamic"></asp:RegularExpressionValidator>
                                <br />
                                <br />
                                <asp:Label ID="observaciones" runat="server" Text="Observaciones:" /><br />
                                <asp:TextBox ID="txtObservaciones" runat="server" Height="60px" Width="95%" TextMode="MultiLine" MaxLength="100"></asp:TextBox><br />
                                <asp:RegularExpressionValidator ID="reg" runat="server"
                                    ErrorMessage="Debe ingresar hasta un maximo de 200 caracteres"
                                    ValidationExpression="^([\S\s]{0,200})$"
                                    ControlToValidate="txtObservaciones"
                                    Display="Dynamic"></asp:RegularExpressionValidator><br />
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <br />
                                <asp:Button ID="btn_AceptarPOP" runat="server" Text="Aceptar" Enabled="false" OnClick="btn_AceptarPOP_Click" />&nbsp;&nbsp;
                                <asp:Button ID="btn_CancelarPOP" runat="server" Text="Cancelar" Enabled="True" />
                            </td>
                        </tr>
                        <tr>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </telerik:RadWindow>
    </telerik:RadAjaxPanel>

    <telerik:RadAjaxPanel runat="server" ID="RadAjaxPanel2" LoadingPanelID="RadAjaxLoadingPanel2">
        <telerik:RadWindow ID="popupReactivar" runat="server" Width="600px" Height="450px" Modal="true" Title="Confirmar">
            <ContentTemplate>
                <div style="padding: 10px; text-align: center">
                    <table style="width: 100%">
                        <tr>
                            <td align="center">
                                <asp:Label runat="server" Text=" Capture los motivos por los cuales se reactivara el proveedor" ID="Label1" Font-Size="Large" Font-Bold="true"></asp:Label>
                                <br />
                                <br />
                                </p>  
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center">
                                <asp:Label runat="server" Text="Motivos" ID="Label2" Font-Size="Medium"></asp:Label>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center">
                                <br />
                                <asp:TextBox ID="txt_motivosReactivar" runat="server" Height="110px" Width="95%" Style="display: block" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <br />
                                <asp:Button ID="btnFinalizarReactivar" runat="server" Text="Finalizar" Enabled="false" OnClick="btnFinalizarReactivar_Click"/>&nbsp;&nbsp;
                                                <asp:Button ID="btn_CancelarReactivar" runat="server" Text="Cancelar" Enabled="true" />

                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </telerik:RadWindow>
    </telerik:RadAjaxPanel>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server" Skin="Office2010Silver">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadWindowManager ID="RWM_Ventana" runat="server" Skin="Office2010Silver">
    </telerik:RadWindowManager>
</asp:Content>
