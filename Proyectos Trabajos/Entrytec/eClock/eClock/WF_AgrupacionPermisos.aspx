<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WF_AgrupacionPermisos.aspx.cs" Inherits="WF_AgrupacionPermisos" %>

<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics2.WebUI.WebCombo.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebCombo" TagPrefix="igcmbo" %>

<%@ Register Assembly="Infragistics2.WebUI.WebCombo.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebCombo" TagPrefix="igcmbo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Página sin título</title>
    <style type="text/css">
        .style1
        {
            width: 321px;
        }
        .style2
        {
            width: 716px;
            height: 41px;
            text-align: center;
        }
        .style3
        {
            width: 401px;
            text-align: right;
        }
        .style4
        {
            width: 215px;
        }
        .style5
        {
            width: 716px;
            height: 16px;
            text-align: center;
        }
    </style>
</head>
<body style="font-size: 11px; font-family: tahoma; text-align: left; margin: 0px; vertical-align: top;">

    <form id="form1" runat="server">
        <table style="width: 706px">
            <tr>
                <td style="width: 716px; height: 24px">
        <igmisc:webgroupbox id="WebGroupBox1" runat="server" enableappstyling="True" height="55px"
            stylesetname="Caribbean" text="Agregar Permiso" width="700px"><Template>
<TABLE style="WIDTH: 100%; HEIGHT: 10px" id="Table1" cellSpacing=5 cellPadding=1 border=0><TBODY><TR>
    <TD align=left class="style1">
    Usuario o eMail</TD><TD style="WIDTH: 111px" align=left>
        <igtxt:WebTextEdit ID="Tbx_Usuario" runat="server" BorderColor="#7F9DB9" 
            BorderStyle="Solid" BorderWidth="1px" CellSpacing="1" 
            UseBrowserDefaults="False" Width="152px">
            <buttonsappearance custombuttondefaulttriangleimages="Arrow">
                <buttonstyle backcolor="#C5D5FC" bordercolor="#ABC1F4" borderstyle="Solid" 
                    borderwidth="1px" forecolor="#506080" width="13px">
                </buttonstyle>
                <buttonhoverstyle backcolor="#DCEDFD" bordercolor="#8C9762">
                </buttonhoverstyle>
                <buttonpressedstyle backcolor="#83A6F4" bordercolor="#75805E">
                </buttonpressedstyle>
                <buttondisabledstyle backcolor="#E1E1DD" bordercolor="#D7D7D7" 
                    forecolor="#BEBEBE">
                </buttondisabledstyle>
            </buttonsappearance>
        </igtxt:WebTextEdit>
    </TD><TD align=left class="style3">Tipo de permiso</TD>
    <td align="left" class="style4">

        <igcmbo:WebCombo ID="Cmb_Permiso" runat="server" BackColor="White" 
            BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" 
            EnableAppStyling="True" ForeColor="Black" SelBackColor="DarkBlue" 
            SelForeColor="White" StyleSetName="Caribbean" Version="4.00" Width="155px">
            <Columns>
                <igtbl:UltraGridColumn>
                    <header caption="Column0">
                    </header>
                </igtbl:UltraGridColumn>
            </Columns>
            <expandeffects shadowcolor="LightGray" />
            <dropdownlayout bordercollapse="Separate" rowheightdefault="20px" 
                version="4.00">
                <framestyle backcolor="Silver" borderstyle="Ridge" borderwidth="2px" 
                    cursor="Default" font-names="Verdana" font-size="10pt" height="130px" 
                    width="325px">
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
    <TD align=left><P align=center><igtxt:webimagebutton id="BtnAgregar" runat="server" Height="22px" Width="150px" Text="Agregar y Guardar" UseBrowserDefaults="False" OnClick="BtnAgregar_Click">
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
                <td style="width: 716px; height: 217px">
                    <igtbl:UltraWebGrid ID="Grid" runat="server" Browser="Xml" Height="323px" OnInitializeDataSource="Grid_InitializeDataSource"
                        OnInitializeLayout="Grid_InitializeLayout" Style="left: 7px; top: -233px" 
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
                                BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="323px"
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
                <td class="style5">
                                        <FONT face="Arial" size="3">
										<asp:Label id="LError" runat="server" ForeColor="#CC0033" Font-Names="Arial Narrow" Font-Size="Smaller"></asp:Label>
										<asp:Label id="LCorrecto" runat="server" ForeColor="Green" Font-Names="Arial Narrow" Font-Size="Smaller"></asp:Label></FONT></td>
            </tr>
            <tr>
                <td class="style2">

                    <igtxt:webimagebutton id="BtnQuitar" runat="server" Height="22px" 
                        Width="150px" Text="Quitar permiso" UseBrowserDefaults="False" 
                        OnClick="BtnQuitar_Click">
                        <Appearance>
                            <Image Height="16px" Width="16px" Url="./Imagenes/CANCELAR.png">
                            </Image>
                            <ButtonStyle Cursor="Default">
                            </ButtonStyle>
                        </Appearance>
                        <RoundedCorners RenderingType="FileImages" MaxWidth="400" MaxHeight="80" ImageUrl="ig_butXP1wh.gif" HoverImageUrl="ig_butXP2wh.gif" FocusImageUrl="ig_butXP3wh.gif" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif">
                        </RoundedCorners>
                        <Alignments VerticalAll="Bottom" VerticalImage="Middle">
                        </Alignments>
                    </igtxt:webimagebutton>
                    <br />
                </td>
            </tr>
        </table>
        &nbsp;
    </form>
</body>
</html>
