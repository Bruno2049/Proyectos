<%@ Register TagPrefix="flashmovie" Namespace="Osmosis.Web.UI.Controls" Assembly="FlashMovie" %>
<%@ Page language="c#" CodeFile="WF_EmpleadosN.aspx.cs" AutoEventWireup="True" Inherits="WF_EmpleadosN" %>

<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1" Namespace="Infragistics.WebUI.Misc"
    TagPrefix="igmisc" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebNavigator.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebNavigator" TagPrefix="ignav" %>
<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register TagPrefix="flashmovie" Namespace="Osmosis.Web.UI.Controls" Assembly="FlashMovie" %>

<%@ Register assembly="Infragistics2.WebUI.WebCombo.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.WebUI.WebCombo" tagprefix="igcmbo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
  <script id="igClientScript" type="text/javascript">

function WIBtn_Borrar_MouseDown(oButton, oEvent) {
    try{
        var row = igtbl_getActiveRow("Uwg_EC_PERSONAS");
	    window.location.href = "WF_EmpleadosN.aspx?Parametros=B"+row.getCell(0).getValue();
    }
	catch (err) {
	    if (row == null)
	        alert("No se ha seleccionado ninguna fila: ");
	    else
	        alert(err.description);
	}	
}
function WIBtn_Enrolar_MouseDown(oButton, oEvent) {
	try
    {
        var row = igtbl_getActiveRow("Uwg_EC_PERSONAS");
        window.location.href = 'WF_EmpleadosN.aspx?Parametros=R'+row.getCell(0).getValue();
	}
	catch(err)
	{
	    if (row == null)
	        alert("No se ha seleccionado ninguna fila: ");
	    else
	        alert(err.description);
	}	
}
function WIBtn_Terminales_MouseDown(oButton, oEvent) {
	try
    {
        var row = igtbl_getActiveRow("Uwg_EC_PERSONAS");
        window.location.href = 'WF_EmpleadosN.aspx?Parametros=T'+row.getCell(0).getValue();
	}
	catch(err) {
	    if (row == null)
	        alert("No se ha seleccionado ninguna fila: ");
	    else
	        alert(err.description);
	}	
}
function WIBtn_Editar_MouseDown(oButton, oEvent) {
	//Add code to handle your event here.
	try
    {
        var row = igtbl_getActiveRow("Uwg_EC_PERSONAS");
    //    alert("Editando el empleado :" + row.getCell(1).getValue());
        window.location.href = 'WF_EmpleadosN.aspx?Parametros=E'+row.getCell(0).getValue();
	}
	catch(err)
	{
	    if (row == null)
	        alert("Seleccione un empleado para editarlo.");
        else
	        alert(err.description);
	}
}
// -->

</script>

<html xmlns="http://www.w3.org/1999/xhtml"  >
<head id="Head1" runat="server">
    <title>eClock</title>
    <style type="text/css">
        .style1
        {
            width: 700px;
            height: 100%;
        }
        .style2
        {
        }
        .style4
        {
            width: 182px;
        }
        .style5
        {
            width: 182px;
            height: 250px;
            text-align: center;
        }
        .style7
        {
            width: 88px;
        }
        .style8
        {
            width: 72px;
        }
        .style10
        {
            width: 67px;
        }
        .style11
        {
            width: 69px;
        }
        .style12
        {
            width: 71px;
        }
        .style13
        {
            width: 193px;
        }
    </style>
</head>
<body style="font-size: 11px; font-family: tahoma; text-align: center; margin: 0px; ">
    <form id="form1" runat="server" style="text-align: center">
                                            
    <table id="Table1" border="0" cellpadding="1" cellspacing="1" 
        style="font-family: Arial; " class="style1">
            <tr>
                <td align="right" class="style4" colspan="11">
                    <asp:CheckBox ID="Chb_PERSONA_BORRADO" runat="server" AutoPostBack="True" Font-Names="Arial Narrow"
                        Font-Size="Small" OnCheckedChanged="Chb_PERSONA_BORRADO_CheckedChanged" 
                        Text="Ver Empleados Borrados" 
                        style="font-family: Arial, Helvetica, sans-serif" />
                </td>
            </tr>
            <tr style="font-family: Arial">
                <td align="right" class="style5" colspan="11">
                    <igtbl:UltraWebGrid ID="Uwg_EC_PERSONAS" runat="server" Browser="Xml" OnInitializeLayout="Uwg_EC_PERSONAS_InitializeLayout"
                        OnInitializeDataSource="Uwg_EC_PERSONAS_DataSource" Height="270px" 
                        Width="379%" Style="top: -233px;
                        left: 7px;">
                        <Bands>
                            <igtbl:UltraGridBand>
                                <AddNewRow View="NotSet" Visible="NotSet">
                                </AddNewRow>
                            </igtbl:UltraGridBand>
                        </Bands><DisplayLayout ViewType="OutlookGroupBy" Version="4.00" AllowSortingDefault="OnClient"
                            StationaryMargins="Header" AllowColSizingDefault="Free" LoadOnDemand="Xml" StationaryMarginsOutlookGroupBy="True"
                            CellClickActionDefault="RowSelect" HeaderClickActionDefault="SortMulti" Name="Uwg_EC_PERSONAS"
                            BorderCollapseDefault="Separate" RowsRange="30" RowSelectorsDefault="No" TableLayout="Fixed"
                            AllowRowNumberingDefault="Continuous" RowHeightDefault="20px" AllowColumnMovingDefault="OnServer"
                            SelectTypeRowDefault="Extended" ScrollBar="Always">
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
                                Font-Names="Microsoft Sans Serif" BackColor="Window" Width="100%" Height="270px">
                            </FrameStyle>
                            <Pager MinimumPagesForDisplay="2">
                            </Pager>
                            <AddNewBox Hidden="False">
                            </AddNewBox>
                        </DisplayLayout>
                    </igtbl:UltraWebGrid></td>
            </tr>
            <tr style="font-family: Arial">
                <td align="center" class="style4" colspan="11">
                    &nbsp;<asp:Label 
                        ID="Lbl_Correcto" runat="server" Font-Names="Arial Narrow" Font-Size="Small"
                        ForeColor="Green" Font-Bold="True"></asp:Label>
                    <asp:Label ID="Lbl_Error" runat="server" Font-Names="Arial Narrow" Font-Size="Small"
                        ForeColor="Red" Font-Bold="True"></asp:Label><br />
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;
                    &nbsp;&nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp;
                    </td>
            </tr>
            <tr>
                    <td class="style6" colspan="3">
                        <asp:LinkButton ID="LnkBtn_Descarga" runat="server" 
                            onclick="LnkBtn_Descarga_Click" Visible="False">Descarga</asp:LinkButton>
                    </td>
                </tr>
            <tr style="font-family: Arial">
                <td align="center" class="style4">
                    
                    <igmisc:WebGroupBox ID="WGB_ActualizarTurnos" runat="server" BorderColor="Silver" 
                        BorderStyle="Double" Text="Actualización Turnos a Nómina" Width="160px" Visible="False">
                    <Template>
                    <table>
                    <tr>
                    <td>
                    <igcmbo:WebCombo ID="Wco_EC_PERSONAS_DATOS_TIPO_NOMINA" runat="server" 
                        BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" 
                        ForeColor="Black" SelBackColor="DarkBlue" SelForeColor="White" Version="4.00" 
                        Visible="False" Height="24px" Width="100px">
                        <expandeffects shadowcolor="LightGray" />
                        <Columns>
                            <igtbl:UltraGridColumn AllowResize="Fixed" Width="200px">
                                <header caption="Column0">
                                </header>
                            </igtbl:UltraGridColumn>
                        </Columns>
                        <dropdownlayout bordercollapse="Separate" rowheightdefault="20px" 
                            version="4.00">
                            <framestyle backcolor="Silver" borderstyle="Ridge" borderwidth="2px" 
                                cursor="Default" font-names="Verdana" font-size="10pt" height="130px" 
                                width="200px">
                            </framestyle>
                            <HeaderStyle BackColor="LightGray" BorderStyle="Solid">
                            <borderdetails colorleft="White" colortop="White" widthleft="1px" 
                                widthtop="1px" />
                            </HeaderStyle>
                            <RowStyle BackColor="White" BorderColor="Gray" BorderStyle="Solid" 
                                BorderWidth="1px">
                            <borderdetails widthleft="0px" widthtop="0px" />
                            </RowStyle>
                            <SelectedRowStyle BackColor="DarkBlue" ForeColor="White" />
                        </dropdownlayout>
                    </igcmbo:WebCombo>
                    </td>
                    <td>
                    <igtxt:WebImageButton ID="WIBtn_ActualizarTurnos" runat="server" Height="24px" 
                        ImageTextSpacing="4" OnClick="WIBtn_ActualizarTurnos_Click" 
                        ToolTip="Actualiza el listado de Turnos en el Sistema de Nómina" 
                        UseBrowserDefaults="False" Visible="False" Width="30px">
                        <Appearance>
                            <Image Height="16px" Url="./Imagenes/Horarios.png" Width="16px" />
                        </Appearance>
                        <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" 
                            FocusImageUrl="ig_butXP3wh.gif" HoverImageUrl="ig_butXP2wh.gif" 
                            ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400" 
                            PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                        <Alignments HorizontalAll="NotSet" VerticalAll="NotSet" 
                            VerticalImage="Middle" />
                    </igtxt:WebImageButton>
                    </td>
                    </tr>
                    </table>
                    </Template>
                    </igmisc:WebGroupBox>
                    </td>
                <td align="center" class="style4">
                    <igtxt:WebImageButton ID="WIBtn_Terminales" runat="server" Height="24px" UseBrowserDefaults="False" 
                        Width="30px" ImageTextSpacing="4" 
                        ToolTip="Selecciona las terminales por las que podrá checar el empleado" 
                        AutoSubmit="False">
                        <Appearance>
                            <Image Url="./Imagenes/selecall.png" Height="16px" Width="16px" />
                            <Style>
<Padding Top="4px"></Padding>
</Style>
                        </Appearance>
                        <RoundedCorners HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif"
                            MaxHeight="80" MaxWidth="400" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif" />
                        <ClientSideEvents MouseDown="WIBtn_Terminales_MouseDown" />
                        <Alignments VerticalImage="Middle" HorizontalAll="NotSet" VerticalAll="NotSet" />
                    </igtxt:WebImageButton>
                </td>
                <td align="center" class="style4">
                    <igtxt:WebImageButton ID="WIBtn_Enrolar" runat="server" Height="24px" UseBrowserDefaults="False" 
                        Width="30px" ImageTextSpacing="4" 
                        ToolTip="Enrola la huella o palma del empleado" AutoSubmit="False">
                        <Appearance>
                            <Image Url="./Imagenes/Roll.png" Height="16px" Width="16px" />
                            <Style>
<Padding Top="4px"></Padding>
</Style>
                        </Appearance>
                        <RoundedCorners HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif"
                            MaxHeight="80" MaxWidth="400" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif" />
                        <ClientSideEvents MouseDown="WIBtn_Enrolar_MouseDown" />
                        <Alignments VerticalImage="Middle" HorizontalAll="NotSet" VerticalAll="NotSet" />
                    </igtxt:WebImageButton>
                </td>
                <td align="center" class="style4">
                    <igtxt:WebImageButton ID="WIBtn_Actualizar" runat="server" Height="24px" UseBrowserDefaults="False" 
                        Width="30px" ImageTextSpacing="4" OnClick="WIBtn_Actualizar_Click" 
                        
                        ToolTip="Actualiza el listado de emplados con respecto al Sistema de Nómina">
                        <Appearance>
                            <Image Url="./Imagenes/gtk-refresh.png" Height="16px" Width="16px" />
                            <Style>
                            <Padding Top="4px"></Padding>
                            </Style>
                        </Appearance>
                        <RoundedCorners HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif"
                            MaxHeight="80" MaxWidth="400" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" 
                            DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif" />
                        <Alignments VerticalImage="Middle" HorizontalAll="NotSet" VerticalAll="NotSet" />
                        
                    </igtxt:WebImageButton>
                </td>
                <td align="center" class="style13">
                    <igtxt:WebImageButton ID="WIBtn_Borrar" runat="server" Height="24px" 
                        Text="Borrar" UseBrowserDefaults="False" 
                        Width="100px" ImageTextSpacing="4">
                        <ClientSideEvents MouseDown="WIBtn_Borrar_MouseDown" /><Appearance>
                            <Image Url="./Imagenes/Delete.png" Height="16px" Width="16px" />
                            <Style>
<Padding Top="4px"></Padding>
</Style>

<ButtonStyle>
<Padding Top="4px"></Padding>
</ButtonStyle>
                        </Appearance>
                        <RoundedCorners HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif"
                            MaxHeight="80" MaxWidth="400" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif" />
                        <Alignments VerticalImage="Middle" />
                        
                    </igtxt:WebImageButton>
                </td>
                <td align="center" class="style4">
                    <igtxt:WebImageButton ID="WIBtn_Nuevo" runat="server" Height="24px" 
                        Text="Nuevo" UseBrowserDefaults="False"
                        Width="100px" OnClick="WIBtn_Nuevo_Click" ImageTextSpacing="4">
                        <Alignments VerticalImage="Middle" />
                        <RoundedCorners HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif"
                            MaxHeight="80" MaxWidth="400" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif" />
                        <Appearance>
                            <Image Url="./Imagenes/New.png" Height="16px" Width="16px" />
                            <Style>
<Padding Top="4px"></Padding>
</Style>

<ButtonStyle>
<Padding Top="4px"></Padding>
</ButtonStyle>
                        </Appearance>
                    </igtxt:WebImageButton>
                </td>
                <td align="center" class="style4">
                    <igtxt:WebImageButton ID="WIBtn_Editar" runat="server" Height="24px" 
                        Text="Editar" UseBrowserDefaults="False"
                        Width="100px" AutoSubmit="False" ImageTextSpacing="4">
                        <RoundedCorners HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif"
                            MaxHeight="80" MaxWidth="400" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif" />
                        <Appearance>
                            <Image Url="./Imagenes/Edit.png" Height="16px" Width="16px" />
                            <Style>
<Padding Top="4px"></Padding>
</Style>

<ButtonStyle>
<Padding Top="4px"></Padding>
</ButtonStyle>
                        </Appearance>
                        <ClientSideEvents MouseDown="WIBtn_Editar_MouseDown" />
                        <Alignments VerticalImage="Middle" />
                    </igtxt:WebImageButton>
                </td>
                <td align="center" class="style4">
                    &nbsp;</td>
                <td align="center" class="style4">
                    &nbsp;</td>
                <td align="center" class="style4">
                    &nbsp;</td>
                <td align="center" class="style4">
                    &nbsp;</td>
            </tr>
            </table>
    </form>
</body>
</html>