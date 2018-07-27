<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Roulette.aspx.cs" ClientIDMode="Static" Inherits="SU_Casino.Roulette" %>

    <!DOCTYPE html>

    <html xmlns="http://www.w3.org/1999/xhtml">

    <head runat="server">
        <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no">
        <title>Roulette</title>
        <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.0/css/bootstrap.min.css" integrity="sha384-9gVQ4dYFwwWSjIDZnLEWnxCjeSWFphJiwGPXr1jddIhOegiu1FwO5qRGvFXOdJZ4" crossorigin="anonymous"/>
        <link rel="stylesheet" href="/src/css/bootstrap.css" />
        <link rel="stylesheet" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.14/themes/ui-lightness/jquery-ui.css" />
        <link rel="stylesheet" href="/src/css/bootstrap-responsive.css" />
        <link rel="stylesheet" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.9.0/themes/ui-lightness/jquery-ui.css" />
        <link rel="stylesheet" href="src/css/index.css"/>
        <link rel="stylesheet" href="/src/css/roulette_wheel_base.css" />
        <link rel="stylesheet" href="/src/css/roulette_wheel.css" />
    </head>

    <body id="roulette">
   
        <div class="container">
            <div class="first"></div>
            <div class="row">
                <div class="col-md-2 col-xs-0"></div>
                <div class="col-md-10 col-xs-12" id="roulette-wrapper">
                
                    <div class="spinner-container">
                        <div class="spinner text-center">
                            <div class="ball">
                                <span></span>
                            </div>
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
                    <div class="btn_container">
                        <h3> Choose a color: </h3>
                        <ul id="roulette-ul">
                            <li><button id="btnBlack" class="btn round-button spinnButton">BLACK</button></li>
                            <li><button id="btnRed" class="btn round-button spinnButton"> RED </button></li>
                        </ul>
                        <form id="form1" runat="server">
                            
                            
                            <asp:HiddenField ID="HiddenFieldrouletteNr" runat="server" />
                            <asp:HiddenField ID="Hiddenfield_text" runat="server" />
                            <asp:HiddenField ID="HiddenField_credit" runat="server" />
                            <asp:HiddenField ID="HiddenField_showInfo" runat="server" />
                            <asp:HiddenField ID="HiddenField_result" runat="server" />
                            <asp:Button ID="btnPlay" runat="server" OnClick="btnPlay_Click" Text="Play again!" class="hidden" />
                            <div id="moneyLable">Credit left:&nbsp; 
                               <span class="cash-sum"> 
                                  <asp:Label ID="lblMoney" runat="server"> </asp:Label>
                               </span>
                            </div>
                        </form>
                        
                    </div>
                </div>
                <div class="col-md-2 col-xs-0"></div>
            </div>
            <div class="row">
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
                    <li id="btnHigh" class="winchance-btn">High (Q)</li>
                    <li id="btnLow" class="winchance-btn">Low (W)</li>
                    <li id="btnZero" class="winchance-btn">Zero (E)</li>
                    <li id="btnDontKnow" class="winchance-btn">Don't know (R)</li>
                </ul>
            </div>
        </div>
    </div>
         <!-- Start info: Information about the game, visible first time set by hidden field 'HiddenField_showInfo' -->
            <div id="startInfo" class="container-fluid overlayer">
                 <div class="row">
                        <div class="col-md-2"></div>
                            <div class="col-md-8 text-center div-center" id="message-content">
                                <div class="info">
                                    <section>
                                        <h1> LETS PLAY ROULETTE </h1>
                                        <p id="introInfoText">
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
        <!-- Winn or Lost: Shows ....  -->
                  <div id="message-container" class="container-fluid overlayer  winner-content">
                    <div class="row">
                        <div class="col-md-2"></div>
                            <div class="col-md-8 text-center" id="message-content-win">
                                <div class="winner">
                                    <!--div class="winner-inner"></div>
                                    <h1> WINNER!!!! </h1-->
                                    <img src="src/images/other/winntext2.png" class="img-responsive" />
                                    <h2><span class="winSpan"> You got <span id="winCredit">+100</span> !!!!</span></h2>
                                  
                                     <button id="btnCloseWin" class="btn btn-large btn-primary" > Play again </button>
                                        
                                </div>
                        </div>
                    <div class="col-md-2"></div>
                </div>
            </div>
         <!-- End: Winn or Lost  -->
     
        <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.0/jquery.min.js"></script>
        <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.11.0/jquery-ui.min.js"></script>
        <script src="/src/js/jquery.fireworks-js/jquery.fireworks.js"></script>
        <script src='/src/js/jquery.keyframes.mini.js'></script>
        <script src="/src/js/index.js"></script>
         <script src="/src/js/roulette_wheel.js"></script>
    </body>

    </html>