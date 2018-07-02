<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CardDraw.aspx.cs" Inherits="SU_Casino.CardDraw" %>

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
                    <td colspan="3">
                        <asp:Label ID="lblMoney" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                            <asp:ImageButton ID="card1" runat="server" Height="340px" ImageUrl="~/Cards/TopCardQuestion.png" OnClick="card1_Click" />
                    </td>
                    <td>
                            <asp:ImageButton ID="card2" runat="server" Height="340px" ImageUrl="~/Cards/TopCardQuestion.png" OnClick="card2_Click" />
                    </td>
                    <td>
                        <asp:Image ID="imgSeperator" runat="server" ImageUrl="~/Cards/seperator.png" Height="340" />
                    </td>
                        <td>
                            <asp:ImageButton ID="card3" runat="server" Height="340px" ImageUrl="~/Cards/TopCard.png" />
                        </td>
                  </tr>
                <tr>
                   <td colspan="3">
                       <asp:Image ID="imgWinner" Visible="false" runat="server"  />
                   </td>
                </tr>
          </table>
        </div>
    </form>
</body>
</html>
