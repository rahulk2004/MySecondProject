<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ServiceDetails.aspx.cs" Inherits="ServiceDetails" Title="Service Details Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .service-rating-container {
    display: flex;
    justify-content: space-between;
    margin-top: 20px;
}

.service-container,
.rating-container {
    width: 70%;
    border: 1px solid #ddd;
    border-radius: 8px;
    background-color: #fff;
    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
    margin-bottom: 20px;
}

.service-container {
    display: flex;
    flex-direction: column;
}

.service-image-container {
    width: 100%;
    height: 30%;
    overflow: hidden;
    display: flex;
    justify-content: left;
    align-items: center;
    margin-left: 8%;
}

.service-image {
    width: 50%;
    max-height: 100%;
}

.service-details-container {
    padding: 20px;
    text-align: left;
    margin-left: 5%;
}

.service-details {
    font-family: Arial, sans-serif;
    width: 100%;
}

.service-details h2 {
    color: #333;
    margin-top: 0;
}

.service-details p {
    margin: 5px 0;
}

.rating-container {
    width: 30%;
    border: 1px solid #ddd;
    border-radius: 8px;
    padding: 20px;
    overflow-y: auto;
   
    height: auto;
}

.ratings-container {
    padding: 20px;
}

.rating-item {
    margin-bottom: 20px;
    border-bottom: 1px solid #ddd;
    padding-bottom: 10px;
}

.feedback-link {
    display: block;
    margin-top: 20px;
    color: #007bff;
    text-decoration: none;
}

.feedback-link:hover {
    text-decoration: underline;
}

#ctl00_ContentPlaceHolder1_lblServiceDetails {
    font-family: Arial, sans-serif;
    font-size: 25px;
    line-height: 1.6;
    color: #333;
}

#ctl00_ContentPlaceHolder1_lblServiceDetails a {
    color: #007bff;
    text-decoration: none;
}

#ctl00_ContentPlaceHolder1_lblServiceDetails a:hover {
    text-decoration: underline;
}

.label {
    color: #333;
    font-weight: bold;
}


    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2>Service Details</h2>
    <div class="service-rating-container">
        <div class="service-container">
            <div class="service-image-container">
                <asp:Image ID="imgService" runat="server" CssClass="service-image" />
            </div>
            <div class="service-details-container">
                
                <asp:Label ID="lblServiceDetails" runat="server" Text=""></asp:Label>
                <asp:Label ID="lblPrice" runat="server" Text=""></asp:Label>
                <asp:Label ID="lblLocation" runat="server" Text=""></asp:Label>
            </div>
        </div>
        <div class="rating-container">
            
                <h3>Ratings</h3>
                <a class="feedback-link" href='<%= ResolveUrl("~/GiveRating.aspx?ServiceID=" + Request.QueryString["ServiceID"]) %>' target="_blank">Give Feedback</a>
                <asp:Repeater ID="RepeaterRatings" runat="server">
                    <ItemTemplate>
                        <div class="rating-item">
                            <p><strong>Username:</strong> <%# Eval("Username") %></p>
                            <p><strong>Rating:</strong> <%# Eval("RatingValue") %></p>
                            <p><strong>Additional Info:</strong> <%# Eval("AdditionalInfo") %></p>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
           
        </div>
    </div>
</asp:Content>
