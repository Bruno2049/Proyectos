<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HotKey.aspx.cs" Inherits="Hotkey_Funtionality.HotKey" %>

<!DOCTYPE html>
<script runat="server">

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string[] carArray = new[] { "Ford", "Honda", "BMW", "Dodge" };
        string[] airplaneArray = new[] { "Boeing 777", "Boeing 747", "Boeing 737" };
        string[] trainArray = new[] { "Bullet Train", "Amtrack", "Tram" };
        if (DropDownList1.SelectedValue == "Car")
        {
            DropDownList2.DataSource = carArray;
        }
        else if (DropDownList1.SelectedValue == "Airplane")
        {
            DropDownList2.DataSource = airplaneArray;
        }
        else if (DropDownList1.SelectedValue == "Train")
        {
            DropDownList2.DataSource = trainArray;
        }
        DropDownList2.DataBind();
        DropDownList2.Visible = DropDownList1.SelectedValue != "Select an Item";
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Write("You selected <b>" +
        DropDownList1.SelectedValue.ToString() + ": " +
        DropDownList2.SelectedValue.ToString() + "</b>");
    }
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>

    This Wizard control has three
        <asp:Wizard ID="Wizard1" runat="server" ActiveStepIndex="2">
            <WizardSteps>
                <asp:WizardStep ID="WizardStep1" runat="server"
                    Title="Provide Personal Info">
                    First name:<br />
                    <asp:TextBox ID="fnameTextBox" runat="server"></asp:TextBox><br />
                    Last name:<br />
                    <asp:TextBox ID="lnameTextBox" runat="server"></asp:TextBox><br />
                    Email:<br />
                    <asp:TextBox ID="emailTextBox" runat="server"></asp:TextBox><br />
                </asp:WizardStep>
                <asp:WizardStep ID="WizardStep2" runat="server"
                    Title="Membership Information">
                    Are you already a member of our group?<br />
                    <asp:RadioButton ID="RadioButton1" runat="server" Text="Yes"
                        GroupName="Member" />
                    <asp:RadioButton ID="RadioButton2" runat="server" Text="No"
                        GroupName="Member" />
                </asp:WizardStep>
                <asp:WizardStep ID="WizardStep3" runat="server"
                    Title="Provided Information"
                    StepType="Complete">
                    <asp:Label ID="Label3" runat="server" />
                </asp:WizardStep>
            </WizardSteps>
        </asp:Wizard>

    <form id="form1" runat="server">

        <div>
            <p>
                <asp:Label ID="Label1" runat="server" AccessKey="N"
                    AssociatedControlID="Textbox1">User<u>n</u>ame</asp:Label>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            </p>
            <p>
                <asp:Label ID="Label2" runat="server" AccessKey="P"
                    AssociatedControlID="Textbox2"><u>P</u>assword</asp:Label>
                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            </p>
            <p>
                <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                    <asp:ListItem>Select an Item</asp:ListItem>
                    <asp:ListItem>Car</asp:ListItem>
                    <asp:ListItem>Airplane</asp:ListItem>
                    <asp:ListItem>Train</asp:ListItem>
                </asp:DropDownList>
            </p>
            <p>
                <asp:DropDownList ID="DropDownList2" runat="server" Visible="false" />
            </p>

            <p>
                <asp:ListBox ID="ListBox1" runat="server" SelectionMode="Multiple">
                    <asp:ListItem>ASP.NET 4.5</asp:ListItem>
                    <asp:ListItem>ASP.NET MVC 4</asp:ListItem>
                    <asp:ListItem>jQuery 1.8.x</asp:ListItem>
                    <asp:ListItem>Visual Studio 2012</asp:ListItem>
                </asp:ListBox>
            </p>
            <p>
                <asp:Table ID="Table1" runat="server">
                    <asp:TableRow ID="TableRow1" runat="server" Font-Bold="True"
                        ForeColor="White" BackColor="DarkGray">
                        <asp:TableHeaderCell>First Name</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Last Name</asp:TableHeaderCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>Jason</asp:TableCell>
                        <asp:TableCell>Gaylord</asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>Scott</asp:TableCell>
                        <asp:TableCell>Hanselman</asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>Todd</asp:TableCell>
                        <asp:TableCell>Miranda</asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>Pranav</asp:TableCell>
                        <asp:TableCell>Rastogi</asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </p>
            <p>
                <asp:Calendar ID="Calendar1" runat="server" OnSelectionChanged="Calendar1_SelectionChanged" SelectionMode="DayWeekMonth"></asp:Calendar>
            </p>
            <p>
                <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Button1_Click" />
            </p>
        </div>
    </form>
</body>
</html>
