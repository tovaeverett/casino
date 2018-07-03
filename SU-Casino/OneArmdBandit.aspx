<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="~/OneArmdBandit.aspx.cs" Inherits="SU_Casino.OneArmdBandit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>
                        <asp:Button ID="btnPull" runat="server" OnClick="btnPull_Click" Text="PULL" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:image id="IMGslot1" runat="server"></asp:image>
                    </td>
                    <td>
                        <asp:image id="IMGslot2" runat="server"></asp:image>
                    </td>
                    <td>
                        <asp:image id="IMGslot3" runat="server"></asp:image>
                    </td>
                </tr>
                <tr>
                    <td>
                       <asp:Image ID="imgWin" runat="server"  />
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:HiddenField ID="HiddenField2" runat="server" />
            <asp:HiddenField ID="HiddenField3" runat="server" />
        </div>
    </form>
</body>
</html>
