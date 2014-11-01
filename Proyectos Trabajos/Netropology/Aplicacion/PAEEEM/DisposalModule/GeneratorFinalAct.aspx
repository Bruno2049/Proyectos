<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="GeneratorFinalAct.aspx.cs"
 Title="Generar Acta Circunstanciada"  Inherits="PAEEEM.DisposalModule.GeneratorFinalAct" %>

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
            width: 200px;
        }
        .Button
        {
            width: 120px;
        }        
        .style1
        {
            width: 212px;
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
                        <td >
                            <div align="right">
                                <asp:Label ID="lblFrom" Text="From FOLIO " runat="server" Font-Size="11pt" CssClass="Label"
                                    Width="80px" /></div>
                        </td>
                        <td class="style1">
                            <div>
                                <asp:DropDownList ID="drpFrom" runat="server" Font-Size="11px" AutoPostBack="true"
                                    CssClass="DropDownList"   />                                   
                            </div>
                        </td>
                          <td>
                            <div align ="right">
                                <asp:Label ID="lblTo" Text="To FOLIO " runat="server" Font-Size="11pt" CssClass="Label"
                                    Width="83px" /></div>
                        </td>
                        <td>
                            <div>
                                <asp:DropDownList ID="drpTo" runat="server" Font-Size="11px" AutoPostBack="true"
                                    CssClass="DropDownList"   />                                   
                            </div>
                        </td>
                        <td  width="150px"></td>
                        <td width="150px"></td>
                       </tr>
                </table>
                <br />
                <div>
                    <asp:GridView ID="grvPartialAct" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                        AllowPaging="True" PageSize="20" 
                        onrowdatabound="grvPartialAct_RowDataBound">
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <Columns>
                            <asp:BoundField DataField="Codigo_Partial" HeaderText="ID Corte"></asp:BoundField>                          
                            <asp:BoundField DataField="Dt_Fecha_Creacion" HeaderText="Fecha Creación" />
                            <asp:BoundField DataField="Dx_Razon_Social" HeaderText="Centro Disposición" />
                            <asp:BoundField DataField="Dx_Estatus_Disposicion" HeaderText="Estatus">  </asp:BoundField>                            
                        </Columns>
                        <PagerTemplate>
                        </PagerTemplate>
                    </asp:GridView>
                    <webdiyer:AspNetPager ID="AspNetPager" CssClass="pagerDRUPAL" runat="server" PageSize="20"
                        AlwaysShow="True" ShowCustomInfoSection="Right" ShowDisabledButtons="true" ShowPageIndexBox="always"
                        PageIndexBoxType="DropDownList" CustomInfoHTML="Page:<font color='red'><b>%currentPageIndex%</b></font>/%PageCount%&nbsp;&nbsp;PageSize:%PageSize%&nbsp;&nbsp;Record:%StartRecordIndex%-%EndRecordIndex% of %RecordCount%"
                        UrlPaging="true" OnPageChanged="AspNetPager_PageChanged" OnPageChanging="AspNetPager_PageChanging"
                        FirstPageText="Primero" LastPageText="Último" NextPageText="Siguiente" PrevPageText="Anterior"
                        CurrentPageButtonClass="cpb">
                    </webdiyer:AspNetPager>
                </div>
                <br />
                <div>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnGeneratorAct" Text="Generar Acta" runat="server" OnClientClick="return confirm(' Se Generará el Acta Circunstanciada.');"
                        CssClass="Button" onclick="btnGeneratorAct_Click" />
                         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                         <asp:Button ID="btnCancel" runat="server" Text="Cancelar" 
                        CssClass="Button" 
                        OnClientClick="return.confirm(' ¿ Desea Salir de ésta Pantalla ?');" onclick="btnCancel_Click" 
                     />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>