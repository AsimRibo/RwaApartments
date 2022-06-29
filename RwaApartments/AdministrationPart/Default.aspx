<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AdministrationPart.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="container py-4">
        <%--Message output--%>
        <asp:Panel ID="panelMessage" runat="server" CssClass="container mt-5" Visible="false">
            <div class="alert alert-danger" role="alert">
                <label>Check user credentials.</label>
            </div>
        </asp:Panel>


        <asp:Panel ID="panelForm" runat="server" Visible="true">
            <%--FORM--%>
            <fieldset class="p-4">
                <legend>Login</legend>
                <div class="mb-3">
                    <asp:Label runat="server" ID="lblUsername" Text="Username" CssClass="form-label"></asp:Label>
                    <asp:TextBox runat="server" ID="txtUsername" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfdUsername" ControlToValidate="txtUsername" Display="Dynamic" ForeColor="Red">* Username can't be left empty.</asp:RequiredFieldValidator>
                </div>
                <div class="mb-3">
                    <asp:Label runat="server" ID="lblPassword" Text="Password" CssClass="form-label"></asp:Label>
                    <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfdPassword" ControlToValidate="txtPassword" Display="Dynamic" ForeColor="Red">* Password can't be left empty.</asp:RequiredFieldValidator>
                </div>
                <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary" OnClick="BtnLogin_Click" />
            </fieldset>
        </asp:Panel>
    </div>
</asp:Content>
