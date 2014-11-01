<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MaterialRecoveryRegistry.aspx.cs"
    Inherits="PAEEEM.DisposalModule.GasRecoveryRegistry" Title="Registro de Recuperación de Residuos y Materiales" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Resources/Css/Pager.css" rel="stylesheet" type="text/css" />
    <link href="../Resources/Css/GridView.css" rel="stylesheet" type="text/css" />

    <script src="../Resources/Script/Calendar/WdatePicker.js" type="text/javascript"></script>

    <link href="../Resources/Script/Calendar/skin/default/datepicker.css" type="text/css" />
    <style type="text/css">
        .Label
        {
            width: 90px;
            color: #333333;
            font-size: 16px;
        }
        .Label_1
        {
            width: 260px;
            color: #333333;
            font-size: 16px;
        }
         .Label_4
        {
            width: 260px;
            color: #333333;
            font-size: 30px;
        }

        .DropDownList
        {
            width: 250px;
        }
        .TextBox
        {
            width: 250px;
        }
        .Button
        {
            width: 150px;
        }
        .Disvisible
        {
            visibility: hidden;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="panel" runat="server">
        <ContentTemplate>
            <div>
                <div align="left">
                   
                   <br>
                   <table>
                   <tr>
                   <td>
                   <asp:Image runat="server" ImageUrl="../images/t_registro_recuperacion3.png" ImageAlign="Top" />
                   </td>
                   <td>
                    <asp:Literal ID="literalMaterial" runat='server' />
                       <asp:Label ID="lblTitle" runat="server" Text="Label" Visible="False"></asp:Label>
                   </td>
                   </tr>
                   </table>
    	           <br>
                  
                </div>
                <div align="right">
                    <br />
                    <asp:Label ID="Label1" Text="Fecha" runat="server" CssClass="Label" />
                    <asp:Literal ID="literalFecha" runat='server' />
                </div>
                <asp:Wizard ID="wizardPages" runat="server" Style="width: 100%;" DisplaySideBar="false"
                    ActiveStepIndex="0" Width="200px">
                    <%--<StartNavigationTemplate>
                        <div>
                            <asp:Button ID="StartNextButton" runat="server" CommandName="MoveNext" Text="Siguiente" />
                            <asp:Button ID="StartPrevious" runat="server" CommandName="Previous" Text="Regresar"
                                OnClientClick="return confirm('¿ Desea Regresar a la Pantalla de Registro de Recuperación ?')" />
                        </div>
                    </StartNavigationTemplate>--%><StartNextButtonStyle CssClass="Disvisible" /><FinishPreviousButtonStyle CssClass="Disvisible" /><FinishCompleteButtonStyle CssClass="Disvisible" /><WizardSteps>
                        <asp:WizardStep runat="server" ID="CaptureWeight" StepType="Start" Title="REGISTRO DE RECUPERACION DE RESIDUOS">
                            <div>
                          
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/t_equipos_recuperacion.png" />
                            </div>
                            <br />
                            <table width="100%">
                                <tr>
                                    <td width="11%">
                                        <asp:Label ID="Label2" runat="server" Text="Programa" CssClass="Label"></asp:Label>
                                    </td>
                                    <td width="31%">
                                        <asp:DropDownList ID="drpProgram" runat="server" CssClass="DropDownList" AutoPostBack="True"
                                            OnSelectedIndexChanged="drpProgram_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td width="28%">
                                        <asp:Label ID="Label3" runat="server" Text="Seleccione la Fecha de Recuperación" CssClass="Label_1"></asp:Label>
                                    </td>
                                    <td width="30%">
                                        <asp:TextBox ID="txtRecoveryDate" runat="server" CssClass="Wdate" Width="200px" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',lang:'en'})"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="11%">
                                        <asp:Label ID="Label4" runat="server" Text="Tecnología" CssClass="Label"></asp:Label>
                                    </td>
                                    <td width="31%">
                                        <asp:DropDownList ID="drpTechnology" runat="server" CssClass="DropDownList" AutoPostBack="True"
                                            OnSelectedIndexChanged="drpTechnology_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td width="28%">
                                    </td>
                                    <td width="30%">
                                    </td>
                                </tr>
                                <tr>
                                    <td width="11%">
                                        <asp:Label ID="Label8" runat="server" Text="Material" CssClass="Label"></asp:Label>
                                    </td>
                                    </td>
                                    <td width="31%">
                                        <asp:DropDownList ID="drpMaterialType" runat="server" CssClass="DropDownList" AutoPostBack="True"
                                            OnSelectedIndexChanged="drpMaterialType_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td width="28%">
                                    </td>
                                    <td width="30%">
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <asp:Panel ID="panelMaterial" runat="server">
                                <table width="100%">
                                    <tr>
                                        <td width="42%" colspan="2">
                                            <asp:Label ID="lblMaterialName" runat="server" Text="Gas Refrigerante" CssClass="Label_1"></asp:Label>
                                            &nbsp;
                                        </td>
                                        <td width="58%">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="11%">
                                            <asp:Label ID="lblGasType" runat="server" Text="Tipo Residuo" CssClass="Label"></asp:Label>
                                        </td>
                                        <td width="31%">
                                            <asp:DropDownList ID="drpGasType" runat="server" CssClass="DropDownList" 
                                                AutoPostBack="True" OnSelectedIndexChanged="drpGasType_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td width="58%">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="11%">
                                            <asp:Label ID="Label7" runat="server" Text="Peso Recuperado" CssClass="Label"></asp:Label>
                                        </td>
                                        <td width="31%">
                                            <asp:TextBox ID="txtWeight" runat="server" CssClass="TextBox" ToolTip="(*) Campo Vacío o con Formato Inválido"></asp:TextBox>
                                            &nbsp;<asp:Literal ID="lblUnit" runat='server' Text="Kgs." /></td>
                                        <td width="58%">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtWeight"
                                                ErrorMessage="(*) Campo Vacío o con Formato Inválido"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                                ControlToValidate="txtWeight" ValidationExpression="^\d+(\.\d+)?$"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <br />
                            <table width="100%">
                                <tr>
                                    <td width="45%" style="text-align: right">
                                        <asp:Button ID="btnRecovery" runat="server" Text="Siguiente" CssClass="Button" OnClick="btnNext_Click" />
                                    </td>
                                    <td width="10%">
                                    </td>
                                    <td width="45%" style="text-align: left">
                                        <asp:Button ID="btnCancel" runat="server" Text="Salir" CssClass="Button" OnClientClick="return confirm('¿ Desea Salir de ésta Pantalla ?');"
                                            OnClick="btnCancel_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:WizardStep>
                        <asp:WizardStep runat="server" ID="Registry" StepType="Finish" Title="REGISTRO DE EQUIPOS CON RECUPERACION">
                            <div>
                                <br>
                                <asp:Image ID="Image2" runat="server" ImageUrl="~/DisposalModule/images/t_equipos1.png" />
                            </div>
                            <br />
                            <table width="100%">
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td colspan="2" style="text-align: right">
                                        <asp:Literal ID="literal2" runat='server' Text="Fecha Recepción" />
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div>
                                            <asp:Label ID="Label6" Text="Programa" runat="server" CssClass="Label" /></div>
                                    </td>
                                    <td>
                                        <div>
                                            <asp:DropDownList ID="drpProgram1" runat="server" CssClass="DropDownList" />
                                        </div>
                                    </td>
                                    <td>
                                        <div>
                                            <asp:Label ID="Label9" Text="Desde" runat="server" CssClass="Label" /></div>
                                    </td>
                                    <td>
                                        <div>
                                            <asp:TextBox ID="txtReceiptFromDate" runat="server" CssClass="Wdate" Width="200px"
                                                onFocus="WdatePicker({maxDate:'#F{$dp.$D(\'ctl00_MainContent_wizardPages_txtReceiptToDate\')}'})"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td>
                                        <div>
                                            <asp:Label ID="Label10" Text="Hasta" runat="server" CssClass="Label" /></div>
                                    </td>
                                    <td>
                                        <div>
                                            <asp:TextBox ID="txtReceiptToDate" runat="server" CssClass="Wdate" Width="200px"
                                                onFocus="WdatePicker({minDate:'#F{$dp.$D(\'ctl00_MainContent_wizardPages_txtReceiptFromDate\')}'})"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div>
                                            <asp:Label ID="Label11" Text="Crédito" runat="server" CssClass="Label" /></div>
                                    </td>
                                    <td>
                                        <div>
                                            <asp:DropDownList ID="drpCredit" runat="server" AutoPostBack="true" CssClass="DropDownList"
                                                OnSelectedIndexChanged="drpCredit_SelectedIndexChanged" />
                                        </div>
                                    </td>
                                    <td>
                                        <div>
                                        </div>
                                    </td>
                                    <td colspan="2" style="text-align: right">
                                        <div>
                                            <asp:Literal ID="literal3" runat="server" Text="Fecha Inhabilitación" />
                                        </div>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div>
                                            <asp:Label ID="Label12" Text="Folio de Ingreso" runat="server" CssClass="Label" /></div>
                                    </td>
                                    <td>
                                        <div>
                                            <asp:DropDownList ID="drpInteralCode" runat="server" CssClass="DropDownList" />
                                        </div>
                                    </td>
                                    <td>
                                        <div>
                                            <asp:Label ID="Label13" runat="server" CssClass="Label" Text="Desde" />
                                        </div>
                                    </td>
                                    <td>
                                        <div>
                                            <asp:TextBox ID="txtInhabilitacionFromDate" runat="server" CssClass="Wdate" Width="200px"
                                                onFocus="WdatePicker({maxDate:'#F{$dp.$D(\'ctl00_MainContent_wizardPages_txtInhabilitacionToDate\')}'})"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label14" runat="server" CssClass="Label" Text="Hasta" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtInhabilitacionToDate" runat="server" CssClass="Wdate" Width="200px"
                                            onFocus="WdatePicker({minDate:'#F{$dp.$D(\'ctl00_MainContent_wizardPages_txtInhabilitacionFromDate\')}'})"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div>
                                            <asp:Label ID="Label15" runat="server" CssClass="Label" Text="Tecnología" />
                                        </div>
                                    </td>
                                    <td>
                                        <div>
                                            <asp:DropDownList ID="drpTechnology1" runat="server" CssClass="DropDownList" />
                                        </div>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label16" runat="server" CssClass="Label" Text="Proveedor" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drpDistributor" runat="server" CssClass="DropDownList" AutoPostBack="true" OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged" />
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:Button ID="btnSearch" runat="server" Text="Buscar" CssClass="Button" OnClick="btnSearch_Click" />
                                    </td>
                                    <td>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:Button ID="btnPrevious" runat="server" Text="Regresar" CssClass="Button" OnClientClick="return confirm('¿ Desea Regresar a la Pantalla Previa para Registro de Recuperación ?');"
                                            OnClick="btnPrevious_Click" />
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <div>
                                <asp:GridView ID="grdOldEquipment" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                                    AllowPaging="True" PageSize="20" OnDataBound="grdOldEquipment_DataBound" DataKeyNames="Id_Credito_Sustitucion">
                                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                                    <FooterStyle CssClass="GridViewFooterStyle" />
                                    <RowStyle CssClass="GridViewRowStyle" />
                                    <PagerStyle CssClass="GridViewPagerStyle" />
                                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                    <Columns>
                                        <asp:BoundField DataField="Dx_Nombre_Programa" HeaderText="Programa"></asp:BoundField>
                                        <asp:BoundField DataField="Dx_Nombre_General" HeaderText="Tecnología" />
                                        <asp:BoundField DataField="Id_Folio" HeaderText="Folio de Ingreso" />
                                        <asp:BoundField DataField="No_Credito" HeaderText="Crédito" />
                                        <asp:BoundField DataField="Dt_Fecha_Recepcion" HeaderText="Fecha Recepción" DataFormatString="{0:dd-MM-yyyy}" />
                                        <asp:BoundField DataField="Dt_Fecha_Inhabilitacion" HeaderText="Fecha Inhabilitación"
                                            DataFormatString="{0:dd-MM-yyyy}" />
                                        <asp:BoundField DataField="Dx_Marca" HeaderText="Marca"></asp:BoundField>
                                        <asp:BoundField DataField="No_Capacidad" HeaderText="Capacidad"></asp:BoundField>
                                        <asp:BoundField DataField="Dx_Color" HeaderText="Color"></asp:BoundField>
                                        <asp:BoundField DataField="ProviderComercialName" HeaderText="Proveedor"></asp:BoundField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ckbSelect" runat="server" />
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
                                    CurrentPageButtonClass="cpb" InvalidPageIndexErrorMessage="¡ El Índice no es Válido !" PageIndexOutOfRangeErrorMessage="! Índice Fuera de Rango !">
                                </webdiyer:AspNetPager>
                            </div>
                            <br />
                            <table width="100%">
                                <tr>
                                    <td style="text-align: center">
                                        <asp:Button ID="btnRegistry" runat="server" CssClass="Button" OnClick="btnRegistry_Click"
                                            OnClientClick="return confirm('¡ Será Registrado el Peso Capturado con la Fecha seleccionada para el Lote de Equipos elegidos !. Confirmar');"
                                            Text="Registrar" />
                                    </td>
                                </tr>
                            </table>
                        </asp:WizardStep>
                    </WizardSteps>
                </asp:Wizard>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
