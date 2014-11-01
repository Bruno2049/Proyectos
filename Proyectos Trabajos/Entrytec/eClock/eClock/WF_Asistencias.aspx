<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="WF_Asistencias.aspx.cs" Inherits="WF_Asistencias" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebNavigator.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebNavigator" TagPrefix="ignav" %>

<%@ Register Assembly="Infragistics2.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.Web.UI.LayoutControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>


<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script id="igClientScript" type="text/javascript">
<!--

function TrEmpleados_AfterNodeSelectionChange(treeId, nodeId){
	var selectedNode = igtree_getNodeById(nodeId);
	var Texto = igtree_getNodeById(nodeId).getText();
	if(Texto == "Todos")
	{
	    self.frames[0].location = "WF_AsistenciasTot.aspx?Parametros="+Texto;	
	    return;
	}
	var node = igtree_getNodeById(nodeId);
	var cnodes = node.getChildNodes();
    if(cnodes.length == 0)
	    self.frames[0].location = "WF_AsistenciasEmp.aspx?Parametros="+Texto;
	else
	    self.frames[0].location = "WF_AsistenciasGrupo.aspx?Parametros="+Texto;
		
	
}
// -->
</script>
    <div>
        <table id="Table1" border="0" cellpadding="1" cellspacing="1" style="width: 100%;
            font-family: Arial" width="300">

        </table>
    
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server" OnInit="ScriptManager1_Init">
    </asp:ScriptManager>
    <ig:WebSplitter ID="WebSplitter1" runat="server" Height="441px" Width="909px">
        <Panes>
            <ig:SplitterPane runat="server" Size="200px">
                <Template>
                    <table width="100%" style="height: 434px">
                        <tr>
                            <td style="font-size: large; background-image: url(skins/GridTitulo.gif); color: white; width: 197px;">
                                Empleados</td>
                        </tr>
                        <tr>
                            <td style="width: 197px">
                                <ignav:UltraWebTree ID="TrEmpleados" runat="server" DefaultImage="" HiliteClass=""
                                    HoverClass="" Indentation="20" Width="100%" Height="360px" OnDemandLoad="TrEmpleados_DemandLoad" CompactRendering="False" EnableViewState="False" LoadOnDemand="ManualSmartCallbacks" SingleBranchExpand="True" WebTreeTarget="ClassicTree">
                                    <Levels>
                                        <ignav:Level Index="0" LevelCheckBoxes="False" />
                                    </Levels>
                                    <ClientSideEvents AfterNodeSelectionChange="TrEmpleados_AfterNodeSelectionChange" />
                                </ignav:UltraWebTree>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 24px; width: 197px;">
                                <igtxt:WebImageButton ID="Btn_NuevoEmp" runat="server" Height="24px" ImageTextSpacing="4"
                                    OnClick="BNuevo_Click" Text="Nuevo Empleado" UseBrowserDefaults="False" Width="152px">
                                    <Appearance>
                                        <Image Height="16px" Url="./Imagenes/New.png" Width="16px" />
                                        <ButtonStyle>
                                            <Padding Top="4px" />
                                        </ButtonStyle>
                                    </Appearance>
                                    <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                                        HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400"
                                        PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                                    <Alignments HorizontalAll="NotSet" VerticalAll="NotSet" VerticalImage="Middle" />
                                </igtxt:WebImageButton>
                            </td>
                        </tr>
                    </table>
                </Template>
            </ig:SplitterPane>
            <ig:SplitterPane runat="server">
                <Template>
                    <table width="100%">
                        <tr>
                            <td style="font-size: large; background-image: url(skins/GridTitulo.gif); color: white; width: 197px; text-align: center;">
                                Asistencia</td>
                        </tr>
                        <tr>
                            <td style="height: 333px" >
                                                                
                                                                <iframe src ="WF_AsistenciasEmp.aspx" width="100%" height="300">
  <p>Your browser does not support iframes.</p>
</iframe>
</td>
                        </tr>
                        <tr>
                            <td><igtxt:WebImageButton ID="Btn_ImprimirAsis" runat="server" Height="24px" ImageTextSpacing="4"
                                    OnClick="BNuevo_Click" Text="Imprimir" UseBrowserDefaults="False" Width="152px">
                                <Appearance>
                                    <Image Height="16px" Url="./Imagenes/New.png" Width="16px" />
                                    <ButtonStyle>
                                        <Padding Top="4px" />
                                    </ButtonStyle>
                                </Appearance>
                                <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                                        HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400"
                                        PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                                <Alignments HorizontalAll="NotSet" VerticalAll="NotSet" VerticalImage="Middle" />
                            </igtxt:WebImageButton>

                            </td>
                        </tr>
                    </table>
                </Template>
            </ig:SplitterPane>
        </Panes>
    </ig:WebSplitter>
</asp:Content>