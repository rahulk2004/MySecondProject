<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="AdminUsers.aspx.cs" Inherits="Admin_AdminUsers" Title="Admin Users" %>

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

        .submit-button {
            float: right;
            margin: 10px;
            cursor: pointer;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h2>User Approval</h2>

<div style="overflow-x: auto;">
    <asp:GridView ID="GridViewUsers" runat="server" AutoGenerateColumns="False" DataKeyNames="UserID"
    OnRowEditing="GridViewUsers_RowEditing" OnRowCancelingEdit="GridViewUsers_RowCancelingEdit"
    OnRowUpdating="GridViewUsers_RowUpdating" OnRowDataBound="GridViewUsers_RowDataBound" EditIndex="-1">
    <Columns>
        <asp:BoundField DataField="UserID" HeaderText="User ID" ReadOnly="true" SortExpression="UserID" />
        <asp:BoundField DataField="UserName" HeaderText="User Name" SortExpression="UserName" />
        <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
        
        <asp:BoundField DataField="UserRole" HeaderText="User Role" SortExpression="UserRole" />
        <asp:BoundField DataField="ShopName" HeaderText="Shop Name" SortExpression="ShopName" />
        <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address" />
        <asp:BoundField DataField="ContactNumber" HeaderText="Contact Number" SortExpression="ContactNumber" />
        
         <asp:TemplateField HeaderText="Image">
            <ItemTemplate>
                <asp:Image ID="imgUser" runat="server" Width="50px" Height="50px" ImageUrl='<%# ResolveUrl(Eval("Image").ToString()) %>' AlternateText="User Image" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Approval Status">
    <ItemTemplate>
        <asp:Label ID="lblApprovalStatus" runat="server" Text='<%# Eval("ApprovalStatus") %>'></asp:Label>
    </ItemTemplate>
    <EditItemTemplate>
        <asp:RadioButton ID="rbApproved" runat="server" Text="Approve" GroupName="ApprovalGroup" />
        <asp:RadioButton ID="rbRejected" runat="server" Text="Reject" GroupName="ApprovalGroup" />
    </EditItemTemplate>
</asp:TemplateField>

        <asp:BoundField DataField="Gender" HeaderText="Gender" SortExpression="Gender" />
        
        <asp:TemplateField HeaderText="Action">
            <ItemTemplate>
                <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="Edit" />
            </ItemTemplate>
            <EditItemTemplate>
                <asp:Button ID="btnUpdate" runat="server" Text="Update" CommandName="Update" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CommandName="Cancel" />
            </EditItemTemplate>
        </asp:TemplateField>
    </Columns>
    <EmptyDataTemplate>
        <p>No data found.</p>
    </EmptyDataTemplate>
</asp:GridView>
</div>


    <asp:Label ID="lblSubmissionStatus" runat="server" Visible="false"></asp:Label>
</asp:Content>
