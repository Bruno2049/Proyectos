<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WF_Suscripciones.aspx.cs" Inherits="WF_Suscripciones" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebNavigator.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebNavigator" TagPrefix="ignav" %>
<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register TagPrefix="flashmovie" Namespace="Osmosis.Web.UI.Controls" Assembly="FlashMovie" %>

<!DOCTYPE html>
  <script id="igClientScript" type="text/javascript">

//function BBorrar_MouseDown(oButton, oEvent) {
//    try {
//        var row = igtbl_getActiveRow("Grid");

//        window.location.href = "WF_Suscripciones.aspx?Parametros=B" + row.getCell(0).getValue();
//    }
//    catch (err) {
//        alert(err.Description);
//    }
//}

//function BEditar_MouseDown(oButton, oEvent){
//	//Add code to handle your event here.
//	try
// {   
//	    var row = igtbl_getActiveRow("Grid");
////    alert("Editando el empleado :" + row.getCell(1).getValue());
//    window.location.href = 'WF_Suscripciones.aspx?Parametros=E'+row.getCell(0).getValue();

//	}
//	catch(err)
//	{
//	alert(err.description);
//	}
//	
//}
// -->

</script>

<html>
<head id="Head1" runat="server">
    <title>eClock</title>
    <style type="text/css">

        .style1
        {
            width: 708px;
        }
        .style5
        {
            text-align: center;
        }
        .style6
        {
            width: 236px;
        }

        .style7
        {
            width: 236px;
            text-align: center;
        }
        .style8
        {
            text-align: center;
        }

    </style>
</head>
<body style="font-size: small; font-family: 'Segoe UI'; text-align: center; margin: 0px;">
    <form id="form1" runat="server" style="text-align: center">
                                            
    <table class="style1">
        <tr>
            <td class="style5">
                &nbsp;</td>
            <td class="style5">
                &nbsp;</td>
            <td class="style6">
                <asp:CheckBox ID="Chb_EmpleadosBorrados" runat="server" AutoPostBack="True" Font-Names="Segoe UI"
                    Font-Size="Small" OnCheckedChanged="CBEmpBorrados_CheckedChanged" 
                    Text="Ver Empleados Borrados" />
            </td>
        </tr>
        <tr>
            <td class="style5" colspan="3" height="100%">
                <igtbl:UltraWebGrid ID="Grid" runat="server" Browser="Xml" OnInitializeLayout="Grid_InitializeLayout"
                    OnInitializeDataSource="Grid_DataSource" Height="100%">
                    <Bands>
                        <igtbl:UltraGridBand RowSizing="Fixed">
                            <AddNewRow View="NotSet" Visible="NotSet">
                            </AddNewRow>
                        </igtbl:UltraGridBand>
                    </Bands>
                    <DisplayLayout ViewType="OutlookGroupBy" Version="4.00" AllowSortingDefault="OnClient"
                        StationaryMargins="Header" LoadOnDemand="Xml" StationaryMarginsOutlookGroupBy="True"
                        CellClickActionDefault="RowSelect" HeaderClickActionDefault="SortMulti" 
                        Name="Grid" RowsRange="30" RowSelectorsDefault="No" TableLayout="Auto"
                        AllowRowNumberingDefault="Continuous" RowHeightDefault="20px" AllowColumnMovingDefault="OnServer"
                        SelectTypeRowDefault="Extended" ScrollBar="Always" 
                        AllowColSizingDefault="Free" >
                        <GroupByRowStyleDefault BorderColor="Window" BackColor="Control">
                        </GroupByRowStyleDefault>
                        <ActivationObject BorderWidth="" BorderColor="">
                        </ActivationObject>
                        <FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
                            <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
                        </FooterStyleDefault>
                        <RowStyleDefault BorderWidth="1px" BorderColor="Silver" BorderStyle="Solid" Font-Size="8.25pt"
                            Font-Names="Microsoft Sans Serif" BackColor="Window">
                            <BorderDetails ColorTop="Window" ColorLeft="Window"></BorderDetails>
                            <Padding Left="3px"></Padding>
                        </RowStyleDefault>
                        <RowExpAreaStyleDefault HorizontalAlign="Justify">
                        </RowExpAreaStyleDefault>
                        <FilterOptionsDefault AllowRowFiltering="OnServer" ShowAllCondition="No" FilterRowView="Top"
                            FilterUIType="FilterRow">
                            <FilterOperandDropDownStyle BorderWidth="1px" BorderColor="Silver" BorderStyle="Solid"
                                Font-Size="11px" Font-Names="Verdana,Arial,Helvetica,sans-serif" BackColor="White"
                                CustomRules="overflow:auto;">
                                <Padding Left="2px"></Padding>
                            </FilterOperandDropDownStyle>
                            <FilterHighlightRowStyle ForeColor="White" BackColor="#151C55">
                            </FilterHighlightRowStyle>
                            <FilterDropDownStyle BorderWidth="1px" BorderColor="Silver" BorderStyle="Solid" Font-Size="11px"
                                Font-Names="Verdana,Arial,Helvetica,sans-serif" BackColor="White" Width="200px"
                                Height="300px" CustomRules="overflow:auto;">
                                <Padding Left="2px"></Padding>
                            </FilterDropDownStyle>
                        </FilterOptionsDefault>
                        <HeaderStyleDefault BackgroundImage="images/GridTitulo.gif" HorizontalAlign="Left" BorderStyle="Solid" BackColor="LightGray">
                            <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
                        </HeaderStyleDefault>
                        <EditCellStyleDefault BorderWidth="0px" BorderStyle="None">
                        </EditCellStyleDefault>
                        <FrameStyle BorderWidth="1px" BorderColor="InactiveCaption" BorderStyle="Solid" Font-Size="8.25pt"
                            Font-Names="Microsoft Sans Serif" BackColor="Window" Height="100%">
                        </FrameStyle>
                        <Pager MinimumPagesForDisplay="2">
                        </Pager>
                        <AddNewBox Hidden="False">
                        </AddNewBox>
                    </DisplayLayout>
                </igtbl:UltraWebGrid>
            </td>
        </tr>
        <tr>
            <td class="style8">
                &nbsp;</td>
            <td class="style8">
                <asp:Label ID="Lbl_Correcto" runat="server" 
                    Font-Names="Segoe UI" Font-Size="Smaller"
                    ForeColor="Green"></asp:Label>
                <asp:Label ID="Lbl_Error" runat="server" Font-Names="Segoe UI" Font-Size="Smaller"
                    ForeColor="Red"></asp:Label>
            </td>
            <td class="style7">
                &nbsp;</td>
        </tr>
        <tr class">
            <td class="style5" >
                <igtxt:WebImageButton ID="WIBtn_Borrar" runat="server" Height="24px" 
                    Text="Borrar" UseBrowserDefaults="False" 
                    Width="100px" ImageTextSpacing="4" onclick="WIBtn_Borrar_Click">
                    <Appearance>
                        <Image Url="./Imagenes/Delete.png" Height="16px" Width="16px" />
                    </Appearance>
                    <RoundedCorners HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif"
                        MaxHeight="80" MaxWidth="400" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif" />
                    <Alignments VerticalImage="Middle" /> 
                </igtxt:WebImageButton>
            </td>
            <td class="style5">
                <igtxt:WebImageButton ID="WIBtn_Nuevo" runat="server" Height="24px" 
                    Text="Nuevo" UseBrowserDefaults="False"
                    Width="100px" OnClick="WIBtn_Nuevo_Click" ImageTextSpacing="4">
                    <Alignments VerticalImage="Middle" />
                    <RoundedCorners HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif"
                        MaxHeight="80" MaxWidth="400" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif" />
                    <Appearance>
                        <Image Url="./Imagenes/New.png" Height="16px" Width="16px" />
                    </Appearance>
                </igtxt:WebImageButton>
            </td>
            <td class="style7">
                <igtxt:WebImageButton ID="WIBtn_Editar" runat="server" Height="24px" 
                    Text="Editar" UseBrowserDefaults="False"
                    Width="100px" ImageTextSpacing="4" 
                    onclick="WIBtn_Editar_Click">
                    <RoundedCorners HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif"
                        MaxHeight="80" MaxWidth="400" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif" />
                    <Appearance>
                        <Image Url="./Imagenes/Edit.png" Height="16px" Width="16px" />
                    </Appearance>
                    <Alignments VerticalImage="Middle" />
                </igtxt:WebImageButton>
            </td>
        </tr>
    </table>
    </form>
</body>

</html>
