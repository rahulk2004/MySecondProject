<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .page-container {
            display: flex;
            align-items: center;
            justify-content: center;
           
            margin-top: 50px;
        }

        .login-container {
            background-color: #fff;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            width: 600px;
            box-sizing: border-box;
            text-align: center;
        }

        .login-container label {
            display: block;
            font-weight: bold;
            margin-bottom: 5px;
            text-align: left;
        }

        .login-container .form-control {
            width: 100%;
            padding: 8px;
            box-sizing: border-box;
            border: 1px solid #ced4da;
            border-radius: 4px;
            margin-bottom: 10px;
        }

        .login-container .form-control:focus {
            outline: none;
            border-color: #007bff;
            box-shadow: 0 0 5px rgba(0, 123, 255, 0.5);
        }

        .login-container .btnLogin {
            background-color: #007bff;
            color: white;
            padding: 10px 15px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
        }

        .login-container .btnLogin:hover {
            background-color: #0056b3;
        }

        .login-container p {
            margin-top: 15px;
        }

        .login-container a {
            color: #007bff;
            text-decoration: none;
            font-weight: bold;
        }

        .login-container a:hover {
            text-decoration: underline;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-container">
        <div class="login-container">
            <label for="txtLoginEmail">Email:</label>
            <asp:TextBox ID="txtLoginEmail" runat="server" CssClass="form-control" />

            <label for="txtLoginPassword">Password:</label>
            <asp:TextBox ID="txtLoginPassword" runat="server" TextMode="Password" 
                CssClass="form-control" MaxLength="10" />

            <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" CssClass="btnLogin" />

            <p>Don't have an account? <a href="Register.aspx">Register here</a>.</p>
            <p>Forgotteen Password ? <a href="ResetPassword.aspx">Reset Password here</a>.</p>
        </div>
    </div>
</asp:Content>
