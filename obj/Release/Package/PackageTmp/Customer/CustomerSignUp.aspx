<%@ Page Title="Customer Sign-Up" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CustomerSignUp.aspx.cs" Inherits="BakeryMS.Customer.CustomerSignUp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <h2 class="mb-4">Sign Up</h2>

        <!-- First Name -->
        <div class="mb-3">
            <label for="txtFirstName" class="form-label">First Name:</label>
            <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" Placeholder="Enter your first name"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName"
                CssClass="text-danger" Display="Dynamic" ErrorMessage="First name is required."></asp:RequiredFieldValidator>
        </div>

        <!-- Last Name -->
        <div class="mb-3">
            <label for="txtLastName" class="form-label">Last Name:</label>
            <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" Placeholder="Enter your last name"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName"
                CssClass="text-danger" Display="Dynamic" ErrorMessage="Last name is required."></asp:RequiredFieldValidator>
        </div>

        <!-- Email -->
        <div class="mb-3">
            <label for="txtEmail" class="form-label">Email:</label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Placeholder="Enter your email" TextMode="Email"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                CssClass="text-danger" Display="Dynamic" ErrorMessage="Email is required."></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                CssClass="text-danger" Display="Dynamic" ErrorMessage="Enter a valid email address."
                ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"></asp:RegularExpressionValidator>
        </div>

        <!-- Password -->
        <div class="mb-3">
            <label for="txtPassword" class="form-label">Password:</label>
            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" Placeholder="Enter your password" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
                CssClass="text-danger" Display="Dynamic" ErrorMessage="Password is required."></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revPassword" runat="server" ControlToValidate="txtPassword"
                CssClass="text-danger" Display="Dynamic"
                ErrorMessage="Password must be at least 8 characters long and contain one uppercase letter, one lowercase letter, and one number."
                ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$"></asp:RegularExpressionValidator>
        </div>

        <!-- Error Message -->
        <div class="my-3">
            <asp:Label ID="lblMessage" runat="server" CssClass="alert alert-danger w-100" Visible="false"></asp:Label>
        </div>

        <div class="d-grid">
            <asp:Button ID="btnSubmit" runat="server" Text="Sign Up" OnClick="btnSubmit_Click" CssClass="btn btn-primary btn-lg" />
        </div>
    </div>
</asp:Content>
