<%@ Page Language="C#" MasterPageFile="~/User/UserMasterPage.master" AutoEventWireup="true" CodeFile="UserProfile.aspx.cs" Inherits="User_UserProfile" Title="User Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        h2 {
            color: #333;
        }

        table {
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 20px;
        }

        table, th, td {
            border: 1px solid #ddd;
        }

        th, td {
            padding: 10px;
            text-align: left;
        }

        .form-group {
            margin-bottom: 15px;
        }

        label {
            display: inline-block;
            width: 150px;
            font-weight: bold;
            margin-right: 10px;
        }

        .btnEdit,
        .btnUpdate {
            background-color: #007bff;
            color: white;
            padding: 10px 15px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
        }

        .btnUpdate {
            background-color: #28a745;
        }

        .file-upload-container {
            margin-top: 15px;
        }

        #ctl00_ContentPlaceHolder1_imgUser {
        width: 150px; 
        height: 150px;
        object-fit: cover; 
    }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2>User Profile</h2>

    <div>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblUsername" runat="server" Text="User Name:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtUsername" runat="server" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEmail" runat="server" Text="Email:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtEmail" runat="server" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblAddress" runat="server" Text="Address:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblContactNumber" runat="server" Text="Contact Number:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtContactNumber" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblGender" runat="server" Text="Gender:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlGender" runat="server">
                        <asp:ListItem Text="Select Gender" Value="" />
                        <asp:ListItem Text="Male" Value="Male" />
                        <asp:ListItem Text="Female" Value="Female" />
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblShopName" runat="server" Text="Shop Name:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtShopName" runat="server" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
    <td>
        <asp:Label ID="lblImage" runat="server" Text="Image:"></asp:Label>
    </td>
    <td>
        <asp:Image ID="imgUser" runat="server" />
        <asp:FileUpload ID="fileUploadImage" runat="server" />
    </td>
</tr>
        </table>

        <asp:Button ID="btnEdit" runat="server" Text="Edit Profile" OnClick="btnEdit_Click" />
        <asp:Button ID="btnUpdate" runat="server" Text="Update Profile" OnClick="btnUpdate_Click" Visible="false" />
    </div>
</asp:Content>
