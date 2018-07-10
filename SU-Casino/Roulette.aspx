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
        <!--li--nk rel="stylesheet" href="/src/css/roulette_wheel.css" /-->
        <% if (this.GetCssRandom() == 0)
            { %>
            <link rel="stylesheet" href="/src/css/roulette_wheel.css" />
            <% } else { %>
                <link rel="stylesheet" href="/src/css/roulette_wheel.css" />
                <% } %>
    </head>

    <body>
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
        <div class="container">
            <div class="row">
                <div class="col-md-2 col-xs-0"></div>
                <div class="col-md-8 col-xs-12" id="roulette-wrapper">
                
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
                        <ul>
                            <li><button id="btnBlack" class="btn btn-large spinnButton">BLACK</button></li>
                            <li><button id="btnRed" class="btn btn-large spinnButton"> RED </button></li>
                        </ul>
                       Credit: <asp:Label ID="lblCredit" runat="server"></asp:Label>
                        
                    </div>
                </div>
                <div class="col-md-2 col-xs-0"></div>
            </div>
            <div class="row">
                <div class="row text-center">
                    <p>
                        
                    </p>
                </div>
            </div>
            <div id="winnerAnnouncer">
                <div class="span8">
                    <h1>YES! YOU ARE A WINNER! +100 </h1>
                </div>
            </div>
            <form id="form1" runat="server">
               
                <asp:Label ID="lblColor" runat="server"></asp:Label>
                <asp:Label ID="lblNr" runat="server"></asp:Label>
                <asp:ScriptManager runat="server" EnablePageMethods="true" />
                <asp:HiddenField ID="HiddenFieldrouletteNr" runat="server" />
                <asp:HiddenField ID="HiddenFieldWinLose" runat="server" />
            </form>
        </div>
     
        <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.0/jquery.min.js"></script>
        <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.11.0/jquery-ui.min.js"></script>
        <script src="/src/js/jquery.fireworks-js/jquery.fireworks.js"></script>
        <script src='/src/js/jquery.keyframes.mini.js'></script>
        <script src="/src/js/roulette_wheel.js"></script>
    </body>

    </html>