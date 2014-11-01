<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReportServerForm.aspx.cs"
    Inherits="PAEEEM.CustomReports.ReportServerForm" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

  

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Resources/Css/StyleSheet.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <%-- <script type="text/javascript">
          window.onload = function () {
              var str = document.getElementsByTagName("a");
              str[4].style.display = "none";
              str[5].style.display = "none";
              str[7].style.display = "none";
              str[8].style.display = "none";
              str[9].style.display = "none";
              str[10].style.display = "none";
          };
    </script> --%>
    <div>
         <rsweb:ReportViewer ID="reportServer" runat="server" ProcessingMode="Remote" Height="29.7cm" Width="28cm" BackColor="White">
        </rsweb:ReportViewer>
        
        <asp:Label ID="lError" runat="server" ForeColor="Red"></asp:Label>
    </div>
</asp:Content>
