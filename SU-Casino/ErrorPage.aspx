<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="SU_Casino.ErrorPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>Error!!!</title>
     <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.0/css/bootstrap.min.css" />
     <link rel="stylesheet" href="src/css/index.css"/>
</head>
<body>
    <div class="container-fluid smallDev">
        <form id="form1" runat="server">
            <div class="row">
                <div class="col-xs-1 col-md-2 col-xl-2"></div>
                <div id="intro" class="col-xs-10 col-md-8 col-xl-8">
                    <section>
                       <p id="error">
                           Something is wrong, Go back to Start Page.
                       </p>
                       <asp:Button ID="btnStart" runat="server"  Text="StartPage" class="btn btn-large btn-primary" OnClick="btnStart_Click" />
                    </section>  
                </div>
            </div>
        </form>
    </div>
</body>
</html>
