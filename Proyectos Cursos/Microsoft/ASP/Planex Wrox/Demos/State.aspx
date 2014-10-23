<%@ Page Title="State Demo" Language="C#" AutoEventWireup="true" CodeFile="State.aspx.cs" Inherits="Demos_State" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title></title>
  <style type="text/css">
    .auto-style1
    {
      width: 100%;
    }
  </style>
</head>
<body>
  <form id="form1" runat="server">
    <div>
      <table class="auto-style1">
      <tr>
        <td>
          <asp:Label ID="Label1" runat="server" Text="Label" EnableViewState="False"></asp:Label>
        </td>
        <td>
          <asp:Button ID="SetDate" runat="server" Text="Set Date" OnClick="SetDate_Click" />
        </td>
      </tr>
      <tr>
        <td>
          <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" Width="200px">
            <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
            <NextPrevStyle VerticalAlign="Bottom" />
            <OtherMonthDayStyle ForeColor="#808080" />
            <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
            <SelectorStyle BackColor="#CCCCCC" />
            <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
            <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
            <WeekendDayStyle BackColor="#FFFFCC" />
          </asp:Calendar>
        </td>
        <td>
          <asp:Button ID="PlainPostback" runat="server" Text="Plain Postback" />
        </td>
      </tr>
      <tr>
        <td>
          <asp:TextBox ID="TextBox1" runat="server" EnableViewState="False"></asp:TextBox>
        </td>
        <td>&nbsp;</td>
      </tr>
      </table>
    </div>
  </form>
</body>
</html>
