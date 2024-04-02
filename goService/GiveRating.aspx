<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="GiveRating.aspx.cs" Inherits="GiveRating" Title="Give Rating Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Give Rating</title>
    <style type="text/css">
        .rating-form {
            width: 300px;
            margin: 0 auto;
            text-align: center;
            padding: 20px;
            border: 1px solid #ddd;
            margin-top: 50px;
        }

        
        
        .form-label {
    display: block;
    font-weight: bold;
    margin-bottom: 5px;
    text-align: left;
    color: #333;
}

.form-group {
    margin-bottom: 15px;
}

.form-control {
    width: 100%;
    padding: 8px;
    box-sizing: border-box;
    border: 1px solid #ced4da;
    border-radius: 4px;
}

.form-control:focus {
    outline: none;
    border-color: #007bff;
    box-shadow: 0 0 5px rgba(0, 123, 255, 0.5);
}

.btnLogin {
    background-color: #007bff;
    color: white;
    padding: 10px 15px;
    border: none;
    border-radius: 4px;
    cursor: pointer;
}

.btnLogin:hover {
    background-color: #0056b3;
}

    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="rating-form">
        <h2>Give Rating</h2>

        <div class="form-group">
            <asp:Label ID="lblUsername" runat="server" Text="Username:" CssClass="form-label"></asp:Label>
            <asp:TextBox ID="txtUsername" runat="server" MaxLength="15" CssClass="form-control"></asp:TextBox>
        </div>

        <div class="form-group">
            <asp:Label ID="lblContactNumber" runat="server" Text="Contact Number:" CssClass="form-label"></asp:Label>
            <asp:TextBox ID="txtContactNumber" runat="server" MaxLength="13" CssClass="form-control"></asp:TextBox>
             <asp:RegularExpressionValidator ID="revContactNumber" runat="server"
                ControlToValidate="txtContactNumber"
                ErrorMessage="Enter a valid contact number"
                ValidationExpression="^\d{10}$"
                Display="Dynamic">
            </asp:RegularExpressionValidator>
        </div>

        <div class="form-group">
            <asp:Label ID="lblRating" runat="server" Text="Rating:" CssClass="form-label"></asp:Label>
            <asp:DropDownList ID="ddlRating" runat="server" CssClass="form-control">
                <asp:ListItem Text="1" Value="1"></asp:ListItem>
                <asp:ListItem Text="2" Value="2"></asp:ListItem>
                <asp:ListItem Text="3" Value="3"></asp:ListItem>
                <asp:ListItem Text="4" Value="4"></asp:ListItem>
                <asp:ListItem Text="5" Value="5"></asp:ListItem>
            </asp:DropDownList>
        </div>

        <div class="form-group">
            <asp:Label ID="lblAdditionalInfo" runat="server" Text="Additional Info:" CssClass="form-label"></asp:Label>
            <asp:TextBox ID="txtAdditionalInfo" runat="server" TextMode="MultiLine" 
                Rows="3" MaxLength="50" CssClass="form-control"></asp:TextBox>
        </div>

        <asp:Button ID="btnSubmitRating" runat="server" Text="Submit Rating" OnClick="btnSubmitRating_Click" CssClass="btnLogin" />
    </div>
</asp:Content>