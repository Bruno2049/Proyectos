<%@ Register TagPrefix="flashmovie" Namespace="Osmosis.Web.UI.Controls" Assembly="FlashMovie" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WF_EmpleadosBus2.aspx.cs" Inherits="WF_EmpleadosBus2" %>

<%@ Register Src="WCBotonesEncabezado.ascx" TagName="WCBotonesEncabezado" TagPrefix="uc2" %>
<%@ Register Src="WC_LinksPiePagina.ascx" TagName="WC_LinksPiePagina" TagPrefix="uc3" %>

<%@ Register Src="WC_Menu.ascx" TagName="WC_Menu" TagPrefix="uc1" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebNavigator.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebNavigator" TagPrefix="ignav" %>
<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register TagPrefix="flashmovie" Namespace="Osmosis.Web.UI.Controls" Assembly="FlashMovie" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script id="igClientScript" type="text/javascript" >
    function BtBuscarEmpleado_Click(oButton, oEvent)
    {         
  		try
 {   
	    var row = igtbl_getActiveRow("Grid");
//    alert("Editando el empleado :" + row.getCell(1).getValue());
   	window.location.href = 'WF_EmpleadosBus2.aspx?Parametros=A'+row.getCell(1).getValue();
	}
	catch(err)
	{
	alert(err.description);
	}
    }
    </script>




<html xmlns="http://www.w3.org/1999/xhtml"  >
<head id="Head1" runat="server">
    <title>eClock</title>
</head>
<body background="skins/boxed-bg.gif" style="font-size: 11px; font-family: tahoma; text-align: center; margin: 10px;">
    <form id="form1" runat="server" style="text-align: center">
        &nbsp;<table border="0" cellpadding="0" cellspacing="0" class="BoxTable" width="960">
                        <tr>
                            <td class="BoxTL" style="width: 11px">
                                <img height="11" src="skins/box-tl.gif" width="11" /></td>
                            <td class="BoxT" style="background-image: url(skins/box-t.gif); background-repeat: repeat-x">
                                <img height="11" src="skins/dummy.gif" width="11" /></td>
                            <td class="BoxTR" style="width: 11px">
                                <img height="11" src="skins/box-tr.gif" width="11" /></td>
                        </tr>
                        <tr>
                            <td class="BoxL" style="background-image: url(skins/box-l.gif); width: 11px; background-repeat: repeat-y">
                                <img height="11" src="skins/dummy.gif" width="11" /></td>
                            <td class="BoxM" valign="top" style="background-color: white">
                                <table align="center" border="0" cellpadding="0" cellspacing="0" class="FullHeight StandardWidth" width="100%">
                                    <tr>
                                        <td class="LogoRow" colspan="2" style="width: 937px">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td class="LogoTD">
                                                        &nbsp;<asp:Image ID="Image1" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                            ImageUrl="WF_Logos_imgencabezado.aspx" /></td>
                                                    <td class="LoginTD" nowrap="nowrap" valign="top" style="text-align: right">
                                                        <div>
                                                            &nbsp;</div>
                                                        <br />
                                                        <br />
                                                        <uc2:WCBotonesEncabezado ID="WCBotonesEncabezado1" runat="server" />
                                                        <br />
                                                        &nbsp;<br />
                                                        &nbsp;</td>
                                                </tr>
                                            </table> 
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="ImagePane01" runat="server" class="ImagePane01" colspan="2" valign="top"
                                            visible="false" style="width: 937px">
                                        </td>
                                    </tr> 
                                    <tr>
                                        <td class="MenuHeight" colspan="2" style="height: 49px; width: 937px;">
                                            <uc1:WC_Menu ID="WC_Menu1" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 16px">
                                            <table id="FooterTable" border="0" cellpadding="0" cellspacing="0" class="FooterTable"
                                                style="display: block" width="100%">
                                                <tr>
                                                    <td id="Td1" runat="server" class="FooterPane" colspan="1" style="width: 8px" valign="top"
                                                        visible="true">
                                                    </td>
                                                    <td id="Td2" runat="server" class="FooterPane" colspan="1" style="background-image: url(skins/FrameSI.gif);
                                                        width: 8px; background-repeat: repeat-x" valign="top" visible="true">
                                                    </td>
                                                    <td id="Td3" runat="server" class="FooterPane" colspan="1" rowspan="3" style="background-image: url(skins/FrameS.gif);
                                                        width: 41px; background-repeat: repeat-x" valign="top" visible="true">
                                                        <asp:Image ID="img_Icono" runat="server" Height="48px" ImageUrl="~/Imagenes.Main/IconosPagina/WF_EmpleadosBus2.aspx.ico.png"
                                                            Width="48px" /></td>
                                                    <td id="Td4" runat="server" class="FooterPane" colspan="2" style="background-image: url(skins/FrameS.gif);
                                                        background-repeat: repeat-x; height: 8px" valign="top" visible="true">
                                                    </td>
                                                    <td id="Td5" runat="server" class="FooterPane" colspan="1" style="background-image: url(skins/FrameSD.gif);
                                                        width: 5px; background-repeat: no-repeat" valign="top" visible="true">
                                                    </td>
                                                    <td id="Td6" runat="server" class="FooterPane" colspan="1" valign="top" visible="true">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td id="Td7" runat="server" class="FooterPane" colspan="1" style="width: 8px; height: 20px"
                                                        valign="top" visible="true">
                                                    </td>
                                                    <td id="Td8" runat="server" class="FooterPane" colspan="1" style="background-image: url(skins/FrameI.gif);
                                                        width: 8px; background-repeat: repeat-y; height: 20px" valign="top" visible="true">
                                                    </td>
                                                    <td id="Td9" runat="server" class="FooterPane" colspan="2" style="height: 20px; text-align: left"
                                                        valign="top" visible="true">
                                                        <asp:Label ID="LTitulo" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="Medium"
                                                            ForeColor="Black" Height="20px" Width="100%"></asp:Label></td>
                                                    <td id="Td10" runat="server" class="FooterPane" colspan="1" style="background-image: url(skins/FrameD.gif);
                                                        width: 12px; background-repeat: repeat-y; height: 8px" valign="top" visible="true">
                                                    </td>
                                                    <td id="Td11" runat="server" class="FooterPane" colspan="1" style="height: 8px" valign="top"
                                                        visible="true">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td id="Td12" runat="server" class="FooterPane" colspan="1" style="width: 8px; height: 20px"
                                                        valign="top" visible="true">
                                                    </td>
                                                    <td id="Td13" runat="server" class="FooterPane" colspan="1" style="background-image: url(skins/FrameI.gif);
                                                        width: 8px; background-repeat: repeat-y; height: 20px" valign="top" visible="true">
                                                    </td>
                                                    <td id="Td14" runat="server" class="FooterPane" colspan="2" style="height: 20px;
                                                        text-align: left" valign="top" visible="true">
                                                        <asp:Label ID="LDescripcion" runat="server" Font-Italic="False" Font-Size="Small"
                                                            ForeColor="Black" Width="100%"></asp:Label></td>
                                                    <td id="Td15" runat="server" class="FooterPane" colspan="1" style="background-image: url(skins/FrameD.gif);
                                                        width: 12px; background-repeat: repeat-y; height: 20px" valign="top" visible="true">
                                                    </td>
                                                    <td id="Td16" runat="server" class="FooterPane" colspan="1" style="height: 20px"
                                                        valign="top" visible="true">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td id="Td17" runat="server" class="FooterPane" colspan="1" style="width: 8px; height: 16px"
                                                        valign="top" visible="true">
                                                    </td>
                                                    <td id="Td18" runat="server" class="FooterPane" colspan="1" style="background-image: url(skins/FrameII.gif);
                                                        width: 8px; background-repeat: no-repeat; height: 16px" valign="top" visible="true">
                                                    </td>
                                                    <td id="Td24" runat="server" class="FooterPane" colspan="1" style="background-image: url(skins/FrameInf.gif);
                                                        width: 41px; background-repeat: repeat-x; height: 16px" valign="top" visible="true">
                                                    </td>
                                                    <td id="Td25" runat="server" class="FooterPane" colspan="2" style="background-image: url(skins/FrameInf.gif);
                                                        background-repeat: repeat-x; height: 16px" valign="top" visible="true">
                                                    </td>
                                                    <td id="Td26" runat="server" class="FooterPane" colspan="1" style="background-image: url(skins/FrameID.gif);
                                                        width: 12px; background-repeat: no-repeat; height: 16px" valign="top" visible="true">
                                                    </td>
                                                    <td id="Td27" runat="server" class="FooterPane" colspan="1" style="height: 16px"
                                                        valign="top" visible="true">
                                                    </td>
                                                </tr>
                                            </table>
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="MenuHeight" colspan="2" style="width: 937px; height: 77px;" align="center">
                                            <table border="0" cellpadding="0" cellspacing="0" class="FooterTable" width="100%">
                                                <tr>
                                                    <td id="Td19" runat="server" class="FooterPane" colspan="1" style="width: 8px" valign="top"
                                                        visible="true">
                                                    </td>
                                                    <td id="Td20" runat="server" class="FooterPane" colspan="1" valign="top" visible="true" style="width: 8px; background-repeat: no-repeat">
                                                    </td>
                                                    <td id="Td21" runat="server" class="FooterPane" colspan="2" valign="top" visible="true">
    
<TABLE width="100%">
       
            <tr>
                <td valign="top" align="right">
                    <asp:CheckBox ID="CBEmpBorrados" runat="server" Font-Names="Arial" Font-Size="Small"
                        Text="Ver empleados borrados" OnCheckedChanged="CBEmpBorrados_CheckedChanged1" AutoPostBack="True" /></td>
            </tr>
            <tr>
                <td style="width: 920px; height: 200px;" valign="top">
                    <igtbl:UltraWebGrid ID="Grid" runat="server" Browser="Xml" Height="200px" OnInitializeDataSource="Grid_DataSource"
                        OnInitializeLayout="Grid_InitializeLayout" Style="left: 1px; top: 0px" Width="100%">
                        <Bands>
                            <igtbl:UltraGridBand>
                                <AddNewRow View="NotSet" Visible="NotSet">
                                </AddNewRow>
                            </igtbl:UltraGridBand>
                        </Bands>
                        <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowRowNumberingDefault="Continuous"
                            AllowSortingDefault="OnClient" BorderCollapseDefault="Separate" CellClickActionDefault="RowSelect"
                            HeaderClickActionDefault="SortMulti" LoadOnDemand="Xml" Name="Grid" RowHeightDefault="20px"
                            RowSelectorsDefault="No" RowsRange="30" SelectTypeRowDefault="Extended" StationaryMargins="Header"
                            StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy">
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
                                BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="200px"
                                Width="100%">
                            </FrameStyle>
                            <Pager MinimumPagesForDisplay="2">
                            </Pager>
                            <AddNewBox Hidden="False">
                            </AddNewBox>
                        </DisplayLayout>
                    </igtbl:UltraWebGrid></td>
            </tr>
            <tr>
                <td style="height: 10%; text-align: center; width: 920px;">
                    &nbsp;<asp:Label ID="LError" runat="server" Font-Names="Arial" Font-Size="Small"
                        ForeColor="Red"></asp:Label>&nbsp;
                    <asp:Label ID="LCorrecto" runat="server" Font-Names="Arial" Font-Size="Small" ForeColor="LimeGreen"></asp:Label><br />
                    <igtxt:WebImageButton ID="BtBuscarEmpleado" runat="server" Height="21px"
                        Text="Abrir Empleado" UseBrowserDefaults="False" Width="128px" AutoSubmit="False">
                        <RoundedCorners HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif"
                            MaxHeight="80" MaxWidth="400" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif" />
                            <ClientSideEvents MouseDown="BtBuscarEmpleado_Click" />
                        <Alignments HorizontalAll="Left" TextImage="TextRightImageLeft"
                            VerticalImage="Middle" />
                        <Appearance>
                            <Image Url="./Imagenes/Empleado.png" Height="18px" Width="20px" />
                        </Appearance>
                    </igtxt:WebImageButton>
                </td>
            </tr>
        </TABLE>
                                                    </td>
                                                    <td id="Td22" runat="server" class="FooterPane" colspan="1" valign="top" visible="true">
                                                    </td>
                                                    <td id="Td23" runat="server" class="FooterPane" colspan="1" style="height: 5px" valign="top"
                                                        visible="true">
                                                    </td>
                                                </tr>
                                            </table>
   
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="FooterRow" colspan="2" style="width: 937px; height: 49px;">
                                            &nbsp;<uc3:WC_LinksPiePagina id="WC_LinksPiePagina1" runat="server"></uc3:WC_LinksPiePagina></td>
                                    </tr>
                                    <tr>
                                        <td class="CopyrightRow" colspan="2" style="color: #aaa; height: 27px; width: 937px;">
                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td class="PrivacyTD" nowrap="nowrap" valign="top" style="height: 14px">
                                                        <span class="FOOTER_objects"> | &nbsp;&nbsp;&nbsp;</span>
                                                Copyright 2007, ID Soft S.A. de C.V. todos los derechos reservados &nbsp; &nbsp; |</td>
                                                    <td class="CopyrightTD" nowrap="nowrap" valign="top" style="width: 50px; height: 14px;">
                                                        <span class="FOOTER_objects">
                                                            <img class="Invisible" height="1" src="dummy.gif" width="1" /><img class="Invisible"
                                                                height="1" src="dummy.gif" width="1" /><img class="Invisible" height="1" src="ico-login-over.gif"
                                                                    width="2" /><img class="Invisible" height="1" src="ico-register-over.gif" width="1" /><br />
                                                </span></td>
                                                </tr>
                                            </table>
                                            <span class="FOOTER_objects"><span id="dnn_dnnCOPYRIGHT_lblCopyright" class="FOOTER_objects">
                                                </span></span></td>
                                    </tr>
                                </table>
                            </td>
                            <td class="BoxR" style="background-image: url(skins/box-r.gif); width: 11px; background-repeat: repeat-y">
                                <img height="11" src="skins/dummy.gif" width="11" /></td>
                        </tr>
                        <tr>
                            <td class="BoxBL" style="width: 11px">
                                <img height="11" src="skins/box-bl.gif" width="11" /></td>
                            <td class="BoxB" style="background-image: url(skins/box-b.gif); background-repeat: repeat-x">
                                <img height="11" src="skins/dummy.gif" width="11" /></td>
                            <td class="BoxBR" style="width: 11px">
                                <img height="11" src="skins/box-br.gif" width="11" /></td>
                        </tr>
                    </table>
    </form>
</body>
</html>
