<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReportServerForm.aspx.cs"
    Inherits="PAEEEM.CustomReports.ReportServerForm" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Resources/Css/StyleSheet.css" rel="Stylesheet" type ="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <rsweb:ReportViewer ID="reportServer" runat="server" ProcessingMode="Remote" Height="29.7cm" Width="28cm" BackColor="White">
            <ServerReport ReportServerUrl="" />
        </rsweb:ReportViewer>
        <asp:Label ID="lError" runat="server" ForeColor="Red"></asp:Label>
    </div>
</asp:Content>
