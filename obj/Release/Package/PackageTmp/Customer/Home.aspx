<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="BakeryMS.Customer.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            background-color: #F8F9FA;
        }

        .product-img {
            border-radius: 10px;
            box-shadow: 2px 2px 10px rgba(0, 0, 0, 0.1);
            margin-bottom: 15px;
        }

        h1, h2 {
            color: #112D4E;
            margin-bottom: 20px;
        }

        p {
            line-height: 1.6;
            margin-bottom: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container text-center mt-4">
        <h1 class="mb-4">Welcome to Blue Oven!</h1>
        <p>Enjoy freshly baked goods made with love and the finest ingredients.</p>

        <div class="container-fluid p-0">
            <img src="/images/storefront.png" class="img-fluid w-75 product-img" alt="Bakery Storefront">
        </div>

        <h2 class="mt-5">Our Story</h2>
        <p class="mx-auto" style="max-width: 800px; max-height: 600px;">
            Blue Oven started as a small home kitchen in Manila, where Juan, a passionate baker, 
            shared his love for traditional Filipino bread with his neighbors. What began as a simple 
            passion turned into a beloved bakery known for its warm, homemade flavors.
        </p>

        <h2 class="mt-5">Why "Blue Oven"?</h2>
        <p class="mx-auto" style="max-width: 800px;">
            While most ovens glow red, our bakery represents warmth, trust, and the love baked into every bread.
            We believe that good food brings people together, and every bite of our bread should feel like home.
        </p>

        <h2 class="mt-5">Our Specialties</h2>
        <div class="row justify-content-center mt-4">
            <div class="col-md-3">
                <img src="/images/Pandesal.png" class="img-fluid product-img" alt="Pandesal">
                <h5>Pandesal</h5>
            </div>
            <div class="col-md-3">
                <img src="/images/Ensaymada.png" class="img-fluid product-img" alt="Ensaymada">
                <h5>Ensaymada</h5>
            </div>
            <div class="col-md-3">
                <img src="/images/Pandecoco.png" class="img-fluid product-img" alt="Pan de Coco">
                <h5>Pan de Coco</h5>
            </div>
        </div>
        <div class="pt-5">
            <asp:HyperLink ID="hlMenu" runat="server" NavigateUrl="Menu.aspx" CssClass="btn text-white" Style="background-color: #112D4E;">
        View Full Menu
            </asp:HyperLink>
        </div>
        <h2 class="mt-5">Our Mission & Vision</h2>
        <p class="mx-auto" style="max-width: 800px;"><strong>Mission:</strong> To bring the warmth of Filipino baking to every home, using only the finest ingredients and time-honored recipes.</p>
        <p class="mx-auto" style="max-width: 800px;"><strong>Vision:</strong> To be the go-to Filipino bakery where tradition and creativity come together, making every bite a nostalgic experience.</p>


    </div>


</asp:Content>
