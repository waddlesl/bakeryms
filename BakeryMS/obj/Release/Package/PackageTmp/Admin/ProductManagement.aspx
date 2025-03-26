<%@ Page Title="Product Management" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductManagement.aspx.cs" Inherits="BakeryMS.Admin.ProductManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-6">
        <div class="row">
            <!-- Left: Add Product Form -->
            <h2>Manage Products</h2>
            <div class="col-lg-4">
                <div class="card">
                    <div class="card-header text-white" style="background-color: #112D4E;">
                        <h5 class="mb-0">Add New Product</h5>
                    </div>
                    <div class="card-body">
                        <div class="mb-3">
                            <label class="form-label">Product Name</label>
                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <label class="form-label">Price</label>
                            <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <label class="form-label">Stock</label>
                            <asp:TextBox ID="txtStock" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <label class="form-label">Category</label>
                            <asp:DropDownList ID="drpCategory" runat="server" CssClass="form-control">
                                <asp:ListItem>Classic &amp; Everyday Breads</asp:ListItem>
                                <asp:ListItem>Sweet &amp; Filled Pastries</asp:ListItem>
                                <asp:ListItem>Crunchy &amp; Toasted Treats</asp:ListItem>
                                <asp:ListItem>Spreads</asp:ListItem>
                                <asp:ListItem>Drinks</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <asp:Button ID="btnAdd" runat="server" Text="Add Product" CssClass="btn w-100 text-white" style="background-color: #112D4E;" OnClick="btnAdd_Click" />
                    </div>
                </div>
            </div>

            <!-- Right: Product Table -->
            <div class="col-lg-8">
                <div class="card">
                    <div class="card-header text-white" style="background-color: #3F72AF;"> 
                        <h5 class="mb-0">Product List</h5>
                    </div>
                    <div class="card-body">
                        <asp:GridView ID="gvProducts" runat="server" AutoGenerateColumns="False" DataKeyNames="ProductID"
                            CssClass="table table-bordered table-striped table-hover"
                            AllowPaging="True" PageSize="10"
                            AllowSorting="True" OnSorting="gvProducts_Sorting"
                            OnPageIndexChanging="gvProducts_PageIndexChanging"
                            OnRowEditing="gvProducts_RowEditing" OnRowCancelingEdit="gvProducts_RowCancelingEdit"
                            OnRowUpdating="gvProducts_RowUpdating" OnRowDeleting="gvProducts_RowDeleting">
                            <Columns>
                                <asp:BoundField DataField="ProductID" HeaderText="ID" SortExpression="ProductID" ReadOnly="true" />
                                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                                <asp:BoundField DataField="Price" HeaderText="Price" SortExpression="Price" />
                                <asp:BoundField DataField="Stock" HeaderText="Stock" SortExpression="Stock" />
                                <asp:BoundField DataField="Category" HeaderText="Category" SortExpression="Category" />
                                <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" EditText="Edit" DeleteText="Delete" />
                            </Columns>
                        </asp:GridView>


                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
