<%@ Page Title="Order Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrderDetails.aspx.cs" Inherits="BakeryMS.Admin.OrderDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Additional head content if needed -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h2 class="text-center mb-4">Order Details</h2>
        
        <!-- Search Panel -->
        <div class="row mb-3">
            <div class="col-md-6 offset-md-3">
                <div class="input-group">
                    <asp:TextBox ID="searchUser_txtbox" runat="server" CssClass="form-control" placeholder="Enter User ID to search"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="searchUser_btn_Click" />
                </div>
            </div>
        </div>
        
        <!-- Order Details GridView -->
        <div class="table-responsive">
            <asp:GridView ID="OrderDetailsGrid" runat="server" AutoGenerateColumns="False" DataKeyNames="OrderDetailID"
                CssClass="table table-bordered table-hover text-center" Width="100%">
                <Columns>
                    <asp:BoundField DataField="OrderDetailID" HeaderText="Order Detail ID" />
                    <asp:BoundField DataField="OrderID" HeaderText="Order ID" />
                    <asp:BoundField DataField="ProductID" HeaderText="Product ID" />
                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                    <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0:C}" />
                    <asp:BoundField DataField="cus_id" HeaderText="Customer ID" />
                    <asp:BoundField DataField="cus_name" HeaderText="Customer Name" />
                    <asp:BoundField DataField="odr_date" HeaderText="Order Date" DataFormatString="{0:MM/dd/yyyy}" />
                    <asp:BoundField DataField="pr_name" HeaderText="Product Name" />
                    <asp:BoundField DataField="pr_qty" HeaderText="Product Qty" />
                    <asp:BoundField DataField="pr_price" HeaderText="Product Price" DataFormatString="{0:C}" />
                    <asp:BoundField DataField="ttl_price" HeaderText="Total Price" DataFormatString="{0:C}" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
