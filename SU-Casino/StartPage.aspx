<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StartPage.aspx.cs" Inherits="SU_Casino.StartPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Start</title>
     <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.0/css/bootstrap.min.css" />
    <link rel="stylesheet" href="src/css/index.css"/>
    <script>
        function ValidateCheckBox(sender, args) {
            args.IsValid = false;
            $("#" + sender.id).parent().find(":checkbox").each(function () {
                if ($(this).attr("checked")) {
                    args.IsValid = true;
                    return;
                }
            });
        }
    </script>
</head>
<body class="bodyStart" id="start">
    <div class="container smallDev">
        <header></header>
    </div>
    <div class="container-fluid smallDev">
      <form id="form1" runat="server">
         <div id="introInfo">
         <div class="row">
             <div class="col-xs-1 col-sm-1 col-xl-2"></div>
              <div id="intro" class="col-xs-10 col-sm-10 col-xl-8">
                 <section>
                     <h1>Welcome to SU Casino</h1>
                     <div id="introInfoTextDiv">
                      <p id="introInfoText">

                          <!----TEXT FROM DB ---->

                      </p>
                  </div>
                </section>  
               </div>
               <div class="col-xs-1 col-sm-1 col-xl-2"></div>
               <asp:hiddenfield ID="hiddenfield_text" runat="server"></asp:hiddenfield>
               <asp:hiddenfield ID="hiddenfield_showInfo" runat="server"></asp:hiddenfield>
               <asp:hiddenfield ID="hiddenfield_userid" runat="server"></asp:hiddenfield>
               <asp:hiddenfield ID="hiddenfield_device" runat="server"></asp:hiddenfield>
               <asp:HiddenField ID="hiddenfield_startCredit" runat="server" />
               <asp:HiddenField ID="hiddenfield_assignmentId" runat="server" />
               <asp:HiddenField ID="hiddenfield_hitId" runat="server" />
               <asp:HiddenField ID="hiddenfield_turkSubmitTo" runat="server" />
          </div>
           <div id="form" class="row">
              <div class="col-xs-1 col-sm-1 col-xl-2"></div>
              <div class="col-xs-10 col-sm-10 col-xl-8 question-container">
                  <section class="questionInfoBox">
                      <h4> TAKE YOUR CLIENT THROUGH THE PGSI QUIZ</h4>
                  </section>
                <div class="form-group question">
                    <fieldset>
                        <legend>Have you bet more than you could really afford to lose?</legend>
                        <asp:RadioButtonList RepeatDirection="Horizontal" CssClass="radioButtonList" ID="q1" runat="server">
                            <asp:ListItem Value="0">Never</asp:ListItem>
                            <asp:ListItem Value="1">Sometimes</asp:ListItem>
                            <asp:ListItem Value="2">Most of the time</asp:ListItem>
                            <asp:ListItem Value="3">Always</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please answer this question." ControlToValidate="q1" CssClass="validationError" Font-Bold="True" SetFocusOnError="True" ForeColor="Red"></asp:RequiredFieldValidator>
                     </fieldset>
                </div>
                <div class="form-group question">
                    <fieldset>
                        <legend>Have you needed to gamble with larger amounts of money to get the same feeling of excitement?</legend>
                        <asp:RadioButtonList RepeatDirection="Horizontal" CssClass="radioButtonList" ID="q2" runat="server">
                            <asp:ListItem Value="0">Never</asp:ListItem>
                            <asp:ListItem Value="1">Sometimes</asp:ListItem>
                            <asp:ListItem Value="2">Most of the time</asp:ListItem>
                            <asp:ListItem Value="3">Always</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please answer this question." ControlToValidate="q2" CssClass="validationError" Font-Bold="True" SetFocusOnError="True" ForeColor="Red"></asp:RequiredFieldValidator>
                     </fieldset>
                </div>
                  <div class="form-group question">
                    <fieldset>
                        <legend>Have you gone back on another day to try to win back the money you lost?</legend>
                        <asp:RadioButtonList RepeatDirection="Horizontal" CssClass="radioButtonList" ID="q3" runat="server">
                            <asp:ListItem Value="0">Never</asp:ListItem>
                            <asp:ListItem Value="1">Sometimes</asp:ListItem>
                            <asp:ListItem Value="2">Most of the time</asp:ListItem>
                            <asp:ListItem Value="3">Always</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please answer this question." CssClass="validationError" Font-Bold="True" SetFocusOnError="True" ForeColor="Red" ControlToValidate="q3"></asp:RequiredFieldValidator>
                     </fieldset>
                </div>
                 <div class="form-group question">
                    <fieldset>
                        <legend>Have you borrowed money or sold anything to gamble?</legend>
                        <asp:RadioButtonList RepeatDirection="Horizontal" CssClass="radioButtonList" ID="q4" runat="server">
                            <asp:ListItem Value="0">Never</asp:ListItem>
                            <asp:ListItem Value="1">Sometimes</asp:ListItem>
                            <asp:ListItem Value="2">Most of the time</asp:ListItem>
                            <asp:ListItem Value="3">Always</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please answer this question." CssClass="validationError" Font-Bold="True" SetFocusOnError="True" ForeColor="Red" ControlToValidate="q4"></asp:RequiredFieldValidator>
                     </fieldset>
                </div>
                <div class="form-group question">
                    <fieldset>
                        <legend>Have you felt that you might have a problem with gambling?</legend>
                        <asp:RadioButtonList RepeatDirection="Horizontal" CssClass="radioButtonList" ID="q5" runat="server">
                            <asp:ListItem Value="0">Never</asp:ListItem>
                            <asp:ListItem Value="1">Sometimes</asp:ListItem>
                            <asp:ListItem Value="2">Most of the time</asp:ListItem>
                            <asp:ListItem Value="3">Always</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please answer this question." CssClass="validationError" Font-Bold="True" SetFocusOnError="True" ForeColor="Red" ControlToValidate="q5"></asp:RequiredFieldValidator>
                     </fieldset>
                </div>
                  <div class="form-group question">
                    <fieldset>
                        <legend>Have people criticised your betting or told you that you had a gambling problem, whether or not you thought it was true?</legend>
                        <asp:RadioButtonList RepeatDirection="Horizontal" CssClass="radioButtonList" ID="q6" runat="server">
                            <asp:ListItem Value="0">Never</asp:ListItem>
                            <asp:ListItem Value="1">Sometimes</asp:ListItem>
                            <asp:ListItem Value="2">Most of the time</asp:ListItem>
                            <asp:ListItem Value="3">Always</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Please answer this question." CssClass="validationError" Font-Bold="True" SetFocusOnError="True" ForeColor="Red" ControlToValidate="q6"></asp:RequiredFieldValidator>
                     </fieldset>
                </div>
                   <div class="form-group question">
                    <fieldset>
                        <legend>Have you felt guilty about the way you gamble or what happens when you gamble?</legend>
                        <asp:RadioButtonList RepeatDirection="Horizontal" CssClass="radioButtonList" ID="q7" runat="server">
                            <asp:ListItem Value="0">Never</asp:ListItem>
                            <asp:ListItem Value="1">Sometimes</asp:ListItem>
                            <asp:ListItem Value="2">Most of the time</asp:ListItem>
                            <asp:ListItem Value="3">Always</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Please answer this question." CssClass="validationError" Font-Bold="True" SetFocusOnError="True" ForeColor="Red" ControlToValidate="q7"></asp:RequiredFieldValidator>
                     </fieldset>
                </div>
                <div class="form-group question">
                    <fieldset>
                        <legend>Has gambling caused you any health problems, including stress or anxiety?</legend>
                        <asp:RadioButtonList RepeatDirection="Horizontal" CssClass="radioButtonList" ID="q8" runat="server">
                            <asp:ListItem Value="0">Never</asp:ListItem>
                            <asp:ListItem Value="1">Sometimes</asp:ListItem>
                            <asp:ListItem Value="2">Most of the time</asp:ListItem>
                            <asp:ListItem Value="3">Always</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Please answer this question." CssClass="validationError" Font-Bold="True" SetFocusOnError="True" ForeColor="Red" ControlToValidate="q8"></asp:RequiredFieldValidator>
                     </fieldset>
                </div>
                  <div class="form-group question">
                    <fieldset>
                        <legend>Has your gambling caused any financial problems for you or your household?</legend>
                        <asp:RadioButtonList RepeatDirection="Horizontal" CssClass="radioButtonList" ID="q9" runat="server">
                            <asp:ListItem Value="0">Never</asp:ListItem>
                            <asp:ListItem Value="1">Sometimes</asp:ListItem>
                            <asp:ListItem Value="2">Most of the time</asp:ListItem>
                            <asp:ListItem Value="3">Always</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Please answer this question." CssClass="validationError" Font-Bold="True" SetFocusOnError="True" ForeColor="Red" ControlToValidate="q9"></asp:RequiredFieldValidator>
                     </fieldset>
                </div>
                   <div class="form-group question">
                    <fieldset>
                        <legend>What is your sex?</legend>
                        <asp:RadioButtonList RepeatDirection="Horizontal" CssClass="radioButtonList" ID="q10" runat="server">
                            <asp:ListItem Value="0">Female</asp:ListItem>
                            <asp:ListItem Value="1">Male </asp:ListItem>
                            <asp:ListItem Value="2">Other/Prefer not to say</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Please answer this question." CssClass="validationError" Font-Bold="True" SetFocusOnError="True" ForeColor="Red" ControlToValidate="q10"></asp:RequiredFieldValidator>
                     </fieldset>
                </div>
                  <div class="form-group question">
                    <fieldset>
                        <legend>What is your age? </legend>
                        <asp:TextBox  CssClass="radioButtonList" ID="q11" runat="server"/>
                     </fieldset>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Please answer this question." CssClass="validationError" Font-Bold="True" SetFocusOnError="True" ForeColor="Red" ControlToValidate="q11"></asp:RequiredFieldValidator>
                </div>
                  <section class="questionInfoBox">
                       <h5> The following questions concern gambling, by which we mean any activity where money (or something of monetary value) is waged on an uncertain outcome governed (to some extent) by chance, with the primary aim of winning more money (or something greater monetary value).</h5>

                  </section>
                
                <div class="form-group question">
                    <fieldset>
                        <legend>
                                Please indicate which forms of gambling that you have engaged in the last 12 months <br /> <i>( Multiple response options are possible )</i>:
                        </legend>
                        <asp:CheckBoxList  CssClass="radioButtonList" ID="q12" runat="server">
                            <asp:ListItem Value="0">Lottery</asp:ListItem>
                            <asp:ListItem Value="1">Sports betting</asp:ListItem>
                            <asp:ListItem Value="2">Horse or other race betting</asp:ListItem>
                            <asp:ListItem Value="3">Card games (e.g. poker, black jack, rummy)</asp:ListItem>
                            <asp:ListItem Value="4">Casino slot machine</asp:ListItem>
                            <asp:ListItem Value="5">Festival gambling</asp:ListItem>
                            <asp:ListItem Value="6">Dice games</asp:ListItem>
                            <asp:ListItem Value="7">Online lottery </asp:ListItem>
                            <asp:ListItem Value="8">Online betting</asp:ListItem>
                            <asp:ListItem Value="9">Online card games</asp:ListItem>
                            <asp:ListItem Value="10">Online slot machines</asp:ListItem>
                            <asp:ListItem Value="11">Other</asp:ListItem>
                        </asp:CheckBoxList>
                        <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Please answer this question." CssClass="validationError" Font-Bold="True" ClientValidationFunction = "ValidateCheckBox" OnServerValidate="CustomValidator1_ServerValidate" SetFocusOnError="True"></asp:CustomValidator>
                     </fieldset>
                </div>
                  <div class="form-group question">
                    <fieldset>
                        <legend>How often have you gambled on average in the last 12 months?</legend>
                        <asp:RadioButtonList  CssClass="radioButtonList" ID="q13" runat="server">
                            <asp:ListItem Value="0">Not at all</asp:ListItem>
                            <asp:ListItem Value="1">A few times</asp:ListItem>
                            <asp:ListItem Value="2">Once a month</asp:ListItem>
                            <asp:ListItem Value="3">Once per week</asp:ListItem>
                            <asp:ListItem Value="4">Once per day or more often</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="Please answer this question." CssClass="validationError" Font-Bold="True" SetFocusOnError="True" ForeColor="Red" ControlToValidate="q13"></asp:RequiredFieldValidator>
                     </fieldset>
                </div>
                  <div class="text-center">
                   <asp:Button ID="btnPlay" runat="server"  Text="Send information" class="btn btn-large btn-primary" OnClick="btnPlay_Click"/>
                  <br />
                  </div>
             </div>
            <div class="col-xs-1 col-sm-1 col-xl-2"></div>
        </div>
     </div>
     <div class="row" id="startPlay">
        <div class="q"></div>
        <div id="startPlayContent">
              <h1>Time to start to play!</h1>
            <p class="countCredit"> Here is your start credit:<br/><span id="value">0</span></p>
            <asp:Button ID="btnStart" runat="server"  Text="Start to play" class="btn btn-large btn-primary" OnClick="btnStart_Click" />
        </div>  
        <div class="q"></div>
     </div>
    </form> 
  </div>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.0/jquery.min.js"></script>
    <script src="src/js/start.js"></script>
    <script type="text/javascript">
    function WebForm_OnSubmit() {
        if (typeof (ValidatorOnSubmit) == "function" && ValidatorOnSubmit() == false) {
            for (var i in Page_Validators) {
                try {
                    if (!Page_Validators[i].isvalid) {
                        var control = $("#" + Page_Validators[i].controltovalidate);
 
                        var top = control.offset().top;
                        $('html, body').animate({ scrollTop: top - 10 }, 800);
                        control.focus();
                        return;
                    }
                } catch (e) { }
            }
            return false;
        }
        return true;
    }
</script>
</body>
</html>

