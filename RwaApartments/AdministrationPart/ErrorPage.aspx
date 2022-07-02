<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="AdministrationPart.ErrorPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Error</title>

    <link href="Content/bootstrap.min.css" rel="stylesheet"/>

</head>
<body>
    <form id="form1" class="container text-center" runat="server">
        <h1>BAD REQUEST - Status code: 400</h1>
        <p>Something went wrong!</p>
        <asp:LinkButton runat="server" ID="btnBack" OnClick="btnBack_Click" CssClass="btn btn-primary" Text="Back to home page"></asp:LinkButton>
    </form>
</body>
</html>
