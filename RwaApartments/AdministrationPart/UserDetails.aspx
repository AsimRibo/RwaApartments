<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPage.Master" AutoEventWireup="true" CodeBehind="UserDetails.aspx.cs" Inherits="AdministrationPart.UserDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.5.0/font/bootstrap-icons.css">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="container">
        <div class="row border rounded-3 w-50 m-auto">
            <div class="col">
                <div class="row border-bottom p-2">
                    <asp:Label runat="server" ID="lblUsername" Text="Username" CssClass="text-center fw-bold"></asp:Label>
                </div>
                <div class="row border-bottom p-2 text-center">
                    <asp:Label runat="server" ID="lblAddress" Text="Address"></asp:Label>
                </div>
                <div class="row border-bottom p-2">
                    <div class="row text-center">
                        <div class="col">
                            <label><b>Email</b></label>
                        </div>
                        <div class="col">
                            <label><b>Confirmed</b></label>
                        </div>
                    </div>
                    <div class="row text-center">
                        <div class="col">
                            <asp:Label runat="server" ID="lblEmail">roki@gmail.com</asp:Label>
                        </div>
                        <div class="col">
                            <asp:Label runat="server" ID="lblConfirmedEmail" Visible="false"><i class="bi bi-check-circle"></i></asp:Label>
                            <asp:Label runat="server" ID="lblNotConfirmedEmail" Visible="false"><i class="bi bi-x-circle"></i></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="row border-bottom p-2">
                    <div class="row text-center">
                        <div class="col">
                            <label><b>Phone</b></label>
                        </div>
                        <div class="col">
                            <label><b>Confirmed</b></label>
                        </div>
                    </div>
                    <div class="row text-center">
                        <div class="col">
                            <asp:Label runat="server" ID="lblPhone">32445455654332</asp:Label>
                        </div>
                        <div class="col">
                            <asp:Label runat="server" ID="lblConfirmedPhone" Visible="false"><i class="bi bi-check-circle"></i></asp:Label>
                            <asp:Label runat="server" ID="lblNotConfirmedPhone" Visible="false"><i class="bi bi-x-circle"></i></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="row p-2">
                    <div class="row text-center">
                        <div class="col">
                            <label><b>Created at</b></label>
                        </div>
                        <div class="col">
                            <label><b>Deleted at</b></label>
                        </div>
                    </div>
                    <div class="row text-center">
                        <div class="col">
                            <asp:Label runat="server" ID="lblCreatedAt"></asp:Label>
                        </div>
                        <div class="col">
                            <asp:Label runat="server" ID="lblDeletedAt"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
