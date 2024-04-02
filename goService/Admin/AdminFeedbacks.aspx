<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="AdminFeedbacks.aspx.cs" Inherits="Admin_AdminFeedbacks" Title="Feedbacks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        table {
            border-collapse: collapse;
            width: 100%;
        }

        th, td {
            border: 1px solid #ddd;
            padding: 8px;
            text-align: left;
        }

        th {
            background-color: #f2f2f2;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="feedback-container">
        <h2>All Feedbacks</h2>
        <asp:Repeater ID="rptFeedbacks" runat="server">
            <HeaderTemplate>
                <table class="feedback-table">
                    <tr>
                        <th>User Name</th>
                        <th>Contact Number</th>
                        <th>Feedback Text</th>
                        <th>Date Submitted</th>
                        <th>Action</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><%# Eval("UserName") %></td>
                    <td><%# Eval("ContactNumber") %></td>
                    <td><%# Eval("FeedbackText") %></td>
                    <td><%# Eval("DateSubmitted") %></td>
                    <td>
                        <asp:Button ID="btnDelete" runat="server" CommandArgument='<%# Eval("FeedbackID") %>' Text="Delete" OnClick="btnDelete_Click" />
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
