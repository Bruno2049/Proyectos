<%@ Page Language="C#"   AutoEventWireup="true" CodeFile="WF_CreacionTurnosExpress.aspx.cs" Inherits="WF_CreacionTurnosExpress" %>

<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>

<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>




 
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Importar Empleados</title>
        <script id="igClientScript" type="text/javascript">
<!--
function delayedAlert()
{
  timeoutID = window.setTimeout(Refresca, 500);
}

function Refresca()
{
  document.getElementById('<%=BGuardarCambios.ClientID%>').click()
}


function WebImageButton1_MouseDown(oButton, oEvent){
    try
    {
        delayedAlert();
    }
	catch(err)
	{
	    alert(err.description);
	}	
		//window.location.href = "WF_CreacionTurnosExpress.aspx?Parametros=GUARDAR";
}
// -->
</script>
    <style type="text/css">
        .style1
        {
            height: 33px;
        }
        .style2
        {
            height: 4px;
            width: 148px;
        }
        .style3
        {
            height: 27px;
            width: 148px;
        }
        .style4
        {
            height: 4px;
            width: 373px;
        }
        .style5
        {
            height: 27px;
            width: 373px;
        }
    </style>
</head>
<body style="font-size: 0.8em; font-family: tahoma; text-align: center; margin: 0px; ">
    <form id="form1" runat="server" >
    <div>    <table width="100%">
    
            <tr>
                <td align="center" style="height: 207px">
                    <table style="width: 100%; height: 100%;">
                        <tr>
                            <td style="text-align: center; " width="100%">
                                <igmisc:webpanel id="Webpanel1" runat="server" Width="488px" 
                                    EnableAppStyling="True" StyleSetName="Caribbean">
                                    
                                    <Header TextAlignment="Left" Text="&lt;SPAN style=&quot;COLOR: white&quot;&gt;Datos Generales&lt;/SPAN&gt;">
                                       
                                    </Header>
                                    <Template>
                                        <table style="height: 100%">
                                            <tr>
                                                <td class="style4">
                                                    <span style="font-size: 0.8em">
                                                    Minutos Retardo de Tolerancia en Entrada y Comida</span></td>
                                                <td class="style2">
                                                    <igtxt:WebNumericEdit ID="txtTolerancia" runat="server" DataMode="Int" MaxValue="999"
                                                        MinValue="0" NullText="0" Width="64px">
                                                    </igtxt:WebNumericEdit>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style5">
                                                    <span style="font-size: 0.8em">Minutos de Comida</span><igtxt:WebDateTimeEdit ID="txthora" runat="server" EditModeFormat="HH:mm" DisplayModeFormat="HH:mm">
                                </igtxt:WebDateTimeEdit>
                                                    <span style="font-size: 0.8em"></span>
                    </td>
                                                <td class="style3">
                                                    <igtxt:WebNumericEdit ID="txtComida" runat="server" DataMode="Int" MaxValue="999"
                                                        MinValue="0" NullText="0" Width="64px">
                                                    </igtxt:WebNumericEdit>
                                                </td>
                                            </tr>
                                        </table>
                                    </Template>
                                </igmisc:WebPanel>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td height="270">
                                <igtbl:ultrawebgrid id="UWGTurnos" runat="server" style="left: 0px; top: 0px"
                        width="100%" OnInitializeDataSource = "UWGTurnos_InitializeDataSource" OnInitializeLayout="UWGTurnos_InitializeLayout"><Bands>
<igtbl:UltraGridBand>
<AddNewRow View="NotSet" Visible="NotSet"></AddNewRow>
</igtbl:UltraGridBand>
</Bands>

<DisplayLayout ViewType="OutlookGroupBy" Version="4.00" AllowSortingDefault="OnClient" StationaryMargins="Header" AllowColSizingDefault="Free" AllowUpdateDefault="Yes" StationaryMarginsOutlookGroupBy="True" HeaderClickActionDefault="SortMulti" Name="UWGTurnos" BorderCollapseDefault="Separate" RowSelectorsDefault="No" TableLayout="Fixed" RowHeightDefault="20px" AllowColumnMovingDefault="OnServer" SelectTypeRowDefault="Extended" AllowAddNewDefault="Yes">
<GroupByBox>
<Style BorderColor="Window" BackColor="ActiveBorder"></Style>
</GroupByBox>

<GroupByRowStyleDefault BorderColor="Window" BackColor="Control"></GroupByRowStyleDefault>

<ActivationObject BorderWidth="" BorderColor=""></ActivationObject>

<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
</FooterStyleDefault>

<RowStyleDefault BorderWidth="1px" BorderColor="Silver" BorderStyle="Solid" Font-Size="8.25pt" Font-Names="Microsoft Sans Serif" BackColor="Window">
<BorderDetails ColorTop="Window" ColorLeft="Window"></BorderDetails>

<Padding Left="3px"></Padding>
</RowStyleDefault>

<FilterOptionsDefault>
<FilterOperandDropDownStyle BorderWidth="1px" BorderColor="Silver" BorderStyle="Solid" Font-Size="11px" Font-Names="Verdana,Arial,Helvetica,sans-serif" BackColor="White" CustomRules="overflow:auto;">
<Padding Left="2px"></Padding>
</FilterOperandDropDownStyle>

<FilterHighlightRowStyle ForeColor="White" BackColor="#151C55"></FilterHighlightRowStyle>

<FilterDropDownStyle BorderWidth="1px" BorderColor="Silver" BorderStyle="Solid" Font-Size="11px" Font-Names="Verdana,Arial,Helvetica,sans-serif" BackColor="White" Width="200px" Height="300px" CustomRules="overflow:auto;">
<Padding Left="2px"></Padding>
</FilterDropDownStyle>
</FilterOptionsDefault>

<HeaderStyleDefault BackgroundImage="images/GridTitulo.gif" HorizontalAlign="Left" BorderStyle="Solid" BackColor="LightGray">
<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
</HeaderStyleDefault>

<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>

<FrameStyle BorderWidth="1px" BorderColor="InactiveCaption" BorderStyle="Solid" Font-Size="8.25pt" Font-Names="Microsoft Sans Serif" BackColor="Window" Width="100%"></FrameStyle>

<Pager MinimumPagesForDisplay="2">
<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
</Style>
</Pager>

<AddNewBox Prompt="Agrega Turno" View="Compact">
<Style BorderWidth="1px" BorderColor="InactiveCaption" BorderStyle="Solid" BackColor="Window">
<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
</Style>
</AddNewBox>
    <AddNewRowDefault Visible="Yes">
        <Style VerticalAlign="Top"></Style>
    </AddNewRowDefault>
</DisplayLayout>
</igtbl:ultrawebgrid></td>
            </tr>
                        <tr>
                            <td class="style1">
                    <igtxt:WebImageButton ID="btnGuardarCambios" runat="server" Height="22px"
                        Text="Guardar Cambios" UseBrowserDefaults="False" Width="150px" ImageTextSpacing="4" AutoSubmit="False" OnClick="BGuardarCambios_Click">
                        <Appearance>
                            <Image Url="./Imagenes/Save_as.png" Height="16px" Width="16px" />
                            <Style Cursor="Default"></Style>
                        </Appearance>
                        <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                            HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400"
                            PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                        <ClientSideEvents MouseDown="WebImageButton1_MouseDown" />
                        <Alignments VerticalImage="Middle" VerticalAll="Bottom" />
                    </igtxt:WebImageButton>
                    <igtxt:WebImageButton ID="BGuardarCambios" runat="server" Height="22px" OnClick="BGuardarCambios_Click" UseBrowserDefaults="False" Width="1px" ImageTextSpacing="0" ClickOnEnterKey="False" ClickOnSpaceKey="False">
                        <Alignments VerticalImage="Middle" VerticalAll="Bottom" />
                        <Appearance>
                            <Style Cursor="Default">
								</Style>
                            <Image Height="16px" Width="16px" />
                        </Appearance>
                        <DisabledAppearance>
                            <Style Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False"></Style>
                        </DisabledAppearance>
                        <FocusAppearance>
                            <Style BackColor="#DEEAF3" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                Font-Strikeout="False" Font-Underline="False"></Style>
                            <InnerBorder ColorBottom="0, 37, 108, 180" ColorLeft="0, 37, 108, 180" ColorRight="0, 37, 108, 180"
                                ColorTop="0, 37, 108, 180" StyleBottom="Solid" StyleLeft="Solid" StyleRight="Solid"
                                StyleTop="Solid" WidthBottom="1px" WidthLeft="1px" WidthRight="1px" WidthTop="1px" />
                        </FocusAppearance>
                        <PressedAppearance ContentShift="DownRight">
                            <Style BackColor="#92B8E7" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                Font-Strikeout="False" Font-Underline="False"></Style>
                            <InnerBorder ColorBottom="0, 37, 108, 180" ColorLeft="0, 37, 108, 180" ColorRight="0, 37, 108, 180"
                                ColorTop="0, 37, 108, 180" StyleBottom="Solid" StyleLeft="Solid" StyleRight="Solid"
                                StyleTop="Solid" WidthBottom="1px" WidthLeft="1px" WidthRight="1px" WidthTop="1px" />
                        </PressedAppearance>
                        <HoverAppearance ContentShift="UpLeft">
                            <Style BackColor="#BCD7F1" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                Font-Strikeout="False" Font-Underline="False"></Style>
                            <InnerBorder ColorBottom="0, 37, 108, 180" ColorLeft="0, 37, 108, 180" ColorRight="0, 37, 108, 180"
                                ColorTop="0, 37, 108, 180" StyleBottom="Solid" StyleLeft="Solid" StyleRight="Solid"
                                StyleTop="Solid" WidthBottom="1px" WidthLeft="1px" WidthRight="1px" WidthTop="1px" />
                        </HoverAppearance>
                    </igtxt:WebImageButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>