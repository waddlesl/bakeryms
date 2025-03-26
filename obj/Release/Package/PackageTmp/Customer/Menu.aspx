<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="BakeryMS.Customer.Menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView ID="gvProducts" runat="server" AutoGenerateColumns="False"
        OnRowCommand="gvProducts_RowCommand" CssClass="table table-striped table-bordered">
        <Columns>
            <asp:TemplateField HeaderText="Image">
                <ItemTemplate>
                    <asp:Image ID="imgProduct" runat="server" CssClass="img-thumbnail"
                        ImageUrl='<%# "~/images/" + Eval("Name").ToString() + ".jpg" %>'
                        AlternateText='<%# Eval("Name") %>'
                        Width="100px" Height="100px" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Name" HeaderText="Product" />
            <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="₱{0:N2}" />
            <asp:TemplateField HeaderText="Quantity">
                <ItemTemplate>
                    <asp:TextBox ID="txtQuantity" runat="server" Text="1"
                        CssClass="form-control" Style="width: 70px;"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="btnAdd" runat="server" CommandName="AddToCart"
                        CommandArgument='<%# Eval("ProductID") %>' Text="Add to Cart"
                        CssClass="btn text-white" Style="background-color: #112D4E;" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>


</asp:Content>
