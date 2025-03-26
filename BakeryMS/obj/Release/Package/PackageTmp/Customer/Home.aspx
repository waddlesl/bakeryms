<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="BakeryMS.Customer.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container text-center mt-4">
        <h1>Welcome to Our Bakery!</h1>
        <p>Enjoy freshly baked goods made with love and the finest ingredients.</p>

        <h2 class="mt-4">Our Bestsellers</h2>
        <div class="row justify-content-center">
            <div class="col-md-3">
                <img src="images/Pandesal.jpg" class="img-fluid product-img" alt="Pandesal">
                <h5>Pandesal</h5>
            </div>
            <div class="col-md-3">
                <img src="images/Ensaymada.jpg" class="img-fluid product-img" alt="Ensaymada">
                <h5>Ensaymada</h5>
            </div>
            <div class="col-md-3">
                <img src="images/EggPie.jpg" class="img-fluid product-img" alt="Egg Pie">
                <h5>Egg Pie</h5>
            </div>
        </div>

        <div class="mt-4">
            <asp:HyperLink ID="hlMenu" runat="server" NavigateUrl="Menu.aspx" CssClass="btn btn-primary">
                View Full Menu
            </asp:HyperLink>
        </div>
    </div>

</asp:Content>
