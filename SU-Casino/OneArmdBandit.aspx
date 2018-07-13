<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="~/OneArmdBandit.aspx.cs" Inherits="SU_Casino.OneArmdBandit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>Slot</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no"/>
    <link rel="stylesheet" href="src/css/bootstrap.css"/>
    <link rel="stylesheet" href="src/css/bootstrap-responsive.css"/>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.0/css/bootstrap.min.css" integrity="sha384-9gVQ4dYFwwWSjIDZnLEWnxCjeSWFphJiwGPXr1jddIhOegiu1FwO5qRGvFXOdJZ4" crossorigin="anonymous"/>
	<link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.9.0/themes/ui-lightness/jquery-ui.css"/>
	<link rel="stylesheet" href="src/css/slot.css"/>
    <link rel="stylesheet" href="src/css/index.css"/>
    <%--<link rel="stylesheet" href="src/css/theme1b.css"/>--%>
    <link id="themeCSS" runat="server" rel="stylesheet" type="text/css" /> 

    <!-- *****Random themes *****-->
       <%  var theme = this.setTheme();
        switch (theme) {
            case 1:%>
                <link rel="stylesheet" href="src/css/themeBlack.css" />
              <%   break;
            case 2:%>
                <link rel="stylesheet" href="src/css/themeGold.css" />
                <% break;
            case 3:%>
                <link rel="stylesheet" href="src/css/theme1b.css" />
               <%  break;
            case 4:%>
                <link rel="stylesheet" href="src/css/theme2.css" />
              <%   break;
            default:%>
                <link rel="stylesheet" href="src/css/theme1b.css" />
               <%  break;
         }%>
        
</head>
<body>
<%--form id="form2" runat="server">--%>
<div id="main-container" class="container-fluid">
        <div class="row header">
            <div class="col-md-2 col-sm-1"></div>
            <div class="col-md-8 col-sm-10"> CASINO </div>
            <div class="col-md-2 col-sm-1"></div>
           <!--h1> Image for theme 1<h1-->
        </div>
        <div class="row playground">
            <div class="col-xl-2 col-md-2 col-sm-1 "></div>
                <div class="col-xl-8 col-md-8 col-sm-10" id="play-container">
                    <div class="col-md-12 playground-slot">
                        <div class="div-center">
                        <div class="playground-inner">
                            <div class="col-md-3 slotSpan">
					            <div class="roulette_container" >
						            <div class="roulette1 roulette" style="display:none;">
                                        <img id="slot_1_1" src="src/images/slot/img1.png"/>
		                                <img id="slot_1_2" src="src/images/slot/img2.png"/>
		                                <img id="slot_1_3" src="src/images/slot/img3.png"/>
	                                    <img id="slot_1_4" src="src/images/slot/img4.png"/>
		                                <img id="slot_1_5" src="src/images/slot/img1.png"/>
						            </div>
					            </div>
				            </div>
				 
				            <div class="col-md-3 slotSpan">
 					            <div class="roulette_container" >
						            <div class="roulette2 roulette" style="display:none;">
							            <img id="slot_1_1" src="src/images/slot/img1.png"/>
		                                <img id="slot_1_2" src="src/images/slot/img2.png"/>
		                                <img id="slot_1_3" src="src/images/slot/img3.png"/>
	                                    <img id="slot_1_4" src="src/images/slot/img4.png"/>
		                                <img id="slot_1_5" src="src/images/slot/img1.png"/>
						            </div>
					            </div>
    		                 </div>
			        
                            <div class="col-md-3 slotSpan">
				                <div class="roulette_container" >
						            <div class="roulette3 roulette" style="display:none;">
							            <img id="slot_1_1" src="src/images/slot/img1.png"/>
		                                <img id="slot_1_2" src="src/images/slot/img2.png"/>
		                                <img id="slot_1_3" src="src/images/slot/img3.png"/>
	                                    <img id="slot_1_4" src="src/images/slot/img4.png"/>
		                                <img id="slot_1_5" src="src/images/slot/img1.png"/>
						            </div>
					            </div>
                            </div>
                        </div>
                    </div>
                    </div>
                    <div class="col-md-12 col-sm-12 game-panel">
                        <form id="form1" runat="server">
                            <button class="round-button  start">SPIN! </button>
                            <asp:Button ID="btnPlay" runat="server" OnClick="btnPlay_Click" Text="Play again!" class="hidden" />
                            <asp:HiddenField ID="HiddenField_Spin1" runat="server" />
                            <asp:HiddenField ID="HiddenField_Spin2" runat="server" />
                            <asp:HiddenField ID="HiddenField_Spin3" runat="server" />
                            <asp:HiddenField ID="HiddenField_WinLose" runat="server" />
                            <asp:HiddenField ID="HiddenField_theme" runat="server" />
                            <asp:HiddenField ID="HiddenField_showInfo" runat="server" />
                            <asp:HiddenField ID="HiddenField_credit" runat="server" />
                            <asp:HiddenField ID="HiddenField_result" runat="server" />
                            <div id="panel1">
                                <div id="moneyLable">Credit left:&nbsp; 
                                <span class="cash-sum"> 
                                    <asp:Label ID="lblMoney" runat="server"></asp:Label>  
                                </span>
                            </div>
                            </div>
                    </form> 
                    </div>
                    <div class="col-xl-2 col-md-2 col-sm-1"></div>
                </div>

        </div>
        <div class="row">
            <div class="col-md-2 col-sm-1"></div>
            
            <div class="col-md-2 col-sm-1"></div>
         </div>
          
        
            <div class="row text-center">
                
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
                    <li id="btnLow" class="winchance-btn">Low</li>
                    <li id="btnZero" class="winchance-btn">Zero</li>
                    <li id="btnDontKnow" class="winchance-btn">Don't know</li>
                </ul>
            </div>
        </div>
    </div>
     <!-- END: Winning chance --> 
   <!-- Start info: Information about the game, visible first time set by hidden field 'HiddenField_showInfo' -->
            <div id="startInfo" class="container-fluid overlayer">
                 <div class="row">
                        <div class="col-md-1 col-xs-0"></div>
                            <div class="col-md-10 col-xs-12 text-center" id="message-content-info">
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
                    <div class="col-md-1 col-xs-0"></div>
            </div>
        </div>
        <!-- END: Start info -->   
        <!-- Win or Lost: Shows ....  -->
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
                            <button id="btnCloseWin" class="btn btn-large btn-primary" > Play again </button>
                        </div>
                    <div class="col-md-2"></div>
                </div>
            </div>
         <!-- End: Win or Lost  -->


    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.0/jquery.min.js"></script>
    <script src="src/js/roulette.js"></script>
    <script src="src/js/index.js"></script>
	<script src="src/js/slot.js"></script>

    <!-------------------***********************--------------------------------->
  
</body>
</html>
