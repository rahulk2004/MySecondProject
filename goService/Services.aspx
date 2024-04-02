<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Services.aspx.cs" Inherits="Services" Title="Services Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        body {
            text-align: center;
        }

        .layout {
            width: 80%;
            margin: auto;
        }

        .service-square {
        display: inline-block;
        width: 30%;
        height: 300px;
        margin: 10px;
        padding: 10px;
        border: 1px solid #ddd;
        text-align: left;
        
    }

    .service-square:hover {
       
        box-shadow: 0 0 10px rgba(0, 0, 0, 0.2);
    }

    .image-container {
        width: 100%;
        height: 60%;
        text-align: center;
    }

    .service-image {
        width: 100%;
        height: auto;
        max-width: 100%;
        max-height: 100%;
    }

    .service-details {
        padding-top: 10px;
    }

    .service-square a {
        text-decoration: none;
    }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2>Services</h2>

    <div>
    <label>Category:</label>
    <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
    </asp:DropDownList>

    <label>Subcategory:</label>
    <asp:DropDownList ID="ddlSubcategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSubcategory_SelectedIndexChanged">
    </asp:DropDownList>

    <label>Location:</label>
    <asp:DropDownList ID="ddlLocation" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:Button ID="btnClear" runat="server" Text="Clear Selections" OnClick="btnClear_Click" />
    </div>
    

    
    <asp:Label ID="lblSelectedFilters" runat="server" Text=""></asp:Label>


    <section class="layout">
        <asp:Repeater ID="RepeaterServices" runat="server" OnItemDataBound="RepeaterServices_ItemDataBound">
            <ItemTemplate>
               <div class="service-square">
                    <asp:HyperLink ID="hlService" runat="server" NavigateUrl='<%# "ServiceDetails.aspx?ServiceID=" + Eval("ServiceID") %>'>
                        <div class="image-container">
                            <asp:Image ID="imgService" runat="server" CssClass="service-image" ImageUrl='<%# ResolveUrl(Eval("ImagePath").ToString()) %>' AlternateText="Service Image" />
                        </div>
                        <div class="service-details">
                            <asp:Label ID="lblServiceName" runat="server" Text='<%# Eval("ServiceName") %>' />
                            <p>Price : <%# Eval("Price")%></p>
                            <p> Location : <%# Eval("Location")%></p>
                            <p>Overall Rating: <%# Eval("OverallRating", "{0:F1}")%></p>
                        </div>
                    </asp:HyperLink>
                </div>
            </ItemTemplate>
            <FooterTemplate>
        <asp:Label ID="lblNoData" runat="server" Text="No services found." Visible="false" />
    </FooterTemplate>
        </asp:Repeater>
    </section>
</asp:Content>
