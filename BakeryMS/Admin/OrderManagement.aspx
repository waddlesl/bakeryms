<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrderManagement.aspx.cs" Inherits="BakeryMS.Customer.OrderManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Include Bootstrap CSS if not already in Site.Master -->
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h2>Manage Orders</h2>
        <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="False" OnRowCommand="gvOrders_RowCommand" CssClass="table table-striped table-bordered">
            <Columns>
                <asp:BoundField DataField="OrderID" HeaderText="Order ID" />
                <asp:BoundField DataField="OrderDate" HeaderText="Order Date" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="Status" HeaderText="Status" />
                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <asp:Button ID="btnAccept" runat="server"
                            CommandName="AcceptOrder"
                            CommandArgument='<%# Eval("OrderID") %>'
                            Text="Accept"
                            CssClass="btn btn-success btn-sm"
                            Visible='<%# Eval("Status").ToString() == "Pending" %>' />
                        &nbsp;
        <asp:Button ID="btnCancel" runat="server"
            CommandName="CancelOrder"
            CommandArgument='<%# Eval("OrderID") %>'
            Text="Cancel"
            CssClass="btn btn-danger btn-sm"
            Visible='<%# Eval("Status").ToString() == "Pending" %>' />
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>

        <hr />

        <h2>Order Summary</h2>
        <div class="row">
            <div class="col-md-4">
                <div class="card text-white mb-3" style="background-color: #112D4E;">
                    <div class="card-header">Total Accepted Orders</div>
                    <div class="card-body">
                        <h5 class="card-title"><span id="lblTotalOrders" runat="server"></span></h5>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="card text-white mb-3" style="background-color: #3F72AF;">
                    <div class="card-header">Total Items Ordered</div>
                    <div class="card-body">
                        <h5 class="card-title"><span id="lblTotalItems" runat="server"></span></h5>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="card mb-3" style="background-color: #DBE2EF;">
                    <div class="card-header">Total Sales</div>
                    <div class="card-body">
                        <h5 class="card-title"><span id="lblTotalSales" runat="server"></span></h5>
                    </div>
                </div>
            </div>
        </div>

        <hr />

        <h2>Top Ordered Products</h2>
        <asp:GridView ID="gvTopProducts" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered">
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="Product" />
                <asp:BoundField DataField="TotalOrdered" HeaderText="Total Ordered" />
            </Columns>
        </asp:GridView>

        <hr />

        <h2>Least Ordered Products</h2>
        <asp:GridView ID="gvLeastProducts" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered">
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="Product" />
                <asp:BoundField DataField="TotalOrdered" HeaderText="Total Ordered" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
