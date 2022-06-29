<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPage.Master" AutoEventWireup="true" CodeBehind="Apartments.aspx.cs" Inherits="AdministrationPart.Apartments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="container py-4">

        <div>
            <asp:Button ID="btnAddApartment" runat="server" CssClass="btn btn-success mb-2" Text="Add apartment" OnClick="btnAddApartment_Click" />
        </div>

        <div class="row">
            <asp:Repeater runat="server" ID="rptApartments">
                <HeaderTemplate>
                    <table id="myTable" class="table">
                        <thead>
                            <tr>
                                <th scope="col">#</th>
                                <th scope="col">Name</th>
                                <th scope="col">City</th>
                                <th scope="col">Price</th>
                                <th scope="col">Status</th>
                                <th scope="col">Total rooms</th>
                                <th scope="col">Max adults</th>
                                <th scope="col"></th>
                                <th scope="col"></th>
                                <th scope="col"></th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <th scope="row"><%# Eval(nameof(RwaUtilities.Models.Apartment.Id)) %></th>
                        <td><%# Eval(nameof(RwaUtilities.Models.Apartment.NameEng)) %></td>
                        <td><%# Eval(nameof(RwaUtilities.Models.Apartment.City)) %></td>
                        <td><%# Eval(nameof(RwaUtilities.Models.Apartment.Price)) %></td>
                        <td><%# Eval(nameof(RwaUtilities.Models.Apartment.Status)) %></td>
                        <td><%# Eval(nameof(RwaUtilities.Models.Apartment.TotalRooms)) %></td>
                        <td><%# Eval(nameof(RwaUtilities.Models.Apartment.MaxAdults)) %></td>
                        <td>
                            <asp:LinkButton runat="server" ID="btnDetails" Text="Details" OnClick="btnDetails_Click" CommandArgument="<%# Eval(nameof(RwaUtilities.Models.Apartment.Id)) %>" CssClass="btn btn-primary"></asp:LinkButton>
                        </td>
                        <td>
                            <asp:LinkButton runat="server" ID="btnUpdate" Text="Update" OnClick="btnUpdate_Click" CommandArgument="<%# Eval(nameof(RwaUtilities.Models.Apartment.Id)) %>" CssClass="btn btn-secondary"></asp:LinkButton>
                        </td>
                        <td>
                            <asp:LinkButton runat="server" ID="btnDelete" Text="Delete" OnClick="btnDelete_Click" CommandArgument="<%# Eval(nameof(RwaUtilities.Models.Apartment.Id)) %>" CssClass="btn btn-danger" OnClientClick="return confirm('Are you sure you want to delete this apartment?');"></asp:LinkButton>
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
