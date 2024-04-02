<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .page-container {
            display: flex;
            align-items: center;
            justify-content: center;
            
            margin: 0;
        }

        .register-container {
            background-color: #fff;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            width: 600px;
            box-sizing: border-box;
            text-align: center;
        }

        .register-container label {
            display: block;
            font-weight: bold;
            margin-bottom: 5px;
            text-align: left;
        }

        .register-container .form-control {
            width: 100%;
            padding: 8px;
            box-sizing: border-box;
            border: 1px solid #ced4da;
            border-radius: 4px;
            margin-bottom: 10px;
        }

        .register-container .btnRegister {
            background-color: #007bff;
            color: white;
            padding: 10px 15px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
        }

        .register-container .btnRegister:hover {
            background-color: #0056b3;
        }

        .register-container p {
            margin-top: 15px;
        }

        .register-container a {
            color: #007bff;
            text-decoration: none;
            font-weight: bold;
        }

        .register-container a:hover {
            text-decoration: underline;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-container">
        <div class="register-container">
            <label for="txtFullName">Full Name:</label>
            <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" 
                />
                
                <label for="txtBirthdate">Birthdate:</label>
            <asp:TextBox ID="txtBirthdate" runat="server" CssClass="form-control" placeholder="mm/dd/yyyy" />

            <label for="ddlGender">Gender:</label>
            <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control">
                <asp:ListItem Text="Select Gender" Value="" />
                <asp:ListItem Text="Male" Value="Male" />
                <asp:ListItem Text="Female" Value="Female" />
            </asp:DropDownList>

            <label for="txtEmail">Email:</label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
            <asp:RegularExpressionValidator ID="regexEmail" runat="server"
    ControlToValidate="txtEmail"
    ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"
    ErrorMessage="Enter a valid email address."
    Display="Dynamic"
    CssClass="text-danger" />

            <label for="txtPassword">Password:</label>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" 
                CssClass="form-control" MaxLength="10" />

            <label for="txtShopName">Shop Name:</label>
            <asp:TextBox ID="txtShopName" runat="server" CssClass="form-control" 
                />

            <label for="txtAddress">Address:</label>
            <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" 
                 />

            <label for="txtContactNumber">Contact Number:</label>
            <asp:TextBox ID="txtContactNumber" runat="server" CssClass="form-control" 
                MaxLength="13" />
                <asp:RegularExpressionValidator ID="regexContactNumber" runat="server"
    ControlToValidate="txtContactNumber"
    ValidationExpression="^\d{10}$"
    ErrorMessage="Enter a valid 10-digit contact number."
    Display="Dynamic"
    CssClass="text-danger" />

            <asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click" CssClass="btnRegister" />

            <p>Already have an account? <a href="Login.aspx">Login here</a>.</p>
        </div>
    </div>
</asp:Content>
