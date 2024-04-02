<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ResetPassword.aspx.cs" Inherits="ResetPassword" Title="Reset Password" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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

        .label {
            display: block;
            font-weight: bold;
            margin-bottom: 5px;
            text-align: left;
            color: #333; /* Add color styling */
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
            color: #333; /* Add color styling */
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

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="page-container">
        <div class="login-container">
            <asp:Label ID="lblEmail" runat="server" Text="Email:" CssClass="label"></asp:Label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>

            <asp:Button ID="btnVerifyEmail" runat="server" Text="Verify Email" OnClick="btnVerifyEmail_Click" CssClass="btnLogin" />

            <asp:Panel ID="pnlResetPassword" runat="server" Visible="false">
                <asp:Label ID="lblNewPassword" runat="server" Text="New Password:" CssClass="label"></asp:Label>
                <asp:TextBox ID="txtNewPassword" TextMode="Password" runat="server" CssClass="form-control"></asp:TextBox>

                <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm Password:" CssClass="label"></asp:Label>
                <asp:TextBox ID="txtConfirmPassword" TextMode="Password" runat="server" CssClass="form-control"></asp:TextBox>

                <asp:Button ID="btnResetPassword" runat="server" Text="Reset Password" OnClick="btnResetPassword_Click" CssClass="btnLogin" />
            </asp:Panel>

            <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label"></asp:Label>
        </div>
    </div>
</asp:Content>
