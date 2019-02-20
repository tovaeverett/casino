<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CardDraw.aspx.cs" Inherits="SU_Casino.CardDraw" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no"/>
    <link rel="stylesheet" href="src/css/bootstrap.css"/>
    <!--link rel="stylesheet" href="src/css/bootstrap-responsive.css"/-->
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.0/css/bootstrap.min.css" />
	<!--link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.9.0/themes/ui-lightness/jquery-ui.css"/-->
	<link rel="stylesheet" href="src/css/card_game.css"/>
    <link rel="stylesheet" href="src/css/index.css"/>
    <link rel="stylesheet" href="/src/css/animate.css" />
    <link rel="stylesheet" href="/src/css/textShadowBorder.css" />
   <!-- *****Random themes *****-->
   
       <%  var theme = this.HiddenField_theme.Value;
        switch (theme) {
             case "null":%>
                <link rel="stylesheet" href="src/css/noTheme.css" />
              <%   break;
             case "0":%>
                <link rel="stylesheet" href="src/css/noTheme.css" />
              <%   break;
            case "1":%>
                <link rel="stylesheet" href="src/css/themeRed.css" />
              <%   break;
            case "2":%>
                <link rel="stylesheet" href="src/css/themeBlue.css" />
                <% break;
            case "3":%>
                <link rel="stylesheet" href="src/css/themeGold.css" />
               <%  break;
            case "4":%>
                <link rel="stylesheet" href="src/css/themeBlack.css" />
              <%   break;
            case "5":%>
                <link rel="stylesheet" href="src/css/themeRedB.css" />
              <%   break;
            case "6":%>
                <link rel="stylesheet" href="src/css/themeRedC.css" />
              <%   break;
            default:%>
                <link rel="stylesheet" href="src/css/themeGold.css" />
               <%  break;
         }%>
        
</head>
<body id="cardV">
    <div id="main-container" class="container-fluid">
        <div class="row header">
        </div>
        <div class="row playground">
            <div class="col-md-2 col-sm-1 col-xl-2"></div>
            <div class="col-xl-8 col-md-8 col-sm-10" id="play-container">
                 <div class="col-md-12 playground-cards">
                     <div class="div-center">
                            <!--**CARDS**-->
                            <div class="col-md-3 cardSpan">
					            <div class="scene scene--card">
	  					            <div class="betCard" id="betCard1">
						                <div class="card__face card__face--front"><img src="src/images/cards/blue_back_1.png"/></div>
						                <div class="card__face card__face--back"><img id="imgCard1" src="src/images/cards/3C.png"/></div>
	  					            </div>
				 	            </div>
				             </div>
				             <div class="col-md-3 cardSpan">
 					            <div class="scene scene--card">
   				                    <div class="betCard" id="resultCard">
 					    	            <div class="card__face card__face--front"><img id="imgCard3" src="src/images/cards/3C.png"/></div>
   						            </div>
 				 	            </div>
    		                </div>
                            <div class="col-md-3 cardSpan">
				                <div class="scene scene--card">
	  					            <div class="betCard" id="betCard2">
						                <div class="card__face card__face--front"><img src="src/images/cards/red_back_1.png"/></div>
						                <div class="card__face card__face--back"><img id="imgCard2" src="src/images/cards/2C.png"/></div>
	  					            </div>
					             </div>
                            </div>
                          </div>
                        <!--**END: CARDS**-->
                        </div>
                        <!--*** PANEL for buttons and credit***-->
                        <div class="col-md-12 col-sm-12 game-panel">
                            <form id="formCards" runat="server">
                               
                                <asp:HiddenField ID="HiddenField_card1" runat="server" Value="5"/>
                                <asp:HiddenField ID="HiddenField_card2" runat="server" Value="3"/> 
                                <asp:HiddenField ID="HiddenField_card3" runat="server" Value="3"/> 
                                <asp:HiddenField ID="HiddenField_WinLose" runat="server" />
                                <asp:HiddenField ID="HiddenField_theme" runat="server" />
                                <asp:HiddenField ID="HiddenField_showInfo" runat="server" />
                                <asp:HiddenField ID="HiddenField_win1" runat="server" />
                                <asp:HiddenField ID="HiddenField_win2" runat="server" />
                                <asp:HiddenField ID="HiddenField_result" runat="server" />
                                <asp:HiddenField ID="Hiddenfield_text" runat="server" />
                                <asp:HiddenField ID="HiddenField_game" runat="server" />
                                <asp:HiddenField ID="HiddenField_currentBalance" runat="server" />
                                <asp:HiddenField ID="HiddenField_Trail" runat="server" />
                                <asp:HiddenField ID="HiddenField_Time1" runat="server" />
                                <asp:HiddenField ID="HiddenField_Time2" runat="server" />
                                <asp:HiddenField ID="HiddenField_Time3" runat="server" />
                                <asp:HiddenField ID="HiddenField_Bet_Card1" runat="server" />
                                <asp:HiddenField ID="HiddenField_Bet_Card2" runat="server" />
                                <div class="combined slower" id="currentBet"></div>
                                <asp:Button ID="btnPlay" runat="server" OnClick="btnPlay_Click" Text="Play!" class="hidden" />
                                <div id="panel1">
                                    <div id="moneyLable">Credits left:&nbsp; 
                                        <span class="cash-sum"> 
                                            <asp:Label ID="lblMoney" runat="server"> </asp:Label>
                                        </span>
                                    </div>
                                </div>
                                <div id="panel2">
                                </div>
                            </form> 
                            </div>
                        </div>
                   <div class="col-md-2 col-sm-1"></div>
                 </div>
    </div>
    <!-- Winning chance: Shows before every game. The value are saved to backend in  -->
    <div id="winchance-container" class="container-fluid overlayer">
        <div class="row text-center" >
           
            <div class="winchance-div">
                 <h2>Which deck gives most profit?</h2>
                <br />
                <ul>
                    <li id="btnHigh" class="winchance-btn">Blue deck</li>
                    <li id="btnLow" class="winchance-btn">Red deck</li>
                    <!--li id="btnZero" class="winchance-btn">Zero (E)</!--li>
                    <li id="btnDontKnow" class="winchance-btn">Don't know (R)</li-->
                </ul>
            </div>
        </div>
    </div>
     <!-- END: Winning chance --> 
   <!-- Start info: Information about the game, visible first time set by hidden field 'HiddenField_showInfo' -->
            <div id="startInfo" class="container-fluid overlayer info-content">
                 <div class="row">
                        <div class="col-md-2"></div>
                            <div class="col-md-8 text-center div-center" id="message-content">
                                <div class="info">
                                    <section>
                                        <h1> Let’s play cards! </h1>
                                        <p id="introInfoText">
                                        Lorem ipsum dolor sit amet, sea mundi ponderum neglegentur ex, at munere delicata cum. 
                                        Inani choro per ex, equidem debitis et pro, sea an ludus omnium. Putent commune omnesque no ius, 
                                        ad hinc everti qui. At modus decore sit. Omnes vivendo propriae eu pri, ut alii esse percipitur eos, 
                                        eu est nibh assentior. Impetus legendos duo an.
                                        </p>
                                        <button id="btnShowInfo"class="btn btn-large btn-primary"> Play! </button>
                                    </section>
                                </div>
                                
                            
                        </div>
                    <div class="col-md-2"></div>
            </div>
        </div>
        <!-- END: Start info -->   
        <!-- Win or Lost: Shows ....  -->
                  <div id="message-container" class="container-fluid overlayer  winner-content">
                    <div class="row">
                        <div class="col-md-2"></div>
                            <div class="col-md-8 text-center" id="message-content-win">
                                <div class="winner">
                                   <img src="src/images/other/wintext.png" class="img-responsive" />
                                    
                                    <h2><span class="winSpan"> You won <span id="winCredit"></span> !
                                        <span id="piggySpan"><img src="src/images/other/piggy-bank-icon.png"/> <br />
                                       <b> The price has been saved in a piggy bank and you can't gamble with it </b>
                                        </span>
                                        </span>
                                        
                                    </h2>
                                    
                                </div>
                        
                            <button id="btnCloseWin" class="btn btn-large btn-primary" > Play! </button>
                        </div>
                    <div class="col-md-2"></div>
                </div>
            </div>
         <!-- End: Win or Lost  -->
                    
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.0/jquery.min.js"></script>
     <script src="/src/js/jquery.fireworks-js/jquery.fireworks.js"></script>
    <script src="src/js/index.js"></script>
    <script src="src/js/card_game.js"></script>
    
</body>
</html>
