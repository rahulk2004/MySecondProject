<%@ Page Language="C#" MasterPageFile="~/User/UserMasterPage.master" AutoEventWireup="true" CodeFile="UserServices.aspx.cs" Inherits="User_UserServices" Title="User Services" %>

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
        .heading-container {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 10px;
        }

        .btn-add-service {
            padding: 10px;
            background-color: #3498db;
            color: #fff;
            text-decoration: none;
            border-radius: 5px;
        }
        .table .gridview-btn {
    padding: 8px 12px;
    margin: 2px;
    text-align: center;
    text-decoration: none;
    cursor: pointer;
    border-radius: 4px;
    font-size: 14px;
    color: #fff;
    display: inline-block;
    transition: background-color 0.3s;
}

.table .gridview-btn:hover {
    background-color: #555;
}

    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
    <div class="heading-container">
        <h2>User Services</h2>
            <asp:LinkButton ID="LinkButton1" runat="server">
                <a href="UserAddService.aspx" class="btn-add-service" >Add Service</a>
            </asp:LinkButton>
        </div>

        <asp:GridView ID="gvServices" runat="server" AutoGenerateColumns="False" OnRowEditing="gvServices_RowEditing"
            OnRowUpdating="gvServices_RowUpdating" OnRowCancelingEdit="gvServices_RowCancelingEdit"
            OnRowDeleting="gvServices_RowDeleting" OnRowDataBound="gvServices_RowDataBound" DataKeyNames="ServiceID" CssClass="table">
            <Columns>
                <asp:BoundField DataField="ServiceID" HeaderText="Service ID" ReadOnly="true" SortExpression="ServiceID" />
                <asp:TemplateField HeaderText="Service Name">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtServiceName" runat="server" Text='<%# Bind("ServiceName") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblServiceName" runat="server" Text='<%# Bind("ServiceName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtDescription" runat="server" Text='<%# Bind("Description") %>' TextMode="MultiLine" Rows="3"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Category">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtCategory" runat="server" Text='<%# Bind("Category") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblCategory" runat="server" Text='<%# Bind("Category") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Subcategory">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtSubcategory" runat="server" Text='<%# Bind("Subcategory") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblSubcategory" runat="server" Text='<%# Bind("Subcategory") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Location">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtLocation" runat="server" Text='<%# Bind("Location") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblLocation" runat="server" Text='<%# Bind("Location") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Price">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtPrice" runat="server" Text='<%# Bind("Price") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblPrice" runat="server" Text='<%# Bind("Price") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ProviderID">
                    <EditItemTemplate>
                        <asp:Label ID="lblProviderID" runat="server" Text='<%# Bind("ProviderID") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblProviderID" runat="server" Text='<%# Bind("ProviderID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ApprovalStatus">
                    <EditItemTemplate>
                        <asp:Label ID="lblApprovalStatus" runat="server" Text='<%# Bind("ApprovalStatus") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblApprovalStatus" runat="server" Text='<%# Bind("ApprovalStatus") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Image">
                    <EditItemTemplate>
                        <asp:FileUpload ID="fileUploadImage" runat="server" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Image ID="imgService" runat="server" Width="50px" Height="50px" ImageUrl='<%# Eval("ImagePath") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:CommandField ShowEditButton="True" />
                <asp:CommandField ShowDeleteButton="True" />
            </Columns>
            <EmptyDataTemplate>
        <p>No data found.</p>
    </EmptyDataTemplate>
        </asp:GridView>
    </div>
</asp:Content>
