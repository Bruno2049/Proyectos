<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WF_TurnoAsignacion.aspx.cs" Inherits="WF_TurnoAsignacion" %>

<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics2.WebUI.WebCombo.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebCombo" TagPrefix="igcmbo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
</head>
<body style="font-size: 11px; font-family: tahoma; text-align: left; margin: 0px; vertical-align: top;">

    <form id="form1" runat="server">
        <table style="width: 650px">
            <tr>
                <td style="width: 716px; height: 24px">
        <igmisc:webgroupbox id="WebGroupBox1" runat="server" enableappstyling="True" height="55px"
            stylesetname="Caribbean" text="Agregar Empleado a este turno" width="710px"><Template>
<TABLE style="WIDTH: 100%; HEIGHT: 10px" id="Table1" cellSpacing=5 cellPadding=1 border=0><TBODY><TR><TD style="WIDTH: 116px" align=left>
<asp:Label ID="Label1" runat="server" 
        Text="No. de Empleado"></asp:Label>
        </TD><TD style="WIDTH: 111px" align=left>
        <igtxt:WebNumericEdit id="Wne_Persona_Link_ID" runat="server" BorderWidth="1px" BorderStyle="Solid" Font-Names="Tahoma" Font-Size="8pt" Height="21px" Width="112px" BorderColor="#7B9EBD">
        </igtxt:WebNumericEdit></TD><TD style="WIDTH: 385px" align=left>
    <asp:Label ID="Lbl_Instrucciones" runat="server" 
        Text="*deje en blanco el campo no. empleado para mostrar un filtro." 
            Visible="False"></asp:Label>
    </TD><TD align=left><P align=center><igtxt:webimagebutton id="BtnAgregar" runat="server" Height="22px" Width="150px" Text="Agregar y Guardar" UseBrowserDefaults="False" OnClick="BtnAgregar_Click">
<Appearance>
<Image Height="16px" Width="16px" Url="./Imagenes/Save_as.png"></Image>

<ButtonStyle Cursor="Default"></ButtonStyle>
</Appearance>

<RoundedCorners RenderingType="FileImages" MaxWidth="400" MaxHeight="80" ImageUrl="ig_butXP1wh.gif" HoverImageUrl="ig_butXP2wh.gif" FocusImageUrl="ig_butXP3wh.gif" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif"></RoundedCorners>

<Alignments VerticalAll="Bottom" VerticalImage="Middle"></Alignments>
</igtxt:webimagebutton></P></TD></TR></TBODY></TABLE>
</Template>
</igmisc:webgroupbox>
                </td>
            </tr>
            <tr>
                <td style="width: 716px; height: 260px">
                    <igtbl:UltraWebGrid ID="Grid" runat="server" Browser="Xml" Height="400px" OnInitializeDataSource="Grid_InitializeDataSource"
                        OnInitializeLayout="Grid_InitializeLayout" Style="left: 7px; " 
                        Width="100%">
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
                            <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="400px"
                                Width="100%">
                            </FrameStyle>
                            <Pager MinimumPagesForDisplay="2">
                            </Pager>
                            <EditCellStyleDefault BorderStyle="None" BorderWidth="0px">
                            </EditCellStyleDefault>
                            <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                            </FooterStyleDefault>
                            <HeaderStyleDefault BackColor="LightGray" BackgroundImage="images/GridTitulo.gif"
                                BorderStyle="Solid" HorizontalAlign="Left">
                                <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                            </HeaderStyleDefault>
                            <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                Font-Names="Microsoft Sans Serif" Font-Size="8.25pt">
                                <Padding Left="3px" />
                                <BorderDetails ColorLeft="Window" ColorTop="Window" />
                            </RowStyleDefault>
                            <GroupByRowStyleDefault BackColor="Control" BorderColor="Window">
                            </GroupByRowStyleDefault>
                            <AddNewBox Hidden="False">
                            </AddNewBox>
                            <ActivationObject BorderColor="" BorderWidth="">
                            </ActivationObject>
                            <FilterOptionsDefault FilterRowView="Top" FilterUIType="FilterRow" ShowAllCondition="No">
                                <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                    CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                    Font-Size="11px" Height="300px" Width="200px">
                                    <Padding Left="2px" />
                                </FilterDropDownStyle>
                                <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                </FilterHighlightRowStyle>
                                <FilterOperandDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid"
                                    BorderWidth="1px" CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                    Font-Size="11px">
                                    <Padding Left="2px" />
                                </FilterOperandDropDownStyle>
                            </FilterOptionsDefault>
                        </DisplayLayout>
                    </igtbl:UltraWebGrid></td>
            </tr>
            <tr>
                <td style="width: 716px">
        <igmisc:webgroupbox id="WebGroupBox2" runat="server" enableappstyling="True" height="55px"
            stylesetname="Caribbean" text="Mover empleados a otro turno" width="710px"><Template>
<TABLE style="WIDTH: 100%; HEIGHT: 10px" id="Table2" cellSpacing=5 cellPadding=1 border=0><TBODY><TR><TD style="WIDTH: 174px" align=left>1. Seleccione los empleados</TD><TD style="WIDTH: 485px" align=left>2. Seleccione el turno al que serán movidos</TD><TD style="WIDTH: 385px" align=left><igcmbo:WebCombo id="CbxTurnos" runat="server" BorderWidth="1px" BorderStyle="Solid" BorderColor="Silver" EnableAppStyling="True" StyleSetName="Caribbean" Version="4.00" BackColor="White" ForeColor="Black" SelBackColor="DarkBlue" SelForeColor="White"><Columns>
<igtbl:UltraGridColumn>
<Header Caption="Column0"></Header>
</igtbl:UltraGridColumn>
</Columns>

<ExpandEffects ShadowColor="LightGray"></ExpandEffects>

<DropDownLayout BorderCollapse="Separate" RowHeightDefault="20px" Version="4.00">
<FrameStyle Cursor="Default" BackColor="Silver" BorderWidth="2px" BorderStyle="Ridge" Font-Names="Verdana" Font-Size="10pt" Height="130px" Width="325px"></FrameStyle>

<HeaderStyle BackColor="LightGray" BorderStyle="Solid">
<BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px"></BorderDetails>
</HeaderStyle>

<RowStyle BackColor="White" BorderColor="Gray" BorderWidth="1px" BorderStyle="Solid">
<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
</RowStyle>

<SelectedRowStyle BackColor="DarkBlue" ForeColor="White"></SelectedRowStyle>
</DropDownLayout>
</igcmbo:WebCombo></TD><TD align=left><P align=center><igtxt:webimagebutton id="BtnMover" runat="server" Height="22px" Width="150px" Text="Mover y Guardar" UseBrowserDefaults="False" OnClick="BtnMover_Click">
<Appearance>
<Image Height="16px" Width="16px" Url="./Imagenes/Save_as.png"></Image>

<ButtonStyle Cursor="Default"></ButtonStyle>
</Appearance>

<RoundedCorners RenderingType="FileImages" MaxWidth="400" MaxHeight="80" ImageUrl="ig_butXP1wh.gif" HoverImageUrl="ig_butXP2wh.gif" FocusImageUrl="ig_butXP3wh.gif" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif"></RoundedCorners>

<Alignments VerticalAll="Bottom" VerticalImage="Middle"></Alignments>
</igtxt:webimagebutton></P></TD></TR></TBODY></TABLE>
</Template>
</igmisc:webgroupbox>
                </td>
            </tr>
        </table>
        &nbsp;
    </form>
</body>
</html>
