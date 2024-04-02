<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="AdminRatings.aspx.cs" Inherits="Admin_AdminRatings" Title="Admin Ratings" %>

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
     <h2>All Ratings</h2>
    
    <asp:GridView ID="GridViewServices" runat="server" AutoGenerateColumns="False" DataKeyNames="ServiceID">
   
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
            <asp:TemplateField HeaderText="Action">
                <ItemTemplate>
                    <asp:HyperLink ID="lnkShowRatings" runat="server" NavigateUrl='<%# "~/Admin/AdminShowRating.aspx?ServiceID=" + Eval("ServiceID") %>'  Text='<%# Eval("OverallRating", "{0:F1}") %>' />

                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
        <EmptyDataTemplate>
        <p>No data found.</p>
    </EmptyDataTemplate>
    </asp:GridView>
</asp:Content>
