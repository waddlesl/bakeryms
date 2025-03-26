<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SalesReport.aspx.cs" Inherits="BakeryMS.Admin.SalesReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function toggleInputs() {
            var reportType = document.querySelector('input[name="reportType"]:checked').value;
            document.getElementById('yearInputDiv').classList.toggle("d-none", reportType !== "yearly");
            document.getElementById('monthInputDiv').classList.toggle("d-none", reportType !== "monthly");
        }

        function printReport() {
            var reportContent = document.getElementById("reportContainer").innerHTML;
            var originalContent = document.body.innerHTML;
            document.body.innerHTML = reportContent;
            window.print();
            document.body.innerHTML = originalContent;
            location.reload();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" />
    <!-- ScriptManager added here -->

    <div class="container">
        <h2>Sales Report</h2>

        <div class="radio-group">
            <label>
                <input type="radio" name="reportType" value="daily" checked onclick="toggleInputs()" />
                Daily Report</label>
            <label>
                <input type="radio" name="reportType" value="monthly" onclick="toggleInputs()" />
                Monthly Report</label>
            <label>
                <input type="radio" name="reportType" value="yearly" onclick="toggleInputs()" />
                Yearly Report</label>
        </div>

        <div id="yearInputDiv" class="d-none">
            <label>Select Year:</label>
            <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control"></asp:DropDownList>
        </div>

        <div id="monthInputDiv" class="d-none">
            <label>Select Year:</label>
            <asp:DropDownList ID="ddlMonthYear" runat="server" CssClass="form-control"></asp:DropDownList>
            <label>Select Month:</label>
            <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control">
                <asp:ListItem Text="January" Value="01" />
                <asp:ListItem Text="February" Value="02" />
                <asp:ListItem Text="March" Value="03" />
                <asp:ListItem Text="April" Value="04" />
                <asp:ListItem Text="May" Value="05" />
                <asp:ListItem Text="June" Value="06" />
                <asp:ListItem Text="July" Value="07" />
                <asp:ListItem Text="August" Value="08" />
                <asp:ListItem Text="September" Value="09" />
                <asp:ListItem Text="October" Value="10" />
                <asp:ListItem Text="November" Value="11" />
                <asp:ListItem Text="December" Value="12" />
            </asp:DropDownList>
        </div>

        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <asp:Button ID="btnGenerateReport" runat="server" Text="Generate Report" CssClass="btn text-white" style="background-color: #112D4E;" OnClick="btnGenerateReport_Click" />
                <button type="button" class="btn btn-secondary" onclick="printReport()">Print Report</button>

                <div id="reportContainer" class="table-responsive mt-3">
                    <asp:GridView ID="gvSalesReport" runat="server" AutoGenerateColumns="true" CssClass="table table-striped table-bordered"></asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
