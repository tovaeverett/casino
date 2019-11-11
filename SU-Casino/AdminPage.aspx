﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminPage.aspx.cs" Inherits="SU_Casino.AdminPage"  validateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.0/css/bootstrap.min.css" />
    <link rel="stylesheet" href="src/css/index.css" />
    <style>
        td > a {
            background: #f1698d;
            padding: 0px 3px 2px 3px;
            border-radius: 4px;
            color: #fff;
            margin-bottom: 2px;
            display: inline-flex;
        }

            td > a:first-of-type {
                background: #17a2b8;
            }
    </style>
</head>
<body id="admin">
    <form id="form1" runat="server">
        <div class="container-fluid">
            <h1>Admin page</h1>
            <br />
            <h3>Text for pages</h3>
            <div>
                <p>
                    <label>Please select a page:</label><br />
                    <asp:DropDownList ID="ddlText" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                    </asp:DropDownList>
                </p>
                <label>Text for the page:</label><br />
                <asp:TextBox ID="txtText" runat="server" Height="419px" TextMode="MultiLine" Width="803px"></asp:TextBox>
            </div>
            <asp:Button ID="btnText" runat="server" OnClick="btnText_Click" Text="Update Text" />
            <asp:HiddenField ID="HiddenField1" runat="server" />
            <br />
            <br />
            <h3>Designmatris</h3>
            <asp:Button ID="btnAddRow" runat="server" OnClick="addRow_Click" Text="Add Row" />
            <asp:Button ID="btnResetMatris" runat="server" OnClick="btnResetMatris_Click" Text="Nollställ Matris" Width="212px" />
            <br />
            <br />
            <asp:GridView ID="gvMatris" runat="server" OnRowEditing="gvMatris_RowEditing" AutoGenerateColumns="False" AutoGenerateEditButton="True" AutoGenerateDeleteButton="True" OnRowUpdating="gvMatris_RowUpdating" OnRowCancelingEdit="gvMatris_RowCancelingEdit" OnRowDeleting="gvMatris_RowDeleting" CssClass="matrix table table-sm table-striped">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID="lblRowId" runat="server" Visible="false" Text='<%#Eval("RowID")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="prop_n">
                        <ItemTemplate><%#Eval("prop_n") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditProp_N" runat="server" Text='<%#Eval("prop_n") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="condition">
                        <ItemTemplate><%#Eval("condition") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditcondition" runat="server" Text='<%#Eval("condition") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="seq">
                        <ItemTemplate><%#Eval("seq") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditSeq" runat="server" Text='<%#Eval("seq") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="trials">
                        <ItemTemplate><%#Eval("trials") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditTrials" runat="server" Text='<%#Eval("trials") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="name">
                        <ItemTemplate>'<%#Eval("name") %>'</ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditName" runat="server" Text='<%#Eval("name") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="info_text">
                        <ItemTemplate><%#Eval("InfoTextType") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblInfoTextType" runat="server" Visible="False" Text='<%#Eval("InfoTextType") %>'></asp:Label>
                            <asp:DropDownList ID="ddlInfoTextType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListInfo_SelectedIndexChanged"></asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="jackpot_text">
                        <ItemTemplate><%#Eval("JackpotTextType") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblJackpotTextType" runat="server" Visible="False" Text='<%#Eval("JackpotTextType") %>'></asp:Label>
                            <asp:DropDownList ID="ddlJackpotTextType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListJackpot_SelectedIndexChanged"></asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="jackpot_time">
                        <ItemTemplate><%#Eval("JackpotTime") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditJackpotTime" runat="server" Text='<%#Eval("JackpotTime") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="banner_text">
                        <ItemTemplate><%#Eval("BannerTextType") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblBannerTextType" runat="server" Visible="False" Text='<%#Eval("BannerTextType") %>'></asp:Label>
                            <asp:DropDownList ID="ddlBannerTextType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListBanner_SelectedIndexChanged"></asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="saldo">
                        <ItemTemplate><%#Eval("saldo") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditSaldo" runat="server" Text='<%#Eval("saldo") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="perc_S0">
                        <ItemTemplate><%#Eval("perc_S0") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditperc_S0" runat="server" Text='<%#Eval("perc_S0") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="perc_S1">
                        <ItemTemplate><%#Eval("perc_S1") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditperc_S1" runat="server" Text='<%#Eval("perc_S1") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="s1_variant">
                        <ItemTemplate><%#Eval("S1_variant") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditS1_variant" runat="server" Text='<%#Eval("S1_variant") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="perc_S2">
                        <ItemTemplate><%#Eval("perc_S2") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditperc_S2" runat="server" Text='<%#Eval("perc_S2") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="perc_S3">
                        <ItemTemplate><%#Eval("perc_S3") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditperc_S3" runat="server" Text='<%#Eval("perc_S3") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="perc_S4">
                        <ItemTemplate><%#Eval("perc_S4") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditperc_S4" runat="server" Text='<%#Eval("perc_S4") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="bet_R1">
                        <ItemTemplate><%#Eval("bet_R1") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditbet_R1" runat="server" Text='<%#Eval("bet_R1") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="bet_R2">
                        <ItemTemplate><%#Eval("bet_R2") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditbet_R2" runat="server" Text='<%#Eval("bet_R2") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="bet_R3">
                        <ItemTemplate><%#Eval("bet_R3") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditbet_R3" runat="server" Text='<%#Eval("bet_R3") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="bet_B4">
                        <ItemTemplate><%#Eval("bet_B4") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditbet_B4" runat="server" Text='<%#Eval("bet_B4") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="if_R1">
                        <ItemTemplate><%#Eval("if_R1") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditIf_R1" runat="server" Text='<%#Eval("if_R1") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="if_R2">
                        <ItemTemplate><%#Eval("if_R2") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditIf_R2" runat="server" Text='<%#Eval("if_R2") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="if_R3">
                        <ItemTemplate><%#Eval("if_R3") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditIf_R3" runat="server" Text='<%#Eval("if_R3") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="if_R4">
                        <ItemTemplate><%#Eval("if_R4") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditIf_R4" runat="server" Text='<%#Eval("if_R4") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="prob_O1">
                        <ItemTemplate><%#Eval("prob_O1") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditprob_O1" runat="server" Text='<%#Eval("prob_O1") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="prob_O2">
                        <ItemTemplate><%#Eval("prob_O2") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditprob_O2" runat="server" Text='<%#Eval("prob_O2") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="win_O1">
                        <ItemTemplate><%#Eval("win_O1") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditwin_O1" runat="server" Text='<%#Eval("win_O1") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="win_O2">
                        <ItemTemplate><%#Eval("win_O2") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditwin_O2" runat="server" Text='<%#Eval("win_O2") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ifS1win">
                        <ItemTemplate><%#Eval("ifS1win") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditifS1win" runat="server" Text='<%#Eval("ifS1win") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ifS2win">
                        <ItemTemplate><%#Eval("ifS2win") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditifS2win" runat="server" Text='<%#Eval("ifS2win") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ifS3win">
                        <ItemTemplate><%#Eval("ifS3win") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditifS3win" runat="server" Text='<%#Eval("ifS3win") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ifS4win">
                        <ItemTemplate><%#Eval("ifS4win") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditifS4win" runat="server" Text='<%#Eval("ifS4win") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ifS1probX">
                        <ItemTemplate><%#Eval("ifS1probX") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditifS1probX" runat="server" Text='<%#Eval("ifS1probX") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ifS2probX">
                        <ItemTemplate><%#Eval("ifS2probX") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditifS2probX" runat="server" Text='<%#Eval("ifS2probX") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="hide">
                        <ItemTemplate><%#Eval("hide") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEdithide" runat="server" Text='<%#Eval("hide") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="freeze_win">
                        <ItemTemplate><%#Eval("freeze_win") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditFreeze_win" runat="server" Text='<%#Eval("freeze_win") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="step_to_win">
                        <ItemTemplate><%#Eval("CloseToWinStep") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditCloseToWinStep" runat="server" Text='<%#Eval("CloseToWinStep") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="colour_win">
                        <ItemTemplate><%#Eval("CloseToWinColour") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditCloseToWinColour" runat="server" Text='<%#Eval("CloseToWinColour") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="multiplier">
                        <ItemTemplate><%#Eval("Multiplier") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditMultiplier" runat="server" Text='<%#Eval("Multiplier") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="spinn_delay1">
                        <ItemTemplate><%#Eval("SpinDelay1") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditSpinDelay1" runat="server" Text='<%#Eval("SpinDelay1") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="spinn_delay2">
                        <ItemTemplate><%#Eval("SpinDelay2") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditSpinDelay2" runat="server" Text='<%#Eval("SpinDelay2") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <br />
            <h3>Export</h3>
            <p>
                <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" Text="Game Report" Width="125px" />
                <asp:Button ID="btnReportQuestions" runat="server" Text="Questions Report" Width="178px" OnClick="btnReportQuestions_Click" />
            </p>
        </div>
    </form>
      <div id="message-container" class="container-fluid overlayer admin-content">
                    <div class="row">
                        <div class="col-md-4"></div>
                            <div class="col-md-4" id="message-content-admin">
                                <div class="login">
                                   
                                    <h2>Login</h2>
                                     <div class="form-group">
                                        <label for="username">Username</label>
                                        <input  class="form-control" id="username" style="max-width:300px;"/>
                                     </div>
                                    <div class="form-group">
                                        <label for="password">Password</label>
                                        <input type="password" class="form-control" id="password" style="max-width:300px;" />
                                     </div>

                                </div>
                            <button id="btnLogin" class="btn btn-large btn-primary" >Submit </button>
                        </div>
                    <div class="col-md-4"></div>
                </div>
            </div>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.0/jquery.min.js"></script>
    <script src="src/js/end.js"></script>
</body>
</html>
