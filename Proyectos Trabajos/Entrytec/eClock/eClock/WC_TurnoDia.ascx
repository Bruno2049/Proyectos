<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WC_TurnoDia.ascx.cs" Inherits="WC_TurnoDia" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<asp:checkbox id="Cbx_Dia" runat="server" text="Dia semana" width="100px"></asp:checkbox>
<igtxt:WebDateTimeEdit ID="Tbx_EntMin" runat="server" DisplayModeFormat="HH:mm"
    EditModeFormat="HH:mm"  EnableAppStyling="True" Font-Size="XX-Small" MinimumNumberOfValidFields="1"
    StyleSetName="Caribbean" TabIndex="1" Width="35px">
</igtxt:WebDateTimeEdit><igtxt:WebDateTimeEdit ID="Tbx_Ent" runat="server" DisplayModeFormat="HH:mm"
    EditModeFormat="HH:mm"  EnableAppStyling="True" Font-Size="XX-Small" MinimumNumberOfValidFields="1"
    StyleSetName="Caribbean" TabIndex="1" Width="35px"></igtxt:WebDateTimeEdit><igtxt:WebDateTimeEdit ID="WebDateTimeEdit2" runat="server" DisplayModeFormat="HH:mm"
    EditModeFormat="HH:mm"  EnableAppStyling="True" Font-Size="XX-Small" MinimumNumberOfValidFields="1"
    StyleSetName="Caribbean" TabIndex="1" Width="35px" OnValueChange="WebDateTimeEdit2_ValueChange">
</igtxt:WebDateTimeEdit><igtxt:WebDateTimeEdit ID="WebDateTimeEdit3" runat="server" DisplayModeFormat="HH:mm"
    EditModeFormat="HH:mm"  EnableAppStyling="True" Font-Size="XX-Small" MinimumNumberOfValidFields="1"
    StyleSetName="Caribbean" TabIndex="1" Width="35px">
</igtxt:WebDateTimeEdit><igtxt:WebDateTimeEdit ID="WebDateTimeEdit4" runat="server" DisplayModeFormat="HH:mm"
    EditModeFormat="HH:mm"  EnableAppStyling="True" Font-Size="XX-Small" MinimumNumberOfValidFields="1"
    StyleSetName="Caribbean" TabIndex="1" Width="35px">
</igtxt:WebDateTimeEdit><igtxt:WebDateTimeEdit ID="WebDateTimeEdit5" runat="server" BackColor="White" DisplayModeFormat="HH:mm"
    EditModeFormat="HH:mm" EnableAppStyling="True" Font-Size="XX-Small" MinimumNumberOfValidFields="1"
    StyleSetName="Caribbean" TabIndex="1" Width="35px">
</igtxt:WebDateTimeEdit>
