<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MaterialRegistryMonitor.aspx.cs" Inherits="PAEEEM.DisposalModule.MaterialRegistryMonitor" Title="Registro de Recuperación de Residuos y Materiales" %>
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
        .DropDownList
        {
            width: 250px;
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
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td colspan="2" style="text-align:right">
                        <asp:Literal ID="literal1" runat='server' Text="Fecha Recepción" />
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div>
                                <asp:Label ID="Label2" Text="Programa" runat="server" CssClass="Label"
                                   /></div>
                        </td>
                        <td>
                            <div>
                                <asp:DropDownList ID="drpProgram" runat="server" AutoPostBack="true"
                                    CssClass="DropDownList" 
                                    onselectedindexchanged="drpProgram_SelectedIndexChanged"/>
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:Label ID="Label3" Text="Desde"  runat="server" CssClass="Label"
                                   /></div>
                        </td>
                        <td>
                            <div>
                                <asp:TextBox ID="txtReceiptFromDate" runat="server"  CssClass="Wdate" 
                                 Width="200px"
                    onclick="WdatePicker({dateFmt:'yyyy-MM-dd',lang:'en'})"></asp:TextBox>
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:Label ID="Label4" Text="Hasta" runat="server" CssClass="Label"
                                    /></div>
                        </td>
                        <td>
                            <div>
                              <asp:TextBox ID="txtReceiptToDate" runat="server"  CssClass="Wdate" Width="200px"
                    onclick="WdatePicker({dateFmt:'yyyy-MM-dd',lang:'en'})"></asp:TextBox>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div>
                                <asp:Label ID="Label5" Text="Crédito" runat="server" CssClass="Label"
                                   /></div>
                        </td>
                        <td>
                            <div>
                                <asp:DropDownList ID="drpCredit" runat="server" AutoPostBack="true"
                                    CssClass="DropDownList" 
                                    onselectedindexchanged="drpCredit_SelectedIndexChanged"/>
                            </div>
                        </td>
                        <td>
                            <div>
                                </div>
                        </td>
                        <td colspan="2" style="text-align:right">
                            <div>
                                <asp:Literal ID="literal2" runat="server" Text="Fecha Inhabilitación" />
                            </div>
                        </td>
                        <td>
                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div>
                                <asp:Label ID="Label7" Text="Folio Boleta" runat="server" CssClass="Label"
                                   /></div>
                        </td>
                        <td>
                            <div>
                                <asp:DropDownList ID="drpInteralCode" runat="server" AutoPostBack="true"
                                    CssClass="DropDownList"/>
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:Label ID="Label9" runat="server" CssClass="Label" Text="Desde" />
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:TextBox ID="txtInhabilitacionFromDate" runat="server" CssClass="Wdate" 
                                    onclick="WdatePicker({dateFmt:'yyyy-MM-dd',lang:'en'})" Width="200px"></asp:TextBox>
                            </div>
                        </td>
                        <td>
                          
                            <asp:Label ID="Label10" runat="server" CssClass="Label" Text="Hasta" />
                          
                        </td>
                        <td>
                            
                            <asp:TextBox ID="txtInhabilitacionToDate" runat="server" CssClass="Wdate" 
                                onclick="WdatePicker({dateFmt:'yyyy-MM-dd',lang:'en'})" Width="200px"></asp:TextBox>
                            
                        </td>
                    </tr>
                     <tr>
                        <td>
                            <div>
                                <asp:Label ID="Label8" runat="server" CssClass="Label" Text="Tecnología" />
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:DropDownList ID="drpTechnology" runat="server" AutoPostBack="true" 
                                    CssClass="DropDownList" />
                            </div>
                        </td>
                        <td>
                         
                            <asp:Label ID="Label6" runat="server" CssClass="Label" Text="Distribuidor" />
                         
                        </td>
                        <td>
                           
                            <asp:DropDownList ID="drpDistributor" runat="server" AutoPostBack="true" 
                                CssClass="DropDownList" />
                           
                        </td>
                        <td>
                          
                        </td>
                        <td>
                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            </td>
                        <td style="text-align:right">
                            <asp:Button ID="btnSearch" runat="server" Text="Buscar" CssClass="Button" 
                                onclick="btnSearch_Click" />
                        </td>
                        <td>
                            </td>
                        <td style="text-align:left">
                            <asp:Button ID="btnCancel" runat="server" Text="Salir" CssClass="Button"  />
                        </td>
                        <td>
                            </td>
                        <td>
                            </td>
                    </tr>
                </table>
                <br />
                <div>
                    <asp:GridView ID="grdOldEquipment" runat="server" AutoGenerateColumns="False"
                        CssClass="GridViewStyle" AllowPaging="True" PageSize="20" 
                        onrowcreated="grdOldEquipment_RowCreated" 
                        ondatabound="grdOldEquipment_DataBound">
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <Columns>
                            <asp:BoundField DataField="Dx_Nombre_Programa" HeaderText="Programa"></asp:BoundField>
                            <asp:BoundField DataField="Dx_Nombre_General" HeaderText="Tecnología" />
                            <asp:BoundField DataField="Id_Folio" HeaderText="Folio Boleta" />
                            <asp:BoundField DataField="No_Credito" HeaderText="Crédito" />
                            <asp:BoundField DataField="Dt_Fecha_Recepcion" HeaderText="Fecha Recepción" DataFormatString="{0:dd-MM-yyyy}" />
                            <asp:BoundField DataField="Dt_Fecha_Inhabilitacion" HeaderText="Fecha Inhabilitación" DataFormatString="{0:dd-MM-yyyy}" />
                            <asp:BoundField DataField="Dx_Marca" HeaderText="Marca"></asp:BoundField>
                            <asp:BoundField DataField="Dx_Unidades" HeaderText="Capacidad"></asp:BoundField>
                            <asp:BoundField DataField="Dx_Color" HeaderText="Color"></asp:BoundField>
                            <asp:BoundField DataField="ProviderComercialName" HeaderText="Distribuidor"></asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="ckbSelect" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Id_Credito_Sustitucion" HeaderText="Id_Credito_Sustitucion"></asp:BoundField>
                        </Columns>
                        <PagerTemplate>
                        </PagerTemplate>
                    </asp:GridView>
                    <webdiyer:AspNetPager ID="AspNetPager" CssClass="pagerDRUPAL" runat="server" PageSize="3"
                        AlwaysShow="True" ShowCustomInfoSection="Right" ShowDisabledButtons="true" ShowPageIndexBox="always"
                        PageIndexBoxType="DropDownList" CustomInfoHTML="Page:<font color='red'><b>%currentPageIndex%</b></font>/%PageCount%&nbsp;&nbsp;PageSize:%PageSize%&nbsp;&nbsp;Record:%StartRecordIndex%-%EndRecordIndex% of %RecordCount%"
                        UrlPaging="false" OnPageChanged="AspNetPager_PageChanged" OnPageChanging="AspNetPager_PageChanging"
                        FirstPageText="Primero" LastPageText="Último" NextPageText="Siguiente" PrevPageText="Anterior"
                        CurrentPageButtonClass="cpb">
                    </webdiyer:AspNetPager>
                </div>
                <br />
                <table width="100%">
                <tr>
                <td style="text-align:center">
                    <asp:Button ID="btnRegistry" runat="server" CssClass="Button" 
                        onclick="btnRegistry_Click" Text="Registrar" />
                </td>
                </tr>
                
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
