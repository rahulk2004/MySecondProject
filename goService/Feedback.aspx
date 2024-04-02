<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Feedback.aspx.cs" Inherits="Feedback" Title="Feedback" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <h2>Feedback</h2>
        <asp:Label ID="lblMessage" runat="server" ForeColor="Green" Visible="false"></asp:Label>
        <br />
        <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="false"></asp:Label>
        <br />
        <asp:TextBox ID="txtUserName" runat="server" placeholder="Your Name" Width="300" CssClass="form-control"></asp:TextBox>
        <br />
        <asp:TextBox ID="txtContactNumber" runat="server" placeholder="Contact Number" Width="300" CssClass="form-control"></asp:TextBox>
        <br />
        <asp:TextBox ID="txtFeedback" runat="server" TextMode="MultiLine" Rows="4" placeholder="Your Feedback" Width="300" CssClass="form-control"></asp:TextBox>
        <br />
        <asp:Button ID="btnSubmit" runat="server" Text="Submit Feedback" OnClick="btnSubmit_Click" CssClass="btn btn-primary" />
    </div>
</asp:Content>
