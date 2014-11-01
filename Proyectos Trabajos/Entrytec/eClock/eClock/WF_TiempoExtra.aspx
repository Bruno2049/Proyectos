<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WF_TiempoExtra.aspx.cs" Inherits="WF_TiempoExtra" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register TagPrefix="uc1" TagName="WC_Menu" Src="WC_Menu.ascx" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Asignación avanzada de turno</title>
        <script id="igClientScript" type="text/javascript">
<!--
    function InitializeLayout(tableName) {
        var grid = igtbl_getGridById(tableName);
        grid.RowSizing = 2;        
        grid.SelectTypeRow = 2;
        return 0;
    }
    
    // Implements functionality for the Select Current Row Button on the page
    function SelectCurrent() {
        var grid = igtbl_getGridById("Grid");
        var row = igtbl_getActiveRow("Grid");
        if(row != null)
            igtbl_selectRow("Grid", row.Element.id, false, false);
        else {
            var cell = igtbl_getActiveCell("Grid");
            if(cell != null)
                igtbl_selectCell("Grid", cell.Element.id, false, false);
        }
        return 0;
    }

    // Implements functionality for the Edit Active Cell Button on the page
    function EditCell() {
        var grid = igtbl_getGridById("Grid");
        igtbl_EnterEditMode("Grid");
        return 0;
    }

    // Implements functionality for the Change Styles Button on the page
    function ChangeStyles() {
        var grid = igtbl_getGridById("Grid");
        return 0;
    }

    // Implements functionality for the Change Cell Data Button on the page
    function ChangeData() {
        var grid = igtbl_getGridById("Grid");
        return 0;
    }

    function AfterExitEditMode(tableName, itemName) {
        return 0;
    }

    function MouseUp(tableName, itemName) {

        return 0;
    }

    function MouseOver(tableName, itemName, type) {
        if(type == 0) { // Are we over a cell
            var cell = igtbl_getElementById(itemName);
            cell.style.cursor = 'hand';
            var label = igtbl_getElementById("Label2");
            var parts = itemName.split("_");
            if(label && parts.length>=4)
                label.innerHTML = "Current Row:" + parts[2] + " - Current Column:" + parts[3];
        }
    }
    
    // Called when the mouse pointer moves out of a cell
    function MouseOut(tableName, itemName, type) {
    }
    
    // called when a cell, row label, or column header is double clicked
    function DblClick(tableName, itemName) {
        var cell = igtbl_getCellById(itemName);
        var column = igtbl_getColumnById(itemName);
        if(column.Key != "PERSONA_D_HE_APL")
            if(cell)
                return 1;
    }
    
    // Called for each keystroke while in edit mode
    function EditKeyDown(tableName, itemName, keyStroke) {
        var column = igtbl_getColumnById(itemName);
        if(column.Key == "PostalCode") {
            if(keyStroke < 48 || keyStroke > 57) 
            {
                if(keyStroke <96 || keyStroke > 105)//needed for numeric keypad
                {
                  if(keyStroke != 13 && keyStroke != 27 && keyStroke != 46 && keyStroke != 8)
                    alert("Only Numeric Values can be entered in this column.");
                }
            }
        }
        return 0;
    }
    
    // Called after a cell, row, or column has been selected.
    function AfterSelectChange(tableName, itemName) {
        return 0;
    }
    function CellChange(tableName, itemName) {
        var cell = igtbl_getElementById(itemName);
        if (cell)
            cell.style.fontWeight = "bold";
        
        return 0;
    }
    function BeforeExitEditMode(tableName, itemName) {
    }
    
    function BeforeRowActivate(tableName, itemName) {
    // Obtain the Row object
        var row = igtbl_getRowById(itemName);
        // Obtain the Cell object for City Column 
        var cell = row.getCell(3);
        // change the cell font
        if (cell.getValue() == "London")
        {
        cell.Element.style.fontStyle = "italic";
        cell.Element.style.fontWeight = "bold";
        }
    }
    
    function BeforeRowInsert(tableName, itemName) {
        var result = confirm("Do you want to Add the new Row?");
        if(result)
            return 0;
        else
            return 1;
    }
    
    function BeforeEnterEditMode(tableName, itemName) {
    }

    var updating = false;  // variable used to prevent infinite loop from multiple CellUpdates
    function BeforeCellUpdate(tableName, itemName, newText) {
        if(updating)
            return;
        updating = true;
        // Obtain the Row object for the current cell
        var row = igtbl_getRowById(itemName);
        // Obtain the Cell object for Update Column 
        var cell = row.getCell(8);
        // Update the Cell value in Update Column 
        cell.setValue("Actualizado");
        updating = false;
        return 0;
    }
    
    function InitializeRow(tableName, rowName) {
        var row = igtbl_getElementById(rowName);
        var FirstActiveCell = igtbl_getBandFAC(tableName, row);
        for(var i = FirstActiveCell; i < row.cells.length; i++) {
            var oCell = igtbl_getCellById(row.cells[i].id);
            if (oCell.Column.Key!="HireDate" && oCell.Column.Key!="BirthDate")
                igtbl_getCellById(row.cells[i].id).setValue("Initial Value");
        }
        return 0;
    }

// -->
        </script>
    <style type="text/css">
        .style1
        {
            height: 42px;
        }
    </style>
</head>
<body style="font-size: 11px; font-family: tahoma; text-align: left; margin: 0px; vertical-align: top;">

    <form id="form1" runat="server">
        <table style="width: 620px">

				<tr>
					<td style="HEIGHT: 200px" align="center">
                        <igtbl:UltraWebGrid ID="Grid" runat="server" Browser="Xml" Height="200px" OnInitializeLayout="Grid_InitializeLayout" OnInitializeDataSource="Grid_InitializeDataSource" Width="100%">
                            <Bands>
                                <igtbl:UltraGridBand GroupByColumnsHidden="Yes" SelectTypeRow="Extended">
                                    <AddNewRow View="NotSet" Visible="NotSet">
                                    </AddNewRow>
                                </igtbl:UltraGridBand>
                            </Bands>
                            <DisplayLayout AllowColSizingDefault="Free" AllowSortingDefault="OnClient"
                                AllowUpdateDefault="Yes" BorderCollapseDefault="Separate" HeaderClickActionDefault="SortMulti"
                                LoadOnDemand="Xml" Name="Grid" RowHeightdefault="20px" RowSelectorsDefault="No"
                                SelectTypeRowDefault="Extended" StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True"
                                tableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy">
                                <GroupByBox Hidden="True">
                                    <Style BackColor="ActiveBorder" BorderColor="Window"></Style>
                                </GroupByBox>
                                <GroupByRowStyleDefault BackColor="Control" BorderColor="Window">
                                </GroupByRowStyleDefault>
                                <ActivationObject BorderColor="" BorderWidth="">
                                </ActivationObject>
                                <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                </FooterStyleDefault>
                                <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Names="Microsoft Sans Serif" Font-Size="8.25pt">
                                    <BorderDetails ColorLeft="Window" ColorTop="Window" />
                                    <Padding Left="3px" />
                                </RowStyleDefault>
                                <FilterOptionsDefault>
                                    <FilterOperandDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid"
                                        BorderWidth="1px" CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                        Font-Size="11px">
                                        <Padding Left="2px" />
                                    </FilterOperandDropDownStyle>
                                    <FilterHighlightrowStyle BackColor="#151C55" ForeColor="White">
                                    </FilterHighlightrowStyle>
                                    <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                        CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                        Font-Size="11px" Height="300px" Width="200px">
                                        <Padding Left="2px" />
                                    </FilterDropDownStyle>
                                </FilterOptionsDefault>
                                <HeaderStyleDefault BackgroundImage="images/GridTitulo.gif" BackColor="LightGray" BorderStyle="Solid" HorizontalAlign="Left">
                                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                </HeaderStyleDefault>
                                <EditCellStyleDefault BorderStyle="None" BorderWidth="0px">
                                </EditCellStyleDefault>
                                <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                    BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="200px"
                                    Width="100%">
                                </FrameStyle>
                                <Pager MinimumPagesForDisplay="2">
                                    
                                </Pager>
                                <ClientSideEvents BeforeRowInsertHandler="BeforeRowInsert" BeforeExitEditModeHandler="BeforeExitEditMode"
                                                CellChangeHandler="CellChange" InitializeLayoutHandler="InitializeLayout" MouseUpHandler="MouseUp"
                                                BeforeEnterEditModeHandler="BeforeEnterEditMode" BeforeCellUpdateHandler="BeforeCellUpdate" AfterSelectChangeHandler="AfterSelectChange"
                                                DblClickHandler="DblClick" EditKeyDownHandler="EditKeyDown" MouseOverHandler="MouseOver" InitializeRowHandler="InitializeRow"
                                                MouseOutHandler="MouseOut" AfterExitEditModeHandler="AfterExitEditMode" BeforeRowActivateHandler="BeforeRowActivate"></ClientSideEvents>
                            </DisplayLayout>
                            
                        </igtbl:UltraWebGrid></td>
				</tr>
				<tr>
					<td style="HEIGHT: 0%" align="center"><asp:label id="LError" runat="server" Font-Size="Smaller" Font-Names="Arial Narrow" ForeColor="Red"></asp:label><asp:label id="LCorrecto" runat="server" Font-Size="Smaller" Font-Names="Arial Narrow" ForeColor="Green"></asp:label></td>
				</tr>
				<tr>
					<td align="center" class="style1"><igtxt:WebImageButton ID="btn_Filtro" runat="server" Height="22px" ImageTextSpacing="4"
                            Text="Filtro" UseBrowserDefaults="False" OnClick="btn_Filtro_Click">
                        <Appearance>
                            <Image Height="16px" Url="./Imagenes/stock-convert.png" Width="16px" />
                            <Style Cursor="Default"></Style>
                        </Appearance>
                        <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                                HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400"
                                PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                        <Alignments VerticalAll="Bottom" VerticalImage="Middle" />
                    </igtxt:WebImageButton>
                        &nbsp;&nbsp;<igtxt:WebImageButton ID="btn_Asignar" runat="server" Height="22px" ImageTextSpacing="4"
                            Text="Asignar y Guardar Horas Calculadas" UseBrowserDefaults="False" OnClick="btn_Asignar_Click">
                            <Appearance>
                                <Image Height="16px" Url="./Imagenes/Assign.png" Width="16px" />
                                <Style Cursor="Default"></Style>
                            </Appearance>
                            <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                                HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400"
                                PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                            <Alignments VerticalAll="Bottom" VerticalImage="Middle" />
                        </igtxt:WebImageButton>
                        &nbsp;
                        <igtxt:WebImageButton ID="btn_Guardar" runat="server" Height="22px" ImageTextSpacing="4"
                            Text="Guardar Cambios" UseBrowserDefaults="False" OnClick="btn_Guardar_Click">
                            <Appearance>
                                <Image Height="16px" Url="./Imagenes/Save_as.png" Width="16px" />
                                <Style Cursor="Default"></Style>
                            </Appearance>
                            <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                                HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400"
                                PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                            <Alignments VerticalAll="Bottom" VerticalImage="Middle" />
                        </igtxt:WebImageButton>
					</td>
				</tr>
			</table><igtxt:WebDateTimeEdit ID="WebDateTimeEdit1" runat="server" EditModeFormat="H:mm:ss" MinimumNumberOfValidFields="1" SelectionOnFocus="CaretToBeginning">
    </igtxt:WebDateTimeEdit>
    </form>
</body>
</html>

 
