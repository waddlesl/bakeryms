<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logout.aspx.cs" Inherits="BakeryMS.Logout" %>

<!DOCTYPE html>
<html>
    <head runat="server">
        <title>Logging out...</title>
        <meta http-equiv="refresh" content="3;url=/Customer/Home.aspx" />
        <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet">
    </head>
    <body class="d-flex justify-content-center align-items-center vh-100">
        <div class="text-center">
            <h2>You have been logged out.</h2>
            <p>Redirecting to home page...</p>
            <a href="/Customer/Home.aspx" class="btn" style="background-color: #112D4E; color: white;">Go to home</a>
        </div>
    </body>
</html>