<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CardDraw2.aspx.cs" Inherits="SU_Casino.CardDraw" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no"/>
    <link rel="stylesheet" href="src/css/bootstrap.css"/>
    <link rel="stylesheet" href="src/css/bootstrap-responsive.css"/>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.0/css/bootstrap.min.css" integrity="sha384-9gVQ4dYFwwWSjIDZnLEWnxCjeSWFphJiwGPXr1jddIhOegiu1FwO5qRGvFXOdJZ4" crossorigin="anonymous"/>
	<!--link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.9.0/themes/ui-lightness/jquery-ui.css"/-->
	<link rel="stylesheet" href="src/css/card_game.css"/>
    <link rel="stylesheet" href="src/css/index.css"/>
    <link rel="stylesheet" href="src/css/card_game2.css"/>
   <!-- *****Random themes *****-->
    <!-- behövs den här triggern ??? -->
       <%  var theme = this.HiddenField_theme.Value;
        switch (theme) {
             case "null":%>
                <link rel="stylesheet" href="src/css/noTheme.css" />
              <%   break;
            case "1":%>
                <link rel="stylesheet" href="src/css/themeBlack.css" />
              <%   break;
            case "2":%>
                <link rel="stylesheet" href="src/css/themeBlue.css" />
                <% break;
            case "3":%>
                <link rel="stylesheet" href="src/css/themeRed.css" />
               <%  break;
            case "4":%>
                <link rel="stylesheet" href="src/css/themeGold.css" />
              <%   break;
            default:%>
                <link rel="stylesheet" href="src/css/themeGold.css" />
               <%  break;
         }%>
        
</head>
<body id="cardH">
    <div id="main-container" class="container-fluid">
        <div class="row header">
        </div>
        <div class="row playground">
            <div class="col-md-2 col-sm-1 col-xl-2"></div>
            <div class="col-xl-8 col-md-8 col-sm-10" id="play-container">
                 <div class="col-md-12 playground-cards">
                     <div class="div-center">
                         
                            <div class="col-md-3 cardSpan"> </div>
				             <div class="col-md-3 cardSpan">
 					            <div class="scene scene--card">
	  					            <div class="betCard" id="betCard1">
						                <div class="card__face card__face--front"><img class="rotate" src="src/images/cards/purple_back0.png"/></div>
						                <div class="card__face card__face--back"><img class="rotate" id="imgCard1" src="src/images/cards/3C.png"/></div>
	  					            </div>
				 	            </div>
                          <div class="scene scene--card">
   				                    <div class="betCard" id="resultCard">
 					    	            <div class="card__face card__face--front"><img class="rotate" id="imgCard3" src="src/images/cards/3C.png"/></div>
   						            </div>
 				 	            </div>
                         <div class="scene scene--card">
	  					            <div class="betCard" id="betCard2">
						                <div class="card__face card__face--front"><img class="rotate" src="src/images/cards/orange_back0.png"/></div>
						                <div class="card__face card__face--back"><img class="rotate" id="imgCard2" src="src/images/cards/2C.png"/></div>
	  					            </div>
					             </div>
    		                </div>
                            <div class="col-md-3 cardSpan">
				                
                            </div>
                      </div>
                     
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
                                <asp:HiddenField ID="HiddenField_bet1" runat="server" />
                                <asp:HiddenField ID="HiddenField_bet2" runat="server" />
                                <asp:HiddenField ID="HiddenField_result" runat="server" />
                                <asp:Button ID="btnPlay" runat="server" OnClick="btnPlay_Click" Text="Play again!" class="hidden" />
                                <div id="panel1">
                                    <div id="moneyLable">Credit left:&nbsp; 
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
                 <h2>What are your chances of winning?</h2>
                <br />
                <ul>
                    <li id="btnHigh" class="winchance-btn">High (Q)</li>
                    <li id="btnLow" class="winchance-btn">Low (W)</li>
                    <li id="btnZero" class="winchance-btn">Zero (E)</li>
                    <li id="btnDontKnow" class="winchance-btn">Don't know (R)</li>
                </ul>
            </div>
        </div>
    </div>
     <!-- END: Winning chance --> 
   <!-- Start info: Information about the game, visible first time set by hidden field 'HiddenField_showInfo' -->
            <div id="startInfo" class="container-fluid overlayer">
                 <div class="row">
                        <div class="col-md-2"></div>
                            <div class="col-md-8 text-center div-center" id="message-content">
                                <div class="info">
                                    <section>
                                        <h1> LETS PLAY CARDS </h1>
                                        <p>
                                        Lorem ipsum dolor sit amet, sea mundi ponderum neglegentur ex, at munere delicata cum. 
                                        Inani choro per ex, equidem debitis et pro, sea an ludus omnium. Putent commune omnesque no ius, 
                                        ad hinc everti qui. At modus decore sit. Omnes vivendo propriae eu pri, ut alii esse percipitur eos, 
                                        eu est nibh assentior. Impetus legendos duo an.
                                        </p>
                                        <button id="btnShowInfo"class="btn btn-large btn-primary"> Start to play </button>
                                    </section>
                                </div>
                                
                            
                        </div>
                    <div class="col-md-2"></div>
            </div>
        </div>
        <!-- END: Start info -->   
        <!-- Win or Lost: Shows ....  -->
                  <div id="message-container" class="container-fluid overlayer winner-content">
                    <div class="row">
                        <div class="col-md-2"></div>
                            <div class="col-md-8 text-center" id="message-content">
                                <div class="winner">
                                   <img src="src/images/other/winntext2.png" class="img-responsive" />
                                    <h2><span class="winSpan"> You got <span id="winCredit">+100</span> !!!!</span></h2>
                                </div>
                            <button id="btnCloseWin" class="btn btn-large btn-primary" > Play again </button>
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
