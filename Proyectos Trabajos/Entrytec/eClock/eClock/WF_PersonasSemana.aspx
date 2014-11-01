<%@ Register Assembly="Infragistics2.WebUI.WebCombo.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebCombo" TagPrefix="igcmbo" %>
<%@ Register TagPrefix="flashmovie" Namespace="Osmosis.Web.UI.Controls" Assembly="FlashMovie" %>

<%@ Page Language="c#" CodeFile="WF_PersonasSemana.aspx.cs" AutoEventWireup="True"
    Inherits="WF_PersonasSemana" %>

<%@ Register Assembly="Infragistics2.WebUI.WebDateChooser.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<%@ Register Src="WCBotonesEncabezado.ascx" TagName="WCBotonesEncabezado" TagPrefix="uc2" %>
<%@ Register Src="WC_LinksPiePagina.ascx" TagName="WC_LinksPiePagina" TagPrefix="uc3" %>
<%@ Register Src="WC_Menu.ascx" TagName="WC_Menu" TagPrefix="uc1" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebNavigator.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebNavigator" TagPrefix="ignav" %>
<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register TagPrefix="flashmovie" Namespace="Osmosis.Web.UI.Controls" Assembly="FlashMovie" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script language="javascript" type="text/javascript" src="Scripts/jquery.js"></script>

<script language="javascript" type="text/javascript" src="Scripts/jquery.simplemodal.js"></script>

<link type="text/css" href="Scripts/basic.css" rel="stylesheet" media="screen">

<script id="Script1" type="text/javascript">
function init() { 
    try
    {
    // Se cambia la llavada directa al webservice por iframe
        //service.useService("WS_PersonasSemana.asmx?WSDL","WS_PersonasSemana"); 
        /*alert("WS_PersonasSemana");*/
	}
	catch(err)
	{
	    alert(err.description);
	}	    
}

function btnAsignarTurno_MouseDown(oButton, oEvent){
	try
    {   
        var Parametros = "";
        //alert('Prueba');
        var gs=igtbl_getGridById("Grid");
        var combo=igcmbo_getComboById("TurnosCombo");
        
        for(var cell in gs.SelectedCells)
        {
        	if(typeof(cell)=="string")
		        cell=igtbl_getCellById(cell);
	        if(cell.Column.Key == "TURNO_NOMBRE" || (cell.Column.Key.length == 8 && cell.Column.Key.substr(0,7) == "TURNO_D")
	        || (cell.Column.Key.length == 13 && cell.Column.Key.substr(0,12) == "ASISTENCIA_D"))
	        {

// Se cambia la llavada directa al webservice por iframe
//                service.WS_PersonasSemana.callService("AsignaHorario",cell.Row.getCell(0).getValue(),cell.Column.Key,combo.dataValue);
                Parametros += "@" + cell.Row.getCell(0).getValue() + "-"+ cell.Column.Key  ;
                cell.setValue(combo.displayValue);
            }
        }
        self.frames[0].location = "WF_PersonasSemanaAct.aspx?Parametros=" + combo.dataValue + Parametros;
//        document.frames.Ifrm.location = "WF_PersonasSemanaAct.aspx?Parametros=" + combo.dataValue + Parametros;
	}
	catch(err)
	{
	    alert(err.description);
	}
}

function onmyresult(){ 
   service.innerHTML= "Resultado : " + event.result.value; 
} 

function modalClose (dialog) {
	dialog.data.fadeOut('slow', function () {
		dialog.container.hide('slow', function () {
			dialog.overlay.slideUp('slow', function () {
				$.modal.close();
			});
		});
	});
	document.location.href="WF_PersonasSemana.aspx";
	    
	//}Parametros = "";
}

var Parametros = "";
function btn_Justificar_MouseDown(oButton, oEvent){
        var gs=igtbl_getGridById("Grid");
        var combo=igcmbo_getComboById("TurnosCombo");
        
        for(var cell in gs.SelectedCells)
        {
        	if(typeof(cell)=="string")
		        cell=igtbl_getCellById(cell);
	        if(cell.Column.Key == "TURNO_NOMBRE" || (cell.Column.Key.length == 8 && cell.Column.Key.substr(0,7) == "TURNO_D")
	        || (cell.Column.Key.length == 13 && cell.Column.Key.substr(0,12) == "ASISTENCIA_D"))
	        {
                Parametros += "@" + cell.Row.getCell(0).getValue() + "-"+ cell.Column.Key;
            }
        }
        $.modal("<div class='test'>\
		<iframe\
		width='430px' src='WF_JustificaDias.aspx?Parametros="+Parametros+"' height='250px' id='Ifrm' frameborder='0'>\
        </iframe></div>",{onClose: modalClose});
}
// -->

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>eClock</title>

    <script id="igClientScript" type="text/javascript">
<!--

// -->
</script>

</head>
<body onload="init();" background="skins/boxed-bg.gif" style="font-size: 11px; font-family: tahoma;
    text-align: center; margin: 0px;">
    <form id="form1" runat="server" style="text-align: center">
                                        <table id="Table1" style="width: 100%; font-family: Arial;" cellspacing="1" cellpadding="1"
                                    width="300" border="0">
                                    <tr>
                                        <td align="right" style="width: 619px" valign="middle">
                                            <asp:LinkButton ID="LBAnterior" runat="server" OnClick="LBAnterior_Click"><< Semana Anterior</asp:LinkButton></td>
                                        <td align="center" valign="top" style="width: 8px">
                                            <igsch:WebDateChooser ID="WDC_PerSem" runat="server" OnValueChanged="WDC_PerSem_ValueChanged">
                                                <CalendarLayout DayNameFormat="FirstLetter" ShowFooter="False" ShowTitle="False">
                                                    <DayHeaderStyle BackColor="#7A96DF" Font-Names="Tahoma" Font-Size="9pt" ForeColor="White" />
                                                    <DayStyle BackColor="White" Font-Names="Arial" Font-Size="9pt" />
                                                    <OtherMonthDayStyle ForeColor="#ACA899" />
                                                    <SelectedDayStyle BackColor="CornflowerBlue" />
                                                    <TitleStyle BackColor="#9EBEF5" />
                                                    <TodayDayStyle BackColor="Transparent" BorderColor="CornflowerBlue" />
                                                    <WeekendDayStyle BackColor="#E0E0E0" />
                                                </CalendarLayout>
                                                <AutoPostBack ValueChanged="True" />
                                            </igsch:WebDateChooser>
                                        </td>
                                        <td align="left" style="width: 619px" valign="middle">
                                            <asp:LinkButton ID="LBSiguiente" runat="server" OnClick="LBSiguiente_Click">Siguente Semana >></asp:LinkButton></td>
                                    </tr>
                                    <tr>
                                        <td style="height: 280px" align="center" colspan="3">
                                            <igtbl:UltraWebGrid ID="Grid" runat="server" Browser="Xml" Height="386px" OnInitializeLayout="Grid_InitializeLayout" OnInitializeDataSource="Grid_InitializeDataSource"
                                                Style="left: 7px; top: -233px" Width="100%">
                                                <Bands>
                                                    <igtbl:UltraGridBand>
                                                        <AddNewRow View="NotSet" Visible="NotSet">
                                                        </AddNewRow>
                                                    </igtbl:UltraGridBand>
                                                </Bands>
                                                <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowRowNumberingDefault="Continuous"
                                                    AllowSortingDefault="OnClient" BorderCollapseDefault="Separate" CellClickActionDefault="RowSelect"
                                                    HeaderClickActionDefault="SortMulti" LoadOnDemand="Xml" Name="Grid" RowHeightDefault="20px"
                                                    RowSelectorsDefault="No" RowsRange="30" ScrollBar="Always" SelectTypeRowDefault="Extended"
                                                    StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed"
                                                    Version="4.00" ViewType="OutlookGroupBy">
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
                                                    <FilterOptionsDefault AllowRowFiltering="OnServer" FilterRowView="Top" FilterUIType="FilterRow"
                                                        ShowAllCondition="No">
                                                        <FilterOperandDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid"
                                                            BorderWidth="1px" CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                            Font-Size="11px">
                                                            <Padding Left="2px" />
                                                        </FilterOperandDropDownStyle>
                                                        <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                        </FilterHighlightRowStyle>
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
                                                        BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="286px"
                                                        Width="100%">
                                                    </FrameStyle>
                                                    <Pager MinimumPagesForDisplay="2">
                                                    </Pager>
                                                    <AddNewBox Hidden="False">
                                                    </AddNewBox>
                                                    <GroupByBox Hidden="True">
                                                    </GroupByBox>
                                                </DisplayLayout>
                                            </igtbl:UltraWebGrid></td>
                                    </tr>
                                    <tr>
                                        <td style="height: 0%" align="center" colspan="3">
                                            <asp:Label ID="LError" runat="server" Font-Size="Smaller" Font-Names="Arial Narrow"
                                                ForeColor="Red"></asp:Label><asp:Label ID="LCorrecto" runat="server" Font-Size="Smaller"
                                                    Font-Names="Arial Narrow" ForeColor="Green"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="height: 0%" align="center" colspan="3">
                                            <table style="width: 100%; height: 52px">
                                                <tr>
                                                    <td style="width: 11px; height: 5px; text-align: right">
                                                        <iframe width="100%" height="1px" id="Ifrm" frameborder="0"></iframe>
                                                    </td>
                                                    <td style="width: 42px; height: 5px">
                                                        <igtxt:WebImageButton ID="btnFiltro" runat="server" Height="21px" ImageTextSpacing="4"
                                                            OnClick="btnFiltro_Click" Text="Volver al Filtro" UseBrowserDefaults="False"
                                                            Width="142px">
                                                            <Alignments HorizontalAll="NotSet" VerticalAll="NotSet" VerticalImage="Bottom" />
                                                            <Appearance>
                                                                <Image Height="18px" Url="./Imagenes/stock-convert.png" Width="20px" />
                                                                <Style Cursor="Default"></Style>

<ButtonStyle Cursor="Default"></ButtonStyle>
                                                            </Appearance>
                                                            <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                                                                HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400"
                                                                PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                                                        </igtxt:WebImageButton>
                                                    </td>
                                                    <td style="width: 66px; height: 5px; text-align: right">
                                                        Turno&nbsp;
                                                    </td>
                                                    <td style="width: 201px; height: 5px">
                                                        <igcmbo:WebCombo ID="TurnosCombo" runat="server" BorderColor="LightGray" DataTextField="TURNO_NOMBRE"
                                                            DataValueField="TURNO_ID" Font-Names="Arial Narrow"
                                                            OnInitializeLayout="TurnosCombo_InitializeLayout" SelBackColor="17, 69, 158"
                                                            SelForeColor="" Version="3.00" Width="177px">
                                                            <Columns>
                                                                <igtbl:UltraGridColumn HeaderText="Column0">
                                                                    <header caption="Column0"></header>
                                                                </igtbl:UltraGridColumn>
                                                            </Columns>
                                                            <ExpandEffects ShadowColor="LightGray"></ExpandEffects>
                                                            <DropDownLayout DropdownWidth="300px" DropdownHeight="200px" BorderCollapse="Separate"
                                                                ColWidthDefault="150px" RowHeightDefault="20px" Version="3.00">
                                                                <FrameStyle BorderColor="Black" BorderWidth="1px" BorderStyle="Solid" Font-Size="8pt"
                                                                    ForeColor="#759AFD" Height="200px" Width="300px">
                                                                </FrameStyle>
                                                                <HeaderStyle 
                                                                    BackColor="Navy" BorderColor="Black" Font-Bold="True" Font-Names="Arial" Font-Size="X-Small"
                                                                    ForeColor="White">
                                                                    <BorderDetails WidthLeft="0px" WidthTop="0px" WidthRight="1px" WidthBottom="1px"></BorderDetails>
                                                                </HeaderStyle>
                                                                <RowAlternateStyle BackColor="WhiteSmoke" ForeColor="Black">
                                                                </RowAlternateStyle>
                                                                <RowStyle BackColor="CornflowerBlue" BorderColor="Black" ForeColor="White">
                                                                    <Padding Left="3px"></Padding>
                                                                </RowStyle>
                                                                <SelectedRowStyle 
                                                                    BackColor="Sienna" ForeColor="WhiteSmoke"></SelectedRowStyle>
                                                            </DropDownLayout>
                                                        </igcmbo:WebCombo>
                                                    </td>
                                                    <td style="width: 127px; height: 5px; text-align: left">
                                                        <igtxt:WebImageButton ID="btnAsignarTurno" runat="server" Height="22px" ImageTextSpacing="4"
                                                            Text="Asignar turno" ToolTip="Asignar turno a las fechas seleccionadas" UseBrowserDefaults="False"
                                                            AutoSubmit="False">
                                                            <Appearance>
                                                                <Image Height="16px" Url="./Imagenes/Save_as.png" Width="16px" />
                                                                <Style Cursor="Default"></Style>
                                                            </Appearance>
                                                            <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                                                                HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400"
                                                                PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                                                            <Alignments VerticalAll="Bottom" VerticalImage="Middle" />
                                                            <ClientSideEvents MouseDown="btnAsignarTurno_MouseDown" />
                                                        </igtxt:WebImageButton>
                                                    </td>
                                                    <td style="height: 5px">
                                                        <igtxt:WebImageButton ID="btn_Justificar" runat="server" AutoSubmit="False" Height="22px"
                                                            ImageTextSpacing="4" Text="Justificar" ToolTip="Justifica las fechas seleccionadas"
                                                            UseBrowserDefaults="False" Width="81px">
                                                            <Appearance>
                                                                <Image Height="16px" Url="./Imagenes/selecall.png" Width="16px" />
                                                                <Style>
<Padding Top="4px"></Padding>
</Style>

<ButtonStyle>
<Padding Top="4px"></Padding>
</ButtonStyle>
                                                            </Appearance>
                                                            <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                                                                HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400"
                                                                PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                                                            <Alignments HorizontalAll="NotSet" VerticalAll="NotSet" VerticalImage="Middle" />
                                                            <ClientSideEvents MouseDown="btn_Justificar_MouseDown" />
                                                        </igtxt:WebImageButton>
                                                    </td>
                                                    <td style="width: 33px; height: 5px">
                                                        <igtxt:WebImageButton ID="Webimagebutton1" runat="server" Height="22px" ImageTextSpacing="4"
                                                            OnClick="BGuardarCambios_Click" Text="Guardar cambios" ToolTip="Asignar turno a las fechas seleccionadas"
                                                            UseBrowserDefaults="False">
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
                                            </table>
                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;
                                        </td>
                                    </tr>
                                </table>
    </form>
</body>
</html>
