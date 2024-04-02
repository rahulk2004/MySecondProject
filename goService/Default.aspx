<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="main-content">
        <section class="welcome-section">
            <div class="container">
                <h1>Welcome to Our Website</h1>
                <p>Your go-to platform for top-notch services.</p>
                <a href="Services.aspx" class="cta-button">Explore Services</a>
            </div>
        </section>

        

        <section class="about-us-section">
            <div class="container">
                <h2>About Us</h2>
                <p>Learn more about our company and our mission to provide excellent services.</p>
                <a href="AboutUs.aspx" class="cta-button">About Us</a>
            </div>
        </section>
    </div>
</asp:Content>

