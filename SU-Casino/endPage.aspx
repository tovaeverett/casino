<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EndPage.aspx.cs" Inherits="SU_Casino.endPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>End</title>
     <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.0/css/bootstrap.min.css" />
     <link rel="stylesheet" href="src/css/index.css"/>
</head>
<body id="end">
    <div class="container-fluid smallDev">
        <form id="form1" runat="server">
            <div id="introInfo">
             <div class="row">
                 <div class="col-xs-1 col-md-2 col-xl-2"></div>
                  <div id="intro" class="col-xs-10 col-md-8 col-xl-8">
                     <section>
                         <h1>End </h1>
                         <div id="introInfoTextDiv">
                          <p id="introInfoText">

                              <!----TEXT FROM DB ---->

                          </p>
                             <asp:Button ID="btnStart" runat="server"  Text="Submit" class="btn btn-large btn-primary" OnClick="btnStart_Click" />
                      </div>
                </section>  
             </div>
            <div class="col-xs-1 col-md-2 col-xl-2"></div>
            <asp:hiddenfield ID="hiddenfield_text" runat="server"></asp:hiddenfield>
            <asp:hiddenfield ID="hiddenfield_userid" runat="server"></asp:hiddenfield>
          </div>
       </div>
    </form>
  </div>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.0/jquery.min.js"></script>
    <script src="src/js/end.js"></script>
</body>
</html>
