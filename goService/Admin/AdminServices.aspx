<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="AdminServices.aspx.cs" Inherits="Admin_AdminServices" Title="Admin Services" %>

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
<h2>Service Approval</h2>
<div style="overflow-x: auto;">
    <asp:GridView ID="GridViewServices" runat="server" AutoGenerateColumns="False" DataKeyNames="ServiceID"
        OnRowEditing="GridViewServices_RowEditing" OnRowCancelingEdit="GridViewServices_RowCancelingEdit"
        OnRowUpdating="GridViewServices_RowUpdating" OnRowDataBound="GridViewServices_RowDataBound" EditIndex="-1">
        <Columns>
            <asp:BoundField DataField="ServiceID" HeaderText="Service ID" ReadOnly="true" SortExpression="ServiceID" />
            <asp:BoundField DataField="ServiceName" HeaderText="Service Name" SortExpression="ServiceName" />
            <asp:TemplateField HeaderText="Service Image">
            <ItemTemplate>
                <asp:Image ID="imgService" runat="server" Width="50px" Height="50px" ImageUrl='<%# ResolveUrl(Eval("ImagePath").ToString()) %>' AlternateText="image Loading" />
            </ItemTemplate>
        </asp:TemplateField>
            <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
            <asp:BoundField DataField="Category" HeaderText="Category" SortExpression="Category" />
            <asp:BoundField DataField="Subcategory" HeaderText="Subcategory" SortExpression="Subcategory" />
            <asp:BoundField DataField="Location" HeaderText="Location" SortExpression="Location" />
            <asp:BoundField DataField="Price" HeaderText="Price" SortExpression="Price" />
            <asp:BoundField DataField="UserName" HeaderText="User Name" SortExpression="UserName" />
            <asp:TemplateField HeaderText="Approval Status">
    <ItemTemplate>
        <asp:Label ID="lblApprovalStatus" runat="server" Text='<%# Eval("ApprovalStatus") %>'></asp:Label>
    </ItemTemplate>
    <EditItemTemplate>
        <asp:RadioButton ID="rbApproved" runat="server" Text="Approve" GroupName="ApprovalStatusGroup" Checked='<%# Eval("ApprovalStatus").ToString() == "Approved" %>' />
        <asp:RadioButton ID="rbRejected" runat="server" Text="Reject" GroupName="ApprovalStatusGroup" Checked='<%# Eval("ApprovalStatus").ToString() == "Rejected" %>' />
    </EditItemTemplate>
</asp:TemplateField>

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