<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WF_EM_Faltas.aspx.cs" Inherits="WF_EM_Faltas" MasterPageFile="MasterPage.master" %>

<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">
    <iframe id="Ifrm" frameborder="0" width="100%" src="WF_AsistenciasEmp.aspx?Parametros=AGRUPACION&amp;Agrupacion=|&amp;Mostrar="+ gup("Parametros") 
    style="height: 433px"></iframe>
</asp:Content>
