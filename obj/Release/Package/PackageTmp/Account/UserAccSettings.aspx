<%@ Page Title="Account Settings" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserAccSettings.aspx.cs" Inherits="BakeryMS.Account.UserAccSettings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row">
            <!-- Left Navigation Bar -->
            <div class="col-lg-3">
                <div class="list-group">
                    <a href="#userDetails" class="list-group-item list-group-item-action">User Details</a>
                    <a href="#editDetails" class="list-group-item list-group-item-action">Edit Details</a>
                    <a href="#changePassword" class="list-group-item list-group-item-action">Change Password</a>
                    <a href="#deactivateAccount" class="list-group-item list-group-item-action">Deactivate Account</a>
                </div>
            </div>

            <!-- Main Content  -->
            <div class="col-lg-9">
                <h2 class="mb-5">Account Settings</h2>

                <!-- User Details  -->
                <div id="userDetails">
                    <h3>User Details</h3>
                    <div class="form-group">
                        <label for="lblFirstName">First Name:</label>
                        <asp:Label ID="lblFirstName" runat="server" CssClass="form-control"></asp:Label>
                    </div>
                    <div class="form-group">
                        <label for="lblLastName">Last Name:</label>
                        <asp:Label ID="lblLastName" runat="server" CssClass="form-control"></asp:Label>
                    </div>
                    <div class="form-group">
                        <label for="lblEmail">Email:</label>
                        <asp:Label ID="lblEmail" runat="server" CssClass="form-control"></asp:Label>
                    </div>
                    <div class="form-group">
                        <label for="lblRole">Role:</label>
                        <asp:Label ID="lblRole" runat="server" CssClass="form-control"></asp:Label>
                    </div>
                </div>

                <!-- Edit Details  -->
                <div id="editDetails" class="mt-5">
                    <h3>Edit Details</h3>
                    <div class="form-group">
                        <label for="txtFirstName">First Name:</label>
                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName"
                            ErrorMessage="First Name is required." Display="Dynamic" ForeColor="Red" ValidationGroup="userDetails"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revFirstName" runat="server" ControlToValidate="txtFirstName"
                            ValidationExpression="^[A-Za-z\s]{2,50}$" ErrorMessage="Invalid First Name." Display="Dynamic" ForeColor="Red" ValidationGroup="userDetails"></asp:RegularExpressionValidator>
                    </div>
                    <div class="form-group">
                        <label for="txtLastName">Last Name:</label>
                        <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName"
                            ErrorMessage="Last Name is required." Display="Dynamic" ForeColor="Red" ValidationGroup="userDetails"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revLastName" runat="server" ControlToValidate="txtLastName"
                            ValidationExpression="^[A-Za-z\s]{2,50}$" ErrorMessage="Invalid Last Name." Display="Dynamic" ForeColor="Red" ValidationGroup="userDetails"></asp:RegularExpressionValidator>
                    </div>
                    <div class="form-group mt-3">
                        <!-- Wrap label in its own div for full width -->
                        <div class="d-block mb-2">
                            <asp:Label ID="lblMessageUpdate" runat="server" CssClass="text-success" Text="Label" Visible="false"></asp:Label>
                        </div>
                        <asp:Button ID="btnUpdate" runat="server" Text="Update Details" CssClass="btn " style="background-color: #112D4E; color: white;"
                            ValidationGroup="userDetails" OnClick="btnUpdate_Click" />
                    </div>
                </div>

                <!-- Change Password  -->
                <div id="changePassword" class="mt-5">
                    <h3>Change Password</h3>
                    <div class="form-group">
                        <label for="txtCurrentPassword">Current Password:</label>
                        <asp:TextBox ID="txtCurrentPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="txtNewPassword">New Password:</label>
                        <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvNewPassword" runat="server" ControlToValidate="txtNewPassword"
                            ErrorMessage="Password is required." Display="Dynamic" ForeColor="Red" ValidationGroup="changePassword"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revNewPassword" runat="server" ControlToValidate="txtNewPassword"
                            ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$" ErrorMessage="Invalid Password." Display="Dynamic" ForeColor="Red" ValidationGroup="changePassword"></asp:RegularExpressionValidator>
                    </div>
                    <div class="form-group">
                        <label for="txtConfirmPassword">Confirm New Password:</label>
                        <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                        <asp:CompareValidator ID="cvPasswords" runat="server" ControlToValidate="txtConfirmPassword"
                            ControlToCompare="txtNewPassword" ErrorMessage="Passwords do not match."
                            Display="Dynamic" ForeColor="Red" ValidationGroup="changePassword"></asp:CompareValidator>
                        <asp:Label ID="lblPasswordMatch" runat="server" Text="Label" Visible="false"></asp:Label>
                    </div>
                    <div class="form-group mt-3">
                        <div class="d-block mb-2">
                            <asp:Label ID="lblMessagePassword" runat="server" CssClass="text-success" Text="" Visible="false"></asp:Label>
                        </div>
                        <asp:Button ID="btnChangePassword" runat="server" Text="Change Password" CssClass="btn" style="background-color: #112D4E; color: white;"
                            OnClick="btnChangePassword_Click" ValidationGroup="changePassword" />
                    </div>
                </div>


                <!-- Deactivate Account  -->
                <div id="deactivateAccount" class="mt-5">
                    <h3>Deactivate Account</h3>
                    <div class="form-group my-3">
                        <asp:Button ID="btnDeactivate" runat="server" Text="Deactivate Account" CssClass="btn btn-danger" OnClick="btnDeactivate_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
