<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CardDraw.aspx.cs" Inherits="SU_Casino.CardDraw" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no"/>
    <link rel="stylesheet" href="src/css/bootstrap.css"/>
    <link rel="stylesheet" href="src/css/bootstrap-responsive.css"/>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.0/css/bootstrap.min.css" integrity="sha384-9gVQ4dYFwwWSjIDZnLEWnxCjeSWFphJiwGPXr1jddIhOegiu1FwO5qRGvFXOdJZ4" crossorigin="anonymous"/>
	<link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.9.0/themes/ui-lightness/jquery-ui.css"/>
	<link rel="stylesheet" href="src/css/card_game.css"/>
    <link rel="stylesheet" href="src/css/index.css"/>
    <link rel="stylesheet" href="src/css/theme1.css"/>
</head>
<body>
    
    <div id="main-container" class="container-fluid">
        <div class="row header">
           <h1> image for : Casino theme 1</h1><!--img src="src/images/logos/theme1Logo.png" class="img-responsive" /-->
        </div>
        <div class="row playground">
            <div class="col-md-2 col-sm-1 "></div>
                <div class="col-md-8 col-sm-10">
                    <div class="col-md-12 playground-cards">
                    <div class="div-center">
                    <div class="col-md-3 cardSpan">
					    <div class="scene scene--card">
	  					    <div class="card" id="betCard1">
						        <div class="card__face card__face--front"><img src="src/images/cards/blue_back.png"/></div>
						        <div class="card__face card__face--back"><img id="imgCard1" src="src/images/cards/3C.png"/></div>
	  					    </div>
				 	    </div>
				     </div>
				     <!--div class="span1 notEqual"><span id="notEqualFirst">&#8800;</span><div-->
				     <div class="col-md-3 cardSpan">
 					    <div class="scene scene--card">
   				            <div class="card" id="resultCard">
 					    	    <div class="card__face card__face--front"><img id="imgCard3" src="src/images/cards/3C.png"/></div>
   						    </div>
 				 	    </div>
    		        </div>
			        <!--div class="span1 notEqual"><span id="notEqualSecond">&#8800;</span> </div-->
                    <div class="col-md-3 cardSpan">
				        <div class="scene scene--card">
	  					    <div class="card" id="betCard2">
						        <div class="card__face card__face--front"><img src="src/images/cards/red_back_0.png"/></div>
						        <div class="card__face card__face--back"><img id="imgCard2" src="src/images/cards/2C.png"/></div>
	  					    </div>
					     </div>
                    </div>
                  </div>
                </div>
           </div>
           <div class="col-md-2 col-sm-1"></div>
         </div>
         <div class="row">
            <div class="col-md-2 col-sm-1"></div>
            <div class="col-md-8 col-sm-10 game-panel">
                <form id="form1" runat="server">
                    <asp:Image ID="imgWinner" Visible="false" runat="server"  />
                    <asp:HiddenField ID="HiddenField_card1" runat="server" Value="5"/>
                    <asp:HiddenField ID="HiddenField_card2" runat="server" Value="3"/> 
                    <asp:HiddenField ID="HiddenField_card3" runat="server" Value="3"/> 
                    <asp:HiddenField ID="HiddenField_WinLose" runat="server" />
                    <asp:HiddenField ID="HiddenField_Theme" runat="server" />
                    <div id="panel1">
                    <div id="moneyLable">Credit left:&nbsp; 
                        <span class="cash-sum"> 
                           <asp:Label ID="lblMoney" runat="server"> </asp:Label>
                        </span>
                    </div>
                </div>
                    <div id="panel2">
                     To play, click on the right or left card of the center card.
                    </div>
               
            </div>
            <div class="col-md-2 col-sm-1"></div>
         </div>
          
        <div id="message-container" class="container-fluid overlayer">
        <div class="row">
        <div class="col-md-2"></div>
         <div class="col-md-8 text-center" id="message-content">
                            <div class="winner">
                                <h1> WINNER!!!! </h1>
                                <h2> You got <span id="winCredit">+100</span> !!!!</h2>
                                <br />
                                <br />
                                <br />
                            </div>
                            <div class="lost">
                                <h1> You lost... </h1>
                                <h2 id="lostCredit"> -100</h2>
                                <br />
                                <br />
                                <br />
                            </div>
                        <asp:Button ID="btnPlay" runat="server" OnClick="btnPlay_Click" Text="Play again!" class="btn btn-large btn-primary" />
                        <asp:Button ID="btnQuit" runat="server" Text="Quit game" class="btn btn-large btn-primary" />
                    </div>
        
        <div class="col-md-2"></div>
        </div>
            <div class="row text-center">
                
            </div>
        </div>
              
           
    </div>
    <div id="winchance-container" class="container-fluid overlayer">
        <div class="row text-center" >
           
            <div class="winchance-div">
                 <h2>What are your chances of winning?</h2>
                <br />
                <ul>
                    <li class="winchance-btn">High</li>
                    <li class="winchance-btn">Low</li>
                    <li class="winchance-btn">Zero</li>
                    <li class="winchance-btn">Don't know</li>
                </ul>
            </div>
        </div>
    </div>
    </form> 
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.0/jquery.min.js"></script>
    <script src="src/js/card_game.js"></script>
</body>
</html>
