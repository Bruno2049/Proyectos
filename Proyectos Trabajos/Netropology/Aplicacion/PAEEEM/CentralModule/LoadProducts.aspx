<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LoadProducts.aspx.cs" Inherits="PAEEEM.CentralModule.LoadProducts" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .Etiqueta {
            padding: 5px 15px ;
        }
       
         .EtiquetaSubEstacion {
            padding: 5px 15px ;
             display: none;
        }
         .dvMainPanel {
            clear:both;
            width: 100%;
            display: inline;
            margin-left: auto;
            margin-right: auto;
        }
        #DivGrid {
            clear:both;
        }
        
    </style>
   
   <script type="text/javascript" id="telerikClientEvents2">
//<![CDATA[

       function CargaArchivo_FilesUploaded(sender,args)
       {
           //Add JavaScript handler code here
           var btn = document.getElementById("btnAttach");
           btn.click();
       }

       function RadPanel_OnRequestStart(sender,args) {
           args.EnableAjax = false;
       }
	   
//]]>
</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server"></telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadPanel" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Width="100%" ClientEvents-OnRequestStart="RadPanel_OnRequestStart" >
        <h2><asp:Label ID="Label1" runat="server" Font-Size="Large" Font-Bold="True" Text="Carga Masiva de Productos" /></h2>
       <fieldset class="legend_info">
        <legend style="font-size: 14px;">Productos</legend>
           <asp:Label ID="lbTecnologia" runat="server" Text="Tecnología" CssClass="Etiqueta"></asp:Label>
            <telerik:RadComboBox ID="cboTecnologias" runat="server" OnSelectedIndexChanged="cboTecnologias_SelectedIndexChanged" AutoPostBack="True" Width="300px">
                <Items>
                    <telerik:RadComboBoxItem runat="server" Text="Seleccione Tecnología" Value="0" />
                </Items>
           </telerik:RadComboBox>
           <br/><br/>
           <asp:Label ID="lbSubEstatciones" runat="server" Text="Subestación" Visible="False"></asp:Label>
            <telerik:RadComboBox ID="cboSubEstacion" runat="server" Visible="False" AutoPostBack="True" Width="300px" OnSelectedIndexChanged="cboSubEstacion_SelectedIndexChanged">
                <Items>
                    <telerik:RadComboBoxItem runat="server" Text="Seleccione Subestación" Value="0" />
                    <telerik:RadComboBoxItem runat="server" Text="Clase aérea" Value="1" />
                    <telerik:RadComboBoxItem runat="server" Text="Con acometida" Value="2" />
                    <telerik:RadComboBoxItem runat="server" Text="Subterránea Integrarse a la Red" Value="3" />
                </Items>
           </telerik:RadComboBox>
           <br/>
           <br/>
        <div id="dvPanel" runat="server" style="width: 100%; display: none ">
          <table width="100%" class="dvMainPanel">
              <tr>
                  <td style="width: 20%"></td>
                  <td style="width: 40%">
                    <div id="dvCargar">
                         <asp:Image ID="imgCarga" runat="server" Height="126px" Width="141px" ImageUrl="~/CentralModule/images/upload.png" />
                         <br/>
                         <br/>
                         <telerik:RadAsyncUpload ID="CargaArchivo" runat="server" OnFileUploaded="CargaArchivoFileUploaded" AutoAddFileInputs="True" OnClientFilesUploaded="CargaArchivo_FilesUploaded" Width="100%">
                            <FileFilters>
                                <telerik:FileFilter Description="Excel 2003" Extensions=".xls" />
                                <telerik:FileFilter Description="Excel 2007 - 2010" Extensions=".xlsx" />
                            </FileFilters>
                            </telerik:RadAsyncUpload>
                   </div>
                  </td>
                  <td style="width: 40%">
                       <div id="dvDesargar">
                        <asp:ImageButton ID="btnDescargar" runat="server" ImageUrl="~/CentralModule/images/Descargar.png" Height="158px" Width="153px" OnClick="btnDescargar_Click" />
                    </div>
                  </td>
                  <td style="width: 20%"></td>
              </tr>
          </table>
          <br/>
          <br/>
          <div id="dvResumenCarga" runat="server" style="display: none">
                <table width="100%">
                    <tr>
                        <td>&nbsp;&nbsp;&nbsp;</td>
                        <td><asp:Label ID="lbTotalrows" runat="server" Text="Label"></asp:Label></td>
                        <td>&nbsp;&nbsp;&nbsp;</td>
                        <td><asp:Label ID="lbrowsOk" runat="server" Text="Label"></asp:Label></td>
                        <td>&nbsp;&nbsp;&nbsp;</td>
                        <td><asp:Label ID="lbrowsKo" runat="server" Text="Label"></asp:Label></td>
                        <td>&nbsp;&nbsp;&nbsp;</td>
                    </tr>
                </table>
            </div>

          <div id="DivGrid" runat="server" style="width:100%;display: none"  >
               <h3>Detalle</h3> 
               <br/>  
                <telerik:RadGrid ID="gdLogErrores" runat="server" AutoGenerateColumns="False" CellSpacing="0" GridLines="None"
                    AllowPaging="True" OnNeedDataSource="gdLogErrores_NeedDataSource">
                       <ClientSettings>
                           <Selecting CellSelectionMode="None" />
                       </ClientSettings>
                       <MasterTableView>
                        <Columns>
                               <telerik:GridBoundColumn DataField="NoRegistro" HeaderText="FILA" FilterControlAltText="Filter column column" UniqueName="column">
                               </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="Campo" HeaderText="CAMPO" FilterControlAltText="Filter column column" UniqueName="column">
                               </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="Error" HeaderText="MOTIVO" FilterControlAltText="Filter column column" UniqueName="column">
                               </telerik:GridBoundColumn>
                           </Columns>
                       </MasterTableView>
                    </telerik:RadGrid>
           </div>
           
        </div>
         
          
    </fieldset>
       <div style="display: none">
               <asp:Button ID="btnAttach" runat="server" Text="Button" ClientIDMode="Static" />
                <asp:HiddenField ID="hidIdLogHeader" runat="server" />
           </div>
    </telerik:RadAjaxPanel>
    </asp:Content>
