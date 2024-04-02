<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ProviderProfile.aspx.cs" Inherits="ProviderProfile" Title="Provider Profile Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   <style>
   .provider-profile {
    width: 60%;
    margin: 0 auto;
    padding: 20px;
    border: 1px solid #ccc;
    border-radius: 8px;
    background-color: #f9f9f9;
}

.profile-heading {
    font-size: 24px;
    font-weight: bold;
    margin-bottom: 20px;
}

.profile-image {
    max-width: 200px;
    max-height: 200px;
    margin: 0 auto 20px;
    display: block;
}

.profile-details {
    margin-bottom: 15px;
}

.profile-label {
    font-weight: bold;
    
}

.profile-info {
    margin-bottom: 10px;
}

   </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="provider-profile">
        <img class="profile-image" id="imgProvider" runat="server" alt="Provider Image" />
        <h2 class="profile-heading">Provider Profile</h2>

        <div class="profile-details">
            <div class="profile-info">
                <asp:Label ID="lblProviderName" runat="server" CssClass="profile-label"></asp:Label>
            </div>
            <div class="profile-info">
                <asp:Label ID="lblEmail" runat="server" CssClass="profile-label"></asp:Label>
            </div>
            <div class="profile-info">
                <asp:Label ID="lblAddress" runat="server" CssClass="profile-label"></asp:Label>
            </div>
            <div class="profile-info">
                <asp:Label ID="lblContactNumber" runat="server" CssClass="profile-label"></asp:Label>
            </div>
            <div class="profile-info">
                <asp:Label ID="lblGender" runat="server" CssClass="profile-label"></asp:Label>
            </div>
            <div class="profile-info">
                <asp:Label ID="lblShopName" runat="server" CssClass="profile-label"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>