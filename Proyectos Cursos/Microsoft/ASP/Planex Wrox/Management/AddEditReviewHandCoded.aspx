<%@ Page Title="Planet Wrox - Management - Insert and Update Reviews" Language="C#" MasterPageFile="~/MasterPages/Management.master" AutoEventWireup="true" CodeFile="AddEditReviewHandCoded.aspx.cs" Inherits="Management_AddEditReviewHandCoded" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  <style type="text/css">
    .auto-style1
    {
      width: 100%;
    }
  </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMainContent" runat="Server">
  <table class="auto-style1">
  <tr>
    <td>
      <asp:Label ID="Label1" runat="server" Text="Title"></asp:Label>
    </td>
    <td>
      <asp:TextBox ID="TitleText" Width="450px" runat="server"></asp:TextBox>
    </td>
  </tr>
  <tr>
    <td>
      <asp:Label ID="Label2" runat="server" Text="Summary"></asp:Label>
    </td>
    <td>
      <asp:TextBox ID="SummaryText" Width="450px" TextMode="MultiLine" runat="server"></asp:TextBox>
    </td>
  </tr>
  <tr>
    <td>
      <asp:Label ID="Label3" runat="server" Text="Body"></asp:Label>
    </td>
    <td>
      <asp:TextBox ID="BodyText" Width="450px" TextMode="MultiLine" runat="server"></asp:TextBox>
    </td>
  </tr>
  <tr>
    <td>
      <asp:Label ID="Label4" runat="server" Text="Genre"></asp:Label>
    </td>
    <td>
      <asp:DropDownList ID="GenreList" runat="server" DataSourceID="EntityDataSource1" DataTextField="Name" DataValueField="Id">
      </asp:DropDownList>
      <asp:EntityDataSource ID="EntityDataSource1" runat="server" ConnectionString="name=PlanetWroxEntities" DefaultContainerName="PlanetWroxEntities" EnableFlattening="False" EntitySetName="Genres">
      </asp:EntityDataSource>
    </td>
  </tr>
  <tr>
    <td>
      <asp:Label ID="Label5" runat="server" Text="Authorized"></asp:Label>
    </td>
    <td>
      <asp:CheckBox ID="Authorized" runat="server" />
    </td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td>
      <asp:Button ID="SaveButton" runat="server" Text="Save" OnClick="SaveButton_Click" />
    </td>
  </tr>
  </table>
</asp:Content>

