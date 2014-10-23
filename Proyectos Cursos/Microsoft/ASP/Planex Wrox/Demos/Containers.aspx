<%@ Page Title="Containers Demo" Language="C#" AutoEventWireup="true" CodeFile="Containers.aspx.cs" Inherits="Demos_Containers" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title></title>
</head>
<body>
  <form id="form1" runat="server">
    <div>
      <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" Text="Show Panel" OnCheckedChanged="CheckBox1_CheckedChanged" />
      <asp:Panel ID="Panel1" runat="server" Visible="False">
        <asp:Wizard ID="Wizard1" runat="server" ActiveStepIndex="0" Width="500px" OnFinishButtonClick="Wizard1_FinishButtonClick">
          <WizardSteps>
            <asp:WizardStep ID="WizardStep1" runat="server" Title="About You">
              <asp:Label ID="Label1" runat="server" Text="Type your name"></asp:Label>
              <asp:TextBox ID="YourName" runat="server"></asp:TextBox>
            </asp:WizardStep>
            <asp:WizardStep ID="WizardStep2" runat="server" Title="Favorite Language" StepType="Finish">
              <asp:DropDownList ID="FavoriteLanguage" runat="server">
                <asp:ListItem>C#</asp:ListItem>
                <asp:ListItem>Visual Basic</asp:ListItem>
                <asp:ListItem>CSS</asp:ListItem>
              </asp:DropDownList>
            </asp:WizardStep>
            <asp:WizardStep runat="server" StepType="Complete" Title="Ready">
              <asp:Label ID="Result" runat="server" Text="Label"></asp:Label>
            </asp:WizardStep>
          </WizardSteps>
        </asp:Wizard>
      </asp:Panel>
    </div>
  </form>
</body>
</html>
