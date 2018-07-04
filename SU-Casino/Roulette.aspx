﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Roulette.aspx.cs" Inherits="SU_Casino.Roulette" %>

    <!DOCTYPE html>

    <html xmlns="http://www.w3.org/1999/xhtml">

    <head runat="server">
        <title>Roulette</title>
        <link rel="stylesheet" href="/src/css/bootstrap.css"></link>
        <link rel="stylesheet" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.14/themes/ui-lightness/jquery-ui.css">
        <link rel="stylesheet" href="/src/css/bootstrap-responsive.css"></link>
        <link rel="stylesheet" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.9.0/themes/ui-lightness/jquery-ui.css">
        <% if (GetCssRandom() == 0)
            { %>
            <link rel="stylesheet" href="/src/css/roulette_wheel.css" />
            <% } else { %>
                <link rel="stylesheet" href="/src/css/roulette_wheel_1.css" />
                <% } %>
    </head>

    <body>
        <dialog id="firstShow" class="site-dialog">
            <header class="dialog-header">
                <h1 class="text-center">What are your chances of winning?</h1>
            </header>
            <div class="dialog-content">
                <div class="span12 offset8 text-center">
                    <div class="span2">
                        <button id="btnHigh" class="btn btn-large btn-primary">Q</button>
                        <h3>High</h3>
                    </div>
                    <div class="span2">
                        <button id="btnLow" class="btn btn-large btn-primary">W</button>
                        <h3>Low</h3>
                    </div>
                    <div class="span2">
                        <button id="btnZero" class="btn btn-large btn-primary">E</button>
                        <h3>Zero</h3>
                    </div>
                    <div class="span2">
                        <button id="btnDontKnow" class="btn btn-large btn-primary">R</button>
                        <h3>Don't know</h3>
                    </div>
                </div>
            </div>
        </dialog>

        <!-- <div class="container-fluid" id="firstShow">
            <div class="row-fluid">
                <h2 class="text-center">What are your chances of winning?</h2>
            </div>
            <div class="row-fluid text-center">
                <div class="span12 offset4">
                    <div class="span1">
                        <button id="btnHigh" class="btn btn-large btn-primary">Q</button>
                        <h3>High</h3>
                    </div>
                    <div class="span1">
                        <button id="btnLow" class="btn btn-large btn-primary">W</button>
                        <h3>Low</h3>
                    </div>
                    <div class="span1">
                        <button id="btnZero" class="btn btn-large btn-primary">E</button>
                        <h3>Zero</h3>
                    </div>
                    <div class="span1">
                        <button id="btnDontKnow" class="btn btn-large btn-primary">R</button>
                        <h3>Don't know</h3>
                    </div>
                </div>
            </div>
        </div> -->
        <div class="container top-pad50">
            <div class="row">
                <div class="span4 offset4">
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
                <div class="span4"></div>
            </div>
            <div class="row top-pad50">
                <div class="row">
                    <div class="span9">
                        <h2 style="display: inline;">You've selected: </h2>
                        <span style="display: inline;" id="selected-color">
                        </span>
                        <h2 id="selected-winning-chance"></h2>
                    </div>
                </div>
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
                </div>
            </div>
            <div id="winnerAnnouncer">
                <div class="span8">
                    <h1>YES! YOU ARE A WINNER! +20 </h1>
                </div>
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