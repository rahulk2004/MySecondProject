<%@ Page Language="C#" MasterPageFile="~/User/UserMasterPage.master" AutoEventWireup="true" CodeFile="UserRating.aspx.cs" Inherits="User_UserRating" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
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
    <h2>User Ratings</h2>

    <asp:GridView ID="GridViewRatings" runat="server" AutoGenerateColumns="False" CssClass="gridview">
        <Columns>
            <asp:BoundField DataField="ServiceID" HeaderText="Service ID" SortExpression="ServiceID" />
            <asp:BoundField DataField="ServiceName" HeaderText="Service Name" SortExpression="ServiceName" />
            <asp:BoundField DataField="Price" HeaderText="Price" SortExpression="Price" />
            <asp:BoundField DataField="Location" HeaderText="Location" SortExpression="Location" />
            <asp:BoundField DataField="UserName" HeaderText="User Name" SortExpression="UserName" />
            <asp:BoundField DataField="ContactNumber" HeaderText="Contact Number" SortExpression="ContactNumber" />
            <asp:BoundField DataField="RatingValue" HeaderText="Rating" SortExpression="RatingValue" />
            <asp:BoundField DataField="AdditionalInfo" HeaderText="Additional Info" SortExpression="AdditionalInfo" />
        </Columns>
        <EmptyDataTemplate>
        <p>No data found.</p>
    </EmptyDataTemplate>
    </asp:GridView>

</asp:Content>
