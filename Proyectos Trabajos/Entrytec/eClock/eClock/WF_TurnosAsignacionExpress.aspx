<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="WF_TurnosAsignacionExpress.aspx.cs" Inherits="WF_AsignacionTurnosExpress" EnableEventValidation = "false" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register assembly="Infragistics2.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.Web.UI.GridControls" tagprefix="ig" %>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<title>eClock</title>
    <style type="text/css">
    .style1
    {
    height: 21px;
    }
    </style>
   <%-- <script type="text/javascript">
        var ID_INDEX = 0;
        var selectedID = new Array();

        function getWpn_EC_TURNOS_() {
            return $find("<% this.Wpn_EC_TURNOS_.ClientID %>");
        }

        function getSelectedRows() {
            var Grid = getWpn_EC_TURNOS_();
            var Behaviors = Grid.get_behaviors();
            var Selection = Behaviors.get_selection();
            return Selection.get_selectedRows();
        }

        function SeleccionaEmpleado() {
            var rows = getSelectedRows();
            if (rows.get_length() > 0) {
                for (var i = 0; i < rows.get_length(); i++) {
                    selectedIDs[i] = rows.getItem(i).get_cell(ID_INDEX).get_text();
                }
            }
            else { 
                
            }
        }
    </script>--%>
</head>
<body style="font-size: 8px; font-family: tahoma; text-align: center; margin: 0px; ">
    <form id="form1" runat="server">
    <div>
        <table style="height: 212px; width: 850px;">
            <tr>
                <td colspan="2" style="height: 302px">
                    <igmisc:WebAsyncRefreshPanel ID="WebAsyncRefreshPanel0" runat="server" Height="" Width="100%">
                        <table style="width: 100%">
                            <tr>
                                <td style="text-align: left; width: 500px; height: 199px; font-family: 'Segoe UI'; font-size: small;"
                                    top">
                                    <ig:WebDataGrid ID="Wdg_EC_PERSONAS" runat="server" Height="100%" Width="100%" 
                                        Font-Size="X-Small" >
                                        <Behaviors>
                                            <ig:ColumnMoving></ig:ColumnMoving>
                                            <ig:ColumnResizing></ig:ColumnResizing>
                                            <ig:RowSelectors>
                                            </ig:RowSelectors>
                                            <ig:Selection CellClickAction="Row" CellSelectType="None" 
                                                RowSelectType="Multiple">
                                            </ig:Selection>
                                        </Behaviors>
                                    </ig:WebDataGrid>
                                </td>
                                <td style="text-align: left; width:200px; height: 199px;" valign="top">
                                    <igmisc:webpanel id="Wpn_EC_TURNOS_" runat="server"   EnableAppStyling="True" 
                                        StyleSetName="Caribbean" ExpandEffect="None"  Width="100%" Height="370px">
                                        <Template>
                                            <asp:RadioButtonList id="RbnL_EC_TURNOS" runat="server" OnLoad="RbnL_EC_TURNOS_Load" 
                                            DataTextField="TURNO_NOMBRE" DataValueField="TURNO_ID" AutoPostBack="True" 
                                            OnSelectedIndexChanged="RbnL_EC_TURNOS_SelectedIndexChanged" RepeatColumns="1" 
                                            Font-Size="XX-Small"></asp:RadioButtonList>
                                        </Template>
                                        <PanelStyle BackgroundImage="./images/igdt_Blank.gif">
                                        </PanelStyle>
                                        <Header Text="Turnos Disponibles">
                                        </Header>
                                </igmisc:webpanel></td>
                            </tr>
                        </table>
                    </igmisc:WebAsyncRefreshPanel>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2" class="style1">
                    <igtxt:WebImageButton ID="WIBtn_Filtro" runat="server" Height="22px" OnClick="WIBtn_Filtro_Click" Text="Filtrar Empleados" UseBrowserDefaults="False" Width="150px" ImageTextSpacing="4">
                        <Alignments VerticalImage="Bottom" HorizontalAll="NotSet" VerticalAll="NotSet" />
                        <Appearance>
                            <Image Url="./Imagenes/stock-convert.png" Height="18px" Width="20px" />
                            <Style Cursor="Default"></Style>
                        </Appearance>
                        <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                        HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400"
                        PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                    </igtxt:WebImageButton>
                    <igtxt:webimagebutton id="WIBtn_Guardar" runat="server" onclick="WIBtn_Guardar_Click"
                        text="Guardar" usebrowserdefaults="False" ImageTextSpacing="4" 
                        Height="22px">
                        <Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
                        <Appearance>
                            <Image Url="./Imagenes/Save_as.png" Height="16px" Width="16px"></Image>
                        </Appearance>

                        <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif" HoverImageUrl="ig_butXP2wh.gif" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" MaxWidth="400" MaxHeight="80" ImageUrl="ig_butXP1wh.gif"></RoundedCorners>
                    </igtxt:webimagebutton>
                </td>
            </tr>
        </table>
    </div>
    <asp:ScriptManager ID="SM" runat="server">
    </asp:ScriptManager>
    </form>
</body>
</html>
