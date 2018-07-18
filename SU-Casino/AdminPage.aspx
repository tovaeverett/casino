﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminPage.aspx.cs" Inherits="SU_Casino.AdminPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:DropDownList ID="ddlText" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:TextBox ID="txtText" runat="server" Height="419px" TextMode="MultiLine" Width="803px"></asp:TextBox>
        </div>
        <asp:Button ID="btnText" runat="server" OnClick="btnText_Click" Text="Update Text" />
        <asp:HiddenField ID="HiddenField1" runat="server" />
        <br />
        <br />
        <asp:Button ID="btnAddRow" runat="server" OnClick="AddRow_Click" Text="Add Row" />

<asp:GridView ID="gvMatris" runat="server" OnRowEditing="gvMatris_RowEditing" AutoGenerateColumns="False" AutoGenerateEditButton="True" AutoGenerateDeleteButton="True" OnRowUpdating="gvMatris_RowUpdating" OnRowCancelingEdit="gvMatris_RowCancelingEdit" OnRowDeleting="gvMatris_RowDeleting">
    <columns>
        <asp:templatefield>
            <itemtemplate>
            <asp:label id="lblRowId" runat="server" Visible="false" Text='<%#Eval("RowID")%>'></asp:label>
            </itemtemplate>
        </asp:templatefield> 
        <asp:templatefield headertext="prop_n">
             <itemtemplate> <%#Eval("prop_n") %></itemtemplate>
                 <EditItemTemplate>
                <asp:TextBox ID="txtEditProp_N" runat="server" Text='<%#Eval("prop_n") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:templatefield>   
        <asp:templatefield headertext="condition">
            <itemtemplate> <%#Eval("condition") %></itemtemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtEditcondition" runat="server" Text='<%#Eval("condition") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:templatefield>
          <asp:templatefield headertext="moment">
            <itemtemplate> <%#Eval("moment") %></itemtemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtEditmoment" runat="server" Text='<%#Eval("moment") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:templatefield>
                  <asp:templatefield headertext="name">
            <itemtemplate> <%#Eval("name") %></itemtemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtEditName" runat="server" Text='<%#Eval("name") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:templatefield>
                  <asp:templatefield headertext="prob_S0">
            <itemtemplate> <%#Eval("prob_S0") %></itemtemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtEditprob_S0" runat="server" Text='<%#Eval("prob_S0") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:templatefield>
                  <asp:templatefield headertext="perc_S1">
            <itemtemplate> <%#Eval("perc_S1") %></itemtemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtEditperc_S1" runat="server" Text='<%#Eval("perc_S1") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:templatefield>
                  <asp:templatefield headertext="perc_S2">
            <itemtemplate> <%#Eval("perc_S2") %></itemtemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtEditperc_S2" runat="server" Text='<%#Eval("perc_S2") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:templatefield>
        <asp:templatefield headertext="perc_S3">
            <itemtemplate> <%#Eval("perc_S3") %></itemtemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtEditperc_S3" runat="server" Text='<%#Eval("perc_S3") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:templatefield>
          <asp:templatefield headertext="perc_S4">
            <itemtemplate> <%#Eval("perc_S4") %></itemtemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtEditperc_S4" runat="server" Text='<%#Eval("perc_S4") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:templatefield>
             <asp:templatefield headertext="bet_R1">
            <itemtemplate> <%#Eval("bet_R1") %></itemtemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtEditbet_R1" runat="server" Text='<%#Eval("bet_R1") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:templatefield>
          <asp:templatefield headertext="bet_R2">
            <itemtemplate> <%#Eval("bet_R2") %></itemtemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtEditbet_R2" runat="server" Text='<%#Eval("bet_R2") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:templatefield>
        <asp:templatefield headertext="prob_O1">
            <itemtemplate> <%#Eval("prob_O1") %></itemtemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtEditprob_O1" runat="server" Text='<%#Eval("prob_O1") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:templatefield>
          <asp:templatefield headertext="prob_O2">
            <itemtemplate> <%#Eval("prob_O2") %></itemtemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtEditprob_O2" runat="server" Text='<%#Eval("prob_O2") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:templatefield>
        <asp:templatefield headertext="win_O1">
            <itemtemplate> <%#Eval("win_O1") %></itemtemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtEditwin_O1" runat="server" Text='<%#Eval("win_O1") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:templatefield>
          <asp:templatefield headertext="win_O2">
            <itemtemplate> <%#Eval("win_O2") %></itemtemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtEditwin_O2" runat="server" Text='<%#Eval("win_O2") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:templatefield>
          <asp:templatefield headertext="ifS0">
            <itemtemplate> <%#Eval("ifS0") %></itemtemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtEditifS0" runat="server" Text='<%#Eval("ifS0") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:templatefield>
          <asp:templatefield headertext="ifS1win">
            <itemtemplate> <%#Eval("ifS1win") %></itemtemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtEditifS1win" runat="server" Text='<%#Eval("ifS1win") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:templatefield>
        <asp:templatefield headertext="ifS2win">
            <itemtemplate> <%#Eval("ifS2win") %></itemtemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtEditifS2win" runat="server" Text='<%#Eval("ifS2win") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:templatefield>
          <asp:templatefield headertext="ifS3win">
            <itemtemplate> <%#Eval("ifS3win") %></itemtemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtEditifS3win" runat="server" Text='<%#Eval("ifS3win") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:templatefield>
                <asp:templatefield headertext="ifS4win">
            <itemtemplate> <%#Eval("ifS4win") %></itemtemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtEditifS4win" runat="server" Text='<%#Eval("ifS4win") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:templatefield>
          <asp:templatefield headertext="ifS1probX">
            <itemtemplate> <%#Eval("ifS1probX") %></itemtemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtEditifS1probX" runat="server" Text='<%#Eval("ifS1probX") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:templatefield>
          <asp:templatefield headertext="ifS2probX">
            <itemtemplate> <%#Eval("ifS2probX") %></itemtemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtEditifS2probX" runat="server" Text='<%#Eval("ifS2probX") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:templatefield>
          <asp:templatefield headertext="ifS3probX">
            <itemtemplate> <%#Eval("ifS3probX") %></itemtemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtEditifS3probX" runat="server" Text='<%#Eval("ifS3probX") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:templatefield>
           <asp:templatefield headertext="ifS4probX">
            <itemtemplate> <%#Eval("ifS4probX") %></itemtemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtEditifS4probX" runat="server" Text='<%#Eval("ifS4probX") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:templatefield>
          <asp:templatefield headertext="hide">
            <itemtemplate> <%#Eval("hide") %></itemtemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtEdithide" runat="server" Text='<%#Eval("hide") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:templatefield>
    </columns>
        </asp:GridView>
        <p>
            <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" Text="Report" Width="125px" />
        </p>
    </form>
</body>
</html>
