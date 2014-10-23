<%@ Page Title="My Profile" Language="C#" MasterPageFile="~/MasterPages/Frontend.master" AutoEventWireup="true" CodeFile="MyProfile.aspx.cs" Inherits="_MyProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  <style type="text/css">
    .auto-style1
    {
      width: 100%;
    }
  </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMainContent" runat="Server">
  <h1>My Profile</h1>
  <p>
    The My Profile page allows you to make changes to your personal profile. Besides changing your password, you can also tell us a bit about yourself.
  </p>
  <table class="auto-style1">
  <tr>
    <td>
      <asp:Label ID="Label1" runat="server" AssociatedControlID="FirstName" Text="First name"></asp:Label>
    </td>
    <td>
      <asp:TextBox ID="FirstName" runat="server"></asp:TextBox>
    </td>
    <td>
      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="FirstName" Display="Dynamic" ErrorMessage="First name is required."></asp:RequiredFieldValidator>
    </td>
  </tr>
  <tr>
    <td>
      <asp:Label ID="Label2" runat="server" AssociatedControlID="LastName" Text="Last name"></asp:Label>
    </td>
    <td>
      <asp:TextBox ID="LastName" runat="server"></asp:TextBox>
    </td>
    <td>
      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="LastName" Display="Dynamic" ErrorMessage="Last name is required."></asp:RequiredFieldValidator>
    </td>
  </tr>
  <tr>
    <td>
      <asp:Label ID="Label3" runat="server" AssociatedControlID="DateOfBirth" Text="Date of birth"></asp:Label>
    </td>
    <td>
      <asp:TextBox ID="DateOfBirth" runat="server"></asp:TextBox>
    </td>
    <td>
      <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="DateOfBirth" Display="Dynamic" ErrorMessage="Date of birth is required."></asp:RequiredFieldValidator>
      <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="DateOfBirth" Display="Dynamic" ErrorMessage="Please enter a valid date." Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
    </td>
  </tr>
  <tr>
    <td>
      <asp:Label ID="Label4" runat="server" AssociatedControlID="Bio" Text="Biography"></asp:Label>
    </td>
    <td>
      <asp:TextBox ID="Bio" runat="server" Height="75px" TextMode="MultiLine" Width="300px"></asp:TextBox>
    </td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>
      <asp:Label ID="Label5" runat="server" AssociatedControlID="PreferenceList" Text="Favorite genres"></asp:Label>
    </td>
    <td>
      <asp:CheckBoxList ID="PreferenceList" runat="server" DataSourceID="EntityDataSource1" DataTextField="Name" DataValueField="Id" OnDataBound="PreferenceList_DataBound">
      </asp:CheckBoxList>
      <asp:EntityDataSource ID="EntityDataSource1" runat="server" ConnectionString="name=PlanetWroxEntities" DefaultContainerName="PlanetWroxEntities" EnableFlattening="False" EntitySetName="Genres" OrderBy="it.Name" Select="it.[Id], it.[Name]">
      </asp:EntityDataSource>
    </td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td>
      <asp:Button ID="SaveButton" runat="server" Text="Save Profile" OnClick="SaveButton_Click" />
    </td>
    <td>&nbsp;</td>
  </tr>
  </table>
  <asp:ChangePassword ID="ChangePassword1" runat="server"></asp:ChangePassword>
</asp:Content>

