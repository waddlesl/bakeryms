<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminAccManagement.aspx.cs" Inherits="BakeryMS.Admin.AdminAccManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h2>Manage Admin</h2>

        <div class="row mb-5">
            <!-- Admin Form -->
            <div class="col-lg-4">
                <div class="card">
                    <div class="card-header text-white bg-dark">
                        <h5 class="my-1">Admin Form</h5>
                    </div>
                    <div class="card-body">
                        <div class="mb-3">
                            <asp:Label ID="lblAdminID" runat="server" Text="Admin ID (For Update):" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="txtAdminID" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="mb-3">
                            <asp:Label ID="lblAdminFName" runat="server" Text="First Name:" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="txtAdminFName" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvAdminFName" runat="server" ControlToValidate="txtAdminFName"
                                ErrorMessage="First Name is required." Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revAdminFName" runat="server" ControlToValidate="txtAdminFName"
                                ValidationExpression="^[A-Za-z\s]{2,50}$" ErrorMessage="Invalid First Name." Display="Dynamic" ForeColor="Red"></asp:RegularExpressionValidator>
                        </div>

                        <div class="mb-3">
                            <asp:Label ID="lblAdminLName" runat="server" Text="Last Name:" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="txtAdminLName" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvAdminLName" runat="server" ControlToValidate="txtAdminLName"
                                ErrorMessage="Last Name is required." Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revAdminLName" runat="server" ControlToValidate="txtAdminLName"
                                ValidationExpression="^[A-Za-z\s]{2,50}$" ErrorMessage="Invalid Last Name." Display="Dynamic" ForeColor="Red"></asp:RegularExpressionValidator>
                        </div>

                        <div class="mb-3">
                            <asp:Label ID="lblAdminEmail" runat="server" Text="Email:" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="txtAdminEmail" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvAdminEmail" runat="server" ControlToValidate="txtAdminEmail"
                                ErrorMessage="Email is required." Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revAdminEmail" runat="server" ControlToValidate="txtAdminEmail"
                                ValidationExpression="^[^\s@]+@[^\s@]+\.[^\s@]+$" ErrorMessage="Invalid Email." Display="Dynamic" ForeColor="Red"></asp:RegularExpressionValidator>
                        </div>

                        <div class="mb-3">
                            <asp:Label ID="lblAdminPassword" runat="server" Text="Password:" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="txtAdminPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvAdminPassword" runat="server" ControlToValidate="txtAdminPassword"
                                ErrorMessage="Password is required." Display="Dynamic" ForeColor="Red" Enabled="false"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revAdminPassword" runat="server" ControlToValidate="txtAdminPassword"
                                ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$" ErrorMessage="Invalid Password." Display="Dynamic" ForeColor="Red" Enabled="false"></asp:RegularExpressionValidator>
                        </div>

                        <div class="mb-3">
                            <asp:Label ID="lblAdminStatus" runat="server" Text="Status:" CssClass="form-label"></asp:Label>
                            <asp:DropDownList ID="drpAdminStatus" runat="server" CssClass="form-select">
                                <asp:ListItem>Active</asp:ListItem>
                                <asp:ListItem>Inactive</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <div class="d-flex gap-2 mt-3">
                            <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" CssClass="btn btn-dark" />
                            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" CssClass="btn btn-warning" />
                        </div>

                        <div class="mt-3">
                            <asp:Label ID="lblAdminMessage" runat="server" Text="" Visible="false" CssClass="text-danger"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Admin List -->
            <div class="col-lg-8">
                <div class="card">
                    <div class="card-header text-white" style="background-color: #3F72AF;">
                        <h5 class="my-1">Admin List</h5>
                    </div>
                    <div class="card-body">
                        <div class="form-group">
                            <asp:Label ID="lblAdminSearch" runat="server" Text="Search (ID or Name):" CssClass="form-label"></asp:Label>
                            <div class="input-group">
                                <asp:TextBox ID="txtAdminSearch" runat="server" CssClass="form-control"></asp:TextBox>
                                <div class="input-group-append">
                                    <asp:Button ID="btnAdminSearch" runat="server" Text="Search" OnClick="btnAdminSearch_Click" CausesValidation="False" CssClass="btn text-white" Style="background-color: #3F72AF;" />
                                </div>
                            </div>
                        </div>

                        <asp:GridView ID="gvAdmin" runat="server" class="table table-striped table-bordered"></asp:GridView>
                    </div>
                </div>
            </div>
        </div>

        <!-- Customer List -->
        <div class="row">
            <div class="col-12">
                <div class="card">
                    <div class="card-header" style="background-color: #DBE2EF;">
                        <h5 class="my-1">Customer List</h5>
                    </div>
                    <div class="card-body">
                        <div class="form-group">
                            <asp:Label ID="lblCusSearch" runat="server" Text="Search (ID or Name):" CssClass="form-label"></asp:Label>
                            <div class="input-group">
                                <asp:TextBox ID="txtCusSearch" runat="server" CssClass="form-control"></asp:TextBox>
                                <div class="input-group-append">
                                    <asp:Button ID="btnCusSearch" runat="server" Text="Search" OnClick="btnCusSearch_Click" CausesValidation="False" CssClass="btn" Style="background-color: #DBE2EF;" />
                                </div>
                            </div>
                        </div>

                        <asp:GridView ID="gvCus" runat="server" class="table table-striped table-bordered"></asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
