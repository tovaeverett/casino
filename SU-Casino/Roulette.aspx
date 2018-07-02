    <%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Roulette.aspx.cs" Inherits="SU_Casino.Roulette" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:RadioButtonList ID="RadioButtonList1" runat="server" Width="323px">
                <asp:ListItem>Black</asp:ListItem>
                <asp:ListItem>Red</asp:ListItem>
            </asp:RadioButtonList>
            <asp:Button ID="btnSpin" runat="server" Text="SPIN" OnClick="btnSpin_Click" /> 
            <asp:Label ID="lblColor" runat="server"></asp:Label>
            <asp:Label ID="lblNr" runat="server"></asp:Label>
            
            <asp:Image ID="imgWinLose" runat="server" />
        </div>
    </form>
</body>
</html>
