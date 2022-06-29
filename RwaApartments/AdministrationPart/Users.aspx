<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPage.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="AdministrationPart.Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="container py-4">
        <div class="row">
            <asp:Repeater runat="server" ID="rptUsers">
                <HeaderTemplate>
                    <table id="myTable" class="table">
                        <thead>
                            <tr>
                                <th scope="col">#</th>
                                <th scope="col">Email</th>
                                <th scope="col">Username</th>
                                <th scope="col">Created at</th>
                                <th scope="col">Phone</th>
                                <th scope="col"></th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <th scope="row"><%# Eval(nameof(RwaUtilities.Models.User.Id)) %></th>
                        <td><%# Eval(nameof(RwaUtilities.Models.User.Email)) %></td>
                        <td><%# Eval(nameof(RwaUtilities.Models.User.UserName)) %></td>
                        <td><%# Eval(nameof(RwaUtilities.Models.User.CreatedAt)) %></td>
                        <td><%# Eval(nameof(RwaUtilities.Models.User.PhoneNumber)) %></td>
                        <td>
                            <asp:LinkButton runat="server" ID="btnDetails" Text="Details" OnClick="btnDetails_Click" CommandArgument="<%# Eval(nameof(RwaUtilities.Models.User.Id)) %>" CssClass="btn btn-secondary"></asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
