<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="BakeryMS.Account.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-4">
                <h2 class="mb-4 text-center">Login</h2>

                <div class="mb-3">
                    <asp:Label ID="lblEmail" runat="server" Text="Email:" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <asp:Label ID="lblPassword" runat="server" Text="Password:" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                </div>

                <asp:Label ID="lblMessage" runat="server" Visible="false" CssClass="text-danger"></asp:Label>

                <div class="d-grid">
                    <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" CssClass="btn text-white" style="background-color: #112D4E;" />
                </div>

                <div class="mt-3 text-center">
                    <asp:Label ID="lblSignUp" runat="server">
                        Don't have an account yet? <a href="/Customer/CustomerSignUp.aspx" class="text-primary">Sign up</a> now.
                    </asp:Label>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
