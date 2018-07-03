<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CardDraw.aspx.cs" Inherits="SU_Casino.CardDraw" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no"/>
    <link rel="stylesheet" href="src/css/bootstrap.css"/>
    <link rel="stylesheet" href="src/css/bootstrap-responsive.css"/>
	<link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.9.0/themes/ui-lightness/jquery-ui.css"/>
	<link rel="stylesheet" href="src/css/card_game.css"/>
    <link rel="stylesheet" href="src/css/index.css"/>
</head>
<body>
    
    <div id="main-container" class="container-fluid">
        <div class="row">
            <div class="span3">
					<div class="scene scene--card">
	  					<div class="card" id="betCard1">
						    <div class="card__face card__face--front"><img src="src/images/cards/blue_back.png"/></div>
						    <div class="card__face card__face--back"><img id="imgCard1" src="src/images/cards/3C.png"/></div>
	  					</div>
				 	</div>
				 </div>
				 <div class="span1 notEqual"><span id="notEqualFirst">&#8800;</span></div>
				 <div class="span3">
 					<div class="scene scene--card">
   				        <div class="card" id="resultCard">
 					    	<div class=""><img id="imgCard3" src="src/images/cards/3C.png"/></div>
   						</div>
 				 	</div>
    		    </div>
			    <div class="span1 notEqual"><span id="notEqualSecond">&#8800;</span> </div>
                <div class="span3">
				    <div class="scene scene--card">
	  					<div class="card" id="betCard2">
						    <div class="card__face card__face--front"><img src="src/images/cards/red_back_0.png"/></div>
						    <div class="card__face card__face--back"><img id="imgCard2" src="src/images/cards/2C.png"/></div>
	  					</div>
					 </div>
                </div>
            </div>
            <form id="form1" runat="server">
                <asp:Image ID="imgWinner" Visible="false" runat="server"  />
                <asp:HiddenField ID="HiddenField_card1" runat="server" Value="5"/>
                <asp:HiddenField ID="HiddenField_card2" runat="server" Value="3"/> 
                <asp:HiddenField ID="HiddenField_card3" runat="server" Value="3"/> 
                <asp:HiddenField ID="HiddenFieldWinLose" runat="server" />
                <asp:Label ID="lblMoney" runat="server"></asp:Label>
                <div id="message-container" class="container-fluid overlayer">
        <div class="row">
        <div class="span2"></div>
        <div class="span8 text-center" style=" background-image:url('https://www.tradeidee.nl/wp-content/uploads/2017/10/goudsparkle.gif');">
            <h1> WINNER!!!! </h1>
            <h2> You got +100 !!!!</h2>
            <asp:Button ID="btnPlay" runat="server" Text="PLAY AGAIN" />
        </div>
        <div class="span2"></div>
        </div>
    </div>
                
            </form>
    </div>
    <div id="winchance-container" class="container-fluid overlayer">
        <div class="row text-center" >
           
            <div class="winchance-div">
                 <h2>What are your chances of winning?</h2>
                <ul>
                    <li class="winchance-btn">High</li>
                    <li class="winchance-btn">Low</li>
                    <li class="winchance-btn">Zero</li>
                    <li class="winchance-btn">Don't know</li>
                </ul>
            </div>
        </div>
    </div>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.0/jquery.min.js"></script>
    <script src="src/js/card_game.js"></script>
</body>
</html>
