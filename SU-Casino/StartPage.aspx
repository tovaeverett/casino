<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StartPage.aspx.cs" Inherits="SU_Casino.StartPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Start</title>
     <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.0/css/bootstrap.min.css" integrity="sha384-9gVQ4dYFwwWSjIDZnLEWnxCjeSWFphJiwGPXr1jddIhOegiu1FwO5qRGvFXOdJZ4" crossorigin="anonymous"/>
    <link rel="stylesheet" href="src/css/index.css"/>
</head>
<body class="bodyStart">
    <div class="container">
        <header></header>
    </div>
    <div class="container">
      <form id="form1" runat="server">
         <div id="introInfo">
         <div class="row">
             <div class="col-xs-1 col-md-2 col-xl-2"></div>
              <div id="intro" class="col-xs-10 col-md-8 col-xl-8">
                    <h1>Welcome to SU Casino</h1>
                  <div id="introInfoTextDiv">
                      <p id="introInfoText">


                      </p>
                  </div>
               </div>
               <div class="col-xs-1 col-md-2 col-xl-2"></div>
               <asp:hiddenfield ID="hiddenfield_text" runat="server"></asp:hiddenfield>
               <asp:hiddenfield ID="hiddenfield_showInfo" runat="server"></asp:hiddenfield>
          </div>
          <div id="form" class="row">
              <div class="col-md-2 col-xl-2"></div>
              <div class="col-md-8 col-xl-8">
                <div class="form-group">
                    <label for="TextBox2">Fill in..</label>
                    <asp:TextBox class="form-control" ID="TextBox2" runat="server"></asp:TextBox> 
                    <small id="TextBox2Help" class="form-text text-muted"></small>
                </div>
                   <asp:Button ID="btnPlay" runat="server"  Text="Send information" class="btn btn-large btn-primary" OnClick="btnPlay_Click"/>
             </div>
            <div class="col-md-2 col-xl-2"></div>
        </div>
     </div>
     <div class="row" id="startPlay">
        <div class=""></div>
        <div id="startPlayContent" class="">
              <h1>Time to start to play!</h1>
            <p class="countCredit"> Here is your start credit:<br/><span id="value">0</span></p>
            <asp:Button ID="btnStart" runat="server"  Text="Start to play" class="btn btn-large btn-primary" OnClick="btnStart_Click" />
        </div>  
        <div class=""></div>
     </div>
    </form> 
  </div>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.0/jquery.min.js"></script>
    <script src="src/js/start.js"></script>
</body>
</html>

