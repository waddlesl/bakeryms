<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CustomerOrders.aspx.cs" Inherits="BakeryMS.Customer.CustomerOrders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Optionally include Bootstrap CSS if not already in Site.Master -->
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h2 class="mb-3">My Cart</h2>
        <asp:GridView ID="gvCart" runat="server" AutoGenerateColumns="False" OnRowCommand="gvCart_RowCommand" CssClass="table table-striped table-bordered">
            <Columns>
                <asp:TemplateField HeaderText="Image">
                    <ItemTemplate>
                        <asp:Image ID="imgProduct" runat="server" CssClass="img-thumbnail"
                            ImageUrl='<%# "~/images/" + Eval("ProductName").ToString() + ".png" %>'
                            AlternateText='<%# Eval("ProductName") %>'
                            Width="80px" Height="80px" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ProductName" HeaderText="Product" />
                <asp:BoundField DataField="UnitPrice" HeaderText="Price per Each" DataFormatString="₱{0:N2}" />
                <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                <asp:BoundField DataField="TotalPrice" HeaderText="Total Price" DataFormatString="₱{0:N2}" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnRemove" runat="server" CommandName="Remove"
                            CommandArgument='<%# Container.DataItemIndex %>' Text="Remove" CssClass="btn btn-danger btn-sm" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <br />
        <asp:Button ID="btnSubmitOrder" runat="server" Text="Submit Order" OnClick="btnSubmitOrder_Click" CssClass="btn btn-primary" />
        <br />
        <br />

        <h2 class="mb-3">My Orders</h2>
        <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="False" OnRowCommand="gvOrders_RowCommand" CssClass="table table-striped table-bordered">
            <Columns>
                <asp:BoundField DataField="OrderID" HeaderText="Order ID" />
                <asp:BoundField DataField="OrderDate" HeaderText="Order Date" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="Status" HeaderText="Status" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnCancelOrder" runat="server" Text="Cancel Order"
                            CommandName="CancelOrder" CommandArgument='<%# Eval("OrderID") %>' CssClass="btn btn-warning btn-sm" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <br />
        <asp:HyperLink ID="hlMenu" runat="server" NavigateUrl="Menu.aspx" CssClass="btn btn-secondary">
            Back to Menu
        </asp:HyperLink>
        <br />
        <br />
        <asp:Button ID="btnNewOrder" runat="server" Text="Start New Order" OnClick="btnNewOrder_Click" CssClass="btn btn-success mb-5" />
    </div>
</asp:Content>
