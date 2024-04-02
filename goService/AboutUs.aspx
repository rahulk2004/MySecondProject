<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AboutUs.aspx.cs" Inherits="AboutUs" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .container {
            width: 50%;
            margin: auto; 
            padding: 20px;
            background-color: #fff;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            text-align: center;
            font-family: 'Arial', sans-serif;
        }

        h2 {
            color: #3498db;
            font-size: 2em;
        }

        p {
            line-height: 1.6;
            font-size: 1.2em;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container">
        <h2>Who we are</h2>
        <p>
            goService is a technology platform offering a variety of services at home. Customers use our platform to book services such as beauty treatments, haircuts, massage therapy, cleaning, plumbing, carpentry, appliance repair, painting, etc. These services are delivered in the comfort of their home and at a time of their choosing. We promise our customers a high quality, standardized, and reliable service experience. To fulfill this promise, we work closely with our hand-picked service partners, enabling them with technology, training, products, tools, financing, insurance, and brand, helping them succeed and deliver on this promise.
        </p>
        <p>
            <strong>Our Vision:</strong> Empower millions of professionals worldwide to deliver services at home like never experienced before.
        </p>
    </div>
</asp:Content>
