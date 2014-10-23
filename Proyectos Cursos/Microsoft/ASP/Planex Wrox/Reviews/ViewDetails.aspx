<%@ Page Language="C#" MasterPageFile="~/MasterPages/Frontend.master" AutoEventWireup="true" CodeFile="ViewDetails.aspx.cs" Inherits="Reviews_ViewDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMainContent" runat="Server">
  <h1><asp:Label ID="TitleLabel" runat="server"></asp:Label></h1>
  <asp:Label CssClass="Summary" ID="SummaryLabel" runat="server"></asp:Label>
  <asp:Label ID="BodyLabel" runat="server"></asp:Label>
</asp:Content>
