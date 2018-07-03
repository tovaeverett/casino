    <%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Roulette.aspx.cs" Inherits="SU_Casino.Roulette" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Roulette</title>
    <link rel="stylesheet" href="/src/css/bootstrap.css"></link>
	<link rel="stylesheet" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.14/themes/ui-lightness/jquery-ui.css">
	<link rel="stylesheet" href="/src/css/bootstrap-responsive.css"></link>
	<link rel="stylesheet" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.9.0/themes/ui-lightness/jquery-ui.css">
    <link rel="stylesheet" href="/src/css/roulette_wheel.css"></link>
</head>
<body>
	<div class="container top-pad50">
    <div class="row">
        <div class="span4">
          <div class="btn_container">
            <p>
			    <button id="btnBlack" class="btn btn-large btn-primary "> Black </button>
                <button id="btnRed" class="btn btn-large btn-primary "> Red </button>
			</p>
			<p>
				<button id="btnSpin" class="btn btn-large btn-primary start"> Spin </button>
			</p>
		</div>
        </div>
        <div class="span4">
          <div class="spinner text-center">
              <div class="ball"><span></span></div>
                <div class="platebg"></div>
                <div class="platetop"></div>
                <div id="toppart" class="topnodebox">
                  <div class="silvernode"></div>
                  <div class="topnode silverbg"></div>
                  <span class="top silverbg"></span>
                  <span class="right silverbg"></span>
                  <span class="down silverbg"></span>
                  <span class="left silverbg"></span>
                </div>
                <div id="rcircle" class="pieContainer">
                <div class="pieBackground"></div>
              </div>
            </div>
    		  </div>
          <div class="span4"></div>
        </div>
		<div class="row">
		</div>
	    <div id="winnerAnnouncer">
            <h1>YES! YOU ARE A WINNER! +20 </h1>
        </div>
    <form id="form1" runat="server">
        <!--div>
            <asp:RadioButtonList ID="RadioButtonList1" runat="server" Width="323px">
                <asp:ListItem>Black</asp:ListItem>
                <asp:ListItem>Red</asp:ListItem>
            </asp:RadioButtonList>
            <asp:Button ID="btnSpin" runat="server" Text="SPIN" OnClick="btnSpin_Click" /> 
            <asp:Label ID="lblColor" runat="server"></asp:Label>
            <asp:Label ID="lblNr" runat="server"></asp:Label>
            
            <asp:Image ID="imgWinLose" runat="server" />
        </div-->
        <asp:HiddenField ID="HiddenFieldrouletteNr" runat="server" />
         <asp:HiddenField ID="HiddenFieldWinLose" runat="server" />
    </form>
  </div>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.0/jquery.min.js"></script>
  	<script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.11.0/jquery-ui.min.js"></script>
    <script src='/src/js/jquery.keyframes.mini.js'></script>
  	<script src="/src/js/roulette_wheel.js"></script>
</body>
</html>
