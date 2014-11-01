<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="PAEEEM.SupplierModule.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <style type="text/css">
    table {
        width: 100%;
        height: auto;
    }
    
    .columnaUno {
        width: 10%;        
    }
    
    .columnaDos {
        width: 22%;
    }
    
    
    .columnaTres 
    {
        width: 24%
    }
    
    .columnaSeparacion {
        width: 5%
    }
    
    #datosCliente {
        width: 100%;
        height: auto;        
    }
    
    #tecnologias
    {
        width: 100%;
        height: auto;  
        margin-top: 10px;        
    }
    
    .rowSeparacion {
        height: 15px;
    }
    
    fieldset {
        border: 1px solid lightblue;
        padding: 4px;
    }
    
              
</style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="datosCliente">
    <table>
        <tr>
            <td class="columnaUno">
                <asp:Label ID="Label1" runat="server" Text="Nombre o Razón Social:"></asp:Label>
            </td>
            <td class="columnaUno">
                <asp:TextBox ID="txtNombRazonSocial" runat="server" Enabled="False" 
                    Width="200px"></asp:TextBox>
            </td>
            <td class="columnaSeparacion"></td>
            <td class="columnaUno">
                <asp:Label ID="Label3" runat="server" Text="Giro de la Empresa:"></asp:Label>
            </td>
            <td class="columnaUno">
                <asp:TextBox ID="txtGiroEmpresa" runat="server" Enabled="False" Width="200px"></asp:TextBox>
            </td>
            <td class="columnaSeparacion"></td>
            <td class="columnaUno">
                <asp:Label ID="Label4" runat="server" Text="RPU:"></asp:Label>
            </td>
            <td class="columnaUno">
                <asp:TextBox ID="txtRpu" runat="server" Enabled="False" Width="150px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="rowSeparacion" colspan="8"></td>
        </tr>
        <tr>
            <td class="columnaUno">
                <asp:Label ID="Label2" runat="server" Text="Codigo Postal:"></asp:Label>
            </td>
            <td class="columnaUno">
                <asp:TextBox ID="txtCodPostal" runat="server" Enabled="False"></asp:TextBox>
            </td>
            <td class="columnaSeparacion"></td>
            <td class="columnaUno">
                <asp:Label ID="Label5" runat="server" Text="Estado:"></asp:Label>
            </td>
            <td class="columnaUno">
                <asp:TextBox ID="txtEstado" runat="server" Enabled="False" Width="200px"></asp:TextBox>
            </td>
            <td class="columnaSeparacion"></td>
            <td class="columnaUno"></td>
            <td class="columnaUno"></td>
        </tr>
        <tr>            
            <td class="rowSeparacion" colspan="8"></td>
        </tr>
        </table>
    <table>
        <tr>
            <td class="columnaTres">
                <asp:DropDownList ID="cboTecnologias" runat="server">
                </asp:DropDownList>
            </td>
            <td class="columnaUno">
                <asp:Button ID="btnAgregaTecnologia" runat="server" Text="Agregar Tecnologia" 
                    onclick="btnAgregaTecnologia_Click" />
            </td>
            <td class="columnaSeparacion">&nbsp;</td>
            <td class="columnaUno">&nbsp;</td>
            <td class="columnaUno">&nbsp;</td>
            <td class="columnaSeparacion">&nbsp;</td>
            <td class="columnaUno">&nbsp;</td>
            <td class="columnaUno">&nbsp;</td>
        </tr>
        <tr>
            <td class="rowSeparacion" colspan="8"></td>
        </tr>
        </table>    
</div>
<div id="tecnologias">
   <fieldset id="fielSetEB" runat="server">
       <legend>Tecnologia de Equipos de Baja Eficiencia</legend>   
        <table>
        <tr>
            <td class="rowSeparacion"></td>
        </tr>
        <tr>
            <td colspan ="6">
                <asp:GridView ID="gvEquiposBaja" runat="server" AutoGenerateColumns="False" 
                    BackColor="White" BorderColor="#006699"  DataKeyNames="IdProducto" 
                    BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
                    OnRowDeleting="gvEquiposBaja_RowDeleting" ShowHeaderWhenEmpty="True" 
                    OnRowDataBound="gvEquiposBaja_RowDataBound" 
                    ondatabound="gvEquiposBaja_DataBound">
                    <Columns>
                        <asp:BoundField DataField="IdProducto" HeaderText="IdProducto" Visible="False" />
                        <asp:TemplateField HeaderText="Tipo de Producto">                                                                                      
                            <ItemTemplate>                                
                                 <asp:DropDownList ID="cboTipoProducto"  runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboTipoProducto_SelectedIndexChanged">
                                </asp:DropDownList> 
                            </ItemTemplate>
                            <HeaderStyle Width="25%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Capacidad">                         
                            <ItemTemplate>
                                 <asp:DropDownList ID="cboCapacidad"  runat="server" >
                                </asp:DropDownList> 
                            </ItemTemplate>
                            <HeaderStyle Width="15%" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Unidad" />
                        <asp:BoundField HeaderText="Cantidad" />
                       
                        <asp:CommandField ButtonType="Button" DeleteText="Borrar" ShowDeleteButton="True" />
                       
                    </Columns>                   
                    <FooterStyle BackColor="White" ForeColor="#000066" BorderColor="#CCCCCC"/>
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <RowStyle ForeColor="#000066"/>
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td class="rowSeparacion"></td>
        </tr> 
       <tr>
            <td class="columnaUno"></td>
            <td class="columnaUno"></td>
            <td class="columnaUno"></td>
            <td class="columnaUno"></td>
            <td class="columnaUno"></td>
            <td class="columnaUno" style="text-align: right">
                <asp:Button ID="btnAgregaProduto" runat="server" OnClick="btnAgregaProduto_Click" Text="Agregar Producto" />
            </td>
        </tr>              
    </table>  
   </fieldset>
    

    <fieldset id="fielSetEA" runat="server">
        <legend>Tecnologia de Equipos de Alta Eficiencia</legend> 
               
        <asp:GridView ID="GridView1" runat="server" BackColor="White" 
            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />
        </asp:GridView>
               
    </fieldset>
    
</div>
    </form>
</body>
</html>
