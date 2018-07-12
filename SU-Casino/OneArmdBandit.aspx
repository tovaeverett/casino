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
</head>
<body>
<form id="form2" runat="server">
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
                        <%--<form id="form1" runat="server">--%>
                            <button class="round-button  start">SPIN! </button>
                            <asp:Image ID="IMGslot1" runat="server"></asp:Image>
                            <asp:Image ID="IMGslot2" runat="server"></asp:Image>
                            <asp:Image ID="IMGslot3" runat="server"></asp:Image>

                            <asp:HiddenField ID="HiddenField_Spin1" runat="server" />
                            <asp:HiddenField ID="HiddenField_Spin2" runat="server" />
                            <asp:HiddenField ID="HiddenField_Spin3" runat="server" />
                            <asp:HiddenField ID="HiddenField_WinLose" runat="server" />
                            <div id="panel1">
                                <div id="moneyLable">Credit left:&nbsp; 
                                <span class="cash-sum"> 
                                    <asp:Label ID="lblMoney" runat="server"></asp:Label>  
                                </span>
                            </div>
                            </div>
                    </div>
                    <div class="col-xl-2 col-md-2 col-sm-1"></div>
                </div>

        </div>
        <div class="row">
            <div class="col-md-2 col-sm-1"></div>
            
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
                        <asp:Button ID="btnPull" runat="server" OnClick="btnPull_Click" Text="Play again!" class="btn btn-large btn-primary" />
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
                   <li id="btnHigh" class="winchance-btn">High</li>
                    <li id="btnLow" class="winchance-btn">Low</li>
                    <li id="btnZero" class="winchance-btn">Zero</li>
                    <li id="btnDontKnow" class="winchance-btn">Don't know</li>
                </ul>
            </div>
        </div>
</div>
</form> 
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.0/jquery.min.js"></script>
    <script src="src/js/roulette.js"></script>
	<script src="src/js/slot.js"></script>

    <!-------------------***********************--------------------------------->
  
</body>
</html>
